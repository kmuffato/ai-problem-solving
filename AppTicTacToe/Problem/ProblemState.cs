using System;
using System.Diagnostics.CodeAnalysis;

namespace AppTicTacToe.Problem
{
    public class ProblemState : IEquatable<ProblemState>
    {
        public char?[,] XorO { get; set; }

        public bool Equals([AllowNull] ProblemState other)
        {
            if (other == null)
            {
                return false;
            }

            if(other.XorO == null && XorO == null)
            {
                return true;
            }

            if (other.XorO == null)
            {
                return false;
            }

            for (int i = 0; i < XorO.GetLength(0); i++)
            {
                for (int j = 0; j < XorO.GetLength(1); j++)
                {
                    if (XorO[i, j] != other.XorO[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
