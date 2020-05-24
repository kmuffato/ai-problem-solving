using System;
using System.Diagnostics.CodeAnalysis;

namespace SearchingApp.Problems.EightPuzzle
{
    public class ProblemState<T> : IEquatable<ProblemState<T>>
    {
        private readonly T[,] array;

        public ProblemState(int i, int j)
        {
            array = new T[i, j];
        }

        public int Length => array.Length;

        public T this[int i, int j]
        {
            get => array[i, j];
            set => array[i, j] = value;
        }

        internal int GetLength(int dimension) => array.GetLength(dimension);

        public static void Copy(ProblemState<T> state, ProblemState<T> transitionState,
            int length) => Array.Copy(state.array, transitionState.array, length);

        public bool Equals([AllowNull] ProblemState<T> other)
        {
            if (other == null)
            {
                return false;
            }

            return this.StateEquals(other);
        }

        public override int GetHashCode()
        {
            var (row, col) = this.FindEmpty();
            return (GetLength(0) * row) + col;
        }
    }
}
