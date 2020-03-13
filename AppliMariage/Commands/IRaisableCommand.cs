using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppliMariage.Commands
{
    public interface IRaisableCommand : ICommand
    {
        string DisplayText { get; set; }
        void RaiseCanExecuteChanged();
    }
}
