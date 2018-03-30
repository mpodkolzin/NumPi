using NumPi.Sequence;
using NumPi.Vectors.Construction;
using NumPi.Vectors.Extensions;
using NumPi.Vectors.Implementations;
using NumPi.Vectors.VirtualVectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors
{
    public class ArrayVectorBuilder : IVectorBuilder
    {
        public static IVectorBuilder VectorBuilderInstance = new ArrayVectorBuilder();

        public Task<IVector<T>> AsyncBuild<T>()
        {
            throw new NotImplementedException();
        }

        public IVector<T> Build<T>(IVectorConstruction vectorConstruction, IVector<T>[] vectors)
        {
            if(vectorConstruction.GetType() == typeof(Return))
            {
                var vcReturn = (Return)vectorConstruction;
                return vectors[vcReturn.VectorLocation];

            }
            if(vectorConstruction.GetType() == typeof(Combine))
            {
                var combine = (Combine)vectorConstruction;
                var data = combine.VectorConstruction.Select(vc => (IVector)VectorBuilderInstance.Build(vc, vectors)).ToArray();
                var frameData = VectorBuilderInstance.Create(data);
                var rowReaders = new object[combine.Size];
                for(long i = 0; i < combine.Size; i++)
                {
                    rowReaders[i] = new RowReaderVector<object>(frameData, VectorBuilderInstance, i);
                }

                var objVec = new ArrayVector<object>(new ArrayVectorData<object>(rowReaders));
                return Vector.unboxVector<T>(objVec, VectorBuilderInstance);
            }
            else
            {
                return null;
            }

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
