using UnityEngine;
using UnityEngine.UIElements;
using System;
using MiniGameWorld.Utilities;
using System.Collections;


namespace MiniGameWorld.UI
{
    public abstract class UIView
    {
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
        }

        public virtual void Disable()
        {
            m_EventRegistry.Dispose();
        }

        public virtual void Show()
        {
            m_RootElement.style.display = DisplayStyle.Flex;
        }

        public virtual void Hide(float delay = 0f)
        {
            m_RootElement.style.display = DisplayStyle.None;
        }
 
        public void HideImmediately()
        {
            m_RootElement.style.display = DisplayStyle.None;
        }

    }
}
