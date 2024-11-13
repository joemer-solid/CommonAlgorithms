using System.Text;
using dotNetRealTimeProcessingBasics.Contracts;

namespace dotNetRealTimeProcessingBasics.Implementations
{
    public sealed class StringByteCountCalculator : IByteCountCalculator<string>
    {
        int IByteCountCalculator<string>.GetByteCount(string p)
        {
            int size = 0;

            if (!string.IsNullOrEmpty(p))
            {
                size = Encoding.Unicode.GetByteCount(p);
            }

            return size;
        }
    }
}
