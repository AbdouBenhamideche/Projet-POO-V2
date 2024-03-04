using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using ApplicationDuVin;





namespace ApplicationDuVin
{

    internal class Program
    {


        static void Main()
        {
            /* GestionUtilisateur gestionnaire = new GestionUtilisateur();
             OEnologue anais = new OEnologue(2, "Oenologue1", "ContactOenologue1");
             // Exemple d'ajout d'utilisateurs
             gestionnaire.AjouterUtilisateur(new Client(1, "Client1", "ContactClient1"));
             gestionnaire.AjouterUtilisateur(anais);
             gestionnaire.AjouterUtilisateur(new PropriétaireVignoble(3, "Proprietaire1", "ContactProprietaire1"));

             // Exemple d'affichage des détails des utilisateurs
             gestionnaire.AfficherDetailsUtilisateurs();

             // Exemple de suppression d'utilisateur
             gestionnaire.SupprimerUtilisateur(anais);

             // Affichage mis à jour après la suppression
             gestionnaire.AfficherDetailsUtilisateurs();
            */


            string cheminFichierCsv = @"DONNE/train_reduced.csv";
  
            // Lecture des données du fichier CSV dans une liste de Vins
            List<Vin> vins = Vin.ImporterDonneesCSV<Vin>(cheminFichierCsv);

            // Affichage des données(juste pour la verification)
            foreach (Vin vin in vins)
            {
                Console.WriteLine($"Alcohol: {vin.alcohol}, Sulphates: {vin.sulphates}, Critic Acid: {vin.citricacid}, Quality: {vin.quality}");
            }
            ArbreDeDecision arbreDeDecision = new ArbreDeDecision();
            //entropy
            Console.WriteLine(arbreDeDecision.CalculateEntropyList(vins));
            //gain
            for (int i = 0; i < 4; i++) {
                Console.WriteLine(arbreDeDecision.CalculateInformationGain(vins,  i));
            }


            //test pour GetAttributeValues
            List<double> uniqueValues = arbreDeDecision.GetAttributeValues(vins, 0);
            Console.WriteLine("Valeurs uniques de l'attribut :");
            foreach (double value in uniqueValues)
            {
                Console.WriteLine(value);
            }



            //test pour GetBestAttribute
            List<string> attributes = new List<string>() { "alcohol", "sulphates", "citricAcid", "quality" };
            Console.WriteLine(arbreDeDecision.GetBestAttribute(vins, attributes, out double? splitValue));



            //test
            Console.WriteLine(arbreDeDecision.GetMostCommonClass(vins));







        }


    }
}



       





