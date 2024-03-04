using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;

namespace ApplicationDuVin
{
    public class Vin
    {
        public double alcohol { get; set; }
        public double sulphates { get; set; }

        [Name("citric acid")]
        public double citricacid { get; set; }
        [Name("volatile acidity")]
        public double volatileacidity { get; set; }
        public int quality { get; set; }
        public Vin(double alcohol, double sulphates, double citricAcid, double volatileAcidity, int quality)
        {
            this.alcohol = alcohol;
            this.sulphates = sulphates;
            this.citricacid = citricAcid;
            this.volatileacidity = volatileAcidity;
            this.quality = quality;
        }
        public Vin()
        {
        }

        public static List<Vin> ImporterDonneesCSV<Vin>(string cheminFichierCsv)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true
            };
            using (var reader = new StreamReader(cheminFichierCsv))
            using (var csv = new CsvReader(reader, config))
            {
                return csv.GetRecords<Vin>().ToList();
            }
        }

        public double GetAttributeValue(int attributeIndex) //pour generalisation(int attributeIndex, List<string> list) biblio
        {
            switch (attributeIndex)
            {
                case 0:
                    return alcohol;
                case 1:
                    return sulphates;
                case 2:
                    return citricacid;
                case 3:
                    return volatileacidity;
                case 4:
                    return quality;
                default:
                    return -1;
            }
        }


         public static int GetAttributeIndex(string attributeName) //pour generalisation(int attributeIndex, List<string> list) biblio
        {
            if (attributeName == null)
                return -1;

            switch (attributeName)
            {
                case "alcohol":
                    return 0;
                case "sulphates":
                    return 1;
                case "citric acid":
                    return 2;
                case "volatile acidity":
                    return 3;
                default:
                    return -1;
            }
        }
    }


}
