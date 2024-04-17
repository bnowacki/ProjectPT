using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Data.Users
{
    public class Employee : User
    {
        private int employeeId;
        private int salary;

        public Employee(string surname, string name, int phone, string email, int employeeId, int salary)
            : base(surname, name, phone, email)
        {
            this.employeeId = employeeId;
            this.salary = salary;
        }

        public int GetEmployeeId()
        {
            return this.employeeId;
        }

        public void SetEmployeeId(int value)
        {
            this.employeeId = value;
        }

        public int GetSalary() { return this.salary; }

        public void SetSalary(int value)
        {
            this.salary = value;
        }
    }
}
