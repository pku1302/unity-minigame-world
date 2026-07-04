using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace MiniGameWorld.Core
{
    public abstract class AbstractState : IState
    {
        public virtual string Name { get; set; }

        protected bool m_Debug = false;
        readonly List<ILink> m_Links = new();

        public bool DebugEnabled { get => m_Debug; set => m_Debug = value; }

        public virtual void Enter()
        {
        }

        public abstract IEnumerator Execute();

        public virtual void Exit()
        {

        }
        public virtual void AddLink(ILink link)
        {
            if (!m_Links.Contains(link))
            {
                m_Links.Add(link);
            }
        }
        public virtual void RemoveLink(ILink link)
        {
            if (m_Links.Contains(link))
            {
                m_Links.Remove(link);
            }
        }

        public virtual void RemoveAllLinks()
        {
            m_Links.Clear();
        }

        public virtual bool ValidateLinks(out IState nextState)
        {
            if (m_Links != null && m_Links.Count > 0)
            {
                foreach (var link in m_Links)
                {
                    if (link.Validate(out nextState))
                    {
                        return true;
                    }
                }
            }

            nextState = null;
            return false;
        }

        public virtual void LogCurrentState()
        {
            if (m_Debug)
            {
                string message = "[AbstractState] Current state: " + Name + "(" + this.GetType().Name + ") ----------------------------------------------------------";
                message = message.Substring(0, 100);
                Debug.Log(message);
            }
        }
    }
}

