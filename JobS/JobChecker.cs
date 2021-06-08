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
        }
    }
}
