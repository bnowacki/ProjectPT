using CoffeShop.Data.Catalog;
using CoffeShop.Data.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Logic.DataContext
{
    public class Cafe
    {
        private Catalog catalog;
        private List<User> users;
        private double initialBalance;

        public Cafe(double initialBalance)
        {
            this.catalog = new Catalog();
            this.users = new List<User>();
            this.initialBalance = initialBalance;
        }

        public void UpdateCafeState(State state)
        {
            this.catalog = new Catalog();
            foreach (var product in state.GetCurrentCatalog().GetProducts())
            {
                this.catalog.AddProduct(new Product(product.Id, product.GetName(), product.GetPrice(), product.GetTypeOfProdcut()));
            }

            this.users = new List<User>();
            foreach (var user in state.GetCurrentUsers())
            {
                // Determine the type of user and create an instance accordingly
                if (user is Employee)
                {
                    Employee employee = (Employee)user;
                    this.users.Add(new Employee(employee.GetSurname(), employee.GetName(), employee.GetPhone(), employee.GetEmail(), employee.GetEmployeeId(), employee.GetSalary()));
                }
                else if (user is Supplier)
                {
                    Supplier supplier = (Supplier)user;
                    this.users.Add(new Supplier(supplier.GetSurname(), supplier.GetName(), supplier.GetPhone(), supplier.GetEmail(), supplier.GetSupplierId()));
                }
                else if (user is Customer)
                {
                    Customer customer = (Customer)user;
                    this.users.Add(new Customer(customer.GetSurname(), customer.GetName(), customer.GetPhone(), customer.GetEmail(), customer.GetCustomerId(), customer.GetBalance()));
                }
            }

            this.initialBalance = state.GetCurrentBalance();
        }

        public Catalog GetCatalog()
        {
            return catalog;
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public double GetBalance()
        {
            return initialBalance;
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
        }

        public void AddProduct(Product product)
        {
            catalog.AddProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            catalog.RemoveProduct(product);
        }
    }
}
