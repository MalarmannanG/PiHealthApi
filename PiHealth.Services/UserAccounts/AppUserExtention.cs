using PiHealth.DataModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiHealth.Services.UserAccounts
{
    public static class AppUserExtention
    {
        public static string GetUsersName(this long userId, List<AppUser> appuser)
        {           
            var user = appuser.FirstOrDefault(a=>a.Id == userId);
            if (user == null)
                return "";
            else
                return user.Name;
        }

        public static string GetUserName(this long? userId, List<AppUser> appuser)
        {
            if (userId == null)
                return "";
            return GetUsersName(userId.Value, appuser);
        }
    }
}
