using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class EventRegistry : IDisposable
    {
        Action m_UnregisterActions;

        public void RegisterCallback<TEvent>(VisualElement visualElement, Action<TEvent> callback) where TEvent : EventBase<TEvent>, new()
        {
            EventCallback<TEvent> eventCallback = new EventCallback<TEvent>(callback);
            visualElement.RegisterCallback(eventCallback);

            m_UnregisterActions += () => visualElement.UnregisterCallback(eventCallback);
        }

        public void RegisterCallback<TEvent>(VisualElement visualElement, Action callback) where TEvent : EventBase<TEvent>, new()
        {
            EventCallback<TEvent> eventCallback = new EventCallback<TEvent>((evt) => callback());
            visualElement.RegisterCallback(eventCallback);

            m_UnregisterActions += () => visualElement.UnregisterCallback(eventCallback);
        }

        public void RegisterValueChangedCallback<T>(BindableElement bindableElement, Action<T> callback) where T : struct
        {
            EventCallback<ChangeEvent<T>> eventCallback = new EventCallback<ChangeEvent<T>>(evt => callback(evt.newValue));
            bindableElement.RegisterCallback(eventCallback);

            m_UnregisterActions += () => bindableElement.UnregisterCallback(eventCallback);
        }

        public void Dispose()
        {
            m_UnregisterActions?.Invoke();
            m_UnregisterActions = null;
        }
    }
}
