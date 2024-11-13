namespace ElectricalMeasurementAlgorithms.Strategy
{
    public interface IStrategy<T,P>
    {
        T Execute(P p);
    }
}
