using dotNetRealTimeProcessingBasics.ArrayPooling;

namespace dotNetRealTimeProcessingBasics.Contracts
{
    public interface IBasicArrayPooling
    {
        Task TransformStringToByteArray(string input);
       
        Task TransformStringToByteArray(string input, IPerformantSerializer<char> arrayPoolingSerializer);
    }
}
