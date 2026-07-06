using MiniGameWorld.UI;
using System.Collections;
using UnityEngine;

namespace MiniGameWorld.Core
{
    public class GameSelectState : AbstractState
    {
        private readonly UIPresenter m_UIPresenter; // 추후 리팩터링 고려

        public GameSelectState(UIPresenter uiPresenter)
        {
            m_UIPresenter = uiPresenter;
        }

        public override void Enter()
        {
            base.Enter();

            m_UIPresenter.ShowView(m_UIPresenter.GameSelectView);
        }

        public override IEnumerator Execute()
        {
            while (true)
                yield return null;
        }

        public override void Exit()
        {
        }
    }
}

