using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    public class Node
    {
        public string Attribute { get; set; }
        public Dictionary<string, Node> Children { get; set; }
        public string Class { get; set; }
        public double? SplitValue { get; set; }




        public (List<Vin> leftData, List<Vin> rightData) SplitDataNumeric(List<Vin> data, int attributeIndex, double splitValue)
        {
            List<Vin> leftData = new List<Vin>();
            List<Vin> rightData = new List<Vin>();

            foreach (Vin vin in data)
            {
                double attributeValue = vin.GetAttributeValue(attributeIndex);

                if (attributeValue <= splitValue)
                {
                    leftData.Add(vin);
                }
                else
                {
                    rightData.Add(vin);
                }
            }

            return (leftData, rightData);
        }

        // Méthode pour diviser les données catégoriques





    }
}
 
