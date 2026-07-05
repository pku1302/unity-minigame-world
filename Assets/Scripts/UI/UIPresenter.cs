using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class UIPresenter : MonoBehaviour
    {
        [SerializeField] private UIDocument m_UIDocument;

        private MainMenuView m_MainMenuView;
        public MainMenuView MainMenuView => m_MainMenuView;

        public event Action StartRequested;
        public event Action SettingsRequested;
        public event Action QuitRequested;
        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            VisualElement root = m_UIDocument.rootVisualElement;

            m_MainMenuView = new MainMenuView(root.Q<VisualElement>("main-menu"));

            m_MainMenuView.StartClicked += OnStartClicked;
            m_MainMenuView.SettingsClicked += OnSettingsClicked;
            m_MainMenuView.QuitClicked += OnQuitClicked;

            m_MainMenuView.Show();
        }

        private void OnDestroy()
        {
            if (m_MainMenuView != null)
            {
                m_MainMenuView.StartClicked -= OnStartClicked;
                m_MainMenuView.SettingsClicked -= OnSettingsClicked;
                m_MainMenuView.QuitClicked -= OnQuitClicked;

                m_MainMenuView.Disable();
            }
        }

        private void OnStartClicked()
        {
            StartRequested?.Invoke();
        }

        private void OnSettingsClicked()
        {
            SettingsRequested?.Invoke();
        }

        private void OnQuitClicked()
        {
            QuitRequested?.Invoke();
        }

    }
}