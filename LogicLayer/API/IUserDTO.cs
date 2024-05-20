namespace LogicLayer.API
{
    public interface IUserDTO
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string GetFullName();
    }
}
