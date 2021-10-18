using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Helpers
{
    public static class Math
    {
        public static int Factorial(int n)
        {
            return n == 0 ? 1 : n * Factorial(n - 1);
        }
    }
}
