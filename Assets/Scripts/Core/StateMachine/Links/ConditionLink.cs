using System;
using UnityEngine;

namespace MiniGameWorld.Core
{
    public class ConditionLink : ILink
    {
        private readonly Func<bool> m_Condition;
        private readonly IState m_NextState;

        public ConditionLink(Func<bool> condition, IState nextState)
        {
            m_Condition = condition ?? throw new ArgumentNullException(nameof(condition));
            m_NextState = nextState ?? throw new ArgumentNullException(nameof(nextState));
        }

        public bool Validate(out IState nextState)
        {
            if (m_Condition())
            {
                nextState = m_NextState;
                return true;
            }

            nextState = null;
            return false;
        }

    }
}
