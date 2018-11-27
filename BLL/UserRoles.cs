using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public static class UserRoles
    {
        public static readonly List<Object> Roles = new List<object>
        {
            new {value = "User", text = "User"},
            new {value = "Editor", text = "Editor"},
            new {value = "Moderator", text = "Moderator"},
            new {value = "Admin", text = "Admin"}
        };
        public const string User = "User";
        public const string Editor = "Editor";
        public const string Moderator = "Moderator";
        public const string Admin = "Admin";
    }
}