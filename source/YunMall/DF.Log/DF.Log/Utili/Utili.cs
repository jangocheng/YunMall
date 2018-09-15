using  System;
using  System.Collections.Generic;
using  System.IO;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;
using  System.Xml;
using  System.Xml.Serialization;

namespace DF.Log.Utili
{
    internal static class Utili
    {
        /// <summary>
        /// 将指定的对象序列化为XML格式的字符串并返回。
        /// </summary>
        /// <param name="o">待序列化的对象</param>
        /// <returns>返回序列化后的字符串</returns>
        public static string Serialize(Object o)
        {
            string xml = "";
            try {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                using (MemoryStream mem = new MemoryStream()) {
                    using (XmlTextWriter writer = new XmlTextWriter(mem, Encoding.UTF8)) {
                        writer.Formatting = Formatting.Indented;
                        XmlSerializerNamespaces n = new XmlSerializerNamespaces();
                        n.Add("", "");
                        serializer.Serialize(writer, o, n);

                        mem.Seek(0, SeekOrigin.Begin);
                        using (StreamReader reader = new StreamReader(mem)) {
                            xml = reader.ReadToEnd();
                        }
                    }
                }
            } catch { xml = ""; }
            return xml;
        }
    }
}
