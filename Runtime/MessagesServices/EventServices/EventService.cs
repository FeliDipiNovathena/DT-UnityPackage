using System;
using System.Collections.Generic;
using UnityEngine;

namespace DT.Scripts.MessagesServices.Events
{
    public static class EventService
    {
        private static Dictionary<string, Action> _simpleEvents;
        private static Dictionary<Type, ICustomEventWrapper> _complexEvents;

        private static event Action<ICustomEventData> _callbackForAll;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
            _simpleEvents = new Dictionary<string, Action>();
            _complexEvents = new Dictionary<Type, ICustomEventWrapper>();
            _callbackForAll = null;
        }

        public static void AddListener(string guid, Action callback)
        {
            if (guid == null)
                return;

            if (!_simpleEvents.ContainsKey(guid))
            {
                _simpleEvents.Add(guid, callback);
                return;
            }

            _simpleEvents[guid] += callback;
        }

        public static void RemoveListener(string guid, Action callback)
        {
            if (!_simpleEvents.ContainsKey(guid))
                return;

            _simpleEvents[guid] -= callback;
        }

        public static void DispatchEvent(string guid)
        {
            if (!_simpleEvents.ContainsKey(guid))
                return;

            _simpleEvents[guid]?.Invoke();
        }

        public static void AddListener<T>(Action<T> callback) where T : ICustomEventData
        {
            var type = typeof(T);
            if (!_complexEvents.ContainsKey(type))
            {
                var eventWrapper = new CustomEventWrapper<T>();
                eventWrapper.EventAction += callback;
                _complexEvents.Add(type, eventWrapper);
                return;
            }

            if (_complexEvents[type] is CustomEventWrapper<T> eventWrap)
                eventWrap.EventAction += callback;
        }

        public static void RemoveListener<T>(Action<T> callback) where T : ICustomEventData
        {
            var type = typeof(T);
            if (!_complexEvents.ContainsKey(type))
                return;

            if (_complexEvents[type] is CustomEventWrapper<T> eventWrap)
                eventWrap.EventAction -= callback;
        }

        public static void DispatchEvent<T>(T eventData) where T : ICustomEventData
        {
            _callbackForAll?.Invoke(eventData);

            var l_type = typeof(T);
            if (!_complexEvents.ContainsKey(l_type))
                return;

            if (_complexEvents[l_type] is CustomEventWrapper<T> eventWrap)
                eventWrap.Dispatch(eventData);
        }

        public static void AddListenerForAll(Action<ICustomEventData> callback)
        {
            _callbackForAll += callback;
        }

        public static void RemoveListenerForAll(Action<ICustomEventData> callback)
        {
            _callbackForAll -= callback;
        }
    }
}