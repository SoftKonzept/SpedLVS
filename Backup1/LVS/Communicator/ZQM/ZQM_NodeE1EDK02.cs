using System.Xml.Serialization;

namespace LVS.Communicator.ZQM
{
    [XmlRoot(ElementName = "E1EDK02")]
    public class ZQM_NodeE1EDK02
    {
        [XmlElement(ElementName = "QUALF")]
        public string QUALF { get; set; }

        [XmlElement(ElementName = "BELNR")]
        public string BELNR { get; set; }

        [XmlElement(ElementName = "DATUM")]
        public string DATUM { get; set; }

        [XmlElement(ElementName = "UZEIT")]
        public string UZEIT { get; set; }

    }
}
