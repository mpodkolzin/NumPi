using NumPi.Reflection;
using NumPi.Sequence;
using NumPi.Vectors.Extensions;
using NumPi.Vectors.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Extensions.FrameExtensions
{
    public class FrameExtensions
    {
        public static DataFrame<int, string> FromRecords<T>(IEnumerable<T> values) 
        {
            //TODO add support for generic row indexes
            return FrameReflectionUtils.ConvertRecordSequence<T>(values);
        }

        public static T[,] ToArray2D<T,R,C>(DataFrame<R,C> frame)
        {
            var res = VectorUtils.ToArray2D<T>(frame.RowCount, frame.ColumnCount, frame.Data);
            return res;

        }

        public static void FromRows<K, V>(IEnumerable<Tuple<K, ISequence<V>>> rows)
        {
        }
    }
}
