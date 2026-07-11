using UnityEngine;
using System.Collections;
using MiniGameWorld.UI;
using MiniGameWorld.Game;
using System;

namespace MiniGameWorld.Core
{
    public class GameState : AbstractState
    {
        private readonly UIPresenter m_UI;
        private readonly MiniGame m_MiniGame;
        private readonly Action<MiniGameResult> m_ResultCallback;

        public GameState(UIPresenter uIPresenter, MiniGame miniGame, Action<MiniGameResult> resultCallback)
        {
            m_UI = uIPresenter;
            m_MiniGame = miniGame;
            m_ResultCallback = resultCallback;
        }
        public override void Enter()
        {
            base.Enter();

            m_UI.ShowView(m_UI.GameView);
            m_MiniGame.gameObject.SetActive(true);
            m_MiniGame.Initialize();
            m_MiniGame.ScoreChanged += OnScoreChanged;
            m_MiniGame.TimerChanged += OnTimeChanged;
            m_MiniGame.ResetGame();
            m_MiniGame.StartGame();
        }
        private void OnScoreChanged(int score)
        {
            m_UI.SetScore(score);
        }
        private void OnTimeChanged(float currentTime, float maxTime)
        {
            m_UI.UpdateTimer(currentTime, maxTime);
        }

        public override IEnumerator Execute()
        {
            while (true)
                yield return null;
        }
        public override void Exit()
        {
            m_MiniGame.ScoreChanged -= OnScoreChanged;
            m_ResultCallback?.Invoke(m_MiniGame.GetResult());
            m_MiniGame.FinishGame();
        }
    }
}