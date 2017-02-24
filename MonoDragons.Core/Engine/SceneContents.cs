using System;
using System.Collections.Generic;

namespace MonoDragons.Core.Engine
{
    public class SceneContents : IDisposable
    {
        private readonly List<IDisposable> _loadedContents = new List<IDisposable>();

        public void Add(IDisposable content)
        {
            _loadedContents.Add(content);
        }

        public void Dispose()
        {
            _loadedContents.ForEach(x => x.Dispose());
            _loadedContents.Clear();
        }
    }
}
