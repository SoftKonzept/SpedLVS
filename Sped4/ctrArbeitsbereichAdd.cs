using LVS;
using System;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrArbeitsbereichAdd : UserControl
    {
        public Globals._GL_USER GL_User;
        internal clsArbeitsbereiche clsAB;
        public Boolean bValueCheck;
        internal clsSystem Sys;



        ///<summary>ctrArbeitsbereichAdd / ctrArbeitsbereichAdd</summary>
        ///<remarks></remarks>
        public ctrArbeitsbereichAdd()
        {
            InitializeComponent();
        }
        ///<summary>ctrArbeitsbereichAdd / ctrArbeitsbereichAdd</summary>
        ///<remarks></remarks>
        private void ctrArbeitsbereichAdd_Load(object sender, EventArgs e)
        {
            //Mandenten laden und in Combo anzeigen zur Auswahl
            cbMandant.DataSource = clsMandanten.GetMandatenList(this.GL_User.User_ID);
            cbMandant.DisplayMember = "Matchcode";
            cbMandant.ValueMember = "Mandanten_ID";

            //autoReihenvergabe nur wenn aktiv wenn Client.Lagerreihenverwaltung aktiv
            this.cbAutoRowAssignment.Enabled = this.Sys.Client.Modul.Stammdaten_Lagerreihenverwaltung;
        }
        ///<summary>ctrArbeitsbereichAdd / InitCtrABAdd</summary>
        ///<remarks></remarks>
        public void InitCtrABAdd()
        {
            ClearFrm();
            clsAB = new clsArbeitsbereiche();
            clsAB.BenutzerID = GL_User.User_ID;
        }
        ///<summary>ctrArbeitsbereichAdd / ClearFrm</summary>
        ///<remarks></remarks>
        public void ClearFrm()
        {
            tbABId.Text = String.Empty;
            tbABName.Text = String.Empty;
            tbBemerkung.Text = String.Empty;
            cbMandant.SelectedIndex = -1;
            cbMandant.Enabled = true;
            cbStatus.Checked = false;
            cbIsLager.Checked = false;
            cbIsSpedition.Checked = false;
            cbAutoRowAssignment.Checked = false;
            bValueCheck = false;
            nudMaxArtCountAusang.Value = 0;
            tbABName.Focus();
        }
        ///<summary>ctrArbeitsbereichAdd / cbStatus_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStatus.Checked)
            {
                cbStatus.Text = "aktiv";
            }
            else
            {
                cbStatus.Text = "inaktiv";
            }
        }
        ///<summary>ctrArbeitsbereichAdd / DoCheck</summary>
        ///<remarks></remarks>
        public Boolean DoCheck()
        {
            //Check der Eingabedaten
            bValueCheck = CheckInputValue();
            if (bValueCheck)
            {
                //Klasse Arbeitsbereich aufrufen
                decimal decAbIDTmp = 0;
                Decimal.TryParse(this.tbABId.Text, out decAbIDTmp);
                decimal decTmpMandant = -1;
                Decimal.TryParse(cbMandant.SelectedValue.ToString(), out decTmpMandant);
                if (decTmpMandant > 0)
                {
                    clsAB.ID = decAbIDTmp;
                    clsAB.ABName = tbABName.Text.Trim();
                    clsAB.Bemerkung = tbBemerkung.Text.Trim();
                    clsAB.ASNTransfer = cbASNTransfer.Checked;
                    clsAB.MandantenID = decTmpMandant;
                    clsAB.Aktiv = cbStatus.Checked;
                    clsAB.IsLager = cbIsLager.Checked;
                    clsAB.IsSpedition = cbIsSpedition.Checked;
                    clsAB.UseAutoRowAssignment = cbAutoRowAssignment.Checked;
                    clsAB.ArtMaxCountInAusgang = (int)nudMaxArtCountAusang.Value;
                    //clsAB.InitArbeitsbereich(decAbIDTmp, tbABName.Text.Trim(), tbBemerkung.Text.Trim(), cbStatus.Checked, cbASNTransfer.Checked, decTmpMandant);
                }
            }
            return bValueCheck;
        }
        ///<summary>ctrArbeitsbereichAdd / CheckInputValue</summary>
        ///<remarks></remarks>
        private Boolean CheckInputValue()
        {
            bool bOK = true;
            string strMes = string.Empty;

            //CHeck ob der Arbeitsbereichsname bereits existiert
            if (clsArbeitsbereiche.ExistArbeitsbereich(tbABName.Text, GL_User.User_ID)
                & (tbABId.Text == string.Empty))
            {
                bOK = false;
                strMes = strMes + "Das Arbeitsbereichsname ist bereits vergeben oder leer!" + Environment.NewLine;
            }
            if (tbABName.Text.Trim() == string.Empty)
            {
                bOK = false;
                strMes = strMes + "Das Eingabefeld Arbeitsbereichsname ist leer!" + Environment.NewLine;
            }
            if (tbBemerkung.Text.Trim() == string.Empty)
            {
                //Ist kein Pflichtfeld
                //bOK = false;
                strMes = strMes + "Das Eingabefeld Bemerkung ist leer!" + Environment.NewLine;
            }
            //Mandant muss ausgwählt sein
            if (cbMandant.SelectedIndex < 0)
            {
                //Ist Pflichtfeld 
                bOK = false;
                strMes = strMes + "Das Eingabefeld Mandant ist leer!" + Environment.NewLine;
            }

            if (!bOK)
            {
                clsMessages.Allgemein_EingabeDatenFehlerhaft(strMes);
            }
            return bOK;
        }
        ///<summary>ctrArbeitsbereichAdd / SetFrmForUpdate</summary>
        ///<remarks></remarks>
        public void SetFrmForUpdate(decimal decABId)
        {
            this.clsAB = new clsArbeitsbereiche();
            this.clsAB.BenutzerID = GL_User.User_ID;
            this.clsAB.ID = decABId;
            this.clsAB.Fill();

            //Form füllen
            this.tbABId.Text = this.clsAB.ID.ToString();
            this.tbABName.Text = this.clsAB.ABName;
            this.tbBemerkung.Text = this.clsAB.Bemerkung;
            this.cbStatus.Checked = this.clsAB.Aktiv;
            this.cbASNTransfer.Checked = this.clsAB.ASNTransfer;
            //this.cbIsLager.Checked = this.clsAB.IsLager;
            //this.cbIsSpedition.Checked=this.clsAB.IsSpedition;
            this.cbAutoRowAssignment.Checked = this.clsAB.UseAutoRowAssignment;
            this.nudMaxArtCountAusang.Value = (decimal)this.clsAB.ArtMaxCountInAusgang;
            Functions.SetComboToSelecetedValue(ref cbMandant, clsAB.MandantenID.ToString());
        }
        ///<summary>ctrArbeitsbereichAdd / cbASNTransfer_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbASNTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (cbASNTransfer.Checked)
            {
                cbASNTransfer.Text = "aktiv";
            }
            else
            {
                cbASNTransfer.Text = "inaktiv";
            }
        }
        ///<summary>ctrArbeitsbereichAdd / tbABName_VisibleChanged</summary>
        ///<remarks></remarks>
        private void tbABName_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
                tbABName.Focus();
        }
    }
}
