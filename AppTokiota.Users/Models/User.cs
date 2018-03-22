using System;
namespace AppTokiota.Users.Models
{
    public class User
    {
        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public User(string email, string password) {
            Email = email;
            Password = password;
        }

    }
}
