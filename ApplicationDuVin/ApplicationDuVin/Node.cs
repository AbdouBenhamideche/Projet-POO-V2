using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationDuVin
{
    public class Node
    {
        public string Attribute { get; set; }
        public Dictionary<string, Node> Children { get; set; }
        public int Class { get; set; }  
        public double? SplitValue { get; set; }



        public void SplitDataNumeric(List<Vin> data, string attribute, double splitValue, out List<Vin> leftSubset, out List<Vin> rightSubset)
        {
            
            Console.WriteLine(splitValue.ToString() + " est la valeur de separation");
            
            leftSubset = new List<Vin>();
            rightSubset = new List<Vin>();

            foreach (Vin vin in data)
            {
                int attributeIndex = Vin.GetAttributeIndex(attribute);
                double attributeValue = vin.GetAttributeValue(attributeIndex);

                // Si la valeur de l'attribut est inférieure ou égale au seuil, ajoutez le vin au sous-ensemble gauche
                if (attributeValue <= splitValue)
                {
                    leftSubset.Add(vin);
                }
                // Sinon, ajoutez le vin au sous-ensemble droit
                else
                {
                    rightSubset.Add(vin);
                }
            }
        }
    }




    // Méthode pour diviser les données catégoriques




}
 
