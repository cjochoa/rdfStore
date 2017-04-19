using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightstarDB.Client;

namespace BrightstarDB.Samples.EntityFramework.Foaf
{
    /// <summary>
    /// This console application shows how the BrightStar entity framework can be mapped onto existing RDF data
    /// More information about this sample project is in the documentation located at [[INSTALLERDIR]]\Docs
    /// </summary>
    class Program
    {
        private static void Main()
        {
            // Initialise license and stores directory location
            SamplesConfiguration.Register();

            //create a unique store name
            var storeName = "foaf_" + Guid.NewGuid();

            //connection string to the BrightstarDB service
            var connectionString = String.Format(@"Type=embedded;storesDirectory={0};",
                                                 SamplesConfiguration.StoresDirectory);

            //Load some  data into the store
            AddDataUsingEntityFramework(connectionString, storeName);

            //Connect to the store via the entity framework and loop through the entities
            PrintOutUsingEntityFramework(connectionString, storeName);

            // Shutdown Brightstar processing threads.
            BrightstarService.Shutdown();

            Console.WriteLine();
            Console.WriteLine("Finished. Press the Return key to exit.");
            Console.ReadLine();
        }

        /// <summary>
        /// Load some RDF data into the store
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="storeName"></param>
        static void AddDataUsingEntityFramework(string connectionString, string storeName)
        {
            Console.WriteLine("Print out store data using Entity Framework...");
            //connect to the same store using the BrightstarDB entity framework context
            var context = new FoafContext(connectionString + "StoreName=" + storeName);

            var person1 = new Person { Name = "John Doe", Emails = new List<string>() { "john@a.com", "doe@b.com" } };

            context.Persons.Add(person1);
            context.SaveChanges();
        }

        /// <summary>
        /// Access the RDF data using the entity framework
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="storeName"></param>
        static void PrintOutUsingEntityFramework(string connectionString, string storeName)
        {
            Console.WriteLine("Print out store data using Entity Framework...");
            //connect to the same store using the BrightstarDB entity framework context
            var context = new FoafContext(connectionString + "StoreName=" + storeName);
            
            //loop through all the Person entities and print out their properties and other people that they 'know'
            Console.WriteLine(@"{0} people found in raw RDF data", context.Persons.Count());
            Console.WriteLine();
            foreach (var person in context.Persons.ToList())
            {
                Console.WriteLine("PERSON name: {0} emails:[{1}]", person.Name, string.Join(",", person.Emails));                
            }
        }

        #region Names
        private static readonly List<string> Firstnames = new List<string>
                                 {
                                     "Jen",
                                     "Kal",
                                     "Gra",
                                     "Andy",
                                     "Jessica",
                                     "Adam",
                                     "Trevor",
                                     "Morris",
                                     "Paul",
                                     "Jane",
                                     "Elliot",
                                     "Annie",
                                     "Rob",
                                     "Mark",
                                     "Tim",
                                     "Gemma",
                                     "Clare",
                                     "Anna",
                                     "Tessa",
                                     "Julia",
                                     "David",
                                     "Andrew",
                                     "Charlie",
                                     "Aled",
                                     "Alex"
                                 };
        private static readonly List<string> Surnames = new List<string>
                                 {
                                     "Wilson",
                                     "Foster",
                                     "Green",
                                     "Fahy",
                                     "Goldsack",
                                     "Webb",
                                     "Fernley",
                                     "McKee",
                                     "Hughes",
                                     "Wong",
                                     "Sully",
                                     "Hague",
                                     "Boyce",
                                     "Pegeot",
                                     "Chappell",
                                     "East",
                                     "Tate",
                                     "Wade",
                                     "Lloyd",
                                     "Hopwseith",
                                     "Matthews",
                                     "Lacey",
                                     "Skipper",
                                     "Chandler",
                                     "Jones"
                                 };

        private static readonly List<string> Employers = new List<string>{"Networked Planet", "Microsoft", "BBC", "Oxford University", "Wicked Skatewear"};
        #endregion
    }
}
