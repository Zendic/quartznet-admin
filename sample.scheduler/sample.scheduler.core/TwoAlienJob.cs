using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;

namespace sample.scheduler.core
{
    public class TwoAlienJob : IStatefulJob
    {
        #region IJob Members

        public void Execute(JobExecutionContext context)
        {
            string alienText = @"
            .-''''-.        .-''''-.
           /        \      /        \
          /_        _\    /_        _\
         // \      / \\  // \      / \\
         |\__\    /__/|  |\__\    /__/|
          \    ||    /    \    ||    /
           \        /      \        /
            \  __  /        \  __  /
             '.__.'          '.__.'
              |  |            |  |
              |  |            |  |
";

            Console.WriteLine(alienText);
        }

        #endregion
    }
}
