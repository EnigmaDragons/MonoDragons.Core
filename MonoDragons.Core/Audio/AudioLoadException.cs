using System;

namespace MonoDragons.Core.AudioSystem
{
    public sealed class AudioLoadException : Exception
    {
        public AudioLoadException(string message, Exception e) : base(message, e) { }
    }
}