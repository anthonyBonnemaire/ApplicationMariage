using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommunicationAppliMariage
{
    public class Action
    {
        private static SerialPort _Sp;
        private static bool _IsDectect;
        public static int TimeWait;

        private const string BUTTON = "button\r";
        private const string DETECT = "detect\r";       

        public static void Lancer()
        {
            IdentificationService.GetInstance().OnCallChanged(TypeCall.StartAll);
            ArreterServeur();
        }

        public static void LancerServeur()
        {
            _Sp = new SerialPort("COM3");
            _Sp.BaudRate = 9600;
            _Sp.Parity = Parity.None;
            _Sp.StopBits = StopBits.One;
            _Sp.DataBits = 8;
            _Sp.Handshake = Handshake.None;
            _Sp.Open();
            Action action = new Action();
            _Sp.DataReceived += new SerialDataReceivedEventHandler(action.port_DataReceived);
        }

        public static void ArreterServeur()
        {            
            _Sp.Close();
            _Sp.Dispose();
            _Sp = null;
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(TimeWait));

            // Si le port est ouvert, le fermer
            if (!_Sp.IsOpen) return;

            string message = _Sp.ReadLine();

            if (message.Equals(DETECT))
            {
                _IsDectect = true;
            }

            if (message.Equals(BUTTON) && _IsDectect)
            {
                Lancer();
                _IsDectect = false;
            }

        }       
    }
}

