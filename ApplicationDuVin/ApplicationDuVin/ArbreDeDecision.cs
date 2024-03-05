using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    public class ArbreDeDecision : IDecisionTree
        
    {

   
        
        public Node BuildTree(List<Vin> data, List<string> attributes)
        {

            
            Console.WriteLine(data.First().alcohol);
            if (data.All(d => d.quality == data.First().quality)) //Cette condition vérifie si tous les vins dans l'ensemble de données ont la même qualité. Si c'est le cas, cela signifie que tous les vins ont la même classe, donc nous retournons un nœud feuille avec cette classe comme prédiction
            {
                Console.WriteLine (data.First());
                return new Node { Class = data.First().quality.ToString() };
            }

            if (attributes.Count == 0)//Cette condition vérifie s'il ne reste plus d'attributs à diviser. Si c'est le cas, cela signifie que nous avons épuisé tous les attributs disponibles pour diviser les données, donc nous retournons un nœud feuille avec la classe majoritaire comme prédiction.
            {
                string majorityClass = data.GroupBy(d => d.quality)
                                           .OrderByDescending(g => g.Count())
                                           .First()
                                           .Key
                                           .ToString();
                return new Node { Class = majorityClass };
            }

            string bestAttribute = GetBestAttribute(data, attributes, out double? splitValue);//Cette ligne appelle une méthode GetBestAttribute pour obtenir le meilleur attribut pour diviser les données
            Console.WriteLine(bestAttribute);
       
            Node node = new Node { Attribute = bestAttribute, Children = new Dictionary<string, Node>(), SplitValue = splitValue };//Cette ligne crée un nouveau nœud de l'arbre de décision avec l'attribut sélectionné, les enfants (qui seront remplis plus tard) et la valeur de division (pour les attributs numériques).

            if (splitValue.HasValue)
            {
                node.SplitDataNumeric(data, bestAttribute, splitValue.Value, out List<Vin> leftSubset, out List<Vin> rightSubset);
                node.Children.Add("Left", BuildTree(leftSubset, attributes));
                node.Children.Add("Right", BuildTree(rightSubset, attributes));
            }
            //else
            //{
            //    node.SplitDataCategorical(data, bestAttribute, out Dictionary<string, List<Vin>> subsets);
            //    foreach (var value in subsets.Keys)
            //    {
            //        node.children.add(value, buildtree(subsets[value], attributes));
            //    }
            //}

            return node;
        }

        public string Classify(string[] instance)
        {
            // Implémentez la logique de classification ici
            throw new NotImplementedException();
        }

        public string Classify(string[] instance, Node node)
        {
            // Implémentez la logique de classification à partir d'un nœud spécifique ici
            throw new NotImplementedException();
        }

        public string GetBestAttribute(List<Vin> data, List<string> attributes, out double? splitValue)
        {
            splitValue = null;
            double maxInformationGain = double.MinValue;
            string bestAttribute = null;

            foreach (string attribute in attributes)
            {
                int attributeIndex = Vin.GetAttributeIndex(attribute);

                if (IsNumericAttribute(data, attributeIndex))
                {
                    double? currentSplitValue; // Change type to double?

                    double informationGain = CalculateInformationGainNumeric(data, attributeIndex, out currentSplitValue);

                    if (informationGain > maxInformationGain)
                    {
                        maxInformationGain = informationGain;
                        bestAttribute = attribute;
                        splitValue = currentSplitValue; //donner la valeur de division 
                    }
                }
                else
                {
                    double informationGain = CalculateInformationGain(data, attributeIndex);

                    if (informationGain > maxInformationGain)
                    {
                        maxInformationGain = informationGain;
                        bestAttribute = attribute;
                        splitValue = null;
                    }
                }
            }

            return bestAttribute;
        }


        public double CalculateInformationGainNumeric(List<Vin> data, int attributeIndex, out double? splitValue)
        {
            // Sort the data by the selected numeric attribute
            data.Sort((a, b) => a.GetAttributeValue(attributeIndex).CompareTo(b.GetAttributeValue(attributeIndex)));

            int totalRecords = data.Count;

            // Initialize variables for tracking the best split point and its information gain
            splitValue = null;
            double bestInformationGain = 0.0;

            // Iterate over the data to find the best split point
            for (int i = 1; i < totalRecords; i++)
            {
                double currentValue = data[i].GetAttributeValue(attributeIndex);
                double previousValue = data[i - 1].GetAttributeValue(attributeIndex);

                // Calculate potential split value as the average of adjacent values
                double potentialSplitValue = (currentValue + previousValue) / 2.0;

                // Split the data into two groups based on the potential split value
                var leftGroup = data.GetRange(0, i);
                var rightGroup = data.GetRange(i, totalRecords - i);

                // Calculate information gain for the split
                double currentInformationGain = CalculateInformationGain(data, attributeIndex);

                // Update the best split if the current information gain is greater
                if (currentInformationGain > bestInformationGain)
                {
                    bestInformationGain = currentInformationGain;
                    splitValue = potentialSplitValue;
                }
            }

            return bestInformationGain;
        }

        public double CalculateInformationGain(List<Vin> data, int attributeIndex)
        {
            if (data == null || data.Count == 0)
                throw new ArgumentException("Data list is empty or null.");

            int totalRecords = data.Count;

            
            double initialEntropy = CalculateEntropyList(data);

            //la methode GetAttributeValue est definie dans la classe Vin
            var groupedByAttribute = data.GroupBy(vin => vin.GetAttributeValue(attributeIndex));

            // Calculate the weighted entropy after the split
            double weightedEntropy = 0.0;

            foreach (var group in groupedByAttribute)
            {
                List<Vin> groupData = group.ToList();
                double groupWeight = (double)groupData.Count / totalRecords;

                // Calculate the entropy for the subgroup and add to the weighted entropy
                weightedEntropy += groupWeight * CalculateEntropyList(groupData);
            }

           
            double informationGain = initialEntropy - weightedEntropy;

            return informationGain;
        }


        public double CalculateEntropyList(List<Vin> data)
        {
            int totalSamples = data.Count;
            var qualityCounts = data.GroupBy(s => s.quality)
                                       .Select(g => new { Quality = g.Key, Count = g.Count() });

            double entropy = 0.0;

            foreach (var count in qualityCounts)
            {

                double proportion = (double)count.Count / totalSamples;
                entropy -= proportion * Math.Log(proportion, 2);
            }

            return entropy;
        }

        public int GetMostCommonClass(List<Vin> data)
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("Data list is empty or null.");
            }

            Dictionary<int, int> classCounts = new Dictionary<int, int>();

            // Count occurrences of each class
            foreach (Vin vin in data)
            {
                int currentClass = vin.quality;

                if (classCounts.ContainsKey(currentClass))
                {
                    classCounts[currentClass]++;
                }
                else
                {
                    classCounts[currentClass] = 1;
                }
            }

            // Find the class with the maximum count
            int mostCommonClass = -1;
            int maxCount = 0;

            foreach (var classCount in classCounts)
            {
                if (classCount.Value > maxCount)
                {
                    maxCount = classCount.Value;
                    mostCommonClass = classCount.Key;
                }
            }

            return mostCommonClass;
        }

        public List<double> GetAttributeValues(List<Vin> data, int attributeIndex)
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException("Data list is empty or null.");
            }

            List<double> uniqueValues = new List<double>();

            foreach (Vin vin in data)
            {
                double attributeValue = vin.GetAttributeValue(attributeIndex);


                if (!uniqueValues.Contains(attributeValue))
                {
                    uniqueValues.Add(attributeValue);
                }
            }

            return uniqueValues;
        }

        public bool IsNumericAttribute(List<Vin> data, int attributeIndex)
        {
            // En supposant que 0 à 3 indices représentent des attributs numériques et 4 est non numérique (qualité)
            if (attributeIndex >= 0 && attributeIndex <= 3)
            {
                return true;
            }
            else {  return false; }
        }

        public float Evaluate(string[] instances, string[] predictedLabels, string[] expertLabels)
        {
            // Implémentez la logique pour évaluer les prédictions par rapport aux étiquettes d'experts
            throw new NotImplementedException();
        }

        public void ConfusionMatrix(string[] predictedLabels, string[] expertLabels, string[] labels)
        {
            // Implémentez la logique pour créer une matrice de confusion
            throw new NotImplementedException();
        }




        public (List<Vin> leftData, List<Vin> rightData) SplitDataCategorical(List<Vin> data, int attributeIndex)
        {
            List<Vin> leftData = new List<Vin>();
            List<Vin> rightData = new List<Vin>();

            // Obtenez toutes les valeurs uniques de l'attribut
            List<double> uniqueValues = GetAttributeValues(data, attributeIndex);

            // Choisissez arbitrairement la première valeur comme valeur de séparation
            double splitValue = uniqueValues.FirstOrDefault();

            foreach (Vin vin in data)
            {
                double attributeValue = vin.GetAttributeValue(attributeIndex);

                if (attributeValue == splitValue)
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
    }
}
