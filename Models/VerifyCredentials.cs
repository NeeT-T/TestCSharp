using System;
using WepProject.Repository;
using System.Collections.Generic;

namespace WepProject.Models
{
    public static class VerifyCredential
    {
        public static User IsValidCredentials(string email, string senha)
        {
            User user = new AuthorizationRepository().GetUserByCredentials(email, senha);
            return (user != null) ? user : null;
        }

    }
}
