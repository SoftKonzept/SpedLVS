using Common.Constants;
using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Models
{
    [Serializable]
    [DataContract]
    public class Articles
    {
        [DataMember]
        [JsonProperty("id")]
        private int _Id = 0;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("AuftragID")]
        private int _AuftragID = 0;
        public int AuftragID
        {
            get { return _AuftragID; }
            set { _AuftragID = value; }
        }

        [DataMember]
        [JsonProperty("AuftragPos")]
        private int _AuftragPos = 0;
        public int AuftragPos
        {
            get { return _AuftragPos; }
            set { _AuftragPos = value; }
        }

        [DataMember]
        [JsonProperty("GArtID")]
        public int GArtID { get; set; } = 0;

        [DataMember]
        [JsonProperty("Gut")]
        public Goodstypes Gut { get; set; }

        [DataMember]
        [JsonProperty("LVS_ID")]
        private int _LVS_ID = 0;
        public int LVS_ID
        {
            get { return _LVS_ID; }
            set { _LVS_ID = value; }
        }

        [DataMember]
        [JsonProperty("bKZ")]
        public int BKZ { get; set; } = 0;

        [DataMember]
        [JsonProperty("gemGewicht")]
        public decimal gemGewicht { get; set; } = 0;

        [DataMember]
        [JsonProperty("netto")]
        public decimal Netto { get; set; } = 0;

        [DataMember]
        [JsonProperty("brutto")]
        public decimal Brutto { get; set; } = 0;

        [DataMember]
        [JsonProperty("dicke")]
        public decimal Dicke { get; set; } = 0;

        [DataMember]
        [JsonProperty("breite")]
        public decimal Breite { get; set; } = 0;

        [DataMember]
        [JsonProperty("laenge")]
        public decimal Laenge { get; set; } = 0;

        [DataMember]
        [JsonProperty("hoehe")]
        public decimal Hoehe { get; set; } = 0;

        [DataMember]
        [JsonProperty("anzahl")]
        public int Anzahl { get; set; } = 0;

        [DataMember]
        [JsonProperty("einheit")]
        public string Einheit { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("werksnummer")]
        private string _Werksnummer = string.Empty;
        public string Werksnummer
        {
            get { return _Werksnummer; }
            set { _Werksnummer = value; }
        }

        [DataMember]
        [JsonProperty("produktionsnummer")]
        private string _Produktionsnummer = string.Empty;
        public string Produktionsnummer
        {
            get { return _Produktionsnummer; }
            set { _Produktionsnummer = value; }
        }

        [DataMember]
        [JsonProperty("exBezeichnung")]
        public string exBezeichnung { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("charge")]
        private string _Charge = string.Empty;
        public string Charge
        {
            get { return _Charge; }
            set { _Charge = value; }
        }

        [DataMember]
        [JsonProperty("bestellnummer")]
        public string Bestellnummer { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("exMaterialnummer")]
        public string exMaterialnummer { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("position")]
        public string Position { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("gutZusatz")]
        public string GutZusatz { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("eingangChecked")]
        public bool EingangChecked { get; set; } = false;

        [DataMember]
        [JsonProperty("storno")]
        public bool Storno { get; set; } = false;

        [DataMember]
        [JsonProperty("stornoDate")]
        public DateTime StornoDate { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        [JsonProperty("umbuchung")]
        public bool Umbuchung { get; set; } = false;

        [DataMember]
        [JsonProperty("abrufReferenz")]
        public string AbrufReferenz { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("tARef")]
        public string TARef { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("abBereichID")]
        public int AbBereichID { get; set; } = 0;

        [DataMember]
        [JsonProperty("mandantenID")]
        public int MandantenID { get; set; } = 0;

        [DataMember]
        [JsonProperty("LAusgangTableID")]
        public int LAusgangTableID { get; set; } = 0;

        [DataMember]
        [JsonProperty("Ausgang")]
        public Ausgaenge Ausgang { get; set; }


        [DataMember]
        [JsonProperty("LEingangTableID")]
        public int LEingangTableID { get; set; } = 0;

        [DataMember]
        [JsonProperty("Eingang")]
        public Eingaenge Eingang { get; set; }


        [DataMember]
        [JsonProperty("artIDRef")]
        public string ArtIDRef { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("auftragPosTableID")]
        public int AuftragPosTableID { get; set; } = 0;

        [DataMember]
        [JsonProperty("ausgangChecked")]
        public bool AusgangChecked { get; set; } = false;

        [DataMember]
        [JsonProperty("artIDAlt")]
        public int ArtIDAlt { get; set; } = 0;

        [DataMember]
        [JsonProperty("info")]
        public string Info { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("lagerOrt")]
        public int LagerOrt { get; set; } = 0;

        [DataMember]
        [JsonProperty("lagerOrtTable")]
        public string LagerOrtTable { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("exLagerOrt")]
        public string exLagerOrt { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("isLagerArtikel")]
        public bool IsLagerArtikel { get; set; } = false;

        [DataMember]
        [JsonProperty("lVSNr_ALTLvs")]
        public int LVSNr_ALTLvs { get; set; } = 0;

        [DataMember]
        [JsonProperty("aDRLagerNr")]
        public int ADRLagerNr { get; set; } = 0;

        [DataMember]
        [JsonProperty("freigabeAbruf")]
        public bool FreigabeAbruf { get; set; } = false;

        [DataMember]
        [JsonProperty("lZZ")]
        public DateTime LZZ { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        [JsonProperty("werk")]
        public string Werk { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("halle")]
        public string Halle { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("reihe")]
        public string Reihe { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ebene")]
        public string Ebene { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("platz")]
        public string Platz { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("exAuftrag")]
        public string exAuftrag { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("exAuftragPos")]
        public string exAuftragPos { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("aSNVerbraucher")]
        public string ASNVerbraucher { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("uB_AltCalcEinlagerung")]
        public bool UB_AltCalcEinlagerung { get; set; } = false;

        [DataMember]
        [JsonProperty("uB_AltCalcAuslagerung")]
        public bool UB_AltCalcAuslagerung { get; set; } = false;

        [DataMember]
        [JsonProperty("uB_AltCalcLagergeld")]
        public bool UB_AltCalcLagergeld { get; set; } = false;

        [DataMember]
        [JsonProperty("uB_NeuCalcEinlagerung")]
        public bool UB_NeuCalcEinlagerung { get; set; } = false;

        [DataMember]
        [JsonProperty("uB_NeuCalcAuslagerung")]
        public bool UB_NeuCalcAuslagerung { get; set; } = false;

        [DataMember]
        [JsonProperty("uB_NeuCalcLagergeld")]
        public bool UB_NeuCalcLagergeld { get; set; } = false;

        [DataMember]
        [JsonProperty("isVerpackt")]
        public bool IsVerpackt { get; set; } = false;

        [DataMember]
        [JsonProperty("interneInfo")]
        public string interneInfo { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("externeInfo")]
        public string externeInfo { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("guete")]
        public string Guete { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("exLsNoA")]
        public string exLsNoA { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("exLsPosA")]
        public string exLsPosA { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("isMulde")]
        public bool IsMulde { get; set; } = false;

        [DataMember]
        [JsonProperty("isLabelPrint")]
        public bool IsLabelPrint { get; set; } = false;

        [DataMember]
        [JsonProperty("isProblem")]
        public bool IsProblem { get; set; } = false;

        [DataMember]
        [JsonProperty("isKorStVerUse")]
        public bool IsKorStVerUse { get; set; } = false;

        [DataMember]
        [JsonProperty("ignLM")]
        public bool IgnLM { get; set; } = false;

        [DataMember]
        [JsonProperty("abladestelle")]
        public string Abladestelle { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("aSNProduktionsnummer")]
        public string ASNProduktionsnummer { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("eAEingangAltLVS")]
        public string EAEingangAltLVS { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("eAAusgangAltLVS")]
        public string EAAusgangAltLVS { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("isStackable")]
        public bool IsStackable { get; set; } = false;

        [DataMember]
        [JsonProperty("glowDate")]
        public DateTime GlowDate { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        [JsonProperty("scanIn")]
        public DateTime ScanIn { get; set; }
        [DataMember]
        [JsonProperty("scanInUser")]
        public int ScanInUser { get; set; }
        [DataMember]
        [JsonProperty("scanOut")]
        public DateTime ScanOut { get; set; }
        [DataMember]
        [JsonProperty("scanOutUser")]
        public int ScanOutUser { get; set; } = 0;

        [DataMember]
        [JsonProperty("IdentifiedByScan")]
        public DateTime IdentifiedByScan { get; set; }

        [DataMember]
        [JsonProperty("CreatedByScanner")]
        public bool CreatedByScanner { get; set; } = false;

        public string LagerOrtAsString
        {
            get
            {
                string sep = " | ";
                string ret = string.Empty;
                List<string> lo = new List<string>();

                if ((Werk != null) && (!this.Werk.Equals(string.Empty))) { lo.Add(this.Werk); }
                if ((Halle != null) && (!this.Halle.Equals(string.Empty))) { lo.Add(this.Halle); }
                if ((Reihe != null) && (!this.Reihe.Equals(string.Empty))) { lo.Add(this.Reihe); }
                if ((Ebene != null) && (!this.Ebene.Equals(string.Empty))) { lo.Add(this.Ebene); }
                if ((Platz != null) && (!this.Platz.Equals(string.Empty))) { lo.Add(this.Platz); }

                int iCount = 0;
                foreach (string s in lo)
                {
                    if (iCount == lo.Count - 1)
                    {
                        ret += s;
                    }
                    else
                    {
                        ret += s + sep;
                    }
                    iCount++;
                }
                return ret;
            }
        }

        //---------------------------------------------------------------------------------------
        //                      Properties ohne DB Field
        //---------------------------------------------------------------------------------------

        public bool bSchaden { get; set; } = false;
        public int LVSNrBeforeUB { get; set; } = 0;
        public int LVSNrAfterUB { get; set; } = 0;
        public int ArtIDAfterUB { get; set; } = 0;

        public bool IsRL { get; set; } = false;
        public bool bSPL { get; set; } = false;
        public bool bSPLartCert { get; set; } = false;
        public bool IsEMECreate { get; set; } = false;
        public bool IsEMLCreate { get; set; } = false;
        public string Lfs  { get; set; } = string.Empty;

        public Articles Copy()
        {
            return (Articles)this.MemberwiseClone();
        }

        //--- Dient als Zwischenspeicher bei der VDA Verarbeitung
        public string ASN_TMS { get; set; } = string.Empty;
        public string ASN_VehicleNo { get; set; } = string.Empty;
        public void SetArtValue(string propDestination, string strReplaceValue)
        {
            string strProperty = propDestination.Remove(0, propDestination.IndexOf('.') + 1);
            try
            {
                this.GetType().GetProperty(strProperty).SetValue(this, strReplaceValue, null);
                //this.GetType().GetProperty(strProperty).SetValue(this, SourceArt.GetType().GetProperty(strProperty).GetValue(SourceArt, null), null);
            }
            catch (Exception ex)
            { }
        }

        public void CombinateValue(string propDestination, List<string> listPropSource)
        {
            string strSetVal = string.Empty;
            for (Int32 i = 0; i <= listPropSource.Count - 1; i++)
            {
                if (strSetVal.Length == 0)
                {
                    if (this.GetType().GetProperty(listPropSource[i].ToString()).GetValue(this, null) != null)
                    {
                        strSetVal = strSetVal + this.GetType().GetProperty(listPropSource[i].ToString()).GetValue(this, null).ToString();
                    }
                }
                else
                {
                    if (this.GetType().GetProperty(listPropSource[i].ToString()).GetValue(this, null) != null)
                    {
                        //strSetVal = strSetVal + clsArtikel.const + this.GetType().GetProperty(listPropSource[i].ToString()).GetValue(this, null).ToString();
                        strSetVal = strSetVal + constValue_Article.Artikel_ValSeparator + this.GetType().GetProperty(listPropSource[i].ToString()).GetValue(this, null).ToString();
                    }
                }
            }
            this.GetType().GetProperty(propDestination).SetValue(this, strSetVal, null);
            //string strTest = this.Werksnummer;
        }
        ///<summary>clsArtikel / CombinateValue</summary>
        ///<remarks></remarks>
        public void CopyArtValue(string propDestination, ref Articles SourceArt)
        {
            string strProperty = propDestination.Remove(0, propDestination.IndexOf('.') + 1);
            try
            {
                string strValue = SourceArt.GetType().GetProperty(strProperty).GetValue(SourceArt, null).ToString();

                this.GetType().GetProperty(strProperty).SetValue(this, SourceArt.GetType().GetProperty(strProperty).GetValue(SourceArt, null), null);
            }
            catch (Exception ex)
            { }
        }
    }

}
