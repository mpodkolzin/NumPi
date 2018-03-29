using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors.Implementations
{
    public class ArrayVectorData<T> : IVectorData<T>
    {
        public IReadOnlyList<T> Values { get => Array.AsReadOnly(_data); private set { } }

        private T[] _data;

        public Int64 Length => _data.LongLength;

        public T this[long index]
        {
            get => _data[index];
        }

        public ArrayVectorData(T[] data)
        {
            //TODO error handling
            _data = data;

        }
    }
}
