using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class PauseView : PopupView
    {
        Button m_ResumeButton;
        Button m_SettingsButton;
        Button m_MainMenuButton;

        public event Action ResumeRequested;
        public event Action SettingsRequested;
        public event Action MainMenuRequested;

        public PauseView(VisualElement root) : base(root)
        {
            RegisterCallbacks();
        }

        protected override void SetVisualElements()
        {
            m_ResumeButton = m_RootElement.Q<Button>("resume-button");
            m_SettingsButton = m_RootElement.Q<Button>("settings-button");
            m_MainMenuButton = m_RootElement.Q<Button>("main-menu-button");
        }

        private void RegisterCallbacks()
        {
            m_EventRegistry.RegisterCallback<ClickEvent>(m_ResumeButton, () => ResumeRequested?.Invoke());
            m_EventRegistry.RegisterCallback<ClickEvent>(m_SettingsButton, () => SettingsRequested?.Invoke());
            m_EventRegistry.RegisterCallback<ClickEvent>(m_MainMenuButton, () => MainMenuRequested?.Invoke());
        }
    }
}
