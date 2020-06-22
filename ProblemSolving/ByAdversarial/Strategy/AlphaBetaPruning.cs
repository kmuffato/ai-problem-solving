using System;

namespace ProblemSolving.ByAdversarial.Strategy
{
    public class AlphaBetaPruning<TState, TAction>
        : IAdversarialSearchStrategy<TState, TAction>
    {
        private int searchPlayer;

        private IAdversarialSearchProblem<TState, TAction> problem;

        public TAction Search(TState state, IAdversarialSearchProblem<TState, TAction> problem)
        {
            this.problem = problem;

            searchPlayer = problem.Player(state);

            var a = default(TAction);

            var alpha = double.NegativeInfinity;

            var actions = problem.Actions(state);

            foreach (var action in actions)
            {
                var vector = MinValue(problem.Result(state, action), alpha, double.PositiveInfinity);

                if (vector > alpha)
                {
                    alpha = vector;

                    a = action;
                }
            }

            return a;
        }

        private double MaxValue(TState state, double alpha, double beta)
        {
            if (problem.TerminalTest(state))
            {
                return problem.Utility(state, searchPlayer);
            }

            var vector = double.NegativeInfinity;

            var actions = problem.Actions(state);

            foreach (var action in actions)
            {
                vector = Math.Max(vector, MinValue(problem.Result(state, action), alpha, beta));

                if (vector >= beta)
                {
                    return vector;
                }

                alpha = Math.Max(alpha, vector);
            }

            return vector;
        }

        private double MinValue(TState state, double alpha, double beta)
        {
            if (problem.TerminalTest(state))
            {
                return problem.Utility(state, searchPlayer);
            }

            var vector = double.PositiveInfinity;

            var actions = problem.Actions(state);

            foreach (var action in actions)
            {
                vector = Math.Min(vector, MaxValue(problem.Result(state, action), alpha, beta));

                if (vector <= alpha)
                {
                    return vector;
                }

                beta = Math.Min(beta, vector);
            }

            return vector;
        }
    }
}
