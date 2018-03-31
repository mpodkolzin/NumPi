using NumPi.Sequence.Construction;
using NumPi.Vectors;
using NumPi.Vectors.Construction;
using NumPi.Vectors.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Indices
{
    public class LinearIndexBuilder : IIndexBuilder
    {

        private IVectorBuilder _vecBuilder = null;

        //TODO make it thread safe if needed
        private static LinearIndexBuilder _instance = null;
        //TODO make it thread safe if needed
        public static LinearIndexBuilder Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new LinearIndexBuilder(VectorBuilder.Instance);
                }
                return _instance;
            }
            set { }
        }

        public LinearIndexBuilder(IVectorBuilder vectorBuilder)
        {
            if(vectorBuilder == null)
            {
                throw new ArgumentNullException(nameof(vectorBuilder));
                //TODO Log
            }

        }

        public IIndex<T> Create<T>(IEnumerable<T> from, bool ordered)
        {
            return new LinearIndex<T>(from, this, ordered);
        }

        public IIndex<T> Create<T>(IReadOnlyCollection<T> from)
        {
            throw new NotImplementedException();
            //return new LinearIndex<T>(from, this, ordered);
        }

        public IIndex<T> Merge<T>(IEnumerable<SequenceConstruction<T>> sequenceConstructions)
        {
            foreach(var costr in sequenceConstructions)
            {
                
            }

            throw new NotImplementedException();
        }

        public IIndex<KeyT> Recreate<KeyT>(IIndex<KeyT> from)
        {
            throw new NotImplementedException();
        }

        public SequenceConstruction<KeyT> GetAddressRange<KeyT>(SequenceConstruction<KeyT> indexConstruction, IRangeBoundary<long> range)
        {
            //TODO come up with better type conversions here
            if(range.GetType() == typeof(IntervalOf<long>))
            {
                var newRange = (IntervalOf<long>)range;
                var newVectorConstr = new GetRange(indexConstruction.VectorConstruction, new IntervalOf<long>(newRange.Start, newRange.End, BoundaryBehavior.Inclusive));
                var newIndex = new LinearRangeIndex<KeyT>(indexConstruction.Index, newRange.Start, newRange.End);
                return new SequenceConstruction<KeyT>(newIndex, newVectorConstr);

            }
            return null;
        }


        public SequenceConstruction<KeyT> GetRange<KeyT>(SequenceConstruction<KeyT> sequenceConstruction, IRangeBoundary<KeyT> range)
        {
            long minAddr = 0L;
            long maxAddr = (sequenceConstruction.Index.KeyCount - 1);

            long loBound = 0;
            long hiBound = 0;

            if(range.GetType() == typeof(IntervalOf<KeyT>))
            {
                var newRange = (IntervalOf<KeyT>)range;
                //TODO support other lookup semantics
                //TODO add error handling missing keys here
                loBound = sequenceConstruction.Index.Lookup(newRange.Start, LookupSemantics.Exact).Value;
                hiBound = sequenceConstruction.Index.Lookup(newRange.End, LookupSemantics.Exact).Value;

            }

            //TODO add checks for missing keys
            if(hiBound > loBound)
            {
                var newIndex = new LinearRangeIndex<KeyT>(sequenceConstruction.Index, loBound, hiBound);
                IVecConstructionCmd newVectorContr = new GetRange(sequenceConstruction.VectorConstruction, new IntervalOf<Int64>(hiBound, loBound, BoundaryBehavior.Inclusive));
                return new SequenceConstruction<KeyT>(newIndex, newVectorContr);

            }
            else
            {
                //TODO reversed range handling
            }

            //TODO
            return null;
            
            
        }
    }
}
