using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuartzAdmin.web.Models;

namespace QuartzAdmin.web.Tests.Models
{
    /// <summary>
    /// Summary description for ConnectionModelTest
    /// </summary>
    [TestClass]
    public class ConnectionModelTest
    {
        #region Scaffolding

        public ConnectionModelTest()
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


        private ConnectionModel CreateValidConnection()
        {
            ConnectionModel connection = new ConnectionModel();
            connection.Name = "name";

            connection.ConnectionParameters.Add(new ConnectionParameterModel() { Key = "key", Value = "value" });
            return connection;
        }

        [TestMethod]
        public void Should_Instantiate()
        {
            // arrange
            ConnectionModel connection = null;

            // act
            connection = new ConnectionModel();

            // assert
            Assert.IsNotNull(connection);
        }

        [TestMethod]
        public void Should_Not_Be_Valid_When_Unitialized()
        {
            // arrange
            ConnectionModel connection = new ConnectionModel();

            // act
            bool isValid = connection.IsValid;

            // assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Should_Be_Valid_When_Initialized()
        {
            // arrange
            ConnectionModel connection = this.CreateValidConnection();

            // act
            bool isValid = connection.IsValid;

            // assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Should_Not_Be_Valid_When_Name_Null()
        {
            // arrange
            ConnectionModel connection = this.CreateValidConnection();
            connection.Name = null;

            // act
            bool isValid = connection.IsValid;

            // assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Should_Not_Be_Valid_When_Name_Empty()
        {
            // arrange
            ConnectionModel connection = this.CreateValidConnection();
            connection.Name = String.Empty;

            // act
            bool isValid = connection.IsValid;

            // assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Should_Not_Be_Valid_When_Zero_Parameters()
        {
            // arrange
            ConnectionModel connection = this.CreateValidConnection();
            connection.ConnectionParameters.Clear();

            // act
            bool isValid = connection.IsValid;

            // assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Should_Not_Be_Valid_When_Parameters_Not_Valid()
        {
            // arrange
            ConnectionModel connection = this.CreateValidConnection();
            connection.ConnectionParameters.Add(new ConnectionParameterModel(){ Key="key", Value=null });

            // act
            bool isValid = connection.IsValid;

            // assert
            Assert.IsFalse(isValid);
        }

    }
}
