using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Indices
{
    public class LinearRangeIndex<KeyT> : IIndex<KeyT>
    {
        public IReadOnlyCollection<KeyT> Keys { get => _rangeIndex.Keys; set { } }
        public IEnumerable<KeyT> KeySeq { get => _rangeIndex.Keys; set { } }
        public long KeyCount { get => _endAddress - _startAddress + 1L; set { } }
        public bool IsEmpty { get => _endAddress < _startAddress; set { } }
        public IIndexBuilder Builder
        {
            get
            {
                if(_rangeIndex == null)
                {
                    initialize();
                }

                return _rangeIndex.Builder;
            }
            set { }
        }
        public IEnumerable<KeyValuePair<KeyT, long>> Mappings { get => _rangeIndex.Mappings; set { } }

        public bool IsOrdered()
        {
            return true;
        }

        public KeyRange<KeyT> KeyRange()
        {
            //TODO
            throw new NotImplementedException();
        }

        public long? Lookup(KeyT key, LookupSemantics semantics) => _rangeIndex.Lookup(key, semantics);

        private IIndex<KeyT> _rangeIndex;
        private IIndex<KeyT> _parentIndex;
        private Int64 _startAddress;
        private Int64 _endAddress;

        public LinearRangeIndex(IIndex<KeyT> index, Int64 startAddress, Int64 endAddress)
        {
            //TODO add error handling here

            _parentIndex = index;
            _startAddress = startAddress;
            _endAddress = endAddress;

            initialize();
        }

        private void initialize()
        {
            //TODO add safety checks
            var newKeys = new KeyT[this.KeyCount];

            for (int i = 0; i < this.KeyCount; i++)
            {
                //not particularly safe conversion;
                newKeys[i] = _parentIndex.Keys.ElementAt((int)(_startAddress + i));
            }

            _rangeIndex = _parentIndex.Builder.Create<KeyT>(newKeys, true);
        }
    }
}
