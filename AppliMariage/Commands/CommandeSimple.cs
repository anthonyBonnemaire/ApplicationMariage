using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppliMariage.Commands
{
    public class CommandeSimple : ICommand
    {
        private Action<object> _execute;

        public CommandeSimple(Action<object> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
        }

        public CommandeSimple(Action execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = x => execute();
        }        

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        private Action<object> action;

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
