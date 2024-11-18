using System.Text;

namespace dotNetRealTimeProcessingBasics.Channels.ConcurrentChannelPoco
{
    public struct PocoChannelMessage
    {
        public PocoChannelMessage(int messageId)
            => MessageId = messageId;

        private DateTime CurrentDateTime = DateTime.Now;

        private int MessageId { get; init; }


        public override string ToString()
        {
            StringBuilder b = new();

            b.AppendLine($"Hello");
            b.AppendLine($"This is message Id: {MessageId}");
            b.AppendLine($"The Day Is: {CurrentDateTime.DayOfWeek}");
            b.AppendLine($"The current time is: {CurrentDateTime.ToLongTimeString()}");           

            return b.ToString();
        }
    }
}
