using DataLayer.API;

namespace DataLayer.Implementation
{
    internal class User : IUser
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
