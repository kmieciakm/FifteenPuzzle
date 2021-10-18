using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Heuristics
{
    public class Euclidean : IHeuristic
    {
        public float Calculate(IPuzzle puzzle, IPuzzle puzzleSolved)
        {
            float cost = 0f;
            var size = puzzle.BoardSize;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (puzzle.Board[x][y] != Puzzle.EmptyField && puzzle.Board[x][y] != puzzleSolved.Board[x][y])
                    {
                        var desiredPosition = puzzleSolved.GetField(puzzle.Board[x][y]);
                        cost += MathF.Sqrt(MathF.Pow(desiredPosition.x - x, 2) + MathF.Pow(desiredPosition.y - y, 2));
                    }
                }
            }
            return cost;
        }
    }
}
