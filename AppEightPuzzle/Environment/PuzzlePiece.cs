namespace AppEightPuzzle.Environment
{
    public class PuzzlePiece<T>
    {
        public T Value { get; set; }

        public static PuzzlePiece<T> Empty => new PuzzlePiece<T> { Value = default };
    }
}
