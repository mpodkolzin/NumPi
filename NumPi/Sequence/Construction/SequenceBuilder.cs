using NumPi.Indices;
using NumPi.Vectors;
using NumPi.Vectors.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Sequence.Construction
{
    public class SequenceBuilder<K, V> : IDictionary<K,V>
    {
        //TODO is IList good enough?
        private IList<K> _keys;
        private IList<V> _values;

        public SequenceBuilder()
        {
            _keys = new List<K>();
            _values = new List<V>();
        }

        public V this[K key] { get => throw new NotImplementedException("Indexer is not supported yet"); set => throw new NotImplementedException("Indexer is not supported yet"); }

        private void add(K key, V val)
        {
            //TODO add error handling here
            _keys.Add(key);
            _values.Add(val);
        }

        public Sequence<K, V> Sequence => 
            new Sequence<K, V>(Index.ofKeys(_keys), Vector.ofValues(_values.ToArray()), VectorBuilder.Instance, LinearIndexBuilder.Instance);

        public ICollection<K> Keys => _keys.ToList();

        public ICollection<V> Values => _values.ToList();

        public int Count => _keys.Count();

        public bool IsReadOnly => false;

        public void Add(K key, V value)
        {
            this.add(key, value);
        }

        public void Add(KeyValuePair<K, V> item)
        {
            this.add(item.Key, item.Value);
        }

        public void Clear()
        {
            _keys.Clear();
            _values.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            //TODO!!!
            throw new NotImplementedException();
        }

        public bool ContainsKey(K key)
        {
            //TODO revisit: O(n) operation
            return _keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return Enumerable.Zip(_keys, _values, (k, v) => new KeyValuePair<K, V>(k, v)).GetEnumerator();
        }

        public bool Remove(K key)
        {
            //TODO
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            //TODO
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, out V value)
        {
            //TODO
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
