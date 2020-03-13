using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliMariage.Commands
{
    public class DelegateCommand<T> : IRaisableCommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public string DisplayText { get; set; }

        public DelegateCommand(string displayText, Action<T> execute, Func<T, bool> canExecute)
        {
            this.DisplayText = displayText;
            _execute = execute;
            _canExecute = canExecute;
        }
        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            T value = (T)(parameter ?? default(T));
            return _canExecute(value);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            T value = (T)(parameter ?? default(T));
            _execute(value);
        }


        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
        #endregion
    }
}
