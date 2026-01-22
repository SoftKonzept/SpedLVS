using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Sped4;
using Sped4.Classes;

namespace Sped4
{
    public partial class frmKontakte : Form
    {
      public Globals._GL_USER GL_User;
      public ctrMenu _ctrMenu;
      public bool eingabeOK = true;
      DataTable konTable = new DataTable();
        private string KonViewID;
        private decimal ADR_ID;             // Schlüssel zu ADR
        private decimal Kontakt_ID;         // ID des PK in DB Kontakte

        private Int32 curRow = 0;

        public frmKontakte()
        {
            InitializeComponent();
            ADR_ID = 0;
            Kontakt_ID = 0;
            KonViewID = "";
            btn1.Text = "Speichern";
        }

        //**********************************************************************
        //------------          Methoden
        //**********************************************************************
        //    
        //-------------------------------- Clean Form  ----------------------
        //
        public void CleanKonForm()
        {
            tbAbt.Text = string.Empty;
            tbAP.Text = string.Empty;
            tbFax.Text = string.Empty;
            tbMail.Text = string.Empty;
            tbTel.Text = string.Empty;
            tbInfo.Text = string.Empty;
            txtSearch.Text = string.Empty;
        }
        //
        //------------------------------ init Kontaktliste ---------------------
        //
        public void initGrdKontakt(decimal _ADR_ID, string _ViewID)
        {
            ADR_ID = _ADR_ID;
            KonViewID = _ViewID;

            GetKontaktADR();
            
            tbAbt.Visible = true;
            tbAP.Visible = true;
            tbTel.Visible = true;
            tbFax.Visible = true;
            tbMail.Visible = true;
            tbInfo.Visible = true;     
            try
            {
                //--- Initialisierung der Connection ------------------
                //+++ Falls die Abfrage geändert wird muss entsprechend auch upRecordTakeOver geändert werden
                
               
                konTable.Clear();
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();

                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT " +
                                                "ID, " +
                                                "ViewID as 'Suchbegriff', " +
                                                "Abteilung, " +
                                                "Ansprechpartner, " +
                                                "Telefon, " +
                                                "Fax, " +
                                                "Mail, " +
                                                "Info " +
                                                          "FROM Kontakte WHERE ADR_ID='"+ADR_ID+"' ORDER BY Abteilung"; //noch ändern

                ada.Fill(konTable);
                ada.Dispose();

                Command.Dispose();
                grdKontakte.DataSource = konTable;
                grdKontakte.Columns["ID"].Visible = false;
                grdKontakte.Columns["Suchbegriff"].Visible = false;
                //KonViewID = grdKontakte["Suchbegriff", 2].Value.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void GetKontaktADR()
        {
          string kontaktName = string.Empty;
          string kontaktStr = string.Empty;
          string kontaktOrt = string.Empty;

          DataSet ds = clsADR.ReadADRbyID(ADR_ID);

          kontaktName = ds.Tables[0].Rows[0]["Name1"].ToString();
          kontaktStr = ds.Tables[0].Rows[0]["Str"].ToString();
          kontaktOrt = ds.Tables[0].Rows[0]["PLZ"].ToString() + " - " + ds.Tables[0].Rows[0]["Ort"].ToString(); ;

          lKontaktname.Text = "Kontakte von : ";
          tbKontaktADR.Text = kontaktName + " \r\n" +
                              kontaktStr + "\r\n" +
                              kontaktOrt;
        
        }
        //
        //------------------------- Abbruch / Beenden  ------------------------
        //
        private void btnAbbruch_Click(object sender, EventArgs e)
        {
            ADR_ID = -1;            // ADR_ID wird wieder neutralisiert
            KonViewID = "";
            this.Close();
        }
        //
        //------------------- Eintrag der Kontakte in DB ----------------------
        //
        private void btn1_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {            
                AssignVar(btn1.Text.ToString());
                //-- Eingabemaske auf Null setzen --
                CleanKonForm();
                initGrdKontakt(ADR_ID, KonViewID);
            }
        }
        //
        //--------------------------------- Assign Value --------------------
        //
        private void AssignVar(String action)
        {
            clsKontakte Kontakt = new clsKontakte();
            Kontakt.BenutzerID = GL_User.User_ID;

            //--------- Leerzeichen werden abgeschnitten
            tbAbt.Text = tbAbt.Text.Trim();
            tbAP.Text = tbAP.Text.Trim();
            tbTel.Text = tbTel.Text.Trim();
            tbFax.Text = tbFax.Text.Trim();
            tbMail.Text = tbMail.Text.Trim();
            tbInfo.Text=tbInfo.Text.Trim();

            //---------- Zusweisung der Werte
            Kontakt.ViewID = KonViewID;
            Kontakt.ADR_ID = ADR_ID;
            Kontakt.Mail = tbMail.Text;
            Kontakt.Ansprechpartner = tbAP.Text;
            Kontakt.Abteilung = tbAbt.Text;
            Kontakt.Telefon = tbTel.Text;
            Kontakt.Fax = tbFax.Text;
            Kontakt.Info = tbInfo.Text;

            if (action == "Speichern")
            {
                // --- Eintrag in DB ----
                Kontakt.Add();
            }
            else
            {
                //---- Update Datensatz in DB ---
                //ADR.updateADR(ID);
                Kontakt.updateKontakt(Kontakt_ID);
                btn1.Text = "Speichern";
                //this.Close();  // hier nicht, da Liste und Form in einem
            }
        }
        //
        //--------------------- Check der Inputdaten ------------------
        //
        private bool CheckInput()
        {
            string strHelp;
            eingabeOK = true;
            strHelp="Folgende Eingaben sind nicht korrekt:\n";
            char[] ad   = {'@'};
            char[] tele = {'0','1','2','3','4','5','6','7','8','9','-','/'};
            char[] num  = {'0','1','2','3','4','5','6','7','8','9'};
            char[] bst  = { 'a', 'A', 'b', 'c', 'C', 'd', 'D', 'e', 'E', 'F', 'f', 'g', 'G', 'h', 'H', 'i', 'I', 'j', 'J', 'k', 'K', 'l', 'L', 'm', 'M', 'n', 'N', 'o', 'O', 'p', 'P', 'q', 'Q', 'r', 'R', 's', 'S', 't', 'T', 'u', 'U', 'v', 'V', 'w', 'W', 'x', 'X', 'y', 'Y', 'z', 'Z' };
            char[] Uml  = { 'ä', 'Ä', 'ö', 'Ö', 'ü', 'Ü' };

            //Abteilung
            if (tbAbt.Text == "")
            {
              strHelp = strHelp + "Abteiung wurde nicht ausgefüllt \n";
              eingabeOK = false;
            }
            //Ansprechpartner
            if (tbAP.Text == "")
            {
              strHelp = strHelp + "Ansprechpartner wurde nicht ausgefüllt \n";
              eingabeOK = false;
            }
            //Telefon
            if (tbTel.Text != "")
            {
              if (tbTel.Text.ToString().IndexOfAny(bst) != -1)
              {
                strHelp = strHelp + "Telefonnummer enthält Buchstaben \n";
                eingabeOK = false;
              }
            }
            // Fax
            if (tbFax.Text !="")
            {
                if (tbFax.Text.ToString().IndexOfAny(bst)!= -1) 
                {
                  strHelp = strHelp + "Fax enthält Buchstaben \n";
                  eingabeOK = false;
                }
            }
            //--- Mail kein Pflichtfeld, dennoch prüfen auf korrekte Eingabe
            if (tbMail.Text != "")
            {
              if (tbMail.Text.ToString().IndexOfAny(ad) == -1)
              {
                strHelp = strHelp + "E-Mail beinhaltet kein '@' \n";
                eingabeOK = false;
              }

              if (tbMail.Text.ToString().IndexOfAny(Uml) != -1)
              {
                strHelp = strHelp + "E-Mail beinhaltet Umlaute \n";
                eingabeOK = false;
              }
            }
                                            
            if (!eingabeOK)
            {
                MessageBox.Show(strHelp);
            }
            return eingabeOK;
        }
        //
        //--------------- update gewählter Datensatz ---------------------------
        //
        private void grdKontakte_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (grdKontakte.Rows.Count >= 1)
            {
                btn1.Text = "Update";   // aus Save-Button wird Update-Button
                //---------- ID des gewählten Datensatzes-----
                Kontakt_ID =Convert.ToInt32(grdKontakte.Rows[grdKontakte.CurrentRow.Index].Cells[0].Value.ToString());
                ReadDataByID(Kontakt_ID);
                initGrdKontakt(ADR_ID, KonViewID);  
            }
        }
        //
        //--------------------- Read Kontakte  ---------------
        //
        public void ReadDataByID(decimal _Kontakt_ID)
        {
            ButtonChange();
            Kontakt_ID = _Kontakt_ID;


            DataTable dataTable = new DataTable();
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();

                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT * FROM Kontakte WHERE ID='" + Kontakt_ID + "'";

                DataSet ds = new DataSet();

                ada.Fill(ds);
                ada.Dispose();
                Command.Dispose();
                SetUpdateRecord(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Globals.SQLcon.Close();
        }
        //
        //----------------------setzt den zu ändernen Datensatz  --------------
        //
        private void SetUpdateRecord(DataSet ds)
        {
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lKontaktname.Text = ds.Tables[0].Rows[i]["ViewID"].ToString();
                ADR_ID =Convert.ToInt32(ds.Tables[0].Rows[i]["ADR_ID"].ToString());
                tbAbt.Text = ds.Tables[0].Rows[i]["Abteilung"].ToString();
                tbAP.Text = ds.Tables[0].Rows[i]["Ansprechpartner"].ToString();
                tbMail.Text = ds.Tables[0].Rows[i]["Mail"].ToString();
                tbTel.Text = ds.Tables[0].Rows[i]["Telefon"].ToString();
                tbFax.Text = ds.Tables[0].Rows[i]["Fax"].ToString();
                tbInfo.Text = ds.Tables[0].Rows[i]["Info"].ToString();
            }
        }
        //
        //
        //
        private void ButtonChange()
        {
            btn1.Text="Update";
        }
        //***************************************** SEARCH ******************************************
        //
        //----------------  Text Search   ---------------------
        //
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchGrd(tbTextSearch.Text.ToUpper());

        }
        //------------------------ Search Methode for the Grid -----------------------
        //
        public void SearchGrd(string Search)
        {
            Int32 _Column = 0;
            Int32 _Row = 0;

            //maxSearches = the # of cells in the grid
            Int32 maxSearches = grdKontakte.Rows.Count * grdKontakte.Columns.Count + 1;
            //int maxSearches = dataGridView1.Rows.Count * dataGridView1.Columns.Count + 1;
            Int32 idx = 1;
            bool isFound = false;

            if (Convert.ToBoolean(Search.Length))
            {
                // If the item is not found and you haven't looked at every cell, keep searching
                while ((!isFound) & (idx < maxSearches))
                {
                    // Only search visible cells
                    if (grdKontakte.Columns[_Column].Visible)
                    {
                        // Do all comparing in UpperCase so it is case insensitive
                        if (grdKontakte[_Column, _Row].Value.ToString().ToUpper().Contains(Search))
                        {
                            // If found position on the item
                            grdKontakte.FirstDisplayedScrollingRowIndex = _Row;
                            grdKontakte[_Column, _Row].Selected = true;
                            isFound = true;
                        }
                    }

                    // Increment the column.
                    _Column++;

                    // If it exceeds the column count
                    if (_Column == grdKontakte.Columns.Count)
                    {
                        _Column = 0; //Go to 0 column
                        _Row++;      //Go to the next row

                        // If it exceeds the row count
                        if (_Row == grdKontakte.Rows.Count)
                        {
                            _Row = 0; //Start over at the top
                        }
                    }

                }
            }
        }
        //
        //---------------- löscht ausgwählten Kontakteintrag  --------------
        //
        private void tsbDelete_Click(object sender, EventArgs e)
        {      
          //
          if (this.grdKontakte.Rows.Count >= 1)
          {
              Kontakt_ID = Convert.ToInt32(this.grdKontakte.Rows[curRow].Cells["ID"].Value.ToString());
              if (Kontakt_ID > 0)
              {
                if (clsMessages.Kontakte_KontaktDelete())
                {

                  clsKontakte Kon = new clsKontakte();
                  Kon.BenutzerID = GL_User.User_ID;
                  Kon.ID = Kontakt_ID;
                  Kon.DeleteKontaktEintrag();
                  initGrdKontakt(ADR_ID, KonViewID);
                }
              }            
          }
        }
        //
        //---------- Datagridview Error Event
        //
        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {

            MessageBox.Show("Error happened " + anError.Context.ToString());

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }
        }

        //
        //
        //
        private void grdKontakte_SelectionChanged(object sender, EventArgs e)
        {
         // curRow = this.grdKontakte.CurrentRow.Index;
        }
        //
        //------------- Excelexport Kontakte --------------------
        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            clsExcel excel = new clsExcel();
            excel.ExportDataTableToExcel(ref this._ctrMenu._frmMain, konTable, "Kontakte");
        }
    }
}
