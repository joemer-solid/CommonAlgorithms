using dotNetRealTimeProcessingBasics.Contracts;
using dotNetRealTimeProcessingBasics.Shared;

namespace dotNetRealTimeProcessingBasics.MemoryPooling
{
    public class BasicMemoryPoolingService : IBasicMemoryPooling
    {
        async Task IBasicMemoryPooling.TransformStringToByteArray(string input)
        {
            await Task.Run(() =>
            {
                input.DisplayToConsole();
                byte[] output = input.ToByteArray();
                output.DisplayToConsole($"{Environment.NewLine}IBasicMemoryPooling.TransformStringToByteArray");
            });
        }
    }
}
