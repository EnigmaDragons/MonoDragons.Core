using System;

namespace MonoDragons.Core.Engine
{
    public interface IScene
    {
        void LoadContent();
        void UnloadContent();
        void Update(TimeSpan delta);
        void Draw();
    }
}
