using PuzzleEnvironment;
using System;

namespace SearchingApp.Problems.EightPuzzle
{
    public static class ProblemStateExtensions
    {
        public static T[] ToValues<T>(this ProblemState<T> state)
        {
            int rows = state.GetLength(0);

            var values = new T[rows * state.GetLength(1)];

            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    values[rows * i + j] = state[i, j];
                }
            }

            return values;
        }

        public static ProblemState<T> ToProblemState<T>(this PuzzleBoard<T> perceptedObj)
        {
            var perceptedState = new ProblemState<T>(
                perceptedObj.Pieces.GetLength(0), perceptedObj.Pieces.GetLength(1));

            for (int i = 0; i < perceptedState.GetLength(0); i++)
            {
                for (int j = 0; j < perceptedState.GetLength(1); j++)
                {
                    perceptedState[i, j] = perceptedObj.Pieces[i, j].Value;
                }
            }

            return perceptedState;
        }

        public static ProblemState<T> ToProblemState<T>(this T[] values, int rows, int columns)
        {
            var state = new ProblemState<T>(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    state[i, j] = values[rows * i + j];
                }
            }

            return state;
        }

        public static PuzzleBoard<T> ToPuzzleBoard<T>(this ProblemState<T> problemState)
        {
            var state = new PuzzleBoard<T>(new PuzzlePiece<T>[problemState.GetLength(0), problemState.GetLength(1)]);

            for (int i = 0; i < problemState.GetLength(0); i++)
            {
                for (int j = 0; j < problemState.GetLength(0); j++)
                {
                    state.Pieces[i, j] = new PuzzlePiece<T> { Value = problemState[i, j] };
                }
            }

            return state;
        }

        public static PuzzleBoard<T> ToPuzzleBoard<T>(this T[] pieceValues, int rows, int columns)
        {
            var statePieces = new PuzzlePiece<T>[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    statePieces[i, j] = new PuzzlePiece<T> { Value = pieceValues[rows * i + j] };
                }
            }

            return new PuzzleBoard<T>(statePieces);
        }

        public static bool StateEquals<T>(this ProblemState<T> left, ProblemState<T> right)
        {
            for (int i = 0; i < left.GetLength(0); i++)
            {
                for (int j = 0; j < left.GetLength(1); j++)
                {
                    if (!Equals(left[i, j], right[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static (int rowNumber, int columnNumber) FindEmpty<T>(this ProblemState<T> state)
        {
            return state.FindValueIndex(default);
        }

        public static (int rowNumber, int columnNumber) FindValueIndex<T>(this ProblemState<T> state, T value)
        {
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (Equals(state[i, j], value))
                    {
                        return (i, j);
                    }
                }
            }

            throw new Exception("Invalid State: No empty field found");
        }
    }
}
