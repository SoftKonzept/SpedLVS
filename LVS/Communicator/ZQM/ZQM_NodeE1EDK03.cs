using System.Xml.Serialization;

namespace LVS.Communicator.ZQM
{
    [XmlRoot(ElementName = "E1EDK03")]
    public class ZQM_NodeE1EDK03
    {
        [XmlElement(ElementName = "IDDAT")]
        public string IDDAT { get; set; }

        [XmlElement(ElementName = "DATUM")]
        public string DATUM { get; set; }

        [XmlElement(ElementName = "UZEIT")]
        public string UZEIT { get; set; }

    }
}
