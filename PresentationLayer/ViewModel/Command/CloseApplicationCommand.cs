using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.ViewModel.Command;

internal class CloseApplicationCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        Application.Current.Shutdown();
    }
}
