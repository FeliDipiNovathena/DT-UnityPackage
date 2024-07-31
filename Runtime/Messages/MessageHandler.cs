using DT.Scripts.MessagesServices;
using UnityEngine;

namespace DT.Scripts.Messages
{
    public class MessageHandler : MonoBehaviour
    {
#if UNITY_WEBGL

        public void SendMessageToWebSite(Message newEvent)
        {
            MessageService.SendMessageToWebSite(newEvent);
        }

#endif

        public void SendNewEvent(string rawMessage)
        {
            MessageService.SendNewEvent(rawMessage);
        }
    }
}