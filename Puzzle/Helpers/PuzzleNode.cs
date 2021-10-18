using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Helpers
{
    public class PuzzleNode : FastPriorityQueueNode
    {
        public IPuzzle Puzzle { get; set; }

        public PuzzleNode(IPuzzle puzzle)
        {
            Puzzle = puzzle;
        }
    }
}
