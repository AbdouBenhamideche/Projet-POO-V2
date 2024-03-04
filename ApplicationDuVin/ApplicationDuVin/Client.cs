using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDuVin;

namespace ApplicationDuVin
{
    internal class Client : Utilisateur
    {
        public Client(int identifiant, string nom, string contact) : base(identifiant, nom, contact)
        {

        }
        
        public override void AfficherDetails()
        {
            Console.WriteLine($"Client - ID: {identifiant}, Nom: {nom}, Contact: {contact}");
        }




    }
}

/*namespace ApplicationDuVin
{
    public class Vin
    {
        public double alcohol { get; set; }
        public double sulphates { get; set; }
        public double citricAcid { get; set; }
        public double volatileAcidity { get; set; }
        public int quality { get; set; }

        public Vin(double alcohol, double sulphates, double citricAcid, int quality)
        {
            this.alcohol = alcohol;
            this.sulphates = sulphates;
            this.citricAcid = citricAcid;
            this.quality = quality;
        }

        public Vin()
        {
        }
    }

    public class VinMap : ClassMap<Vin>
    {
        public VinMap()
        {
            Map(v => v.alcohol).Name("alcohol");
            Map(v => v.sulphates).Name("sulphates");
            Map(v => v.citricAcid).Name("citricAcid");
            Map(v => v.quality).Name("quality");
        }
    }

    public static class CsvImporter
    {
        public static List<T> ImporterDonneesCSV<T, TMap>(string cheminFichierCsv)
            where T : class
            where TMap : ClassMap<T>
        {
            using (var reader = new StreamReader(cheminFichierCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<TMap>();
                return csv.GetRecords<T>().ToList();
            }
        }
    }
}*/


/*string cheminFichierCsv = "C:/Users/bahlo/Desktop/TP 01 POO/Données - Qualité du Vin/train_reduced.csv";

// Lecture des données du fichier CSV dans une liste de Vins
List<Vin> vins = CsvImporter.ImporterDonneesCSV<Vin, VinMap>(cheminFichierCsv);

// Affichage des données(juste pour la verification)
foreach (Vin vin in vins)
{
    Console.WriteLine($"Alcohol: {vin.alcohol}, Sulphates: {vin.sulphates}, Critic Acid: {vin.citricAcid}, Quality: {vin.quality}");
}*/
