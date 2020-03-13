using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppliMariage.Models
{
    public interface IMessageDialog
    {
        event Action<bool> OnDialogDisplayChanged;
        void Show(string title, Action callback);
        void Show(string title, string message, Action callback);
        bool IsVisible { get; }
    }

    public class MessageDialogService : IMessageDialog
    {
        private static MessageDialogService _MessageDialog;
        public static MessageDialogService GetIntance()
        {
            if (_MessageDialog == null)
                _MessageDialog = new MessageDialogService();

            return _MessageDialog;
        }


        private Action<bool> onDialogDisplayChanged;
        public event Action<bool> OnDialogDisplayChanged
        {
            add { onDialogDisplayChanged += value; }
            remove { onDialogDisplayChanged -= value; }
        }

        public delegate bool DisplayRequestEvent(string title, string message);

        private DisplayRequestEvent onDiplayRequested;
        public event DisplayRequestEvent OnDiplayRequested
        {
            add { onDiplayRequested += value; }
            remove { onDiplayRequested -= value; }
        }

        public bool IsVisible { get; private set; }

        public void Show(string title, Action callback)
        {
            Show(title, string.Empty, callback);
        }

        public void Show(string title, string message, Action callback)
        {
            NotifyDisplayChanged(true);
            Task.Factory.StartNew(new Action(() =>
            {                
                if (onDiplayRequested != null)
                    onDiplayRequested(title, message);

                if (callback != null)
                    Application.Current.Dispatcher.Invoke(callback);

                Application.Current.Dispatcher.Invoke(new Action(() => { NotifyDisplayChanged(false); }));
            }));
        }

        private void NotifyDisplayChanged(bool visible)
        {
            if (onDialogDisplayChanged != null)
            {
                IsVisible = visible;
                onDialogDisplayChanged(visible);
            }
        }        
    }
}
