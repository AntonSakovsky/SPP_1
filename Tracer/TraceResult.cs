using Tracer.Interfaces;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Xml;

namespace Tracer
{
    [XmlRoot("root")]
    public class TraceResult : ISerializer
    {

        [XmlElement("thread")]
        public List<ThreadInfo> Threads { get; }
        public TraceResult(List<ThreadInfo> threadsInfo)
        {
            Threads = threadsInfo;
        }

        public TraceResult() { }

        public string toJSON()
        {
            return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        public string toXML()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResult));

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  "
            };

            using (StringWriter sw = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                xmlSerializer.Serialize(writer, this);
                return sw.ToString();
            }
        }

    }
}
