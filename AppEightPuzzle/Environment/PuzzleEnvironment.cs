namespace AppEightPuzzle.Environment
{
    public class PuzzleEnvironment<T>
    {
        public PuzzleEnvironment(PuzzleBoard<T> initialState)
        {
            State = initialState;
        }

        public PuzzleBoard<T> State { get; private set; }
    }
}
