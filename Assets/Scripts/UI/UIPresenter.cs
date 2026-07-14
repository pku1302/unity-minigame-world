using MiniGameWorld.Game;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class UIPresenter : MonoBehaviour
    {
        [SerializeField] private UIDocument m_UIDocument;

        private UIView m_CurrentView;

        private MainMenuView m_MainMenuView;
        private GameSelectView m_GameSelectView;
        private GameView m_GameView;
        private ResultView m_ResultView;
        private PauseView m_PauseView;

        public MainMenuView MainMenuView => m_MainMenuView;
        public GameSelectView GameSelectView => m_GameSelectView;
        public GameView GameView => m_GameView;
        public ResultView ResultView => m_ResultView;
        public PauseView PauseView => m_PauseView;

        public event Action StartRequested;
        public event Action SettingsRequested;
        public event Action QuitRequested;
        public event Action MainMenuRequested;
        public event Action FinishRequested;
        public event Action PauseRequested;
        public event Action ResumeRequested;

        public event Action<float, float> OnTimerChanged;

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            VisualElement root = m_UIDocument.rootVisualElement;

            m_MainMenuView = new MainMenuView(root.Q<VisualElement>("main-menu"));
            m_GameSelectView = new GameSelectView(root.Q<VisualElement>("game-select"));
            m_GameView = new GameView(root.Q<VisualElement>("test-game"));
            m_ResultView = new ResultView(root.Q<VisualElement>("game-result"));
            m_PauseView = new PauseView(root.Q<VisualElement>("pause-overlay"));
            
            m_MainMenuView.StartClicked += OnStartClicked;
            m_MainMenuView.SettingsClicked += OnSettingsClicked;
            m_MainMenuView.QuitClicked += OnQuitClicked;

            m_GameSelectView.StartClicked += OnStartClicked;
            m_GameSelectView.BackClicked += OnMainMenuClicked;

            m_GameView.FinishRequested += OnFinishRequested;
            m_GameView.PauseRequested += OnPauseRequested;

            m_ResultView.RetryClicked += OnStartClicked;
            m_ResultView.MainMenuClicked += OnMainMenuClicked;

            m_PauseView.ResumeRequested += OnResumeClicked;
            m_PauseView.MainMenuRequested += OnMainMenuClicked;
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

            if (m_GameSelectView != null)
            {
                m_GameSelectView.StartClicked -= OnStartClicked;
                m_GameSelectView.BackClicked -= OnMainMenuClicked;

                m_GameSelectView.Disable();
            }

            if (m_GameView != null)
            {
                m_GameView.FinishRequested -= OnFinishRequested;

                m_GameView.Disable();
            }

            if (m_ResultView != null)
            {
                m_ResultView.RetryClicked -= OnStartClicked;
                m_ResultView.MainMenuClicked -= OnMainMenuClicked;

                m_ResultView.Disable();
            }
        }

        public void UpdateTimer(float currentTime, float maxTime)
        {
            m_GameView.SetTimer(currentTime, maxTime);
        }

        public void SetScore(int score)
        {
            m_GameView.SetScore(score);
        }
        public void SetResult(MiniGameResult result)
        {
            m_ResultView.SetResult(result);
        }
        private void OnResumeClicked()
        {
            ResumeRequested?.Invoke();
        }

        private void OnFinishRequested()
        {
            FinishRequested?.Invoke();
        }
        private void OnPauseRequested()
        {
            PauseRequested?.Invoke();
        }
        private void OnMainMenuClicked()
        {
            MainMenuRequested?.Invoke();
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
        public void ShowView(UIView view)
        {
            if (m_CurrentView == view)
                return;

            m_CurrentView?.Hide();

            view.Show();
            m_CurrentView = view;
        }
        public void HideView(UIView view)
        {
            view.Hide();
        }
        public void OpenPopup(PopupView popup)
        {
            popup.Show();
        }
        public void ClosePopup(PopupView popup)
        {
            popup.Hide();
        }

    }
}