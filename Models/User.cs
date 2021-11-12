using System;
using WepProject.Repository;

namespace WepProject.Models
{
    public class User
    {
        public string Senha { get; }
        public string Email { get; }
        public Address Address { get; }

        public User() { }

        public User(string email, string senha, Address address)
        {
            Senha = senha;
            Email = email;
            Address = address;
        }

        public int AddToDb()
        {
            var userRepo = new UserRepository();
            return userRepo.AddToDb(this);
        }
    }
}
