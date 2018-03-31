using NumPi.Indices;
using NumPi.Vectors.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors.Extensions
{
    public static class Vector
    {
        public static IVector<T> ofValues<T>(this IVector vector, T[] data)
        {
            return VectorBuilder.Instance.Create<T>(data);
        }
        public static IVector<T> ofValues<T>(T[] data)
        {
            return VectorBuilder.Instance.Create<T>(data);
        }

        //TODO method should go to helper module or VectorModule
        //TODO probably not very efficient
        public static IVector<object> BoxVector(IVector vector, IVectorBuilder vectorBuilder)
        {
            switch (Type.GetTypeCode(vector.ElementType))
            {
                case TypeCode.String:
                    var strVec = (IVector<string>)vector;
                    var boxedVals = strVec.Data.Values.Select(v => (object)v).ToArray();
                    var boxedVec = vectorBuilder.Create<object>(boxedVals);
                    return boxedVec;
                case TypeCode.Int32:
                    var intVec = (IVector<int>)vector;
                    var boxedValsInt = intVec.Data.Values.Select(v => (object)v).ToArray();
                    var boxedVecInt = vectorBuilder.Create<object>(boxedValsInt);
                    return boxedVecInt;
                default:
                    return null;
            }
        }
        public static IVector TransformColumn(IVector vector, IVectorBuilder vectorBuilder, IVecConstructionCmd vectorConstr)
        {
            var boxedVector = BoxVector(vector, vectorBuilder);

            var newVec = vectorBuilder.Build(vectorConstr, new IVector<object>[] { boxedVector });
            return newVec;
            //switch (Type.GetTypeCode(vector.ElementType))
            //{
            //    case TypeCode.String:
            //        var strVec = (IVector<T>)vector;
            //        var boxedVals = strVec.Data.Values.Select(v => (object)v).ToArray();
            //        var boxedVec = vectorBuilder.Create<object>(boxedVals);
            //        return strVec;
            //    case TypeCode.Int32:
            //        var intVec = (IVector<T>)vector;
            //        var boxedValsInt = intVec.Data.Values.Select(v => (object)v).ToArray();
            //        var boxedVecInt = vectorBuilder.Create<object>(boxedValsInt);
            //        return intVec;
            //    default:
            //        return null;
            //}
        }

        //TODO unsafe
        public static IVector<T> unboxVector<T>(IVector<object> vector, IVectorBuilder vectorBuilder)
        {
            var unboxedVals = vector.Data.Values.Select(v => (T)v).ToArray();
            var unboxedVec = vectorBuilder.Create<T>(unboxedVals);
            return unboxedVec;
        }


        public static IVector<T> CreateRowVector<T>(IVectorBuilder vectorBuilder, long rowKeyCount, long colKeyCount, Func<object, T> mapper, IVector<IVector> data)
        {
            var boxedData = data.Select(v => BoxVector(v, vectorBuilder)).Data.Values.ToArray();
            var vectorsConstr = new List<IVecConstructionCmd>();

            for(long i = 0; i < colKeyCount; i++)
            {
                vectorsConstr.Add(new Return(i));
            }

            var cmd = new Combine(rowKeyCount, vectorsConstr);

            var boxedVec = vectorBuilder.Build(cmd, boxedData);
            var resVec = boxedVec.Select(v => mapper.Invoke(v));

            return resVec;

        }
    }
}
