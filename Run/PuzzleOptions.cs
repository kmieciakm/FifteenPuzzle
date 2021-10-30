using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Game
{
    [Verb("solve", true, HelpText = "Solves a given puzzle.")]
    class PuzzleOptions
    {
        [Option('b', "bfs", HelpText = "Breadth-first search")]
        public string BFS { get; set; }

        [Option('d', "dfs", HelpText = "Depth-first search")]
        public string DFS { get; set; }

        [Option('h', "bf", HelpText = "Best-first strategy")]
        public string BEFS { get; set; }

        [Value(0, MetaName = "board", HelpText = "Board to solved by rows.", Required = true)]
        public string Board { get; set; }
    }
}
