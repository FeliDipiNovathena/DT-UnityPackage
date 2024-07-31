using DT.Scripts.MessagesServices.Events;
using Newtonsoft.Json;
using System;

namespace DT.Scripts.Messages.Effect
{
    public class MessageExampleEffect : MessageEffect
    {
        public override string ID => "YOUR_MESSAGE_KEY";

        public override void Execute(string messageData)
        {
            ExampleMessage newMessage = JsonConvert.DeserializeObject<ExampleMessage>(messageData);

            EventService.DispatchEvent(newMessage);
        }
    }

    [Serializable]
    public class ExampleMessage : ICustomEventData
    {
        [JsonProperty("id")] public string ID { get; set; }
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("state")] public string State { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
    }
}