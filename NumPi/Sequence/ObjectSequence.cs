using NumPi.Common;
using NumPi.Indices;
using NumPi.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Sequence
{
    public class ObjectSequence<KeyT> : Sequence<KeyT, object>
    {
        public ObjectSequence(IIndex<KeyT> index, IVector<object> vector, IVectorBuilder vectorBuilder, IIndexBuilder indexBuilder)
            :base(index, vector, vectorBuilder, indexBuilder)
        {

        }

        public ObjectSequence(Sequence<KeyT, object> seq):base(seq.Index, (IVector<object>)seq.Vector, seq.VectorBuilder, seq.IndexBuilder)
        {

        }

        public IEnumerable<NewT> GetValues<NewT>()
        {
            //TODO THIS IS NOT SAFE METHOD
            return this.Values.Select(v => TypeConversionUtils.ConvertTo<NewT>(v, TypeConversionUtils.ConversionType.Safe));
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public void HiThere()
        //{

        //}
    }
}
