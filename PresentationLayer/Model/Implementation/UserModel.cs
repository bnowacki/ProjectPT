using System;
using PresentationLayer.Model.API;

namespace PresentationLayer.Model.Implementation;

internal class UserModel : IUserModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
