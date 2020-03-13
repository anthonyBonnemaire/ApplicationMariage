using CommunicationAppliMariage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationAppliMariage
{   
    public interface ICallChanged
    {
        event EventHandler<TypeCall> CallChanged;

        void OnCallChanged(TypeCall typeCall);
    }
}
