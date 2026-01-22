using LVS.Constants;
using System;
using System.IO;

namespace LVS.IniValuePrinter
{
    public class InitValuePrinter_Druckereinstellungen
    {
        public const string const_Section = "Druckereinstellungen";

        public string PrinterName { get; set; } = string.Empty;
        public string PaperSource { get; set; } = string.Empty;
        public int PrintCount { get; set; } = 1;
        public InitValuePrinter_Druckereinstellungen(string myDocumentArt, Globals._GL_SYSTEM myGLSystem, int myUserId)
        {
            constValue_PrinterIni pIni = new constValue_PrinterIni(myUserId);
            PrinterName = string.Empty;
            PaperSource = string.Empty;
            PrintCount = 1;
            if (File.Exists(pIni.IniFilePaht))
            {
                clsINI.clsINI iniPrinterSetting = new clsINI.clsINI(pIni.IniFilePaht);


                if (iniPrinterSetting.SectionNames().Contains(const_Section))
                {
                    string keyPrinter = myDocumentArt.ToString() + "_Drucker";
                    if (iniPrinterSetting.ReadString(const_Section, keyPrinter) != null)
                    {
                        PrinterName = iniPrinterSetting.ReadString(const_Section, keyPrinter);
                    }

                    string keyTray = myDocumentArt.ToString() + "_Fach";
                    if (iniPrinterSetting.ReadString(const_Section, keyTray) != null)
                    {
                        PaperSource = iniPrinterSetting.ReadString(const_Section, keyTray);
                    }
                }


                try
                {
                    clsINI.clsINI iniSped4 = new clsINI.clsINI();
                    iniSped4 = GlobalINI.GetINI();

                    string sectionMandant = myGLSystem.client_MatchCode + "MANDANT_" + myGLSystem.sys_MandantenID.ToString();

                    var sectionTmp = iniSped4.SectionNames();
                    if (iniSped4.SectionNames().Contains(sectionMandant))
                    {
                        string keyPrintCount = myDocumentArt.ToString() + "PrintCount";
                        if (iniSped4.ReadString(sectionMandant, keyPrintCount) != null)
                        {
                            int iTmp = 1;
                            string tmpRead = iniSped4.ReadString(sectionMandant, keyPrintCount);
                            if (!tmpRead.Equals(string.Empty))
                            {
                                int.TryParse(tmpRead, out iTmp);
                                PrintCount = iTmp;
                            }
                            else
                            {
                                PrintCount = 1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }
            }
        }
    }
}
