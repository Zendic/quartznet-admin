using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuartzAdmin.web.Controllers;
using System.Web.Mvc;
using QuartzAdmin.web.Tests.Fakes;
using QuartzAdmin.web.Models;

namespace QuartzAdmin.web.Tests.Controllers
{
    /// <summary>
    /// Summary description for ConnectionControllerTest
    /// </summary>
    [TestClass]
    public class ConnectionControllerTest
    {
        #region Scaffolding

        public ConnectionControllerTest()
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

        #endregion

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
        public void Create_Get_Should_Return_View()
        {
            // arrange
            ConnectionController connectionController = new ConnectionController(new FakeConnectionRepository());

            // act
            ViewResult viewResult = connectionController.Create() as ViewResult;

            // assert
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void Create_Post_Should_Create_Connection_When_Valid()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            ConnectionController connectionController = new ConnectionController(connectionRepository);

            FormCollection formCollection = new FormCollection();
            formCollection["Name"] = "connection1";
            formCollection["ConnectionParameterKey1"] = "key1";
            formCollection["ConnectionParameterValue1"] = "value1";

            connectionController.ValueProvider = formCollection.ToValueProvider();

            // act
            ViewResult viewResult = connectionController.Create(formCollection) as ViewResult;

            // assert
            Assert.IsTrue(connectionRepository.Count == 1);
        }

        [TestMethod]
        public void Create_Post_Should_Not_Create_Connection_When_Invalid()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            ConnectionController connectionController = new ConnectionController(connectionRepository);

            FormCollection formCollection = new FormCollection();
            connectionController.ValueProvider = formCollection.ToValueProvider();

            // act
            ViewResult viewResult = connectionController.Create(formCollection) as ViewResult;

            // assert
            Assert.IsTrue(connectionRepository.Count == 0);
        }

        [TestMethod]
        public void Create_Post_Should_Redisplay_With_Errors_When_Invalid()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            ConnectionController connectionController = new ConnectionController(connectionRepository);

            FormCollection formCollection = new FormCollection();
            connectionController.ValueProvider = formCollection.ToValueProvider();

            // act
            ViewResult viewResult = connectionController.Create(formCollection) as ViewResult;

            // assert
            Assert.IsTrue(viewResult.ViewData.Model is ConnectionModel);
            Assert.IsTrue(viewResult.ViewData.ModelState["Name"].Errors.Count > 0);
        }

        [TestMethod]
        public void Create_Post_Should_Redisplay_With_Errors_When_Duplicate_Name()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            connectionRepository.AddConnection(new ConnectionModel() { ConnectionId = 1, Name = "connection1" });

            FormCollection formCollection = new FormCollection();
            formCollection["Name"] = "connection1";
            formCollection["ConnectionParameterKey1"] = "key1";
            formCollection["ConnectionParameterValue1"] = "value1";

            ConnectionController connectionController = new ConnectionController(connectionRepository);
            connectionController.ValueProvider = formCollection.ToValueProvider();

            // act
            ViewResult viewResult = connectionController.Create(formCollection) as ViewResult;

            // assert
            Assert.IsTrue(viewResult.ViewData.ModelState["Name"].Errors.Count == 1);
        }

        [TestMethod]
        public void Index_Get_Should_Return_View()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            ConnectionController connectionController = new ConnectionController(connectionRepository);

            // act
            ViewResult viewResult = connectionController.Index() as ViewResult;

            // assert
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void Index_Get_Should_Display_Connections()
        {
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            connectionRepository.AddConnection(new ConnectionModel() { ConnectionId = 1, Name = "connection1" });
            connectionRepository.AddConnection(new ConnectionModel() { ConnectionId = 2, Name = "connection2" });

            ConnectionController connectionController = new ConnectionController(connectionRepository);

            // act
            ViewResult viewResult = connectionController.Index() as ViewResult;
            IEnumerable<ConnectionModel> connectionList = viewResult.ViewData.Model as IEnumerable<ConnectionModel>;

            // assert
            Assert.IsNotNull(connectionList);
            Assert.IsTrue(connectionList.Count() == 2);
        }

        [TestMethod]
        public void Edit_Get_Should_Return_View_When_Valid_Id()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            connectionRepository.AddConnection(new ConnectionModel() { ConnectionId = 1, Name = "connection1" });

            ConnectionController connectionController = new ConnectionController(connectionRepository);

            // act
            ViewResult viewResult = connectionController.Edit(1) as ViewResult;

            // assert
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void Edit_Get_Should_Display_Connection_When_Valid_Id()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            connectionRepository.AddConnection(new ConnectionModel() { ConnectionId = 1, Name = "connection1" });

            ConnectionController connectionController = new ConnectionController(connectionRepository);

            // act
            ViewResult viewResult = connectionController.Edit(1) as ViewResult;
            ConnectionModel connection = viewResult.ViewData.Model as ConnectionModel;

            // assert
            Assert.IsNotNull(connection);
            Assert.IsTrue(connection.ConnectionId == 1);
        }

        [TestMethod]
        public void Edit_Get_Should_Display_Error_When_Invalid_Id()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            ConnectionController connectionController = new ConnectionController(connectionRepository);

            // act
            ViewResult viewResult = connectionController.Edit(1) as ViewResult;

            // assert
            Assert.IsNotNull(viewResult);
            Assert.IsTrue(viewResult.ViewName == "NotFound");
        }

        [TestMethod]
        public void Edit_Post_Should_Update_Connection_When_Valid()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            connectionRepository.AddConnection(new ConnectionModel() { ConnectionId = 1, Name = "connection1" });

            FormCollection formCollection = new FormCollection();
            formCollection["Name"] = "connectionA";
            formCollection["ConnectionParameterKey1"] = "key1";
            formCollection["ConnectionParameterValue1"] = "value1";

            ConnectionController connectionController = new ConnectionController(connectionRepository);
            connectionController.ValueProvider = formCollection.ToValueProvider();

            // act
            ViewResult viewResult = connectionController.Edit(1, formCollection) as ViewResult;
            ConnectionModel connection = connectionRepository.GetConnection(1);

            // assert
            Assert.IsTrue(connection.Name == "connectionA");
        }

        /*
         * interesting problem... the in-memory object was updated, so not possible to get previous state
        [TestMethod]
        public void Edit_Post_Should_Not_Update_Connection_When_Invalid()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            connectionRepository.AddConnection(new ConnectionModel() { ConnectionId = 1, Name = "connection1" });

            FormCollection formCollection = new FormCollection();
            formCollection["Name"] = String.Empty;

            ConnectionController connectionController = new ConnectionController(connectionRepository);
            connectionController.ValueProvider = formCollection.ToValueProvider();

            // act
            ViewResult viewResult = connectionController.Edit(1, formCollection) as ViewResult;
            ConnectionModel connection = connectionRepository.GetConnection(1);

            // assert
            Assert.IsTrue(connection.Name == "connection1");
        }
         * */

        [TestMethod]
        public void Edit_Post_Should_Redisplay_With_Errors_When_Invalid()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            connectionRepository.AddConnection(new ConnectionModel() { ConnectionId = 1, Name = "connection1" });

            FormCollection formCollection = new FormCollection();
            formCollection["Name"] = String.Empty;

            ConnectionController connectionController = new ConnectionController(connectionRepository);
            connectionController.ValueProvider = formCollection.ToValueProvider();

            // act
            ViewResult viewResult = connectionController.Edit(1, formCollection) as ViewResult;

            // assert
            Assert.IsTrue(viewResult.ViewData.Model is ConnectionModel);
            Assert.IsTrue(viewResult.ViewData.ModelState["Name"].Errors.Count > 0);
        }

        [TestMethod]
        public void Edit_Post_Should_Redisplay_With_Errors_When_Duplicate_Name()
        {
            // arrange
            FakeConnectionRepository connectionRepository = new FakeConnectionRepository();
            connectionRepository.AddConnection(new ConnectionModel() { ConnectionId = 1, Name = "connection1" });
            connectionRepository.AddConnection(new ConnectionModel() { ConnectionId = 2, Name = "connection2" });

            FormCollection formCollection = new FormCollection();
            formCollection["Name"] = "connection2";
            formCollection["ConnectionParameterKey1"] = "key1";
            formCollection["ConnectionParameterValue1"] = "value1";

            ConnectionController connectionController = new ConnectionController(connectionRepository);
            connectionController.ValueProvider = formCollection.ToValueProvider();

            // act
            ViewResult viewResult = connectionController.Edit(1, formCollection) as ViewResult;

            // assert
            Assert.IsTrue(viewResult.ViewData.ModelState["Name"].Errors.Count == 1);
        }
    
    }
}
