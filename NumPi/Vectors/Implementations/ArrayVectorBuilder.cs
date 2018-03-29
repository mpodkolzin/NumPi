using NumPi.Vectors.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors
{
    public class ArrayVectorBuilder : IVectorBuilder
    {

        public Task<IVector<T>> AsyncBuild<T>()
        {
            throw new NotImplementedException();
        }

        public IVector<T> Build<T>()
        {
            throw new NotImplementedException();
        }

        public IVector<T> Create<T>(T[] from)
        {
            var data = new ArrayVectorData<T>(from);
            return new ArrayVector<T>(data);
        }


        //TODO moved to generic static class VectorBuilder
        //public static ArrayVectorBuilder Instance;
    }
}
