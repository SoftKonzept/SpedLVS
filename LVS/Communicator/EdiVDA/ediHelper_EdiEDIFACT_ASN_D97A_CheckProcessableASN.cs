using LVS.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    public class ediHelper_EdiEDIFACT_ASN_D97A_CheckProcessableASN
    {
        //public const string const_UNH_S009 = "DESADV:D:97A:UN:";
        public static bool IsASNFileProcessable(clsJobs myJob, string myFilePathName)
        {
            bool myReturn = false;
            string strLine = string.Empty;
            string strTmp = string.Empty;
            string strVerweisTeil1 = string.Empty;
            string strVerweisTeil2 = string.Empty;
            string strVerweisTeil3 = string.Empty;
            string strVerweisGlobalSender = string.Empty;
            string strVerweisCheck = string.Empty;
            try
            {
                if (File.Exists(myFilePathName))
                {
                    using (StreamReader sr = new StreamReader(myFilePathName))
                    {
                        strLine = sr.ReadToEnd().Replace(Environment.NewLine, "");
                    }
                    char cSplit = constValue_Edifact.const_Edifact_UNA6_SegmentEndzeichen[0]; //  clsEdiVDA4984Read.const_Check_UNA6_SegmentEndzeichen[0];
                    List<string> tmpList = strLine.Split(new char[] { cSplit }).ToList();
                    if (
                         (tmpList != null) &&
                         (tmpList.Count > 0) &&
                         (myJob is clsJobs)
                       )
                    {
                        bool bIsDESADVD96A = false;
                        bool bVersenderOK = false;

                        ediHelper_EdiEDIFACT_ASN_Check AsnCheck = new ediHelper_EdiEDIFACT_ASN_Check(myJob, tmpList);
                        bIsDESADVD96A = AsnCheck.IsAsnArtKorrekt;
                        bVersenderOK = AsnCheck.ExistsAdrVerweis;
                        myReturn = (bIsDESADVD96A) && (bVersenderOK);

                        //string strTmpCheck = "UNH+";
                        //string strDESADV96ACheck = string.Empty;
                        //strDESADV96ACheck = tmpList.FirstOrDefault(x => x.Contains(strTmpCheck));

                        //bIsDESADVD96A = ediHelper_EdiEDIFACT_ASN_CheckAsnArt.CheckAsnArt(strDESADV96ACheck, myJob.ASNFileTyp);  

                        //if (strDESADV96ACheck != null)
                        //{
                        //    string strToCheck = string.Empty;
                        //    UNH uNH = new UNH(strDESADV96ACheck, myJob.ASNFileTyp);
                        //    if (uNH != null)
                        //    {
                        //        if (uNH.S009 != null)
                        //        {
                        //            strToCheck += uNH.S009.f_0065_MessageTypIdentifier + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                        //            strToCheck += uNH.S009.f_0052_MessageTypVersion + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                        //            strToCheck += uNH.S009.f_0054_MessageRelease + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                        //            strToCheck += uNH.S009.f_0051_ControllingAgency + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                        //        }
                        //    }
                        //    bIsDESADVD96A = (strToCheck.Contains(ediHelper_EdiEDIFACT_ASN_D96A_CheckProcessableASN.const_UNH_S009));
                        //}

                        //strVerweisTeil1 = tmpList.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CZ));
                        //NAD nadTeil1 = new NAD(strVerweisTeil1, myJob.ASNFileTyp);
                        //strVerweisTeil2 = tmpList.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CN));
                        //NAD nadTeil2 = new NAD(strVerweisTeil2, myJob.ASNFileTyp);
                        //strVerweisTeil3 = tmpList.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_FW));
                        //NAD nadTeil3 = new NAD(strVerweisTeil3, myJob.ASNFileTyp);

                        //strVerweisGlobalSender = nadTeil1.C082.f_3039_PartyId + "#0#0";
                        //strVerweisCheck = nadTeil1.C082.f_3039_PartyId + "#" + nadTeil2.C082.f_3039_PartyId + "#" + nadTeil3.C082.f_3039_PartyId;
                        ////strVerweisCheck = nadTeil1.C082.f_3039_PartyId + "#" + nadTeil2.C082.f_3039_PartyId.TrimStart('0') + "#" + nadTeil3.C082.f_3039_PartyId.TrimStart('0');

                        ////Dictionary<string, clsADRVerweis> dicSenderVerweis = clsADRVerweis.FillDictAdrVerweis(myJob.MandantenID, myJob.ArbeitsbereichID, 1, myJob.ASNFileTyp);

                        //Dictionary<string, clsADRVerweis> dicSenderVerweis = clsADRVerweis.FillDictAdrVerweis(0, 0, 1, myJob.ASNFileTyp);
                        ////--- zuerst Check zur Verbindung Sender / Emfpänger
                        //bVersenderOK = dicSenderVerweis.ContainsKey(strVerweisCheck);
                        //if (!bVersenderOK)
                        //{
                        //    //-- dann CHeck Sender global
                        //    bVersenderOK = dicSenderVerweis.ContainsKey(strVerweisGlobalSender);
                        //}

                        myReturn = (bIsDESADVD96A) && (bVersenderOK);

                        //-- Errormail keine Adr konnte zugewiesen werden
                        if (!myReturn)
                        {
                            clsError Error = new clsError();
                            Error.Aktion = "Task_ReadASN - ediHelper_EdiEDIFACT_ASN_D97A_GetAdrVerweis ";
                            Error.Datum = DateTime.Now;
                            Error.ErrorText = "ASN -  es kann keine Adresse zugeordnet werden !!!"; // string.Empty;
                            Error.exceptText = string.Empty;
                            Error.WriteError();

                            clsMail ErrorMail = new clsMail();
                            ErrorMail.InitClass(new Globals._GL_USER(), null);
                            ErrorMail.Subject = "Error - Task_ReadASN - ediHelper_EdiEDIFACT_ASN_D96A_GetAdrVerweis";
                            string strMes = "EDIFACT - Meldung:" + Environment.NewLine;
                            strMes += "Adresse Lieferant/Auftrageber: " + strVerweisTeil1 + Environment.NewLine;
                            strMes += "Adresse Empfänger            : " + strVerweisTeil2 + Environment.NewLine;
                            strMes += "Adresse Lager                : " + strVerweisTeil3 + Environment.NewLine;
                            strMes += "Verweis Global               : " + strVerweisGlobalSender + Environment.NewLine;
                            strMes += "Verweis                      : " + strVerweisCheck + Environment.NewLine;

                            strMes += Environment.NewLine;
                            if (AsnCheck.DicSenderVerweis.Count > 0)
                            {
                                strMes += "DictVerweise: " + Environment.NewLine;
                                int iCount = 1;

                                foreach (var item in AsnCheck.DicSenderVerweis)
                                {
                                    strMes += iCount.ToString("000") + ":  " + item.Key + " | " + item.Value.ID + " <> " + item.Value.Verweis + Environment.NewLine;
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
            }
            catch (Exception ex)
            {
                //Fehlermeldung
                string strEx = ex.ToString();
            }
            return myReturn;
        }
    }
}
