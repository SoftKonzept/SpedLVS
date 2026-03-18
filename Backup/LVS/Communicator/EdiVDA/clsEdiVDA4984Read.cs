using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class clsEdiVDA4984Read
    {
        internal clsASN ASN;
        //internal clsEdiSegment Segment;

        public const string const_Check_BGM236_LFE = "BGM+241:::LAB-ED";
        public const string const_Check_NAD_Versender = "NAD+ST+";
        public const string const_Check_UNA6_SegmentEndzeichen = "'";
        public List<clsVDAClientValue> listVDAClientValueSatz { get; set; }


        /// <summary>
        ///                 hier noch Austausch durch LVS.Constants constValue_Edifact
        /// </summary>

        internal string UNA1_GruppendatenelementTrennzeichen = ":";
        internal string UNA2_SegmentDatenelementTrennzeichen = "+";
        internal string UNA3_Dezimalzeichen = ".";
        internal string UNA4_Freigabezeichen = "?";
        internal string UNA5_Reserviert = " ";
        internal string UNA6_SegmentEndzeichen = "'";


        private string prozess;

        public string Prozess
        {
            get { return prozess; }
            set { prozess = value; }
        }

        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        public clsSystem Sys;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public List<string> ListEdiVDASatzString;
        public List<clsLogbuchCon> ListErrorEdiVDA;
        internal clsLogbuchCon tmpLog;
        internal List<string> ListVDA4984SqlSaveString;

        public int DocumentenNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public string SupplierNumber { get; set; }
        public List<Tuple<int, DateTime>> ListCall;

        /*****************************************************************
         *          Methoden  /  Prozedures
         * **************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myASN"></param>
        /// <param name="mySys"></param>
        public void InitClass(Globals._GL_USER myGLUser, clsASN myASN, clsSystem mySystem, string myStringFileValue)
        {
            clsLogbuchCon tmpLog = new clsLogbuchCon();
            tmpLog.GL_User = this.GL_User;

            this.GL_User = myGLUser;
            this.Sys = mySystem;
            this.ASN = myASN;

            if (!myStringFileValue.Equals(string.Empty))
            {

                //--- init Logdateien/Prozess
                ListErrorEdiVDA = new List<clsLogbuchCon>();
                tmpLog = new clsLogbuchCon();
                tmpLog.GL_User = myGLUser;
                tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                //tmpLog.TableName = "Queue";
                //tmpLog.TableID = myQueue.ID;

                //--- beinhaltet die EdiStrings die hinterher zeile für zeile die DFÜ -Datei ergeben
                ListEdiVDASatzString = new List<string>();
                ListErrorEdiVDA = new List<clsLogbuchCon>();
                char cSplit = UNA6_SegmentEndzeichen[0];
                List<string> listEdiValue = myStringFileValue.Split(new char[] { cSplit }).ToList();

                ListVDA4984SqlSaveString = new List<string>();

                //Kopfdaten
                DocumentenNumber = 0;
                DocumentDate = DateTime.MinValue;
                SupplierNumber = string.Empty;
                ListCall = new List<Tuple<int, DateTime>>();

                if (listEdiValue.Count > 0)
                {
                    int iCounter = 0;
                    string strTmp = string.Empty;
                    int iTmp = 0;

                    clsEdiVDA4984Value tmp4984 = new clsEdiVDA4984Value();
                    tmp4984.InitClass(ref this.GL_User, (int)this.ASN.Job.MandantenID, (int)this.ASN.Job.ArbeitsbereichID);


                    bool bLEExist = false;

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
                                        this.DocumentenNumber = iTmp;
                                        tmp4984.DocNo = DocumentenNumber;
                                    }
                                    break;
                                case "DTM":
                                    strTmp = string.Empty;
                                    if (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_DTM.Dokumentendatum_137).ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentDTMFieldValue(str, enumEdi4984SegmQualifier_DTM.Dokumentendatum_137);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        else
                                        {
                                            DateTime dtTmp = DateTime.MinValue;
                                            DateTime.TryParse(strTmp, out dtTmp);
                                            this.DocumentDate = dtTmp;
                                            tmp4984.DocDate = DocumentDate;

                                            if (clsEdiVDA4984Read.ExistLE(this.DocumentenNumber, this.DocumentDate))
                                            {
                                                strTmp = string.Empty;
                                                strTmp = "LE [" + this.DocumentenNumber.ToString() + "/" + this.DocumentDate.ToString("dd.MM.yyyy") + "] ist bereits vorhanden!!!";
                                                tmpLog = new clsLogbuchCon();
                                                tmpLog.GL_User = this.GL_User;
                                                tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                                tmpLog.LogText = strTmp;
                                                tmpLog.TableName = string.Empty;
                                                decimal decTmp = 0;
                                                tmpLog.TableID = decTmp;
                                                this.ListErrorEdiVDA.Add(tmpLog);

                                                bLEExist = true;
                                                break;
                                            }
                                        }

                                    }
                                    else if (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_DTM.Referenzdatum_171).ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentDTMFieldValue(str, enumEdi4984SegmQualifier_DTM.Referenzdatum_171);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        else
                                        {
                                            DateTime dtTmp = DateTime.MinValue;
                                            DateTime.TryParse(strTmp, out dtTmp);
                                            tmp4984.CallDate = dtTmp;
                                        }
                                    }
                                    else if (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_DTM.DatumResetNull_52).ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentDTMFieldValue(str, enumEdi4984SegmQualifier_DTM.DatumResetNull_52);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        else
                                        {
                                            DateTime dtTmp = DateTime.MinValue;
                                            DateTime.TryParse(strTmp, out dtTmp);
                                            tmp4984.ResetFSZDate = dtTmp;
                                        }
                                    }
                                    else if (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_DTM.DatumEFZ_51).ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentDTMFieldValue(str, enumEdi4984SegmQualifier_DTM.DatumEFZ_51);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        else
                                        {
                                            DateTime dtTmp = DateTime.MinValue;
                                            DateTime.TryParse(strTmp, out dtTmp);
                                            tmp4984.EfzDate = dtTmp;
                                        }
                                    }
                                    else if (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_DTM.gewünschterLiefertermin_2).ToString()))
                                    {
                                        if (str.Length > 6)
                                        {
                                            strTmp = ediHelper_SegmentSplitt.GetSegmentDTMFieldValue(str, enumEdi4984SegmQualifier_DTM.gewünschterLiefertermin_2);
                                            if (strTmp.Equals(string.Empty))
                                            {
                                                SetError(strSegment);
                                            }
                                            else
                                            {
                                                DateTime dtTmp = DateTime.MinValue;
                                                DateTime.TryParse(strTmp, out dtTmp);
                                                tmp4984.DeliveryDate = dtTmp;
                                                ListVDA4984SqlSaveString.Add(tmp4984.AddString());
                                            }
                                        }
                                    }
                                    else if (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_DTM.spaetestesLieferadatum_63).ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentDTMFieldValue(str, enumEdi4984SegmQualifier_DTM.spaetestesLieferadatum_63);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        else
                                        {
                                            DateTime dtTmp = DateTime.MinValue;
                                            DateTime.TryParse(strTmp, out dtTmp);
                                            tmp4984.DeliveryDate = dtTmp;
                                            ListVDA4984SqlSaveString.Add(tmp4984.AddString());
                                        }
                                    }
                                    break;

                                case "NAD":
                                    strTmp = string.Empty;
                                    if (str.Contains(strSegment + "+" + enumEdi4984SegmQualifier_NAD.SE.ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        else
                                        {
                                            this.SupplierNumber = strTmp;
                                            tmp4984.SupplierId = SupplierNumber;
                                        }
                                    }
                                    break;

                                case "LIN":
                                    //neue Daten
                                    tmp4984 = new clsEdiVDA4984Value();
                                    tmp4984.InitClass(ref this.GL_User, (int)this.ASN.Job.MandantenID, (int)this.ASN.Job.ArbeitsbereichID);
                                    tmp4984.DocNo = DocumentenNumber;
                                    tmp4984.DocDate = DocumentDate;
                                    tmp4984.SupplierId = SupplierNumber;


                                    //Position
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
                                        tmp4984.Position = iTmp;
                                    }
                                    //Artikelvereweis / Güterart
                                    strTmp = string.Empty;
                                    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 4);
                                    if (strTmp.Equals(string.Empty))
                                    {
                                        SetError(strSegment);
                                    }
                                    else
                                    {
                                        tmp4984.ArtikelVerweis = strTmp;
                                    }
                                    break;

                                case "RFF":
                                    strTmp = string.Empty;
                                    //Ordernummer / Bestellnummer
                                    if (str.Contains(strSegment + "+" + enumEdi4984SegmQualifier_RFF.ON.ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        else
                                        {
                                            tmp4984.OrderNo = strTmp;
                                        }
                                    }
                                    //Lieferabrufnummer
                                    else if (str.Contains(strSegment + "+" + enumEdi4984SegmQualifier_RFF.AAN.ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        {
                                            iTmp = 0;
                                            int.TryParse(strTmp, out iTmp);
                                            tmp4984.CallNo = iTmp;
                                        }
                                    }
                                    break;

                                case "QTY":
                                    strTmp = string.Empty;
                                    //--Rückstand
                                    if (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_QTY.Rückstand_83).ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        else
                                        {
                                            iTmp = 0;
                                            int.TryParse(strTmp, out iTmp);
                                            tmp4984.DiffQTY = iTmp;
                                        }
                                    }
                                    //
                                    else if (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_QTY.Eingangsfortschrittszahl_70).ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        {
                                            iTmp = 0;
                                            int.TryParse(strTmp, out iTmp);
                                            tmp4984.EfzQTY = iTmp;
                                        }
                                    }
                                    //
                                    else if (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_QTY.zuLieferndeMenge_113).ToString()))
                                    {
                                        strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 3);
                                        if (strTmp.Equals(string.Empty))
                                        {
                                            SetError(strSegment);
                                        }
                                        {
                                            iTmp = 0;
                                            int.TryParse(strTmp, out iTmp);
                                            tmp4984.DeliveryQTY = iTmp;
                                        }
                                    }

                                    break;

                                case "SCC":
                                    strTmp = string.Empty;
                                    ////--10 oder 24
                                    //if (
                                    //        (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_SSC.Diverses_24).ToString())) ||
                                    //        (str.Contains(strSegment + "+" + ((int)enumEdi4984SegmQualifier_SSC.Sofortbedarf_10).ToString()))
                                    //   )
                                    //{
                                    //    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str,2);
                                    //    if (strTmp.Equals(string.Empty))
                                    //    {
                                    //        SetError(strSegment);
                                    //    }
                                    //    {
                                    //        iTmp = 0;
                                    //        int.TryParse(strTmp, out iTmp);
                                    //        tmp4984.DeliveryQTY = iTmp;
                                    //    }
                                    //}
                                    strTmp = ediHelper_SegmentSplitt.GetSegmentFieldValue(str, 2);
                                    if (strTmp.Equals(string.Empty))
                                    {
                                        SetError(strSegment);
                                    }
                                    {
                                        iTmp = 0;
                                        int.TryParse(strTmp, out iTmp);
                                        tmp4984.SSCQualifier = iTmp;
                                    }
                                    break;

                                case "UNT":
                                    //listVDA4984SqlSaveString.Add(tmp4984.AddString());
                                    break;
                                case "UNZ":
                                    break;

                            }//Ende switch
                        }
                        else
                        {
                            break;
                        }
                    }//foreach
                }
                else
                {
                    //FEhlermeldugn
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool AddVDA4984()
        {
            bool bReturn = false;
            if (ListVDA4984SqlSaveString.Count > 0)
            {
                string strSql = string.Empty;
                foreach (string str in ListVDA4984SqlSaveString)
                {
                    strSql += str;
                }
                if (clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "InserVDA4984", this.GL_User.User_ID))
                {
                    string strTmp = string.Empty;
                    strTmp = "ASN [# " + this.DocumentenNumber + " vom " + DocumentDate.ToString("dd.MM.yyyy") + "] wurde gespeichert!!!";
                    tmpLog = new clsLogbuchCon();
                    tmpLog.GL_User = this.GL_User;
                    tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                    tmpLog.LogText = strTmp;
                    tmpLog.TableName = string.Empty;
                    decimal decTmp = 0;
                    tmpLog.TableID = decTmp;
                    //this..Add(tmpLog);
                }
                else
                {
                    string strTmp = string.Empty;
                    strTmp = "ASN konnte nicht gespeichert werden !!! -> SQL:[" + strSql + "]";
                    tmpLog = new clsLogbuchCon();
                    tmpLog.GL_User = this.GL_User;
                    tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                    tmpLog.LogText = strTmp;
                    tmpLog.TableName = string.Empty;
                    decimal decTmp = 0;
                    tmpLog.TableID = decTmp;
                    this.ListErrorEdiVDA.Add(tmpLog);
                }
            }
            else
            {
                string strTmp = string.Empty;
                strTmp = "Keine ASN-Daten zur Speicherung vorhanden!!!";
                tmpLog = new clsLogbuchCon();
                tmpLog.GL_User = this.GL_User;
                tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                tmpLog.LogText = strTmp;
                tmpLog.TableName = string.Empty;
                decimal decTmp = 0;
                tmpLog.TableID = decTmp;
                this.ListErrorEdiVDA.Add(tmpLog);

            }

            return bReturn;
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
            this.ListErrorEdiVDA.Add(tmpLog);
        }
        /// <summary>
        ///             ExistLE
        /// </summary>
        /// <param name="myDocNo"></param>
        /// <param name="myDocDate"></param>
        /// <returns></returns>
        public static bool ExistLE(int myDocNo, DateTime myDocDate)
        {
            bool retVal = false;
            string strToCheck = myDocNo.ToString() + "#" + myDocDate.ToString("yyyyMMdd");
            string strSQL = string.Empty;
            strSQL = "SELECT ID FROM EdiVDA4984Value " +
                        "WHERE " +
                            "CAST(DocNo as nvarchar)+'#'+CONVERT(nvarchar (10), DocDate,112) = '" + strToCheck + "' ;";

            retVal = clsSQLCOM.ExecuteSQL_GetValueBool(strSQL, 0);
            return retVal;
        }


    }
}
