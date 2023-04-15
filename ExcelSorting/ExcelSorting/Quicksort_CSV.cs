using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelSorting
{
    internal static class Quicksort_CSV
    {
        public static void Quicksort(List<PlayerRecords> records, int left, int right, string[] columns)
        {
            if (left < right)
            {
                int pivotIndex = Partition(records, left, right, columns);
                Quicksort(records, left, pivotIndex - 1, columns);
                Quicksort(records, pivotIndex+1, right, columns);
            }
        }

        // finds the proper index of the pivot point, swaps it to the right spot, an returns said index so
        // each side of the list can be sorted
        private static int Partition(List<PlayerRecords> records, int left, int right, string[] columns)
        {
            PlayerRecords pivot = records[right];   // change pivot to the right most node
            int num = left - 1;

            // finds correct index of the pivot point (counts how many items are > the value, which in turn
            // gives you the index
            for (int i = left; i < right; i++)
            {
                if (pivot.CompareTo(records[i], columns) <= 0)  // if the item comes before the pivot, swap
                {
                    num++;
                    Swap(records, num, i);
                }
            }

            Swap(records, num+1, right);
            return num + 1;
        }

        private static void Swap(List<PlayerRecords> list, int num, int i)
        {
            PlayerRecords temp = list[i];
            list[i] = list[num];
            list[num] = temp;
        }
    }
}
