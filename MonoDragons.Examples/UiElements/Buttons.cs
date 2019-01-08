using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Examples
{
    public static class Buttons
    {
        public static TextButton Text(string text, Point position, Action action) => Text(text, position, action, () => true);
        public static TextButton Text(string text, Point position, Action action, Func<bool> isVisible)
        {
            return new TextButton(
                new Rectangle(position, new Point(300, 50)),
                action, 
                text,
                Color.FromNonPremultiplied(208, 42, 208, 160),
                Color.FromNonPremultiplied(208, 42, 208, 100),
                Color.FromNonPremultiplied(208, 42, 208, 40),
                isVisible
            );
        }
    }
}
