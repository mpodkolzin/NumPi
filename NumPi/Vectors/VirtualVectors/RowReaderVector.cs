using NumPi.Common;
using NumPi.Indices;
using NumPi.Vectors.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors.VirtualVectors
{
    public class RowReaderVector<T> : IVector<T>
    {
        public IVectorData<T> Data
        {
            get => new ArrayVectorData<T>(_dataArray);
            set { }
        }
        public Type ElementType { get => typeof(T); set { } }
        public long Length { get => _dataArray.Length; set { } }

        public IVector<NewT> Convert<NewT>(Func<T, NewT> typeConvertor)
        {
            throw new NotImplementedException();
        }

        public T GetValue(long address)
        {
            var column = _parentData.GetValue(address);
            var res = column.GetObject(_rowAddress);
            return TypeConversionUtils.ConvertTo<T>(res, TypeConversionUtils.ConversionType.Safe);
        }

        public object GetObject(long address)
        {
            return GetValue(address);
        }

        private IVector<IVector> _parentData;
        private IVector<T> _baseVector;
        private long _rowAddress;
        private IVectorBuilder _vectorBuilder;

        private T[] _dataArray;

        private void initDataArray()
        {
            _dataArray = new T[_parentData.Length];
            for (long i = 0; i < _parentData.Length; i++)
            {
                _dataArray[i] = this.GetValue(i);
            }


        }

        public IVector<NewT> Select<NewT>(Func<T, NewT> f)
        {
            throw new NotImplementedException();
        }

        public RowReaderVector(IVector<IVector> data, IVectorBuilder vectorBuilder, long rowAddress) 
        {
            //TODO add error handling
            _parentData = data;
            _rowAddress = rowAddress;
            _vectorBuilder = vectorBuilder;
            initDataArray();
        }
    }
}
