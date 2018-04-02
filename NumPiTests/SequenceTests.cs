using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumPi.Sequence;
using NumPi.Sequence.Extensions;
using System.Linq;
using NumPi.Sequence.Construction;

namespace NumPiTests
{
    /// <summary>
    /// Summary description for SequenceTests
    /// </summary>
    [TestClass]
    public class SequenceTests
    {
        public SequenceTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Can_create_sequence_from_listof_values()
        {
            List<string> testVals = new List<string>() { "val1", "val2", "val4", "val4" };

            var seq = Sequence.OfValues(testVals);
            Assert.IsNotNull(seq);
            Assert.AreEqual(testVals.Count, seq.Values.Count());
        }

        [TestMethod]
        public void Can_create_sequence_from_collection_of_rows()
        {

            var sequenceBuilder = new SequenceBuilder<string, object>();
            sequenceBuilder.Add("Sin", "Test01");
            sequenceBuilder.Add("Cos", "Test01");
            var seq = sequenceBuilder.Sequence;

        }



    }
}
