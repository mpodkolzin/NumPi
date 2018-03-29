using NumPi.Sequence.Construction;
using NumPi.Vectors;
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
    }
}
