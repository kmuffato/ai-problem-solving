using AppTicTacToe.Environment;
using AppTicTacToe.Problem;
using ProblemSolving.ByAdversarial.Strategy;
using System;

namespace AppTicTacToe
{
    public class Program
    {
        public static void Main()
        {
            var board = new TicTacToeBoard();

            var minimax = new MinimaxDecision<ProblemState, ProblemAction>();

            var alphaBeta = new AlphaBetaPruning<ProblemState, ProblemAction>();

            var agent = new ProblemAgent(alphaBeta);

            ProblemAction action;

            while ((action = agent.Work(board)) != default)
            {
                var player = agent.Problem.Player(agent.State);

                if (player == ProblemDefinition.PlayerX)
                {
                    Console.WriteLine($"Enter Positions (e.g. {action.PutPosition.x} {action.PutPosition.y}): ");
                    action = ProblemStateExtensions.FromInput(player, Console.ReadLine());
                }

                board = agent.Problem.Result(agent.State, action).FromProblemState();

                WriteBoard(board);
            }
        }

        private static void WriteBoard(TicTacToeBoard board)
        {
            var cc = Console.ForegroundColor;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.ForegroundColor = board.BoardPieces[i, j] != null
                            && board.BoardPieces[i, j].Value == TicTacToeValue.Cross
                        ? ConsoleColor.Red
                        : ConsoleColor.Blue;

                    Console.Write("{0}",
                        board.BoardPieces[i, j] == null
                            ? " "
                            : board.BoardPieces[i, j].Value == TicTacToeValue.Cross ? "X" : "O");

                    Console.ForegroundColor = cc;

                    if (j != 2)
                    {
                        Console.Write(" | ");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
                if (i != 2)
                {
                    Console.WriteLine("---------");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
