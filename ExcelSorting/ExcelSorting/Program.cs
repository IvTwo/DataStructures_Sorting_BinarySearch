using CsvHelper;
using System.Globalization;

namespace ExcelSorting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Read a CSV File
            // Path to the CSV file
            string path = "gamer_data.csv";

            // Create a reader object to read the CSV file
            StreamReader streamReader = new StreamReader(path);

            CsvReader csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            // Read the CSV records into an array
            PlayerRecords[] records = csv.GetRecords<PlayerRecords>().ToArray();
            #endregion

            PrintRecords(records);
        }

        public static void userInterface()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to the Card Guessing Game----!");
                Console.WriteLine();
                Console.WriteLine("\t1. Query\n" + "\t2. Guess\n" + "\t3. Quit\n");

                switch (UserInput()) // handle user input
                {
                    case "1":
                        break;

                    case "2":
                        break;

                    case "3":
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine("---");
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            }
        }

        // return the user input
        public static string UserInput()
        {
            Console.Write("\t * Your Input: ");
            return Console.ReadLine();
        }

        // print method for testing
        public static void PrintRecords(PlayerRecords[] records)
        {
            Console.WriteLine("Before sorting:");
            foreach (var record in records)
            {
                Console.WriteLine("Username: {0}, Game: {1}, Weekly Minutes: {0}, Weekly IAP: {0}",
                            record.Name, record.Game, record.Weekly_Minutes_Played, record.Weekly_IAP);
            }
        }

    }
}