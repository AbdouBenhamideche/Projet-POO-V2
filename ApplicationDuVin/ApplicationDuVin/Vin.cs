using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace ApplicationDuVin
{
    public class Vin
    {
        public double alcohol { get; set; }
        public double sulphates { get; set; }
        public double citricAcid { get; set; }
        public double volatileAcidity { get; set; }
        public int quality { get; set; }
        public Vin(double alcohol, double sulphates, double citricAcid, double volatileAcidity, int quality)
        {
            this.alcohol = alcohol;
            this.sulphates = sulphates;
            this.citricAcid = citricAcid;
            this.volatileAcidity = volatileAcidity;
            this.quality = quality;
        }

        public static List<Vin> ImporterDonneesCSV<Vin>(string cheminFichierCsv)
        {
            using (var reader = new StreamReader(cheminFichierCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
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
                    return citricAcid;
                case 3:
                    return volatileAcidity;
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
                case "citricAcid":
                    return 2;
                case "volatileAcidity":
                    return 3;
                default:
                    return -1;
            }
        }
    }


}
