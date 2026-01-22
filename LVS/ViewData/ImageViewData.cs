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
    public class ImageViewData
    {
        public List<Images> ListImageData { get; set; } = new List<Images>();
        public Images Image { get; set; }
        //internal Globals._GL_SYSTEM _GLSystem;
        internal Globals._GL_USER _GL_User;
        //internal clsSystem Sys { get; set; }
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

        public ImageViewData()
        {
            _GL_User = new Globals._GL_USER();
            InitCls();
        }
        public ImageViewData(Globals._GL_USER myGLUser)
        {
            _GL_User = myGLUser;
            InitCls();
        }
        public ImageViewData(Globals._GL_USER myGLUser, int Id)
        {
            _GL_User = myGLUser;
            InitCls();
            Image.Id = Id;
            if (Image.Id > 0)
            {
                Fill();
            }
        }

        public ImageViewData(int myUserId, Images myImage)
        {
            _GL_User.User_ID = myUserId;
            Image = myImage.Copy();
            if (Image.Id > 0)
            {
                Fill();
            }
        }
        public string Errortext { get; set; }


        public void InitCls()
        {
            Image = new Images();
            Image.AuftragTableID = 0;
            Image.LEingangTableID = 0;
            Image.LAusgangTableID = 0;
            Image.Pfad = string.Empty;
            Image.AuftragPosTableID = 0;

            Image.DocImage = null;
            Image.TableId = 0;
            Image.TableName = string.Empty;
            Image.ScanFilename = string.Empty;
            Image.ImageArt = string.Empty;
            Image.Thumbnail = null;
            Image.IsForSPLMessage = false;
            Image.ImageArt = string.Empty;
            Image.WorkspaceId = 0;
        }

        public void Add()
        {
            try
            {
                Image.PicNum = GetMaxPicNum();

                clsSQLARCHIVE sqlArchive = new clsSQLARCHIVE();
                sqlArchive.init();

                SqlCommand InsertCommand = new SqlCommand();
                //InsertCommand.Connection = Globals.SQLconArchive.Connection;
                InsertCommand.Connection = sqlArchive.Connection;
                InsertCommand.CommandText = sql_Add;
                InsertCommand.CommandType = CommandType.Text;
                InsertCommand.Parameters.Clear();

                InsertCommand.Parameters.AddWithValue("@AuftragTableID", Image.AuftragTableID);
                InsertCommand.Parameters.AddWithValue("@LEingangTableID", Image.LEingangTableID);
                InsertCommand.Parameters.AddWithValue("@LAusgangTableID", Image.LAusgangTableID);
                InsertCommand.Parameters.AddWithValue("@Pfad", Image.Pfad);
                InsertCommand.Parameters.AddWithValue("@ScanFilename", Image.ScanFilename);
                InsertCommand.Parameters.AddWithValue("@PicNum", Image.PicNum);
                InsertCommand.Parameters.AddWithValue("@ImageArt", Image.ImageArt);
                InsertCommand.Parameters.AddWithValue("@AuftragPosTableID", Image.AuftragPosTableID);

                if ((Image.DocImage != null) && (Image.DocImage.Length > 0))
                {
                    //InsertCommand.Parameters.AddWithValue("@DocImage", Image.DocImage);
                    InsertCommand.Parameters.Add(helper_Image.CreateByteParameter("@DocImage", Image.DocImage));
                }

                InsertCommand.Parameters.AddWithValue("@TableName", Image.TableName);
                InsertCommand.Parameters.AddWithValue("@TableID", Image.TableId);


                if ((Image.Thumbnail != null) && (Image.Thumbnail.Length > 0))
                {
                    //InsertCommand.Parameters.AddWithValue("@Thumbnail", Image.Thumbnail);
                    InsertCommand.Parameters.Add(helper_Image.CreateByteParameter("@Thumbnail", Image.Thumbnail));
                }
                InsertCommand.Parameters.AddWithValue("@IsForSPLMessage", Image.IsForSPLMessage);
                Image.Created = DateTime.Now;
                InsertCommand.Parameters.AddWithValue("@Created", Image.Created);
                InsertCommand.Parameters.AddWithValue("@WorkspaceId", Image.WorkspaceId);

                //Globals.SQLconArchive.Open();
                sqlArchive.Open();
                //InsertCommand.ExecuteNonQuery();
                var res = InsertCommand.ExecuteScalar();
                if (res != null)
                {
                    int iTmp = 0;
                    int.TryParse(res.ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        this.Image.Id = iTmp;
                    }
                }
                InsertCommand.Dispose();
                sqlArchive.Close();
                //Globals.SQLconArchive.Close();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public void Delete()
        {
            try
            {
                string strSQL = "DELETE FROM Images WHERE Id=" + Image.Id + ";";
                bool bOK = clsSQLARCHIVE.ExecuteSQL(strSQL, BenutzerID);
                if (bOK)
                {
                    //Add Logbucheintrag Eintrag
                    string Beschreibung = "Dokument: ID " + Image.Id + "/ [Table/ID]: " + Image.TableName + "/" + Image.TableId + " wurde gelöscht!";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

                    switch (Image.TableName)
                    {
                        case "Artikel":
                            clsArtikelVita.DeleteImageFromArtikel(this._GL_User, Image.TableId, enumLagerAktionen.ImageToArtikel.ToString(), Image.ScanFilename);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private Int32 GetMaxPicNum()
        {
            Int32 myIMaxPicNum = 0;

            string strSql = string.Empty;
            strSql = "Select MAX(PicNum) FROM Images " +
                                         "WHERE " +
                                            "TableName='" + Image.TableName + "' AND " +
                                            "TableID=" + Image.TableId + ";";

            string myStrReturn = clsSQLARCHIVE.ExecuteSQL_GetValue(strSql, BenutzerID);
            if (myStrReturn != String.Empty)
            {
                if (!Int32.TryParse(myStrReturn, out myIMaxPicNum))
                {
                    myIMaxPicNum = 0;
                }
            }
            else
            {
                myIMaxPicNum = 0;
            }
            return myIMaxPicNum + 1;
        }
        public bool UpdateIsForSPLMessage(bool mySelect)
        {
            string strSql = string.Empty;
            strSql = "Update Images SET IsForSPLMessage=" + Convert.ToInt32(mySelect) +
                                " WHERE ID =" + Image.Id + ";";
            return clsSQLARCHIVE.ExecuteSQL(strSql, BenutzerID);
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
                strSql = "INSERT INTO Images ";
                strSql += "(";

                strSql += " AuftragTableID";
                strSql += ", LEingangTableID";
                strSql += ", LAusgangTableID";
                strSql += ", Pfad";
                strSql += ", ScanFilename";
                strSql += ", PicNum";
                strSql += ", ImageArt";
                strSql += ", AuftragPosTableID";
                strSql += ", DocImage";
                strSql += ", TableName";
                strSql += ", TableID";
                strSql += ", Thumbnail";
                strSql += ", IsForSPLMessage";
                strSql += ", Created";
                strSql += ", WorkspaceId";

                strSql += ")";
                strSql += " VALUES ";
                strSql += "(";

                strSql += "@AuftragTableID, ";
                strSql += "@LEingangTableID, ";
                strSql += "@LAusgangTableID, ";
                strSql += "@Pfad, ";
                strSql += "@ScanFilename, ";
                strSql += "@PicNum, ";
                strSql += "@ImageArt, ";
                strSql += "@AuftragPosTableID, ";
                strSql += "@DocImage, ";
                strSql += "@TableName, ";
                strSql += "@TableID, ";
                strSql += "@Thumbnail, ";
                strSql += "@IsForSPLMessage, ";
                strSql += "@Created, ";
                strSql += "@WorkspaceId ";

                strSql += ") ";
                strSql += " Select @@IDENTITY as 'Id' ;";
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
                string strSql = "Update Images SET ";
                //strSql += " Tab =" + Archive.TableName;
                //strSql += ", TableId =" + Archive.TableId;
                //strSql += ", FileArt =" + (int)Archive.FileArt;
                //strSql += ", Extension =" + Archive.Extension;
                //strSql += ", UserId =" + Archive.UserId;
                //strSql += ", Description =" + Archive.Description;
                //strSql += ", ReportDocSettingAssignmentId =" + Archive.ReportDocSettingAssignmentId;
                //strSql += ", ReportDocSettingId =" + Archive.ReportDocSettingId;
                //strSql += ", DocKey =" + Archive.DocKey;
                //strSql += ", WorkspaceId =" + Archive.WorkspaceId;
                //strSql += ", DocKeyID =" + Archive.DocKeyID;

                //strSql += " WHERE Id=" + Archive.Id;
                return strSql;
            }
        }

        public string sql_Fill
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Images WHERE Id= " + Image.Id;
                return strSql;
            }
        }
        public string sql_FillByCreated
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Images WHERE Created= '" + Image.Created.ToString() + "';";
                return strSql;
            }
        }
        public string sql_GetImageDataById
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select DocImage FROM Images WHERE Id= " + Image.Id;
                return strSql;
            }
        }
        public string sql_GetThumbnailById
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select Thumbnail FROM Images WHERE Id= " + Image.Id;
                return strSql;
            }
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             Function / Procedure
        ///-----------------------------------------------------------------------------------------------------

        private void SetValue(DataRow myRow)
        {
            Image = new Images();

            int iTmp = 0;
            int.TryParse(myRow["Id"].ToString(), out iTmp);
            this.Image.Id = iTmp;

            iTmp = 0;
            int.TryParse(myRow["AuftragTableID"].ToString(), out iTmp);
            this.Image.AuftragTableID = iTmp;

            iTmp = 0;
            int.TryParse(myRow["LEingangTableID"].ToString(), out iTmp);
            this.Image.LEingangTableID = iTmp;

            iTmp = 0;
            int.TryParse(myRow["LAusgangTableID"].ToString(), out iTmp);
            this.Image.LAusgangTableID = iTmp;

            this.Image.Pfad = myRow["Pfad"].ToString();
            this.Image.ScanFilename = myRow["ScanFilename"].ToString();

            iTmp = 0;
            int.TryParse(myRow["PicNum"].ToString(), out iTmp);
            this.Image.PicNum = iTmp;

            this.Image.TableName = myRow["TableName"].ToString();

            iTmp = 0;
            int.TryParse(myRow["TableID"].ToString(), out iTmp);
            this.Image.TableId = iTmp;
            this.Image.ImageArt = myRow["ImageArt"].ToString();

            if (myRow["DocImage"] != System.DBNull.Value)
            {
                object obj = clsSQLARCHIVE.ExecuteSQLWithTRANSACTIONGetObject(sql_GetImageDataById, "DocScan", _GL_User.User_ID);
                this.Image.DocImage = (byte[])obj;
            }
            if (myRow["Thumbnail"] != System.DBNull.Value)
            {
                object obj = clsSQLARCHIVE.ExecuteSQLWithTRANSACTIONGetObject(sql_GetThumbnailById, "DocScan", _GL_User.User_ID);
                this.Image.Thumbnail = (byte[])obj;
            }

            this.Image.IsForSPLMessage = (bool)myRow["IsForSPLMessage"];

            DateTime dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(myRow["Created"].ToString(), out dtTmp);
            this.Image.Created = dtTmp;
            iTmp = 0;
            int.TryParse(myRow["WorkspaceId"].ToString(), out iTmp);
            this.Image.WorkspaceId = iTmp;
        }
        public void Fill()
        {
            string strSql = string.Empty;
            strSql = this.sql_Fill;
            System.Data.DataTable dt = clsSQLARCHIVE.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Image");
            foreach (DataRow dr in dt.Rows)
            {
                SetValue(dr);
            }
        }
        public void FillByCreated()
        {
            string strSql = string.Empty;
            strSql = this.sql_FillByCreated;
            System.Data.DataTable dt = clsSQLARCHIVE.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Image");
            foreach (DataRow dr in dt.Rows)
            {
                SetValue(dr);
            }
        }
        /// <summary>
        /// 
        /// </summary>

        public static DataTable GetImages(Globals._GL_USER myGLUser, string myTableName, decimal myTableID, decimal myArbeitsbereichId)
        {
            string strSQL = "Select * FROM Images ";
            strSQL += "WHERE ";
            strSQL += "TableName='" + myTableName + "' ";
            strSQL += "AND ";
            strSQL += "TableID=" + (Int32)myTableID + " ";
            strSQL += "AND ";
            strSQL += "WorkspaceId=" + myArbeitsbereichId + " ";

            DataTable dt = clsSQLARCHIVE.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Images");

            return dt;
        }

        public static List<ImageView> GetSearchValueList(Globals._GL_USER myGLUser, int myWorkspaceId, int SearchValue, string mySearchField)
        {
            ArticleViewData art = new ArticleViewData();
            EingangViewData ein = new EingangViewData();
            AusgangViewData aus = new AusgangViewData();

            List<ImageView> retList = new List<ImageView>();
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM Images WHERE ";
            switch (mySearchField)
            {
                case ArchiveViewData.const_Datafield_LvsID:
                    art = new ArticleViewData(SearchValue, (int)myGLUser.User_ID, myWorkspaceId);
                    ein = new EingangViewData(art.Artikel.LEingangTableID, (int)myGLUser.User_ID, false);

                    strSQL += "(TableName = '" + enumDatabaseSped4_TableNames.Artikel + "' and TableID=" + art.Artikel.Id + ") ";
                    strSQL += " OR ";
                    strSQL += "(TableName = '" + enumDatabaseSped4_TableNames.LEingang + "' and TableID=" + art.Artikel.LEingangTableID + ") ";

                    if (art.Artikel.LAusgangTableID > 0)
                    {
                        aus = new AusgangViewData(art.Artikel.LAusgangTableID, (int)myGLUser.User_ID, false);
                        strSQL += " OR ";
                        strSQL += "(TableName = '" + enumDatabaseSped4_TableNames.LAusgang + "' and TableID=" + art.Artikel.LAusgangTableID + ") ";
                    }
                    break;
                case ArchiveViewData.const_Datafield_EingangID:
                    ein = new EingangViewData(SearchValue, (int)myGLUser.User_ID, myWorkspaceId, true);
                    if (ein.Eingang.LEingangID == SearchValue)
                    {
                        strSQL += "(TableName = '" + enumDatabaseSped4_TableNames.LEingang + "' and TableID=" + ein.Eingang.Id + ") ";
                        //---- Suche nach entsprechenden Artikel
                        if (ein.ListArticleInEingang.Count > 0)
                        {
                            foreach (var item in ein.ListArticleInEingang)
                            {
                                strSQL += "OR ";
                                strSQL += "(TableName = '" + enumDatabaseSped4_TableNames.Artikel + "' and TableId = " + item.Id + ") ";
                            }
                        }
                    }
                    else
                    {
                        strSQL = string.Empty;
                    }

                    break;
                case ArchiveViewData.const_Datafield_AusgangID:
                    aus = new AusgangViewData(SearchValue, (int)myGLUser.User_ID, myWorkspaceId, true);
                    if (aus.Ausgang.LAusgangID == SearchValue)
                    {
                        strSQL += "(TableName = '" + enumDatabaseSped4_TableNames.LAusgang + "' and TableID=" + aus.Ausgang.Id + ") ";

                        if (aus.ListArticleInAusgang.Count > 0)
                        {
                            foreach (var item in aus.ListArticleInAusgang)
                            {
                                strSQL += "OR ";
                                strSQL += "(TableName = '" + enumDatabaseSped4_TableNames.Artikel + "' and TableId = " + item.Id + ") ";
                            }
                        }
                    }
                    else
                    {
                        strSQL = string.Empty;
                    }

                    break;
                default:
                    strSQL = string.Empty;
                    break;
            }

            DataTable dt = clsSQLARCHIVE.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Images", "Images", myGLUser.User_ID);
            foreach (DataRow dr in dt.Rows)
            {
                int iTmp = 0;
                int.TryParse(dr["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    ImageViewData dsViewData = new ImageViewData(myGLUser, iTmp);
                    ImageView tmpView = new ImageView();
                    tmpView.Image = dsViewData.Image.Copy();
                    switch (mySearchField)
                    {
                        case ArchiveViewData.const_Datafield_LvsID:
                            tmpView.LVSNr = art.Artikel.LVS_ID;
                            tmpView.LEingangID = ein.Eingang.LEingangID;
                            if (art.Artikel.LAusgangTableID > 0)
                            {
                                tmpView.LAusgangID = aus.Ausgang.LAusgangID;
                            }
                            break;
                        case ArchiveViewData.const_Datafield_EingangID:
                            tmpView.LEingangID = ein.Eingang.LEingangID;
                            break;
                        case ArchiveViewData.const_Datafield_AusgangID:
                            tmpView.LAusgangID = aus.Ausgang.LAusgangID;
                            break;
                    }
                    retList.Add(tmpView);
                }
            }
            return retList;
        }

        public static DataTable GetRowsWithoutWorkspaceId()
        {
            string strSQL = "Select Id, TableName, TableId FROM Images ";
            strSQL += "WHERE ";
            strSQL += "WorkspaceId=0";
            DataTable dt = clsSQLARCHIVE.ExecuteSQL_GetDataTable(strSQL, 1, "GetRowsWithoutWorkspaceId");
            return dt;
        }

    }
}
