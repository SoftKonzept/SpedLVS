using LVS;
using Sped4.Classes;
using Sped4.Struct;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;


namespace Sped4
{
    public partial class ctrAufträge : UserControl
    {
        const string const_Filter_Auftraggeber = "FilterAuftraggeber";
        const string const_Filter_AuftragNr = "FilterAuftragNr";
        const string const_Filter_BLST = "FilterBLS";
        const string const_Filter_ELST = "FilterELS";
        const string const_Filter_Relation = "FilterRelation";

        public LVS.clsTour Tour;

        public Globals._GL_USER GL_User;
        public ctrSUList ctrSUListe;
        public ctrMenu _ctrMenu;
        public frmAuftragView _frmAV;
        internal bool bo_FirstLoad = true;
        internal bool bo_grdFormat = false;

        //++++++++++++++++++++++++++
        public Int32 listenArt = 1;
        // 1. Liste Wochenübersicht
        // 2. Liste Zeitraum
        // 3. disponiert
        // 4. durchgeführt
        // 5. Vergabe an SU
        // 6. Stornierte Aufträge
        // 7. alle Aufträge (egal welcher Status) als Übersicht

        //Recourcenanzeige für Checkbox Fahrer / Auflieger
        public bool ShowFahrer = false;
        public bool ShowAuflieger = false;
        public bool vergabe = false;
        public decimal MandantenID = 0;

        public DateTime SearchDateVon = DateTime.Today;//DateTime.MinValue;
        public DateTime SearchDateBis = DateTime.Today.AddDays(6);//DateTime.MaxValue;

        public DateTime DispoDatumStart = DateTime.Today.Date;
        public DateTime DispoDatumEnd = DateTime.Today.Date.AddDays(3);

        public delegate void ThreadCtrInvokeEventHandler();
        public delegate void ctrAuftragRefreshEventHandler();
        public event ctrAuftragRefreshEventHandler ctrAuftragRefresh;

        public delegate void DispoDataChangeEventHandler(ctrAufträge ctrAuftrag);
        public event DispoDataChangeEventHandler DispoDataChangedByCtrAuftrag;

        public delegate void OpenKalenerFrmEventHandler(frmDispoKalender _Kalender, DateTime _From, DateTime _To);
        public event OpenKalenerFrmEventHandler OpenKalender;

        public DataTable dtOrderList = new DataTable("Order");
        internal structAuftPosRow strAuftrPosRow;
        public frmAuftrag_Splitting Split;

        //Grid Angaben Breite / Höhe
        internal Int32 iCtrWidth = 0;
        internal Int32 iWStatus = 50;
        internal Int32 iWDetails = 50;
        internal Int32 iWBesch = 50;
        internal Int32 iWBesch2 = 50;
        internal Int32 iWWerte = 50;
        internal Int32 iWSpalte6 = 50;
        internal Int32 iWTermin = 50;
        internal Int32 iWBearbeitung = 50;
        internal Int32 iWDruck = 50;
        internal Int32 RowHigh = 50;
        internal decimal decStoredFontSize = 0.0M;

        internal decimal decLastSelected = 0;
        List<string> ListArtTableColumns;


        /*******************************************************************************
         *                      Methoden / Procedure
         * ****************************************************************************/
        ///<summary>ctrAufträge/ OpenFrmLogin</summary>
        ///<remarks></remarks> 
        public ctrAufträge()
        {
            InitializeComponent(); ;
            this.ctrAuftragRefresh += new ctrAuftragRefreshEventHandler(InitDGV);
        }
        ///<summary>ctrAufträge/ ctrAufträge_Load</summary>
        ///<remarks></remarks> 
        private void ctrAufträge_Load(object sender, EventArgs e)
        {
            Tour = new LVS.clsTour();
            Tour.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);

            //eingestellte Schriftgröße User
            if (GL_User.us_decFontSize >= this.nudFontSize.Minimum)
            {
                this.nudFontSize.Value = GL_User.us_decFontSize;
            }
            LoadCBRelation();
            cbSkin.DataSource = Enum.GetNames(typeof(enumSkinAuftragsListe));
            tsmiStatusListe.Visible = false;
            InitDGV();
            this.bo_FirstLoad = false;
        }
        ///<summary>ctrAufträge/ AddDataTableColumn</summary>
        ///<remarks>Hinzufügen der sichtbaren Spalten für das Datagrid</remarks> 
        private void AddDataTableColumn(ref DataTable myDT)
        {
            myDT.Columns.Add("colStatus", typeof(System.Byte));
            myDT.Columns.Add("colInfo", typeof(System.Byte));
            myDT.Columns.Add("colBeschreibung", typeof(System.String));
            myDT.Columns.Add("colDaten", typeof(System.String));
            myDT.Columns.Add("colGewicht", typeof(System.String));
            myDT.Columns.Add("colBeschreibung2", typeof(System.String));
            myDT.Columns.Add("colLadeTermine", typeof(System.String));
            myDT.Columns.Add("colLieferTermine", typeof(System.String));
            myDT.Columns.Add("colBearbeitung", typeof(System.String));
        }
        ///<summary>ctrAufträge/ SetCtrWidth</summary>
        ///<remarks></remarks>
        private void SetCtrWidth()
        {
            iCtrWidth = 0;
            foreach (GridViewColumn col in this.dgv.Columns)
            {
                switch (col.Name)
                {
                    case "colStatus":
                    case "colInfo":
                    case "colBeschreibung":
                    case "colDaten":
                    case "colGewicht":
                    case "colBeschreibung2":
                    case "colLadeTermine":
                    case "colLieferTermine":
                    case "colBearbeitung":
                        iCtrWidth = iCtrWidth + col.Width;
                        break;
                    default:
                        ;
                        break;
                }
            }
            this.Width = this.iCtrWidth + 50;
            //this.Refresh();
        }
        ///<summary>ctrAufträge/ InitDGV</summary>
        ///<remarks>Füllt das Datagrid</remarks> 
        public void InitDGV()
        {
            this.Tour.GetOrder(this.listenArt, this.SearchDateVon, this.SearchDateBis);
            this.afColorLabel1.myText = this.Tour.CtrTxt;
            dtOrderList = this.Tour.dtOrderList;
            AddDataTableColumn(ref dtOrderList);

            //Datasource des Datagrids und Zuweisung
            this.dgv.MasterTemplate.Templates.Clear();
            this.dgv.DataSource = dtOrderList;

            iCtrWidth = 0;
            foreach (GridViewColumn col in this.dgv.Columns)
            {
                switch (col.Name)
                {
                    case "colStatus":
                        col.HeaderText = "Status";
                        col.ImageLayout = ImageLayout.Center;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        iCtrWidth = iCtrWidth + col.Width;
                        break;
                    case "colInfo":
                        col.HeaderText = "Info";
                        col.ImageLayout = ImageLayout.Center;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        break;
                    case "colBeschreibung":
                        col.HeaderText = string.Empty;
                        col.TextAlignment = ContentAlignment.TopLeft;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        iCtrWidth = iCtrWidth + col.Width;
                        break;
                    case "colDaten":
                        col.HeaderText = "Daten";
                        col.TextAlignment = ContentAlignment.TopLeft;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        iCtrWidth = iCtrWidth + col.Width;
                        break;
                    case "colGewicht":
                        col.HeaderText = "Gewicht";
                        col.TextAlignment = ContentAlignment.TopLeft;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        iCtrWidth = iCtrWidth + col.Width;
                        break;
                    case "colBeschreibung2":
                        col.HeaderText = string.Empty;
                        col.TextAlignment = ContentAlignment.TopLeft;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        iCtrWidth = iCtrWidth + col.Width;
                        break;
                    case "colLadeTermine":
                        col.HeaderText = "[Be]-Termine";
                        col.TextAlignment = ContentAlignment.TopLeft;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        iCtrWidth = iCtrWidth + col.Width;
                        break;
                    case "colLieferTermine":
                        col.HeaderText = "[Ent]-Termine";
                        col.TextAlignment = ContentAlignment.TopLeft;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        iCtrWidth = iCtrWidth + col.Width;
                        break;
                    case "colBearbeitung":
                        col.HeaderText = "Bearbeitung";
                        col.TextAlignment = ContentAlignment.TopLeft;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        iCtrWidth = iCtrWidth + col.Width;
                        break;
                    default:
                        col.IsVisible = false;
                        break;
                }
            }
            this.dgv.BestFitColumns();

            //////Artikel Template
            ListArtTableColumns = new List<string>();
            GridViewTemplate tmpArt = new GridViewTemplate();
            tmpArt.DataSource = this.Tour.dtOrderListArtikel;
            dgv.MasterTemplate.Templates.Add(tmpArt);
            foreach (GridViewColumn colArt in tmpArt.Columns)
            {
                ListArtTableColumns.Add(colArt.Name);
                switch (colArt.Name)
                {
                    //case "ID":
                    case "AuftragPosTableID":
                        colArt.IsVisible = false;
                        break;
                    case "Laenge":
                    case "Dicke":
                    case "Breite":
                    case "Brutto":
                        GridViewDecimalColumn tmpCol = colArt as GridViewDecimalColumn;
                        if (tmpCol is GridViewDecimalColumn)
                        {
                            tmpCol.IsVisible = true;
                            tmpCol.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            tmpCol.FormatString = "{0:n}";
                        }
                        break;
                    default:
                        colArt.IsVisible = true;
                        colArt.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        break;
                }
            }
            tmpArt.BestFitColumns();

            //////Setzt die Verknüpfung zwischen den Templates            
            GridViewRelation relArtikel = new GridViewRelation(dgv.MasterTemplate);
            relArtikel.ChildTemplate = tmpArt;
            relArtikel.RelationName = "Artikel";
            relArtikel.ParentColumnNames.Add("ID");
            relArtikel.ChildColumnNames.Add("AuftragPosTableID");
            dgv.Relations.Add(relArtikel);
            //CtrBreite setzen
            SetCtrWidth();
        }
        ///<summary>ctrAufträge/ dgv_CellFormatting</summary>
        ///<remarks></remarks> 
        private void dgv_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                try
                {
                    string ColName = e.Column.Name.ToString();
                    string strSelSkin = cbSkin.Text.ToString();
                    if (e.CellElement.Value != null)
                    {
                        switch (ColName)
                        {
                            case "colStatus":
                                Int32 iStatus = 0;
                                string strStatus = e.CellElement.RowInfo.Cells["Status"].Value.ToString();
                                Int32.TryParse(strStatus, out iStatus);

                                bool bPrio = false;
                                Boolean.TryParse(e.CellElement.RowInfo.Cells["Prio"].Value.ToString(), out bPrio);
                                e.CellElement.Image = Functions.GetDataGridCellStatusImage(iStatus);
                                //HIntergrund 
                                if ((bPrio) & (iStatus <= 2))
                                {
                                    e.CellElement.DrawFill = true;
                                    e.CellElement.NumberOfColors = 1;
                                    e.CellElement.BackColor = Color.Red;
                                }
                                else
                                {
                                    e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, Telerik.WinControls.ValueResetFlags.Local);
                                    e.CellElement.ResetValue(LightVisualElement.ForeColorProperty, Telerik.WinControls.ValueResetFlags.Local);
                                    e.CellElement.ResetValue(LightVisualElement.NumberOfColorsProperty, Telerik.WinControls.ValueResetFlags.Local);
                                    e.CellElement.ResetValue(LightVisualElement.BackColorProperty, Telerik.WinControls.ValueResetFlags.Local);
                                }
                                break;
                            case "colInfo":
                                ////Scan
                                bool bScan = false;
                                bool.TryParse(e.CellElement.RowInfo.Cells["Scan"].Value.ToString(), out bScan);
                                ////READ
                                bool bRead = false;
                                bool.TryParse(e.CellElement.RowInfo.Cells["Read"].Value.ToString(), out bRead);
                                e.CellElement.Image = Functions.GetInfoImage(bRead, bScan);
                                break;

                            case "colBeschreibung":
                                switch (strSelSkin)
                                {
                                    case "Details":
                                        e.CellElement.Value = "Auftrag/Pos : " +
                                                                Environment.NewLine + "Auftraggeber : " +
                                                                Environment.NewLine +
                                                                Environment.NewLine + "Versender : " +
                                                                Environment.NewLine +
                                                                Environment.NewLine + "Empfänger: " +
                                                                Environment.NewLine;
                                        e.CellElement.Image = null;
                                        break;
                                    case "Standard":
                                        e.CellElement.Value = "Auftrag/Pos : " +
                                                                Environment.NewLine +
                                                                Environment.NewLine + "Versender : " +
                                                                Environment.NewLine + "Empfänger: ";
                                        e.CellElement.Image = null;
                                        break;
                                    default:
                                        e.CellElement.Value = "DEFAULT";
                                        e.CellElement.Image = null;
                                        break;

                                }
                                break;

                            case "colDaten":
                                switch (strSelSkin)
                                {
                                    case "Details":
                                        if (this.GL_User.IsAdmin)
                                        {
                                            e.CellElement.Value = e.CellElement.RowInfo.Cells["AuftragID"].Value.ToString() + " / " + e.CellElement.RowInfo.Cells["AuftragPos"].Value.ToString() + " [" + e.CellElement.RowInfo.Cells["AuftragTableID"].Value.ToString() + "/" + e.CellElement.RowInfo.Cells["ID"].Value.ToString() + "]" +
                                                                  Environment.NewLine + e.CellElement.RowInfo.Cells["DetailADR_A"].Value.ToString() +
                                                                  Environment.NewLine + e.CellElement.RowInfo.Cells["DetailADR_V"].Value.ToString() +
                                                                  Environment.NewLine + e.CellElement.RowInfo.Cells["DetailADR_E"].Value.ToString();
                                        }
                                        else
                                        {
                                            e.CellElement.Value = e.CellElement.RowInfo.Cells["AuftragID"].Value.ToString() + " / " + e.CellElement.RowInfo.Cells["AuftragPos"].Value.ToString() +
                                                                  Environment.NewLine + e.CellElement.RowInfo.Cells["DetailADR_A"].Value.ToString() +
                                                                  Environment.NewLine + e.CellElement.RowInfo.Cells["DetailADR_V"].Value.ToString() +
                                                                  Environment.NewLine + e.CellElement.RowInfo.Cells["DetailADR_E"].Value.ToString();
                                        }

                                        e.CellElement.Image = null;
                                        break;
                                    case "Standard":
                                        if (this.GL_User.IsAdmin)
                                        {
                                            e.CellElement.Value = e.CellElement.RowInfo.Cells["AuftragID"].Value.ToString() + " / " + e.CellElement.RowInfo.Cells["AuftragPos"].Value.ToString() + " [" + e.CellElement.RowInfo.Cells["AuftragTableID"].Value.ToString() + "/" + e.CellElement.RowInfo.Cells["ID"].Value.ToString() + "]" +
                                                                   Environment.NewLine +
                                                                   Environment.NewLine + e.CellElement.RowInfo.Cells["StandardADR_V"].Value.ToString() +
                                                                   Environment.NewLine + e.CellElement.RowInfo.Cells["StandardADR_E"].Value.ToString();
                                        }
                                        else
                                        {
                                            e.CellElement.Value = e.CellElement.RowInfo.Cells["AuftragID"].Value.ToString() + " / " + e.CellElement.RowInfo.Cells["AuftragPos"].Value.ToString() +
                                                                   Environment.NewLine +
                                                                   Environment.NewLine + e.CellElement.RowInfo.Cells["StandardADR_V"].Value.ToString() +
                                                                   Environment.NewLine + e.CellElement.RowInfo.Cells["StandardADR_E"].Value.ToString();
                                        }
                                        e.CellElement.Image = null;
                                        break;
                                    default:
                                        e.CellElement.Value = "DEFAULT";
                                        e.CellElement.Image = null;
                                        break;

                                }
                                break;

                            case "colGewicht":
                                decimal decBrutto = 0;
                                Decimal.TryParse(e.CellElement.RowInfo.Cells["Brutto"].Value.ToString(), out decBrutto);
                                decimal decGesamtBrutto = 0;
                                Decimal.TryParse(e.CellElement.RowInfo.Cells["Gesamtgewicht"].Value.ToString(), out decGesamtBrutto);

                                DateTime LTermin3 = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["LieferTermin"].Value.ToString(), out LTermin3);
                                TimeSpan dtSpan3 = LTermin3 - DateTime.Now;

                                e.CellElement.Value = Functions.FormatDecimal(decBrutto) + " [kg]" +                               // Restgewicht nach Auftragsplitting des Auftrags
                                                                   Environment.NewLine + "(" + Functions.FormatDecimal(decGesamtBrutto) + "[kg])" +       // Gewicht ist das Gesamte Auftragsgewicht
                                                                   Environment.NewLine + "Gut: " + e.CellElement.RowInfo.Cells["Gut"].Value.ToString() +
                                                                   Environment.NewLine + "Lade #: " + e.CellElement.RowInfo.Cells["Ladenummer"].Value.ToString();
                                e.CellElement.ForeColor = Functions.GetColorByDringlichkeit(dtSpan3);
                                e.CellElement.Image = null;
                                break;

                            case "colBeschreibung2":
                                DateTime LTermin4 = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["LieferTermin"].Value.ToString(), out LTermin4);
                                TimeSpan dtSpan4 = LTermin4 - DateTime.Now;
                                e.CellElement.Value = "KW_v/b: " + Environment.NewLine +
                                                      "VSB: " + Environment.NewLine +
                                                      "Termin: " + Environment.NewLine +
                                                      "ZF: " + Environment.NewLine +
                                                      "Ö-Zeit: " + Environment.NewLine;
                                e.CellElement.Image = null;
                                break;

                            case "colLadeTermine":
                                Int32 KWLade = 0;
                                Int32.TryParse(e.CellElement.RowInfo.Cells["vKW"].Value.ToString(), out KWLade);
                                string strKWLade = string.Empty;
                                if (KWLade > 0)
                                {
                                    strKWLade = KWLade.ToString();
                                }

                                DateTime VSB = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["VSB"].Value.ToString(), out VSB);
                                string strVSB = string.Empty;
                                if (VSB > (DateTime)Globals.DefaultDateTimeMinValue)
                                {
                                    strVSB = VSB.ToShortDateString();
                                }
                                DateTime LadeTermin = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["LadeTermin"].Value.ToString(), out LadeTermin);
                                string strLadeTermin = string.Empty;
                                if (LadeTermin > (DateTime)Globals.DefaultDateTimeMinValue)
                                {
                                    strLadeTermin = LadeTermin.ToShortDateString();
                                }
                                TimeSpan tpToLadeTermin = LadeTermin - DateTime.Now; // zur Ermittlung der Backcolor

                                DateTime LadeZF = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["LadeZF"].Value.ToString(), out LadeZF);
                                string strLadeZF = string.Empty;
                                if (LadeZF > (DateTime)Globals.DefaultDateTimeMinValue)
                                {
                                    strLadeZF = Functions.FormatToHHMM(LadeZF);
                                }

                                DateTime WAvon1 = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["WAvB"].Value.ToString(), out WAvon1);
                                DateTime WAbis1 = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["WAbB"].Value.ToString(), out WAbis1);
                                string strWAvon = Functions.FormatToHHMM(WAvon1);
                                string strWAbis = Functions.FormatToHHMM(WAbis1);
                                string strWA = string.Empty;
                                string strDef = "00:00";
                                if ((strDef != strWAvon) | (strDef != strWAbis))
                                {
                                    strWA = strWAvon + "-" + strWAbis;
                                }
                                else
                                {
                                    strWA = string.Empty;
                                }
                                e.CellElement.Value = strKWLade + Environment.NewLine + // 
                                                      strVSB + Environment.NewLine +   //VSB                                                    
                                                      strLadeTermin + Environment.NewLine + //LT
                                                      strLadeZF + Environment.NewLine +
                                                      strWA + Environment.NewLine;
                                e.CellElement.Image = null;
                                break;

                            case "colLieferTermine":
                                Int32 KWLiefer = 0;
                                Int32.TryParse(e.CellElement.RowInfo.Cells["bKW"].Value.ToString(), out KWLiefer);
                                string strKWLiefer = string.Empty;
                                if (KWLiefer > 0)
                                {
                                    strKWLiefer = KWLiefer.ToString();
                                }

                                DateTime LieferTermin = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["LieferTermin"].Value.ToString(), out LieferTermin);
                                string strLieferTermin = string.Empty;
                                if (LieferTermin < (DateTime)Globals.DefaultDateTimeMaxValue)
                                {
                                    strLieferTermin = LieferTermin.ToShortDateString();
                                }

                                DateTime LieferZF = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["LieferZF"].Value.ToString(), out LieferZF);
                                string strLieferZF = string.Empty;
                                if (LieferZF > (DateTime)Globals.DefaultDateTimeMinValue)
                                {
                                    strLieferZF = Functions.FormatToHHMM(LieferZF);
                                }
                                DateTime WAvonE = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["WAvE"].Value.ToString(), out WAvonE);
                                DateTime WAbisE = clsSystem.const_DefaultDateTimeValue_Min;
                                DateTime.TryParse(e.CellElement.RowInfo.Cells["WAbE"].Value.ToString(), out WAbisE);
                                string strWAvonE = Functions.FormatToHHMM(WAvonE);
                                string strWAbisE = Functions.FormatToHHMM(WAbisE);
                                string strWAE = string.Empty;
                                strDef = "00:00";
                                if ((strDef != strWAvonE) | (strDef != strWAbisE))
                                {
                                    strWAE = strWAvonE + "-" + strWAbisE;
                                }
                                else
                                {
                                    strWAE = string.Empty;
                                }
                                e.CellElement.Value = strKWLiefer + Environment.NewLine + //KW
                                                      " " + Environment.NewLine +   //VSB                                                    
                                                      strLieferTermin + Environment.NewLine + //LT
                                                      strLieferZF + Environment.NewLine +
                                                      strWAE + Environment.NewLine;
                                e.CellElement.Image = null;
                                break;
                            case "colBearbeitung":
                                ////Int32 iStatus1 = 0;
                                ////string strStatus1 = e.CellElement.RowInfo.Cells["Status"].Value.ToString();
                                ////Int32.TryParse(strStatus1, out iStatus);
                                ////bool bZFReq = false;
                                ////bool.TryParse(e.CellElement.RowInfo.Cells["LieferZFReq"].Value.ToString(), out bZFReq);
                                //if (iStatus1 >= 4)
                                //{
                                //    //DateTime BDatum = clsSystem.const_DefaultDateTimeValue_Min;
                                //    //if ((list.m_dt_B_Date != DateTime.MaxValue) | (list.m_dt_E_Date != DateTime.MaxValue))
                                //    //{
                                //    //    e.CellElement.Value = "Beladezeit:" + Environment.NewLine +
                                //    //                                        list.m_dt_B_Date.ToShortDateString() + " " +
                                //    //                                        list.m_dt_B_Date.ToShortTimeString() + Environment.NewLine +
                                //    //                                        "Entladezeit:" + Environment.NewLine +
                                //    //                                        list.m_dt_E_Date.ToShortDateString() + " " +
                                //    //                                        list.m_dt_E_Date.ToShortTimeString() + Environment.NewLine +
                                //    //                                        list.m_str_Resscource;
                                //    //}
                                //}
                                e.CellElement.Image = null;
                                break;

                            default:

                                break;
                        };
                        //anpassen der Schriftgröße
                        Font CellFont = new System.Drawing.Font(e.CellElement.Font.FontFamily, (float)nudFontSize.Value);
                        e.CellElement.Font = CellFont;
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
        }
        ///<summary>ctrAufträge/ dgv_RowFormatting</summary>
        ///<remarks></remarks> 
        private void dgv_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            //e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
            //e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
            //e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);

            if (e.RowElement.RowInfo.Cells["LieferTermin"] != null)
            {
                DateTime LTermin1 = (DateTime)Globals.DefaultDateTimeMaxValue;
                DateTime.TryParse(e.RowElement.RowInfo.Cells["LieferTermin"].Value.ToString(), out LTermin1);
                TimeSpan dtSpan1 = LTermin1 - DateTime.Now;
                e.RowElement.ForeColor = Functions.GetColorByDringlichkeit(dtSpan1);
                //e.RowElement.DrawFill = true;
                //e.RowElement.GradientStyle = GradientStyles.Solid;
                //e.RowElement.BackColor = Color.Aqua;
            }

        }
        ///<summary>ctrAufträge/ tsbRefresh_Click</summary>
        ///<remarks>Liste der Aufträge wird aktualisiert.</remarks> 
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            InitDGV();
        }
        ///<summary>ctrAufträge/ cbSkin_SelectedIndexChanged</summary>
        ///<remarks></remarks> 
        private void cbSkin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.bo_FirstLoad)
            {
                InitDGV();
            }
        }
        ///<summary>ctrAufträge/ dgv_CellClick</summary>
        ///<remarks></remarks> 
        private void dgv_CellClick(object sender, GridViewCellEventArgs e)
        {
            if ((e.RowIndex > -1) && (e.ColumnIndex > -1))
            {
                decimal decTmp = 0;
                Decimal.TryParse(this.dgv.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    this.Tour.Auftrag.AuftragPos.ID = decTmp;
                    this.Tour.Auftrag.AuftragPos.Fill();
                    this.Tour.Auftrag.ID = this.Tour.Auftrag.AuftragPos.AuftragTableID;
                    this.Tour.Auftrag.Fill();
                }
            }
        }
        ///<summary>ctrAufträge/ dgv_CellDoubleClick</summary>
        ///<remarks></remarks> 
        private void dgv_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if ((e.RowIndex > -1) && (e.ColumnIndex > -1))
            {
                miDetails_Click(sender, e);
            }
        }
        ///<summary>ctrAufträge/ dgv_MouseDoubleClick</summary>
        ///<remarks></remarks> 
        private void dgv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ////in Mousedown musste die Verknüpfung erstellt werden,damit diese Methode aufgerufen werden kann
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }
        ///<summary>ctrAufträge/ dgv_DragDrop</summary>
        ///<remarks></remarks> 
        private void dgv_DragDrop(object sender, DragEventArgs e)
        {
            if ((e.KeyState & 0x01) != 1)
            {
                try
                {
                    if (e.Data.GetDataPresent(typeof(structAuftPosRow)))
                    {
                        structAuftPosRow KommiBack = default(structAuftPosRow);
                        try
                        {
                            //BAustelle löschen Kommission aus Dispoplan
                            //clsResource recource = new clsResource();
                            clsKommission kommission = new clsKommission();

                            //KommiBack = (Globals.strAuftPosRow)e.Data.GetData(typeof(Globals.strAuftPosRow));
                            //kommission.ID = KommiBack.KommissionsID;   //ID in DB Kommission
                            //if (kommission.ID > 0)
                            //{
                            //    kommission.DeleteKommiPos();
                            //}

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }
            }
        }
        ///<summary>ctrAufträge/ dgv_DragEnter</summary>
        ///<remarks></remarks> 
        private void dgv_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.KeyState & 0x01) != 1)
            {
                if (e.Data.GetDataPresent(typeof(structAuftPosRow)))
                {
                    structAuftPosRow KommiBack = default(structAuftPosRow);
                    try
                    {
                        KommiBack = (structAuftPosRow)e.Data.GetData(typeof(structAuftPosRow));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }
        ///<summary>ctrAufträge/ dgv_RowMouseMove</summary>
        ///<remarks></remarks> 
        private void dgv_RowMouseMove(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(GridDataRowElement))
            {
                if (
                    !object.ReferenceEquals(
                                                ((GridDataRowElement)sender).RowInfo.Cells["ID"].Value
                                                , DBNull.Value
                                             )

                )
                {
                    strAuftrPosRow = default(structAuftPosRow);
                    decimal decTmp = 0;
                    Decimal.TryParse(((GridDataRowElement)sender).RowInfo.Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        strAuftrPosRow.AuftragPosTableID = decTmp;
                        textBox1.Text = decTmp.ToString();
                    }
                    else
                    {
                        textBox1.Text = string.Empty;
                    }
                }
                else
                {
                    textBox1.Text = string.Empty;
                }
            }
        }
        ///<summary>ctrAufträge/ dgv_MouseDown</summary>
        ///<remarks></remarks> 
        private void dgv_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left))
            {
                if (strAuftrPosRow.AuftragPosTableID > 0)
                {
                    this.Tour.Auftrag.AuftragPos.ID = strAuftrPosRow.AuftragPosTableID;
                    this.Tour.Auftrag.AuftragPos.Fill();
                    this.Tour.Auftrag.ID = this.Tour.Auftrag.AuftragPos.AuftragTableID;
                    this.Tour.Auftrag.Fill();
                }

                if (e.Clicks == 2)
                {
                    dgv_MouseDoubleClick(sender, e);
                }
                else
                {
                    if (strAuftrPosRow.AuftragPosTableID > 0)
                    {
                        try
                        {
                            this.strAuftrPosRow.Receiverctr = this;
                            Panel ddPanel = new Panel();
                            ddPanel.AllowDrop = true;
                            ddPanel.DoDragDrop(strAuftrPosRow, DragDropEffects.Copy);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }
        ///<summary>ctrAufträge/ RowUpdateFromDragDrop</summary>
        ///<remarks>Aufträge an SU</remarks>
        public void RowUpdateFromDragDrop(structAuftPosRow IDandRowID)
        {
            this.InitDGV();
            ////disponierte Auftrag wird aus dem Datattable gelöscht
            //Int32 RowIndex = -1; // IDandRowID.RowIndex;
            //RowIndex = Functions.GetValueRowNrFromDataTable(ref dtOrderList, "ID", IDandRowID.AuftragPosTableID.ToString());
            //if ((RowIndex > -1) &
            //    (RowIndex <= dtOrderList.Rows.Count - 1)
            //    )
            //{
            //    this.dgv.DataSource = null;
            //    dtOrderList.Rows.RemoveAt(RowIndex);

            //    this._ctrMenu._frmMain.boStatusWork = false;
            //    //FIlter Check, da sonst wieder alle Daten angezeigt werden
            //    //wenn ein Filter gesetzt wurde, dann soll der Filter gesetzt
            //    //bleiben und nur die entsprechenden Datensätze angezeigt werden
            //    //if (!FilterCheck())
            //    //{
            //    //    SetGrdDataSource(dataTable);
            //    //    //FormatGrd(dataTable);
            //    //}
            //}
            //this.dgv.BeginUpdate();
            //foreach (GridViewRowInfo row in this.dgv.Rows)
            //{
            //    if ((decimal)row.Cells["ID"].Value == IDandRowID.AuftragPosTableID)
            //    {
            //        this.dgv.Rows.Remove(row);
            //    }
            //}
            //this.dgv.EndUpdate();
        }
        ///<summary>ctrAufträge/ miAdd_Click</summary>
        ///<remarks></remarks> 
        private void miAdd_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenFrmAuftrag_Fast(this);
        }
        ///<summary>ctrAufträge/ miDetails_Click</summary>
        ///<remarks></remarks> 
        public void miDetails_Click(object sender, EventArgs e)
        {
            OpenCtrToEditOrder();
        }
        ///<summary>ctrAufträge/ miUpdate_Click</summary>
        ///<remarks></remarks> 
        private void miUpdate_Click(object sender, EventArgs e)
        {
            OpenCtrToEditOrder();
        }
        ///<summary>ctrAufträge/ OpenCtrToEditOrder</summary>
        ///<remarks></remarks> 
        private void OpenCtrToEditOrder()
        {
            if (this.dgv.Rows.Count > 0)
            {
                this._ctrMenu = this._ctrMenu.OpenFrmAuftragView(this, this.Tour);
            }
        }
        ///<summary>ctrAufträge/ LoadCBRelation</summary>
        ///<remarks></remarks> 
        private void LoadCBRelation()
        {
            DataTable dtRelationen = clsRelationen.GetRelationsliste();
            AddLeerZeileToTableRelation(ref dtRelationen);
            cbRelation.DataSource = dtRelationen;
            cbRelation.DisplayMember = "Relation";
            cbRelation.ValueMember = "Relation";
            cbRelation.SelectedValue = "";
        }
        ///<summary>ctrAufträge/ AddLeerZeileToTableRelation</summary>
        ///<remarks></remarks> 
        private void AddLeerZeileToTableRelation(ref DataTable dt)
        {
            DataRow row = dt.NewRow();
            row["ID"] = 1;
            row["Relation"] = "";

            dt.Rows.Add(row);
        }
        ///<summary>ctrAufträge/ SetMenuAllVisible</summary>
        ///<remarks></remarks> 
        private void SetMenuAllVisible()
        {
            //menü rechte Maustast
            miAdd.Visible = true;
            miUpdate.Visible = true;
            miDetails.Visible = true;
            mitSplitting.Visible = true;
            miAPAuf.Visible = true;
            miDelete.Visible = true;
            // miExcel.Visible = true;
            miAddImage.Visible = true;
            miToKommiInPlan.Visible = true;
            tsmiStatusListe.Visible = true;
            miStatChange.Visible = true;
            miFrachtvergabe.Visible = true;
            miCloseCtr.Visible = true;
            miDocs.Visible = true;

            //ToolStripMenü
            tsbNeu.Visible = true;
            tsbChange.Visible = true;
            tsbDetails.Visible = true;
            tsbSplitt.Visible = true;
            //tsbEcxel.Visible = true; ist raus
            tsbDelete.Visible = true;
            tsbScan.Visible = true;
            tsbRefresh.Visible = true;
            tsbListen.Visible = true;
            tsbClose.Visible = true;
            tsbChange.Enabled = true;
        }
        ///<summary>ctrAufträge/ miStatChange_Click</summary>
        ///<remarks></remarks>
        private void miStatChange_Click(object sender, EventArgs e)
        {
            OpenStatusChange();
        }
        ///<summary>ctrAufträge/ tsbtnStatChange_Click</summary>
        ///<remarks></remarks>
        private void tsbtnStatChange_Click(object sender, EventArgs e)
        {
            OpenStatusChange();
        }
        ///<summary>ctrAufträge/ OpenStatusChange</summary>
        ///<remarks></remarks>
        private void OpenStatusChange()
        {
            if (this.dgv.Rows.Count >= 1)
            {
                if (listenArt == 5)
                {
                    if (this.Tour.Auftrag.AuftragPos.Status >= 4)
                    {
                        if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmStatusChange)) != null)
                        {
                            Functions.frm_FormTypeClose(typeof(frmStatusChange));
                        }
                        frmStatusChange stL = new frmStatusChange();
                        stL.ctrAuftrag = this;
                        stL.GL_User = GL_User;
                        stL.Auftrag = this.Tour.Auftrag;
                        stL.Show();
                        stL.BringToFront();
                    }
                }
                else
                {
                    string stTxt = "Der Status dies Datensatzes kann nicht geändert werden, da der Datensatz den entsprechenden Status noch nicht erreicht hat!";
                    clsMessages.Allgemein_ERRORTextShow(stTxt);
                }
            }
        }
        ///<summary>ctrAufträge/ miDelete_Click</summary>
        ///<remarks>Auftrag wird storniert</remarks>
        private void miDelete_Click(object sender, EventArgs e)
        {
            if (GL_User.write_Order)
            {
                DialogResult resutl1 = MessageBox.Show("Soll die Stornierung des Auftrages durchgeführt werden?", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (resutl1 == DialogResult.OK)
                {
                    this.Tour.Auftrag.AuftragPos.Status = 3;
                    this.Tour.Auftrag.AuftragPos.Update();
                    InitDGV();
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>ctrAufträge/ tsbtnLegende_Click</summary>
        ///<remarks></remarks>
        private void tsbtnLegende_Click(object sender, EventArgs e)
        {
            OpenFrmStatusLegend();
        }
        ///<summary>ctrAufträge/ OpenFrmStatusLegend</summary>
        ///<remarks></remarks>
        public void OpenFrmStatusLegend()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmStatusLegende)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmStatusLegende));
            }
            frmStatusLegende stl = new frmStatusLegende();
            stl.Show();
            stl.BringToFront();
        }
        ///<summary>ctrAufträge/ nudFontSize_ValueChanged</summary>
        ///<remarks></remarks>
        private void nudFontSize_ValueChanged(object sender, EventArgs e)
        {
            GL_User.us_decFontSize = nudFontSize.Value;
            clsUser.SetUserFontSize(GL_User);
        }
        ///<summary>ctrAufträge / SetFilterforDGV</summary>
        ///<remarks></remarks>
        public void SetFilterforDGV(ref RadGridView myDGV, bool bClearFilter)
        {
            myDGV.EnableFiltering = true;
            myDGV.FilterDescriptors.Clear();

            string strFilter = string.Empty;
            if (!bClearFilter)
            {
                CompositeFilterDescriptor compositeFilter = new CompositeFilterDescriptor();

                //alle Filter prüfen
                //Relation
                if (ckbRelation.Checked)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Relation", FilterOperator.IsEqualTo, cbRelation.Text.ToString()));
                }
                //Auftraggeber MC
                if (cbSearchAuftraggeber.Checked)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("AuftraggeberMC", FilterOperator.IsEqualTo, tbSearchA.Text));
                }
                //AuftragID 
                if (cbAuftrag.Checked)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    decimal decTmp = 0;
                    Decimal.TryParse(tbSearchAuftrag.Text, out decTmp);
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("AuftragID", FilterOperator.IsEqualTo, decTmp));
                }
                //PLZ Beladestelle 
                if (cbBelPLZ.Checked)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("B_PLZ", FilterOperator.StartsWith, tbPLZ_BLS.Text));
                }
                //PLZ Entladestelle 
                if (cbEntlPLZ.Checked)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("E_PLZ", FilterOperator.StartsWith, tbPLZ_ELS.Text));
                }
                myDGV.FilterDescriptors.Add(compositeFilter);
            }
            else
            {
                myDGV.FilterDescriptors.Clear();
            }
        }
        ///<summary>ctrAufträge/ pbFilter_Click</summary>
        ///<remarks></remarks>
        private void pbFilter_Click(object sender, EventArgs e)
        {
            SetFilterforDGV(ref this.dgv, false);
        }
        ///<summary>ctrAufträge/ ckbRelation_CheckedChanged</summary>
        ///<remarks></remarks>
        private void ckbRelation_CheckedChanged(object sender, EventArgs e)
        {
            SetFilterSelection(ctrAufträge.const_Filter_Relation);
        }
        ///<summary>ctrAufträge/ cbSearchAuftraggeber_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbSearchAuftraggeber_CheckedChanged(object sender, EventArgs e)
        {
            SetFilterSelection(ctrAufträge.const_Filter_Auftraggeber);
        }
        ///<summary>ctrAufträge/ cbAuftrag_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbAuftrag_CheckedChanged(object sender, EventArgs e)
        {
            SetFilterSelection(ctrAufträge.const_Filter_AuftragNr);
        }
        ///<summary>ctrAufträge/ cbBelPLZ_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbBelPLZ_CheckedChanged(object sender, EventArgs e)
        {
            SetFilterSelection(ctrAufträge.const_Filter_BLST);
        }
        ///<summary>ctrAufträge/ cbEntlPLZ_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbEntlPLZ_CheckedChanged(object sender, EventArgs e)
        {
            SetFilterSelection(ctrAufträge.const_Filter_ELST);
        }
        ///<summary>ctrAufträge/ SetFilterSelection</summary>
        ///<remarks></remarks>
        private void SetFilterSelection(string strFilter)
        {
            switch (strFilter)
            {
                case ctrAufträge.const_Filter_Auftraggeber:
                    cbEntlPLZ.Checked = false;
                    tbPLZ_ELS.Text = string.Empty;
                    tbPLZ_ELS.Enabled = false;

                    tbSearchA.Enabled = true;
                    if (cbSearchAuftraggeber.Checked)
                    {
                        tbSearchA.Enabled = true;
                    }
                    else
                    {
                        tbSearchA.Enabled = false;
                        tbSearchA.Text = string.Empty;
                        this.dgv.FilterDescriptors.Clear();
                    }

                    ckbRelation.Checked = false;
                    cbRelation.SelectedValue = String.Empty;
                    cbRelation.Enabled = false;

                    cbBelPLZ.Checked = false;
                    tbPLZ_BLS.Text = string.Empty;
                    tbPLZ_BLS.Enabled = false;

                    cbAuftrag.Checked = false;
                    tbSearchAuftrag.Text = string.Empty;
                    tbSearchAuftrag.Enabled = false;
                    break;

                case ctrAufträge.const_Filter_AuftragNr:
                    cbEntlPLZ.Checked = false;
                    tbPLZ_ELS.Text = string.Empty;
                    tbPLZ_ELS.Enabled = false;

                    cbSearchAuftraggeber.Checked = false;
                    tbSearchA.Text = string.Empty;
                    tbSearchA.Enabled = false;

                    ckbRelation.Checked = false;
                    cbRelation.SelectedValue = String.Empty;
                    cbRelation.Enabled = false;

                    cbBelPLZ.Checked = false;
                    tbPLZ_BLS.Text = string.Empty;
                    tbPLZ_BLS.Enabled = false;

                    if (cbAuftrag.Checked)
                    {
                        tbSearchAuftrag.Enabled = true;
                    }
                    else
                    {
                        tbSearchAuftrag.Enabled = false;
                        tbSearchAuftrag.Text = string.Empty;
                        this.dgv.FilterDescriptors.Clear();
                    }
                    break;

                case ctrAufträge.const_Filter_BLST:
                    cbEntlPLZ.Checked = false;
                    tbPLZ_ELS.Text = string.Empty;
                    tbPLZ_ELS.Enabled = false;

                    cbSearchAuftraggeber.Checked = false;
                    tbSearchA.Text = string.Empty;
                    tbSearchA.Enabled = false;

                    ckbRelation.Checked = false;
                    cbRelation.SelectedValue = String.Empty;
                    cbRelation.Enabled = false;

                    if (cbBelPLZ.Checked)
                    {
                        tbPLZ_BLS.Enabled = true;
                    }
                    else
                    {
                        tbPLZ_BLS.Text = string.Empty;
                        tbPLZ_BLS.Enabled = false;
                        this.dgv.FilterDescriptors.Clear();
                    }

                    cbAuftrag.Checked = false;
                    tbSearchAuftrag.Text = string.Empty;
                    tbSearchAuftrag.Enabled = false;
                    break;

                case ctrAufträge.const_Filter_ELST:
                    if (cbEntlPLZ.Checked)
                    {
                        tbPLZ_ELS.Enabled = true;
                    }
                    else
                    {
                        tbPLZ_ELS.Enabled = false;
                        tbPLZ_ELS.Text = string.Empty;
                        this.dgv.FilterDescriptors.Clear();
                    }

                    cbSearchAuftraggeber.Checked = false;
                    tbSearchA.Text = string.Empty;
                    tbSearchA.Enabled = false;

                    ckbRelation.Checked = false;
                    cbRelation.SelectedValue = String.Empty;
                    cbRelation.Enabled = false;

                    cbBelPLZ.Checked = false;
                    tbPLZ_BLS.Text = string.Empty;
                    tbPLZ_BLS.Enabled = false;

                    cbAuftrag.Checked = false;
                    tbSearchAuftrag.Text = string.Empty;
                    tbSearchAuftrag.Enabled = false;
                    break;

                case ctrAufträge.const_Filter_Relation:
                    cbEntlPLZ.Checked = false;
                    tbPLZ_ELS.Text = string.Empty;
                    tbPLZ_ELS.Enabled = false;

                    cbSearchAuftraggeber.Checked = false;
                    tbSearchA.Text = string.Empty;
                    tbSearchA.Enabled = false;

                    if (ckbRelation.Checked)
                    {
                        cbRelation.Enabled = true;
                    }
                    else
                    {
                        cbRelation.SelectedValue = String.Empty;
                        cbRelation.Enabled = false;
                        this.dgv.FilterDescriptors.Clear();
                    }

                    cbBelPLZ.Checked = false;
                    tbPLZ_BLS.Text = string.Empty;
                    tbPLZ_BLS.Enabled = false;

                    cbAuftrag.Checked = false;
                    tbSearchAuftrag.Text = string.Empty;
                    tbSearchAuftrag.Enabled = false;
                    break;
            }
            InitDGV();
        }
        ///<summary>ctrAufträge/ pictureBox1_Click</summary>
        ///<remarks></remarks>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            InitDGV();
        }
        ///<summary>ctrAufträge/ miCloseCtr_Click</summary>
        ///<remarks></remarks>
        private void miCloseCtr_Click(object sender, EventArgs e)
        {
            if (this._ctrMenu.GetType() == typeof(ctrMenu))
            {
                this._ctrMenu.CloseCtrAutrag();
            }
        }
        ///<summary>ctrAufträge/ tsbtnAnpassen_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAnpassen_Click(object sender, EventArgs e)
        {
            SetCtrWidth();
        }
        ///<summary>ctrAufträge/ SetSearchTimeDistance</summary>
        ///<remarks></remarks>
        public void SetSearchTimeDistance(DateTime dtVon, DateTime dtBis)
        {
            SearchDateVon = dtVon;
            SearchDateBis = dtBis;
            afColorLabel1.myText = "Aufträge [" + dtVon.ToShortDateString() + " bis " + dtBis.ToShortDateString() + "]";
        }
        ///<summary>ctrAufträge/ dgv_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void dgv_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                //nur bei den Artikeldaten soll der ToolTipp angezegit werden,
                //deshalb wird hier eine CHeck auf die Spalte der Artikelliste gemacht
                if (ListArtTableColumns.Contains(cell.ColumnInfo.Name))
                {
                    e.ToolTipText = cell.Value.ToString();
                }
            }
        }
        /********************************************************************************************
         *                          Menüpunkt Auftragsliste
         * *****************************************************************************************/
        ///<summary>ctrAufträge/ tsmaktWeek_Click</summary>
        ///<remarks>aktuelle Auftragsliste</remarks>
        private void tsmaktWeek_Click(object sender, EventArgs e)
        {
            listenArt = 1;
            SetSearchTimeDistance(DateTime.Today, DateTime.Today.AddDays(7));
            InitDGV();
        }
        ///<summary>ctrAufträge/ miOpenOrder_Click</summary>
        ///<remarks>offene Aufträge nach Zeitraum</remarks>
        private void miOpenOrder_Click(object sender, EventArgs e)
        {
            listenArt = 2;
            OpenAuftragsListenZeitraum();
        }
        ///<summary>ctrAufträge/ miDispoOrder_Click</summary>
        ///<remarks>Disponierte Aufträge nach Zeit</remarks>
        private void miDispoOrder_Click(object sender, EventArgs e)
        {
            listenArt = 3;
            OpenAuftragsListenZeitraum();
        }
        ///<summary>ctrAufträge/ miDoneOrder_Click</summary>
        ///<remarks>durchgeführte Aufträge nach Zeitraum</remarks>
        private void miDoneOrder_Click(object sender, EventArgs e)
        {
            listenArt = 4;
            OpenAuftragsListenZeitraum();
        }
        ///<summary>ctrAufträge/ miVergabe_Click</summary>
        ///<remarks>Aufträge an SU</remarks>
        private void miVergabe_Click(object sender, EventArgs e)
        {
            OpenListeSU();
            OpenSUListeMitZeitraum();
        }
        ///<summary>ctrAufträge/ mitSplitting_Click</summary>
        ///<remarks></remarks>
        private void mitSplitting_Click(object sender, EventArgs e)
        {
            if (GL_User.write_Disposition)
            {
                decimal Pos;
                if (this.dgv.Rows.Count >= 1)
                {
                    //-- Update ---
                    if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmAuftrag_Splitting)) != null)
                    {
                        Functions.frm_FormTypeClose(typeof(frmAuftrag_Splitting));
                    }

                    //frmAuftrag_Splitting UAuftrag = new frmAuftrag_Splitting(this, Convert.ToDecimal(strID), Pos);
                    frmAuftrag_Splitting UAuftrag = new frmAuftrag_Splitting(this);
                    UAuftrag.GL_User = this.GL_User;
                    UAuftrag.Show();
                    UAuftrag.BringToFront();
                }
                this.InitDGV();
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }




        //-------------- AuftragPos auflösen -------------------
        //
        //
        private void miAPAuf_Click(object sender, EventArgs e)
        {
            string str = string.Empty;

            /****************************************************************************
             * Wenn eine Auftragsposition aufgelöst wird, dann:
             * - AuftragPos in DB löschen
             * - Artikel einer Auftragsposition werden der Auftragspos. 0 zugeordnet
             *   ->Vorgehen:
             *      > Check Artikel (Güterart und Abmessungen) unter der Auftragsnummer noch einmal vorhanden
             *      > ja - und Position == 0 Mengen zusammenfassen unter Pos=0
             *      > ja - und Position != 0 Artikel Positionsupdate auf Pos=0
             *      > nein - Artikel Positionsupdate auf Pos=0
             ****************************************************************************/
            //decimal auftragID = Convert.ToDecimal(this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["AuftragID"].Value);
            //decimal PosID = Convert.ToDecimal(this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["Pos"].Value);
            //decimal AP = Convert.ToDecimal(this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["ID"].Value);
            //if (PosID == 0)
            //{
            //    clsMessages.Auftragsliste_AuftragPosLoeschenNichtMoeglich();
            //}
            //else
            //{
            //    //prüfen Status >=5
            //    clsAuftragsstatus aStatus = new clsAuftragsstatus();
            //    aStatus.AP_ID = AP;
            //    aStatus.Auftrag_ID = auftragID;
            //    aStatus.AuftragPos = PosID;

            //    Int32 Status = aStatus.GetAuftragsstatus();
            //    if (Status <= 3)
            //    {
            //        DialogResult resutl1 = MessageBox.Show("Soll die Auftragspositione aufgelöst werden?", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //        if (resutl1 == DialogResult.OK)
            //        {
            //            // Löschen
            //            if (PosID > 0)
            //            {
            //                string Nachricht = "Die Auftragsposition wird aufgelöst und gelöscht.";
            //                DialogResult result2 = MessageBox.Show(Nachricht, "Letzte INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //                if (result2 == DialogResult.OK)
            //                {
            //                    //Baustelle  löschen als Transaction
            //                    //decimal AP_ID = clsAuftragPos.GetIDbyAuftragAndAuftragPos(this.GL_User,auftragID, PosID, MandantenID, this.GL_User.sys_ArbeitsbereichID);

            //                    //Artikel wieder Hauptauftrag zuweisen Pos=0
            //                    clsArtikel.UpdateArtikelNachAuftragPosAufloesung(this.GL_User, AP);

            //                    //löschen in AuftragPos DB
            //                    clsAuftragPos clsPos = new clsAuftragPos();
            //                    clsPos._GL_User = this.GL_User;
            //                    clsPos.ID = AP;
            //                    clsPos.Fill();
            //                    clsPos.Delete();
            //                    //Auftrag aus Kommission löschen, wenn Status nicht durchgeführt - Testen oben
            //                    //clsKommission.DeleteKommiPosByAuftragAuftragPos(auftragID, PosID);
            //                    //DB OrderPosRec 
            //                    //if (clsOrderPosRectangle.IsAuftragAuftragPosIn(auftragID, PosID))
            //                    //{
            //                    //    clsOrderPosRectangle.DeleteRectanglePosByAuftragAuftragPos(auftragID, PosID);
            //                   // }
            //                    //löschen der Lieferscheine
            //                    //Int32 AP_ID = clsAuftragPos.GetIDbyAuftragAndAuftragPos(auftragID, PosID);
            //                    if (AP > 0)
            //                    {
            //                        if (clsLieferscheine.LieferscheinExist(AP))
            //                        {
            //                            clsLieferscheine.DeleteLieferscheinByAP_ID(AP);
            //                        }
            //                    }

            //                    //löschen der AuftragposID-Datensätze aus DB AuftragRead
            //                    if (AP > -1)
            //                    {
            //                        clsAuftragRead read = new clsAuftragRead();
            //                        read.DeleteReadAuftragAuftragPosID(AP);
            //                    }
            //                }
            //            }
            //        }
            //        init();
            //    }
            //    else
            //    {
            //        DialogResult resutl1 = MessageBox.Show("Der Auftrag / Auftragsposition kann nicht gelöscht werden, da der Transportauftrag bereits durchgeführt wurde!", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //    }
            //}
        }



        //
        //------ nachträglich Auftrag Image hinterlegen  -------------------
        //
        private void miAddImage_Click(object sender, EventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                this._ctrMenu.OpenScanFrm(this.Tour.Auftrag.ID, this.Tour.Auftrag.AuftragPos.ID, 0, 0, this);
            }
        }
        //
        //------------ Frachtvergaben an SU ------------------------
        //
        private void miFrachtvergabe_Click(object sender, EventArgs e)
        {
            if (GL_User.write_TransportOrder)
            {
                //if (grdAuftrag.Rows.Count >= 1)
                //{
                //    //--- ausgewählte Datensatz ------
                //    decimal myAuftrag = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["AuftragID"].Value;
                //    decimal myAuftragPos = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["Pos"].Value;
                //    //decimal myArtikelID = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["ID"].Value;
                //    decimal myAuftragPosTableID = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["ID"].Value;
                //    frmDispoFrachtvergabe fv = new frmDispoFrachtvergabe(this);
                //    fv.GL_User = this.GL_User;
                //    fv._AuftragPosTableID = myAuftragPosTableID;
                //    fv._AuftragID = myAuftrag;
                //    fv._AuftragPos = myAuftragPos;
                //    fv.StartPosition = FormStartPosition.CenterScreen;
                //    fv.Show();
                //    fv.BringToFront();

                //}
                this.InitDGV();
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //

        private void OpenAuftragsListenZeitraum()
        {
            frmAuftragslisteZeitraum az = new frmAuftragslisteZeitraum();
            az.ctrAuftrag = this;
            az.StartPosition = FormStartPosition.CenterScreen;
            az.Show();
            az.BringToFront();
        }
        //
        //---------- Auftragsliste nach Zeitraum -------------------
        //
        private void miListeZeitraum_Click(object sender, EventArgs e)
        {
            listenArt = 2;
            OpenAuftragsListenZeitraum();
        }
        //
        //
        //
        public void AbbruchFrmAuftragslisteZeitraum()
        {
            listenArt = 1;
            afColorLabel1.myText = "Aufträge [komplett]";
            InitDGV();
        }
        //

        //

        //

        //

        //

        //
        private void miAllOrder_Click(object sender, EventArgs e)
        {
            listenArt = 7;
            InitDGV();
        }
        //
        private void tsmiStronoAuftrZeitraum_Click(object sender, EventArgs e)
        {
            listenArt = 6;
            OpenAuftragsListenZeitraum();
        }
        //
        //

        //
        //
        public void OpenSUListeMitZeitraum()
        {
            listenArt = 5;
            frmAuftragslisteZeitraum az = new frmAuftragslisteZeitraum();
            az.ctrSUListe = this.ctrSUListe;
            az.ctrAuftrag = this;
            az.StartPosition = FormStartPosition.CenterScreen;
            az.Show();
            az.BringToFront();
        }

        //
        // aus dem Menü zum Auftrag im Dispoplan ------------------
        //
        private void miToKommiInPlan_Click(object sender, EventArgs e)
        {
            if (this.dgv.Rows.Count >= 0)
            {

                ////--- ausgewählte Datensatz ------
                //decimal AP_ID = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["ID"].Value;
                ////decimal AuftragPos = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["Pos"].Value;
                //if (clsAuftragPos.GetStatusByAuftragPosTableID(this.GL_User, AP_ID) >= 4)
                //{
                //    DataSet ds = clsKommission.getKommiRecByAuftragID(Auftrag, AuftragPos);

                //    for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //        DispoDatumStart = (DateTime)ds.Tables[0].Rows[i]["B_Zeit"];
                //        DispoDatumEnd = (DateTime)ds.Tables[0].Rows[i]["E_Zeit"];

                //        DispoDatumStart = DispoDatumStart.Date;
                //        DispoDatumEnd = DispoDatumEnd.Date;
                //    }

                //    if (Functions.frm_IsFormAlreadyOpen(typeof(frmDispoKalender)) != null)
                //    {
                //        Functions.frm_FormClose(typeof(frmDispoKalender));
                //    }
                //    frmDispoKalender Kalender = new frmDispoKalender(this);
                //    //Kalender.MdiParent = this.ParentForm;

                //    //DispoDataChanged += Kalender.MenuDispoDataChanged;
                //    DispoDataChangedByCtrAuftrag += Kalender.ctrAuftragDispoDataChanged;
                //    Kalender.WindowState = FormWindowState.Maximized;
                //    Kalender.Show();
                //    Kalender.BringToFront();
                //    if (DispoDataChangedByCtrAuftrag != null)
                //    {
                //        DispoDataChangedByCtrAuftrag(this);
                //    }
                //}

            }
        }
        //
        //
        //
        private void tsmiStatusListe_Click(object sender, EventArgs e)
        {
            if (this.dgv.Rows.Count >= 1)
            {
                //--- ausgewählte Datensatz ------
                //decimal Auftrag = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["AuftragID"].Value;
                //if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmStatusInfoAuftrag)) != null)
                //{
                //    Functions.frm_FormTypeClose(typeof(frmStatusInfoAuftrag));
                //}
                //frmStatusInfoAuftrag stL = new frmStatusInfoAuftrag(Auftrag);
                //stL.Show();
                //stL.BringToFront();
            }
        }
        //
        //
        //

        void stL_RefreshAuftragList()
        {
            throw new NotImplementedException();
        }

        //
        //





        //
        private void miDocs_Click(object sender, EventArgs e)
        {
            if (this.dgv.Rows.Count >= 1)
            {
                //decimal AuftragID = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["AuftragID"].Value;
                //decimal AuftragPos = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["Pos"].Value;
                //DataSet ds = new DataSet(); //Leer

                //if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmPrintCenter)) != null)
                //{
                //    Functions.frm_FormTypeClose(typeof(frmPrintCenter));
                //}
                //frmPrintCenter pC = new frmPrintCenter();
                //pC._AuftragID = AuftragID;
                //pC._AuftragPos = AuftragPos;
                //pC.GL_User = GL_User;
                //pC.Show();
                //pC.BringToFront();
            }
        }
        //
        //
        //-------------- öffnet Übersicht Subunternehmer --------
        //
        private void tsbtnSub_Click(object sender, EventArgs e)
        {
            OpenListeSU();
        }
        //
        public void OpenListeSU()
        {
            bool open = false;

            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrAufträge))
                {
                    open = true;
                    i = this.ParentForm.Controls.Count - 1;
                }
            }
            if (open)
            {
                CloseSUList();

                //SU Liste
                ctrSUList ctr_SUList = new ctrSUList();
                ctr_SUList.GL_User = GL_User;
                ctr_SUList.ctrAuftrag = this;
                ctr_SUList.Dock = System.Windows.Forms.DockStyle.Left;
                ctr_SUList.Name = "TempSUList";
                this.ParentForm.Controls.Add(ctr_SUList);
                this.ParentForm.Controls.SetChildIndex(ctr_SUList, 0);
                ctr_SUList.SetSearchTimeDistance(DateTime.Today.Date, DateTime.Today.Date.AddDays(3));
                ctr_SUList.InitCtrSUList();
                ctr_SUList.Hide();
                ctr_SUList.Show();
                ctrSUListe = ctr_SUList;



                // Splitter zur Auftragsliste generieren und anzeigen
                Splitter splitter = new Splitter();
                splitter.BackColor = Sped4.Properties.Settings.Default.EffectColor;
                splitter.BorderStyle = BorderStyle.None;
                splitter.Dock = DockStyle.Left;
                splitter.Name = "TempSplitterSUList";
                this.ParentForm.Controls.Add(splitter);
                this.ParentForm.Controls.SetChildIndex(splitter, 0);
                splitter.Show();
            }

        }
        //
        public void CloseSUList()
        {
            for (Int32 k = 0; (k <= (this.ParentForm.Controls.Count - 1)); k++)
            {
                if (this.ParentForm.Controls[k].Name == "TempSplitterSUList")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[k]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[k].GetType() == typeof(ctrSUList))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[k]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //
        public void OpenDispoPlan()
        {
            this._ctrMenu.DispoKalenderOpenSplitter();
        }

























    }
}
