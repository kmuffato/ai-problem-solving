using System;

namespace SearchingApp.Problems.EightPuzzle
{
    class HeuristicFunctions
    {
        public static double StraightLineDistance(
            ProblemState<int> stateSrc, ProblemState<int> stateDest)
        {
            double totalDist = 0;

            for (int i = 0; i < stateDest.GetLength(0); i++)
            {
                for (int j = 0; j < stateDest.GetLength(1); j++)
                {
                    totalDist += StraightLineDistance(
                        stateSrc.FindValueIndex(stateDest[i, j]),
                        (i, j));
                }
            }

            return totalDist;
        }


        public static double StraightLineDistance(
            (int x1, int x2) srcPiece, (int x1, int x2) destPiece)
        {
            var euclidienMetric = Math.Sqrt(
                Math.Pow(destPiece.x1 - srcPiece.x1, 2) +
                Math.Pow(destPiece.x2 - srcPiece.x2, 2));

            return euclidienMetric;
        }

        public static double ManhattanDistance(
            ProblemState<int> stateSrc, ProblemState<int> stateDest)
        {
            double totalDist = 0;

            for (int i = 0; i < stateDest.GetLength(0); i++)
            {
                for (int j = 0; j < stateDest.GetLength(1); j++)
                {
                    totalDist += ManhattanDistance(
                        stateSrc.FindValueIndex(stateDest[i, j]),
                        (i, j));
                }
            }

            return totalDist;
        }


        public static double ManhattanDistance(
            (int x1, int x2) srcPiece, (int x1, int x2) destPiece)
        {
            var euclidienMetric = Math.Abs(destPiece.x1 - srcPiece.x1) +
                Math.Abs(destPiece.x2 - srcPiece.x2);

            return euclidienMetric;
        }
    }
}
