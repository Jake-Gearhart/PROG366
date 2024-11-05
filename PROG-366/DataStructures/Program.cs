namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = [];
            Dictionary<string, string> dictionary = new();
            Stack<string> stack = new();
            Queue<string> queue = new();

            using StreamReader reader = new("sample-data.csv");
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] entries = line.Split(',');

                list.Add(entries[0]);
                list.Add(entries[1]);

                dictionary.Put(entries[0], entries[1]);

                stack.Push(entries[1]);

                queue.Enqueue(entries[1]);
            }
        }
    }

    class Dictionary<K, V> ()
    {
        private readonly List<K> keys = [];
        private readonly List<V> values = [];

        public void Put(K key, V value)
        {
            int index = keys.IndexOf(key);
            if (index == -1)
            {
                keys.Add(key);
                values.Add(value);
            }
            else
            {
                values[index] = value;
            }
        }

        public V Get (K key)
        {
            return values[keys.IndexOf(key)];
        }
    }

    class Stack<T> ()
    {
        private readonly List<T> elements = [];

        public void Push (T element)
        {
            elements.Add(element);
        }

        public T Pop ()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty.");

            T returnElement = elements[^1];
            elements.RemoveAt(elements.Count - 1);
            return returnElement;
        }

        public int Size ()
        {
            return elements.Count;
        }

        public bool IsEmpty ()
        {
            if (elements.Count == 0) return true;
            
            return false;
        }
    }

    class Queue<T> ()
    {
        private readonly List<T> elements = [];

        public void Enqueue (T element)
        {
            elements.Add(element);
        }

        public T Dequeue ()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue is empty.");

            T returnElement = elements[0];
            elements.RemoveAt(0);
            return returnElement;
        }

        public int Size ()
        {
            return elements.Count;
        }

        public bool IsEmpty ()
        {
            return elements.Count == 0;
        }
    }
}