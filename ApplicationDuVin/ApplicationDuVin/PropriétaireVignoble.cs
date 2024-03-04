using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    internal class PropriétaireVignoble : Utilisateur
    {
        public PropriétaireVignoble(int identifiant, string nom, string contact) : base(identifiant, nom, contact)
        {
        }

        public override void AfficherDetails()
        {
            Console.WriteLine($"Propriétaire Vignoble - ID: {identifiant}, Nom: {nom}, Contact: {contact}");
        }

    }
}
