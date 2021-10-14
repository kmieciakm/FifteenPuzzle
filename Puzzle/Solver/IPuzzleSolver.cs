using System;
using System.Collections.Generic;
using DeepCopy;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Solver
{
    interface IPuzzleSolver
    {
        void Solve(ref IPuzzle puzzle);
    }
}
