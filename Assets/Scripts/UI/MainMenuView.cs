using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class MainMenuView : BaseMenuView
    {
        Button m_StartButton;
        Button m_SettingsButton;
        Button m_QuitButton;

        public event Action StartClicked;
        public event Action SettingsClicked;
        public event Action QuitClicked;

        public MainMenuView(VisualElement root) : base(root)
        {
            RegisterCallbacks();
        }

        protected override void SetVisualElements()
        {
            m_StartButton = m_RootElement.Q<Button>("start-button");
            m_SettingsButton = m_RootElement.Q<Button>("settings-button");
            m_QuitButton = m_RootElement.Q<Button>("quit-button");
        }

        private void RegisterCallbacks()
        {
            m_EventRegistry.RegisterCallback<ClickEvent>(m_StartButton, OnStartClick);
            m_EventRegistry.RegisterCallback<ClickEvent>(m_SettingsButton, OnSettingsClick);
            m_EventRegistry.RegisterCallback<ClickEvent>(m_QuitButton, OnQuitClick);
        }

        private void OnStartClick()
        {
            StartClicked?.Invoke();
        }

        private void OnSettingsClick()
        {
            SettingsClicked?.Invoke();
        }

        private void OnQuitClick()
        {
            QuitClicked?.Invoke();
        }

    }
}
