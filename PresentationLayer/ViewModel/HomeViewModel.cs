using PresentationLayer.ViewModel.Command;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    internal class HomeViewModel : IViewModel
    {
        public ICommand StartAppCommand { get; set; }

        public ICommand ExitAppCommand { get; set; }

        public HomeViewModel()
        {
            StartAppCommand = new SwitchViewCommand("UserMasterView");

            ExitAppCommand = new CloseApplicationCommand();
        }
    }
}
