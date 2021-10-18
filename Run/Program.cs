using Puzzles = Puzzle;
using Puzzle.Solver;
using System;
using System.Threading;
using Puzzle;
using System.Diagnostics;
using Puzzle.Heuristics;

namespace Run
{
    class Program
    {
        private const int MB = 1048576;

        static void Main(string[] args)
        {
            Thread ExtendedStackThread = new (
                new ParameterizedThreadStart(SolvePuzzle),
                10 * MB);

            ExtendedStackThread.Start();
        }

        private static void SolvePuzzle(object data)
        {
            IPuzzle puzzle = new Puzzles.Puzzle(3)
            {
                Board = new int[][]
                {
                    new int[3] { 2, 3, 5 },
                    new int[3] { 6, 4, 7 },
                    new int[3] { 1, 8, 0 }
                }
            };

            var timer = new Stopwatch();
            timer.Start();

            BEFS solver = new();
            var result = solver.Solve(ref puzzle, new Manhattan());

            timer.Stop();

            Console.WriteLine();
            if (result is true)
            {
                Console.WriteLine("Solved !!!");
            }
            else
            {
                Console.WriteLine("Unsolved !!!");
            }

            Console.WriteLine($"{timer.Elapsed}");

            ShowPuzzle(puzzle);
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
