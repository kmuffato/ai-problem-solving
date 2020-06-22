using System;

namespace ProblemSolving.ByAdversarial.Strategy
{
    public class MinimaxDecision<TState, TAction>
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
                var vector = MinValue(problem.Result(state, action));

                //var min = Minimax(Problem.Result(state, action));

                if (vector > alpha)
                {
                    alpha = vector;
                    a = action;
                }
            }

            return a;
        }

        private double MaxValue(TState state)
        {
            if (problem.TerminalTest(state))
            {
                return problem.Utility(state, searchPlayer);
            }

            var vector = double.NegativeInfinity;

            var actions = problem.Actions(state);

            foreach (var action in actions)
            {
                vector = Math.Max(vector, MinValue(problem.Result(state, action)));
            }

            return vector;
        }

        private double MinValue(TState state)
        {
            if (problem.TerminalTest(state))
            {
                return problem.Utility(state, searchPlayer);
            }

            var vector = double.PositiveInfinity;

            var actions = problem.Actions(state);

            foreach (var action in actions)
            {
                vector = Math.Min(vector, MaxValue(problem.Result(state, action)));
            }

            return vector;
        }

        private double Minimax(TState state)
        {
            if (problem.TerminalTest(state))
            {
                return problem.Utility(state, searchPlayer);
            }

            if (searchPlayer == problem.Player(state))
            {
                var vector = double.NegativeInfinity;

                var actions = problem.Actions(state);

                foreach (var action in actions)
                {
                    vector = Math.Max(vector, Minimax(problem.Result(state, action)));
                }

                return vector;
            }
            else
            {
                var vector = double.PositiveInfinity;

                var actions = problem.Actions(state);

                foreach (var action in actions)
                {
                    vector = Math.Min(vector, Minimax(problem.Result(state, action)));
                }
                return vector;
            }

        }
    }
}
