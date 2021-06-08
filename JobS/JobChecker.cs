using JobS.Lib;
using NCrontab;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace JobS
{
    public class JobChecker : IJobChecker
    {
        //private static readonly HttpClient client = new HttpClient();

        TcpClient client;

        public void Check(string jobName)
        {

            while (true)
            {
                try
                {
                    client = new TcpClient(IPAddress.Loopback.ToString(), 1337);
                    break;
                }
                catch (SocketException ex)
                {
                    throw;
                }
            }
            using (var sw = new StreamWriter(client.GetStream()))
            {
                sw.Write(jobName);
            }

            //var jobs = ConfigHelper.GetConfigVal("Jobs");
            
           
            //foreach (var job in jobs)
            //{

            //    string JobCronTime =  job.Value; 
            //    var schedule = CrontabSchedule.Parse(JobCronTime);
            //    var nextExecution = schedule.GetNextOccurrence(DateTime.Now);
            //    double nextExecutionSeconds = Math.Round(nextExecution.Subtract(DateTime.Now).TotalSeconds);

            //    Console.WriteLine(nextExecutionSeconds); 

            //    if (nextExecutionSeconds <= 20)
            //    {
            //        Thread.Sleep(Convert.ToInt32(nextExecutionSeconds)*1000);
            //        while (true)
            //        {
            //            try
            //            {
            //                client = new TcpClient(IPAddress.Loopback.ToString(), 1337);
            //                break;
            //            }
            //            catch (SocketException ex)
            //            {
            //                throw;
            //            }
            //        }
            //        using (var sw = new StreamWriter(client.GetStream()))
            //        {
            //            sw.Write(job.Key);
            //        }
            //    }
            //}

       
        }
    }
}
