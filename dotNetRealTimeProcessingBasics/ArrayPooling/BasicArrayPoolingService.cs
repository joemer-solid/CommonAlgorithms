using dotNetRealTimeProcessingBasics.Contracts;
using dotNetRealTimeProcessingBasics.Shared;

namespace dotNetRealTimeProcessingBasics.ArrayPooling
{
    public class BasicArrayPoolingService : IBasicArrayPooling
    {
        async Task IBasicArrayPooling.TransformStringToByteArray(string input)
        {
            await Task.Run(() =>
            {
                input.DisplayToConsole();
                byte[] output = input.ToByteArray();
                output.DisplayToConsole($"{Environment.NewLine}IBasicArrayPooling.TransformStringToByteArray");
            });
        }
    }
}
