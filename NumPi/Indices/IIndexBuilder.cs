using NumPi.Sequence.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Indices
{
    public enum BoundaryBehavior
    {
        Inclusive,
        Exclusive
    }

    public interface IRangeBoundary<K>
    {
        BoundaryBehavior BoundaryBehavior { get; set; }
    }

    public class StartAt<K> : IRangeBoundary<K>
    {
        private BoundaryBehavior _boundaryBehavior;
        public BoundaryBehavior BoundaryBehavior
        {
            get => _boundaryBehavior;
            set { }
        }
        public K Start;
    }
    public class EndAt<K> : IRangeBoundary<K>
    {
        private BoundaryBehavior _boundaryBehavior;
        public BoundaryBehavior BoundaryBehavior
        {
            get => _boundaryBehavior;
            set { }
        }
        public K End;
    }
    public class IntervalOf<K> : IRangeBoundary<K>
    {
        private BoundaryBehavior _boundaryBehavior;
        public BoundaryBehavior BoundaryBehavior
        {
            get => _boundaryBehavior;
            set { }
        }
        public K Start;
        public K End;

        public IntervalOf(K start, K end, BoundaryBehavior behavior)
        {
            //TODO add error handling
            Start = start;
            End = end;

            _boundaryBehavior = behavior;

        }

    }




    public interface IIndexBuilder
    {
        IIndex<KeyT> Create<KeyT>(IEnumerable<KeyT> from, bool ordered);
        IIndex<KeyT> Create<KeyT>(IReadOnlyCollection<KeyT> from);
        IIndex<KeyT> Merge<KeyT>(IEnumerable<SequenceConstruction<KeyT>> sequenceConstructions);
        IIndex<KeyT> Recreate<KeyT>(IIndex<KeyT> from);
        SequenceConstruction<KeyT> GetAddressRange<KeyT>(SequenceConstruction<KeyT> indexConstruction, IRangeBoundary<long> range);
        SequenceConstruction<KeyT> GetRange<KeyT>(SequenceConstruction<KeyT> sequenceConstruction, IRangeBoundary<KeyT> range);

    }
}
