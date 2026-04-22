using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using Sped4;
using Sped4.Classes;

namespace Sped4
{
    public partial class frmAuftragErfassung : frmTEMPLATE
    {
        private string gueterArt;
        private int Status;
        private DataTable PosTable = new DataTable();
        private int auftrag = 0;
        private int RowIndex;
        ctrAufträge _ctrAuftrag = new ctrAufträge();
       

        public frmAuftragErfassung()
        {
            InitializeComponent();
            //_ctrAuftrag = ctrAuftrag;
            initForm();
        }
        //*************************************************************************************
        //------------    Methoden
        //*************************************************************************************
        //
        //--------------- init
        //
        private void initForm()
        {
            string strAuftrDatum = string.Empty;
            strAuftrDatum = DateTime.Today.ToShortDateString();
            button1.Visible = false;                                        // Save Button ausblenden bis Eingabedaten bestätigt
            tbAuftragsdatum.Text = strAuftrDatum;  //(DateTime.Now).ToString("dd.MM.yyyy");   // Auftragsdatum = aktuelle Datum


            clsAuftrag Auftrag = new clsAuftrag();

            tbANr.Text = Auftrag.ANr.ToString();                       // Auftragsnummer
            RowSetDefaultVal();                                             // Setzt Standardwerte im Grid
            Status = 0;
            gueterArt = "";

            cbUnvollstaendig.Checked = false;
            cbVollstaendig.Checked = false;

            //---- TextBoxen leeren -----
            tbAuftraggeber.Enabled = true;
            tbEmpfaenger.Enabled = true;
            tbVersender.Enabled = true;

            tbAuftraggeber.Text = "";
            tbEmpfaenger.Text = "";
            tbVersender.Text = "";
            tbLadeNr.Text = "";
            tbNotiz.Text = "";

            tbVersender.Enabled = false;
            tbEmpfaenger.Enabled = false;
            tbAuftraggeber.Enabled = false;

            //---- Grid
            afGrid1.Rows.Clear();

            //---- Vorlagen Grid
            InitVorlageGrd();
            grdArtVorlage.Visible = false;

        }
        //
        //-------- Init Grid Vorschlag Artikel ----------------
        //
        private void InitVorlageGrd()
        {

            //grdArtVorlage.DataSource = clsArtikel.ArtikelVorlagenTable();
            //grdArtVorlage.Columns["ID"].Visible = false;
            //grdArtVorlage.Width.
            //grdArtVorlage.Columns["Gut"].AutoSizeMode;   
        }
        //
        //----------- Datagrid ComboboxColumn ----------------
        //
        private void afGrid1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedValue != null)
            {
                gueterArt = ((ComboBox)sender).SelectedValue.ToString();  // Gib Value der Combobox aus Null abfangen
            }
        }
        //
        //--------------------------- Auswahl ComboBox  -------------------------
        //
        private void multiColumnComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (multiColumnComboBox1.SelectedIndex >= 0)
            {
                DataRowView drv = (DataRowView)multiColumnComboBox1.SelectedItem;
                tbAuftraggeber.Text = drv.Row.ItemArray[1].ToString() + ", " +
                                drv.Row.ItemArray[2].ToString() + ", " +
                                drv.Row.ItemArray[4].ToString() + ", " +
                                drv.Row.ItemArray[5].ToString() + " " +
                                drv.Row.ItemArray[6].ToString();
            }
        }
        private void multiColumnComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (multiColumnComboBox2.SelectedIndex >= 0)
            {
                DataRowView drv = (DataRowView)multiColumnComboBox2.SelectedItem;
                tbVersender.Text = drv.Row.ItemArray[1].ToString() + ", " +
                                drv.Row.ItemArray[2].ToString() + ", " +
                                drv.Row.ItemArray[4].ToString() + ", " +
                                drv.Row.ItemArray[5].ToString() + " " +
                                drv.Row.ItemArray[6].ToString();
            }
        }
        private void multiColumnComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (multiColumnComboBox3.SelectedIndex >= 0)
            {
                DataRowView drv = (DataRowView)multiColumnComboBox3.SelectedItem;
                tbEmpfaenger.Text = drv.Row.ItemArray[1].ToString() + ", " +
                                drv.Row.ItemArray[2].ToString() + ", " +
                                drv.Row.ItemArray[4].ToString() + ", " +
                                drv.Row.ItemArray[5].ToString() + " " +
                                drv.Row.ItemArray[6].ToString();
            }
        }
        //
        //
        //
        private void multiColumnComboBox1_OpenSearchForm(object sender, EventArgs e)
        {
            FormSearch newForm = new FormSearch((MultiColumnComboBox)sender);
            newForm.ShowDialog();
        }

        private void multiColumnComboBox2_OpenSearchForm(object sender, EventArgs e)
        {
            FormSearch newForm = new FormSearch((MultiColumnComboBox)sender);
            newForm.ShowDialog();
        }

        private void multiColumnComboBox3_OpenSearchForm(object sender, EventArgs e)
        {
            FormSearch newForm = new FormSearch((MultiColumnComboBox)sender);
            newForm.ShowDialog();
        }
        //
        //----------- Save Button  --------------------
        //
        private void button1_Click(object sender, EventArgs e)
        {
           
            if ((multiColumnComboBox1.SelectedIndex > -1) &
                 (multiColumnComboBox2.SelectedIndex > -1) &
                 (multiColumnComboBox3.SelectedIndex > -1)) 
            {           
                tbT_KW.Text = tbT_KW.Text.ToString().Trim();
                tbLadeNr.Text = tbLadeNr.Text.ToString().Trim();
                tbNotiz.Text = tbNotiz.Text.ToString().Trim();
                
                AssignValueAuftrag();
                AssignValueArtikel();
                AssignValueAuftragPos();

                initForm();
                
            }
            else
            {
                string Help;
                Help = "Folgende Pflichtfelder sind nicht ausgefüllt: \n";
                if (multiColumnComboBox1.SelectedIndex == -1)
                {
                    Help = Help + "Auftraggeber nicht ausgewählt \n";
                }
                if (multiColumnComboBox2.SelectedIndex == -1)
                {
                    Help = Help + "Versender nicht ausgewählt \n";
                }
                if (multiColumnComboBox3.SelectedIndex == -1)
                {
                    Help = Help + "Empfänger nicht ausgewählt \n";
                }
                MessageBox.Show(Help);
                Help = "";

            }
        }
        //
        //-------- zuweisen der Werte  -------------
        //
        private void AssignValueArtikel()
        {
            //--- Artikeldaten ----
            clsArtikel Artikel = new clsArtikel();
            Artikel.SetColArtTable();

            for (int i = 0; i < afGrid1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < afGrid1.Columns.Count; j++)
                {
                    //if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Artikel")
                    //{
                    //    Artikel.ArtikelNr = afGrid1[j, i].Value.ToString();
                    //}
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Gut")
                    {
                        Artikel.Gut = afGrid1[j, i].Value.ToString();
                    }
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Dicke")
                    {
                        Artikel.Dicke = Convert.ToDecimal(afGrid1[j, i].Value.ToString());
                    }
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Breite")
                    {
                        Artikel.Breite = Convert.ToDecimal(afGrid1[j, i].Value.ToString());
                    } 
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Länge")
                    {
                        Artikel.Laenge = Convert.ToDecimal(afGrid1[j, i].Value.ToString());
                    } 
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Höhe")
                    {
                        Artikel.Hoehe = Convert.ToDecimal(afGrid1[j, i].Value.ToString());
                    } 
                    //if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Menge")
                    //{
                    //    Artikel.Menge = Convert.ToInt32(afGrid1[j, i].Value.ToString());
                    //} 
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "ME")
                    {
                        Artikel.ME = afGrid1[j, i].Value.ToString();
                    }
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Gewicht")
                    {
                        Artikel.Gewicht = Convert.ToDecimal(afGrid1[j, i].Value.ToString());
                    }
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Ladenummer")
                    {
                        Artikel.Ladenummer = afGrid1[j, i].Value.ToString();
                    }
                }
                Artikel.ValueToArtTable();
            }
            //int Auftraggeber = Convert.ToInt32(multiColumnComboBox1.SelectedValue.ToString());
            //auftrag = clsAuftrag.GetIDbyValue(Auftraggeber, GGewichtAuftrag, Convert.ToDateTime(tbAuftragsdatum.Text.ToString())); 
            //-- Eintrag in DB -----
           
          
          // Artikel.Add(auftrag, auftragPos);

        }
        //
        //------------- Assign Auftrag  ---------------
        //
        private void AssignValueAuftragPos()
        {
            int Auftraggeber = Convert.ToInt32(multiColumnComboBox1.SelectedValue.ToString());
            int Beladestelle = Convert.ToInt32(multiColumnComboBox2.SelectedValue.ToString());
            int Entladestelle = Convert.ToInt32(multiColumnComboBox3.SelectedValue.ToString());

            clsAuftragPos AuftragPos = new clsAuftragPos();
             
            //--- Setz Val für AuftragPos ------
            AuftragPos.Auftrag_ID = auftrag;

            if (cbTermin.Checked == false)
            {
                AuftragPos.T_Date = dtpT_date.Value;

            }
            else
            {
                AuftragPos.T_Date = Convert.ToDateTime("01.01.2000"); // bedeutet leer - nicht gewählt

            }
            //--- status wird automatisch hier gesetzt - Auftragsstatus siehe Globals
            AuftragPos.Status = SetStatus();

            AuftragPos.Ladenummer = tbLadeNr.Text;

            AuftragPos.Artikel_ID = AuftragPos.ID;

            // -- Eintrag in DB ---
            AuftragPos.Add();
        }
        //
        //------------- Assign Auftrag  ---------------
        //
        private void AssignValueAuftrag()
        {
            clsAuftrag Auftrag = new clsAuftrag();
            //auftrag = tbANr.tex

            int Auftraggeber = Convert.ToInt32(multiColumnComboBox1.SelectedValue.ToString());

            //----- Addition Einzelgewichte jeder Position beendet und ergibt das Gesamtgewicht des Auftrags
            Auftrag.Gewicht = GetGGewicht();
            Auftrag.LadeNr = tbLadeNr.Text;
            Auftrag.Notiz = tbNotiz.Text;
            Auftrag.Status = SetStatus();
            Auftrag.ADate =Convert.ToDateTime(tbAuftragsdatum.Text.ToString());
            Auftrag.KD_ID = Auftraggeber;
            
            if (cbTermin.Checked == false)
            {
                Auftrag.T_Date = dtpT_date.Value;

            }
            else
            {
                Auftrag.T_Date = Convert.ToDateTime("01.01.2000"); // bedeutet leer - nicht gewählt
              
            }

            //*** Eintrag DB Artikel und Auftrag
            Auftrag.Add(auftrag);
        }
        //
        //------ liest aus dem Datagrid das Gesamtgewicht des Auftrags heraus -----
        //
        private decimal GetGGewicht()
        {
            decimal GGewichtAuftrag = 0.00M;
            for (int i = 0; i < afGrid1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < afGrid1.Columns.Count; j++)
                {
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Gewicht")
                    {
                        GGewichtAuftrag = GGewichtAuftrag + Convert.ToDecimal(this.afGrid1[j, i].Value.ToString());
                    }
                }
            }
            return GGewichtAuftrag;
        }

        //
        //------- Setzt Status für den Auftrag  ---------
        //
        private int SetStatus()
        {
            if (cbUnvollstaendig.Checked)
            {
                Status = 1;
                button1.Visible = true;
            }
            if (cbVollstaendig.Checked)
            {
                Status = 2;
                button1.Visible = true;
            }
            return Status;
        }
        //
        //---------- CheckBox Termine unbekannt  ---------------------------
        //
        private void cbTermin_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTermin.Checked)
            {
                dtpT_date.Enabled = false;
                tbT_KW.Enabled = false;
                cbUnvollstaendig.Checked = true;
                cbVollstaendig.Checked = false;
            }
            else
            {
                dtpT_date.Enabled = true;
                tbT_KW.Enabled = true;
                cbUnvollstaendig.Checked = false;
                cbUnvollstaendig.Checked = true;
            }
        }
        //
        //---- Standardwerte werden für jede neue Zeile/Celle gesetzt ------
        //
        private void afGrid1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            RowSetDefaultVal();
        }
        //
        private void RowSetDefaultVal()
        {
            int intDefault = 0;
            string strDefault = "";
            decimal decDefault=0.00M;
                

            for (int i = afGrid1.Rows.Count-1; i < afGrid1.Rows.Count; i++)
            {
                for (int j = 0; j < afGrid1.Columns.Count - 1; j++)
                {
                    //if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Artikel")
                    //{
                    //    afGrid1[j, i].Value = strDefault;
                    //}
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Gut")
                    {
                        afGrid1[j, i].Value = strDefault;
                    }
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Dicke")
                    {
                        afGrid1[j, i].Value = decDefault;
                    }
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Breite")
                    {
                        afGrid1[j, i].Value = decDefault;
                    }
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Länge")
                    {
                        afGrid1[j, i].Value = decDefault;
                    }
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Höhe")
                    {
                        afGrid1[j, i].Value = decDefault;
                    }
                    //if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Menge")
                    //{
                    //    afGrid1[j, i].Value = intDefault;
                    //}
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "ME")
                    {
                        afGrid1[j, i].Value = strDefault;
                    }
                    if (afGrid1.Rows[0].DataGridView.Columns[j].HeaderText.ToString() == "Gewicht")
                    {
                        afGrid1[j, i].Value = decDefault;
                    } 
                }
            }
        }
        //
        //---- Prüft of einer der CheckBoxen ausgewählt ist -----
        //
        private void cbVollstaendig_CheckedChanged(object sender, EventArgs e)
        {
            cbCheck(sender, e);
        }
        private void cbUnvollstaendig_CheckedChanged(object sender, EventArgs e)
        {
            cbCheck(sender, e);
        }
        private void cbCheck(object sender, EventArgs e)
        {
            if ((cbUnvollstaendig.Checked) | (cbVollstaendig.Checked))
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
        }
        //
        //----  Abbruch -------------
        //
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //************************************* Drag & Drop **************************************
        //  --  Grid Artikelvorlagen nach afGrd1 ( Eingabe Artikel )
        //
        //
         private void afGrid1_DragEnter(object sender, DragEventArgs e)
         {
             e.Effect = DragDropEffects.Copy;
         }
         private void grdArtVorlage_MouseDown(object sender, MouseEventArgs e)
         {
             if (e.Button == MouseButtons.Left)
             {

                 DataRowView view = (DataRowView)grdArtVorlage.CurrentRow.DataBoundItem;
                 if (view != null)
                 {
                     grdArtVorlage.DoDragDrop(view, DragDropEffects.Copy);
                 }
             
             }
         }
         private void afGrid1_DragDrop(object sender, DragEventArgs e)
         {
             try
             {

                 if (e.Data.GetDataPresent(typeof(DataRowView)))
                 {
                     Point p = afGrid1.PointToClient(new Point(e.X, e.Y));
                     //int i = afGrid1.

                     DataRowView drop = (DataRowView)e.Data.GetData(typeof(DataRowView));
                     int row = afGrid1.Rows.Count;

                     string g = drop["Gut"].ToString();
                     decimal d = Convert.ToDecimal(drop["Dicke"].ToString());
                     decimal b = Convert.ToDecimal(drop["Breite"].ToString());
                     decimal l = Convert.ToDecimal(drop["Laenge"].ToString());
                     decimal h = Convert.ToDecimal(drop["Hoehe"].ToString());

 /**                    afGrid1[row, 2].Value = g;
                     afGrid1[row, 3].Value = d;
                     afGrid1[row, 4].Value = b;
                     afGrid1[row, 5].Value = l;
                     afGrid1[row, 6].Value = h;

  * ***/
                     afGrid1.CurrentRow.Cells["Gut"].Value = g;
                     afGrid1.CurrentRow.Cells["Dicke"].Value = d;
                     afGrid1.CurrentRow.Cells["Breite"].Value = b;
                     afGrid1.CurrentRow.Cells["Laenge"].Value = l;
                     afGrid1.CurrentRow.Cells["Hoehe"].Value = h;

                     //afGrid1.Rows[row].Cells["Gut"].Value = Convert.ToDecimal();
                     //Gut.Selected =Convert.ToString(drop["GArt"].ToString());
                     // afGrid1.Rows[row].Cells["Dicke"].Value = Convert.ToDecimal(drop["Dicke"].ToString());
                     // afGrid1.Rows[row].Cells["Breite"].Value = Convert.ToDecimal(drop["Breite"].ToString());
                     // afGrid1.Rows[row].Cells["Länge"].Value = Convert.ToDecimal(drop["Laenge"].ToString());
                     // afGrid1.Rows[row].Cells["Höhe"].Value = Convert.ToDecimal(drop["Hoehe"].ToString());

                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
         }
        //
        //------ Grid Vorlage erscheint wenn die erste Celle im afGrid ausgewählt wurde ----
        //
         private void afGrid1_CellEnter(object sender, DataGridViewCellEventArgs e)
         {
             if (afGrid1[0,0].Selected)
             {
                 //MessageBox.Show("Celle 0-0");
                 grdArtVorlage.Visible = true;
             }
         }
 


    }
}

 

