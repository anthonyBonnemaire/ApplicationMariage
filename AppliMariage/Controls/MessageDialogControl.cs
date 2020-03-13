using AppliMariage.Commands;
using AppliMariage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppliMariage.Controls
{
    public class MessageDialogControl : Control
    {
        private ManualResetEvent userInteractionEvent;        
        static MessageDialogControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageDialogControl), new FrameworkPropertyMetadata(typeof(MessageDialogControl)));

        }
        public MessageDialogControl()
        {
            this.AnswerCommand = new CommandeSimple(userAnswer_Execute);
            MessageDialogService.GetIntance().OnDiplayRequested += MessageDialogControl_OnDiplayRequested;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }


        public void userAnswer_Execute()
        {
            userAnswer_Execute(this);
        }

        public static void userAnswer_Execute(MessageDialogControl messageControl)
        {            
            if (messageControl.userInteractionEvent != null)
            {
                messageControl.userInteractionEvent.Set();
            }
            Application.Current.Dispatcher.Invoke(new Action(() => 
            VisualStateManager.GoToState(messageControl, "Hidden", true)));            
        }

        public bool MessageDialogControl_OnDiplayRequested(string title, string message)
        {
            userInteractionEvent = new ManualResetEvent(false);
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                VisualStateManager.GoToState(this, "Displayed", true);
                this.Title = title;
                this.Message = message;
            }));
            userInteractionEvent.WaitOne();
            userInteractionEvent = null;
            return true;

        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MessageDialogControl), new PropertyMetadata(string.Empty));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageDialogControl), new PropertyMetadata(string.Empty));


        public ICommand AnswerCommand { get; set; }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Buttons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MessageDialogControl), new PropertyMetadata(null));
    }
}
