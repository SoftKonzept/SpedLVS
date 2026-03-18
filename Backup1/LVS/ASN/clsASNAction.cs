using Common.SqlStatementCreater;
using LVS.ASN;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsASNAction
    {
        public clsADR AdrAuftraggeber;
        public clsADR AdrEmpfaenger;

        public const string const_DBTableName = "ASNAction";

        public const string const_DBColName_ID = "ID";
        public const string const_DBColName_ActionASN = "ActionASN";
        public const string const_DBColName_ActionName = "ActionName";
        public const string const_DBColName_Auftraggeber = "Auftraggeber";
        public const string const_DBColName_Empfaenger = "Empfaenger";
        public const string const_DBColName_OrderID = "OrderID";
        public const string const_DBColName_MandantID = "MandantID";
        public const string const_DBColName_AbBereichID = "AbBereichID";
        public const string const_DBColName_ASNTypID = "ASNTypID";
        public const string const_DBColName_Bemerkung = "Bemerkung";
        public const string const_DBColName_activ = "activ";
        public const string const_DBColName_IsVirtFile = "IsVirtFile";
        public const string const_DBColName_UseOldPropertyValue = "UseOldPropertyValue";


        public const string const_ASNActionDescription_EME = "EME an Empfänger";
        public const string const_ASNActionDescription_EML = "EML an Lieferant";
        public const string const_ASNActionDescription_AME = "AME an Empfänger";
        public const string const_ASNActionDescription_AML = "AML an Lieferant";
        public const string const_ASNActionDescription_STE = "STE an Empfänger";
        public const string const_ASNActionDescription_STL = "STL an Lieferant";
        public const string const_ASNActionDescription_RLE = "RLE an Empfänger";
        public const string const_ASNActionDescription_RLL = "RLL an Lieferant";
        public const string const_ASNActionDescription_TSE = "TSE an Empfänger";
        public const string const_ASNActionDescription_TSL = "TSL an Lieferant";


        public const Int32 const_ASNAction_Eingang = 1;
        public const string const_ASNActionName_Eingang = "Eingang";
        public const Int32 const_ASNAction_Ausgang = 2;
        public const string const_ASNActionName_Ausgang = "Ausgang";
        public const Int32 const_ASNAction_StornoKorrektur = 3;
        public const string const_ASNActionName_StornoKorrektur = "Korrektur- Stornierverfahren";
        public const Int32 const_ASNAction_RücklieferungSL = 4;
        public const string const_ASNActionName_Rücklieferung = "Rücklieferung";
        public const Int32 const_ASNAction_SPLIn = 5;
        public const string const_ASNActionName_SPLIn = "SPL IN";
        public const Int32 const_ASNAction_SPLOut = 6;
        public const string const_ASNActionName_SPLOut = "SPL OUT";
        public const Int32 const_ASNAction_Umbuchung = 7;
        public const string const_ASNActionName_Umbuchung = "Umbuchung";

        public static Dictionary<int, string> GetASNActionSource()
        {
            Dictionary<int, string> dictReturn = new Dictionary<int, string>();
            dictReturn.Add(const_ASNAction_Eingang, const_ASNActionName_Eingang);
            dictReturn.Add(const_ASNAction_Ausgang, const_ASNActionName_Ausgang);
            dictReturn.Add(const_ASNAction_StornoKorrektur, const_ASNActionName_StornoKorrektur);
            dictReturn.Add(const_ASNAction_RücklieferungSL, const_ASNActionName_Rücklieferung);
            dictReturn.Add(const_ASNAction_SPLIn, const_ASNActionName_SPLIn);
            dictReturn.Add(const_ASNAction_SPLOut, const_ASNActionName_SPLOut);
            dictReturn.Add(const_ASNAction_Umbuchung, const_ASNActionName_Umbuchung);
            return dictReturn;
        }

        internal clsASNTyp ASNTyp;

        public Globals._GL_USER GL_User;
        public Globals._GL_SYSTEM GL_System;
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
        public Int32 ASNActionProcessNr { get; set; }
        public string ASNActionName { get; set; }
        public decimal Auftraggeber { get; set; }
        public decimal Empfaenger { get; set; }
        public decimal ASNTypID { get; set; }
        public Int32 OrderID { get; set; }
        public decimal MandantenID { get; set; }
        public decimal AbBereichID { get; set; }
        public string Bemerkung { get; set; }
        public bool activ { get; set; }
        public bool IsVirtFile { get; set; }
        public bool UseOldPropertyValue { get; set; }

        public Dictionary<decimal, clsASNAction> DictASNAction;
        public Dictionary<decimal, string> DictASNActionASNTyp;
        public List<int> ListASNActionASNTypActiv = new List<int>();


        public List<clsASNAction> ListAsnActionByAdr
        {
            get
            {
                return GetASNActionbyAuftraggeber((int)this.AdrAuftraggeber.ID);
            }
        }

        /**********************************************************************
         *                  Methoden / Procedure
         * *******************************************************************/
        ///<summary>clsASNAction / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(ref Globals._GL_USER myGLUser)
        {
            this.GL_User = myGLUser;
            InitSubCls();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitSubCls()
        {
            AdrAuftraggeber = new clsADR();
            if (this.Auftraggeber > 0)
            {
                AdrAuftraggeber.InitClass(this.GL_User, this.GL_System, this.Auftraggeber, true);
            }
            AdrEmpfaenger = new clsADR();
            if (this.Empfaenger > 0)
            {
                AdrEmpfaenger.InitClass(this.GL_User, this.GL_System, this.Empfaenger, true);
            }

            ASNTyp = new clsASNTyp();
            ASNTyp.InitClass(ref this.GL_User);
            if (this.ASNTypID > 0)
            {
                ASNTyp.TypID = (int)this.ASNTypID;
                ASNTyp.FillbyTypID();
            }
        }
        ///<summary>clsASNAction / Copy</summary>
        ///<remarks></remarks>
        public clsASNAction Copy()
        {
            return (clsASNAction)this.MemberwiseClone();
        }
        ///<summary>clsASNAction / InitClassByAction</summary>
        ///<remarks></remarks>
        public void InitClassByAction(ref clsLager myLager, Int32 iAction)
        {
            // neu 08.01.2023 mr
            if ((myLager.Artikel.ID > 0) && (myLager.Artikel.LEingangTableID > 0))
            {
                myLager.Eingang.LEingangTableID = myLager.Artikel.LEingangTableID;
                myLager.Eingang.FillEingang();
                if (myLager.Artikel.LAusgangTableID > 0)
                {
                    myLager.Ausgang.LAusgangTableID = myLager.Artikel.LAusgangTableID;
                    myLager.Ausgang.FillAusgang();
                }
            }
            else if (myLager.Eingang.LEingangTableID > 0)
            {
                myLager.Eingang.LEingangTableID = myLager.Artikel.LEingangTableID;
                myLager.Eingang.FillEingang();
            }
            else if (myLager.Ausgang.LAusgangTableID > 0)
            { }

            //myLager.Eingang.LEingangTableID = myLager.Artikel.LEingangTableID;
            //myLager.Eingang.FillEingang();
            //if (myLager.Artikel.LAusgangTableID > 0)
            //{
            //    myLager.Ausgang.LAusgangTableID = myLager.Artikel.LAusgangTableID;
            //    myLager.Ausgang.FillAusgang();
            //}

            switch (iAction)
            {
                case clsASNAction.const_ASNAction_Eingang:
                case clsASNAction.const_ASNAction_StornoKorrektur:
                case clsASNAction.const_ASNAction_SPLIn:
                case clsASNAction.const_ASNAction_SPLOut:
                case clsASNAction.const_ASNAction_RücklieferungSL:
                case clsASNAction.const_ASNAction_Umbuchung:
                    this.ASNActionProcessNr = iAction;
                    this.Auftraggeber = myLager.Eingang.Auftraggeber;
                    this.Empfaenger = myLager.Eingang.Empfaenger;
                    this.MandantenID = myLager.Eingang.MandantenID;
                    this.AbBereichID = myLager.Eingang.AbBereichID;
                    FillDictASNAction();
                    break;

                case clsASNAction.const_ASNAction_Ausgang:
                    this.ASNActionProcessNr = iAction;
                    this.Auftraggeber = myLager.Ausgang.Auftraggeber;
                    this.Empfaenger = myLager.Ausgang.Empfaenger;
                    this.MandantenID = myLager.Ausgang.MandantenID;
                    this.AbBereichID = myLager.Ausgang.AbBereichID;
                    FillDictASNAction();
                    break;
            }

        }
        ///<summary>clsASNAction / Add</summary>
        ///<remarks></remarks>>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = AddSqlString();
            strSql = strSql + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                Fill();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string AddSqlString()
        {
            string strSql = string.Empty;
            strSql = " INSERT INTO ASNAction (ActionASN, ActionName, Auftraggeber, Empfaenger, OrderID, MandantID, AbBereichID, ASNTypID, Bemerkung," +
                                            "activ, IsVirtFile, UseOldPropertyValue) " +
                                            "VALUES (" + (Int32)this.ASNActionProcessNr +
                                                    ",'" + this.ASNActionName + "'" +
                                                    ", " + (Int32)this.Auftraggeber +
                                                    ", " + (Int32)this.Empfaenger +
                                                    ", " + (Int32)this.OrderID +
                                                    ", " + (Int32)this.MandantenID +
                                                    ", " + (Int32)this.AbBereichID +
                                                    ", " + (Int32)this.ASNTypID +
                                                    ", '" + this.Bemerkung + "'" +
                                                    ", " + Convert.ToInt32(this.activ) +
                                                    ", " + Convert.ToInt32(this.IsVirtFile) +
                                                    ", " + Convert.ToInt32(this.UseOldPropertyValue) +
                                                    "); ";
            return strSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = string.Empty;
            strSql = "Update ASNAction SET " +
                                        "ActionASN =" + (Int32)this.ASNActionProcessNr +
                                        ", ActionName = '" + this.ASNActionName + "'" +
                                        ", Auftraggeber =  " + (Int32)this.Auftraggeber +
                                        ", Empfaenger =  " + (Int32)this.Empfaenger +
                                        ", OrderID = " + (Int32)this.OrderID +
                                        ", MandantID =  " + (Int32)this.MandantenID +
                                        ", AbBereichID = " + (Int32)this.AbBereichID +
                                        ", ASNTypID =  " + (Int32)this.ASNTypID +
                                        ", Bemerkung =  '" + this.Bemerkung + "'" +
                                        ", activ =" + Convert.ToInt32(this.activ) +
                                        ", IsVirtFile = " + Convert.ToInt32(this.IsVirtFile) +
                                        ", UseOldPropertyValue = " + Convert.ToInt32(this.UseOldPropertyValue) +


                                        " where ID=" + this.ID + ";";
            bool bReturn = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM ASNAction  where ID=" + this.ID + ";";
            bool bReturn = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return bReturn;
        }
        ///<summary>clsASNAction / SetASNActionArtForProcess</summary>
        ///<remarks></remarks>
        public void SetASNActionArtForProcess(ref clsLager myLager)
        {
            this.ASNActionProcessNr = 0;
            if (myLager.Artikel.LEingangTableID > 0)
            {
                //Check auf Korrektur und Stornierverfahren
                if (myLager.Eingang.IsKorrStVerfahrenInUse)
                {
                    this.ASNActionProcessNr = clsASNAction.const_ASNAction_StornoKorrektur;
                }
                else
                {
                    if (myLager.Artikel.LAusgangTableID < 1)
                    {
                        //Eingang 
                        // -> Aritkel.LEingangTableID>0 and Artikel.LAusgangTableID=0
                        // -> keine Lagerausgangsmeldungen 
                        //     -> EM ja
                        //     -> AM nein
                        this.ASNActionProcessNr = clsASNAction.const_ASNAction_Eingang;
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                if ((myLager.Ausgang.LAusgangTableID > 0) && (myLager.Ausgang.Checked))
                {
                    //--> AM LAgerausgang
                    //Ausgang 
                    // -> Aritkel.LEingangTableID>0 and Artikel.LAusgangTableID>0 
                    // -> Lagerausgangsmeldung 
                    //     -> EM ja
                    //     -> AM nein
                    this.ASNActionProcessNr = clsASNAction.const_ASNAction_Ausgang;
                }
            }
        }
        /// <summary>
        ///             Abfrage sorgt dafür, dass nur die hinterlegten Verbindungen (Auftraggeber zu Empfänger) erstellt werden
        ///             
        ///             21.02.2020 wieder rein genommen
        ///              "AND a.Empfaenger="+(Int32) this.Empfaenger +" "+
        ///              
        ///             08.06.2020 korrigiert wegen SLE
        ///             "AND ((a.Empfaenger=" + (Int32)this.Empfaenger + ") OR " +
        ///             "(a.Empfaenger=0) )" +
        /// </summary>
        public void FillDictASNAction()
        {
            DictASNAction = new Dictionary<decimal, clsASNAction>();
            DictASNActionASNTyp = new Dictionary<decimal, string>();
            ListASNActionASNTypActiv = new List<int>();
            DataTable dt = new DataTable("ASNAction");
            string strSQL = string.Empty;
            //strSQL = "SELECT a.*, b.Typ FROM ASNAction a " +
            //                        "INNER JOIN ASNTyp b on b.TypID=a.ASNTypID " +
            //                        "WHERE " +
            //                            "a.ActionASN=" + this.ASNActionProcessNr + " " +
            //                            "AND a.Auftraggeber =" + (Int32)this.Auftraggeber + " " +

            //                            //Test wenn Empfänger 0 dann Meldungen sollen an Lieferanten raus
            //                            "AND (" +
            //                                        "(a.Empfaenger=" + (Int32)this.Empfaenger + ") OR " +
            //                                        "(a.Empfaenger=0) " +
            //                                  ") " +
            //                            //"AND (" +
            //                            //           "(a.Empfaenger=" + (Int32)this.Empfaenger + ") OR " +
            //                            //           "(a.Empfaenger=0) OR "+
            //                            //           "(a.Empfaenger=" + (Int32)this.Auftraggeber + ")"+
            //                            //     ")" +

            //                            //"AND a.Empfaenger=" +(Int32) this.Empfaenger +" "+

            //                            "AND a.MandantID=" + (Int32)this.MandantenID + " " +
            //                            "AND a.AbBereichID=" + (Int32)this.AbBereichID + " " +
            //                            "AND a.activ=1 " +
            //                            "Order by OrderID;";
            strSQL = sqlCreater_asn_FillDictASNAction.sqlString(this.ASNActionProcessNr
                                                                , (Int32)this.Auftraggeber
                                                                , (Int32)this.Empfaenger
                                                                , (Int32)this.MandantenID
                                                                , (Int32)this.AbBereichID);

            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this.BenutzerID, "ASNAction");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                string strTyp = dt.Rows[i]["Typ"].ToString();
                if (decTmp > 0)
                {
                    clsASNAction tmpAction = new clsASNAction();
                    tmpAction.GL_User = this.GL_User;
                    tmpAction.ID = decTmp;
                    tmpAction.Fill();

                    if (!this.DictASNAction.ContainsKey(tmpAction.ID))
                    {
                        this.DictASNAction.Add(tmpAction.ID, tmpAction);
                    }
                    if (!this.DictASNActionASNTyp.ContainsKey(tmpAction.ASNTypID))
                    {
                        this.DictASNActionASNTyp.Add(tmpAction.ASNTypID, strTyp);
                    }
                    if (!ListASNActionASNTypActiv.Contains((int)tmpAction.ASNTypID))
                    {
                        ListASNActionASNTypActiv.Add((int)tmpAction.ASNTypID);
                    }
                }
            }
        }
        /// <summary>
        ///             Ermittelt alle ASNAktionen anhand des Auftraggebers
        /// </summary>
        /// <param name="myAdrID"></param>
        /// <returns>List clsASNAction</returns>
        public List<clsASNAction> GetASNActionbyAuftraggeber(Int32 myAdrID)
        {
            List<clsASNAction> retList = new List<clsASNAction>();
            DataTable dt = new DataTable("ASNAction");
            string strSQL = string.Empty;
            strSQL = "SELECT a.*, b.Typ FROM ASNAction a " +
                                    "INNER JOIN ASNTyp b on b.TypId=a.ASNTypID " +
                                    "WHERE " +
                                        "a.Auftraggeber =" + myAdrID + " " +
                                        "Order by a.ID, a.OrderID;";

            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this.BenutzerID, "ASNAction");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                string strTyp = dt.Rows[i]["Typ"].ToString();
                if (decTmp > 0)
                {
                    clsASNAction tmpASNAction = new clsASNAction();
                    tmpASNAction.InitClass(ref this.GL_User);
                    tmpASNAction.ID = decTmp;
                    tmpASNAction.Fill();
                    retList.Add(tmpASNAction);
                }
            }
            return retList;
        }
        ///<summary>clsASNAction / GetASNActionbyADR</summary>
        ///<remarks>Als übergabe ist die default ASNPartner Adrid gedacht, damit werden die Default ASNAction ermittelt</remarks>
        public List<clsASNAction> GetASNActionbyADR(Int32 myAdrID)
        {
            List<clsASNAction> retList = new List<clsASNAction>();
            DataTable dt = new DataTable("ASNAction");
            string strSQL = string.Empty;
            strSQL = "SELECT a.*, b.Typ FROM ASNAction a " +
                                    "INNER JOIN ASNTyp b on b.TypId=a.ASNTypID " +
                                    "WHERE " +
                                        "a.Auftraggeber =" + myAdrID + " " +
                                        "AND a.Empfaenger=" + myAdrID + " " +
                                        "AND a.MandantID=" + (Int32)this.MandantenID + " " +
                                        "AND a.AbBereichID=" + (Int32)this.AbBereichID + " " +
                                        "Order by a.ID, a.OrderID;";

            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this.BenutzerID, "ASNAction");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                string strTyp = dt.Rows[i]["Typ"].ToString();
                if (decTmp > 0)
                {
                    clsASNAction tmpASNAction = new clsASNAction();
                    tmpASNAction.InitClass(ref this.GL_User);
                    tmpASNAction.ID = decTmp;
                    tmpASNAction.Fill();
                    retList.Add(tmpASNAction);
                }
            }
            return retList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAuftraggeber"></param>
        /// <param name="myAbBereichID"></param>
        /// <returns></returns>
        public static string SQLGetASNActionByAuftraggeber(int myAuftraggeber)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT a.*, b.Typ FROM ASNAction a " +
                                    "INNER JOIN ASNTyp b on b.TypId=a.ASNTypID " +
                                    "WHERE " +
                                        " a.Auftraggeber=" + myAuftraggeber +
                                        //" AND a.AbBereichID=" + myAbBereichID + 
                                        "  Order by a.ID, a.OrderID;";
            return strSQL;
        }
        ///<summary>clsASNAction / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("ASNAction");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASNAction WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNAction");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ActionASN"].ToString(), out iTmp);
                this.ASNActionProcessNr = iTmp;
                this.ASNActionName = dt.Rows[i]["ActionName"].ToString();
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Auftraggeber"].ToString(), out decTmp);
                this.Auftraggeber = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Empfaenger"].ToString(), out decTmp);
                this.Empfaenger = decTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["OrderID"].ToString(), out iTmp);
                this.OrderID = iTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["MandantID"].ToString(), out decTmp);
                this.MandantenID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AbBereichID"].ToString(), out decTmp);
                this.AbBereichID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ASNTypID"].ToString(), out decTmp);
                this.ASNTypID = decTmp;
                this.Bemerkung = dt.Rows[i]["Bemerkung"].ToString();
                this.activ = (bool)dt.Rows[i]["activ"];
                this.IsVirtFile = (bool)dt.Rows[i]["IsVirtFile"];
                this.UseOldPropertyValue = (bool)dt.Rows[i]["UseOldPropertyValue"];
            }
            InitSubCls();
        }

        /*************************************************************************
         *                      public static
         * **********************************************************************/
        //public static Dictionary<decimal, string> GetASNTypDict(decimal myBenutzer)
        //{
        //    Dictionary<decimal, string> dict = new Dictionary<decimal, string>();
        //    //DataTable dt = new DataTable("ASNTyp");
        //    //string strSQL = string.Empty;
        //    //strSQL = "SELECT * FROM ASNTyp Order BY ID";
        //    //dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myBenutzer, "ASNTyp");
        //    //for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        //    //{
        //    //    decimal decID = (decimal)dt.Rows[i]["ID"];
        //    //    string strTyp = dt.Rows[i]["Typ"].ToString();
        //    //    dict.Add(decID, strTyp);
        //    //}
        //    return dict;
        //}
        ///<summary>clsASNAction / GetAdrIDWithAssignASNAction</summary>
        ///<remarks></remarks>
        public static List<Int32> GetAdrIDWithAssignASNAction(Globals._GL_USER myGLUser, Int32 myAdrID)
        {
            List<Int32> retList = new List<Int32>();
            DataTable dt = new DataTable("ASNAction");
            string strSQL = string.Empty;
            strSQL = "SELECT DISTINCT Auftraggeber FROM ASNAction WHERE Empfaenger=" + myAdrID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "ASNAction");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Auftraggeber"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    retList.Add((Int32)decTmp);
                }
            }
            return retList;
        }
        ///<summary>clsASNAction / GetASNAktionList</summary>
        ///<remarks></remarks>
        public static DataTable GetASNAktionList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("Aktion", typeof(string));

            DataRow row = dt.NewRow();
            row["ID"] = clsASNAction.const_ASNAction_Eingang;
            row["Aktion"] = clsASNAction.const_ASNActionName_Eingang;
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["ID"] = clsASNAction.const_ASNAction_Ausgang;
            row["Aktion"] = clsASNAction.const_ASNActionName_Ausgang;
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["ID"] = clsASNAction.const_ASNAction_StornoKorrektur;
            row["Aktion"] = clsASNAction.const_ASNActionName_StornoKorrektur;
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["ID"] = clsASNAction.const_ASNAction_RücklieferungSL;
            row["Aktion"] = clsASNAction.const_ASNActionName_Rücklieferung;
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["ID"] = clsASNAction.const_ASNAction_SPLIn;
            row["Aktion"] = clsASNAction.const_ASNActionName_SPLIn;
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["ID"] = clsASNAction.const_ASNAction_SPLOut;
            row["Aktion"] = clsASNAction.const_ASNActionName_SPLOut;
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["ID"] = clsASNAction.const_ASNAction_Umbuchung;
            row["Aktion"] = clsASNAction.const_ASNActionName_Umbuchung;
            dt.Rows.Add(row);
            return dt;
        }
        ///<summary>clsASNAction / GetAdrIDWithAssignASNAction</summary>
        ///<remarks></remarks>
        public static DataTable GetASNAction(Globals._GL_USER myGLUser)
        {
            DataTable dt = new DataTable("ASNAction");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASNAction ORDER BY Auftraggeber; ";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "ASNAction");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ExistAction()
        {
            bool bReturn = false;
            string strSql = "SELECT ID FROM ASNAction " +
                                        "WHERE " +
                                            "ASNTypID=" + (int)this.ASNTypID +
                                            " AND Auftraggeber=" + (int)this.Auftraggeber +
                                            " AND Empfaenger=" + (int)this.Empfaenger +
                                            " AND AbBereichID=" + (int)this.AbBereichID +
                                            " ;";
            bReturn = clsSQLCOM.ExecuteSQL_GetValueBool(strSql, this.BenutzerID);
            return bReturn;
        }
        /// <summary>
        ///             Standard ASNActions werden angelegt
        ///             1. EME 
        ///             2. EML
        ///             3. AME
        ///             4. AML
        ///             5. KS-Verfahren STE
        ///             6. KS-Verfahren STL
        ///             7. RLE
        ///             8. RLL
        ///             9. SPL STE                   
        ///             10. SPL TSE
        ///             11. SPL STL
        /// </summary>
        /// <param name="myWizzard"></param>
        public void CreateDefaultActionRange(clsASNWizzard myWizzard)
        {
            if ((myWizzard.AsnAction.AdrEmpfaenger is clsADR) && (myWizzard.AsnAction.AdrEmpfaenger.ID > 0))
            {
                this.Empfaenger = myWizzard.AsnAction.AdrEmpfaenger.ID;

                //DefaultList
                List<clsASNAction> ListAction = GetDefaultASNActionList(myWizzard);
                if (ListAction.Count > 0)
                {
                    string strSql = string.Empty;
                    foreach (clsASNAction itm in ListAction)
                    {
                        if (!itm.ExistAction())
                        {
                            strSql += itm.AddSqlString();
                        }
                    }
                    //sql Eintrag 
                    if (!strSql.Equals(string.Empty))
                    {
                        clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "ASNActionRangeAdd", this.BenutzerID);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myWiz"></param>
        /// <returns></returns>
        private List<clsASNAction> GetDefaultASNActionList(clsASNWizzard myWiz)
        {
            List<clsASNAction> ListAction = new List<clsASNAction>();
            for (int x = 0; x <= 6; x++)
            {
                switch (x)
                {
                    case 1:
                        //EME
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_Eingang, 1, clsASNTyp.const_ASNTyp_EME, const_ASNActionDescription_EME));
                        //EML
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_Eingang, 2, clsASNTyp.const_ASNTyp_EML, const_ASNActionDescription_EML));
                        break;
                    case 2:
                        //AME
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_Ausgang, 1, clsASNTyp.const_ASNTyp_AME, const_ASNActionDescription_AME));
                        //AML
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_Ausgang, 2, clsASNTyp.const_ASNTyp_AML, const_ASNActionDescription_AML));
                        break;
                    case 3:
                        //STE
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_StornoKorrektur, 1, clsASNTyp.const_ASNTyp_STE, const_ASNActionDescription_STE));
                        //EME
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_StornoKorrektur, 2, clsASNTyp.const_ASNTyp_EME, const_ASNActionDescription_EME));
                        break;
                    case 4:
                        //STE
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_RücklieferungSL, 1, clsASNTyp.const_ASNTyp_AME, const_ASNActionDescription_AME));
                        //EME
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_RücklieferungSL, 2, clsASNTyp.const_ASNTyp_AML, const_ASNActionDescription_AML));
                        break;
                    case 5:
                        //STE
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_SPLIn, 1, clsASNTyp.const_ASNTyp_STE, const_ASNActionDescription_STE));
                        //TSE
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_SPLIn, 2, clsASNTyp.const_ASNTyp_TSE, const_ASNActionDescription_TSE));
                        //RLL
                        ListAction.Add(this.CreateASNActionDefaultItem(myWiz, const_ASNAction_SPLIn, 3, clsASNTyp.const_ASNTyp_RLL, const_ASNActionDescription_RLL));
                        break;
                    case 6:
                        break;
                }
            }
            return ListAction;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myWiz"></param>
        /// <param name="myAsnActionId"></param>
        /// <param name="myOrderId"></param>
        /// <param name="myAsnTypId"></param>
        /// <param name="myDescription"></param>
        /// <returns></returns>
        private clsASNAction CreateASNActionDefaultItem(clsASNWizzard myWiz, int myAsnActionId, int myOrderId, int myAsnTypId, string myDescription)
        {
            clsASNAction tmpAction = new clsASNAction();
            tmpAction.InitClass(ref this.GL_User);
            tmpAction.ASNActionProcessNr = myAsnActionId;
            tmpAction.ASNActionName = clsASNAction.GetASNActionNameByProcessId(myAsnActionId);
            tmpAction.Auftraggeber = myWiz.AuftragggeberAdr.ID;
            tmpAction.Empfaenger = this.Empfaenger;
            tmpAction.OrderID = myOrderId;
            tmpAction.MandantenID = myWiz.GLSystem.sys_MandantenID;
            tmpAction.AbBereichID = myWiz.GLSystem.sys_ArbeitsbereichID;
            tmpAction.ASNTypID = myAsnTypId;
            tmpAction.Bemerkung = myDescription;
            tmpAction.activ = false;
            tmpAction.IsVirtFile = false;

            return tmpAction;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myProcessId"></param>
        /// <returns></returns>
        public static string GetASNActionNameByProcessId(int myProcessId)
        {
            string strReturn = string.Empty;
            switch (myProcessId)
            {
                case 1:
                    strReturn = const_ASNActionName_Eingang;
                    break;
                case 2:
                    strReturn = const_ASNActionName_Ausgang;
                    break;
                case 3:
                    strReturn = const_ASNActionName_StornoKorrektur;
                    break;
                case 4:
                    strReturn = const_ASNActionName_Rücklieferung;
                    break;
                case 5:
                    strReturn = const_ASNActionName_SPLIn;
                    break;
                case 6:
                    strReturn = const_ASNActionName_SPLOut;
                    break;
            }
            return strReturn;
        }

        /// <summary>
        ///         
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySenderAdrID"></param>
        /// <returns></returns>
        public static DataTable GetASNActionByAuftraggeber(Globals._GL_USER myGLUser, clsSystem mySystem, decimal myAuftraggeberID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = clsASNAction.SQLGetASNActionByAuftraggeber((int)myAuftraggeberID);
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "ASNActionsProcess");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySourceAdrId"></param>
        /// <param name="myDestAdrId"></param>
        /// <param name="myUserId"></param>
        /// <returns></returns>
        public static bool InsertShapeByRefAdr(List<int> myListID, int myAuftraggeberId, int myEmpfaengerId, decimal myUserId, decimal myAbBereichID, decimal myMandandtId)
        {
            bool bReturn = false;
            //Eintrag
            string strSql = string.Empty;
            strSql = " INSERT INTO [ASNAction] ([ActionASN],[ActionName],[Auftraggeber],[Empfaenger],[OrderID],[MandantID] " +
                                                ",[AbBereichID],[ASNTypID],[Bemerkung],[activ],[IsVirtFile],[UseOldPropertyValue]) " +
                    "SELECT " +
                            " a.ActionASN " +
                            ", a.ActionName " +
                            ", " + myAuftraggeberId +
                            ", " + myEmpfaengerId +
                            ", a.OrderID " +
                            ", " + (int)myMandandtId + //", a.MandantID " +
                            ", " + (int)myAbBereichID + //", a.AbBereichID" +
                            ", a.ASNTypID " +
                            ", a.Bemerkung " +
                            ", a.activ " +
                            ", a.IsVirtFile " +
                            ", a.UseOldPropertyValue " +

                                " FROM ASNAction a " +
                                    "WHERE " +
                                    " a.ID in (" + string.Join(",", myListID.ToArray()) + ");";
            bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "InsertASNArtFieldAssignment", myUserId);
            return bReturn;
        }
    }
}
