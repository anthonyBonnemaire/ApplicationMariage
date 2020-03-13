using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CommunicationAppliMariage
{
    public enum TypeCall
    {
        StartAll,
        Stop
    }

    public class TypeCallEventArgs : EventArgs
    {
        private TypeCall _typeCall;

        public TypeCallEventArgs(TypeCall typeCall)
        {
            _typeCall = typeCall;
        }

        public TypeCall TypeCall
        {
            get { return _typeCall; }
            set { _typeCall = value; }
        }
    }


    public interface IIdentificationService
    {        
        void AjouterAction(TypeCall typeCall, ICommand command);
        void OnCallChanged(TypeCall typeCall);
        void LancerAction(TypeCall typeCall);
    }

    public class IdentificationService : IIdentificationService
    {

        private static IdentificationService _Instance;
        public static IdentificationService GetInstance()
        {
            if (_Instance == null)
                _Instance = new IdentificationService();
            return _Instance;
        }

        private Dictionary<TypeCall, ICommand> _DicoCall;
        private Dictionary<TypeCall, ICommand> DicoCall
        {
            get
            {
                if(_DicoCall == null)
                    _DicoCall = new Dictionary<TypeCall, ICommand>();

                return _DicoCall;
            }
        }

        public void AjouterAction(TypeCall typeCall, ICommand command)
        {
            if (typeCall == null || command == null)
                return;

            DicoCall.Add(typeCall, command);
        }

        public event EventHandler<TypeCallEventArgs> CallChanged;

        public void OnCallChanged(TypeCall typeCall)
        {
            if(CallChanged != null)
                CallChanged(this,new TypeCallEventArgs(typeCall));
        }

        public void LancerAction(TypeCall typeCall)
        {
            if (typeCall == null)
                return;

            ICommand command;

            if (DicoCall.TryGetValue(typeCall, out command))
                command.Execute(typeCall);
        }


    }
}
