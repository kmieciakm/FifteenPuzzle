using FluentAssertions;
using Puzzle.Heuristics;
using Puzzle.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Puzzle.Tests
{
    public class PuzzleSolver_BEFS_UnitTests
    {
        [Fact]
        public void BEFS_SolvedPuzzle()
        {
            var puzzle = Puzzle.GetSolvedPuzzle(4);

            BEFS solver = new();
            ZeroHeuristic heuristic = new();
            var result = solver.Solve(ref puzzle, heuristic);

            result.Should().BeTrue();
            puzzle.IsSolved().Should().BeTrue();
        }

        [Fact]
        public void BEFS_Euclidean_SimplePuzzle()
        {
            IPuzzle puzzle = new Puzzle(3)
            {
                Board = new int[][]
                {
                    new int[3] { 2, 3, 5 },
                    new int[3] { 6, 4, 7 },
                    new int[3] { 1, 8, 0 }
                }
            };

            BEFS solver = new();
            Euclidean heuristic = new();
            var result = solver.Solve(ref puzzle, heuristic);

            result.Should().BeTrue();
            puzzle.IsSolved().Should().BeTrue();
        }

        [Fact]
        public void BEFS_Manhattan_ComplexPuzzle()
        {
            IPuzzle puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  1,  2,  3,  0 },
                    new int[4] {  5, 13,  7,  8 },
                    new int[4] { 10,  9, 12,  4 },
                    new int[4] {  6, 14, 15, 11 }
                }
            };

            BEFS solver = new();
            Manhattan heuristic = new();
            var result = solver.Solve(ref puzzle, heuristic);

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

            BEFS solver = new();
            Manhattan heuristic = new();
            var result = solver.Solve(ref puzzle, heuristic);

            result.Should().BeFalse();
            puzzle.IsSolved().Should().BeFalse();
        }
    }
}
