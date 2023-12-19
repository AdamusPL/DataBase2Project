using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer.Generators
{
    internal class CSVGenerator : IEntityGenerator
    {
        public void Generate()
        {
            List<FieldOfStudy> fieldsOfStudies = [];

            fieldsOfStudies.Add(new FieldOfStudy("Informatyka Techniczna", 1, "w1"));
            fieldsOfStudies.Add(new FieldOfStudy("Informatyka Techniczna", 2, "w1"));
            fieldsOfStudies.Add(new FieldOfStudy("Informatyka Stosowana", 1, "w1"));
            fieldsOfStudies.Add(new FieldOfStudy("Informatyka Stosowana", 2, "w1"));
            fieldsOfStudies.Add(new FieldOfStudy("Informatyka Algorytmiczna", 1, "w1"));
            fieldsOfStudies.Add(new FieldOfStudy("Informatyka Algorytmiczna", 2, "w1"));
            fieldsOfStudies.Add(new FieldOfStudy("Informatyka Informatyczna", 1, "w1"));
            fieldsOfStudies.Add(new FieldOfStudy("Informatyka Informatyczna", 2, "w1"));
            fieldsOfStudies.Add(new FieldOfStudy("Elektoronika stosowana", 1, "w2"));
            fieldsOfStudies.Add(new FieldOfStudy("Elektoronika stosowana", 2, "w2"));
            fieldsOfStudies.Add(new FieldOfStudy("Elektoronika mikroprocesorowa", 1, "w2"));
            fieldsOfStudies.Add(new FieldOfStudy("Elektoronika mikroprocesorowa", 2, "w2"));
            fieldsOfStudies.Add(new FieldOfStudy("Automatyka i robotyka", 1, "w2"));
            fieldsOfStudies.Add(new FieldOfStudy("Automatyka i robotyka", 2, "w2"));
            fieldsOfStudies.Add(new FieldOfStudy("Chemia przemysłowa", 1, "w3"));
            fieldsOfStudies.Add(new FieldOfStudy("Chemia przemysłowa", 2, "w3"));
            fieldsOfStudies.Add(new FieldOfStudy("Chemia stosowana", 1, "w3"));
            fieldsOfStudies.Add(new FieldOfStudy("Chemia stosowana", 2, "w3"));
            fieldsOfStudies.Add(new FieldOfStudy("Chemia metaamfetaminowa", 1, "w3"));
            fieldsOfStudies.Add(new FieldOfStudy("Chemia metaamfetaminowa", 2, "w3"));
            fieldsOfStudies.Add(new FieldOfStudy("Techniki mikroprocesorowe", 1, "w4"));
            fieldsOfStudies.Add(new FieldOfStudy("Techniki mikroprocesorowe", 2, "w4"));
            fieldsOfStudies.Add(new FieldOfStudy("Techniki czarnej magii", 1, "w4"));
            fieldsOfStudies.Add(new FieldOfStudy("Techniki czarnej magii", 2, "w4"));
            fieldsOfStudies.Add(new FieldOfStudy("Techniki MS excel", 1, "w4"));
            fieldsOfStudies.Add(new FieldOfStudy("Techniki MS excel", 2, "w4"));
            fieldsOfStudies.Add(new FieldOfStudy("Górnictwo Śląskie", 1, "w5"));
            fieldsOfStudies.Add(new FieldOfStudy("Górnictwo Śląskie", 2, "w5"));
            fieldsOfStudies.Add(new FieldOfStudy("Górnictwo Dogecoina", 1, "w5"));
            fieldsOfStudies.Add(new FieldOfStudy("Górnictwo Dogecoina", 2, "w5"));
            fieldsOfStudies.Add(new FieldOfStudy("Górnictwo złota", 1, "w5"));
            fieldsOfStudies.Add(new FieldOfStudy("Górnictwo złota", 2, "w5"));
            fieldsOfStudies.Add(new FieldOfStudy("Górnictwo soli", 1, "w5"));
            fieldsOfStudies.Add(new FieldOfStudy("Górnictwo soli", 2, "w5"));
            fieldsOfStudies.Add(new FieldOfStudy("Matematyka prosta", 1, "w6"));
            fieldsOfStudies.Add(new FieldOfStudy("Matematyka prosta", 2, "w6"));
            fieldsOfStudies.Add(new FieldOfStudy("Matematyka trudna", 1, "w6"));
            fieldsOfStudies.Add(new FieldOfStudy("Matematyka trudna", 2, "w6"));
            fieldsOfStudies.Add(new FieldOfStudy("Fizyka atomowa", 1, "w7"));
            fieldsOfStudies.Add(new FieldOfStudy("Fizyka atomowa", 2, "w7"));
            fieldsOfStudies.Add(new FieldOfStudy("Fizyka kinematyczna", 1, "w7"));
            fieldsOfStudies.Add(new FieldOfStudy("Fizyka kinematyczna", 2, "w7"));
            fieldsOfStudies.Add(new FieldOfStudy("Fizyka optyczna", 1, "w7"));
            fieldsOfStudies.Add(new FieldOfStudy("Fizyka optyczna", 2, "w7"));
            fieldsOfStudies.Add(new FieldOfStudy("Czekanie stosowane", 1, "w8"));
            fieldsOfStudies.Add(new FieldOfStudy("Czekanie stosowane", 2, "w8"));
            fieldsOfStudies.Add(new FieldOfStudy("Drzemkologia stosowana", 1, "w8"));
            fieldsOfStudies.Add(new FieldOfStudy("Drzemkologia stosowana", 2, "w8"));
            fieldsOfStudies.Add(new FieldOfStudy("Budowanie mostów", 1, "w9"));
            fieldsOfStudies.Add(new FieldOfStudy("Budowanie mostów", 2, "w9"));
            fieldsOfStudies.Add(new FieldOfStudy("Budowanie dróg", 1, "w9"));
            fieldsOfStudies.Add(new FieldOfStudy("Budowanie dróg", 2, "w9"));
            fieldsOfStudies.Add(new FieldOfStudy("Budowanie budynków", 1, "w9"));
            fieldsOfStudies.Add(new FieldOfStudy("Budowanie budynków", 2, "w9"));
            fieldsOfStudies.Add(new FieldOfStudy("Architektura klasyczna", 1, "w10"));
            fieldsOfStudies.Add(new FieldOfStudy("Architektura klasyczna", 2, "w10"));
            fieldsOfStudies.Add(new FieldOfStudy("Architektura współczesna", 1, "w10"));
            fieldsOfStudies.Add(new FieldOfStudy("Architektura współczesna", 2, "w10"));
            fieldsOfStudies.Add(new FieldOfStudy("Metody Rutkowskie", 1, "w11"));
            fieldsOfStudies.Add(new FieldOfStudy("Metody Rutkowskie", 2, "w11"));
            fieldsOfStudies.Add(new FieldOfStudy("Śledzenie Łososi we fjordach", 1, "w11"));
            fieldsOfStudies.Add(new FieldOfStudy("Śledzenie Łososi we fjordach", 2, "w11"));
            fieldsOfStudies.Add(new FieldOfStudy("Robienie Hałasu", 1, "w12"));
            fieldsOfStudies.Add(new FieldOfStudy("Robienie Hałasu", 2, "w12"));
            fieldsOfStudies.Add(new FieldOfStudy("Lans i Bans", 1, "w12"));
            fieldsOfStudies.Add(new FieldOfStudy("Lans i Bans", 2, "w12"));

            var csv = new StringBuilder();

            foreach (var item in fieldsOfStudies)
            {
                var newLine = string.Format("{0};{1};{2}", item.Name, item.Degree, item.FacultyId);
                csv.AppendLine(newLine);
            }
            string path = Path.Combine(Environment.CurrentDirectory, @"Files\", "fieldsOfStudies.csv");
            File.WriteAllText(path, csv.ToString());
        }
    }
}
