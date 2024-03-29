﻿using CsvHelper;
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
            


            string cheminFichierCsv = @"DONNE/train_reduced.csv";
  
            // Lecture des données du fichier CSV dans une liste de Vins
            List<Vin> vins = Vin.ImporterDonneesCSV<Vin>(cheminFichierCsv);

            
            ArbreDeDecision arbreDeDecision = new ArbreDeDecision();
            
            
            //entropy
            Console.WriteLine("l'entropie"+arbreDeDecision.CalculateEntropyList(vins));



            //gain
            for (int i = 0; i < 4; i++) {
                Console.WriteLine("le gain" + arbreDeDecision.CalculateInformationGain(vins,  i));
            }


            ////test pour GetAttributeValues
            //List<double> uniqueValues = arbreDeDecision.GetAttributeValues(vins, 0);
            //Console.WriteLine("Valeurs uniques de l'attribut :");
            //foreach (double value in uniqueValues)
            //{
            //    Console.WriteLine(value);
            //}
            //Console.WriteLine();


            //test pour GetBestAttribute




            List<string> attributes = new List<string>() {"alcohol", "sulphates", "citric acid", "volatile acidity", "quality" };
            Console.WriteLine(arbreDeDecision.GetBestAttribute(vins, attributes, out double? splitValue));



            ////test
            //Console.WriteLine(arbreDeDecision.GetMostCommonClass(vins));

            //Console.WriteLine();
            //Console.WriteLine(vins.First().alcohol);






            //faire l'arbre
            Node root;
             root = arbreDeDecision.BuildTree(vins, attributes);


            void PrintTree(Node node, string indent = "")
            {
                if (node == null)
                    return;

                if (node.Attribute != null)
                {
                    Console.WriteLine(indent + node.Attribute + ":");
                    foreach (var child in node.Children)
                    {
                        Console.WriteLine(indent + "|_" + child.Key);
                        PrintTree(child.Value, indent + "|  ");
                    }
                }
                else
                {
                    Console.WriteLine(indent + "Class: " + node.Class);
                }
            }
            PrintTree(root, "");
            //lire l arbre










        }


    }
}



       





