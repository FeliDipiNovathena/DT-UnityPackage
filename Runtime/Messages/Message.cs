using Newtonsoft.Json;

namespace DT.Scripts.Messages
{
    public class Message
    {
        [JsonProperty("key")] public string MessageKey { get; set; }
        [JsonProperty("data")] public string Data { get; set; }

        public Message(string messageKey, string messageData)
        {
            MessageKey = messageKey;
            Data = messageData;
        }

        public bool IsNull()
        {
            return string.IsNullOrEmpty(MessageKey) || string.IsNullOrEmpty(Data);
        }
    }
}