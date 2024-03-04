using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    internal class GestionUtilisateur
    {

        private List<Client> clients = new List<Client>();
        private List<OEnologue> oenologues = new List<OEnologue>();
        private List<PropriétaireVignoble> proprietairesVignoble = new List<PropriétaireVignoble>();

        public void AjouterUtilisateur(Utilisateur utilisateur)
        {
            if (utilisateur is Client)
            {
                clients.Add((Client)utilisateur);
            }
            else if (utilisateur is OEnologue)
            {
                oenologues.Add((OEnologue)utilisateur);
            }
            else if (utilisateur is PropriétaireVignoble)
            {
                proprietairesVignoble.Add((PropriétaireVignoble)utilisateur);
            }

            Console.WriteLine("Utilisateur ajouté avec succès.");
        }

        public void ModifierUtilisateur(Utilisateur utilisateur)
        { 
            Console.WriteLine($"Modification de l'utilisateur avec ID {utilisateur.Identifiant}");
        }

        public void SupprimerUtilisateur(Utilisateur utilisateur)
        {
            if (utilisateur is Client)
            {
                clients.Remove((Client)utilisateur);
            }
            else if (utilisateur is OEnologue)
            {
                oenologues.Remove((OEnologue)utilisateur);
            }
            else if (utilisateur is PropriétaireVignoble)
            {
                proprietairesVignoble.Remove((PropriétaireVignoble)utilisateur);
            }

            Console.WriteLine("Utilisateur supprimé avec succès.");
        }

        public void AfficherDetailsUtilisateurs()
        {
            Console.WriteLine("Clients:");
            foreach (var client in clients)
            {
                client.AfficherDetails();
            }

            Console.WriteLine("\nŒnologues:");
            foreach (var oenologue in oenologues)
            {
                oenologue.AfficherDetails();
            }
            
            Console.WriteLine("\nPropriétaires de Vignoble:");
            foreach (var proprietaire in proprietairesVignoble)
            {
                proprietaire.AfficherDetails();
            }
        }
    }


}

