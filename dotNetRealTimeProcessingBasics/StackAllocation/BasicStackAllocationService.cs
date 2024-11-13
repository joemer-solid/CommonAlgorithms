using dotNetRealTimeProcessingBasics.Contracts;
using dotNetRealTimeProcessingBasics.Shared;

namespace dotNetRealTimeProcessingBasics.StackAllocation
{
    public class BasicStackAllocationService : IBasicStackAllocation
    {
        async Task IBasicStackAllocation.TransformStringToByteArray(string input)
        {
            await Task.Run(() =>
            {
                input.DisplayToConsole();
                byte[] output = input.ToByteArray();
                output.DisplayToConsole($"{Environment.NewLine}IBasicStackAllocation.TransformStringToByteArray");
            });
        }
    }
}
