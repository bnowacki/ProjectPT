using Data.API;
using Service.API;

namespace Service.Implementation;

internal class UserCRUD : IUserCRUD
{
    private IDataRepository _repository;

    public UserCRUD(IDataRepository repository)
    {
        this._repository = repository;
    }

    private IUserDTO Map(IUser user)
    {
        return new UserDTO(user.Id, user.Name, user.Email);
    }

    public async Task AddUserAsync(int id, string name, string email)
    {
        await this._repository.AddUserAsync(id, name, email);
    }

    public async Task<IUserDTO> GetUserAsync(int id)
    {
        return this.Map(await this._repository.GetUserAsync(id));
    }

    public async Task UpdateUserAsync(int id, string name, string email)
    {
        await this._repository.UpdateUserAsync(id, name, email);
    }

    public async Task DeleteUserAsync(int id)
    {
        await this._repository.DeleteUserAsync(id);
    }

    public async Task<Dictionary<int, IUserDTO>> GetAllUsersAsync()
    {
        Dictionary<int, IUserDTO> result = new Dictionary<int, IUserDTO>();

        foreach (IUser user in (await this._repository.GetAllUsersAsync()).Values)
        {
            result.Add(user.Id, this.Map(user));
        }

        return result;
    }

    public async Task<int> GetUsersCountAsync()
    {
        return await this._repository.GetUsersCountAsync();
    }
}
