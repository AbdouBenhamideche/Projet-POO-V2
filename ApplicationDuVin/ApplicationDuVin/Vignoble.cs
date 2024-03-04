using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    internal class Vignoble : Terrain
    {
        private PropriétaireVignoble propriétaireVignoble;

        public Vignoble(int largeur, int longueur, string emplacement, PropriétaireVignoble propriétaireVignoble) : base(largeur, longueur, emplacement)
        {
            this.propriétaireVignoble = propriétaireVignoble;
        }

        public PropriétaireVignoble PropriétaireVignoble { get { return propriétaireVignoble; } set { propriétaireVignoble = value; } }
    }
}
