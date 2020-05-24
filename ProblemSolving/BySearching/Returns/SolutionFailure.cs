namespace ProblemSolving.BySearching.Returns
{
    public class SolutionFailure<TState, TAction> : SolutionSearchBase<TState, TAction>
    {
        public SolutionFailure()
        {
            Node = default;
        }

        public override Node<TState, TAction> Node { get; set; }
    }
}
