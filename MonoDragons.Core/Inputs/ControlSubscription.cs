using System;

namespace MonoDragons.Core.Inputs
{
    public class ControlSubscription : ISubscription<ControlStateChanged>
    {
        private readonly Control _control;
        private readonly Action _onActive;
        private readonly Action _onInactive;

        public ControlSubscription(Control control, Action onActive) 
            : this (control, onActive, () => { }) { }

        public ControlSubscription(Control control, Action onActive, Action onInactive)
        {
            _control = control;
            _onActive = onActive;
            _onInactive = onInactive;
        }

        public void Update(ControlStateChanged stateChanged)
        {
            if (!stateChanged.Control.Equals(_control))
                return;
            if (stateChanged.State.Equals(ControlState.Active))
                _onActive();
            if (stateChanged.State.Equals(ControlState.Inactive))
                _onInactive();
        }
    }
}
