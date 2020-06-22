using AppTicTacToe.Environment;

namespace AppTicTacToe.Problem
{
    public static class ProblemStateExtensions
    {
        public static ProblemState FromBoard(this TicTacToeBoard board)
        {
            var state = new ProblemState
            {
                XorO = new char?[3, 3]
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    state.XorO[i, j] = board.BoardPieces[i, j] != null
                        ? board.BoardPieces[i, j].Value == TicTacToeValue.Cross
                            ? (char)ProblemDefinition.PlayerX
                            : (char)ProblemDefinition.PlayerO
                        : new char?();
                }
            }

            return state;
        }

        public static TicTacToeBoard FromProblemState(this ProblemState state)
        {
            var board = new TicTacToeBoard();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board.BoardPieces[i, j] = state.XorO[i, j].HasValue
                        ? new TicTacToePiece()
                        {
                            Value = state.XorO[i, j].Value == ProblemDefinition.PlayerX
                                ? new TicTacToeValue?(TicTacToeValue.Cross)
                                : new TicTacToeValue?(TicTacToeValue.Nought)
                        }
                        : null;
                }
            }

            return board;
        }

        public static ProblemAction FromInput(int? player, string positionXY)
        {
            return new ProblemAction
            {
                PutPosition = (int.Parse(positionXY.Split(' ')[0]), int.Parse(positionXY.Split(' ')[1])),
                Put = (char)player
            };
        }
    }
}
