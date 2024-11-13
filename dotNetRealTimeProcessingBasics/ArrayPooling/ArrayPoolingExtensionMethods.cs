using dotNetRealTimeProcessingBasics.Contracts;
using dotNetRealTimeProcessingBasics.Implementations;
using dotNetRealTimeProcessingBasics.Shared;
using System.Buffers;
using System.Text;

namespace dotNetRealTimeProcessingBasics.ArrayPooling
{

    public static class ArrayPoolingExtensionMethods
    {
        private readonly static Lazy<IByteCountCalculator<string>> _stringByteCountCalculator = new(() => new StringByteCountCalculator());

        public static byte[] ToByteArray(this string input)
        {
            byte[] destination = [];
            const int MIN_BUFFER_SIZE = 256;
            try
            {
                int inputByteCount = _stringByteCountCalculator.Value.GetByteCount(input);
                MemoryAllocationMetaInformation allocInfo = new("ArrayPoolingExtensionForString::ToByteArray",
                    inputByteCount, MIN_BUFFER_SIZE);

                allocInfo.ToString().DisplayToConsole();

                destination = ArrayPool<byte>.Shared.Rent(allocInfo.AllocatedMinByteBufferSize);
                Encoding.UTF8.GetBytes(input, destination);
                return destination;
            }
            finally
            {
                // need to make sure you return this allocation or else you may end up with a memory leak
                ArrayPool<byte>.Shared.Return(destination);
            }
        }
    }
}
