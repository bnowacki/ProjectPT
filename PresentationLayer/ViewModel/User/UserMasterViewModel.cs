using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PresentationLayer.Model.API;
using PresentationLayer.Model.Implementation;
using PresentationLayer.ViewModel.Command;

namespace PresentationLayer.ViewModel
{
    internal class UserMasterViewModel : IViewModel
    {
        private IUserModelOperation _model;
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public ObservableCollection<UserModel> Users { get; set; }

        public UserMasterViewModel()
        {
            _model = IUserModelOperation.New();

            Users = new ObservableCollection<UserModel>{};

            EditCommand = new RelayCommand(EditUser);
            DeleteCommand = new RelayCommand(DeleteUser);

            LoadUsers();
        }
        private async void LoadUsers()
        {
            IEnumerable<IUserModel> users = await _model.GetAll();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Users.Clear();

                foreach (IUserModel u in Users)
                {
                    Users.Add(new UserModel { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email });
                }
            });

            OnPropertyChanged(nameof(Users));
        }

        private void EditUser(object? parameter)
        {
            if (parameter is UserModel user)
            {
                // Logic to edit the user
                // For example, open a new window or dialog to edit the user
                 MessageBox.Show($"Editing user: {user.FirstName} {user.LastName}");
            }
        }

        private void DeleteUser(object? parameter)
        {
            if (parameter is UserModel user)
            {
                Users.Remove(user);
            }
        }
    }
}


