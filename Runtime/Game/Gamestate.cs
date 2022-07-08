using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Marmalade;
using Marmalade.Inputs;

namespace Marmalade
{
    public class Gamestate : IState<Gamestate>
    {
        private string name;
        private Guid guid = Guid.NewGuid();
        private Gamestate state;
        private HashSet<Transition<Gamestate>> transitions = new HashSet<Transition<Gamestate>>();
        private HashSet<TransitionFromAny<Gamestate>> transitionsFromAny = new HashSet<TransitionFromAny<Gamestate>>();
        private Guid inputState;

        public Guid Guid => guid;

        public Gamestate State
        {
            get => state;
            set => state = value;
        }

        public HashSet<Transition<Gamestate>> Transitions => transitions;

        public HashSet<TransitionFromAny<Gamestate>> TransitionsFromAny => transitionsFromAny;

        public Gamestate(string name)
        {
            this.name = name;
        }

        public virtual void Start()
        {
            if (inputState != Guid.Empty)
                Resources.Get<InputManager>().GoToState(inputState);
            UpdateAllTransitions();
            DebugLogger.Log("[Gamestate] - Activating Gameplay State " + name);
        }

        public virtual void Update()
        {
            UpdateAllTransitions();
            State?.Update();
        }

        public void FixedUpdate()
        {
            State?.FixedUpdate();
        }

        public virtual void End()
        {
            if (inputState != Guid.Empty)
                Resources.Get<InputManager>().GoToState(Guid.Empty);
            DebugLogger.Log("[Gamestate] - Deactivating Gameplay State " + name);
        }

        public Gamestate AddState(Gamestate state, bool goTo = false)
        {
            if (goTo)
                GoToState(state);
            return this;
        }

        public void GoToState(Gamestate state)
        {
            State?.End();
            this.state = state;
            State?.Start();
            UpdateAllTransitions();
        }

        private void UpdateAllTransitions()
        {
            UpdateTransitions();
            UpdateTransitionsFromAny();
        }

        private void UpdateTransitions()
        {
            foreach (Transition<Gamestate> transition in transitions)
            {
                if (State == transition.state && transition.CanTransition())
                {
                    GoToState(transition.goTo);
                    return;
                }
            }
        }

        private void UpdateTransitionsFromAny()
        {
            foreach (TransitionFromAny<Gamestate> transition in transitionsFromAny)
            {
                if (State != transition.goTo && transition.CanTransition())
                {
                    GoToState(transition.goTo);
                    return;
                }
            }
        }

        public Gamestate SetInputState(Guid guid)
        {
            inputState = guid;
            return this;
        }

        public Gamestate AddTransition(Transition<Gamestate> transition)
        {
            Transitions.Add(transition);
            return this;
        }

        public Gamestate AddTransitionFromAny(TransitionFromAny<Gamestate> transitionFromAny)
        {
            TransitionsFromAny.Add(transitionFromAny);
            return this;
        }

        public bool Equals(IStateMachine<Gamestate> o) => GetHashCode() == o?.GetHashCode();

    }
}
