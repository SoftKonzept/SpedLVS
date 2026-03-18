using LVS.Constants;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LVS
{
    public class clsADRVerweis
    {

        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GLSystem;

        public clsADR ADRSender;
        public clsADR ADRVerwAdr;




        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set
            {
                _BenutzerID = value;
            }
        }


        public static List<string> ListVerweisArt()
        {
            List<string> listTmp = new List<string>();
            listTmp.Add(string.Empty);
            listTmp.Add("SENDER");
            listTmp.Add("RECEIVER");
            return listTmp;
        }
        public decimal ID { get; set; }
        public decimal SenderAdrID { get; set; }
        public decimal VerweisAdrID { get; set; }
        public string SupplierNo { get; set; }
        public decimal MandantenID { get; set; }
        public decimal ArbeitsbereichID { get; set; }
        public string Verweis { get; set; }
        public bool aktiv { get; set; }
        public string ASNFileTyp { get; set; }
        public string LieferantenVerweis { get; set; }
        public string VerweisArt { get; set; }
        public string Bemerkung { get; set; }
        public bool UseS712F04 { get; set; }
        public bool UseS713F13 { get; set; }
        public string SenderVerweis { get; set; }

        public List<clsADRVerweis> ListVerweisAdr;
        public string Description { get; set; }

        /*****************************************************************************
         * 
         * ***************************************************************************/
        ///<summary>clsADRVerweis / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser)
        {
            this._GL_User = myGLUser;
            ADRSender = new clsADR();
            ADRVerwAdr = new clsADR();

            this.ListVerweisAdr = new List<clsADRVerweis>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySenderAdr"></param>
        /// <param name="myVerweiAdr"></param>
        /// <param name="mySystem"></param>
        public void InitClass(Globals._GL_USER myGLUser, decimal mySenderAdr, clsSystem mySystem)
        {
            try
            {
                this._GL_User = myGLUser;
                this.ListVerweisAdr = new List<clsADRVerweis>();
                this.SenderAdrID = mySenderAdr;

                ADRSender = new clsADR();
                ADRVerwAdr = new clsADR();
                if ((mySystem is clsSystem) && (mySystem.AbBereich is clsArbeitsbereiche))
                {
                    this.ArbeitsbereichID = mySystem.AbBereich.ID;
                    this.MandantenID = mySystem.Mandant.ID;
                    FillByAdrID();
                    //ADRSender.InitClass(this._GL_User, this._GLSystem, mySenderAdr, false);
                    FillListVerweisAdr();
                }
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }
        ///<summary>clsADRVerweis / Add</summary>
        ///<remarks></remarks>>
        public void Add()
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO ADRVerweis (SenderAdrID, VerweisAdrID, MandantenID, ArbeitsbereichID, Verweis, aktiv" +
                                             ", ASNFileTyp, LieferantenVerweis, VerweisArt, Bemerkung, UseS712F04, UseS713F13" +
                                             ", SenderVerweis, SupplierNo, Description" +
                                    ") " +
                                     "VALUES (" + SenderAdrID +
                                     ", " + VerweisAdrID +
                                     //", " + LieferantenID +
                                     ", " + MandantenID +
                                     ", '" + ArbeitsbereichID + "'" +
                                     ", '" + Verweis + "'" +
                                     ", " + Convert.ToInt32(aktiv) +
                                     ", '" + ASNFileTyp + "'" +
                                     ", '" + LieferantenVerweis + "'" +
                                     ", '" + VerweisArt + "'" +
                                     ", '" + Bemerkung + "'" +
                                     ", " + Convert.ToInt32(UseS712F04) +
                                     ", " + Convert.ToInt32(UseS713F13) +
                                     ", '" + this.SenderVerweis + "'" +
                                     ", '" + SupplierNo + "'" +
                                     ", '" + Description + "'" +
                                     ");";
            strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
            }
        }
        ///<summary>clsADRVerweis / Update</summary>
        ///<remarks>Update Adressdaten.</remarks>
        public void Update()
        {
            string strSQL = string.Empty;
            strSQL = "Update ADRVerweis " +
                            "SET " +
                                " SenderAdrID=" + SenderAdrID +
                                ", VerweisAdrID=" + VerweisAdrID +
                                //", LieferantenID='" + LieferantenID + "'" +
                                ", MandantenID=" + MandantenID +
                                ", ArbeitsbereichID=" + ArbeitsbereichID +
                                ", Verweis='" + Verweis + "'" +
                                ", aktiv=" + Convert.ToInt32(aktiv) +
                                ", ASNFileTyp='" + ASNFileTyp + "'" +
                                ", LieferantenVerweis='" + LieferantenVerweis + "'" +
                                ", VerweisArt ='" + VerweisArt + "'" +
                                ", Bemerkung ='" + Bemerkung + "'" +
                                ", UseS712F04=" + Convert.ToInt32(UseS712F04) +
                                ", UseS713F13=" + Convert.ToInt32(UseS713F13) +
                                ", SenderVerweis= '" + this.SenderVerweis + "'" +
                                ", SupplierNo='" + SupplierNo + "'" +

                                    " WHERE ID=" + ID + ";";

            clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            this.Fill();
        }
        ///<summary>clsADRVerweis / Delete</summary>
        ///<remarks>Delete Adressdaten.</remarks>
        public void Delete()
        {
            string strSQL = string.Empty;
            strSQL = "Delete from ADRVerweis WHERE ID=" + ID + ";";
            clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            this.Fill();
        }
        ///<summary>clsADRVerweis / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Verweise");
            //decimal decTmp = 0;
            FillValToClass(dt);
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillByAdrID()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis " +
                                    "WHERE " +
                                        "SenderAdrID=" + this.SenderAdrID +
                                        " AND VerweisAdrID=" + this.SenderAdrID + // ja beide gleich
                                        " AND MandantenID=" + this.MandantenID +
                                        " AND ArbeitsbereichID=" + this.ArbeitsbereichID +
                                        " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Verweise");
            FillValToClass(dt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        private void FillValToClass(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.SenderAdrID = (decimal)dt.Rows[i]["SenderAdrID"];
                this.VerweisAdrID = (decimal)dt.Rows[i]["VerweisAdrID"];
                //this.LieferantenID = (decimal)dt.Rows[i]["LieferantenID"];
                this.MandantenID = (decimal)dt.Rows[i]["MandantenID"];
                this.ArbeitsbereichID = (decimal)dt.Rows[i]["ArbeitsbereichID"];
                this.Verweis = dt.Rows[i]["Verweis"].ToString();
                this.aktiv = (bool)dt.Rows[i]["aktiv"];
                this.ASNFileTyp = dt.Rows[i]["ASNFileTyp"].ToString();
                this.LieferantenVerweis = dt.Rows[i]["LieferantenVerweis"].ToString();
                this.VerweisArt = string.Empty;
                if (dt.Rows[i]["VerweisArt"] != DBNull.Value)
                {
                    this.VerweisArt = dt.Rows[i]["VerweisArt"].ToString();
                }
                this.Bemerkung = string.Empty;
                if (dt.Rows[i]["Bemerkung"] != DBNull.Value)
                {
                    this.Bemerkung = dt.Rows[i]["Bemerkung"].ToString();
                }
                this.UseS712F04 = (bool)dt.Rows[i]["UseS712F04"];
                this.UseS713F13 = (bool)dt.Rows[i]["UseS713F13"];
                this.SenderVerweis = dt.Rows[i]["SenderVerweis"].ToString();
                this.SupplierNo = string.Empty;
                if (dt.Rows[i]["SupplierNo"] != DBNull.Value)
                {
                    this.SupplierNo = dt.Rows[i]["SupplierNo"].ToString();
                }


                //Adressen
                ADRSender = new clsADR();
                ADRSender.ID = this.SenderAdrID;
                ADRSender.FillClassOnly();

                ADRVerwAdr = new clsADR();
                ADRVerwAdr.ID = this.VerweisAdrID;
                ADRVerwAdr.FillClassOnly();
            }
        }
        ///<summary>clsADRVerweis / FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public void FillClassByVerweis(string myVerweis, string myASNFileTyp)
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ADRVerweis WHERE Verweis='" + myVerweis + "' and ASNFileTyp='" + myASNFileTyp + "' AND aktiv=1;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                this.Fill();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ExistVerweis()
        {
            bool bReturn = false;
            string strSql = "SELECT ID FROM ADRVerweis " +
                                        "WHERE ASNFileTyp='" + this.ASNFileTyp + "'" +
                                                " AND SenderAdrID=" + (int)this.SenderAdrID +
                                                " AND VerweisArt='" + this.VerweisArt + "'  ;";
            bReturn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, this.BenutzerID);
            return bReturn;
        }
        ///<summary>clsADRVerweis / GetADRVerweiseList</summary>
        ///<remarks></remarks>
        public DataTable GetADRVerweiseList()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "select " +
                                "ADRVerweis.ID" +
                                //", ADR.ViewID as Sender " +
                                //", (Select ViewID FROM ADR WHERE ID=ADRVerweis.VerweisAdrID) as ADRVerweis "+
                                ", Arbeitsbereich.Name as Arbeitsbereich " +
                                ", ADRVerweis.MandantenID " +
                                ", ADRVerweis.SenderAdrId " +
                                ", ADRVerweis.VerweisAdrId " +
                                ", ADRVerweis.Verweis" +
                                //", ADRVerweis.LieferantenID" +
                                ", ADRVerweis.aktiv" +
                                ", ADRVerweis.ASNFileTyp " +
                                ", ADRVerweis.LieferantenVerweis" +
                                ", ADRVerweis.VerweisArt" +
                                ", ADRVerweis.Bemerkung " +
                                ", ADRVerweis.UseS712F04 " +
                                ", ADRVerweis.UseS713F13 " +
                                ", ADRVerweis.SenderVerweis" +
                                ", ADRVerweis.SupplierNo" +
                                " from ADRVerweis " +
                                " LEFT JOIN ADR ON ADRVerweis.SenderAdrID = ADR.ID " +
                                " LEFT JOIN Arbeitsbereich ON ADRVerweis.ArbeitsbereichID = Arbeitsbereich.ID " +
                                " WHERE SenderAdrID=" + this.SenderAdrID + ";";
            //" WHERE VerweisAdrID=" + this.VerweisAdrID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ADRVerweise");
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillListVerweisAdr()
        {
            this.ListVerweisAdr = new List<clsADRVerweis>();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis " +
                                            "WHERE " +
                                                " MandantenID=" + (int)this.MandantenID +
                                                " AND ArbeitsbereichID=" + (int)this.ArbeitsbereichID +
                                                " AND SenderAdrID=" + (int)this.SenderAdrID + " ;";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this._GL_User.User_ID, "Verweise");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsADRVerweis verw = new clsADRVerweis();
                    verw._GL_User = this._GL_User;
                    verw.ID = decTmp;
                    verw.Fill();
                    this.ListVerweisAdr.Add(verw);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdrToCheck"></param>
        /// <returns></returns>
        public bool CheckEdiCommForAdr(decimal myAdrToCheck)
        {
            bool bReturn = false;
            if ((myAdrToCheck > 0) &&
                (this.ListVerweisAdr != null) &&
                (this.ListVerweisAdr.Count > 0)
               )
            {
                clsADRVerweis tmpVerweis = ListVerweisAdr.FirstOrDefault(x => x.VerweisAdrID == myAdrToCheck);
                if (tmpVerweis is clsADRVerweis)
                {
                    bReturn = true;
                }
            }
            return bReturn;
        }
        /********************************************************************************
         *                  public static
         * ******************************************************************************/
        ///<summary>clsADRVerweis / Fill</summary>
        ///<remarks></remarks>
        public static Dictionary<decimal, clsADRVerweis> GetAdrVerweis(Globals._GL_USER myGLUser, decimal mySenderAdr, decimal myMandant, decimal myABereichID)
        {
            Dictionary<decimal, clsADRVerweis> dict = new Dictionary<decimal, clsADRVerweis>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis " +
                                            "WHERE " +
                                                " MandantenID=" + (int)myMandant +
                                                " AND ArbeitsbereichID=" + (int)myABereichID +
                                                " AND SenderAdrID=" + (int)mySenderAdr + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Verweise");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsADRVerweis verw = new clsADRVerweis();
                    verw._GL_User = myGLUser;
                    verw.ID = decTmp;
                    verw.Fill();
                    if (!dict.ContainsKey(verw.VerweisAdrID))
                    {
                        dict.Add(verw.VerweisAdrID, verw);
                    }
                }
            }
            return dict;
        }
        ///<summary>clsADRVerweis / FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static Dictionary<string, clsADRVerweis> FillDictAdrVerweis(decimal myMandant, decimal myArbeitsbereich, decimal myBenuzter, string myASNFileTyp)
        {
            string strSql = string.Empty;
            List<string> strWhereStatement = new List<string>();
            if ((int)myMandant > 0)
            {
                string s = "MandantenID=" + (int)myMandant;
                strWhereStatement.Add(s);
            }
            if ((int)myArbeitsbereich > 0)
            {
                string s = "ArbeitsbereichID = " + (int)myArbeitsbereich;
                strWhereStatement.Add(s);
            }
            if (myASNFileTyp.Length > 0)
            {
                string s = "ASNFileTyp = '" + myASNFileTyp + "'";
                strWhereStatement.Add(s);
            }

            //--- sql Statement
            if (strWhereStatement.Count > 0)
            {
                strSql = "SELECT * FROM ADRVerweis ";
                strSql += " WHERE ";

                int iCount = 0;
                foreach (string s in strWhereStatement)
                {
                    if (iCount > 0)
                    {
                        strSql += " AND ";
                    }
                    strSql += s;
                    iCount++;
                }
            }

            Dictionary<string, clsADRVerweis> dict = clsADRVerweis.GetDictBySql(strSql, (int)myBenuzter);
            //dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myBenuzter, "Verweise");
            //decimal decTmp = 0;
            //for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            //{
            //    decTmp = 0;
            //    Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
            //    if (decTmp > 0)
            //    {
            //        clsADRVerweis tmpVerweis = new clsADRVerweis();
            //        tmpVerweis.BenutzerID = myBenuzter;
            //        tmpVerweis.ID = decTmp;
            //        tmpVerweis.Fill();
            //        if (!dict.ContainsKey(tmpVerweis.Verweis))
            //        {
            //            dict.Add(tmpVerweis.Verweis, tmpVerweis);
            //        }
            //    }
            //}
            return dict;
        }

        public static Dictionary<string, clsADRVerweis> GetDictBySql(string mySql, int myBenuzter)
        { 
            Dictionary<string, clsADRVerweis> dict = new Dictionary<string, clsADRVerweis>();
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(mySql, myBenuzter, "Verweise");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsADRVerweis tmpVerweis = new clsADRVerweis();
                    tmpVerweis.BenutzerID = myBenuzter;
                    tmpVerweis.ID = decTmp;
                    tmpVerweis.Fill();
                    if (!dict.ContainsKey(tmpVerweis.Verweis))
                    {
                        dict.Add(tmpVerweis.Verweis, tmpVerweis);
                    }
                }
            }
            return dict;
        }
        ///<summary>clsADRVerweis / FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static Dictionary<string, clsADRVerweis> FillDictAdrVerweisAll(decimal myBenuzter, string myASNFileTyp)
        {
            Dictionary<string, clsADRVerweis> dict = new Dictionary<string, clsADRVerweis>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis WHERE aktiv=1 AND ASNFileTyp='" + myASNFileTyp + "' ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myBenuzter, "Verweise");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsADRVerweis tmpVerweis = new clsADRVerweis();
                    tmpVerweis.BenutzerID = myBenuzter;
                    tmpVerweis.ID = decTmp;
                    tmpVerweis.Fill();
                    dict.Add(tmpVerweis.Verweis, tmpVerweis);
                }
            }
            return dict;
        }
        ///<summary>clsADRVerweis / FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static Dictionary<string, clsADRVerweis> FillDictAdrVerweisBySender(decimal decSender, decimal myBenuzter)
        {
            Dictionary<string, clsADRVerweis> dict = new Dictionary<string, clsADRVerweis>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis WHERE aktiv=1 AND SenderAdrID=" + decSender + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myBenuzter, "Verweise");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsADRVerweis tmpVerweis = new clsADRVerweis();
                    tmpVerweis.BenutzerID = myBenuzter;
                    tmpVerweis.ID = decTmp;
                    tmpVerweis.Fill();
                    dict.Add(tmpVerweis.Verweis, tmpVerweis);
                }
            }
            return dict;
        }
        ///<summary>clsADRVerweis / FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static string GetLieferantenVerweisBySenderAndReceiverAdr(decimal decSender, decimal decReceiver, decimal myBenuzter, string myASNFileTyp, decimal decAbBereich)
        {
            string strSql = string.Empty;
            strSql = " SELECT Top (1) LieferantenVerweis FROM ADRVerweis " +
                                                        " WHERE " +
                                                            " SenderAdrID=" + (Int32)decSender +
                                                            " AND VerweisAdrID =" + (Int32)decReceiver +
                                                            " AND ArbeitsbereichID=" + (Int32)decAbBereich +
                                                            " AND ASNFileTyp ='" + myASNFileTyp + "'" +
                                                            " AND aktiv=1 ; ";
            string strReturn = clsSQLcon.ExecuteSQL_GetValue(strSql, myBenuzter);
            return strReturn;
        }
        ///<summary>clsADRVerweis / FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static string GetSenderVerweisBySenderAndReceiverAdr(decimal decSender, decimal decReceiver, decimal myBenuzter, string myASNFileTyp, decimal decAbBereich)
        {
            string strSql = string.Empty;
            strSql = " SELECT Top (1) SenderVerweis FROM ADRVerweis " +
                                                        " WHERE " +
                                                            " SenderAdrID=" + (Int32)decSender +
                                                            " AND VerweisAdrID =" + (Int32)decReceiver +
                                                            " AND ArbeitsbereichID=" + (Int32)decAbBereich +
                                                            " AND ASNFileTyp ='" + myASNFileTyp + "'" +
                                                            " AND aktiv=1 ; ";
            string strReturn = clsSQLcon.ExecuteSQL_GetValue(strSql, myBenuzter);
            return strReturn;
        }
        public static string GetSupplierNoBySenderAndReceiverAdr(decimal decSender, decimal decReceiver, decimal myBenuzter, string myASNFileTyp, decimal decAbBereich)
        {
            string strSql = string.Empty;
            strSql = " SELECT Top (1) SupplierNo FROM ADRVerweis " +
                                                        " WHERE " +
                                                            " SenderAdrID=" + (Int32)decSender +
                                                            " AND VerweisAdrID =" + (Int32)decReceiver +
                                                            " AND ArbeitsbereichID=" + (Int32)decAbBereich +
                                                            " AND ASNFileTyp ='" + myASNFileTyp + "'" +
                                                            " ; ";
            string strReturn = clsSQLcon.ExecuteSQL_GetValue(strSql, myBenuzter);
            return strReturn;
        }


        public static string GetSupplierNoBySenderAndReceiverAdr(Common.Models.Eingaenge myEingang)
        {
            string strSql = string.Empty;
            string strSupplierNo = string.Empty;

            if (myEingang.ASN > 0)
            {
                Globals._GL_USER gL_USER = new Globals._GL_USER();
                gL_USER.User_ID = 1;
                AsnViewData asnVD = new AsnViewData(myEingang.ASN, gL_USER);

                switch (asnVD.asnHead.ASNFileTyp)
                {
                    case Constants.constValue_AsnArt.const_ArtBeschreibung_VDA4913:
                        strSql = " SELECT Top (1) SupplierNo FROM ADRVerweis " +
                                            " WHERE " +
                                                " SenderAdrID=" + (Int32)myEingang.Auftraggeber +
                                                " AND VerweisAdrID =" + (Int32)myEingang.Empfaenger +
                                                " AND ArbeitsbereichID=" + (Int32)myEingang.ArbeitsbereichId +
                                                " AND ASNFileTyp ='" + constValue_AsnArt.const_ArtBeschreibung_VDA4913 + "'" +
                                                " ; ";
                        strSupplierNo = clsSQLcon.ExecuteSQL_GetValue(strSql, gL_USER.User_ID);
                        break;

                    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_96A:
                    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
                        strSupplierNo = EdiClientWorkspaceValueViewData.GetSuppliertCode_NAD_CZ(myEingang.Auftraggeber, myEingang.ArbeitsbereichId, asnVD.asnHead.ASNFileTyp);
                        break;

                    default:

                        break;
                }
            }
            return strSupplierNo;
        }
        public static string GetSupplierNoBySenderAndReceiverAdr(clsLEingang myEingang)
        {
            string strSql = string.Empty;
            string strSupplierNo = string.Empty;
            if (myEingang.ASN > 0)
            {
                EingangViewData eingangVD = new EingangViewData((int)myEingang.LEingangTableID, 1, false);
                strSupplierNo = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(eingangVD.Eingang);
            }
            return strSupplierNo;
        }

        ///<summary>clsADRVerweis / Exist</summary>
        ///<remarks></remarks>
        public static bool Exist(ref clsADRVerweis myAdrVerweis, decimal myBenuzter)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ADRVerweis " +
                                    "WHERE " +
                                        "SenderAdrID=" + (int)myAdrVerweis.SenderAdrID +
                                        " AND VerweisAdrID=" + (int)myAdrVerweis.VerweisAdrID +
                                        " AND MandantenID=" + (int)myAdrVerweis.MandantenID +
                                        " AND ArbeitsbereichID=" + (int)myAdrVerweis.ArbeitsbereichID +
                                        " AND ASNFileTyp ='" + myAdrVerweis.ASNFileTyp + "'" +
                                        " AND LieferantenVerweis= '" + myAdrVerweis.LieferantenVerweis + "'" +
                                        " ;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myBenuzter);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            bReturn = (iTmp > 0);
            return bReturn;
        }
    }
}
