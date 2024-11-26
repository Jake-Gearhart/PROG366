class Program
{
    static void Main()
    {
        Random random = new();
        int[] numbers = new int[10];

        for (int i = 0; i < 10; i++)
        {
            int newNum;
            do
            {
                newNum = random.Next(1, 100);
            }
            while (numbers.Contains(newNum));

            numbers[i] = newNum;
        }

        Console.Clear();

        int[] temp = new int[10];

        Console.WriteLine("Original arr:");
        numbers.CopyTo(temp, 0);
        LogNumbers(temp);

        Console.WriteLine("\nBubble Sort:");
        numbers.CopyTo(temp, 0);
        LogNumbers(temp);
        BubbleSort(temp);

        Console.WriteLine("\nInsertion Sort:");
        numbers.CopyTo(temp, 0);
        LogNumbers(temp);
        InsertionSort(temp);

        Console.WriteLine("\nSelection Sort:");
        numbers.CopyTo(temp, 0);
        LogNumbers(temp);
        SelectionSort(temp);

        Console.WriteLine("\nHeap Sort:");
        numbers.CopyTo(temp, 0);
        LogNumbers(temp);
        HeapSort(temp);

        Console.WriteLine("\nQuick Sort:");
        numbers.CopyTo(temp, 0);
        LogNumbers(temp);
        QuickSort(temp);

        Console.WriteLine("\nMerge Sort:");
        numbers.CopyTo(temp, 0);
        LogNumbers(temp);
        MergeSort(temp);
    }

    static void LogNumbers(int[] arr)
    {
        Console.WriteLine(
            string.Join(", ", arr.Select(n => n.ToString("D2")))
        );
    }

    static void BubbleSort(int[] arr)
    {
        int n = arr.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    (arr[j + 1], arr[j]) = (arr[j], arr[j + 1]);
                }
            }

            LogNumbers(arr);
        }
    }

    static void InsertionSort(int[] arr)
    {
        int n = arr.Length;

        for (int i = 1; i < n; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            arr[j + 1] = key;

            LogNumbers(arr);
        }
    }

    static void SelectionSort(int[] arr)
    {
        int n = arr.Length;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }

            (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);

            LogNumbers(arr);
        }
    }

    static void Heapify(int[] arr, int n, int i)
    {
        int largest = i;
        int leftIndex = 2 * i + 1; 
        int rightIndex = 2 * i + 2; 

        if (leftIndex < n && arr[leftIndex] > arr[largest]) largest = leftIndex;

        if (rightIndex < n && arr[rightIndex] > arr[largest]) largest = rightIndex;

        if (largest != i) {
            (arr[largest], arr[i]) = (arr[i], arr[largest]);
            Heapify(arr, n, largest);
        }
    }

    static void HeapSort(int[] arr)
    {
        int arrLength = arr.Length;

        for (int i = arrLength / 2 - 1; i >= 0; i--)
        {
            Heapify(arr, arrLength, i);
        }

        for (int i = arrLength - 1; i > 0; i--)
        {
            (arr[i], arr[0]) = (arr[0], arr[i]);
            LogNumbers(arr);

            Heapify(arr, i, 0);
        }
    }

    static int QuickSortPartition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j <= high - 1; j++)
        {
            if (arr[j] < pivot) {
                i++;
                if (i != j)
                {
                    (arr[j], arr[i]) = (arr[i], arr[j]);
                    LogNumbers(arr);
                }
            }
        }
        
        if (i+1 != high)
        {
            (arr[high], arr[i+1]) = (arr[i+1], arr[high]);
            LogNumbers(arr);
        }

        return i + 1;
    }

    static void QuickSort(int[] arr, int? lowInput = null, int? highInput = null)
    {
        int low = lowInput ?? 0;
        int high = highInput ?? arr.Length - 1;

        if (low < high)
        {
            int partition = QuickSortPartition(arr, low, high);

            QuickSort(arr, low, partition - 1);
            QuickSort(arr, partition + 1, high);
        }
    }

    static void Merge(int[] arr, int left, int middle, int right)
    {
        int leftArrayLength = middle - left + 1;
        int rightArrayLength = right - middle;

        int[] leftArray = new int[leftArrayLength];
        int[] rightArray = new int[rightArrayLength];

        for (int i = 0; i < leftArrayLength; ++i)
        {
            leftArray[i] = arr[left + i];
        }
        for (int j = 0; j < rightArrayLength; ++j)
        {
            rightArray[j] = arr[middle + 1 + j];
        }

        int leftArrayIndex = 0;
        int rightArrayIndex = 0;

        while (leftArrayIndex < leftArrayLength && rightArrayIndex < rightArrayLength) {
            if (leftArray[leftArrayIndex] <= rightArray[rightArrayIndex]) {
                arr[left] = leftArray[leftArrayIndex];
                leftArrayIndex++;
            }
            else {
                arr[left] = rightArray[rightArrayIndex];
                rightArrayIndex++;
            }
            left++;
        }

        while (leftArrayIndex < leftArrayLength) {
            arr[left] = leftArray[leftArrayIndex];
            leftArrayIndex++;
            left++;
        }

        while (rightArrayIndex < rightArrayLength) {
            arr[left] = rightArray[rightArrayIndex];
            rightArrayIndex++;
            left++;
        }
    }

    static void MergeSort(int[] arr, int? leftInput = null, int? rightInput = null)
    {
        int left = leftInput ?? 0;
        int right = rightInput ?? arr.Length - 1;

        if (left < right) {

            int middle = left + (right - left) / 2;

            MergeSort(arr, left, middle);
            MergeSort(arr, middle + 1, right);

            Merge(arr, left, middle, right);
        }

        LogNumbers(arr);
    }
}
