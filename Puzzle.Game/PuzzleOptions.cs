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
        [Option('b', "bfs", HelpText = "Breadth-first search order permutation: l - left, r - right, u - up, d - down", Group = "solver")]
        public string BFS { get; set; }

        [Option('d', "dfs", HelpText = "Depth-first search order permutation: l - left, r - right, u - up, d - down", Group = "solver")]
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
            { "h", new Hamming() }
        };

        static Dictionary<string, Direction> OrderKeys = new()
        {
            { "l", Direction.LEFT },
            { "r", Direction.RIGHT },
            { "u", Direction.UP },
            { "d", Direction.DOWN }
        };

        public static bool UseBFS(this PuzzleOptions option) => !string.IsNullOrEmpty(option.BFS);
        public static bool UseDFS(this PuzzleOptions option) => !string.IsNullOrEmpty(option.DFS);
        public static bool UseBEFS(this PuzzleOptions option) => !string.IsNullOrEmpty(option.BEFS);

        public static IPuzzle ParsePuzzle(this PuzzleOptions option)
        {
            var boardCharacters = option.Board
                .ToCharArray()
                .Select(s => s.ToString())
                .Select(character => int.Parse(character));

            var boardSize = (int)Math.Sqrt(boardCharacters.Count());

            Puzzle puzzle = new(boardSize);
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    puzzle.Board[x][y] = boardCharacters.ElementAt((boardSize * x) + y);
                }
            }
            return puzzle;
        }

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

        public static List<Direction> GetMovesOrder(this PuzzleOptions option)
        {
            if (option.UseBFS()) return ParseOrder(option.BFS);
            else if (option.UseDFS()) return ParseOrder(option.DFS);
            else throw new ArgumentException("No option with order specified.");
        }

        private static List<Direction> ParseOrder(string order)
        {
            var keys = order
                .ToCharArray()
                .Select(s => s.ToString().ToLower())
                .Where(s => OrderKeys.ContainsKey(s));

            if (keys.Count() != 4 || keys.Distinct().Count() != 4)
            {
                throw new ArgumentException($"Invalid order permutation.");
            }

            var movesOrder = new List<Direction>();
            foreach (var key in keys)
            {
                movesOrder.Add(OrderKeys[key]);
            }
            return movesOrder;
        }
    }
}
