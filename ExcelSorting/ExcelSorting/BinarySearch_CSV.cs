

namespace ExcelSorting
{
    internal static class BinarySearch_CSV
    {
        public static int BinarySearch(List<PlayerRecords> recordList, string value, string header)
        {
            // first, create a sorted list based on the header
            string[] array = { header };
            Quicksort_CSV.Quicksort(recordList, 0, recordList.Count - 1, array);

            int left = 0;
            int right = recordList.Count - 1;

            while (left <= right)
            {
                int middle = (left + right) / 2;

                if (recordList[middle].GetValueByHeader(header) == value)
                {
                    // if a match is found
                    return middle;
                }
                else if (string.Compare(recordList[middle].GetValueByHeader(header), value) < 0)
                {
                    // value is > middle, search right half
                    left = middle + 1;
                }
                else
                {
                    // value is less than middle, search left half
                    right = middle - 1;
                }
            }

            // no mathcing record found
            return -1;
        }
    }
}
