using  System;
using  System.Text;
using  log4net.Appender;
using  log4net.Core;
using  log4net.Layout;
using  System.IO;
using  log4net.Util;
using  log4net.Layout.Pattern;
using  System.Reflection;

namespace  DF.Log
{
    internal class CustomPropertyLayout : PatternLayout
    {
        public CustomPropertyLayout()
        {
            this.AddConverter("property", typeof(CustomPropertyPatternConverter));
        }
    }

    internal class CustomPropertyPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            if (Option != null)
            {
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // Write all the key value pairs
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }
        /// <summary>
        /// 通过反射获取传入的日志对象的某个属性的值
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            if (propertyInfo != null)
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
            return propertyValue;
        }
    }

    /// <summary>
    /// 第三方支付报文日志记录
    /// 记录的格式为 请求报文文件 {yyyymmdd}\{tranCode}\{mark}_req.txt 
    ///              响应报文文件 {yyyymmdd}\{tranCode}\{mark}_resp.txt 
    /// </summary>
    /// <author></author>
    internal class CustomFileAppender : AppenderSkeleton
    {
        public CustomFileAppender()
        {
        }

        public PatternLayout File
        {
            get { return m_filePattern; }
            set { m_filePattern = value; }
        }

        public Encoding Encoding
        {
            get { return m_encoding; }
            set { m_encoding = value; }
        }

        public SecurityContext SecurityContext
        {
            get { return m_securityContext; }
            set { m_securityContext = value; }
        }

        override public void ActivateOptions()
        {
            base.ActivateOptions();

            if (m_securityContext == null)
            {
                m_securityContext = SecurityContextProvider.DefaultProvider.CreateSecurityContext(this);
            }
        }

        override protected void Append(LoggingEvent loggingEvent)
        {
            try
            {
                // Render the file name
                StringWriter stringWriter = new StringWriter();
                m_filePattern.Format(stringWriter, loggingEvent);
                string fileName = stringWriter.ToString();

                fileName = SystemInfo.ConvertToFullPath(fileName);

                FileStream fileStream = null;

                using  (m_securityContext.Impersonate(this))
                {
                    // Ensure that the directory structure exists
                    string directoryFullName = Path.GetDirectoryName(fileName);

                    // Only create the directory if it does not exist
                    // doing this check here resolves some permissions failures
                    if (!Directory.Exists(directoryFullName))
                    {
                        Directory.CreateDirectory(directoryFullName);
                    }

                    // Open file stream while impersonating
                    fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }

                if (fileStream != null)
                {
                    using  (StreamWriter streamWriter = new StreamWriter(fileStream, m_encoding))
                    {
                        RenderLoggingEvent(streamWriter, loggingEvent);
                    }

                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Error("Failed to append to file", ex);
            }
        }

        private PatternLayout m_filePattern = null;
        private Encoding m_encoding = Encoding.Default;
        private SecurityContext m_securityContext;
    }
}
