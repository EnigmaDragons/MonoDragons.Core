using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Text;
using MonoDragons.Core.UserInterface;
using System;
using MonoDragons.Core.Render;

namespace MonoDragons.Core.Engine
{
    public static class World
    {
        private static readonly ColoredRectangle _darken = new ColoredRectangle
        {
            Color = Color.FromNonPremultiplied(0, 0, 0, 130),
            Transform = new Transform2(new Size2(1920, 1080))
        };
        
        private static SpriteBatch _spriteBatch;
        private static Display _display;

        public static void Init(SpriteBatch spriteBatch, Display display)
        {
            _spriteBatch = spriteBatch;
            _display = display;
            DefaultFont.Load(GameInstance.ContentManager);
        }

        public static void Draw(Texture2D texture, Rectangle rectangle, Color color)
        {
            _spriteBatch.Draw(texture, ScaleRectangle(rectangle), color);
        }

        public static void DrawBackgroundColor(Color color)
        {
            GameInstance.GraphicsDevice.Clear(color);
        }

        public static void Draw(string imageName, Vector2 pixelPosition)
        {
            var resource = Resources.Load<Texture2D>(imageName);
            _spriteBatch.Draw(resource, new Rectangle(ScalePoint(pixelPosition), ScalePoint(resource.Width, resource.Height)), Color.White);
        }

        public static void Draw(string imageName, Rectangle rectPostion)
        {
            _spriteBatch.Draw(Resources.Load<Texture2D>(imageName), ScaleRectangle(rectPostion), Color.White);
        }

        public static void Draw(Texture2D texture, Rectangle rectPosition)
        {
            Resources.Put(texture.GetHashCode().ToString(), texture);
            _spriteBatch.Draw(texture, ScaleRectangle(rectPosition), Color.White);
        }

        public static void DrawRotatedFromCenter(Texture2D texture, Rectangle rectPosition, Rotation2 rotation)
        {
            Resources.Put(texture.GetHashCode().ToString(), texture);
            var scaledRect = ScaleRectangle(rectPosition);
            _spriteBatch.Draw(texture, null, scaledRect, null, new Vector2(scaledRect.Width / 2, scaledRect.Height / 2),
                rotation.Value * .017453292519f, new Vector2(1, 1));
        }

        public static void Draw(Texture2D texture, Vector2 position)
        {
            _spriteBatch.Draw(texture, new Rectangle(ScalePoint(position), ScalePoint(texture.Width, texture.Height)), Color.White);
        }

        public static void Draw(string name, Transform2 transform)
        {
            Draw(name, transform.ToRectangle());
        }

        public static void DrawRotatedFromCenter(string name, Transform2 transform)
        {
            var resource = Resources.Load<Texture2D>(name);
            var x = transform.Rotation.Value;
            _spriteBatch.Draw(resource, null, ScaleRectangle(transform.ToRectangle()), null, new Vector2(resource.Width / 2, resource.Height / 2),
                transform.Rotation.Value * .017453292519f, new Vector2(1, 1));
        }

        public static void Draw(Texture2D texture, Transform2 transform)
        {
            Draw(texture, transform.ToRectangle());
        }

        public static void Darken()
        {
            _darken.Draw(Transform2.Zero);
        }

        private static Rectangle ScaleRectangle(Rectangle rectangle)
        {
            return new Rectangle(ScalePoint(rectangle.Location), ScalePoint(rectangle.Size));
        }

        private static Point ScalePoint(float x, float y)
        {
            return ScalePoint(new Vector2(x, y));
        }

        private static Point ScalePoint(Vector2 vector)
        {
            return new Point((int)Math.Round(vector.X * _display.Scale), (int)Math.Round(vector.Y * _display.Scale));
        }

        private static Point ScalePoint(Point point)
        {
            return ScalePoint(point.ToVector2());
        }
    }
}
