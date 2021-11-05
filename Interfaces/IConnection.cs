using System;
using System.Collections.Generic;
using WepProject.Models;

namespace WepProject.Interfaces.Connection
{
    public interface IConnection
    {
        public int CheckUsersByCredentials(string x, string y);
    }

}
