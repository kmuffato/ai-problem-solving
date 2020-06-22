using ProblemSolving.BySearching.Returns;

namespace ProblemSolving.BySearching.Strategy
{
    public class DepthLimitedStrategy<TState, TAction> : ISearchStrategy<TState, TAction>
    {
        public DepthLimitedStrategy(int limit)
        {
            Limit = limit;
        }

        public int Limit { get; }

        public SolutionSearchBase<TState, TAction> Search(ISearchProblem<TState, TAction> problem)
        {
            return RecursiveDLS(Node<TState, TAction>.Root(problem.InitialState), problem, Limit);
        }

        private SolutionSearchBase<TState, TAction> RecursiveDLS(
            Node<TState, TAction> node, ISearchProblem<TState, TAction> problem, int limit)
        {
            if (problem.GoalTest(node.State))
            {
                return new SolutionFound<TState, TAction>(node);
            }
            else if (limit == 0)
            {
                return new SolutionCutoff<TState, TAction>();
            }
            else
            {
                var cutoffOccurred = false;

                var actions = problem.Actions(node.State);

                foreach (var action in actions)
                {
                    var child = NodeExtensions.ChildNode(problem, node, action);

                    var result = RecursiveDLS(child, problem, limit - 1);

                    if (result.GetType() == typeof(SolutionCutoff<TState, TAction>))
                    {
                        cutoffOccurred = true;
                    }
                    else if (result.GetType() != typeof(SolutionFailure<TState, TAction>))
                    {
                        return result;
                    }
                }

                if (cutoffOccurred)
                {
                    return new SolutionCutoff<TState, TAction>();
                }
                else
                {
                    return new SolutionFailure<TState, TAction>();
                }
            }
        }
    }
}
