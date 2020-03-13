using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AppliMariage.Models
{
    [Serializable]
    public class Roulettes
    {
        public static string PathRoulettes = ConfigurationManager.AppSettings.Get("pathRoulettes");
        
        public List<Roulette> ListeRoulette { get; set; }
    }
}
