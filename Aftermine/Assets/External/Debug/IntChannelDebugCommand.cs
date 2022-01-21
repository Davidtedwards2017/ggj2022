using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gamedev.utilities.events;

namespace gamedev.utilities.debug
{

    [CreateAssetMenu(menuName = "Utilities/Debug/Int Channel Debug Command")]
    public class IntChannelDebugCommand : DebugCommandSO
    {
        public IntChannel channel;

        public override void Invoke(params object[] arguments)
        {
            if (arguments.Length < 0) 
            {
                Debug.LogError(string.Format("Command {0} missing argument", commandInfo.Id));
                return;
            }

            int value;
            if (!int.TryParse((string) arguments[0], out value))
            {
                Debug.LogError(string.Format("Command {0} invalid argument: {1}", commandInfo.Id, arguments[0]));
                return;
            }

            channel.Set(value);
        }
    }
}