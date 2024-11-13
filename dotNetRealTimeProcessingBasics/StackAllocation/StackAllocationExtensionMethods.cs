using dotNetRealTimeProcessingBasics.Contracts;
using dotNetRealTimeProcessingBasics.Implementations;
using dotNetRealTimeProcessingBasics.Shared;
using System.Text;

namespace dotNetRealTimeProcessingBasics.StackAllocation
{
    public static class StackAllocationExtensionMethods
    {
        private readonly static Lazy<IByteCountCalculator<string>> _stringByteCountCalculator = new(() => new StringByteCountCalculator());

        /// <summary>
        /// Returns a Span<T> and is not allocated on the heap so no GC takes place
        /// Span allocations cannot be returned outside the method scope
        /// Limited memory space
        /// Performs best when size is constant
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string input)
        {
            const int MIN_BUFFER_SIZE = 256;

            int inputByteCount = _stringByteCountCalculator.Value.GetByteCount(input);
            MemoryAllocationMetaInformation allocInfo = new("StackAllocationExtensionForString::ToByteArray",
                inputByteCount, MIN_BUFFER_SIZE);

            allocInfo.ToString().DisplayToConsole();

            Span<byte> destination = stackalloc byte[allocInfo.AllocatedMinByteBufferSize];
            Encoding.UTF8.GetBytes(input, destination);

            return destination.ToArray();

        }
    }
}
