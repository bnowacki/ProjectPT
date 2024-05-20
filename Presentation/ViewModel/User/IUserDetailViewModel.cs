using Presentation.Model.API;
using System;
using System.Windows.Input;

namespace Presentation.ViewModel;

public interface IUserDetailViewModel
{
    static IUserDetailViewModel CreateViewModel(int id, string name, string email, IUserModelOperation model, IErrorInformer informer)
    {
        return new UserDetailViewModel(id, name, email, model, informer);
    }

    ICommand UpdateUser { get; set; }

    int Id { get; set; }

    string Name { get; set; }

    string Email { get; set; }
}

