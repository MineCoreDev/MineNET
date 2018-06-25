using System;

namespace MineNET.Events.IOEvents
{
    public sealed class IOEvent
    {
        public event EventHandler<InputActionEventArgs> InputAction;
        internal void OnInputAction(object sender, InputActionEventArgs e)
        {
            this.InputAction?.Invoke(sender, e);
        }

        public event EventHandler<OutputActionEventArgs> OutputAction;
        internal void OnOutputAction(object sender, OutputActionEventArgs e)
        {
            this.OutputAction?.Invoke(sender, e);
        }
    }
}
