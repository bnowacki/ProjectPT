using Service.API;

namespace Service.Implementation;

internal class UserDTO : IUserDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public UserDTO(int id, string name, string email)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
    }
}
