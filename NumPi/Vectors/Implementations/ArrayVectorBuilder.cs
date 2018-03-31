using NumPi.Indices;
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

        public IVector<T> Build<T>(IVecConstructionCmd vectorConstruction, IVector<T>[] vectors)
        {
            if(vectorConstruction.GetType() == typeof(Return))
            {
                var vcReturn = (Return)vectorConstruction;
                return vectors[vcReturn.VectorLocation];

            }
            else if(vectorConstruction.GetType() == typeof(Combine))
            {
                var combine = (Combine)vectorConstruction;
                var data = combine.VecConstructionCmds.Select(vc => (IVector)VectorBuilderInstance.Build(vc, vectors)).ToArray();
                var frameData = VectorBuilderInstance.Create(data);
                var rowReaders = new object[combine.Size];
                for(long i = 0; i < combine.Size; i++)
                {
                    rowReaders[i] = new RowReaderVector<object>(frameData, VectorBuilderInstance, i);
                }

                var objVec = new ArrayVector<object>(new ArrayVectorData<object>(rowReaders));
                //var objectSeq = new ObjectSequence<T>(objVec);
                return Vector.unboxVector<T>(objVec, VectorBuilderInstance);
            }
            else if(vectorConstruction.GetType() == typeof(GetRange))
            {


                var cmd = (GetRange)vectorConstruction;
                //TODO
                var range = (IntervalOf<Int64>)cmd.RangeBoundary;
                var newVecData = this.buildArrayVector(cmd.VecConstructionCmd, vectors);
                var newData = new T[(range.End - range.Start + 1)];

                long idx = 0;
                for(long i = range.Start; i <= range.End; i++)
                {
                    newData[idx] = newVecData[i];
                    idx++;
                }
                return new ArrayVector<T>(new ArrayVectorData<T>(newData));

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

        private  ArrayVectorData<T> buildArrayVector<T>(IVecConstructionCmd commands, IVector<T>[] vectors)
        {
            var res = Build(commands, vectors);
            if(res.GetType() == typeof(ArrayVector<T>))
            {
                return (ArrayVectorData<T>)res.Data;

            }
            //TODO other cases
            else
            {

            }
            return null;
        }


        IVector Build(IVecConstructionCmd vectorConstruction, IVector[] vectors)
        {
            return null;

        }

    }
}
