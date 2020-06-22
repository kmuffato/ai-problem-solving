namespace ProblemSolving.ByAdversarial
{
    public interface IAdversarialSearchProblem<TState, TAction>
    {
        TState InitialState { get; set; }

        int Player(TState state);

        TAction[] Actions(TState state);

        TState Result(TState state, TAction action);

        bool TerminalTest(TState state);

        double  Utility(TState state, int player);
         

    }
}
