using Common.Enumerations;
using Common.Models;
using Common.Views;
using LVS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LVS.ViewData
{
    public class ArchiveViewData
    {
        public const string const_Datafield_NotSet = "NOT-Set";
        public const string const_Datafield_LvsID = "LVS-ID";
        public const string const_Datafield_EingangID = "Eingang-ID";
        public const string const_Datafield_AusgangID = "Ausgang-ID";
        public const string const_Datafield_RGId = "RG-ID";

        public List<ArchiveView> ListSearchArchiveData { get; set; } = new List<ArchiveView>();

        internal Globals._GL_SYSTEM _GLSystem;
        internal Globals._GL_USER _GL_User;
        internal clsSystem Sys { get; set; }
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }

        }

        public ArchiveViewData()
        {
            _GL_User = new Globals._GL_USER();
            _GLSystem = new Globals._GL_SYSTEM();
            Sys = new clsSystem();
            Sys.Client = new clsClient();
            Sys.Client.InitClass(clsClient.const_ClientMatchcode_SZG + "_");
            Archive = new Archives();
        }
        public ArchiveViewData(Globals._GL_SYSTEM myGLSystem, Globals._GL_USER myGLUser, clsSystem mySystem)
        {
            _GL_User = myGLUser;
            _GLSystem = myGLSystem;
            Sys = mySystem;
            Archive = new Archives();
        }

        public Archives Archive { get; set; } = new Archives();
        internal string Filepath { get; set; } = string.Empty;
        //internal clsImages Image { get; set; }

        public string Errortext { get; set; }


        public void Add()
        {
            try
            {
                SqlCommand InsertCommand = new SqlCommand();
                InsertCommand.Connection = Globals.SQLconArchive.Connection;
                InsertCommand.CommandText = sql_Add;
                InsertCommand.CommandType = CommandType.Text;
                InsertCommand.Parameters.Clear();

                InsertCommand.Parameters.AddWithValue("@TableName", Archive.TableName);
                InsertCommand.Parameters.AddWithValue("@TableId", Archive.TableId);
                InsertCommand.Parameters.AddWithValue("@FileArt", Archive.FileArt);
                if ((Archive.FileData != null) && (Archive.FileData.Length > 0))
                {
                    InsertCommand.Parameters.Add(helper_Image.CreateByteParameter("@FileData", Archive.FileData));
                }
                InsertCommand.Parameters.AddWithValue("@Extension", Archive.Extension);
                InsertCommand.Parameters.AddWithValue("@Filename", Archive.Filename);
                InsertCommand.Parameters.AddWithValue("@Created", DateTime.Now);
                InsertCommand.Parameters.AddWithValue("@UserId", Archive.UserId);
                InsertCommand.Parameters.AddWithValue("@Description", Archive.Description);
                InsertCommand.Parameters.AddWithValue("@ReportDocSettingAssignmentId", Archive.ReportDocSettingAssignmentId);
                InsertCommand.Parameters.AddWithValue("@ReportDocSettingId", Archive.ReportDocSettingId);
                InsertCommand.Parameters.AddWithValue("@DocKey", Archive.DocKey);
                InsertCommand.Parameters.AddWithValue("@WorkspaceId", Archive.WorkspaceId);
                InsertCommand.Parameters.AddWithValue("@DocKeyID", Archive.DocKeyID);

                Globals.SQLconArchive.Open();
                //InsertCommand.ExecuteNonQuery();
                object obj = InsertCommand.ExecuteScalar();
                if (obj != null)
                {
                    int iId = 0;
                    int.TryParse(obj.ToString(), out iId);
                    Archive.Id = iId;
                }
                InsertCommand.Dispose();
                Globals.SQLconArchive.Close();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public void Delete()
        {
        }


        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO Archive ";
                strSql += "(";
                strSql += " TableName, TableId, FileArt, FileData, Extension, [Filename], Created, UserId, [Description],";
                strSql += " ReportDocSettingAssignmentId, ReportDocSettingId, DocKey, WorkspaceId, DocKeyID";
                strSql += ")";
                strSql += " VALUES ";
                strSql += "(";

                strSql += "@TableName, ";
                strSql += "@TableId, ";
                strSql += "@FileArt, ";
                strSql += "@FileData, ";
                strSql += "@Extension, ";
                strSql += "@Filename, ";
                strSql += "@Created, ";
                strSql += "@UserId, ";
                strSql += "@Description, ";
                strSql += "@ReportDocSettingAssignmentId, ";
                strSql += "@ReportDocSettingId, ";
                strSql += "@DocKey, ";
                strSql += "@WorkspaceId, ";
                strSql += "@DocKeyID ";

                strSql += ") ";
                strSql += " Select @@IDENTITY as 'ID' ;";
                return strSql;
            }
        }
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update
        {
            get
            {
                string strSql = "Update Archive SET ";
                strSql += " Tab =" + Archive.TableName;
                strSql += ", TableId =" + Archive.TableId;
                strSql += ", FileArt =" + (int)Archive.FileArt;
                strSql += ", Extension =" + Archive.Extension;
                strSql += ", UserId =" + Archive.UserId;
                strSql += ", Description =" + Archive.Description;
                strSql += ", ReportDocSettingAssignmentId =" + Archive.ReportDocSettingAssignmentId;
                strSql += ", ReportDocSettingId =" + Archive.ReportDocSettingId;
                strSql += ", DocKey =" + Archive.DocKey;
                strSql += ", WorkspaceId =" + Archive.WorkspaceId;
                strSql += ", DocKeyID =" + Archive.DocKeyID;

                strSql += " WHERE Id=" + Archive.Id;
                return strSql;
            }
        }

        public string sql_Fill
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Archive WHERE Id= " + Archive.Id;
                return strSql;
            }
        }
        public string sql_GetFileDataById
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select FileData FROM Archive WHERE Id= " + Archive.Id;
                return strSql;
            }
        }

        public string sql_ExistData
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select Id FROM Archive " +
                                    "WHERE TableName = '" + Archive.TableName + "'" +
                                    "and TableId = " + Archive.TableId +
                                    "and FileArt = " + (int)Archive.FileArt +
                                    "and ReportDocSettingId = " + Archive.ReportDocSettingId;
                return strSql;
            }
        }

        public string sql_Main
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Archive ";
                return strSql;
            }
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             Function / Procedure
        ///-----------------------------------------------------------------------------------------------------

        private void SetValue(DataRow myRow)
        {
            Archive = new Archives();
            int iTmp = 0;
            int.TryParse(myRow["Id"].ToString(), out iTmp);
            this.Archive.Id = iTmp;
            this.Archive.TableName = myRow["TableName"].ToString();
            iTmp = 0;
            int.TryParse(myRow["TableId"].ToString(), out iTmp);
            this.Archive.TableId = iTmp;

            iTmp = 0;
            int.TryParse(myRow["FileArt"].ToString(), out iTmp);
            enumFileArt tmpFileArt = enumFileArt.NotSet;
            tmpFileArt = (enumFileArt)iTmp;
            this.Archive.FileArt = tmpFileArt;

            this.Archive.Extension = myRow["Extension"].ToString();
            this.Archive.Filename = myRow["Filename"].ToString();

            DateTime tmpDT = new DateTime(1900, 1, 1);
            DateTime.TryParse(myRow["Created"].ToString(), out tmpDT);

            iTmp = 0;
            int.TryParse(myRow["UserId"].ToString(), out iTmp);
            this.Archive.UserId = iTmp;
            this.Archive.Description = myRow["Description"].ToString();

            iTmp = 0;
            int.TryParse(myRow["ReportDocSettingAssignmentId"].ToString(), out iTmp);
            this.Archive.ReportDocSettingAssignmentId = iTmp;

            iTmp = 0;
            int.TryParse(myRow["ReportDocSettingId"].ToString(), out iTmp);
            this.Archive.ReportDocSettingId = iTmp;

            this.Archive.DocKey = myRow["DocKey"].ToString();

            iTmp = 0;
            int.TryParse(myRow["DocKeyID"].ToString(), out iTmp);
            this.Archive.DocKeyID = iTmp;

            if (myRow["FileData"] != System.DBNull.Value)
            {
                object obj = clsSQLARCHIVE.ExecuteSQLWithTRANSACTIONGetObject(sql_GetFileDataById, "Archiv", _GL_User.User_ID);
                this.Archive.FileData = (byte[])obj;
            }
        }

        public void Fill()
        {
            string strSql = string.Empty;
            strSql = this.sql_Fill;
            System.Data.DataTable dt = clsSQLARCHIVE.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Archiv");
            foreach (DataRow dr in dt.Rows)
            {
                SetValue(dr);
            }
        }

        public bool ExistArchiveData()
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = this.sql_ExistData;
            bReturn = clsSQLARCHIVE.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return bReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        public void GetList(string mySearchField, int mySearchValue)
        {
            string strSql = string.Empty;

            strSql = this.sql_Main;
            strSql += "WHERE ";

            switch (mySearchField)
            {
                case ArchiveViewData.const_Datafield_LvsID:
                    ArticleViewData artViewData = new ArticleViewData(mySearchValue, (int)BenutzerID, (int)this.Sys.AbBereich.ID);
                    if ((artViewData.Artikel.Id > 0) && (artViewData.Artikel.LVS_ID == mySearchValue))
                    {
                        strSql += "(TableName = '" + enumDatabaseSped4_TableNames.Artikel + "' and TableId = " + artViewData.Artikel.Id + ") ";
                        strSql += "OR ";
                        strSql += "(TableName = '" + enumDatabaseSped4_TableNames.LEingang + "' and TableId = " + artViewData.Artikel.LEingangTableID + ") ";

                        if (artViewData.Artikel.LAusgangTableID > 0)
                        {
                            strSql += "OR ";
                            strSql += "(TableName = '" + enumDatabaseSped4_TableNames.LAusgang + "' and TableId = " + artViewData.Artikel.LAusgangTableID + ") ";
                        }
                    }
                    else
                    {
                        strSql = string.Empty;
                    }
                    break;
                case ArchiveViewData.const_Datafield_EingangID:
                    EingangViewData einViewData = new EingangViewData(mySearchValue, (int)BenutzerID, (int)this.Sys.AbBereich.ID, true);
                    if ((einViewData.Eingang.Id > 0) && (einViewData.Eingang.LEingangID == mySearchValue))
                    {
                        strSql += "(TableName = '" + enumDatabaseSped4_TableNames.LEingang + "' and TableId = " + einViewData.Eingang.Id + ") ";

                        //---- Suche nach entsprechenden Artikel
                        if (einViewData.ListArticleInEingang.Count > 0)
                        {
                            foreach (var item in einViewData.ListArticleInEingang)
                            {
                                strSql += "OR ";
                                strSql += "(TableName = '" + enumDatabaseSped4_TableNames.Artikel + "' and TableId = " + item.Id + ") ";
                            }
                        }
                    }
                    else
                    {
                        strSql = string.Empty;
                    }
                    break;
                case ArchiveViewData.const_Datafield_AusgangID:
                    AusgangViewData ausViewData = new AusgangViewData(mySearchValue, (int)BenutzerID, (int)this.Sys.AbBereich.ID, true);
                    if ((ausViewData.Ausgang.Id > 0) && (ausViewData.Ausgang.LAusgangID == mySearchValue))
                    {
                        strSql += "(TableName = '" + enumDatabaseSped4_TableNames.LAusgang + "' and TableId = " + ausViewData.Ausgang.Id + ") ";
                        if (ausViewData.ListArticleInAusgang.Count > 0)
                        {
                            foreach (var item in ausViewData.ListArticleInAusgang)
                            {
                                strSql += "OR ";
                                strSql += "(TableName = '" + enumDatabaseSped4_TableNames.Artikel + "' and TableId = " + item.Id + ") ";
                            }
                        }
                    }
                    else
                    {
                        strSql = string.Empty;
                    }
                    break;
                case ArchiveViewData.const_Datafield_RGId:
                    int iRgId = 0;
                    int.TryParse(mySearchValue.ToString(), out iRgId);
                    InvoiceViewData iVD = new InvoiceViewData(iRgId, (int)BenutzerID, true);
                    if ((iVD.Invoice.Id > 0) && (iVD.Invoice.InvoiceNo == mySearchValue))
                    {
                        strSql += "(TableName = '" + enumDatabaseSped4_TableNames.Rechnungen + "' and TableId = " + iVD.Invoice.Id + ") ";
                    }
                    else
                    {
                        strSql = string.Empty;
                    }
                    break;
                default:
                    strSql = string.Empty;
                    break;
            }

            if (!strSql.Equals(string.Empty))
            {
                ListSearchArchiveData = new List<ArchiveView>();
                System.Data.DataTable dt = clsSQLARCHIVE.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ArchiveSearch");
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    ArticleViewData art = new ArticleViewData();
                    EingangViewData ein = new EingangViewData();
                    AusgangViewData aus = new AusgangViewData();


                    if (this.Archive.Id > 0)
                    {
                        ArchiveView tmpView = new ArchiveView();
                        tmpView.Archive = this.Archive;

                        enumDatabaseSped4_TableNames tmpTableName = enumDatabaseSped4_TableNames.NotSet;
                        Enum.TryParse(tmpView.Archive.TableName, out tmpTableName);

                        switch (tmpTableName)
                        {
                            case enumDatabaseSped4_TableNames.Artikel:
                                art = new ArticleViewData(this.Archive.TableId, _GL_User);
                                tmpView.LVSNr = art.Artikel.LVS_ID;

                                art = new ArticleViewData(this.Archive.TableId, _GL_User);
                                tmpView.LVSNr = art.Artikel.LVS_ID;
                                break;
                            case enumDatabaseSped4_TableNames.LAusgang:
                                aus = new AusgangViewData(art.Artikel.LAusgangTableID, (int)BenutzerID, false);
                                tmpView.LAusgangID = aus.Ausgang.LAusgangID;
                                break;

                            case enumDatabaseSped4_TableNames.LEingang:
                                ein = new EingangViewData(art.Artikel.LEingangTableID, (int)BenutzerID, false);
                                tmpView.LEingangID = ein.Eingang.LEingangID;
                                break;

                            case enumDatabaseSped4_TableNames.Rechnungen:
                                break;

                            case enumDatabaseSped4_TableNames.NotSet:
                                break;
                        }
                        ListSearchArchiveData.Add(tmpView);
                    }
                }
            }
        }


    }
}
