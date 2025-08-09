/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;
using Steve.ManagerHero.UserService.Domain.Entities;
using Steve.ManagerHero.UserService.Domain.Events;
using Steve.ManagerHero.UserService.Domain.Services;
using Steve.ManagerHero.UserService.Domain.ValueObjects;
using Steve.ManagerHero.UserService.Infrastructure.Security;

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

/// <summary>
/// This class represents a user in the system.
/// </summary>
public class User : AggregateRoot
{
    /// <summary>
    /// The key of the user current logged
    /// </summary>
    public const string CurrentUserKey = "me";

    // Personal Information
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string DisplayName { get; private set; }

    // Contact Information
    public EmailAddress EmailAddress { get; private set; }
    public EmailAddress? SecondaryEmailAddress { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }

    // Authentication
    public string PasswordHash { get; private set; } = default!;
    public string PasswordSalt { get; private set; } = default!;
    public DateTime? PasswordChangedDate { get; private set; }
    public DateTime? LastLoginDate { get; private set; }

    // Address
    public Address? Address { get; private set; }

    // Status
    public UserStatus Status { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public DateTime? EmailVerifiedAt { get; private set; }
    public bool IsPhoneVerified { get; private set; }
    public DateTime? PhoneVerifiedAt { get; private set; }

    // Timestamps
    public DateTime? UpdatedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Navigation Properties
    private readonly List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();


    private readonly List<Session> _sessions = new();
    public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

    private readonly List<UserIdentity> _identities = new();
    public IReadOnlyCollection<UserIdentity> Identities => _identities.AsReadOnly();

    private readonly List<PasswordHistoryEntity> _passwordHistories = new();
    public IReadOnlyCollection<PasswordHistoryEntity> PasswordHistories => _passwordHistories.AsReadOnly();

    public User() : base() { }

    // Constructor for new user registration
    private User(
        string firstName,
        string lastName,
        EmailAddress emailAddress,
        string passwordHash,
        string passwordSalt
    ) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        DisplayName = $"{firstName} {lastName}";
        Status = UserStatus.Active;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;

        AddEvent(new RegistrationEvent(this));
    }

    // Factory method for creating new user
    public static User Register(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        string passwordSalt
    )
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidOperationException("First name cannot be empty");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidOperationException("Last name cannot be empty");

        var emailAddress = new EmailAddress(email);

        var user = new User(firstName, lastName, emailAddress, passwordHash, passwordSalt);

        user._identities.Add(UserIdentity.RegisterByEmail(user));

        return user;
    }

    public static User RegisterFromExternal(
        string displayName,
        string email
    )
    {
        var user = new User()
        {
            EmailAddress = new EmailAddress(email),
            DisplayName = displayName,
            FirstName = string.Empty,
            LastName = string.Empty
        };
        user.VerifyEmail();

        user._identities.Add(UserIdentity.RegisterByEmail(user));

        return user;
    }

    public void Update(
        string emailAddress,
        string firstName,
        string lastName,
        string displayName,
        string? secondaryEmailAddress,
        string? phoneNumber,
        Address? address
    )
    {
        UpdateName(firstName, lastName);
        if (!string.IsNullOrEmpty(displayName))
            DisplayName = displayName;


        UpdateEmail(new EmailAddress(emailAddress));

        if (!string.IsNullOrEmpty(secondaryEmailAddress))
        {
            SecondaryEmailAddress = new EmailAddress(secondaryEmailAddress);
        }


        if (!string.IsNullOrEmpty(phoneNumber))
            UpdatePhoneNumber(new PhoneNumber(phoneNumber));


        if (address != null)
            Address = address;

        UpdatedAt = DateTime.UtcNow.ToUniversalTime();
    }

    // Domain Methods
    public void UpdateName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new InvalidOperationException("First and last names cannot be empty");

        FirstName = firstName;
        LastName = lastName;
        DisplayName = $"{firstName} {lastName}";
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateEmail(EmailAddress emailAddress)
    {
        if (EmailAddress == emailAddress) return;

        EmailAddress = emailAddress;
        IsEmailVerified = false;
        UpdatedAt = DateTime.UtcNow;

        AddEvent(new UserEmailChangedEvent(this));
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;
        IsPhoneVerified = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string currentPassword, string newPassword, IPasswordHasher passwordHasher, IPasswordHistoryPolicyService? policy = null)
    {
        // Verify password
        if (passwordHasher.Verify(currentPassword, PasswordHash, PasswordSalt) == false)
            throw new PasswordIncorrectException();

        // Reset password
        ResetPassword(newPassword, passwordHasher, policy);
    }

    public void ResetPassword(string newPassword, IPasswordHasher passwordHasher, IPasswordHistoryPolicyService? policy = null)
    {
        // Hash new password
        (string passwordHash, string passwordSalt) = passwordHasher.Hash(newPassword);

        if (policy != null)
        {
            // policy will check reuse
            if (policy.IsPasswordUsed(this, newPassword))
                throw new ConflictException(UserErrorCodes.PasswordUsed, UserErrorMessages.PasswordUsedMessage);


            // add the current password to history BEFORE updating (or keep ordering consistent with your policy)
            _passwordHistories.Add(new PasswordHistoryEntity(Id, passwordHash, passwordSalt));

            if (_passwordHistories.Count != 1)
                // trim to policy count
                _passwordHistories.RemoveRange(policy.MaxHistoryCount, Math.Max(0, _passwordHistories.Count - policy.MaxHistoryCount));
        }

        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;

        PasswordChangedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddEvent(new UserPasswordChangedEvent(this));
    }


    public void VerifyEmail()
    {
        if (IsEmailVerified) throw new EmailAlreadyVerifiedException();

        IsEmailVerified = true;
        EmailVerifiedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void VerifyPhone()
    {
        if (PhoneNumber == null)
            throw new InvalidOperationException("Phone number not set");

        IsPhoneVerified = true;
        PhoneVerifiedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddIdentity(UserIdentity identity)
    {
        var identityExists = _identities.FirstOrDefault(i => i.Provider == identity.Provider && i.ProviderId == identity.ProviderId);
        if (identityExists == null)
            _identities.Add(identity);
    }


    /// <summary>
    /// Add a role
    /// </summary>
    /// <param name="role">The role object to add</param>
    /// <exception cref="UserAlreadyHasRoleException">Throw if the user has already has role</exception>
    public void AddRole(Role role)
    {
        if (_userRoles.Any(r => r.RoleId == role.Id))
            throw new UserAlreadyHasRoleException();

        var userRole = new UserRole(this, role);
        _userRoles.Add(userRole);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveRole(Role role)
    {
        var userRole = _userRoles.FirstOrDefault(ur => ur.RoleId == role.Id);
        if (userRole == null)
        {
            throw new InvalidOperationException("User does not have this role.");
        }

        _userRoles.Remove(userRole);
    }

    public void RemoveRoles(IEnumerable<Role> roles)
    {
        foreach (var role in roles)
        {
            RemoveRole(role);
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void LoginPassword(string passwordRequest)
    {
        LastLoginDate = DateTime.UtcNow;
    }

    public void Login(IdentityProvider provider)
    {
        var identityExists = _identities.FirstOrDefault(i => i.Provider == provider);
        if (identityExists != null)
        {
            identityExists.Login();
        }

        LastLoginDate = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive) return;

        IsActive = false;
        Status = UserStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;

        // Revoke all active sessions
        //_sessions.ForEach(s => s.Deactivate());

        AddEvent(new UserDeactivatedEvent(this));
    }

    public void Activate()
    {
        if (IsActive) return;

        IsActive = true;
        Status = UserStatus.Active;
        UpdatedAt = DateTime.UtcNow;

        AddEvent(new UserActivatedEvent(this));
    }
}