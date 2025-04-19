/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Common;
using Steve.ManagerHero.UserService.Domain.Constants;
using Steve.ManagerHero.UserService.Domain.Events;
using Steve.ManagerHero.UserService.Domain.Exceptions;
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
    public bool IsPhoneVerified { get; private set; }

    // Timestamps
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation Properties
    private readonly List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();


    private readonly List<Session> _sessions = new();
    public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

    //private readonly List<RefreshToken> _refreshTokens = new();
    //public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    public User() { }

    // Constructor for new user registration
    private User(
        string firstName,
        string lastName,
        EmailAddress emailAddress,
        PasswordHash passwordHash
    )
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
    public static User Create(
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

        return new User(firstName, lastName, emailAddress, passwordHash);
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
        if (IsEmailVerified) return;

        IsEmailVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void VerifyPhone()
    {
        if (PhoneNumber == null)
            throw new InvalidOperationException("Phone number not set");

        IsPhoneVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }

    // public void AddRole(Role role)
    // {
    //     if (_roles.Any(r => r.Id == role.Id))
    //         return;

    //     _roles.Add(role);
    //     UpdatedAt = DateTime.UtcNow;
    // }

    // public void RemoveRole(Role role)
    // {
    //     _roles.RemoveAll(r => r.Id == role.Id);
    //     UpdatedAt = DateTime.UtcNow;
    // }

    // public bool HasPermission(string permissionCode)
    // {
    //     return _roles.Any(r => r.RolePermissions.Any(p => p.Permission.Code == permissionCode));
    // }

    public void RecordLogin()
    {
        LastLoginDate = DateTime.UtcNow;
    }

    public void LoginPassword(string passwordRequest)
    {
        bool isCorrectPassword = PasswordHash.Verify(passwordRequest);

        if (!isCorrectPassword)
            throw ExceptionProviders.User.LoginPasswordFailedException;

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