class Program
{
    static void Main()
    {
        int[] data = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];

        LinearSearch(data, 12);
        BinarySearch(data, 12);
        InterpolationSearch(data, 12);
    }

    static int LinearSearch(int[] data, int target)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == target) return data[i];
        }

        return -1;
    }

    static int BinarySearch(int[] data, int target)
    {
        int low = 0, high = data.Length - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;

            if (data[mid] == target) {
                return data[mid];
            }
            else if (data[mid] < target) {
                low = mid + 1;
            }
            else {
                high = mid - 1;
            }
        }

        return -1;
    }

    static int InterpolationSearch(int[] data, int target)
    {
        int low = 0, high = data.Length - 1;

        while (low <= high && target >= data[low] && target <= data[high])
        {
            if (low == high)
            {
                if (data[low] == target) return data[low];
                return -1;
            }

            int pos = low + ((target - data[low]) * (high - low) / (data[high] - data[low]));

            if (data[pos] == target) {
                return data[pos];
            }
            else if (data[pos] < target) {
                low = pos + 1;
            }
            else {
                high = pos - 1;
            }
        }

        return -1;
    }
}