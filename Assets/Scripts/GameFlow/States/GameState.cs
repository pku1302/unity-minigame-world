using UnityEngine;
using System.Collections;
using MiniGameWorld.UI;
using MiniGameWorld.Game;
using System;
using UnityEngine.InputSystem;

namespace MiniGameWorld.Core
{
    public class GameState : AbstractState
    {
        private readonly UIPresenter m_UI;
        private readonly MiniGame m_MiniGame;
        private readonly CurrencyManager m_CurrencyManager;

        private bool m_IsPaused;

        private readonly Action<MiniGameResult> m_GameFinishedCallback;
        public GameState(UIPresenter uIPresenter, MiniGame miniGame, Action<MiniGameResult> gameFinishedCallback, CurrencyManager currencyManager)
        {
            m_UI = uIPresenter;
            m_MiniGame = miniGame;
            m_GameFinishedCallback = gameFinishedCallback;
            m_CurrencyManager = currencyManager;
        }
        public override void Enter()
        {
            base.Enter();

            m_IsPaused = false;
            m_MiniGame.gameObject.SetActive(true);

            m_UI.ShowView(m_UI.GameView);
            m_UI.PauseRequested += OnPauseRequested;
            m_UI.ResumeRequested += OnResumeRequested;
            m_UI.FinishRequested += OnFinishRequested;

            m_MiniGame.Initialize();
            m_MiniGame.ScoreChanged += OnScoreChanged;
            m_MiniGame.TimerChanged += OnTimeChanged;
            m_MiniGame.GameFinished += OnGameFinished;
            m_MiniGame.ResetGame();
            m_MiniGame.StartGame();
        }
 
        private void OnPauseRequested()
        {
            if (m_IsPaused)
                return;

            m_IsPaused = true;

            m_UI.OpenPopup(m_UI.PauseView);
            m_MiniGame.Pause();
        }
        private void OnResumeRequested()
        {
            if (!m_IsPaused)
                return;

            m_IsPaused = false;

            m_UI.ClosePopup(m_UI.PauseView);
            m_MiniGame.Resume();
        }

        private void OnFinishRequested()
        {
            m_MiniGame.FinishGame();
        }

        private void OnGameFinished(MiniGameResult result)
        {
            m_GameFinishedCallback?.Invoke(result);
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
            {
                if (Keyboard.current.escapeKey.wasPressedThisFrame)
                {
                    if (m_IsPaused)
                    {
                        OnResumeRequested();
                    }
                    else
                    {
                        OnPauseRequested();
                    }
                }

                yield return null;
            }
        }
        public override void Exit()
        {
            if (m_IsPaused)
            {
                m_MiniGame.Resume();
            }
            m_UI.ClosePopup(m_UI.PauseView);

            m_UI.PauseRequested -= OnPauseRequested;
            m_UI.ResumeRequested -= OnResumeRequested;
            m_UI.FinishRequested -= OnFinishRequested;

            m_MiniGame.ScoreChanged -= OnScoreChanged;
            m_MiniGame.TimerChanged -= OnTimeChanged;
            m_MiniGame.GameFinished -= OnGameFinished;

            m_MiniGame.Cleanup();
        }
    }
}