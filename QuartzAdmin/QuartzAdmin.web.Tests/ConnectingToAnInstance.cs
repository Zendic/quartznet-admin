using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;

namespace QuartzAdmin.web.Tests
{
    /// <summary>
    /// Summary description for ConnectingToAnInstance
    /// </summary>
    [TestClass]
    public class ConnectingToAnInstance
    {
        public ConnectingToAnInstance()
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
Story: Connecting to an instance

As Operations
I want to connect to a running instance of quartz
So that I can view jobs, schedules, and triggers

Scenario: Verify quartz is running
Given a quartz instance is running
When I connect
Then a valid instance should be returned

Scenario: List all jobs in an instance
Given a quartz instance is running
When I connect and request a list of jobs
Then a list of jobs should be returned

Scenario: List all triggers in an instance
Given a quartz instance is running
When I connect and request a list of triggers
Then a list of triggers should be returned
    */

        [TestInitialize()]
        public void TestSetup()
        {
            StartTestScheduler();
        }
        [TestCleanup()]
        public void TestTeardown()
        {
            StopTestScheduler();
        }


        [TestMethod]
        public void Verify_quartz_is_running()
        {
            // Arrange
            QuartzAdmin.web.Controllers.InstanceController controller = GetInstanceController();
            QuartzAdmin.web.Models.InstanceModel instance = GetTestInstance();
            controller.Repository.Save(instance);

            // Act
            ActionResult result = controller.Connect(instance.InstanceName);
            bool isConnected = false;

            if (result is ViewResult)
            {
                if (((ViewResult)result).ViewData.Model is QuartzAdmin.web.Models.InstanceViewModel)
                {
                    isConnected = true;
                }
            }

            // Assert
            Assert.IsTrue(isConnected);

        }

        [TestMethod]
        public void List_all_jobs_in_an_instance()
        {
            // Arrange
            QuartzAdmin.web.Controllers.InstanceController controller = GetInstanceController();
            QuartzAdmin.web.Models.InstanceModel instance = GetTestInstance();
            controller.Repository.Save(instance);

            // Act
            ActionResult result = controller.Connect(instance.InstanceName);
            int countOfJobs = 0;

            if (result is ViewResult)
            {
                if (((ViewResult)result).ViewData.Model is QuartzAdmin.web.Models.InstanceViewModel)
                {
                    countOfJobs = ((QuartzAdmin.web.Models.InstanceViewModel)((ViewResult)result).ViewData.Model).Jobs.Count;
                }
            }

            // Assert
            Assert.IsTrue(countOfJobs > 0);
        }
        [TestMethod]
        public void List_all_triggers_in_an_instance()
        {
            // Arrange
            QuartzAdmin.web.Controllers.InstanceController controller = GetInstanceController();
            QuartzAdmin.web.Models.InstanceModel instance = GetTestInstance();
            controller.Repository.Save(instance);

            // Act
            ActionResult result = controller.Connect(instance.InstanceName);
            int countOfTriggers = 0;

            if (result is ViewResult)
            {
                if (((ViewResult)result).ViewData.Model is QuartzAdmin.web.Models.InstanceViewModel)
                {
                    countOfTriggers = ((QuartzAdmin.web.Models.InstanceViewModel)((ViewResult)result).ViewData.Model).Triggers.Count;
                }
            }

            // Assert
            Assert.IsTrue(countOfTriggers > 0);
        }

        private QuartzAdmin.web.Controllers.InstanceController GetInstanceController()
        {
            QuartzAdmin.web.Controllers.InstanceController cont = new QuartzAdmin.web.Controllers.InstanceController(new Fakes.FakeInstanceRepository());

            return cont;

        }

        private QuartzAdmin.web.Models.InstanceModel GetTestInstance()
        {
            QuartzAdmin.web.Models.InstanceModel instance = new QuartzAdmin.web.Models.InstanceModel();
            instance.InstanceName = "MyTestInstance";
            instance.InstanceProperties.Add(new QuartzAdmin.web.Models.InstancePropertyModel() { PropertyName = "quartz.scheduler.instanceName", PropertyValue = "SampleQuartzScheduler" });
            instance.InstanceProperties.Add(new QuartzAdmin.web.Models.InstancePropertyModel() { PropertyName = "quartz.threadPool.type", PropertyValue = "Quartz.Simpl.SimpleThreadPool, Quartz" });
            instance.InstanceProperties.Add(new QuartzAdmin.web.Models.InstancePropertyModel() { PropertyName = "quartz.scheduler.proxy", PropertyValue = "true" });
            instance.InstanceProperties.Add(new QuartzAdmin.web.Models.InstancePropertyModel() { PropertyName = "quartz.scheduler.proxy.address", PropertyValue = "tcp://localhost:567/QuartzScheduler" });
            return instance;

        }

        private void StartTestScheduler()
        {
            IScheduler sched = GetTestScheduler();

            sched.Start();

            // construct job info
            JobDetail jobDetail = new JobDetail("myJob", null, typeof(sample.scheduler.core.ConsoleJob1));
            // fire every hour
            Trigger trigger = TriggerUtils.MakeHourlyTrigger();
            // start on the next even hour
            trigger.StartTimeUtc = TriggerUtils.GetEvenHourDate(DateTime.UtcNow);
            trigger.Name = "myTrigger";
            sched.ScheduleJob(jobDetail, trigger); 
        }

        private void StopTestScheduler()
        {
            IScheduler sched = GetTestScheduler();
            sched.Shutdown();

        }

        private IScheduler GetTestScheduler()
        {
            System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection();

            ISchedulerFactory schedFact = new StdSchedulerFactory(props);

            props.Add("quartz.scheduler.instanceName", "SampleQuartzScheduler");
            props.Add("quartz.scheduler.exporter.type", "Quartz.Simpl.RemotingSchedulerExporter, Quartz");
            props.Add("quartz.scheduler.exporter.port", "567");
            props.Add("quartz.scheduler.exporter.bindName", "QuartzScheduler");
            props.Add("quartz.scheduler.exporter.channelType", "tcp");

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            return sched;
        }
    }
}
