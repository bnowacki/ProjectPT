namespace DataLayer.API
{
    public interface IUser
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string GetFullName();
    }
}
