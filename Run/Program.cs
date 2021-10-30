using System;
using System.Threading;
using CommandLine;
using System.Collections.Generic;
using System.Linq;

namespace Puzzle.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<PuzzleOptions>(args);
            var output = result.MapResult(Solve, _ => "\0");

            Console.WriteLine(output);
            Console.ReadKey();

            static string Solve(PuzzleOptions options)
            {
                var puzzle = ParsePuzzle(options.Board);
                ShowPuzzle(puzzle);
                return "-1";
            }
        }

        private static IPuzzle ParsePuzzle(string board)
        {
            var boardCharacters = board
                .ToCharArray()
                .Select(s => s.ToString())
                .Select(character => int.Parse(character));

            var boardSize = (int) Math.Sqrt(boardCharacters.Count());

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
