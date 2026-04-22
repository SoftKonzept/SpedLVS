using LVS;
using LVS.Dokumente;
using Sped4.Settings;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrPrint : UserControl
    {
        public frmPrintCenter _frmPrintCenter;
        public Int32 SearchButton = 0;

        public Globals._GL_USER GL_User;
        public decimal _AuftragID = 0;
        public decimal _AuftragPos = 0;
        public decimal _ArtikelTableID = 0;
        public decimal _AuftragPosTableID = 0;
        internal decimal _MandantenID = 0;
        internal string DocArt = string.Empty;
        internal string DocName = string.Empty;
        internal DataTable dtPrintdaten = new DataTable();
        public DataTable dtArtikelDetails = new DataTable("Artikeldetails");
        internal bool doPrint = false;
        internal bool boNeutralitaet = false;

        internal string strZM = string.Empty;
        internal string strA = string.Empty;
        internal string strFaher = string.Empty;
        internal string Notiz = string.Empty;

        public decimal decTmpVADR = 0;
        public decimal decTmpEADR = 0;

        public bool boCreatOwnDoc = false;
        public ctrArtDetails ctrArtD;
        internal bool boPrintDirect = false;

        public ctrPrint(decimal myArtikelTableID)
        {
            InitializeComponent();
        }
        //
        private void ctrPrint_Load(object sender, EventArgs e)
        {
            _AuftragPosTableID = this._frmPrintCenter._AuftragPosTableID;
            _AuftragID = this._frmPrintCenter._AuftragID;
            _AuftragPos = this._frmPrintCenter._AuftragPos;

            GetArtikelDaten();
            InitTableArtikelDetails();
            //DataTable Printdaten erstellen 
            dtPrintdaten = ctrPrintSettings.InitTablePrintDaten();
            //InitColTable();
            SetDefaulValueToFrm();
            SetFirstLoadFrm();
        }
        //
        private void GetArtikelDaten()
        {
            //Auftragnummer und AuftragPosNummer ermitteln
            //DataTable dt = clsArtikel.GetAllArtikeldateDispoByID(this.GL_User,_ArtikelTableID);
            DataTable dt = clsArtikel.GetAllArtikeldateDispoByAuftragPosTableID(this.GL_User, _AuftragPosTableID);
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    //hier können die Artikeldaten übernommen werden
                    _AuftragID = (decimal)dt.Rows[i]["AuftragID"];
                    _AuftragPos = (decimal)dt.Rows[i]["AuftragPos"];
                    _MandantenID = (decimal)dt.Rows[i]["MandantenID"];
                }
            }
        }
        //
        private void SetDefaulValueToFrm()
        {
            //Setz die vorgegebenen Standardwerte
            dtp_PrintDate.Value = DateTime.Today.Date;
            nudPMA.Value = 1;
            nudPMLfs.Value = 2;
            decTmpVADR = 0;
            decTmpEADR = 0;

            tbVersender.Text = string.Empty;
            tbSearchV.Text = string.Empty;
            tbEmpfaenger.Text = string.Empty;
            tbSearchE.Text = string.Empty;
            tbDocName.Text = string.Empty;
            tbZM.Text = string.Empty;
            tbAuflieger.Text = string.Empty;
            tbFahrer.Text = string.Empty;
        }
        //
        private void SetFirstLoadFrm()
        {
            cbLfSchein.Checked = true;
        }

        /********************************************************************
         *              Checkboxen
         * ******************************************************************/

        private void cbAnmeldeschein_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFremdLfs.Checked)
            {
                cbLfSchein.Checked = false;
                cbNeutrLfSchein.Checked = false;
                cbAbholschein.Checked = false;
                //cbAnmeldeschein.Checked = false;
                cbOwnDoc.Checked = false;
                cbLfsAbholschein.Checked = false;
                cbNeutrLfsAbholschein.Checked = false;
                DocArt = "Lieferschein";
                DocName = "Lieferschein";
            }
            else
            {
                DocArt = string.Empty;
                DocName = string.Empty;
            }
            ChangeValueOnFrm();
        }
        //
        private void cbAbholschein_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAbholschein.Checked)
            {
                cbLfSchein.Checked = false;
                cbNeutrLfSchein.Checked = false;
                //cbAbholschein.Checked = false;
                cbFremdLfs.Checked = false;
                cbOwnDoc.Checked = false;
                cbNeutrLfsAbholschein.Checked = false;
                cbLfsAbholschein.Checked = false;
                DocArt = "Abholschein";
                DocName = "Abholschein";
            }
            else
            {
                DocArt = string.Empty;
                DocName = string.Empty;
            }
            ChangeValueOnFrm();
        }
        //
        //---------- ADR übernahme -----------------
        //
        public void TakeOverADRID(decimal decADR)
        {
            if (SearchButton == 2)
            {
                SetADRToFrm(decADR, true);
            }
            if (SearchButton == 3)
            {
                SetADRToFrm(decADR, false);
            }
        }
        //
        private void cbNeutrLfSchein_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNeutrLfSchein.Checked)
            {
                cbLfSchein.Checked = false;
                //cbNeutrLfSchein.Checked = false;
                cbAbholschein.Checked = false;
                cbFremdLfs.Checked = false;
                cbOwnDoc.Checked = false;
                cbLfsAbholschein.Checked = false;
                cbNeutrLfsAbholschein.Checked = false;

                ctrArtD.cbNeutralitaet.Checked = true;
                DocArt = "Lieferschein";
                DocName = "Lieferschein";
                boNeutralitaet = true;
            }
            else
            {
                DocArt = string.Empty;
                DocName = string.Empty;
                ctrArtD.cbNeutralitaet.Checked = false;
                boNeutralitaet = false;
            }
            //tbDocName.Text = DocName;
            ctrArtD.SetNeutraleADRtoFrm();
            ChangeValueOnFrm();
        }
        //
        private void cbLfSchein_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLfSchein.Checked)
            {
                //cbLfSchein.Checked = false;
                cbNeutrLfSchein.Checked = false;
                cbAbholschein.Checked = false;
                cbFremdLfs.Checked = false;
                cbOwnDoc.Checked = false;
                cbNeutrLfsAbholschein.Checked = false;
                cbLfsAbholschein.Checked = false;
            }
            else
            {
                DocArt = string.Empty;
                DocName = string.Empty;
            }

            ChangeValueOnFrm();
        }
        //
        private void cbLfsAbholschein_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLfsAbholschein.Checked)
            {
                cbLfSchein.Checked = false;
                cbNeutrLfSchein.Checked = false;
                cbAbholschein.Checked = false;
                cbFremdLfs.Checked = false;
                cbOwnDoc.Checked = false;
                cbNeutrLfsAbholschein.Checked = false;
                //cbLfsAbholschein.checked = false;
            }
            else
            {
                DocArt = string.Empty;
                DocName = string.Empty;
            }
            ChangeValueOnFrm();
        }
        //
        private void cbNeutrLfsAbholschein_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNeutrLfsAbholschein.Checked)
            {
                cbLfSchein.Checked = false;
                cbNeutrLfSchein.Checked = false;
                cbAbholschein.Checked = false;
                cbFremdLfs.Checked = false;
                cbOwnDoc.Checked = false;
                //cbNeutrLfsAbholschein.Checked = false;
                cbLfsAbholschein.Checked = false;
            }
            else
            {
                DocArt = string.Empty;
                DocName = string.Empty;
            }
            ChangeValueOnFrm();

            ctrArtD.SetNeutraleADRtoFrm();
        }
        //
        private void cbOwnDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOwnDoc.Checked)
            {
                cbLfSchein.Checked = false;
                cbNeutrLfSchein.Checked = false;
                cbAbholschein.Checked = false;
                cbFremdLfs.Checked = false;
                //cbOwnDoc.Checked = false;
                cbNeutrLfsAbholschein.Checked = false;
                cbLfsAbholschein.Checked = false;
                DocArt = "OwnDoc";
                boCreatOwnDoc = true;
            }
            else
            {
                tbNeutrDocName.Text = string.Empty;
                boCreatOwnDoc = false;
                DocArt = string.Empty;
                DocName = string.Empty;
            }
            ChangeValueOnFrm();
            ctrArtD.boCreatOwnDoc = boCreatOwnDoc;
            ctrArtD.InitEigDoc(boCreatOwnDoc);
        }
        /*****************************************************************************
         *                              Print start
         * **************************************************************************/
        //
        //------------- Druck erstellen und starten --------------
        //
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            boPrintDirect = false;
            DoPrint(boPrintDirect);
        }
        //
        private void tsbtnDirectPrint_Click(object sender, EventArgs e)
        {
            boPrintDirect = true;
            DoPrint(boPrintDirect);
        }
        //
        private void DoPrint(bool bo_PrintDirekt)
        {
            //Druck fremder Lieferscheine
            if (cbFremdLfs.Checked)
            {
                if ((ctrArtD.do_ChangesArtikel == true) |
                    (ctrArtD.do_ChangesAuftragdetails == true))
                {
                    if (ctrArtD.do_ChangesArtikel)
                    {
                        clsMessages.PrintCenter_ChangedArtikelDaten();
                    }
                    if (ctrArtD.do_ChangesAuftragdetails)
                    {
                        clsMessages.PrintCenter_ChangedAuftragsDaten();
                    }
                }
                else
                {
                    AssignValueToTable();
                    OpenFrmFDocs();
                }
            }
            else
            {
                if ((ctrArtD.do_ChangesArtikel == true) |
                    (ctrArtD.do_ChangesAuftragdetails == true))
                {
                    if (ctrArtD.do_ChangesArtikel)
                    {
                        clsMessages.PrintCenter_ChangedArtikelDaten();
                    }
                    if (ctrArtD.do_ChangesAuftragdetails)
                    {
                        clsMessages.PrintCenter_ChangedAuftragsDaten();
                    }
                }
                else
                {
                    /********************************
                     * Table erstellen mit
                     * -Rescourcen
                     * -Artikel
                     * -Auftragsdaten
                     * *****************************/

                    //neutraler Lieferschein - Check ob neutrale ADR gesetzt
                    if ((cbNeutrLfSchein.Checked) |
                        (cbOwnDoc.Checked))
                    {
                        if ((ctrArtD.ADR_ID_nE == 0) & (ctrArtD.ADR_ID_nV == 0))
                        {
                            doPrint = false; ;
                            clsMessages.Print_NeutrADR_NotSelected();
                        }
                        else
                        {
                            doPrint = true;
                        }
                    }
                    else
                    {
                        doPrint = true;
                    }
                    if (doPrint)
                    {
                        // Check ob eine Dokumentenart ausgewählt wurde
                        if ((!cbLfSchein.Checked) &
                            (!cbNeutrLfSchein.Checked) &
                            (!cbAbholschein.Checked) &
                            (!cbFremdLfs.Checked) &
                            (!cbNeutrLfsAbholschein.Checked) &
                            (!cbLfsAbholschein.Checked) &
                            (!cbOwnDoc.Checked)
                            )
                        {
                            doPrint = false;
                            clsMessages.Print_NoDocArtSelected();
                        }
                        else
                        {
                            doPrint = true;
                        }

                        if (doPrint)
                        {
                            //Lieferscheine
                            if ((cbLfSchein.Checked) |
                                (cbAbholschein.Checked) |       //ABholschein
                                (cbNeutrLfSchein.Checked) |     //neutraler Lieferschein
                                (cbOwnDoc.Checked)             //Eigene Dokumente
                                )
                            {
                                PrintDoc(boPrintDirect);
                            }
                            //neutraler Lieferschein und Abholschein
                            if (cbNeutrLfsAbholschein.Checked)
                            {
                                if ((ctrArtD.ADR_ID_nE == 0) & (ctrArtD.ADR_ID_nV == 0))
                                {
                                    clsMessages.Print_NeutrADR_NotSelected();
                                }
                                else
                                {
                                    boPrintDirect = true;
                                    PrintDoc(boPrintDirect);

                                    AssignValueToTable();
                                    boNeutralitaet = true;
                                    PrintDoc(boPrintDirect);
                                }
                            }
                            //Lieferschein und Abholschein
                            if (cbLfsAbholschein.Checked)
                            {
                                boPrintDirect = true;
                                cbAbholschein.Checked = true;
                                PrintDoc(boPrintDirect);


                                cbAbholschein.Checked = true;
                                cbLfSchein.Checked = true;
                                boNeutralitaet = false;
                                PrintDoc(boPrintDirect);
                            }
                        }
                    }
                }
            }
        }
        //
        private void OpenFrmFDocs()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmFDocs)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmFDocs));
            }
            frmFDocs fDocs = new frmFDocs();
            fDocs.GL_User = GL_User;
            fDocs.boDirectPrint = boPrintDirect;
            fDocs.PrintAnzahl = (Int32)nudPMLfs.Value;
            //dt.TableName = "Auftragsdaten";
            fDocs.dtPrintdaten = dtPrintdaten;
            fDocs.dtArtikelDetails = dtArtikelDetails;
            fDocs.StartPosition = FormStartPosition.CenterParent;
            fDocs.Show();
            fDocs.BringToFront();
        }
        //
        private void PrintDoc(bool bo_PrintDirekt)
        {
            SetPrintdaten();
            AssignValueToTable();

            Int32 iAnzahl = 0;
            if (DocArt == "Abholschein")
            {
                iAnzahl = (Int32)nudPMA.Value;
            }
            else
            {
                iAnzahl = (Int32)nudPMLfs.Value;
            }

            for (Int32 i = 1; i < iAnzahl + 1; i++)
            {
                OpenReportView(bo_PrintDirekt);

                //Update Papiere erstellt, wenn Auftrag schon in Kommission
                if (clsKommission.IsAuftragPositionIn(this.GL_User, _AuftragPosTableID))
                {
                    clsKommission.UpdateDocsByAuftragPosTableID(this.GL_User, true, _AuftragPosTableID);
                }
                //Refresh
                if (_frmPrintCenter._AFKalenderItemKommi != null)
                {
                    _frmPrintCenter._AFKalenderItemKommi.myKalenderItemKommi_Refresh();
                }
            }
            dtPrintdaten.Clear();
            dtArtikelDetails.Clear();
            InitTableArtikelDetails();
            InitDGV();
        }
        //
        //
        //
        private void OpenReportView(bool bo_direktPrint)
        {
            DataSet ds = new DataSet(); //Leer
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmReportViewer)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmReportViewer));
            }
            frmReportViewer reportview = new frmReportViewer(ds, DocArt);
            reportview.GL_User = GL_User;
            reportview._ArtikelTableID = this._ArtikelTableID;
            reportview._AuftragID = this._AuftragID;
            reportview._AuftragPosTableID = this._AuftragPosTableID;
            reportview._MandantenID = _MandantenID;
            reportview.dtPrintdaten = dtPrintdaten;
            reportview.dtArtikelDetails = dtArtikelDetails;
            reportview.neutralerLfs = boNeutralitaet;

            if (bo_direktPrint)
            {
                reportview.Hide();
                reportview.PrintDirect();
                reportview.Close();
            }
            else
            {
                reportview.StartPosition = FormStartPosition.CenterParent;
                reportview.Show();
                reportview.BringToFront();
            }

        }
        //
        private void AssignValueToTable()
        {
            DataRow row = dtPrintdaten.NewRow();
            row["AuftragID"] = _AuftragID;
            row["AuftragPos"] = _AuftragPos;
            row["ZM"] = tbZM.Text;
            row["Auflieger"] = tbAuflieger.Text;
            row["Notiz"] = ctrArtD.Notiz;
            row["DocName"] = tbDocName.Text.Trim();
            row["Ladenummer"] = ctrArtD.strLadenummer;
            row["ZF"] = ctrArtD.strZF;
            row["Fahrer"] = tbFahrer.Text.Trim();
            row["ADR_ID_V"] = decTmpVADR;
            row["ADR_ID_E"] = decTmpEADR;
            row["Date"] = dtp_PrintDate.Value;
            row["DocArt"] = DocArt;
            row["PrintNotiz"] = cbNotizPrint.Checked;
            row["AuftragPosTableID"] = _AuftragPosTableID;
            dtPrintdaten.Rows.Add(row);
        }
        //
        //
        //
        private void InitDGV()
        {
            dtArtikelDetails.DefaultView.RowFilter = "Standard=false";

            dgv.DataSource = dtArtikelDetails.DefaultView;
            dgv.Columns["Spalte"].Visible = false;
            dgv.Columns["Text"].DisplayIndex = 0;
            dgv.Columns["Standard"].Visible = false;
        }
        //
        //------- Spalten auswahl -------------
        //
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                string strCol = dgv.Rows[dgv.CurrentCell.RowIndex].Cells["Text"].Value.ToString();
                bool boSelected = (bool)dgv.Rows[dgv.CurrentCell.RowIndex].Cells["Selected"].Value;

                for (Int32 i = 0; i <= dtArtikelDetails.Rows.Count - 1; i++)
                {
                    string strCoDT = dtArtikelDetails.Rows[i]["Text"].ToString();
                    if (strCoDT == strCol)
                    {
                        if (boSelected)
                        {
                            dtArtikelDetails.Rows[i]["Selected"] = false;
                        }
                        else
                        {
                            dtArtikelDetails.Rows[i]["Selected"] = true;
                        }
                        i = dtArtikelDetails.Rows.Count;
                    }
                }
            }
        }
        //
        //
        //
        private void tbNeutrDocName_TextChanged(object sender, EventArgs e)
        {
            tbDocName.Text = tbNeutrDocName.Text;
        }
        //
        private void SetPrintdaten()
        {
            //SetDefaulValueToFrm();
            //Setz die neuen Werte, wenn ein neue Dokument gewählt wird
            //Statusabfrage zur Recourcenermittlung
            if (cbLfSchein.Checked)
            {
                /********************
                 * Lieferschein
                 ********************/
                btnVersender.Text = "Versender";
                btnEmfaenger.Text = "Empfänger";
                DocName = "Lieferschein";
                DocArt = "Lieferschein";

            }
            if (cbNeutrLfSchein.Checked)
            {
                /********************
                 * neutraler Lieferschein
                 *********************/
                btnVersender.Text = "Versender";
                btnEmfaenger.Text = "Empfänger";

                //Dokumentendaten
                DocName = "Lieferschein";
                DocArt = "Lieferschein";
            }
            if (cbAbholschein.Checked)
            {
                /********************
                 * Abholschein
                  ************************/
                btnVersender.Text = "VON:";
                btnEmfaenger.Text = "FÜR:";

                //Dokumentendaten
                DocName = "Abholschein";
                DocArt = "Abholschein";
            }
            if (cbOwnDoc.Checked)
            {
                /********************
                 * Owndoc
                 *****************************/
                btnVersender.Text = "Versender";
                btnEmfaenger.Text = "Empfänger";
                //Dokumentenart
                DocArt = "OwnDoc";
                DocName = tbDocName.Text.Trim();
                boCreatOwnDoc = true;
            }

            SetADRToFrm(decTmpVADR, true);

            //Empfänger
            SetADRToFrm(decTmpEADR, false);

            tbDocName.Text = DocName;
            //DRuckdatum
            dtp_PrintDate.Value = DateTime.Today.Date;
            GetRessourcenForDoc();
        }
        //
        //------------- Get and Set Ressourcen ----------------
        //
        private void GetRessourcenForDoc()
        {
            //Baustelle 
            Int32 iStatus = clsAuftragPos.GetStatusByAuftragPosTableID(this.GL_User, _AuftragPosTableID);
            if (iStatus > 3)
            {
                //decimal apID = clsAuftragPos.GetIDbyAuftragAndAuftragPos(this.GL_User,_AuftragID, _AuftragPos, _MandantenID, this.GL_User.sys_ArbeitsbereichID);

                string Name = clsTour.GetNameByAP_ID(this.GL_User, _AuftragPosTableID);
                string VName = clsTour.GetVorNameByAP_ID(this.GL_User, _AuftragPosTableID);
                if (Name != string.Empty)
                {
                    strFaher = Name + "," + VName;
                }

                strA = clsTour.GetA_KFZByAP_ID(this.GL_User, _AuftragPosTableID);
                strZM = clsTour.GetZM_KFZByAP_ID(this.GL_User, _AuftragPosTableID);
                tbAuflieger.Text = strA;
                tbZM.Text = strZM;
                tbFahrer.Text = strFaher;
            }
        }
        //
        private void ChangeValueOnFrm()
        {
            //SetDefaulValueToFrm();
            //Setz die neuen Werte, wenn ein neue Dokument gewählt wird
            if (cbLfSchein.Checked)
            {
                /********************
                 * Lieferschein
                 ********************/
                decTmpVADR = ctrArtD.ADR_ID_V;
                decTmpEADR = ctrArtD.ADR_ID_E;
            }
            if (cbNeutrLfSchein.Checked)
            {
                /********************
                 * neutraler Lieferschein
                 *********************/
                decTmpVADR = ctrArtD.ADR_ID_nV;
                decTmpEADR = ctrArtD.ADR_ID_nE;
            }
            if (cbAbholschein.Checked)
            {
                /********************
                 * Abholschein
                  ************************/
                decTmpVADR = ctrArtD.ADR_ID_V;
                decTmpEADR = ctrArtD.ADR_ID_A;
            }
            SetPrintdaten();
            ReSetDataTableArtikelDetails(cbFremdLfs.Checked);
        }
        //
        //
        //
        private void InitTableArtikelDetails()
        {
            //DataTable ArtikelColumns
            ctrPrintSettings.InitDataTableArtikelDBColumns(ref dtArtikelDetails);
            ctrPrintSettings.SetArtikelColumnStandard(ref dtArtikelDetails);
            //SetArtikelColumnStandard();
            InitDGV();
            dgv.Refresh();
        }
        //
        //------------- Setz die zu druckenden ADR für Versender und Empfänger ------------------
        //
        public void SetADRToFrm(decimal decADR, bool Versender)
        {
            if (Versender)
            {
                decTmpVADR = decADR;
                tbVersender.Text = clsADR.GetADRString(decTmpVADR);
                tbSearchV.Text = clsADR.GetMatchCodeByID(decTmpVADR, GL_User.User_ID);
                tbSearchV.Text = tbSearchV.Text.Trim();
            }
            else
            {
                decTmpEADR = decADR;
                tbEmpfaenger.Text = clsADR.GetADRString(decTmpEADR);
                tbSearchE.Text = clsADR.GetMatchCodeByID(decTmpEADR, GL_User.User_ID);
                tbSearchE.Text = tbSearchE.Text.Trim();
            }
        }
        //
        //------------ AdressForm für die Adresssuche wird geöffnet ------------------
        //
        private void OpenADRAuswahl()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADRSearch)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmADRSearch));
            }
            frmADRSearch ADR = new frmADRSearch();
            ADR.ctrPrint = this;
            ADR.StartPosition = FormStartPosition.CenterScreen;
            ADR.Dock = DockStyle.Fill;
            ADR.Show();
            ADR.BringToFront();
        }
        //
        //-------------- ADR Search Versender --------------
        //
        private void btnVersender_Click(object sender, EventArgs e)
        {
            SearchButton = 2;
            OpenADRAuswahl();
        }
        //
        //------------ ADR Search Empfänger ----------------
        //
        private void btnEmfaenger_Click(object sender, EventArgs e)
        {
            SearchButton = 3;
            OpenADRAuswahl();
        }
        //
        //
        private void tbSearchV_TextChanged(object sender, EventArgs e)
        {
            string SearchText = tbSearchV.Text.ToString();
            if (ctrArtD.VTable.Rows.Count < 1)
            {
                ctrArtD.initVersender();
            }
            tbVersender.Text = Functions.GetADRTableSearchResult(ctrArtD.VTable, SearchText);
        }
        //
        //-------- Edit beendet wird die ADR ID gesetzt -----------
        //
        private void tbSearchV_Validated(object sender, EventArgs e)
        {
            decimal iADR = clsADR.GetIDByMatchcode(tbSearchV.Text);
            decTmpVADR = iADR;
        }
        //
        //
        //
        private void tbSearchE_TextChanged(object sender, EventArgs e)
        {
            string SearchText = tbSearchE.Text.ToString();
            if (ctrArtD.ETable.Rows.Count < 1)
            {
                ctrArtD.initEmpfaenger();
            }
            tbEmpfaenger.Text = Functions.GetADRTableSearchResult(ctrArtD.ETable, SearchText);
        }
        //
        //
        //
        private void tbSearchE_Validated(object sender, EventArgs e)
        {
            decimal iADR = clsADR.GetIDByMatchcode(tbSearchE.Text);
            decTmpEADR = iADR;
        }
        //
        //-------- die Standardwerte werden entsprechend dem Kunden neue gesetzt ---------
        //
        private void ReSetDataTableArtikelDetails(bool FremdDocs)
        {
            dgv.DataSource = null;
            if (FremdDocs)
            {
                fDocs_Holzrichter.SetArtikelColumnHolzrichter(ref dtArtikelDetails);
            }
            else
            {
                ctrPrintSettings.SetArtikelColumnStandard(ref dtArtikelDetails);
            }
            InitDGV();
        }



    }
}
