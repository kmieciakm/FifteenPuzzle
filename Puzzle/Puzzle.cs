using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Puzzle
{
    public interface IPuzzle
    {
        int[][] Board { get; init; }
        int BoardSize { get; }

        void GenerateBoard();
        bool IsSolved();
        bool CanMakeMove(Move move);
        void TryMakeMove(Move move);
        public List<Move> GetPossibleMoves();
    }

    public class Puzzle : IPuzzle
    {
        private static readonly int Empty = 0;

        public int[][] Board { get; init; }
        public int BoardSize { get; }

        public Puzzle(int boardSize)
        {
            BoardSize = boardSize;
            Board = new int[boardSize][];
            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                Board[rowIndex] = new int[BoardSize];
            }
        }

        public void GenerateBoard()
        {
            Random random = new();
            var numbers = Enumerable.Range(0, BoardSize * BoardSize)
                .OrderBy(x => random.Next())
                .Select(x => (int)x)
                .ToArray();
            var numberIndex = 0;

            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < BoardSize; columnIndex++)
                {
                    Board[rowIndex][columnIndex] = numbers[numberIndex];
                    numberIndex++;
                }
            }
        }

        public bool IsSolved()
        {
            var boardFields = Board.SelectMany(f => f).ToList();
            var solvedFields = Enumerable.Range(1, (Board.Length * Board.Length) - 1).Append(0);
            return boardFields.SequenceEqual(solvedFields);
        }

        public bool CanMakeMove(Move move)
        {
            if (IsValidField(move.X, move.Y) is false) return false;

            return move.Direction switch
            {
                Direction.DOWN =>  IsValidField(move.X + 1, move.Y) && IsEmptyField(move.X + 1, move.Y),
                Direction.UP =>    IsValidField(move.X - 1, move.Y) && IsEmptyField(move.X - 1, move.Y),
                Direction.LEFT =>  IsValidField(move.X, move.Y - 1) && IsEmptyField(move.X, move.Y - 1),
                Direction.RIGHT => IsValidField(move.X, move.Y + 1) && IsEmptyField(move.X, move.Y + 1),
                _ => false
            };

            bool IsValidField(int x, int y)
            {
                return 
                    x >= 0 &&
                    x < Board.Length &&
                    y >= 0 &&
                    y < Board[move.X].Length;
            }
            bool IsEmptyField(int x, int y)
            {
                return Board[x][y] == Empty;
            }
        }

        public void TryMakeMove(Move move)
        {
            if (CanMakeMove(move))
            {
                switch (move.Direction)
                {
                    case Direction.DOWN:  Swap(move.X, move.Y, move.X + 1, move.Y); break;
                    case Direction.UP:    Swap(move.X, move.Y, move.X - 1, move.Y); break;
                    case Direction.LEFT:  Swap(move.X, move.Y, move.X, move.Y - 1); break;
                    case Direction.RIGHT: Swap(move.X, move.Y, move.X, move.Y + 1); break;
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

        private void Swap(int x1, int y1, int x2, int y2)
        {
            var temp = Board[x1][y1];
            Board[x1][y1] = Board[x2][y2];
            Board[x2][y2] = temp;
        }
    }

    public class PuzzleComparer : IEqualityComparer<IPuzzle>
    {
        public bool Equals(IPuzzle puzzle1, IPuzzle puzzle2)
        {
            var puzzle1Fields = puzzle1.Board.SelectMany(f => f).ToList();
            var puzzle2Fields = puzzle2.Board.SelectMany(f => f).ToList();
            return puzzle1Fields.SequenceEqual(puzzle2Fields);
        }

        public int GetHashCode([DisallowNull] IPuzzle obj)
        {
            return obj.GetHashCode();
        }
    }
}
