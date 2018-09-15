using  System;
using  System.Collections.Generic;
using  System.Configuration;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.DBUtility
{
    internal class NosqlConfigHelper:ConfigHelper<NosqlConfigHelper>
    {
        [ConfigurationProperty("ConnectionString", IsRequired = true)]
        public string ConnectionString
        {
            get
            {
                return (string)base["ConnectionString"];
            }
            set
            {
                base["ConnectionString"] = value;
            }
        }

        [ConfigurationProperty("DbName", IsRequired = true)]
        public string DbName
        {
            get
            {
                return (string)base["DbName"];
            }
            set
            {
                base["DbName"] = value;
            }
        }
    }
}
