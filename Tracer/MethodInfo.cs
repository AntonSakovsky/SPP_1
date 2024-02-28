using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Tracer
{
    
    public class MethodInfo
    {
        [JsonProperty("name")]
        [XmlAttribute("name")]
        public string? MethodName { get; set; }

        [JsonProperty("class")]
        [XmlAttribute("class")]
        public string? ClassName { get; set; }

        [JsonProperty("time")]
        [XmlAttribute("time")]
        public double ExecutionTime { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public Stopwatch Stopwatch { get; set; }

        [JsonProperty("methods")]
        [XmlElement("method")]
        public List<MethodInfo> Methods { get; set; }
        public MethodInfo() { }
        public MethodInfo(string? methodName, string? className)
        {
            MethodName = methodName;
            ClassName = className;
            Methods = new List<MethodInfo>();
            Stopwatch = new Stopwatch();
        }

    }
}
