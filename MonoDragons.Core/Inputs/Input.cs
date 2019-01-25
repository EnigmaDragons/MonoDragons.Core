using System;
using MonoDragons.Core.EventSystem;

namespace MonoDragons.Core.Inputs
{
    public static class Input
    {
        private static readonly MustInit<IController> Default = new MustInit<IController>("Default Controller");

        public static void SetDefaultController(IController controller) => Default.Set(controller);

        public static void On(Control control, Action onPress) => On(Default.Get(), control, onPress, () => { });
        public static void On(IController controller, Control control, Action onPress) => On(controller, control, onPress, () => { });
        public static void On(Control control, Action onPress, Action onRelease) => On(Default.Get(), control, onPress, onRelease);
        public static void On(IController controller, Control control, Action onPress, Action onRelease)
        {            
            Event.Subscribe<ControlStateChanged>(c =>
            {
                if (c.ControllerId != controller.Id)
                    return;
                if (c.Control != control)
                    return;
                if (c.State == ControlState.Active)
                    onPress();
                else if (c.State == ControlState.Inactive)
                    onRelease();
            }, controller);
        }

        public static void OnDirection(Action<Direction> onDirection) => OnDirection(Default.Get(), onDirection);
        public static void OnDirection(IController controller, Action<Direction> onDirection)
        {
            Event.Subscribe<DirectionChanged>(d =>
            {
                if (d.ControllerId != controller.Id)
                    return;
                onDirection(d.Current);
            }, controller);
        }

        public static void ClearTransientBindings()
        {
            Event.Unsubscribe(Default);
        }
    }
}
