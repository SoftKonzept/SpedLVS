using Common.Enumerations;
using Common.Models;
using LVS.Constants;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class AddressViewData
    {
        public Addresses Address { get; set; }
        private int BenutzerID { get; set; }
        public AddressViewData adrViewData { get; set; }

        public List<Addresses> ListAddresses { get; set; }


        public AddressViewData()
        {
            InitCls();
        }
        public AddressViewData(Addresses myAdr, int myUserId) : this()
        {
            Address = myAdr;
            BenutzerID = myUserId;
        }
        public AddressViewData(int myId, int myUserId) : this()
        {
            //InitCls();
            Address.Id = myId;
            BenutzerID = myUserId;
            if (Address.Id > 0)
            {
                Fill();
            }
        }

        private void InitCls()
        {
            Address = new Addresses();
        }

        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Address", "ADR", 0);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
            else
            {
                Address = new Addresses();
            }
        }

        private void SetValue(DataRow row)
        {
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            Address.Id = iTmp;
            Address.ViewId = row["ViewID"].ToString();
            iTmp = 0;
            int.TryParse(row["KD_ID"].ToString(), out iTmp);
            Address.KundenId = iTmp;
            Address.FBez = row["FBez"].ToString();
            Address.Name1 = row["Name1"].ToString();
            Address.Name2 = row["Name2"].ToString();
            Address.Name3 = row["Name3"].ToString();
            Address.Street = row["Str"].ToString();
            Address.HouseNo = row["HausNr"].ToString();
            Address.POBox = row["PF"].ToString();
            Address.ZIP = row["PLZ"].ToString();
            //address.PFOrt = row["PLZPF"].ToString();
            Address.City = row["Ort"].ToString();
            //address.OrtPF = row["OrtPF"].ToString();
            Address.Land = row["Land"].ToString();
            Address.LKZ = row["LKZ"].ToString();
            //address.WAvon = (DateTime)row["WAvon"];
            //address.WAbis = (DateTime)row["WAbis"];
            //address.Dummy = (bool)row["Dummy"];
            //address.UserInfoTxt = row["UserInfoTxt"].ToString();
            Address.activ = (bool)row["activ"];
            Address.ASNCom = (bool)row["ASNCom"];
            iTmp = 0;
            int.TryParse(row["Lagernummer"].ToString(), out iTmp);
            Address.Storenumber = iTmp;
            iTmp = 0;
            int.TryParse(row["AdrID_Be"].ToString(), out iTmp);
            Address.AdrId_Be = iTmp;
            iTmp = 0;
            int.TryParse(row["AdrID_Ent"].ToString(), out iTmp);
            Address.AdrId_Ent = iTmp;
            iTmp = 0;
            int.TryParse(row["AdrID_Post"].ToString(), out iTmp);
            Address.AdrId_Post = iTmp;
            iTmp = 0;
            int.TryParse(row["AdrID_RG"].ToString(), out iTmp);
            Address.AdrId_RG = iTmp;
            Address.IsAuftraggeber = (bool)row["IsAuftraggeber"];
            Address.IsVersender = (bool)row["IsVersender"];
            Address.IsBelade = (bool)row["IsBelade"];
            Address.IsEmpfaenger = (bool)row["IsEmpfaenger"];
            Address.IsEntlade = (bool)row["IsEntlade"];
            //address.IsPost = (bool)row["IsPost"];
            //address.IsRG = (bool)row["IsRG"];
            //address.CalcLagerVers = (bool)row["CalcLagerVers"];
            //address.DocEinlagerAnzeige = row["DocEinlagerAnzeige"].ToString();
            //address.DocAuslagerAnzeige = row["DocAuslagerAnzeige"].ToString();
            //address.Verweis = row["Verweis"].ToString();
            //int iTmp = 0;
            //int.TryParse(row["PostRGBy"].ToString(), out iTmp);
            //address.PostRGBy = iTmp;
            //iTmp = 0;
            //int.TryParse(row["PostAnlageBy"].ToString(), out iTmp);
            //address.PostAnlageBy = iTmp;
            //iTmp = 0;
            //int.TryParse(row["PostLfsBy"].ToString(), out iTmp);
            //address.PostLfsBy = iTmp;
            //iTmp = 0;
            //int.TryParse(row["PostListBy"].ToString(), out iTmp);
            //address.PostListBy = iTmp;
            //address.IsDiv = (bool)row["IsDiv"];
            //address.IsSpedition = (bool)row["IsSpedition"];
            //iTmp = 0;
            //decimal.TryParse(row["PostAnzeigeBy"].ToString(), out iTmp);
            //address.PostAnzeigeBy = iTmp;
            iTmp = 0;
            if (row["DUNSNr"] != null)
            {
                int.TryParse(row["DUNSNr"].ToString(), out iTmp);
            }
            Address.DunsNr = iTmp;
            if (Address.Id > 0)
            {
                AddressCustomViewData acVD = new AddressCustomViewData(Address, BenutzerID);
                Address.CustomerData = acVD.AddressCustomer.Copy();
            }
        }

        public void GetAddresslist(enumAppProcess myProcess, int myWorkspaceId)
        {
            ListAddresses = new List<Addresses>();
            string strSQL = sql_Get_Main;
            switch (myProcess)
            {
                case enumAppProcess.StoreIn:
                    strSQL += "where IsAuftraggeber=1 ";
                    break;
                case enumAppProcess.StoreOut:
                    strSQL = string.Empty;
                    strSQL = "SELECT DISTINCT adr.* " +
                                        "FROM Artikel a " +
                                        "Inner join LEingang e on e.ID = a.LEingangTableID " +
                                        "Inner join ADR adr on adr.ID = e.Auftraggeber " +
                                                "where a.LA_Checked = 0 and a.AB_ID =" + myWorkspaceId.ToString();
                    break;
                case enumAppProcess.NotSet:
                    break;
            }
            DataTable dt = new DataTable("ADR");
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());
            ListAddresses = new List<Addresses>();

            foreach (DataRow dr in dt.Rows)
            {
                Address = new Addresses();
                SetValue(dr);
                ListAddresses.Add(Address);
            }
            ListAddresses = ListAddresses.OrderBy(x => x.ViewId).ToList();
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            //string strSql = sql_Add;
            //strSql = strSql + "Select @@IDENTITY as 'ID';";

            //string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            //int.TryParse(strTmp, out int iTmp);
            //this.Ausgang.Id = iTmp;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
            //if (Artikel.Id > 0)
            //{
            //    //ID = myArtID;
            //    //GetArtikeldatenByTableID();
            //    //decimal decTmpLagerEingangID = clsLager.GetLEingangIDByLEingangTableID(this._GL_User.User_ID, this.LEingangTableID);

            //    string strSql = string.Empty;
            //    strSql = sql_Delete;
            //    //bool mybExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            //    bool mybExecOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "DELETE",BenutzerID);
            //    //Logbuch Eintrag
            //    if (mybExecOK)
            //    {
            //        //Add Logbucheintrag 
            //        string myBeschreibung = "Artikel gelöscht: Artikel ID [" + Artikel.Id.ToString() + "] ";
            //        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            //    }
            //}
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

        public string GetADRString()
        {
            string str = string.Empty;
            str += Address.Name1 + " - ";
            str += Address.ZIP + " - ";
            str += Address.City;
            return str;
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
                strSql = "SELECT * FROM ADR WHERE ID=" + Address.Id + ";";
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
                strSql = "SELECT * FROM ADR ";
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
                string strSql = "Delete FROM ADR WHERE ID =" + Address.Id;
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
                strSql = "Update ADR SET " +
                                        "ViewID='" + Address.ViewId + "'" +
                                        ", KD_ID =" + Address.KundenId +
                                        ", Fbez = '" + Address.FBez + "'" +
                                        ", Name1 = '" + Address.Name1 + "'" +
                                        ", Name2 = '" + Address.Name2 + "'" +
                                        ", Name3 = '" + Address.Name3 + "'" +
                                        ", Str = '" + Address.Street + "'" +
                                        ", PLZ = '" + Address.ZIP + "'" +
                                        ", PLZPF = '" + Address.ZIP + "'" +
                                        ", Ort = '" + Address.City + "'" +
                                        ", OrtPF = '" + Address.City + "'" +

                                         ", DUNSNr = " + Address.DunsNr +


                                        "  WHERE ID=" + Address.Id + "; ";
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
        public static string GetSupplierNo(int ClientId, int ReciepienId, int WorkspaceId)
        {
            string strReturn = string.Empty;
            strReturn = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(ClientId, ReciepienId, 1, constValue_AsnArt.const_Art_VDA4913, WorkspaceId);
            return strReturn;
        }

    }
}

