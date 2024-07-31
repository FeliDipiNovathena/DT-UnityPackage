using DT.Scripts.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DT.Scripts.MessagesServices
{
    public static class MessageService
    {
#if UNITY_WEBGL

        [DllImport("__Internal")]
        private static extern void ReceiveMessageForUnity(string rawMessage);

        public static void SendMessageToWebSite(Message newEvent)
        {
            Debug.Log(newEvent);

            var e = JsonConvert.SerializeObject(newEvent);
            ReceiveMessageForUnity(e);
        }

#endif
        private static Dictionary<string, Type> _messageEffects;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
            _messageEffects = new Dictionary<string, Type>();

            Type baseType = typeof(MessageEffect);
            IEnumerable<Type> messageTypes = Assembly.GetAssembly(baseType).GetTypes().Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract);

            foreach (var type in messageTypes)
            {
                MessageEffect effectInstance = Activator.CreateInstance(type) as MessageEffect;
                _messageEffects[effectInstance.ID] = type;
            }
        }

        public static void SendNewEvent(string rawMessage)
        {
            Message message = JsonConvert.DeserializeObject<Message>(rawMessage);
            if (message.IsNull())
            {
                Debug.LogError("String converted to json is not type of Message");
                return;
            }

            EnqueueNewEvent(message);
        }

        private static void EnqueueNewEvent(Message message)
        {
            if (_messageEffects.TryGetValue(message.MessageKey, out var effectType))
            {
                var effect = Activator.CreateInstance(effectType) as MessageEffect;
                effect.Execute(message.Data);
            }
        }
    }
}