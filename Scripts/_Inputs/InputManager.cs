using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Inputs
{
    [AddComponentMenu("FpCore/Logic/InputManager")]
    public class InputManager : GameSystem<InputManager>
    {
        private Dictionary<Guid, InputState> statesById = new Dictionary<Guid, InputState>();
        private InputState state;

        protected override void Awake()
        {
            base.Awake();
            statesById[Guid.Empty] = InputState.None;
        }

        private void Update()
        {
            if (!InputState.None.Equals(state))
                state.EvaluateInput();
        }

        public InputState RegisterState(InputState state)
        {
            statesById[state.Guid] = state;
            return state;
        }

        public bool GoToState(Guid guid)
        {
            if (!statesById.TryGetValue(guid, out InputState state) || !(state.Equals(state)))
                return false;
            this.state = state;
            return true;
        }
    }
}
