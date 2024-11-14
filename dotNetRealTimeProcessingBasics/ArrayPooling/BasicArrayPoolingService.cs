using dotNetRealTimeProcessingBasics.Contracts;
using dotNetRealTimeProcessingBasics.Shared;

namespace dotNetRealTimeProcessingBasics.ArrayPooling
{
    public struct BasicArrayPoolingService : IBasicArrayPooling
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

        async Task IBasicArrayPooling.TransformStringToByteArray(string input, IPerformantSerializer<char> arrayPoolingSerializer) 
        {
            await Task.Run(() =>
            {
                input.DisplayToConsole();
                byte[] output = arrayPoolingSerializer.TransformToByteArray(input.ToCharArray());
                output.DisplayToConsole($"{Environment.NewLine}IBasicArrayPooling(ArrayPoolingSerialize).TransformStringToByteArray");

            });
        }
    }
}
