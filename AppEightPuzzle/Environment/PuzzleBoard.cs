namespace AppEightPuzzle.Environment
{
    public class PuzzleBoard<T>
    {
        public PuzzleBoard(PuzzlePiece<T>[,] pieces)
        {
            Pieces = pieces;
        }

        public PuzzlePiece<T>[,] Pieces { get; set; }
    }
}
