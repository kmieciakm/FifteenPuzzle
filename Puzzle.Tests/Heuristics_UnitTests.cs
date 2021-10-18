using FluentAssertions;
using Puzzle.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Puzzle.Tests
{
    public class Heuristics_UnitTests
    {
        [Fact]
        public void Zero()
        {
            IPuzzle puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  5,  2,  3, 13 },
                    new int[4] {  1,  7,  6,  8 },
                    new int[4] {  0, 10, 11, 12 },
                    new int[4] {  4, 14, 15,  9 }
                }
            };
            IPuzzle solvedPuzzle = Puzzle.GetSolvedPuzzle(puzzle.BoardSize);

            ZeroHeuristic zeroHeuristic = new();
            zeroHeuristic.Calculate(puzzle, solvedPuzzle).Should().Be(0f);
        }

        [Fact]
        public void Hamming()
        {
            IPuzzle puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  1,  2,  3, 13 },
                    new int[4] {  5,  7,  6,  8 },
                    new int[4] {  9, 10, 11, 12 },
                    new int[4] {  4, 14, 15,  0 }
                }
            };
            IPuzzle solvedPuzzle = Puzzle.GetSolvedPuzzle(puzzle.BoardSize);

            Hamming hamming = new();
            hamming.Calculate(puzzle, solvedPuzzle).Should().Be(4f);
        }

        [Fact]
        public void Euclidean()
        {
            IPuzzle puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  1,  2,  3,  4 },
                    new int[4] {  5,  6, 14,  8 },
                    new int[4] {  9, 10, 11, 12 },
                    new int[4] { 13,  7, 15,  0 }
                }
            };
            IPuzzle solvedPuzzle = Puzzle.GetSolvedPuzzle(puzzle.BoardSize);
            var expectedResult = 2 * (float)Math.Sqrt(5f);

            Euclidean euclidean = new();
            euclidean.Calculate(puzzle, solvedPuzzle).Should().Be(expectedResult);
        }

        [Fact]
        public void Manhattan()
        {
            IPuzzle puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  1,  2, 14,  4 },
                    new int[4] {  5,  6,  7,  8 },
                    new int[4] {  9, 10, 11, 12 },
                    new int[4] { 13,  3, 15,  0 }
                }
            };
            IPuzzle solvedPuzzle = Puzzle.GetSolvedPuzzle(puzzle.BoardSize);

            Manhattan manhattan = new();
            var result = manhattan.Calculate(puzzle, solvedPuzzle);
            result.Should().Be(8f);
        }
    }
}
