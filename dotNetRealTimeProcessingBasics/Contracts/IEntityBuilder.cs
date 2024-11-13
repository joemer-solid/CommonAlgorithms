namespace dotNetRealTimeProcessingBasics.Contracts
{
    public interface IEntityBuilder<T,P>
    {
        T Builder(P p);
    }
}
