using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WepProject.Models;
using WepProject.Interfaces.Connection;

namespace WepProject.Database
{
    //Melhorias:
    //- Separar as dependencias! Fazer com que as classes Connection
    //sejam apenas para conectar no banco e não para acessar dados
    //- Talvez arrumar uma forma melhor de serialização. Talvez usando as classes com
    //namespace Serialization

    public class ConnectionWithSqlServer : IConnection
    {
        private const string connectionString = "Server=localhost;Database=users;User Id=sa;Password=Senha_root";

        public ConnectionWithSqlServer() { }

        public int CheckUsersByCredentials(string email, string password)
        {
            int rows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM credentials WHERE Email = @email AND Password = @password";
                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@email", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 255);

                cmd.Parameters["@email"].Value = email;
                cmd.Parameters["@password"].Value = password;

                //cmd.Parameters.AddWithValue("@email", email);
                //cmd.Parameters.AddWithValue("@password", password);

                try 
                {
                    conn.Open();
                    cmd.Prepare();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) rows++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return rows;
        }

    }
}
