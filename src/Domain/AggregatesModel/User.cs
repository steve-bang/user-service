/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;
using Steve.ManagerHero.UserService.Domain.Events;
using Steve.ManagerHero.UserService.Domain.ValueObjects;

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
    public PasswordHash PasswordHash { get; private set; }
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

    public string[] RoleNames => _userRoles.Any() ? _userRoles.Select(ur => ur.Role).Select(r => r.Name).ToArray()
        : [];

    private readonly List<UserIdentity> _identities = new();
    public IReadOnlyCollection<UserIdentity> Identities => _identities.AsReadOnly();

    //private readonly List<RefreshToken> _refreshTokens = new();
    //public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    public User() : base() { }

    // Constructor for new user registration
    private User(
        string firstName,
        string lastName,
        EmailAddress emailAddress,
        PasswordHash passwordHash
    ) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        PasswordHash = passwordHash;
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
        string password
    )
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidOperationException("First name cannot be empty");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidOperationException("Last name cannot be empty");

        var emailAddress = new EmailAddress(email);
        var passwordHash = PasswordHash.Create(password);

        var user = new User(firstName, lastName, emailAddress, passwordHash);

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

    public void ChangePassword(string currentPassword, string newPassword)
    {
        if (PasswordHash.Verify(currentPassword) == false)
            throw new PasswordIncorrectException();

        UpdatePassword(newPassword);
    }

    public void UpdatePassword(string newPassword)
    {
        var newPasswordHash = PasswordHash.Create(newPassword);
        PasswordHash = newPasswordHash;
        PasswordChangedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        // Revoke all session
        _sessions.Clear();

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

    // public bool HasPermission(string permissionCode)
    // {
    //     return _roles.Any(r => r.RolePermissions.Any(p => p.Permission.Code == permissionCode));
    // }

    public void LoginPassword(string passwordRequest)
    {
        bool isCorrectPassword = PasswordHash.Verify(passwordRequest);

        if (!isCorrectPassword)
            throw new InvalidCredentialException();

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