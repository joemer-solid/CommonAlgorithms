namespace dotNetRealTimeProcessingBasics.Contracts
{
    public interface IPerformantSerializer<P> where P : struct
    {
        byte[] TransformToByteArray(ReadOnlySpan<P> span);
    }
}
