using System;
using Presentation.Model.API;

namespace Presentation.Model.Implementation;

internal class UserModel : IUserModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public UserModel(int id, string name, string email)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
    }
}
