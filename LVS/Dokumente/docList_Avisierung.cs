namespace LVS.Dokumente
{
    using LVS;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Summary description for docList_Avisierung.
    /// </summary>
    public partial class docList_Avisierung : Telerik.Reporting.Report
    {
        public Globals._GL_USER GL_User;
        internal bool Kunde;
        internal decimal iID;

        public docList_Avisierung()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //                
        }

        //
        //
        /**************************************************************************************+
         *                      SQL Abfrage Liste
         * 
         * *************************************************************************************/
        private string GetMainSQLString_Kunde()
        {
            string sql = "SELECT DISTINCT" +
                        "(CAST(Artikel.AuftragID as varchar(6)) + '-'+CAST(Artikel.AuftragPos as varchar(3))) as 'AuftragPos', " +
                        "(SELECT Name1 FROM ADR WHERE ID = Auftrag.KD_ID) AS 'Kunde', " +
                        "Artikel.Werksnummer, " +
                        "Artikel.GArt AS 'Gut', " +
                            "CAST(Artikel.Dicke AS varchar(8)) + 'x' +" +
                            "CAST(Artikel.Breite AS varchar(8)) + 'x' +" +
                            "CAST(Artikel.Laenge AS varchar(8)) + 'x' +" +
                            "CAST(Artikel.Hoehe AS varchar(8)) AS 'Abmessungen', " +
                        "Artikel.Brutto AS 'Gewicht' " +
                        "FROM Artikel " +
                        "INNER JOIN Auftrag ON Auftrag.ANr = Artikel.AuftragID " +
                        "INNER JOIN AuftragPos ON Auftrag.ANr=AuftragPos.Auftrag_ID ";
            //Zusatz 
            return sql;
        }
        //
        private string GetMainSQLString_Verlader()
        {
            string sql = "SELECT DISTINCT" +
                        "(CAST(Artikel.AuftragID as varchar(6)) + '-'+CAST(Artikel.AuftragPos as varchar(3))) as 'AuftragPos', " +
                        "(SELECT Name1 FROM ADR WHERE ID = Auftrag.B_ID) AS 'Kunde', " +    //Kunde da DataSourcebindung
                        "Artikel.Werksnummer, " +
                        "Artikel.GArt AS 'Gut', " +
                            "CAST(Artikel.Dicke AS varchar(8)) + 'x' +" +
                            "CAST(Artikel.Breite AS varchar(8)) + 'x' +" +
                            "CAST(Artikel.Laenge AS varchar(8)) + 'x' +" +
                            "CAST(Artikel.Hoehe AS varchar(8)) AS 'Abmessungen', " +
                        "Artikel.Brutto AS 'Gewicht' " +
                        "FROM Artikel " +
                        "INNER JOIN Auftrag ON Auftrag.ANr = Artikel.AuftragID " +
                        "INNER JOIN AuftragPos ON Auftrag.ANr=AuftragPos.Auftrag_ID ";
            //Zusatz 
            return sql;
        }
        //
        private string GetSQLByKD_ID(decimal KD_ID)
        {
            string sql = string.Empty;
            sql = "WHERE Auftrag.KD_ID ='" + KD_ID + "' " +
                 "AND AuftragPos.VSB>'31.12.9999'";
            return sql;
        }
        //
        private string GetSQLByB_ID(decimal B_ID)
        {
            string sql = string.Empty;
            sql = "WHERE Auftrag.B_ID = '" + B_ID + "' " +
                  "AND AuftragPos.VSB>'31.12.9999'"; ;
            return sql;
        }
        //
        private DataTable GetArtikelTableForList(bool Kunde, decimal iID)
        {
            DataTable dt = new DataTable("Avisierungsliste");
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;

            string strSQL = string.Empty;

            if (Kunde)
            {
                strSQL = GetMainSQLString_Verlader();
                //strSQL = GetMainSQLString_Kunde();
                strSQL = strSQL + GetSQLByKD_ID(iID);
            }
            else
            {
                //strSQL = GetMainSQLString_Verlader();
                strSQL = GetMainSQLString_Kunde();
                strSQL = strSQL + GetSQLByB_ID(iID);
            }

            Command.CommandText = strSQL;
            ada.Fill(dt);
            Command.Dispose();
            Globals.SQLcon.Close();
            return dt;
        }
        /**************************************************************************************************/
        //
        //
        //
        public void InitListe(bool _Kunde, decimal _iID)
        {
            Kunde = _Kunde;
            iID = _iID;

            SetInfoToFrm();
        }
        //
        private void docList_Avisierung_NeedDataSource(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetArtikelTableForList(Kunde, iID);
            dtArtikel.DataSource = dt;
        }
        //
        //
        private void SetInfoToFrm()
        {
            if (Kunde)
            {
                //tbVerlader.Value = "Verlader: ";
                tbVerlader.Value = "Auftraggeber: ";
            }
            else
            {
                //tbVerlader.Value = "Auftraggeber: ";
                tbVerlader.Value = "Verlader: ";
            }
            tbVerladerName.Value = clsADR.GetADRString(iID);
            tbDate.Value = DateTime.Now.Date.ToShortDateString();
        }
    }
}