using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumPi;
using NumPi.Indices;
using System.Linq;
using NumPi.Vectors.Helpers;
using NumPi.Vectors.Extensions;
using NumPi.Vectors;
using NumPi.Reflection;
using System.Diagnostics;
using NumPi.Extensions.FrameExtensions;

namespace NumPiTests
{
    [TestClass]
    public class DataFrameTests
    {

        public DataFrame<int, string> GetTestDataFrame()
        {
            var cols = new List<string>() { "Name", "Value", "Timestamp" };
            var colIndex = Index.ofKeys<string>(cols);
            var rowidxArray = Enumerable.Range(0, 3).ToArray();
            var rowIndex = Index.ofKeys<int>(rowidxArray);
            var colList = cols.Select(c => new ListVector<object>()).ToArray();

            var col1 = new ListVector<string>() { "Test1", "Test2", "Test3" };
            var col2 = new ListVector<int>() { 10, 20, 30 };
            var col3 = new ListVector<DateTime>() { DateTime.Now, DateTime.Now, DateTime.Now };

            var colVectors = new List<IVector>() { col1.ToVector(), col2.ToVector(), col3.ToVector() };
            var dataFrame = new DataFrame<int, string>(rowIndex, colIndex, Vector.ofValues(colVectors.ToArray()), LinearIndexBuilder.Instance, VectorBuilder.Instance);

            return dataFrame;

        }

        [TestMethod]
        public void TestMethod1()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            var dataFrame = GetTestDataFrame();
            var frameData = dataFrame.GetFrameData();
            dataFrame._tempwriteData(frameData.Columns.ToList(), 
                (data) => {
                    foreach(var el in data.Item1)
                    {
                        Trace.WriteLine(el.ToString());
                    }
                });


            var rows2 = dataFrame.Rows;
            var cols2 = dataFrame.Columns;
            Assert.AreEqual(3, dataFrame.RowKeys.Count());
            Assert.AreEqual(3, dataFrame.ColumnKeys.Count());


            var colKeys = dataFrame.ColumnKeys.ToList();

            Console.WriteLine();
        }

        public class Record
        {
            public string Name;
            public string Timestamp;
            public int Value;
        }

        [TestMethod]
        public void Can_Get_Range_from_DataFrame()
        {
            var df = GetTestDataFrame();
            var res = df.GetAddressRange(new IntervalOf<long>(1, 2, BoundaryBehavior.Inclusive));
            Assert.AreEqual(2, res.RowCount);
        }

        [TestMethod]
        public void Can_get_columns_from_data_frame()
        {
            var df = GetTestDataFrame();
            var res = df.Columns;

            Assert.IsNotNull(res);
            Assert.AreEqual(3, res.Keys.Count());
            Assert.AreEqual(3, res.Values.Count());
            Assert.AreEqual(df.ColumnCount, res.Values.Count());
            Assert.AreEqual(df.ColumnKeys.Count(), res.Keys.Count());
        }

        [TestMethod]
        public void Can_get_rows_from_DataFrame()
        {

            var df = GetTestDataFrame();
            var res = df.Rows;
            Assert.IsNotNull(res);
            Assert.AreEqual(df.RowCount, res.Values.Count());
            Assert.AreEqual(df.RowKeys.Count(), res.Keys.Count());
            Assert.AreEqual(3, res.Values.Count());
            Assert.AreEqual(3, res.Keys.Count());

        }


        [TestMethod]
        public void Can_Create_DataFrame_From_List_Of_Generic_objects()
        {

            List<Record> testRecords = new List<Record>()
            {
                new Record { Name = "Test rec1", Timestamp = "Now1", Value = 10},
                new Record { Name = "Test rec2", Timestamp = "Now2", Value = 20},
                new Record { Name = "Test rec3", Timestamp = "Now3", Value = 30},
            };


            var res = FrameReflectionUtils.ConvertRecordSequence<Record>(testRecords);

            Assert.AreEqual(3, res.ColumnKeys.Count());
            Assert.AreEqual(testRecords.Count, res.RowCount);

        }

        [TestMethod]
        public void Can_get_single_row()
        {

            int testRowIdx = 1;

            var df = GetTestDataFrame();
            var row = df.GetRow<object>(testRowIdx);
            Assert.IsNotNull(row);
            Assert.AreEqual(3, row.Vector.Length);

            var val = row.GetObject("Name");
            var val2 = row["Value"];

        }

        [TestMethod]
        public void Can_request_frame_data()
        {
            var df = GetTestDataFrame();
            var frameData = df.GetFrameData();

        }

        [TestMethod]
        public void Can_convert_to_2d_Array()
        {
            //TODO
            var df = GetTestDataFrame();
            var res = FrameExtensions.ToArray2D<string, int, string>(df);
        }
    }
}
