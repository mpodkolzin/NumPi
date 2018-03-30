using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors
{
    public interface IVectorData<T>
    {
        IReadOnlyList<T> Values { get;}
    }

    public class VectorData<T>
    {
        public IReadOnlyCollection<T> DataList;

        public VectorData(IEnumerable<T> data)
        {
            //TODO add error handling here
            DataList = data.ToList().AsReadOnly();

        }
    }

    public interface IVector
    {
        Type ElementType { get; set; }
        Int64 Length { get; set; }
        object GetObject(long address);


        //Object Data { get; set; }

    }
    public interface IVector<T> : IVector
    {
        //TODO not sure we need VectorData container type here
        IVectorData<T> Data { get; set; }

        //TODO probably need to make it nullable
        T GetValue(Int64 address);

        IVector<NewT> Convert<NewT>(Func<T, NewT> typeConvertor);
        IVector<NewT> Select<NewT>(Func<T, NewT> f);

    }
}
