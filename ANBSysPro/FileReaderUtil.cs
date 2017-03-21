using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ANBSysPro
{
    public class FileReaderUtil
    {
        public static bool CalculateWordOccurences(string filePath, long topNNumberofWords, string[] wordsToSkip)
        {
            bool retVal = false;
            try
            {
                TreeNode root = new TreeNode(null, '?');

                List<TreeNode> topNNodes = new List<TreeNode>();
                for (int i = 0; i < topNNumberofWords; i++)
                {
                    topNNodes.Add(root);
                }

                using (FileStream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sreader = new StreamReader(fstream))
                    {
                        string line;
                        while ((line = sreader.ReadLine()) != null)
                        {
                            string[] words = line.Split(null);
                            foreach (string word in words)
                            {
                                if (!wordsToSkip.Contains(word, StringComparer.OrdinalIgnoreCase))
                                {
                                    root.AddWord(word.Trim());
                                }
                            }
                        }
                    }
                }

                root.GetTopCount(ref topNNodes);
                topNNodes.Reverse();
                foreach (TreeNode node in topNNodes)
                {
                    Console.WriteLine("{0} - {1} times", node.ToString(), node._wordsCount);
                }
                retVal = true;
                Console.WriteLine("Counting Completed");
            }
            catch (FileNotFoundException ex)
            {
                retVal = false;
                throw ex;
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }

            return retVal;
        }
    }

    public class TreeNode : IComparable<TreeNode>
    {
        private char _char;
        public int _wordsCount;
        private TreeNode _parent = null;
        private ConcurrentDictionary<char, TreeNode> _childs = null;

        public TreeNode(TreeNode parent, char c)
        {
            _char = c;
            _wordsCount = 0;
            _parent = parent;
            _childs = new ConcurrentDictionary<char, TreeNode>();
        }

        public void AddWord(string word, int index = 0)
        {
            if (index < word.Length)
            {
                char key = word[index];
                if (char.IsLetter(key))
                {
                    if (!_childs.ContainsKey(key))
                    {
                        _childs.TryAdd(key, new TreeNode(this, key));
                    }
                    _childs[key].AddWord(word, index + 1);
                }
                else
                {
                    AddWord(word, index + 1);
                }
            }
            else
            {
                if (_parent != null)
                {
                    lock (this)
                    {
                        _wordsCount++;
                    }
                }
            }
        }

        public int GetCount(string word, int index = 0)
        {
            if (index < word.Length)
            {
                char key = word[index];
                if (!_childs.ContainsKey(key))
                {
                    return -1;
                }
                return _childs[key].GetCount(word, index + 1);
            }
            else
            {
                return _wordsCount;
            }
        }

        public void GetTopCount(ref List<TreeNode> mostCounted)
        {
            if (_wordsCount > mostCounted[0]._wordsCount)
            {
                mostCounted[0] = this;
                mostCounted.Sort();
            }
            foreach (char key in _childs.Keys)
            {
                _childs[key].GetTopCount(ref mostCounted);
            }
        }

        public override string ToString()
        {
            if (_parent == null) return "";
            else return _parent.ToString() + _char;
        }

        public int CompareTo(TreeNode other)
        {
            return this._wordsCount.CompareTo(other._wordsCount);
        }
    }
}
