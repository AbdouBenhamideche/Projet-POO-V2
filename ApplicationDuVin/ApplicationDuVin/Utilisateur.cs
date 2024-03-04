using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    internal abstract class Utilisateur
    {
        protected int identifiant;
        protected string nom;
        protected string contact;
        public Utilisateur(int identifiant, string nom, string contact)
        {
            this.identifiant = identifiant;
            this.nom = nom;
            this.contact = contact;
        }

        public int Identifiant {  
         get { return identifiant; } 
         set { identifiant = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        public virtual void AfficherDetails() { }
    }
}
