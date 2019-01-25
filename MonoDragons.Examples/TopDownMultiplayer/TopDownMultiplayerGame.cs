using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Physics;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Examples.TopDownMultiplayer
{
    public sealed class TopDownMultiplayerGame : SimpleScene
    {
        public override void Init()
        {
            Input.SetDefaultController(new KeyboardController(new Map<Keys, Control>()));
            var c1 = new KeyboardController(
                new Map<Keys, HorizontalDirection>
                {
                    {Keys.A, HorizontalDirection.Left},
                    {Keys.D, HorizontalDirection.Right}
                },
                new Map<Keys, VerticalDirection>
                {
                    {Keys.W, VerticalDirection.Up},
                    {Keys.S, VerticalDirection.Down}
                },
                new Map<Keys, Control>());
            
            var c2 = new KeyboardController(
                new Map<Keys, HorizontalDirection>
                {
                    {Keys.NumPad4, HorizontalDirection.Left},
                    {Keys.NumPad6, HorizontalDirection.Right}
                },
                new Map<Keys, VerticalDirection>
                {
                    {Keys.NumPad8, VerticalDirection.Up},
                    {Keys.NumPad5, VerticalDirection.Down}
                },
                new Map<Keys, Control>());
            Add(c1);
            Add(c2);
            const int width = 20;
            const int height = 12;
            Add(new TopDownMap(width, height));
            
            var char1 = new TopDownCharacter().UseController(c1).SpawnAt(new Point(3, 3));
            var char2 = new TopDownCharacter().UseController(c2).SpawnAt(new Point(8, 8));
            Add(char1);
            Add(char2);
            Add(new PhysicsWorld(TopDownWorld.XTileSize * width, TopDownWorld.YTileSize * height).Add(char1).Add(char2));
        }
    }
}
