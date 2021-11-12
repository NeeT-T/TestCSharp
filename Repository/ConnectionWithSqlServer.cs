using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WepProject.Models;

namespace WepProject.Database
{
    //Melhorias:
    //- Separar as dependencias! Fazer com que as classes Connection
    //sejam apenas para conectar no banco e não para acessar dados
    //- Arrumar uma forma melhor de serialização melhor.

    public class ConnectionWithSqlServer 
    {
        private const string connectionString = "Server=localhost;Database=wep;User Id=sa;Password=Senha_root";

        public ConnectionWithSqlServer() { }

        public User GetUsersByCredentials(string email, string password)
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

        private User SetUser(SqlDataReader reader)
        {
            var email = reader.GetString(1);
            var senha = reader.GetString(2);
            var cep = reader.GetString(3);
            var cidade = reader.GetString(4);
            var estado = reader.GetString(5);
            var logradouro = reader.GetString(6);
            var bairro = reader.GetString(7);
            var complemento = reader.GetString(8);
            Address address = new Address(cep, cidade, estado, logradouro, bairro, complemento);
            return new User(email, senha, address);
        }

        public int HasEmail(string email)
        {
            int rows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM users WHERE Email = @email";
                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@email", SqlDbType.VarChar, 255);

                cmd.Parameters["@email"].Value = email;

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
