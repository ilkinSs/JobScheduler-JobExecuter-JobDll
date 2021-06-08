using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using IJob;
using JobEx.Lib;

namespace JobEx
{
    public   class JobExecutor 
    {
        public void Run(string JobName)
        {

            string className = ConfigHelper.GetConfigVal( JobName, "Class");
            string jobPath = ConfigHelper.GetConfigVal(JobName, "Name");

            Assembly assembly = Assembly.LoadFrom(jobPath);
            Type type = assembly.GetTypes().FirstOrDefault(x => x.Name == className);
            if (type.GetInterface(nameof(IJob)) != null)
            {
                MethodInfo run = type.GetMethod("Run");
                var instance = Activator.CreateInstance(type);
                run.Invoke(instance, null);
            }
            else
            {
                Console.WriteLine("This job doesnt implement IJob interface from IJob.dll");
            }
           
         
        }

       
    }
}
