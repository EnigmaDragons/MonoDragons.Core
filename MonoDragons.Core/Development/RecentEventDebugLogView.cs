using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Physics;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Development
{
    public sealed class RecentEventDebugLogView : IVisualAutomaton
    {
        private readonly List<string> _lines = new List<string>();
        private readonly Queue<string> _new = new Queue<string>();

        public Vector2 Position { get; set; }
        public int MaxLines { get; set; } = 40;
        public string HideTextPart { get; set; } = "";
        public string FilterStartsWith { get; set; } = "";

        public RecentEventDebugLogView()
        {
            Logger.AddSink(Append);
        }

        public void Clear()
        {
            _lines.Clear();
        }
        
        public void Draw(Transform2 parentTransform)
        {
            for (var i = 0; i < _lines.Count; i++)
                UI.DrawText(_lines[i], new Vector2(Position.X, Position.Y + i * 20), DevText.GhostColor, DevText.Font);
        }

        public void Update(TimeSpan delta)
        {
            while (_new.Count > 0)
            {
                while (_lines.Count >= MaxLines)
                    _lines.RemoveAt(0);
                _lines.Add(_new.Dequeue());
            }
        }

        private void Append(string text)
        {
            if (!string.IsNullOrWhiteSpace(FilterStartsWith) && text.StartsWith(FilterStartsWith))
                return;
                
            var rendered = HideTextPart.Length > 0 ? text.Replace(HideTextPart, "") : text; 
            _new.Enqueue(rendered);
        }
    }
}
