using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Xml
{
    public static class XmlApplier
    {
        public static T[] Deserialize<T>(string xml, string rootName)
        {
            var serializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootName));

            return serializer.Deserialize(new StringReader(xml)) as T[];
        }

        public static string SerializeCollection<T>(T[] collection, string rootName)
        {
            var serializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootName));

            var result = new StringBuilder();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(new StringWriter(result), collection, namespaces);

            return result.ToString();
        }
    }
}
