using System;
using System.Diagnostics.CodeAnalysis;

namespace AppEightPuzzle.Problem
{
    public sealed class ProblemAction : IEquatable<ProblemAction>
    {
        public string Move { get; }

        private ProblemAction()
        {

        }

        private ProblemAction(string move)
        {
            Move = move ?? string.Empty;
        }

        public static ProblemAction Nothing => new ProblemAction(string.Empty);

        public static ProblemAction Up => new ProblemAction(nameof(Up));

        public static ProblemAction Down => new ProblemAction(nameof(Down));

        public static ProblemAction Left => new ProblemAction(nameof(Left));

        public static ProblemAction Right => new ProblemAction(nameof(Right));

        public static bool operator ==(ProblemAction left, ProblemAction right)
        {
            return Equals(left, default)
                ? Equals(right, default)
                : Equals(right, default) ? false : string.Equals(left.Move, right.Move);
        }

        public static bool operator !=(ProblemAction left, ProblemAction right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj is ProblemAction action &&
                   Move == action.Move;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Move);
        }

        public bool Equals([AllowNull] ProblemAction other)
        {
            return this == other;
        }
    }
}
