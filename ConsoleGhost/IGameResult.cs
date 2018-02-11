using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGhost
{
    public interface IGameResult
    {
        /// <summary>
        /// Index of winning player (starting on 0) or -1 if the winner is unknown
        /// </summary>
        int Winner { get; set; }        

        string Explanation { get; set; }


    }
}
