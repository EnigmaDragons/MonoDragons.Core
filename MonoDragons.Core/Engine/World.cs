using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace MonoDragons.Core.Engine
{
    public static class World
    {
        private static Game _game;
        private static ContentManager _content;
        private static SpriteBatch _spriteBatch;
        private static INavigation _navigation;

        private static SceneContents _sceneContents = new SceneContents();
        private static SceneContents _oldSceneContents = new SceneContents();

        public static void Init(Game game, INavigation navigation, SpriteBatch spriteBatch)
        {
            _game = game;
            _content = game.Content;
            _navigation = navigation;
            _spriteBatch = spriteBatch;
        }

        public static void PlaySound(string soundName)
        {
            Load<SoundEffect>(soundName).Play();
        }

        public static void PlayMusic(string songName)
        {
            MediaPlayer.Stop();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Load<Song>(songName));
        }

        private static T Load<T>(string resourceName)
        {
            var resource = _content.Load<T>(resourceName);
            _sceneContents.Add((IDisposable)resource);
            return resource;
        }

        public static void NavigateToScene(string sceneView)
        {
            _oldSceneContents = _sceneContents;
            _oldSceneContents.Dispose();
        }

        public static void Draw(Texture2D texture, Vector2 pixelPosition)
        {
            _spriteBatch.Draw(texture, pixelPosition);
        }

        public static void Draw(Texture2D texture, Rectangle rectPostion)
        {
            _spriteBatch.Draw(texture, rectPostion, Color.White);
        }
    }
}
