using dotNetRealTimeProcessingBasics.ArrayPooling;
using dotNetRealTimeProcessingBasics.Channels.ConcurrentChannelPoco;
using dotNetRealTimeProcessingBasics.Contracts;
using dotNetRealTimeProcessingBasics.MemoryPooling;
using dotNetRealTimeProcessingBasics.Shared;
using dotNetRealTimeProcessingBasics.StackAllocation;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;


namespace dotNetRealTimeProcessingBasics
{
    /// <summary>
    /// https://www.nuget.org/packages/Microsoft.Extensions.Hosting/8.0.1#show-readme-container
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            bool testArrayAndMemoryAllocations = false;

            if (testArrayAndMemoryAllocations)
            {
                ServiceCollection services = new();

                services.AddTransient<IBasicMemoryPooling, BasicMemoryPoolingService>();
                services.AddTransient<IBasicStackAllocation, BasicStackAllocationService>();
                services.AddSingleton<BasicMemoryPoolingTester>();
                services.AddSingleton<BasicStackAllocationTester>();

                var serviceProvider = services.BuildServiceProvider();
                var basicMemoryPoolTester = serviceProvider.GetService<BasicMemoryPoolingTester>();
                var basicStackAllocationTester = serviceProvider.GetService<BasicStackAllocationTester>();

                string arrayPoolTesterInput = "hello pine instruments...greetings from Edinburg PA!";
                string memoryPoolTestInput = "Welcome to the exciting world of real-time features in .NET Core applications! In today's digital landscape, users expect instant updates and seamless interactions.";
                string stackAllocTestInput = "stackalloc - Returns a Span<T> and is not allocated on the heap so no GC takes place. Span allocations cannot be returned outside the method scope.";

                // this is because it is a struct and not a reference type managed on the heap
                BasicArrayPoolingTester basicArrayPoolTester = new(new BasicArrayPoolingService());

                Task.Run(async () =>
                {
                    await basicArrayPoolTester!.TransformStringToByteArray(arrayPoolTesterInput);
                    await basicArrayPoolTester!.PerformantTransformStringToByteArray(arrayPoolTesterInput);
                    await basicMemoryPoolTester!.TransformStringToByteArray(memoryPoolTestInput);
                    await basicStackAllocationTester!.TransformStringToByteArray(stackAllocTestInput);
                }).ContinueWith(trunner =>
                {
                    if (trunner.IsFaulted)
                    {
                        Console.WriteLine(trunner.Exception.Message);
                    }
                    else if (trunner.IsCompletedSuccessfully)
                    {
                        "Task(s) were successfully completed!".DisplayToConsole();
                    }
                });
            }

            TestPocoChannelProducerService();

            Console.ReadLine();
        }

        private static void TestPocoChannelProducerService()
        {
            PocoChannelProducerService.RunAsync();
        }
    } 



    public class BasicStackAllocationTester(IBasicStackAllocation basicStackAllocation)
    {
        private readonly IBasicStackAllocation _basicStackAllocation = basicStackAllocation ?? throw new ArgumentNullException(nameof(basicStackAllocation));

        public Task TransformStringToByteArray(string input)
        {            
            string testId = $"{Environment.NewLine}BasicStackAllocationTester::TransformStringToByteArray Tester";
            testId.DisplayToConsole();
            return _basicStackAllocation.TransformStringToByteArray(input);
        }
    }

    public class BasicArrayPoolingTester(IBasicArrayPooling basicArrayPooling)
    {
        private readonly IBasicArrayPooling _basicArrayPooling = basicArrayPooling ?? throw new ArgumentNullException(nameof(basicArrayPooling));

        public Task TransformStringToByteArray(string input)
        {
            string testId = $"{Environment.NewLine}BasicArrayPoolingTester::TransformStringToByteArray Tester";
            testId.DisplayToConsole();
            return _basicArrayPooling.TransformStringToByteArray(input);
        }

        public Task PerformantTransformStringToByteArray(string input)
        {
            string testId = $"{Environment.NewLine}BasicArrayPoolingTester::PerformantTransformStringToByteArray Tester";
            testId.DisplayToConsole();
            return _basicArrayPooling.TransformStringToByteArray(input, new ArrayPoolingPerformantSerializer());
        }
    }

    public class BasicMemoryPoolingTester(IBasicMemoryPooling basicMemoryPooling)
    {
        private readonly IBasicMemoryPooling _basicMemoryPooling = basicMemoryPooling ?? throw new ArgumentNullException(nameof(basicMemoryPooling));

        public Task TransformStringToByteArray(string input)
        {
            string testId = $"{Environment.NewLine}BasicMemoryPoolingTester::TransformStringToByteArray Tester";
            testId.DisplayToConsole();
            return _basicMemoryPooling.TransformStringToByteArray(input);
        }
    }
}


