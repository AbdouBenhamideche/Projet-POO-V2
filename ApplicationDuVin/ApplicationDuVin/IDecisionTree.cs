using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

    namespace ApplicationDuVin
{
        internal interface IDecisionTree
        {
            Node BuildTree(List<Vin > data, List<string> attributes);
            string Classify(string[] instance);
            string Classify(string[] instance, Node node);
            string GetBestAttribute(List<Vin> data, List<string> attributes, out double? splitValue);
            double CalculateInformationGainNumeric(List<Vin> data, int attributeIndex, out double? splitValue);
            double CalculateInformationGain(List<Vin> data, int attributeIndex);
            double CalculateEntropyList(List<Vin> data);
            int GetMostCommonClass(List<Vin> data);
            List<double> GetAttributeValues(List<Vin> data, int attributeIndex);
            bool IsNumericAttribute(List<Vin> data, int attributeIndex);
            float Evaluate(string[] instances, string[] predictedLabels, string[] expertLabels);
            void ConfusionMatrix(string[] predictedLabels, string[] expertLabels, string[] labels);
        }
    }

