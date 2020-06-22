using System;
using System.Collections.Generic;

namespace ProblemSolving.ByAdversarial.Agent
{
    public abstract class SimpleProblemSolvingAgentBase<TState, TAction>
        where TAction : IEquatable<TAction>
    {
        protected SimpleProblemSolvingAgentBase(IAdversarialSearchStrategy<TState, TAction> searchStrategy)
        {
            SearchStrategy = searchStrategy;
        }

        public List<TAction> Seq { get; set; }

        public TState State { get; set; }

        public IAdversarialSearchProblem<TState, TAction> Problem { get; set; }

        public IAdversarialSearchStrategy<TState, TAction> SearchStrategy { get; }

        public virtual TAction Work<TPercepted>(TPercepted percept)
        {
            State = UpdateState(State, percept);

            if (Seq == null || Seq.Count == 0)
            {
                Problem = FormulateProblem(State);

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

        protected abstract IAdversarialSearchProblem<TState, TAction> FormulateProblem(TState state);

        protected virtual List<TAction> Search(IAdversarialSearchProblem<TState, TAction> problem)
        {
            var action = SearchStrategy.Search(State, problem);

            var seq = new List<TAction>();

            if (action != null)
            {
                seq.Add(action);
            }

            return seq;
        }
    }
}