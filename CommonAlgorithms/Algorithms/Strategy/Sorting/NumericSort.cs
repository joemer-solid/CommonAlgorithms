using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Strategy.Sorting
{
    public sealed class NumericSort : SortingBase<int>, INumericSort
    {
        public override int CompareTo(int other)
        {
            throw new NotImplementedException();
        }

        IEnumerable<int> ISort<IEnumerable<int>, IEnumerable<int>>.Execute(IEnumerable<int> p)
        {
            int[] numericList = p.ToArray<int>();
            int swapBookmark = 0;


            for (int i = 0; i <= numericList.Length - 1; i++)
            {
                for (int j = i + 1; j <= numericList.Length - 1; j++)
                {
                    if (numericList[i] > numericList[j])
                    {
                        swapBookmark = numericList[i];
                        numericList[i] = numericList[j];
                        numericList[j] = swapBookmark;
                        swapBookmark = 0;

                    }
                }
            }

            return numericList;
        }
    }
}
