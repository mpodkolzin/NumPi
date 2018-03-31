using NumPi.Indices;
using NumPi.Vectors.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Sequence.Construction
{
    public class SequenceConstruction<T>
    {
        public IIndex<T> Index;
        public IVecConstructionCmd VectorConstruction;

        public SequenceConstruction(IIndex<T> index, IVecConstructionCmd vectorConstruction)
        {
            //TODO add error handling
            Index = index;
            VectorConstruction = vectorConstruction;
        }

    }
}
