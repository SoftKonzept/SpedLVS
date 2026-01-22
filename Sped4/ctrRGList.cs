using Common.Enumerations;
using LVS;
using Sped4.Classes;
using Sped4.Classes.TelerikCls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace Sped4
{
    public partial class ctrRGList : UserControl
    {

        public Globals._GL_USER GL_User;
        internal clsADR ADR;
        internal ctrMenu _ctrMenu;
        internal clsTarif Tarif;
        internal clsFaktLager FaktLager;
        internal DataTable dtRGList = new DataTable("RGList");
        internal Int32 SearchButton = 0;

        internal DateTime dtAbrVon;
        internal DateTime dtAbrBis;

        //internal List<string> ListAttachmentPath;
        internal string AttachmentPath;
        internal string FileName;
        const string const_FileName = "_Rechnungsbuch";
        const string const_Headline = "Rechnungsbuch";
        DataTable dtOPAuftraggeber = new DataTable("OPAuftraggeber");
        DataTable dtTarife = new DataTable("Tarife");
        internal List<string> AttachmentList = new List<string>();
        internal BackgroundWorker bw;
        internal delegate void ThreadCtrInvokeEventHandler();

        public string DokumentenArt = string.Empty;
        public List<string> LogMessages { get; set; } = new List<string>();

        ///<summary>ctrRGList / ctrRGList</summary>
        ///<remarks></remarks>
        public ctrRGList()
        {
            InitializeComponent();
        }
        ///<summary>ctrRGList / ctrRGList_Load</summary>
        ///<remarks></remarks>
        private void ctrRGList_Load(object sender, EventArgs e)
        {
            SetAbrechnungsMonat(DateTime.Now.AddMonths(-1));
            AttachmentPath = this._ctrMenu._frmMain.GL_System.sys_WorkingPathExport;

            FaktLager = new clsFaktLager();
            FaktLager.Sys = this._ctrMenu._frmMain.system;
            FaktLager.bUseBKZ = this._ctrMenu._frmMain.system.Client.Modul.Lager_USEBKZ;
            FaktLager._GL_User = this.GL_User;
            FaktLager.Abrechnungsdatum = DateTime.Now;
            FaktLager.MandantenID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;

            Tarif = new clsTarif();
            Tarif._GL_User = this.GL_User;

            FaktLager.Rechnung = new clsRechnung();
            FaktLager.Rechnung.sys = this.FaktLager.Sys;
            FaktLager.Rechnung._GL_User = this.GL_User;
            FaktLager.Rechnung.MandantenID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
            FaktLager.Rechnung._GL_User = this.GL_User;
            FaktLager.Rechnung._GL_System = this._ctrMenu._frmMain.GL_System;

            FaktLager.PropertyChanged += new PropertyChangedEventHandler(UpdateProgress);

            ADR = new clsADR();
            ADR._GL_User = this.GL_User;

            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            RadGridLocalizationProvider.CurrentProvider = new clsGermanRadGridLocalizationProvider();
            SetAdrEingabeEnabeld(true);
            SetAuswahlRGDaten();
            this.tscbKunde.SelectedIndex = 0;
            CustomerSettings();
            InitDGV(false);
        }
        ///<summary>ctrEinlagerung / CustomerSettings</summary>
        ///<remarks>Hier kann den Kundenwünschen entsprechend ein / mehere Elemente 
        ///         ein-/ausgeblendet werden</remarks>
        private void CustomerSettings()
        {
            //tsbMailAnhang.Visible = this._ctrMenu._frmMain.system.Client.Modul.Fakt_SendRGAnhangMailExcel;
            tsbMailAnhang.Visible = (!this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion);
            tsbtnMailRGRGAnhang.Visible = tsbMailAnhang.Visible;

            this.cbSingleTarifCalculation.Visible = this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection;
            this.cbSingleTarifCalculation.Checked = false;
            this.splitPanel1.Collapsed = true;
            this.tsbtnCalcAll.Enabled = (!this._ctrMenu._frmMain.system.Client.Modul.Fakt_DeactivateMenueCtrRGList);
            this.cbSingleTarifCalculation.Visible = false;
        }
        /// <summary>
        /// Die Methode die als Reaktion auf das Event ausgeführt wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProgress(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Progress":
                    //MessageBox.Show(FaktLager.strProgress);
                    this._ctrMenu._frmMain.StatusBarWork(true, FaktLager.strProgress);
                    break;
                case "MaxProgress":
                    //MessageBox.Show(FaktLager.decMaxProgress.ToString());
                    this._ctrMenu._frmMain.ResetStatusBar();
                    this._ctrMenu._frmMain.InitStatusBar((Int32)FaktLager.decMaxProgress);
                    break;
            }
        }
        ///<summary>ctrRGList / SetAbrechnungsMonat</summary>
        ///<remarks></remarks>
        private void SetAbrechnungsMonat(DateTime myDate)
        {
            string strMonat = myDate.ToString("MMMM");
            Functions.SetToolStripComboToSelecetedItem(ref tscombo, strMonat);
            dtAbrVon = Functions.GetFirstDayOfMonth(myDate);
            dtAbrBis = Functions.GetLastDayOfMonth(myDate);
        }
        ///<summary>ctrRGList / ctrRGList_Load</summary>
        ///<remarks></remarks>
        private void ClearAdressEingabeFelder()
        {
            tbSearchA.Text = string.Empty;
            tbAuftraggeber.Text = string.Empty;
            SearchButton = 0;
        }
        ///<summary>ctrRGList / ctrRGList_Load</summary>
        ///<remarks></remarks>
        private void SetAdrEingabeEnabeld(bool bEnabled)
        {
            btnSearchA.Enabled = bEnabled;
            tbSearchA.Enabled = bEnabled;
            tbAuftraggeber.Enabled = bEnabled;
        }
        ///<summary>ctrRGList / SetAuswahlRGDaten</summary>
        ///<remarks></remarks>
        private void SetAuswahlRGDaten()
        {
            DateTime dtTmp = DateTime.Now.AddMonths(-1);
            //Datum
            string strTmp = "01." + dtTmp.Month.ToString() + "." + DateTime.Now.Year.ToString();
            dtpVon.Value = Functions.GetFirstDayOfMonth(System.Convert.ToDateTime(strTmp));
            dtpBis.Value = Functions.GetLastDayOfMonth(System.Convert.ToDateTime(strTmp));
            ClearAdressEingabeFelder();
        }
        ///<summary>ctrRGList / btnSearchA_Click</summary>
        ///<remarks></remarks>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrRGList / tbSearchA_TextChanged</summary>
        ///<remarks></remarks>
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
        ///<summary>ctrRGList / tsbtnSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            InitDGV(false);
        }
        ///<summary>ctrRGList / InitDGV</summary>
        ///<remarks></remarks>
        public void InitDGV(bool bGetAllWorkspaces)
        {
            string strHeadLine = "Rechnungen ";
            if (!bGetAllWorkspaces)
            {
                strHeadLine = strHeadLine + "[" + aktuellerArbeitsbereichToolStripMenuItem.Text + "]";
            }
            else
            {
                strHeadLine = strHeadLine + "[" + überAlleArbeitsbereicheToolStripMenuItem.Text + "]";
            }
            afColorLabel1.myText = strHeadLine;
            this._ctrMenu._frmMain.ResetStatusBar();
            this._ctrMenu._frmMain.InitStatusBar(4);
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            this._ctrMenu._frmMain.StatusBarWork(false, "Daten werden geladen und initialisiert...");

            FaktLager.Sys = this._ctrMenu._frmMain.system;
            FaktLager.Rechnung.sys = this._ctrMenu._frmMain.system;
            FaktLager.Rechnung.RGDatumVon = dtpVon.Value;
            FaktLager.Rechnung.RGDatumBis = dtpBis.Value;
            FaktLager.Rechnung.Empfaenger = ADR.ID;
            dtRGList.Clear();
            this._ctrMenu._frmMain.StatusBarWork(false, "Daten werden geladen und initialisiert...");
            dtRGList = FaktLager.Rechnung.GetRechnungListForPeriode(bGetAllWorkspaces);
            this.dgv.DataSource = dtRGList;
            for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
            {
                string strColName = this.dgv.Columns[i].Name;
                switch (strColName)
                {
                    case "Datum":
                        this.dgv.Columns[i].FormatString = "{0:d}";
                        break;
                    case "Netto €":
                    case "Brutto €":
                    case "MWSt €":
                        this.dgv.Columns[i].FormatString = "{0:c}";
                        this.dgv.Columns[i].IsVisible = true;
                        break;
                    case "Einlagerung":
                    case "Auslagerung":
                    case "Lagerkosten":
                    case "Nebenkosten":
                        this.dgv.Columns[i].FormatString = "{0:c}";
                        this.dgv.Columns[i].IsVisible = false;
                        break;
                    case "FibuInfo":
                        this.dgv.Columns[i].IsVisible = true;
                        this.dgv.Columns[i].MaxWidth = 200;
                        this.dgv.Columns[i].WrapText = true;
                        break;
                    case "Beleg":
                    case "Kunde":
                    case "ID":
                    case "Art":
                    case "RGArt":
                        this.dgv.Columns[i].IsVisible = true;
                        break;
                    default:
                        this.dgv.Columns[i].IsVisible = false;
                        break;
                }
            }
            //Berechnung Bestanddaten
            CalcSumGridDaten();
            this.dgv.BestFitColumns();
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);


            if (this.dgv.Rows.Count > 0)
            {
                if (this.dgv.CurrentRow.Index > -1)
                {
                    decimal decTmp = 0;
                    string strTmp = this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["ID"].Value.ToString();
                    if (Decimal.TryParse(strTmp, out decTmp))
                    {
                        FaktLager.Rechnung.ID = decTmp;
                        FaktLager.Rechnung.Fill();
                    }
                }
            }
        }
        ///<summary>ctrRGList / CalcSumGridDaten</summary>
        ///<remarks></remarks>
        private void CalcSumGridDaten()
        {
            Int32 iTmp = 0;
            iTmp = dtRGList.Rows.Count;
            decimal decTmpNetto = 0;
            decimal decTmpBrutto = 0;
            if (iTmp > 0)
            {
                foreach (DataRow row in dtRGList.Rows)
                {
                    decimal dec1 = 0;
                    Decimal.TryParse(row["Netto €"].ToString(), out dec1);
                    decTmpNetto = decTmpNetto + dec1;

                    decimal dec2 = 0;
                    Decimal.TryParse(row["Brutto €"].ToString(), out dec2);
                    decTmpBrutto = decTmpBrutto + dec2;
                }
            }
            tbAnzahl.Text = iTmp.ToString();
            tbNetto.Text = Functions.FormatDecimal(decTmpNetto);
            tbBrutto.Text = Functions.FormatDecimal(decTmpBrutto);
        }
        ///<summary>ctrRGList / tsbtnClear_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            InitDGV(false);
        }
        ///<summary>ctrRGList / tsbtnExcel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnExcel_Click(object sender, EventArgs e)
        {
            FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName + ".xls";
            saveFileDialog.InitialDirectory = AttachmentPath;
            saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
            saveFileDialog.ShowDialog();
            FileName = saveFileDialog.FileName;

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }

            bool openExportFile = false;
            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgv, FileName, ref openExportFile, this.GL_User, true);

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(FileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Rechnungsbuch - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrRGList / btnSearchA_Click</summary>
        ///<remarks>Adressdaten werden anhand der ID ermittelt und in die entsprechenden
        ///         Eingabefelder übergeben. Der Matchcode aus den Adressen wird vorerst
        ///         hier mitübernommen wird später noch einmal überschrieben.</remarks>
        public void SetADRByID(decimal ADR_ID)
        {
            switch (SearchButton)
            {
                case 1:
                    ADR.ID = ADR_ID;
                    ADR.Fill();
                    tbSearchA.Text = ADR.ViewID;
                    tbAuftraggeber.Text = ADR.Name1 + " - " + ADR.PLZ + " - " + ADR.Ort;
                    break;
            }
            SearchButton = 0;
        }
        ///<summary>ctrRGList / tsbtnCalcAll_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCalcAll_Click(object sender, EventArgs e)
        {
            DateTime dtTmp = DateTime.Now;
            Int32 iMonat = tscombo.SelectedIndex + 1;
            string strTmp = "01." + iMonat.ToString() + "." + DateTime.Now.Year.ToString();
            DateTime.TryParse(strTmp, out dtTmp);
            SetAbrechnungsMonat(dtTmp);
            decimal KundenID = 0;
            if (
                    (tbAuftraggeber.Text != string.Empty && tscbKunde.Text == "Kunde") ||
                    (tbAuftraggeber.Text != string.Empty && cbSingleTarifCalculation.Checked)
                )
            {
                KundenID = ADR.ID;
            }
            if (ADR.ID > 0 || tscbKunde.Text == "Gesamt")
            {
                FaktLager.GL_System = this._ctrMenu._frmMain.GL_System;
                FaktLager.MandantenID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
                //FaktLager.CalcAll(dtAbrVon, dtAbrBis, dtAbrBis, ref this._ctrMenu._frmMain, KundenID);
                dtAbrVon = this.dtpVon.Value;
                dtAbrBis = this.dtpBis.Value;
                FaktLager.CalcAll(dtAbrVon, dtAbrBis, dtAbrBis, this.cbSingleTarifCalculation.Checked, KundenID, this.Tarif.ID);
                InitDGV(false);
            }
        }
        ///<summary>ctrRGList / tsbtnCalcAll_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCloseCtr_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrRGList();
        }
        ///<summary>ctrRGList / dgv_Click</summary>
        ///<remarks></remarks>
        private void dgv_Click(object sender, EventArgs e)
        {
            //if (this.dgv.Rows.Count > 0)
            //{
            //    if (this.dgv.CurrentRow.Index > -1)
            //    {
            //        decimal decTmp = 0;
            //        string strTmp = this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["ID"].Value.ToString();
            //        if (Decimal.TryParse(strTmp, out decTmp))
            //        {
            //            FaktLager.Rechnung.ID = decTmp;
            //            FaktLager.Rechnung.Fill();
            //            //Manuelle REchnungen können gelöscht werden
            //            this.tsbtnDeleteRGGS.Enabled = FaktLager.Rechnung.RGArt.Contains(clsRechnung.const_RechnungsArt_Manuell);
            //        }
            //    }
            //}
        }
        ///<summary>ctrRGList / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void dgv_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                if (e.RowIndex > -1)
                {
                    this.tsbtnDeleteRGGS.Enabled = false;
                    decimal decTmp = 0;
                    string strTmp = this.dgv.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                    if (Decimal.TryParse(strTmp, out decTmp))
                    {
                        FaktLager.Rechnung.ID = decTmp;
                        FaktLager.Rechnung.Fill();
                        //Manuelle REchnungen können gelöscht werden
                        this.tsbtnDeleteRGGS.Enabled = ((FaktLager.Rechnung.RGArt.Contains(clsRechnung.const_RechnungsArt_Manuell)) && (!FaktLager.Rechnung.Druck));
                    }
                }
            }
        }
        ///<summary>ctrRGList / dtpVon_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpVon_ValueChanged(object sender, EventArgs e)
        {
            DateTime ZeitRaumMonat = ((DateTimePicker)sender).Value.Date;
            dtpVon.Value = ((DateTimePicker)sender).Value;
            dtpBis.Value = Functions.GetLastDayOfMonth(ZeitRaumMonat);
        }
        ///<summary>ctrRGList / tsbtnPrintRGBook_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrintRGBook_Click(object sender, EventArgs e)
        {
            if (clsRechnung.CountRGForRGBook(GL_User) > 0)
            {
                this.DokumentenArt = enumDokumentenArt.RGBuch.ToString();
                this._ctrMenu.OpenFrmReporView(this, true);
                clsRechnung.UpdatePrintRGBook(this.GL_User);
            }
            else
            {
                if (clsMessages.Fakturierung_RGBookPrintAgain())
                {
                    this.DokumentenArt = enumDokumentenArt.RGBuch.ToString();
                    this._ctrMenu.OpenFrmReporView(this, true);
                }
                else
                {
                    clsMessages.Fakturierung_RGBookNoRG();
                }
            }
        }
        ///<summary>ctrRGList / tsbtnStorno_Click</summary>
        ///<remarks></remarks>
        private void tsbtnStorno_Click(object sender, EventArgs e)
        {
            FaktLager.Rechnung.MandantenID = this.FaktLager.MandantenID;
            if (this.FaktLager.Rechnung.RGBookPrintDate == Globals.DefaultDateTimeMinValue)
            {
                if (
                    (this.FaktLager.Rechnung.ID > 0) &&
                    (this.FaktLager.Rechnung.StornoID == 0) &&
                    (this.FaktLager.Rechnung.ExistStornoZurRG == false)
                    )
                {
                    FaktLager.Rechnung.MandantenID = this.FaktLager.MandantenID;
                    FaktLager.Rechnung.CreateStorno();
                }
                InitDGV(false);
            }
            else
            {
                clsMessages.Fakturierung_RGBookPrinted();
            }
        }
        ///<summary>ctrRGList / tsbtnPrintRGALL_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrintRGALL_Click(object sender, EventArgs e)
        {
            //REchnung

            if (this.dgv.Rows.Count > 0)
            {
                foreach (DataRow dr in ((DataTable)(dgv.DataSource)).Rows)
                {
                    this.FaktLager.Rechnung.ID = (decimal)dr["ID"];
                    this.FaktLager.Rechnung.Fill();
                    if (this.FaktLager.Rechnung.Druck == false)
                    {
                        this.DokumentenArt = enumDokumentenArt.LagerRechnung.ToString();
                        this._ctrMenu.OpenFrmReporView(this, true);
                        Thread.Sleep(100);
                        List<decimal> AuftraggeberMat = new List<decimal>();
                        bool bUseMatDoc = false;
                        clsSystem sys = new clsSystem(Application.StartupPath);
                        sys.GetAuftraggeberAnhangMat(ref _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_System.sys_MandantenID);
                        if (_ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat != string.Empty)
                        {

                            string[] AuftraggeberSplit = _ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat.Split(',');
                            foreach (string auftraggeber in AuftraggeberSplit)
                            {
                                if (this.FaktLager.Rechnung.Empfaenger == clsADR.GetIDByMatchcode(auftraggeber))
                                {
                                    bUseMatDoc = true;
                                }
                            }
                        }
                        if (true) { }
                        ctrPrintLager TmpPrint = new ctrPrintLager();
                        TmpPrint.Hide();
                        TmpPrint._ctrMenu = this._ctrMenu;


                        if (!bUseMatDoc)
                        {
                            this.DokumentenArt = enumDokumentenArt.RGAnhang.ToString();
                        }
                        else
                        {
                            this.DokumentenArt = enumDokumentenArt.RGAnhangMat.ToString();
                        }
                        this._ctrMenu.OpenFrmReporView(this, true);

                        this.FaktLager.Rechnung.Druck = true;
                        this.FaktLager.Rechnung.Druckdatum = DateTime.Now;
                        this.FaktLager.Rechnung.UpdateRechnungPrint();
                    }
                }
            }
        }
        /////<summary>ctrRGList / toolStripLabel2_Click</summary>
        /////<remarks></remarks>
        //private void toolStripLabel2_Click(object sender, EventArgs e)
        //{
        //   DateTime dtTmp = DateTime.Now;
        //   Int32 iMonat = tscombo.SelectedIndex + 1;
        //   string strTmp = "01." + iMonat.ToString() + "." + DateTime.Now.Year.ToString();
        //   DateTime.TryParse(strTmp, out dtTmp);
        //   SetAbrechnungsMonat(dtTmp);
        //   FaktLager._GL_System = this._ctrMenu._frmMain.GL_System;
        //   FaktLager.MandantenID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
        //   //FaktLager.CalcAll(dtAbrVon, dtAbrBis, dtAbrBis, ref this._ctrMenu._frmMain, KundenID);
        //   FaktLager.testData(dtAbrVon, dtAbrBis, dtAbrBis,this.ADR.ID);
        //}
        ///<summary>ctrRGList / tsbMailAnhang_Click</summary>
        ///<remarks></remarks>
        private void tsbMailAnhang_Click(object sender, EventArgs e)
        {
            FaktLager.SendAnhangAsMail(dtAbrBis, this._ctrMenu._frmMain.system);
        }
        ///<summary>ctrRGList / tsbtnPrint_Click_1</summary>
        ///<remarks>Vorschau</remarks>
        private void tsbtnPrint_Click_1(object sender, EventArgs e)
        {
            PrintOrPreview(false);
            //if (!this.FaktLager.Rechnung.Druck)
            //{
            //   //REchnung
            //   //this.DokumentenArt = Globals.enumDokumentenart.LagerRechnung.ToString();
            //   switch (this.FaktLager.Rechnung.RGArt)
            //   {
            //       case clsRechnung.const_RechnungsArt_Lager:
            //           this.DokumentenArt = Globals.enumDokumentenart.LagerRechnung.ToString();
            //           break;
            //       case clsRechnung.const_RechnungsArt_Manuell:
            //           this.DokumentenArt = Globals.enumDokumentenart.manuelleRGGS.ToString();
            //           break;
            //       default:
            //           this.DokumentenArt = Globals.enumDokumentenart.LagerRechnung.ToString();
            //           break;
            //   }
            //   this._ctrMenu.OpenFrmReporView(this, false );
            //   Thread.Sleep(100);

            //   if (this.DokumentenArt == Globals.enumDokumentenart.LagerRechnung.ToString())
            //   {
            //       List<decimal> AuftraggeberMat = new List<decimal>();
            //       bool bUseMatDoc = false;
            //       clsSystem sys = new clsSystem();
            //       sys.GetAuftraggeberAnhangMat(ref _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_System.sys_MandantenID);
            //       if (_ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat != string.Empty)
            //       {

            //           string[] AuftraggeberSplit = _ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat.Split(',');
            //           foreach (string auftraggeber in AuftraggeberSplit)
            //           {
            //               if (this.FaktLager.Rechnung.Empfaenger == clsADR.GetIDByMatchcode(auftraggeber))
            //               {
            //                   bUseMatDoc = true;
            //               }
            //           }
            //       }
            //       if (true) { }
            //       ctrPrintLager TmpPrint = new ctrPrintLager();
            //       TmpPrint.Hide();
            //       TmpPrint._ctrMenu = this._ctrMenu;

            //       if (!bUseMatDoc)
            //       {
            //           this.DokumentenArt = Globals.enumDokumentenart.RGAnhang.ToString();
            //       }
            //       else
            //       {
            //           this.DokumentenArt = Globals.enumDokumentenart.RGAnhangMat.ToString();
            //       }

            //       //this.DokumentenArt = Globals.enumDokumentenart.RGAnhang.ToString();
            //       this._ctrMenu.OpenFrmReporView(this, false);
            //   }
            //   this.FaktLager.Rechnung.Druck = true;
            //   this.FaktLager.Rechnung.Druckdatum = DateTime.Now;
            //   this.FaktLager.Rechnung.UpdateRechnungPrint();
            //   this.FaktLager.Rechnung.Fill();
            //}
            //else
            //{
            //   if (clsMessages.Fakturierung_RGGSPrintAgain())
            //   {
            //       switch (this.FaktLager.Rechnung.RGArt)
            //       { 
            //           case clsRechnung.const_RechnungsArt_Lager:
            //               this.DokumentenArt = Globals.enumDokumentenart.LagerRechnung.ToString();
            //               break;
            //           case clsRechnung.const_RechnungsArt_Manuell:
            //               this.DokumentenArt = Globals.enumDokumentenart.manuelleRGGS.ToString();
            //               break;
            //           default:
            //               this.DokumentenArt = Globals.enumDokumentenart.LagerRechnung.ToString();
            //               break;
            //       }
            //      this._ctrMenu.OpenFrmReporView(this, false);
            //      Thread.Sleep(100);

            //      //Wenn Lagerrechnung dann Anhang
            //      if (this.DokumentenArt == Globals.enumDokumentenart.LagerRechnung.ToString())
            //      {

            //          List<decimal> AuftraggeberMat = new List<decimal>();
            //          bool bUseMatDoc = false;
            //          clsSystem sys = new clsSystem();
            //          sys.GetAuftraggeberAnhangMat(ref _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_System.sys_MandantenID);
            //          if (_ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat != string.Empty)
            //          {

            //              string[] AuftraggeberSplit = _ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat.Split(',');
            //              foreach (string auftraggeber in AuftraggeberSplit)
            //              {
            //                  if (this.FaktLager.Rechnung.Empfaenger == clsADR.GetIDByMatchcode(auftraggeber))
            //                  {
            //                      bUseMatDoc = true;
            //                  }
            //              }
            //          }
            //          if (true) { }
            //          ctrPrintLager TmpPrint = new ctrPrintLager();
            //          TmpPrint.Hide();
            //          TmpPrint._ctrMenu = this._ctrMenu;


            //          if (!bUseMatDoc)
            //          {
            //              this.DokumentenArt = Globals.enumDokumentenart.RGAnhang.ToString();
            //          }
            //          else
            //          {
            //              this.DokumentenArt = Globals.enumDokumentenart.RGAnhangMat.ToString();
            //          }

            //          this._ctrMenu.OpenFrmReporView(this, false);
            //      }
            //   }
            //}
        }
        ///<summary>ctrRGList / tsbtnPrint_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            PrintOrPreview(true);
            ///**
            //this.DokumentenArt = Globals.enumDokumentenart.LagerRechnung.ToString();
            //this._ctrMenu.OpenFrmReporView(this, false);
            //***/
            //if (!this.FaktLager.Rechnung.Druck)
            //{
            //    //REchnung
            //    this.DokumentenArt = Globals.enumDokumentenart.LagerRechnung.ToString();
            //    this._ctrMenu.OpenFrmReporView(this, true);
            //    Thread.Sleep(100);

            //    List<decimal> AuftraggeberMat = new List<decimal>();
            //    bool bUseMatDoc = false;
            //    clsSystem sys = new clsSystem();
            //    sys.GetAuftraggeberAnhangMat(ref _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_System.sys_MandantenID);
            //    if (_ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat != string.Empty)
            //    {

            //        string[] AuftraggeberSplit = _ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat.Split(',');
            //        foreach (string auftraggeber in AuftraggeberSplit)
            //        {
            //            if (this.FaktLager.Rechnung.Empfaenger == clsADR.GetIDByMatchcode(auftraggeber))
            //            {
            //                bUseMatDoc = true;
            //            }
            //        }
            //    }
            //    if (true) { }
            //    ctrPrintLager TmpPrint = new ctrPrintLager();
            //    TmpPrint.Hide();
            //    TmpPrint._ctrMenu = this._ctrMenu;


            //    if (!bUseMatDoc)
            //    {
            //        this.DokumentenArt = Globals.enumDokumentenart.RGAnhang.ToString();
            //    }
            //    else
            //    {
            //        this.DokumentenArt = Globals.enumDokumentenart.RGAnhangMat.ToString();
            //    }


            //    //this.DokumentenArt = Globals.enumDokumentenart.RGAnhang.ToString();
            //    this._ctrMenu.OpenFrmReporView(this, true);

            //    this.FaktLager.Rechnung.Druck = true;
            //    this.FaktLager.Rechnung.Druckdatum = DateTime.Now;
            //    this.FaktLager.Rechnung.UpdateRechnungPrint();
            //    this.FaktLager.Rechnung.Fill();
            //}
            //else
            //{
            //    if (clsMessages.Fakturierung_RGGSPrintAgain())
            //    {
            //        // this._ctrMenu.OpenFrmReporView(this, true);
            //        this.DokumentenArt = Globals.enumDokumentenart.LagerRechnung.ToString();
            //        this._ctrMenu.OpenFrmReporView(this, true);
            //        Thread.Sleep(100);
            //        List<decimal> AuftraggeberMat = new List<decimal>();
            //        bool bUseMatDoc = false;
            //        clsSystem sys = new clsSystem();
            //        sys.GetAuftraggeberAnhangMat(ref _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_System.sys_MandantenID);
            //        if (_ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat != string.Empty)
            //        {

            //            string[] AuftraggeberSplit = _ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat.Split(',');
            //            foreach (string auftraggeber in AuftraggeberSplit)
            //            {
            //                if (this.FaktLager.Rechnung.Empfaenger == clsADR.GetIDByMatchcode(auftraggeber))
            //                {
            //                    bUseMatDoc = true;
            //                }
            //            }
            //        }
            //        if (true) { }
            //        ctrPrintLager TmpPrint = new ctrPrintLager();
            //        TmpPrint.Hide();
            //        TmpPrint._ctrMenu = this._ctrMenu;


            //        if (!bUseMatDoc)
            //        {
            //            this.DokumentenArt = Globals.enumDokumentenart.RGAnhang.ToString();
            //        }
            //        else
            //        {
            //            this.DokumentenArt = Globals.enumDokumentenart.RGAnhangMat.ToString();
            //        }

            //        this._ctrMenu.OpenFrmReporView(this, true);
            //    }
            //}
        }
        ///<summary>ctrRGList / PrintOrPreview</summary>
        ///<remarks></remarks>
        private void PrintOrPreview(bool bDirectPrint)
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                if (!this.FaktLager.Rechnung.Druck)
                {
                    //REchnung
                    //this.DokumentenArt = Globals.enumDokumentenart.LagerRechnung.ToString();
                    switch (this.FaktLager.Rechnung.RGArt)
                    {
                        case clsRechnung.const_RechnungsArt_Lager:
                            this.DokumentenArt = enumDokumentenArt.LagerRechnung.ToString();
                            break;
                        case clsRechnung.const_RechnungsArt_Manuell:
                            this.DokumentenArt = enumDokumentenArt.manuelleRGGS.ToString();
                            break;
                        default:
                            this.DokumentenArt = enumDokumentenArt.LagerRechnung.ToString();
                            break;
                    }
                    this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                    Thread.Sleep(100);

                    if (this.DokumentenArt == enumDokumentenArt.LagerRechnung.ToString())
                    {
                        List<decimal> AuftraggeberMat = new List<decimal>();
                        bool bUseMatDoc = false;
                        clsSystem sys = new clsSystem(Application.StartupPath);
                        sys.GetAuftraggeberAnhangMat(ref _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_System.sys_MandantenID);
                        if (_ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat != string.Empty)
                        {
                            string[] AuftraggeberSplit = _ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat.Split(',');
                            foreach (string auftraggeber in AuftraggeberSplit)
                            {
                                if (this.FaktLager.Rechnung.Empfaenger == clsADR.GetIDByMatchcode(auftraggeber))
                                {
                                    bUseMatDoc = true;
                                }
                            }
                        }
                        if (true) { }
                        ctrPrintLager TmpPrint = new ctrPrintLager();
                        TmpPrint.Hide();
                        TmpPrint._ctrMenu = this._ctrMenu;

                        if (!bUseMatDoc)
                        {
                            this.DokumentenArt = enumDokumentenArt.RGAnhang.ToString();
                        }
                        else
                        {
                            this.DokumentenArt = enumDokumentenArt.RGAnhangMat.ToString();
                        }

                        //this.DokumentenArt = Globals.enumDokumentenart.RGAnhang.ToString();
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                    }

                    if (bDirectPrint)
                    {
                        this.FaktLager.Rechnung.Druck = true;
                        this.FaktLager.Rechnung.Druckdatum = DateTime.Now;
                        this.FaktLager.Rechnung.UpdateRechnungPrint();
                        this.FaktLager.Rechnung.Fill();
                    }
                }
                else
                {
                    if (clsMessages.Fakturierung_RGGSPrintAgain())
                    {
                        switch (this.FaktLager.Rechnung.RGArt)
                        {
                            case clsRechnung.const_RechnungsArt_Lager:
                                this.DokumentenArt = enumIniDocKey.Lagerrechnung.ToString();
                                break;

                            case clsRechnung.const_RechnungsArt_Manuell:
                                this.DokumentenArt = enumIniDocKey.manuelleRGGS.ToString();
                                break;

                            default:
                                this.DokumentenArt = enumIniDocKey.Lagerrechnung.ToString();
                                break;
                        }
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                        Thread.Sleep(100);

                        if (this._ctrMenu._frmMain.system.Client.Modul.Print_Documents_UseRGAnhang)
                        {
                            //Wenn Lagerrechnung dann Anhang
                            if (this.DokumentenArt == enumIniDocKey.Lagerrechnung.ToString())
                            {

                                List<decimal> AuftraggeberMat = new List<decimal>();
                                bool bUseMatDoc = false;
                                clsSystem sys = new clsSystem(Application.StartupPath);
                                sys.GetAuftraggeberAnhangMat(ref _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_System.sys_MandantenID);
                                if (_ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat != string.Empty)
                                {

                                    string[] AuftraggeberSplit = _ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat.Split(',');
                                    foreach (string auftraggeber in AuftraggeberSplit)
                                    {
                                        if (this.FaktLager.Rechnung.Empfaenger == clsADR.GetIDByMatchcode(auftraggeber))
                                        {
                                            bUseMatDoc = true;
                                        }
                                    }
                                }
                                if (true) { }
                                ctrPrintLager TmpPrint = new ctrPrintLager();
                                TmpPrint.Hide();
                                TmpPrint._ctrMenu = this._ctrMenu;


                                if (!bUseMatDoc)
                                {
                                    this.DokumentenArt = enumDokumentenArt.RGAnhang.ToString();
                                }
                                else
                                {
                                    this.DokumentenArt = enumDokumentenArt.RGAnhangMat.ToString();
                                }

                                this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                            }
                        }
                    }
                }
            }
            else
            {
                this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.FaktLager.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
                //REchnung
                switch (this.FaktLager.Rechnung.RGArt)
                {
                    case clsRechnung.const_RechnungsArt_Lager:
                        this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.Lagerrechnung.ToString());
                        //this.DokumentenArt = Globals.enumIniDocKey.Lagerrechnung.ToString();
                        break;
                    case clsRechnung.const_RechnungsArt_Manuell:
                        if (this.FaktLager.Rechnung.GS)
                        {
                            this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.ManuelleGutschrift.ToString());
                        }
                        else
                        {
                            this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.Manuellerechnung.ToString());
                        }
                        //this.DokumentenArt = Globals.enumIniDocKey.manuelleRGGS.ToString();
                        break;
                    default:
                        //this.DokumentenArt = Globals.enumIniDocKey.Lagerrechnung.ToString();
                        this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.Lagerrechnung.ToString());
                        break;
                }

                if (this.FaktLager.RepDocSettings is clsReportDocSetting)
                {
                    this.DokumentenArt = this.FaktLager.RepDocSettings.DocKey;
                    this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                    Thread.Sleep(100);

                    if (this.FaktLager.RepDocSettings.DocKey.Equals(enumIniDocKey.Lagerrechnung.ToString()))
                    {
                        //RG Anhang
                        this.FaktLager.RepDocSettings = null;
                        this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.RGAnhang.ToString());
                        if (this.FaktLager.RepDocSettings is clsReportDocSetting)
                        {
                            this.DokumentenArt = this.FaktLager.RepDocSettings.DocKey;
                            this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                            Thread.Sleep(100);
                        }
                    }
                    //--- Wenn noch nicht gedruckt und das Dokument gedruckt wird, dann update in DB
                    if (!this.FaktLager.Rechnung.Druck)
                    {
                        if (bDirectPrint)
                        {
                            this.FaktLager.Rechnung.Druck = true;
                            this.FaktLager.Rechnung.Druckdatum = DateTime.Now;
                            this.FaktLager.Rechnung.UpdateRechnungPrint();
                            this.FaktLager.Rechnung.Fill();
                        }
                    }
                }
            }
        }
        ///<summary>ctrRGList / cbSingleTarifCalculation_ToggleStateChanged</summary>
        ///<remarks></remarks>
        private void cbSingleTarifCalculation_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            tbSearchA.Text = string.Empty;
            this.splitPanel1.Collapsed = (!cbSingleTarifCalculation.Checked);
            if (cbSingleTarifCalculation.Checked)
            {
                InitDGVOPAuftraggeber();
                InitDGVTarif();
            }
        }
        ///<summary>ctrRGList / InitDGVAbrKunden</summary>
        ///<remarks>Füllt das Grid mit den abzurechnenden Kunden.</remarks>
        private void InitDGVOPAuftraggeber()
        {
            //Grid abzurechnende Kunden lagen
            dtOPAuftraggeber.Clear();
            FaktLager.VonZeitraum = dtpVon.Value;
            FaktLager.BisZeitraum = dtpBis.Value;
            dtOPAuftraggeber = FaktLager.dtAbrKunden;
            this.dgvOPAuftraggeber.DataSource = dtOPAuftraggeber;
            if (dgvOPAuftraggeber.Rows.Count > 0)
            {
                this.dgvOPAuftraggeber.Columns["AuftraggeberID"].IsVisible = false;
            }

            if (this.dgvOPAuftraggeber.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvOPAuftraggeber.Rows.Count - 1; i++)
                {
                    if (this.dgvOPAuftraggeber.Rows[i] != null)
                    {
                        decimal decTmp = 0;
                        string strTmp = this.dgvOPAuftraggeber.Rows[i].Cells["AuftraggeberID"].Value.ToString();
                        Decimal.TryParse(strTmp, out decTmp);
                        if (decTmp > 0)
                        {
                            ADR.ID = decTmp;
                            ADR.FillClassOnly();
                            InitLoadAfterAuftraggeberSelection();
                            break;
                        }
                    }
                }
            }
        }
        ///<summary>ctrRGList / dgvOPAuftraggeber_CellClick</summary>
        ///<remarks></remarks>
        private void InitLoadAfterAuftraggeberSelection()
        {
            Tarif._GL_User = this.GL_User;
            Tarif.AdrID = ADR.ID;
            FaktLager.Auftraggeber = ADR.ID;
            tbSearchA.Text = ADR.ViewID;
            InitDGVTarif();
            SearchButton = 1;
        }
        ///<summary>ctrRGList / InitDGVAbrKunden</summary>
        ///<remarks></remarks>
        private void InitDGVTarif()
        {
            //ClearFrm();
            dtTarife.Clear();
            Tarif.AdrID = ADR.ID;
            dtTarife = clsTarif.GetTarifeByAdrID(this.GL_User, Tarif.AdrID, true, dtpVon.Value, dtpBis.Value, this._ctrMenu._frmMain.system.AbBereich.ID);
            this.dgvTarife.DataSource = dtTarife;
            //Columns visible setzen

            if (this.dgvTarife.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvTarife.Columns.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        if (this.dgvTarife.Rows[i] != null)
                        {
                            decimal decTmp = 0;
                            string strTarifID = this.dgvTarife.Rows[i].Cells["ID"].Value.ToString();
                            if (Decimal.TryParse(strTarifID, out decTmp))
                            {
                                SetAndFillTarifandTarifPos(decTmp);
                            }
                        }
                    }
                    string strTmp = this.dgvTarife.Columns[i].Name.ToString();
                    if (strTmp == "Tarifname")
                    {
                        this.dgvTarife.Columns[i].IsVisible = true;
                        this.dgvTarife.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                    }
                    else
                    {
                        this.dgvTarife.Columns[i].IsVisible = false;
                    }
                }
                this.dgvTarife.ClearSelection();
            }
        }
        ///<summary>ctrRGList / SetAndFillTarifandTarifPos</summary>
        ///<remarks></remarks>
        private void SetAndFillTarifandTarifPos(decimal myTarifID)
        {
            Tarif = new clsTarif();
            Tarif._GL_User = this.GL_User;
            Tarif.ID = myTarifID;
            Tarif.Fill();
        }
        ///<summary>ctrRGList / tsbtnRefreshAuftraggeber_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefreshAuftraggeber_Click(object sender, EventArgs e)
        {
            InitDGVOPAuftraggeber();
        }
        ///<summary>ctrRGList / tsbtnRefreshTarife_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefreshTarife_Click(object sender, EventArgs e)
        {
            InitDGVTarif();
        }
        ///<summary>ctrRGList / dgvOPAuftraggeber_Click</summary>
        ///<remarks></remarks>
        private void dgvOPAuftraggeber_Click(object sender, EventArgs e)
        {
            if (this.dgvOPAuftraggeber.CurrentCell != null)
            {
                string strTmp = this.dgvOPAuftraggeber.Rows[this.dgvOPAuftraggeber.CurrentCell.RowIndex].Cells["AuftraggeberID"].Value.ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    this.ADR.ID = decTmp;
                    this.ADR.FillClassOnly();
                    if (clsADR.ExistAdrID(decTmp, this.GL_User.User_ID))
                    {
                        InitLoadAfterAuftraggeberSelection();
                        tabControl1.SelectTab(tpTarife);
                    }
                }
            }
        }
        ///<summary>ctrRGList / dgvOPAuftraggeber_SelectionChanged</summary>
        ///<remarks></remarks>
        private void dgvOPAuftraggeber_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvOPAuftraggeber.CurrentCell != null)
            {
                string strTmp = this.dgvOPAuftraggeber.Rows[this.dgvOPAuftraggeber.CurrentCell.RowIndex].Cells["AuftraggeberID"].Value.ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    this.ADR.ID = decTmp;
                    this.ADR.FillClassOnly();
                    if (clsADR.ExistAdrID(decTmp, this.GL_User.User_ID))
                    {
                        InitLoadAfterAuftraggeberSelection();
                        tabControl1.SelectTab(tpTarife);
                    }
                }
            }
        }
        ///<summary>ctrRGList / dgvTarife_SelectionChanged</summary>
        ///<remarks></remarks>
        private void dgvTarife_SelectionChanged(object sender, EventArgs e)
        {
            string strTarif = string.Empty;
            if (this.dgvTarife.CurrentCell != null)
            {
                strTarif = this.dgvTarife.Rows[this.dgvTarife.CurrentCell.RowIndex].Cells["Tarifname"].Value.ToString();
                decimal decTmp = 0;
                string strTarifID = this.dgvTarife.Rows[this.dgvTarife.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                if (Decimal.TryParse(strTarifID, out decTmp))
                {
                    Tarif = new clsTarif();
                    Tarif._GL_User = this.GL_User;
                    Tarif.ID = decTmp;
                    Tarif.Fill();
                }
            }
        }
        ///<summary>ctrRGList / tsbtnExcelExport_Click</summary>
        ///<remarks></remarks>
        private void tsbtnExcelExport_Click(object sender, EventArgs e)
        {
            bool openExportFile = false;
            if (this._ctrMenu._frmMain.system.Client.Modul.Excel_UseOldExport)
            {
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

                Sped4.Classes.TelerikCls.clsExcelML exPort = new clsExcelML();
                exPort.InitClass(ref this._ctrMenu._frmMain, this.GL_User);

                //Headertext erstellt
                string txt = string.Empty;

                txt = "Rechnungsbuch  ";
                exPort.ListHeaderText.Add(txt);
                //Kunde wenn gewählt
                if (tbAuftraggeber.Text != string.Empty)
                {
                    txt = "Auftraggeber: " + ADR.ViewID + " - " + ADR.Name1;
                    exPort.ListHeaderText.Add(txt);
                }
                //Zeitraum
                if ((dtpVon.Enabled) && (dtpBis.Enabled))
                {
                    txt = "Zeitraum: " + dtpVon.Value.ToShortDateString() + " bis " + dtpBis.Value.ToShortDateString();
                }
                if ((dtpVon.Enabled) && (!dtpBis.Enabled))
                {
                    txt = "Stichtag: " + dtpVon.Value.ToShortDateString();
                }
                exPort.ListHeaderText.Add(txt);
                exPort.Telerik_RunExportToExcelML(ref this.dgv, strFileName, ref openExportFile, true, "Rechnungsbuch");
                //exPort.Telerik_RunExportToExcelML(this.dgv, strFileName, ref openExportFile, true, "Rechnungsbuch");

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
                        error.Aktion = "Rechnungsbuch - Excelexport öffnen";
                        error.exceptText = ex.ToString();
                    }
                }
            }
            else
            {
                ////          bool bUseBKZ = false;
                ////          string strSql = "Select  " +
                ////                   "LVS_ID as LVS,exMaterialNummer as MATNR,Produktionsnummer as PRODNR,a.Dicke as DICKE,Cast(a.Breite as integer) as BREITE,CAST(a.Laenge as integer) as LAENGE,CAST(a.Brutto as integer)  as BRUTTO, " +
                ////                   "Cast(b.[Date] as Date) as EDATUM, exInfo as BEMERK, " +
                ////                   "Cast(a.Netto as integer) as NETTO, " +
                ////              //"FreigabeAbruf as Freigabe," +
                ////                   "(Select ViewID from ADR where ID =b.Auftraggeber) as KUNDE ";
                ////          strSql += " From Artikel a " +
                ////                        "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                ////                        "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                ////                        "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                ////                        "WHERE " +
                ////                        "( " +
                ////                              "b.Auftraggeber=" + this.ADR.ID + " ";
                ////          if (bUseBKZ)
                ////          {
                ////              strSql += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                ////          }
                ////          else
                ////          {
                ////              strSql += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                ////          }
                ////          //"AND b.Mandant=" + MandantenID + " " +
                ////          strSql += " AND b.DirectDelivery=0  AND b.AbBereich=" + this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID + " " +
                ////                    "AND b.Date <'" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' ";
                ////          //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
                ////          strSql = strSql +
                ////          ") " +
                ////          "OR " +
                ////          "(" +
                ////                "b.Auftraggeber=" + this.ADR.ID + " ";
                ////          if (bUseBKZ)
                ////          {
                ////              strSql += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                ////          }
                ////          else
                ////          {
                ////              strSql += " AND a.CheckArt=1 AND b.[Check]=1";
                ////          }
                ////          //"AND b.Mandant=" + MandantenID + " " +
                ////          strSql += " AND b.DirectDelivery=0 AND b.AbBereich=" + this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID + " " +
                ////              //"AND (c.Datum between '" + BestandVon.Date.ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') " +
                ////              //"AND (c.Datum between '" + BestandVon.Date.AddDays(1).ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') " +         29.10.2014
                ////" AND c.Datum>='" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' " +
                ////              //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
                ////"AND b.Date <'" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' ";

                ////          strSql = strSql + ")";
                ////          DataTable dtGewBestand = new DataTable("Bestand");
                ////          dtGewBestand = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.GL_User.User_ID, "Bestand");
                ////          LVS.clsExcel Excel = new clsExcel();
                ////          string FileName = "BestandKW" + (Functions.GetCalendarWeek(DateTime.Now)) + "_" + ADR.ViewID;
                ////          string FilePath = Excel.ExportDataTableToWorksheet(dtGewBestand, AttachmentPath + "\\" + FileName);
                ////          openExportFile = true;
            }

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(FileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Rechnungsbuch - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }

        }
        ///<summary>ctrRGList / tsbtnDeleteRGGS_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDeleteRGGS_Click(object sender, EventArgs e)
        {
            if (this.FaktLager.Rechnung.ID > 0)
            {
                //nur manuelle Rechnungen können gelöscht werden
                if (this.FaktLager.Rechnung.RGArt.Contains(clsRechnung.const_RechnungsArt_Manuell))
                {
                    if (clsMessages.Fakturierung_Delete())
                    {
                        //Datensatz soll gelöscht werden
                        FaktLager.Rechnung.sys = this._ctrMenu._frmMain.system;
                        this.FaktLager.Rechnung.Delete(false);
                        InitDGV(false);
                    }
                }
                else
                {
                    clsMessages.Fakturierung_DeniedLöschenRechnung();
                }
            }
            else
            {
                //keine REchnung ausgewählt
                clsMessages.Allgemein_KeinDatensatzAusgewaehlt();
            }
        }
        ///<summary>ctrRGList / aktuellerArbeitsbereichToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void aktuellerArbeitsbereichToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitDGV(false);
        }
        ///<summary>ctrRGList / überAlleArbeitsbereicheToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void überAlleArbeitsbereicheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitDGV(true);
        }
        ///<summary>ctrRGList / tsbtnMailRGRGAnhang_Click</summary>
        ///<remarks></remarks>
        private void tsbtnMailRGRGAnhang_Click(object sender, EventArgs e)
        {
            if (!this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                if (this.FaktLager.Rechnung.ID > 0)
                {
                    this._ctrMenu._frmMain.system.ReportDocSetting = new clsReportDocSetting();
                    this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.FaktLager.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
                    this.AttachmentList.Clear();
                    if (this._ctrMenu._frmMain.system.ReportDocSetting.ExistReportSetting(enumIniDocKey.LagerrechnungMail))
                    {
                        //PDF erstellen
                        //AttachmentList = this.FaktLager.CreateRGAndRGAnhangToPDF(this._ctrMenu._frmMain.system);
                        //if (this.AttachmentList.Count > 0)
                        //{
                        //    this._ctrMenu.OpenCtrMailCockpitInFrm(this);
                        //}
                        int iCol0Width = 40;
                        int iCol1Width = 120;
                        try
                        {
                            LogMessages = new List<string>();
                            LogMessages.Add("-----------------------------------" + Environment.NewLine);
                            LogMessages.Add("Start ctrRGList.cs");
                            LogMessages.Add("Z 1365 - this._ctrMenu.OpenCtrMailCockpitInFrm(this)");
                            LogMessages.Add("       -> Ziel  this.FaktLager.CreateRGAndRGAnhangToPDF(this._ctrMenu._frmMain.system)");

                            //PDF erstellen
                            AttachmentList = this.FaktLager.CreateRGAndRGAnhangToPDF(this._ctrMenu._frmMain.system);
                            LogMessages.AddRange(FaktLager.LogMessages);

                            if (_ctrMenu._frmMain.systemSped.System_VE_eInvoiceLogActivatet)
                            {
                                LogMessages.Add("--- LOG" + Environment.NewLine);
                                foreach (var s in FaktLager.LogMessages)
                                {
                                    LogMessages.Add(string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "-->".PadRight(iCol0Width), s));
                                }
                                LogMessages.Add("-----------------------------------" + Environment.NewLine);
                                LogMessages.Add("ENDE ctrRGList.cs - ");
                                LogMessages.Add("Z 1374 - this.FaktLager.CreateRGAndRGAnhangToPDF(this._ctrMenu._frmMain.system)");

                                ///--- Test INfo ausgabe
                                clsMail ErrorMail = new clsMail();
                                ErrorMail.InitClass(new Globals._GL_USER(), null);
                                ErrorMail.Subject = "Info Log zur Error Mail E-Rechnung";

                                string strMes = "Logdaten PDF E-Rechnung Erstellung:" + Environment.NewLine;
                                strMes += "--------------------------------------" + Environment.NewLine;
                                strMes += Environment.NewLine;
                                foreach (string logLine in LogMessages)
                                {
                                    strMes += logLine + Environment.NewLine;
                                }
                                strMes += Environment.NewLine;
                                strMes += "AttachmentList";
                                if ((this.AttachmentList != null) && (this.AttachmentList.Count > 0))
                                {
                                    int iCount = 0;
                                    foreach (string attachment in this.AttachmentList)
                                    {
                                        iCount++;
                                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "Attachment " + iCount.ToString().PadRight(iCol0Width), attachment);
                                    }
                                }
                                else
                                {
                                    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "AttachmentList".PadRight(iCol0Width), "Null or empty");
                                }
                                ErrorMail.Message = strMes;
                                ErrorMail.SendError();
                            }
                            if (this.AttachmentList.Count > 0)
                            {
                                this._ctrMenu.OpenCtrMailCockpitInFrm(this);
                            }
                        }
                        catch (Exception ex)
                        {
                            clsMail ErrorMail = new clsMail();
                            ErrorMail.InitClass(new Globals._GL_USER(), null);
                            ErrorMail.Subject = "tsbtnMailRGRGAnhang_Click | this.FaktLager.CreateRGAndRGAnhangToPDF(this._ctrMenu._frmMain.system) - Error Mail E-Rechnung";

                            string strMes = "Exception bei Aufruf this.FaktLager.CreateRGAndRGAnhangToPDF(this._ctrMenu._frmMain.system) [tsbtnMailRGRGAnhang_Click > Zeile 1360]" + Environment.NewLine;

                            //strMes += Environment.NewLine + Environment.NewLine;
                            //strMes += "-----------------------------------" + Environment.NewLine;
                            //strMes += "ZUGFeRDInvoice";
                            //strMes += "Paremter:";
                            //strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myUriReportSource:".PadRight(iCol0Width), myUriReportSource.ToString());
                            //strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myPdfFilePath:".PadRight(iCol0Width), myPdfFilePath);
                            //strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myEditPdfFilePath:".PadRight(iCol0Width), myEditPdfFilePath.ToString());
                            //strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myInvoiceId:".PadRight(iCol0Width), myInvoiceId.ToString());

                            //strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "ePdfFilePath:".PadRight(iCol0Width), ePdfFilePath);
                            //if ((myInvoiceViewData is InvoiceViewData) && (myInvoiceViewData.Invoice is Invoices))
                            //{
                            //    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "InvoiceId:".PadRight(iCol0Width), myInvoiceViewData.Invoice.Id);
                            //    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "Invoice Nr:".PadRight(iCol0Width), myInvoiceViewData.Invoice.InvoiceNo);
                            //    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "Receiver:".PadRight(iCol0Width), myInvoiceViewData.Invoice.AdrReceiver.AddressStringShort);
                            //}
                            //else
                            //{
                            //    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myInvoiceViewData".PadRight(iCol0Width), "Null");
                            //}
                            //strMes += "-----------------------------------" + Environment.NewLine;
                            //strMes += "ZUGFeRDInvoice";
                            //strMes += "Paremter:";
                            //strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myPdfFilePath:".PadRight(iCol0Width), myPdfFilePath);
                            //if ((InvoiceViewData is InvoiceViewData) && (InvoiceViewData.Invoice is Invoices))
                            //{
                            //    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "InvoiceId:".PadRight(iCol0Width), InvoiceViewData.Invoice.Id);
                            //    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "Invoice Nr:".PadRight(iCol0Width), InvoiceViewData.Invoice.InvoiceNo);
                            //    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "Receiver:".PadRight(iCol0Width), InvoiceViewData.Invoice.AdrReceiver.AddressStringShort);
                            //}
                            //else
                            //{
                            //    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "InvoiceViewData".PadRight(iCol0Width), "Null");
                            //}

                            strMes += "-----------------------------------" + Environment.NewLine;
                            strMes += "AttachmentList";
                            if ((this.AttachmentList != null) && (this.AttachmentList.Count > 0))
                            {
                                int iCount = 0;
                                foreach (string attachment in this.AttachmentList)
                                {
                                    iCount++;
                                    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "Attachment " + iCount.ToString().PadRight(iCol0Width), attachment);
                                }
                            }
                            else
                            {
                                strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "AttachmentList".PadRight(iCol0Width), "Null or empty");
                            }

                            strMes += ">>>" + Environment.NewLine;
                            strMes += ">>> ex.Message:" + Environment.NewLine;
                            strMes += ex.Message;
                            strMes += ">>> ex.InnerException:" + Environment.NewLine;
                            strMes += ex.InnerException.ToString();

                            ErrorMail.Message = strMes;
                            ErrorMail.SendError();
                        }




                    }
                    else
                    {
                        string str = "Für diese Dokumentenart sind keine Dokumente hinterlegt!";
                        clsMessages.Allgemein_InfoTextShow(str);
                    }
                }
            }
        }
        ///<summary>ctrRGList / miInvoicePreview_Click</summary>
        ///<remarks></remarks>
        private void miInvoicePreview_Click(object sender, EventArgs e)
        {
            PrintOrPreview(false);
        }
        ///<summary>ctrRGList / miInvoicePrint_Click</summary>
        ///<remarks></remarks>
        private void miInvoicePrint_Click(object sender, EventArgs e)
        {
            PrintOrPreview(true);
        }
        ///<summary>ctrRGList / miInvoicePrintAll_Click</summary>
        ///<remarks></remarks>
        private void miInvoicePrintAll_Click(object sender, EventArgs e)
        {
            //REchnung
            if (this.dgv.Rows.Count > 0)
            {
                foreach (DataRow dr in ((DataTable)(dgv.DataSource)).Rows)
                {
                    this.FaktLager.Rechnung.ID = (decimal)dr["ID"];
                    this.FaktLager.Rechnung.Fill();
                    if (this.FaktLager.Rechnung.Druck == false)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                        {
                            this.DokumentenArt = enumIniDocKey.Lagerrechnung.ToString();
                            this._ctrMenu.OpenFrmReporView(this, true);
                            Thread.Sleep(100);
                            List<decimal> AuftraggeberMat = new List<decimal>();
                            bool bUseMatDoc = false;
                            clsSystem sys = new clsSystem(Application.StartupPath);
                            sys.GetAuftraggeberAnhangMat(ref _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_System.sys_MandantenID);
                            if (_ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat != string.Empty)
                            {

                                string[] AuftraggeberSplit = _ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat.Split(',');
                                foreach (string auftraggeber in AuftraggeberSplit)
                                {
                                    if (this.FaktLager.Rechnung.Empfaenger == clsADR.GetIDByMatchcode(auftraggeber))
                                    {
                                        bUseMatDoc = true;
                                    }
                                }
                            }
                            if (true) { }
                            ctrPrintLager TmpPrint = new ctrPrintLager();
                            TmpPrint.Hide();
                            TmpPrint._ctrMenu = this._ctrMenu;


                            if (!bUseMatDoc)
                            {
                                this.DokumentenArt = enumIniDocKey.RGAnhang.ToString();
                            }
                            else
                            {
                                this.DokumentenArt = enumDokumentenArt.RGAnhangMat.ToString();
                            }
                            this._ctrMenu.OpenFrmReporView(this, true);
                        }
                        else
                        {
                            //Rechnung und Anhang
                            this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.FaktLager.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
                            this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.Lagerrechnung.ToString());
                            if (this.FaktLager.RepDocSettings is clsReportDocSetting)
                            {
                                this._ctrMenu.OpenFrmReporView(this, true);
                            }
                            this.FaktLager.RepDocSettings = null;
                            this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.RGAnhang.ToString());
                            if (this.FaktLager.RepDocSettings is clsReportDocSetting)
                            {
                                this._ctrMenu.OpenFrmReporView(this, true);
                            }
                        }
                        this.FaktLager.Rechnung.Druck = true;
                        this.FaktLager.Rechnung.Druckdatum = DateTime.Now;
                        this.FaktLager.Rechnung.UpdateRechnungPrint();
                    }
                }
            }
        }
        ///<summary>ctrRGList / druckRechnungsbuchToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void druckRechnungsbuchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                if (clsRechnung.CountRGForRGBook(GL_User) > 0)
                {
                    this.DokumentenArt = enumDokumentenArt.RGBuch.ToString();
                    this._ctrMenu.OpenFrmReporView(this, true);
                    clsRechnung.UpdatePrintRGBook(this.GL_User);
                }
                else
                {
                    if (clsMessages.Fakturierung_RGBookPrintAgain())
                    {
                        this.DokumentenArt = enumDokumentenArt.RGBuch.ToString();
                        this._ctrMenu.OpenFrmReporView(this, true);
                    }
                    else
                    {
                        clsMessages.Fakturierung_RGBookNoRG();
                    }
                }
            }
            else
            {
                this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.FaktLager.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
                this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.RGBuch.ToString());
                if (this.FaktLager.RepDocSettings is clsReportDocSetting)
                {
                    this._ctrMenu.OpenFrmReporView(this, true);
                }
            }
        }
    }
}
