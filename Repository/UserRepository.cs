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

        public int AddToDb(User user)
        {
            int rows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO users Values (@email, @senha, @cep, @cidade, @estado, @logradouro, @bairro, @complemento)";
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
                    rows = cmd.ExecuteNonQuery();
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
