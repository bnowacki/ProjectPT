using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Presentation.Model.API;

namespace Presentation.ViewModel;

internal class UserMasterViewModel : IViewModel, IUserMasterViewModel
{
    public ICommand SwitchToProductMasterPage { get; set; }

    public ICommand SwitchToStateMasterPage { get; set; }

    public ICommand SwitchToEventMasterPage { get; set; }

    public ICommand CreateUser { get; set; }

    public ICommand RemoveUser { get; set; }

    private readonly IUserModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private ObservableCollection<IUserDetailViewModel> _users;

    public ObservableCollection<IUserDetailViewModel> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
        }
    }

    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    private string _email;

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    private DateTime _dateOfBirth;

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            _dateOfBirth = value;
            OnPropertyChanged(nameof(DateOfBirth));
        }
    }

    private bool _isUserSelected;

    public bool IsUserSelected
    {
        get => _isUserSelected;
        set
        {
            IsUserDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

            _isUserSelected = value;
            OnPropertyChanged(nameof(IsUserSelected));
        }
    }

    private Visibility _isUserDetailVisible;

    public Visibility IsUserDetailVisible
    {
        get => _isUserDetailVisible;
        set
        {
            _isUserDetailVisible = value;
            OnPropertyChanged(nameof(IsUserDetailVisible));
        }
    }

    private IUserDetailViewModel _selectedDetailViewModel;

    public IUserDetailViewModel SelectedDetailViewModel
    {
        get => _selectedDetailViewModel;
        set
        {
            _selectedDetailViewModel = value;
            IsUserSelected = true;

            OnPropertyChanged(nameof(SelectedDetailViewModel));
        }
    }

    public UserMasterViewModel(IUserModelOperation? model = null, IErrorInformer? informer = null)
    {
        SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");
        SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
        SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

        CreateUser = new OnClickCommand(e => StoreUser(), c => CanStoreUser());
        RemoveUser = new OnClickCommand(e => DeleteUser());

        Users = new ObservableCollection<IUserDetailViewModel>();

        _modelOperation = model ?? IUserModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();

        IsUserSelected = false;

        Task.Run(LoadUsers);
    }

    private bool CanStoreUser()
    {
        return !(
            string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(Email)
        );
    }

    private void StoreUser()
    {
        Task.Run(async () =>
        {
            int lastId = await _modelOperation.GetCountAsync() + 1;

            await _modelOperation.AddAsync(lastId, Name, Email);

            _informer.InformSuccess("User successfully created!");

            LoadUsers();
        });
    }

    private void DeleteUser()
    {
        Task.Run(async () =>
        {
            try
            {
                await _modelOperation.DeleteAsync(SelectedDetailViewModel.Id);

                _informer.InformSuccess("User successfully deleted!");

                LoadUsers();
            }
            catch (Exception e)
            {
                _informer.InformError("Error while deleting user! Remember to remove all associated events!");
            }
        });
    }

    private async void LoadUsers()
    {
        Dictionary<int, IUserModel> Users = await _modelOperation.GetAllAsync();

        Application.Current.Dispatcher.Invoke(() =>
        {
            _users.Clear();

            foreach (IUserModel u in Users.Values)
            {
                _users.Add(new UserDetailViewModel(u.Id, u.Name, u.Email));
            }
        });

        OnPropertyChanged(nameof(Users));
    }
}
