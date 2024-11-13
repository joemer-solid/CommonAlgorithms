using dotNetRealTimeProcessingBasics.Contracts;
using dotNetRealTimeProcessingBasics.Implementations;
using dotNetRealTimeProcessingBasics.Shared;
using System.Buffers;
using System.Text;

namespace dotNetRealTimeProcessingBasics.MemoryPooling
{

    public static class MemoryPoolingExtensionMethods
    {
        private readonly static Lazy<IByteCountCalculator<string>> _stringByteCountCalculator = new(() => new StringByteCountCalculator());

        /// <summary>
        /// Uses StackAlloc() memory - not allocated on the heap, performs best when size is constant, limited memory space (few kilobytes allocated for this)
        /// Memory pool using ArrayPool under the covers, but eliminates the need for returning allocation back to pool by utilizing IDisposable
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string input)
        {
            IMemoryOwner<byte> destination;

            int inputByteCount = _stringByteCountCalculator.Value.GetByteCount(input);
            MemoryAllocationMetaInformation allocInfo = new("MemoryPoolingExtensionForString::ToByteArray",
                inputByteCount, inputByteCount);

            allocInfo.ToString().DisplayToConsole();

            using (destination = MemoryPool<byte>.Shared.Rent(allocInfo.AllocatedMinByteBufferSize))
            {
                Encoding.UTF8.GetBytes(input, destination.Memory.Span);
                return destination.Memory.ToArray();
            }
        }
    }
}
