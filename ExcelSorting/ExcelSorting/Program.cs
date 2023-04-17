using CsvHelper;
using CsvHelper.Configuration;
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

            UserInterface(ref recordList);
            
        }

        public static void UserInterface(ref List<PlayerRecords> recordList)
        {
            bool keepGoing = true;
            int count = 0;
            while (keepGoing)
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to The Gamer Sort----!");
                Console.WriteLine();
                Console.WriteLine("Press \"q\" to quit");
                Console.WriteLine("Sort By...");
                

                // take user input. If there are X arguments then the progrma knows to sort by multiple column
                // otherwise the program defults to sorting by username
                Console.WriteLine();
                string userInput = UserInput();
                string[] columnArray = { "", "" };
                
                switch (userInput.ToLower())   // handle user input
                {
                    case "name time":
                        columnArray[0] = "User_Name";
                        columnArray[1] = "Weekly_Minutes_Played";
                        break;

                    case "name iap":
                        columnArray[0] = "User_Name";
                        columnArray[1] = "Weekly_IAP";
                        break;

                    case "game time":
                        columnArray[0] = "Game";
                        columnArray[1] = "Weekly_Minutes_Played";
                        break;

                    case "game iap":
                        columnArray[0] = "Game";
                        columnArray[1] = "Weekly_IAP";
                        break;

                    case "name":
                        columnArray[0] = "User_Name";
                        break;

                    case "game":
                        columnArray[0] = "Game";
                        break;

                    case "time":
                        columnArray[1] = "Weekly_Minutes_Played";
                        break;

                    case "iap":
                        columnArray[0] = "Weekly_IAP";
                        break;

                    case "q":
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine("---");
                        Console.WriteLine("Invalid Input!");
                        break;
                }
                string[] columns = userInput.Length > 0 ? columnArray : new string[] { "User_Name" };


                Quicksort_CSV.Quicksort(recordList, 0, recordList.Count-1, columns);
                PrintRecords(recordList);

                // continue?
                Console.WriteLine();
                Console.WriteLine("Would you like to continue (y = yes | n = no)");
                if (UserInput().Equals("n"))
                {
                    keepGoing = false;
                }

                // print results to a new csv file
                count++;
                string outputFile = "output" + count.ToString();
                using (StreamWriter writer = new StreamWriter(outputFile + ".csv"))
                using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.Context.RegisterClassMap<PlayerRecordMap>();
                    csvWriter.WriteRecords(recordList);
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

        public sealed class PlayerRecordMap : ClassMap<PlayerRecords>
        {
            public PlayerRecordMap()
            {
                Map(m => m.User_name).Name("User_name");
                Map(m => m.Game).Name("Game");
                Map(m => m.Weekly_Minutes_Played).Name("Weekly_Minutes_Played");
                Map(m => m.Weekly_IAP).Name("Weekly_IAP");
            }
        }
    }
}