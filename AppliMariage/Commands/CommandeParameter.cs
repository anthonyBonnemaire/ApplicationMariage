using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppliMariage.Commands
{
    public class CommandePredicate : ICommand
    {
        private Action<object> _execute;
        private Predicate<object> _predicate;
        private ManualResetEvent userInteractionEvent;

        public CommandePredicate(Action<object> execute, Predicate<object> predicate)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            userInteractionEvent = new ManualResetEvent(false);
            Task.Factory.StartNew(() =>
            {
                while (!_predicate.Invoke(null)) ;

                if (userInteractionEvent != null)
                {
                    userInteractionEvent.Set();
                }
            });
            userInteractionEvent.WaitOne();
            userInteractionEvent = null;
            _execute(parameter);
        }




        public event EventHandler CanExecuteChanged;
    }
}

