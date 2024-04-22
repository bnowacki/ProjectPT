namespace CoffeShop.Data.Users
{
    public abstract class User
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Phone { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
