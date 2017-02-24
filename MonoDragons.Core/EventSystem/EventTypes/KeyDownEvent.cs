using Microsoft.Xna.Framework.Input;

namespace MonoDragons.Core.EventSystem.EventTypes
{
    public class DownKeyEvent
    {
        public Keys Key { get; }

        public DownKeyEvent(Keys key)
        {
            Key = key;
        }
    }
}
