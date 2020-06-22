using ProblemSolving.ByAdversarial;
using System;
using System.Collections.Generic;

namespace AppTicTacToe.Problem
{
    public class ProblemDefinition : IAdversarialSearchProblem<ProblemState, ProblemAction>
    {
        public const int PlayerX = 'X';
        public const int PlayerO = 'O';

        public ProblemState InitialState { get; set; }

        public int Player(ProblemState state)
        {
            int x = 0, o = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (state.XorO[i, j].HasValue)
                    {
                        if (state.XorO[i, j].Value == PlayerX)
                        {
                            x++;
                        }
                        else
                        {
                            o++;
                        }
                    }
                }
            }

            return o < x ? PlayerO : PlayerX;
        }

        public ProblemAction[] Actions(ProblemState state)
        {
            if (GetWinner(state).HasValue)
            {
                return new ProblemAction[0];
            }

            var player = Player(state);

            var actions = new List<ProblemAction>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (state.XorO[i, j].HasValue)
                    {
                        continue;
                    }

                    actions.Add(new ProblemAction
                    {
                        PutPosition = (i, j),
                        Put = (char)player
                    });
                }
            }

            return actions.ToArray();
        }

        public ProblemState Result(ProblemState state, ProblemAction action)
        {
            var newState = new char?[3, 3];

            Array.Copy(state.XorO, newState, state.XorO.Length);

            newState[action.PutPosition.x, action.PutPosition.y] = action.Put;

            return new ProblemState
            {
                XorO = newState
            };
        }

        public bool TerminalTest(ProblemState state)
        {
            return Actions(state).Length == 0;
        }

        public double Utility(ProblemState state, int player)
        {
            int? winner = GetWinner(state);

            if (winner.HasValue && winner.Value == player)
            {
                return 10;
            }
            else if (winner.HasValue && winner.Value != player)
            {
                return -10;
            }
            else
            {
                return 0;
            }

            throw new Exception();
        }

        private int? GetWinner(ProblemState state)
        {
            if ((state.XorO[1, 1].HasValue && state.XorO[0, 0] == state.XorO[1, 1] && state.XorO[1, 1] == state.XorO[2, 2])
                ||
                (state.XorO[1, 1].HasValue && state.XorO[2, 0] == state.XorO[1, 1] && state.XorO[1, 1] == state.XorO[0, 2]))
            {
                return state.XorO[1, 1].Value;
            }

            for (int i = 0; i < 3; i++)
            {
                var verticalWin = state.XorO[i, 0].HasValue
                    && state.XorO[i, 0] == state.XorO[i, 1]
                    && state.XorO[i, 1] == state.XorO[i, 2];

                if (verticalWin)
                {
                    return state.XorO[i, 0].Value;
                }

                var horizontalWin = state.XorO[0, i].HasValue
                    && state.XorO[0, i] == state.XorO[1, i]
                    && state.XorO[1, i] == state.XorO[2, i];

                if (horizontalWin)
                {
                    return state.XorO[0, i].Value;
                }
            }

            return null;
        }
    }
}
