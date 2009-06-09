using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;

namespace QuartzAdmin.web.Tests
{
    /// <summary>
    /// Summary description for DefiningAnInstance
    /// </summary>
    [TestClass]
    public class DefiningAnInstance
    {
        public DefiningAnInstance()
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

        /*
Defining a connection

    As Operations I want to save a connection definition to a quartz instance which contains a human readable name and all information required to make a connection So that I can identify and connect to a running quartz instance 

   1. Scenario: Valid name and connection information

          When the a valid name and connection information are entered Then the connection is saved 

   2. Scenario: Name is already in use

          Given the specified name is already in use When the name is specified for a new connection Then the connection is not saved and the user is notified that the name is already in use 

   3. Scenario: Name not specified

          When the name is not specified Then the connection is not saved and the user is notified that the name is required 

   4. Scenario: Connection properties not specified

          When no connection properties are specified Then the connection is not saved and the user is notified that at least one connection property is required 
         * * */



        [TestMethod]
        public void Valid_name_and_connection_information()
        {
            //Arrange
            //Models.InstanceRepository repo = new QuartzAdmin.web.Models.InstanceRepository();
            FormCollection formData = new FormCollection();
            QuartzAdmin.web.Controllers.InstanceController controller = GetInstanceController();
            formData.Add("InstanceName", "MyFirstInstance");
            formData.Add("InstancePropertyKey1", "Red");
            formData.Add("InstancyPropertyValue1", "Dog");
            controller.ValueProvider = formData.ToValueProvider();
            
            //Act
            controller.Create(formData);
            Models.InstanceModel newInstance = controller.Repository.GetByName("MyFirstInstance");

            //Assert
            Assert.IsNotNull(newInstance);
            Assert.AreEqual(formData["InstanceName"], newInstance.InstanceName);
        }
            
        private QuartzAdmin.web.Controllers.InstanceController GetInstanceController()
        {
            QuartzAdmin.web.Controllers.InstanceController cont = new QuartzAdmin.web.Controllers.InstanceController(new Fakes.FakeInstanceRepository());

            return cont;

        }
    }
}
