using LVS.ASN.ASNFormatFunctions;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    public class clsEdiDelforD97A_Read
    {
        public const string const_Check_BGM236_PS = "BGM+241:::PS"; //241 = Delivery Schedule PS = planned shipment
        public const string const_Check_NAD_Versender = "NAD+ST+";
        internal clsASN ASN;
        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        public clsSystem Sys;
        public List<string> ListEdiDelforString;
        public List<clsLogbuchCon> ListErrorEdiDelfor;
        internal List<EdiDelforD97AValues> ListEdiDelforSqlSaveString;
        public List<clsLogbuchCon> ListErrorEdi;
        internal string UNA6_SegmentEndzeichen = "'";
        internal clsLogbuchCon tmpLog;
        public string Prozess { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;

        public clsEdiDelforD97A_Read(Globals._GL_USER myGLUser, clsASN myASN, clsSystem mySystem, string myStringFileValue)
        {
            clsLogbuchCon tmpLog = new clsLogbuchCon();
            tmpLog.GL_User = this.GL_User;

            this.GL_User = myGLUser;
            this.Sys = mySystem;
            this.ASN = myASN.Copy();

            if (!myStringFileValue.Equals(string.Empty))
            {
                //--- init Logdateien/Prozess
                ListErrorEdi = new List<clsLogbuchCon>();
                tmpLog = new clsLogbuchCon();
                tmpLog.GL_User = myGLUser;
                tmpLog.Typ = enumLogArtItem.ERROR.ToString();

                //--- beinhaltet die EdiStrings die hinterher zeile für zeile die DFÜ -Datei ergeben
                ListEdiDelforString = new List<string>();
                ListErrorEdiDelfor = new List<clsLogbuchCon>();
                char cSplit = UNA6_SegmentEndzeichen[0];
                List<string> listEdiValue = myStringFileValue.Split(new char[] { cSplit }).ToList();

                ListEdiDelforSqlSaveString = new List<EdiDelforD97AValues>();

                if (listEdiValue.Count > 0)
                {
                    int iCounter = 0;
                    string strTmp = string.Empty;
                    int iTmp = 0;

                    bool bLEExist = false;
                    EdiDelforD97AValues del = new EdiDelforD97AValues();
                    del.WorkspaceId = (int)ASN.Job.ArbeitsbereichID;
                    del.IsActive = true;

                    foreach (string str in listEdiValue)
                    {
                        if (
                                (!bLEExist) &&
                                (!str.Equals(string.Empty))
                           )
                        {
                            string strSegment = str.Substring(0, 3);
                            switch (strSegment)
                            {
                                //Kopfdaten
                                case "BGM":
                                    strTmp = string.Empty;
                                    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 6);
                                    if (strTmp.Equals(string.Empty))
                                    {
                                        SetError(strSegment);
                                    }
                                    else
                                    {
                                        iTmp = 0;
                                        int.TryParse(strTmp, out iTmp);
                                        del.DocumentNo = iTmp;
                                    }
                                    break;
                                case "DTM":
                                    strTmp = string.Empty;
                                    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 2);
                                    if (strTmp.Equals(string.Empty))
                                    {
                                        SetError(strSegment);
                                    }
                                    else
                                    {
                                        iTmp = 0;
                                        int.TryParse(strTmp, out iTmp);
                                        strTmp = string.Empty;
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                        strTmp = strTmp.Substring(0, 8);
                                        switch (iTmp)
                                        {
                                            case 10:
                                                del.DeliveryDate = Format_GlowDateFromEDI.Execute_yyyyMMddToDateTime(strTmp);

                                                ListEdiDelforSqlSaveString.Add(del);
                                                //del = new EdiDelforD97AValues();
                                                del = GetDelforValue(del);
                                                break;
                                            case 50:
                                                del.GoodReceiptDate = Format_GlowDateFromEDI.Execute_yyyyMMddToDateTime(strTmp);
                                                break;
                                            case 51:
                                                del.CumQuantityStartDate = Format_GlowDateFromEDI.Execute_yyyyMMddToDateTime(strTmp);
                                                break;
                                            case 137:
                                                del.DocumentDate = Format_GlowDateFromEDI.Execute_yyyyMMddToDateTime(strTmp);
                                                break;
                                        }
                                    }
                                    break;

                                case "NAD":
                                    EdiClientWorkspaceValue tmpEdi = new EdiClientWorkspaceValue();
                                    strTmp = string.Empty;
                                    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 2);
                                    EdiClientWorkspaceValueViewData vd = new EdiClientWorkspaceValueViewData();
                                    if (vd.ListEdiAdrWorkspaceAssignments.Count > 0)
                                    {
                                        tmpEdi = vd.ListEdiAdrWorkspaceAssignments.FirstOrDefault(x => x.WorkspaceId == (int)ASN.Job.ArbeitsbereichID && x.Property == "NAD+" + strTmp);
                                    }
                                    switch (strTmp)
                                    {
                                        case "SF":
                                            if (tmpEdi.Id > 0)
                                            {
                                                del.Client = tmpEdi.AdrId;
                                            }
                                            else
                                            {
                                                del.Client = 0;
                                            }
                                            break;
                                        case "ST":
                                            if (tmpEdi.Id > 0)
                                            {
                                                del.Recipient = tmpEdi.AdrId;
                                            }
                                            else
                                            {
                                                del.Recipient = 0;
                                            }
                                            break;
                                        case "SU":
                                            if (tmpEdi.Id > 0)
                                            {
                                                del.Supplier = tmpEdi.AdrId;
                                            }
                                            else
                                            {
                                                del.Supplier = 0;
                                            }
                                            break;
                                    }
                                    break;

                                case "LIN":
                                    strTmp = string.Empty;
                                    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 4);
                                    del.Werksnummer = strTmp;
                                    break;

                                case "RFF":
                                    strTmp = string.Empty;
                                    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 2);

                                    switch (strTmp)
                                    {
                                        case "ON":
                                            strTmp = string.Empty;
                                            del.OrderNo = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                            break;
                                        case "AAN":
                                            strTmp = string.Empty;
                                            strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                            iTmp = 0;
                                            int.TryParse(strTmp, out iTmp);
                                            del.DeliveryScheduleNumber = iTmp;
                                            break;
                                        case "SI":
                                            strTmp = string.Empty;
                                            del.SID = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                            break;
                                    }
                                    break;

                                case "QTY":
                                    strTmp = string.Empty;
                                    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 2);
                                    switch (strTmp)
                                    {
                                        case "1":
                                            strTmp = string.Empty;
                                            strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                            iTmp = 0;
                                            int.TryParse(strTmp, out iTmp);
                                            del.CallQuantity = iTmp;
                                            break;
                                        case "48":
                                            strTmp = string.Empty;
                                            strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                            iTmp = 0;
                                            int.TryParse(strTmp, out iTmp);
                                            del.ReceivedQuantity = iTmp;
                                            break;
                                        case "70":
                                            strTmp = string.Empty;
                                            strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                            iTmp = 0;
                                            int.TryParse(strTmp, out iTmp);
                                            del.CumQuantityReceived = iTmp;
                                            break;
                                    }
                                    break;

                                case "SCC":
                                    strTmp = string.Empty;
                                    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 2);
                                    iTmp = 0;
                                    int.TryParse(strTmp, out iTmp);
                                    del.SchedulingConditions = iTmp;
                                    break;

                                case "UNT":
                                case "UNZ":
                                    break;

                            }//Ende switch
                        }
                        else
                        {
                            break;
                        }
                    }//foreach

                    // Anzahl der geplanten 
                    if (ListEdiDelforSqlSaveString.Count > 0)
                    {
                        List<string> ListSqlInsertDelfor = new List<string>();

                        int iPos = 0;
                        bool bDelforeDeaktivateOldValue = true;
                        foreach (EdiDelforD97AValues itm in ListEdiDelforSqlSaveString)
                        {
                            if (itm.CallQuantity > 0)
                            {
                                iPos++;
                                itm.Position = iPos;
                                EdiDelforViewData vd = new EdiDelforViewData(itm);
                                if (bDelforeDeaktivateOldValue)
                                {
                                    ListSqlInsertDelfor.Add(vd.sql_Update_DeactivateOldDelforCalls);
                                    bDelforeDeaktivateOldValue = false;
                                }
                                ListSqlInsertDelfor.Add(vd.sql_Add);
                            }
                        }

                        string strSql = string.Empty;
                        foreach (string s in ListSqlInsertDelfor)
                        {
                            strSql += s;
                        }

                        bool bReturn = false;
                        if (strSql.Length > 0)
                        {
                            try
                            {
                                bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "EDIFACTDelfor", 1);
                                this.Filename = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASN.ASNTyp.Typ, del.DocumentNo.ToString()) + this.ASN.Job.FileName;
                            }
                            catch (Exception ex)
                            {
                                string str = ex.Message;
                            }
                        }
                        strSql = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySegment"></param>
        private void SetError(string mySegment)
        {
            string strTmp = string.Empty;
            strTmp = Prozess + "Segement [" + mySegment + "] fehlerhaft!";
            tmpLog = new clsLogbuchCon();
            tmpLog.GL_User = this.GL_User;
            tmpLog.Typ = enumLogArtItem.ERROR.ToString();
            tmpLog.LogText = strTmp;
            tmpLog.TableName = string.Empty;
            decimal decTmp = 0;
            tmpLog.TableID = decTmp;
            this.ListErrorEdi.Add(tmpLog);
        }

        private EdiDelforD97AValues GetDelforValue(EdiDelforD97AValues myDel)
        {
            EdiDelforD97AValues edi = new EdiDelforD97AValues();
            edi.DocumentDate = myDel.DocumentDate;
            edi.DocumentNo = myDel.DocumentNo;
            edi.DeliveryScheduleNumber = myDel.DeliveryScheduleNumber;
            edi.Position = myDel.Position;
            edi.Client = myDel.Client;
            edi.Supplier = myDel.Supplier;
            edi.Recipient = myDel.Recipient;
            edi.Werksnummer = myDel.Werksnummer;
            edi.OrderNo = myDel.OrderNo;
            edi.CumQuantityReceived = myDel.CumQuantityReceived;
            edi.CumQuantityStartDate = myDel.CumQuantityStartDate;
            edi.ReceivedQuantity = myDel.ReceivedQuantity;
            edi.SID = myDel.SID;
            edi.GoodReceiptDate = myDel.GoodReceiptDate;
            edi.SchedulingConditions = myDel.SchedulingConditions;
            edi.CallQuantity = 0;
            edi.DeliveryDate = new DateTime(1900, 1, 1);
            edi.IsActive = myDel.IsActive;
            edi.WorkspaceId = myDel.WorkspaceId;

            return edi;
        }

    }
}
