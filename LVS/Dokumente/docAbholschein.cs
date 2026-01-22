namespace LVS.Dokumente
{
    using LVS;
    using System;
    using System.Data;

    /// <summary>
    /// Summary description for docAbholschein.
    /// </summary>
    public partial class docAbholschein : docLieferschein
    {
        public decimal AuftragPosTableID = 0;
        public decimal AuftragID = 0;
        public decimal AuftragPos = 0;

        public bool IsAnmeldeschein = false;
        public Globals._GL_USER GL_User;


        public docAbholschein()
        {
            InitializeComponent();
        }
        //
        //
        //
        public void SetAbholschein(decimal myAuftragPosTableID)
        {
            IsLieferschein = false; //Abholschein
            GL_User_Lfs = GL_User;
            AuftragPosTableID = myAuftragPosTableID;
            AuftragID = clsAuftragPos.GetAuftragIDByID(AuftragPosTableID);
            AuftragPos = clsAuftragPos.GetAuftragPosByID(AuftragPosTableID);

            Lfs = new clsLieferscheine();
            Lfs.boIsLieferschein = IsLieferschein;
            Lfs.AuftragID = AuftragID;
            Lfs.AuftragPos = AuftragPos;
            Lfs.dtArtikelDetails = dtArtikelDetails;
            Lfs.dtPrintdaten = dtPrintdaten;
            Lfs.SetPrintDaten();
            Lfs.GetAbholscheinDaten(AuftragPosTableID);


            tbLieferscheinNr.Value = string.Empty;
            tbAuftrag.Value = AuftragID.ToString() + " / " + AuftragPos.ToString();

            SetPrintDatenValue();

            InitBriefkopf();
            //lfs_pageHeader.Dispose();

            //Versender
            InitVersenderAbholschein();

            this.detail = lfs_detail;
            tbEHeadline.Value = "FÜR:";
            tbVHeadline.Value = "VON:";

            //Auftraggeber
            InitEmpfaengerAbholschein();

            panelUnterschrift.Dispose();
        }
        //
        //----------------- aus Emfpänger wird Auftraggeber im Abholschein -------------
        //
        private void InitEmpfaengerAbholschein()
        {
            tbEName1.Value = Lfs.EName1;
            tbEName2.Value = Lfs.EName2;
            tbEName3.Value = Lfs.EName3;
            tbEStr.Value = Lfs.EStr;
            tbEPLZ.Value = Lfs.EPLZ;
            tbEOrt.Value = Lfs.EOrt;

        }
        //
        //------ Versender--------
        //
        public void InitVersenderAbholschein()
        {
            tbVName1.Value = Lfs.VName1;
            tbVName2.Value = Lfs.VName2;
            tbVName3.Value = Lfs.VName3;
            tbVStr.Value = Lfs.VStr;
            tbVPLZ.Value = Lfs.VPLZ;
            tbVOrt.Value = Lfs.VOrt;
        }
        //
        //
        //
        private void docAbholschein_NeedDataSource(object sender, EventArgs e)
        {
            DataTable dataTable = Lfs.dsLieferschein.Tables["Artikel"];

            if (dataTable.Columns["Gewicht"] == null)
            {
                if (dataTable.Columns["gemGewicht"] != null)
                {
                    dataTable.Columns["gemGewicht"].ColumnName = "Gewicht";
                }
            }
            dtArtikel.DataSource = dataTable;
        }

    }
}