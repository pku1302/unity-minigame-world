using UnityEngine;
using System.Collections;
using MiniGameWorld.UI;

namespace MiniGameWorld.Core
{
    public class MainMenuState : AbstractState
    {
        private readonly UIPresenter m_UI; // 추후 리팩터링 고려
        private readonly CurrencyManager m_CurrencyManager;

        public MainMenuState(UIPresenter uiPresenter, CurrencyManager currencyManager)
        {
            m_UI = uiPresenter;
            m_CurrencyManager = currencyManager;
        }

        public override void Enter()
        {
            base.Enter();

            m_UI.ShowView(m_UI.MainMenuView);

            m_UI.MainMenuView.SetCoinCount(m_CurrencyManager.Currency);

            m_CurrencyManager.CurrencyChanged += OnCurrencyChanged;
        }
        private void OnCurrencyChanged(int amount)
        {
            m_UI.MainMenuView.SetCoinCount(amount);
        }
        
        public override IEnumerator Execute()
        {
            while (true)
                yield return null;
        }

        public override void Exit()
        {
            m_CurrencyManager.CurrencyChanged -= OnCurrencyChanged;
        }
    }
}
