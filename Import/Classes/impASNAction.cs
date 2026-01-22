using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class impASNAction
    {
        internal clsSystemImport SysImport;
        internal ADR Adr;
        public List<clsADR> ListSource;
        public List<clsADR> ListDest;
        public List<string> ListLog;


        public delegate void SetProzessInforamtion(List<string> myInfoList);
        public event EventHandler<List<string>> SetProzessInforamtionHandler;

        public impASNAction(clsSystemImport mySys)
        {
            this.SysImport = mySys;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DoImport()
        {
            string strTmp = string.Empty;
            bool bReturn = false;
            ListLog = new List<string>();

            //--- Source Adressen ermitteln
            FillListSource();

            if (this.ListSource.Count > 0)
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("-------- ASN Communication");
                ListLog.Add(strTmp);

                int iCount = 0;
                foreach (clsADR item in this.ListSource)
                {
                        iCount++;
                    //--- check exist asnaction
                    strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString(" [" + item.ID.ToString("00#") + "/" + item.ViewID + "] - ");
                    ListLog.Add(strTmp);

                    clsASNAction asnA = new clsASNAction();
                    asnA.InitClass(ref this.SysImport.GLUser);
                    asnA.Empfaenger = 18;
                    asnA.Auftraggeber = item.ID;
                    asnA.MandantenID = this.SysImport.AbBereich.MandantenID;
                    asnA.AbBereichID = this.SysImport.AbBereich.ID;


                    //Eingänge
                    asnA.ASNActionProcessNr = clsASNAction.const_ASNAction_Eingang;
                    asnA.ASNActionName = clsASNAction.const_ASNActionName_Eingang;
                    asnA.OrderID = 1;
                    asnA.ASNTypID = clsASNTyp.GetAsnTypId(this.SysImport.GLUser, clsASNTyp.const_string_ASNTyp_EME);
                    asnA.Bemerkung = "EME an Empfänger";
                    asnA.activ = true;
                    asnA.IsVirtFile = true;
                    if (!asnA.ExistAction())
                    {
                        asnA.Add();
                        strTmp = string.Empty;
                        strTmp = helper_LogStringCreater.CreateString("[add AsnAction] - [" + clsASNTyp.const_string_ASNTyp_EME+ "] - ");
                        ListLog.Add(strTmp);
                    }

                    asnA.ASNActionProcessNr = clsASNAction.const_ASNAction_Eingang;
                    asnA.ASNActionName = clsASNAction.const_ASNActionName_Eingang;
                    asnA.OrderID = 1;
                    asnA.ASNTypID = clsASNTyp.GetAsnTypId(this.SysImport.GLUser, clsASNTyp.const_string_ASNTyp_EML);
                    asnA.Bemerkung = "EML an Empfänger";
                    asnA.activ = false;
                    asnA.IsVirtFile = false;
                    if (!asnA.ExistAction())
                    {
                        asnA.Add();
                    strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString("[add AsnAction] - [" + clsASNTyp.const_string_ASNTyp_EML + "]");
                    ListLog.Add(strTmp);
                    }


                    // Ausgänge
                    asnA.ASNActionProcessNr = clsASNAction.const_ASNAction_Ausgang;
                    asnA.ASNActionName = clsASNAction.const_ASNActionName_Ausgang;
                    asnA.OrderID = 1;
                    asnA.ASNTypID = clsASNTyp.GetAsnTypId(this.SysImport.GLUser, clsASNTyp.const_string_ASNTyp_AME);
                    asnA.Bemerkung = "AME an Empfänger";
                    asnA.activ = true;
                    asnA.IsVirtFile = false;
                    if (!asnA.ExistAction())
                    {
                        asnA.Add();
                        strTmp = string.Empty;
                        strTmp = helper_LogStringCreater.CreateString("[add AsnAction] - [" + clsASNTyp.const_string_ASNTyp_AME + "]");
                        ListLog.Add(strTmp);
                    }

                    asnA.ASNActionProcessNr = clsASNAction.const_ASNAction_Ausgang;
                    asnA.ASNActionName = clsASNAction.const_ASNActionName_Ausgang;
                    asnA.OrderID = 1;
                    asnA.ASNTypID = clsASNTyp.GetAsnTypId(this.SysImport.GLUser, clsASNTyp.const_string_ASNTyp_AML);
                    asnA.Bemerkung = "AML an Empfänger";
                    asnA.activ = false;
                    asnA.IsVirtFile = false;
                    if (!asnA.ExistAction())
                    {
                        asnA.Add();
                        strTmp = string.Empty;
                        strTmp = helper_LogStringCreater.CreateString("[add AsnAction] - [" + clsASNTyp.const_string_ASNTyp_AML + "]");
                        ListLog.Add(strTmp);
                    }

                    // ADR Verweise
                    clsADRVerweis ver = new clsADRVerweis();
                    ver.InitClass(this.SysImport.GLUser);
                    ver.SenderAdrID = item.ID;
                    ver.ASNFileTyp = enumASNFileTyp.VDA4913.ToString();
                    ver.MandantenID = this.SysImport.AbBereich.MandantenID;
                    ver.ArbeitsbereichID = this.SysImport.AbBereich.ID;
                    ver.SupplierNo = string.Empty;
                    ver.aktiv = true;
                    ver.UseS712F04 = true;
                    ver.UseS713F13 = true;
                    ver.SenderVerweis = string.Empty;
                    ver.Bemerkung = string.Empty;
                    ver.LieferantenVerweis = string.Empty;
                    ver.Verweis = string.Empty;

                    //-- SENDER
                    ver.VerweisArt = "SENDER";
                    ver.VerweisAdrID = item.ID;
                    if (!ver.ExistVerweis())
                    {
                        ver.Add();
                        strTmp = string.Empty;
                        strTmp = helper_LogStringCreater.CreateString("[add AdrVerweis] - [" + ver.VerweisArt + "]");
                        ListLog.Add(strTmp);
                    }

                    //-- SENDER
                    ver.VerweisArt = "RECEIVER";
                    ver.VerweisAdrID = 18;
                    if (!ver.ExistVerweis())
                    {
                        ver.Add();
                        strTmp = string.Empty;
                        strTmp = helper_LogStringCreater.CreateString("[add AdrVerweis] - [" + ver.VerweisArt + "]");
                        ListLog.Add(strTmp);
                    } 
                }
                SetProzessInforamtionHandler(this, ListLog);
            }
            else
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("ADR Source Anzahl: 0 !!!");
                ListLog.Add(strTmp);
            }
            return bReturn;
        }
        /// <summary>
        ///             SOURCE - Güterarten
        /// </summary>
        private void FillListSource()
        {
            ListSource = new List<clsADR>();
            DataTable dt = clsADR.GetADRList(this.SysImport.GLUser.User_ID);
            foreach (DataRow row in dt.Rows)
            {
                string strId = row["ID"].ToString();
                decimal decTmp =0;
                decimal.TryParse(strId, out decTmp);
                if (decTmp > 0)
                {
                    clsADR adr = new clsADR();
                    adr.InitClass(this.SysImport.GLUser, this.SysImport.GLSystem, decTmp, true);
                    ListSource.Add(adr);
                }
            }
        }


    }
}
