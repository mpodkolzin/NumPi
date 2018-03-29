using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Indices
{
    public static class IndexExtensions
    {
        public static IIndex<T> ofKeys<T>(this IIndex<T> index, IEnumerable<T> keys)
        {
            return LinearIndexBuilder.Instance.Create<T>(keys, false);
        }
    }
    public static class Index
    {
        public static IIndex<T> ofKeys<T>(IEnumerable<T> keys)
        {
            return LinearIndexBuilder.Instance.Create<T>(keys, false);
        }
    }
}
