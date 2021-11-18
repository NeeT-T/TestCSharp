using System;
using System.Collections.Generic;
using WepProject.Repository;

namespace WepProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public User() { }

        public User(string email, string senha, Address address)
        {
            Senha = senha;
            Email = email;
            Address = address;
        }

        public User(int id, string email, string senha, Address address)
        {
            Id = id;
            Senha = senha;
            Email = email;
            Address = address;
        }

        public int AddToDb()
        {
            return new UserRepository().AddToDb(this);
        }

        public bool HasEqualEmail()
        {
            int rows = new UserRepository().HasEqualEmail(this.Email);
            return (rows > 0) ? true : false;
        }

        public List<User> GetUsers()
        {
            return new UserRepository().GetUsers();
        }
        
        public User GetUserById(int id)
        {
            return new UserRepository().GetUsersById(id);
        }

        public int UpdateUser()
        {
            return new UserRepository().UpdateUser(this);
        }

        public int RemoveUser(int id)
        {
            return new UserRepository().RemoveUser(id);
        }

        public bool Equals(User user)
        {
            return this.Id == user.Id && this.Email == user.Email && this.Senha == user.Senha && this.Address.Equals(user.Address);
        }

    }
}
