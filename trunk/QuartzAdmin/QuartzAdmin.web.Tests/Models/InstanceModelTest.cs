using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBehave.Narrator.Framework;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using System.Reflection;

namespace QuartzAdmin.web.Tests.Models
{
    /// <summary>
    /// Summary description for InstanceModelTest
    /// </summary>
    [Theme("Instance activites")]
    [TestClass]
    public class InstanceModelTest
    {
        public InstanceModelTest()
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

        [ClassInitialize()]
        public static void Setup(TestContext testContext) 
        {
            if (!ActiveRecordStarter.IsInitialized)
            {
                //Castle.ActiveRecord.Framework.IConfigurationSource config = ActiveRecordSectionHandler.Instance;
                //Castle.ActiveRecord.Framework.IConfigurationSource config = new XmlConfigurationSource("QuartzAdmin.web.Tests.dll.config");
                InPlaceConfigurationSource config = new InPlaceConfigurationSource();
                Dictionary<string, string> properties = new Dictionary<string, string>();

                properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
                properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
                properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
                properties.Add("connection.connection_string", "Data Source=|DataDirectory|..\\..\\..\\database\\quartz_admin.db");
                properties.Add("proxyfactory.factory_class", "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle");

                config.Add(typeof(ActiveRecordBase), properties);

                Assembly asm1 = Assembly.Load("QuartzAdmin.web");
                ActiveRecordStarter.Initialize(asm1, config);
            }

        }

        [TestMethod]
        public void DoesInstanceConnect()
        {
            QuartzAdmin.web.Models.InstanceRepository instanceRepo = new QuartzAdmin.web.Models.InstanceRepository();
            QuartzAdmin.web.Models.InstanceModel instance = instanceRepo.GetInstance("Instance1");

            try
            {
                Quartz.IScheduler sched = instance.GetQuartzScheduler();
                Assert.IsNotNull(sched);
            }
            catch (Exception ex)
            {
                Assert.Fail("Error connecting", ex);
            }
        }

        [Story]
        [TestMethod]
        public void DoesInstanceConnect2()
        {
            Story instanceConnectStory = new Story("Connect to instance");
            QuartzAdmin.web.Models.InstanceRepository instanceRepo = new QuartzAdmin.web.Models.InstanceRepository();
            QuartzAdmin.web.Models.InstanceModel instance = null;
            Quartz.IScheduler sched = null;

            instanceConnectStory
                .AsA("System Admin")
                .IWant("to connect to a running quartz instance")
                .SoThat("I can perform admin functions");

            instanceConnectStory
                .WithScenario("Perform connection to instance")
                .Given("the instance name is", "Instance1", delegate(string instanceName) { instance = instanceRepo.GetInstance(instanceName); })
                .When("the connection is attempted", delegate() { sched = instance.GetQuartzScheduler(); })
                .Then("the connection is not null", delegate() { Assert.IsNotNull(sched); });




        }

        [Story]
        [TestMethod]
        public void ShouldInstanceNameBeNull()
        {


            Story instanceNameStory = new Story("Should instance name be null");
            QuartzAdmin.web.Models.InstanceModel instance=null;
            instanceNameStory
                .AsA("System Admin")
                .IWant("to create a new quartz instance")
                .SoThat("I can administer it using this tool");

            instanceNameStory
                .WithScenario("Create new instance with null name")
                .Given("The instance name is", null, delegate(string instanceName)
                {
                    instance = new QuartzAdmin.web.Models.InstanceModel();
                    instance.InstanceName = instanceName;
                    
                })
                .And("The instance has properties", delegate(){instance.InstanceProperties.Add(new QuartzAdmin.web.Models.InstancePropertyModel() { PropertyName = "Hello", PropertyValue = "World" });})
                .When("the instance is validated")
                .Then("the validation should fail", delegate() { 
                    Assert.IsFalse(instance.IsValid()); 
                });


            instanceNameStory
                .WithScenario("Create new instance name and with no properties")
                .Given("The instance name is", null, delegate(string instanceName)
                {
                    instance = new QuartzAdmin.web.Models.InstanceModel();
                    instance.InstanceName = instanceName;

                })
                .And("The instance has properties", delegate() { instance.InstanceProperties.Add(new QuartzAdmin.web.Models.InstancePropertyModel() { PropertyName = "Hello", PropertyValue = "World" }); })
                .When("the instance is validated")
                .Then("the validation should fail", delegate()
            {
                Assert.IsFalse(instance.IsValid());
            });

        }
        [Story]
        [TestMethod]
        public void ShouldInstancePropertiesBeEmpty()
        {


            Story instanceNameStory = new Story("Should instance name be null");
            QuartzAdmin.web.Models.InstanceModel instance = null;
            instanceNameStory
                .AsA("System Admin")
                .IWant("to create a new quartz instance")
                .SoThat("I can administer it using this tool");

            instanceNameStory
                .WithScenario("Create new instance with empty properties")
                .Given("The instance name is", "HelloWorld", delegate(string instanceName)
                {
                    instance = new QuartzAdmin.web.Models.InstanceModel();
                    instance.InstanceName = instanceName;

                })
                .When("the instance is validated")
                .Then("the validation should fail", delegate(){Assert.IsFalse(instance.IsValid());});

        }
    }
}
