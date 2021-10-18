using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Heuristics
{
    public class Hamming : IHeuristic
    {
        public float Calculate(IPuzzle puzzle, IPuzzle puzzleSolved)
        {
            float misplaced = 0f;
            var size = puzzle.BoardSize;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (puzzle.Board[x][y] != Puzzle.EmptyField && puzzle.Board[x][y] != puzzleSolved.Board[x][y])
                    {
                        misplaced++;
                    }
                }
            }
            return misplaced;
        }
    }
}
