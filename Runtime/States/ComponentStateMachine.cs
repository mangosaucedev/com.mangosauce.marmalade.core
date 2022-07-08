using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade
{
    public abstract class ComponentStateMachine<T, TState> : GameSystem<T>, IStateMachine<TState>
        where T : ComponentStateMachine<T, TState>
        where TState : IState<TState>
    {
            
#if UNITY_EDITOR
        [SerializeField] private string currentState;
#endif

        private TState state;
        private List<TState> states = new List<TState>();
        private Dictionary<string, TState> statesByName = new Dictionary<string, TState>();
        private HashSet<Transition<TState>> transitions = new HashSet<Transition<TState>>();
        private HashSet<TransitionFromAny<TState>> transitionsFromAny = new HashSet<TransitionFromAny<TState>>();

        public TState State
        {
            get => state;
            set => state = value;
        }

        public List<TState> States => states;

        public Dictionary<string, TState> StatesByName => statesByName;

        public HashSet<Transition<TState>> Transitions => transitions;

        public HashSet<TransitionFromAny<TState>> TransitionsFromAny => transitionsFromAny;

        public void Update()
        {
            UpdateAllTransitions();
            state?.Update();
#if UNITY_EDITOR
            currentState = (State?.GetType().Name ?? "None") + " | " + (State?.State?.GetType().Name ?? "None");
#endif
        }

        public void FixedUpdate()
        {
            state?.FixedUpdate();
        }

        public TState AddState(TState state, bool goTo = false)
        {
            if (goTo)
                GoToState(state);
            return state;
        }

        public ComponentStateMachine<T, TState> AddStates(TState[] states)
        {
            foreach (TState state in states)
                AddState(state);
            return this;
        }

        public void GoToState(TState state)
        {
            this.state?.End();
            this.state = state;
            this.state?.Start();
            UpdateAllTransitions();
        }

        private void UpdateAllTransitions()
        {
            UpdateTransitions();
            UpdateTransitionsFromAny();
        }

        private void UpdateTransitions()
        {
            foreach (Transition<TState> transition in transitions)
            {
                if (((State == null && transition.state == null) || (State != null && State.Equals(transition.state))) && transition.CanTransition())
                {
                    GoToState(transition.goTo);
                    return;
                } 
            }
        }

        private void UpdateTransitionsFromAny()
        {
            foreach (TransitionFromAny<TState> transition in transitionsFromAny)
            {
                if ((State == null && transition.goTo != null) || (State != null && !(State.Equals(transition.goTo))) && transition.CanTransition())
                {
                    GoToState(transition.goTo);
                    return;
                }
            }
        }

        public ComponentStateMachine<T, TState> AddTransition(Transition<TState> transition)
        {
            Transitions.Add(transition);
            return this;
        }

        public ComponentStateMachine<T, TState> AddTransitionFromAny(TransitionFromAny<TState> transitionFromAny)
        {
            TransitionsFromAny.Add(transitionFromAny);
            return this;
        }

        public bool Equals(IStateMachine<TState> o) => GetHashCode() == o.GetHashCode();
    }
}
