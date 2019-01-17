namespace MonoDragons.Core.Inputs.KeyboardEvents
{
    public sealed class KeyboardCharacterInputted
    {
        public char Character { get; }

        public KeyboardCharacterInputted(char ch) => Character = ch;
    }
}
