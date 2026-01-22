using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Import
{
    public class clsLvsImport
    {

        public delegate void SetProzessInforamtion(List<string> myInfoList);
        public event EventHandler<List<string>> SetProzessInforamtionHandler;

        internal clsSystemImport SysImport;
        public List<string> ListLog;
        public List<string> ListError;
        public clsLvsImport(clsSystemImport mySys)
        {
            SysImport = mySys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            string strTmp = string.Empty;
            bool bReturn = false;
            ListLog = new List<string>();
            ListError = new List<string>();

            //Check DB Verbindung
            if (SysImport.CheckConnectionLVSOld)
            {
                if ((SysImport.CheckConnectionSped) && (SysImport.CheckConnectionCom))
                {
                    //ADR
                    impADR impAdr = new impADR(this.SysImport);
                    impAdr.SetProzessInfoEventHandler += Imp_SetProzessInfoEventHandler;
                    impAdr.DoImport();
                    this.ListLog.AddRange(impAdr.ListLog);
                    strTmp = string.Empty;
                    strTmp = "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++";
                    ListLog.Add(strTmp);
                    SetProzessInforamtionHandler(this, ListLog);

                    //Güterarten
                    //if (!SysImport.Import_GutOnlyIsUsed)
                    //{
                    //    impGueterarten impGut = new impGueterarten(this.SysImport);
                    //    impGut.SetProzessInfoEventHandler += Imp_SetProzessInfoEventHandler;
                    //    impGut.DoImport();
                    //    if (impGut.ListLog != null)
                    //    {
                    //        SetProzessInforamtionHandler(this, impGut.ListLog);
                    //        this.ListLog.AddRange(impGut.ListLog);
                    //        strTmp = string.Empty;
                    //        strTmp = "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++";
                    //        ListLog.Add(strTmp);
                    //        SetProzessInforamtionHandler(this, ListLog);
                    //    }
                    //}

                    //////Artikel und Eingänge
                    impArtikel impArt = new impArtikel(this.SysImport);
                    impArt.SetProzessInfoEventHandler += Imp_SetProzessInfoEventHandler;
                    impArt.DoImportUB();
                    this.ListLog.AddRange(impArt.ListLog);
                    strTmp = string.Empty;
                    strTmp = "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++";
                    ListLog.Add(strTmp);
                    SetProzessInforamtionHandler(this, ListLog);

                    impArt.DoImport();
                    this.ListLog.AddRange(impArt.ListLog);
                    strTmp = string.Empty;
                    strTmp = "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++";
                    ListLog.Add(strTmp);
                    SetProzessInforamtionHandler(this, ListLog);


                }
                else
                {
                    strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString("DB: Sped4 ist nicht erreichbar!!!");
                    ListLog.Add(strTmp);
                }
            }
            else
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("DB: LvsOld ist nicht erreichbar!!!");
                ListLog.Add(strTmp);
            }
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Imp_SetProzessInfoEventHandler(object sender, EventArgs e)
        {
            this.SetProzessInforamtionHandler(this, this.ListLog);
        }
    }
}
