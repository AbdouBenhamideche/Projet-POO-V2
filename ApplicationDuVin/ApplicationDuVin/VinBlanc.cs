using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    internal class VinBlanc : Vin
    {
        private float concentration;
        private float intensitéArômes;
        public VinBlanc(float alcohol, float sulphates, float citricAcid, double volatileAcidity, int quality, float concentration, float intensitéArômes) : base(alcohol, sulphates, citricAcid,volatileAcidity, quality) {
           this.concentration = concentration;
           this.intensitéArômes = intensitéArômes;
        }

        public float Concentration { get {  return concentration; } set {  concentration = value; } }
        public float IntensitéArômes { get { return intensitéArômes;} set { intensitéArômes=value; } }
    }
}
