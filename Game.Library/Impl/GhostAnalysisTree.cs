using Game.Library.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Library.Impl
{
    internal sealed class GhostAnalysisTree
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

            if (GetWord(analysis.State).Length < word.Length)
            {
                if (_validWords.Contains(word))
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
        private static string[] _validWords;
       
        private GhostAnalysisTree()
        {           
            _validWords = Resources.gosthGameDict.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            _validWords = RemoveDerivedWords(_validWords);

            var initialStateAnalysis = new GhostGameStateAnalysis(new GhostGameState(""));
            _tree = new TreeNode<GhostGameStateAnalysis>(initialStateAnalysis);

            foreach (var word in _validWords)
            {
                AddWordToTree(word, _tree);
            }

            AnalyseTree(_tree);
        }       

        private string[] RemoveDerivedWords(string[] words)
        {
            var result = new List<string>();
            var lastWord = "";

            Array.Sort(words, StringComparer.InvariantCultureIgnoreCase);

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
                child = tree.Children.FirstOrDefault(childNode => GetWord(childNode.Value.State) == word.Substring(0, childNode.Depth));
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
            return tree.AddChild(new GhostGameStateAnalysis(new GhostGameState(newWord)));
        }

        private void AnalyseNode(TreeNode<GhostGameStateAnalysis> treeNode)
        {
            if (treeNode.Children.Count == 0)
            {
                // The previous player completed the word, so you win
                treeNode.Value.Winner = treeNode.Value.State.CurrentPlayer;
                treeNode.Value.ExpectedMaxTurns = 0;
                treeNode.Value.ExpectedWinner = treeNode.Value.State.CurrentPlayer;
                treeNode.Value.LongestPossibleWord = GetWord(treeNode.Value.State);
                treeNode.Value.ShortestPossibleWord = GetWord(treeNode.Value.State);
            }
            else
            {
                treeNode.Value.ExpectedWinner = FindExpectedWinner(treeNode);
                treeNode.Value.ExpectedMaxTurns = FindExpectedMaxTurns(treeNode) + 1;
                treeNode.Value.LongestPossibleWord = FindLongestPossibleWord(treeNode);
                treeNode.Value.ShortestPossibleWord = FindShortestPossibleWord(treeNode);
                treeNode.Value.RecommendedWordList = FindRecommendedWordList(treeNode);
            }
        }

        private int FindExpectedWinner(TreeNode<GhostGameStateAnalysis> treeNode)
        {
            var thisPlayer = treeNode.Value.State.CurrentPlayer;
            var opposingPlayer = thisPlayer == 0 ? 1 : 0;

            if (treeNode.Children.Any(childNode => childNode.Value.ExpectedWinner == thisPlayer))
            {
                return thisPlayer;
            }
            else if (treeNode.Children.All(childNode => childNode.Value.ExpectedWinner == opposingPlayer))
            {
                return opposingPlayer;
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
            if (result == "")
                return "";

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

        private List<string> FindRecommendedWordList(TreeNode<GhostGameStateAnalysis>  treeNode)
        {
            var thisPlayer = treeNode.Value.State.CurrentPlayer;

            var winningChildren = treeNode.Children.Where(child => child.Value.ExpectedWinner == thisPlayer).ToList();
            if (winningChildren.Count > 0)
            {
                return winningChildren.Select(child => GetWord(child.Value.State)).ToList();                
            }

            var longestPossibleWord = FindLongestPossibleWord(treeNode);
            var longestWordChildren = treeNode.Children.Where(child => child.Value.LongestPossibleWord.Length == longestPossibleWord.Length).ToList();
            if (longestWordChildren.Count > 0)
            {
                return longestWordChildren.Select(child => GetWord(child.Value.State)).ToList();
            }

            return treeNode.Children.Select(child => GetWord(child.Value.State)).ToList();
        }

        private int FindExpectedMaxTurns(TreeNode<GhostGameStateAnalysis> treeNode)
        {
            return treeNode.Children.Min(child => child.Value.ExpectedMaxTurns );
        }

        private void AnalyseTree(TreeNode<GhostGameStateAnalysis> treeNode)
        {
            treeNode.PostTraverse(AnalyseNode);
        }        

        private string GetWord(IState state)
        {
            return (state as GhostGameState).Word;
        }

        private int GetCurrentPlayer(string word)
        {
            return word.Length % 2 == 0 ? 0 : 1;
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
