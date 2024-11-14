using dotNetRealTimeProcessingBasics.Contracts;
using System.Buffers;
using System.Buffers.Binary;
using System.Text;

namespace dotNetRealTimeProcessingBasics.ArrayPooling
{
    public struct ArrayPoolingPerformantSerializer : IPerformantSerializer<char>
    {
        byte[] IPerformantSerializer<char>.TransformToByteArray(ReadOnlySpan<char> span)
        {
            byte[] destination = [];
            const int MIN_BUFFER_SIZE = 256;
            try
            {
                destination = ArrayPool<byte>.Shared.Rent(MIN_BUFFER_SIZE);
                Encoding.UTF8.GetBytes(span.ToArray(), destination);
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

