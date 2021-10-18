using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Heuristics
{
    public class Manhattan : IHeuristic
    {
        public float Calculate(IPuzzle puzzle, IPuzzle puzzleSolved)
        {
            double cost = 0f;
            var size = puzzle.BoardSize;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (puzzle.Board[x][y] != Puzzle.EmptyField && puzzle.Board[x][y] != puzzleSolved.Board[x][y])
                    {
                        var desiredPosition = puzzleSolved.GetField(puzzle.Board[x][y]);
                        cost += Math.Abs(desiredPosition.x - x) + Math.Abs(desiredPosition.y - y);
                    }
                }
            }
            return (float)cost;
        }
    }
}
