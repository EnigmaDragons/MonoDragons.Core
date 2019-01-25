namespace MonoDragons.Core.Inputs
{
    public struct DirectionChanged
    {
        public int ControllerId { get; }
        public Direction Previous { get; }
        public Direction Current { get; }

        public DirectionChanged(int controllerId, Direction previous, Direction current)
        {
            ControllerId = controllerId;
            Previous = previous;
            Current = current;
        }
    }
}
