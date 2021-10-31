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

        public bool Solve(ref IPuzzle puzzle, IHeuristic heuristic, out string steps)
        {
            var visited = new HashSet<IPuzzle>();
            var desiredPuzzle = Puzzle.GetSolvedPuzzle(puzzle.BoardSize);
            var maxQueueSize = Helpers.Math.Factorial(puzzle.BoardSize * puzzle.BoardSize);
            var toVisitPriority = new FastPriorityQueue<PuzzleNode>(maxQueueSize);
            var stepsBuilder = new StringBuilder();

            var cost = heuristic.Calculate(puzzle, desiredPuzzle);
            toVisitPriority.Enqueue(new PuzzleNode(puzzle), cost);

            while (toVisitPriority.Count > 0)
            {
                puzzle = toVisitPriority.Dequeue().Puzzle;
                visited.Add(puzzle);

                if (puzzle.IsSolved())
                {
                    steps = stepsBuilder.ToString();
                    return true;
                }

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
                        stepsBuilder.Append(move.ToString());
                    }
                }
            }

            steps = string.Empty;
            return false;
        }
    }
}
