using LVS;
using LVS.Helper;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrADRManAdd : UserControl
    {
        public Globals._GL_USER GLUser;
        public ctrMenu _ctrMenu;
        public ctrEinlagerung _ctrEinlagerung;
        public ctrAuslagerung _ctrAuslagerung;
        public ctrArtDetails _ctrArtDetails;
        public frmAuftrag_Fast _frmAuftragFast;
        public clsLEingang Eingang;
        public clsLAusgang Ausgang;
        public clsAuftrag Auftrag;
        public clsADRMan ADRMan;
        public frmTmp _frmTmp;

        bool bUpdate;

        /******************************************************************
         *                  Procedure / Methoden
         * ***************************************************************/
        ///<summary>ctrADRManAdd / ctrADRManAdd</summary>
        ///<remarks></remarks>
        public ctrADRManAdd()
        {
            InitializeComponent();
        }
        ///<summary>ctrADRManAdd / ctrADRManAdd_Load</summary>
        ///<remarks></remarks>
        private void ctrADRManAdd_Load(object sender, EventArgs e)
        {
            cbFBez.DataSource = Enum.GetNames(typeof(enumFBez));
            cbLKZ.DataSource = helper_Laenderkennzeichen.DicCountry().ToList();
            cbLKZ.DisplayMember = "Key";
            cbLKZ.ValueMember = "Key";
            cbLKZ.SelectedIndex = 0;
            cbLand.DataSource = helper_Laenderkennzeichen.DicCountry().ToList();
            cbLand.DisplayMember = "Value";
            cbLand.ValueMember = "Key";
            cbLand.SelectedIndex = 0;
            Functions.SetComboToSelecetedValue(ref cbLand, "D");

            InitCtr();
        }
        ///<summary>ctrADRManAdd / InitCtr</summary>
        ///<remarks></remarks>
        private void InitCtr()
        {
            bUpdate = false;
            this.ADRMan = new clsADRMan();

            string strTmpText = string.Empty;
            if (this._ctrEinlagerung != null)
            {
                //this.ADRMan = this.Eingang.AdrManuell;  
                this.Eingang = _ctrEinlagerung.Lager.Eingang;
                this.ADRMan.InitClass(this.GLUser, this.Eingang.LEingangTableID, clsLEingang.const_DBTableName);
                clsADRMan tmpADRMan = new clsADRMan();
                try
                {
                    this.ADRMan.DictManuellADREinlagerung.TryGetValue(this.Eingang.AdrManuell.AdrArtID, out tmpADRMan);
                    if (tmpADRMan != null)
                    {
                        this.ADRMan = tmpADRMan;
                    }
                    else
                    {
                        this.ADRMan = new clsADRMan();
                        this.ADRMan.InitClass(this.GLUser, this.Eingang.LEingangTableID, clsLEingang.const_DBTableName);
                    }
                    bUpdate = true;
                    SetAdrDatenToCtr();
                }
                catch (Exception ex)
                {
                    string strError = ex.ToString();
                    bUpdate = false;
                    ClearInputField();
                }
                strTmpText = "Einlagerung ID[" + this.ADRMan.TableID.ToString() + "] | " + this._ctrEinlagerung.manAdrArt;
            }
            if (this._ctrAuslagerung != null)
            {
                this.Ausgang = _ctrAuslagerung.Lager.Ausgang;
                this.ADRMan.InitClass(this.GLUser, this.Ausgang.LAusgangTableID, clsLAusgang.const_DBTableName);
                this.ADRMan = this.Ausgang.AdrManuell;
                clsADRMan tmpADRMan = new clsADRMan();
                try
                {
                    this.ADRMan.DictManuellADRAuslagerung.TryGetValue(this.Ausgang.AdrManuell.AdrArtID, out tmpADRMan);
                    if (tmpADRMan != null)
                    {
                        this.ADRMan = tmpADRMan;
                    }
                    else
                    {
                        this.ADRMan = new clsADRMan();
                        this.ADRMan.InitClass(this.GLUser, this.Ausgang.LAusgangTableID, clsLAusgang.const_DBTableName);
                    }
                    bUpdate = true;
                    SetAdrDatenToCtr();
                }
                catch (Exception ex)
                {
                    string strError = ex.ToString();
                    bUpdate = false;
                    ClearInputField();
                }
                strTmpText = "Auslagerung ID[" + this.ADRMan.TableID.ToString() + "] | " + this._ctrAuslagerung.manAdrArt;
            }
            if (this._frmAuftragFast != null)
            {
                this.Auftrag = this._frmAuftragFast.Auftrag;
                this.ADRMan.InitClass(this.GLUser, this.Auftrag.ID, clsAuftrag.const_DBTableName);
                this.ADRMan = this.Auftrag.AdrManuell;
                clsADRMan tmpADRMan = new clsADRMan();
                try
                {
                    this.ADRMan.DictManuellADRAuftrag.TryGetValue(this.Auftrag.AdrManuell.AdrArtID, out tmpADRMan);
                    if (tmpADRMan != null)
                    {
                        this.ADRMan = tmpADRMan;
                    }
                    else
                    {
                        this.ADRMan = new clsADRMan();
                        this.ADRMan.InitClass(this.GLUser, this.Auftrag.ID, clsAuftrag.const_DBTableName);
                    }
                    bUpdate = true;
                    SetAdrDatenToCtr();
                }
                catch (Exception ex)
                {
                    string strError = ex.ToString();
                    bUpdate = false;
                    ClearInputField();
                }
                strTmpText = "Auftragerfassung ID[" + this.ADRMan.TableID.ToString() + "] | " + this._frmAuftragFast.manAdrArt;
            }
            if (this._ctrArtDetails != null)
            {
                this.Auftrag = this._ctrArtDetails.Auftrag;
                this.ADRMan.InitClass(this.GLUser, this.Auftrag.ID, clsAuftrag.const_DBTableName);
                this.ADRMan = this.Auftrag.AdrManuell;
                clsADRMan tmpADRMan = new clsADRMan();
                try
                {
                    this.ADRMan.DictManuellADRAuftrag.TryGetValue(this.Auftrag.AdrManuell.AdrArtID, out tmpADRMan);
                    if (tmpADRMan != null)
                    {
                        this.ADRMan = tmpADRMan;
                    }
                    else
                    {
                        this.ADRMan = new clsADRMan();
                        this.ADRMan.InitClass(this.GLUser, this.Auftrag.ID, clsAuftrag.const_DBTableName);
                    }
                    bUpdate = true;
                    SetAdrDatenToCtr();
                }
                catch (Exception ex)
                {
                    string strError = ex.ToString();
                    bUpdate = false;
                    ClearInputField();
                }
                strTmpText = "Auftragerfassung ID[" + this.ADRMan.TableID.ToString() + "] | " + this._ctrArtDetails.manAdrArt;
            }
            this.tslAdrArt.Text = strTmpText;
        }
        ///<summary>ctrADRManAdd / ClearInputField</summary>
        ///<remarks></remarks>
        private void ClearInputField()
        {
            tbName1.Text = string.Empty;
            tbName2.Text = string.Empty;
            tbName3.Text = string.Empty;
            tbStr.Text = string.Empty;
            tbOrt.Text = string.Empty;
            tbPLZ.Text = string.Empty;
        }
        ///<summary>ctrADRManAdd / SetAdrDatenToCtr</summary>
        ///<remarks></remarks>
        private void SetAdrDatenToCtr()
        {
            if (this.ADRMan != null)
            {
                if (this.ADRMan.ID > 0)
                {
                    Functions.SetComboToSelecetedValue(ref this.cbFBez, this.ADRMan.FBez);
                    tbName1.Text = this.ADRMan.Name1;
                    tbName2.Text = this.ADRMan.Name2;
                    tbName3.Text = this.ADRMan.Name3;
                    tbStr.Text = this.ADRMan.Str;
                    tbOrt.Text = this.ADRMan.Ort;
                    tbPLZ.Text = this.ADRMan.PLZ;
                }
                else
                {
                    ClearInputField();
                }
            }
        }
        ///<summary>ctrADRManAdd / AssignValue</summary>
        ///<remarks></remarks>
        private void AssignValue()
        {
            decimal decTmpID = this.ADRMan.ID;
            this.ADRMan = new clsADRMan();
            this.ADRMan._GL_User = this.GLUser;

            if (this._ctrEinlagerung != null)
            {
                this.ADRMan.AdrArtID = this.Eingang.AdrManuell.AdrArtID;
                this.ADRMan.TableName = this.Eingang.AdrManuell.TableName;
                this.ADRMan.TableID = this.Eingang.AdrManuell.TableID;
            }
            if (this._ctrAuslagerung != null)
            {
                this.ADRMan.AdrArtID = this.Ausgang.AdrManuell.AdrArtID;
                this.ADRMan.TableName = this.Ausgang.AdrManuell.TableName;
                this.ADRMan.TableID = this.Ausgang.AdrManuell.TableID;
            }
            if (
                (this._frmAuftragFast != null) ||
                (this._ctrArtDetails != null)
               )
            {
                this.ADRMan.AdrArtID = this.Auftrag.AdrManuell.AdrArtID;
                this.ADRMan.TableName = this.Auftrag.AdrManuell.TableName;
                this.ADRMan.TableID = this.Auftrag.AdrManuell.TableID;
            }


            this.ADRMan.FBez = cbFBez.Text.ToString();
            this.ADRMan.Name1 = tbName1.Text.Trim();
            this.ADRMan.Name2 = tbName2.Text.Trim();
            this.ADRMan.Name3 = tbName3.Text.Trim();
            this.ADRMan.Str = tbStr.Text.Trim();
            this.ADRMan.HausNr = tbHausNr.Text.Trim();
            this.ADRMan.PLZ = tbPLZ.Text.Trim();
            this.ADRMan.Ort = tbOrt.Text.Trim();
            if (cbLand.SelectedIndex > -1)
            {
                this.ADRMan.LKZ = cbLand.SelectedValue.ToString();
                this.ADRMan.Land = cbLand.Text.ToString();
            }
            else
            {
                this.ADRMan.LKZ = string.Empty;
                this.ADRMan.Land = string.Empty;
            }

            //check Update noch einmal
            bUpdate = this.ADRMan.CheckManADRForAdrArt();
            if (bUpdate)
            {
                this.ADRMan.ID = decTmpID;
                this.ADRMan.Update();
                UpdateEinAusgangData();
            }
            else
            {
                this.ADRMan.Add();
                UpdateEinAusgangData();
            }
            CloseCtr();
        }
        ///<summary>ctrADRManAdd / UpdateEinAusgangData</summary>
        ///<remarks></remarks>
        private void UpdateEinAusgangData()
        {
            if (this._ctrEinlagerung != null)
            {
                switch (this.ADRMan.AdrArtID)
                {
                    case clsADRMan.cont_AdrArtID_Auftraggeber:
                        //Auftraggeber kann keien manuelle Adresse hinzugefügt werden
                        break;
                    case clsADRMan.cont_AdrArtID_Versender:
                        this.Eingang.Versender = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Beladeadresse:
                        this.Eingang.BeladeID = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Empfaenger:
                        this.Eingang.Empfaenger = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Entladeadresse:
                        this.Eingang.EntladeID = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Spedition:
                        this.Eingang.SpedID = 0;
                        break;
                }
                this.Eingang.UpdateLagerEingang();
            }
            if (this._ctrAuslagerung != null)
            {
                switch (this.ADRMan.AdrArtID)
                {
                    case clsADRMan.cont_AdrArtID_Auftraggeber:
                        //Auftraggeber kann keien manuelle Adresse hinzugefügt werden
                        break;
                    case clsADRMan.cont_AdrArtID_Versender:
                        this.Ausgang.Versender = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Beladeadresse:
                        this.Ausgang.BeladeID = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Empfaenger:
                        this.Ausgang.Empfaenger = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Entladeadresse:
                        this.Ausgang.Entladestelle = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Spedition:
                        this.Ausgang.SpedID = 0;
                        break;
                }
                //Update der Ausgang
                this.Ausgang.UpdateLagerAusgang();
            }
            if (
                    (this._frmAuftragFast != null) ||
                    (this._ctrArtDetails != null)
                )
            {
                switch (this.ADRMan.AdrArtID)
                {
                    case clsADRMan.cont_AdrArtID_Auftraggeber:
                        //Auftraggeber kann keien manuelle Adresse hinzugefügt werden
                        break;
                    case clsADRMan.cont_AdrArtID_Versender:
                        this.Auftrag.B_ID = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Beladeadresse:
                        //this.Ausgang.BeladeID = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Empfaenger:
                        this.Auftrag.E_ID = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Entladeadresse:
                        //this.Ausgang.Entladestelle = 0;
                        break;
                    case clsADRMan.cont_AdrArtID_Spedition:
                        //this.Ausgang.SpedID = 0;
                        break;
                }
                //Update der Ausgang
                this.Auftrag.Update();
            }
        }
        ///<summary>ctrADRManAdd / cbLand_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void cbLand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLand.SelectedValue != null)
            {
                Functions.SetComboToSelecetedItem(ref cbLKZ, cbLand.SelectedValue.ToString());
            }
        }
        ///<summary>ctrADRManAdd / tsbtnSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            if (this.GLUser.write_ADR)
            {
                AssignValue();
            }
            CloseCtr();
        }
        ///<summary>ctrADRManAdd / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            CloseCtr();
        }
        ///<summary>ctrADRManAdd / tsbtnDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            if (this.ADRMan.ID > 0)
            {
                if (clsMessages.ADR_DeleteDatenSatz())
                {
                    this.ADRMan.Delete();
                    ClearInputField();
                }
            }
        }
        ///<summary>ctrADRManAdd / CloseCtr</summary>
        ///<remarks></remarks>
        private void CloseCtr()
        {
            if (this._ctrEinlagerung != null)
            {
                this._ctrMenu._ctrEinlagerung.SetLEingangskopfdatenToFrm(true);
            }
            if (this._ctrAuslagerung != null)
            {
                this._ctrMenu._ctrAuslagerung.SetLAusgangsdatenToFrm();
            }
            if (this._frmAuftragFast != null)
            {
                this._frmAuftragFast.SetAuftragDatenToFrm();
                this._frmAuftragFast.InitDGV();
            }
            if (this._ctrArtDetails != null)
            {
                this._ctrArtDetails.SetADRDatenToCtr();
            }
            this._frmTmp.CloseFrmTmp();
        }


    }
}
