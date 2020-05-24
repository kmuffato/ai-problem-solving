using ProblemSolving.BySearching.Returns;

namespace ProblemSolving.BySearching
{
    public interface ISearchStrategy<TState, TAction>
    {
        SolutionSearchBase<TState, TAction> Search(IProblemDefinition<TState, TAction> problem);
    }
}
