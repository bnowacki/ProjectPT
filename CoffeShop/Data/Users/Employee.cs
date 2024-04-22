namespace CoffeShop.Data.Users
{
    public class Employee : User
    {
        private int salary;
        public int Salary
        {
            get { return salary; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Salary cannot be negative.");
                }
                salary = value;
            }
        }
    }
}
