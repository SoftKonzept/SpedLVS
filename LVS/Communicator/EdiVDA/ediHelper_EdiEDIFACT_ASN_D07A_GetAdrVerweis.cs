using Common.Models;
using LVS.Constants;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    public class ediHelper_EdiEDIFACT_ASN_D07A_GetAdrVerweis
    {
        public const string const_UNH_S009 = "DESADV:D:07A:UN:";  // UNH+00059870+DESADV:D:07A:UN:GAVF11

        /// <summary>
        ///             Verwendete Segmente
        ///             
        ///             1. NNAD+SF
        ///             2. NNAD+ST
        ///             3. Loc+11   // 11 = Place of dispatch
        /// </summary>
        /// <param name="myJob"></param>
        /// <param name="myFilePathName"></param>
        /// <returns></returns>
        public static AddressReferences GetAdrReference(clsJobs myJob, string myDesadvFile)
        {
            AddressReferences returnAdrRef = new AddressReferences();
            string strVerweisTeil1 = string.Empty;
            string strVerweisTeil2 = string.Empty;
            string strVerweisTeil3 = string.Empty;
            string strVerweisGlobalSender = string.Empty;
            string strVerweisCheck = string.Empty;
            try
            {
                if (!myDesadvFile.Equals(string.Empty))
                {
                    char cSplit = constValue_Edifact.const_Edifact_UNA6_SegmentEndzeichen[0]; //  clsEdiVDA4984Read.const_Check_UNA6_SegmentEndzeichen[0];
                    List<string> tmpList = myDesadvFile.Split(new char[] { cSplit }).ToList();

                    if (
                         (tmpList != null) &&
                         (tmpList.Count > 0) &&
                         (myJob is clsJobs)
                       )
                    {
                        strVerweisTeil1 = tmpList.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_SF));
                        NAD nadTeil1 = new NAD(strVerweisTeil1, myJob.ASNFileTyp);

                        strVerweisTeil2 = tmpList.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_LOC_11));
                        LOC locTeil2 = new LOC(strVerweisTeil2, myJob.ASNFileTyp);

                        strVerweisTeil3 = tmpList.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_ST));
                        NAD nadTeil3 = new NAD(strVerweisTeil3, myJob.ASNFileTyp);


                        //strVerweisTeil3 = tmpList.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_LOC_11));
                        //NAD nadTeil3 = new NAD(strVerweisTeil3, myJob.ASNFileTyp);
                        //LOC locTeil3 = new LOC(strVerweisTeil3, myJob.ASNFileTyp);

                        if ((nadTeil1.C082.f_3039_PartyId != null))
                        {
                            strVerweisGlobalSender = nadTeil1.C082.f_3039_PartyId + "#0#0";
                            strVerweisCheck += nadTeil1.C082.f_3039_PartyId;
                        }
                        if ((locTeil2.C517.f_3225_LocationIdentifier != null))
                        {
                            strVerweisCheck += "#" + locTeil2.C517.f_3225_LocationIdentifier;
                        }

                        if ((nadTeil3.C082.f_3039_PartyId != null))
                        {
                            strVerweisCheck += "#" + nadTeil3.C082.f_3039_PartyId;
                        }

                        AddressReferenceViewData adrVerweis = new AddressReferenceViewData();
                        Dictionary<string, AddressReferences> dicSenderVerweis = AddressReferenceViewData.FillDictAdrVerweisBySender((int)myJob.AdrVerweisID, 1);
                        returnAdrRef = new AddressReferences();

                        if (dicSenderVerweis.TryGetValue(strVerweisCheck, out returnAdrRef))
                        {
                            //-- Daten sind nun in returnAdrRef gespeichert
                            // Der Schlüssel wurde gefunden, und der Wert ist in 'addressReference' gespeichert
                            //Console.WriteLine($"Gefundene Adresse: {addressReference}");
                        }
                        else
                        {
                            // Der Schlüssel wurde nicht gefunden
                            //Console.WriteLine("Schlüssel nicht im Dictionary vorhanden.");
                            clsError Error = new clsError();
                            Error.Aktion = "Task_ReadASN - ediHelper_EdiEDIFACT_ASN_D07A_GetAdrVerweis ";
                            Error.Datum = DateTime.Now;
                            Error.ErrorText = "ASN -  es kann keine Adresse zugeordnet werden !!!"; // string.Empty;
                            Error.exceptText = string.Empty;
                            Error.WriteError();

                            clsMail ErrorMail = new clsMail();
                            ErrorMail.InitClass(new Globals._GL_USER(), null);
                            ErrorMail.Subject = "Error - Task_ReadASN - ediHelper_EdiEDIFACT_ASN_D07A_GetAdrVerweis";
                            string strMes = "EDIFACT - Meldung:" + Environment.NewLine;
                            strMes += "Adresse Lieferant/Auftrageber: " + strVerweisTeil1 + Environment.NewLine;
                            strMes += "Adresse Empfänger            : " + strVerweisTeil2 + Environment.NewLine;
                            strMes += "Adresse Lager                : " + strVerweisTeil3 + Environment.NewLine;
                            strMes += "Verweis Global               : " + strVerweisGlobalSender + Environment.NewLine;
                            strMes += "Verweis                      : " + strVerweisCheck + Environment.NewLine;

                            strMes += Environment.NewLine;
                            strMes += Environment.NewLine;
                            if (tmpList.Count > 0)
                            {
                                strMes += "Edi - Message: " + Environment.NewLine;
                                int iCount = 1;
                                foreach (string s in tmpList)
                                {
                                    if (!s.Equals(string.Empty))
                                    {
                                        strMes += iCount.ToString("000") + ":  " + s + Environment.NewLine;
                                        iCount++;
                                    }
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
            return returnAdrRef;
        }
    }
}
