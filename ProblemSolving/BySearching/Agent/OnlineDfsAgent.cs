using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSolving.BySearching.Agent
{
    public abstract class OnlineDfsAgent<TState, TAction>
        where TState : IEquatable<TState>
    {
        protected readonly EqualityComparer<TState> sEqComp = EqualityComparer<TState>.Default;

        public ISearchProblem<TState, TAction> Problem { get; set; }

        public Dictionary<(TState State, TAction Action), TState> Result { get; set; }

        public Dictionary<TState, Stack<TAction>> Untried { get; set; }

        public Dictionary<TState, Stack<TState>> Unbacktracked { get; set; }

        public (TState State, TAction Action) Previous { get; set; }

        protected abstract TState UpdateState<TPercepted>(TPercepted percept);

        protected abstract ISearchProblem<TState, TAction> FormulateProblem(
            TState state);

        public virtual TAction Work<TPercepted>(TPercepted percept)
        {
            var state = UpdateState(percept);

            Problem = FormulateProblem(state);

            if (Problem.GoalTest(state))
            {
                return default;
            }

            if (!Untried.ContainsKey(state))
            {
                Untried[state] = new Stack<TAction>(Problem.Actions(state));
            }

            if (Previous.State != null)
            {
                Result[Previous] = state;

                if (Unbacktracked[state] == null)
                {
                    Unbacktracked[state] = new Stack<TState>();
                }

                Unbacktracked[state].Push(Previous.State);
            }

            if (Untried[state] == null || Untried[state].Count == 0)
            {
                if (Unbacktracked[state] == null || Unbacktracked[state].Count == 0)
                {
                    return default;
                }
                else
                {
                    var ustate = Unbacktracked[state].Pop();

                    var action = Result
                        .Where(x =>
                            sEqComp.Equals(x.Key.State, state) && sEqComp.Equals(ustate, x.Value))
                        .Select(x => x.Key.Action).FirstOrDefault();

                    Previous = (Previous.State, action);
                }
            }
            else
            {
                Previous = (Previous.State, Untried[state].Pop());
            }

            Previous = (state, Previous.Action);

            return Previous.Action;
        }
    }
}
