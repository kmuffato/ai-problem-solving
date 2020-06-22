namespace ProblemSolving.BySearching
{
    public static class NodeExtensions
    {
        public static Node<TState, TAction> ChildNode<TState, TAction>(
            ISearchProblem<TState, TAction> problem,
            Node<TState, TAction> parent,
            TAction action)
        {
            return new Node<TState, TAction>(
                problem.Result(parent.State, action),
                parent,
                action,
                parent.PathCost + problem.StepCost(parent.State, action));
        }
    }
}
