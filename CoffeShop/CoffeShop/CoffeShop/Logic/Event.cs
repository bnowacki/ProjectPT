using CoffeShop.Data.Catalog;
using CoffeShop.Data.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Logic
{
    public class Events
    {

        public void SellProducts(Product[] products, State state)
        {
            // Check if products are available in the catalog
            foreach (var product in products)
            {
                if (!state.GetCurrentCatalog().GetProducts().Contains(product))
                {
                    throw new InvalidOperationException($"Product '{product.GetName()}' is not available in the catalog.");
                }
            }

            // Calculate total price of sold products
            float totalPrice = 0;
            foreach (var product in products)
            {
                totalPrice += product.GetPrice();
            }

            // Deduct sold products from inventory or update sales records
            // For simplicity, let's assume we are deducting from inventory directly
            foreach (var product in products)
            {
                state.GetCurrentCatalog().RemoveProduct(product);
            }

            // Update current balance
            state.SetCurrentBalance(state.GetCurrentBalance() + totalPrice);
        }

        public void TakeDelivery(Product[] products, State state)
        {
            foreach (var product in products)
            {
                state.GetCurrentCatalog().AddProduct(product);
            }
        }

        public void PayEmployee(Employee employee, State state)
        {
            if (!state.GetCurrentUsers().Contains(employee))
            {
                throw new InvalidOperationException($"Employee '{employee.GetName()}' is not found in current users.");
            }

            // Deduct employee's salary from current balance
            state.SetCurrentBalance(state.GetCurrentBalance() - employee.GetSalary());
        }

        public void PayTaxes(float taxAmount, State state)
        {
            // Deduct taxes from current balance
            state.SetCurrentBalance(state.GetCurrentBalance() - taxAmount);
        }
    }
}
