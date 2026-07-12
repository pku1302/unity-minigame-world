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

        public event Action<MiniGameResult> GameFinishedCallback;

        public GameState(UIPresenter uIPresenter, MiniGame miniGame, Action<MiniGameResult> gameFinishedCallback)
        {
            m_UI = uIPresenter;
            m_MiniGame = miniGame;
            GameFinishedCallback = gameFinishedCallback;
        }
        public override void Enter()
        {
            base.Enter();

            m_UI.ShowView(m_UI.GameView);
            m_MiniGame.gameObject.SetActive(true);
            m_MiniGame.Initialize();
            m_MiniGame.ScoreChanged += OnScoreChanged;
            m_MiniGame.TimerChanged += OnTimeChanged;
            m_MiniGame.GameFinished += OnGameFinished;
            m_MiniGame.ResetGame();
            m_MiniGame.StartGame();
        }
        private void OnGameFinished(MiniGameResult result)
        {
            GameFinishedCallback?.Invoke(result);
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
            m_MiniGame.FinishGame();
        }
    }
}