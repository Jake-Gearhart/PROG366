class Program
{
    static void Main()
    {
        Random random = new();
        List<int> numbers = [];

        for (int i = 0; i < 10; i++)
        {
            numbers.Add(random.Next(10, 100));
        }

        Console.WriteLine("Original List:");
        Console.WriteLine(string.Join(", ", numbers));

        Console.WriteLine("\nSelection Sort:");
        Console.WriteLine(string.Join(", ", numbers));
        SelectionSort([.. numbers]);

        Console.WriteLine("\nInsertion Sort:");
        Console.WriteLine(string.Join(", ", numbers));
        InsertionSort([.. numbers]);

        Console.WriteLine("\nBubble Sort:");
        Console.WriteLine(string.Join(", ", numbers));
        BubbleSort([.. numbers]);
    }

    static void SelectionSort(List<int> list)
    {
        int n = list.Count;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < n; j++)
            {
                if (list[j] < list[minIndex])
                {
                    minIndex = j;
                }
            }

            (list[i], list[minIndex]) = (list[minIndex], list[i]);
            Console.WriteLine(string.Join(", ", list));
        }
    }

    static void InsertionSort(List<int> list)
    {
        int n = list.Count;

        for (int i = 1; i < n; i++)
        {
            int key = list[i];
            int j = i - 1;

            while (j >= 0 && list[j] > key)
            {
                list[j + 1] = list[j];
                j--;
            }

            list[j + 1] = key;

            Console.WriteLine(string.Join(", ", list));
        }
    }

    static void BubbleSort(List<int> list)
    {
        int n = list.Count;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (list[j] > list[j + 1])
                {
                    (list[j + 1], list[j]) = (list[j], list[j + 1]);
                }
            }

            Console.WriteLine(string.Join(", ", list));
        }
    }
}