namespace ProblemSolving
{
    public interface IProblemDefinition<TState, TAction>
    {
        TState InitialState { get; set; }

        TState GoalState { get; set; }

        TAction[] Actions(TState state);

        bool GoalTest(TState state);

        double StepCost(TState state, TAction action);

        TState TransitionModel(TState state, TAction action);
    }
}
