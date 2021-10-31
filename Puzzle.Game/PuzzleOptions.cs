using CommandLine;
using Puzzle.Heuristics;
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
        [Option('b', "bfs", HelpText = "Breadth-first search", Group = "solver")]
        public string BFS { get; set; }

        [Option('d', "dfs", HelpText = "Depth-first search", Group = "solver")]
        public string DFS { get; set; }

        [Option('h', "bf", HelpText = "Best-first strategy heuristic: z - zero, m - manhattan, h - hamming, e - euclidean", Group = "solver")]
        public string BEFS { get; set; }

        [Value(0, MetaName = "board", HelpText = "Board to solved by rows.", Required = true)]
        public string Board { get; set; }
    }

    static class PuzzleOptionsExtentions
    {
        static Dictionary<string, IHeuristic> HeuristicKeys = new()
        {
            { "z", new ZeroHeuristic() },
            { "m", new Manhattan() },
            { "e", new Euclidean() },
            { "h", new Hamming() },
        };

        public static bool UseBFS(this PuzzleOptions option) => !string.IsNullOrEmpty(option.BFS);
        public static bool UseDFS(this PuzzleOptions option) => !string.IsNullOrEmpty(option.DFS);
        public static bool UseBEFS(this PuzzleOptions option) => !string.IsNullOrEmpty(option.BEFS);

        public static IHeuristic GetHeuristic(this PuzzleOptions option)
        {
            string key = "";

            if (option.UseBEFS())
            {
                key = option.BEFS.ToLower();
            }

            if (!HeuristicKeys.ContainsKey(key))
            {
                throw new ArgumentException($"Heuristic key: {key} is not supported.");
            }
            return HeuristicKeys[key];
        }
    }
}
