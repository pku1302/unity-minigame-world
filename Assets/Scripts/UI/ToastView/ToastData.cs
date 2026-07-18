using UnityEngine;

namespace MiniGameWorld.UI
{
    public class ToastData
    {
        public string Title { get; }
        public string Message { get; }
        public Sprite Icon { get; }

        public ToastData(
            string title,
            string message,
            Sprite icon = null)
        {
            Title = title;
            Message = message;
            Icon = icon;
        }
    }
}