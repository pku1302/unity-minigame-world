using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class ResultView : BaseMenuView
    {
        Button m_RetryButton;
        Button m_MainMenuButton;

        public event Action RetryClicked;
        public event Action MainMenuClicked;

        public ResultView(VisualElement root) : base(root)
        {
            RegisterCallbacks();
        }

        protected override void SetVisualElements()
        {
            m_RetryButton = m_RootElement.Q<Button>("retry-button");
            m_MainMenuButton = m_RootElement.Q<Button>("main-menu-button");
        }
        private void RegisterCallbacks()
        {
            m_EventRegistry.RegisterCallback<ClickEvent>(m_RetryButton, OnRetryClick);
            m_EventRegistry.RegisterCallback<ClickEvent>(m_MainMenuButton, OnMainMenuClick);
        }
        private void OnRetryClick()
        {
            RetryClicked?.Invoke();
        }
        private void OnMainMenuClick()
        {
            MainMenuClicked?.Invoke();
        }
    }

}