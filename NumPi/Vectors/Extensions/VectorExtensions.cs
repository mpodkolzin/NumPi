using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors.Extensions
{
    public static class Vector
    {
        public static IVector<T> ofValues<T>(this IVector vector, T[] data)
        {
            return VectorBuilder.Instance.Create<T>(data);
        }
        public static IVector<T> ofValues<T>(T[] data)
        {
            return VectorBuilder.Instance.Create<T>(data);
        }
    }
}
