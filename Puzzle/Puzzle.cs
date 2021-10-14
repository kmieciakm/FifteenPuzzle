using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Puzzle
{
    public interface IPuzzle
    {
        int[][] Board { get; set; }
        int BoardSize { get; set; }

        void GenerateBoard();
        bool IsSolved();
        bool CanMakeMove(Move move);
        void TryMakeMove(Move move);
        public List<Move> GetPossibleMoves();
    }

    public class Puzzle : IPuzzle
    {
        public static int[][] SolvedBoard
        {
            get
            {
                return new int[][] {
                    new int[4] {  1,  2,  3,  4 },
                    new int[4] {  5,  6,  7,  8 },
                    new int[4] {  9, 10, 11, 12 },
                    new int[4] { 13, 14, 15,  0 },
                };
            }
        }
        public static int Empty { get { return 0; } }

        public int[][] Board { get; set; }
        public int BoardSize { get; set; } = 4;

        public void GenerateBoard()
        {
            Random random = new();
            var numbers = Enumerable.Range(0, BoardSize * BoardSize)
                .OrderBy(x => random.Next())
                .Select(x => (int)x)
                .ToArray();
            var numberIndex = 0;

            Board = new int[BoardSize][];
            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                Board[rowIndex] = new int[BoardSize];
                for (int columnIndex = 0; columnIndex < BoardSize; columnIndex++)
                {
                    Board[rowIndex][columnIndex] = numbers[numberIndex];
                    numberIndex++;
                }
            }
        }

        public bool IsSolved()
        {
            // TODO: Can be optimized
            var boardFields = Board.SelectMany(f => f).ToList();
            var solvedBoardFields = Enumerable.Range(1, (Board.Length * Board.Length) - 1).Append(0);
            return boardFields.SequenceEqual(solvedBoardFields);
        }

        public bool CanMakeMove(Move move)
        {
            Predicate<Move> isValidField = move => { return 
                (move.X >= 0 && move.X < Board.Length) &&
                (move.Y >= 0 && move.Y < Board[move.X].Length);
            };

            return move.Direction switch
            {
                Direction.DOWN =>  isValidField(move) && move.X + 1 < Board.Length         && Board[move.X + 1][move.Y] == Empty,
                Direction.UP =>    isValidField(move) && move.X - 1 >= 0                   && Board[move.X - 1][move.Y] == Empty,
                Direction.LEFT =>  isValidField(move) && move.Y - 1 >= 0                   && Board[move.X][move.Y - 1] == Empty,
                Direction.RIGHT => isValidField(move) && move.Y + 1 < Board[move.X].Length && Board[move.X][move.Y + 1] == Empty,
                _ => false
            };
        }

        public void TryMakeMove(Move move)
        {
            if (CanMakeMove(move))
            {
                switch (move.Direction)
                {
                    case Direction.DOWN:  Swap((move.X, move.Y), (move.X + 1, move.Y)); break;
                    case Direction.UP:    Swap((move.X, move.Y), (move.X - 1, move.Y)); break;
                    case Direction.LEFT:  Swap((move.X, move.Y), (move.X, move.Y - 1)); break;
                    case Direction.RIGHT: Swap((move.X, move.Y), (move.X, move.Y + 1)); break;
                };
            }
        }

        public List<Move> GetPossibleMoves()
        {
            var emptyField = GetEmptyField();
            var moves = new List<Move>()
            {
                new Move(emptyField.x + 1, emptyField.y, Direction.UP),
                new Move(emptyField.x - 1, emptyField.y, Direction.DOWN),
                new Move(emptyField.x, emptyField.y - 1, Direction.RIGHT),
                new Move(emptyField.x, emptyField.y + 1, Direction.LEFT)
            };
            return moves.Where(move => CanMakeMove(move)).ToList();
        }

        private (int x, int y) GetEmptyField()
        {
            (int, int) filed = new();
            for (int rowIndex = 0; rowIndex < Board.Length; rowIndex++)
            {
                bool @break = false;
                for (int columnIndex = 0; columnIndex < Board[rowIndex].Length; columnIndex++)
                {
                    if (Board[rowIndex][columnIndex] == Empty)
                    {
                        filed = (rowIndex, columnIndex);
                        @break = true;
                        break;
                    }
                }
                if (@break) break;
            }
            return filed;
        }

        private void Swap((int x, int y) field1, (int x, int y) field2)
        {
            var temp = Board[field1.x][field1.y];
            Board[field1.x][field1.y] = Board[field2.x][field2.y];
            Board[field2.x][field2.y] = temp;
        }
    }

    public class PuzzleComperer : IEqualityComparer<IPuzzle>
    {
        public bool Equals(IPuzzle x, IPuzzle y)
        {
            var xPuzzleFields = x.Board.SelectMany(f => f).ToList();
            var yPuzzleFields = y.Board.SelectMany(f => f).ToList();
            return xPuzzleFields.SequenceEqual(yPuzzleFields);
        }

        public int GetHashCode([DisallowNull] IPuzzle obj)
        {
            return obj.GetHashCode();
        }
    }
}
