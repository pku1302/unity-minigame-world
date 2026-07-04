using System;
using System.Collections;
using UnityEngine;

namespace MiniGameWorld.Core
{
    public class StateMachine
    {
        public IState CurrentState { get; private set; }

        Coroutine m_CurrentPlayCoroutine;

        bool m_PlayLock;

        public virtual void SetCurrentState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            if (CurrentState != null && m_CurrentPlayCoroutine != null)
            {
                Skip();
            }

            CurrentState = state;

            Utilities.Coroutines.StartCoroutine(Play());
        }

        private IEnumerator Play()
        {
            if (!m_PlayLock)
            {
                m_PlayLock = true;

                CurrentState.Enter();

                m_CurrentPlayCoroutine = Utilities.Coroutines.StartCoroutine(CurrentState.Execute());

                yield return m_CurrentPlayCoroutine;

                m_CurrentPlayCoroutine = null;
            }
        }

        void Skip()
        {
            if (CurrentState == null)
                throw new Exception($"{nameof(CurrentState)} is null");

            if (m_CurrentPlayCoroutine != null)
            {
                Utilities.Coroutines.StopCoroutine(ref m_CurrentPlayCoroutine);
                CurrentState.Exit();
                m_CurrentPlayCoroutine= null;
                m_PlayLock= false;
            }
        }

        Coroutine m_LoopCoroutine;

        public virtual void Run(IState initialState)
        {
            SetCurrentState(initialState);
            Run();
        }

        public virtual void Run()
        {
            if (m_LoopCoroutine != null)
                return;

            m_LoopCoroutine = Utilities.Coroutines.StartCoroutine(Loop());
        }


        public void Stop()
        {
            if (m_LoopCoroutine == null)
                return;

            if (CurrentState != null && m_CurrentPlayCoroutine != null)
            {
                Skip();
            }

            Utilities.Coroutines.StopCoroutine(ref m_LoopCoroutine);
            CurrentState = null;
        }

        protected virtual IEnumerator Loop()
        {
            while (true)
            {
                if (CurrentState != null)
                {
                    if (CurrentState.ValidateLinks(out var nextState))
                    {
                        Skip();
                        SetCurrentState(nextState);
                    }
                }

                yield return null;
            }
        }
    }
}

