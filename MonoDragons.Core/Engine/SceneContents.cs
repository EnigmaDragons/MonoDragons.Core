using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using MonoDragons.Core.Common;

namespace MonoDragons.Core.Engine
{
    public class SceneContents : IDisposable
    {
        private readonly Dictionary<string, IDisposable> _loadedContents = new Dictionary<string, IDisposable>();
        private readonly ContentManager _contentManager;

        public SceneContents(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public void Add(string resourceName, IDisposable content)
        {
            _loadedContents.Add(resourceName, content);
        }

        public T Load<T>(string resourceName) where T : IDisposable
        {
            if (!_loadedContents.ContainsKey(resourceName))
                _loadedContents.Add(resourceName, _contentManager.Load<T>(resourceName));
            return (T)_loadedContents[resourceName];
        }

        public void Dispose()
        {
            _loadedContents.Values.ForEach(x => x.Dispose());
            _loadedContents.Clear();
        }
    }
}
