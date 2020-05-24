using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ProblemSolving.BySearching.Strategy.Utilities
{
    public class NodeStateEqualityComparer<TState, TAction>
        : EqualityComparer<Node<TState, TAction>> 
        where TState : IEquatable<TState>
    {
        public EqualityComparer<TState> StateEqualComparer { get; }

        public NodeStateEqualityComparer(EqualityComparer<TState> stateEqualComparer)
        {
            StateEqualComparer = stateEqualComparer;
        }

        public static EqualityComparer<Node<TState, TAction>> New()
        {
            return new NodeStateEqualityComparer<TState, TAction>(
                EqualityComparer<TState>.Default);
        }

        public override bool Equals([AllowNull] Node<TState, TAction> x, [AllowNull] Node<TState, TAction> y)
        {
            return StateEqualComparer.Equals(x.State, y.State);
        }

        public override int GetHashCode([DisallowNull] Node<TState, TAction> obj)
        {
            return obj.State.GetHashCode();
        }
    }
}
