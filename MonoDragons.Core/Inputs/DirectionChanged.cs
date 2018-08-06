namespace MonoDragons.Core.Inputs
{
    public struct DirectionChanged
    {
        public Direction Previous { get; }
        public Direction Current { get; }

        public DirectionChanged(Direction previous, Direction current)
        {
            Previous = previous;
            Current = current;
        }
    }
}