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


        public object this[KeyT key]
        {
            get
            {
                return GetObject(key);
            }
            set { }
        }

        public object GetObject(KeyT key)
        {
            var address = _index.Lookup(key, LookupSemantics.Exact);
            if(address == null)
            {
                throw new KeyNotFoundException(key.ToString());
            }
            return _vector.GetValue(address.Value);
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

        public Sequence(IEnumerable<KeyT> keys, IEnumerable<ValT> values)
        {
            //TODO add error handling here
            _vectorBuilder = Vectors.VectorBuilder.Instance;
            _indexBuilder = LinearIndexBuilder.Instance;
            _index = Index.ofKeys(keys);
            _vector = _vectorBuilder.Create(values.ToArray());
        }

    }
}
