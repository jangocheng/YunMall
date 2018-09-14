using  System;
using  System.Collections.Generic;
using  System.Configuration;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // redis配置节类
    // 用于读取配置文件中RedisConfig配置节中的参数信息
    // ========================================创建信息========================================
    // 创建人：   
    // 创建时间： 2016-5-21     	
    // ========================================变更信息========================================
    // 日期          版本        修改人         描述
    // 
    // ========================================================================================
    /// <summary>
    /// redis配置节类
    /// </summary>
    public sealed class RedisConfig : ConfigurationSection
    {
        public static RedisConfig GetConfig()
        {
            RedisConfig section = GetConfig("RedisConfig");
            return section;
        }

        public static RedisConfig GetConfig(string sectionName)
        {
            RedisConfig section = (RedisConfig)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }
        /// <summary>
        /// 可写的Redis链接地址
        /// </summary>
        [ConfigurationProperty("WriteServerConStr", IsRequired = false)]
        public string WriteServerConStr
        {
            get
            {
                return (string)base["WriteServerConStr"];
            }
            set
            {
                base["WriteServerConStr"] = value;
            }
        }

        /// <summary>
        /// 可读的Redis链接地址
        /// </summary>
        [ConfigurationProperty("ReadServerConStr", IsRequired = false)]
        public string ReadServerConStr
        {
            get
            {
                return (string)base["ReadServerConStr"];
            }
            set
            {
                base["ReadServerConStr"] = value;
            }
        }
        /// <summary>
        /// 最大写链接数
        /// </summary>
        [ConfigurationProperty("MaxWritePoolSize", IsRequired = false, DefaultValue = 5)]
        public int MaxWritePoolSize
        {
            get
            {
                int _maxWritePoolSize = (int)base["MaxWritePoolSize"];
                return _maxWritePoolSize > 0 ? _maxWritePoolSize : 5;
            }
            set
            {
                base["MaxWritePoolSize"] = value;
            }
        }


        /// <summary>
        /// 最大读链接数
        /// </summary>
        [ConfigurationProperty("MaxReadPoolSize", IsRequired = false, DefaultValue = 5)]
        public int MaxReadPoolSize
        {
            get
            {
                int _maxReadPoolSize = (int)base["MaxReadPoolSize"];
                return _maxReadPoolSize > 0 ? _maxReadPoolSize : 5;
            }
            set
            {
                base["MaxReadPoolSize"] = value;
            }
        }


        /// <summary>
        /// 自动重启
        /// </summary>
        [ConfigurationProperty("AutoStart", IsRequired = false, DefaultValue = true)]
        public bool AutoStart
        {
            get
            {
                return (bool)base["AutoStart"];
            }
            set
            {
                base["AutoStart"] = value;
            }
        }



        /// <summary>
        /// 本地缓存到期时间，单位:秒
        /// </summary>
        [ConfigurationProperty("LocalCacheTime", IsRequired = false, DefaultValue = 36000)]
        public int LocalCacheTime
        {
            get
            {
                return (int)base["LocalCacheTime"];
            }
            set
            {
                base["LocalCacheTime"] = value;
            }
        }


        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项
        /// </summary>
        [ConfigurationProperty("RecordeLog", IsRequired = false, DefaultValue = false)]
        public bool RecordeLog
        {
            get
            {
                return (bool)base["RecordeLog"];
            }
            set
            {
                base["RecordeLog"] = value;
            }
        }

        /// <summary>
        /// 作为Session使用时,Session的有效时间
        /// </summary>
        [ConfigurationProperty("SessionTimeOut", IsRequired = false, DefaultValue = 20)]
        public int SessionTimeOut
        {
            get
            {
                return (int)base["SessionTimeOut"];
            }
            set
            {
                base["SessionTimeOut"] = value;
            }
        }

    }
}
