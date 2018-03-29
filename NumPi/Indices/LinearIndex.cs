using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Indices
{
    public class LinearIndex<T> : IIndex<T>
    {
        public IReadOnlyCollection<T> Keys { get => _keys; set { } }
        public IEnumerable<T> KeySeq { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long KeyCount { get => (Int64)_keys.Count; set { } }
        public bool IsEmpty { get => !_keys.Any(); set { } }
        public IIndexBuilder Builder { get => _builder; set { } }
        public bool IsOrdered { get; private set; }
        public IEnumerable<KeyValuePair<T, long>> Mappings
        {
            get
            {
                return _keys.Select((k, i) => new KeyValuePair<T, long>(k, i));
            }
            set { }
        }
        public Int64? Lookup(T key, LookupSemantics semantics)
        {
            Int64 val;
            if(lookupTable.TryGetValue(key, out val))
            {
                return val;
            }
            else
            {
                return null;
            }

        }
        public KeyRange<T> KeyRange()
        {
            var minKey = _keys.ElementAt(0);
            var maxKey = _keys.ElementAt(_keys.Count - 1);
            return new KeyRange<T>(minKey, maxKey);
        }

        private IReadOnlyCollection<T> _keys;

        private IIndexBuilder _builder;

        private Dictionary<T, Int64> lookupTable;

        //public LinearIndex(IIndex<T> index, Int64 startAddress, Int64 endAddress)
        //{
        //}

        private void buildLookupTable()
        {
            lookupTable = new Dictionary<T, long>();
            long idx = 0L;
            foreach(var key in _keys)
            {
                if(!lookupTable.ContainsKey(key))
                {
                    lookupTable.Add(key, idx);
                    idx = idx + 1L;
                }
            }
        }

        public LinearIndex(IEnumerable<T> keys, IIndexBuilder builder, bool ordered)
        {
            //TODO add error handling
            _keys = keys.ToList().AsReadOnly();
            _builder = builder;
            buildLookupTable();
        }
        public LinearIndex(IReadOnlyCollection<T> keys, IIndexBuilder builder, bool ordered)
        {
            //TODO add error handling
            _keys = keys;
            _builder = builder;
        }

    }
}
