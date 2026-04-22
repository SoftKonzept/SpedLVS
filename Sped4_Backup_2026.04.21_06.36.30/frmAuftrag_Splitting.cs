using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace Sped4
{
    public partial class frmAuftrag_Splitting : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        frmAuftragView av;
        //bool drag = false;
        //internal clsAuftrag Auftrag;
        internal clsTour Tour;

        public decimal AuftragID;
        public Int32 Status;
        public ctrSUList ctrSUListe;

        ctrAufträge ctrAuftrag = new ctrAufträge();
        public bool inputOK = true;     // Dateneingabe OK?
        public bool DateOK = true;      // Termine (Liefertermin und VSB) dürfen die Daten im Hauptauftrag nicht überschreiten

        public DataTable ArtikelTable = new DataTable("Artikel");

        /***************************************************************************************
         *                              Methoden/Procedure
         * ************************************************************************************/
        ///<summary>frmAuftrag_Splitting/ frmAuftrag_Splitting</summary>
        ///<remarks></remarks>
        public frmAuftrag_Splitting(ctrAufträge _ctrAuftrag)
        {
            InitializeComponent();
            ctrAuftrag = _ctrAuftrag;
            dtpPosLieferTermin.MinDate = clsSystem.const_DefaultDateTimeValue_Min;
            dtpPosLieferTermin.MaxDate = clsSystem.const_DefaultDateTimeValue_Max;
            dtpPosVSB.MinDate = clsSystem.const_DefaultDateTimeValue_Min;
            dtpPosVSB.MaxDate = clsSystem.const_DefaultDateTimeValue_Max;
        }
        ///<summary>frmAuftrag_Splitting/ frmAuftrag_Splitting</summary>
        ///<remarks></remarks>
        private void frmAuftrag_Splitting_Load(object sender, EventArgs e)
        {
            Tour = ctrAuftrag.Tour;
            if (this.Tour.Auftrag.ID > 0)
            {
                this.Text = "Auftrag #: [" + this.Tour.Auftrag.ANr.ToString() + "]";
                InitForm();

                // //------ AuftragsView wird geladen in panAuftragView
                frmAuftragView _av = new frmAuftragView();
                _av.SplitContainerCollaped(true);
                _av.Tour = this.Tour;
                _av._ctrMenu = ctrAuftrag._ctrMenu;
                av = _av;
                ////av._AuftragPosTableID = this.AuftragPosTableID;
                //av._ctrAuftrag = ctrAuftrag;
                av._AuftragSplit = this;
                //av._ctrSUListe = ctrSUListe;
                av.CanDoDragDrop = true;
                av.FormBorderStyle = FormBorderStyle.None;
                av.TopLevel = false;
                av.Dock = DockStyle.Fill;
                av.Parent = this.panAuftragView;
                av.tsbtnArtikelDetails.Enabled = false;
                av.Show();
                av.BringToFront();
            }
            else
            {
                this.Close();
            }
        }
        ///<summary>frmAuftrag_Splitting/ tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///<summary>frmAuftrag_Splitting/ InitForm</summary>
        ///<remarks></remarks>
        private void InitForm()
        {
            Tour.Auftrag.Fill();
            Tour.Auftrag.InitADRinClass();
            ArtikelTable.Rows.Clear();
            gbSplitting.Enabled = false;
            CheckSelectedArtForNewAuftragPos();
            SetAuftragDatenToFrm();
            InitDGVAuftragPos();
        }
        ///<summary>frmAuftrag_Splitting/ InitForm</summary>
        ///<remarks></remarks>
        private void SetAuftragDatenToFrm()
        {
            string strZF = string.Empty;
            tbAuftragID.Text = Tour.Auftrag.ANr.ToString() + " / " + Tour.Auftrag.AuftragPos.AuftragPos.ToString();
            tbAuftragsdatum.Text = Tour.Auftrag.ADate.ToShortDateString();
            tbAuftraggeber.Text = Tour.Auftrag.adrAuftraggeber.Name1;
            tbVersender.Text = Tour.Auftrag.adrBS.Name1 + "-" + Tour.Auftrag.adrBS.PLZ + " " + Tour.Auftrag.adrBS.Ort;
            tbEmpfaenger.Text = Tour.Auftrag.adrES.Name1 + "-" + Tour.Auftrag.adrES.PLZ + " " + Tour.Auftrag.adrES.Ort;
            tbLiefertermin.Text = Tour.Auftrag.AuftragPos.LieferTermin.ToShortDateString();



            DateTime ZF = Tour.Auftrag.AuftragPos.LadeZF;

            if (Convert.ToInt32(ZF.Hour.ToString()) < 10)
            {
                strZF = "0" + ZF.Hour.ToString();
                numLadeHour.Value = Convert.ToInt32(strZF);
            }
            else
            {
                strZF = ZF.Hour.ToString();
                numLadeHour.Value = Convert.ToInt32(strZF);
            }
            if (Convert.ToInt32(ZF.Minute.ToString()) < 10)
            {
                strZF = strZF + ":0" + ZF.Minute.ToString();
                numLadeMin.Value = Convert.ToInt32(ZF.Minute.ToString());
            }
            else
            {
                strZF = strZF + ":" + ZF.Minute.ToString();
                numLadeMin.Value = Convert.ToInt32(ZF.Minute.ToString());
            }

            tbZF.Text = strZF;
            tbVSB.Text = Tour.Auftrag.AuftragPos.VSB.ToShortDateString();
            tbLadeNr.Text = Tour.Auftrag.AuftragPos.Ladenummer;
            Status = Tour.Auftrag.AuftragPos.Status;
            tbPosLadenummer.Text = tbLadeNr.Text;
            tbGesamtgewicht.Text = Functions.FormatDecimal(Tour.Auftrag.BruttoGesamtgewicht);
            tbPosGewicht.Text = Functions.FormatDecimal(0);
            tbRestgewicht.Text = Functions.FormatDecimal(0);
            tbPosGewichtAlt.Text = Functions.FormatDecimal(Tour.Auftrag.AuftragPos.BruttoSumme);
        }
        ///<summary>frmAuftrag_Splitting/ SetAuftragDatenForNewAuftragPos</summary>
        ///<remarks></remarks>
        private void SetAuftragDatenForNewAuftragPos()
        {
            //neue AuftragPos ID ermitteln
            tbAuftragPos.Text = this.Tour.Auftrag.AuftragPos.NextAufragPos.ToString();
            //Auftragdaten, die übernommen werden können 

            //DateTime dtTmp = Convert.ToDateTime(tbVSB.Text);
            //if (dtTmp >= dtpPosVSB.MaxDate)
            //{
            //    dtpPosVSB.Value = dtpPosVSB.MaxDate;
            //}
            //else
            //{
            //    dtpPosVSB.Value = Convert.ToDateTime(tbVSB.Text);
            //}
            dtpPosVSB.Value = Tour.Auftrag.AuftragPos.VSB;
            dtpPosVSB.Enabled = !(Globals.DefaultDateTimeMinValue == Tour.Auftrag.AuftragPos.VSB);

            //Ladetermin            
            dtpPosLadeTermin.Value = Tour.Auftrag.AuftragPos.LadeTermin;
            dtpPosLadeTermin.Enabled = !(Globals.DefaultDateTimeMinValue == Tour.Auftrag.AuftragPos.LadeTermin);
            //Liefertermnin            
            //DateTime dt1Tmp = Convert.ToDateTime(tbLiefertermin.Text);
            //if (dt1Tmp >= dtpPosLieferTermin.MaxDate)
            //{
            //    dtpPosLieferTermin.Value = dtpPosLieferTermin.MaxDate;
            //}
            //else
            //{
            //    dtpPosLieferTermin.Value = Convert.ToDateTime(tbLiefertermin.Text);
            //}            
            dtpPosLieferTermin.Value = Tour.Auftrag.AuftragPos.LieferTermin;
            dtpPosLieferTermin.Enabled = !(Globals.DefaultDateTimeMaxValue == dtpPosLieferTermin.Value);

            tbPosLadenummer.Text = this.Tour.Auftrag.AuftragPos.Ladenummer;
            tbPosGewicht.Text = Functions.FormatDecimal(0);
        }
        ///<summary>frmAuftrag_Splitting/ InitDGVArtikel</summary>
        ///<remarks></remarks>
        public void InitDGVArtikel()
        {
            ArtikelTable = clsArtikel.GetDataTableForArtikelGrdSplitting(this.GL_User, this.Tour.Auftrag.AuftragPos.ID);
            dgvArtikel.DataSource = ArtikelTable;
            for (Int32 i = 0; i <= dgvArtikel.Columns.Count - 1; i++)
            {
                string strCol = dgvArtikel.Columns[i].Name;
                switch (strCol)
                {
                    case "AuftragPosTableID":
                        dgvArtikel.Columns[i].IsVisible = false;
                        break;

                    default:
                        dgvArtikel.Columns[i].IsVisible = true;
                        break;
                }
            }
            this.dgvArtikel.BestFitColumns();
            //Artikelselect
            if (dgvArtikel.Rows.Count > 0)
            {
                string strArtikelID = this.dgvArtikel.Rows[0].Cells["ID"].Value.ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strArtikelID, out decTmp);
                if (decTmp > 0)
                {
                    this.Tour.Auftrag.AuftragPos.Artikel.ID = decTmp;
                    this.Tour.Auftrag.AuftragPos.Artikel.GetArtikeldatenByTableID();
                }
            }
        }
        ///<summary>frmAuftrag_Splitting/ InitDGVAuftragPos</summary>
        ///<remarks></remarks>
        private void InitDGVAuftragPos()
        {
            Tour.Auftrag.GetTableAuftragPosByAuftrag();
            this.dgvAuftragPos.DataSource = Tour.Auftrag.dtAuftragPositonByAuftrag;
            for (Int32 i = 0; i <= dgvAuftragPos.Columns.Count - 1; i++)
            {
                string strCol = dgvAuftragPos.Columns[i].Name;
                switch (strCol)
                {
                    case "colStatus":
                        dgvAuftragPos.Columns[i].IsVisible = true;
                        dgvAuftragPos.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        break;

                    case "AuftragPos":
                        dgvAuftragPos.Columns[i].IsVisible = true;
                        dgvAuftragPos.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        dgvAuftragPos.Columns[i].HeaderText = "Pos #";
                        break;

                    case "Gewicht":
                        dgvAuftragPos.Columns[i].IsVisible = true;
                        dgvAuftragPos.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                        dgvAuftragPos.Columns[i].HeaderText = "Gewicht [kg]";
                        break;

                    default:
                        dgvAuftragPos.Columns[i].IsVisible = false;
                        break;
                }
            }
            this.dgvAuftragPos.BestFitColumns();
        }
        ///<summary>frmAuftrag_Splitting/ dgvAuftragPos_CellFormatting</summary>
        ///<remarks></remarks>
        private void dgvAuftragPos_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (this.dgvAuftragPos.Rows.Count > 0)
            {
                string ColName = e.Column.Name.ToString();
                switch (ColName)
                {
                    case "colStatus":
                        //Status
                        Int32 iStatus = 0;
                        string strStatus = e.CellElement.RowInfo.Cells["Status"].Value.ToString();
                        Int32.TryParse(strStatus, out iStatus);
                        e.CellElement.Image = Functions.GetDataGridCellStatusImage(iStatus);
                        e.Row.Height = 35;
                        break;
                }
            }
        }
        ///<summary>frmAuftrag_Splitting/ dgvAuftragPos_CellFormatting</summary>
        ///<remarks>ein-/ausblenden der Auftragspositionsliste</remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.splitPanel1.Collapsed)
            {
                this.tsbtnShowHideDGVAuftragPos.Image = Sped4.Properties.Resources.layout_left;
            }
            else
            {
                this.tsbtnShowHideDGVAuftragPos.Image = Sped4.Properties.Resources.layout;
            }
        }
        ///<summary>frmAuftrag_Splitting/ dgvArtikel_MouseClick</summary>
        ///<remarks></remarks>
        private void dgvArtikel_MouseClick(object sender, MouseEventArgs e)
        {

        }
        ///<summary>frmAuftrag_Splitting/ dgvArtikel_ContextMenuOpening</summary>
        ///<remarks></remarks>
        private void dgvArtikel_ContextMenuOpening(object sender, Telerik.WinControls.UI.ContextMenuOpeningEventArgs e)
        {
            if (this.dgvArtikel.Rows.Count > 0)
            {
                bool bSelect = (bool)this.dgvArtikel.Rows[this.dgvArtikel.CurrentRow.Index].Cells["Select"].Value;
                if (bSelect)
                {
                    RadMenuSeparatorItem separator;
                    RadMenuItem customMenuItem;
                    if (this.dgvArtikel.SelectedRows.Count > 0)
                    {
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Artikelsplitt";
                        customMenuItem.Click += new EventHandler(OpenFrmMengenAenderung);
                        e.ContextMenu.Items.Add(customMenuItem);

                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Artikel auflösen";
                        customMenuItem.Click += new EventHandler(MergeArtikel);
                        e.ContextMenu.Items.Add(customMenuItem);
                    }
                }
                else
                {
                    string strError = "Es wurde kein Artikel selektiert!";
                    clsMessages.Allgemein_ERRORTextShow(strError);
                }
            }
        }
        ///<summary>frmAuftrag_Splitting/ MergeArtikel</summary>
        ///<remarks></remarks>
        private void MergeArtikel(object sender, EventArgs e)
        {
            if (this.Tour.Auftrag.AuftragPos.Artikel.ID > 0)
            {
                this.Tour.Auftrag.AuftragPos.Fill();
                //Es müsse mindestens zwei Artikel vorhanden sein
                if (this.Tour.Auftrag.AuftragPos.dtAuftrPosArtikel.Rows.Count > 1)
                {
                    string strInfo = "Es wurde kein Artikel mit der passenden Güterart gefunde. Der Vorgang kann nicht durchgeführt werden!";
                    //Artikeltable sortieren,damit der kleinste ID ermittelt werden kann
                    this.Tour.Auftrag.AuftragPos.dtAuftrPosArtikel.DefaultView.Sort = "ID";
                    DataTable dtTmp = this.Tour.Auftrag.AuftragPos.dtAuftrPosArtikel.DefaultView.ToTable();
                    foreach (DataRow row in dtTmp.Rows)
                    {
                        decimal decTmp = 0;
                        Decimal.TryParse(row["ID"].ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            clsArtikel ArtToMerge = new clsArtikel();
                            ArtToMerge.InitClass(this.GL_User, this.ctrAuftrag._ctrMenu._frmMain.GL_System);
                            ArtToMerge.ID = decTmp;
                            ArtToMerge.GetArtikeldatenByTableID();
                            //vergleich Güterart (nur die gleiche Güterart darf zusammengelegt werden
                            if (
                                (this.Tour.Auftrag.AuftragPos.Artikel.ID != ArtToMerge.ID) &&
                                (this.Tour.Auftrag.AuftragPos.Artikel.GArtID == ArtToMerge.GArtID)
                                )
                            {
                                //Anzahl, Brutto, Menge addieren
                                ArtToMerge.Brutto = ArtToMerge.Brutto + this.Tour.Auftrag.AuftragPos.Artikel.Brutto;
                                ArtToMerge.Netto = ArtToMerge.Netto + this.Tour.Auftrag.AuftragPos.Artikel.Netto;
                                ArtToMerge.Anzahl = ArtToMerge.Anzahl + this.Tour.Auftrag.AuftragPos.Artikel.Anzahl;

                                //Transaktion mit Update und Delete des Artikels
                                if (this.Tour.Auftrag.AuftragPos.Artikel.DoArtikelMerge(ref ArtToMerge))
                                {
                                    strInfo = string.Empty;
                                }
                                else
                                {
                                    strInfo = "Der Vorgang konnte nicht erfolgreich durchgeführt!";
                                }
                                //Info String
                                break;
                            }
                        }
                    }
                    //Info Merge ok
                    if (!strInfo.Equals(string.Empty))
                    {
                        clsMessages.Allgemein_ERRORTextShow(strInfo);
                    }
                    ReloadAuftragPosValue(this.Tour.Auftrag.AuftragPos.ID);
                }
            }
        }
        ///<summary>frmAuftrag_Splitting/ dgvAuftragPos_ContextMenuOpening</summary>
        ///<remarks></remarks>
        private void dgvAuftragPos_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            if (this.dgvAuftragPos.Rows.Count > 0)
            {
                string strAuftragPosTableID = this.dgvAuftragPos.Rows[this.dgvAuftragPos.CurrentRow.Index].Cells["ID"].Value.ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strAuftragPosTableID.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    Tour.Auftrag.AuftragPos.ID = decTmp;
                    Tour.Auftrag.AuftragPos.Fill();
                }
                // Eine Auftragposition muss stehen bleiben
                if (this.Tour.Auftrag.AuftragPos.AuftragPos > 0)
                {
                    RadMenuSeparatorItem separator;
                    RadMenuItem customMenuItem;
                    if (this.dgvAuftragPos.SelectedRows.Count > 0)
                    {
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Auftragposition auflösen";
                        customMenuItem.Click += new EventHandler(DoCancelAuftragPos);
                        e.ContextMenu.Items.Add(customMenuItem);
                    }
                }
            }
        }
        ///<summary>frmAuftrag_Splitting/ DoMenuAuftragPosAction</summary>
        ///<remarks></remarks>
        private void DoCancelAuftragPos(object sender, EventArgs e)
        {
            if (clsMessages.Auftragsplitting_CancelAuftragPos())
            {
                ArtikelTable.DefaultView.RowFilter = string.Empty;
                //Artikel für das Update zusammenstelle in Liste
                List<decimal> listArtikelID = GetArtikelIDListFromTable(ArtikelTable.DefaultView.ToTable(true, "ID"));
                this.Tour.Auftrag.AuftragPos.DoCancelAuftragPos(listArtikelID);
                InitForm();
            }
        }
        ///<summary>frmAuftrag_Splitting/ OpenFrmMengenAenderung</summary>
        ///<remarks></remarks>
        private void OpenFrmMengenAenderung(object sender, EventArgs e)
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmAMengeChange)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmAMengeChange));
            }
            frmAMengeChange Menge = new frmAMengeChange();
            Menge.frmAuftragSplitting = this;
            Menge.StartPosition = FormStartPosition.CenterScreen;
            Menge.Show();
            Menge.BringToFront();
        }
        ///<summary>frmAuftrag_Splitting/ CheckSelectedArtForNewAuftragPos</summary>
        ///<remarks></remarks>
        private void CheckSelectedArtForNewAuftragPos()
        {
            DataTable dtCheck = ArtikelTable.Copy();
            object CountObj = new object();
            if (dtCheck.Rows.Count > 0)
            {
                CountObj = dtCheck.Compute("Count(Select)", "Select=1");
            }
            Int32 iTmp = 0;
            Int32.TryParse(CountObj.ToString(), out iTmp);
            this.tsbSpeichern.Enabled = (iTmp > 0);
        }
        ///<summary>frmAuftrag_Splitting/ dgvArtikel_CellClick</summary>
        ///<remarks></remarks>
        private void dgvArtikel_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.dgvArtikel.Rows[e.RowIndex] != null)
                {
                    //nur in der Spalte Select
                    if (this.dgvArtikel.Columns[e.ColumnIndex].HeaderText == "Select")
                    {
                        bool bSelect = (bool)this.dgvArtikel.Rows[e.RowIndex].Cells["Select"].Value;
                        this.dgvArtikel.Rows[e.RowIndex].Cells["Select"].Value = (!bSelect);
                        CheckSelectedArtForNewAuftragPos();
                        //Artikel ermitteln
                        string strArtikelID = this.dgvArtikel.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        decimal decTmp = 0;
                        Decimal.TryParse(strArtikelID, out decTmp);
                        if (decTmp > 0)
                        {
                            this.Tour.Auftrag.AuftragPos.Artikel.ID = decTmp;
                            this.Tour.Auftrag.AuftragPos.Artikel.GetArtikeldatenByTableID();

                            //neue AuftragPos Nr setzen
                            tbAuftragPos.Text = this.Tour.Auftrag.AuftragPos.NextAufragPos.ToString();
                            decTmp = 0;
                            object objBrutto;
                            objBrutto = ArtikelTable.Compute("SUM(Brutto)", "Select=1");
                            Decimal.TryParse(objBrutto.ToString(), out decTmp);
                            tbPosGewicht.Text = Functions.FormatDecimal(decTmp);
                        }
                    }
                }
            }
        }
        ///<summary>frmAuftrag_Splitting/ tsbSpeichern_Click</summary>
        ///<remarks>Neue Auftragsposition speichern</remarks>
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            //prüfen, ob Artikel ausgewählt wurden
            DoSplitting();
            InitForm();
        }
        ///<summary>frmAuftrag_Splitting/ dgvAuftragPos_CellClick</summary>
        ///<remarks></remarks>
        private void dgvAuftragPos_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //AuftragPos ermitteln
            if (this.dgvAuftragPos.Rows.Count > 0)
            {
                string strAuftragPosTableID = this.dgvAuftragPos.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strAuftragPosTableID.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    ReloadAuftragPosValue(decTmp);
                }
            }
        }
        ///<summary>frmAuftrag_Splitting/ GetArtikelIDListFromTable</summary>
        ///<remarks></remarks>
        private void ReloadAuftragPosValue(decimal myAuftragPosID)
        {
            Tour.Auftrag.AuftragPos.ID = myAuftragPosID;
            Tour.Auftrag.AuftragPos.Fill();
            SetAuftragDatenToFrm();
            InitDGVArtikel();
            gbSplitting.Enabled = true;
            tsbSpeichern.Enabled = true;
        }
        ///<summary>frmAuftrag_Splitting/ GetArtikelIDListFromTable</summary>
        ///<remarks></remarks>
        private List<decimal> GetArtikelIDListFromTable(DataTable dt)
        {
            List<decimal> listArtikelID = new List<decimal>();
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                string strTmp = dt.Rows[i]["ID"].ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    listArtikelID.Add(decTmp);
                }
            }
            return listArtikelID;
        }
        ///<summary>frmAuftrag_Splitting/ DoSplitting</summary>
        ///<remarks>Der eigentliche Splittauftrag wird durchgeführt</remarks>
        private void DoSplitting()
        {
            ArtikelTable.DefaultView.RowFilter = "Select=1";
            //Artikel für das Update zusammenstelle in Liste
            List<decimal> listArtikelID = GetArtikelIDListFromTable(ArtikelTable.DefaultView.ToTable(true, "ID"));
            ArtikelTable.DefaultView.RowFilter = string.Empty;

            //es muss mindestens ein Artikel ausgewählt sein
            if (listArtikelID.Count > 0)
            {
                //neu AuftragPos initialisieren
                clsAuftragPos APNeu = this.Tour.Auftrag.AuftragPos.Copy();
                //Datensatzwerte neu setzen
                APNeu.ID = 0;
                APNeu.AuftragPos = APNeu.NextAufragPos;

                APNeu.LieferTermin = dtpPosLieferTermin.Value;
                APNeu.VSB = dtpPosVSB.Value;
                APNeu.LadeZF = Functions.GetStrTimeZF(numLadeHour, numLadeMin);
                APNeu.Ladenummer = tbPosLadenummer.Text;
                //Baustelle
                //CheckFunktion Status einbauen
                APNeu.Status = Status;
                APNeu.DoAuftragPosSplitt(listArtikelID);
            }
            else
            {
                clsMessages.Auftragsplitting_NoArtikelSelected();
            }
        }
        ///<summary>frmAuftrag_Splitting/ dtpPosT_Date_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpPosT_Date_ValueChanged(object sender, EventArgs e)
        {
            if (dtpPosLieferTermin.Value < dtpPosVSB.Value)
            {
                dtpPosLieferTermin.Value = dtpPosVSB.Value;
            }
        }
        ///<summary>frmAuftrag_Splitting/ dtpPosVSB_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpPosVSB_ValueChanged(object sender, EventArgs e)
        {
            if (dtpPosVSB.Value > dtpPosLieferTermin.Value)
            {
                dtpPosVSB.Value = dtpPosLieferTermin.Value;
            }
        }
        ///<summary>frmAuftrag_Splitting/ tsbtnRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitForm();
        }
    }
}
