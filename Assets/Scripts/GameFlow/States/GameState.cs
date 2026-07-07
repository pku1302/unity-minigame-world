using UnityEngine;
using System.Collections;
using MiniGameWorld.UI;
using MiniGameWorld.Game;

namespace MiniGameWorld.Core
{
    public class GameState : AbstractState
    {
        private readonly UIPresenter m_UI;
        private readonly MiniGame m_MiniGame;

        public GameState(UIPresenter uIPresenter, MiniGame miniGame)
        {
            m_UI = uIPresenter;
            m_MiniGame = miniGame;
        }

        public override void Enter()
        {
            base.Enter();

            m_UI.ShowView(m_UI.GameView);

            m_MiniGame.gameObject.SetActive(true);
            m_MiniGame.StartGame();
        }

        public override IEnumerator Execute()
        {
            while (true)
                yield return null;
        }

        public override void Exit()
        {
            m_MiniGame.FinishGame();
        }
    }

}