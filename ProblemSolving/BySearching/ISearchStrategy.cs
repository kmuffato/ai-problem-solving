using ProblemSolving.BySearching.Returns;

namespace ProblemSolving.BySearching
{
    public interface ISearchStrategy<TState, TAction>
    {
        SolutionSearchBase<TState, TAction> Search(ISearchProblem<TState, TAction> problem);
    }
}
