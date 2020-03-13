using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace AppliMariage.Models
{
    public class Roulette : INotifyPropertyChanged, IEquatable<Roulette>, ICloneable
    {
        public Roulette()
        {
            Guid = Guid.NewGuid();
            Resultat = String.Empty;
            Source = null;
        }

        private Guid _Guid;
        [XmlIgnore]
        public Guid Guid
        {
            get { return _Guid; }
            private set { _Guid = value; OnPropertyChanged("Guid"); }
        }

        private String _Resultat;
        public String Resultat
        {
            get { return _Resultat; }
            set { _Resultat = value; OnPropertyChanged("Resultat"); }
        }

        private String _Source;
        public String Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }

        private Color _Couleur;
        public Color Couleur
        {
            get { return _Couleur; }
            set { _Couleur = value; OnPropertyChanged("Couleur"); }
        }      

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Equals(Roulette other)
        {
            return other.Resultat.Equals(this.Resultat);
        }

        public object Clone()
        {
            Roulette roulette = new Roulette();
            roulette.Resultat = this.Resultat;
            roulette.Source = this.Source;
            return roulette;
        }
    }
}
