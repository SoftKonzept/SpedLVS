using LVS.ASN.Defaults;
using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    /// <summary>
    ///             änhliche Funktion ediHelper_EdiEDIFACT_ASN_D96A_CheckProcessableASN
    /// </summary>
    public class ediHelper_EdiEDIFACT_ASN_D96A_GetAdrVerweis
    {
        public const string const_UNH_S009 = "DESADV:D:96A:UN:";
        public static clsADRVerweis GetAdrVerweis_Sender(clsJobs myJob, string myStrLine)
        {
            clsADRVerweis VersenderVerweis = new clsADRVerweis();
            string strLine = myStrLine;
            string strTmp = string.Empty;

            string strVerweisTeil1 = string.Empty;
            string strVerweisTeil2 = string.Empty;
            string strVerweisTeil3 = string.Empty;
            string strVerweisGlobalSender = string.Empty;
            string strVerweisCheck = string.Empty;
            Dictionary<string, clsADRVerweis> dicSenderVerweis = new Dictionary<string, clsADRVerweis>();
            try
            {
                char cSplit = constValue_Edifact.const_Edifact_UNA6_SegmentEndzeichen[0]; //  clsEdiVDA4984Read.const_Check_UNA6_SegmentEndzeichen[0];
                List<string> tmpList = strLine.Split(new char[] { cSplit }).ToList();

                if (
                        (tmpList != null) &&
                        (tmpList.Count > 0)
                    //&& (myJob is clsJobs)
                    )
                {
                    bool bIsDESADVD96A = false;
                    bool bVersenderOK = false;

                    string strTmpCheck = "UNH+";
                    string strDESADV96ACheck = string.Empty;
                    strDESADV96ACheck = tmpList.FirstOrDefault(x => x.Contains(strTmpCheck));
                    if (strDESADV96ACheck != null)
                    {
                        string strToCheck = string.Empty;
                        UNH uNH = new UNH(strDESADV96ACheck, myJob.ASNFileTyp);
                        if (uNH != null)
                        {
                            if (uNH.S009 != null)
                            {
                                strToCheck += uNH.S009.f_0065_MessageTypIdentifier + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                                strToCheck += uNH.S009.f_0052_MessageTypVersion + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                                strToCheck += uNH.S009.f_0054_MessageRelease + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                                strToCheck += uNH.S009.f_0051_ControllingAgency + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                            }
                        }
                        bIsDESADVD96A = (strToCheck.Contains(Default_DESADV_D96A.const_UNH_S009));
                    }

                    if (bIsDESADVD96A)
                    {

                        //strVerweisTeil1 = tmpList.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CZ));
                        //NAD nadTeil1 = new NAD(strVerweisTeil1, myJob.ASNFileTyp);
                        //strVerweisTeil2 = tmpList.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CN));
                        //NAD nadTeil2 = new NAD(strVerweisTeil2, myJob.ASNFileTyp);
                        //strVerweisTeil3 = tmpList.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_FW));
                        //NAD nadTeil3 = new NAD(strVerweisTeil3, myJob.ASNFileTyp);

                        strVerweisTeil1 = tmpList.FirstOrDefault(x => x != null && x.TrimStart().StartsWith(constValue_Edifact.const_Edifact_NAD_CZ, StringComparison.OrdinalIgnoreCase));
                        NAD nadTeil1 = strVerweisTeil1 != null ? new NAD(strVerweisTeil1, myJob.ASNFileTyp) : null;

                        strVerweisTeil2 = tmpList.FirstOrDefault(x => x != null && x.TrimStart().StartsWith(constValue_Edifact.const_Edifact_NAD_CN, StringComparison.OrdinalIgnoreCase));
                        NAD nadTeil2 = strVerweisTeil2 != null ? new NAD(strVerweisTeil2, myJob.ASNFileTyp) : null;

                        strVerweisTeil3 = tmpList.FirstOrDefault(x => x != null && x.TrimStart().StartsWith(constValue_Edifact.const_Edifact_NAD_FW, StringComparison.OrdinalIgnoreCase));
                        NAD nadTeil3 = strVerweisTeil3 != null ? new NAD(strVerweisTeil3, myJob.ASNFileTyp) : null;

                        strVerweisGlobalSender = nadTeil1.C082.f_3039_PartyId + "#0#0";
                        strVerweisCheck = nadTeil1.C082.f_3039_PartyId + "#" + nadTeil2.C082.f_3039_PartyId + "#" + nadTeil3.C082.f_3039_PartyId;

                        dicSenderVerweis = clsADRVerweis.FillDictAdrVerweis(0, 0, 1, myJob.ASNFileTyp);

                        //--- direkte Zuweisung Sender / Empfänger
                        bVersenderOK = dicSenderVerweis.ContainsKey(strVerweisCheck);
                        if (bVersenderOK)
                        {
                            //-- direkte zuweisung liegt vor
                            dicSenderVerweis.TryGetValue(strVerweisCheck, out VersenderVerweis);
                        }
                        else
                        {
                            //-- dann CHeck Sender global
                            bVersenderOK = dicSenderVerweis.ContainsKey(strVerweisGlobalSender);
                            if (bVersenderOK)
                            {
                                dicSenderVerweis.TryGetValue(strVerweisGlobalSender, out VersenderVerweis);
                            }
                        }
                    }


                    //-- Errormail keine Adr konnte zugewiesen werden
                    if (VersenderVerweis.ID == 0)
                    {
                        clsError Error = new clsError();
                        Error.Aktion = "Task_ReadASN - ediHelper_EdiEDIFACT_ASN_D96A_GetAdrVerweis.GetAdrVerweis_Sender ";
                        Error.Datum = DateTime.Now;
                        Error.ErrorText = "ASN -  es kann keine Adresse zugeordnet werden !!!"; // string.Empty;
                        Error.exceptText = string.Empty;
                        Error.WriteError();

                        clsMail ErrorMail = new clsMail();
                        ErrorMail.InitClass(new Globals._GL_USER(), null);
                        ErrorMail.Subject = "Error - Task_ReadASN - ediHelper_EdiEDIFACT_ASN_D96A_GetAdrVerweis.GetAdrVerweis_Sender";
                        string strMes = "EDIFACT - Meldung:" + Environment.NewLine;
                        strMes += "Adresse Lieferant/Auftrageber: " + strVerweisTeil1 + Environment.NewLine;
                        strMes += "Adresse Empfänger            : " + strVerweisTeil2 + Environment.NewLine;
                        strMes += "Adresse Lager                : " + strVerweisTeil3 + Environment.NewLine;
                        strMes += "Verweis Global               : " + strVerweisGlobalSender + Environment.NewLine;
                        strMes += "Verweis                      : " + strVerweisCheck + Environment.NewLine;

                        strMes += Environment.NewLine;
                        if (dicSenderVerweis.Count > 0)
                        {
                            strMes += "DictVerweise: " + Environment.NewLine;
                            int iCount = 1;

                            foreach (var item in dicSenderVerweis)
                            {
                                strMes += iCount.ToString("00") + ":  " + item.Key + " | " + item.Value.ID + "-" + item.Value.Verweis + Environment.NewLine;
                                iCount++;
                            }
                        }

                        strMes += Environment.NewLine;
                        if (tmpList.Count > 0)
                        {
                            strMes += "Edi - Message: " + Environment.NewLine;
                            int iCount = 1;
                            foreach (string s in tmpList)
                            {
                                strMes += iCount.ToString("000") + ":  " + s + Environment.NewLine;
                                iCount++;
                            }
                        }
                        ErrorMail.Message = strMes;
                        ErrorMail.SendError();

                    }
                }
            }
            catch (Exception ex)
            {
                //Fehlermeldung
                string strEx = ex.ToString();
            }
            return VersenderVerweis;
        }
    }
}
