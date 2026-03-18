using Common.Models;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace LVS.Views
{
    public class ctrASNRead_AsnArticleVdaView
    {
        public ctrASNRead_AsnArticleVdaView(Eingaenge myEingang, Articles myArticle)
        {
            eingang = myEingang.Copy();
            article = myArticle.Copy();
        }

        [DisplayName("ASN")]
        [DataMember]
        public int ASN
        {
            get
            {
                if (eingang is Eingaenge)
                {
                    return (int)eingang.ASN;
                }
                else
                {
                    return 0;
                }
            }
        }
        [DisplayName("Netto")]
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

        [DisplayName("Brutto")]
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
        [DisplayName("Dicke")]
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

        [DisplayName("Breite")]
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

        [DisplayName("Laenge")]
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
        [DisplayName("Höhe")]
        [DataMember]
        public decimal Hoehe
        {
            get
            {
                if (article is Articles)
                {
                    return article.Hoehe;
                }
                else
                {
                    return 0;
                }
            }
        }
        [DisplayName("Anzahl")]
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

        [DisplayName("Einheit")]
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

        [DisplayName("Pos")]
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
        [DisplayName("Werksnummer")]
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

        [DisplayName("Produktionsnummer")]
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

        [DisplayName("Charge")]
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

        [DisplayName("Gut")]
        [DataMember]
        public string Gut
        {
            get
            {
                if (article is Articles)
                {
                    string strReturn = string.Empty;
                    if ((article.Gut is Goodstypes) && (article.Gut.Id > 0))
                    {
                        strReturn = article.Gut.Bezeichnung;
                    }
                    return strReturn;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Bestellnummer")]
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


        [DisplayName("exBezeichnung")]
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

        [DisplayName("exMaterialnummer")]
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

        [DisplayName("Glühdatum")]
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

        [DisplayName("Lieferschein")]
        [DataMember]
        public string LfsNr
        {
            get
            {
                if (eingang is Eingaenge)
                {
                    return article.Lfs;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("KFZ")]
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


        [Browsable(false)]
        public Eingaenge eingang { get; set; }

        [Browsable(false)]
        public Articles article { get; set; }

    }
}
