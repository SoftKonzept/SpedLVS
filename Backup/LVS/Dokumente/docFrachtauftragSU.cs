namespace LVS.Dokumente
{
    using System;
    using System.Data;

    /// <summary>
    /// Summary description for clsFrachtauftragSU.
    /// </summary>
    public partial class docFrachtauftragSU : docBriefkofpHeisiep
    {
        public DataSet ds;
        public docFrachtauftragSU()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //this.dtArtikel.DataSource = ds.Tables["Artikel"];  
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        //
        //
        //
        public void IntiDataSet(DataSet _ds)
        {
            ds = _ds;
            InitSubunternehmer();
            InitBeladestelle();
            InitEntladestelle();
            InitTransportauftragdaten();
            this.detail_Frachtauftrag = bk_Details;
            this.bk_Details.Dispose();
        }
        //
        //------------- Initialisierung Subunternehmer -------------------
        //
        private void InitSubunternehmer()
        {
            for (Int32 i = 0; i < ds.Tables["SU"].Rows.Count; i++)
            {
                tbADRAnrede.Value = ds.Tables["SU"].Rows[i]["FBez"].ToString();
                tbADRName1.Value = ds.Tables["SU"].Rows[i]["Name1"].ToString();
                tbADRName2.Value = ds.Tables["SU"].Rows[i]["Name2"].ToString();
                tbADRStr.Value = ds.Tables["SU"].Rows[i]["Str"].ToString();
                tbADRzHd.Value = ds.Tables["SU"].Rows[i]["zHd"].ToString();
                tbADRPLZ.Value = ds.Tables["SU"].Rows[i]["PLZ"].ToString();
                tbADROrt.Value = ds.Tables["SU"].Rows[i]["Ort"].ToString();
            }
        }
        //
        //----------- Versand - Beladestelle -----------------------------
        //
        private void InitBeladestelle()
        {
            for (Int32 i = 0; i < ds.Tables["Versender"].Rows.Count; i++)
            {
                tbVName1.Value = ds.Tables["Versender"].Rows[i]["Name1"].ToString();
                tbVName2.Value = ds.Tables["Versender"].Rows[i]["Name2"].ToString();
                tbVStr.Value = ds.Tables["Versender"].Rows[i]["Str"].ToString();
                tbVPLZOrt.Value = ds.Tables["Versender"].Rows[i]["PLZ"].ToString() + " " + ds.Tables["Versender"].Rows[i]["Ort"].ToString();
            }
        }
        //
        //-------------------Entladestelle----------------------
        //
        private void InitEntladestelle()
        {
            for (Int32 i = 0; i < ds.Tables["Empfänger"].Rows.Count; i++)
            {
                tbEName1.Value = ds.Tables["Empfänger"].Rows[i]["Name1"].ToString();
                tbEName2.Value = ds.Tables["Empfänger"].Rows[i]["Name2"].ToString();
                tbEStr.Value = ds.Tables["Empfänger"].Rows[i]["Str"].ToString();
                tbEPLZOrt.Value = ds.Tables["Empfänger"].Rows[i]["PLZ"].ToString() + " " + ds.Tables["Empfänger"].Rows[i]["Ort"].ToString();
            }
        }
        //
        //----------------------Transportauftrag--------------------
        //
        private void InitTransportauftragdaten()
        {
            if (ds.Tables["Transportauftrag"] != null)
            {
                for (Int32 i = 0; i < ds.Tables["Transportauftrag"].Rows.Count; i++)
                {
                    DateTime BDate = Convert.ToDateTime(ds.Tables["Transportauftrag"].Rows[i]["B_Date"].ToString());
                    DateTime BTime = Convert.ToDateTime(ds.Tables["Transportauftrag"].Rows[i]["B_Time"].ToString());
                    DateTime EDate = Convert.ToDateTime(ds.Tables["Transportauftrag"].Rows[i]["E_Date"].ToString());
                    DateTime ETime = Convert.ToDateTime(ds.Tables["Transportauftrag"].Rows[i]["E_Time"].ToString());

                    tbAuftragnummer.Value = ds.Tables["Auftrag"].Rows[i]["Auftrag_ID"].ToString() + " / " + ds.Tables["Auftrag"].Rows[i]["AuftragPos"].ToString();
                    tbBDatum.Value = BDate.ToShortDateString();

                    if (BTime.ToShortTimeString() == "00:00")
                    {
                        tbBZF.Value = string.Empty;
                    }
                    else
                    {
                        tbBZF.Value = BTime.ToShortTimeString();
                    }
                    tbEDatum.Value = EDate.ToShortDateString();
                    if (ETime.ToShortTimeString() == "00:00")
                    {
                        tbEZF.Value = string.Empty;
                    }
                    else
                    {
                        tbEZF.Value = ETime.ToShortTimeString();
                    }
                    tbFracht.Value = ds.Tables["Transportauftrag"].Rows[i]["Fracht"].ToString();
                    tbInfo.Value = ds.Tables["Transportauftrag"].Rows[i]["Info"].ToString();
                    tbLadenummer.Value = ds.Tables["Transportauftrag"].Rows[i]["Ladenummer"].ToString();
                }
            }
        }
        //
        //------------ Binding DataSource mit Artikel ------------------
        //
        private void detail_Frachtauftrag_ItemDataBinding(object sender, EventArgs e)
        {
            this.dtArtikel.DataSource = ds.Tables["Artikel"];
        }
    }
}