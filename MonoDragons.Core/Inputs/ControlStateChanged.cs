
namespace MonoDragons.Core.Inputs
{
    public struct ControlStateChanged
    {
        public int ControllerId { get; }
        public Control Control { get; }
        public ControlState State { get; }

        public ControlStateChanged(int controllerId, Control control, ControlState state)
        {
            ControllerId = controllerId;
            Control = control;
            State = state;
        }
    }
}
