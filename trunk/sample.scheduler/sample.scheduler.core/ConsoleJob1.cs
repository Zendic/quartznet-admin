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
            string extraText = "[none]";
            if (context.MergedJobDataMap.Contains("ExtraText"))
            {
                extraText = context.MergedJobDataMap["ExtraText"].ToString();
            }
            Console.WriteLine(string.Format("Hello from ConsoleJob1 - {0}\nExtra Text:{1}", DateTime.Now, extraText));
        }

        #endregion
    }
}
