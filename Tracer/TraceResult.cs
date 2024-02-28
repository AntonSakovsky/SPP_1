using Tracer.Interfaces;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Xml;

namespace Tracer
{
    [XmlRoot("root")]
    public class TraceResult : ISerializer
    {
        private IResultOutput ResultOutput { get; }

        [XmlElement("thread")]
        public List<ThreadInfo> Threads { get; }
        public TraceResult(List<ThreadInfo> threadsInfo, IResultOutput resultOutput)
        {
            Threads = threadsInfo;
            ResultOutput = resultOutput;
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

        public void OutputToConsole()
        {
            ResultOutput.ConsoleOutput(toJSON()); 
        }

        public void OutputToFile(string path)
        {
            ResultOutput.FileOutput(toJSON(), path); 
        }

    }
}
