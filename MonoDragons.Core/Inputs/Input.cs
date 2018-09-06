using System;
using MonoDragons.Core.EventSystem;

namespace MonoDragons.Core.Inputs
{
    public static class Input
    {
        private static IController _controller;

        public static void SetController(IController controller)
        {
            _controller = controller;
        }

        public static void On(Control control, Action onPress)
        {
            On(control, onPress, () => { });
        }

        public static void On(Control control, Action onPress, Action onRelease)
        {            
            Event.Subscribe<ControlStateChanged>(c =>
            {
                if (!c.Control.Equals(control))
                    return;
                if (c.State.Equals(ControlState.Active))
                    onPress();
                else if (c.State.Equals(ControlState.Inactive))
                    onRelease();
            }, _controller);
        }

        public static void OnDirection(Action<Direction> onDirection)
        {
            Event.Subscribe<DirectionChanged>(d => onDirection(d.Current), _controller);
        }

        public static void ClearTransientBindings()
        {
            Event.Unsubscribe(_controller);
        }
    }
}
