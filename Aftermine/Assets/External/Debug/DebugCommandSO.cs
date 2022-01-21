using UnityEngine;
using System.Text.RegularExpressions;

namespace gamedev.utilities.debug
{
    [CreateAssetMenu(menuName = "Utilities/Debug/Channel Debug Command")]
    public abstract class DebugCommandSO : ScriptableObject, IDebugCommand
    {
        public CommandInfo commandInfo = new CommandInfo();
        public string CommandId => commandInfo.Id;
        public string CommandDescription => commandInfo.Description;
        public string CommandFormat => commandInfo.Format;

        public abstract void Invoke(params object[] arguments);

        private void OnValidate()
        {
            commandInfo.Id = Regex.Replace(commandInfo.Id, @"\s+", "_"); 
        }

        [System.Serializable]
        public class CommandInfo 
        {
            public string Id;
            public string Description;
            public string Format;
        }
    }
}