/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService;

public class ProjectSettings
{
    /// <summary>
    /// The title of the project.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// The domain of the project.
    /// </summary>
    public string Domain { get; set; } = null!;

    /// <summary>
    /// The link of the verification action. When the user register a new user, we send the email address for verification their email.
    /// </summary>
    public string VerificationLink { get; set; } = null!;

    /// <summary>
    /// The link of the reset password action.
    /// </summary>
    public string ResetPassword { get; set; } = null!;
}