using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
//using System.Windows.Forms;

namespace LVS
{
    public class clsReportDocSettingAssignment
    {
        public clsReportDocSettingAssignment()
        { }
        public clsReportDocSettingAssignment(string myStartupPath) : this()
        {
            this.StartupPath = myStartupPath;
        }

        public Globals._GL_USER GLUser;
        public Globals._GL_SYSTEM GLSystem;
        public LVS.clsSystem Sys;

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GLUser.User_ID;
                return _BenutzerID;
            }
            set
            {
                _BenutzerID = value;
            }
        }

        /*********************************************************************************************************/
        public Int32 ID { get; set; }
        public Int32 DocKeyID { get; set; }
        public string DocKey { get; set; }
        public string Path { get; set; }
        public string StartupPath { get; set; }
        public string ReportFileName { get; set; }
        public decimal AdrID { get; set; }          // zugewiesene Adressid
        public decimal AbBereichID { get; set; }    // Arbeitsbereich
        public decimal MandantenID { get; set; }    // Mandant
        public string Art { get; set; }
        public bool IsDefault { get; set; }
        private bool _IsCustomizedDoc;
        public bool IsCustomizedDoc
        {
            get
            {
                _IsCustomizedDoc = CheckForCustumizeDoc();
                if (!_IsCustomizedDoc)
                {
                    this.AdrID = 0;
                    this.IsDefault = true;
                }
                this.FillbyAdr();
                return _IsCustomizedDoc;
            }
        }

        public bool ExistDocKey
        {
            get
            {
                string strSQL = string.Empty;
                strSQL = "Select Distinct DocKey FROM ReportDocSettingAssignment " +
                                        "WHERE " +
                                        " DocKey='" + this.DocKey + "' " +
                                        " AND AbBereichID=" + (int)this.AbBereichID +
                                        " AND MandantenID=" + (int)this.MandantenID;
                return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
            }
        }


        public string AdrStringShort
        {
            get
            {
                string strAdr = string.Empty;
                if (AdrID > 0)
                {
                    clsADR tmpAdr = new clsADR();
                    tmpAdr.InitClass(this.GLUser, this.GLSystem, this.AdrID, true);
                    strAdr = tmpAdr.ADRStringShort;
                }
                return strAdr;
            }
        }
        public string AbBereichName
        {
            get
            {
                string strName = string.Empty;
                if (this.AbBereichID > 0)
                {
                    clsArbeitsbereiche tmpAbBereich = new clsArbeitsbereiche();
                    tmpAbBereich.InitCls(this.GLUser, this.AbBereichID);
                    strName = tmpAbBereich.ABName;
                }
                return strName;
            }
        }
        public bool ExistDocKeyAsDefault
        {
            get
            {
                string strSQL = string.Empty;
                strSQL = "Select Distinct DocKey FROM ReportDocSettingAssignment " +
                                        "WHERE IsDefault=1 AND DocKey='" + this.DocKey + "' ";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
            }
        }

        public bool ExistClassItem
        {
            get
            {
                string strSQL = string.Empty;
                strSQL = "Select ID FROM ReportDocSettingAssignment " +
                                        "WHERE " +
                                            " DocKey = '" + this.DocKey + "'" +
                                            " AND [Path]='" + this.Path + "' " +
                                            " AND ReportFileName = '" + this.ReportFileName + "'" +
                                            " AND IsDefault=" + Convert.ToInt32(this.IsDefault) +
                                            " AND AdrID =" + this.AdrID +
                                            " AND AbBereichID =" + this.AbBereichID +
                                            " AND DocKeyID =" + this.DocKeyID +
                                            " AND MandantenID =" + this.MandantenID +
                                            "; ";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
            }
        }


        private byte[] _ReportData;
        public byte[] ReportData
        {
            get
            {
                return _ReportData;
            }
            set { _ReportData = value; }
        }
        public bool ExistReportData { get; set; }
        public string FileExtension { get; set; }


        /*********************************************************************************************************
         *                              Procedure / Methoden
         * ******************************************************************************************************/
        ///<summary>clsReportDocSettingAssignment / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSys, clsSystem mySystem)
        {
            this.GLUser = myGLUser;
            this.GLSystem = myGLSys;
            this.Sys = mySystem;
            this.StartupPath = mySystem.StartupPath;
        }
        ///<summary>clsReportDoc / AddSQL</summary>
        ///<remarks></remarks>
        public string AddSQL()
        {
            string strSQL = string.Empty;
            strSQL = " INSERT INTO ReportDocSettingAssignment (DocKey, Path, ReportFileName, IsDefault, AdrID, AbBereichID, DocKeyID" +
                                         ", MandantenID, FileExtension) " +
                                         "VALUES ('" + this.DocKey + "'" +
                                                ", '" + this.Path + "'" +
                                                ", '" + this.ReportFileName + "'" +
                                                ", " + Convert.ToInt32(this.IsDefault) +
                                                ", " + (Int32)this.AdrID +
                                                ", " + (Int32)this.AbBereichID +
                                                ", " + this.DocKeyID +
                                                ", " + (int)this.MandantenID +
                                                ", '" + FileExtension + "'" +

                                                "); ";
            return strSQL;
        }
        ///<summary>clsReportDocSettingAssignment / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSQL = string.Empty;
            strSQL = this.AddSQL();
            strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                this.ID = iTmp;
                UpdateByteData(false);
            }
        }

        public void UpdateByteData(bool myDelete)
        {
            if (this.ID > 0)
            {
                if (myDelete)
                {
                    //string strSQL = "Update ReportDocSettingAssignment SET Report=@p WHERE ID=" + this.ID;
                    //InsertByteData(strSQL, ReportData, "@p");

                    string strSQL = "Update ReportDocSettingAssignment SET " +
                                                                        "Report=null " +
                                                                        ", Path='' " +
                                                                        ", ReportFileName='' " +
                                                                        ", FileExtension='' " +
                                                                        "WHERE ID=" + this.ID;
                    clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
                }
                else
                {
                    string FilePath = System.IO.Path.Combine(StartupPath + Path, ReportFileName);
                    if (File.Exists(FilePath))
                    {
                        FileStream stream = File.OpenRead(FilePath);
                        byte[] ReportData = new byte[stream.Length];
                        stream.Read(ReportData, 0, ReportData.Length);
                        if ((ReportData != null) && (ReportData.Length > 0))
                        {
                            string strSQL = "Update ReportDocSettingAssignment SET Report=@p WHERE ID=" + this.ID;
                            InsertByteData(strSQL, ReportData, "@p");
                        }
                        stream.Close();
                    }
                }
            }
        }
        private void InsertByteData(string strSQL, byte[] arrObj, string strParameterName)
        {
            strSQL = strSQL.ToString().Trim();
            try
            {
                SqlCommand InsertCommand = new SqlCommand();
                InsertCommand.Connection = Globals.SQLcon.Connection;
                InsertCommand.CommandText = strSQL;
                InsertCommand.CommandType = CommandType.Text;
                InsertCommand.Parameters.Clear();

                SqlParameter p = new SqlParameter(strParameterName, SqlDbType.Binary);
                p.Value = arrObj;
                InsertCommand.Parameters.Add(p);

                Globals.SQLcon.Open();
                InsertCommand.ExecuteNonQuery();
                InsertCommand.Dispose();
                Globals.SQLcon.Close();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        ///<summary>clsReportDocSettingAssignment / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();

            string strSql = string.Empty;
            strSql = "SELECT * " +
                              ", case when Report is null then 0 else 1 end as FileExist " +
                              "FROM ReportDocSettingAssignment " +
                                "WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReportDocSettingAssignment");

            //DataColumn col = new DataColumn();
            //col.ColumnName = "File";
            //col.DataType = typeof(bool);
            //dt.Columns.Add(col);

            FillClassByTable(dt);
        }
        ///<summary>clsReportDocSettingAssignment / FillbyAdr</summary>
        ///<remarks></remarks>
        public void FillbyAdr()
        {
            DataTable dt = new DataTable();
            //string strSql = sql_GetList;
            string strSql = "SELECT Top(1)* FROM ReportDocSettingAssignment " +
                                            "WHERE " +
                                                  "IsDefault=" + Convert.ToInt32(this.IsDefault) +
                                                  "AND DocKey='" + this.DocKey + "' " +
                                                  "AND AdrID=" + (Int32)this.AdrID + " " +
                                                  "AND MandantenID=" + (int)this.MandantenID + " " +
                                                  "AND AbBereichID=" + (Int32)this.AbBereichID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "RepoortDocSettingAssignment");

            //DataColumn col = new DataColumn();
            //col.ColumnName = "File";
            //col.DataType = typeof(bool);
            //dt.Columns.Add(col);

            FillClassByTable(dt);
        }
        ///<summary>clsReportDocSettingAssignment / FillClassByTable</summary>
        ///<remarks></remarks>
        private void FillClassByTable(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                this.ID = iTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["DocKeyID"].ToString(), out iTmp);
                this.DocKeyID = iTmp;
                this.DocKey = dt.Rows[i]["DocKey"].ToString();
                this.Path = dt.Rows[i]["Path"].ToString();
                this.ReportFileName = dt.Rows[i]["ReportFileName"].ToString();
                this.IsDefault = (bool)dt.Rows[i]["IsDefault"];
                decimal decTmp = 0;
                decimal.TryParse(dt.Rows[i]["AdrID"].ToString(), out decTmp);
                this.AdrID = decTmp;
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["AbBereichID"].ToString(), out decTmp);
                this.AbBereichID = decTmp;
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["MandantenID"].ToString(), out decTmp);
                this.MandantenID = decTmp;
                this.FileExtension = dt.Rows[i]["FileExtension"].ToString();

                if (dt.Rows[i]["Report"] is DBNull)
                {
                    this.ExistReportData = false;
                    this.ReportData = null;
                }
                else
                {
                    this.ExistReportData = true;
                    string strSql = "SELECT Report FROM ReportDocSettingAssignment  WHERE ID=" + this.ID;
                    object obj = clsSQLcon.ExecuteSQLWithTRANSACTIONGetObject(strSql, "Report", GLUser.User_ID);
                    this.ReportData = (byte[])obj;
                }
                if (dt.Columns.Contains("FileExist"))
                {
                    if (dt.Rows[i]["FileExist"] is DBNull)
                    {
                        dt.Rows[i]["FileExist"] = this.ExistReportData;
                    }
                }

            }
        }
        ///<summary>clsReportDocSettingAssignment / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {
            string strSql = string.Empty;
            strSql = "Update ReportDocSettingAssignment SET  " +
                            "DocKey ='" + this.DocKey + "' " +
                            ", Path ='" + this.Path + "' " +
                            ", ReportFileName='" + this.ReportFileName + "' " +
                            ", IsDefault=" + Convert.ToInt32(this.IsDefault) +
                            ", AdrID=" + (Int32)this.AdrID +
                            ", AbBereichID=" + (Int32)this.AbBereichID +
                            ", DocKeyID=" + this.DocKeyID +
                            ", MandantenID =" + (int)this.MandantenID +
                            ", FileExtension='" + this.FileExtension + "' " +
                            " WHERE ID=" + this.ID;
            if (clsSQLcon.ExecuteSQL(strSql, this.BenutzerID))
            {
                UpdateByteData(false);
            }
        }
        ///<summary>clsReportDocSettingAssignment / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE ReportDocSettingAssignment WHERE ID=" + this.ID;
            clsSQLcon.ExecuteSQL(strSql, this.BenutzerID);
        }


        private string sql_GetList
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT Top(1) *" +
                         ", case when Report is null then 0 else 1 end as FileExist " +
                         " FROM ReportDocSettingAssignment " +
                                                                "WHERE " +
                                                                    " DocKey='" + this.DocKey + "' " +
                                                                    " AND MandantenID=" + (int)this.MandantenID +
                                                                    " AND IsDefault=1 ";
                return this.sql_GetList;
            }
        }



        ///<summary>clsReportDocSettingAssignment / FillClsByDocKey</summary>
        ///<remarks>Füll nur diese Klasse mit den Werte aus der DB</remarks>
        public void FillClsByDocKey()
        {
            PrinterSettings ps = new PrinterSettings();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            //strSql = sql_GetList;
            strSql = "SELECT Top(1) * FROM ReportDocSettingAssignment " +
                                                        "WHERE " +
                                                            " DocKey='" + this.DocKey + "' " +
                                                            " AND MandantenID=" + (int)this.MandantenID +
                                                            " AND IsDefault=1 ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReportDocSettingAss");

            //DataColumn col = new DataColumn();
            //col.ColumnName = "File";
            //col.DataType = typeof(bool);
            //dt.Columns.Add(col);

            FillClassByTable(dt);
        }
        ///<summary>clsReportDocSettingAssignment / InitFillTable</summary>
        ///<remarks></remarks
        public void InitFillTable(int myRefAbBreicheId)
        {
            clsDocKey DocKeys = new clsDocKey();
            string strSql = string.Empty;

            foreach (var pair in DocKeys.DictDocKey)
            {
                clsReportDocSettingAssignment tmpRDSAss = new clsReportDocSettingAssignment();
                tmpRDSAss.InitClass(this.GLUser, this.GLSystem, this.Sys);
                tmpRDSAss.DocKey = pair.Value.ToString();
                tmpRDSAss.DocKeyID = pair.Key;
                tmpRDSAss.AbBereichID = this.Sys.AbBereich.ID;
                tmpRDSAss.MandantenID = this.Sys.AbBereich.MandantenID;

                if (tmpRDSAss.DocKey.Equals(enumIniDocKey.Bestandsliste))
                {
                    string str = string.Empty;
                }

                if (!tmpRDSAss.ExistDocKey)
                {
                    tmpRDSAss.Path = string.Empty;
                    tmpRDSAss.ReportFileName = string.Empty;
                    tmpRDSAss.IsDefault = true;
                    tmpRDSAss.AdrID = 0;

                    if (myRefAbBreicheId > 0)
                    {
                        clsArbeitsbereiche RefArbeitsbereich = new clsArbeitsbereiche();
                        RefArbeitsbereich.InitCls(this.GLUser, myRefAbBreicheId);
                        List<clsReportDocSettingAssignment> ListRefSource = GetListByArbeitsbereich((int)RefArbeitsbereich.ID);
                        if (ListRefSource.Count > 0)
                        {
                            clsReportDocSettingAssignment tmpSource = ListRefSource.FirstOrDefault(x => x.DocKeyID == tmpRDSAss.DocKeyID);
                            if (tmpSource is clsReportDocSettingAssignment)
                            {
                                if (!tmpSource.Path.Equals(this.Sys.AbBereich.Mandant.ReportPath))
                                {
                                    tmpRDSAss.Path = this.Sys.AbBereich.Mandant.ReportPath;
                                }
                                else
                                {
                                    tmpRDSAss.Path = tmpSource.Path;
                                }
                                tmpRDSAss.ReportFileName = tmpSource.ReportFileName;
                                tmpRDSAss.IsDefault = true;
                                //tmpRDSAss.AdrID = 0;
                            }
                        }
                    }
                    strSql = strSql + tmpRDSAss.AddSQL();
                }
                else
                {
                    //Update KeyDocID
                    tmpRDSAss.FillClsByDocKey();
                    //Value neu setzen
                    tmpRDSAss.DocKeyID = pair.Key;
                    if (tmpRDSAss.ID > 0)
                    {
                        tmpRDSAss.Update();
                    }
                }
            }
            if (!strSql.Equals(string.Empty))
            {
                clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ReportDocSetting", this.BenutzerID);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myRefAbBereichID"></param>
        /// <returns></returns>
        private List<clsReportDocSettingAssignment> GetListByArbeitsbereich(int myRefAbBereichID)
        {
            List<clsReportDocSettingAssignment> listReturn = new List<clsReportDocSettingAssignment>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ReportDocSettingAssignment WHERE AbBereichID=" + myRefAbBereichID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReportDocSettingAssignment");
            foreach (DataRow row in dt.Rows)
            {
                int iTmp = 0;
                int.TryParse(row["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    clsReportDocSettingAssignment tmpR = new clsReportDocSettingAssignment();
                    tmpR.InitClass(this.GLUser, this.GLSystem, this.Sys);
                    tmpR.ID = iTmp;
                    tmpR.Fill();

                    if (!listReturn.Exists(x => x.DocKeyID == tmpR.DocKeyID))
                    {
                        listReturn.Add(tmpR);
                    }
                }
            }
            return listReturn;
        }
        ///<summary>clsReportDoc / CheckForCustumizeDoc</summary>
        ///<remarks></remarks>
        private bool CheckForCustumizeDoc()
        {
            string strSql = string.Empty;
            strSql = "Select DISTINCT ID " +
                                     "FROM ReportDocSettingAssignment ass " +
                                     "INNER JOIN ReportDocSetting rds on rds.DocKey = ass.DocKey " +
                                     "WHERE " +
                                        "ass.IsDefault = 0 " +
                                        " AND rds.Art=" + this.Art +
                                        " AND ass.MandantenID =" + (int)+this.MandantenID +
                                        " AND ass.AbBereichID =" + (int)+this.AbBereichID +
                                        " AND ass.AdrID=" + (Int32)this.AdrID + " ";
            bool bResult = clsSQLcon.ExecuteSQL_GetValueBool(strSql, this.BenutzerID);

            return bResult;
        }


        private DataTable GetReportDocSettingAssignmentByWorkspace()
        {
            string strSql = string.Empty;
            strSql = "Select ass.*" +
                            "FROM ReportDocSettingAssignment ass " +
                            "INNER JOIN ReportDocSetting rds on rds.DocKey = ass.DocKey " +
                            "WHERE " +
                            "ass.IsDefault = 1 " +
                            " AND ass.MandantenID =" + (int)+this.MandantenID +
                            //" AND ass.AbBereichID =" + (int)+this.AbBereichID +
                            " Order by ass.DocKeyID " +
                            "; ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.BenutzerID, "ReportDocSettingAssignmentByWorkspace");
            return dt;
        }

        public void CreateDocSettingforWorkspace()
        {
            ////CHeck Existing DocSettings for Workspace
            //DataTable dtExistDocSettingAsignByWorkspace = GetReportDocSettingAssignmentByWorkspace();
            //if (dtExistDocSettingAsignByWorkspace.Rows.Count > 0)
            //{
            //    List<string> ListSQL = new List<string>();

            //    foreach (DataRow r in dtExistDocSettingAsignByWorkspace.Rows)
            //    {
            //        int iTmp = 0;
            //        int.TryParse(r["ID"].ToString(), out iTmp);
            //        if (iTmp > 0)
            //        {
            //            clsReportDocSettingAssignment rsaTmp = this.Copy();
            //            rsaTmp.ID = iTmp;
            //            rsaTmp.Fill();
            //            rsaTmp.MandantenID = this.MandantenID;

            //            rsaTmp.Add();
            //        }
            //    }
            //}
            //else
            //{
            //    // neu anlegen der Datensätze
            //    DataTable dtSource = clsReportDocSetting.GetReportSettings(this.GLUser, myRange, this.SelArbeitsbereich);

            //}
        }


        public clsReportDocSettingAssignment Copy()
        {
            return (clsReportDocSettingAssignment)this.MemberwiseClone();
        }

    }
}
