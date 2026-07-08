using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class GameView : BaseMenuView
    {
        Button m_FinishButton;
        Label m_ScoreLabel;

        public event Action FinishClicked;

        public GameView(VisualElement root) : base(root)
        {
            RegisterCallbacks();
        }

        protected override void SetVisualElements()
        {
            m_FinishButton = m_RootElement.Q<Button>("finish-button");
            m_ScoreLabel = m_RootElement.Q<Label>("score-label");
        }

        private void RegisterCallbacks()
        {
            m_EventRegistry.RegisterCallback<ClickEvent>(m_FinishButton, OnFinishClick);
        }

        private void OnFinishClick()
        {
            FinishClicked?.Invoke();
        }
        public void SetScore(int score)
        {
            m_ScoreLabel.text = $"Score: {score}";
        }
    }
}
