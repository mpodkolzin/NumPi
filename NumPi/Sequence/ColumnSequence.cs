using NumPi.Indices;
using NumPi.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Sequence
{
    public class ColumnSequence<TRowKey, TColumnKey> : Sequence<TColumnKey, ObjectSequence<TRowKey>>
    {
        public ColumnSequence(IIndex<TColumnKey> index, IVector<ObjectSequence<TRowKey>> vector, IVectorBuilder vectorBuilder, IIndexBuilder indexBuilder):
            base(index, vector, vectorBuilder, indexBuilder)
        {

        }
        public ColumnSequence(Sequence<TColumnKey, ObjectSequence<TRowKey>> sequence):base(sequence.Index, sequence.Vector, sequence.VectorBuilder, sequence.IndexBuilder)
        {

        }
    }
}
