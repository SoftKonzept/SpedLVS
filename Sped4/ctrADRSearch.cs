using LVS;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrADRSearch : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        public frmADRSearch _frmADRSearch;
        internal clsADR ADR;
        public DataTable adrList = new DataTable();
        public DataTable tempTable = new DataTable();
        public Int32 iAdrRange = -1;
        public Int32 ADRSearchButton = 0;
        public delegate void ADRTakeOverEventHandler(decimal TakeOverID);
        public event ADRTakeOverEventHandler getADRTakeOver;

        /********************************************************************************
         * 
         * *****************************************************************************/
        ///<summary>ctrADRSearch / ctrADRSearch</summary>
        ///<remarks></remarks>
        public ctrADRSearch()
        {
            InitializeComponent();
        }
        ///<summary>ctrADRSearch / ctrADRSearch_Load</summary>
        ///<remarks></remarks>
        private void ctrADRSearch_Load(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListeKomplett;
        }
        ///<summary>ctrADRSearch / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr()
        {
            InitDGV();
        }
        ///<summary>ctrADRSearch / Copy</summary>
        ///<remarks></remarks>
        public ctrADRSearch Copy()
        {
            return (ctrADRSearch)this.MemberwiseClone();
        }
        ///<summary>ctrADRSearch / ReadADRDataTable</summary>
        ///<remarks></remarks>
        private DataTable ReadADRDataTable(Int32 iAdrMenge)
        {
            /***********************************************
             * Adresslisten
             * 0 = Kundenliste
             * 1 = Adressliste komplett
             * 2 = Adressliste nur aktiv
             * 3 = Adressliste nur passiv
             * 4 = Adressliste aktive Auftraggeber
             * 5 = Adressliste aktive Lieferanten / Versender 
             * 6 = Adressliste aktive Empfänger
             * 7 = Adressliste aktive Entladestellen
             * 8 = Adressliste aktive Speditionen
             * 9 = Adressliste aktive Beladestellen
             * 10= Adressliste aktive Postadressen
             * 11= Adressliste aktive Rechnungsadressen
             * 
             * 
             * 14 = Abruf Empfänger
             * ********************************************/
            adrList.Rows.Clear();
            switch (iAdrMenge)
            {
                case 0:
                    adrList = clsKunde.dataTableKD(this.GL_User.User_ID);
                    break;
                case 1:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case 2:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case 3:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeKunde:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeVersender:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeEmpfaenger:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeEntlade:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeSpedition:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListePost:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeBelade:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeRechnung:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                default:
                    // Diverse ?? 
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
            }
            return adrList;
        }
        ///<summary>ctrADRSearch / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV()
        {
            DataTable tmpTable = ReadADRDataTable(iAdrRange);

            this.grdADRList.DataSource = adrList;
            if (this.grdADRList.Rows.Count > 0)
            {
                this.grdADRList.Rows[0].IsSelected = true;
                grdADRList.Columns["ID"].IsVisible = (this.GL_User.IsAdmin);

                for (Int32 i = 0; i <= this.grdADRList.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    string strTmp = this.grdADRList.Rows[i].Cells["ID"].Value.ToString();
                    Decimal.TryParse(strTmp, out decTmp);
                    if (ADR.ID == decTmp)
                    {
                        this.grdADRList.Rows[i].IsSelected = true;
                        this.grdADRList.Rows[i].IsCurrent = true;
                    }
                }
                grdADRList.BestFitColumns();
            }
        }
        ///<summary>ctrADRSearch / miListeKomplett_Click</summary>
        ///<remarks></remarks>
        private void miListeKomplett_Click(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListeKomplett;
            afColorLabel1.myText = "Adressliste [komplett]";
            InitDGV();
        }
        ///<summary>ctrADRSearch / adresslisteaktivToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void adresslisteaktivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListAktiv;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListAktivString;
            InitDGV();
        }
        ///<summary>ctrADRSearch / adresslistepassivToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void adresslistepassivToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        ///<summary>ctrADRSearch / grdADRList_CellClick</summary>
        ///<remarks></remarks>
        private void grdADRList_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                decimal decTmp = 0;
                string strTmp = this.grdADRList.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    ADR = new clsADR();
                    ADR._GL_User = this.GL_User;
                    ADR._GL_System = this._ctrMenu._frmMain.GL_System;
                    ADR.ID = decTmp;
                    ADR.Fill();
                    SetAdrInfo();
                }
            }
        }
        ///<summary>ctrADRSearch / SetAdrInfo</summary>
        ///<remarks></remarks>
        private void SetAdrInfo()
        {
            tbAdrInfo.Text = ADR.ADRString;
        }
        ///<summary>vctrADRSearch / txtSearch_TextChanged</summary>
        ///<remarks></remarks>
        public void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchAdr();
        }
        ///<summary>ctrADRSearch / SearchAdr</summary>
        ///<remarks>Liste der Aktuellen Adressen durchsuchen</remarks>
        private void SearchAdr()
        {
            if (cbSearchArt.Checked)
            {
                SearchGrdADRList(txtSearch.Text.ToUpper());// Volltextsuche
            }
            else
            {
                if (txtSearch.Text.ToString() == "")
                {
                    InitDGV();
                }
                else
                {
                    //Block verschoben .. doppeltes laden der adrList bei leerem suchstring verhindert
                    adrList = ReadADRDataTable(iAdrRange);
                    string SearchText = txtSearch.Text.ToString();
                    string Ausgabe = string.Empty;

                    DataRow[] rows = adrList.Select("Suchbegriff LIKE '" + SearchText + "%'", "Suchbegriff");
                    tempTable.Clear();
                    tempTable = adrList.Clone();

                    foreach (DataRow row in rows)
                    {
                        Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                        tempTable.ImportRow(row);
                    }
                    grdADRList.DataSource = tempTable;
                    if (grdADRList.RowCount > 0)
                    {
                        grdADRList.Rows[0].IsSelected = true;
                        grdADRList.CurrentRow = grdADRList.Rows[0];
                        Int32 iCurRow = this.grdADRList.CurrentRow.Index;
                    }
                }
            }
        }
        ///<summary>ctrADRSearch / miClose_Click</summary>
        ///<remarks></remarks>
        private void miClose_Click(object sender, EventArgs e)
        {
            this._frmADRSearch.CloseFrm();
        }
        ///<summary>ctrADRSearch / SearchGrdADRList</summary>
        ///<remarks></remarks>
        private void SearchGrdADRList(string Search)
        {
            adrList = ReadADRDataTable(iAdrRange);
            //Spalte hinzufügen
            DataColumn col1 = adrList.Columns.Add("Find", typeof(Boolean));
            bool isFound = false;
            if (Search.ToString() == "")
            {
                iAdrRange = clsADR.const_AdrRange_AdrListeKomplett;
                this.InitDGV();
            }
            else
            {
                if (Convert.ToBoolean(Search.Length))
                {
                    // If the item is not found and you haven't looked at every cell, keep searching
                    //while ((!isFound) & (idx < maxSearches))
                    for (Int32 _Row = 0; _Row <= adrList.Rows.Count - 1; _Row++)
                    {
                        for (Int32 _Column = 1; _Column <= adrList.Columns.Count - 1; _Column++)
                        {

                            // Do all comparing in UpperCase so it is case insensitive
                            //if (grdADRList[_Column, _Row].Value.ToString().ToUpper().Contains(Search))
                            if (adrList.Rows[_Row][_Column].ToString().ToUpper().Contains(Search))
                            {
                                // If found position on the item
                                //grdADRList.FirstDisplayedScrollingRowIndex = _Row;
                                //grdADRList[_Column, _Row].Selected = true;
                                string test = adrList.Rows[_Row][_Column].ToString().ToUpper().Contains(Search).ToString();
                                isFound = true;
                                _Column = adrList.Columns.Count;
                            }

                        }
                        adrList.Rows[_Row]["Find"] = isFound;
                        isFound = false;
                    }
                }
                string Ausgabe = string.Empty;
                DataRow[] rows = adrList.Select("Find =true", "Find");
                tempTable.Clear();
                tempTable = adrList.Clone();
                foreach (DataRow row in rows)
                {
                    Ausgabe = Ausgabe + row["Find"].ToString() + "\n";
                    tempTable.ImportRow(row);
                }
                tempTable.Columns.Remove("Find");
                adrList.Columns.Remove("Find");
                adrList.Clear();
                adrList = tempTable;
                grdADRList.DataSource = adrList;
            }
        }
        ///<summary>ctrADRSearch / grdADRList_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void grdADRList_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            //if (AdrSucheAktiv)
            //{
            //Adresssuche ID wird übergeben und frm geschlossen
            //this._ctrMenu._ctrAdrList.SetADRAfterADRSearch(this.ADR.ID);
            getADRTakeOver(ADR.ID);
            if (this._frmADRSearch != null)
            {
                //wenn die Frm nach der ID übergabe direkt geschlossen wird, dann kommte es zur 
                //Exception im Telerik Grid. Um dies zu umgehen wird die Frm hier per Hide() 
                //ausgeblendet und dann mit Hilfe eines Backgroundworkers in der FrmADRSearch
                //der Sleepbefehl 1 s aufgerufen, so dass der normale Thread dieses Event 
                //dann verlassen hat und die Frm geschlossen werden kann.

                //this._frmADRSearch.Hide();
                //this._frmADRSearch.WaitForClosing();
            }
            //}
        }
        ///<summary>ctrADRSearch / cbMatchcodeSearch_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbSearchArt_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSearchArt.Checked)
            {
                cbSearchArt.Text = "Volltextsuche aktiviert";
                lSSuchbegriff.Text = "Suche nach...";
            }
            else
            {
                cbSearchArt.Text = "Volltextsuche deaktiviert";
                lSSuchbegriff.Text = "Suchbegriff";
            }
        }
        ///<summary>ctrADRSearch / tsbtnRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDGV();
        }
        ///<summary>ctrADRSearch / tsbtnRefresh_Click</summary>
        ///<remarks></remarks>
        public void SetGlobalValue(ctrMenu myCtrMenu)
        {
            this._ctrMenu = myCtrMenu;
            this.GL_User = myCtrMenu.GL_User;

            //Klasse Adresse initialisieren
            ADR = new clsADR();
            ADR.sys = this._ctrMenu._frmMain.system;
            ADR.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, 0, false);
        }
        ///<summary>ctrADRSearch / SetAFColorLabelMyText</summary>
        ///<remarks>ctr Überschrift</remarks>
        public void SetAFColorLabelMyText(string labelText)
        {
            afColorLabel1.myText = labelText;
        }

    }
}
