namespace ProblemSolving.BySearching
{
    public class Node<TState, TAction>
    {
        protected Node()
        { }

        public Node(
            TState state,
            Node<TState, TAction> parent,
            TAction action,
            double pathCost)
        {
            State = state;
            Parent = parent;
            Action = action;
            PathCost = pathCost;
        }

        public TState State { get; private set; }

        public Node<TState, TAction> Parent { get; private set; }

        public TAction Action { get; private set; }

        public double PathCost { get; private set; }

        public static Node<TState, TAction> Root(TState state)
        {
            return new Node<TState, TAction>(state, default, default, default);
        }

        public static Node<TState, TAction> ChildNode(
            IProblemDefinition<TState, TAction> problem,
            Node<TState, TAction> parent,
            TAction action)
        {
            return new Node<TState, TAction>(
                problem.TransitionModel(parent.State, action),
                parent,
                action,
                parent.PathCost + problem.StepCost(parent.State, action));
        }
    }
}
