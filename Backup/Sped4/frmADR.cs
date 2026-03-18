using LVS;
using System;
using System.Windows.Forms;


namespace Sped4
{
    public partial class frmADR : Form
    {
        internal string oldMatchcode = string.Empty;
        internal clsADR ADR = new clsADR();
        internal new bool Update = false;

        public Globals._GL_USER GL_User;
        private decimal ID;
        public ctrADR_List _ctrADRList;

        public frmADR()
        {
            InitializeComponent();
        }
        //
        //---------------------------- init der Comboboxen --------------------
        //
        private void frmADR_Load(object sender, EventArgs e)
        {
            initADR();
            cbLand.DataSource = Enum.GetNames(typeof(enumLand));
            cbFBez.DataSource = Enum.GetNames(typeof(enumFBez));
            tbKundennummer.Text = "0";

            if (this._ctrADRList.ADR != null)
            {
                ADR = this._ctrADRList.ADR;
                if (ADR.ID > 0)
                {
                    ReadDataByID(ADR.ID);
                }
            }
        }
        //******************************************************************************+
        //************            Methoden
        //******************************************************************************
        //
        //----------- INIT FORM  -------------
        //
        private void initADR()
        {
            ID = 0;
            numStdVon.Value = 6;
            numMinVon.Value = 0;
            numStdBis.Value = 15;
            numMinBis.Value = 0;

            pbINr.Visible = false;
            btnKDNrCheck.Visible = false;
            cbDummyADR.Checked = false;

        }
        //        

        //
        //
        //
        private void SetKundennummerToFrm()
        {
            //neue Kundennummer - fortlaufend wird generiert
            clsKunde kunde = new clsKunde();
            decimal autoKundennummer = kunde.NewKD_ID;
            tbKundennummer.Text = autoKundennummer.ToString();
        }
        //
        //------ trimmen und zuweisen der Werte --------
        //
        private void AssignVar()
        {
            int BerA = 2;
            int BerV = 2;
            int BerE = 2;

            ADR = new clsADR();
            ADR._GL_User = this.GL_User;

            //--------- Leerzeichen werden abgeschnitten
            tbSuchname.Text = tbSuchname.Text.Trim();
            tbName1.Text = tbName1.Text.Trim();
            tbName2.Text = tbName2.Text.Trim();
            tbName3.Text = tbName3.Text.Trim();
            tbStr.Text = tbStr.Text.Trim();
            tbHausNr.Text = tbHausNr.Text.Trim();
            tbPF.Text = tbPF.Text.Trim();
            tbPLZ.Text = tbPLZ.Text.Trim();
            tbOrt.Text = tbOrt.Text.Trim();
            tbPLZPF.Text = tbPLZPF.Text.Trim();
            tbOrtPF.Text = tbOrtPF.Text.Trim();
            //tbWAvon.Text = tbWAvon.Text.Trim();
            //tbWAbis.Text = tbWAbis.Text.Trim();

            //---------- Zusweisung der Werte
            ADR.ViewID = tbSuchname.Text;
            if (cbFBez.SelectedItem == null)
            {
                ADR.FBez = cbFBez.SelectedText.ToString();
            }
            else
            {
                ADR.FBez = cbFBez.SelectedItem.ToString();
            }

            ADR.Name1 = tbName1.Text;
            ADR.Name2 = tbName2.Text;
            ADR.Name3 = tbName3.Text;
            ADR.Str = tbStr.Text;
            ADR.HausNr = tbHausNr.Text;
            ADR.PF = tbPF.Text;
            ADR.PLZ = tbPLZ.Text;
            ADR.Ort = tbOrt.Text;
            ADR.PLZPF = tbPLZPF.Text;
            ADR.OrtPF = tbOrtPF.Text;
            ADR.Land = cbLand.SelectedItem.ToString();

            string von = numStdVon.Value.ToString() + ":" + numMinVon.Value.ToString();
            string bis = numStdBis.Value.ToString() + ":" + numMinBis.Value.ToString();

            ADR.WAvon = Convert.ToDateTime("01.01.1900 " + von + ":00");
            ADR.WAbis = Convert.ToDateTime("01.01.1900 " + bis + ":00");

            ADR.Dummy = cbDummyADR.Checked;
            //Kundennummer nicht bei Dummy
            if (!cbDummyADR.Checked)
            {
                if (!Update)
                {
                    SetKundennummerToFrm();
                }
                ADR.KD_ID = Convert.ToInt32(tbKundennummer.Text);
            }
            else
            {
                ADR.KD_ID = 0;
            }

            ADR.DocEinlagerAnzeige = "Standard";
            ADR.DocAuslagerAnzeige = "Standard";

            if (!Update)
            {
                // --- Eintrag in DB ----
                ADR.Add();
                ID = ADR.GetADR_ID();

                //Kundeneintrag nicht für Dummys
                if (!ADR.Dummy)
                {
                    //Eintrag KD_ID in DB Kunde
                    clsKunde kd = new clsKunde();
                    //ID = ADR.GetADR_ID();
                    kd.ADR_ID = ID;
                    kd.KD_ID = ADR.KD_ID;
                    kd.SetDefValueToKDDaten();
                    kd.Add();
                }
            }
            else
            {
                //MessageBox.Show("Update");
                //---- Update Datensatz in DB ---
                ADR.Update();
                this.Close();
            }

            if (this._ctrADRList != null)
            {
                this._ctrADRList.iAdrRange = 1;
                this._ctrADRList.initList();
            }
            else
            {
                this.Close();
            }
        }
        //
        //-------------------------------------- Clean Form -------------------------------
        //
        public void ADRForm_Clean()
        {
            tbSuchname.Text = "";
            tbName1.Text = "";
            tbName2.Text = "";
            tbName3.Text = "";
            tbStr.Text = "";
            tbPLZ.Text = "";
            tbOrt.Text = "";
            tbPF.Text = "";
            tbPLZPF.Text = "";
            tbOrtPF.Text = "";
            cbLand.DataSource = Enum.GetNames(typeof(enumLand));
            cbFBez.DataSource = Enum.GetNames(typeof(enumFBez));
            tbKundennummer.Text = "0";
            //SetKundennummerToFrm();
        }
        //
        //-------------------------------------------- Check for missing Input -------------
        //
        public void CheckMissingInput()
        {
            string strHelp;

            strHelp = "Folgende Pflichtfelder wurden nicht ausgefüllt:\n";
            //-----  Info welche Felder fehlen  ------------------------
            if (tbSuchname.Text == "")
            {
                strHelp = strHelp + "Matchcode \n";
            }
            if (tbName1.Text == "")
            {
                strHelp = strHelp + "Name1 \n";
            }
            if (tbStr.Text == "")
            {
                strHelp = strHelp + "Strasse \n";
            }
            if (tbPLZ.Text == "")
            {
                strHelp = strHelp + "PLZ \n";
            }
            if (tbOrt.Text == "")
            {
                strHelp = strHelp + "Ort \n";
            }
            if (tbPF.Text == "")
            {
                strHelp = strHelp + "Postfach \n";
            }
            if (tbPLZPF.Text == "")
            {
                strHelp = strHelp + "PLZ Postfach \n";
            }
            if (tbOrtPF.Text == "")
            {
                strHelp = strHelp + "Ort Postfach \n";
            }
            MessageBox.Show(strHelp);
        }
        //
        //-------------- Read ADR ---------------
        //
        public void ReadDataByID(decimal adrID)
        {
            Update = true;
            ADR.ID = adrID;
            ADR.Fill();
            ID = adrID;
            SetUpdateItem();
        }
        //
        //----------------------------- setzt den zu ändernen Datensatz  --------------
        //
        private void SetUpdateItem()
        {
            int BerA = 2;
            int BerV = 2;
            int BerE = 2;
            DateTime dtDefault = Convert.ToDateTime("01.01.1900 00:00:00");
            DateTime WAvon = default(DateTime);
            DateTime WAbis = default(DateTime);


            cbDummyADR.Checked = ADR.Dummy;

            if (cbDummyADR.Checked)
            {
                SetFrmForDummy(cbDummyADR.Checked);
                tbSuchname.Text = ADR.ViewID;
                tbPLZ.Text = ADR.PLZ;
                tbOrt.Text = ADR.Ort;
                tbKundennummer.Text = ADR.KD_ID.ToString();
            }
            else
            {
                tbSuchname.Text = ADR.ViewID;
                tbKundennummer.Text = ADR.KD_ID.ToString();
                cbFBez.Text = ADR.FBez;
                tbName1.Text = ADR.Name1;
                tbName2.Text = ADR.Name2;
                tbName3.Text = ADR.Name3;
                tbStr.Text = ADR.Str;
                tbHausNr.Text = ADR.HausNr;
                tbPF.Text = ADR.PF;
                tbPLZ.Text = ADR.PLZ;
                tbPLZPF.Text = ADR.PLZPF;
                tbOrt.Text = ADR.Ort;
                tbOrtPF.Text = ADR.OrtPF;
                cbLand.Text = ADR.Land;
                WAvon = ADR.WAvon;
                WAbis = ADR.WAbis;

            }
            //------ WA
            numStdVon.Value = Convert.ToDecimal(WAvon.Hour.ToString());
            numMinVon.Value = Convert.ToDecimal(WAvon.Minute.ToString());
            numStdBis.Value = Convert.ToDecimal(WAbis.Hour.ToString());
            numMinVon.Value = Convert.ToDecimal(WAbis.Minute.ToString());
        }
        //
        //----------- 
        //
        private void SetInfoImageForKDID()
        {
            if (Functions.CheckNum(tbSuchname.Text))
            {
                decimal KDNr = Convert.ToDecimal(tbSuchname.Text);
                if (clsKunde.CheckKundenNummerIsUsed(KDNr) | (Convert.ToInt32(tbSuchname.Text) == 0))
                {
                    pbINr.Image = Sped4.Properties.Resources.critical.ToBitmap();
                }
                else
                {
                    pbINr.Image = Sped4.Properties.Resources.check;
                }
            }
            else
            {
                clsMessages.ADR_ViewIDisNotNo();
            }
        }
        //
        //-------------- Button Check Kundennummer -----------------
        //
        private void btnKDNrCheck_Click(object sender, EventArgs e)
        {
            SetInfoImageForKDID();
        }
        ///<summary>frmADR / tsbSpeichern_Click</summary>
        ///<remarks>Speichern der Eingabe.</remarks>
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            bool auth = false;
            bool Save = false;
            bool SaveOK = true;

            if (cbDummyADR.Checked)
            {
                Save = true;
            }
            else
            {
                //Check, ob Suchbegriff / Matchcode bereits vergeben
                tbSuchname.Text = tbSuchname.Text.ToString().Trim();
                if (!Update)
                {
                    tbSuchname.Text = tbSuchname.Text.ToString().Trim();
                    if (!clsADR.CheckMatchcodeIsUsed(tbSuchname.Text))
                    {
                        Save = true;
                        if (tbSuchname.Text.ToString() == "0")
                        {
                            Save = false;
                            clsMessages.ADR_ViewIDnotZero();
                        }
                    }
                    else
                    {
                        clsMessages.ADR_ViewIDIsUsed();
                        Save = false;
                    }
                }
                else
                {
                    Save = true;
                }
            }

            //Check Berechtigung
            if (Save)
            {
                auth = GL_User.write_ADR;
            }
            else
            {
                auth = GL_User.write_ADR;
            }

            if (auth)
            {
                if (Save)
                {
                    if (cbDummyADR.Checked)
                    {
                        //Eingabefeld hier Name3 darf nicht leer sein
                        if (tbName3.Text != "")
                        {
                            AssignVar();
                            //-- Eingabemaske auf Null setzen
                            ADRForm_Clean();
                        }
                        SetFrmForDummy(false);
                        if (this._ctrADRList != null)
                        {
                            _ctrADRList.SetOldSearchList();
                        }
                    }
                    else
                    {
                        //------ Eingabeüberprüfung der Pflichtfelder-----
                        // Adresse mit Straße muss vollständig sein ( Suchbegriff, Name1,Str, PLZ, Ort)

                        if ((tbSuchname.Text != "") & (tbName1.Text != ""))
                        {
                            if (!clsADR.ViewIDExists(this.GL_User, tbSuchname.Text) | (Update == true))
                            {
                                if (SaveOK)
                                {
                                    if (((tbStr.Text != "") & (tbPLZ.Text != "") & (tbOrt.Text != "")) | ((tbPF.Text != "") & (tbPLZPF.Text != "") & (tbOrtPF.Text != "")))
                                    {
                                        AssignVar();

                                        //-- Eingabemaske auf Null setzen
                                        ADRForm_Clean();
                                    }
                                    else
                                    {
                                        CheckMissingInput();
                                    }
                                }
                            }
                            else
                            {
                                CheckMissingInput();
                            }
                            if (this._ctrADRList != null)
                            {
                                _ctrADRList.SetOldSearchList();
                            }
                        }
                        else
                        {
                            clsMessages.ADR_ViewIDnotZero();
                        }
                    }
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>frmADR / tsbtnClose_Click</summary>
        ///<remarks>Schliesst die Frm.</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///<summary>frmADR / cbDummyADR_CheckedChanged</summary>
        ///<remarks>Auswahl Dummy Adresse.</remarks>
        private void cbDummyADR_CheckedChanged(object sender, EventArgs e)
        {
            SetFrmForDummy(cbDummyADR.Checked);
        }
        ///<summary>frmADR / SetFrmForDummy</summary>
        ///<remarks>Auswahl Dummy Adresse.</remarks> 
        private void SetFrmForDummy(bool bDummy)
        {
            ADRForm_Clean();
            if (bDummy)
            {
                //Textboxen deak.
                tbSuchname.Enabled = false;
                cbFBez.Enabled = false;
                tbName1.Enabled = false;
                tbName2.Enabled = false;
                tbName3.Text = "MusterAdresse";
                tbStr.Enabled = false;
                tbPLZPF.Enabled = false;
                tbOrtPF.Enabled = false;
                tbPF.Enabled = false;
                cbLand.Enabled = false;
                numStdVon.Enabled = false;
                numStdBis.Enabled = false;
                numMinVon.Enabled = false;
                numMinBis.Enabled = false;
                tbKundennummer.Enabled = false;

                //Vorgaben setzen
                tbSuchname.Text = "_";
            }
            else
            {
                //Textboxen deak.
                tbSuchname.Enabled = true;
                cbFBez.Enabled = true;
                tbName1.Enabled = true;
                tbName2.Enabled = true;
                //tbName3.Enabled = false;
                tbStr.Enabled = true;
                tbPLZPF.Enabled = true;
                tbOrtPF.Enabled = true;
                tbPF.Enabled = true;
                cbLand.Enabled = true;
                numStdVon.Enabled = true;
                numStdBis.Enabled = true;
                numMinVon.Enabled = true;
                numMinBis.Enabled = true;
                tbKundennummer.Enabled = true;
                cbDummyADR.Checked = false;
            }
        }
        ///<summary>frmADR / tbOrt_TextChanged</summary>
        ///<remarks>Nach Ortseingabe wird das Suchfeld aktualisiert.</remarks> 
        private void tbOrt_TextChanged(object sender, EventArgs e)
        {
            if (cbDummyADR.Checked)
            {
                tbSuchname.Text = "_" + tbOrt.Text;
            }
        }
    }
}
