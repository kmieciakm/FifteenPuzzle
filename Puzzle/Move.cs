using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle
{
    public struct Move
    {
        public Move(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }

        public override string ToString()
        {
            return Direction switch
            {
                Direction.UP => "U",
                Direction.DOWN => "D",
                Direction.LEFT => "L",
                Direction.RIGHT => "R",
                _ => string.Empty
            };
        }
    }
}
