using ProblemSolving.BySearching.Returns;
using ProblemSolving.BySearching.Strategy.Utilities;
using System;
using System.Collections.Generic;

namespace ProblemSolving.BySearching.Strategy
{
    public class UniformCostStrategy2<TState, TAction> : ISearchStrategy<TState, TAction>
        where TState : IEquatable<TState>
    {
        private readonly EqualityComparer<TState> sComparer 
            = EqualityComparer<TState>.Default;

        public SolutionSearchBase<TState, TAction> Search(IProblemDefinition<TState, TAction> problem)
        {
            var rootNode = Node<TState, TAction>.Root(problem.InitialState);

            if (problem.GoalTest(rootNode.State))
            {
                return new SolutionFound<TState, TAction>(rootNode);
            }

            var frontier = new BasicPriorityQueue<double, FrontierItem<TState, TAction>>(
                x => x.Evaluation);

            var explored = new HashSet<TState>(sComparer);

            frontier.Enqueue(new FrontierItem<TState, TAction>(rootNode, EvaluationFuntion(rootNode)));

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

                    var childElement = new FrontierItem<TState,TAction>(child, EvaluationFuntion(child));

                    var comparedChild = frontier.CherryPeek(
                        x => sComparer.Equals(x.Node.State, childElement.Node.State));

                    var stateInFrontier = comparedChild != default;

                    var containsState = explored.Contains(child.State) || stateInFrontier;

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

        public double EvaluationFuntion(Node<TState, TAction> node)
        {
            return node.PathCost;
        }
    }
}
