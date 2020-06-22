namespace ProblemSolving.BySearching
{
    public interface ISearchProblem<TState, TAction>
    {
        TState InitialState { get; set; }

        TState GoalState { get; set; }

        TAction[] Actions(TState state);

        bool GoalTest(TState state);

        double StepCost(TState state, TAction action);

        TState Result(TState state, TAction action);
    }
}
