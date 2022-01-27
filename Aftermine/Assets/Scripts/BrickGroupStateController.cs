using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGroupStateController : MonoBehaviour
{
    public enum State 
    {
        Jiggle,
        Moving,
        Stopped,
        Sliding
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
    public GlobalPropertiesSO globalProperties;

    [ReadOnly]
    public IState Current;

    private MovingState Moving;
    private StoppedState Stopped;
    private SlidingState Sliding;
    private JiggleState Jiggle;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Moving = new MovingState(group);
        Stopped = new StoppedState(group);
        Sliding = new SlidingState(group);
        Jiggle = new JiggleState(group);
    }

    public void StartMovement()
    {
        ChangeState(State.Moving);
    }

    public void StopMovement()
    {
        ChangeState(State.Stopped);
    }

    public void StartSliding(BrickSlidingEventArgs slidingArgs)
    {
        Sliding.slidingArgs = slidingArgs;
        ChangeState(State.Sliding);
    }

    public bool IsStopped()
    {
        return Current == Stopped;
    }

    public bool IsSliding()
    {
        return Current == Sliding;
    }

    public bool IsMoving()
    {
        return Current == Moving;
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
            case State.Sliding:
                Current = Sliding;
                break;
            case State.Jiggle:
                Current = Jiggle;
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
    public class JiggleState : IState
    {
        public State State { get { return State.Jiggle; } }
        private BrickGroup group;

        private float duration;

        public JiggleState(BrickGroup group)
        {
            this.group = group;
        }

        public void OnEnter()
        {
            duration = group.difficulty.BrickJiggleDuration;
            foreach (var brick in group.Bricks)
            {
                brick.Jiggle.StartJiggle();
            }
        }

        public void OnExit()
        {
            foreach (var brick in group.Bricks)
            {
                brick.Jiggle.StopJiggle();
            }
        }

        public void Update()
        {
            duration -= Time.deltaTime;
            if (duration <=0)
            {
                group.RequestMovement();
            }
        }

        public void LateUpdate()
        {
        }
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
        }

        public void OnExit()
        {
        }

        public void Update()
        {
            foreach (var brick in group.Bricks)
            {
                if (!brick.groundCheck.Grounded)
                {
                    group.RequestMovement();
                    return;
                }
            }
        }

        public void LateUpdate()
        {

        }
    }

    public class SlidingState : IState
    {
        public State State { get { return State.Sliding; } }
        private BrickGroup group;

        public BrickSlidingEventArgs slidingArgs;

        public SlidingState(BrickGroup group)
        {
            this.group = group;
        }

        public void OnEnter()
        {
            Debug.Log(group + " Entered state: Sliding");

            var slidingGroup = slidingArgs.SlideGroup;
            foreach (var brick in group.Bricks)
            {
                brick.slider.StartSliding(slidingGroup);
            }
        }

        public void OnExit()
        {
            foreach (var brick in group.Bricks)
            {
                brick.slider.StopSlide();
            }
        }

        public void Update()
        {

        }

        public void LateUpdate()
        {
            foreach (var brick in group.Bricks)
            {
                brick.slider.UpdateMovement();
            }
        }
    }
}
