using CoffeShop.Data.Catalog;
using CoffeShop.Data.Users;
using CoffeShop.Logic.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Logic
{
    public class State
    {
        private Catalog currentCatalog;
        private List<User> currentUsers;
        private double currentBalance;

        public State(Cafe shop)
        {
            this.currentCatalog = new Catalog();
            foreach (var product in shop.GetCatalog().GetProducts())
            {
                this.currentCatalog.AddProduct(new Product(product.Id, product.GetName(), product.GetPrice(), product.GetTypeOfProdcut()));
            }

            this.currentUsers = new List<User>();
            foreach (var user in shop.GetUsers())
            {
                // Determine the type of user and create an instance accordingly
                if (user is Employee)
                {
                    Employee employee = (Employee)user;
                    this.currentUsers.Add(new Employee(employee.GetSurname(), employee.GetName(), employee.GetPhone(), employee.GetEmail(), employee.GetEmployeeId(), employee.GetSalary()));
                }
                else if (user is Supplier)
                {
                    Supplier supplier = (Supplier)user;
                    this.currentUsers.Add(new Supplier(supplier.GetSurname(), supplier.GetName(), supplier.GetPhone(), supplier.GetEmail(), supplier.GetSupplierId()));
                }
                else if (user is Customer)
                {
                    Customer customer = (Customer)user;
                    this.currentUsers.Add(new Customer(customer.GetSurname(), customer.GetName(), customer.GetPhone(), customer.GetEmail(), customer.GetCustomerId(), customer.GetBalance()));
                }
            }

            this.currentBalance = shop.GetBalance();
        }

        public Catalog GetCurrentCatalog()
        {
            Dictionary<int, Product> productDictionary = new Dictionary<int, Product>();
            foreach (var product in currentCatalog.GetProducts())
            {
                productDictionary.Add(product.Id, product);
            }
            return new Catalog(productDictionary);
        }


        public void SetCurrentCatalog(Catalog catalog)
        {
            this.currentCatalog = catalog;
        }

        public List<User> GetCurrentUsers()
        {
            // Return a shallow copy of the currentUsers list
            return new List<User>(currentUsers);
        }

        public void SetCurrentUsers(List<User> users)
        {
            this.currentUsers = users;
        }

        public double GetCurrentBalance()
        {
            return currentBalance;
        }

        public void SetCurrentBalance(double value)
        {
            this.currentBalance = value;
        }
    }
}
