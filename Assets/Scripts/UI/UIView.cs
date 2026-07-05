using UnityEngine;
using UnityEngine.UIElements;
using System;
using MiniGameWorld.Utilities;
using System.Collections;


namespace MiniGameWorld.UI
{
    public abstract class UIView
    {
        public const string k_VisibleClass = "screen-visible";
        public const string k_HiddenClass = "screen-hidden";

        protected bool m_HideOnAwake = true;
        protected bool m_IsTransparent;

        protected bool m_UseTransition = true;
        protected float m_TransitionDelay = 0.15f;

        protected VisualElement m_RootElement;
        protected EventRegistry m_EventRegistry;

        protected Coroutine m_DisplayRoutine;

        public bool IsHidden => m_RootElement.style.display == DisplayStyle.None;
        public bool IsTransparent => m_IsTransparent;
        public VisualElement ParentElement => m_RootElement;

        public UIView(VisualElement rootElement)
        {
            m_RootElement = rootElement ?? throw new ArgumentNullException(nameof(rootElement));

            Initialize();
        }

        private void Initialize()
        {
            if (m_HideOnAwake)
            {
                HideImmediately();
            }

            m_EventRegistry = new EventRegistry();
            m_EventRegistry.RegisterCallback<TransitionEndEvent>(m_RootElement, ParentElement_TransitionEnd);
        }

        public virtual void Disable()
        {
            m_EventRegistry.Dispose();
        }

        private void ParentElement_TransitionEnd(TransitionEndEvent evt)
        {
            if (evt.target == m_RootElement && m_RootElement.ClassListContains(k_HiddenClass))
            {
                HideImmediately();
            }
        }
        public virtual void Show()
        {
            Coroutines.StopCoroutine(ref m_DisplayRoutine);
            m_DisplayRoutine = Coroutines.StartCoroutine(ShowWithDelay(m_TransitionDelay));
        }
        private IEnumerator ShowWithDelay(float delayInSecs)
        {
            yield return new WaitForSeconds(delayInSecs);

            m_RootElement.style.display = DisplayStyle.Flex;

            if (m_UseTransition)
            {
                m_RootElement.AddToClassList(k_VisibleClass);
                m_RootElement.BringToFront();
                m_RootElement.RemoveFromClassList(k_HiddenClass);
            }
        }
        public virtual void Hide(float delay = 0f)
        {
            Coroutines.StopCoroutine(ref m_DisplayRoutine);
            m_DisplayRoutine = Coroutines.StartCoroutine(HideWithDelay(delay));
        }

        private IEnumerator HideWithDelay(float delayInSecs)
        {
            yield return new WaitForSeconds(delayInSecs);

            if (m_UseTransition)
            {
                m_RootElement.AddToClassList(k_HiddenClass);
                m_RootElement.RemoveFromClassList(k_VisibleClass);
            }
            else
            {
                HideImmediately();
            }
        }
        public void HideImmediately()
        {
            m_RootElement.style.display = DisplayStyle.None;
        }

    }
}
