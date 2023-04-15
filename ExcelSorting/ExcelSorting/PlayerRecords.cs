using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;

namespace ExcelSorting
{
    internal class PlayerRecords
    {
        public string User_name { get; set; }
        public string Game { get; set; }
        public int Weekly_Minutes_Played { get; set; }
        public int Weekly_IAP { get; set; }

        // custom CompareTo() method
        public int CompareTo(PlayerRecords? other, string[] columns = null)
        {
            int comparison = 0; // stops the loop once a comparison is made

            for (int i = 0; i < columns.Length && comparison == 0; i++)
            {
                switch (columns[i])  // switch statement checks what column is being checked
                {
                    // Use nameof() to get the name string of properties.
                    case nameof(User_name):
                        comparison = User_name.CompareTo(other.User_name);
                        break;

                    case nameof(Game):
                        comparison = Game.CompareTo(other.Game);
                        break;

                    case nameof(Weekly_Minutes_Played):
                        comparison = Weekly_Minutes_Played.CompareTo(other.Weekly_Minutes_Played);
                        break;

                    case nameof(Weekly_IAP):
                        comparison = Weekly_IAP.CompareTo(other.Weekly_IAP);
                        break;

                    default:
                        comparison = User_name.CompareTo(other.User_name);
                        break;
                }
            }
            return comparison;
        }
    }
}
