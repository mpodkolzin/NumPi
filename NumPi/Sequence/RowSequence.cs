using NumPi.Indices;
using NumPi.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Sequence
{
    public class RowSequence<TRowKey, TColumnKey> : Sequence<TRowKey, ObjectSequence<TColumnKey>>
    {
        public RowSequence(IIndex<TRowKey> index, IVector<ObjectSequence<TColumnKey>> vector, IVectorBuilder vectorBuilder, IIndexBuilder indexBuilder)
            :base(index, vector, vectorBuilder, indexBuilder)
        {

        }
    }
}
