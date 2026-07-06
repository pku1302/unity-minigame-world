using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class GameView : BaseMenuView
    {
        Button m_FinishButton;

        public event Action FinishClicked;

        public GameView(VisualElement root) : base(root)
        {
            RegisterCallbacks();
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
            FinishClicked?.Invoke();
        }
    }
}
