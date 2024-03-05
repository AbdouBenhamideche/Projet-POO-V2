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






        // Méthode pour diviser les données catégoriques


        public void SplitDataNumeric(List<Vin> data, string attribute, double splitValue, out List<Vin> leftSubset, out List<Vin> rightSubset)
        {
            leftSubset= new List<Vin>();
            Console.WriteLine(0);
            
            leftSubset = data.Where(d => Convert.ToDouble(d.GetType().GetProperty(attribute).GetValue(d)) <= splitValue).ToList();
            
            rightSubset = data.Where(d => Convert.ToDouble(d.GetType().GetProperty(attribute).GetValue(d)) > splitValue).ToList();
        }

        public void SplitDataCategorical(List<Vin> data, string attribute, out Dictionary<string, List<Vin>> subsets)
        {
            subsets = new Dictionary<string, List<Vin>>();
            var attributeValues = data.Select(d => d.GetType().GetProperty(attribute).GetValue(d).ToString()).Distinct();
            foreach (var value in attributeValues)
            {
                subsets.Add(value, data.Where(d => d.GetType().GetProperty(attribute).GetValue(d).ToString() == value).ToList());
            }
        }
    }
}
 
