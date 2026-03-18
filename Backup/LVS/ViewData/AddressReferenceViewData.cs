using Common.Models;
using Common.Views;
using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class AddressReferenceViewData
    {

        public AddressReferences adrReference { get; set; }
        public Addresses SenderAddress { get; set; }
        public Addresses ReceiverAddress { get; set; }
        private int BenutzerID { get; set; }
        public AddressViewData adrViewData { get; set; }

        //public List<AddressReferences> ListAllAddressReferences { get; set; } = new List<AddressReferences>();
        public List<AddressReferences> ListAddressReferences { get; set; } = new List<AddressReferences>();
        public List<AddressReferenceView> ListAdrReferenceView { get; set; } = new List<AddressReferenceView>();


        public AddressReferenceViewData()
        {
            InitCls();
        }
        public AddressReferenceViewData(AddressReferences myAdrRef, int myUserId) : this()
        {
            adrReference = myAdrRef;
            BenutzerID = myUserId;
        }

        public AddressReferenceViewData(int myId, int myUserId) : this()
        {
            //InitCls();
            adrReference.Id = myId;
            BenutzerID = myUserId;
            if (adrReference.Id > 0)
            {
                Fill();
            }
        }
        public AddressReferenceViewData(int mySenderAdrId, int myUserId, bool myFillList) : this()
        {
            adrReference.SenderAdrId = mySenderAdrId;
            BenutzerID = myUserId;
            if ((adrReference.SenderAdrId > 0) && (myFillList))
            {
                GetADRVerweiseList();
            }
        }

        private void InitCls()
        {
            adrReference = new AddressReferences();
            //ListAllAddressReferences= new List<AddressReferences>();
        }

        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "AddressReference", "Reference", 0);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
            else
            {
                adrReference = new AddressReferences();
            }
        }

        private void SetValue(DataRow row)
        {
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            this.adrReference.Id = iTmp;

            iTmp = 0;
            int.TryParse(row["SenderAdrID"].ToString(), out iTmp);
            this.adrReference.SenderAdrId = iTmp;

            iTmp = 0;
            int.TryParse(row["VerweisAdrID"].ToString(), out iTmp);
            this.adrReference.VerweisAdrId = iTmp;

            iTmp = 0;
            int.TryParse(row["LieferantenID"].ToString(), out iTmp);
            this.adrReference.SupplierId = iTmp;

            iTmp = 0;
            int.TryParse(row["MandantenID"].ToString(), out iTmp);
            this.adrReference.MandantenId = iTmp;

            iTmp = 0;
            int.TryParse(row["ArbeitsbereichID"].ToString(), out iTmp);
            this.adrReference.WorkspaceId = iTmp;


            this.adrReference.Reference = row["Verweis"].ToString();
            this.adrReference.IsActive = (bool)row["aktiv"];
            this.adrReference.ASNFileTyp = row["ASNFileTyp"].ToString();
            this.adrReference.SupplierReference = row["LieferantenVerweis"].ToString();
            this.adrReference.ReferenceArt = string.Empty;
            if (row["VerweisArt"] != DBNull.Value)
            {
                this.adrReference.ReferenceArt = row["VerweisArt"].ToString();
            }
            this.adrReference.Remark = string.Empty;
            if (row["Bemerkung"] != DBNull.Value)
            {
                this.adrReference.Remark = row["Bemerkung"].ToString();
            }
            this.adrReference.UseS712F04 = (bool)row["UseS712F04"];
            this.adrReference.UseS713F13 = (bool)row["UseS713F13"];
            this.adrReference.SenderReference = row["SenderVerweis"].ToString();
            this.adrReference.SupplierNo = string.Empty;
            if (row["SupplierNo"] != DBNull.Value)
            {
                this.adrReference.SupplierNo = row["SupplierNo"].ToString();
            }

            if (this.adrReference.SenderAdrId > 0)
            {
                adrViewData = new AddressViewData(this.adrReference.SenderAdrId, BenutzerID);
                SenderAddress = adrViewData.Address.Copy();
                this.adrReference.SenderAddress = adrViewData.Address.Copy();
            }
            if (this.adrReference.VerweisAdrId > 0)
            {
                adrViewData = new AddressViewData(this.adrReference.VerweisAdrId, BenutzerID);
                ReceiverAddress = adrViewData.Address.Copy();
                this.adrReference.ReceiverAddress = adrViewData.Address.Copy();
            }

            this.adrReference.Description = row["Description"].ToString();


            if (row.Table.Columns.Contains("ReferencePart1") && row["ReferencePart1"] != DBNull.Value)
                this.adrReference.ReferencePart1 = row["ReferencePart1"].ToString();

            if (row.Table.Columns.Contains("ReferencePart2") && row["ReferencePart2"] != DBNull.Value)
                this.adrReference.ReferencePart2 = row["ReferencePart2"].ToString();

            if (row.Table.Columns.Contains("ReferencePart3") && row["ReferencePart3"] != DBNull.Value)
                this.adrReference.ReferencePart3 = row["ReferencePart3"].ToString();

        }
        public bool ExistVerweis()
        {
            bool bReturn = false;
            string strSql = "SELECT ID FROM ADRVerweis " +
                                        "WHERE ASNFileTyp='" + adrReference.Reference + "'" +
                                                " AND SenderAdrID=" + adrReference.SenderAdrId +
                                                " AND VerweisArt='" + adrReference.ReferenceArt + "'  ;";
            bReturn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, this.BenutzerID);
            return bReturn;
        }

        public void FillClassByVerweis(string myVerweis, string myASNFileTyp)
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ADRVerweis WHERE Verweis='" + myVerweis + "' and ASNFileTyp='" + myASNFileTyp + "' AND aktiv=1;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                this.adrReference.Id = iTmp;
                this.Fill();
            }
        }
        private void FillListVerweisAdr()
        {
            ListAddressReferences = new List<AddressReferences>();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis " +
                                            "WHERE " +
                                                " MandantenID=" + adrReference.MandantenId +
                                                " AND ArbeitsbereichID=" + adrReference.WorkspaceId +
                                                " AND SenderAdrID=" + adrReference.SenderAdrId + " ;";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Verweise");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    adrReference.Id = iTmp;
                    Fill();
                    if (ListAddressReferences.Contains(adrReference))
                    {
                        ListAddressReferences.Add(adrReference);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void GetADRVerweiseListAll()
        {
            ListAdrReferenceView = new List<AddressReferenceView>();

            string strSql = string.Empty;
            strSql = "select ADRVerweis.*" +
                            " from ADRVerweis ";
            //" LEFT JOIN ADR ON ADRVerweis.SenderAdrID = ADR.ID " +
            //" LEFT JOIN Arbeitsbereich ON ADRVerweis.ArbeitsbereichID = Arbeitsbereich.ID " +
            //" WHERE SenderAdrID=" + adrReference.SenderAdrId + ";";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Verweise");
            FillList(dt, true);

        }
        public void GetADRVerweiseList()
        {
            ListAdrReferenceView = new List<AddressReferenceView>();

            string strSql = string.Empty;
            strSql = "select ADRVerweis.*" +
                            " from ADRVerweis " +
                            " LEFT JOIN ADR ON ADRVerweis.SenderAdrID = ADR.ID " +
                            " LEFT JOIN Arbeitsbereich ON ADRVerweis.ArbeitsbereichID = Arbeitsbereich.ID " +
                            " WHERE SenderAdrID=" + adrReference.SenderAdrId + ";";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Verweise");
            FillList(dt, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="myAllReferences"></param>
        private void FillList(DataTable dt, bool myAllReferences)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    adrReference = new AddressReferences();
                    SetValue(dt.Rows[i]);
                    if (adrReference.Id > 0)
                    {
                        //-- Liste Auflistung der AdrReference
                        if (!ListAddressReferences.Contains(adrReference))
                        {
                            ListAddressReferences.Add(adrReference);
                        }

                        //--- Erstellen der AdressReferenceView Liste
                        int iwsTmp = 0;
                        int.TryParse(dt.Rows[i]["ArbeitsbereichID"].ToString(), out iwsTmp);
                        if (iwsTmp > 0)
                        {
                            WorkspaceViewData wVD = new WorkspaceViewData(iwsTmp);
                            AddressReferenceView v = new AddressReferenceView(adrReference, wVD.Workspace);

                            if (myAllReferences)
                            {
                                //if (!ListAdrReferenceView.Contains(v))
                                //{
                                //    ListAdrReferenceView.Add(v);
                                //}
                            }
                            else
                            {
                                if (!ListAdrReferenceView.Contains(v))
                                {
                                    ListAdrReferenceView.Add(v);
                                }
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSql = sql_Add;
            strSql = strSql + "Select @@IDENTITY as 'ID';";

            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            adrReference.Id = iTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myListImported"></param>
        /// <returns></returns>
        public bool AddbyImport(List<AddressReferences> myListImported, int myAdrId)
        {
            bool bReturn = false;
            InitCls();
            this.ListAddressReferences = myListImported;
            if (this.ListAddressReferences != null && this.ListAddressReferences.Count > 0)
            {
                List<string> listSqlStatements = new List<string>();
                foreach (AddressReferences assign in this.ListAddressReferences)
                {
                    this.adrReference = new AddressReferences();
                    this.adrReference = assign.Copy();
                    this.adrReference.SenderAdrId = myAdrId;
                    switch (this.adrReference.ReferenceArt)
                    {
                        case "RECEIVER":
                            break;
                        case "SENDER":
                            this.adrReference.VerweisAdrId = myAdrId;
                            break;
                    }
                    listSqlStatements.Add(sql_Add);
                }
                // Execute all SQL statements in a transaction
                if (listSqlStatements.Count > 0)
                {
                    string strSql = string.Empty;
                    foreach (string sql in listSqlStatements)
                    {
                        strSql += sql + Environment.NewLine;
                    }

                    bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "AddressReferences", this.BenutzerID);
                }
            }
            return bReturn;
        }


        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
            if (adrReference.Id > 0)
            {
                string strSql = string.Empty;
                strSql = sql_Delete;
                bool mybExecOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "DELETE", BenutzerID);
                //Logbuch Eintrag
                if (mybExecOK)
                {
                    ////Add Logbucheintrag 
                    //string myBeschreibung = "Artikel gelöscht: Artikel ID [" + Artikel.Id.ToString() + "] ";
                    //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
                }
            }
        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = sql_Update;
            bool retVal = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            return retVal;
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Add sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO ADRVerweis (SenderAdrID, VerweisAdrID, MandantenID, ArbeitsbereichID, Verweis, aktiv" +
                                 ", ASNFileTyp, LieferantenVerweis, VerweisArt, Bemerkung, UseS712F04, UseS713F13" +
                                 ", SenderVerweis, SupplierNo, Description, ReferencePart1, ReferencePart2, ReferencePart3" +
                        ") " +
                         "VALUES (" + adrReference.SenderAdrId +
                         ", " + adrReference.VerweisAdrId +
                         //", " + LieferantenID +
                         ", " + adrReference.MandantenId +
                         ", '" + adrReference.WorkspaceId + "'" +
                         ", '" + adrReference.Reference + "'" +
                         ", " + Convert.ToInt32(adrReference.IsActive) +
                         ", '" + adrReference.ASNFileTyp + "'" +
                         ", '" + adrReference.SupplierReference + "'" +
                         ", '" + adrReference.ReferenceArt + "'" +
                         ", '" + adrReference.Remark + "'" +
                         ", " + Convert.ToInt32(adrReference.UseS712F04) +
                         ", " + Convert.ToInt32(adrReference.UseS713F13) +
                         ", '" + adrReference.SenderReference + "'" +
                         ", '" + adrReference.SupplierNo + "'" +
                         ", '" + adrReference.Description + "'" +
                         ", '" + adrReference.ReferencePart1 + "'" +
                         ", '" + adrReference.ReferencePart2 + "'" +
                         ", '" + adrReference.ReferencePart3 + "'" +
                         ");";
                //strSql = strSql + " Select @@IDENTITY as 'ID'; ";

                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * from ADRVerweis WHERE ID=" + adrReference.Id + ";";
                return strSql;
            }
        }
        public string sql_GetList
        {
            get
            {
                string strSql = string.Empty;
                strSql = "select " +
                                "ADRVerweis.ID" +
                                ", Arbeitsbereich.Name as Arbeitsbereich " +
                                ", ADRVerweis.MandantenID " +
                                ", ADRVerweis.SenderAdrId " +
                                ", ADRVerweis.VerweisAdrId " +
                                ", ADRVerweis.Verweis" +
                                ", ADRVerweis.aktiv" +
                                ", ADRVerweis.ASNFileTyp " +
                                ", ADRVerweis.LieferantenVerweis" +
                                ", ADRVerweis.VerweisArt" +
                                ", ADRVerweis.Bemerkung " +
                                ", ADRVerweis.UseS712F04 " +
                                ", ADRVerweis.UseS713F13 " +
                                ", ADRVerweis.SenderVerweis" +
                                ", ADRVerweis.SupplierNo" +
                                ", ADRVerweis.ReferencePart1" +
                                ", ADRVerweis.ReferencePart2" +
                                ", ADRVerweis.ReferencePart3" +
                                " from ADRVerweis " +
                                " LEFT JOIN ADR ON ADRVerweis.SenderAdrID = ADR.ID " +
                                " LEFT JOIN Arbeitsbereich ON ADRVerweis.ArbeitsbereichID = Arbeitsbereich.ID " +
                                " WHERE SenderAdrID=" + adrReference.SenderAdrId + ";";
                return strSql;
            }
        }

        /// <summary>
        ///             GET_Main
        /// </summary>
        public string sql_Get_Main
        {
            get
            {
                string strSql = string.Empty;
                //strSql = "SELECT * FROM ADR ";
                return strSql;
            }
        }

        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Delete FROM ADRVerweis WHERE ID =" + adrReference.Id;
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
                string strSql = string.Empty;
                strSql = "Update ADRVerweis " +
                                    "SET " +
                                        " SenderAdrID=" + adrReference.SenderAdrId +
                                        ", VerweisAdrID=" + adrReference.VerweisAdrId +
                                        //", LieferantenID='" + LieferantenID + "'" +
                                        ", MandantenID=" + adrReference.MandantenId +
                                        ", ArbeitsbereichID=" + adrReference.WorkspaceId +
                                        ", Verweis='" + adrReference.Reference + "'" +
                                        ", aktiv=" + Convert.ToInt32(adrReference.IsActive) +
                                        ", ASNFileTyp='" + adrReference.ASNFileTyp + "'" +
                                        ", LieferantenVerweis='" + adrReference.SupplierReference + "'" +
                                        ", VerweisArt ='" + adrReference.ReferenceArt + "'" +
                                        ", Bemerkung ='" + adrReference.Remark + "'" +
                                        ", UseS712F04=" + Convert.ToInt32(adrReference.UseS712F04) +
                                        ", UseS713F13=" + Convert.ToInt32(adrReference.UseS713F13) +
                                        ", SenderVerweis= '" + adrReference.SenderReference + "'" +
                                        ", SupplierNo='" + adrReference.SupplierNo + "'" +
                                        ", ReferencePart1='" + adrReference.ReferencePart1 + "'" +
                                        ", ReferencePart2='" + adrReference.ReferencePart2 + "'" +
                                        ", ReferencePart3='" + adrReference.ReferencePart3 + "'" +

                                            " WHERE ID=" + adrReference.Id + ";";
                return strSql;
            }
        }



        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="ReciepienId"></param>
        /// <param name="WorkspaceId"></param>
        /// <returns></returns>
        //public static string GetSupplierNo(int ClientId, int ReciepienId, int WorkspaceId)
        //{
        //    string strReturn = string.Empty;
        //    strReturn = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(ClientId, ReciepienId, 1, constValue_AsnArt.const_Art_VDA4913, WorkspaceId);
        //    return strReturn;
        //}
        ///<summary>GetAdrVerweis</summary>
        ///<remarks></remarks>
        public static Dictionary<int, AddressReferences> GetAdrVerweis(int myUserId, int mySenderAdr, int myMandant, int myWorkspaceId)
        {
            Dictionary<int, AddressReferences> dict = new Dictionary<int, AddressReferences>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis " +
                                            "WHERE " +
                                                " MandantenID=" + myMandant +
                                                " AND ArbeitsbereichID=" + myWorkspaceId +
                                                " AND SenderAdrID=" + mySenderAdr + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myUserId, "Verweise");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    AddressReferenceViewData vd = new AddressReferenceViewData(iTmp, myUserId);
                    if (!dict.ContainsKey(vd.adrReference.VerweisAdrId))
                    {
                        dict.Add(vd.adrReference.VerweisAdrId, vd.adrReference);
                    }
                }
            }
            return dict;
        }
        ///<summary>FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static Dictionary<int, AddressReferences> FillDictAdrVerweis(int myMandant, int myWorkspaceId, int myUserId, string myASNFileTyp)
        {
            Dictionary<int, AddressReferences> dict = new Dictionary<int, AddressReferences>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis " +
                                     " WHERE " +
                                             "MandantenID=" + myMandant + " " +
                                             "AND ArbeitsbereichID=" + myWorkspaceId +
                                             " AND ASNFileTyp='" + myASNFileTyp + "' ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myUserId, "Verweise");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    AddressReferenceViewData vd = new AddressReferenceViewData(iTmp, myUserId);
                    if (!dict.ContainsKey(vd.adrReference.VerweisAdrId))
                    {
                        dict.Add(vd.adrReference.VerweisAdrId, vd.adrReference);
                    }
                }
            }
            return dict;
        }
        ///<summary>FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static Dictionary<string, AddressReferences> FillDictAdrVerweisAll(int myUserId, string myASNFileTyp)
        {
            Dictionary<string, AddressReferences> dict = new Dictionary<string, AddressReferences>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis WHERE aktiv=1 AND ASNFileTyp='" + myASNFileTyp + "' ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myUserId, "Verweise");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    AddressReferenceViewData vd = new AddressReferenceViewData(iTmp, myUserId);
                    if (!dict.ContainsKey(vd.adrReference.Reference))
                    {
                        dict.Add(vd.adrReference.Reference, vd.adrReference);
                    }
                }
            }
            return dict;
        }
        ///<summary>FillDictAdrVerweisSender</summary>
        ///<remarks></remarks>
        public static Dictionary<string, AddressReferences> FillDictAdrVerweisSender(int myMandantenId, int myWorkspaceId, int myUserId, string myASNFileTyp)
        {
            Dictionary<string, AddressReferences> dict = new Dictionary<string, AddressReferences>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis " +
                                    "WHERE " +
                                        "aktiv=1 " +
                                        " AND MandantenID=" + myMandantenId +
                                        " AND ArbeitsbereichID=" + myWorkspaceId +
                                        " AND ASNFileTyp='" + myASNFileTyp + "' " +
                                        "AND  VerweisArt='SENDER' " +
                                    ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myUserId, "Verweise");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    AddressReferenceViewData vd = new AddressReferenceViewData(iTmp, myUserId);
                    if (!dict.ContainsKey(vd.adrReference.Reference))
                    {
                        dict.Add(vd.adrReference.Reference, vd.adrReference);
                    }
                }
            }
            return dict;
        }
        ///<summary>FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static Dictionary<string, AddressReferences> FillDictAdrVerweisReceiver(int myMandantenId, int myWorkspaceId, int myUserId, string myASNFileTyp)
        {
            Dictionary<string, AddressReferences> dict = new Dictionary<string, AddressReferences>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis " +
                        "WHERE " +
                            "aktiv=1 " +
                            " AND MandantenID=" + myMandantenId +
                            " AND ArbeitsbereichID=" + myWorkspaceId +
                            " AND ASNFileTyp='" + myASNFileTyp + "' " +
                            "AND  VerweisArt='RECEIVER' " +
                            ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myUserId, "Verweise");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    AddressReferenceViewData vd = new AddressReferenceViewData(iTmp, myUserId);
                    if (!dict.ContainsKey(vd.adrReference.Reference))
                    {
                        dict.Add(vd.adrReference.Reference, vd.adrReference);
                    }
                }
            }
            return dict;
        }

        ///<summary>FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static Dictionary<string, AddressReferences> FillDictAdrVerweisBySender(int mySenderId, int myUserId)
        {
            Dictionary<string, AddressReferences> dict = new Dictionary<string, AddressReferences>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRVerweis WHERE aktiv=1 AND SenderAdrID=" + mySenderId + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myUserId, "Verweise");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    AddressReferenceViewData vd = new AddressReferenceViewData(iTmp, myUserId);
                    if (!dict.ContainsKey(vd.adrReference.Reference))
                    {
                        dict.Add(vd.adrReference.Reference, vd.adrReference);
                    }
                }
            }
            return dict;
        }
        ///<summary>clsADRVerweis / FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static string GetLieferantenVerweisBySenderAndReceiverAdrAndAsnFileTyp(int mySender, int myReceiver, int myUserId, string myASNFileTyp, int myWorkspaceId)
        {
            string strSql = string.Empty;
            strSql = " SELECT Top (1) LieferantenVerweis FROM ADRVerweis " +
                                                        " WHERE " +
                                                            " SenderAdrID=" + mySender +
                                                            " AND VerweisAdrID =" + myReceiver +
                                                            " AND ArbeitsbereichID=" + myWorkspaceId +
                                                            " AND ASNFileTyp ='" + myASNFileTyp + "'" +
                                                            " AND aktiv=1 ; ";
            string strReturn = clsSQLcon.ExecuteSQL_GetValue(strSql, myUserId);
            return strReturn;
        }
        ///<summary>clsADRVerweis / FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static string GetLieferantenVerweisBySenderAndReceiverAdr(int mySender, int myReceiver, int myUserId, int myWorkspaceId)
        {
            string strSql = string.Empty;
            strSql = " SELECT Top (1) LieferantenVerweis FROM ADRVerweis " +
                                                        " WHERE " +
                                                            " SenderAdrID=" + mySender +
                                                            " AND VerweisAdrID =" + myReceiver +
                                                            " AND ArbeitsbereichID=" + myWorkspaceId +
                                                            //" AND ASNFileTyp ='" + myASNFileTyp + "'" +
                                                            " AND aktiv=1 ; ";
            string strReturn = clsSQLcon.ExecuteSQL_GetValue(strSql, myUserId);
            return strReturn;
        }
        ///<summary>clsADRVerweis / FillDictAdrVerweis</summary>
        ///<remarks></remarks>
        public static string GetSenderVerweisBySenderAndReceiverAdr(int mySender, int myReceiver, int myUserId, string myASNFileTyp, int myWorkspaceId)
        {
            string strSql = string.Empty;
            strSql = " SELECT Top (1) SenderVerweis FROM ADRVerweis " +
                                                        " WHERE " +
                                                            " SenderAdrID=" + mySender +
                                                            " AND VerweisAdrID =" + myReceiver +
                                                            " AND ArbeitsbereichID=" + myWorkspaceId +
                                                            " AND ASNFileTyp ='" + myASNFileTyp + "'" +
                                                            " AND aktiv=1 ; ";
            string strReturn = clsSQLcon.ExecuteSQL_GetValue(strSql, myUserId);
            return strReturn;
        }

        public static string GetSupplierNoBySenderAndReceiverAdr(int mySender, int myReceiver, int myUserId, string myASNFileTyp, int myWorkspaceId)
        {
            string strSql = string.Empty;
            strSql = " SELECT Top (1) SupplierNo FROM ADRVerweis " +
                                                        " WHERE " +
                                                            " SenderAdrID=" + mySender +
                                                            " AND VerweisAdrID =" + myReceiver +
                                                            " AND ArbeitsbereichID=" + myWorkspaceId +
                                                            " AND ASNFileTyp ='" + myASNFileTyp + "'" +
                                                            " ; ";
            string strReturn = clsSQLcon.ExecuteSQL_GetValue(strSql, myUserId);
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
        public static bool Exist(ref AddressReferences myAdrReference, decimal myBenuzter)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ADRVerweis " +
                                    "WHERE " +
                                        "SenderAdrID=" + myAdrReference.SenderAdrId +
                                        " AND VerweisAdrID=" + myAdrReference.VerweisAdrId +
                                        " AND MandantenID=" + myAdrReference.MandantenId +
                                        " AND ArbeitsbereichID=" + myAdrReference.WorkspaceId +
                                        " AND ASNFileTyp ='" + myAdrReference.ASNFileTyp + "'" +
                                        " AND LieferantenVerweis= '" + myAdrReference.SupplierReference + "'" +
                                        " ;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myBenuzter);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            bReturn = (iTmp > 0);
            return bReturn;
        }
        ///<summary>clsADRVerweis / Exist</summary>
        ///<remarks></remarks>
        //public static int GETWorkspaceBySupplierReference(int mySenderAdrId, string mySupplierRefrence, string myAsnFileTyp, int myUserId)
        public static int GETWorkspaceBySupplierReference(string mySupplierRefrence, string myAsnFileTyp, int myUserId)
        {
            int iReturn = 0;
            string strSql = string.Empty;
            strSql = "SELECT Top(1) ArbeitsbereichID FROM ADRVerweis " +
                                    "WHERE " +
                                        "LieferantenVerweis='" + mySupplierRefrence + "'" +
                                        " AND ASNFileTyp ='" + myAsnFileTyp + "'" +
                                        //" AND SenderAdrID=" + mySenderAdrId +
                                        " ;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myUserId);
            int iTmp = 0;
            int.TryParse(strTmp, out iReturn);
            return iReturn;
        }

    }
}

