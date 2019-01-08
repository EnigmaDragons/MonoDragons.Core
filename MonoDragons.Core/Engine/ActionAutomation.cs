using System;

namespace MonoDragons.Core.Engine
{
    public sealed class ActionAutomation : IAutomaton
    {
        private readonly Action<TimeSpan> _action;

        public ActionAutomation(Action action) : this(x => action()) {}
        public ActionAutomation(Action<TimeSpan> action)
        {
            _action = action;
        }

        public void Update(TimeSpan delta)
        {
            _action(delta);
        }
    }
}