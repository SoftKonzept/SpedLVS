using LVS;
using Sped4.Classes;
using Sped4.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrFreeForCall : UserControl
    {
        internal const string const_Headline = "Freigabe für Abruf";
        internal const string const_FileName = "_ArtikelfreigabeAbruf";
        internal const string viewName = const_Headline;
        internal ctrMenu _ctrMenu;
        internal ctrArtSearchFilter _ctrArtSearchFilter;
        public Globals._GL_USER GLUser;
        internal frmTmp _frmTmp;
        internal clsLager Lager;

        public decimal _ArtikelIDTakeOver = 0;
        internal string _ADRSearch;
        public Int32 SearchButton = 0;      //für Suche in FrmAdrSearch
        internal DataTable dtBestand;
        internal DataTable dtBestandToCall;
        internal DataTable dtBestandClone;
        internal List<string> ListAttachmentPath;
        internal string AttachmentPath;
        internal string FileName;
        DataColumn[] dts;

        public ctrFreeForCall()
        {
            InitializeComponent();
            //Deklarationen
            afColorLabel1.myText = const_Headline;
        }
        ///<summary>ctrFreeForCall / ctrExtraCharge_Load</summary>
        ///<remarks></remarks>
        private void ctrFreeForCall_Load(object sender, EventArgs e)
        {
            dtBestandToCall = new DataTable();
            dtBestand = new DataTable();
            AttachmentPath = this._ctrMenu._frmMain.GL_System.sys_WorkingPathExport;
            InitFilterSearchCtr();
            Functions.InitComboViews(this._ctrMenu._frmMain.GL_System, ref tscbBestand, "Bestand");
            Functions.InitComboViews(this._ctrMenu._frmMain.GL_System, ref tscbBestandForCall, "Bestand");
        }
        ///<summary>ctrFreeForCall / cbSchaden_CheckedChanged</summary>
        ///<remarks></remarks>
        private void InitFilterSearchCtr()
        {
            _ctrArtSearchFilter = new ctrArtSearchFilter();
            _ctrArtSearchFilter.InitCtr(this);
            _ctrArtSearchFilter.Dock = DockStyle.Fill;
            _ctrArtSearchFilter.Parent = this.splitPanel1;
            _ctrArtSearchFilter.Show();
            _ctrArtSearchFilter.BringToFront();
            this.splitPanel1.Width = _ctrArtSearchFilter.Width + 20;
        }
        ///<summary>ctrFreeForCall / InitGlobals</summary>
        ///<remarks></remarks>
        public void InitGlobals(ctrMenu myCtrMenu)
        {
            this.GLUser = myCtrMenu.GL_User;
            this._ctrMenu = myCtrMenu;
            Lager = new clsLager();
            Lager.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
        }
        ///<summary>ctrFreeForCall / InitDGVBestand</summary>
        ///<remarks></remarks>
        private void InitDGVBestand()
        {
            dtBestand = new DataTable();
            dtBestand = Lager.GetBestandToSelectForFreeForCall(this._ctrMenu._frmMain.system.Client.Modul.Lager_USEBKZ);
            dts = new DataColumn[dtBestand.Columns.Count];
            dtBestand.Columns.CopyTo(dts, 0);
            //this.dgvBestand.DataSource = dtBestand.DefaultView;
            setViewBestand();

            dtBestandToCall = new DataTable();
            dtBestandToCall = dtBestand.Clone();
            dtBestandToCall.TableName = "FreeToCall";
            if (this._ctrArtSearchFilter != null)
            {
                this._ctrArtSearchFilter.ClearFilterInput();
                this._ctrArtSearchFilter.SetFilterSearchElementAllEnabled(false);
                this._ctrArtSearchFilter.SetFilterElementEnabledByColumns(ref this.dgvBestand);
            }
        }
        ///<summary>ctrFreeForCall / ClearSumWeightAndQuantityFields</summary>
        ///<remarks></remarks>
        private void ClearSumWeightAndQuantityFields()
        {
            tbAnzahlArtikelSelektiert.Text = string.Empty;
            tbFreigabeVolumenSelektiert.Text = string.Empty;
        }
        ///<summary>ctrFreeForCall / InitDGVBestandFreigabe</summary>
        ///<remarks></remarks>
        private void InitDGVBestandFreigabe()
        {

            Functions.setView(ref dtBestandToCall, ref dgvBestandForCall, "Bestand", tscbBestandForCall.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System);
            this.dgvBestandForCall.DataSource = dtBestandToCall;
            FormatDGV(ref this.dgvBestandForCall);
            decimal decSumGewicht = 0;
            Int32 iArtikelAnzahl = 0;
            object objSumGewicht = 0;
            object objSumAnzahl = 0;
            if (dtBestandToCall.Rows.Count > 0)
            {
                objSumGewicht = dtBestandToCall.Compute("Sum(Brutto)", "");
                objSumAnzahl = dtBestandToCall.Compute("COUNT(ArtikelID)", "");
            }
            decimal.TryParse(objSumGewicht.ToString(), out decSumGewicht);
            tbFreigabeVolumenSelektiert.Text = Functions.FormatDecimal(decSumGewicht);
            Int32.TryParse(objSumAnzahl.ToString(), out iArtikelAnzahl);
            tbAnzahlArtikelSelektiert.Text = iArtikelAnzahl.ToString();
        }
        ///<summary>ctrFreeForCall / FormatDGV</summary>
        ///<remarks>Formatiert die beiden GridView gleich</remarks>
        private void FormatDGV(ref RadGridView myDGV, bool bAktiv = true)
        {
            if (bAktiv == true)
            {
                for (Int32 i = 0; i <= myDGV.Columns.Count - 1; i++)
                {
                    string colName = myDGV.Columns[i].Name.ToString();
                    switch (colName)
                    {
                        case "Dicke":
                        case "Breite":
                        case "Laenge":
                        case "Netto":
                        case "Brutto":
                        case "LZZ":
                        case "Select":
                        case "Produktionsnummer":
                        case "LVSNr":
                        case "Freigabe":
                        case "Lieferschein":
                        case "MaterialNr":
                            //case "exMatrik":
                            myDGV.Columns[i].IsVisible = true;
                            break;
                        default:
                            myDGV.Columns[i].IsVisible = false;
                            break;
                    }
                    this.dgvBestand.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
                }
            }

        }
        ///<summary>ctrFreeForCall / tsbCloseCtr_Click</summary>
        ///<remarks></remarks>
        private void tsbCloseCtr_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrFreeForCall();
        }
        ///<summary>ctrFreeForCall / btnSearchA_Click</summary>
        ///<remarks></remarks>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            _ADRSearch = "Auftraggeber";
            SearchButton = 1;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrFreeForCall / tbSearchA_TextChanged</summary>
        ///<remarks></remarks>
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = Functions.GetADRTableSearchResultTable(tbSearchA.Text, this.GLUser);
            if (dtTmp.Rows.Count > 0)
            {
                this.Lager.ADR.ID = clsADR.GetIDByMatchcode(tbSearchA.Text);
                tbADRAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
            }
            else
            {
                tbADRAuftraggeber.Text = string.Empty;
            }
        }
        ///<summary>ctrFreeForCall / SetADRToFrm</summary>
        ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
        ///<param name="ADR_ID">ADR_ID</param>
        public void SetADRToFrm(decimal myDecADR_ID)
        {
            string strE = string.Empty;
            string strMC = string.Empty;
            DataSet ds = clsADR.ReadADRbyID(myDecADR_ID);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strMC = ds.Tables[0].Rows[i]["ViewID"].ToString();
                strE = ds.Tables[0].Rows[i]["ViewID"].ToString() + " - ";
                strE = strE + ds.Tables[0].Rows[i]["KD_ID"].ToString() + " - ";
                strE = strE + ds.Tables[0].Rows[i]["Name1"].ToString() + " - ";
                strE = strE + ds.Tables[0].Rows[i]["PLZ"].ToString() + " - ";
                strE = strE + ds.Tables[0].Rows[i]["Ort"].ToString();

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
                        Lager.ADR.ID = myDecADR_ID;
                        tbSearchA.Text = strMC;
                        tbADRAuftraggeber.Text = strE;
                        break;
                }
            }
        }
        ///<summary>ctrFreeForCall / tsbtnBestandGet_Click</summary>
        ///<remarks>Der Bestand für die angegebene Adresse soll ermittelt werden</remarks>
        private void tsbtnBestandGet_Click(object sender, EventArgs e)
        {
            InitDGVBestand();
            InitDGVBestandFreigabe();
            SetDGVCellSelectOrUnselect(ref dtBestand, true, true);
        }
        ///<summary>ctrFreeForCall / tsbtnAllCheck_Click</summary>
        ///<remarks>Markiert alle Artikeldatensätze in der Table als markiert / unmarkiert</remarks> 
        private void SetArtikelCheckedOrUncheckValue(ref DataTable dt, bool bChecked)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i]["Select"] = bChecked;
            }
        }
        ///<summary>ctrFreeForCall / tbSearchA_TextChanged</summary>
        ///<remarks></remarks>
        private void tsbtnSelectBestandTillCallValue_Click(object sender, EventArgs e)
        {
            TransferSelectedArtikelToBestand(ref dtBestand, ref dtBestandToCall);
        }
        ///<summary>ctrFreeForCall / SetDGVCellSelectOrUnselect</summary>
        ///<remarks></remarks>
        private void SetDGVCellSelectOrUnselect(ref DataTable dt, bool bSelected, bool bCountFreeValue)
        {
            if (dt != null)
            {
                Int32 iToBreak = -1;
                decimal SumValueForFree = 0;
                //Spalte Select soll komplett selektiert / nicht selektiert werden
                if (bCountFreeValue)
                {
                    for (Int32 i = 0; i <= this.dgvBestand.Rows.Count - 1; i++)
                    {
                        //Check Menge Volumen zur Freigabe
                        decimal decTmp = 0;
                        decimal.TryParse(this.dgvBestand.Rows[i].Cells["Brutto"].Value.ToString(), out decTmp);
                        SumValueForFree = SumValueForFree + decTmp;

                        if (SumValueForFree <= nudCallValue.Value)
                        {
                            this.dgvBestand.Rows[i].Cells["Select"].Value = bSelected;
                            //dt.Rows[i]["Select"] = bSelected;
                        }
                        else
                        {
                            //iToBreak = j;
                            //dt.Rows[i]["Select"] = bSelected;
                            //dt.Rows[i]["Select"] = (!bSelected);
                            this.dgvBestand.Rows[i].Cells["Select"].Value = (!bSelected);
                        }
                    }
                }
                else
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        //foreach(DataColumn dc in dtRow)
                        dtRow["Select"] = bSelected;

                    }
                }
            }
        }
        ///<summary>ctrFreeForCall / TransferSelectedArtikelToBestandForFree</summary>
        ///<remarks></remarks> 
        private void TransferSelectedArtikelToBestand(ref DataTable dtSource, ref DataTable dtDest, decimal decVolumen = 0)
        {
            if (dtSource.Rows.Count > 0)
            {
                //Statusbar
                this._ctrMenu._frmMain.InitStatusBar(dtSource.Rows.Count);
                Int32 i = 0;
                while (i <= dtSource.Rows.Count - 1)
                {
                    bool bSelect = (bool)dtSource.Rows[i]["Select"];
                    if (bSelect)
                    {
                        DataRow row;
                        row = dtSource.Rows[i];
                        row["Select"] = false;
                        dtDest.ImportRow(row);
                        //die Row muss nun aus dem TableSource entfernt werden
                        dtSource.Rows.RemoveAt(i);
                        i = -1;
                        this._ctrMenu._frmMain.InitStatusBar(dtSource.Rows.Count);
                    }
                    i++;
                    this._ctrMenu._frmMain.StatusBarWork(true, "Datenimport läuft...");
                }
            }
            this._ctrMenu._frmMain.InitStatusBar(0);
            InitDGVBestandFreigabe();
        }
        ///<summary>ctrFreeForCall / dgvBestand_CellClick</summary>
        ///<remarks></remarks> 
        private void dgvBestand_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.dgvBestand.Rows[e.RowIndex] != null)
                {
                    string strTmpArtikelID = this.dgvBestand.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString();
                    decimal decTmp = 0;
                    decimal.TryParse(strTmpArtikelID, out decTmp);
                    if (decTmp > 0)
                    {
                        Int32 i = 0;
                        while (i <= dtBestand.Rows.Count - 1)
                        {
                            decimal decTmp2 = 0;

                            string strTmpArtikelID2 = dtBestand.Rows[i]["ArtikelID"].ToString();
                            Decimal.TryParse(strTmpArtikelID2, out decTmp2);
                            if (decTmp2 > 0)
                            {
                                if (decTmp == decTmp2)
                                {
                                    bool bSelect = (bool)dtBestand.Rows[i]["Select"];

                                    this.dtBestand.Rows[i]["Select"] = !bSelect;
                                    //setViewBestand();
                                    break;
                                }
                                i++;
                            }
                        }
                    }
                }
            }
        }
        ///<summary>ctrFreeForCall / dgvBestandForCall_CellClick25002000</summary>
        ///<remarks></remarks> 
        private void dgvBestandForCall_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.dgvBestandForCall.Rows[e.RowIndex] != null)
                {
                    string strTmpArtikelID = this.dgvBestandForCall.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString();
                    decimal decTmp = 0;
                    decimal.TryParse(strTmpArtikelID, out decTmp);
                    if (decTmp > 0)
                    {
                        Int32 i = 0;
                        while (i <= dtBestandToCall.Rows.Count - 1)
                        {
                            decimal decTmp2 = 0;

                            string strTmpArtikelID2 = dtBestandToCall.Rows[i]["ArtikelID"].ToString();
                            Decimal.TryParse(strTmpArtikelID2, out decTmp2);
                            if (decTmp2 > 0)
                            {
                                if (decTmp == decTmp2)
                                {
                                    bool bSelect = (bool)dtBestandToCall.Rows[i]["Select"];
                                    this.dtBestandToCall.Rows[i]["Select"] = !bSelect;
                                    break;
                                }
                                i++;
                            }
                        }
                    }
                }
            }
        }
        ///<summary>ctrFreeForCall / tsbtnBestandSelectAll_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnBestandSelectAll_Click(object sender, EventArgs e)
        {
            //SetDGVCellSelectOrUnselect(ref dgvBestand, true, false);
            SetDGVCellSelectOrUnselect(ref dtBestand, true, false);
        }
        ///<summary>ctrFreeForCall / tsbtnBestandUnSelectAll_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnBestandUnSelectAll_Click(object sender, EventArgs e)
        {
            //SetDGVCellSelectOrUnselect(ref dgvBestand, false, false);
            SetDGVCellSelectOrUnselect(ref dtBestand, false, false);
        }
        ///<summary>ctrFreeForCall / tsbtnFreeForCallUnSelectAll_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnFreeForCallUnSelectAll_Click(object sender, EventArgs e)
        {
            //SetDGVCellSelectOrUnselect(ref dgvBestandForCall, true, false);
            SetDGVCellSelectOrUnselect(ref dtBestandToCall, false, false);
        }
        ///<summary>ctrFreeForCall / dgvBestandForCall_CeltsbtnFreeForCallSelectAll_ClicklClick</summary>
        ///<remarks></remarks> 
        private void tsbtnFreeForCallSelectAll_Click(object sender, EventArgs e)
        {
            //SetDGVCellSelectOrUnselect(ref dgvBestandForCall, true, false);
            SetDGVCellSelectOrUnselect(ref dtBestandToCall, true, false);
        }
        ///<summary>ctrFreeForCall / tsbtnTransferInFreeToCallBestand_Clickasass</summary>
        ///<remarks></remarks>
        private void tsbtnTransferInFreeToCallBestand_Click(object sender, EventArgs e)
        {
            TransferSelectedArtikelToBestand(ref dtBestand, ref dtBestandToCall);
        }
        ///<summary>ctrFreeForCall / tsbtnTransferOutOfFreeForCallBestand_Click</summary>
        ///<remarks></remarks>
        private void tsbtnTransferOutOfFreeForCallBestand_Click(object sender, EventArgs e)
        {
            TransferSelectedArtikelToBestand(ref dtBestandToCall, ref dtBestand);
        }
        ///<summary>ctrFreeForCall / tsbtnTransferInFreeToCallBestand_Clickasass</summary>
        ///<remarks></remarks>
        private void tsbtnFreeForCallExport_Click(object sender, EventArgs e)
        {
            FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName + ".xls";
            saveFileDialog.InitialDirectory = AttachmentPath;
            saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            FileName = this.saveFileDialog.FileName;
            bool openExportFile = false;

            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvBestandForCall, FileName, ref openExportFile, this.GLUser, true);

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(FileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GLUser;
                    error.Code = clsError.code1_501;
                    error.Aktion = "ArtikelfreigabeAbruf - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrFreeForCall / tsbtnFreeForCallMail_Click</summary>
        ///<remarks></remarks>
        private void tsbtnFreeForCallMail_Click(object sender, EventArgs e)
        {
            if (this.dgvBestandForCall.Rows.Count > 0)
            {
                FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName + ".xls";
                saveFileDialog.InitialDirectory = AttachmentPath;
                saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
                saveFileDialog.ShowDialog();
                FileName = saveFileDialog.FileName;

                if (FileName.Equals(String.Empty))
                {
                    return;
                }
                bool openExportFile = false;
                Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvBestandForCall, FileName, ref openExportFile, this.GLUser, false);
                if (!openExportFile.Equals(string.Empty))
                {
                    ListAttachmentPath = new List<string>();
                    ListAttachmentPath.Add(FileName);
                    this._ctrMenu.OpenCtrMailCockpitInFrm(this);
                }
            }
        }
        ///<summary>ctrFreeForCall / nudCallValue_ValueChanged</summary>
        ///<remarks></remarks>
        private void nudCallValue_ValueChanged(object sender, EventArgs e)
        {
            SetDGVCellSelectOrUnselect(ref dtBestand, true, true);
        }
        ///<summary>ctrFreeForCall / tsbtnSaveFreeForCall_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSaveFreeForCall_Click(object sender, EventArgs e)
        {
            if (this.dgvBestandForCall.Rows.Count > 0)
            {
                List<string> listArtID = new List<string>();
                for (Int32 i = 0; i <= dtBestandToCall.Rows.Count - 1; i++)
                {
                    listArtID.Add(dtBestandToCall.Rows[i]["ArtikelID"].ToString());
                }
                this.Lager.Artikel.UpdateFreeForCall(listArtID);
                DataTable dtTmp = dtBestandToCall.Copy();
                dtTmp.Columns["Produktionsnummer"].ColumnName = "Prod.-Nr.";
                clsPrint print = new clsPrint(dtTmp, _ctrMenu._frmMain.GL_System, viewName, 0);

                string Titel = tbADRAuftraggeber.Text;

                print.printTitel = clsADR.GetADRStringKDNrName(clsADR.GetIDByMatchcode(tbSearchA.Text)).ToString();
                print.Print(false, true, _ctrMenu);

                ClearSumWeightAndQuantityFields();
                tbSearchA.Text = string.Empty;
                tbADRAuftraggeber.Text = string.Empty;
                dtBestand.Rows.Clear();
                dtBestandToCall.Rows.Clear();
                this.Lager.ADR.ID = 0;
                nudCallValue.Value = 0;
            }
        }
        ///<summary>ctrFreeForCall / tsbtnSearchShow_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSearchShow_Click(object sender, EventArgs e)
        {
            this.splitPanel1.Collapsed = (!this.splitPanel1.Collapsed);

        }

        private void tscbBestand_SelectedIndexChanged(object sender, EventArgs e)
        {
            setViewBestand();
        }

        private void tscbBestandForCall_SelectedIndexChanged(object sender, EventArgs e)
        {
            setViewBestandForCall();
        }

        private void setViewBestand()
        {
            string item = tscbBestand.SelectedItem.ToString();
            Functions.setView(ref dtBestand, ref dgvBestand, "Bestand", tscbBestand.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, true, dts);
            if (dgvBestand.Columns["Select"] != null && dgvBestand.Columns["Select"].Index != 0)
            {
                //dgvBestand.Columns["Select"].Is;
                dtBestand.Columns["Select"].SetOrdinal(0);
                dgvBestand.DataSource = null;
                dgvBestand.DataSource = dtBestand;
                dgvBestand.Columns["Select"].IsVisible = true;
            }
            bool bDefault = false;
            if (item == "Default")
            {
                bDefault = true;
            }
            FormatDGV(ref this.dgvBestand, bDefault);
            this._ctrArtSearchFilter.SetFilterforDGV(ref this.dgvBestand, false);
        }

        private void setViewBestandForCall()
        {
            string item = tscbBestandForCall.SelectedItem.ToString();
            Functions.setView(ref dtBestandToCall, ref dgvBestandForCall, "Bestand", tscbBestandForCall.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, true, dts);
            if (dgvBestandForCall.Columns["Select"] != null && dgvBestandForCall.Columns["Select"].Index != 0)
            {
                //dgvBestand.Columns["Select"].Is;
                dtBestandToCall.Columns["Select"].SetOrdinal(0);
                dgvBestandForCall.DataSource = null;
                dgvBestandForCall.DataSource = dtBestandToCall;
                dgvBestandForCall.Columns["Select"].IsVisible = true;
            }
            bool bDefault = false;
            if (item == "Default")
            {
                bDefault = true;
            }
            FormatDGV(ref this.dgvBestandForCall, bDefault);
        }

        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            if (this.dgvBestandForCall != null && this.dgvBestandForCall.RowCount > 0)
            {
                DataTable dtTmp = dtBestandToCall.Copy();
                dtTmp.Columns["Produktionsnummer"].ColumnName = "Prod.-Nr.";
                clsPrint print = new clsPrint(dtTmp, _ctrMenu._frmMain.GL_System, viewName, 0);

                string Titel = tbADRAuftraggeber.Text;
                //print.printTitel =  " | " + Titel;
                print.printTitel = clsADR.GetADRStringKDNrName(clsADR.GetIDByMatchcode(tbSearchA.Text)).ToString();
                print.Print(false, true, _ctrMenu);
            }

        }

        private void dgvBestandForCall_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                if (cell.ColumnInfo.Name == "Status")
                {
                    e.ToolTipText = string.Empty;
                    foreach (KeyValuePair<string, string> kvp in DictSettings.DicStatus())
                    {
                        e.ToolTipText += kvp.Key + " : " + kvp.Value + " \n";
                    }
                }
                else
                {
                    e.ToolTipText = cell.Value.ToString();
                }
            }
        }

        private void dgvBestand_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                if (cell.ColumnInfo.Name == "Status")
                {
                    e.ToolTipText = string.Empty;
                    foreach (KeyValuePair<string, string> kvp in DictSettings.DicStatus())
                    {
                        e.ToolTipText += kvp.Key + " : " + kvp.Value + " \n";
                    }
                }
                else
                {
                    e.ToolTipText = cell.Value.ToString();
                }
            }
        }
        /// <summary>
        /// tbADRAuftraggeber_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbADRAuftraggeber_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
