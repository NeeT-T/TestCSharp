using System;

namespace WepProject.Models
{
    public class UserCredential
    {
        public string Email { get; }
        public string Password { get; }

        public UserCredential(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
