using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WepProject.Models;

namespace WepProject.Repository
{

    public class AuthorizationRepository 
    {
        private const string connectionString = "Server=localhost;Database=wep;User Id=sa;Password=Senha_root";

        public AuthorizationRepository() { }

        public User GetUserByCredentials(string email, string password)
        {
            User user = new User();
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM users WHERE Email = @email AND Senha = @password";
                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@email", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 255);

                cmd.Parameters["@email"].Value = email;
                cmd.Parameters["@password"].Value = password;

                try 
                {
                    conn.Open();
                    cmd.Prepare();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) {
                            while (reader.Read())
                            {
                                return SetUser(reader);
                            }
                        }
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return (user != null) ? user : null;
        }

        public User SetUser(SqlDataReader reader)
        {
            var id = reader.GetInt32(0);
            var email = reader.GetString(1);
            var senha = reader.GetString(2);
            var cep = reader.GetString(3);
            var cidade = reader.GetString(4);
            var estado = reader.GetString(5);
            var logradouro = reader.GetString(6);
            var bairro = reader.GetString(7);
            var complemento = reader.GetString(8);
            Address address = new Address(cep, cidade, estado, logradouro, bairro, complemento);
            return new User(id, email, senha, address);
        }
    }
}
