using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class User
    {
        public String Name { get; set; }
        public String Password { get; set; }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
