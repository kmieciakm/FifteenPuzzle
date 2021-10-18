using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Puzzle.Tests
{
    public class Puzzle_UnitTests
    {
        [Fact]
        public void BoardGeneration()
        {
            var puzzle = new Puzzle(4);
            puzzle.GenerateBoard();

            var fields = puzzle.Board.SelectMany(field => field).OrderBy(field => field).ToList();
            var fieldsExpected = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            fields.Should().BeEquivalentTo(fieldsExpected);
        }

        [Fact]
        public void GetSolvedPuzzle()
        {
            Puzzle.GetSolvedPuzzle(4).IsSolved().Should().BeTrue();
        }

        [Fact]
        public void IsSolved_True()
        {
            var puzzle = Puzzle.GetSolvedPuzzle(4);
            puzzle.IsSolved().Should().BeTrue();
        }

        [Fact]
        public void IsSolved_False()
        {
            var puzzle = Puzzle.GetSolvedPuzzle(4);

            var temp = puzzle.Board[0][0];
            puzzle.Board[0][0] = puzzle.Board[0][1];
            puzzle.Board[0][1] = temp;

            puzzle.IsSolved().Should().BeFalse();
        }

        [Fact]
        public void CanMove()
        {
            var puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  1,  2,  3,  4 },
                    new int[4] {  5,  6,  7,  8 },
                    new int[4] {  9, 10, 11, 12 },
                    new int[4] { 13, 14,  0, 15 }
                }
            };

            puzzle.CanMakeMove(new Move(1, 1, Direction.UP)).Should().BeFalse();
            puzzle.CanMakeMove(new Move(1, 1, Direction.DOWN)).Should().BeFalse();
            puzzle.CanMakeMove(new Move(1, 1, Direction.RIGHT)).Should().BeFalse();
            puzzle.CanMakeMove(new Move(1, 1, Direction.LEFT)).Should().BeFalse();

            puzzle.CanMakeMove(new Move(3, 3, Direction.UP)).Should().BeFalse();
            puzzle.CanMakeMove(new Move(3, 3, Direction.DOWN)).Should().BeFalse();
            puzzle.CanMakeMove(new Move(3, 3, Direction.RIGHT)).Should().BeFalse();
            puzzle.CanMakeMove(new Move(3, 3, Direction.LEFT)).Should().BeTrue();
        }

        [Fact]
        public void CanMove_SmallPuzzle()
        {
            IPuzzle puzzle = new Puzzle(3)
            {
                Board = new int[][] {
                    new int[3] { 1, 2, 3 },
                    new int[3] { 4, 5, 6 },
                    new int[3] { 7, 0, 8 }
                }
            };

            puzzle.CanMakeMove(new Move(4, 1, Direction.UP)).Should().BeFalse();
            puzzle.CanMakeMove(new Move(1, 1, Direction.DOWN)).Should().BeTrue();
            puzzle.CanMakeMove(new Move(2, 0, Direction.RIGHT)).Should().BeTrue();
            puzzle.CanMakeMove(new Move(2, 2, Direction.LEFT)).Should().BeTrue();
        }

        [Fact]
        public void MakeMove()
        {
            var puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  1,  2,  3,  4 },
                    new int[4] {  5,  6,  7,  8 },
                    new int[4] {  9, 10, 11, 12 },
                    new int[4] { 13, 14,  0, 15 }
                }
            };

            var move = new Move(3, 3, Direction.LEFT);
            puzzle.TryMakeMove(move);
            puzzle.IsSolved().Should().BeTrue();
        }

        [Fact]
        public void MakeMove_Twice()
        {
            var puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] { 1, 2, 3, 4 },
                    new int[4] { 5, 6, 7, 8 },
                    new int[4] { 9, 10, 0, 12 },
                    new int[4] { 13, 14, 11, 15 }
                }
            };

            puzzle.TryMakeMove(new Move(3, 2, Direction.UP));
            puzzle.TryMakeMove(new Move(3, 3, Direction.LEFT));
            puzzle.IsSolved().Should().BeTrue();
        }

        [Fact]
        public void GetPossibleMoves_Center()
        {
            var puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  1,  2,  3,  4 },
                    new int[4] {  5,  6,  7,  8 },
                    new int[4] {  9, 10,  0, 12 },
                    new int[4] { 13, 14, 11, 15 }
                }
            };

            List<Move> possibleMoves = new()
            {
                new Move(3, 2, Direction.UP),
                new Move(2, 3, Direction.LEFT),
                new Move(1, 2, Direction.DOWN),
                new Move(2, 1, Direction.RIGHT)
            };

            puzzle.GetPossibleMoves().Should().BeEquivalentTo(possibleMoves);
        }

        [Fact]
        public void GetPossibleMoves_Border()
        {
            var puzzle = new Puzzle(4)
            {
                Board = new int[][] {
                    new int[4] {  1,  2,  3,  4 },
                    new int[4] {  5,  6,  7,  8 },
                    new int[4] {  9, 10, 11, 12 },
                    new int[4] { 13, 14,  0, 15 }
                }
            };

            List<Move> possibleMoves = new()
            {
                new Move(2, 2, Direction.DOWN),
                new Move(3, 3, Direction.LEFT),
                new Move(3, 1, Direction.RIGHT)
            };

            puzzle.GetPossibleMoves().Should().BeEquivalentTo(possibleMoves);
        }
    }
}
