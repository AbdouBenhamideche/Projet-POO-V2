using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    internal class Terrain
    {
        private int largeur;
        private int longueur;
        private string emplacement;

        public Terrain(int largeur, int longueur, string emplacement)
        {
            this.largeur = largeur;
            this.longueur = longueur;
            this.emplacement = emplacement;
        }

        public int Largeur { get { return largeur; } set { largeur = value; } }
        public int Longueur { get { return longueur; } set {  longueur = value; } }
        public string Emplacement { get {  return emplacement; } set {  emplacement = value; } }

        public float CalculerSurface()
        {
            return largeur*longueur;
        }

    }
}
