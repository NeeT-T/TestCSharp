using System;
using WepProject.Database;
using System.Collections.Generic;
using WepProject.Interfaces.Connection;

namespace WepProject.Models
{
    public class LoginValidation
    {
        public LoginValidation() { }

        public bool isValidCredentials(UserCredential userCredential)
        {
            //IConnection connection = new ConnectionWithXmlData();
            IConnection connection = new ConnectionWithSqlServer();
            int rows = connection.CheckUsersByCredentials(userCredential.Email, userCredential.Password);
            return (rows > 0) ? true : false;
            //return credentials.Exists(user => user.Email == userCredential.Email && user.Password == userCredential.Password);
        }
    }

}
