using Microsoft.Xna.Framework.Graphics;

namespace MonoDragons.Core.Engine
{
    public interface IGame
    {
        T Load<T>(string resourceName);
        GraphicsDevice Graphics { get; }
    }
}
