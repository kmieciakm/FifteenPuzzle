using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Game
{
    interface IPuzzleOptions
    {
        [Option('b', "bfs", HelpText = "Breadth-first search")]
        string BFS { get; set; }
        [Option('d', "dfs", HelpText = "Depth-first search")]
        string DFS { get; set; }
        [Option('h', "bf", HelpText = "Best-first strategy")]
        string BEFS { get; set; }

        [Option('q', "quiet", HelpText = "Suppresses steps display.")]
        bool Quiet { get; set; }

        [Value(0, MetaName = "board", HelpText = "Board to solved by rows.", Required = true)]
        string Board { get; set; }
    }

    [Verb("solve", true, HelpText = "Solves a given puzzle.")]
    class PuzzleOptions : IPuzzleOptions
    {
        public string BFS { get; set; }
        public string DFS { get; set; }
        public string BEFS { get; set; }
        public bool Quiet { get; set; }
        public string Board { get; set; }
    }
}
