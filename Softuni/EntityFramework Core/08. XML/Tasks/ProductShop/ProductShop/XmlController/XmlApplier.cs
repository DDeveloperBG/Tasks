using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.XmlController
{
    public static class XmlApplier
    {
        public static ICollection<T> Deserialize<T>(string rootElementName, string inputXml)
        {
            XmlSerializer serializer 
                = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootElementName));

            StringReader reader = new StringReader(inputXml);

            return serializer.Deserialize(reader) as ICollection<T>;
        }

        public static string SerializeMany<T>(string rootElementName, T[] input)
        {
            XmlSerializer serializer 
                = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootElementName));

            StringBuilder result = new StringBuilder();
           
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(new StringWriter(result), input, namespaces);

            return result.ToString();
        }

        public static string SerializeOne<T>(T input)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            StringBuilder result = new StringBuilder();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(new StringWriter(result), input, namespaces);

            return result.ToString();
        }
    }
}
