using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    [ReadOnly]
    public StateDefintion current;
    public List<StateDefintion> states;

    public void SetState(State state)
    {
        if (current != null && current.State == state)
        {
            return;
        }

        if (current != null)
        {
            current.Exit();
        }

        current = states.FirstOrDefault(s => s.State == state);

        if (current != null)
        {
            current.Enter();
        }
    }
}
