namespace Bag
{
    class Program
    {
        static void Main()
        {

            Bag<int> myBag = new();
            myBag.Add(5);
            myBag.Add(4);
            myBag.Add(3);
            myBag.Add(2);
            myBag.Add(1);

            Console.WriteLine(myBag.GetMarble());
            Console.WriteLine(myBag.GetMarble());
            Console.WriteLine(myBag.GetMarble());
            Console.WriteLine(myBag.GetMarble());
            Console.WriteLine(myBag.GetMarble());
        }
    }

    class Bag<T>
    {
        public void Add (T value)
        {
            linkedList.Add(value);
        }

        public T? GetMarble ()
        {
            if (linkedList.startingNode == null) return default(T);

            if (iterator.initialized == false) iterator.SetNodes(linkedList.startingNode);

            return iterator.Next().value;
        }

        private readonly LinkedList<T> linkedList = new();

        private readonly Iterator<T> iterator = new();
    }

    class Iterator<T>()
    {
        private Node<T>[]? nodes;

        private int currentIndex = 0;

        public bool initialized = false;

        public void SetNodes(Node<T> node)
        {
            List<Node<T>> unrandomizedNodes = new List<Node<T>> { node };

            while (node.next != null)
            {
                node = node.next;
                unrandomizedNodes.Add(node);
            }

            nodes = new Node<T>[unrandomizedNodes.Count];
            Random rand = new();

            for (int i = 0; i < nodes.Length; i++)
            {
                int randomIndex = rand.Next(unrandomizedNodes.Count);
                nodes[i] = unrandomizedNodes[randomIndex];
                unrandomizedNodes.RemoveAt(randomIndex);
            }

            initialized = true;
        }

        public Node<T> Next ()
        {
            if (nodes == null) throw new IndexOutOfRangeException();

            Node<T> returnNode = nodes[currentIndex];

            currentIndex += 1;

            return returnNode;
        }
    }

    class LinkedList<T>
    {
        public Node<T>? startingNode = null;

        public int Size
        {
            get {
                if (startingNode == null) return 0;

                Node<T> node = startingNode;
                int count = 1;
                while (node.next != null) {
                    node = node.next;
                    count += 1;
                }
                
                return count;
            }
        }

        public void Add (T value)
        {
            Node<T> newNode = new(value);
            if (startingNode == null) {
                startingNode = newNode;
            }
            else {
                Node<T> node = startingNode;
            
                while (node.next != null) {
                    node = node.next;
                }

                node.next = newNode;
            }
        }

        public void Remove (int index)
        {
            Node<T> previousNode = GetNode(index - 1);

            if (previousNode.next == null) {
                throw new IndexOutOfRangeException();
            }

            previousNode.next = previousNode.next.next;
        }

        public Node<T> GetNode (int index)
        {
            if (startingNode == null) throw new IndexOutOfRangeException();

            Node<T> node = startingNode;

            for (int i = 0; i < index; i++)
            {
                if (node.next == null) throw new IndexOutOfRangeException();
                node = node.next;
            }

            return node;
        }
    }

    class Node<T>(T inputValue)
    {
        public T value = inputValue;
        public Node<T>? next = null;
    }
}