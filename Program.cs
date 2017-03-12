using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(@"comuni.json"))
            {
                string json = file.ReadToEnd();

                var comuni = JsonConvert.DeserializeObject<List<Comune>>(json);

                PrintComuniOrderedByRegioneThenByProvincia(comuni);
            }
        }

        public static void PrintComuniOrderedByRegioneThenByProvincia(List<Comune> comuni)
        {
            var grouped = comuni.OrderBy(c => c.Regione.Nome).ThenBy(c => c.Provincia.Nome).GroupBy(c => c.Regione);

            var count = 1;

            foreach (var group in grouped)
            {
                foreach (var comune in group)
                {
                    Console.WriteLine("{0} - Regione: {1}, Provincia: {2}, Comune: {3}", count, comune.Regione.Nome, comune.Provincia.Nome, comune.Nome);

                    count++;
                }
            }
        }
    }

    public class Comune
    {
        public string Nome { get; set; }
        public Regione Regione { get; set; }
        public Regione Provincia { get; set; }
    }

    public class Regione
    {
        public string Nome { get; set; }

    }

    public class Provincia
    {
        public string Nome { get; set; }

    }
}