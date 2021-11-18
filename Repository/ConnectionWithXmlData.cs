using System;
using System.IO;
using System.Xml;
using WepProject.Models;
using System.Collections.Generic;

namespace WepProject.Database
{
    public class ConnectionWithXmlData
    {
        private const string _pathDataFile = "Database/DataFile.xml";

        public ConnectionWithXmlData() { }

        public int CheckUsersByCredentials(string email, string password)
        {
            string tempEmail = "";
            string tempPassword = "";
            int rows = 0;
            using (var reader = new XmlTextReader(_pathDataFile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement("email"))
                    {
                        reader.Read();
                        tempEmail = reader.Value;
                    }
                    if (reader.IsStartElement("password"))
                    {
                        reader.Read();
                        tempPassword = reader.Value;
                    }
                    if (!String.IsNullOrEmpty(tempPassword) && !String.IsNullOrEmpty(tempEmail))
                    {
                        if (String.Equals(tempEmail, email) && String.Equals(tempPassword, password))
                        {
                            rows++;
                            break;
                        }
                        tempEmail = "";
                        tempPassword = "";
                    }
                }
            }
            return rows;
        }
    }
}
