using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumPi.Vectors.Extensions;

namespace NumPi.Vectors.Helpers
{
    public static class VectorUtils
    {
        public static T[,] ToArray2D<T>(long rowCount, long colCount, IVector<IVector> data)
        {
            var res = new T[rowCount, colCount];
            var d = data.Data.Values;
            for (int i = 0; i < rowCount; i++)
            {
                var vec = data.Data.Values[i];
                var boxedVector = Vector.BoxVector(vec, VectorBuilder.Instance);
                for (int j = 0; j < colCount; j++)
                {
                    var val = boxedVector.Data.Values[j];
                    if(val != null)
                    {
                        res[i, j] = (T)Convert.ChangeType(val, typeof(T));
                    }

                }
            }

            return res;

            //foreach (var vec in data.Data.Values)
            //{
            //    var boxedVector = Vector.BoxVector(vec, VectorBuilder.Instance);
            //}

        }
    }
}
