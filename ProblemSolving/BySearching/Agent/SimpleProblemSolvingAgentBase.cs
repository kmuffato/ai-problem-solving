using System;
using System.Collections.Generic;

namespace ProblemSolving.BySearching.Agent
{
    public abstract class SimpleProblemSolvingAgentBase<TState, TAction>
        where TAction : IEquatable<TAction>
    {
        protected SimpleProblemSolvingAgentBase(ISearchStrategy<TState, TAction> searchStrategy)
        {
            SearchStrategy = searchStrategy;
        }

        public List<TAction> Seq { get; set; }

        public TState State { get; set; }

        public TState Goal { get; set; }

        public ISearchProblem<TState, TAction> Problem { get; set; }

        public ISearchStrategy<TState, TAction> SearchStrategy { get; }

        public virtual TAction Work<TPercepted>(TPercepted percept)
        {
            State = UpdateState(State, percept);

            if (Seq == null || Seq.Count == 0)
            {
                Goal = FormulateGoal(State);

                Problem = FormulateProblem(State, Goal);

                Seq = Search(Problem);

                if (Seq.Count == 0)
                {
                    return default;
                }
            }

            var act = Seq[0];

            Seq = Seq.GetRange(1, Seq.Count - 1);

            return act;
        }

        protected abstract TState UpdateState<TPercepted>(TState state, TPercepted percept);

        protected abstract TState FormulateGoal(TState state);

        protected abstract ISearchProblem<TState, TAction> FormulateProblem(TState state, TState goal);

        protected virtual List<TAction> Search(ISearchProblem<TState, TAction> problem)
        {
            var solution = SearchStrategy.Search(problem);

            var seq = new List<TAction>();

            var node = solution.Node;

            while (node != default && node.Action != null && !node.Action.Equals(default))
            {
                seq.Add(node.Action);

                node = node.Parent;
            }

            seq.Reverse();

            return seq;
        }
    }
}