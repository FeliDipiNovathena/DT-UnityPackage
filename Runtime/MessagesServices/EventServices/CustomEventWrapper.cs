using System;

namespace DT.Scripts.MessagesServices.Events
{
    public class CustomEventWrapper<T> : ICustomEventWrapper<T> where T : ICustomEventData
    {
        public event Action<T> EventAction;

        public void Dispatch(T eventData)
        {
            EventAction?.Invoke(eventData);
        }
    }
}