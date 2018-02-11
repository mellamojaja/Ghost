using ConsoleGhost.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGhost.Impl
{
    public sealed class GhostAnalysisTree
    {
        public static GhostAnalysisTree Instance
        {
            get
            {
                if (_tree == null)
                {
                    lock (syncRoot)
                    {
                        if (_tree == null)
                        {
                            _instance = new GhostAnalysisTree();                            
                        }
                    }
                }
                return _instance;
            }            
        }

        public TreeNode<GhostGameStateAnalysis> Tree
        {
            get
            {
                return _tree;
            }
        }

        public TreeNode<GhostGameStateAnalysis> FindWordNodeOrLongestExistingRoot(string word)
        {
            return FindWordNodeOrLongestExistingRoot(word, _tree);
        }

        public WordType FindWordType(string word)
        {
            var treeNode = FindWordNodeOrLongestExistingRoot(word, _tree);
            var analysis = treeNode.Value;

            if (analysis.State.Word.Length < word.Length)
            {
                if (_words.Contains(word))
                {
                    return WordType.derived;                    
                }
                else
                {
                    return WordType.invalid;
                }
            } 

            if (treeNode.Children.Count > 0)
            {
                return WordType.incompleted;
            }
            else
            {
                return WordType.completed;
            }
        }

        #region Private
        private static volatile GhostAnalysisTree _instance;        
        private static object syncRoot = new Object();
        private static TreeNode<GhostGameStateAnalysis> _tree;
        private static string[] _words;

        private GhostAnalysisTree()
        {
            _words = Resources.gosthGameDict.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            _words = RemoveDerivedWords(_words);

            var initialStateAnalysis = new GhostGameStateAnalysis(new GhostGameState() { CurrentPlayer = 0, Word = "" });
            _tree = new TreeNode<GhostGameStateAnalysis>(initialStateAnalysis);

            foreach (var word in _words)
            {
                AddWordToTree(word, _tree);
            }

            AnalyseTree(_tree);
        }       

        private string[] RemoveDerivedWords(string[] words)
        {
            var result = new List<string>();
            var lastWord = "";

            Array.Sort(words, StringComparer.InvariantCulture);

            foreach (var word in words)
            {
                if (word.Length < 4 || (lastWord != "" && word.Contains(lastWord)))
                {
                    continue;
                }
                else
                {
                    result.Add(word);
                    lastWord = word;
                }
            }

            return result.ToArray();
        }

        private TreeNode<GhostGameStateAnalysis> AddWordToTree(string word, TreeNode<GhostGameStateAnalysis> treeNode)
        {
            var node = FindWordNodeOrLongestExistingRoot(word, treeNode);
            if (node.Depth == word.Length)
            {
                return node;
            }
            else
            {
                var child = InsertAsChild(word, node);
                return AddWordToTree(word, child);
            }
        }

        private TreeNode<GhostGameStateAnalysis> FindWordNodeOrLongestExistingRoot(string word, TreeNode<GhostGameStateAnalysis> tree)
        {
            if (tree.Depth == word.Length)
            {
                return tree;
            }

            TreeNode<GhostGameStateAnalysis> child;
            if (tree.Children.Count == 0)
            {
                return tree;
            }
            else
            {
                child = tree.Children.FirstOrDefault(childNode => childNode.Value.State.Word == word.Substring(0, childNode.Depth));
                if (child == null)
                {
                    return tree;
                }
            }
            return FindWordNodeOrLongestExistingRoot(word, child);
        }

        private TreeNode<GhostGameStateAnalysis> InsertAsChild(string word, TreeNode<GhostGameStateAnalysis> tree)
        {
            var newWord = word.Substring(0, tree.Depth + 1);
            var nextPlayer = tree.Value.State.CurrentPlayer == 0 ? 1 : 0;
            return tree.AddChild(new GhostGameStateAnalysis()
            {
                State = new GhostGameState() { CurrentPlayer = nextPlayer, Word = newWord },
                Winner = -1,
                ExpectedWinner = -1,
                LongestPossibleWord = "",
                ShortestPossibleWord = ""                
            });
        }

        private void AnalyseNode(TreeNode<GhostGameStateAnalysis> treeNode)
        {
            if (treeNode.Children.Count == 0)
            {
                // The previous player completed the word, so you win
                treeNode.Value.Winner = treeNode.Value.State.CurrentPlayer;
                treeNode.Value.ExpectedWinner = treeNode.Value.State.CurrentPlayer;
                treeNode.Value.LongestPossibleWord = treeNode.Value.State.Word;
                treeNode.Value.ShortestPossibleWord = treeNode.Value.State.Word;
            }
            else
            {
                treeNode.Value.ExpectedWinner = FindExpectedWinner(treeNode);                
                treeNode.Value.LongestPossibleWord = FindLongestPossibleWord(treeNode);
                treeNode.Value.ShortestPossibleWord = FindShortestPossibleWord(treeNode);
            }
        }

        private int FindExpectedWinner(TreeNode<GhostGameStateAnalysis> treeNode)
        {
            if (treeNode.Children.All(childNode => childNode.Value.ExpectedWinner == 0))
            {
                return 0;
            }
            else if (treeNode.Children.All(childNode => childNode.Value.ExpectedWinner == 1))
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        private string FindLongestPossibleWord(TreeNode<GhostGameStateAnalysis> treeNode)
        {
            var result = "";            
            foreach (var child in treeNode.Children)
            {
                if (result == "" || result.Length < child.Value.LongestPossibleWord.Length)
                {
                    result = child.Value.LongestPossibleWord;
                }                
            }
            return result;
        }

        private string FindShortestPossibleWord(TreeNode<GhostGameStateAnalysis> treeNode)
        {
            var result = "";            
            foreach (var child in treeNode.Children)
            {                
                if (result == "" || result.Length > child.Value.ShortestPossibleWord.Length)
                {
                    result = child.Value.ShortestPossibleWord;
                }
            }
            return result;
        }

        private void AnalyseTree(TreeNode<GhostGameStateAnalysis> treeNode)
        {
            treeNode.PostTraverse(AnalyseNode);
        }        
        #endregion
    }

    public enum WordType
    {
        invalid,
        completed, 
        incompleted,
        derived
    }
}
