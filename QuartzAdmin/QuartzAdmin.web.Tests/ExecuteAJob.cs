using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quartz;
using Quartz.Impl;
using Moq;

namespace QuartzAdmin.web.Tests
{
    /// <summary>
    /// Summary description for ExecuteAJob
    /// </summary>
    [TestClass]
    public class ExecuteAJob
    {
        public ExecuteAJob()
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
        public void Execute_a_job()
        {
            // Arrange
            // - Add a job into the test scheduler
            IScheduler sched = GetTestScheduler();
            JobDetail job = new JobDetail("TestJob", "TestGroup", typeof(Quartz.Job.NoOpJob));
            sched.AddJob(job, true);
            // - Setup the mock HTTP Request
            var request = new Mock<System.Web.HttpRequestBase>();
            var context = new Mock<System.Web.HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            System.Collections.Specialized.NameValueCollection formParameters = new System.Collections.Specialized.NameValueCollection();
            // NOTE: adding items to the formParameter collection is possible here
            request.SetupGet(x => x.Form).Returns(formParameters);

            
            // - Create the fake instance repo and the job execution controller
            QuartzAdmin.web.Models.IInstanceRepository instanceRepo = new Fakes.FakeInstanceRepository();
            instanceRepo.Save(GetTestInstance());
            QuartzAdmin.web.Controllers.JobExecutionController jec = new QuartzAdmin.web.Controllers.JobExecutionController(instanceRepo);

            // - Set the fake request for the controller
            jec.ControllerContext = new System.Web.Mvc.ControllerContext(context.Object, new System.Web.Routing.RouteData(), jec);

            // Act
            System.Web.Mvc.ActionResult result = jec.RunNow("MyTestInstance", "TestGroup", "TestJob");

            //Assert
            Assert.IsTrue(result is System.Web.Mvc.ContentResult && ((System.Web.Mvc.ContentResult)result).Content == "Job execution started");

        }

        [TestMethod]
        public void Execute_a_job_with_job_data_map()
        {
            // Arrange
            // - Add a job into the test scheduler
            IScheduler sched = GetTestScheduler();
            JobDetail job = new JobDetail("TestJob2", "TestGroup", typeof(Quartz.Job.NoOpJob));
            job.JobDataMap.Add("MyParam1", "Initial Data");
            sched.AddJob(job, true);
            // - Setup the mock HTTP Request
            var request = new Mock<System.Web.HttpRequestBase>();
            var context = new Mock<System.Web.HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            System.Collections.Specialized.NameValueCollection formParameters = new System.Collections.Specialized.NameValueCollection();

            formParameters.Add("jdm_MyParam1", "Working on the railroad");
            request.SetupGet(x => x.Form).Returns(formParameters);


            // - Create the fake instance repo and the job execution controller
            QuartzAdmin.web.Models.IInstanceRepository instanceRepo = new Fakes.FakeInstanceRepository();
            instanceRepo.Save(GetTestInstance());
            QuartzAdmin.web.Controllers.JobExecutionController jec = new QuartzAdmin.web.Controllers.JobExecutionController(instanceRepo);

            // - Set the mocked context for the controller
            jec.ControllerContext = new System.Web.Mvc.ControllerContext(context.Object, new System.Web.Routing.RouteData(), jec);

            // Act
            System.Web.Mvc.ActionResult result = jec.RunNow("MyTestInstance", "TestGroup", "TestJob2");
            // - Get the triggers of the job
            Trigger[] trigOfJob = sched.GetTriggersOfJob("TestJob2", "TestGroup");

            //Assert
            Assert.IsTrue(result is System.Web.Mvc.ContentResult && ((System.Web.Mvc.ContentResult)result).Content == "Job execution started");
            Assert.IsTrue(trigOfJob.Count() > 0);
            Assert.AreEqual(trigOfJob[0].JobDataMap["MyParam1"], "Working on the railroad");

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
