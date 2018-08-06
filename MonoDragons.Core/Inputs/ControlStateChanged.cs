
namespace MonoDragons.Core.Inputs
{
    public struct ControlStateChanged
    {
        public Control Control { get; }
        public ControlState State { get; }

        public ControlStateChanged(Control control, ControlState state)
        {
            Control = control;
            State = state;
        }
    }
}
