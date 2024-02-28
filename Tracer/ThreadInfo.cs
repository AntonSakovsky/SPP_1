using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Tracer
{
    [XmlType("thread")]
    public class ThreadInfo
    {

        [JsonProperty("id", Order = 1)]
        [XmlAttribute("id")]
        public int Id {  get; set; }

        [JsonProperty("time", Order = 2)]
        [XmlAttribute("time")]
        public string? Time { get; set; }

        [JsonProperty("methods", Order = 3)]
        [XmlElement("method")]
        public List<MethodInfo> Methods { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public Stack<MethodInfo> Stack { get; set; }

        public ThreadInfo() { }

        public ThreadInfo(int id)
        {
            Id = id;
            Time = "";
            Methods = new List<MethodInfo>();
            Stack = new Stack<MethodInfo>();

        }
        public ThreadInfo(int id, string time, List<MethodInfo> methods) 
        {
            Id = id;
            Time = time;
            Methods = methods;
            Stack = new Stack<MethodInfo>();

        }
    }
}
