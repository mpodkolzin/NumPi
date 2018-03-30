using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumPi.Indices;
using NumPi.Vectors;

namespace NumPi.Sequence
{
    public class Sequence<KeyT, ValT> : ISequence<KeyT>
    {
        public IVector<ValT> Vector { get => _vector; set { } }
        public IIndex<KeyT> Index { get => _index; set { } }
        public IVectorBuilder VectorBuilder { get => _vectorBuilder; set { } }
        public IEnumerable<KeyT> Keys => _index.Keys;
        public IEnumerable<ValT> Values => _index.Mappings.Select(kv => _vector.GetValue(kv.Value));

        public IIndexBuilder IndexBuilder => _indexBuilder;

        IVector ISequence<KeyT>.Vector { get => _vector; set { } }

        public object TryGetObject(KeyT key)
        {
            var address = _index.Lookup(key, LookupSemantics.Exact);
            if(address == null)
            {
                throw new KeyNotFoundException(key.ToString());
            }
            return _vector.GetValue(address.Value);
        }

        object ISequence<KeyT>.TryGetObject(KeyT key)
        {
            throw new NotImplementedException();
        }

        private IVectorBuilder _vectorBuilder;
        private IIndexBuilder _indexBuilder;
        private IIndex<KeyT> _index;
        private IVector<ValT> _vector;



        public Sequence(IIndex<KeyT> index, IVector<ValT> vector, IVectorBuilder vectorBuilder, IIndexBuilder indexBuilder)
        {
            //TODO add error handling here
            _index = index;
            _vector = vector;
            _vectorBuilder = vectorBuilder;
            _indexBuilder = indexBuilder;
        }

    }
}
