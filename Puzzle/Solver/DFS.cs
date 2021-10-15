using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Solver
{
    public class DFS : IPuzzleSolver
    {
        private Stack<IPuzzle> Visited { get; set; } = new Stack<IPuzzle>();
        private PuzzleComparer Comparer { get; set; } = new PuzzleComparer();

        /// <summary>
        /// The algorithm makes the first move and explores the resulting puzzle as far as possible before backtracking.
        /// </summary>
        /// <param name="puzzle">Puzzle to solve.</param>
        /// <returns>True if found solved puzzle otherwise, false.</returns>
        public bool Solve(ref IPuzzle puzzle)
        {
            Visited.Push(puzzle);
            Console.WriteLine($"Visited {Visited.Count} nodes");

            if (puzzle.IsSolved())
                return true;

            var possibleMoves = puzzle.GetPossibleMoves();
            IPuzzle nextPuzzle;
            foreach (var move in possibleMoves)
            {
                nextPuzzle = puzzle.GetCopy();
                nextPuzzle.TryMakeMove(move);
                if (!Visited.Contains(nextPuzzle, Comparer))
                {
                    if (Solve(ref nextPuzzle))
                    {
                        puzzle = nextPuzzle;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
