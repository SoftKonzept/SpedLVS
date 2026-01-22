using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrStatistik : UserControl
    {
        public const string const_StatistikArt_Gesamtbestand = "Gesamtbestand";
        public const string const_StatistikArt_Bestandsbewegungen = "Bestandsbewegungen";
        public const string const_StatistikArt_druchschnLagerbestand = "durchschnittlicher Lagerbestand";
        public const string const_StatistikArt_druchschnLagerdauer = "durchschnittliche Lagerdauer";
        public const string const_StatistikArt_Monatsuebersicht = "Monatsübersicht";

        public ctrMenu _ctrMenu;
        internal string strStatistikArt;
        //internal clsUserList UserList;
        internal Globals._GL_USER GL_User;
        internal Globals._GL_SYSTEM GL_System;
        internal ctrFilterSelect _ctrFilterSelect;
        //internal IListSource listSourceChart;
        internal DataTable dtDGV = new DataTable();
        internal clsADR ADR = new clsADR();
        internal clsLager Lager;
        internal Int32 SearchButton = 0;
        //internal List<string> ListAttachmentPath;
        internal string AttachmentPath;
        internal string FileName;
        const string const_FileName = "_Statistik";
        const string const_Headline = "Statistik";
        List<string> listBestand = new List<string>();
        List<string> listRGLager = new List<string>()
        {
            "Umsatz Lager",
            "Umsatz Direktanlieferung",
            "Umsatz KFZ"
        };
        List<string> listRGDispo = new List<string>()
        {
            "Umsatz KFZ"
        };


        ///<summary>ctrStatistik / ctrStatistik</summary>
        ///<remarks>.</remarks>
        public ctrStatistik()
        {
            InitializeComponent();
        }
        ///<summary>ctrStatistik / InitCtr</summary>
        ///<remarks>.</remarks>
        public void InitCtr()
        {
            AttachmentPath = this._ctrMenu._frmMain.GL_System.sys_WorkingPathExport;
            this.GL_System = this._ctrMenu._frmMain.GL_System;
            this.GL_User = this._ctrMenu._frmMain.GL_User;
            Lager = new clsLager();
            Lager.sys = this._ctrMenu._frmMain.system;
            Lager._GL_System = this.GL_System;
            Lager._GL_User = this.GL_User;

            //Beim Start soll das Filterpanel ausgeblendet sein
            this.splittCtr.Panel2Collapsed = true;
            //ctrFilterSelect erstellen
            CreateCtrFilterSelect();

            //ComboBox Liste initialisieren
            comboListe.DataSource = _ctrFilterSelect.UserList.dtUserListAvailable;
            comboListe.DisplayMember = "Bezeichnung";
            comboListe.ValueMember = "ID";
            //combobox Statistikart
            switch (strStatistikArt)
            {
                case "Lager":
                    InitStatistikListBestand();
                    comboStatistikArt.DataSource = listBestand;
                    break;

                case "RGLager":
                    comboStatistikArt.DataSource = listRGLager;
                    break;

                case "RGDispo":
                    comboStatistikArt.DataSource = listRGDispo;
                    break;
            }
            if (comboStatistikArt.Items.Count > 0)
            {
                comboStatistikArt.SelectedIndex = 0;
            }
            if (comboListe.Items.Count > 0)
            {
                //comboListe.SelectedIndex = 0;
            }
            if (comboChart.Items.Count > 0)
            {
                comboChart.SelectedIndex = 0;
            }
            CustomizeCtr();
        }
        ///<summary>ctrStatistik / CustomizeCtr</summary>
        ///<remarks></remarks>
        private void CustomizeCtr()
        {
            this._ctrMenu._frmMain.system.Client.ctrStatistik_CustomizeCtrCheckBoxRLExcl(ref this.cbRLExcl);
            this._ctrMenu._frmMain.system.Client.ctrStatistik_CustomizeCtrCheckBoxSchaedenExcl(ref this.cbSchadenExcl);
            this._ctrMenu._frmMain.system.Client.ctrStatistik_CustomizeCtrCheckBoxSPLExcl(ref this.cbSPLExcl);
        }
        ///<summary>ctrStatistik / InitStatistikListBestand</summary>
        ///<remarks></remarks>
        private void InitStatistikListBestand()
        {
            listBestand = new List<string>();
            //Gesamtbestand
            if (this._ctrMenu._frmMain.system.Client.Modul.Statistik_Gesamtbestand)
            {
                listBestand.Add(ctrStatistik.const_StatistikArt_Gesamtbestand);
            }
            //Bestandsbewegungen
            if (this._ctrMenu._frmMain.system.Client.Modul.Statistik_Bestandsbewegungen)
            {
                listBestand.Add(ctrStatistik.const_StatistikArt_Bestandsbewegungen);
            }
            //durchschn. Lagerbestand
            if (this._ctrMenu._frmMain.system.Client.Modul.Statistik_durchschn_Lagerbestand)
            {
                listBestand.Add(ctrStatistik.const_StatistikArt_druchschnLagerbestand);
            }
            //durchschn Lagerdauer
            if (this._ctrMenu._frmMain.system.Client.Modul.Statistik_druchschn_Lagerdauer)
            {
                listBestand.Add(ctrStatistik.const_StatistikArt_druchschnLagerdauer);
            }
            //Monatsübersicht
            if (this._ctrMenu._frmMain.system.Client.Modul.Statistik_Monatsuebersicht)
            {
                listBestand.Add(ctrStatistik.const_StatistikArt_Monatsuebersicht);
            }
        }
        ///<summary>ctrStatistik / CreateCtrFilterSelect</summary>
        ///<remarks></remarks>
        private void CreateCtrFilterSelect()
        {

            //ctrFilterSelect erstellen
            _ctrFilterSelect = new Sped4.ctrFilterSelect();
            _ctrFilterSelect.ctrMenu = this._ctrMenu;
            _ctrFilterSelect.InitCtr();
            _ctrFilterSelect.Parent = this.splittCtr.Panel2;
            _ctrFilterSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            _ctrFilterSelect.Name = "TempFilterSelect";
            _ctrFilterSelect.Show();

        }
        ///<summary>ctrStatistik / InitCtr</summary>
        ///<remarks>Ein- und Ausblenden der Filter</remarks>
        private void cbFilterActivate_CheckedChanged(object sender, EventArgs e)
        {
            this.splittCtr.Panel2Collapsed = !this.splittCtr.Panel2Collapsed;
        }
        ///<summary>ctrStatistik / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrStatistik();
        }
        ///<summary>ctrStatistik / tsbtnSearch_Click</summary>
        ///<remarks>Suche starten</remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            cvStatChart.Series.Clear();
            decimal decTmp = 0;
            if (comboListe.SelectedValue != null)
            {
                Decimal.TryParse(comboListe.SelectedValue.ToString(), out decTmp);
            }
            _ctrFilterSelect.UserList.ID = decTmp;
            _ctrFilterSelect.UserList.Fill();

            dtDGV = new DataTable();

            DataTable dtFilterValue = new DataTable();
            dtFilterValue = _ctrFilterSelect.GetFilterValue();

            this.Lager.BestandVon = dtpVon.Value;
            this.Lager.BestandBis = dtpBis.Value;
            this.Lager.MandantenID = this.GL_System.sys_MandantenID;
            this.Lager.RLJournalExcl = cbRLExcl.Checked;
            this.Lager.SchadenJournalExcl = cbSchadenExcl.Checked;
            this.Lager.SPLEndbestandExcl = cbSPLExcl.Checked;


            switch (strStatistikArt)
            {
                case "Lager":
                    switch (comboStatistikArt.SelectedItem.ToString())
                    {
                        case "Monatsübersicht":
                            if (this.ADR.ID > 0)
                            {
                                dtDGV = this.Lager.GetStatistikMonatsuebersicht(this.ADR.ID, false);
                                InitChart(false);
                            }
                            else
                            {
                                //clsMessages.ADR_KeineAdressenIdVorhanden();
                                dtDGV = this.Lager.GetStatistikMonatsuebersicht(0, true);
                                InitChart(true);
                            }
                            this.dgv.DataSource = dtDGV;
                            foreach (GridViewDataColumn col in this.dgv.Columns)
                            {
                                if (col.DataType.FullName.Equals("System.Decimal"))
                                {
                                    col.FormatString = "{0:N3}";
                                }
                                if (col.Name.Equals("Datum"))
                                {
                                    col.IsVisible = false;
                                }
                            }
                            this.dgv.BestFitColumns();
                            break;
                        case "Gesamtbestand":
                            dtDGV = this.Lager.GetStatistikTagesbestandkomplett();
                            break;
                        case "Bestandsbewegungen":
                            dtDGV = this.Lager.GetStatitikBestandVerlauf((Int32)this.ADR.ID);
                            InitChart(false);
                            this.dgv.DataSource = dtDGV;
                            this.dgv.BestFitColumns();
                            break;
                        case "durchschnittlicher Lagerbestand":
                            dtDGV = this.Lager.GetStatitikDSLagerbestand();
                            this.dgv.DataSource = dtDGV;
                            this.dgv.BestFitColumns();
                            break;
                        case "durchschnittliche Lagerdauer":
                            dtDGV = this.Lager.GetStatistikDSLagerdauer();
                            this.dgv.DataSource = dtDGV;
                            this.dgv.BestFitColumns();
                            break;
                        case "Waggon EA":
                            dtDGV = this.Lager.GetStatistikWaggonEA();
                            this.dgv.DataSource = dtDGV;
                            this.dgv.BestFitColumns();
                            break;
                        case "LKW EA":
                            dtDGV = this.Lager.GetStatistikLKWEA();
                            this.dgv.DataSource = dtDGV;
                            this.dgv.BestFitColumns();
                            break;
                    }
                    break;

                default:
                    this.dgv.DataSource = dtDGV;
                    this.dgv.BestFitColumns();
                    break;
            }
        }
        ///<summary>ctrStatistik / InitChart</summary>
        ///<remarks></remarks>
        private void InitChart(bool bKomplett)
        {
            if (dtDGV.Rows.Count > 0)
            {
                cvStatChart.Series.Clear();
                CategoricalAxis cat = new CategoricalAxis();
                cat.LabelFitMode = AxisLabelFitMode.MultiLine;

                switch (comboStatistikArt.SelectedItem.ToString())
                {
                    case "Tagesbestand":
                        break;
                    case "Monatsübersicht":
                        DataTable dtEingang = new DataTable();
                        dtEingang.Columns.Add("Monat", typeof(string));
                        dtEingang.Columns.Add("Wert", typeof(decimal));

                        DataTable dtAusgang = new DataTable();
                        dtAusgang.Columns.Add("Monat", typeof(string));
                        dtAusgang.Columns.Add("Wert", typeof(decimal));

                        DataTable dtEndbestand = new DataTable();
                        dtEndbestand.Columns.Add("Monat", typeof(string));
                        dtEndbestand.Columns.Add("Wert", typeof(decimal));

                        for (int i = 0; i < dtDGV.Rows.Count; i++)
                        {
                            if (!dtDGV.Rows[i][0].ToString().Contains("Summe"))
                            {
                                //Eingnag
                                DataRow dr1 = dtEingang.NewRow();
                                dr1[0] = dtDGV.Rows[i][0];
                                decimal decTmp = 0;
                                Decimal.TryParse(dtDGV.Rows[i]["Brutto Eingang"].ToString(), out decTmp);
                                dr1[1] = decTmp;
                                dtEingang.Rows.Add(dr1);

                                //Ausgang
                                DataRow dr2 = dtAusgang.NewRow();
                                dr2[0] = dtDGV.Rows[i][0];
                                decTmp = 0;
                                Decimal.TryParse(dtDGV.Rows[i]["Brutto Ausgang"].ToString(), out decTmp);
                                dr2[1] = decTmp;
                                dtAusgang.Rows.Add(dr2);

                                //Endbestand
                                DataRow dr3 = dtEndbestand.NewRow();
                                dr3[0] = dtDGV.Rows[i][0];
                                decTmp = 0;
                                Decimal.TryParse(dtDGV.Rows[i]["Endbestand"].ToString(), out decTmp);
                                dr3[1] = decTmp;
                                dtEndbestand.Rows.Add(dr3);
                            }
                        }

                        BarSeries barEingang = new BarSeries();
                        barEingang.Name = "Eingang";
                        barEingang.ValueMember = "Wert";
                        barEingang.CategoryMember = "Monat";
                        barEingang.LegendTitle = "Eingang";
                        barEingang.DataSource = dtEingang;
                        barEingang.HorizontalAxis = cat;
                        barEingang.BackColor = Color.Green;
                        cvStatChart.Series.Add(barEingang);

                        BarSeries barAusgang = new BarSeries();
                        barAusgang.Name = "Ausgang";
                        barAusgang.ValueMember = "Wert";
                        barAusgang.CategoryMember = "Monat";
                        barAusgang.LegendTitle = "Ausgang";
                        barAusgang.DataSource = dtAusgang;
                        barAusgang.HorizontalAxis = cat;
                        barAusgang.BackColor = Color.Red;
                        cvStatChart.Series.Add(barAusgang);

                        switch (comboChart.SelectedItem.ToString())
                        {
                            case "Balkendiagramm":
                                BarSeries barEB = new BarSeries();
                                barEB.Name = "Endbestand";
                                barEB.ValueMember = "Wert";
                                barEB.CategoryMember = "Monat";
                                barEB.LegendTitle = "Endbestand";
                                barEB.DataSource = dtEndbestand;
                                barEB.HorizontalAxis = cat;
                                barEB.BackColor = Color.Beige;
                                cvStatChart.Series.Add(barEB);
                                break;

                            case "Kurvendiagramm":
                                AreaSeries areaEB = new AreaSeries();
                                areaEB.Name = "Endbestand";
                                areaEB.ValueMember = "Wert";
                                areaEB.CategoryMember = "Monat";
                                areaEB.LegendTitle = "Endbestand";
                                areaEB.DataSource = dtEndbestand;
                                areaEB.BackColor = Color.Beige;
                                areaEB.HorizontalAxis = cat;
                                cvStatChart.Series.Add(areaEB);
                                break;

                            default:
                                break;
                        }
                        //Titel
                        if (!bKomplett)
                        {
                            this.cvStatChart.Title = "Monatsübersicht Kunde: " + this.tbSearchA.Text;
                        }
                        else
                        {
                            this.cvStatChart.Title = "Monatsübersicht Arbeitsbereich: " + this._ctrMenu._frmMain.system.AbBereich.ABName;
                        }
                        this.cvStatChart.ShowTitle = true;
                        this.cvStatChart.ChartElement.TitleElement.TextOrientation = Orientation.Horizontal;
                        this.cvStatChart.ChartElement.TitlePosition = TitlePosition.Top;
                        this.cvStatChart.ChartElement.FlipText = false;
                        //Legende
                        this.cvStatChart.ShowLegend = true;
                        this.cvStatChart.LegendTitle = "Bestände";

                        break;
                    case "Bestandsbewegungen":
                        switch (comboChart.SelectedItem.ToString())
                        {
                            //case "Balkendiagramm":
                            //    break;
                            case "Balkendiagramm":
                            case "Kurvendiagramm":
                                LineSeries lSeria = new LineSeries();
                                cvStatChart.Series.Add(lSeria);
                                lSeria.ValueMember = "Bestand [kg]";
                                lSeria.CategoryMember = "Datum";
                                lSeria.LegendTitle = "Tagesbestand";
                                lSeria.DataSource = dtDGV;
                                lSeria.HorizontalAxis.LabelFitMode = AxisLabelFitMode.MultiLine;
                                break;

                            default:
                                break;
                        }
                        break;
                }
            }

        }
        ///<summary>ctrStatistik / dtpVon_ValueChanged</summary>
        ///<remarks>Suche starten</remarks>
        private void dtpVon_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
            DateTime.TryParse(((DateTimePicker)sender).Value.Date.ToShortDateString(), out dtTmp);

            DateTime ZeitRaumMonat = dtTmp;
            dtpVon.Value = ((DateTimePicker)sender).Value;
            dtpBis.Value = Functions.GetLastDayOfMonth(ZeitRaumMonat);
        }
        ///<summary>ctrStatistik / dtpVon_ValueChanged</summary>
        ///<remarks>Suche starten</remarks>
        private void comboChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bKomplett = !(this.ADR.ID > 0);
            InitChart(bKomplett);
        }
        ///<summary>ctrStatistik / comboStatistikArt_SelectedIndexChanged</summary>
        ///<remarks>Suche starten</remarks>
        private void comboStatistikArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bEnabled = false;
            switch (comboStatistikArt.SelectedItem.ToString())
            {
                case "Bestandsbewegungen":
                case "Monatsübersicht":
                    bEnabled = true;
                    break;
                default:
                    bEnabled = false;
                    break;
            }
            this.comboChart.Enabled = bEnabled;
        }
        ///<summary>ctrStatistik / btnSearchA_Click</summary>
        ///<remarks>Suche starten</remarks>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrStatistik / tbSearchA_TextChanged</summary>
        ///<remarks>Suche starten</remarks>
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = Functions.GetADRTableSearchResultTable(tbSearchA.Text, this.GL_User);
            if (dtTmp.Rows.Count > 0)
            {
                tbAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
                ADR.ID = Functions.GetADR_IDFromTable(dtTmp);
            }
            else
            {
                tbAuftraggeber.Text = string.Empty;
                ADR.ID = 0;
            }
            ADR.Fill();
        }
        ///<summary>ctrStatistik / SetADRToFrm</summary>
        ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
        ///<param name="ADR_ID">ADR_ID</param>
        public void SetADRToFrm(decimal myDecADR_ID)
        {
            ADR.ID = myDecADR_ID;
            ADR.Fill();

            string strE = string.Empty;
            string strMC = string.Empty;
            DataSet ds = clsADR.ReadADRbyID(myDecADR_ID);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strMC = ADR.ViewID;
                strE = ADR.ViewID + " - " + ADR.Name1 + " - " + ADR.PLZ + " - " + ADR.Ort;

                //SearchButton
                // 1 = KD /Auftraggeber
                // 2 = Versender
                // 3 = Empfänger
                // 4 = neutrale Versandadresse
                // 5 = neutrale Empfangsadresse
                // 6 = Mandanten
                // 7 = Spedition
                // 8 =
                // 9 =
                // 10= Rechnungsempfänger
                switch (SearchButton)
                {
                    case 1:
                        tbSearchA.Text = strMC;
                        tbAuftraggeber.Text = strE;
                        break;
                }
            }
        }
        ///<summary>ctrStatistik / dgv_CellFormatting</summary>
        ///<remarks></remarks>
        private void dgv_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                switch (strStatistikArt)
                {
                    case "Lager":
                        switch (comboStatistikArt.SelectedItem.ToString())
                        {
                            case "Monatsübersicht":
                                if (e.CellElement.RowInfo.Cells[0].Value.ToString().Contains("Summe"))
                                {
                                    Font CellFont = new System.Drawing.Font(e.CellElement.Font.FontFamily, e.CellElement.Font.Size + 1, FontStyle.Bold);
                                    e.CellElement.Font = CellFont;
                                }
                                break;
                            case "Gesamtbestand":

                                break;
                            case "Bestandsbewegungen":
                                break;
                            case "durchschnittlicher Lagerbestand":
                                break;
                            case "durchschnittliche Lagerdauer":
                                break;
                            case "Waggon EA":
                                break;
                            case "LKW EA":
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        ///<summary>ctrStatistik / tsbtnPrintChart_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrintChart_Click(object sender, EventArgs e)
        {
            RadPrintDocument document = new RadPrintDocument();

            document.HeaderHeight = 75;
            document.HeaderFont = new Font("Arial", 12);
            document.LeftHeader = InitExportTitel();
            //document.MiddleHeader = "Middle header";
            //document.RightHeader = "Right header";
            document.ReverseHeaderOnEvenPages = true;

            //document.FooterHeight = 30;
            //document.FooterFont = new Font("Arial", 22);
            //document.LeftFooter = "Left footer";
            //document.MiddleFooter = "Middle footer";
            //document.RightFooter = "Right footer";
            //document.ReverseFooterOnEvenPages = true;

            document.AssociatedObject = this.cvStatChart;

            RadPrintPreviewDialog dialog = new RadPrintPreviewDialog(document);
            dialog.ShowDialog(this._ctrMenu._frmMain);
        }
        ///<summary>ctrStatistik / tsbtnPrintChart_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            this.dgv.PrintPreview();
        }
        ///<summary>ctrStatistik / tsbtnExcel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnExcel_Click(object sender, EventArgs e)
        {
            bool openExportFile = false;
            FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName + ".xls";
            saveFileDialog.InitialDirectory = AttachmentPath;
            saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
            saveFileDialog.ShowDialog();
            FileName = saveFileDialog.FileName;

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            string strFileName = this.saveFileDialog.FileName;

            Sped4.Classes.TelerikCls.clsExcelML exPort = new Sped4.Classes.TelerikCls.clsExcelML();
            exPort.InitClass(ref this._ctrMenu._frmMain, this.GL_User);
            exPort.ListHeaderText.Add(InitExportTitel());
            exPort.Telerik_RunExportToExcelML(ref this.dgv, strFileName, ref openExportFile, true, "Statistik");
            //exPort.Telerik_RunExportToExcelML(this.dgv, strFileName, ref openExportFile, true, "Statistik");

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(strFileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Bestand - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                }
            }
        }
        ///<summary>ctrStatistik / InitExportTitel</summary>
        ///<remarks></remarks>
        private string InitExportTitel()
        {
            //Headertext erstellt
            string txt = string.Empty;

            txt = "Statistik  ";
            //Kunde wenn gewählt
            if (tbAuftraggeber.Text != string.Empty)
            {
                txt = txt + Environment.NewLine +
                      "Kunde: " + ADR.ViewID + " - " + ADR.Name1;
            }
            //Zeitraum
            if ((dtpVon.Enabled) && (dtpBis.Enabled))
            {
                txt = txt + Environment.NewLine +
                      "Zeitraum: " + dtpVon.Value.ToShortDateString() + " bis " + dtpBis.Value.ToShortDateString();
            }
            if ((dtpVon.Enabled) && (!dtpBis.Enabled))
            {
                txt = txt + Environment.NewLine +
                      "Stichtag: " + dtpVon.Value.ToShortDateString();
            }
            return txt;
        }

        private void afMinMaxPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
