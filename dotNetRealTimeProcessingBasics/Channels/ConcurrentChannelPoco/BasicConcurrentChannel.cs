using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNetRealTimeProcessingBasics.Channels.ConcurrentChannelPoco
{
    public class BasicConcurrentChannel<T>
    {
        /// <summary>
        /// Represents a thread-safe FIFO collection
        /// Contextually represents the Channel's backing store
        /// </summary>
        private readonly ConcurrentQueue<T> ChannelQueue = new();

        /// <summary>
        /// Coordinates the notification process when something is added to the queue
        /// used to implement thread synchronization
        /// Using Lock and Monitor, only one internal thread can access our application code at any given point in time. 
        /// But, if we want more control over the number of internal threads that can access our application code, 
        /// then we need to use SemaphoreSlim class in C#. For a better understanding, please have a look at the below image.
        /// consumers calling the semaphore in its initial state will have to wait for an item ('key') to be enqueued
        /// </summary>
        private readonly SemaphoreSlim Semaphore = new(0);

        public void Write(T item)
        {
            ChannelQueue.Enqueue(item);
            // insert a new 'key' or process that will then be accessible via read async
            // releasing the item from the queue to the sempahore slim
            Semaphore.Release();
        }

        /// <summary>
        /// Using the maybe pattern, if the item is null then an empty result will be returned to the consumer
        /// so they won't have to check for null
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> TryReadAsync()
        {
            Task<List<T>> readResultTask = Task.FromResult(new List<T>());

            // this will block if the semaphore has a count of zero
            await Semaphore.WaitAsync();
            ChannelQueue.TryDequeue(out var item);

            if(item is not null)
            {
               readResultTask.Result.Add(item);
            }

            return readResultTask.Result;            
        }
    }
}
