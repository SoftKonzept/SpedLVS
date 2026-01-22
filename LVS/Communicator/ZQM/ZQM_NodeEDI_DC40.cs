using System.Xml.Serialization;

namespace LVS.Communicator.ZQM
{
    [XmlRoot(ElementName = "EDI_DC40")]
    public class ZQM_NodeEDI_DC40
    {
        [XmlElement(ElementName = "DOCNUM")]
        public string DOCNUM { get; set; }

        [XmlElement(ElementName = "IDOCTYP")]
        public string IDOCTYP { get; set; }

        [XmlElement(ElementName = "CIMTYP")]
        public string CIMTYP { get; set; }

        [XmlElement(ElementName = "RCVPRN")]
        public string RCVPRN { get; set; }

    }
}
