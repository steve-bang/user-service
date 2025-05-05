/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.UserService.Application.DTO;

/// <summary>
/// Represents the data required to register a new user.
/// This class is typically used in user registration APIs or forms.
/// </summary>
public class RegisterUserRequest
{
    /// <summary>
    /// The email address of the user.
    /// Must be provided and follow a valid email format.
    /// </summary>
    public string EmailAddress { get; set; } = null!;

    /// <summary>
    /// The password chosen by the user.
    /// Must be provided.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Confirmation of the user's password.
    /// Must match the Password field (typically validated separately).
    /// </summary>
    public string ConfirmPassword { get; set; } = null!;

    /// <summary>
    /// The user's first name.
    /// Must be provided.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// The user's last name.
    /// Must be provided.
    /// </summary>
    public string LastName { get; set; } = null!;
}
