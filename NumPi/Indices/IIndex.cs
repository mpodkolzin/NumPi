using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Indices
{

    public enum LookupSemantics
    {
        Exact,//only exact is supported so far
        ExactOrGrater,
        ExactOrSmaller
    }

    public struct KeyRange<T>
    {
        public T MinKey;
        public T MaxKey;

        public KeyRange(T minKey, T maxKey)
        {
            MinKey = minKey;
            MaxKey = maxKey;
        }
    }

    public interface IIndex<T>
    {
        IReadOnlyCollection<T> Keys { get; set; }
        IEnumerable<T> KeySeq { get; set; }
        Int64 KeyCount { get; set; }
        bool IsEmpty { get; set; }
        IIndexBuilder Builder { get; set; }
        /// Returns all key-address mappings in the index
        IEnumerable<KeyValuePair<T, Int64>> Mappings { get; set; }
        Int64? Lookup(T key, LookupSemantics semantics);
        KeyRange<T> KeyRange();
        bool IsOrdered();
    }
}
