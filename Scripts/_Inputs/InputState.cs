using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marmalade.Inputs
{
    public struct InputState : IResource
    {
        public static readonly InputState None = new InputState();

        private Guid guid;
        private HashSet<IInputEvaluator> evaluators;

        public Guid Guid => guid;

        public InputState(Guid guid) 
        { 
            this.guid = guid; 
            evaluators = new HashSet<IInputEvaluator>();
        }

        public static InputState New() => new InputState(Guid.NewGuid());

        public InputState AddEvaluator(IInputEvaluator evaluator)
        {
            evaluators.Add(evaluator);
            return this;
        }

        public void EvaluateInput()
        {
            foreach (IInputEvaluator evaluator in evaluators)
                evaluator.Evaluate();
        }
    }
}
