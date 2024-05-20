using System.Windows.Input;

namespace PresentationLayer.ViewModel.Command;

internal class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;

    private readonly Predicate<object?>? _canExecute;

    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? param)
    {
        return _canExecute == null || _canExecute(param);
    }

    public virtual void Execute(object? param)
    {
        _execute.Invoke(param);
    }
    
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
