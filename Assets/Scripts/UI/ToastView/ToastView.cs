using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class ToastView : UIView
    {
        Label m_TitleLabel;
        Label m_MessageLabel;
        VisualElement m_Icon;

        public ToastView(VisualElement rootElement) : base(rootElement)
        {
            SetVisualElements();
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