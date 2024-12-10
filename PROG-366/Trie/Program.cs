namespace Trie {
    class Program
    {
        static void Main()
        {
            Console.Clear();
            
            Trie trie = new();

            trie.Insert("fish");
            trie.Insert("fishing");
            trie.Insert("fisherman");
            trie.Insert("boat");
            trie.Insert("sailboat");

            Console.WriteLine($"Words: {string.Join(", ", trie.GetAllWords())}");
        }
    }
    
    public class Trie
    {
        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children = [];
            public bool IsEndOfWord = false;
        }

        private readonly TrieNode root = new();

        public void Insert(string word)
        {
            TrieNode currentNode = root;

            foreach (char c in word)
            {
                if (!currentNode.Children.ContainsKey(c))
                {
                    currentNode.Children[c] = new TrieNode();
                }

                currentNode = currentNode.Children[c];
            }

            currentNode.IsEndOfWord = true;
        }

        public List<string> GetAllWords()
        {
            List<string> words = [];

            static void GetWords(TrieNode node, string prefix, List<string> words)
            {
                if (node.IsEndOfWord)
                {
                    words.Add(prefix);
                }
                foreach (var child in node.Children)
                {
                    GetWords(child.Value, prefix + child.Key, words);
                }
            };

            GetWords(root, "", words);
            
            return words;
        }
    }
}