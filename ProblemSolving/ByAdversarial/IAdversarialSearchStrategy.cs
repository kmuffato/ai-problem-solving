namespace ProblemSolving.ByAdversarial
{
    public interface IAdversarialSearchStrategy<TState, TAction>
    {
        TAction Search(TState state, IAdversarialSearchProblem<TState, TAction> Problem);
    }
}
