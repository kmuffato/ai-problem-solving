using ProblemSolving.BySearching.Strategy.Utilities;
using System;
using System.Collections.Generic;
using ProblemSolving.BySearching.Returns;

namespace ProblemSolving.BySearching.Strategy
{
    public class UniformCostStrategy<TState, TAction> : ISearchStrategy<TState, TAction>
        where TState : IEquatable<TState>
    {
        private readonly EqualityComparer<TState> sComparer
            = EqualityComparer<TState>.Default;

        public SolutionSearchBase<TState, TAction> Search(ISearchProblem<TState, TAction> problem)
        {
            var rootNode = Node<TState, TAction>.Root(problem.InitialState);

            if (problem.GoalTest(rootNode.State))
            {
                return new SolutionFound<TState, TAction>(rootNode);
            }

            var frontier = new BasicPriorityQueue<double, Node<TState, TAction>>(
                x => x.PathCost);

            var explored = new HashSet<TState>(sComparer);

            frontier.Enqueue(rootNode);

            while (frontier.Count > 0)
            {
                var node = frontier.Dequeue();

                if (problem.GoalTest(node.State))
                {
                    return new SolutionFound<TState, TAction>(node);
                }

                explored.Add(node.State);

                var actions = problem.Actions(node.State);

                foreach (var action in actions)
                {
                    var child = NodeExtensions.ChildNode(problem, node, action);

                    var comparedChild = frontier.CherryPeek(
                        n => sComparer.Equals(n.State, child.State));

                    bool stateInFrontier = comparedChild != default;

                    bool containsState = explored.Contains(child.State) || stateInFrontier;

                    if (!containsState)
                    {
                        frontier.Enqueue(child);
                    }
                    else if (stateInFrontier
                        && comparedChild.PathCost > child.PathCost)
                    {
                        frontier.ReplaceWith(comparedChild, child);
                    }
                }
            }

            return new SolutionFailure<TState, TAction>();
        }
    }
}
