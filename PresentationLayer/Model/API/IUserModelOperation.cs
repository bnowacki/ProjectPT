using PresentationLayer.Model.Implementation;
using LogicLayer.API;

namespace PresentationLayer.Model.API;

public interface IUserModelOperation
{
    static IUserModelOperation New(ILogic? logic = null)
    {
        return new UserModelOperation(logic);
    }

    Task Create(string firstName, string lastName, string email);

    Task<IUserModel> Get(Guid id);

    Task Update(Guid id, string firstName, string lastName, string email);

    Task Delete(Guid id);

    Task<IEnumerable<IUserModel>> GetAll();

}
