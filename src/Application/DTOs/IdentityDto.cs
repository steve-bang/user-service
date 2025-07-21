/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.DTO;

public class IdentityDto
{
    public Guid Id { get; set; }
    public string Provider { get; set; } = null!;
    public DateTime? LastLoginAt { get; set; }
    public IDictionary<string, object>? IdentityData { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CreatedAt { get; set; }

}