using System;
using UnityEngine;


namespace MiniGameWorld.Game
{
    public abstract class MiniGame : MonoBehaviour
    {
        protected int m_Score;

        public event Action<MiniGameResult> Finished;
        public event Action<int> ScoreChanged;
        protected void RaiseScoreChanged(int score)
        {
            ScoreChanged?.Invoke(score);
        }

        public abstract void Initialize();
        public abstract void StartGame();
        public abstract void ResetGame();
        public abstract void FinishGame();
        public abstract void Dispose();

        public virtual MiniGameResult GetResult()
        {
            return new MiniGameResult
            {
                Score = m_Score
            };
        }
    }
}