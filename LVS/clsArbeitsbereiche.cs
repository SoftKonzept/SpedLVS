using System;
using System.Data;
//using System.Windows.Forms;

namespace LVS
{
    public class clsArbeitsbereiche
    {
        ///<summary>clsArbeitsbereiche
        ///         Eigenschaften:
        ///             - ID
        ///             - ABName
        ///             - Bemerkung
        ///             - aktiv
        ///             - BenutzerID</summary>

        public Globals._GL_USER GLUser;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GLUser.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public decimal ID { get; set; }
        public string ABName { get; set; }
        public string Bemerkung { get; set; }
        public bool Aktiv { get; set; }
        public bool IsLager { get; set; }
        public bool IsSpedition { get; set; }
        public bool ASNTransfer { get; set; }
        public bool Exist { get; set; }
        private decimal mandantenID;
        public decimal MandantenID
        {
            get
            {
                return mandantenID;
            }
            set
            {
                int iTmp = 0;
                int.TryParse(value.ToString(), out iTmp);
                if (iTmp > 0)
                {
                    this.Mandant = new clsMandanten();
                    this.Mandant.InitCls(this.GLUser, iTmp);
                }
                mandantenID = iTmp;
            }
        }

        public bool UseAutoRowAssignment { get; set; }

        public clsMandanten Mandant { get; set; }

        public clsArbeitsbereichDefaultValue DefaultValue;

        public string ArbeitsbereichString
        {
            get
            {
                string strRet = string.Empty;
                if (this.ID > 0)
                {
                    strRet += "[" + this.ID.ToString() + "] - " + this.ABName;
                }
                return strRet;
            }
        }

        public int ArtMaxCountInAusgang { get; set; }
        public int AdrId { get; set; }
        public clsCompany Company;

        /***************************************************************************************************
        *                                     Methoden und Funktionen
        ***************************************************************************************************/
        public clsArbeitsbereiche()
        {

        }
        public clsArbeitsbereiche(int myId, int myUserId) : this()
        {
            GLUser = new Globals._GL_USER();
            GLUser.User_ID = myUserId;
            DefaultValue = new clsArbeitsbereichDefaultValue();
            DefaultValue._GL_User = GLUser;

            if (myId > 0)
            {
                this.ID = myId;
                this.Fill();

                DefaultValue.InitCls(this.ID);
            }
        }

        ///<summary>clsArbeitsbereiche / InitCls</summary>
        ///<remarks></remarks>
        public void InitCls(Globals._GL_USER myGLUser, decimal myAbBereichID)
        {
            GLUser = myGLUser;
            DefaultValue = new clsArbeitsbereichDefaultValue();
            DefaultValue._GL_User = GLUser;

            if (myAbBereichID > 0)
            {
                this.ID = myAbBereichID;
                this.Fill();

                DefaultValue.InitCls(this.ID);
            }
        }
        public void InitDefaultValue()
        {
            DefaultValue = new clsArbeitsbereichDefaultValue();
            DefaultValue._GL_User = GLUser;
            DefaultValue.InitCls(this.ID);
        }
        public void InitCls(Globals._GL_USER myGLUser, clsQueue myQueue)
        {
            GLUser = myGLUser;
            DefaultValue = new clsArbeitsbereichDefaultValue();
            DefaultValue._GL_User = GLUser;

            string strSql = string.Empty;
            switch (myQueue.TableName)
            {
                case "LEingang":
                    strSql = "SELECT AbBereich FROM LEingang WHERE ID=" + (int)myQueue.TableID;
                    break;

                case "LAusgang":
                    strSql = "SELECT AbBereich FROM LAusgang WHERE ID=" + (int)myQueue.TableID;
                    break;

                case "Artikel":
                    strSql = "SELECT AB_ID FROM Artikel WHERE ID=" + (int)myQueue.TableID;
                    break;
            }
            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Arbeitsbereich", this.BenutzerID);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                this.ID = iTmp;
                this.Fill();

                DefaultValue.InitCls(this.ID);
            }
        }

        public clsArbeitsbereiche Copy()
        {
            return (clsArbeitsbereiche)this.MemberwiseClone();
        }
        ///<summary>clsArbeitsbereiche / InitArbeitsbereich</summary>
        ///<remarks>Wertezuweisung der Klasseneigenschaften.</remarks>
        ///<param name="decID">ID = decID.</param>
        ///<param name="strName">ABName = strName;</param>
        ///<param name="strBemerkung">Bemerkung = strBemerkung;</param>
        ///<param name="bAktiv">aktiv = bAktiv;</param>
        public void InitArbeitsbereich(decimal decID, string strName, string strBemerkung, bool bAktiv, bool bASN, decimal myMandant)
        {
            //ID = decID;
            //ABName = strName;
            //Bemerkung = strBemerkung;
            //Aktiv = bAktiv;
            //ASNTransfer = bASN;
            //MandantenID = myMandant;
        }


        ///<summary>clsArbeitsbereiche / AddArbreitsbereich</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void AddArbreitsbereich()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO Arbeitsbereich (Name, Bemerkung, aktiv, ASNTransfer, MandantenID, IsLager, IsSpedition, UseAutoRowAssignment" +
                                                 ",ArtMaxCountInAusgang, AdrId) " +
                                           "VALUES ('" + ABName + "'" +
                                                    ",'" + Bemerkung + "'" +
                                                    ", " + Convert.ToInt32(Aktiv) +
                                                    ", " + Convert.ToInt32(ASNTransfer) +
                                                    ", " + MandantenID +
                                                    ", " + Convert.ToInt32(IsLager) +
                                                    ", " + Convert.ToInt32(IsSpedition) +
                                                    ", " + Convert.ToInt32(UseAutoRowAssignment) +
                                                    ", " + this.ArtMaxCountInAusgang +
                                                    ", " + this.AdrId +
                                                    ")";

            strSql = strSql + "Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
            }
            //clsSQLcon.ExecuteSQL(strSql, BenutzerID);
        }
        ///<summary>clsArbeitsbereiche / UpdateArbeitsbereich</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public bool UpdateArbeitsbereich()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Arbeitsbereich SET " +
                                                    "Name ='" + ABName + "'" +
                                                    ", Bemerkung = '" + Bemerkung + "'" +
                                                    ", aktiv = " + Convert.ToInt32(Aktiv) +
                                                    ", ASNTransfer=" + Convert.ToInt32(ASNTransfer) +
                                                    ", MandantenID =" + MandantenID +
                                                    ", IsLager = " + Convert.ToInt32(IsLager) +
                                                    ", IsSpedition = " + Convert.ToInt32(IsSpedition) +
                                                    ", UseAutoRowAssignment=" + Convert.ToInt32(UseAutoRowAssignment) +
                                                    ", ArtMaxCountInAusgang = " + this.ArtMaxCountInAusgang +
                                                    ", AdrId = " + this.AdrId +

                                                    " WHERE ID='" + ID + "'";
                return clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }


        public static string sql_GetArbeitsbereichList()
        {
            string strSql = string.Empty;
            strSql = "Select a.ID" +
                            ", a.Name as Arbeitsbereich" +
                            ", a.MandantenID" +
                            ", c.ViewID as Mandantenname" +
                            ", a.Bemerkung " +
                            ", a.aktiv" +
                            ", a.IsLager as Lager" +
                            ", a.IsSpedition as Spedition" +
                            ", a.ASNTransfer" +
                            ", a.UseAutoRowAssignment" +
                            ", a.ArtMaxCountInAusgang " +
                            ", a.AdrId " +

                            " FROM Arbeitsbereich a " +
                            "INNER JOIN Mandanten b ON b.ID=a.MandantenID " +
                            "INNER JOIN ADR c ON c.ID = b.ADR_ID " +
                            "ORDER BY ID ";
            return strSql;
        }

        ///<summary>clsArbeitsbereiche / GetArbeitsbereichList</summary>
        ///<remarks>Statische Funktion, die alle Arbeitsbereiche ausliest.</remarks>
        ///<returns>Returns DataTable</returns>
        public static DataTable GetArbeitsbereichList(decimal decBenutzerID)
        {
            DataTable dt = new DataTable();
            string strSql = clsArbeitsbereiche.sql_GetArbeitsbereichList();
            //strSql = "Select a.ID" +
            //                ", a.Name as Arbeitsbereich" +
            //                ", a.MandantenID" +
            //                ", c.ViewID as Mandantenname" +
            //                ", a.Bemerkung " +
            //                ", a.aktiv" +
            //                ", a.IsLager as Lager" +
            //                ", a.IsSpedition as Spedition" +
            //                ", a.ASNTransfer" +
            //                ", a.UseAutoRowAssignment" +
            //                ", a.ArtMaxCountInAusgang " +

            //                " FROM Arbeitsbereich a " +
            //                "INNER JOIN Mandanten b ON b.ID=a.MandantenID " +
            //                "INNER JOIN ADR c ON c.ID = b.ADR_ID " +
            //                "ORDER BY ID ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "Arbeitsbereichsliste");
            return dt;
        }
        ///<summary>clsArbeitsbereiche / GetArbeitsbereichListByStatus</summary>
        ///<remarks>Statische Funktion, die alle Arbeitsbereiche ausliest nach Auswahl aktiv / inaktiv.</remarks>
        ///<returns>Returns DataTable</returns>
        public static DataTable GetArbeitsbereichListByStatus(decimal decBenutzerID, bool baktiv)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select ID, Name as Arbeitsbereich, Bemerkung, aktiv " +
                                                    " FROM Arbeitsbereich " +
                                                    "WHERE aktiv=" + Convert.ToInt32(baktiv) +
                                                    " ORDER BY ID ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "Arbeitsbereichsliste");
            return dt;
        }
        ///<summary>clsArbeitsbereiche / GetArbeitsbereichListByUser</summary>
        ///<remarks>Statische Funktion, die alle Arbeitsbereiche ausliest nach Auswahl aktiv / inaktiv.</remarks>
        ///<returns>Returns DataTable</returns>
        public static DataTable GetArbeitsbereichListByUser(decimal decBenutzerID, bool baktiv)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select ID" +
                            ", Name as Arbeitsbereich" +
                            ", Bemerkung" +
                            ", aktiv " +
                                    "FROM Arbeitsbereich a " +
                                    "INNER JOIN ArbeitsbereichUser u ON u.AbBereichID=a.ID " +
                                    "WHERE " +
                                            "a.aktiv=" + Convert.ToInt32(baktiv) +
                                            " AND u.UserID=" + decBenutzerID + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "Arbeitsbereichsliste");
            return dt;
        }
        ///<summary>clsArbeitsbereiche / GetArbeitsbereichByID</summary>
        ///<remarks>Ermittelt anhand der Arbeitsbereichs ID die Daten des Arbeitsbereichs. Die Eigenschaftswerte werden direkt zugewiesen.</remarks>
        ///<returns>Returns BOOL Abfrage OK >>> </returns>
        public bool Fill()
        {
            bool boVal = false;
            if (ID > 0)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Arbeitsbereich " +
                                            " WHERE ID=" + ID + ";";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Arbeitsbereiche");

                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.ABName = dt.Rows[i]["Name"].ToString().Trim();
                    this.Bemerkung = dt.Rows[i]["Bemerkung"].ToString().Trim();
                    this.Aktiv = (bool)dt.Rows[i]["aktiv"];
                    this.ASNTransfer = (bool)dt.Rows[i]["ASNTransfer"];
                    decimal decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["MandantenID"].ToString(), out decTmp);
                    this.MandantenID = decTmp;
                    this.IsLager = (bool)dt.Rows[i]["IsLager"];
                    this.IsSpedition = (bool)dt.Rows[i]["IsSpedition"];
                    this.UseAutoRowAssignment = (bool)dt.Rows[i]["UseAutoRowAssignment"];
                    int iTmp = 0;
                    int.TryParse(dt.Rows[i]["ArtMaxCountInAusgang"].ToString(), out iTmp);
                    this.ArtMaxCountInAusgang = iTmp;
                    iTmp = 0;
                    int.TryParse(dt.Rows[i]["AdrId"].ToString(), out iTmp);
                    this.AdrId = iTmp;

                    if (this.AdrId > 0)
                    {
                        Company = new clsCompany();
                        Company.InitCls(this.GLUser, (int)this.ID, this.AdrId);
                    }
                    boVal = true;
                }
            }
            Exist = boVal;
            return boVal;
        }
        ///<summary>clsArbeitsbereiche / ExistArbeitsbereich</summary>
        ///<remarks>Prüft, ob der Arbeitsbereichsname bereits in der DB existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public static bool ExistArbeitsbereich(string strABName, decimal decBenutzerID)
        {
            bool boVal = true;
            strABName = strABName.Trim();
            if (strABName != string.Empty)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Arbeitsbereich WHERE Name='" + strABName + "'";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "Arbeitsbereiche");

                if (dt.Rows.Count == 0)
                {
                    boVal = false;
                }
            }
            return boVal;
        }
        /// <summary>
        ///             beinhaltet die Arbeitsbereiche:
        ///             - nicht den aktuellen
        ///             - wo mindestens 1 Report hinterlegt ist
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myCurAbBereichID"></param>
        /// <returns></returns>
        public static DataTable GetArbeitsbereichForInitReports(Globals._GL_USER myGLUser, int myCurAbBereichID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            //strSql = "SELECT DISTINCT a.* " +
            //                    "FROM Arbeitsbereich a " +
            //                    "INNER JOIN ReportDocSettingAssignment rsa on a.ID = rsa.AbBereichID " +
            //                    " WHERE " +
            //                        " a.ID <> " + myCurAbBereichID +
            //                        " AND rsa.ReportFileName <> '' ";

            strSql = "SELECT DISTINCT a.* " +
                    "FROM Arbeitsbereich a " +
                    "INNER JOIN ReportDocSettingAssignment rsa on a.ID = rsa.AbBereichID ;";
            //" WHERE " +
            //    " a.ID <> " + myCurAbBereichID +
            //    " AND rsa.ReportFileName <> '' ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Arbeitsbereichsliste");
            return dt;
        }

    }
}
