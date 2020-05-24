namespace ProblemSolving.BySearching.Returns
{
    public abstract class SolutionSearchBase<TState, TAction>
    {
        public abstract Node<TState, TAction> Node { get; set; }
    }
}
