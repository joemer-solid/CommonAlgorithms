namespace dotNetRealTimeProcessingBasics.Channels.ConcurrentChannelPoco
{
    public class PocoChannelProducerService
    {
        private readonly static Lazy<BasicConcurrentChannel<PocoChannelMessage>> _basicConcurrentChannel = new();

        public static Task? RunAsync()
        {
            const int MaxRun = 10;
            int iterations = 0;

            _ = Task.Run(async () =>
            {
                for(int iterations = 0; ; iterations++) 
                {
                    await Task.Delay(3000);
                    _basicConcurrentChannel.Value.Write(new PocoChannelMessage(iterations));

                    if(iterations > MaxRun) { break; }
                }
            });

            while (iterations <= MaxRun)
            {

                Task.Run(async () =>
                {
                    List<PocoChannelMessage> messagePacket = (List<PocoChannelMessage>)await _basicConcurrentChannel.Value.TryReadAsync();

                    if (messagePacket != null && messagePacket.Count > 0)
                    {
                        Console.SetCursorPosition(0, 2);

                        Console.WriteLine(messagePacket[0].ToString());
                    }
                });
            }

            return default;
        }
    }
}
