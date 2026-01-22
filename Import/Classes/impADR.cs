using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Import;
using Import.Enumerations;
using LVS;

namespace Import
{
    public class impADR
    {
        internal clsSystemImport SysImport;
        internal ADR Adr;
        public List<ADR> ListSource;
        public List<clsADR> ListDest;
        public List<string> ListLog;
        internal frmADRSelection AdrSelection;

        public delegate void SetProzessInfo();
        public event EventHandler SetProzessInfoEventHandler;

        public impADR(clsSystemImport mySys)
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

            //--- Source Bereinigen 
            CleanUpSource();

            //--- Source Adressen ermitteln
            FillListSource();

            if (this.ListSource.Count > 0)
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("-------- ADR");
                ListLog.Add(strTmp);

                //--- Bestehende Daten ermitteln
                ListDest = helper_sql_clsADR.GetAdrList(this.SysImport);

                int iCount = 0;
                foreach (ADR item in this.ListSource)
                {
                    iCount++;
                    strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString(" ["+iCount.ToString("00#")+"/"+this.ListSource.Count.ToString("00#") +"] - ");

                    if (this.ListDest.Count > 0)
                    {
                        clsADR tmpCheckAdr = this.ListDest.FirstOrDefault(x => x.ViewID == item.SUCHB.Trim() && x.PLZ == item.PLZ && x.Ort == item.ORT);
                        if (tmpCheckAdr is clsADR)
                        {
                            strTmp += "[exist] -> " + item.SUCHB.ToString();
                            if (
                                    (tmpCheckAdr.Verweis is null) ||
                                    (tmpCheckAdr.Verweis != item.VERWEIS.Trim())
                               )
                            {
                                tmpCheckAdr.Verweis = item.VERWEIS.Trim();
                                tmpCheckAdr.Update();
                                foreach (clsADR adr in ListDest)
                                {
                                    if (adr.ID == tmpCheckAdr.ID)
                                    {
                                        adr.Verweis = tmpCheckAdr.Verweis;
                                        break;
                                    }
                                }
                                //ListDest = helper_sql_clsADR.GetAdrList(this.SysImport);
                            }
                        }
                        else
                        {
                            if (CheckSelection(item, this.ListDest))
                            {
                                InsertDestination(item);
                                strTmp += "[add]   -> " + item.SUCHB.ToString();
                                ListLog.Add(strTmp);
                            }
                        }
                    }
                    else
                    {
                        //Check ob schon vorhanden
                        //if (!clsGut.ViewIDExists(this.SysImport.GLUser, item.TEXT1))
                        if (!this.ListDest.Any(x => x.ViewID == item.SUCHB.Trim()))
                        {
                            InsertDestination(item);
                            strTmp += "[add]   -> " + item.SUCHB.ToString();
                            ListLog.Add(strTmp);
                        }
                        else
                        {
                            strTmp += " existiert bereits!";
                        }
                    }

                }
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
        ///             Return =
        ///             true -> Adressdatensatz soll so importiert werden
        ///             false -> Adresse ist manuell zugewiesen, und Daten in der DB wurden angepasst, kein Import
        /// </summary>
        /// <param name="myAdrItem"></param>
        /// <param name="myDestList"></param>
        private bool CheckSelection(ADR myAdrItem, List<clsADR> myDestList)
        {
            bool bReturn = false;
            List<clsADR> tmpDest = new List<clsADR>();
            tmpDest = myDestList.Where(x => x.ViewID.Contains(myAdrItem.SUCHB.Trim()) || x.Ort.ToUpper() == myAdrItem.ORT.Trim().ToUpper() || x.PLZ == myAdrItem.PLZ).ToList();
            if (tmpDest.Count == 0)
            {
                tmpDest = myDestList.Where(x => x.ViewID.StartsWith(myAdrItem.SUCHB.Trim().Substring(0,3)) || 
                                           x.Ort.ToUpper().StartsWith(myAdrItem.ORT.Trim().Substring(0,3)) || 
                                           x.PLZ.StartsWith(myAdrItem.PLZ.Trim().Substring(0,2))
                                           ).ToList();
            }

            AdrSelection = new frmADRSelection(tmpDest, myAdrItem, this.SysImport.GLSystem, this.SysImport.GLUser);
            AdrSelection.ShowDialog();
            if (AdrSelection.SelectedAdrDest is clsADR)
            {
                if (
                        (AdrSelection.SelectedAdrDest.ID > 0)
                    )
                {
                    // jetzt müssen die Daten in der zu importierenden Datenbank angepasst werden,
                    // da die Adresse vorhanden aber unterschiedlich benannt o. ä.
                    // - Artikel, Eingänge jeweils die Daten von Auftraggeber, Empfänger, Versender anpassen
                    helper_sql_ART.UpdateArtADRDaten(this.SysImport.GLUser, enumADRArt.Auftraggeber, AdrSelection.SelectedAdrDest.ViewID, myAdrItem.SUCHB.Trim());
                    helper_sql_ART.UpdateArtADRDaten(this.SysImport.GLUser, enumADRArt.Empfänger, AdrSelection.SelectedAdrDest.ViewID, myAdrItem.SUCHB.Trim());
                    helper_sql_EA.UpdateEAADRDaten(this.SysImport.GLUser, enumADRArt.Auftraggeber, AdrSelection.SelectedAdrDest.ViewID, myAdrItem.SUCHB.Trim());
                    helper_sql_EA.UpdateEAADRDaten(this.SysImport.GLUser, enumADRArt.Empfänger, AdrSelection.SelectedAdrDest.ViewID, myAdrItem.SUCHB.Trim());

                    AdrSelection.SelectedAdrDest.Verweis = myAdrItem.VERWEIS.Trim();
                    AdrSelection.SelectedAdrDest.Update();
                    foreach (clsADR adr in ListDest)
                    {
                        if (adr.ID == AdrSelection.SelectedAdrDest.ID)
                        {
                            adr.Verweis = AdrSelection.SelectedAdrDest.Verweis;
                            break;
                        }
                    }
                }
            }
            else
            {
                // es soll die Adresse so importiert werden
                bReturn = true;
            }            
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myCls"></param>
        private void InsertDestination(ADR myCls)
        {
            clsADR tmp = new clsADR();
            tmp.InitClass(this.SysImport.GLUser, this.SysImport.GLSystem, 0, true);
            tmp.ViewID = myCls.SUCHB.Trim();
            tmp.FBez = "Firma";
            tmp.Name1 = myCls.F1.Trim();
            tmp.Name2 = myCls.F2.Trim();
            tmp.Name3 = myCls.F3.Trim();
            tmp.Str = myCls.STRA.Trim();
            tmp.PF = string.Empty;
            tmp.HausNr = string.Empty;
            tmp.PLZ = myCls.PLZ.Trim();
            tmp.PLZPF = string.Empty;
            tmp.Ort= myCls.ORT.Trim();
            tmp.OrtPF = string.Empty;
            tmp.Land = "Deutschland";
            tmp.WAvon = Convert.ToDateTime("01.01.1900 06:00");
            tmp.WAbis = Convert.ToDateTime("01.01.1900 16:00");
            tmp.Dummy = false;
            tmp.LKZ = "D";
            tmp.UserInfoTxt = string.Empty;
            tmp.activ = true;
            tmp.Lagernummer = 0;
            tmp.ASNCommunication = true;
            tmp.AdrID_Be = 0;
            tmp.AdrID_Ent = 0;
            tmp.AdrID_Post = 0;
            tmp.AdrID_RG = 0;
            tmp.IsAuftraggeber = true;
            tmp.IsVersender = true;
            tmp.IsBelade = true;
            tmp.IsEntlade = true;
            tmp.IsPost = true;
            tmp.IsRG = true;
            tmp.CalcLagerVers = false;
            tmp.DocAuslagerAnzeige = string.Empty;
            tmp.DocEinlagerAnzeige = string.Empty;
            tmp.Verweis = myCls.VERWEIS.Trim();
            tmp.PostRGBy = 0;
            tmp.PostAnlageBy = 0;
            tmp.PostLfsBy = 0;
            tmp.PostListBy = 0;
            tmp.IsDiv = false;
            tmp.IsSpedition = false;
            tmp.PostAnzeigeBy = 0;
            
            tmp.Add();

            //Kundendaten hinterlegen
            if (tmp.ID > 0)
            {
                clsKunde kd = new clsKunde();
                kd.InitClass(this.SysImport.GLUser, tmp.ID, 0);
                kd.KD_ID = 0;
                kd.ADR_ID = tmp.ID;
                kd.SetDefValueToKDDaten();
                kd.Add();
            }
        }

        /// <summary>
        ///             SOURCE - Güterarten
        /// </summary>
        private void FillListSource()
        {
            ListSource = new List<ADR>();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADR ;";
            DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, this.SysImport.GLUser.User_ID, "ADR");
            foreach (DataRow row in dt.Rows)
            {
                ADR tmpCls = new ADR();
                tmpCls = FillGTCls(row);

                if (
                    (tmpCls.SUCHB != null) &&
                    (!tmpCls.SUCHB.Equals(string.Empty))
                  )
                {
                    ListSource.Add(tmpCls);
                }
            }
        }
        /// <summary>
        ///             SOURCE - Güterarten
        /// </summary>
        /// <param name="myRow"></param>
        /// <returns></returns>
        private ADR FillGTCls(DataRow myRow)
        {
            ADR tmpCls = new ADR();
            //int iTmp = 0;
            //int.TryParse(myRow["NR"].ToString(), out iTmp);
            //tmpCls.NR = iTmp;
            tmpCls.SUCHB = myRow["SUCHB"].ToString().Trim();
            tmpCls.F1 = string.Empty;
            if (tmpCls.F1 != null)
            {
                tmpCls.F1 = myRow["F1"].ToString().Trim();
            }
            tmpCls.F2 = string.Empty;
            if (tmpCls.F2 != null)
            {
                tmpCls.F2 = myRow["F2"].ToString().Trim();
            }
            tmpCls.F3 = string.Empty;
            if (tmpCls.F3 != null)
            {
                tmpCls.F3 = myRow["F3"].ToString().Trim();
            }
            tmpCls.STRA = string.Empty;
            if (tmpCls.STRA != null)
            {
                tmpCls.STRA = myRow["STRA"].ToString().Trim();
            }
            tmpCls.PLZ = string.Empty;
            if (tmpCls.PLZ != null)
            {
                string strPLZ = myRow["PLZ"].ToString().Trim();                
                tmpCls.PLZ = helper_Regex.OnlyNumbers(strPLZ);
            }
            tmpCls.ORT = string.Empty;
            if (tmpCls.ORT != null)
            {
                tmpCls.ORT = myRow["ORT"].ToString().Trim();
            }
            tmpCls.VERWEIS = string.Empty;
            if (tmpCls.ORT != null)
            {
                tmpCls.VERWEIS = myRow["VERWEIS"].ToString().Trim();
            }
            
            return tmpCls;
        }
        /// <summary>
        ///             DELETE WHERE SUCHB = null
        /// </summary>
        private void CleanUpSource()
        {
            string strSql = "Delete ADR WHERE SUCHB IS NULL;";
            clsSQLconImport.ExecuteSQLWithTRANSACTION(strSql, "CleanUpADR", this.SysImport.GLUser.User_ID);           
        }

    }
}
