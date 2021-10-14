using DeepCopy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Solver
{
    public class BFS : IPuzzleSolver
    {
        public void Solve(ref IPuzzle puzzle)
        {
            HashSet<IPuzzle> visited = new();
            Queue<IPuzzle> toVisit = new();
            PuzzleComparer comparer = new PuzzleComparer();

            toVisit.Enqueue(puzzle);
            while (toVisit.Count > 0)
            {
                puzzle = toVisit.Dequeue();
                visited.Add(puzzle);

                var possibleMoves = puzzle.GetPossibleMoves();
                IPuzzle nextPuzzle;
                foreach (var move in possibleMoves)
                {
                    nextPuzzle = DeepCopier.Copy(puzzle);
                    nextPuzzle.TryMakeMove(move);
                    if (!visited.Contains(nextPuzzle, comparer))
                    {
                        toVisit.Enqueue(nextPuzzle);
                    }
                }

                if (puzzle.IsSolved())
                {
                    return;
                }
            }
        }
    }
}
