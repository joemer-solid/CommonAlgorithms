using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Strategy.Search
{
    public abstract class SearchBase<P>
    {
        protected abstract IEnumerable<P> GetSortedList(IEnumerable<P> listToSort);
       
    }
}
