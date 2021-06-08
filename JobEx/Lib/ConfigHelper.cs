using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobEx.Lib
{
    static public class ConfigHelper
    {


        public static string GetConfigVal(string sectionName, string property)
        {


            var value = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection(sectionName)[property];

            return value;
        }

    }
}
