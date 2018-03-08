using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Render
{
    public static class CurrentDisplay
    {
        private static Display _display;

        internal static void Init(Display display)
        {
            _display = display;
        }

        public static Display Get()
        {
            return _display;
        }

        public static Rectangle FullScreenRectangle => new Rectangle(0, 0, _display.ProgramWidth, _display.ProgramHeight);
    }
}
