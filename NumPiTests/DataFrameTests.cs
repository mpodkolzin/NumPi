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

namespace NumPiTests
{
    [TestClass]
    public class DataFrameTests
    {

        public List<List<string>> testData = new List<List<string>>() {

            new List<string>()
            {
                "Test1", "10", "Now1"
            },
            new List<string>()
            {
                "Test2", "20", "Now2"
            },
            new List<string>()
            {
                "Test3", "30", "Now3"
            }
        };

        public DataFrame<int, string> GetTestDataFrame()
        {
            var cols = new List<string>() { "Name", "Value", "Timestamp" };
            var colIndex = Index.ofKeys<string>(cols);
            var rowidxArray = Enumerable.Range(0, testData.Count).ToArray();
            var rowIndex = Index.ofKeys<int>(rowidxArray);
            var colList = cols.Select(c => new ListVector<string>()).ToArray();

            var col1 = new ListVector<string>();
            var col2 = new ListVector<int>();
            var col3 = new ListVector<string>();

            col1.Add("Test1");
            col1.Add("Test2");
            col1.Add("Test3");
            col2.Add(10);
            col2.Add(20);
            col2.Add(30);
            col3.Add("Now1");
            col3.Add("Now2");
            col3.Add("Now3");

            var colVectors2 = new List<IVector>() { col1.ToVector(), col2.ToVector(), col3.ToVector() };

            //var vec = colVectors2.First();
            //var isgen = vec.GetType().IsGenericParameter;
            //var  cgenAttrs = vec.GetType().ContainsGenericParameters;
            //var  genAttrs = vec.GetType().GenericParameterAttributes;
            //IVector<string> boxedVec = vec as IVector<string>;

            foreach(var row in testData)
            {
                for (int i = 0; i < cols.Count; i++)
                {
                    var rowArr = row.ToArray();
                    colList[i].Add(row[i]);
                }
            }
            var colVectors = colList.Select(c => c.ToVector()).ToArray();

            var dataFrame = new DataFrame<int, string>(rowIndex, colIndex, Vector.ofValues(colVectors2.ToArray()), LinearIndexBuilder.Instance, VectorBuilder.Instance);
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


            //dataFrame.TryGetRow<string>(1);
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

        public void CanGetRange()
        {
            var df = GetTestDataFrame();

            var res = df.GetAddressRange(new IntervalOf<long>(1, 2, BoundaryBehavior.Inclusive));


        }


        [TestMethod]
        public void CanCreateDataFrameFromListOfRecords()
        {

            List<Record> testRecords = new List<Record>()
            {
                new Record { Name = "Test rec1", Timestamp = "Now1", Value = 10},
                new Record { Name = "Test rec2", Timestamp = "Now2", Value = 20},
                new Record { Name = "Test rec3", Timestamp = "Now3", Value = 30},
            };


            FrameReflectionUtils.ConvertRecordSequence<Record>(testRecords);

        }
    }
}
