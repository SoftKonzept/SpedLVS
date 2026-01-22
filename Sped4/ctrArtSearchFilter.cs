using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrArtSearchFilter : UserControl
    {
        Globals._GL_USER GLUser;
        internal clsLager Lager;
        internal ctrSearch ctrSearch;
        internal ctrUmbuchung ctrUB;
        internal ctrBestand _ctrBestand;
        internal ctrJournal _ctrJournal;
        internal ctrFreeForCall _ctrFreeForCall;
        internal ctrAuslagerung _ctrAuslagerung;
        internal ctrArbeitsliste _ctrArbeitsliste;
        internal ctrWorklist _ctrWorklist;
        internal ctrWaggonbuch _ctrWaggonbuch;
        //internal ctrSendToSPL _ctrSendToSPL;

        internal Int32 iSearchButton = 0;
        private Dictionary<int, string> DictSearchRange = new Dictionary<int, string>()
        {
            { -1, "alle Artikel" },
            { 0, "eingelagerte Artikel" },
            { 1, "ausgelagerte Artikel" }
        };
        internal string lGueterartenText = "Güterart ID";


        /************************************************************
         *              
         * *********************************************************/
        ///<summary>ctrArtSearchFilter / ctrArtSearchFilter</summary>
        ///<remarks></remarks>
        public ctrArtSearchFilter()
        {
            InitializeComponent();
        }
        ///<summary>ctrArtSearchFilter / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr(object myObj)
        {
            tscbSearchSpace.ComboBox.DataSource = DictSearchRange.ToArray();
            tscbSearchSpace.ComboBox.DisplayMember = "Value";
            tscbSearchSpace.ComboBox.ValueMember = "Key";

            ClearFilterInput();

            if (myObj.GetType() == typeof(ctrSearch))
            {
                this.GLUser = ((ctrSearch)myObj).GL_User;
                this.ctrSearch = (ctrSearch)myObj;
                Lager = new clsLager();
                Lager.InitClass(this.GLUser, this.ctrSearch._ctrMenu._frmMain.GL_System, this.ctrSearch._ctrMenu._frmMain.system);
                SetFilterSearchElementAllEnabled(true);
            }
            if (myObj.GetType() == typeof(ctrArbeitsliste))
            {
                this.GLUser = ((ctrArbeitsliste)myObj).GL_User;
                this._ctrArbeitsliste = (ctrArbeitsliste)myObj;
                Lager = new clsLager();
                Lager.InitClass(this.GLUser, this._ctrArbeitsliste._ctrMenu._frmMain.GL_System, this._ctrArbeitsliste._ctrMenu._frmMain.system);
                SetFilterSearchElementAllEnabled(true);
            }
            if (myObj.GetType() == typeof(ctrUmbuchung))
            {
                this.GLUser = ((ctrUmbuchung)myObj).GL_User;
                this.ctrUB = (ctrUmbuchung)myObj;
                Lager = new clsLager();
                Lager.InitClass(this.GLUser, this.ctrUB._ctrMenu._frmMain.GL_System, this.ctrUB._ctrMenu._frmMain.system);
                SetFilterSearchElementAllEnabled(false);
                SetFilterElementEnabledByColumns(ref this.ctrUB.dgv);
            }
            if (myObj.GetType() == typeof(ctrBestand))
            {
                this.GLUser = ((ctrBestand)myObj).GL_User;
                this._ctrBestand = (ctrBestand)myObj;
                Lager = new clsLager();
                Lager.InitClass(this.GLUser, this._ctrBestand._ctrMenu._frmMain.GL_System, this._ctrBestand._ctrMenu._frmMain.system);
                SetFilterSearchElementAllEnabled(false);
                SetFilterElementEnabledByColumns(ref this._ctrBestand.dgv);
            }
            if (myObj.GetType() == typeof(ctrJournal))
            {
                this.GLUser = ((ctrJournal)myObj).GL_User;
                this._ctrJournal = (ctrJournal)myObj;
                Lager = new clsLager();
                Lager.InitClass(this.GLUser, this._ctrJournal._ctrMenu._frmMain.GL_System, this._ctrJournal._ctrMenu._frmMain.system);
                SetFilterSearchElementAllEnabled(true);
                //SetFilterElementEnabledByColumns(ref this._ctrJournal.rDgv);
            }
            if (myObj.GetType() == typeof(ctrFreeForCall))
            {
                this.GLUser = ((ctrFreeForCall)myObj).GLUser;
                this._ctrFreeForCall = (ctrFreeForCall)myObj;
                Lager = new clsLager();
                Lager.InitClass(this.GLUser, this._ctrFreeForCall._ctrMenu._frmMain.GL_System, this._ctrFreeForCall._ctrMenu._frmMain.system);
                SetFilterSearchElementAllEnabled(false);
                SetFilterElementEnabledByColumns(ref this._ctrFreeForCall.dgvBestand);
            }
            //if (myObj.GetType() == typeof(ctrSendToSPL))
            //{
            //   this.GLUser = ((ctrSendToSPL)myObj).GLUser;
            //   this._ctrSendToSPL = (ctrSendToSPL)myObj;
            //   Lager = new clsLager();
            //   Lager.InitClass(this.GLUser, this._ctrSendToSPL._ctrMenu._frmMain.GL_System);
            //   SetFilterSearchElementAllEnabled(false);
            //   SetFilterElementEnabledByColumns(ref this._ctrSendToSPL.dgvBestand);
            //}
            if (myObj.GetType() == typeof(ctrAuslagerung))
            {
                this.GLUser = ((ctrAuslagerung)myObj).GL_User;
                this._ctrAuslagerung = (ctrAuslagerung)myObj;
                Lager = new clsLager();
                Lager.InitClass(this.GLUser, this._ctrAuslagerung._ctrMenu._frmMain.GL_System, this._ctrAuslagerung._ctrMenu._frmMain.system);
                SetFilterSearchElementAllEnabled(false);
                SetFilterElementEnabledByColumns(ref this._ctrAuslagerung.dgv);
            }
            if (myObj.GetType() == typeof(ctrWorklist))
            {
                this.GLUser = ((ctrWorklist)myObj).GL_User;
                this._ctrWorklist = (ctrWorklist)myObj;
                Lager = new clsLager();
                Lager.InitClass(this.GLUser, this._ctrWorklist._ctrMenu._frmMain.GL_System, this._ctrWorklist._ctrMenu._frmMain.system);
                SetFilterSearchElementAllEnabled(true);
            }
            if (myObj.GetType() == typeof(ctrWaggonbuch))
            {
                this.GLUser = ((ctrWaggonbuch)myObj).GL_User;
                this._ctrWaggonbuch = (ctrWaggonbuch)myObj;
                Lager = new clsLager();
                Lager.InitClass(this.GLUser, this._ctrWaggonbuch._ctrMenu._frmMain.GL_System, this._ctrWaggonbuch._ctrMenu._frmMain.system);
                SetFilterSearchElementAllEnabled(true);
            }
            lGArtID.Text = lGueterartenText + "[" + Lager.Artikel.GArt.ID.ToString() + "]";
        }
        ///<summary>ctrArtSearchFilter / tsbtnSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            AssignSearchValue();
            //in dem entsprechenden Ctr Suchen
            if (this.ctrSearch != null)
            {
                this.ctrSearch.tstbSearchArtikel.Text = string.Empty;
                this.ctrSearch.runWorker(true);
            }
            if (this.ctrUB != null)
            {
                SetFilterforDGV(ref this.ctrUB.dgv, false);
            }
            if (this._ctrBestand != null)
            {
                SetFilterforDGV(ref this._ctrBestand.dgv, false);
            }
            if (this._ctrJournal != null)
            {
                SetFilterforDGV(ref this._ctrJournal.rDgv, false);
            }
            if (this._ctrFreeForCall != null)
            {
                SetFilterforDGV(ref this._ctrFreeForCall.dgvBestand, false);
            }
            if (this._ctrAuslagerung != null)
            {
                SetFilterforDGV(ref this._ctrAuslagerung.dgv, false);
            }
            if (this._ctrWorklist != null)
            {
                SetFilterforDGV(ref this._ctrWorklist.dgvArtikel, false);
                this._ctrWorklist.runWorker(true);
                this._ctrWorklist.strAuftraggeber = this.tbSearchA.Text;
            }

        }
        ///<summary>ctrArtSearchFilter / AssignSearchValue</summary>
        ///<remarks>Filterwerte werden gesetzt</remarks>
        private void AssignSearchValue()
        {
            this.Lager.FilterAuftraggeber = this.Lager.ADR.ID;
            this.Lager.FilterArtikelID = nudArtikelID.Value;
            this.Lager.FilterLVSNr = nudLvsNr.Value;
            this.Lager.FilterLEingangID = nudEingangID.Value;
            this.Lager.FilterLAusgangID = nudAusgangID.Value;
            this.Lager.FilterArtikelFreigabe = comboArtFreigabe.SelectedIndex;
            this.Lager.FilterGutID = this.Lager.Artikel.GArt.ID;
            this.Lager.FilterWerksnummer = tbWerksnummer.Text.Trim();
            this.Lager.FilterProduktionsnummer = tbProduktionsNr.Text.Trim();
            this.Lager.FilterCharge = tbCharge.Text.Trim();
            this.Lager.FilterBestellnummer = tbBestellnummer.Text.Trim();
            this.Lager.FilterDicke = nudDicke.Value;
            this.Lager.FilterBreite = nudBreite.Value;
            this.Lager.FilterLaenge = nudLaenge.Value;
            this.Lager.FilterHoehe = nudHoehe.Value;
            this.Lager.FilterBrutto = nudBrutto.Value;
            this.Lager.FilterSearchSpace = (Int32)tscbSearchSpace.ComboBox.SelectedValue;
        }
        ///<summary>ctrArtSearchFilter / tsbtnClear_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            ClearFilterInput();
            if (this.ctrSearch != null)
            {
                this.ctrSearch.InitDGV(false);
            }
            if (this.ctrUB != null)
            {
                SetFilterforDGV(ref this.ctrUB.dgv, true);
            }
            if (this._ctrBestand != null)
            {
                SetFilterforDGV(ref this._ctrBestand.dgv, true);
            }
            if (this._ctrJournal != null)
            {
                SetFilterforDGV(ref this._ctrJournal.rDgv, true);
            }
            if (this._ctrFreeForCall != null)
            {
                SetFilterforDGV(ref this._ctrFreeForCall.dgvBestand, true);
            }
            if (this._ctrAuslagerung != null)
            {
                SetFilterforDGV(ref this._ctrAuslagerung.dgv, true);
            }
            if (this._ctrArbeitsliste != null)
            {
                this._ctrArbeitsliste.clearFilter();
            }
        }
        ///<summary>ctrArtSearchFilter / ClearFilterInput</summary>
        ///<remarks></remarks>
        public void ClearFilterInput()
        {
            if (tscbSearchSpace.Items.Count > 0)
            {
                tscbSearchSpace.ComboBox.SelectedIndex = 0;
            }
            iSearchButton = 0;
            tbAuftraggeber.Text = string.Empty;
            tbSearchA.Text = string.Empty;
            nudArtikelID.Value = clsLager.const_FilterArtikelID;
            nudLvsNr.Value = clsLager.const_FilterLVSNr;
            nudEingangID.Value = clsLager.const_FilterLEingangID;
            nudAusgangID.Value = clsLager.const_FilterLAusgangID;
            tbGArtSearch.Text = string.Empty;
            tbGArt.Text = string.Empty;
            tbWerksnummer.Text = string.Empty;
            tbProduktionsNr.Text = string.Empty;
            tbCharge.Text = string.Empty;
            tbBestellnummer.Text = string.Empty;
            nudDicke.Value = clsLager.const_FilterDicke;
            nudBreite.Value = clsLager.const_FilterBreite;
            nudLaenge.Value = clsLager.const_FilterLaenge;
            nudHoehe.Value = clsLager.const_FilterHoehe;
            nudBrutto.Value = clsLager.const_FilterBrutto;
            comboArtFreigabe.SelectedIndex = clsLager.const_FilterArtikelFreigabe;
            tbTransportRef.Text = string.Empty;
            tbMaterialnummer.Text = string.Empty;
        }
        ///<summary>ctrArtSearchFilter / SetFilterSearchElementAllEnabled</summary>
        ///<remarks></remarks>
        public void SetFilterSearchElementAllEnabled(bool bEnabled)
        {
            //tscbSearchSpace.ComboBox.SelectedIndex = 0;
            //iSearchButton = 0;
            btnSearchA.Enabled = bEnabled;
            tbAuftraggeber.Enabled = bEnabled;
            tbSearchA.Enabled = bEnabled;
            nudArtikelID.Enabled = bEnabled;
            nudLvsNr.Enabled = bEnabled;
            nudEingangID.Enabled = bEnabled;
            nudAusgangID.Enabled = bEnabled;
            btnGArt.Enabled = bEnabled;
            tbGArtSearch.Enabled = bEnabled;
            tbGArt.Enabled = bEnabled;
            tbWerksnummer.Enabled = bEnabled;
            tbProduktionsNr.Enabled = bEnabled;
            tbCharge.Enabled = bEnabled;
            tbBestellnummer.Enabled = bEnabled;
            nudDicke.Enabled = bEnabled;
            nudBreite.Enabled = bEnabled;
            nudLaenge.Enabled = bEnabled;
            nudHoehe.Enabled = bEnabled;
            nudBrutto.Enabled = bEnabled;
            comboArtFreigabe.Enabled = bEnabled;
            tbTransportRef.Enabled = bEnabled;
            tbMaterialnummer.Enabled = bEnabled;

            //in den folgenden Ctr müssen hier die entsprechenen
            //Filterelemente komplett deaktiviert werden
            if (
                    (this.ctrUB != null) ||
                    (this._ctrBestand != null) ||
                    //(this._ctrJournal != null) ||
                    (this._ctrFreeForCall != null) ||
                    (this._ctrAuslagerung != null)
                )
            {
                tscbSearchSpace.ComboBox.Enabled = false;
                tscbSearchSpace.Enabled = false;

                btnSearchA.Enabled = false;
                tbAuftraggeber.Enabled = false;
                tbSearchA.Enabled = false;

                btnGArt.Enabled = false;
                tbGArtSearch.Enabled = false;
                tbGArt.Enabled = false;
            }
        }
        ///<summary>ctrArtSearchFilter / btnSearchA_Click</summary>
        ///<remarks></remarks>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            iSearchButton = 1;
            if (this.ctrSearch != null)
            {
                this.ctrSearch._ctrMenu.OpenADRSearch(this.ctrSearch);
            }
            if (this._ctrJournal != null)
            {
                this._ctrJournal.SearchButton = iSearchButton;
                this._ctrJournal._ctrMenu.OpenADRSearch(this._ctrJournal);
            }
        }
        ///<summary>ctrArtSearchFilter / btnGArt_Click</summary>
        ///<remarks></remarks>
        private void btnGArt_Click(object sender, EventArgs e)
        {
            if (this.ctrSearch != null)
            {
                this.ctrSearch._ctrMenu.OpenFrmGArtenList(this.ctrSearch);
            }
            if (this._ctrJournal != null)
            {
                this._ctrJournal._ctrMenu.OpenFrmGArtenList(this._ctrJournal);
            }
        }
        ///<summary>ctrArtSearchFilter/TakeOverGueterArt</summary>
        ///<remarks></remarks>
        public void TakeOverGueterArt(decimal TakeOver_ID)
        {
            Lager.Artikel.GArt = new clsGut();
            if (this.ctrSearch != null)
            {
                Lager.Artikel.GArt.InitClass(this.GLUser, this.ctrSearch._ctrMenu._frmMain.GL_System);
            }
            if (this._ctrJournal != null)
            {
                Lager.Artikel.GArt.InitClass(this.GLUser, this._ctrJournal._ctrMenu._frmMain.GL_System);
            }
            Lager.Artikel.GArt.ID = TakeOver_ID;
            Lager.Artikel.GArt.Fill();

            if (Lager.Artikel.GArt.ID > 0)
            {
                tbGArtSearch.Text = Lager.Artikel.GArt.ViewID;
                tbGArt.Text = Lager.Artikel.GArt.Bezeichnung;
            }
            else
            {
                tbGArtSearch.Text = string.Empty;
                tbGArt.Text = string.Empty;
            }
            lGArtID.Text = lGueterartenText + "[" + this.Lager.Artikel.GArt.ID.ToString() + "]:";
        }
        ///<summary>ctrArtSearchFilter / SetADRToFrm</summary>
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
                switch (iSearchButton)
                {
                    case 1:
                        Lager.ADR.ID = myDecADR_ID;
                        Lager.ADR.Fill();
                        tbSearchA.Text = strMC;
                        tbAuftraggeber.Text = strE;
                        break;
                }
            }
        }
        ///<summary>ctrArtSearchFilter / btnClearAuftraggeber_Click</summary>
        ///<remarks></remarks>
        private void btnClearAuftraggeber_Click(object sender, EventArgs e)
        {
            this.Lager.ADR.ID = 0;
            tbAuftraggeber.Text = string.Empty;
            tbSearchA.Text = string.Empty;
        }
        ///<summary>ctrArtSearchFilter / btnClearArtikelID_Click</summary>
        ///<remarks></remarks>
        private void btnClearArtikelID_Click(object sender, EventArgs e)
        {
            nudArtikelID.Value = 0;
        }
        ///<summary>ctrArtSearchFilter / btnClearLVSNr_Click</summary>
        ///<remarks></remarks>
        private void btnClearLVSNr_Click(object sender, EventArgs e)
        {
            nudLvsNr.Value = 0;
        }
        ///<summary>ctrArtSearchFilter / btnClearEingangID_Click</summary>
        ///<remarks></remarks>
        private void btnClearEingangID_Click(object sender, EventArgs e)
        {
            nudEingangID.Value = 0;
        }
        ///<summary>ctrArtSearchFilter / btnClearAusgangID_Click</summary>
        ///<remarks></remarks>
        private void btnClearAusgangID_Click(object sender, EventArgs e)
        {
            nudAusgangID.Value = 0;
        }
        ///<summary>ctrArtSearchFilter / btnClearGArtID_Click</summary>
        ///<remarks></remarks>
        private void btnClearGArtID_Click(object sender, EventArgs e)
        {
            this.Lager.Artikel.GArt = new clsGut();
            this.Lager.Artikel.GArt.InitClass(this.GLUser, this.ctrSearch._ctrMenu._frmMain.GL_System);
            this.lGArtID.Text = lGueterartenText + "[0]";
            this.tbGArtSearch.Text = string.Empty;
            this.tbGArt.Text = string.Empty;
        }
        ///<summary>ctrArtSearchFilter / btnClearWerksnummer_Click</summary>
        ///<remarks></remarks>
        private void btnClearWerksnummer_Click(object sender, EventArgs e)
        {
            tbWerksnummer.Text = string.Empty;
        }
        ///<summary>ctrArtSearchFilter / btnClearProduktionsnummer_Click</summary>
        ///<remarks></remarks>
        private void btnClearProduktionsnummer_Click(object sender, EventArgs e)
        {
            tbProduktionsNr.Text = string.Empty;
        }
        ///<summary>ctrArtSearchFilter / btnClearCharge_Click</summary>
        ///<remarks></remarks>
        private void btnClearCharge_Click(object sender, EventArgs e)
        {
            tbCharge.Text = string.Empty;
        }
        ///<summary>ctrArtSearchFilter / btnClearBestellnummer_Click</summary>
        ///<remarks></remarks>
        private void btnClearBestellnummer_Click(object sender, EventArgs e)
        {
            tbBestellnummer.Text = string.Empty;
        }
        ///<summary>ctrArtSearchFilter / btnClearDicke_Click</summary>
        ///<remarks></remarks>
        private void btnClearDicke_Click(object sender, EventArgs e)
        {
            nudDicke.Value = 0;
        }
        ///<summary>ctrArtSearchFilter / btnClearCharge_Click</summary>
        ///<remarks></remarks>
        private void btnClearBreite_Click(object sender, EventArgs e)
        {
            nudBreite.Value = 0;
        }
        ///<summary>ctrArtSearchFilter / btnClearLaenge_Click</summary>
        ///<remarks></remarks>
        private void btnClearLaenge_Click(object sender, EventArgs e)
        {
            nudLaenge.Value = 0;
        }
        ///<summary>ctrArtSearchFilter / btnClearHoehe_Click</summary>
        ///<remarks></remarks>
        private void btnClearHoehe_Click(object sender, EventArgs e)
        {
            nudHoehe.Value = 0;
        }
        ///<summary>ctrArtSearchFilter / btnClearBrutto_Click</summary>
        ///<remarks></remarks>
        private void btnClearBrutto_Click(object sender, EventArgs e)
        {
            nudBrutto.Value = 0;
        }
        ///<summary>ctrArtSearchFilter / btnClearArtikelID_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearArtikelID_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearAuftraggeber_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearAuftraggeber_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearLVSNr_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearLVSNr_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearEingangID_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearEingangID_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearAusgangID_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearAusgangID_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearGArtID_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearGArtID_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearWerksnummer_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearWerksnummer_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearProduktionsnummer_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearProduktionsnummer_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearCharge_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearCharge_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearBestellnummer_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearBestellnummer_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearDicke_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearDicke_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearBreite_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearBreite_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearHoehe_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearHoehe_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / btnClearBrutto_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void btnClearBrutto_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = ((RadRepeatButtonElement)sender).Text;
        }
        ///<summary>ctrArtSearchFilter / SetFilterElementEnabledByColumns</summary>
        ///<remarks></remarks>
        public void SetFilterElementEnabledByColumns(ref RadGridView myDGV)
        {
            for (Int32 i = 0; i <= myDGV.Columns.Count - 1; i++)
            {
                string ColName = myDGV.Columns[i].Name;
                switch (ColName)
                {
                    case "Auftraggeber":
                        if (this._ctrJournal != null)
                        {
                            btnSearchA.Enabled = (!btnSearchA.Enabled);
                            tbAuftraggeber.Enabled = (!tbAuftraggeber.Enabled);
                            tbSearchA.Enabled = (!tbSearchA.Enabled);
                        }
                        else
                        {
                            btnSearchA.Enabled = true;
                            tbAuftraggeber.Enabled = true;
                            tbSearchA.Enabled = true;
                        }
                        break;
                    case "ArtikelID":
                        nudArtikelID.Enabled = (!nudArtikelID.Enabled);
                        break;
                    case "LVSNr":
                    case "LvsNr":
                    case "LVS_ID":
                        nudLvsNr.Enabled = (!nudLvsNr.Enabled);
                        break;
                    case "Eingang":
                    case "EingangID":
                    case "LEingangID":
                        nudEingangID.Enabled = (!nudEingangID.Enabled);
                        break;
                    case "Ausgang":
                    case "AusgangID":
                    case "LAusgangID":
                        nudAusgangID.Enabled = (!nudAusgangID.Enabled);
                        break;
                    case "Freigabe":
                    case "FreigabeAbruf":
                        comboArtFreigabe.Enabled = (!comboArtFreigabe.Enabled);
                        break;
                    case "Gut":
                        if (this._ctrJournal != null)
                        {
                            btnGArt.Enabled = (!btnGArt.Enabled);
                            tbGArt.Enabled = (!tbGArt.Enabled);
                            tbGArtSearch.Enabled = (!tbGArtSearch.Enabled);
                        }
                        else
                        {
                            btnGArt.Enabled = false;
                            tbGArtSearch.Enabled = false;
                            tbGArt.Enabled = false;
                        }
                        break;
                    case "Werksnummer":
                        tbWerksnummer.Enabled = (!tbWerksnummer.Enabled);
                        break;
                    case "Produktionsnummer":
                        tbProduktionsNr.Enabled = (!tbProduktionsNr.Enabled);
                        break;
                    case "Charge":
                        tbCharge.Enabled = (!tbCharge.Enabled);
                        break;
                    case "Bestellnummer":
                        tbBestellnummer.Enabled = (!tbBestellnummer.Enabled);
                        break;
                    case "Dicke":
                        nudDicke.Enabled = (!nudDicke.Enabled);
                        break;
                    case "Breite":
                        nudBreite.Enabled = (!nudBreite.Enabled);
                        break;
                    case "Laenge":
                    case "Länge":
                        nudLaenge.Enabled = (!nudLaenge.Enabled);
                        break;
                    case "Hoehe":
                    case "Höhe":
                        nudHoehe.Enabled = (!nudHoehe.Enabled);
                        break;
                    case "Brutto":
                        nudBrutto.Enabled = (!nudBrutto.Enabled);
                        break;
                    case "exTransportRef":
                    case "ExTransportRef":
                        tbTransportRef.Enabled = (!tbTransportRef.Enabled);
                        break;
                    case "Materialnummer":
                    case "exMaterialnummer":
                        tbMaterialnummer.Enabled = (!tbMaterialnummer.Enabled);
                        break;

                }
            }
        }
        ///<summary>ctrArtSearchFilter / SetFilterforDGV</summary>
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
                //Auftraggeber
                if (Lager.ADR.ID != clsLager.const_FilterAuftraggeber)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }

                    //unterscheidung, da hier als Auftraggeber die ViewId der Adresse verwendet wird
                    if (this._ctrJournal != null || this._ctrWorklist != null || this._ctrBestand != null)
                    {
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Auftraggeber", FilterOperator.IsEqualTo, Lager.ADR.ViewID));
                    }
                    else
                    {
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Auftraggeber", FilterOperator.IsEqualTo, Lager.ADR.ID));
                    }
                }
                //ArtikelID
                if (nudArtikelID.Value != clsLager.const_FilterArtikelID)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("ArtikelID", FilterOperator.IsGreaterThanOrEqualTo, nudArtikelID.Value));
                }
                //LVSNR
                if (nudLvsNr.Value != clsLager.const_FilterLVSNr)
                {
                    if (myDGV.Columns["LVSNr"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("LVSNr", FilterOperator.IsGreaterThanOrEqualTo, nudLvsNr.Value));
                    }
                    if (myDGV.Columns["LVS_ID"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("LVS_ID", FilterOperator.IsGreaterThanOrEqualTo, nudLvsNr.Value));
                    }
                }
                //Eingang
                if (nudEingangID.Value != clsLager.const_FilterLEingangID)
                {
                    if (myDGV.Columns["Eingang"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Eingang", FilterOperator.IsGreaterThanOrEqualTo, nudEingangID.Value));
                    }
                    if (myDGV.Columns["EingangID"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("EingangID", FilterOperator.IsGreaterThanOrEqualTo, nudEingangID.Value));
                    }
                    if (myDGV.Columns["LEingangID"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("LEingangID", FilterOperator.IsGreaterThanOrEqualTo, nudEingangID.Value));
                    }
                }
                //Ausgang
                if (nudAusgangID.Value != clsLager.const_FilterLAusgangID)
                {
                    if (myDGV.Columns["Ausgang"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Ausgang", FilterOperator.IsGreaterThanOrEqualTo, nudAusgangID.Value));
                    }
                    if (myDGV.Columns["AusgangID"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("AusgangID", FilterOperator.IsGreaterThanOrEqualTo, nudAusgangID.Value));
                    }
                    if (myDGV.Columns["LAusgangID"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("LAusgangID", FilterOperator.IsGreaterThanOrEqualTo, nudAusgangID.Value));
                    }
                }
                //Freigabe
                if (comboArtFreigabe.SelectedIndex != clsLager.const_FilterArtikelFreigabe)
                {
                    bool bFreigabe = false;
                    if (comboArtFreigabe.SelectedIndex == 1)
                    {
                        bFreigabe = true;
                    }
                    if (myDGV.Columns["Freigabe"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Freigabe", FilterOperator.IsEqualTo, bFreigabe));
                    }
                    if (myDGV.Columns["FreigabeAbruf"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("FreigabeAbruf", FilterOperator.IsEqualTo, bFreigabe));
                    }
                }
                //Güterart
                if (Lager.Artikel.GArt.ID != clsLager.const_FilterGutID)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    //
                    if (this._ctrJournal != null)
                    {
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("GueterartID", FilterOperator.IsEqualTo, Lager.Artikel.GArt.ID));
                    }
                    else
                    {
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Gut", FilterOperator.IsEqualTo, Lager.Artikel.GArt.ID));
                    }
                }
                //Werksnummer
                if (tbWerksnummer.Text.Trim() != string.Empty)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Werksnummer", FilterOperator.StartsWith, tbWerksnummer.Text.Trim()));
                }
                //Produktionsnummer
                if (tbProduktionsNr.Text.Trim() != string.Empty)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Produktionsnummer", FilterOperator.StartsWith, tbProduktionsNr.Text.Trim()));
                }
                //Charge
                if (tbCharge.Text.Trim() != string.Empty)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Charge", FilterOperator.StartsWith, tbCharge.Text.Trim()));
                }
                //Bestellnummer
                if (tbBestellnummer.Text.Trim() != string.Empty)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Bestellnummer", FilterOperator.StartsWith, tbBestellnummer.Text.Trim()));
                }
                //Dicke
                if (nudDicke.Value != clsLager.const_FilterDicke)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Dicke", FilterOperator.IsEqualTo, nudDicke.Value));
                }
                //Breite
                if (nudBreite.Value != clsLager.const_FilterBreite)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Breite", FilterOperator.IsEqualTo, nudBreite.Value));
                }
                //Laenge
                if (nudLaenge.Value != clsLager.const_FilterLaenge)
                {
                    if (myDGV.Columns["Laenge"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Laenge", FilterOperator.IsEqualTo, nudLaenge.Value));
                    }
                    if (myDGV.Columns["Länge"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Länge", FilterOperator.IsEqualTo, nudLaenge.Value));
                    }
                }
                //Höhe
                if (nudHoehe.Value != clsLager.const_FilterHoehe)
                {
                    if (myDGV.Columns["Hoehe"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Hoehe", FilterOperator.IsEqualTo, nudHoehe.Value));
                    }
                    if (myDGV.Columns["Höhe"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Höhe", FilterOperator.IsEqualTo, nudHoehe.Value));
                    }
                }
                //Brutto
                if (nudBrutto.Value != clsLager.const_FilterBrutto)
                {
                    if (compositeFilter.FilterDescriptors.Count > 0)
                    {
                        compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                    }
                    compositeFilter.FilterDescriptors.Add(new FilterDescriptor("Brutto", FilterOperator.IsEqualTo, nudBrutto.Value));
                }
                //Brutto
                if (tbTransportRef.Text.Trim() != string.Empty)
                {
                    if (myDGV.Columns["exTransportRef"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("exTransportRef", FilterOperator.StartsWith, tbTransportRef.Text));
                    }
                }
                if (tbMaterialnummer.Text.Trim() != string.Empty)
                {
                    if (myDGV.Columns["exMaterialNummer"] != null)
                    {
                        if (compositeFilter.FilterDescriptors.Count > 0)
                        {
                            compositeFilter.LogicalOperator = FilterLogicalOperator.And;
                        }
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor("exMaterialNummer", FilterOperator.StartsWith, tbMaterialnummer.Text));
                    }
                }

                myDGV.FilterDescriptors.Add(compositeFilter);
            }
            else
            {
                myDGV.FilterDescriptors.Clear();
            }
        }
        ///<summary>ctrArtSearchFilter / tbSearchA_TextChanged</summary>
        ///<remarks></remarks>
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GLUser.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbSearchA.Text.ToString();
            string Ausgabe = string.Empty;

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();
            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
            if (dtTmp.Rows.Count > 0)
            {
                Lager.ADR.ID = Functions.GetADR_IDFromTable(dtTmp);
                Lager.ADR.Fill();
            }
            else
            {
                Lager.ADR.ID = 0;
            }
        }
        ///<summary>ctrArtSearchFilter / tbGArtSearch_TextChanged</summary>
        ///<remarks></remarks>
        private void tbGArtSearch_TextChanged(object sender, EventArgs e)
        {
            //Güterarten laden
            DataTable dt = new DataTable();
            dt = clsGut.GetGArtenForCombo(this.GLUser.User_ID);
            string Filter = tbGArtSearch.Text.Trim();
            DataTable dtTmp = new DataTable();
            decimal _decGArtID = 0;
            if (Filter != string.Empty)
            {
                dt.DefaultView.RowFilter = "ViewID ='" + Filter + "'";
                dtTmp = dt.DefaultView.ToTable();
                if (dtTmp.Rows.Count > 0)
                {
                    tbGArtSearch.Text = dtTmp.Rows[0]["ViewID"].ToString();
                    tbGArt.Text = dtTmp.Rows[0]["Bezeichnung"].ToString();
                    _decGArtID = (decimal)dtTmp.Rows[0]["ID"];
                    Lager.Artikel.GArt.ID = _decGArtID;
                    if (this.ctrSearch != null)
                    {
                        Lager.Artikel.GArt.InitClass(this.GLUser, this.ctrSearch._ctrMenu._frmMain.GL_System);
                    }
                    if (this._ctrJournal != null)
                    {
                        Lager.Artikel.GArt.InitClass(this.GLUser, this._ctrJournal._ctrMenu._frmMain.GL_System);
                    }
                    TakeOverGueterArt(_decGArtID);
                }
                else
                {
                    tbGArt.Text = string.Empty;
                    _decGArtID = 0;
                }
            }
            else
            {
                tbGArt.Text = string.Empty;
                _decGArtID = 0;
            }
        }


        internal static void GetFilterString()
        {
            throw new NotImplementedException();
        }

        private void btnClearMaterialnummer_Click(object sender, EventArgs e)
        {
            tbMaterialnummer.Text = string.Empty;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void tbMaterialnummer_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
