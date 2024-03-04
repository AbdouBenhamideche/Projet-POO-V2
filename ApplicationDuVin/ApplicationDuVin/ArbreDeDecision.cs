using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDuVin
{
    public class ArbreDeDecision : IDecisionTree
    {
        /*public Node BuildTree(List<Vin> data, List<string> attributes)
        {
            // Implémentez la logique de construction de l'arbre de décision ici
            // Utilisez les méthodes auxiliaires pour les calculs nécessaires
            // Retournez le nœud racine de l'arbre

            // Vérifiez s'il n'y a pas de données ou d'attributs
            if (data == null || data.Count == 0 || attributes == null || attributes.Count == 0)
            {
                throw new ArgumentException("Invalid data or attributes.");
            }

            // Obtenez la classe la plus fréquente dans les données
            int mostCommonClass = GetMostCommonClass(data);

            // Si toutes les instances ont la même classe, créez une feuille avec cette classe
            if (data.All(vin => vin.quality == mostCommonClass))
            {
                return new Node(true, mostCommonClass);
            }

            // Si aucun attribut n'est disponible, créez une feuille avec la classe majoritaire
            if (attributes.Count == 0)
            {
                return new Node(true, mostCommonClass);
            }

            // Trouvez l'attribut qui donne le meilleur gain d'information
            string bestAttribute = GetBestAttribute(data, attributes, out double? splitValue);

            // Créez un nouveau nœud avec l'attribut sélectionné
            Node node = new Node(false, null, attributes.IndexOf(bestAttribute), splitValue);

            // Divisez les données en fonction de l'attribut sélectionné
            List<Vin> leftData, rightData;
            if (splitValue.HasValue && IsNumericAttribute(data, node.SplitAttributeIndex))
            {
                (leftData, rightData) = node.SplitDataNumeric(data, node.SplitAttributeIndex, splitValue.Value);
            }
            else
            {
                (leftData, rightData) = SplitDataCategorical(data, node.SplitAttributeIndex);
            }

            // Récursivement construisez les sous-arbres
            node.LeftChild = BuildTree(leftData, attributes.Except(new List<string> { bestAttribute }).ToList());
            node.RightChild = BuildTree(rightData, attributes.Except(new List<string> { bestAttribute }).ToList());

            return node;
        }*/

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
                        splitValue = currentSplitValue;
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
            // Assuming that 0 to 3 indices represent numeric attributes and 4 is non-numeric (quality)
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
