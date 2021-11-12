using System;
using WepProject.Database;
using System.Collections.Generic;

namespace WepProject.Models
{
    public static class VerifyCredential
    {
        public static User IsValidCredentials(string email, string senha)
        {
            var connection = new ConnectionWithSqlServer();
            User user = connection.GetUsersByCredentials(email, senha);
            return (user != null) ? user : null;
        }

        public static bool HasEmail(string email)
        {
            var connection = new ConnectionWithSqlServer();
            int rows = connection.HasEmail(email);
            return (rows > 0) ? true : false;
        }

    }
}
