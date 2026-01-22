using LVS;
using Sped4.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrAuftragDetails : UserControl
    {
        public Globals._GL_USER GL_User;

        clsFrachtvergabe fv;
        public DataSet dsTransportauftrag;
        public decimal _AuftragID;
        public decimal _AuftragPos;
        public decimal _KundeID = 0;
        public decimal _MandantenID = 0;
        public decimal _AuftragPosTableID;
        public DateTime DefaultDateTime = Convert.ToDateTime("01.01.1900 00:00:00");
        public bool SUisIn = false;

        public ctrAuftragDetails()
        {
            InitializeComponent();
        }
        //
        //-------------- LOAD  -------------------------
        //
        private void ctrAuftragDetails_Load(object sender, EventArgs e)
        {
            initForm();
            nudHBZ.Enabled = false;
            nudHLT.Enabled = false;
            nudMinBZ.Enabled = false;
            nudMinLT.Enabled = false;

            fv.BenutzerID = GL_User.User_ID;
        }
        //
        //
        //
        private void initForm()
        {
            clsFrachtvergabe _fv = new clsFrachtvergabe();
            fv = _fv;
            fv.BenutzerID = GL_User.User_ID;
            fv.AuftragID = _AuftragID;
            fv.AuftragPos = _AuftragPos;
            fv.ID_AP = _AuftragPosTableID;
            fv.GetAuftragsDaten();
            GetAuftragsdaten();
            dsTransportauftrag = fv.dsTransportauftrag;

        }
        //
        //------ Auftragsdaten  ----------
        //
        private void GetAuftragsdaten()
        {
            DataSet ds = clsAuftragPos.ReadDataByID(_AuftragID, _AuftragPos);

            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //Ausgabe der Auftragsdaten
                tbANr.Text = ds.Tables[0].Rows[i]["ANr"].ToString() + " / " + ds.Tables[0].Rows[i]["AuftragPos"].ToString();
                tbADatum.Text = DateTime.Today.Date.ToShortDateString();
                tbLadeNr.Text = ds.Tables[0].Rows[i]["Ladenummer"].ToString();

                tbVSB.Text = ((DateTime)ds.Tables[0].Rows[i]["VSB"]).ToShortDateString();
                dtpLT.Value = (DateTime)ds.Tables[0].Rows[i]["Liefertermin"];
                dtpBT.Value = DateTime.Today;

                //BeladeZF
                DateTime ZF = Convert.ToDateTime(ds.Tables[0].Rows[i]["ZF"].ToString());
                nudHBZ.Value = ZF.Hour;
                nudMinBZ.Value = ZF.Minute;

                //Auftraggeber
                GetAuftraggeber((decimal)ds.Tables[0].Rows[i]["KD_ID"]);
                //Versender
                GetVersender((decimal)ds.Tables[0].Rows[i]["B_ID"]);
                //Empfänger
                GetEmpfaenger((decimal)ds.Tables[0].Rows[i]["E_ID"]);


                tbADatum.Text = ((DateTime)ds.Tables[0].Rows[i]["ADate"]).ToShortDateString();

                //Artikeldaten
                GetArtikelDaten();
            }
        }
        //
        private void GetAuftraggeber(decimal adrID)
        {
            string strAG = string.Empty;
            DataSet ds = clsADR.ReadADRbyID(adrID);

            fv.InsertAuftraggebertoDataSet(ds);

            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strAG = ds.Tables[0].Rows[i]["FBez"].ToString() + "\r\n";
                strAG = strAG + ds.Tables[0].Rows[i]["Name1"].ToString() + "\r\n";
                strAG = strAG + ds.Tables[0].Rows[i]["Name2"].ToString() + "\r\n";
                strAG = strAG + ds.Tables[0].Rows[i]["Name3"].ToString() + "\r\n";
                strAG = strAG + ds.Tables[0].Rows[i]["Str"].ToString() + "\r\n";
                strAG = strAG + ds.Tables[0].Rows[i]["PLZ"].ToString() + " - " + ds.Tables[0].Rows[i]["Ort"].ToString() + "\r\n";
            }
            tbAuftraggeber.Text = strAG;
        }
        //
        //---------- Auftraggeber  --------------
        //
        private void GetVersender(decimal adrID)
        {
            string strVer = string.Empty;
            DataSet ds = clsADR.ReadADRbyID(adrID);
            fv.InsertVersendertoDataSet(ds);

            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strVer = ds.Tables[0].Rows[i]["FBez"].ToString() + "\r\n";
                strVer = strVer + ds.Tables[0].Rows[i]["Name1"].ToString() + "\r\n";
                strVer = strVer + ds.Tables[0].Rows[i]["Name2"].ToString() + "\r\n";
                strVer = strVer + ds.Tables[0].Rows[i]["Name3"].ToString() + "\r\n";
                strVer = strVer + ds.Tables[0].Rows[i]["Str"].ToString() + "\r\n";
                strVer = strVer + ds.Tables[0].Rows[i]["PLZ"].ToString() + " - " + ds.Tables[0].Rows[i]["Ort"].ToString() + "\r\n";
            }
            tbVersender.Text = strVer;
        }
        //
        //---------- Empfänger ---------------
        //
        private void GetEmpfaenger(decimal adrID)
        {
            string strE = string.Empty;
            DataSet ds = clsADR.ReadADRbyID(adrID);

            fv.InsertEmpfaengertoDataSet(ds);

            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strE = ds.Tables[0].Rows[i]["FBez"].ToString() + "\r\n";
                strE = strE + ds.Tables[0].Rows[i]["Name1"].ToString() + "\r\n";
                strE = strE + ds.Tables[0].Rows[i]["Name2"].ToString() + "\r\n";
                strE = strE + ds.Tables[0].Rows[i]["Name3"].ToString() + "\r\n";
                strE = strE + ds.Tables[0].Rows[i]["Str"].ToString() + "\r\n";
                strE = strE + ds.Tables[0].Rows[i]["PLZ"].ToString() + " - " + ds.Tables[0].Rows[i]["Ort"].ToString() + "\r\n";
            }
            tbEmpfaenger.Text = strE;
        }
        //
        //------- Subunternehmer   ---------------
        //
        public void SetSubunternehmer(decimal ADR_ID)
        {
            _KundeID = ADR_ID;
            fv.KundeID = _KundeID;

            string strE = string.Empty;
            DataSet ds = clsADR.ReadADRbyID(ADR_ID);

            fv.InsertSUtoDataSet(ds);

            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strE = ds.Tables[0].Rows[i]["FBez"].ToString() + "\r\n";
                strE = strE + ds.Tables[0].Rows[i]["Name1"].ToString() + "\r\n";
                strE = strE + ds.Tables[0].Rows[i]["Name2"].ToString() + "\r\n";
                strE = strE + ds.Tables[0].Rows[i]["Name3"].ToString() + "\r\n";
                strE = strE + ds.Tables[0].Rows[i]["Str"].ToString() + "\r\n";
                strE = strE + ds.Tables[0].Rows[i]["PLZ"].ToString() + " - " + ds.Tables[0].Rows[i]["Ort"].ToString() + "\r\n";
            }
            tbSU.Text = strE;
            SUisIn = true;
        }
        //
        //-------------- Datagrid Artikeldaten
        //
        private void GetArtikelDaten()
        {
            DataTable dt = clsArtikel.GetDataTableForArtikelGrd(this.GL_User, _AuftragPosTableID);
            CheckForGewicht(ref dt);

            fv.InsertArtikeltoDataSet(dt);
            afGrid1.DataSource = dt;
            afGrid1.Columns["ID"].Visible = false;
            afGrid1.Columns["gemGewicht"].Visible = false;
            afGrid1.Columns["AuftragPosTableID"].Visible = false;
            afGrid1.Columns["Brutto"].HeaderCell.Value = "Gewicht";

        }
        //
        //
        //
        private void CheckForGewicht(ref DataTable dt)
        {
            //wenn tatGewicht = 0 dann gemGewicht
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decNetto = (decimal)dt.Rows[i]["Netto"];
                decimal decBrutto = (decimal)dt.Rows[i]["Brutto"];

                if (decNetto == 0)
                {
                    dt.Rows[i]["Netto"] = decBrutto;
                    if (decBrutto > 0)
                    {
                        decimal artikelID = (decimal)dt.Rows[i]["ID"];
                        clsArtikel.UpdateArtikelBrutto(this.GL_User, artikelID, decBrutto);
                    }
                }
            }
        }
        //
        //---------- Datum Check - Datum darf nicht in der Vergangenheit liegen ----
        //
        private void dtpBeladeTermin_ValueChanged(object sender, EventArgs e)
        {
            if (dtpBT.Value < DateTime.Today)
            {
                dtpBT.Value = DateTime.Today.Date;
                clsMessages.DateCheck_DateToBeInPast();
            }
        }
        //
        //------------ ZF Beladetermin -------------------
        //
        private void cbBZF_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBZF.Checked == true)
            {
                nudHBZ.Enabled = true;
                nudMinBZ.Enabled = true;
            }
            else
            {
                nudHBZ.Enabled = false;
                nudMinBZ.Enabled = false;
            }
        }
        //
        //----------- ZF Liefertermin --------------------
        //
        private void cbLTZF_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLTZF.Checked == true)
            {
                nudHLT.Enabled = true;
                nudMinLT.Enabled = true;
            }
            else
            {
                nudHLT.Enabled = false;
                nudHLT.Enabled = false;
            }
        }
        //
        //------- Fracht nach Vereinbarung -----------------
        //
        private void cbFrachtVB_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFrachtVB.Checked == true)
            {
                tbFracht.Text = "VB";
            }
            else
            {
                tbFracht.Text = string.Empty;
            }
        }
        //
        //
        //
        public bool CheckValue()
        {
            bool retValue = true;

            if (dtpLT.Value < DateTime.Now.Date)
            {
                retValue = false;
            }

            return retValue;
        }
        //
        //--------- Assign Value
        //
        public void AssignValue()
        {
            if (_KundeID > 0)
            {
                fv.zHd = tbzHd.Text;
                //fv.ID_AP = clsAuftragPos.GetIDbyAuftragAndAuftragPos(this.GL_User, AuftragID, AuftragPos, MandantenID, this.GL_User.sys_ArbeitsbereichID);
                fv.ID_AP = _AuftragPosTableID;
                fv.B_Date = dtpBT.Value;
                fv.E_Date = dtpLT.Value;
                fv.Ladenummer = tbLadeNr.Text;

                if (cbBZF.Checked == true)
                {
                    fv.B_Time = Functions.GetStrTimeZF(nudHBZ, nudMinBZ);
                }
                else
                {
                    fv.B_Time = DefaultDateTime;
                }
                if (cbLTZF.Checked == true)
                {
                    fv.E_Time = Functions.GetStrTimeZF(nudHLT, nudMinLT);
                }
                else
                {
                    fv.E_Time = DefaultDateTime;
                }
                fv.VSB = Convert.ToDateTime(tbVSB.Text);
                fv.strFracht = tbFracht.Text;
                fv.Info = tbInfo.Text;

                if (!clsFrachtvergabe.IsIDIn(fv.ID_AP))
                {
                    //hinzufügen Transportauftrag zur DB
                    fv.InsertTransportauftragDatentoDataSet();
                    fv.AddTransportauftrag();
                    fv.UpdateSU();
                }
            }
            else
            {
                clsMessages.Frachtvergabe_SUfehlt();
            }
        }
        //
        //
        //

        public void OpenFrmADRPanelFrachtvergabe()
        {
            //Panel für ADR CTR öffnen
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADRPanelFrachtvergabe)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmADRPanelFrachtvergabe));
            }
            frmADRPanelFrachtvergabe vergabe = new frmADRPanelFrachtvergabe(this);
            vergabe.StartPosition = FormStartPosition.CenterParent;
            vergabe.Show();
            vergabe.BringToFront();
        }

        private void tbSU_DoubleClick(object sender, EventArgs e)
        {
            OpenFrmADRPanelFrachtvergabe();
        }



    }
}
