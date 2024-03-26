using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class ParameterCommand<T> : ICommand
    {
        public ParameterCommand(Action<T> execute) : this(execute, null!) { }

        public ParameterCommand(Action<T> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            if (parameter == null)
                return _canExecute();
            return _canExecute();
        }

        public virtual void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler? CanExecuteChanged;

        internal void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private readonly Action<T> _execute;
        private readonly Func<bool> _canExecute;
    }
}
