using Common.Models;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace LVS.Views
{
    [Serializable]
    [DataContract]
    public class ctrASNRead_AsnVdaView
    {
        public ctrASNRead_AsnVdaView()
        { }
        public ctrASNRead_AsnVdaView(Asn myAsn, EingangViewData myEingangVD, DataTable myDtAsnValues) : this()
        {
            AsnMessage = myAsn;
            if (myEingangVD.Eingang is Eingaenge)
            {
                eingang = myEingangVD.Eingang.Copy();
                eingang.ASN = AsnMessage.Id;
            }
            ListArticleInEingang = myEingangVD.ListArticleInEingang.ToList();
            if (myDtAsnValues != null)
            {
                dtAsnValues = myDtAsnValues.Copy();
            }
        }
        [Browsable(false)]
        public Asn AsnMessage { get; set; }

        [Browsable(false)]
        public Eingaenge eingang { get; set; } = new Eingaenge();

        [DisplayName("Select")]
        [DataMember]
        public bool Select { get; set; } = false;

        [DisplayName("ASN")]
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

        [DisplayName("ASN-Datum")]
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

        [DisplayName("Auftraggeber")]
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

        [DisplayName("Empfänger")]
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

        [DisplayName("Lieferant")]
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

        [DisplayName("ExTransportRef")]
        [DataMember]
        //public string ExTransportRef { get; set; } = string.Empty;
        public string ExTransportRef
        {
            get
            {
                if (eingang is Eingaenge)
                {
                    return eingang.ExTransportRef;
                }
                else
                {
                    return string.Empty;
                }
            }
        }



        [DisplayName("Lieferschein-Nr")]
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

        [DisplayName("VS-Datum")]
        [DataMember]
        public DateTime VS_Datum { get; set; } = new DateTime(1900, 1, 1);

        [DisplayName("RefAuftraggeber")]
        [DataMember]
        public string RefAuftraggeber { get; set; } = string.Empty;

        [DisplayName("RefEmpfaenger")]
        [DataMember]
        public string RefEmpfaenger { get; set; } = string.Empty;

        [DisplayName("Id")]
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

        //[DataMember]
        [Browsable(false)]
        public List<Articles> ListArticleInEingang { get; set; } = new List<Articles>();

        //[DataMember]
        [Browsable(false)]
        public DataTable dtAsnValues { get; set; } = new DataTable();



    }
}
