using  System;
using  System.Collections.Generic;
using  System.Configuration;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.DBUtility
{
   internal class ConfigHelper<T> : ConfigurationSection
    {
        internal static T GetConfig()
        {
            return GetConfig("");
        }
        internal static T GetConfig(string sectionName)
        {
            T section = (T)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }
    }
}
