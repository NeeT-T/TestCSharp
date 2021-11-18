using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WepProject.Models;

namespace WepProject.Repository
{

    public class UserRepository
    {
        private const string connectionString = "Server=localhost;Database=wep;User Id=sa;Password=Senha_root";

        public UserRepository() {}

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM users";
                var cmd = new SqlCommand(query, conn);

                try 
                {
                    conn.Open();
                    cmd.Prepare();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) {
                            while (reader.Read())
                            {
                                users.Add(new AuthorizationRepository().SetUser(reader));   
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return users;
        }

        public User GetUsersById(int id)
        {
            User user = new User();
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM users where id = @id";
                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@id", SqlDbType.Int);

                cmd.Parameters["@id"].Value = id;

                try 
                {
                    conn.Open();
                    cmd.Prepare();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) {
                            while (reader.Read())
                            {
                                user = new AuthorizationRepository().SetUser(reader);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return user;
        }

        public int AddToDb(User user)
        {
            int userId = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO users Values (@email, @senha, @cep, @cidade, @estado, @logradouro, @bairro, @complemento);"
                    + "SELECT CAST(scope_identity() AS int)";
                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@email", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@senha", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@cep", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@cidade", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 2);
                cmd.Parameters.Add("@logradouro", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@bairro", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@complemento", SqlDbType.VarChar, 45);

                cmd.Parameters["@email"].Value = user.Email;
                cmd.Parameters["@senha"].Value = user.Senha;
                cmd.Parameters["@cep"].Value = user.Address.Cep;
                cmd.Parameters["@cidade"].Value = user.Address.Localidade;
                cmd.Parameters["@estado"].Value = user.Address.UF;
                cmd.Parameters["@logradouro"].Value = user.Address.Logradouro;
                cmd.Parameters["@bairro"].Value = user.Address.Bairro;
                cmd.Parameters["@complemento"].Value = user.Address.Complemento;

                try 
                {
                    conn.Open();
                    cmd.Prepare();
                    userId = (Int32)cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return userId;
        }

        public int UpdateUser(User user)
        {
            int rows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE users SET Email = @email, Senha = @senha, Cep = @cep, Cidade = @cidade, Estado = @estado, Logradouro = @logradouro, Bairro = @bairro, Complemento = @complemento WHERE Id = @id;";
                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@senha", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@cep", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@cidade", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 2);
                cmd.Parameters.Add("@logradouro", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@bairro", SqlDbType.VarChar, 255);
                cmd.Parameters.Add("@complemento", SqlDbType.VarChar, 45);

                cmd.Parameters["@id"].Value = user.Id;
                cmd.Parameters["@email"].Value = user.Email;
                cmd.Parameters["@senha"].Value = user.Senha;
                cmd.Parameters["@cep"].Value = user.Address.Cep;
                cmd.Parameters["@cidade"].Value = user.Address.Localidade;
                cmd.Parameters["@estado"].Value = user.Address.UF;
                cmd.Parameters["@logradouro"].Value = user.Address.Logradouro;
                cmd.Parameters["@bairro"].Value = user.Address.Bairro;
                cmd.Parameters["@complemento"].Value = user.Address.Complemento;

                try 
                {
                    conn.Open();
                    cmd.Prepare();
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return rows;
        }


        public int RemoveUser(int id)
        {
            int rows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM users WHERE @id = Id";
                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@id", SqlDbType.Int);

                cmd.Parameters["@id"].Value = id;

                try 
                {
                    conn.Open();
                    cmd.Prepare();
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return rows;
        }

        public int HasEqualEmail(string email)
        {
            int rows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 Id FROM users WHERE Email = @email";
                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@email", SqlDbType.VarChar, 255);

                cmd.Parameters["@email"].Value = email;

                try 
                {
                    conn.Open();
                    cmd.Prepare();
                    rows = (Int32)cmd.ExecuteScalar();
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
