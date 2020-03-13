using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommunicationAppliMariage
{
    public class EventArgCallChanged : EventArgs 
    {
        public TypeCall TypeCall { get; set; }
        public object parameter { get; set; }

    }

    public class IdentificationService
    {
        public event EventHandler<EventArgCallChanged> CallChanged;

        public void OnCallChanged(EventArgCallChanged typeCall)
        {
            if (CallChanged != null)
            {
                CallChanged(this, new EventArgCallChanged());
            }
        }

        private Dictionary<TypeCall, ICommand> _ActionToTypeCall;

        public void AjouterAction(TypeCall typeCall, ICommand commande)
        {
            if (typeCall == null || commande == null)
                return;

            if (_ActionToTypeCall == null)
                _ActionToTypeCall = new Dictionary<TypeCall, ICommand>();

            if (_ActionToTypeCall.ContainsKey(typeCall))
                return;

            _ActionToTypeCall.Add(typeCall, commande);
        }
        

        public void SupprimerAction(TypeCall typeCall)
        {

            if (typeCall == null || _ActionToTypeCall == null || !_ActionToTypeCall.ContainsKey(typeCall))
                return;

            _ActionToTypeCall.Remove(typeCall);
        }


        public void ChangerAction(TypeCall typeCall, ICommand commande)
        {

            if (typeCall == null || _ActionToTypeCall == null || !_ActionToTypeCall.ContainsKey(typeCall))
                return;

            _ActionToTypeCall[typeCall] = commande ;
        }

        public void LancerAction(TypeCall typeCall, object parameter)
        {
            if(typeCall == null || _ActionToTypeCall == null && !_ActionToTypeCall.ContainsKey(typeCall))
                return;            

            if (_ActionToTypeCall[typeCall] != null)
                _ActionToTypeCall[typeCall].Execute(parameter);
        }


    }
}
