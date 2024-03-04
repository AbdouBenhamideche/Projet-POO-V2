using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    internal class VinRosé : Vin
    {
        private string arôme;
        public VinRosé(float alcohol, float sulphates, float citricAcid, double volatileAcidity, int quality, string arôme): base(alcohol, sulphates, citricAcid, volatileAcidity, quality)
        {
            this.arôme = arôme;
        }
        public string Arôme { get { return arôme; } set {arôme = value; } }
    }
}
