using Common.Enumerations;
using LVS;
using Sped4.Classes;
using Sped4.Controls;
using Sped4.Struct;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrSUList : UserControl
    {
        public Globals._GL_USER GL_User;
        DataTable dtSU = new DataTable("Aufträge SU");
        DataTable tmpSU = new DataTable();
        DateTime SearchDateVon = DateTime.Today;//DateTime.MinValue;
        DateTime SearchDateBis = DateTime.Today.AddDays(3);//DateTime.MaxValue;
        internal Int32 listenArt = 5; //fest in ctrAuftrag hinterlegt

        public ctrAufträge ctrAuftrag;

        public ctrSUList()
        {
            InitializeComponent();
        }
        //
        //------------ Load -------------------
        //
        private void ctrSUList_Load(object sender, EventArgs e)
        {
            afColorLabel1.Text = "Aufträge Subunternehmer";
            listenArt = 5;

            if (ctrAuftrag != null)
            {
                AddColToGridAuftragsList();
            }
            else
            {
                CloseCtrSUListe();
            }
        }
        //
        //----------- close ctr --------------
        //
        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseCtrSUListe();
        }
        //
        public void CloseCtrSUListe()
        {
            Int32 Count = this.ParentForm.Controls.Count;
            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == "TempSplitterSUList")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrSUList))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //
        //
        //
        public void SetSearchTimeDistance(DateTime dtVon, DateTime dtBis)
        {
            SearchDateVon = dtVon;
            SearchDateBis = dtBis;
            afColorLabel1.myText = "Aufträge an Subunternehmer [" + dtVon.ToShortDateString() + " bis " + dtBis.ToShortDateString() + "]";
        }
        //
        //
        //
        private void LoadDataFromDB()
        {
            dtSU.Clear();
            dtSU.Columns.Clear();
            ctrAuftrag.SearchDateBis = SearchDateBis;
            ctrAuftrag.SearchDateVon = SearchDateVon;
            //Baustelle
            //dtSU = clsAuftrag.GetAuftragsdatenByZeitraumAndStatus(this.GL_User , SearchDateVon, SearchDateBis, 4, true);
            CheckInputAuftragListe();
            clsAuftragListe check = new clsAuftragListe();
            check._GL_User = this.GL_User;
            //check.CheckDispoAndFAngaben(ref dtSU, GL_User);
            for (Int32 i = 0; i <= dtSU.Rows.Count - 1; i++)
            {
                check.CheckDispoAndFAngaben(ref dtSU, GL_User, i);
            }
        }
        //
        public void InitCtrSUList()
        {
            LoadDataFromDB();
            grdAuftrag.DataSource = dtSU;
            SetDgvColumnsVisible();
        }
        //
        //
        private void SetDgvColumnsVisible()
        {
            if (dtSU.Rows.Count > 0)
            {
                grdAuftrag.Columns["ID"].Visible = false;
                grdAuftrag.Columns["AuftragID"].Visible = false;
                grdAuftrag.Columns["KD_ID"].Visible = false;
                grdAuftrag.Columns["AuftraggeberName"].Visible = false;
                grdAuftrag.Columns["AuftraggeberPLZ"].Visible = false;
                grdAuftrag.Columns["AuftraggeberOrt"].Visible = false;
                grdAuftrag.Columns["Pos"].Visible = false;
                grdAuftrag.Columns["BSID"].Visible = false;
                grdAuftrag.Columns["Beladestelle"].Visible = false;
                grdAuftrag.Columns["B_PLZ"].Visible = false;
                grdAuftrag.Columns["B_Ort"].Visible = false;
                grdAuftrag.Columns["ESID"].Visible = false;
                grdAuftrag.Columns["Entladestelle"].Visible = false;
                grdAuftrag.Columns["E_PLZ"].Visible = false;
                grdAuftrag.Columns["E_Ort"].Visible = false;
                grdAuftrag.Columns["faellig"].Visible = false;
                grdAuftrag.Columns["VSB"].Visible = false;
                grdAuftrag.Columns["Stat"].Visible = false;
                grdAuftrag.Columns["ZF"].Visible = false;
                grdAuftrag.Columns["Prio"].Visible = false;
                grdAuftrag.Columns["Ladenummer"].Visible = false;
                grdAuftrag.Columns["Ber_A"].Visible = false;
                grdAuftrag.Columns["Ber_V"].Visible = false;
                grdAuftrag.Columns["Ber_E"].Visible = false;
                grdAuftrag.Columns["WAv"].Visible = false;
                grdAuftrag.Columns["WAb"].Visible = false;
                grdAuftrag.Columns["B_Strasse"].Visible = false;
                grdAuftrag.Columns["E_Strasse"].Visible = false;
                grdAuftrag.Columns["Relation"].Visible = false;
                grdAuftrag.Columns["dispo"].Visible = false;
                grdAuftrag.Columns["FAngaben"].Visible = false;
                grdAuftrag.Columns["okADR"].Visible = false;
                grdAuftrag.Columns["ScanExist"].Visible = false;
                grdAuftrag.Columns["Read"].Visible = false;
                grdAuftrag.Columns["Dringlichkeit"].Visible = false;
                grdAuftrag.Columns["ZFRequire"].Visible = false;
                if (listenArt <= 4)
                {
                    grdAuftrag.Columns["Beladezeit"].Visible = false;
                    grdAuftrag.Columns["Entladezeit"].Visible = false;
                }
                grdAuftrag.Columns["Netto"].Visible = false;
                grdAuftrag.Columns["Brutto"].Visible = false;
                grdAuftrag.Columns["drDate"].Visible = false;
                if (listenArt == 5)
                {
                    grdAuftrag.Columns["Beladezeit"].Visible = false;
                    grdAuftrag.Columns["Entladezeit"].Visible = false;
                    grdAuftrag.Columns["SU_ID"].Visible = false;
                }
            }
        }
        //
        private void FormatGrd(object sender)
        {
            if (dtSU.Rows.Count > 0)
            {
                AFGrid grd = (AFGrid)sender;
                //Grid durchlaufen und Zelle für Zelle Daten hinterlegen
                for (Int32 iRow = 0; iRow <= grd.Rows.Count - 1; iRow++)
                {
                    //clsAuftragListe list= new clsAuftragListe();
                    clsAuftragListe list = new clsAuftragListe();
                    list._GL_User = this.GL_User;
                    list.SetAuftragsListe(ref list, dtSU, listenArt, iRow, GL_User);

                    try
                    {
                        //Column 3
                        grd.Rows[iRow].Cells[2].Value = "Auftrag/Pos : " +
                                                         Environment.NewLine + "Auftraggeber : " +
                                                         Environment.NewLine +
                                                         Environment.NewLine + "Versender : " +
                                                         Environment.NewLine +
                                                         Environment.NewLine + "Empfänger: " +
                                                         Environment.NewLine;


                        //Column 4
                        grd.Rows[iRow].Cells[3].Value = list.m_i_AuftragID.ToString() + " / " + list.m_i_AuftragPos.ToString() +
                                                         Environment.NewLine + list.m_str_Auftraggeber_ADR +
                                                         Environment.NewLine + list.m_str_B_ADR +
                                                         Environment.NewLine + list.m_str_E_ADR;

                        //Column 5
                        decimal ausgPosGewicht = 0.0m;
                        decimal GesamtGewicht = 0.0m;
                        ausgPosGewicht = list.m_dec_Brutto;
                        GesamtGewicht = list.m_dec_GesamtBrutto;
                        /****
                        if (list.m_dec_Brutto > 0)
                        {
                            ausgPosGewicht = list.m_dec_Brutto;
                            GesamtGewicht = list.m_dec_GesamtBrutto; 
                        }
                        else
                        {
                            ausgPosGewicht = list.m_dec_gemPosGewicht;
                            GesamtGewicht = list.m_dec_tatGesamtGewicht;
                        }
                        ****/
                        grd.Rows[iRow].Cells[4].Value = Functions.FormatDecimal(ausgPosGewicht) + " [kg]" +                               // Restgewicht nach Auftragsplitting des Auftrags
                                                               Environment.NewLine + "(" + Functions.FormatDecimal(GesamtGewicht) + "[kg])" +       // Gewicht ist das Gesamte Auftragsgewicht
                                                               Environment.NewLine + list.m_str_Gut +
                                                               Environment.NewLine + "Lade #: " + list.m_str_Ladenummer;

                        //Column 6
                        grd.Rows[iRow].Cells[5].Value = "LT: " + Environment.NewLine +
                                                            "WA: " + Environment.NewLine +
                                                            "VSB: " + Environment.NewLine +
                                                            "ZF: " + Environment.NewLine;

                        //Column 7
                        string strWAvon = Functions.FormatToHHMM(list.m_dt_WAvon);
                        string strWAbis = Functions.FormatToHHMM(list.m_dt_WAbis);
                        string strWA = string.Empty;
                        string strDef = "00:00";
                        if ((strDef != strWAvon) | (strDef != strWAbis))
                        {
                            strWA = strWAvon + "-" + strWAbis;
                        }

                        grd.Rows[iRow].Cells[6].Value = list.m_dt_Liefertermin.ToShortDateString() + Environment.NewLine + //LT
                                                           strWA + Environment.NewLine +    //WA von
                                                           list.m_dt_VSB.ToShortDateString() + Environment.NewLine +          //WA bis
                                                           Functions.FormatToHHMM(list.m_dt_ZF) + Environment.NewLine;

                        //Column 8
                        if (list.m_i_Status >= 4)
                        {
                            if ((list.m_dt_B_Date != DateTime.MaxValue) | (list.m_dt_E_Date != DateTime.MaxValue))
                            {
                                grd.Rows[iRow].Cells[7].Value = "Beladezeit:" + Environment.NewLine +
                                                                    list.m_dt_B_Date.ToShortDateString() + Environment.NewLine +
                                                                    list.m_dt_B_Date.ToShortTimeString() + Environment.NewLine +
                                                                    "Entladezeit:" + Environment.NewLine +
                                                                    list.m_dt_E_Date.ToShortDateString() + Environment.NewLine +
                                                                    list.m_dt_E_Date.ToShortTimeString();
                            }
                        }
                        //Column 9
                        string drDatum = string.Empty;
                        if (list.m_i_Status >= 4)
                        {
                            if (list.m_dt_drDatum != DateTime.MaxValue)
                            {
                                drDatum = list.m_dt_drDatum.ToShortDateString();
                            }
                        }
                        grd.Rows[iRow].Cells[8].Value = drDatum;

                        //Column 10
                        /***
                        string SU = string.Empty;
                        if (list.m_i_Status >= 4)
                        {
                            if (list.m_dt_drDatum != DateTime.MaxValue)
                            {
                                drDatum = list.m_dt_drDatum.ToShortDateString();
                            }
                        }
                         * ***/
                        grd.Rows[iRow].Cells[8].Value = list.SU;


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
        //
        //---------- Spalten zum Tabel hinzufügen -----------------
        //
        private void CheckInputAuftragListe()
        {
            DataColumn col3 = dtSU.Columns.Add("dispo", typeof(bool));
            DataColumn col4 = dtSU.Columns.Add("FAngaben", typeof(String));
            DataColumn col5 = dtSU.Columns.Add("okADR", typeof(bool));
            DataColumn col6 = dtSU.Columns.Add("SU", typeof(string));
            DataColumn col7 = dtSU.Columns.Add("ScanExist", typeof(bool));
            DataColumn col8 = dtSU.Columns.Add("Read", typeof(bool));
            DataColumn col9 = dtSU.Columns.Add("Dringlichkeit", typeof(Int32));
            DataColumn co20 = dtSU.Columns.Add("ZFRequire", typeof(bool));
        }
        //
        //------------ Erstell die gewünschten Columns fürs Grid  ---------------
        //
        private void AddColToGridAuftragsList()
        {

            //----------- Formatierung der Zellen des GRID 
            // STATUS
            System.Windows.Forms.DataGridViewImageColumn Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            Column1.Name = "Status";
            Column1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdAuftrag.Columns.Add(Column1);


            // Details
            System.Windows.Forms.DataGridViewImageColumn Column2 = new System.Windows.Forms.DataGridViewImageColumn();
            //System.Windows.Forms.DataGridViewButtonColumn Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
            Column2.Name = "Details";
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdAuftrag.Columns.Add(Column2);


            //Beschreibung
            System.Windows.Forms.DataGridViewTextBoxColumn Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column3.Name = "Beschreibung";
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Column3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grdAuftrag.Columns.Add(Column3);

            //Beschreibung2
            System.Windows.Forms.DataGridViewTextBoxColumn Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column4.Name = "Beschreibung2";
            Column4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Column4.HeaderText = "Daten";
            Column4.Width = 150;
            grdAuftrag.Columns.Add(Column4);

            //Werte
            System.Windows.Forms.DataGridViewTextBoxColumn Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column5.Name = "Werte";
            Column5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Column5.HeaderText = "Gewicht/Gut";
            Column5.Width = 100;
            grdAuftrag.Columns.Add(Column5);

            //Beschriftung Termine / VSB
            System.Windows.Forms.DataGridViewTextBoxColumn Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column6.Name = "Spalte6";
            Column6.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Column6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Column6.HeaderText = "";
            Column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdAuftrag.Columns.Add(Column6);

            //Daten Termine / VSB
            System.Windows.Forms.DataGridViewTextBoxColumn Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column7.Name = "Termine";
            Column7.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Column7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Column7.HeaderText = "Termine";
            //Column7.Width = 120;
            Column7.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdAuftrag.Columns.Add(Column7);

            //Entladung
            System.Windows.Forms.DataGridViewTextBoxColumn Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column8.Name = "Bearbeitung";
            Column8.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Column8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Column8.HeaderText = "Bearbeitung";
            Column8.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdAuftrag.Columns.Add(Column8);

            //Papiere gedruckt
            System.Windows.Forms.DataGridViewTextBoxColumn Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column9.Name = "Druck";
            Column9.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Column9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Column9.HeaderText = "Druck";
            Column9.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdAuftrag.Columns.Add(Column9);

        }
        //
        //
        //
        private void grdAuftrag_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Int32 iCount = grdAuftrag.Rows.Count;
            if (e.RowIndex <= iCount - 1)
            {
                try
                {
                    Int32 iDring = 0;
                    Int32 iStatus = 0;
                    bool okADR = false;
                    bool prioritaet = false;
                    bool read = true;
                    bool scan = false;
                    bool ZFReq = false;

                    if (grdAuftrag.Columns["Dringlichkeit"] != null)
                    {
                        if ((!object.ReferenceEquals(grdAuftrag.Rows[e.RowIndex].Cells["Dringlichkeit"].Value, DBNull.Value)))
                        {
                            if (grdAuftrag.Rows[e.RowIndex].Cells["Dringlichkeit"].Value != null)
                            {
                                iDring = (Int32)grdAuftrag.Rows[e.RowIndex].Cells["Dringlichkeit"].Value;
                            }
                        }
                    }
                    if (grdAuftrag.Columns["Stat"] != null)
                    {
                        if ((!object.ReferenceEquals(grdAuftrag.Rows[e.RowIndex].Cells["Stat"].Value, DBNull.Value)))
                        {
                            if (grdAuftrag.Rows[e.RowIndex].Cells["Stat"].Value != null)
                            {
                                iStatus = (Int32)grdAuftrag.Rows[e.RowIndex].Cells["Stat"].Value;
                            }
                        }
                    }
                    if (grdAuftrag.Columns["okADR"] != null)
                    {
                        if ((!object.ReferenceEquals(grdAuftrag.Rows[e.RowIndex].Cells["okADR"].Value, DBNull.Value)))
                        {
                            if (grdAuftrag.Rows[e.RowIndex].Cells["okADR"].Value != null)
                            {
                                okADR = (bool)grdAuftrag.Rows[e.RowIndex].Cells["okADR"].Value;
                            }
                        }
                    }
                    if (grdAuftrag.Columns["Prio"] != null)
                    {
                        if ((!object.ReferenceEquals(grdAuftrag.Rows[e.RowIndex].Cells["Prio"].Value, DBNull.Value)))
                        {
                            if (grdAuftrag.Rows[e.RowIndex].Cells["Prio"].Value != null)
                            {
                                prioritaet = (bool)grdAuftrag.Rows[e.RowIndex].Cells["Prio"].Value;
                            }
                        }
                    }
                    if (grdAuftrag.Columns["Read"] != null)
                    {
                        if ((!object.ReferenceEquals(grdAuftrag.Rows[e.RowIndex].Cells["Read"].Value, DBNull.Value)))
                        {
                            if (grdAuftrag.Rows[e.RowIndex].Cells["Read"].Value != null)
                            {
                                read = (bool)grdAuftrag.Rows[e.RowIndex].Cells["Read"].Value;
                            }
                        }
                    }
                    if (grdAuftrag.Columns["ScanExist"] != null)
                    {
                        if ((!object.ReferenceEquals(grdAuftrag.Rows[e.RowIndex].Cells["ScanExist"].Value, DBNull.Value)))
                        {
                            if (grdAuftrag.Rows[e.RowIndex].Cells["ScanExist"].Value != null)
                            {
                                scan = (bool)grdAuftrag.Rows[e.RowIndex].Cells["ScanExist"].Value;
                            }
                        }
                    }
                    if (grdAuftrag.Columns["ZFRequire"] != null)
                    {
                        if ((!object.ReferenceEquals(grdAuftrag.Rows[e.RowIndex].Cells["ZFRequire"].Value, DBNull.Value)))
                        {
                            if (grdAuftrag.Rows[e.RowIndex].Cells["ZFRequire"].Value != null)
                            {
                                ZFReq = (bool)grdAuftrag.Rows[e.RowIndex].Cells["ZFRequire"].Value;
                            }
                        }
                    }

                    /********************************************************************/


                    if (e.ColumnIndex == 0)
                    {
                        /************************************************************************************
                         * Übersicht Status und entsprechende Images
                         * - 1 unvollständig  - delete_16.png   (rotes Kreuz)
                         * - 2 vollständig    - add.png         (grünes Kreuz)
                         * - 3 storniert      - form_green_delete.png (grünes Form mit rotem Kreuz)
                         * - 4 disponiert     - disponiert.png  (rotes Fähnchen)
                         * - 5 durchgeführt   - done.png        (blaues Fähnchen)
                         * - 6 Freigabe Berechnung - Freigabe_Berechnung.png  (grünes Fähnchen)
                         * - 7 berechnet      - check       (gründer Haken) 
                         * 
                         * ***********************************************************************************/

                        if ((iStatus == 1))    //unvollständig
                        {
                            e.Value = Sped4.Properties.Resources.delete;
                            //Functions.GetDataGridCellStatusImage(ref e, iStatus);
                            e.Value = Functions.GetDataGridCellStatusImage(iStatus);
                        }
                        else
                        {
                            if ((okADR))
                            {
                                //Functions.GetDataGridCellStatusImage(ref e, iStatus);
                                e.Value = Functions.GetDataGridCellStatusImage(iStatus);
                                /****
                                switch (iStatus)
                                {
                                    case 2:
                                        e.Value = Sped4.Properties.Resources.add;
                                        break;
                                    case 3:
                                        e.Value = Sped4.Properties.Resources.form_green_delete;
                                        break;
                                    case 4:
                                        e.Value = Sped4.Properties.Resources.disponiert;
                                        break;
                                    case 5:
                                        e.Value = Sped4.Properties.Resources.done;
                                        break;
                                    case 6:
                                        e.Value = Sped4.Properties.Resources.Freigabe_Berechnung;
                                        break;
                                }
                                 * ****/
                            }
                        }

                        //---- entsprechende Ausgabe
                        if ((prioritaet) & (iStatus <= 2))
                        {
                            e.CellStyle.BackColor = Color.Red;
                        }
                    }

                    /*****************************************************************************************************
                     *          COLUMN 2
                     * **************************************************************************************************/

                    if (e.ColumnIndex == 1)
                    {
                        if (!read)
                        {
                            e.Value = Sped4.Properties.Resources.New;
                        }
                        else
                        {
                            // Wenn Auftrag noch nicht hinterlegt, dann Warnung
                            if (scan)
                            {
                                e.Value = Sped4.Properties.Resources.document_attachment;
                            }
                            else
                            {
                                e.Value = Sped4.Properties.Resources.document_delete;
                            }
                        }
                    }


                    /*****************************************************************************************************
                     *          COLUMN 3
                     * **************************************************************************************************/


                    if ((e.ColumnIndex > 1) | (e.ColumnIndex <= iCount))
                    {
                        //Beschreibung: Dringlichkeitsstufen 1-3
                        // 1: Liefertermin innerhalb 2 W-Tage
                        // 2: Liefertermin in 2 W-Tagen
                        // 3: Liefertermin größer 2 Tage entfernt
                        switch (iDring)
                        {
                            case 1:
                                e.CellStyle.ForeColor = Color.Blue;
                                break;
                            case 2:
                                e.CellStyle.ForeColor = Color.RoyalBlue;
                                break;
                            case 3:
                                e.CellStyle.ForeColor = Color.Gray;
                                break;
                            case 4:
                                e.CellStyle.ForeColor = Color.Black;
                                break;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        //
        //
        //
        private void FormatGrdSUList(object sender)
        {
            if (dtSU.Rows.Count > 0)
            {
                AFGrid grd = (AFGrid)sender;
                //Grid durchlaufen und Zelle für Zelle Daten hinterlegen
                for (Int32 iRow = 0; iRow <= grd.Rows.Count - 1; iRow++)
                {
                    //clsAuftragListe list= new clsAuftragListe();
                    clsAuftragListe list = new clsAuftragListe();
                    list._GL_User = this.GL_User;
                    list.SetAuftragsListe(ref list, dtSU, listenArt, iRow, GL_User);

                    try
                    {
                        //Column 3
                        grd.Rows[iRow].Cells[2].Value = "Auftrag/Pos : " +
                                                         Environment.NewLine + "Auftraggeber : " +
                                                         Environment.NewLine +
                                                         Environment.NewLine + "Versender : " +
                                                         Environment.NewLine +
                                                         Environment.NewLine + "Empfänger: " +
                                                         Environment.NewLine;


                        //Column 4
                        grd.Rows[iRow].Cells[3].Value = list.m_i_AuftragID.ToString() + " / " + list.m_i_AuftragPos.ToString() +
                                                         Environment.NewLine + list.m_str_Auftraggeber_ADR +
                                                         Environment.NewLine + list.m_str_B_ADR +
                                                         Environment.NewLine + list.m_str_E_ADR;

                        //Column 5
                        decimal ausgPosGewicht = 0.0m;
                        decimal GesamtGewicht = 0.0m;
                        ausgPosGewicht = list.m_dec_Brutto;
                        GesamtGewicht = list.m_dec_GesamtBrutto;
                        /****
                        if (list.m_dec_tatPosGewicht > 0)
                        {
                            ausgPosGewicht = list.m_dec_tatPosGewicht;
                            GesamtGewicht = list.m_dec_tatGesamtGewicht; 
                        }
                        else
                        {
                            ausgPosGewicht = list.m_dec_gemPosGewicht;
                            GesamtGewicht = list.m_dec_tatGesamtGewicht;
                        }
                        ***/
                        grd.Rows[iRow].Cells[4].Value = Functions.FormatDecimal(ausgPosGewicht) + " [kg]" +                               // Restgewicht nach Auftragsplitting des Auftrags
                                                               Environment.NewLine + "(" + Functions.FormatDecimal(GesamtGewicht) + "[kg])" +       // Gewicht ist das Gesamte Auftragsgewicht
                                                               Environment.NewLine + list.m_str_Gut +
                                                               Environment.NewLine + "Lade #: " + list.m_str_Ladenummer;

                        //Column 6
                        grd.Rows[iRow].Cells[5].Value = "LT: " + Environment.NewLine +
                                                            "WA: " + Environment.NewLine +
                                                            "VSB: " + Environment.NewLine +
                                                            "ZF: " + Environment.NewLine;

                        //Column 7
                        string strWAvon = Functions.FormatToHHMM(list.m_dt_WAvon);
                        string strWAbis = Functions.FormatToHHMM(list.m_dt_WAbis);
                        string strWA = string.Empty;
                        string strDef = "00:00";
                        if ((strDef != strWAvon) | (strDef != strWAbis))
                        {
                            strWA = strWAvon + "-" + strWAbis;
                        }

                        grd.Rows[iRow].Cells[6].Value = list.m_dt_Liefertermin.ToShortDateString() + Environment.NewLine + //LT
                                                           strWA + Environment.NewLine +    //WA von
                                                           list.m_dt_VSB.ToShortDateString() + Environment.NewLine +          //WA bis
                                                           Functions.FormatToHHMM(list.m_dt_ZF) + Environment.NewLine;

                        //Column 8
                        if (list.m_i_Status >= 4)
                        {
                            if ((list.m_dt_B_Date != DateTime.MaxValue) | (list.m_dt_E_Date != DateTime.MaxValue))
                            {
                                grd.Rows[iRow].Cells[7].Value = "Beladezeit:" + Environment.NewLine +
                                                                    list.m_dt_B_Date.ToShortDateString() + Environment.NewLine +
                                                                    list.m_dt_B_Date.ToShortTimeString() + Environment.NewLine +
                                                                    "Entladezeit:" + Environment.NewLine +
                                                                    list.m_dt_E_Date.ToShortDateString() + Environment.NewLine +
                                                                    list.m_dt_E_Date.ToShortTimeString();
                            }
                        }
                        //Column 9
                        string drDatum = string.Empty;
                        if (list.m_i_Status >= 4)
                        {
                            if (list.m_dt_drDatum != DateTime.MaxValue)
                            {
                                drDatum = list.m_dt_drDatum.ToShortDateString();
                            }
                        }
                        grd.Rows[iRow].Cells[8].Value = drDatum;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
        //
        //
        //
        private void grdAuftrag_DataSourceChanged(object sender, EventArgs e)
        {
            FormatGrdSUList(sender);
        }
        //
        //
        //
        private void grdAuftrag_DoubleClick(object sender, EventArgs e)
        {
            miDetails_Click(sender, e);
        }
        //
        //
        private void miDetails_Click(object sender, EventArgs e)
        {

        }
        //
        //
        //
        public void ctrSUList_Resize()
        {
            Int32 gridGr = Functions.dgv_GetWidthShownGrid(ref grdAuftrag);

            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrSUList))
                {
                    this.ParentForm.Controls[i].Width = gridGr;
                    this.ParentForm.Controls[i].Refresh();
                }
            }
        }
        //
        //
        //
        private void tsbtnAnpassen_Click(object sender, EventArgs e)
        {
            ctrSUList_Resize();
        }
        //
        //--------------- combo mit SU ------------------
        //
        private void cbSearchSU_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSearchSU.Checked)
            {
                cbSuAuswahl.Enabled = true;
                pbFilter.Enabled = true;
                FillComboSuAuswahl();
            }
            else
            {
                cbSuAuswahl.DataSource = null;
                cbSuAuswahl.Enabled = false;
                pbFilter.Enabled = false;
                InitCtrSUList();
            }
        }
        //
        //----------- combo füllen mit den Subunternehmern -------------
        //
        private void FillComboSuAuswahl()
        {
            if (dtSU.Rows.Count > 0)
            {
                //DataTable dtAuswahl = new DataTable();
                DataSet ds = new DataSet();
                ds = clsADR.GetSUForCBSUAuswahl(SearchDateVon, SearchDateBis, listenArt);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbSuAuswahl.DataSource = ds.Tables[0];
                    cbSuAuswahl.DisplayMember = "SU";
                    cbSuAuswahl.ValueMember = "SU_ID";
                }
            }

        }
        //
        //----------- Auftrag an SU stornieren ------------------
        //
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (GL_User.write_TransportOrder)
            {
                if (grdAuftrag.Rows.Count >= 1)
                {
                    decimal auftrag = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["AuftragID"].Value;
                    decimal myAuftragPosTableID = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["ID"].Value;
                    //--- ausgewählte Datensatz ------
                    clsFrachtvergabe fv = new clsFrachtvergabe();
                    fv.BenutzerID = GL_User.User_ID;
                    fv.AuftragID = auftrag;
                    fv.AuftragPos = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["Pos"].Value;
                    fv.ID_AP = myAuftragPosTableID;
                    fv.DeleteTransportauftrag();
                    //listenArt = 5;

                    //Transportauftrag an SU aus DB AuftragScan löschen
                    //clsDocScan.DeleteAuftragScan(auftrag, Globals.enumImageArt.Subunternehmerauftrag.ToString());
                    clsDocScan ds = new clsDocScan();
                    ds._GL_User = this.GL_User;
                    ds.m_dec_AuftragPosTableID = myAuftragPosTableID;
                    ds.m_str_ImageArt = enumDokumentenArt.FrachtauftragAnSU.ToString();
                    ds.DeleteAuftragScanByAuftragPosTableIDAndImageArt();

                    InitCtrSUList();
                    ctrAuftrag.listenArt = 1;
                    ctrAuftrag.InitDGV();
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //
        //------------- Refresh liste -------------
        //
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            InitCtrSUList();
        }
        /**********************************************************************
         * 
         *                          Drag & Drop
         * 
         * ********************************************************************/
        //
        //
        private void grdAuftrag_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(structAuftPosRow)))
            {
                structAuftPosRow IDAndRowID = default(structAuftPosRow);
                try
                {
                    IDAndRowID = (structAuftPosRow)e.Data.GetData(typeof(structAuftPosRow));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                e.Effect = DragDropEffects.Copy;
            }
        }
        //
        //
        //
        private void grdAuftrag_DragDrop(object sender, DragEventArgs e)
        {
            if (GL_User.write_Disposition)
            {
                try
                {
                    // für Daten Kommission
                    if (e.Data.GetDataPresent(typeof(structAuftPosRow)))
                    {
                        structAuftPosRow IDAndRowID = default(structAuftPosRow);
                        IDAndRowID = (structAuftPosRow)e.Data.GetData(typeof(structAuftPosRow));
                        OpenFrmDispoFrachtvergabe(IDAndRowID.ArtikelID);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //
        //
        //
        private void OpenFrmDispoFrachtvergabe(decimal myArtikelID)
        {
            frmDispoFrachtvergabe fv = new frmDispoFrachtvergabe(this.ctrAuftrag);
            fv.GL_User = this.GL_User;
            fv._ArtikelTableID = myArtikelID;
            fv.StartPosition = FormStartPosition.CenterScreen;
            fv.Show();
            fv.BringToFront();

        }
        //
        //------------------ Searcht for SU -------------------
        //
        private void pbFilter_Click(object sender, EventArgs e)
        {
            FilterSU();
        }
        //
        //
        //
        private void FilterSU()
        {
            string SearchText = cbSuAuswahl.SelectedValue.ToString();
            string Ausgabe = string.Empty;

            LoadDataFromDB();
            DataRow[] rows = dtSU.Select("SU_ID = '" + SearchText + "'", "SU_ID");
            tmpSU.Clear();
            tmpSU = dtSU.Clone();

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["SU_ID"].ToString() + "\n";
                tmpSU.ImportRow(row);
            }
            dtSU.Clear();
            dtSU = tmpSU;
            grdAuftrag.DataSource = dtSU;
        }

        private void tsbtnStatChange_Click(object sender, EventArgs e)
        {
            /***
            if (grdAuftrag.Rows.Count >= 1)
            {
                //--- ausgewählte Datensatz ------
                decimal Auftrag = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["AuftragID"].Value;
                decimal AuftragPos = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["Pos"].Value;

                clsAuftragsstatus ast = new clsAuftragsstatus();
                ast.Auftrag_ID = Auftrag;
                ast.AuftragPos = AuftragPos;
                Int32 iStatus = ast.GetAuftragsstatus();

                if (iStatus >= 4)
                {
                    if (Functions.IsFormAlreadyOpen(typeof(frmStatusChange)) != null)
                    {
                        Functions.FormClose(typeof(frmStatusChange));
                    }
                    frmStatusChange stL = new frmStatusChange();
                    stL.ctrSUListe = this;
                    stL.GL_User = GL_User;
                    stL.Auftrag = Auftrag;
                    stL.AuftragPos = AuftragPos;
                    stL.Status = iStatus;
                    stL.Show();
                    stL.BringToFront();
                    //stL.RefreshAuftragList += new frmStatusChange.RefreshAuftragListEventHandler(init);
                }
                //}
            }
            ***/
            ChangeStatus(sender, e);
        }
        //
        private void ChangeStatus(object sender, EventArgs e)
        {
            if (grdAuftrag.Rows.Count >= 1)
            {
                //--- ausgewählte Datensatz ------
                decimal Auftrag = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["AuftragID"].Value;
                decimal AuftragPos = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["Pos"].Value;

                clsAuftragsstatus ast = new clsAuftragsstatus();
                ast.Auftrag_ID = Auftrag;
                ast.AuftragPos = AuftragPos;
                ast.AP_ID = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["ID"].Value;
                Int32 iStatus = ast.GetAuftragsstatus();

                if (iStatus >= 4)
                {
                    if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmStatusChange)) != null)
                    {
                        Functions.frm_FormTypeClose(typeof(frmStatusChange));
                    }
                    //frmStatusChange stL = new frmStatusChange();
                    //stL.ctrSUListe = this;
                    //stL.GL_User = GL_User;
                    //stL.Auftrag = Auftrag;
                    //stL.AuftragPos = AuftragPos;
                    //stL.Status = iStatus;
                    //stL.Show();
                    //stL.BringToFront();
                    //stL.RefreshAuftragList += new frmStatusChange.RefreshAuftragListEventHandler(init);
                }
                //}
            }
        }
        //
        //
        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmAuftragslisteZeitraum az = new frmAuftragslisteZeitraum();
            az.ctrSUListe = this;
            az.ctrAuftrag = this.ctrAuftrag;
            az.StartPosition = FormStartPosition.CenterScreen;
            az.Show();
            az.BringToFront();
        }

        private void miDocs_Click(object sender, EventArgs e)
        {
            if (grdAuftrag.Rows.Count >= 1)
            {
                decimal AuftragID = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["AuftragID"].Value;
                decimal AuftragPos = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["Pos"].Value;
                decimal AuftragPosTableID = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["ID"].Value;
                DataSet ds = new DataSet(); //Leer

                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmPrintCenter)) != null)
                {
                    Functions.frm_FormTypeClose(typeof(frmPrintCenter));
                }
                frmPrintCenter pC = new frmPrintCenter();
                pC._AuftragID = AuftragID;
                pC._AuftragPos = AuftragPos;
                pC._AuftragPosTableID = AuftragPosTableID;
                pC.GL_User = GL_User;
                pC.Show();
                pC.BringToFront();
            }
        }
        //
        private void grdAuftrag_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //in Mousedown musste die Verknüpfung erstellt werden,damit diese Methode aufgerufen werden kann
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
            else
            {
                miDetails_Click(sender, e);
            }
        }
        //
        private void miDetails_Click(object sender, MouseEventArgs e)
        {
            if (this.grdAuftrag.Rows.Count > 0)
            {
                clsMessages mes = new clsMessages();
                decimal AuftragPosID = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["ID"].Value;
                decimal AuftragID = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["AuftragID"].Value;
                decimal AuftragPos = (decimal)this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["Pos"].Value;

                Functions.frm_FormTypeClose(typeof(frmAuftragView));
                //frmAuftragView av = new frmAuftragView(AuftragID, AuftragPos, ctrAuftrag, ctrAuftrag.Split, this);
                //Baustelle
                //frmAuftragView av = new frmAuftragView(AuftragPosID);
                frmAuftragView av = new frmAuftragView();
                //av._AuftragPosTableID = AuftragPosID;
                //av._AuftragNr = AuftragID;
                //av._AuftragPosNr = AuftragPos;
                av._ctrAuftrag = ctrAuftrag;
                av._AuftragSplit = ctrAuftrag.Split;
                av._ctrSUListe = this;
                av.GL_User = GL_User;
                av.Show();
                av.BringToFront();
            }
        }
        //
        //
        private void miCloseCtr_Click(object sender, EventArgs e)
        {
            CloseCtrSUListe();
        }

    }
}
