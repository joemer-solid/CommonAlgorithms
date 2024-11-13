using dotNetRealTimeProcessingBasics.Contracts;
using dotNetRealTimeProcessingBasics.Implementations;
using System.Text;

namespace dotNetRealTimeProcessingBasics.Shared
{
    public struct MemoryAllocationMetaInformation
    {
        

        public MemoryAllocationMetaInformation(string operationName, int inputByteCount, int allocatedMinByteBufferSize)
        {
            OperationName = operationName;
            InputByteCount = inputByteCount;
            AllocatedMinByteBufferSize = allocatedMinByteBufferSize;
        }

        public int InputByteCount { get; init; }

        public int AllocatedMinByteBufferSize { get; init; }

        public string OperationName { get; init; }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Operation Name: {OperationName ?? string.Empty}");
            sb.AppendLine($"Input Byte Count: {InputByteCount}");
            sb.AppendLine($"Allocated Min. Buffer Size {AllocatedMinByteBufferSize}");

            return sb.ToString();
        }
    }
}
