using LVS;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrDistance : UserControl
    {
        internal DataTable dt = new DataTable("Entfernungen");
        public Globals._GL_USER GL_User;
        internal ctrMenu _ctrMenu;
        internal bool boUpdate = false;
        private DataTable dtDistance;
        public frmAuftrag_Fast _frmAuftragFast;
        //public frmFrachtBerechnung _frmFrachtBerechnung;
        internal clsDistance Distance = new clsDistance();

        public delegate void ctrDistanceCloseEventHandler();
        public event ctrDistanceCloseEventHandler ctrDistanceClose;

        public bool bo_AdrTakeOver = false;
        string strDistance = string.Empty;


        ///<summary>ctrDistance / ctrDistance</summary>
        ///<remarks></remarks>
        public ctrDistance()
        {
            InitializeComponent();
        }
        ///<summary>ctrDistance / toolStripButton5_Click</summary>
        ///<remarks>Close Form </remarks>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            // this.Close();
        }
        ///<summary>ctrDistance / frmDistance_Load</summary>
        ///<remarks>INIT FORM</remarks>
        private void frmDistance_Load(object sender, EventArgs e)
        {
            //User zuweisen
            if (_frmAuftragFast != null)
            {
                GL_User = _frmAuftragFast._ctrMenu._frmMain.GL_User; ;
            }

            if (GL_User.User_ID > 0)
            {
                //ComboLandFüllen
                cbVonLand.DataSource = Enum.GetNames(typeof(enumLand));
                cbNachLand.DataSource = Enum.GetNames(typeof(enumLand));

                // init Form
                InitComboSObj();
                ReSetFrm();
                InitDGV();

                //Übernahme der Daten aus frmAuftragFast
                if (_frmAuftragFast != null)
                {
                    if (_frmAuftragFast.bo_AdrTakeOverForDistanceCenter)
                    {
                        EnableDistanceEingabe(true);
                        this.tcDistance.SelectedTab = tabPageDistance;
                        TakeOverDatenForNewDistance();
                    }
                    else
                    {
                        this.tcDistance.SelectedTab = tabPageList;
                    }
                }
            }
        }
        ///<summary>ctrDistance / TakeOverDatenForNewDistance</summary>
        ///<remarks>/remarks>
        private void TakeOverDatenForNewDistance()
        {
            decimal decVAdrID = 0.0M;
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
                    strE_PLZ = clsADR.GetPLZByID(this.GL_User, decEAdrID);
                    strE_Ort = clsADR.GetOrtByID(this.GL_User, decEAdrID);
                    strELand = clsADR.GetLandByID(this.GL_User, decEAdrID);

                    tbNachPLZ.Text = strE_PLZ.Trim();
                    tbNachOrt.Text = strE_Ort.Trim();
                    cbNachLand.SelectedText = strELand.Trim();
                }
            }
            //Land

            Distance.GL_User = GL_User;
            Distance.vPLZ = tbVonPLZ.Text;
            Distance.vOrt = tbVonOrt.Text;
            Distance.nOrt = tbNachOrt.Text;
            Distance.nPLZ = tbNachPLZ.Text;
            Distance.ExistDistance();
            if (Distance.kmExist)
            {
                Distance.GetDistance(Distance.vPLZ, Distance.vOrt, Distance.nPLZ, Distance.nOrt);
                tbKM.Text = Distance.km.ToString();
            }
        }
        ///<summary>ctrDistance / TakeOverDatenForNewDistance</summary>
        ///<remarks>Init Combobox zur Distance suche</remarks>
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
        ///<summary>ctrDistance / TakeOverDatenForNewDistance</summary>
        ///<remarks>DGV Distance wird initialisiert</remarks>
        private void InitDGV()
        {
            dtDistance = new DataTable();
            dtDistance = clsDistance.GetAllDistance(GL_User.User_ID);
            //SetDGVDataSource(dt);
            this.dgv.DataSource = dtDistance.DefaultView;
            for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
            {
                string strColName = this.dgv.Columns[i].Name;
                switch (strColName)
                {
                    case "ID":
                        this.dgv.Columns[i].IsVisible = false;
                        break;
                    default:
                        this.dgv.Columns[i].IsVisible = true;
                        break;
                }
            }
            this.dgv.BestFitColumns();
        }
        ///<summary>ctrDistance / SetDGVDataSource</summary>
        ///<remarks></remarks>
        private void SetDGVDataSource(DataTable dtSource)
        {
            dgv.DataSource = null;
            dgv.DataSource = dtSource;
            //dgv.Columns["ID"].Visible = false;
        }
        ///<summary>ctrDistance / ReSetFrm</summary>
        ///<remarks></remarks>
        private void ReSetFrm()
        {
            EnableDistanceEingabe(false);
            boUpdate = false;
            Distance.ID = 0;
            tbVonPLZ.Text = string.Empty;
            tbVonOrt.Text = string.Empty;
            tbNachPLZ.Text = string.Empty;
            tbNachOrt.Text = string.Empty;
            tbKM.Text = "0";
            tbSearch.Text = string.Empty;
            pbGMapsInfo.Image = null;
        }
        ///<summary>ctrDistance / EnableDistanceEingabe</summary>
        ///<remarks></remarks>
        private void EnableDistanceEingabe(bool boEnable)
        {
            tbNachOrt.Enabled = boEnable;
            tbNachPLZ.Enabled = boEnable;
            tbVonOrt.Enabled = boEnable;
            tbVonPLZ.Enabled = boEnable;
            tbKM.Enabled = boEnable;
        }
        ///<summary>ctrDistance / EnableDistanceEingabe</summary>
        ///<remarks>Save / Update</remarks>
        private void tsbtnSave_Click(object sender, EventArgs e)
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
                //CompositeFilterDescriptor compositeFilter = new CompositeFilterDescriptor();
                //compositeFilter.FilterDescriptors.Add(new FilterDescriptor("ID", FilterOperator.IsEqualTo, Distance.ID.ToString()));
                //this.dgv.FilterDescriptors.Clear();
                //this.dgv.FilterDescriptors.Add(compositeFilter);
                SetFilterforDGV(ref this.dgv, "ID", Distance.ID.ToString(), false);
                this.tcDistance.SelectedTab = tabPageList;
            }
        }
        ///<summary>ctrDistance / EnableDistanceEingabe</summary>
        ///<remarks>New Distance</remarks>
        private void tsbtnNew_Click(object sender, EventArgs e)
        {
            SetFrmForNewItem();
        }
        ///<summary>ctrDistance / SetFrmForNewItem</summary>
        ///<remarks></remarks>
        private void SetFrmForNewItem()
        {
            EnableDistanceEingabe(true);
            boUpdate = false;
        }
        ///<summary>ctrDistance / SetFrmForNewItem</summary>
        ///<remarks>prüfen der Eingaben - es müssen alle Eingaben vorhanden sein </remarks>
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
        ///<summary>ctrDistance / AssignValue</summary>
        ///<remarks></remarks>
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
            distance.IsgMaps = false;

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
                        distance.ID = Distance.ID;
                        if (distance.ID > 0)
                        {
                            distance.UpdateDistance();
                        }
                    }
                }
                Distance.ID = distance.ID;
                Distance.FillByID();
            }
        }
        ///<summary>ctrDistance / dgv_CellClick</summary>
        ///<remarks>Übernahme der Daten aus Datagrid </remarks>
        private void dgv_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                decimal decTmp = 0;
                Decimal.TryParse(this.dgv.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    Distance.ID = decTmp;
                    Distance.FillByID();
                }
            }
        }
        ///<summary>ctrDistance / dgv_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgv_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (this.Distance.ID > 0)
            {
                boUpdate = true;
                EnableDistanceEingabe(true);
                SetClsDistanceValueToFrm();
            }
        }
        /////<summary>ctrDistance / dgv_CellDoubleClick</summary>
        /////<remarks>Übernahme der Daten aus Datagrid </remarks>
        //private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    boUpdate = true;
        //    EnableDistanceEingabe(true);
        //    DistanceID = (decimal)this.dgv.Rows[e.RowIndex].Cells["ID"].Value;
        //    SetClsDistanceValueToFrm();
        //}
        ///<summary>ctrDistance / SetClsDistanceValueToFrm</summary>
        ///<remarks> </remarks>
        private void SetClsDistanceValueToFrm()
        {
            //Distance.ID = DistanceID;
            //Distance.FillByID();
            tbKM.Text = Distance.km.ToString();
            tbVonPLZ.Text = Distance.vPLZ;
            tbVonOrt.Text = Distance.vOrt;
            Functions.SetComboToSelecetedItem(ref cbVonLand, Distance.vLand);
            tbNachPLZ.Text = Distance.nPLZ;
            tbNachOrt.Text = Distance.nOrt;
            Functions.SetComboToSelecetedItem(ref cbNachLand, Distance.nLand);
            SetGMapsInfoImage(Distance.IsgMaps);
        }
        ///<summary>ctrDistance / SetClsDistanceValueToFrm</summary>
        ///<remarks>löscht den gewählten Datensatz  </remarks>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            Distance.DeleteDistanceByID();
            ReSetFrm();
            InitDGV();
        }
        ///<summary>ctrDistance / SetClsDistanceValueToFrm</summary>
        ///<remarks>Search im DGV</remarks>
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            SetFilterforDGV(ref this.dgv, cbSObj.SelectedValue.ToString(), tbSearch.Text, (tbSearch.Text == string.Empty));
        }
        ///<summary>ctrAufträge / SetFilterforDGV</summary>
        ///<remarks></remarks>
        public void SetFilterforDGV(ref RadGridView myDGV, string mySelCol, string mySearchText, bool bClearFilter)
        {
            myDGV.EnableFiltering = true;
            myDGV.FilterDescriptors.Clear();
            string strFilter = string.Empty;
            if (!bClearFilter)
            {
                string strCol = string.Empty;
                CompositeFilterDescriptor compositeFilter = new CompositeFilterDescriptor();
                switch (mySelCol)
                {
                    case "von PLZ":
                        strCol = "vPLZ";
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor(strCol, FilterOperator.StartsWith, mySearchText));
                        break;

                    case "von Ort":
                        strCol = "vOrt";
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor(strCol, FilterOperator.StartsWith, mySearchText));
                        break;

                    case "nach PLZ":
                        strCol = "nPLZ";
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor(strCol, FilterOperator.StartsWith, mySearchText));
                        break;

                    case "nach Ort":
                        strCol = "nOrt";
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor(strCol, FilterOperator.StartsWith, mySearchText));
                        break;
                    case "ID":
                        strCol = "ID";
                        decimal decTmp = 0;
                        decimal.TryParse(mySearchText, out decTmp);
                        compositeFilter.FilterDescriptors.Add(new FilterDescriptor(strCol, FilterOperator.IsEqualTo, decTmp));
                        break;
                }
                myDGV.FilterDescriptors.Add(compositeFilter);
            }
            else
            {
                myDGV.FilterDescriptors.Clear();
            }
        }
        ///<summary>ctrDistance / SearchInsertetDisantceInTable</summary>
        ///<remarks></remarks>
        private void SearchInsertetDisantceInTable()
        {
            if (
                (tbVonPLZ.Text != string.Empty) &
                (tbVonOrt.Text != string.Empty) &
                (tbNachPLZ.Text != string.Empty) &
                (tbNachOrt.Text != string.Empty) &
                (tbKM.Text != string.Empty)
               )
            {
                string strSearch = "vPLZ='" + tbVonPLZ.Text + "' AND " +
                                     "vOrt='" + tbVonOrt.Text + "' AND " +
                                     "nPLZ='" + tbNachPLZ.Text + "' AND " +
                                     "nOrt='" + tbNachOrt.Text + "'";

                DataView dvSView = new DataView(dt);
                dvSView.RowFilter = strSearch;
                SetDGVDataSource(dvSView.ToTable());
            }
        }
        ///<summary>ctrDistance / cbSObj_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void cbSObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitDGV();
        }
        ///<summary>ctrDistance / toolStripButton2_Click_1</summary>
        ///<remarks>Selected Distance übernehmen</remarks>
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {

            if (_frmAuftragFast != null)
            {
                //this._frmAuftragFast.tbEntfernung.Text = strDistance;
            }
            //Baustelle
            //this.Close();
        }
        ///<summary>ctrDistance / dgv_SelectionChanged</summary>
        ///<remarks></remarks>
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgv.CurrentRow != null)
            {
                strDistance = this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["KM"].Value.ToString();
            }
        }
        ///<summary>ctrDistance / tsbtnRoute_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRoute_Click(object sender, EventArgs e)
        {
            this.webGMaps.Refresh();
            if ((tbVonOrt.Text != string.Empty) || (tbNachOrt.Text != string.Empty))
            {
                //Chech vOrt und nOrt müssen ausgefüllt sein
                clsDistance route = new clsDistance();
                route.GL_User = this.GL_User;

                route.vPLZ = tbVonPLZ.Text;
                route.vOrt = tbVonOrt.Text;
                route.vLand = cbVonLand.SelectedItem.ToString();

                route.nPLZ = tbNachPLZ.Text;
                route.nOrt = tbNachOrt.Text;
                route.nLand = cbNachLand.SelectedItem.ToString();


                route.GetGMapDistance();
                //Ausgabe
                tbKM.Text = route.km.ToString();
                SetGMapsInfoImage(route.IsgMaps);
                this.webGMaps.Url = route.gMapsBrowserLink; ;
                this.webGMaps.Refresh();
            }

        }
        ///<summary>ctrDistance / SetGMapsInfoImage</summary>
        ///<remarks></remarks>
        private void SetGMapsInfoImage(bool myIsGMaps)
        {
            if (myIsGMaps)
            {
                pbGMapsInfo.Image = Sped4.Properties.Resources.signal_flag_checkered;
                pbGMapsInfo.Tag = "km aus Enfernungsermittlung!";
            }
            else
            {
                pbGMapsInfo.Image = null;
                pbGMapsInfo.Tag = string.Empty;
            }
        }
        ///<summary>ctrDistance / toolStripButton1_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ReSetFrm();
            InitDGV();
        }
        ///<summary>ctrDistance / tsbtnKmTakeOver_Click</summary>
        ///<remarks></remarks>
        private void tsbtnKmTakeOver_Click(object sender, EventArgs e)
        {
            if (_frmAuftragFast != null)
            {
                this._frmAuftragFast.tbEntfernung.Text = tbKM.Text; ;
            }
            this.ctrDistanceClose();
        }




    }
}
