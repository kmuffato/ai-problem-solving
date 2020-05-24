using ProblemSolving;
using System.Collections.Generic;

namespace SearchingApp.Problems.EightPuzzle
{
    public class ProblemDefinition<T> : IProblemDefinition<ProblemState<T>, ProblemAction>
    {
        public ProblemState<T> InitialState { get; set; }

        public ProblemState<T> GoalState { get; set; }

        public ProblemAction[] Actions(ProblemState<T> state)
        {
            var (rowNumber, columnNumber) = state.FindEmpty();

            var actions = new List<ProblemAction>();

            if (columnNumber > 0)
            {
                actions.Add(ProblemAction.Left);
            }

            if (columnNumber < state.GetLength(0) - 1)
            {
                actions.Add(ProblemAction.Right);
            }

            if (rowNumber > 0)
            {
                actions.Add(ProblemAction.Up);
            }

            if (rowNumber < state.GetLength(1) - 1)
            {
                actions.Add(ProblemAction.Down);
            }

            return actions.ToArray();
        }

        public bool GoalTest(ProblemState<T> state)
        {
            return GoalState.StateEquals(state);
        }

        public double StepCost(ProblemState<T> state, ProblemAction action)
        {
            return 1;
        }

        public ProblemState<T> TransitionModel(ProblemState<T> state, ProblemAction action)
        {
            var (rowNumber, columnNumber) = state.FindEmpty();

            var transitionState = new ProblemState<T>(state.GetLength(0), state.GetLength(1));

            ProblemState<T>.Copy(state, transitionState, state.Length);

            int row = rowNumber, col = columnNumber;

            if (action == ProblemAction.Up)
            {
                row = rowNumber - 1;
            }

            if (action == ProblemAction.Down)
            {
                row = rowNumber + 1;
            }

            if (action == ProblemAction.Left)
            {
                col = columnNumber - 1;
            }

            if (action == ProblemAction.Right)
            {
                col = columnNumber + 1;
            }

            transitionState[row, col] = state[rowNumber, columnNumber];

            transitionState[rowNumber, columnNumber] = state[row, col];

            return transitionState;
        }
    }
}
