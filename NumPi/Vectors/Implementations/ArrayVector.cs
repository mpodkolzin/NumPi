using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors.Implementations
{
    public class ArrayVector<T> : IVector<T>
    {
        private ArrayVectorData<T> _data;
        public Type ElementType
        {
            get => typeof(T);
            set { }
        }
        public long Length { get => _data.Values.Count; set { } }

        public IVectorData<T> Data
        {
            //get => new ArrayVectorData<T>(_data.Data);
            get => _data;
            set { }
        }


        public T GetValue(long address)
        {
            return _data[address];
        }

        public IVector<NewT> Convert<NewT>(Func<T, NewT> typeConvertor)
        {
            throw new NotImplementedException();
        }

        public object GetObject(long address)
        {
            return GetValue(address);
        }

        public ArrayVector(ArrayVectorData<T> data)
        {
            //TODO error handling
            _data = data;

        }

    }
}
