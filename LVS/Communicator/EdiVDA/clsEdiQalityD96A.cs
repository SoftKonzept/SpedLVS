using LVS.ASN.ASNFormatFunctions;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    public class clsEdiQalityD96A
    {
        //public const string const_Check_UNH_QualityD96A = "UNH+1+QALITY:D:96A:UN";
        public List<string> ListEdiQualityString;
        public List<clsLogbuchCon> ListErrorEdiQuality;
        public List<clsLogbuchCon> ListErrorEdi;
        internal clsLogbuchCon tmpLog;
        internal clsASN ASN;
        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        public clsSystem Sys;


        internal List<_EdiQalityD96AValue> ListEdiQalityD96AValueSqlSaveString;

        //internal string UNA6_SegmentEndzeichen = "'";

        public string Prozess { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;

        public clsEdiQalityD96A(Globals._GL_USER myGLUser, clsASN myASN, clsSystem mySystem, string myStringFileValue)
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
                //ListEdiDelforString = new List<string>();
                //ListErrorEdiDelfor = new List<clsLogbuchCon>();
                char cSplit = Constants.constValue_Edifact.const_Edifact_UNA6_SegmentEndzeichen[0];
                List<string> listEdiValue = myStringFileValue.Split(new char[] { cSplit }).ToList();

                ListEdiQalityD96AValueSqlSaveString = new List<_EdiQalityD96AValue>();

                if (listEdiValue.Count > 0)
                {
                    int iCounter = 0;
                    string strTmp = string.Empty;
                    int iTmp = 0;

                    bool bLEExist = false;

                    _EdiQalityD96A q = new _EdiQalityD96A();
                    q.IsActive = true;
                    q.WorkspaceId = (int)ASN.Job.ArbeitsbereichID;
                    q.Path = ASN.Job.PathDirectory;
                    q.FileName = ASN.Job.FileName;



                    foreach (string str in listEdiValue)
                    {
                        _EdiQalityD96AValue qValue = new _EdiQalityD96AValue();
                        qValue.EdiSegmentElement = str;

                        if (
                                (!bLEExist) &&
                                (!str.Equals(string.Empty))
                           )
                        {
                            string strSegment = str.Substring(0, 3);
                            switch (strSegment)
                            {
                                //Kopfdaten
                                //-- BGM+1::86+0030006822-229212505'
                                case "BGM":
                                    strTmp = string.Empty;
                                    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 5);
                                    if (strTmp.Equals(string.Empty))
                                    {
                                        SetError(strSegment);
                                    }
                                    else
                                    {
                                        q.iDocNo = strTmp;
                                        //q.ListQalityD96AValue.Add(qValue);
                                    }
                                    break;

                                //-- DTM+137:20221020:102'
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
                                                //del.DeliveryDate = Format_GlowDateFromEDI.Execute_yyyyMMddToDateTime(strTmp);

                                                //ListEdiDelforSqlSaveString.Add(del);
                                                ////del = new EdiDelforD97AValues();
                                                //del = GetDelforValue(del);
                                                break;
                                            case 50:
                                                //del.GoodReceiptDate = Format_GlowDateFromEDI.Execute_yyyyMMddToDateTime(strTmp);
                                                break;
                                            case 51:
                                                //del.CumQuantityStartDate = Format_GlowDateFromEDI.Execute_yyyyMMddToDateTime(strTmp);
                                                break;
                                            case 137:
                                                q.iDocDate = Format_GlowDateFromEDI.Execute_yyyyMMddToDateTime(strTmp);
                                                break;
                                        }
                                    }
                                    break;

                                    //case "NAD":
                                    //    EdiAdrWorkspaceAssignment tmpEdi = new EdiAdrWorkspaceAssignment();
                                    //    strTmp = string.Empty;
                                    //    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 2);
                                    //    EdiAdrWorkspaceAssignmentViewData vd = new EdiAdrWorkspaceAssignmentViewData();
                                    //    if (vd.ListEdiAdrWorkspaceAssignments.Count > 0)
                                    //    {
                                    //        tmpEdi = vd.ListEdiAdrWorkspaceAssignments.FirstOrDefault(x => x.WorkspaceId == (int)ASN.Job.ArbeitsbereichID && x.Property == "NAD+"+ strTmp);
                                    //    }
                                    //    switch (strTmp)
                                    //    {
                                    //        case "SF":
                                    //            if (tmpEdi.Id > 0)
                                    //            {
                                    //                del.Client = tmpEdi.AdrId;
                                    //            }
                                    //            else
                                    //            {
                                    //                del.Client = 0;
                                    //            }                                                
                                    //            break;
                                    //        case "ST":                                            
                                    //            if (tmpEdi.Id > 0)
                                    //            {
                                    //                del.Recipient = tmpEdi.AdrId;
                                    //            }
                                    //            else
                                    //            {
                                    //                del.Recipient = 0;
                                    //            }
                                    //            break;
                                    //        case "SU":                                            
                                    //            if (tmpEdi.Id > 0)
                                    //            {
                                    //                del.Supplier = tmpEdi.AdrId;
                                    //            }
                                    //            else
                                    //            {
                                    //                del.Supplier = 0;
                                    //            }
                                    //            break;
                                    //    }
                                    //    break;

                                    //case "LIN":
                                    //    strTmp = string.Empty;
                                    //    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 4);
                                    //    del.Werksnummer=strTmp;
                                    //    break;

                                    //case "RFF":
                                    //    strTmp = string.Empty;
                                    //    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 2);

                                    //    switch (strTmp)
                                    //    {
                                    //        case "ON":
                                    //            strTmp = string.Empty;
                                    //            del.OrderNo = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);                                            
                                    //            break;
                                    //        case "AAN":
                                    //            strTmp = string.Empty;
                                    //            strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                    //            iTmp = 0;
                                    //            int.TryParse(strTmp, out iTmp);
                                    //            del.DeliveryScheduleNumber = iTmp;
                                    //            break;
                                    //        case "SI":
                                    //            strTmp = string.Empty; 
                                    //            del.SID = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                    //            break;
                                    //    }        
                                    //    break;

                                    //case "QTY":
                                    //    strTmp = string.Empty;
                                    //    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 2);
                                    //    switch (strTmp)
                                    //    {
                                    //        case "1":
                                    //            strTmp = string.Empty;
                                    //            strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                    //            iTmp = 0;
                                    //            int.TryParse (strTmp, out iTmp);
                                    //            del.CallQuantity = iTmp;
                                    //            break;
                                    //        case "48":
                                    //            strTmp = string.Empty;
                                    //            strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                    //            iTmp = 0;
                                    //            int.TryParse(strTmp, out iTmp);
                                    //            del.ReceivedQuantity = iTmp;
                                    //            break;
                                    //        case "70":
                                    //            strTmp = string.Empty;
                                    //            strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                    //            iTmp = 0;
                                    //            int.TryParse(strTmp, out iTmp);
                                    //            del.CumQuantityReceived = iTmp;
                                    //            break;
                                    //    }
                                    //    break;

                                    //case "SCC":
                                    //    strTmp = string.Empty;
                                    //    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 2);
                                    //    iTmp = 0;
                                    //    int.TryParse(strTmp,out iTmp);
                                    //    del.SchedulingConditions = iTmp;
                                    //    break;

                                    //case "UNT":
                                    //case "UNZ":
                                    //    break;


                            }//Ende switch
                        }
                        else
                        {
                            break;
                        }
                    }//foreach

                    if (q.ListQalityD96AValue.Count > 0)
                    {
                        EdiQalityD96AViewData qVD = new EdiQalityD96AViewData(q);
                        qVD.Add();

                        if (qVD.ediQalityD96A.Id > 0)
                        {
                            string Sql = string.Empty;
                            foreach (_EdiQalityD96AValue item in qVD.ediQalityD96A.ListQalityD96AValue)
                            {
                                item.EdiQalityId = qVD.ediQalityD96A.Id;
                                EdiQalityD96AValueViewData valueVD = new EdiQalityD96AValueViewData(item);
                                Sql += valueVD.sql_Add;
                            }
                            bool bOk = clsSQLCOM.ExecuteSQLWithTRANSACTION(Sql, "QalityD96AValueAdd", this.GL_User.User_ID);
                        }
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
