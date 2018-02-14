using System;
using System.Collections.Generic;

namespace Game.Library.Impl
{
    internal abstract class GhostBasePlayer : IPlayer
    {
        public GhostBasePlayer(string name)
        {
            _name = name;            
            _rnd = new Random();           
        }

        public string Name { get { return _name; } }

        public PlayerType Type { get { return _type; } }

        public abstract IState NextMove(IGame game);
        
        public virtual IStateAnalysis Analyse(IGame game)
        {
            return game.Analysis;
        }

        #region Protected
        protected string _name;
        protected PlayerType _type;
        protected Random _rnd;          
        
        protected string PickRandom(List<string> wordList)
        {
            var r = _rnd.Next(wordList.Count);
            return wordList[r];
        }               
        #endregion
    }
}
