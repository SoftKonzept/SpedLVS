using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LVS
{
    public class clsDocKey
    {
        public const string const_DocumentArt_Eingang = "Eingang";
        public const string const_DocumentArt_Ausgang = "Ausgang";
        public const string const_DocumentArt_RG = "Rechnung";
        public const string const_DocumentArt_Mail = "Mail";
        public const string const_DocumentArt_ListPrint = "ListPrint";
        public const string const_DocumentArt_ListMiscellaneous = "ListMiscellaneous";

        public Dictionary<int, string> DictDocKey = new Dictionary<int, string>()
        {
            //Eingang
            { 101, enumIniDocKey.LabelAll.ToString()},
            { 102, enumIniDocKey.LabelOne.ToString() },
            { 103, enumIniDocKey.LabelOneNeutral.ToString() },
            { 104, enumIniDocKey.SchadenLabel.ToString() },
            { 105, enumIniDocKey.SchadenDoc.ToString() },
            { 106, enumIniDocKey.SPLLabel.ToString() },
            { 107, enumIniDocKey.SPLDoc.ToString() },
            { 108, enumIniDocKey.EingangDoc.ToString() },
            { 109, enumIniDocKey.EingangAnzeige.ToString() },
            { 110, enumIniDocKey.EingangAnzeigePerDay.ToString() },
            { 111, enumIniDocKey.EingangLfs.ToString() },
            { 112, enumIniDocKey.Eingangsliste.ToString() },
            { 113,enumIniDocKey.EingangAnzeigeMail.ToString() },
            { 114,enumIniDocKey.EingangAnzeigePerDayMail.ToString() },
            { 115,enumIniDocKey.EingangLfsMail.ToString() },

            //Ausgang
            {201, enumIniDocKey.AusgangDoc.ToString() },
            {202, enumIniDocKey.AusgangAnzeige.ToString() },
            {203, enumIniDocKey.AusgangAnzeigePerDay.ToString() },
            {204, enumIniDocKey.AusgangLfs.ToString() },
            {205, enumIniDocKey.Ausgangsliste.ToString() },
            {206, enumIniDocKey.AusgangNeutralDoc.ToString() },
            {207, enumIniDocKey.CMRFrachtbrief.ToString() },
            {208, enumIniDocKey.KVOFrachtbrief.ToString() },
            {209,enumIniDocKey.AusgangAnzeigeMail.ToString() },
            {210,enumIniDocKey.AusgangAnzeigePerDayMail.ToString() },
            {211,enumIniDocKey.AusgangLfsMail.ToString() },

            //Rechnungen
            {301,enumIniDocKey.Lagerrechnung.ToString() },
            {302,enumIniDocKey.Manuellerechnung.ToString() },
            {303,enumIniDocKey.ManuelleGutschrift.ToString() },
            {304,enumIniDocKey.RGAnhang.ToString() },
            {305,enumIniDocKey.RGBuch.ToString() },
            {306,enumIniDocKey.LagerrechnungMail.ToString() },
            {307,enumIniDocKey.ManuellerechnungMail.ToString() },

            //ListPrint
            {401,enumIniDocKey.Bestandsliste.ToString() },
            {402,enumIniDocKey.Journal.ToString() },
            {403,enumIniDocKey.Inventur.ToString() },
            {404,enumIniDocKey.Adressliste.ToString() },
            {405,enumIniDocKey.KundenInformationen.ToString() },
            {406,enumIniDocKey.TarifeKunden.ToString() },


            //Mail 500

            // miscellaneous - Diverses 600
            { 601, enumIniDocKey.LOLabelLinks.ToString() },
            { 602, enumIniDocKey.LOLabelRechts.ToString() },
            { 603, enumIniDocKey.LOLabelBeide.ToString() },
            { 604, enumIniDocKey.LOLabelLinksOben.ToString() },
            { 605, enumIniDocKey.LOLabelRechtsUnten.ToString() },
            { 606, enumIniDocKey.LOLabelBeideLinksObenRechtsUnten.ToString() }
        };


        public List<string> ListDocKeyEingang
        {
            get
            {
                List<string> tmpList = new List<string>();
                tmpList.Add(enumIniDocKey.LabelAll.ToString());
                tmpList.Add(enumIniDocKey.LabelOne.ToString());
                tmpList.Add(enumIniDocKey.LabelOneNeutral.ToString());
                tmpList.Add(enumIniDocKey.SchadenLabel.ToString());
                tmpList.Add(enumIniDocKey.SchadenDoc.ToString());
                tmpList.Add(enumIniDocKey.SPLLabel.ToString());
                tmpList.Add(enumIniDocKey.SPLDoc.ToString());
                tmpList.Add(enumIniDocKey.EingangDoc.ToString());
                tmpList.Add(enumIniDocKey.EingangAnzeige.ToString());
                tmpList.Add(enumIniDocKey.EingangAnzeigePerDay.ToString());
                tmpList.Add(enumIniDocKey.EingangLfs.ToString());
                tmpList.Add(enumIniDocKey.Eingangsliste.ToString());
                tmpList.Add(enumIniDocKey.EingangAnzeigeMail.ToString());
                tmpList.Add(enumIniDocKey.EingangAnzeigePerDayMail.ToString());
                tmpList.Add(enumIniDocKey.EingangLfsMail.ToString());
                return tmpList;
            }
        }
        public List<string> ListDocKeyAusgang
        {
            get
            {
                List<string> tmpList = new List<string>();
                tmpList.Add(enumIniDocKey.AusgangDoc.ToString());
                tmpList.Add(enumIniDocKey.AusgangAnzeige.ToString());
                tmpList.Add(enumIniDocKey.AusgangAnzeigePerDay.ToString());
                tmpList.Add(enumIniDocKey.AusgangLfs.ToString());
                tmpList.Add(enumIniDocKey.Ausgangsliste.ToString());
                tmpList.Add(enumIniDocKey.AusgangNeutralDoc.ToString());
                tmpList.Add(enumIniDocKey.CMRFrachtbrief.ToString());
                tmpList.Add(enumIniDocKey.KVOFrachtbrief.ToString());
                tmpList.Add(enumIniDocKey.AusgangAnzeigeMail.ToString());
                tmpList.Add(enumIniDocKey.AusgangAnzeigePerDayMail.ToString());
                tmpList.Add(enumIniDocKey.AusgangLfsMail.ToString());
                return tmpList;
            }
        }

        public List<string> ListDocKeyMail
        {
            get
            {
                List<string> tmpList = new List<string>();
                return tmpList;
            }
        }

        public List<string> ListDocKeyRechnung
        {
            get
            {
                List<string> tmpList = new List<string>();
                tmpList.Add(enumIniDocKey.Lagerrechnung.ToString());
                tmpList.Add(enumIniDocKey.Manuellerechnung.ToString());
                tmpList.Add(enumIniDocKey.RGBuch.ToString());
                tmpList.Add(enumIniDocKey.ManuelleGutschrift.ToString());
                tmpList.Add(enumIniDocKey.RGAnhang.ToString());
                tmpList.Add(enumIniDocKey.LagerrechnungMail.ToString());
                tmpList.Add(enumIniDocKey.ManuellerechnungMail.ToString());
                return tmpList;
            }
        }

        public List<string> ListDocKeyListPrint
        {
            get
            {
                List<string> tmpList = new List<string>();
                tmpList.Add(enumIniDocKey.Bestandsliste.ToString());
                tmpList.Add(enumIniDocKey.Journal.ToString());
                tmpList.Add(enumIniDocKey.Inventur.ToString());
                tmpList.Add(enumIniDocKey.Adressliste.ToString());
                tmpList.Add(enumIniDocKey.KundenInformationen.ToString());
                tmpList.Add(enumIniDocKey.TarifeKunden.ToString());
                return tmpList;
            }
        }

        public List<string> ListDocKeyMiscellaneous
        {
            get
            {
                List<string> tmpList = new List<string>();
                tmpList.Add(enumIniDocKey.LOLabelLinks.ToString());
                tmpList.Add(enumIniDocKey.LOLabelRechts.ToString());
                tmpList.Add(enumIniDocKey.LOLabelBeide.ToString());
                tmpList.Add(enumIniDocKey.LOLabelLinksOben.ToString());
                tmpList.Add(enumIniDocKey.LOLabelRechtsUnten.ToString());
                tmpList.Add(enumIniDocKey.LOLabelBeideLinksObenRechtsUnten.ToString());
                return tmpList;
            }
        }

        public Dictionary<string, List<string>> DictDocKeyArt
        {
            get
            {
                Dictionary<string, List<string>> tmpDict = new Dictionary<string, List<string>>();
                tmpDict.Add(clsReportDocSetting.const_DocumentArt_Eingang, this.ListDocKeyEingang);
                tmpDict.Add(clsReportDocSetting.const_DocumentArt_Ausgang, this.ListDocKeyAusgang);
                tmpDict.Add(clsReportDocSetting.const_DocumentArt_Mail, this.ListDocKeyMail);
                tmpDict.Add(clsReportDocSetting.const_DocumentArt_RG, this.ListDocKeyRechnung);
                tmpDict.Add(clsReportDocSetting.const_DocumentArt_ListPrint, this.ListDocKeyListPrint);
                tmpDict.Add(clsReportDocSetting.const_DocumentArt_Miscellaneous, this.ListDocKeyMiscellaneous);
                return tmpDict;
            }
        }

        public DataTable dtDocKeys
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(Int32));
                dt.Columns.Add("DocKey", typeof(string));
                //for (Int32 i = 0; i <= this.DictDocKey.Count-1; i++)
                foreach (KeyValuePair<Int32, string> itm in this.DictDocKey)
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = itm.Key;
                    row["DocKey"] = itm.Value;
                    dt.Rows.Add(row);
                }
                return dt;
            }
        }

        public DataTable dtDocKeysForAdrText
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(Int32));
                dt.Columns.Add("DocKey", typeof(string));
                //for (Int32 i = 0; i <= this.DictDocKey.Count-1; i++)


                foreach (KeyValuePair<Int32, string> itm in this.DictDocKey.OrderBy(x => x.Value))
                {
                    enumIniDocKey dKey = new enumIniDocKey();
                    dKey = (enumIniDocKey)Enum.Parse(typeof(enumIniDocKey), itm.Value);
                    switch (dKey)
                    {
                        case enumIniDocKey.EingangDoc:
                        case enumIniDocKey.EingangAnzeige:
                        case enumIniDocKey.EingangLfs:
                        case enumIniDocKey.Eingangsliste:
                        case enumIniDocKey.AusgangDoc:
                        case enumIniDocKey.AusgangAnzeige:
                        case enumIniDocKey.AusgangLfs:
                        case enumIniDocKey.CMRFrachtbrief:
                        case enumIniDocKey.Ausgangsliste:
                        case enumIniDocKey.KVOFrachtbrief:
                        case enumIniDocKey.Lagerrechnung:
                        case enumIniDocKey.Manuellerechnung:
                        case enumIniDocKey.ManuelleGutschrift:
                        case enumIniDocKey.LagerrechnungMail:
                        case enumIniDocKey.ManuellerechnungMail:

                            DataRow row = dt.NewRow();
                            row["ID"] = itm.Key;
                            row["DocKey"] = itm.Value;
                            dt.Rows.Add(row);
                            break;
                    }

                }
                return dt;
            }
        }

        public Int32 GetDocKeyID(string myDocKey)
        {
            Int32 iTmp = 0;
            iTmp = this.DictDocKey.FirstOrDefault(x => x.Value == myDocKey).Key;
            return iTmp;
        }

        public string GetDocKey(int myDocKeyId)
        {
            //Int32 iTmp = 0;
            string strReturn = this.DictDocKey.FirstOrDefault(x => x.Key == myDocKeyId).Value;
            return strReturn;
        }

        public string GetDocKeyArt(string myDocKey)
        {
            string strReturn = string.Empty;
            foreach (KeyValuePair<string, List<string>> itm in this.DictDocKeyArt)
            {
                List<string> tmpList = itm.Value.ToList();
                if (tmpList.Contains(myDocKey))
                {
                    strReturn = itm.Key;
                    break;
                }
            }
            return strReturn;
        }
    }
}