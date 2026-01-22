using LVS;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmKunde : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        private bool update = false;
        private decimal db_KD_ID;
        private decimal KD_ID;
        private decimal ADR_ID;

        internal decimal KD_IDold = 0;

        private decimal adrID;
        ctrADR_List _ctrADRList = new ctrADR_List();


        //Sammeldebitorennummer am Anfang
        public Int32 defDebobitor = 10000;

        public frmKunde(ctrADR_List ctrADRList)
        {
            InitializeComponent();
            InitForm();
            _ctrADRList = ctrADRList;
            GL_User = _ctrADRList.GL_User;
        }
        //
        //
        //
        private void InitForm()
        {
            CleanTextBox();
            adrID = 0;
            tbDebitor.Text = defDebobitor.ToString(); //Sammeldebitorennummer bei Heisiep



            lKreditor_Neu.Visible = false;
            tbKreditor.Visible = false;

            lBank2.Visible = false;
            lB2_Kto_Neu.Visible = false;
            lB2_BLZ_Neu.Visible = false;
            lSwift2_Neu.Visible = false;
            lIBAN2_Neu.Visible = false;

            tbB2.Visible = false;
            tbB2_Kto.Visible = false;
            tbB2_BLZ.Visible = false;
            tbSWIFT2.Visible = false;
            tbIBAN2.Visible = false;


            //check Kunden id false
            pbINr.Visible = false;
            btnKDNrCheck.Visible = false;


        }
        //
        //------------------------------------ Read ADR ---------------
        //
        public void TakeOverID(decimal ID)
        {
            adrID = ID;

            //ADR Daten
            SetRecValueADR(clsADR.ReadADRbyID(adrID));

            //Kontakt Daten
            //Load_KontaktGrid(clsKontakte.ReadKontaktebyID(adrID));

            //KD Daten
            SetRecValueKD(clsKunde.ReadKDbyID(adrID));

            /****
            if (tbKD_ID.Text == string.Empty)
            {
              KD_IDold = Convert.ToInt32(tbKD_ID.Text);
            }
            else
            {
              KD_IDold = Convert.ToInt32(tbKD_ID.Text);
            }
             * ***/
            if (tbKD_ID.Text == string.Empty)
            {
                KD_IDold = 0;
            }
            else
            {
                KD_IDold = Convert.ToInt32(tbKD_ID.Text);
            }
        }
        //
        //------------------ Zweigt den gewählten ADR- Datensatz an--------------
        //
        private void SetRecValueADR(DataSet ds)
        {
            string strPLZ, strOrt, strPLZPF, strOrtPF;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ID"].ToString() != "")
                    {
                        ADR_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["ViewID"].ToString() != "")
                    {
                        lSuchname.Text = ds.Tables[0].Rows[i]["ViewID"].ToString();
                    }
                    else
                    {
                        lSuchname.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["FBez"].ToString() != "")
                    {
                        lFBez.Text = ds.Tables[0].Rows[i]["FBez"].ToString();
                    }
                    else
                    {
                        lFBez.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["Name1"].ToString() != "")
                    {
                        lName1.Text = ds.Tables[0].Rows[i]["Name1"].ToString();
                    }
                    else
                    {
                        lName1.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["Name2"].ToString() != "")
                    {
                        lName2.Text = ds.Tables[0].Rows[i]["Name2"].ToString();
                    }
                    else
                    {
                        lName2.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["Name3"].ToString() != "")
                    {
                        lName3.Text = ds.Tables[0].Rows[i]["Name3"].ToString();
                    }
                    else
                    {
                        lName3.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["Str"].ToString() != "")
                    {
                        lStr.Text = ds.Tables[0].Rows[i]["Str"].ToString();
                    }
                    else
                    {
                        lStr.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["PF"].ToString() != "")
                    {
                        lPF.Text = ds.Tables[0].Rows[i]["PF"].ToString();
                    }
                    else
                    {
                        lPF.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["PLZ"].ToString() != "")
                    {
                        strPLZ = ds.Tables[0].Rows[i]["PLZ"].ToString();
                    }
                    else
                    {
                        strPLZ = "";
                    }
                    if (ds.Tables[0].Rows[i]["PLZPF"].ToString() != "")
                    {
                        strPLZPF = ds.Tables[0].Rows[i]["PLZPF"].ToString();
                    }
                    else
                    {
                        strPLZPF = "";
                    }
                    if (ds.Tables[0].Rows[i]["Ort"].ToString() != "")
                    {
                        strOrt = ds.Tables[0].Rows[i]["Ort"].ToString();
                    }
                    else
                    {
                        strOrt = "";
                    }
                    if (ds.Tables[0].Rows[i]["OrtPF"].ToString() != "")
                    {
                        strOrtPF = ds.Tables[0].Rows[i]["OrtPF"].ToString();
                    }
                    else
                    {
                        strOrtPF = "";
                    }
                    if (ds.Tables[0].Rows[i]["Land"].ToString() != "")
                    {
                        lLand.Text = ds.Tables[0].Rows[i]["Land"].ToString();
                    }
                    else
                    {
                        lLand.Text = "";
                    }

                    lPFPLZuOrt.Text = strPLZPF + " " + strOrtPF;
                    lPLZuOrt.Text = strPLZ + " " + strOrt;
                }
            }
            else
            {
                gbAdressdetails.Text = "!!! keine Adressdaten vorhanden !!!";
                lSuchname.Text = "";
                lFBez.Text = "";
                lName1.Text = "";
                lName2.Text = "";
                lName3.Text = "";
                lStr.Text = "";
                lPF.Text = "";
                strPLZ = "";
                strPLZPF = "";
                strOrt = "";
                strOrtPF = "";
                lLand.Text = "";
            }
        }
        //
        //--------------- zeigt die zugehörigen Kontakte an   ------------------------  
        //
        private void Load_KontaktGrid(DataTable dt)
        {
            grdKontakte.DataSource = dt;
            int test = grdKontakte.RowCount;
            if (grdKontakte.RowCount > 0)
            {
                grdKontakte.Columns["ID"].Visible = false;
                grdKontakte.Columns["Suchbegriff"].Visible = false;
                gbKontakte.Text = "zugehörende Kontaktdaten";
                gbKDDaten.Text = "zugehörende Kundendaten";
            }
            else
            {
                grdKontakte.Visible = false;
                gbKontakte.Text = "!!! keine Kontakte vorhanden !!!";
            }
        }
        //
        //---------------- zeigt die zugehörigen Kundendaten an ---------------------
        //
        private void SetRecValueKD(DataSet ds)
        {
            string strBVer1 = "";
            string strBVer2 = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                gbKDDaten.Text = "zugehörende Kundendaten";
                for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["ID"].ToString() != "")
                    {
                        db_KD_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["KD_ID"].ToString() != "")
                    {
                        lKD_ID.Text = "Kunden Nr.: " + ds.Tables[0].Rows[i]["KD_ID"].ToString();
                        tbKD_ID.Text = ds.Tables[0].Rows[i]["KD_ID"].ToString();
                        KD_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["KD_ID"].ToString());
                        update = true;
                    }
                    else
                    {
                        lKD_ID.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["SteuerNr"].ToString() != "")
                    {
                        lSteuerNr.Text = "Steuernummer: " + ds.Tables[0].Rows[i]["SteuerNr"].ToString();
                        tbStNr.Text = ds.Tables[0].Rows[i]["SteuerNr"].ToString();
                        update = true;
                    }
                    else
                    {
                        lSteuerNr.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["USt_ID"].ToString() != "")
                    {
                        lUSt.Text = "Umsatzsteuer ID: " + ds.Tables[0].Rows[i]["USt_ID"].ToString();
                        tbUStID.Text = ds.Tables[0].Rows[i]["USt_ID"].ToString();
                        update = true;
                    }
                    else
                    {
                        lUSt.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["MwSt"].Equals("T"))
                    {
                        lMwStSatz.Text = "MwSt. Satz: " + ds.Tables[0].Rows[i]["MwStSatz"].ToString();
                        tbMwStSatz.Text = ds.Tables[0].Rows[i]["MwStSatz"].ToString();
                        update = true;
                    }
                    else
                    {
                        lMwStSatz.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["Bank1"].ToString() != "")
                    {
                        lB1.Text = ds.Tables[0].Rows[i]["Bank1"].ToString();
                        tbB1.Text = ds.Tables[0].Rows[i]["Bank1"].ToString();
                    }
                    else
                    {
                        lB1.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["BLZ1"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[i]["BLZ1"].ToString() == "0")
                        {
                            strBVer1 = "";
                        }
                        else
                        {
                            strBVer1 = strBVer1 + "(BLZ " + ds.Tables[0].Rows[i]["BLZ1"].ToString() + ")";
                            tbB1_BLZ.Text = ds.Tables[0].Rows[i]["BLZ1"].ToString();
                        }
                    }
                    else
                    {
                        strBVer1 = "";
                    }
                    if (ds.Tables[0].Rows[i]["Kto1"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[i]["Kto1"].ToString() == "0")
                        {
                            strBVer1 = "";
                        }
                        else
                        {
                            strBVer1 = strBVer1 + " Kto. " + ds.Tables[0].Rows[i]["Kto1"].ToString();
                            tbB1_Kto.Text = ds.Tables[0].Rows[i]["Kto1"].ToString();
                        }
                    }
                    else
                    {
                        strBVer1 = "";
                    }
                    if (ds.Tables[0].Rows[i]["Swift1"].ToString() != "")
                    {
                        lSwift1.Text = "SWIFT: " + ds.Tables[0].Rows[i]["Swift1"].ToString();
                        tbSwift1.Text = ds.Tables[0].Rows[i]["Swift1"].ToString();
                    }
                    else
                    {
                        lSwift1.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["IBAN1"].ToString() != "")
                    {
                        lIBAN1.Text = "IBAN: " + ds.Tables[0].Rows[i]["IBAN1"].ToString();
                        tbIBAN1.Text = ds.Tables[0].Rows[i]["IBAN1"].ToString();
                    }
                    else
                    {
                        lIBAN1.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["Bank2"].ToString() != "")
                    {
                        lB2.Text = ds.Tables[0].Rows[i]["Bank2"].ToString();
                    }
                    else
                    {
                        lB2.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["BLZ2"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[i]["BLZ2"].ToString() == "0")
                        {
                            strBVer2 = "";
                        }
                        else
                        {
                            strBVer2 = strBVer2 + "(BLZ " + ds.Tables[0].Rows[i]["BLZ2"].ToString() + ")";
                        }
                    }
                    else
                    {
                        strBVer2 = "";
                    }
                    if (ds.Tables[0].Rows[i]["Kto2"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[i]["Kto2"].ToString() == "0")
                        {
                            strBVer2 = "";
                        }
                        else
                        {
                            strBVer2 = strBVer2 + " Kto. " + ds.Tables[0].Rows[i]["Kto2"].ToString();
                        }
                    }
                    else
                    {
                        strBVer2 = "";
                    }
                    if (ds.Tables[0].Rows[i]["Swift2"].ToString() != "")
                    {
                        lSwift2.Text = "SWIFT: " + ds.Tables[0].Rows[i]["Swift2"].ToString();
                    }
                    else
                    {
                        lSwift2.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["IBAN2"].ToString() != "")
                    {
                        lIBAN2.Text = "IBAN :" + ds.Tables[0].Rows[i]["IBAN2"].ToString();
                    }
                    else
                    {
                        lIBAN2.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["Kreditor"].ToString() != "")
                    {
                        lKreditor.Text = "Kreditor: " + ds.Tables[0].Rows[i]["Kreditor"].ToString();
                    }
                    else
                    {
                        lKreditor.Text = "";
                    }
                    if (ds.Tables[0].Rows[i]["Debitor"].ToString() != "")
                    {
                        lDebitor.Text = "Debitor: " + ds.Tables[0].Rows[i]["Debitor"].ToString();
                        tbDebitor.Text = ds.Tables[0].Rows[i]["Debitor"].ToString();
                    }
                    else
                    {
                        lDebitor.Text = "";
                    }
                    lB1_BLZ.Text = strBVer1;
                    lB2_BLZ.Text = strBVer2;

                }
            }
            else
            {
                gbKDDaten.Text = " !!! keine Kundendaten vorhanden !!! ";

                lKD_ID.Text = "";
                lSteuerNr.Text = "";
                lUSt.Text = "";
                lMwStSatz.Text = "";
                lB1.Text = "";
                lB1_BLZ.Text = "";
                //lB1_Kto.Text = "";
                lSwift1.Text = "";
                lIBAN1.Text = "";
                lB2.Text = "";
                lB2_BLZ.Text = "";
                //lB2_Kto.Text = "";
                lSwift2.Text = "";
                lIBAN2.Text = "";
                lKreditor.Text = "";
                lDebitor.Text = "";
            }
        }
        //
        //-------------- CheckBox MwST  -----------------------
        //
        private void cbMwSt_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMwSt.Checked != true)
                tbMwStSatz.Visible = false;
        }
        //
        //
        //
        private void CleanTextBox()
        {
            tbKD_ID.Text = "0";
            tbStNr.Text = ""; ;
            tbUStID.Text = "";
            cbMwSt.Checked = true;
            tbMwStSatz.Text = "19,00";
            tbB1.Text = "";
            tbB1_Kto.Text = "0";
            tbB1_BLZ.Text = "0";
            tbSwift1.Text = "";
            tbIBAN1.Text = "";
            tbB2.Text = "";
            tbB2_Kto.Text = "0";
            tbB2_BLZ.Text = "0";
            tbSWIFT2.Text = "";
            tbIBAN2.Text = "";
            tbDebitor.Text = "";
            tbKreditor.Text = "";
            cbMwSt.Checked = true;
            tbMwStSatz.Visible = true;
        }
        //
        //------------- AssignValue  ------------------------
        //
        private void AssignVar()
        {
            clsKunde KD = new clsKunde();
            KD.BenutzerID = GL_User.User_ID;

            //--------- Leerzeichen werden abgeschnitten
            tbStNr.Text = tbStNr.Text.ToString().Trim();
            tbUStID.Text = tbUStID.Text.ToString().Trim();
            tbMwStSatz.Text = tbMwStSatz.Text.ToString().Trim();

            tbB1.Text = tbB1.Text.ToString().Trim();
            tbB1_Kto.Text = tbB1_Kto.Text.ToString().Trim();
            tbB1_BLZ.Text = tbB1_BLZ.Text.ToString().Trim();
            tbSwift1.Text = tbSwift1.Text.ToString().Trim();
            tbIBAN1.Text = tbIBAN1.Text.ToString().Trim();

            tbB2.Text = tbB2.Text.ToString().Trim();
            tbB2_Kto.Text = tbB2_Kto.Text.ToString().Trim();
            tbB2_BLZ.Text = tbB2_BLZ.Text.ToString().Trim();
            tbSWIFT2.Text = tbSWIFT2.Text.ToString().Trim();
            tbIBAN2.Text = tbIBAN2.Text.ToString().Trim();

            tbKreditor.Text = tbKreditor.Text.ToString().Trim();
            tbDebitor.Text = tbDebitor.Text.ToString().Trim();

            tbDebitor.Text = tbDebitor.Text.ToString().Trim();

            //---------- Zusweisung der Werte
            KD.ADR_ID = ADR_ID;
            KD.KD_ID = Convert.ToInt32(tbKD_ID.Text.ToString());
            KD.ID = db_KD_ID;

            if (tbStNr.Text.ToString() != "")
            {
                KD.SteuerNr = tbStNr.Text;
            }
            else
            {
                KD.SteuerNr = "";
            }
            if (tbUStID.Text.ToString() != "")
            {
                KD.USt_ID = tbUStID.Text;
            }
            else
            {
                KD.USt_ID = "";
            }
            if (cbMwSt.Checked)
            {
                KD.MwSt = true;      //Char
                KD.MwStSatz = Convert.ToDecimal(tbMwStSatz.Text.ToString());
            }
            else
            {
                KD.MwSt = false;
                KD.MwStSatz = 0.00M;
            }
            KD.Bank1 = tbB1.Text.ToString();        // Pflichtfeld

            if (tbB1_Kto.Text.ToString() != "")
            {
                if (Functions.CheckNum(tbB1_Kto.Text.ToString()))
                {
                    KD.Kto1 = Convert.ToInt32(tbB1_Kto.Text.ToString());
                }
                else
                {
                    KD.Kto1 = 0;
                    clsMessages.ADR_FalscheEingabeKtoOrBLZ();
                }
            }
            else
            {
                KD.Kto1 = 0;
            }
            if (tbB1_BLZ.Text.ToString() != "")
            {
                if (Functions.CheckNum(tbB1_BLZ.Text.ToString()))
                {
                    KD.BLZ1 = Convert.ToInt32(tbB1_BLZ.Text.ToString());
                }
                else
                {
                    KD.BLZ1 = 0;
                    clsMessages.ADR_FalscheEingabeKtoOrBLZ();
                }
            }
            else
            {
                KD.BLZ1 = 0;
            }
            if (tbSwift1.Text.ToString() != "")
            {
                KD.Swift1 = tbSwift1.Text.ToString();
            }
            else
            {
                KD.Swift1 = "";
            }
            if (tbIBAN1.Text.ToString() != "")
            {
                KD.IBAN1 = tbIBAN1.Text.ToString();
            }
            else
            {
                KD.IBAN1 = "";
            }

            //------------ Angaben Bank 2  ---------------------
            if (tbB2.Text.ToString() != "")
            {
                KD.Bank2 = tbB2.Text.ToString();
            }

            if (tbB2_Kto.Text.ToString() != "")
            {
                if (Functions.CheckNum(tbB2_Kto.Text.ToString()))
                {
                    KD.Kto2 = Convert.ToInt32(tbB2_Kto.Text.ToString());
                }
                else
                {
                    KD.Kto2 = 0;
                    clsMessages.ADR_FalscheEingabeKtoOrBLZ();
                }
            }
            else
            {
                KD.Kto2 = 0;
            }
            if (tbB2_BLZ.Text.ToString() != "")
            {
                if (Functions.CheckNum(tbB2_BLZ.Text.ToString()))
                {
                    KD.BLZ2 = Convert.ToInt32(tbB2_BLZ.Text.ToString());
                }
                else
                {
                    KD.BLZ2 = 0;
                    clsMessages.ADR_FalscheEingabeKtoOrBLZ();
                }
            }
            else
            {
                KD.BLZ2 = 0;
            }
            if (tbSWIFT2.Text.ToString() != "")
            {
                KD.Swift2 = tbSWIFT2.Text.ToString();
            }
            else
            {
                KD.Swift2 = "";
            }
            if (tbIBAN2.Text.ToString() != "")
            {
                KD.IBAN2 = tbIBAN2.Text.ToString();
            }
            else
            {
                KD.IBAN2 = "";
            }

            //-- Debitor / Kreditor bei Heisiep raus sind versteckt visible = false
            if (tbDebitor.Text.ToString() != "")
            {
                Int32 tmp = 0;
                if (Int32.TryParse(tbDebitor.Text.ToString(), out tmp))
                {
                }
                KD.Debitor = tmp;
            }
            else
            {
                KD.Debitor = defDebobitor;
            }

            if (tbKreditor.Text.ToString() != "")
            {
                KD.Kreditor = Convert.ToInt32(tbKreditor.Text.ToString());
            }
            else
            {
                KD.Kreditor = 0;
            }



            // Check Save oder Update
            if (!update)
            {
                if (GL_User.write_Kunde)
                {
                    // --- Eintrag in DB ----
                    KD.Add();
                    clsADR.updateADRforKD(KD.KD_ID, KD.ADR_ID, GL_User.User_ID);
                }
                else
                {
                    clsMessages.User_NoAuthen();
                }
            }
            else
            {
                if (GL_User.write_Kunde)
                {
                    //---- Update Datensatz in DB ---
                    KD.updateKD();
                    clsADR.updateADRforKD(KD.KD_ID, KD.ADR_ID, GL_User.User_ID);
                    this.Close();
                }
                else
                {
                    clsMessages.User_NoAuthen();
                }
            }
        }
        //
        //--------------- SAVE / Update  -------------------------
        //
        private void btn1_Click(object sender, EventArgs e)
        {
            if (CheckMissingInput() == true)
            {
                AssignVar();
                FormClean();
                //KD Daten
                SetRecValueKD(clsKunde.ReadKDbyID(adrID));
                _ctrADRList.iAdrRange = clsADR.const_AdrRange_AdrListeKomplett;
                _ctrADRList.initList();
            }
        }
        //
        //------------- Form Clean    ---------------------
        //
        private void FormClean()
        {
            tbKD_ID.Text = "0";
            tbUStID.Text = "0";
            tbMwStSatz.Text = "";
            tbStNr.Text = "";
            tbB1.Text = "";
            tbB1_BLZ.Text = "";
            tbB1_Kto.Text = "";
            tbIBAN1.Text = "";
            tbSwift1.Text = "";
            tbB2.Text = "";
            tbB2_BLZ.Text = "";
            tbB2_Kto.Text = "";
            tbIBAN2.Text = "";
            tbSWIFT2.Text = "";
            tbDebitor.Text = "";
            tbKreditor.Text = "";
        }
        //
        //------------ Update / Übernahme in Update Kontakte --------
        //
        private void grdKontakte_DoubleClick(object sender, EventArgs e)
        {
            string KonViewID = string.Empty;

            if (adrID > 0)
            {
                //clsADR.ReadViewIDbyID(adrID);
                KonViewID = clsADR.ReadViewIDbyID(adrID);   // Suchname

                // Splitter zur Kontaktform generieren und anzeigen
                Splitter splitter = new Splitter();
                splitter.BackColor = Sped4.Properties.Settings.Default.EffectColor;
                splitter.BorderStyle = BorderStyle.None;
                splitter.Dock = DockStyle.Left;
                splitter.Name = "TempSplitterKontakte";
                this.ParentForm.Controls.Add(splitter);
                this.ParentForm.Controls.SetChildIndex(splitter, 0);
                splitter.Show();

                /****
                //if (Kontakte == null)           // Problem doppelclick Kontakte
                //{
                    if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmKontakte)) != null)
                    {
                        Functions.frm_FormTypeClose(typeof(frmKontakte));
                    }
                    //------ Kontaktform  ------
                    Kontakte = new frmKontakte();
                    Kontakte.GL_User = GL_User;
                    Kontakte.MdiParent = this.ParentForm;
                    Kontakte.Dock = DockStyle.Left;
                    Kontakte.Show();
                    Kontakte.initGrdKontakt(adrID, KonViewID);
                }
                 * ***/
            }
        }
        //
        //---------------- check Input Daten  --------------
        //
        private bool CheckMissingInput()
        {
            bool CheckOK = true;
            string strHelp = "Folgende Felder / Pflichtfelder wurden nicht ausgefüllt:\n";

            //char[] num = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            //-----  Info welche Felder fehlen  ------------------------
            //Kundennummer

            if (tbKD_ID.Text == "")
            {
                strHelp = strHelp + "Neue Kundennummer fehlt \n";
                CheckOK = false;
            }
            else
            {
                Int32 Num;
                bool isNum = Int32.TryParse(tbKD_ID.Text.ToString(), out Num);
                if (isNum)
                {
                    /*****
                    //Check, ob Kundennummer bereits vergeben
                    if(btn1.Text=="Speichern")
                    {
                      if (clsKunde.CheckKundenNummerIsUsed(Convert.ToInt32(tbKD_ID.Text)))
                      {
                        strHelp = strHelp + "Neue Kundennummer ist bereits vergeben \n";
                        CheckOK = false;
                      }
                    }
                    else
                    {
                      //Wenn die KundenID geändert wurde muss getestet werden, ob die bereits vorhanden ist           
                      Int32 KDNrNeu = Convert.ToInt32(tbKD_ID.Text);
                      if(KD_IDold!=KDNrNeu)
                      {
                        if (clsKunde.CheckKundenNummerIsUsed(KDNrNeu))
                        {
                          strHelp = strHelp + "Neue Kundennummer ist bereits vergeben \n";
                          CheckOK = false;
                        }
                      }                   
                    }
                    /******
                    //USTt
                     if (tbUStID.Text == "")
                     {
                         strHelp = strHelp + "USt.-ID \n";
                         CheckOK=false;
                     }
                     //Steuernummer
                     if (tbStNr.Text == "")
                     {
                         strHelp = strHelp + "Steuernummer \n";
                         CheckOK=false;
                     }
                     //Bank1
                     if (tbB1.Text == "")
                     {
                         strHelp = strHelp + "Bankname fehlt (kein Pflichtfeld)  \n";
                         //CheckOK=false;
                     }
                     //Konto Bank1
                     if (tbB1_Kto.Text=="")
                     {
                       if (tbB1_Kto.ToString().IndexOfAny(num) != -1) 
                       {
                         strHelp = strHelp + "Kontonummer darf nur aus Ziffern bestehen \n";
                         CheckOK = false;
                       }    
                     }
                     else
                     {
                       strHelp = strHelp + "Kontonummer fehlt (kein Pflichtfeld) \n";
                     }
                     //BLZ Bank1
                     if (tbB1_BLZ.Text != "")
                     {
                       if (tbB1_BLZ.ToString().IndexOfAny(num) != -1)
                       {
                         strHelp = strHelp + "BLZ darf nur aus Ziffern bestehen \n";
                         //CheckOK = false;
                       }
                     }
                     else
                     {
                       strHelp = strHelp + "BLZ fehlt (kein Pflichtfeld) \n";
                     }
                     //Swift
                     if (tbSwift1.Text == "")
                     {
                        strHelp = strHelp + "SWIFT-Angaben fehlen (kein Pflichtfeld) \n";
                        //CheckOK = false;
                     }
                     //IBAN
                     if (tbSwift1.Text == "")
                     {
                       strHelp = strHelp + "SWIFT-Angaben fehlen (kein Pflichtfeld) \n";
                       //CheckOK = false;
                     }
                      * *****/
                }
                else
                {
                    CheckOK = false;
                    strHelp = strHelp + "Kundennummer darf nur aus Ziffern bestehen \n";
                }
            }

            if (!CheckOK)
            {
                MessageBox.Show(strHelp);
            }
            return CheckOK;
        }
        //
        //
        //
        private void SetInfoImageForKDID()
        {
            if (Functions.CheckNum(tbKD_ID.Text))
            {
                decimal KDNr = Convert.ToDecimal(tbKD_ID.Text);
                if (clsKunde.CheckKundenNummerIsUsed(KDNr) | (Convert.ToInt32(tbKD_ID.Text) == 0))
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
        //--------------- Check KD ID Button --------------
        //
        private void btnKDNrCheck_Click(object sender, EventArgs e)
        {
            SetInfoImageForKDID();
        }
        //
        //ä-------------- speichern --------------
        //
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            if (CheckMissingInput() == true)
            {
                AssignVar();
                FormClean();
                //KD Daten
                SetRecValueKD(clsKunde.ReadKDbyID(adrID));
                _ctrADRList.SetOldSearchList();
                //_ctrADRList.initList(true);
            }
        }
        //
        //----------------- Form schliessen -----------------
        //
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
