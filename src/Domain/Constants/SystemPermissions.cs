/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Constants;

public static class SystemPermissions
{
    public static class Users
    {
        public const string View = "users.view";
        public const string Create = "users.create";
        public const string Edit = "users.edit";
        public const string Delete = "users.delete";
        public const string Impersonate = "users.impersonate";
    }

    public static class Roles
    {
        public const string View = "roles.view";
        public const string Create = "roles.create";
        public const string Edit = "roles.edit";
        public const string Delete = "roles.delete";
        public const string Assign = "roles.assign";
    }

    public static class Permissions
    {
        public const string View = "permissions.view";
        public const string Manage = "permissions.manage";
    }

    // Add more categories as needed
}