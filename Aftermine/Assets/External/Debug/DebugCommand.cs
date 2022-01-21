using System;

namespace gamedev.utilities.debug
{
    public class DebugCommand : DebugCommandBase
    {
        private Action command;
        
        public DebugCommand(string id, string description, string format, Action command) : base (id, description, format)
        {
            this.command = command;
        }

        public void Invoke()
        {
            command.Invoke();
        }
        
    }
}