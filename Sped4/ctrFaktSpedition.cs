using LVS;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrFaktSpedition : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        public ctrCalcSped _ctrCalcSped;

        internal DataTable dtAuftraege;
        internal DataTable dtAuftraggeber = new DataTable();
        internal DataTable dtMandanten;
        internal decimal _MandantenID;
        public Int32 _CalcMode;
        /**************************************
        * CalcMode
         *1=Spedition / Disposition
        * 2=Lager
        * ***********************************/

        internal string _MandantenName;

        public Int32 DocArtPrintAgain = 0;
        /**************************************
         * 1=RG
         * 2=GS
         * 3=GSanSU
         * 4=FVGS
         * ***********************************/

        public DateTime SearchDateVon = DateTime.Today;//DateTime.MinValue;
        public DateTime SearchDateBis = DateTime.Today.AddDays(7);//DateTime.MaxValue;

        //-------------- DGV Cellstyle
        public System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleDecimal = new DataGridViewCellStyle();
        public System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleString = new DataGridViewCellStyle();
        public System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleImage = new DataGridViewCellStyle();
        public System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleInt = new DataGridViewCellStyle();


        public delegate void ctrFakturierungRefreshEventHandler();
        public event ctrFakturierungRefreshEventHandler ctrFakturierungRefersh;


        public ctrFaktSpedition()
        {
            InitializeComponent();
        }
        ///<summary>ctrFakturierung / ctrFakturierung_Load</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status 7 = Freigabe zur Abrechnung</remarks>
        private void ctrFakturierung_Load(object sender, EventArgs e)
        {
            //INIT
            dtAuftraege = new DataTable();
            _MandantenID = 0;
            //combo Mandanten füllen
            dtMandanten = new DataTable("Mandanten");
            //Baustelle 
            //Functions.InitComboMandanten(GL_User, ref tscbMandanten, ref dtMandanten);
            InitCtr();
        }
        ///<summary>ctrFakturierung / tsbtnClose_Click</summary>
        ///<remarks>Abrechnungsmodus Spedition</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrFakturierung();
        }
        ///<summary>ctrFakturierung / tsbClose_Click</summary>
        ///<remarks>Form schliessen</remarks>
        private void InitCtr()
        {
            //CalcMode
            //1 = Spedition
            //2 = Lager
            bool bCalcInit = false;
            switch (_CalcMode)
            {
                case 1:
                    InitCalcSped();
                    bCalcInit = true;
                    GetAuftragDatenByStatus(0);
                    break;
                case 2:
                    InitCalcLager();
                    bCalcInit = true;
                    break;
            }
        }
        ///<summary>ctrFakturierung / tsbClose_Click</summary>
        ///<remarks>Form schliessen</remarks>
        private void tsbClose_Click(object sender, EventArgs e)
        {
            Int32 Count = this.ParentForm.Controls.Count;
            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == "TempSplitterFakturierung")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrFaktSpedition))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        ///<summary>ctrFakturierung / ctrFakturierung_Load</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status 5 = Auftrag durchgeführt</remarks>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            _CalcMode = 1;
            GetAuftragDatenByStatus(5);
        }
        ///<summary>ctrFakturierung / toolStripMenuItem5_Click</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status 7 = Freigabe zur Berechnung</remarks>
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            _CalcMode = 1;
            GetAuftragDatenByStatus(7);
        }
        ///<summary>ctrFakturierung / statusToolStripMenuItem_Click</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status 7 = Freigabe zur Berechnung</remarks>
        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _CalcMode = 1;
            //hier müsste man eventuell über die Rechnungen gehen
            GetAuftragDatenByStatus(8);
        }
        ///<summary>ctrFakturierung / toolStripMenuItem4_Click</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status 7 = Freigabe zur Abrechnung</remarks>
        private void GetAuftragDatenByStatus(Int32 myStatus)
        {
            dtAuftraege.Clear();
            switch (myStatus)
            {
                case 4:
                    //dtAuftraege = clsArtikel.GetArtikelForDispoStatusDisponiert(this.GL_User, _MandantenID, GL_User.sys_ArbeitsbereichID);
                    break;
                case 5:
                    //dtAuftraege = clsArtikel.GetArtikelForDispoStatusDone(this.GL_User, _MandantenID, GL_User.sys_ArbeitsbereichID);
                    break;
                case 6:
                    //dtAuftraege = clsArtikel.GetArtikelForDispoStatusFreigabeAbrechnung(this.GL_User, _MandantenID, GL_User.sys_ArbeitsbereichID);
                    break;
                case 7:
                    //dtAuftraege = clsArtikel.GetArtikelForDispoStatusAbgerechnet(this.GL_User, _MandantenID, GL_User.sys_ArbeitsbereichID);
                    break;
                default:
                    //dtAuftraege = clsArtikel.GetArtikelForDispoCalcDefault(this.GL_User, _MandantenID, GL_User.sys_ArbeitsbereichID);
                    break;
            }
            this.dgv.DataSource = dtAuftraege;
            for (Int32 i = 3; i <= dgv.Columns.Count - 1; i++)
            {
                this.dgv.Columns[i].Visible = false;
            }
            //Gridbreite/Panelbreite anpassen (+20 für den Scrollbalken)
            this.splitContainer1.SplitterDistance = Functions.dgv_GetWidthShownGrid(ref this.dgv) + 20;
            this.splitContainer1.Refresh();

            if (dtAuftraege.Rows.Count > 0)
            {
                //Filter werden geladen
                InitComboAuftraggeber();
                InitComboAuftragStatus();
                InitComboAuftragNr();
                //Filter freigeben
                gbFilter.Enabled = true;
            }
        }
        /****************************************************************************************************************
         *                                      Combo Boxen
         * **************************************************************************************************************/
        ///<summary>ctrFakturierung / tscbMandanten_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbMandanten_SelectedIndexChanged(object sender, EventArgs e)
        {
            Functions.SetMandantenDaten(ref this.tscbMandanten, ref this._MandantenID, ref this._MandantenName);
        }
        ///<summary>ctrFakturierung / LoadComboAuftraggeber</summary>
        ///<remarks>Läd die Auftraggeber in ComboBox zur Auswahl</remarks>
        private void InitComboAuftraggeber()
        {
            dtAuftraggeber.Clear();
            //dtAuftraggeber = clsArtikel.GetAuftraggeberForDispoStatusDone(this.GL_User, _MandantenID, GL_User.sys_ArbeitsbereichID);
            if (dtAuftraege.Rows.Count > 0)
            {
                DataRow row = dtAuftraggeber.NewRow();
                row["Auftraggeber"] = "-- bitte wählen--";
                row["KD_ID"] = -1;
                dtAuftraggeber.Rows.InsertAt(row, 0);

                cbAuftraggeber.DisplayMember = dtAuftraggeber.Columns["Auftraggeber"].ToString();
                cbAuftraggeber.ValueMember = dtAuftraggeber.Columns["KD_ID"].ToString();
                cbAuftraggeber.DataSource = dtAuftraggeber;
            }
            else
            {
                this.cbAuftraggeber.Enabled = false;
            }
        }
        ///<summary>ctrFakturierung / LoadComboAuftraggeber</summary>
        ///<remarks>Läd die Auftragnummer in ComboBox zur Auswahl</remarks>
        private void InitComboAuftragNr()
        {
            DataView dvSView = new DataView(dtAuftraege);
            dvSView.Sort = "ANr";

            DataTable dtTmp = new DataTable();
            dtTmp = dvSView.ToTable(true, "ANr");
            dtTmp.Columns.Add("Auftrag", typeof(String));

            for (Int32 i = 0; i <= dtTmp.Rows.Count - 1; i++)
            {
                dtTmp.Rows[i]["Auftrag"] = dtTmp.Rows[i]["ANr"].ToString();
            }


            if (dtTmp.Rows.Count > 0)
            {
                DataRow row = dtTmp.NewRow();
                row["Auftrag"] = "-- bitte wählen--";
                row["ANr"] = 0;
                dtTmp.Rows.InsertAt(row, 0);

                cbAuftragNr.DisplayMember = dtTmp.Columns["Auftrag"].ToString();
                cbAuftragNr.ValueMember = dtTmp.Columns["ANr"].ToString();
                cbAuftragNr.DataSource = dtTmp;
            }
            else
            {
                this.cbAuftragNr.Enabled = false;
            }
        }
        ///<summary>ctrFakturierung / InitComboAuftragStatus</summary>
        ///<remarks>Läd die Auftraggeber in ComboBox zur Auswahl</remarks>
        private void InitComboAuftragStatus()
        {
            DataTable dtTmp = Functions.InitEnumAuftragStatusToTableForDataSource(4, 8);
            this.cbStatus.DisplayMember = "Status";
            this.cbStatus.ValueMember = "StatusID";
            this.cbStatus.DataSource = dtTmp;
            this.cbStatus.SelectedIndex = 0;
        }
        ///<summary>ctrFakturierung / InitCalcSped</summary>
        ///<remarks>Controll CalcSped wird geladen</remarks>
        private void InitCalcSped()
        {
            _ctrCalcSped = new ctrCalcSped();
            _ctrCalcSped.GL_User = this.GL_User;
            _ctrCalcSped.Parent = this.splitContainer1.Panel2;
            _ctrCalcSped.Dock = DockStyle.Fill;
            _ctrCalcSped.Show();
            _ctrCalcSped.BringToFront();
        }
        ///<summary>ctrFakturierung / InitCalcLager</summary>
        ///<remarks>Controll CalcSped wird geladen</remarks>
        private void InitCalcLager()
        {

        }
        ///<summary>ctrFakturierung / dgv_CellFormatting</summary>
        ///<remarks>Formatierung Datagridview</remarks>
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Int32 myStatus = 0;
            decimal myAuftragID = 0;
            decimal myAuftragPosID = 0;
            decimal myID = 0;
            string myAuftraggeber = string.Empty;
            DateTime myAuftragDate = DateTime.MinValue;
            string myVon = string.Empty;
            string myNach = string.Empty;
            string myGut = string.Empty;
            decimal myNetto = 0;
            decimal myBrutto = 0;

            // Auftrag
            if (dgv.Columns["Stat"] != null)
            {
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["Stat"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["Stat"].Value != null)
                    {
                        myStatus = (Int32)dgv.Rows[e.RowIndex].Cells["Stat"].Value;
                    }
                }
            }

            // Auftrag
            if (dgv.Columns["ANr"] != null)
            {
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["ANr"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["ANr"].Value != null)
                    {
                        myAuftragID = (decimal)dgv.Rows[e.RowIndex].Cells["ANr"].Value;
                    }
                }
            }
            // AuftragPos
            if (dgv.Columns["AuftragPos"] != null)
            {
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["AuftragPos"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["AuftragPos"].Value != null)
                    {
                        myAuftragPosID = (decimal)dgv.Rows[e.RowIndex].Cells["AuftragPos"].Value;
                    }
                }
            }
            // Auftrag Datum
            if (dgv.Columns["ADate"] != null)
            {
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["ADate"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["ADate"].Value != null)
                    {
                        myAuftragDate = (DateTime)dgv.Rows[e.RowIndex].Cells["ADate"].Value;
                    }
                }
            }
            // Auftraggeber
            if (dgv.Columns["Auftraggeber"] != null)
            {
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["Auftraggeber"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["Auftraggeber"].Value != null)
                    {
                        myAuftraggeber = dgv.Rows[e.RowIndex].Cells["Auftraggeber"].Value.ToString();
                    }
                }
            }
            //von
            if (dgv.Columns["von"] != null)
            {
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["von"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["von"].Value != null)
                    {
                        myVon = dgv.Rows[e.RowIndex].Cells["von"].Value.ToString();
                    }
                }
            }
            //nach
            if (dgv.Columns["nach"] != null)
            {
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["nach"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["nach"].Value != null)
                    {
                        myNach = dgv.Rows[e.RowIndex].Cells["nach"].Value.ToString();
                    }
                }
            }
            //Gut
            if (dgv.Columns["GArt"] != null)
            {
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["GArt"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["GArt"].Value != null)
                    {
                        myGut = dgv.Rows[e.RowIndex].Cells["GArt"].Value.ToString();
                    }
                }
            }
            //Netto
            if (dgv.Columns["Netto"] != null)
            {
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["Netto"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["Netto"].Value != null)
                    {
                        myNetto = (decimal)dgv.Rows[e.RowIndex].Cells["Netto"].Value;
                    }
                }
            }
            //Brutto
            if (dgv.Columns["Brutto"] != null)
            {
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["Brutto"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["Brutto"].Value != null)
                    {
                        myBrutto = (decimal)dgv.Rows[e.RowIndex].Cells["Brutto"].Value;
                    }
                }
            }


            //Status
            if (e.ColumnIndex == 0)
            {
                e.Value = Functions.GetDataGridCellStatusImage(myStatus);
            }
            if (e.ColumnIndex == 1)
            {
                e.Value = "Auftrag/Position: " + Environment.NewLine +
                         "ID: " + Environment.NewLine +
                         "Datum: " + Environment.NewLine +
                         "Auftraggeber: " + Environment.NewLine +
                         "von: " + Environment.NewLine +
                         "nach: " + Environment.NewLine +
                         "Güterart: " + Environment.NewLine +
                         "Gewicht-Netto: " + Environment.NewLine +
                         "Gewicht-Brutto: " + Environment.NewLine;
            }
            if (e.ColumnIndex == 2)
            {
                e.Value = myAuftragID.ToString() + "/" + myAuftragPosID.ToString() + Environment.NewLine +
                          myID.ToString() + Environment.NewLine +
                          myAuftragDate.ToString() + Environment.NewLine +
                          myAuftraggeber + Environment.NewLine +
                          myVon + Environment.NewLine +
                          myNach + Environment.NewLine +
                          myGut + Environment.NewLine +
                          Functions.FormatDecimal(myNetto) + Environment.NewLine +
                          Functions.FormatDecimal(myBrutto) + Environment.NewLine;
            }
        }

        private void statusUnvollständigeDokumenteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /*******************************************************************************************************
         *                                      Filter
         * ****************************************************************************************************/
        ///<summary>ctrFakturierung / cbAuftraggeber_SelectedIndexChanged</summary>
        ///<remarks>Filter Auftraggeber</remarks>
        private void cbAuftraggeber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAuftraggeber.SelectedIndex > 0)
            {
                cbAuftragNr.SelectedIndex = 0;
                cbStatus.SelectedIndex = 0;
                SetGridFilter(1);
            }
            else
            {
                this.dgv.DataSource = dtAuftraege;
            }
        }
        ///<summary>ctrFakturierung / cbStatus_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStatus.SelectedIndex > 0)
            {
                cbAuftragNr.SelectedIndex = 0;
                cbAuftraggeber.SelectedIndex = 0;
                SetGridFilter(2);
            }
            else
            {
                this.dgv.DataSource = dtAuftraege;
            }
        }
        ///<summary>ctrFakturierung / cbAuftragNr_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void cbAuftragNr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAuftragNr.SelectedIndex > 0)
            {
                cbAuftraggeber.SelectedIndex = 0;
                cbStatus.SelectedIndex = 0;
                SetGridFilter(3);
            }
            else
            {
                this.dgv.DataSource = dtAuftraege;
            }
        }
        ///<summary>ctrFakturierung / cbAuftraggeber_SelectedIndexChanged</summary>
        ///<remarks>Filter Auftraggeber</remarks>
        private void SetGridFilter(Int32 myFilterMode)
        {
            //MODE 
            //1=Auftraggeber
            //2=Auftragsnummer
            //3=Rechnungsnummer

            string strFilter = string.Empty;
            switch (myFilterMode)
            {
                case 1:
                    decimal decSelectedAuftraggeber = (decimal)cbAuftraggeber.SelectedValue;
                    strFilter = "KD_ID='" + decSelectedAuftraggeber.ToString() + "'";
                    break;
                case 2:
                    Int32 iSelectedStatus = (Int32)cbStatus.SelectedValue;
                    strFilter = "Stat ='" + iSelectedStatus.ToString() + "'";
                    break;
                case 3:
                    decimal iSelectedAuftrag = (decimal)cbAuftragNr.SelectedValue;
                    strFilter = "ANr ='" + iSelectedAuftrag.ToString() + "'";
                    break;
            }

            DataView dvSView = new DataView(dtAuftraege);
            dvSView.RowFilter = strFilter;
            DataTable dtTmp = new DataTable();
            dtTmp.Clear();
            dtTmp = dvSView.ToTable();
            if (strFilter != string.Empty)
            {
                this.dgv.DataSource = dtTmp;
            }
            else
            {
                this.dgv.DataSource = dtAuftraege;
            }
        }
        /**********************************************************************************************************
         *                          Context Menü DGV Auftragliste
         * *******************************************************************************************************/
        ///<summary>ctrFakturierung / dgv_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void dgv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                //in Mousedown musste die Verknüpfung erstellt werden,damit diese Methode aufgerufen werden kann
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
                else
                {
                    miDetails_Click(sender, e);
                }
            }
        }
        ///<summary>ctrFakturierung / miDetails_Click</summary>
        ///<remarks>Öffnet das Fenster Auftragview</remarks>
        private void miDetails_Click(object sender, EventArgs e)
        {

            decimal decSelectedAuftragPosTableID = (decimal)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells["AuftragPosTableID"].Value;
            Functions.frm_FormTypeClose(typeof(frmAuftragView));
            //Baustelle
            //frmAuftragView av = new frmAuftragView(decSelectedAuftragPosTableID);
            frmAuftragView av = new frmAuftragView();
            av.GL_User = GL_User;
            //av._AuftragPosTableID = decSelectedAuftragPosTableID;
            av._ctrMenu = this._ctrMenu;
            av.Show();
            av.BringToFront();
        }
        ///<summary>ctrFakturierung / tsbtnLager_Click</summary>
        ///<remarks>Abrechnungsmodus Lager</remarks>
        private void tsbtnLager_Click(object sender, EventArgs e)
        {
            _CalcMode = 2;
            InitCtr();
        }
        ///<summary>ctrFakturierung / tsbtnSped_Click</summary>
        ///<remarks>Abrechnungsmodus Spedition</remarks>
        private void tsbtnSped_Click(object sender, EventArgs e)
        {
            _CalcMode = 1;
            InitCtr();
        }








































    }
}
