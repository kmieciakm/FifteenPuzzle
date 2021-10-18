using System;
using System.Collections.Generic;
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
        (int x, int y) GetField(int value);
        IEnumerable<Move> GetPossibleMoves();
        IPuzzle GetCopy();
    }

    public class Puzzle : IPuzzle
    {
        public static readonly int EmptyField = 0;

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
            var numberIndex = 0;
            var numbers = Enumerable.Range(0, BoardSize * BoardSize)
                .OrderBy(x => random.Next())
                .ToArray();

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
            var number = 1;
            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < BoardSize; columnIndex++)
                {
                    if (Board[rowIndex][columnIndex] != number)
                    {
                        return false;
                    }
                    number++;
                    if (number == BoardSize * BoardSize) number = 0;
                }
            }
            return true;
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
                    y < Board[x].Length;
            }
            bool IsEmptyField(int x, int y)
            {
                return Board[x][y] == EmptyField;
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

        public IEnumerable<Move> GetPossibleMoves()
        {
            var (x, y) = GetField(EmptyField);
            var moves = new List<Move>()
            {
                new Move(x + 1, y, Direction.UP),
                new Move(x - 1, y, Direction.DOWN),
                new Move(x, y - 1, Direction.RIGHT),
                new Move(x, y + 1, Direction.LEFT)
            };
            return moves.Where(move => CanMakeMove(move));
        }

        public (int x, int y) GetField(int value)
        {
            (int, int) field = new();
            for (int rowIndex = 0; rowIndex < Board.Length; rowIndex++)
            {
                bool @break = false;
                for (int columnIndex = 0; columnIndex < Board[rowIndex].Length; columnIndex++)
                {
                    if (Board[rowIndex][columnIndex] == value)
                    {
                        field = (rowIndex, columnIndex);
                        @break = true;
                        break;
                    }
                }
                if (@break) break;
            }
            return field;
        }

        private void Swap(int x1, int y1, int x2, int y2)
        {
            var temp = Board[x1][y1];
            Board[x1][y1] = Board[x2][y2];
            Board[x2][y2] = temp;
        }

        public static IPuzzle GetSolvedPuzzle(int size)
        {
            var solvedPuzzle = new Puzzle(size);
            var number = 1;

            for (int rowIndex = 0; rowIndex < size; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < size; columnIndex++)
                {
                    solvedPuzzle.Board[rowIndex][columnIndex] = number;
                    number++;
                }
            }
            solvedPuzzle.Board[size - 1][size - 1] = 0;
            return solvedPuzzle;
        }

        public override bool Equals(object obj)
        {
            if (obj is Puzzle puzzle)
            {
                for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < BoardSize; columnIndex++)
                    {
                        if (puzzle.Board[rowIndex][columnIndex] != Board[rowIndex][columnIndex])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Board);
        }

        public IPuzzle GetCopy()
        {
            var board = new int[BoardSize][];
            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                board[rowIndex] = new int[BoardSize];
                for (int columnIndex = 0; columnIndex < BoardSize; columnIndex++)
                {
                    board[rowIndex][columnIndex] = Board[rowIndex][columnIndex];
                }
            }
            return new Puzzle(BoardSize) { Board = board };
        }
    }
}
