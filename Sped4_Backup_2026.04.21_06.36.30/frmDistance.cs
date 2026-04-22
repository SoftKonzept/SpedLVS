using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sped4;
using LVS;

namespace Sped4
{
    public partial class frmDistance : frmTEMPLATE
    {
        internal DataTable dt = new DataTable("Entfernungen");
        public Globals._GL_USER GL_User;
        internal bool boUpdate = false;
        internal decimal DistanceID = 0.0M;
        public frmAuftrag_Fast _frmAuftragFast;

        
        public bool bo_AdrTakeOver = false;
        string strDistance = string.Empty;


        public frmDistance()
        {
            InitializeComponent();
        }
        //
        //--------- Close Form -------------
        //
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        //--------------- INIT FORM ------------
        //
        private void frmDistance_Load(object sender, EventArgs e)
        {
            //User zuweisen
            if (_frmAuftragFast != null)
            {
                GL_User = _frmAuftragFast._ctrMenu._frmMain.GL_User;
            }


            if (GL_User.User_ID > 0)
            {
                //ComboLandFüllen
                cbVonLand.DataSource = Enum.GetNames(typeof(Globals.enumLand));
                cbNachLand.DataSource = Enum.GetNames(typeof(Globals.enumLand));

                // init Form
                InitComboSObj();
                ReSetFrm();

                //Übernahme der Daten aus frmAuftragFast
                if (_frmAuftragFast != null)
                {
                    if (_frmAuftragFast.bo_AdrTakeOverForDistanceCenter)
                    {
                        EnableDistanceEingabe(true);
                        TakeOverDatenForNewDistance();
                    }
                }
            }
        }
        //
        //
        //
        private void TakeOverDatenForNewDistance()
        {   
            decimal decVAdrID=0.0M;
            decimal decEAdrID = 0.0M;
            string strV_PLZ = string.Empty;
            string strV_Ort = string.Empty;
            string strE_PLZ = string.Empty;
            string strE_Ort = string.Empty;
            string strVLand = string.Empty;
            string strELand = string.Empty;
            string strVADRSearch = string.Empty;
            string strEADRSearch = string.Empty;

            if (_frmAuftragFast != null)
            {
                strVADRSearch = _frmAuftragFast.tbSearchV.Text;
                strEADRSearch = _frmAuftragFast.tbSearchE.Text;
            }
            //Versender
            if (clsADR.ViewIDExists(this.GL_User, strVADRSearch))
            {
                decVAdrID = clsADR.GetIDByMatchcode(strVADRSearch);
                if (decVAdrID > 0)
                {
                    strV_PLZ = clsADR.GetPLZByID(this.GL_User, decVAdrID);
                    strV_Ort = clsADR.GetOrtByID(this.GL_User, decVAdrID);
                    strVLand = clsADR.GetLandByID(this.GL_User, decVAdrID);


                    tbVonPLZ.Text = strV_PLZ.Trim();
                    tbVonOrt.Text = strV_Ort.Trim();
                    cbVonLand.SelectedText = strVLand.Trim();
                }
            }
            //Empfänger
            if (clsADR.ViewIDExists(this.GL_User, strEADRSearch))
            {
                decEAdrID = clsADR.GetIDByMatchcode(strEADRSearch);
                if (decEAdrID > 0)
                {
                    strE_PLZ = clsADR.GetPLZByID(this.GL_User,decEAdrID);
                    strE_Ort = clsADR.GetOrtByID(this.GL_User,decEAdrID);
                    strELand = clsADR.GetLandByID(this.GL_User, decEAdrID);

                    tbNachPLZ.Text = strE_PLZ.Trim();
                    tbNachOrt.Text = strE_Ort.Trim();
                    cbNachLand.SelectedText= strELand.Trim();
                }
            }
            //Land





            clsDistance distance = new clsDistance();
            distance.GL_User = GL_User;
            distance.vPLZ = tbVonPLZ.Text;
            distance.vOrt = tbVonOrt.Text;
            distance.nOrt = tbNachOrt.Text;
            distance.nPLZ = tbNachPLZ.Text;
            distance.ExistDistance();
            if (distance.kmExist)
            {
                distance.GetDistance(distance.vPLZ, distance.vOrt, distance.nPLZ, distance.nOrt);
                tbKM.Text = distance.km.ToString();
            }
        }
        //
        //-------- Init Combobox zur Distance suche -----------
        //
        private void InitComboSObj()
        {
            string[] strArray = {"von PLZ",
                                    "von Ort",
                                    "nach PLZ",
                                    "nach Ort"            
                                    };
            cbSObj.DataSource = strArray;
            cbSObj.SelectedItem = "von PLZ";
        }
        //
        //------- DGV Distance wird initialisiert --------------
        //
        private void InitDGV()
        {
            dt.Clear();
            dt = clsDistance.GetAllDistance(GL_User.User_ID);
            SetDGVDataSource(dt);
        }
        //
        //
        private void SetDGVDataSource(DataTable dtSource)
        {
            dgv.DataSource = null;
            dgv.DataSource = dtSource;
            dgv.Columns["ID"].Visible = false;
        }
        //
        //
        //
        private void ReSetFrm()
        { 
            EnableDistanceEingabe(false);
            boUpdate = false;
            DistanceID = 0.0M;
            tbVonPLZ.Text = string.Empty;
            tbVonOrt.Text = string.Empty;
            tbNachPLZ.Text = string.Empty;
            tbNachOrt.Text = string.Empty;
            tbKM.Text = "0";
        }
        //
        //
        //
        private void EnableDistanceEingabe(bool boEnable)
        {
            tbNachOrt.Enabled = boEnable;
            tbNachPLZ.Enabled = boEnable;
            tbVonOrt.Enabled = boEnable;
            tbVonPLZ.Enabled = boEnable;
            tbKM.Enabled = boEnable;
        }
        //
        //----------- Save / Update -------------
        //
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            tbVonPLZ.Text = tbVonPLZ.Text.Trim();
            tbVonOrt.Text = tbVonOrt.Text.Trim();
            tbNachPLZ.Text = tbNachPLZ.Text.Trim();
            tbNachOrt.Text = tbNachOrt.Text.Trim();
            tbKM.Text = tbKM.Text.Trim();

            if (CheckDinstanceEingabe())
            {                
                AssignValue();

                //Grid neu laden
                InitDGV();
                //Nach dem Speichern soll der neue 
                //Datensatz als Auswahl im Grid stehen
                SearchInsertetDisantceInTable();
                
                ReSetFrm();
             }
        }
        //
        //--------- New Distance -------------------------
        //
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SetFrmForNewItem();
        }
        //
        //
        //
        private void SetFrmForNewItem()
        {
            EnableDistanceEingabe(true);
            boUpdate = false;
        }
        //
        //----- prüfen der Eingaben - es müssen alle Eingaben vorhanden sein --------
        //
        private bool CheckDinstanceEingabe()
        {
            bool boOK = true;
            string strMess = "Folgende Angaben fehlen oder sind fehlerhaft:" + Environment.NewLine;

            if (tbVonPLZ.Text == string.Empty)
            {
                boOK = false;
                strMess = strMess + "Von PLZ fehlt" + Environment.NewLine;

            }
            if (tbVonOrt.Text == string.Empty)
            {
                boOK = false;
                strMess = strMess + "Von Ort fehlt" + Environment.NewLine;

            }
            if (tbNachPLZ.Text == string.Empty)
            {
                boOK = false;
                strMess = strMess + "Nach PLZ fehlt" + Environment.NewLine;

            }
            if (tbNachOrt.Text == string.Empty)
            {
                boOK = false;
                strMess = strMess + "Nach Ort fehlt" + Environment.NewLine;

            }
            if (tbKM.Text == string.Empty)
            {
                boOK = false;
                strMess = strMess + "km fehlt" + Environment.NewLine;
            }
            else
            {
                if (!Functions.CheckNum(tbKM.Text))
                {
                    boOK = false;
                    strMess = strMess + "km - Angabe muss ein Zahlenwert sein" + Environment.NewLine;
                }
                else
                {
                    Int32 ikm = 0;
                    if (Int32.TryParse(tbKM.Text, out ikm))
                    {
                        if (ikm < 1)
                        {
                            boOK = false;
                            strMess = strMess + "km - Angabe muss > 0 sein" + Environment.NewLine;
                        }
                    }
                }
            }

            if (!boOK)
            {
                clsMessages.Allgemein_FehlerDistanceEingabe(strMess);
            }

            return boOK;
        }
        //
        //
        //
        private void AssignValue()
        {
            clsDistance distance = new clsDistance();
            distance.GL_User = GL_User;
            distance.vPLZ = tbVonPLZ.Text;
            distance.vOrt = tbVonOrt.Text;
            distance.vLand = cbVonLand.SelectedValue.ToString();
            distance.nOrt = tbNachOrt.Text;
            distance.nPLZ = tbNachPLZ.Text;
            distance.nLand = cbNachLand.SelectedValue.ToString();

            Int32 iTmp = 0;
            if (!Int32.TryParse(tbKM.Text, out iTmp))
            {
                iTmp = 0;
            }
            distance.km = iTmp;

            if (distance.km > 0)
            {
                distance.ExistDistance();

                if (!boUpdate)
                {
                    if (!distance.kmExist)
                    {
                        distance.AddDistance();
                    }
                    else
                    {
                        distance.UpdateDistance();
                    }
                }
                else
                {
                    if (!distance.kmExist)
                    {
                        distance.ID = DistanceID;
                        if (distance.ID > 0)
                        {
                            distance.UpdateDistance();
                        }
                    }
                }
            }
        }
        //
        //------------ Übernahme der Daten aus Datagrid ------------
        //
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            boUpdate = true;
            EnableDistanceEingabe(true);
            //Distance ID setzen aus DGV
            DistanceID = (decimal)this.dgv.Rows[e.RowIndex].Cells["ID"].Value;
            tbKM.Text = this.dgv.Rows[e.RowIndex].Cells["km"].Value.ToString();
            tbVonPLZ.Text = this.dgv.Rows[e.RowIndex].Cells["vPLZ"].Value.ToString();
            tbVonOrt.Text = this.dgv.Rows[e.RowIndex].Cells["vOrt"].Value.ToString();
            cbVonLand.SelectedValue = this.dgv.Rows[e.RowIndex].Cells["vLand"].Value.ToString();
            tbNachPLZ.Text = this.dgv.Rows[e.RowIndex].Cells["nPLZ"].Value.ToString();
            tbNachOrt.Text = this.dgv.Rows[e.RowIndex].Cells["nOrt"].Value.ToString();
            cbNachLand.SelectedValue = this.dgv.Rows[e.RowIndex].Cells["nLand"].Value.ToString();
        }
        //
        //----------------- löscht den gewählten Datensatz -------------
        //
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
           // clsDistance.DeleteDistanceByID(DistanceID, GL_User);
           // ReSetFrm();
        }
        //
        //---------------- Search im DGV -----------------
        //
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string strCol = string.Empty;
            string strCBSelected=cbSObj.SelectedValue.ToString();      
            string strSearchText = tbSearch.Text;

            if (strSearchText == string.Empty)
            {
                InitDGV();
            }
            else
            {
                switch (strCBSelected)
                {
                    case "von PLZ":
                        strCol = "vPLZ";
                        break;

                    case "von Ort":
                        strCol = "vOrt";
                        break;

                    case "nach PLZ":
                        strCol = "nPLZ";
                        break;

                    case "nach Ort":
                        strCol = "nOrt";
                        break;
                }

                DataTable dtTmp = new DataTable();
                dtTmp = dt.Clone();
                dtTmp = Functions.SearchInDataTableByFilter(strCol, strSearchText, dt);
                dgv.DataSource = dtTmp;
            }

        }
        //
        private void SearchInsertetDisantceInTable()
        {                   
            if(
                (tbVonPLZ.Text != string.Empty) &
                (tbVonOrt.Text != string.Empty) &
                (tbNachPLZ.Text != string.Empty) &
                (tbNachOrt.Text != string.Empty) &
                (tbKM.Text != string.Empty)                  
               )
            {
                string strSearch="vPLZ='"+tbVonPLZ.Text+"' AND "+
                                     "vOrt='"+tbVonOrt.Text+"' AND "+
                                     "nPLZ='" + tbNachPLZ.Text + "' AND " +
                                     "nOrt='" + tbNachOrt.Text+"'";

                DataView dvSView = new DataView(dt);
                dvSView.RowFilter = strSearch;
                SetDGVDataSource(dvSView.ToTable());
            }
        }
        //
        //
        //
        private void cbSObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitDGV();
        }
        //
        //Selected Distance übernehmen
        //
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            if (_frmAuftragFast != null)
            { 
                //this._frmAuftragFast.tbEntfernung.Text = strDistance;
            }            
            this.Close();
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgv.CurrentRow != null)
            {
                strDistance = this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["KM"].Value.ToString();
            }
        }
        //
        //
        //
        private void tsbtnRoute_Click(object sender, EventArgs e)
        {
            if ((tbVonOrt.Text != string.Empty) || (tbNachOrt.Text != string.Empty))
            {
                //Chech vOrt und nOrt müssen ausgefüllt sein
                clsDistance route = new clsDistance();
                route.GL_User = this.GL_User;

                route.vPLZ = tbVonPLZ.ToString();
                route.vOrt = tbVonOrt.ToString();
                route.vLand = cbVonLand.SelectedText.ToString();

                route.nPLZ = tbNachPLZ.Text;
                route.nOrt = tbNachOrt.Text;
                route.nLand = cbNachLand.SelectedText.ToString();


                route.GetGMapDistance();
                //Ausgabe
                tbKM.Text = route.km.ToString();
            }

        }
    }
}
