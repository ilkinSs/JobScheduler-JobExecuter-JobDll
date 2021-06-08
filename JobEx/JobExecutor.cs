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
              
                var instance = Activator.CreateInstance(type);
                MethodInfo run = type.GetMethod("Run");
                var x = typeof(IJob.IJob).GetMethod(run.Name);

                if(x.ReturnType.FullName == run.ReturnType.FullName && x.ReturnParameter.ParameterType.FullName== run.ReturnParameter.ParameterType.FullName && x.Name == run.Name)
                {
                    run.Invoke(instance, null);

                }

            }
            else
            {
                Console.WriteLine("This job doesnt implement IJob interface from IJob.dll");
            }
           
         
        }

       
    }
}
