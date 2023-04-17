using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection.PortableExecutable;

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

            #region Main UI
            bool keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to The Sort/Search Showcase----!");
                Console.WriteLine();
                Console.WriteLine("\t1. Sort\n" + "\t2. Search\n" + "\tq = Quit\n");

                switch (UserInput()) // handle user input
                {
                    case "1":
                        SortingUI(ref recordList);
                        break;

                    case "2":

                        break;

                    case "q":
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine("---");
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            }
            #endregion
        }

        // searching
        public static void SearchingUI(ref List<PlayerRecords> recordList)
        {
            bool keepGoing = true;
            int count = 0;
            while (keepGoing)
            {
                Console.WriteLine();
                Console.WriteLine("Press \"q\" to quit");
                Console.WriteLine("Find...");
                string userInput = UserInput();
                string[] words = userInput.Split(' ');

                int index = 0;
                string header = "";

                switch (words[0].ToLower())   // handle user input
                {
                    case "name":
                        header = "User_name";
                        break;

                    case "game":
                        header = "Game";
                        break;

                    case "time":
                        header = "Weekly_Minutes_Player";
                        break;

                    case "iap":
                        header = "Weekly_IAP";
                        break;

                    default:
                        Console.WriteLine("---");
                        Console.WriteLine("Invalid Input!");
                        break;
                }

                // Find the index of the first record that matches the search query
                index = BinarySearch_CSV.BinarySearch(recordList, words[1], header);

                // if no records match the querry
                if (index == -1)
                {
                    Console.WriteLine("No matching records found.");
                    return;
                }
                else    // otherwise print the records
                {
                    List<PlayerRecords> searchedRecords = new List<PlayerRecords>();
                    while (index < recordList.Count && recordList[index].GetValueByHeader(header) == words[1])
                    {
                        searchedRecords.Add(recordList[index]);
                    }
                    PrintRecords(searchedRecords);
                }
            }
        }

        // sorting
        public static void SortingUI(ref List<PlayerRecords> recordList)
        {
            bool keepGoing = true;
            int count = 0;
            while (keepGoing)
            {
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
                        columnArray[0] = "Weekly_Minutes_Played";
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

        // format for the csv output
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