using System;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class GameView : BaseMenuView
    {
        ScoreView m_ScoreView;
        TimerView m_TimerView;
        Button m_FinishButton;

        public event Action FinishRequested;

        public GameView(VisualElement root) : base(root)
        {
            RegisterCallbacks();
            m_ScoreView = new ScoreView(root);
            m_TimerView = new TimerView(root);
        }

        protected override void SetVisualElements()
        {
            m_FinishButton = m_RootElement.Q<Button>("finish-button");
        }

        private void RegisterCallbacks()
        {
            m_EventRegistry.RegisterCallback<ClickEvent>(m_FinishButton, OnFinishClick);
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
