using NumPi.Indices;
using NumPi.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Sequence
{
    public interface ISequence<KeyT>
    {
        IVector Vector { get; set; }
        IIndex<KeyT> Index { get; set; }
        IVectorBuilder VectorBuilder { get; set; }
        object GetObject(KeyT key);
        object this[KeyT index]
        {
            get;
            set;
        }

    }
}
