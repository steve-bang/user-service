/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.ValueObjects;

namespace Steve.ManagerHero.UserService.Application.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    public string EmailAddress { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? DisplayName { get; set; }
    public string? SecondaryEmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public Address? Address { get; set; }
    public bool IsEmailVerified { get; set; }
    public DateTime? EmailVerifiedAt { get; set; }
    public bool IsPhoneVerified { get; set; }
    public DateTime? PhoneVerifiedAt { get; set; }
    public bool IsActive { get; set; }
    
    public List<IdentityDto> Identities { get; set; }

    public UserDto() { }

}