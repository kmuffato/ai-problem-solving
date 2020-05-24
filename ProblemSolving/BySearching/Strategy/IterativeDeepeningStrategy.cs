using ProblemSolving.BySearching.Returns;

namespace ProblemSolving.BySearching.Strategy
{
    public class IterativeDeepeningStrategy<TState, TAction>
        : ISearchStrategy<TState, TAction>
    {
        public SolutionSearchBase<TState, TAction> Search(IProblemDefinition<TState, TAction> problem)
        {
            for (int i = 0; i < int.MaxValue; i++)
            {
                var result = new DepthLimitedStrategy<TState, TAction>(i).Search(problem);

                if(result.GetType() != typeof(SolutionCutoff<TState,TAction>))
                {
                    return result;
                }
            }

            return new SolutionFailure<TState, TAction>();
        }
    }
}
