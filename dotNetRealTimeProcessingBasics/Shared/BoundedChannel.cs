using System.Threading.Channels;

namespace dotNetRealTimeProcessingBasics.Shared
{
    

    /// <summary>
    /// The preceding code creates a channel that has a maximum capacity of n items.     
    /// When you create a bounded channel, the channel is bound to a maximum capacity. 
    /// When the bound is reached, the default behavior is that the channel asynchronously 
    /// blocks the producer until space becomes available. You can configure this behavior by 
    /// specifying an option when you create the channel. 
    /// Bounded channels can be created with any capacity value greater than zero. 
    /// For other examples, see Bounded creation patterns.
    /// see: https://devblogs.microsoft.com/dotnet/an-introduction-to-system-threading-channels/
    /// </summary>
    /// <typeparam name="P"></typeparam>
    public abstract class BoundedChannel<P> : IBoundedChannel<P> where P : struct
    {
        #region CTOR
        public BoundedChannel(int channelCapacity, string channelName)
        {
            ProcessChannel = Channel.CreateBounded<P>(new BoundedChannelOptions(channelCapacity)
            {
                 AllowSynchronousContinuations  = false,
                 SingleWriter = true,
                 SingleReader = false,
                 FullMode = BoundedChannelFullMode.DropWrite
            });

            ChannelName = channelName ?? $"Unnamed BoundedChannel<{nameof(P)}>";
        }

        #endregion

        #region Properties

        public string ChannelName { get; private set; }

        public Channel<P> ProcessChannel { get; private set; }

        #endregion

        #region IBoundedChannel implementation

        public abstract Task Process(ChannelReader<P> reader);

        /// <summary>
        /// TryRead will try to synchronously extract the next element from the channel, returning whether it was successful in doing so. 
        /// ReadAsync will also extract the next element from the channel, but if an element can’t be retrieved synchronously, it will return a
        /// task for that element. And WaitToReadAsync returns a ValueTask<bool> that serves as a notification for when an element is available 
        /// to be consumed. 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ChannelClosedException"></exception>
        public virtual async ValueTask<P> ReadAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (!await ProcessChannel.Reader.WaitToReadAsync(cancellationToken).ConfigureAwait(false))
                    throw new ChannelClosedException();

                if (ProcessChannel.Reader.TryRead(out P item))
                    return item;
            }
        }

        /// <summary>
        /// WriteAsync is virtual. Some implementations may choose to provide a more optimized implementation, but with abstract 
        /// TryWrite and WaitToWriteAsync,
        /// while loop: because channels by default can be used by any number of producers and any number of consumers concurrently.
        ///  also highlights why WaitToWriteAsync returns a ValueTask<bool> instead of just ValueTask, as well as situations beyond 
        /// a full buffer in which TryWrite may return false
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ChannelCompletedException"></exception>
        public virtual async ValueTask TryWriteAsync(P item, CancellationToken cancellationToken)
        {
            while (await ProcessChannel.Writer.WaitToWriteAsync(cancellationToken).ConfigureAwait(false))
            {
                if (ProcessChannel.Writer.TryWrite(item))
                    return;
            }
        }

        #endregion

        #region Callbacks / event handlers

        /// <summary>
        /// Whenever the channel is full and a new item is added, the itemDropped callback is invoked.
        /// In this example, the provided callback writes the item to the console, 
        /// but you're free to take any other action you want.
        /// </summary>
        /// <param name="p"></param>
        protected virtual void Dropped(P p)
            => $"Channel Capacity Is Reached Item Dropped: {p}".DisplayToConsole();

        #endregion
    }
}
