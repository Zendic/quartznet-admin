using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sample.scheduler.console
{
    class Program
    {
        static void Main(string[] args)
        {
            sample.scheduler.core.QuartzHost host = new sample.scheduler.core.QuartzHost();
            host.StartScheduler();

            System.Threading.Thread.Sleep(0);
        }
    }
}
