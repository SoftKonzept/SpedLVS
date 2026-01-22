using Common.Models;
using System;
using System.Runtime.Serialization;

namespace LVS.Views
{
    public class ctrASNRead_AsnArticleEdifactView
    {
        public ctrASNRead_AsnArticleEdifactView(Eingaenge myEingang, Articles myArticle)
        {
            eingang = myEingang.Copy();
            article = myArticle.Copy();
        }
        internal Eingaenge eingang { get; set; }
        internal Articles article { get; set; }

        [DataMember]
        public int ASN
        {
            get
            {
                if (eingang is Eingaenge)
                {
                    return eingang.ASN;
                }
                else
                {
                    return 0;
                }
            }
        }

        [DataMember]
        public string Position
        {
            get
            {
                if (article is Articles)
                {
                    return article.Position;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DataMember]
        public decimal Dicke
        {
            get
            {
                if (article is Articles)
                {
                    return article.Dicke;
                }
                else
                {
                    return 0;
                }
            }
        }
        [DataMember]
        public decimal Breite
        {
            get
            {
                if (article is Articles)
                {
                    return article.Breite;
                }
                else
                {
                    return 0;
                }
            }
        }

        [DataMember]
        public decimal Laenge
        {
            get
            {
                if (article is Articles)
                {
                    return article.Laenge;
                }
                else
                {
                    return 0;
                }
            }
        }

        [DataMember]
        public decimal Netto
        {
            get
            {
                if (article is Articles)
                {
                    return article.Netto;
                }
                else
                {
                    return 0;
                }
            }
        }

        [DataMember]
        public decimal Brutto
        {
            get
            {
                if (article is Articles)
                {
                    return article.Brutto;
                }
                else
                {
                    return 0;
                }
            }
        }

        [DataMember]
        public int Anzahl
        {
            get
            {
                if (article is Articles)
                {
                    return article.Anzahl;
                }
                else
                {
                    return 0;
                }
            }
        }

        [DataMember]
        public string Einheit
        {
            get
            {
                if (article is Articles)
                {
                    return article.Einheit;
                }
                else
                {
                    return "kg";
                }
            }
        }

        [DataMember]
        public string exMaterialnummer
        {
            get
            {
                if (article is Articles)
                {
                    return article.exMaterialnummer;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DataMember]
        public string Bestellnummer
        {
            get
            {
                if (article is Articles)
                {
                    return article.Bestellnummer;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DataMember]
        public string Produktionsnummer
        {
            get
            {
                if (article is Articles)
                {
                    return article.Produktionsnummer;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DataMember]
        public string Werksnummer
        {
            get
            {
                if (article is Articles)
                {
                    return article.Werksnummer;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DataMember]
        public string Charge
        {
            get
            {
                if (article is Articles)
                {
                    return article.Charge;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DataMember]
        public string exBezeichnung
        {
            get
            {
                if (article is Articles)
                {
                    return article.exBezeichnung;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

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
        public string Vehicle
        {
            get
            {
                if (eingang is Eingaenge)
                {
                    if (eingang.IsWaggon) { return eingang.WaggonNr; }
                    else { return eingang.KFZ; }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DataMember]
        public DateTime Glowdate
        {
            get
            {
                if (article is Articles)
                {
                    return article.GlowDate;
                }
                else
                {
                    return new DateTime(1900, 1, 1);
                }
            }
        }



    }
}
