using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.DBUtility
{
    /// <summary>
    /// SQL实体
    /// </summary>
    public class SQLEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbParameter[] Parameter { get; set; }

    }
}
