using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class ToastView : UIView
    {
        private const float AnimationDuration = 0.3f;
        private const float HiddenY = -100f;
        private const float VisibleY = 32f;

        Label m_TitleLabel;
        Label m_MessageLabel;
        VisualElement m_Icon;

        public ToastView(VisualElement rootElement) : base(rootElement)
        {
            SetVisualElements();
        }

        private IEnumerator Animate(float fromY, float toY, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float t = Mathf.Clamp01(elapsed / duration);

                // SmoothStep
                t = t * t * (3f - 2f * t);

                float y = Mathf.Lerp(fromY, toY, t);

                m_RootElement.style.translate = new Translate(0, y);

                yield return null;
            }

            m_RootElement.style.translate = new Translate(0, toY);
        }

        public IEnumerator PlayShowAnimation()
        {
            yield return Animate(HiddenY, VisibleY, AnimationDuration);
        }

        public IEnumerator PlayHideAnimation()
        {
            yield return Animate(VisibleY, HiddenY, AnimationDuration);
        }

        protected virtual void SetVisualElements()
        {
            m_TitleLabel = m_RootElement.Q<Label>("toast-title");
            m_MessageLabel = m_RootElement.Q<Label>("toast-message");
            m_Icon = m_RootElement.Q<VisualElement>("toast-icon");
        }
        public void SetData(ToastData data)
        {
            m_TitleLabel.text = data.Title;
            m_MessageLabel.text = data.Message;

            if (data.Icon != null)
            {
                m_Icon.style.backgroundImage = new StyleBackground(data.Icon);
                m_Icon.style.display = DisplayStyle.Flex;
            }
            else
            {
                m_Icon.style.display = DisplayStyle.None;
            }
        }
    }
}