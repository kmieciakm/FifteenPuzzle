using FluentAssertions;
using Puzzle.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Puzzle.Tests
{
    public class PuzzleSolver_BFS_UnitTests
    {
        [Fact]
        public void BFS_SolvedPuzzle()
        {
            IPuzzle puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  1,  2,  3,  4 },
                    new int[4] {  5,  6,  7,  8 },
                    new int[4] {  9, 10, 11, 12 },
                    new int[4] { 13, 14, 15,  0 }
                }
            };

            BFS BFSsolver = new();
            var result = BFSsolver.Solve(ref puzzle);

            result.Should().BeTrue();
            puzzle.IsSolved().Should().BeTrue();
        }

        [Fact]
        public void BFS_SimplePuzzle()
        {
            IPuzzle puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  1,  2,  3,  4 },
                    new int[4] {  5,  6,  7,  8 },
                    new int[4] {  9, 10,  0, 12 },
                    new int[4] { 13, 14, 11, 15 }
                }
            };

            BFS BFSsolver = new();
            var result = BFSsolver.Solve(ref puzzle);

            result.Should().BeTrue();
            puzzle.IsSolved().Should().BeTrue();
        }

        [Fact]
        public void BFS_ComplexPuzzle()
        {
            IPuzzle puzzle = new Puzzle(3)
            {
                Board = new int[][] {
                    new int[3] { 0, 1, 3 },
                    new int[3] { 5, 2, 6 },
                    new int[3] { 4, 7, 8 }
                }
            };

            BFS BFSsolver = new();
            var result = BFSsolver.Solve(ref puzzle);

            result.Should().BeTrue();
            puzzle.IsSolved().Should().BeTrue();
        }

        [Fact]
        public void BFS_ImpossiblePuzzle()
        {
            IPuzzle puzzle = new Puzzle(2)
            {
                Board = new int[][] {
                    new int[2] {  1,  0 },
                    new int[2] {  2,  3 }
                }
            };

            BFS BFSsolver = new();
            var result = BFSsolver.Solve(ref puzzle);

            result.Should().BeFalse();
            puzzle.IsSolved().Should().BeFalse();
        }
    }
}
