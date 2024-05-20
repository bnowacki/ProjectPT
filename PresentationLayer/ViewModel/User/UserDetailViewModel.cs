using System.Windows.Input;
using PresentationLayer.Model.API;
using PresentationLayer.ViewModel.Command;

namespace PresentationLayer.ViewModel;

internal class UserDetailViewModel : IViewModel
{
    public ICommand UpdateUser { get; set; }

    private readonly IUserModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private Guid _id;
    public Guid Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
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

    public UserDetailViewModel(IUserModelOperation? model = null, IErrorInformer? informer = null)
    {
        UpdateUser = new RelayCommand(e => Update(), c => CanUpdate());

        _modelOperation = model ?? IUserModelOperation.New();
        _informer = informer ?? new PopupErrorInformer();
    }

    public UserDetailViewModel(Guid id, string firstName, string lastName, string email, IUserModelOperation? model = null, IErrorInformer? informer = null)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;

        UpdateUser = new RelayCommand(e => Update(), c => CanUpdate());

        _modelOperation = model ?? IUserModelOperation.New();
        _informer = informer ?? new PopupErrorInformer();
    }

    private void Update()
    {
        Task.Run(() =>
        {
            _modelOperation.Update(Id, FirstName, LastName, Email);

            _informer.InformSuccess("User successfully updated!");
        });
    }

    private bool CanUpdate()
    {
        return !(
            string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName) ||
            string.IsNullOrWhiteSpace(Email)
        );
    }
}
