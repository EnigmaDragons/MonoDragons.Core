using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;

namespace MonoDragons.MazeWalkers.EcsScenes
{
    public class TestScene : IScene
    {
        public void Init()
        {
            Entity.Create()
                .Add(new ScreenBackgroundColor {Color = Color.Blue});
            for (var x = 0; x < 1632; x += 32)
                for(var y = 0; y < 932; y += 32)
                    Entity.Create()
                        .Add(new Spatial2(new Transform2(new Vector2(x, y), new Size2(32, 32))))
                        .Add(new Sprite("Images/Tiles/tile1"));
            Entity.Create()
                .Add(new Spatial2(new Transform2(new Vector2(100, 100), new Size2(70, 70))))
                .Add(new Motion2(new Velocity2 { Speed = 0.05f, Direction = Rotation2.DownRight }))
                .Add(new Sprite("Images/Crono Nigga/Walk/down1(new).png"));
        }

        public void Update(TimeSpan delta) { }
        public void Draw() { }
    }
}
