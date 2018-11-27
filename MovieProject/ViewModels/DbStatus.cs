using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject
{
    public static class DbStatus
    {
        public const string UserToEditor = "pending for validation";
        public const string EditorToManager = "pending for approval";
        public const string ManagerToUserGood = "approved";
        public const string ManagerToUserBad = "not-approved";
        public const string ManagerToEditor = "denied";
        public const string EditorToUser = "rejected";
        public const string Cancelled = "cancelled";


    }
}
