using Common.Enumerations;
using Common.Models;
using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    /// <summary>
    ///                 Datenbank LVS | Kunde
    /// </summary>
    public class AddressCustomViewData
    {
        public Addresses Address { get; set; }
        public AddressCustomer AddressCustomer { get; set; }
        private int BenutzerID { get; set; }
        public AddressViewData adrViewData { get; set; }

        public List<Addresses> ListAddresses { get; set; }


        public AddressCustomViewData()
        {
            InitCls();
        }

        public AddressCustomViewData(Addresses myAdr, int myUserId) : this()
        {
            //InitCls();
            if (myAdr is Addresses)
            {
                Address = (Addresses)myAdr;
                BenutzerID = myUserId;
                if (Address.Id > 0)
                {
                    FillByAdrId();
                }
            }
        }
        public AddressCustomViewData(AddressCustomer myAdrCustomer, int myUserId) : this()
        {
            //InitCls();
            if (myAdrCustomer is AddressCustomer)
            {
                AddressCustomer = (AddressCustomer)myAdrCustomer;
                BenutzerID = myUserId;
                if (AddressCustomer.Id > 0)
                {
                    Fill();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            Address = new Addresses();
            AddressCustomer = new AddressCustomer();
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillByAdrId()
        {
            sqlStatementCreater.sqlCreater_AddressCustumer sql = new sqlStatementCreater.sqlCreater_AddressCustumer(Address);
            string strSQL = sql.sql_GetByAdrId;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Address", "ADR", 0);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        public void Fill()
        {
            sqlStatementCreater.sqlCreater_AddressCustumer sql = new sqlStatementCreater.sqlCreater_AddressCustumer(AddressCustomer);
            string strSQL = sql.sql_GetById;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Address", "ADR", 0);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        private void SetValue(DataRow row)
        {
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            AddressCustomer.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["KD_ID"].ToString(), out iTmp);
            AddressCustomer.KD_ID = iTmp;
            iTmp = 0;
            int.TryParse(row["ADR_ID"].ToString(), out iTmp);
            AddressCustomer.AdrId = iTmp;
            AddressCustomer.SteuerNr = row["SteuerNr"].ToString();
            AddressCustomer.UstId = row["USt_ID"].ToString();
            AddressCustomer.MwSt = (bool)row["MwSt"];
            decimal decTmp = 0M;
            decimal.TryParse(row["MwStSatz"].ToString(), out decTmp);
            AddressCustomer.MwStSatz = decTmp;

            AddressCustomer.Bank1 = row["Bank1"].ToString();
            iTmp = 0;
            Int32.TryParse(row["BLZ1"].ToString(), out iTmp);
            AddressCustomer.BLZ1 = iTmp;
            AddressCustomer.Kto1 = row["Kto1"].ToString();
            AddressCustomer.Swift1 = row["Swift1"].ToString();
            AddressCustomer.IBAN1 = row["IBAN1"].ToString();
            AddressCustomer.Bank2 = row["Bank2"].ToString();

            iTmp = 0;
            Int32.TryParse(row["BLZ2"].ToString(), out iTmp);
            AddressCustomer.BLZ2 = iTmp;
            AddressCustomer.Kto2 = row["Kto2"].ToString();
            AddressCustomer.Swift2 = row["Swift2"].ToString();
            AddressCustomer.IBAN2 = row["IBAN2"].ToString();

            iTmp = 0;
            Int32.TryParse(row["Kreditor"].ToString(), out iTmp);
            AddressCustomer.Kreditor = iTmp;
            iTmp = 0;
            Int32.TryParse(row["Debitor"].ToString(), out iTmp);
            AddressCustomer.Debitor = iTmp;
            iTmp = 0;
            Int32.TryParse(row["ZZ"].ToString(), out iTmp);
            AddressCustomer.Zahlungziel = iTmp;
            iTmp = 0;
            Int32.TryParse(row["KD_IDman"].ToString(), out iTmp);
            AddressCustomer.KD_IDman = iTmp;
            AddressCustomer.SalesTaxKeyDebitor = (Int32)row["SalesTaxKeyDebitor"];
            AddressCustomer.SalesTaxKeyKreditor = (Int32)row["SalesTaxKeyKreditor"];

            AddressCustomer.Contact = row["Contact"].ToString();
            AddressCustomer.Phone = row["Phone"].ToString();
            AddressCustomer.Mailaddress = row["Mailaddress"].ToString();
            AddressCustomer.Organisation = row["Organisation"].ToString();

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
            string strSql = sql_Add;
            strSql = strSql + "Select @@IDENTITY as 'ID';";

            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            if (iTmp > 0)
            {
                AddressCustomer.Id = iTmp;
            }
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
            //ArtikelVita
            //clsArtikelVita.LagerAusgangChange(BenutzerID, this.Ausgang.Id, this.Ausgang.LAusgangID);
            //// Logbucheintrag Eintrag
            //string Beschreibung = "Lager Ausgang geändert: Nr [" + this.Ausgang.LAusgangID.ToString() + "] "+
            //                      "/ Mandant [" + this.Ausgang.MandantenID.ToString() + "] "+
            //                      "/ Arbeitsbereich [" + this.Ausgang.ArbeitsbereichId.ToString() + "]";

            //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
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
                //strSql = "Update Artikel SET " +
                //                        "AuftragID=" + Artikel.AuftragID +
                //                        ", AuftragPos=" + Artikel.AuftragPos +
                //                        ", Mandanten_ID=" + Artikel.MandantenID +
                //                        ", AB_ID=" + Artikel.AbBereichID +
                //                        ", BKZ = " + Artikel.BKZ +
                //                        //ID REF
                //                        ", LVS_ID=" + Artikel.LVS_ID +
                //                        ", GArtID='" + Artikel.GArtID + "'" +
                //                        ", GutZusatz='" + Artikel.GutZusatz + "'" +
                //                        ", Werksnummer='" + Artikel.Werksnummer + "'" +
                //                        ", Produktionsnummer='" + Artikel.Produktionsnummer + "'" +
                //                        ", Charge='" + Artikel.Charge + "'" +
                //                        ", Bestellnummer='" + Artikel.Bestellnummer + "'" +
                //                        ", exMaterialnummer='" + Artikel.exMaterialnummer + "'" +
                //                        ", exBezeichnung='" + Artikel.exBezeichnung + "'" +
                //                        ", Position='" + Artikel.Position + "'" +
                //                        ", ArtIDRef='" + Artikel.ArtIDRef + "'" +

                //                        //Maße - Gewichte
                //                        ", Anzahl=" + Artikel.Anzahl +
                //                        ", Einheit='" + Artikel.Einheit + "'" +
                //                        ", Dicke='" + Artikel.Dicke.ToString().Replace(",", ".") + "'" +
                //                        ", Breite='" + Artikel.Breite.ToString().Replace(",", ".") + "'" +
                //                        ", Laenge='" + Artikel.Laenge.ToString().Replace(",", ".") + "'" +
                //                        ", Hoehe='" + Artikel.Hoehe.ToString().Replace(",", ".") + "'" +
                //                        ", Netto='" + Artikel.Netto.ToString().Replace(",", ".") + "'" +
                //                        ", Brutto='" + Artikel.Brutto.ToString().Replace(",", ".") + "'" +
                //                        ", TARef= '" + Artikel.TARef + "'" +
                //                        ", LEingangTableID=" + Artikel.LEingangTableID +
                //                        ", LAusgangTableID=" + Artikel.LAusgangTableID +
                //                        ", ArtIDAlt =" + Artikel.ArtIDAlt +
                //                        //Flags
                //                        ", UB=" + Convert.ToInt32(Artikel.Umbuchung) +
                //                        ", AbrufRef ='" + Artikel.AbrufReferenz + "'" +
                //                        ", CheckArt= '" + Artikel.EingangChecked + "'" +
                //                        ", LA_Checked ='" + Artikel.AusgangChecked + "'" +
                //                        ", Info='" + Artikel.Info + "'" +
                //                        ", LagerOrt=" + Artikel.LagerOrt +
                //                        ", LOTable='" + Artikel.LagerOrtTable + "'" +
                //                        ", exLagerOrt = '" + Artikel.exLagerOrt + "'" +
                //                        //", IsLagerArtikel ="+ Convert.ToInt32(IsLagerArtikel)+
                //                        ", ADRLagerNr=" + Artikel.ADRLagerNr +
                //                        //", FreigabeAbruf="+Convert.ToInt32(FreigabeAbruf)+   //Flag wird nicht hier upgedatet
                //                        ", LZZ ='" + Artikel.LZZ + "'" +
                //                        ", Werk ='" + Artikel.Werk + "'" +
                //                        ", Halle ='" + Artikel.Halle + "'" +
                //                        ", Reihe ='" + Artikel.Reihe + "'" +
                //                        ", Ebene = '" + Artikel.Ebene + "'" +
                //                        ", Platz ='" + Artikel.Platz + "'" +
                //                        ", exAuftrag ='" + Artikel.exAuftrag + "'" +
                //                        ", exAuftragPos ='" + Artikel.exAuftragPos + "'" +
                //                        ", ASNVerbraucher ='" + Artikel.ASNVerbraucher + "'" +
                //                        ", UB_AltCalcEinlagerung =" + Convert.ToInt32(Artikel.UB_AltCalcEinlagerung) +  //nur über UB
                //                        ", UB_AltCalcAuslagerung =" + Convert.ToInt32(Artikel.UB_AltCalcAuslagerung) +
                //                        ", UB_AltCalcLagergeld =" + Convert.ToInt32(Artikel.UB_AltCalcLagergeld) +
                //                        ", UB_NeuCalcEinlagerung =" + Convert.ToInt32(Artikel.UB_NeuCalcEinlagerung) +
                //                        ", UB_NeuCalcAuslagerung =" + Convert.ToInt32(Artikel.UB_NeuCalcAuslagerung) +
                //                        ", UB_NeuCalcLagergeld =" + Convert.ToInt32(Artikel.UB_NeuCalcLagergeld) +
                //                        ", IsVerpackt =" + Convert.ToInt32(Artikel.IsVerpackt) +
                //                        ", intInfo='" + Artikel.interneInfo + "'" +
                //                        ", exInfo='" + Artikel.externeInfo + "'" +
                //                        ", Guete='" + Artikel.Guete + "'" +
                //                        ", IsMulde=" + Convert.ToInt32(Artikel.IsMulde) +
                //                        ", IsLabelPrint=" + Convert.ToInt32(Artikel.IsLabelPrint) +
                //                        ", IsProblem=" + Convert.ToInt32(Artikel.IsProblem) +
                //                        ", IsStackable=" + Convert.ToInt32(this.Artikel.IsStackable) +
                //                        ", GlowDate='" + this.Artikel.GlowDate + "'" +

                //                        "  WHERE ID=" + Artikel.Id + "; ";
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

