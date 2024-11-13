using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonAlgorithms.ArraySegments
{
    public interface IArraySegmentBuilder<T>
    {
        Task<IEnumerable<ArraySegment<T>>> Build(int bufferSize);
    }
}
