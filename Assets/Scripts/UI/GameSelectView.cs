using System;
using UnityEngine;
using UnityEngine.UIElements;


namespace MiniGameWorld.UI
{
    public class GameSelectView : BaseMenuView
    {
        Button m_StartButton;
        Button m_BackButton;

        public event Action StartClicked;
        public event Action BackClicked;

        public GameSelectView(VisualElement root) : base(root)
        {
            RegisterCallbacks();
        }

        protected override void SetVisualElements()
        {
            m_StartButton = m_RootElement.Q<Button>("prototype-game-button");
            m_BackButton = m_RootElement.Q<Button>("back-button");
        }

        private void RegisterCallbacks()
        {
            m_EventRegistry.RegisterCallback<ClickEvent>(m_StartButton, OnStart);
            m_EventRegistry.RegisterCallback<ClickEvent>(m_BackButton, OnBack);
        }

        private void OnStart()
        {
            StartClicked?.Invoke();
        }

        private void OnBack()
        {
            BackClicked?.Invoke();
        }
    }
}

