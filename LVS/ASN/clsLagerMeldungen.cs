using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsLagerMeldungen
    {
        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        //public clsSystem System; 
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

        public decimal ID { get; set; }
        public decimal ASNTypID { get; set; }
        public decimal ASNID { get; set; }
        public decimal ArtikelID { get; set; }
        public DateTime Datum { get; set; }
        public string Info { get; set; }
        public string FileName { get; set; }
        public decimal Sender { get; set; }
        public decimal Receiver { get; set; }
        public Int32 ASNAction { get; set; }


        public bool LM_LSL { get; set; }  //Lieferschein vom Lieferanten mit Artikeln, die eingelagert werden sollen
        public bool LM_EML { get; set; }  //Lagereingangsmeldung an den Lieferanten
        public bool LM_EME { get; set; }  //Lagereingangsmeldung an den Empfänger
        public bool LM_AML { get; set; }  //Lagerausgangsmeldung an den Lieferanten
        public bool LM_AME { get; set; }  //Lagerausgangsmeldung an den Empfänger
        public bool LM_AVL { get; set; }  //Avis an den Lieferanten
        public bool LM_AVE { get; set; }  //Avis an den Empfänger
        public bool LM_STL { get; set; }  //Stornomeldung an den Lieferanten  
        public bool LM_STE { get; set; }  //Stornomeldung an den Empfänger
        public bool LM_BML { get; set; }  //Stornomeldung an den Empfänger
        public bool LM_BME { get; set; }  //Stornomeldung an den Empfänger
        public bool LM_RLL { get; set; }  //Rücklieferung an den Lieferant
        public bool LM_TSL { get; set; }  //Transportschaden an den Lieferant

        public bool LM_UBL { get; set; }  //Transportschaden an den Lieferant
        public bool LM_UBE { get; set; }  //Transportschaden an den Lieferant

        private void SetAllLM(bool bSet)
        {
            LM_LSL = bSet;
            LM_EML = bSet;
            LM_EME = bSet;
            LM_AML = bSet;
            LM_AME = bSet;
            LM_AVL = bSet;
            LM_AVE = bSet;
            LM_STL = bSet;
            LM_STE = bSet;
            LM_BME = bSet;
            LM_BML = bSet;
            LM_RLL = bSet;
            LM_TSL = bSet;
            LM_UBE = bSet;
            LM_UBL = bSet;
        }

        public Dictionary<Int32, List<clsLagerMeldungen>> DictLagermeldungenSender = new Dictionary<int, List<clsLagerMeldungen>>();
        public Dictionary<Int32, List<clsLagerMeldungen>> DictLagermeldungenReceiver = new Dictionary<int, List<clsLagerMeldungen>>();
        /*********************************************************************************
         *                  procedure / Methoden
         * *****************************************************************************/
        ///<summary>clsLagerMeldungen / InitLagerMeldungen</summary>
        ///<remarks>.</remarks>
        public void InitLagerMeldungen(clsArtikel myArtikel)
        {
            this.GL_User = myArtikel._GL_User;
            this.GLSystem = myArtikel._GL_System;
            SetAllLM(false);

            if (myArtikel.ID > 0)
            {
                FillLMbyArtikel(myArtikel.ID);
            }

            //prüfen, ob für diesen Abreitsbereich der ASNTransfer frei ist
            //if (
            //    (this.GLSystem.sys_ArbeitsbereichID == myArtikel.AbBereichID) &
            //    (this.GLSystem.sys_MandantenID == myArtikel.MandantenID) &
            //    (this.GLSystem.sys_Arbeitsbereich_ASNTransfer)
            //   )
            //{
            //    FillLMbyArtikel(myArtikel.ID);
            //}

            //if (
            //    (this.System.AbBereich.ID == myArtikel.AbBereichID) &
            //    (this.System.AbBereich.MandantenID == myArtikel.MandantenID) &
            //    (this.System.AbBereich.ASNTransfer)
            //   )
            //{
            //    FillLMbyArtikel(myArtikel.ID);
            //}
        }


        ///<summary>clsLagerMeldungen / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO LagerMeldungen (ASNTypID, ArtikelID, ASNID, Datum, Info, FileName, Sender, Receiver" +
                                        ") " +
                                            "VALUES (" + ASNTypID +
                                                    ", " + ArtikelID +
                                                    ", " + ASNID +
                                                    ", '" + Datum + "'" +
                                                    ", '" + Info + "'" +
                                                    ", '" + FileName + "'" +
                                                    ", " + Sender +
                                                    ", " + Receiver +
                                                    ", " + ASNAction +
                                                    ");";
            strSql = strSql + " Select  @@IDENTITY; ";
            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "LagerMeldungen", BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                this.Fill();
            }
        }
        ///<summary>clsLagerMeldungen / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("Lagermeldungen");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM LagerMeldungen WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Lagermeldungen");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.ASNTypID = (decimal)dt.Rows[i]["ASNTypID"];
                this.ASNID = (decimal)dt.Rows[i]["ASNID"];
                this.Datum = (DateTime)dt.Rows[i]["Datum"];
                this.ArtikelID = (decimal)dt.Rows[i]["ArtikelID"];
                this.Info = dt.Rows[i]["Info"].ToString();
                this.FileName = dt.Rows[i]["FileName"].ToString();
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Sender"].ToString(), out decTmp);
                this.Sender = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Receiver"].ToString(), out decTmp);
                this.Sender = decTmp;
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ASNAction"].ToString(), out iTmp);
                this.ASNAction = iTmp;
            }
        }
        ///<summary>clsLagerMeldungen / Copy</summary>
        ///<remarks></remarks>
        public clsLagerMeldungen Copy()
        {
            return (clsLagerMeldungen)this.MemberwiseClone();
        }
        ///<summary>clsLagerMeldungen / Fill</summary>
        ///<remarks></remarks>
        public void FillDictLagermeldungenSender(decimal myAdrID)
        {
            DictLagermeldungenSender = new Dictionary<int, List<clsLagerMeldungen>>();
            List<clsLagerMeldungen> listTmp = new List<clsLagerMeldungen>();

            DataTable dt = new DataTable("Lagermeldungen");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM LagerMeldungen WHERE Sender=" + (Int32)myAdrID + " Order BY ASNTypID, Datum;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Lagermeldungen");

            decimal iOldASNTyp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (i == 0)
                {
                    decimal.TryParse(dt.Rows[i]["ASNTypID"].ToString(), out iOldASNTyp);
                }

                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsLagerMeldungen tmpLM = this.Copy();
                    tmpLM.ID = decTmp;
                    tmpLM.Fill();
                    listTmp.Add(tmpLM);

                    if (iOldASNTyp != tmpLM.ASNTypID)
                    {
                        if (!DictLagermeldungenSender.ContainsKey((Int32)tmpLM.ASNTypID))
                        {
                            DictLagermeldungenSender.Add((Int32)tmpLM.ASNTypID, listTmp);
                            listTmp = new List<clsLagerMeldungen>();
                        }
                    }
                }
            }
        }
        ///<summary>clsLagerMeldungen / Fill</summary>
        ///<remarks></remarks>
        public void FillDictLagermeldungenReceiver(decimal myAdrID)
        {
            DictLagermeldungenReceiver = new Dictionary<int, List<clsLagerMeldungen>>();
            List<clsLagerMeldungen> listTmp = new List<clsLagerMeldungen>();

            DataTable dt = new DataTable("Lagermeldungen");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM LagerMeldungen WHERE Receiver=" + (Int32)myAdrID + " Order BY ASNTypID, Datum;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Lagermeldungen");

            decimal iOldASNTyp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (i == 0)
                {
                    decimal.TryParse(dt.Rows[i]["ASNTypID"].ToString(), out iOldASNTyp);
                }

                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsLagerMeldungen tmpLM = this.Copy();
                    tmpLM.ID = decTmp;
                    tmpLM.Fill();
                    listTmp.Add(tmpLM);

                    if (iOldASNTyp != tmpLM.ASNTypID)
                    {
                        if (!DictLagermeldungenReceiver.ContainsKey((Int32)tmpLM.ASNTypID))
                        {
                            DictLagermeldungenReceiver.Add((Int32)tmpLM.ASNTypID, listTmp);
                            listTmp = new List<clsLagerMeldungen>();
                        }
                    }
                }
            }
        }
        ///<summary>clsLagerMeldungen / FillLMbyArtikel</summary>
        ///<remarks></remarks>
        public void FillLMbyArtikel(decimal myArtikelID)
        {
            Dictionary<Int32, string> dictTmp = clsASNTyp.GetASNTypDict(this.GL_User.User_ID);
            //Dictionary<decimal, string> dictTmp = clsASNTyp.GetASNTypDict(this.GL_User.User_ID);
            DataTable dt = new DataTable("Lagermeldungen");
            string strSQL = string.Empty;
            strSQL = "SELECT DISTINCT ASNTypID FROM LagerMeldungen WHERE ArtikelID=" + myArtikelID + ";"; // A = ArtikelID??
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Lagermeldungen");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                bool bExist = false;
                this.ASNTypID = (decimal)dt.Rows[i]["ASNTypID"];
                string strTyp = string.Empty;
                dictTmp.TryGetValue((Int32)this.ASNTypID, out strTyp);
                switch ((Int32)this.ASNTypID)
                {
                    //case "LSL":
                    case clsASNTyp.const_ASNTyp_LSL:
                        this.LM_LSL = true;
                        break;
                    //case "EML":
                    case clsASNTyp.const_ASNTyp_EML:
                        this.LM_EML = true;
                        break;
                    //case "EME":
                    case clsASNTyp.const_ASNTyp_EME:
                        this.LM_EME = true;
                        break;
                    case clsASNTyp.const_ASNTyp_BML:
                        this.LM_BML = true;
                        break;
                    case clsASNTyp.const_ASNTyp_BME:
                        this.LM_BME = true;
                        break;
                    //case "AML":
                    case clsASNTyp.const_ASNTyp_AML:
                        this.LM_AML = true;
                        break;
                    //case "AME":
                    case clsASNTyp.const_ASNTyp_AME:
                        this.LM_AME = true;
                        break;
                    //case "AVL":
                    case clsASNTyp.const_ASNTyp_AVL:
                        this.LM_AVL = true;
                        break;
                    //case "AVE":
                    case clsASNTyp.const_ASNTyp_AVE:
                        this.LM_AVE = true;
                        break;
                    //case "STL":
                    case clsASNTyp.const_ASNTyp_STL:
                        this.LM_STE = true;
                        break;
                    //case "STE":
                    case clsASNTyp.const_ASNTyp_STE:
                        this.LM_STE = true;
                        break;
                    //case "RLL":
                    case clsASNTyp.const_ASNTyp_RLL:
                        this.LM_RLL = true;
                        break;
                    //case "TSL":
                    case clsASNTyp.const_ASNTyp_TSL:
                        this.LM_TSL = true;
                        break;
                    case clsASNTyp.const_ASNTyp_UBE:
                        this.LM_UBE = true;
                        break;
                    case clsASNTyp.const_ASNTyp_UBL:
                        this.LM_UBL = true;
                        break;

                }
            }
        }
        ///<summary>clsLagerMeldungen / AddArtikelList</summary>
        ///<remarks>Eintrag der Lagermeldungen und der ArtikelVita.</remarks>
        public void AddArtikelList(List<string> myList, string myAsnTyp)
        {
            string strSql = string.Empty;
            //Liste durchlaufen
            for (Int32 i = 0; i <= myList.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(myList[i].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsLagerMeldungen lmTmp = new clsLagerMeldungen();
                    lmTmp.ASNTypID = this.ASNTypID;
                    lmTmp.ASNID = this.ASNID;
                    lmTmp.Datum = DateTime.Now;
                    lmTmp.ArtikelID = decTmp;
                    lmTmp.Info = string.Empty;
                    lmTmp.FileName = this.FileName;
                    lmTmp.Sender = this.Sender;
                    lmTmp.Receiver = this.Receiver;
                    lmTmp.ASNAction = this.ASNAction;
                    strSql = strSql + lmTmp.GetSQLAdd();

                    strSql = strSql + " " + clsArtikelVita.AddArtikelLagermeldungen(this.GL_User, lmTmp.ArtikelID, myAsnTyp);
                }
            }
            if (strSql != string.Empty)
            {
                bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InsertLM", this.BenutzerID);
            }
        }
        ///<summary>clsLagerMeldungen / GetSQLAdd</summary>
        ///<remarks></remarks>
        private string GetSQLAdd()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO LagerMeldungen (ASNTypID, ArtikelID, ASNID, Datum, Info, FileName, Sender, Receiver, ASNAction" +
                                        ") " +
                                            "VALUES (" + ASNTypID +
                                                    ", " + ArtikelID +
                                                    ", " + ASNID +
                                                    ", '" + Datum + "'" +
                                                    ", '" + Info + "'" +
                                                    ", '" + FileName + "'" +
                                                    ", " + (Int32)Sender +
                                                    ", " + (Int32)Receiver +
                                                    ", " + (Int32)ASNAction +
                                                    "); ";
            return strSql;
        }


    }
}
