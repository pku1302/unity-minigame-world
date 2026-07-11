using System;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class GameView : BaseMenuView
    {
        VisualElement m_ScoreView;
        VisualElement m_TimerView;
        VisualElement m_Fill;
        Label m_ScoreLabel;
        Button m_FinishButton;

        public event Action FinishRequested;

        public GameView(VisualElement root) : base(root)
        {
            RegisterCallbacks();
        }

        protected override void SetVisualElements()
        {
            m_ScoreView = m_RootElement.Q<VisualElement>("score");
            m_ScoreLabel = m_ScoreView.Q<Label>("score-label");
            m_TimerView = m_RootElement.Q<VisualElement>("timer");
            m_Fill = m_TimerView.Q<VisualElement>("fill");
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
            m_ScoreLabel.text = $"Score: {score}";
        }
        public void SetTimer(float currentTime, float maxTime)
        {
            float ratio = currentTime / maxTime;
            m_Fill.style.width = Length.Percent(ratio * 100f);
        }
    }
}
