using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    internal class VinRouge : Vin
    {
        private string cépage;
        public VinRouge(float alcohol, float sulphates, float criticAcid, double volatileAcidity, int quality, string cépage) : base(alcohol, sulphates, criticAcid, volatileAcidity, quality)
        {
            this.cépage = cépage;
        }
        public string Cépage { get { return cépage; } set { cépage=value;} }
        
    }
}
