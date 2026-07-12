using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class TimerView
    {
        private readonly VisualElement m_Fill;

        public TimerView(VisualElement root)
        {
            m_Fill = root.Q<VisualElement>("timer-fill");
        }
        public void SetFill(float ratio)
        {
            m_Fill.style.width = Length.Percent(ratio * 100f);
        }
    }
}
