using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Data.Users
{
    public class Customer : User
    {
        private int customerId;
        private int balance;

        // Constructor to initialize the Customers object
        public Customer(string surname, string name, int phone, string email, int customerId, int balance)
            : base(surname, name, phone, email)
        {
            this.customerId = customerId;
            this.balance = balance;
        }

        public int GetCustomerId()
        {
            return this.customerId;
        }

        public void SetCustomerId(int value)
        {
            this.customerId = value;
        }

        public int GetBalance() { return this.balance; }

        public void SetBalance(int value) { this.balance = value; }
    }
}
