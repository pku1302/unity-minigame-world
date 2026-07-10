using System;
using UnityEngine;

namespace MiniGameWorld.Game
{
    public class MiniGameTimer
    {
        public event Action<float> TimeChanged;
        public event Action TimeOver;

        private float m_CurrentTime;
        private float m_MaxTime;
        private bool m_IsRunning;
        public float CurrentTime => m_CurrentTime;
        public bool IsRunning => m_IsRunning;
        public bool IsTimeOver => m_CurrentTime <= 0f;
        public float MaxTime => m_MaxTime;

        private void SetCurrentTime(float time)
        {
            m_CurrentTime = Mathf.Clamp(time, 0f, m_MaxTime);

            TimeChanged?.Invoke( m_CurrentTime);

            if (m_IsRunning && m_CurrentTime <= 0f)
            {
                m_IsRunning = false;
                TimeOver?.Invoke();
            }
        }

        public void StartTimer()
        {
            if (m_IsRunning)
                return;

            m_IsRunning = true;
        }

        public void ResetTimer(float startTime)
        {
            m_MaxTime = startTime;
            m_IsRunning =false;
            SetCurrentTime(startTime);
        }

        public void StopTimer()
        {
            m_IsRunning = false;
        }
        public void Tick(float deltaTime)
        {
            if (!m_IsRunning)
                return;

            SetCurrentTime(m_CurrentTime - deltaTime);
        }
        public void AddTime(float amount)
        {
            if (!m_IsRunning)
                return;

            SetCurrentTime(m_CurrentTime + amount);
        }
        public void ReduceTime(float amount)
        {
            if (!m_IsRunning)
                return;

            SetCurrentTime(m_CurrentTime - amount);
        }
    }
}
