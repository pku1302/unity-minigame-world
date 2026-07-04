using System;
using System.Collections;
using UnityEngine;

namespace MiniGameWorld.Core
{
    public class State : AbstractState
    {
        readonly Action m_OnExecute;

        public State(Action onExecute, string stateName = nameof(State), bool enableDebug = false)
        {
            m_OnExecute = onExecute;
            Name = stateName;

            DebugEnabled = enableDebug;
        }

        public override IEnumerator Execute()
        {
            yield return null;

            if (m_Debug)
                base.LogCurrentState();

            m_OnExecute?.Invoke();
        }
    }
}