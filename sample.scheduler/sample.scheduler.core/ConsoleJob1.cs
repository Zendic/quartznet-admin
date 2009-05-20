using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sample.scheduler.core
{
    public class ConsoleJob1 : Quartz.IJob
    {
        #region IJob Members

        public void Execute(Quartz.JobExecutionContext context)
        {
            Console.WriteLine(string.Format("Hello from ConsoleJob1 - {0}", DateTime.Now));
        }

        #endregion
    }
}
