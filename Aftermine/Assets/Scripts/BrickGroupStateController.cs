using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGroupStateController : MonoBehaviour
{
    public enum State 
    {
        Moving,
        Stopped,
    }

    public interface IState
    {
        State State { get;}
        void Update();
        void LateUpdate();
        void OnEnter();
        void OnExit();
    }

    public BrickGroup group;

    [ReadOnly]
    public IState Current;

    private MovingState Moving;
    private StoppedState Stopped;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Moving = new MovingState(group);
        Stopped = new StoppedState(group);
    }

    public void StopMovement()
    {
        ChangeState(State.Stopped);
    }

    public void ChangeState(State state)
    {
        if (Current != null && Current.State == state) return;

        if (Current != null) Current.OnExit();

        switch (state) 
        {
            case State.Moving:
                Current = Moving;
                break;
            case State.Stopped:
                Current = Stopped;
                break;
        }

        Current.OnEnter();

    }

    private void Update()
    {
        if (Current != null) Current.Update();
    }

    private void LateUpdate()
    {
        if (Current != null) Current.LateUpdate();
    }


    [System.Serializable]
    public class MovingState : IState
    {
        public State State { get { return State.Moving; } }
        private BrickGroup group;

        public MovingState(BrickGroup group)
        {
            this.group = group;
        }

        public void OnEnter()
        {
            Debug.Log(group + " Entered state: Moving");
        }

        public void OnExit()
        {
            foreach (var brick in group.Bricks)
            {
                brick.mover.StopMovement();
            }
        }

        public void Update()
        {

        }

        public void LateUpdate()
        {
            foreach (var brick in group.Bricks)
            {
                brick.mover.UpdateMovement();
            }
        }
    }

    [System.Serializable]
    public class StoppedState : IState
    {
        public State State { get { return State.Stopped; } }
        private BrickGroup group;

        public StoppedState(BrickGroup group)
        {
            this.group = group;
        }

        public void OnEnter()
        {
            Debug.Log(group + " Entered state: Stopped");
        }

        public void OnExit()
        {
        }

        public void Update()
        {

        }

        public void LateUpdate()
        {
        }
    }
}
