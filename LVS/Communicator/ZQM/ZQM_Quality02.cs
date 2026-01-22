using LVS.ASN.ASNFormatFunctions;
using LVS.Models;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace LVS.Communicator.ZQM
{
    public class ZQM_Quality02
    {
        internal XmlDocument _iDocZQM;
        internal XmlDocument iDocZQM
        {
            get { return _iDocZQM; }
            set
            {
                _iDocZQM = value;
                iDocXml = _iDocZQM.InnerXml.ToString();
            }
        }

        public string iDocXml { get; set; } = string.Empty;

        //-------- EDI_DC40 -------------------------
        internal const string const_NodeListName_EDI_DC40 = "//EDI_DC40";
        public string iDocTyp { get; set; } = string.Empty;
        public string iDocName { get; set; } = string.Empty;
        public string iDocNo { get; set; } = string.Empty;
        public string iRCVPRN { get; set; } = string.Empty;


        internal XmlNodeList Nl_EDI_DC40
        {
            set
            {
                XmlNodeList nl = value as XmlNodeList;
                if (nl != null)
                {
                    foreach (XmlNode node in nl)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ZQM_NodeEDI_DC40));
                        StringReader reader = new StringReader(node.OuterXml);
                        var NodeEDI_DC40 = (ZQM_NodeEDI_DC40)serializer.Deserialize(reader);
                        reader.Close();

                        if ((NodeEDI_DC40 != null) && (NodeEDI_DC40 is ZQM_NodeEDI_DC40))
                        {
                            iDocTyp = NodeEDI_DC40.IDOCTYP;
                            iDocNo = NodeEDI_DC40.DOCNUM.TrimStart('0');
                            iDocName = NodeEDI_DC40.CIMTYP;
                            iRCVPRN = NodeEDI_DC40.RCVPRN.TrimStart('0');
                        }
                    }
                }
            }
        }

        //-------- E1EDK03 -------------------------

        internal const string const_NodeListName_E1EDK03 = "//E1EDK03";
        public DateTime iDocDate { get; set; } = new DateTime(1900, 1, 1, 0, 0, 0, 0);

        internal XmlNodeList Nl_E1EDK03
        {
            set
            {
                XmlNodeList nl = value as XmlNodeList;
                if (nl != null)
                {
                    foreach (XmlNode node in nl)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ZQM_NodeE1EDK03));
                        StringReader reader = new StringReader(node.OuterXml);
                        var NodeE1EDK03 = (ZQM_NodeE1EDK03)serializer.Deserialize(reader);
                        reader.Close();

                        if ((NodeE1EDK03 != null) && (NodeE1EDK03 is ZQM_NodeE1EDK03))
                        {
                            iDocDate = Format_GlowDateFromEDI.Execute_yyyyMMddToDateTime(NodeE1EDK03.DATUM);
                        }
                    }
                }
            }
        }

        //-------- E1EDK02 --------------------------------------------------------------------------------------- Lfs-Nr
        internal const string const_NodeListName_E1EDK02 = "//E1EDK02";
        public string LfsNo { get; set; } = string.Empty;

        internal XmlNodeList Nl_E1EDK02
        {
            set
            {
                XmlNodeList nl = value as XmlNodeList;
                if (nl != null)
                {
                    foreach (XmlNode node in nl)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ZQM_NodeE1EDK02));
                        StringReader reader = new StringReader(node.OuterXml);
                        var NodeE1EDK02 = (ZQM_NodeE1EDK02)serializer.Deserialize(reader);
                        reader.Close();

                        if ((NodeE1EDK02 != null) && (NodeE1EDK02 is ZQM_NodeE1EDK02))
                        {
                            int iQualf = 0;
                            int.TryParse(NodeE1EDK02.QUALF, out iQualf);

                            switch (iQualf)
                            {
                                case 12:
                                    LfsNo = NodeE1EDK02.BELNR.TrimStart('0');
                                    break;
                            }
                        }
                    }
                }
            }
        }



        //-------- E1EDP02 ------------------------------------------------------------------------------ Produktionsnummer
        internal const string const_NodeListName_E1EDP02 = "//E1EDP02";
        public string Produktionsnummer { get; set; } = string.Empty;
        //public string Bandnummer { get; set; } = string.Empty;

        internal XmlNodeList Nl_E1EDP02
        {
            set
            {
                XmlNodeList nl = value as XmlNodeList;
                if (nl != null)
                {
                    foreach (XmlNode node in nl)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ZQM_NodeE1EDP02));
                        StringReader reader = new StringReader(node.OuterXml);
                        var NodeE1EDP02 = (ZQM_NodeE1EDP02)serializer.Deserialize(reader);
                        reader.Close();

                        if ((NodeE1EDP02 != null) && (NodeE1EDP02 is ZQM_NodeE1EDP02))
                        {
                            int iQualf = 0;
                            int.TryParse(NodeE1EDP02.QUALF, out iQualf);

                            switch (iQualf)
                            {
                                case 69:
                                    this.Produktionsnummer = NodeE1EDP02.BELNR.TrimStart('0');
                                    break;
                                    //case 79:
                                    //    this.Bandnummer = NodeE1EDP02.BELNR;
                                    //    break;
                            }
                        }
                    }
                }
            }
        }


        //-------- E1EDLIN ------------------------------------------------------------------------------ Anzahl + Unit
        internal const string const_NodeListName_E1EDLIN = "//E1EDLIN";
        public decimal Quantity { get; set; } = 0;
        public string Unit { get; set; } = string.Empty;

        internal XmlNodeList Nl_E1EDLIN
        {
            set
            {
                XmlNodeList nl = value as XmlNodeList;
                if (nl != null)
                {
                    foreach (XmlNode node in nl)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ZQM_NodeE1EDLIN));
                        StringReader reader = new StringReader(node.OuterXml);
                        var NodeE1EDLIN = (ZQM_NodeE1EDLIN)serializer.Deserialize(reader);
                        reader.Close();

                        if ((NodeE1EDLIN != null) && (NodeE1EDLIN is ZQM_NodeE1EDLIN))
                        {
                            decimal decTmp = 0;
                            decimal.TryParse(NodeE1EDLIN.QUANTITY, out decTmp);
                            if (decTmp > 0)
                            {
                                this.Quantity = decTmp;
                            }
                            this.Unit = NodeE1EDLIN.UNIT;

                        }
                    }
                }
            }
        }

        //-- Messwerte unter E1CCI01 | CHARACTERISTIC_NAME der Messwertname



        public EdiZQMQalityXml ediZQMQalityXml { get; set; }

        public ZQM_Quality02(XmlDocument myXmlDoc)
        {
            iDocZQM = myXmlDoc;

            Nl_EDI_DC40 = iDocZQM.SelectNodes(const_NodeListName_EDI_DC40);
            Nl_E1EDK03 = iDocZQM.SelectNodes(const_NodeListName_E1EDK03);
            Nl_E1EDK02 = iDocZQM.SelectNodes(const_NodeListName_E1EDK02);
            Nl_E1EDP02 = iDocZQM.SelectNodes(const_NodeListName_E1EDP02);
            Nl_E1EDLIN = iDocZQM.SelectNodes(const_NodeListName_E1EDLIN);

            string str = string.Empty;
        }


        public ZQM_Quality02(string myXml)
        {
            iDocZQM = new XmlDocument();
            iDocZQM.LoadXml(myXml);

            Nl_EDI_DC40 = iDocZQM.SelectNodes(const_NodeListName_EDI_DC40);
            Nl_E1EDK03 = iDocZQM.SelectNodes(const_NodeListName_E1EDK03);
            Nl_E1EDK02 = iDocZQM.SelectNodes(const_NodeListName_E1EDK02);
            Nl_E1EDP02 = iDocZQM.SelectNodes(const_NodeListName_E1EDP02);
            Nl_E1EDLIN = iDocZQM.SelectNodes(const_NodeListName_E1EDLIN);

            string str = string.Empty;
        }


    }
}
