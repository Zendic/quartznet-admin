using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuartzAdmin.web.Models;
using System.Web.Mvc;

namespace QuartzAdmin.web.Tests.Models
{
    /// <summary>
    /// Summary description for ConnectionParameterModelTest
    /// </summary>
    [TestClass]
    public class ConnectionParameterModelTest
    {
        #region Scaffolding

        public ConnectionParameterModelTest()
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

        #endregion

        [TestMethod]
        public void Should_Instantiate()
        {
            // arrange
            ConnectionParameterModel connectionParameter;

            // act
            connectionParameter = new ConnectionParameterModel();

            // assert
            Assert.IsNotNull(connectionParameter);
        }

        [TestMethod]
        public void Should_Create_List_From_Valid_Form_Collection()
        {
            // arrange
            FormCollection formCollection = new FormCollection();
            formCollection["ConnectionParameterKey1"] = "key1";
            formCollection["ConnectionParameterValue1"] = "value1";

            // act
            List<ConnectionParameterModel> connectionParameterList = ConnectionParameterModel.FromFormCollection(formCollection);

            // assert
            Assert.IsTrue(connectionParameterList.Count == 1);
            Assert.IsTrue(connectionParameterList[0].Key == "key1");
            Assert.IsTrue(connectionParameterList[0].Value == "value1");
        }    
    }
}
