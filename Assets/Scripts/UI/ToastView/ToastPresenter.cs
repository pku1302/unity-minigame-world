using MiniGameWorld.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameWorld.UI
{
    public class ToastPresenter
    {
        private const float m_DisplayTime = 2f;
        private  ToastView m_View { get; }
        private readonly Queue<ToastData> m_Queue = new();
        private Coroutine m_DisplayRoutine;
        private MonoBehaviour m_Runner;
        public ToastView ToastView => m_View;

        public ToastPresenter(MonoBehaviour runner, ToastView toastView)
        {
            m_View = toastView;
            m_Runner = runner;
        }

        public void Show(ToastData data)
        {
            m_Queue.Enqueue(data);

            if (m_DisplayRoutine != null)
                return;

            m_DisplayRoutine = m_Runner.StartCoroutine(DisplayRoutine());
        }

        public void Clear()
        {
            m_Queue.Clear();

            if (m_DisplayRoutine != null)
            {
                m_Runner.StopCoroutine(m_DisplayRoutine);
                m_DisplayRoutine = null;
            }

            m_View.Hide();
        }

        private IEnumerator DisplayRoutine()
        {
            while (m_Queue.Count > 0)
            {
                ToastData toast = m_Queue.Dequeue();

                m_View.SetData(toast);

                m_View.Show();

                yield return m_View.PlayShowAnimation();

                yield return new WaitForSeconds(m_DisplayTime);

                yield return m_View.PlayHideAnimation();

                m_View.Hide();
            }

            m_DisplayRoutine = null;
        }
    }
}