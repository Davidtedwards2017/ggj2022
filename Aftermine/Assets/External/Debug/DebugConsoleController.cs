using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.Events;

namespace gamedev.utilities.debug
{
    public class DebugConsoleController : MonoBehaviour
    {
        private bool _ShowConsole = false;
        private GUIStyle guiStyle = new GUIStyle();

        public DebugCommandCollection commands;

        public Animator Animator;
        public TMP_InputField InputField;

        public UnityEvent OnConsoleOpen;
        public UnityEvent OnConsoleClose;

        private void Awake()
        {
            _ShowConsole = false;
            Animator.SetBool("visible", _ShowConsole);

            InputField.onSubmit.AddListener(HandleInput);
        }

        public void ToggleConsole()
        {
            _ShowConsole = !_ShowConsole;
            Animator.SetBool("visible", _ShowConsole);

            if (_ShowConsole)
            {
                OnConsoleOpen?.Invoke();
                StartCoroutine(SelectInputField());
            }
            else
            {
                OnConsoleClose?.Invoke();
            }
        }

        IEnumerator SelectInputField()
        {
            yield return new WaitForEndOfFrame();
            InputField.ActivateInputField();
            InputField.Select();
        }


        public void HandleInput(string input)
        {
            var tokens = input.Split(' ', 2);
            if (tokens.Length == 0) return;

            string commandText = tokens[0];
            var args = (tokens.Length == 2) ?  tokens[1].Split(' ') : new string[] {};
            
            var command =  commands.Collection.FirstOrDefault(e => e.CommandId.Equals(commandText, 
            System.StringComparison.CurrentCultureIgnoreCase));

            if (command != null)
            {
                command.Invoke(args);
            } 
            else
            {
                Debug.LogError(string.Format("Unknown command: {0}", commandText));
            }

            InputField.text = string.Empty;

        }
        
    }
}
