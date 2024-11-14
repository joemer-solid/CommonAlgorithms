using System.Threading.Channels;

namespace dotNetRealTimeProcessingBasics.Contracts
{
    public interface IBoundedChannel<P>
    {
        Task Process(ChannelReader<P> reader);
        ValueTask TryWriteAsync(P item, CancellationToken cancellationToken);
        ValueTask<P> ReadAsync(CancellationToken cancellationToken);
    }
}
