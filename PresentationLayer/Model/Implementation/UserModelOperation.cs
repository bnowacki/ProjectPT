using PresentationLayer.Model.API;
using LogicLayer.API;

namespace PresentationLayer.Model.Implementation;

internal class UserModelOperation : IUserModelOperation
{
    private ILogic _logic;

    public UserModelOperation(ILogic? logic = null)
    {
        _logic = logic ?? ILogic.New();
    }

    private static IUserModel Map(IUserDTO user)
    {
        return new UserModel { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
    }

    public async Task Create(string firstName, string lastName, string email)
    {
        await _logic.CreateUser(firstName, lastName, email);
    }

    public async Task<IUserModel> Get(Guid id)
    {
        return Map(await _logic.GetUser(id));
    }

    public async Task Update(Guid id, string firstName, string lastName, string email)
    {
        await _logic.UpdateUser(id, firstName, lastName, email);
    }

    public async Task Delete(Guid id)
    {
        await _logic.DeleteUser(id);
    }

    public async Task<IEnumerable<IUserModel>> GetAll()
    {
        List<IUserModel> result = new();

        foreach (IUserDTO user in (await _logic.GetUsers()))
        {
            result.Add(Map(user));
        }

        return result;
    }
}
