namespace ProblemSolving.BySearching.Returns
{
    public class SolutionCutoff<TState, TAction> : SolutionSearchBase<TState, TAction>
    {
        public SolutionCutoff()
        {
            Node = default;
        }

        public override Node<TState, TAction> Node { get; set; }
    }
}
