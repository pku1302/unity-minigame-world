using System;
using UnityEngine;

namespace MiniGameWorld.Game
{
    public abstract class MiniGame : MonoBehaviour
    {
        protected int m_Score;
        protected MiniGameTimer Timer = new MiniGameTimer();

        public event Action<MiniGameResult> Finished;
        public event Action<int> ScoreChanged;
        public event Action<float, float> TimerChanged;

        [SerializeField]
        private float m_MaxTime = 60f;

        public float MaxTime => m_MaxTime;

        protected virtual void Update()
        {
            Timer.Tick(Time.deltaTime);
        }

        protected void RaiseScoreChanged(int score)
        {
            ScoreChanged?.Invoke(score);
        }
        protected void AddTime(float amount)
        {
            Timer.AddTime(amount);
        }
        protected void ReduceTime(float amount)
        {
            Timer.ReduceTime(amount);
        }
        protected void RaiseFinished()
        {
            Finished?.Invoke(GetResult());
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
            return new MiniGameResult (m_Score)
            {
                Score = m_Score
            };
        }
    }
}