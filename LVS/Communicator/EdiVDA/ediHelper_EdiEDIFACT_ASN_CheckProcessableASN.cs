using LVS.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    public class ediHelper_EdiEDIFACT_ASN_CheckProcessableASN
    {
        public static bool IsASNFileProcessable(clsJobs myJob, string myFilePathName)
        {
            bool myReturn = false;
            string strLine = string.Empty;
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
                        bool bIsDESADV_D07A = false;
                        bool bVersenderOK = false;

                        ediHelper_EdiEDIFACT_ASN_Check AsnCheck = new ediHelper_EdiEDIFACT_ASN_Check(myJob, tmpList);
                        bIsDESADV_D07A = AsnCheck.IsAsnArtKorrekt;
                        bVersenderOK = AsnCheck.ExistsAdrVerweis;
                        myReturn = (bIsDESADV_D07A) && (bVersenderOK);


                        //-- Errormail keine Adr konnte zugewiesen werden
                        if (!myReturn)
                        {
                            clsError Error = new clsError();
                            Error.Aktion = "Task_ReadASN - ediHelper_EdiEDIFACT_ASN_07A_GetAdrVerweis ";
                            Error.Datum = DateTime.Now;
                            Error.ErrorText = "ASN -  es kann keine Adresse zugeordnet werden !!!"; // string.Empty;
                            Error.exceptText = string.Empty;
                            Error.WriteError();

                            clsMail ErrorMail = new clsMail();
                            ErrorMail.InitClass(new Globals._GL_USER(), null);
                            ErrorMail.Subject = "Error - Task_ReadASN - ediHelper_EdiEDIFACT_ASN_06A_GetAdrVerweis";
                            string strMes = "EDIFACT - Meldung:" + Environment.NewLine;
                            //strMes += "Adresse Lieferant/Auftrageber: " + strVerweisTeil1 + Environment.NewLine;
                            //strMes += "Adresse Empfänger            : " + strVerweisTeil2 + Environment.NewLine;
                            //strMes += "Adresse Lager                : " + strVerweisTeil3 + Environment.NewLine;
                            foreach (var x in AsnCheck.DicSenderVerweis)
                            {
                                //-- Adr Lieferant/Auftraggeber
                                if (x.Key.StartsWith(constValue_Edifact.const_Edifact_NAD_CZ))
                                {
                                    strMes += "Adresse Lieferant/Auftrageber: " + x.Key + Environment.NewLine;
                                }
                                else if (x.Key.StartsWith(constValue_Edifact.const_Edifact_NAD_CN))
                                {
                                    strMes += "Adresse Empfänger            : " + x.Key + Environment.NewLine;
                                }
                                else if (x.Key.StartsWith(constValue_Edifact.const_Edifact_NAD_FW))
                                {
                                    strMes += "Adresse Lager                : " + x.Key + Environment.NewLine;
                                }
                            }
                            strMes += "Verweis Global               : " + AsnCheck.AdrVerweisGlobal + Environment.NewLine;
                            strMes += "Verweis                      : " + AsnCheck.AdrVerweis + Environment.NewLine;

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
