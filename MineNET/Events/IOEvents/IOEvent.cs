using System;

namespace MineNET.Events.IOEvents
{
    public sealed class IOEvent
    {
        public event EventHandler<InputActionEventArgs> InputAction;
        public void OnInputAction(object sender, InputActionEventArgs e)
        {
            this.InputAction?.Invoke(sender, e);
        }
    }
}
