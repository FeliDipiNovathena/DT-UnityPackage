using Newtonsoft.Json;

namespace DT.Scripts.Messages
{
    public class Message
    {
        [JsonProperty("key")] public string MessageKey { get; set; }
        [JsonProperty("data")] public string MessageData { get; set; }

        public Message(string messageKey, string messageData)
        {
            MessageKey = messageKey;
            MessageData = messageData;
        }

        public bool IsNull()
        {
            return string.IsNullOrEmpty(MessageKey);
        }
    }
}