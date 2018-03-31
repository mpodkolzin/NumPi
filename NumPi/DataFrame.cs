using NumPi.Indices;
using NumPi.Sequence;
using NumPi.Sequence.Construction;
using NumPi.Vectors;
using NumPi.Vectors.Construction;
using NumPi.Vectors.Extensions;
using NumPi.Vectors.VirtualVectors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi
{

    public class ColumnDefinition
    {
        public Type Type;
        public IVector Values;

        public ColumnDefinition(Type type, IVector vector)
        {
            Type = type;
            Values = vector;

        }
    }


    public class FrameData
    {
        public IEnumerable<object[]> ColumnKeys;
        public IEnumerable<object[]> RowKeys;
        public IEnumerable<ColumnDefinition> Columns;

        public FrameData()
        {
            //TODO add injection and error handling

        }

    }

    public class DataFrame<TRowKey, TColumnKey>
    {
        private IIndex<TRowKey> _rowIndex;
        private IIndex<TColumnKey> _columnIndex;
        private IVector<IVector> _data;
        private IVectorBuilder _vectorBuilder;
        private IIndexBuilder _indexBuilder;


        public int RowCount;// => TODO
        public int ColumnCount;// => TODO


        public IEnumerable<TRowKey> RowKeys => _rowIndex.Keys;
        public IEnumerable<TColumnKey> ColumnKeys => _columnIndex.Keys;
        public IEnumerable<Type> ColumnTypes => _columnIndex.Mappings.Select(kvp => _data.GetValue(kvp.Value).ElementType);
        public bool IsEmpty  => !_rowIndex.Mappings.Any();

        //public GetRowKeyAt(Int64 address) => 

        public DataFrame(IIndex<TRowKey> rowIndex, IIndex<TColumnKey> columnIndex, IVector<IVector> data, IIndexBuilder indexBuilder, IVectorBuilder vectorBuilder)
        {
            //TODO add error handling
            _data = data;
            _rowIndex = rowIndex;
            _columnIndex = columnIndex;
            _vectorBuilder = vectorBuilder;
            _indexBuilder = indexBuilder;

        }
        public DataFrame<TRowKey, TColumnKey> Merge(DataFrame<TRowKey, TColumnKey> otherFrame)
        {
            return null;
        }

        public FrameData GetFrameData()
        {

            var res = new FrameData();
            res.ColumnKeys = _columnIndex.Mappings.Select(kv => new object[] { kv.Key });
            res.RowKeys = _rowIndex.Mappings.Select(kv => new object[] { kv.Key });
            res.Columns = _data.Data.Values.Select(d => createColDefinition(d));

            return res;
        }



        public RowSequence<TRowKey, TColumnKey> Rows
        {
            get
            {
                Func<object, ObjectSequence<TColumnKey>> mapper = (vecReader) =>
                {
                    return new ObjectSequence<TColumnKey>(_columnIndex, (IVector<object>)vecReader, _vectorBuilder, _indexBuilder);
                };
                
                var vec = Vector.CreateRowVector<ObjectSequence<TColumnKey>>(_vectorBuilder, _rowIndex.KeyCount, _columnIndex.KeyCount, mapper, _data);
                return new RowSequence<TRowKey, TColumnKey>(_rowIndex, vec, _vectorBuilder, _indexBuilder);
            }
        }

        public ColumnSequence<TRowKey, TColumnKey> Columns
        {
            get {
                var newData = _data.Select(vec => new ObjectSequence<TRowKey>(_rowIndex, Vector.BoxVector(vec, _vectorBuilder), _vectorBuilder, _indexBuilder));
                var colSeq = new ColumnSequence<TRowKey, TColumnKey>(new Sequence<TColumnKey, ObjectSequence<TRowKey>>(_columnIndex, newData, _vectorBuilder, _indexBuilder));
                return colSeq;
            }
            set { }
        }

        public ISequence<T> TryGetRow<T>(TRowKey rowKey)
        {
            var rowAddress = _rowIndex.Lookup(rowKey, LookupSemantics.Exact);
            if(rowAddress == null)
            {
                throw new KeyNotFoundException();
            }
            var vector = new RowReaderVector<T>(_data, _vectorBuilder, rowAddress.Value);

            var res = new Sequence<TColumnKey, T>(_columnIndex, vector, _vectorBuilder, _indexBuilder);

            return null;
        }




        //===========================================WORK IN PROGRESS REGION=======================================================

        #region WorkInProgress

        public DataFrame<TRowKey, TColumnKey> GetAddressRange(IRangeBoundary<long> range)
        {
            var seqConstrCmd = _indexBuilder.GetAddressRange(new SequenceConstruction<TRowKey>(_rowIndex, new Return(0)), range);
            var newData = _data.Select<IVector>(
                vec =>
                {
                    //_vectorBuilder.Build(seqConstrCmd.VectorConstruction, new IVector[] { Vector.TransformColumn<IVector>(vec, _vectorBuilder) });
                    //var newVec = Vector.unboxVector<IVector>
                    var res = Vector.TransformColumn(vec, _vectorBuilder, seqConstrCmd.VectorConstruction);
                    //var res =_vectorBuilder.Build<IVector>(seqConstrCmd.VectorConstruction, new IVector[] { vec });
                    return res;
                    
                });
            return new DataFrame<TRowKey, TColumnKey>(seqConstrCmd.Index, _columnIndex, newData, _indexBuilder, _vectorBuilder);

        }


        ////TEST
        private ColumnDefinition createColDefinition(IVector vector)
        {
            var vType = vector.GetType();
            if(vType.GenericTypeArguments.Any())
            {
                return new ColumnDefinition(vType.GenericTypeArguments.First(), vector);
            }
            else
            {
                return null;
            }

        }

        public void getKeys<T>(IIndex<T> index) 
        {
            if(index.Keys.Any())
            {
            }
            
        }

        public void _tempwriteData(List<ColumnDefinition> columns, Action<Tuple<IEnumerable<object>, Type>> writer)
        {
            foreach(var col in columns)
            {
                switch(Type.GetTypeCode(col.Type))
                {
                    case TypeCode.String:
                        var vecStr = (IVector<string>)col.Values;
                        writer.Invoke(Tuple.Create(vecStr.Data.Values.Select(v => (object)v), typeof(string)));
                        break;
                    case TypeCode.Int32:
                        var vecInt32 = (IVector<int>)col.Values;
                        writer.Invoke(Tuple.Create(vecInt32.Data.Values.Select(v => (object)v), typeof(int)));
                        break;
                    case TypeCode.Int64:
                        var vecInt64 = (IVector<long>)col.Values;
                        writer.Invoke(Tuple.Create(vecInt64.Data.Values.Select(v => (object)v), typeof(long)));
                        break;
                    case TypeCode.Double:
                        var vecDouble = (IVector<long>)col.Values;
                        writer.Invoke(Tuple.Create(vecDouble.Data.Values.Select(v => (object)v), typeof(double)));
                        break;
                    case TypeCode.Boolean:
                        var vecDateTime = (IVector<DateTime>)col.Values;
                        writer.Invoke(Tuple.Create(vecDateTime.Data.Values.Select(v => (object)v), typeof(bool)));
                        var res = vecDateTime.Data.Values;
                        break;
                    case TypeCode.DateTime:
                        var vecBool = (IVector<bool>)col.Values;
                        break;
                }
            }
        }


        //UNFINISHED
        public DataTable toDataTable()
        {
            var dt = new DataTable();
            var data = GetFrameData();
            //TODO Add error handling here
            var types = data.RowKeys.Select(k => k[0].GetType()).ToList();
            var flattenKeys = data.ColumnKeys.Select(c => c[0]).ToList();

            foreach(var colKey in data.ColumnKeys)
            {
                var dk = new DataColumn(colKey[0].ToString(), colKey[0].GetType());
                dt.Columns.Add(dk);
            }

            return dt;
            
        }
        #endregion WorkInProgress

    }
}
