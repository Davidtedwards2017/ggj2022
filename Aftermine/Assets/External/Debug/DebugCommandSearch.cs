using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Text.RegularExpressions;

namespace gamedev.utilities.debug
{
    public class DebugCommandSearch : MonoBehaviour
    {
        public TMP_Text HelpMenuText;
        public DebugCommandCollection Commands;

        public void UpdateSearch(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                ClearText();
            }
            else
            {
                UpdateText(text, GetCommandsStartingWith(text));
            }
        }

        private List<IDebugCommand> GetCommandsStartingWith(string text)
        {
            var matches = new List<IDebugCommand>();

            matches = Commands.Collection.Where(entry => entry.CommandId.StartsWith(text))
                .OrderBy(entry => entry.CommandId)
                .Cast<IDebugCommand>()
                .ToList();

            return matches;
        }

        private void ClearText()
        {
            HelpMenuText.text = string.Empty;
        }

        private void UpdateText(string searchText, List<IDebugCommand> commands)
        {
            var text = string.Empty;

            foreach (var command in commands)
            {
                text += AppendCommand(command);
            }

            Regex regex = new Regex("(" + searchText + ")");
            string cleanString = regex.Replace(text, "<b>$1</b>");

            HelpMenuText.text = text;
        }

        private string AppendCommand(IDebugCommand command)
        {
            return string.Format("{0} {2} <i>[{1}]</i><br>", command.CommandId, command.CommandDescription, command.CommandFormat);
        }
    }
}