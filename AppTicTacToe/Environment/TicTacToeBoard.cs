namespace AppTicTacToe.Environment
{
    public class TicTacToeBoard
    {
        public TicTacToeBoard()
        {
            BoardPieces = new TicTacToePiece[3, 3];
        }

        public TicTacToePiece[,] BoardPieces { get; set; }
    }
}
