using System;
using System.Threading;
using CommandLine;
using System.Collections.Generic;
using System.Linq;
using Puzzle.Solver;
using Puzzle.Heuristics;
using System.Text;

namespace Puzzle.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var result = Parser.Default.ParseArguments<PuzzleOptions>(args);
                var output = result.MapResult(Solve, _ => "\0");
                Console.WriteLine(output);
            }
            catch (Exception error)
            {
                HandleError(error);
            }

            static string Solve(PuzzleOptions option)
            {
                IPuzzle puzzle = ParsePuzzle(option.Board);
                bool isSolved = false;
                string steps = "";

                if (option.UseBFS())
                {
                    var solver = new BFS();
                    isSolved = solver.Solve(ref puzzle, out steps);
                }
                else if (option.UseDFS())
                {
                    var solver = new DFS();
                    isSolved = solver.Solve(ref puzzle, out steps);
                }
                else if (option.UseBEFS())
                {
                    var solver = new BEFS();
                    var heuristic = option.GetHeuristic();
                    isSolved = solver.Solve(ref puzzle, heuristic, out steps);
                }

                return BuildOutput(isSolved, steps);
            }
        }

        private static void HandleError(Exception error)
        {
            Console.WriteLine("ERROR");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error.Message);
            Console.ResetColor();
            Console.WriteLine("Use --help for more details.");
        }

        private static IPuzzle ParsePuzzle(string board)
        {
            var boardCharacters = board
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

        private static string BuildOutput(bool isSolved, string steps)
        {
            var isSolvedOutput = isSolved ? steps.Length.ToString() : "-1";
            var outputBuilder = new StringBuilder();
            outputBuilder.AppendLine(isSolvedOutput);
            outputBuilder.AppendLine(steps);
            return outputBuilder.ToString();
        }

        private static void ShowPuzzle(IPuzzle puzzle)
        {
            for (int rowIndex = 0; rowIndex < puzzle.BoardSize; rowIndex++)
            {
                Console.WriteLine();
                for (int columnIndex = 0; columnIndex < puzzle.BoardSize; columnIndex++)
                {
                    Console.Write(puzzle.Board[rowIndex][columnIndex]);
                }
            }
            Console.WriteLine();
        }
    }
}
