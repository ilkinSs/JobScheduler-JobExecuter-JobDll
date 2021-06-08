using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;

namespace JobEx
{
    class Program
    {
        
        static void Main(string[] args)
        {
            JobExecutor jobExecutor = new JobExecutor();
            string jobname = "";
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 1337);
            server.Start();

            while (true)
            {
                var client = server.AcceptTcpClient();

                using (var sr = new StreamReader(client.GetStream()))
                {
                    jobname = sr.ReadToEnd();
                }
                if (jobname != null)
                {
                    jobExecutor.Run(jobname);

                }
            }

          
         
        }

    }
}
