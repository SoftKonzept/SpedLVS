using Common.Models;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace LVS.Views
{
    [Serializable]
    [DataContract]
    public class ctrASNRead_AsnEdifactView
    {
        public ctrASNRead_AsnEdifactView(Asn myAsn, EingangViewData myEingangVD)
        {
            AsnMessage = myAsn;
            eingang = myEingangVD.Eingang.Copy();
            ListArticleInEingang = myEingangVD.ListArticleInEingang.ToList();
        }

        public Asn AsnMessage { get; set; }
        public Eingaenge eingang { get; set; }


        [DataMember]
        public bool Select { get; set; } = false;

        [DataMember]
        public int ASN
        {
            get
            {
                if (AsnMessage is Asn)
                {
                    return AsnMessage.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        [DataMember]
        public DateTime ASN_Datum
        {
            get
            {
                if (AsnMessage is Asn)
                {
                    return AsnMessage.Datum;
                }
                else
                {
                    return new DateTime(1900, 1, 1);
                }
            }
        }

        [DataMember]
        public string AuftraggeberView
        {
            get
            {
                if (eingang is Eingaenge)
                {
                    return eingang.AuftraggeberString;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DataMember]
        public string EmpfaengerView
        {
            get
            {
                if (eingang is Eingaenge)
                {
                    return eingang.EmpfaengerString;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        //[DataMember]
        //public string RefAuftraggeber { get; set; } = string.Empty;

        [DataMember]
        public string Lieferantennummer
        {
            get
            {
                if (eingang is Eingaenge)
                {
                    return eingang.Lieferant;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DataMember]
        public string ExTransportRef { get; set; } = string.Empty;

        //[DataMember]
        //public DateTime VS_Datum { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        public string LfsNr
        {
            get
            {
                if (eingang is Eingaenge)
                {
                    return eingang.LfsNr;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DataMember]
        public int Id
        {
            get
            {
                if (AsnMessage is Asn)
                {
                    return AsnMessage.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        [DataMember]
        public List<Articles> ListArticleInEingang { get; set; } = new List<Articles>();

    }
}
