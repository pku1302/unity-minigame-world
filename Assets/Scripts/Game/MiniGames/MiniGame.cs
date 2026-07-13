using System;
using UnityEngine;

namespace MiniGameWorld.Game
{
    public enum GameType
    {
        Flower,

    }
    public interface ICollectContext
    {
        void AddScore(int amount);
        void AddTime(float amount);
        void AddCoin(int amount);
    }
    public abstract class MiniGame : MonoBehaviour, ICollectContext
    {
        protected int m_Score;
        protected GameType m_Type;
        protected MiniGameTimer Timer = new MiniGameTimer();

        public event Action<MiniGameResult> GameFinished;
        public event Action<int> ScoreChanged;
        public event Action<float, float> TimerChanged;
        public event Action<int> CoinCollected;

        [SerializeField]
        private float m_MaxTime = 60f;

        public float MaxTime => m_MaxTime;

        protected virtual void Update()
        {
            Timer.Tick(Time.deltaTime);
        }

        public virtual void Pause()
        {

        }
        public virtual void Resume()
        {

        }

        protected void RaiseScoreChanged(int score)
        {
            ScoreChanged?.Invoke(score);
        }
        public void AddTime(float amount)
        {
            Timer.AddTime(amount);
        }
        public void AddScore(int amount)
        {
            m_Score += amount;
            RaiseScoreChanged(m_Score);
        }
        public void AddCoin(int amount)
        {
            CoinCollected?.Invoke(amount);
        }

        protected void ReduceTime(float amount)
        {
            Timer.ReduceTime(amount);
        }
        protected void RaiseFinished()
        {
            GameFinished?.Invoke(GetResult());
        }
        protected virtual void OnTimerChanged(float currentTime, float maxTime)
        {
            TimerChanged?.Invoke(currentTime, maxTime);
        }
        public virtual void Initialize()
        {
            Timer.TimeChanged += OnTimerChanged;
            Timer.TimeOver += FinishGame;
        }
        public virtual void StartGame()
        {
            Timer.StartTimer();
        }
        public virtual void ResetGame()
        {
            m_Score = 0;
            Timer.ResetTimer(m_MaxTime);
        }
        public abstract void FinishGame();
        public virtual void Dispose()
        {
            Timer.TimeChanged -= OnTimerChanged;
            Timer.TimeOver -= FinishGame;
        }
        public virtual MiniGameResult GetResult()
        {
            return new MiniGameResult (m_Score, m_Type)
            {
                Score = m_Score,
                GameType = m_Type
            };
        }
    }
}