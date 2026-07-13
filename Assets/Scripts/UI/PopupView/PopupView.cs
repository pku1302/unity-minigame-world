using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public abstract class PopupView : UIView
    {
        protected PopupView(VisualElement root) : base(root)
        {
            SetVisualElements();
        }
        public virtual void Open()
        {
            Show();
        }
        public virtual void Close()
        {
            Hide(); 
        }

        protected virtual void SetVisualElements()
        {

        }
    }
}
