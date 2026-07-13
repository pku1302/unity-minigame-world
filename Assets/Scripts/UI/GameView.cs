using System;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class GameView : BaseMenuView
    {
        ScoreView m_ScoreView;
        TimerView m_TimerView;
        Button m_FinishButton;
        Button m_PauseButton;

        public event Action FinishRequested;
        public event Action PauseRequested;

        public GameView(VisualElement root) : base(root)
        {
            RegisterCallbacks();
            m_ScoreView = new ScoreView(root);
            m_TimerView = new TimerView(root);
        }

        protected override void SetVisualElements()
        {
            m_FinishButton = m_RootElement.Q<Button>("finish-button");
            m_PauseButton = m_RootElement.Q<Button>("pause-button");
        }

        private void RegisterCallbacks()
        {
            m_EventRegistry.RegisterCallback<ClickEvent>(m_FinishButton, OnFinishClick);
            m_EventRegistry.RegisterCallback<ClickEvent>(m_PauseButton, OnPauseRequested);
        }
        private void OnPauseRequested()
        {
            PauseRequested?.Invoke();
        }

        private void OnFinishClick()
        {
            FinishRequested?.Invoke();
        }
        public void SetScore(int score)
        {
            m_ScoreView.SetScore(score);
        }
        public void SetTimer(float currentTime, float maxTime)
        {
            m_TimerView.SetFill(currentTime / maxTime);
        }
    }
}
