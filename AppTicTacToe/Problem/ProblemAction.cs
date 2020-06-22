using System;
using System.Diagnostics.CodeAnalysis;

namespace AppTicTacToe.Problem
{
    public class ProblemAction : IEquatable<ProblemAction>
    {
        public (int x, int y) PutPosition { get; set; }

        public char Put { get; set; }

        public bool Equals([AllowNull] ProblemAction other)
        {
            return other != null && other.PutPosition == PutPosition && other.Put == Put;
        }
    }
}
