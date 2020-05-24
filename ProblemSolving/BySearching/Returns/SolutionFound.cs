namespace ProblemSolving.BySearching.Returns
{
    public class SolutionFound<TState, TAction> : SolutionSearchBase<TState, TAction>
    {
        public SolutionFound(Node<TState, TAction> node)
        {
            Node = node;
        }

        public override Node<TState, TAction> Node { get; set; }
    }
}
