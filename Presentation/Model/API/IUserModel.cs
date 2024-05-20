using System;

namespace Presentation.Model.API;

public interface IUserModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

}
