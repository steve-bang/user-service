/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Domain.Exception;

public class PermissionCodeAlreadyTakenException : ConflictException
{
    public PermissionCodeAlreadyTakenException(string code) : base(
        PermissionErrorCodes.CodeAlreadyTaken,
        string.Format(PermissionErrorMessages.CodeAlreadyTaken, code)
    )
    { }

}