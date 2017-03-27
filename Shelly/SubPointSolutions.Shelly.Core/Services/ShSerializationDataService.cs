using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace SubPointSolutions.Shelly.Core.Services
{
    public class ShSerializationDataService : ShAppDataServiceBase
    {
        #region static

        static ShSerializationDataService()
        {
            KnownTypes = new List<Type>();

            var types = typeof(ShSerializationDataService).Assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(DataContractAttribute), true).Any());

            RegisterKnownTypes(types);
        }

        #endregion

        #region properties

        public static List<Type> KnownTypes { get; set; }

        #endregion

        #region methods

        public static void RegisterKnownTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
                RegisterKnownType(type);
        }

        public static void RegisterKnownType(Type type)
        {
            if (!KnownTypes.Contains(type))
                KnownTypes.Add(type);

        }

        public string Serialize(object obj)
        {
            var serializer = new DataContractSerializer(obj.GetType(), KnownTypes);

            Stream ms = null;
            StreamReader sr = null;

            try
            {
                ms = new MemoryStream();

                serializer.WriteObject(ms, obj);
                ms.Position = 0;

                sr = new StreamReader(ms);

                return sr.ReadToEnd();
            }
            finally
            {
                if (sr != null)
                    sr.Dispose();
                else if (ms != null)
                    ms.Dispose();
            }
        }

        public object Deserialize(Type type, string objString)
        {
            var serializer = new DataContractSerializer(type, KnownTypes);

            Stream stream = null;
            StreamWriter writer = null;

            var xmlDoc = XDocument.Parse(objString);

            var clients = xmlDoc.Descendants()
                .Where(d => d.Name.LocalName == "UnitTest1.Client");

            foreach (var client in clients)
            {
                var props = client.Descendants().ToArray();

                foreach (var p in props)
                {
                    if (!p.Descendants().Any(d => d.Name.LocalName == "Value"))
                    {
                        var value = p.Value;

                        var refValue = new XElement("Value", value);
                        p.Value = string.Empty;

                        p.Add(refValue);
                    }
                }
            }

            var xml = xmlDoc.ToString();
            objString = xml.Replace("Value xmlns=\"\"", "Value");
            try
            {
                stream = new MemoryStream();
                writer = new StreamWriter(stream);

                writer.Write(objString);
                writer.Flush();

                stream.Position = 0;

                return serializer.ReadObject(stream);

            }
            finally
            {
                if (writer != null)
                    writer.Dispose();
                else if (stream != null)
                    stream.Dispose();
            }
        }

        #endregion
    }
}
