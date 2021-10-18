using Priority_Queue;
using Puzzle.Helpers;
using Puzzle.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Solver
{
    public class BEFS : IInformedPuzzleSolver
    {
        private PuzzleComparer Comparer { get; set; } = new PuzzleComparer();

        public bool Solve(ref IPuzzle puzzle, IHeuristic heuristic)
        {
            var visited = new HashSet<IPuzzle>();
            var desiredPuzzle = Puzzle.GetSolvedPuzzle(puzzle.BoardSize);
            int maxQueueSize = Helpers.Math.Factorial(puzzle.BoardSize * puzzle.BoardSize);
            var toVisitPriority = new FastPriorityQueue<PuzzleNode>(maxQueueSize);

            var cost = heuristic.Calculate(puzzle, desiredPuzzle);
            toVisitPriority.Enqueue(new PuzzleNode(puzzle), cost);

            while (toVisitPriority.Count > 0)
            {
                puzzle = toVisitPriority.Dequeue().Puzzle;
                visited.Add(puzzle);
                //Console.WriteLine($"Visited {visited.Count} nodes");

                var possibleMoves = puzzle.GetPossibleMoves();
                IPuzzle nextPuzzle;
                foreach (var move in possibleMoves)
                {
                    nextPuzzle = puzzle.GetCopy();
                    nextPuzzle.TryMakeMove(move);
                    if (!visited.Contains(nextPuzzle, Comparer))
                    {
                        cost = heuristic.Calculate(nextPuzzle, desiredPuzzle);
                        toVisitPriority.Enqueue(new PuzzleNode(nextPuzzle), cost);
                    }
                }

                if (puzzle.IsSolved())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
