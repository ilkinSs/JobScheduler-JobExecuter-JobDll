using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobS.Lib
{
    static public class ConfigHelper
    {


        public static IEnumerable<IConfigurationSection> GetConfigVal(string sectionName)
        {    
            var value = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection(sectionName).GetChildren();
            return value;
        }
    }
}
