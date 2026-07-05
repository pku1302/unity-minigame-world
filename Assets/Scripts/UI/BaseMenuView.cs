using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public abstract class BaseMenuView : UIView
    {
        protected BaseMenuView(VisualElement rootElement) : base(rootElement)
        {
            SetVisualElements();
        }
        public override void Hide(float delay = 0)
        {
            base.Hide(delay);
        }
        public override void Show()
        {
            base.Show();
        }
        protected virtual void SetVisualElements()
        {
        }
    }
}
