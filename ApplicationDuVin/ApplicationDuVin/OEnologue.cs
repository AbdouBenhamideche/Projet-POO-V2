using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    internal class OEnologue : Utilisateur
    {

        public OEnologue(int identifiant, string nom, string contact) : base(identifiant, nom, contact)
        {
        }

        public override void AfficherDetails()
        {
            Console.WriteLine($"Œnologue - ID: {identifiant}, Nom: {nom}, Contact: {contact}");
        }
    }

}
