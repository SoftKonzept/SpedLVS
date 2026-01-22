using System.Xml.Serialization;

namespace LVS.Communicator.ZQM
{
    [XmlRoot(ElementName = "E1EDLIN")]
    public class ZQM_NodeE1EDLIN
    {
        [XmlElement(ElementName = "QUANTITY")]
        public string QUANTITY { get; set; }

        [XmlElement(ElementName = "UNIT")]
        public string UNIT { get; set; }

    }
}
