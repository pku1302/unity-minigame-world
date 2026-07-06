using UnityEngine;
using System.Collections;
using MiniGameWorld.UI;

namespace MiniGameWorld.Core
{
    public class GameState : AbstractState
    {
        private readonly UIPresenter m_UI;

        public GameState(UIPresenter uIPresenter)
        {
            m_UI = uIPresenter;
        }

        public override void Enter()
        {
            base.Enter();

            m_UI.ShowView(m_UI.GameView);
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