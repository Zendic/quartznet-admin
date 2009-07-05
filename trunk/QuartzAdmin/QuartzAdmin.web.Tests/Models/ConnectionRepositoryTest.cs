using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuartzAdmin.web.Tests.Fakes;
using QuartzAdmin.web.Models;

namespace QuartzAdmin.web.Tests.Models
{
    /// <summary>
    /// Summary description for ConnectionRepositoryTest
    /// </summary>
    [TestClass]
    public class ConnectionRepositoryTest
    {

        #region Scaffolding

        public ConnectionRepositoryTest()
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

        private IConnectionRepository GetConnectionRepository()
        {
            return new FakeConnectionRepository();
        }


        [TestMethod]
        public void Should_Instantiate()
        {
            // arrange
            IConnectionRepository connectionRepository;
            
            // act
            connectionRepository = this.GetConnectionRepository();

            // assert
            Assert.IsNotNull(connectionRepository);

        }

        [TestMethod]
        public void Should_Have_Count()
        {
            // arrange
            IConnectionRepository connectionRepository = this.GetConnectionRepository();

            // act
            int count = connectionRepository.Count;

            // assert
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void Should_Allow_Add()
        {
            // arrange
            IConnectionRepository connectionRepository = this.GetConnectionRepository();

            // act
            connectionRepository.AddConnection(new ConnectionModel());

            // assert
            Assert.IsTrue(connectionRepository.Count == 1);

        }

        [TestMethod]
        public void Should_Return_Item_For_Valid_Id()
        {
            // arrange
            IConnectionRepository connectionRepository = this.GetConnectionRepository();
            ConnectionModel connection = new ConnectionModel();
            connection.ConnectionId = 1;
            connectionRepository.AddConnection(connection);

            // act
            ConnectionModel retrievedConnection = connectionRepository.GetConnection(1);

            // assert
            Assert.IsTrue(retrievedConnection.ConnectionId == 1);
        }

        [TestMethod]
        public void Should_Return_Null_For_Invalid_Id()
        {
            // arrange
            IConnectionRepository connectionRepository = this.GetConnectionRepository();

            // act
            ConnectionModel retrievedConnection = connectionRepository.GetConnection(1);

            // assert
            Assert.IsNull(retrievedConnection);
        }

        [TestMethod]
        public void Should_Allow_Remoe()
        {
            // arrange
            IConnectionRepository connectionRepository = this.GetConnectionRepository();
            ConnectionModel connection = new ConnectionModel();
            connection.ConnectionId = 1;
            connectionRepository.AddConnection(connection);

            // act
            connectionRepository.RemoveConnection(connection);
            ConnectionModel retrievedConnection = connectionRepository.GetConnection(1);

            // assert
            Assert.IsNull(retrievedConnection);
        }

        [TestMethod]
        public void Should_Not_Save_When_Duplicate_Name()
        {
            // arrange
            IConnectionRepository connectionRepository = this.GetConnectionRepository();
            
            ConnectionModel connection1 = new ConnectionModel();
            connection1.Name = "connection1";
            connectionRepository.AddConnection(connection1);

            ConnectionModel connection2 = new ConnectionModel();
            connection2.Name = "connection1";
            connectionRepository.AddConnection(connection2);

            // act
            bool exceptionThrown = false;
            try
            {
                connectionRepository.Save();
            }
            catch
            {
                exceptionThrown = true;
            }

            // assert
            Assert.IsTrue(exceptionThrown);

        }
    }
}
