using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    internal class VinMousseux: Vin
    {
        private float quantiteDioxydeDeCarbone;
        private float pressionGazCarbonique;
        public VinMousseux(float alcohol, float sulphates, float citricAcid, double volatileAcidity, int quality, float quantiteDioxydeDeCarbone, float pressionGazCarbonique) : base(alcohol, sulphates, citricAcid, volatileAcidity, quality)
        {
            this.quantiteDioxydeDeCarbone=quantiteDioxydeDeCarbone;
            this.pressionGazCarbonique=pressionGazCarbonique;
        }

        public float QuantiteDioxydeDeCarbone { get { return quantiteDioxydeDeCarbone; } set { quantiteDioxydeDeCarbone = value; } }
        public float PressionGazCarbonique { get {  return pressionGazCarbonique; } set {  pressionGazCarbonique = value; } }
    }
}
