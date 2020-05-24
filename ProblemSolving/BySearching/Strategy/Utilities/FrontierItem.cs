namespace ProblemSolving.BySearching.Strategy.Utilities
{
    public class FrontierItem<TState, TAction>
    {
        public FrontierItem(Node<TState, TAction> node, double evaluation)
        {
            Node = node;
            Evaluation = evaluation;
        }

        public Node<TState, TAction> Node { get; private set; }


        public double Evaluation { get; private set; }
    }
}
