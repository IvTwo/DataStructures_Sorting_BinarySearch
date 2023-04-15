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

            // Read the CSV records into a list
            PlayerRecords[] records = csv.GetRecords<PlayerRecords>().ToArray();
            List<PlayerRecords> recordList = new List<PlayerRecords>(records);
            #endregion

            PrintRecords(recordList);

            string[] columns = {"User_Name", "Weekly_Minutes_Played" };
            Quicksort_CSV.Quicksort(recordList, 0, recordList.Count-1, columns);

            PrintRecords(recordList);
            
            //// take user input. If there are X arguments then the progrma knows to sort by multiple column
            //// otherwise the program defults to sorting by username
            //string[] userInput = UserInput().Split(' ');
            //string[] columns = userInput.Length > 0 ? userInput : new string[] { "User_Name" };
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
        public static void PrintRecords(List<PlayerRecords> records)
        {
            Console.WriteLine();
            Console.WriteLine("Record List");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine(string.Format("  {0,-12} | {1,-6} | {2,-14} | {3,-10}", "Username", "Game", "Weekly Minutes", "Weekly IAP"));
            Console.WriteLine("-----------------------------------------------------");
            foreach (var record in records)
            {
                Console.WriteLine(string.Format("  {0,-12} | {1,-6} | {2,-14} | {3,-10}", record.User_name, record.Game, record.Weekly_Minutes_Played, record.Weekly_IAP));
            }
            Console.WriteLine("-----------------------------------------------------");
        }
    }
}