using AppTicTacToe.Environment;
using ProblemSolving.ByAdversarial;
using ProblemSolving.ByAdversarial.Agent;

namespace AppTicTacToe.Problem
{
    public class ProblemAgent : SimpleProblemSolvingAgentBase<ProblemState, ProblemAction>
    {
        public ProblemAgent(IAdversarialSearchStrategy<ProblemState, ProblemAction> searchStrategy) 
            : base(searchStrategy)
        {
        }

        protected override IAdversarialSearchProblem<ProblemState, ProblemAction> FormulateProblem(ProblemState state)
        {
            return new ProblemDefinition
            {
                InitialState = state
            };
        }

        protected override ProblemState UpdateState<TPercepted>(ProblemState state, TPercepted percept)
        {
            return (percept as TicTacToeBoard).FromBoard();
        }
    }
}
