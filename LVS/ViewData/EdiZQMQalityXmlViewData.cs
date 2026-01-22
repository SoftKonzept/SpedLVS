using LVS.Communicator.ZQM;
using LVS.Constants;
using LVS.Models;
using LVS.sqlStatementCreater;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class EdiZQMQalityXmlViewData
    {
        public WorkspaceViewData workspaceVD { get; set; }
        public EdiZQMQalityXml ediZQMQalityXml { get; set; }
        public List<EdiZQMQalityXml> ListZQMQalityActive { get; set; }
        private int BenutzerID { get; set; }
        public bool Exist
        {
            get
            {
                bool bReturn = false;
                if (ediZQMQalityXml != null)
                {
                    sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml(ediZQMQalityXml);
                    string strSql = sql.sql_Exist();
                    var ExistId = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetValue(strSql, "TableExist", (decimal)BenutzerID);
                    int iTmp = 0;
                    int.TryParse(ExistId.ToString(), out iTmp);

                    if (iTmp > 0)
                    {
                        ediZQMQalityXml.Description = "Zertifikat existiert bereits!" + Environment.NewLine;
                        ediZQMQalityXml.Description += "Id [" + iTmp + "]";

                        //EdiZQMQalityXmlViewData tmpVD = new EdiZQMQalityXmlViewData(iTmp);
                        //if ((tmpVD.ediZQMQalityXml.Id > 0) && (tmpVD.ediZQMQalityXml.Id == iTmp))
                        //{
                        //    ediZQMQalityXml.Description = "Zertifikat existiert bereits!" + Environment.NewLine;
                        //    ediZQMQalityXml.Description +="Id [" + tmpVD.ediZQMQalityXml.Id.ToString() + "]";
                        //    //ediZQMQalityXml.Description += " | Produktionsnummer [" + tmpVD.ediZQMQalityXml.Produktionsnummer + "]";
                        //    //ediZQMQalityXml.Description += " | LfsNr [" + tmpVD.ediZQMQalityXml.LfsNr + "]";
                        //}
                    }
                    bReturn = (iTmp > 0);
                }
                return bReturn;
            }

        }

        public EdiZQMQalityXmlViewData()
        {
            InitCls();
        }
        public EdiZQMQalityXmlViewData(int myEdiZQMQalityId) : this()
        {
            InitCls();
            if (myEdiZQMQalityId > 0)
            {
                myEdiZQMQalityId = ediZQMQalityXml.Id = myEdiZQMQalityId;
                Fill();
            }
        }
        public EdiZQMQalityXmlViewData(EdiZQMQalityXml myEdiZQMQalityXml) : this()
        {
            this.ediZQMQalityXml = myEdiZQMQalityXml;

        }
        public EdiZQMQalityXmlViewData(string myFilePath) : this()
        {
            this.ediZQMQalityXml = null;
            if (File.Exists(myFilePath))
            {
                var tmp = ReadEdiZQMQalityXmlByFilePath(myFilePath);
                if (tmp is EdiZQMQalityXml)
                {
                    this.ediZQMQalityXml = tmp.Copy();
                }
            }
        }
        public EdiZQMQalityXml ReadEdiZQMQalityXmlByFilePath(string myFilePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(myFilePath);
            ZQM_Quality02 zqm = new ZQM_Quality02(doc);
            //EdiZQMQalityXml ediZqm = ediZqm = InitEdiZQMQalityXml(zqm, myFilePath);
            EdiZQMQalityXml ediZqm = InitEdiZQMQalityXml(zqm, myFilePath);
            return ediZqm;
        }

        public EdiZQMQalityXml ReadEdiZQMQalityXmlByIdocXmlString(EdiZQMQalityXml myEdiZqm)
        {
            EdiZQMQalityXml ediZqm = null;
            if (myEdiZqm.iDocXml.Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(myEdiZqm.iDocXml);
                ZQM_Quality02 zqm = new ZQM_Quality02(doc);

                ediZqm = InitEdiZQMQalityXml(zqm, Path.Combine(myEdiZqm.Path, myEdiZqm.FileName));
            }
            return ediZqm;
        }

        public EdiZQMQalityXml InitEdiZQMQalityXml(ZQM_Quality02 myZqm, string myFilePath)
        {
            EdiZQMQalityXml ediZqm = null;
            if (myZqm is ZQM_Quality02)
            {
                ediZqm = new EdiZQMQalityXml();
                ediZqm.iDocNo = myZqm.iDocNo;
                ediZqm.iDocDate = myZqm.iDocDate;
                ediZqm.Path = System.IO.Path.GetDirectoryName(myFilePath);
                ediZqm.FileName = System.IO.Path.GetFileName(myFilePath);
                ediZqm.WorkspaceId = AddressReferenceViewData.GETWorkspaceBySupplierReference(myZqm.iRCVPRN, constValue_AsnArt.const_Art_XML_ZQM_QALITY02, 1);
                ediZqm.IsActive = true;
                ediZqm.ArticleId = 0;
                ediZqm.iDocXml = myZqm.iDocXml;
                ediZqm.Produktionsnummer = myZqm.Produktionsnummer;
                ediZqm.LfsNr = myZqm.LfsNo;
                ediZqm.Description = "RCVPRN =" + myZqm.iRCVPRN + " " + Environment.NewLine;
                ediZqm.Description += "E1EDP02|069 =" + myZqm.Produktionsnummer + " " + Environment.NewLine;
                ediZqm.Description += "E1EDK02|012 =" + myZqm.LfsNo;
                ediZqm.WorkspaceXmlRef = myZqm.iRCVPRN;
            }
            return ediZqm;
        }


        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            ediZQMQalityXml = new EdiZQMQalityXml();
            ListZQMQalityActive = new List<EdiZQMQalityXml>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void Fill()
        {
            sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml(ediZQMQalityXml);
            string strSQL = sql.sql_Add(); // sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiZQMQalityXml");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public void SetValue(DataRow row)
        {
            ediZQMQalityXml = new EdiZQMQalityXml();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            ediZQMQalityXml.Id = iTmp;

            ediZQMQalityXml.iDocNo = row["iDocNo"].ToString();
            DateTime tmpDate = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["iDocDate"].ToString(), out tmpDate);
            ediZQMQalityXml.iDocDate = tmpDate;

            ediZQMQalityXml.Path = row["Path"].ToString();
            ediZQMQalityXml.FileName = row["FileName"].ToString();

            iTmp = 0;
            int.TryParse(row["WorkspaceId"].ToString(), out iTmp);
            ediZQMQalityXml.WorkspaceId = iTmp;

            if (ediZQMQalityXml.WorkspaceId > 0)
            {
                workspaceVD = new WorkspaceViewData(ediZQMQalityXml.WorkspaceId);
                ediZQMQalityXml.Workspace = workspaceVD.Workspace.Copy();
            }

            iTmp = 0;
            int.TryParse(row["ArticleId"].ToString(), out iTmp);
            ediZQMQalityXml.ArticleId = iTmp;

            ediZQMQalityXml.IsActive = (bool)row["IsActive"];
            ediZQMQalityXml.iDocXml = row["iDocXml"].ToString();
            ediZQMQalityXml.LfsNr = row["LfsNr"].ToString();
            ediZQMQalityXml.Produktionsnummer = row["Produktionsnummer"].ToString();
            ediZQMQalityXml.Description = row["Description"].ToString();
            ediZQMQalityXml.WorkspaceXmlRef = row["WorkspaceXmlRef"].ToString();
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public bool Add()
        {
            bool bReturn = false;
            if (this.Exist)
            {
                ediZQMQalityXml.IsActive = false;
            }
            //ediZQMQalityXml.Produktionsnummer = ediZQMQalityXml.Produktionsnummer.TrimStart('0');
            //ediZQMQalityXml.LfsNr = ediZQMQalityXml.LfsNr.TrimStart('0');
            ediZQMQalityXml.iDocXml = ediZQMQalityXml.iDocXml.Replace("'", "");

            sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml(ediZQMQalityXml);
            string strSQL = sql.sql_Add(); // sql_Add;
            strSQL += " Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, BenutzerID);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                ediZQMQalityXml.Id = iTmp;
                bReturn = true;
            }
            else
            {
                clsMail ErrorMail = new clsMail();
                ErrorMail.InitClass(new Globals._GL_USER(), null);
                ErrorMail.Subject = "Error - Communicator -  EdiZQMQalityXmlViewData -Add()";
                string strMes = "Communicator - EdiZQMQalityXmlViewData.Add() " + Environment.NewLine;
                strMes += "Zertifikat" + Environment.NewLine;
                strMes += String.Format("{0,-10} \t{1}", "iDoc:", ediZQMQalityXml.iDocNo) + Environment.NewLine;
                strMes += String.Format("{0,-10} \t{1}", "Datum:", ediZQMQalityXml.iDocDate.ToString()) + Environment.NewLine;
                strMes += String.Format("{0,-10} \t{1}", "Path:", ediZQMQalityXml.Path) + Environment.NewLine;
                strMes += String.Format("{0,-10} \t{1}", "Datei:", ediZQMQalityXml.FileName) + Environment.NewLine;
                strMes += String.Format("{0,-10} \t{1}", "LfsNr:", ediZQMQalityXml.LfsNr) + Environment.NewLine;
                strMes += String.Format("{0,-10} \t{1}", "ProdNr:", ediZQMQalityXml.Produktionsnummer) + Environment.NewLine;
                strMes += String.Format("{0,-10} \t{1}", "AB-Ref:", ediZQMQalityXml.WorkspaceXmlRef) + Environment.NewLine;

                strMes += "XML Datei:" + Environment.NewLine;
                strMes += ediZQMQalityXml.iDocXml + Environment.NewLine;

                strMes += Environment.NewLine;
                strMes += Environment.NewLine;

                strMes += "SQL-Statement:" + Environment.NewLine;
                strMes += strSQL + Environment.NewLine;

                ErrorMail.Message = strMes;
                ErrorMail.SendError();
            }
            return bReturn;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            //string strSql = sql_Update;
            sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml(ediZQMQalityXml);
            string strSQL = sql.sql_Update(); // sql_Add;
            bool retVal = clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
            return retVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void FillList(bool mybInclSub)
        {
            //ListEdiDelforValue = new List<EdiDelforD97AValues>();
            //string strSQL = sql_GetList;
            //DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        SetValue(dr, mybInclSub);
            //        ListEdiDelforValue.Add(EdiDelforValue);
            //    }
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            //string strSQL = sql_GetList;
            sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml(ediZQMQalityXml);
            string strSQL = sql.sql_GetList(); // sql_Add;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void GetListByDate(DateTime mySelectedDate)
        {
            //string strSQL = sql_GetList;
            sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml(ediZQMQalityXml);
            string strSQL = sql.sql_GetListByDate(mySelectedDate); // sql_Add;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    if (!ListZQMQalityActive.Contains(ediZQMQalityXml))
                    {
                        ListZQMQalityActive.Add(ediZQMQalityXml);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void GetListActivItems()
        {
            ListZQMQalityActive = new List<EdiZQMQalityXml>();
            //string strSQL = sql_GetList;
            sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml(ediZQMQalityXml);
            string strSQL = sql.sql_GetList();
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    if (!ListZQMQalityActive.Contains(ediZQMQalityXml))
                    {
                        ListZQMQalityActive.Add(ediZQMQalityXml);
                    }
                }
            }
            //return dt;
        }

        public void GetListItemsByLfsAndProductionNo(string myLfsNo, string myProductionNo)
        {
            ListZQMQalityActive = new List<EdiZQMQalityXml>();
            //string strSQL = sql_GetList;
            sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml();
            string strSQL = sql.sql_GetListByLfsNrAndProductionNo(myLfsNo, myProductionNo);
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    if (!ListZQMQalityActive.Contains(ediZQMQalityXml))
                    {
                        ListZQMQalityActive.Add(ediZQMQalityXml);
                    }
                }
            }
            //return dt;
        }
        public void GetListActivItemsFrom(DateTime myDate)
        {
            ListZQMQalityActive = new List<EdiZQMQalityXml>();
            string strSql = string.Empty;
            //strSql = "SELECT * FROM EdiZQMQalityXml WHERE Created >= CAST('"+ myDate.AddDays(-1).Date.ToString("dd.MM.yyyy")+"'; ";
            sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml(ediZQMQalityXml);
            string strSQL = sql.sql_GetListActivItemsFrom(myDate);

            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "EdiDelforValue");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    if (!ListZQMQalityActive.Contains(ediZQMQalityXml))
                    {
                        ListZQMQalityActive.Add(ediZQMQalityXml);
                    }
                }
            }
            //return dt;
        }
        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Add sql - String
        /// </summary>
        //public string sql_Add
        //{
        //    get
        //    {
        //        string strSQL = "INSERT INTO EdiZQMQalityXml ([iDocNo], [iDocDate], [Path], [FileName], [WorkspaceId], " +
        //                                                    "[ArticleId], [IsActive], [iDocXml], [LfsNr] ,[Produktionsnummer], [Description])" +
        //                                                    "VALUES "+
        //                                                    "("+"" +
        //                                                         "'" + ediZQMQalityXml.iDocNo + "'" +
        //                                                         ", '" + ediZQMQalityXml.iDocDate + "'" +
        //                                                         ", '" + ediZQMQalityXml.Path + "'"+
        //                                                         ", '" + ediZQMQalityXml.FileName + "'" +                                                                
        //                                                         ", " + ediZQMQalityXml.WorkspaceId + 
        //                                                         ", " + ediZQMQalityXml.ArticleId +
        //                                                         ", " + Convert.ToInt32(ediZQMQalityXml.IsActive) +
        //                                                         ", '" + ediZQMQalityXml.iDocXml + "'" +
        //                                                         ", '" + ediZQMQalityXml.LfsNr + "'" +
        //                                                         ", '" + ediZQMQalityXml.Produktionsnummer + "'" +
        //                                                          ", '" + ediZQMQalityXml.Description + "'" +
        //                                                     ") ";
        //        return strSQL;
        //    }
        //}
        /// <summary>
        ///             GET
        /// </summary>
        //public string sql_Get
        //{
        //    get
        //    {
        //        string strSql = string.Empty;
        //        strSql = "SELECT * FROM EdiZQMQalityXml WHERE ID=" + ediZQMQalityXml.Id + "; ";
        //        return strSql;
        //    }
        //}
        /// <summary>
        ///             Exist
        /// </summary>
        //public string sql_Exist
        //{
        //    get
        //    {
        //        string strSql = string.Empty;
        //        strSql = "SELECT Top(1) * FROM EdiZQMQalityXml ";
        //        strSql += "WHERE ";
        //        strSql += "IDocNo ='" + ediZQMQalityXml.iDocNo + "' "; 
        //        strSql += "AND LfsNr ='" + ediZQMQalityXml.LfsNr + "' ";
        //        strSql += "AND Produktionsnummer='" + ediZQMQalityXml.Produktionsnummer + "' ";
        //        return strSql;
        //    }
        //}

        ///// <summary>
        /////             GET List
        ///// </summary>
        //public string sql_GetList
        //{
        //    get
        //    {
        //        string strSql = string.Empty;
        //        strSql = "SELECT * FROM EdiZQMQalityXml WHERE IsActive=1; ";
        //        return strSql;
        //    }
        //}
        ///// <summary>
        /////             GET_Main
        ///// </summary>
        //public string sql_Get_Main
        //{
        //    get
        //    {
        //        string strSql = string.Empty;
        //        strSql = "SELECT * FROM EdiZQMQalityXml";
        //        return strSql;
        //    }
        //}

        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        //public string sql_Delete
        //{
        //    get
        //    {
        //        string strSql = string.Empty;
        //        return strSql;
        //    }
        //}
        /// <summary>
        ///             Update sql - String
        /// </summary>
        //public string sql_Update
        //{
        //    get
        //    {
        //        string strSql = string.Empty;
        //        strSql = "Update EdiZQMQalityXml SET " +
        //                                    " [iDocNo] = '" + ediZQMQalityXml.iDocNo + "'" +
        //                                    ", [iDocDate] = '" + ediZQMQalityXml.iDocDate + "'" +
        //                                    ", [Path] = '" + ediZQMQalityXml.Path + "'" +
        //                                    ", [FileName] = '" + ediZQMQalityXml.FileName+ "'"  +                                            
        //                                    ", [WorkspaceId] = " + ediZQMQalityXml.WorkspaceId +
        //                                    ", [ArticleId] = " + ediZQMQalityXml.ArticleId +
        //                                    ", [IsActive] = " + Convert.ToInt32(ediZQMQalityXml.IsActive) + 
        //                                    ", [iDocXml] = '" + ediZQMQalityXml.iDocXml + "' " +
        //                                    ", [LfsNr] = '"+ ediZQMQalityXml.LfsNr + "'" +
        //                                    ", [Produktionsnummer] = '" + ediZQMQalityXml.Produktionsnummer + "' " +
        //                                    ", [Description] = '" + ediZQMQalityXml.Description + "'" + 

        //                                    " WHERE ID=" + ediZQMQalityXml.Id + " ;";
        //        return strSql;
        //    }
        //}
        /// <summary>
        ///             Update sql - String
        /// </summary>
        //public string sql_Update_IsActive
        //{
        //    get
        //    {
        //        string strSql = string.Empty;
        //        strSql = "Update EdiZQMQalityXml SET " +
        //                                    "WorkspaceId = " + ediZQMQalityXml.WorkspaceId +
        //                                    ", ArticleId = " + ediZQMQalityXml.ArticleId + 
        //                                    ", IsActive= "+ Convert.ToInt32(ediZQMQalityXml.IsActive) +" "+
        //                                    ", [LfsNr] = '" + ediZQMQalityXml.LfsNr + "'" +
        //                                    ", [Produktionsnummer] = '" + ediZQMQalityXml.Produktionsnummer + "'" +
        //                                    ", [Description] = '" + ediZQMQalityXml.Description + "'" +
        //                                    "WHERE ID=" + ediZQMQalityXml.Id + " ; ";
        //        return strSql;
        //    }
        //}



        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------

        //public static bool ExistNewDelforCallToProceed(int myUserId, int myWorkspaceId)
        //{
        //    string strSql = "SELECT Id FROM EdiDelforD97AValue where IsActive=1 and WorkspaceId="+myWorkspaceId+";";
        //    bool bReturn = clsSQLCOM.ExecuteSQL_GetValueBool(strSql, myUserId);
        //    return bReturn;
        //}
    }
}

