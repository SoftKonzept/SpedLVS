using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class MandantenViewData
    {
        public Mandanten Mandant { get; set; }
        private int BenutzerID { get; set; }
        public List<Mandanten> ListMandanten { get; set; }

        internal AddressViewData adrVD { get; set; }


        public MandantenViewData()
        {
            InitCls();
        }

        public MandantenViewData(int myId)
        {
            InitCls();
            Mandant.Id = myId;
            if (Mandant.Id > 0)
            {
                Fill();
            }
        }

        private void InitCls()
        {
            Mandant = new Mandanten();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Fill()
        {
            string strSQL = sql_Get_Main + " WHERE ID=" + Mandant.Id.ToString();
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Mandant", "Mandant", 0);
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
        private void SetValue(DataRow row)
        {
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            Mandant.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["ADR_ID"].ToString(), out iTmp);
            Mandant.AddressId = iTmp;
            Mandant.Description = row["Beschreibung"].ToString().Trim();
            Mandant.IsActiv = (bool)row["aktiv"];
            Mandant.Matchcode = row["Matchcode"].ToString().Trim();
            Mandant.IsDefaultSped = (bool)row["Default_Sped"];
            Mandant.IsDefaultStore = (bool)row["Default_Lager"];
            Mandant.VDA4905Verweis = row["VDA4905Verweis"].ToString().Trim();
            Mandant.ReportPath = row["ReportPath"].ToString().Trim();
            Mandant.Bank = row["Bank"].ToString().Trim();
            Mandant.Blz = row["Blz"].ToString().Trim();
            Mandant.Bic = row["Bic"].ToString().Trim();
            Mandant.Konto = row["Konto"].ToString().Trim();
            Mandant.Iban = row["Iban"].ToString().Trim();
            Mandant.Contact = row["Contact"].ToString().Trim();
            Mandant.Mail = row["Mail"].ToString().Trim();
            Mandant.Homepage = row["Homepage"].ToString().Trim();
            Mandant.Phone = row["Phone"].ToString().Trim();
            Mandant.VatId = row["VatId"].ToString().Trim();
            Mandant.TaxNumber = row["TaxNumber"].ToString().Trim();
            Mandant.Organisation = row["Organisation"].ToString().Trim();

            if (Mandant.AddressId > 0)
            {
                adrVD = new AddressViewData(Mandant.AddressId, this.BenutzerID);
                Mandant.Address = adrVD.Address.Copy();
            }
        }

        public void GetMandantenList()
        {
            ListMandanten = new List<Mandanten>();

            string strSQL = sql_Get_Mandantenlist;
            DataTable dt = new DataTable("Mandanten");
            dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Mandanten", "Mandanten", 0);
            foreach (DataRow dr in dt.Rows)
            {
                Mandant = new Mandanten();
                SetValue(dr);
                ListMandanten.Add(Mandant);
            }
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public bool Add()
        {
            bool bTransActionOK = false;
            if (Mandant.AddressId > 0)
            {
                string strSql = string.Empty;
                //CHeck der Default-Werte
                //Sped
                if (Mandant.IsDefaultSped)
                {
                    strSql = strSql + "Update Mandanten Set Default_Sped=0; ";
                }
                //Lager
                if (Mandant.IsDefaultStore)
                {
                    strSql = strSql + "Update Mandanten Set Default_Lager=0; ";
                }
                //Insert
                strSql = strSql + "INSERT INTO Mandanten (ADR_ID, Matchcode, Beschreibung, aktiv, Default_Sped, Default_Lager, VDA4905Verweis,ReportPath, " +
                                                         "Bank, BLZ, BIC, Konto, IBAN, Contact, Mail, Homepage, Phone, VatId, TaxNumber, Organisation) " +
                                               "VALUES (" + Mandant.AddressId + ",'"
                                                           + Mandant.Matchcode + "','"
                                                           + Mandant.Description + "', "
                                                           + Convert.ToInt32(Mandant.IsActiv) + ", "
                                                           + Convert.ToInt32(Mandant.IsDefaultSped) + ","
                                                           + Convert.ToInt32(Mandant.IsDefaultStore) +
                                                           ", '" + Mandant.VDA4905Verweis + "'" +
                                                           ", '" + Mandant.ReportPath + "'" +
                                                           ", '" + Mandant.Bank + "'" +
                                                           ", '" + Mandant.Blz + "'" +
                                                           ", '" + Mandant.Bic + "'" +
                                                           ", '" + Mandant.Konto + "'" +
                                                           ", '" + Mandant.Iban + "'" +
                                                           ", '" + Mandant.Mail + "'" +
                                                           ", '" + Mandant.Homepage + "'" +
                                                           ", '" + Mandant.Phone + "'" +
                                                           ", '" + Mandant.VatId + "'" +
                                                           ", '" + Mandant.TaxNumber + "'" +
                                                           ", '" + Mandant.Organisation + "'" +

                                                           "); ";
                strSql += " Select @@IDENTITY as 'ID';";
                string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "MandantenInsert", BenutzerID);
                //bEintragOK=clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                int iTmp = 0;
                int.TryParse(strTmp, out iTmp);
                if (iTmp > 0)
                {
                    bTransActionOK = true;
                    clsPrimeKeys pk = new clsPrimeKeys();
                    pk.BenutzerID = BenutzerID;
                    pk.Mandanten_ID = iTmp;
                    pk.AddNewPrimeKeys();

                }
            }
            return bTransActionOK;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        //public void Delete()
        //{
        //}
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            bool bTransActionOK = false;
            if (Mandant.Id > 0)
            {
                string strSql = string.Empty;
                //CHeck der Default-Werte
                //Sped
                if (Mandant.IsDefaultSped)
                {
                    strSql = strSql + "Update Mandanten Set Default_Sped=0; ";
                }
                //Lager
                if (Mandant.IsDefaultStore)
                {
                    strSql = strSql + "Update Mandanten Set Default_Lager=0; ";
                }
                strSql = strSql + " Update Mandanten SET " +
                                                    "ADR_ID =" + Mandant.AddressId + " " +
                                                    ", Matchcode = '" + Mandant.Matchcode + "'" +
                                                    ", Beschreibung ='" + Mandant.Description + "'" +
                                                    ", aktiv =" + Convert.ToInt32(Mandant.IsActiv) + " " +
                                                    ", Default_Sped =" + Convert.ToInt32(Mandant.IsDefaultSped) + " " +
                                                    ", Default_Lager =" + Convert.ToInt32(Mandant.IsDefaultStore) + " " +
                                                    ", VDA4905Verweis ='" + Mandant.VDA4905Verweis + "' " +
                                                    ", ReportPath = '" + Mandant.ReportPath + "'" +
                                                    ", Bank = '" + Mandant.Bank + "'" +
                                                    ", BLZ = '" + Mandant.Blz + "'" +
                                                    ", BIC = '" + Mandant.Bic + "'" +
                                                    ", Konto = '" + Mandant.Konto + "'" +
                                                    ", IBAN = '" + Mandant.Iban + "'" +
                                                    ", Contact = '" + Mandant.Contact + "'" +
                                                    ", Mail = '" + Mandant.Mail + "'" +
                                                    ", Homepage = '" + Mandant.Homepage + "'" +
                                                    ", Phone = '" + Mandant.Phone + "'" +
                                                    ", VatId = '" + Mandant.VatId + "'" +
                                                    ", TaxNumber = '" + Mandant.TaxNumber + "'" +
                                                    ", Organisation = '" + Mandant.Organisation + "'" +

                                                    " WHERE ID='" + Mandant.Id + "'";

                bTransActionOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "MandantenUpdate", BenutzerID);
            }
            return bTransActionOK;
            //return retVal;
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
        public string sql_Get_Mandantenlist
        {
            get
            {
                string strSql = sql_Get_Main + " ORDER BY ID ";
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
                strSql = "Select * FROM Mandanten ";
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
                return strSql;
            }
        }

    }
}

