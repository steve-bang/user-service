/*
* Author: Steve Bang
* History:
* - [2025-08-10] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.BuildingBlocks.CQRS;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public record LoginPhoneOtpRequestQuery(
    string PhoneNumber
) : IQuery<string>;