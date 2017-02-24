using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using System;

namespace MonoDragons.MazeWalkers.Scenes
{
    public sealed class MainMenuScene : IScene
    {
        private INavigation _navigation;
        private bool _enterKeyPressed;

        public void Draw()
        {
            World.DrawBrackgroundColor(Color.Black);
            World.Draw("Images/Background/Maze", new Vector2(58, 0));
            World.DrawText("MazeWalker", new Vector2(610, 320), Color.White);
            World.DrawText("Press ENTER to begin", new Vector2(570, 380), Color.White);
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(TimeSpan delta)
        {
            var ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Enter) && !_enterKeyPressed) { }
            _enterKeyPressed = ks.IsKeyDown(Keys.Enter);
        }

        public void SetNavigaton(INavigation navigation)
        {
            _navigation = navigation;
        }
    }

    //public class LoadedTexture
    //{
    //    private readonly string _textureName;
    //    private const string ImageFolder = "Images/";

    //    public LoadedTexture(string textureName)
    //    {
    //        _textureName = textureName;
    //    }

    //    public Texture2D Get()
    //    {
    //        return new GameInstance().Load<Texture2D>(ImageFolder + _textureName);
    //    }
    //}

    //public class DrawTextOnScreen
    //{
    //    private readonly string text;
    //    private readonly Vector2 location;
    //    private readonly Color color;

    //    public DrawTextOnScreen(string text, Vector2 screenLocation)
    //        : this(text, screenLocation, Color.Black) { }

    //    public DrawTextOnScreen(string text, Vector2 screenLocation, Color color)
    //    {
    //        this.text = text;
    //        this.location = screenLocation;
    //        this.color = color;
    //    }

    //    public void Go()
    //    {
    //        new SpritesBatchInstance().DrawText(text, location, color);
    //    }
    //}
}
