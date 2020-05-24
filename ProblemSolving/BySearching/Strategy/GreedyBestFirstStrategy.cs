using ProblemSolving.BySearching.Returns;
using ProblemSolving.BySearching.Strategy.Utilities;
using System;
using System.Collections.Generic;

namespace ProblemSolving.BySearching.Strategy
{
    public class GreedyBestFirstStrategy<TState, TAction> : ISearchStrategy<TState, TAction>
        where TState : IEquatable<TState>
    {
        public delegate double HeuristicFunction(TState source, TState destination);

        private readonly HeuristicFunction h;

        private readonly EqualityComparer<TState> sComparer
            = EqualityComparer<TState>.Default;

        public GreedyBestFirstStrategy(HeuristicFunction h)
        {
            this.h = h;
        }

        public SolutionSearchBase<TState, TAction> Search(
            IProblemDefinition<TState, TAction> problem)
        {
            var rootNode = Node<TState, TAction>.Root(problem.InitialState);

            if (problem.GoalTest(rootNode.State))
            {
                return new SolutionFound<TState, TAction>(rootNode);
            }

            var frontier = new BasicPriorityQueue<double, FrontierItem<TState, TAction>>(
                x => x.Evaluation);

            var explored = new HashSet<TState>(sComparer);

            frontier.Enqueue(new FrontierItem<TState, TAction>(rootNode, 
                EvaluationFuntion(problem, rootNode)));

            while (frontier.Count > 0)
            {
                var element = frontier.Dequeue();

                if (problem.GoalTest(element.Node.State))
                {
                    return new SolutionFound<TState, TAction>(element.Node);
                }

                explored.Add(element.Node.State);

                var actions = problem.Actions(element.Node.State);

                foreach (var action in actions)
                {
                    var child = Node<TState, TAction>.ChildNode(problem, element.Node, action);

                    var childElement = new FrontierItem<TState, TAction>(child, 
                        EvaluationFuntion(problem, child));

                    var comparedChild = frontier.CherryPeek(
                        x => sComparer.Equals(x.Node.State, childElement.Node.State));

                    bool stateInFrontier = comparedChild != default;

                    bool containsState = explored.Contains(child.State) || stateInFrontier;

                    if (!containsState)
                    {
                        frontier.Enqueue(childElement);
                    }
                    else if (stateInFrontier
                        && comparedChild.Evaluation > childElement.Evaluation)
                    {
                        frontier.ReplaceWith(comparedChild, childElement);
                    }
                }
            }

            return new SolutionFailure<TState, TAction>();
        }

        public double EvaluationFuntion(IProblemDefinition<TState, TAction> problem,
            Node<TState, TAction> node)
        {
            return h(node.State, problem.GoalState);
        }
    }
}
