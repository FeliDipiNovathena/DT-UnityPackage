namespace DT.Scripts.Messages
{
    public abstract class MessageEffect
    {
        public abstract string ID { get; }

        public abstract void Execute(string data);
    }
}