using LVS.Constants;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    public class clsEdiDesadvD07A_Read
    {
        //internal clsASN ASN;
        //public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        //public clsSystem Sys;
        public AsnViewData asnVd = new AsnViewData();
        public string Prozess { get; set; } = string.Empty;
        public int BenutzerId { get; set; } = 0;
        public List<string> ListEdiVDASatzString;
        public List<clsLogbuchCon> ListErrorEdiVDA;
        internal clsLogbuchCon tmpLog;
        internal List<string> ListEdifactSqlSaveString;


        public clsEdiDesadvD07A_Read(Asn myAsn)
        {
            asnVd = new AsnViewData(myAsn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myASN"></param>
        /// <param name="mySys"></param>
        //public void InitClass(Globals._GL_USER myGLUser, clsASN myASN, clsSystem mySystem, string myStringFileValue)
        public void InitClass(Globals._GL_USER myGLUser, clsSystem mySystem)
        {
            clsLogbuchCon tmpLog = new clsLogbuchCon();
            //tmpLog.GL_User = this.GL_User;

            this.GL_User = myGLUser;
            //this.Sys = mySystem;
            //this.ASN = myASN;

            if (!asnVd.asnHead.EdiMessageValue.Equals(string.Empty))
            {
                //--- init Logdateien/Prozess
                ListErrorEdiVDA = new List<clsLogbuchCon>();
                tmpLog = new clsLogbuchCon();
                tmpLog.GL_User = myGLUser;
                tmpLog.Typ = enumLogArtItem.ERROR.ToString();

                char cSplit = constValue_Edifact.const_Edifact_UNA6_SegmentEndzeichen[0];
                List<string> listEdiValue = asnVd.asnHead.EdiMessageValue.Split(new char[] { cSplit }).ToList();
                listEdiValue.Remove(string.Empty); //-- leeren Eintrag löschen aus der Liste, kommt wenn edi Message einen Zeilenumbruch beinhaltet

                ListEdifactSqlSaveString = new List<string>();
                if ((listEdiValue.Count > 0) && (asnVd.asnHead.Id > 0))
                {
                    int iCounter = 0;
                    string strTmp = string.Empty;
                    //int iTmp = 0;

                    //bool bLEExist = false;

                    List<string> ListSqlEfactValuesToSave = new List<string>();
                    EdifactValueViewData evVD = new EdifactValueViewData();
                    foreach (string str in listEdiValue)
                    {
                        if (!str.Equals(string.Empty))
                        {
                            iCounter++;
                            evVD.edifactValue = new EdifactValue();
                            evVD.edifactValue.AsnId = (int)asnVd.asnHead.Id;
                            evVD.edifactValue.EdiSegmentElement = str;
                            evVD.edifactValue.EdiSegmentElementValue = string.Empty;
                            evVD.edifactValue.Property = strTmp;
                            evVD.edifactValue.OrderId = iCounter;

                            ListSqlEfactValuesToSave.Add(evVD.sql_Add);
                        }
                        else
                        {
                            string s = string.Empty;
                            //listEdiValue.Remove(s);
                        }
                    }
                    //EdifactMessageToClasses edi = new EdifactMessageToClasses(asnVd.asnHead);
                    if (listEdiValue.Count == ListSqlEfactValuesToSave.Count)
                    {
                        try
                        {
                            evVD.AddStringList(ListSqlEfactValuesToSave);
                        }
                        catch (Exception ex)
                        {
                            string str = ex.Message;
                            tmpLog.LogText = ex.Message;
                            tmpLog.Datum = DateTime.Now;
                            ListErrorEdiVDA.Add(tmpLog);
                        }
                    }
                    else
                    {
                        string str = "EdiValues konnten  nicht hinzugefügt werden!!!" + Environment.NewLine;
                        str += "ASN Id = " + asnVd.asnHead.Id + Environment.NewLine;
                        str += "ListEdiValue.COUNT = " + listEdiValue.Count.ToString() + Environment.NewLine;
                        tmpLog.LogText = str;
                        tmpLog.Datum = DateTime.Now;
                        ListErrorEdiVDA.Add(tmpLog);
                    }
                }
            }
        }


    }
}
