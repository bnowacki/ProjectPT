using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Data.Users
{
    public abstract class User
    {
        private string surname;
        private string name;
        private int phone;
        private string email;

        public User(string surname, string name, int phone, string email)
        {
            this.surname = surname;
            this.name = name;
            this.phone = phone;
            this.email = email;
        }

        public string GetSurname()
        {
            return this.surname;
        }

        public void SetSurname(string value)
        {
            this.surname = value;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string value)
        {
            this.name = value;
        }

        public int GetPhone()
        {
            return this.phone;
        }

        public void SetPhone(int value)
        {
            this.phone = value;
        }

        public string GetEmail()
        {
            return this.email;
        }

        public void SetEmail(string value)
        {
            this.email = value;
        }
    }
}
