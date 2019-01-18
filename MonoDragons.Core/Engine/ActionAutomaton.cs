using System;

namespace MonoDragons.Core.Engine
{
    public sealed class ActionAutomaton : IAutomaton
    {
        private readonly Action<TimeSpan> _action;

        public ActionAutomaton(Action action) : this(x => action()) {}
        public ActionAutomaton(Action<TimeSpan> action)
        {
            _action = action;
        }

        public void Update(TimeSpan delta)
        {
            _action(delta);
        }
    }
}