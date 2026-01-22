using Common.Enumerations;
using Common.Models;
using LVS;
using LVS.Communicator.EdiVDA;
using LVS.Constants;
using LVS.CustomProcesses;
using LVS.Models;
using LVS.ViewData;
using LVS.Views;
using Sped4.TelerikControls;
using Sped4.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Sped4.Classes
{
    public partial class ctrASNRead : UserControl
    {
        internal List<TabPage> tabPagesListAll = new List<TabPage>();

        public Globals._GL_USER GL_User;
        internal clsADR ADR;
        internal ctrMenu _ctrMenu;
        internal clsLager Lager = new clsLager();
        const string const_FileName = "_ASNList";
        private string AttachmentPath;
        internal clsASN ASN;
        internal BackgroundWorker workerBar;

        public AsnReadViewData asnVD = new AsnReadViewData();

        internal List<string> ListSearchFields = new List<string>()
        {
            "Produktionsnummer",
            "Lieferschein",
            "Charge"
        };

        private DataTable dtVDA4913DataSoucre { get; set; } = new DataTable();
        public List<ctrASNRead_AsnEdifactView> List_ctrAsnRead_AsnEdifactViewDataSource { get; set; } = new List<ctrASNRead_AsnEdifactView>();
        public List<ctrASNRead_AsnVdaView> List_ctrAsnRead_AsnVdaViewDataSource { get; set; } = new List<ctrASNRead_AsnVdaView>();
        public List<ctrASNRead_AsnArticleVdaView> List_ctrAsnRead_AsnArticleVdaView { get; set; } = new List<ctrASNRead_AsnArticleVdaView>();

        internal const string const_rtsDisplayProcess_Text_UseProcessOld = "DisplayProzess: alt";
        internal const string const_rtsDisplayProcess_Text_UseProcessNew = "DisplayProzess: neu";
        internal Stopwatch stopwatch = new Stopwatch();

        internal GridViewTemplate tmpArtVda = new GridViewTemplate();
        internal ctrAsnRead_GridView_InitGridViewRelation initVdaGridViewRelation;

        ///<summary>ctrASNRead / InitDGV</summary>
        ///<remarks></remarks>
        public ctrASNRead()
        {
            InitializeComponent();
        }
        ///<summary>ctrASNRead / ctrASNRead_Load</summary>
        ///<remarks></remarks>
        private async void ctrASNRead_Load(object sender, EventArgs e)
        {
            if (this._ctrMenu._frmMain.system.AbBereich.DefaultValue == null)
            {
                this._ctrMenu._frmMain.system.AbBereich.DefaultValue = new clsArbeitsbereichDefaultValue();
                this._ctrMenu._frmMain.system.AbBereich.DefaultValue.InitCls(this._ctrMenu._frmMain.system.AbBereich.ID);
            }

            //this.dgv.ChildViewExpanding += new Telerik.WinControls.UI.ChildViewExpandingEventHandler(this.dgv_ChildViewExpanding);
            dgv.ChildViewExpanding += dgv_ChildViewExpanding;

            panAdminInfo.Visible = _ctrMenu._frmMain.GL_User.IsAdmin;
            //stopwatch = new Stopwatch();
            //rtsDisplayProcess.Value = false;
            rtsDisplayProcess.Value = true;

            AttachmentPath = this._ctrMenu._frmMain.GL_System.sys_WorkingPathExport;
            InitCtr();

            int iIndex = 0;
            List<string> listEdifact = new List<string>()
            {
                constValue_AsnArt.const_Art_VDA4913
            };

            //-- Selection to show first Tab with Data
            if (AsnReadViewData.ExistNewASNToProceedByAsnFileType(GL_User.User_ID, _ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID, listEdifact))
            {
                iIndex = 0;
            }
            else
            {
                listEdifact = new List<string>()
                {
                    constValue_AsnArt.const_Art_EDIFACT_ASN_D96A,
                    constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A
                };
                if (AsnReadViewData.ExistNewASNToProceedByAsnFileType(GL_User.User_ID, _ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID, listEdifact))
                {
                    iIndex = 1;
                }
                else
                {
                    iIndex = 0;
                }
            }
            tabCtrAsnRead.SelectTab(iIndex);
            //LoadDgvForSelectedTab(iIndex);

            // Daten werden im Hintergrund geladen
            await Task.Run(() => LoadDgvForSelectedTab(iIndex));

            //tabCtrAsnRead.Refresh();
        }
        ///<summary>ctrASNRead / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr()
        {
            //-- variable Suchfelder
            tscVDA4913SearchField.Items.Clear();
            tscVDA4913SearchField.Items.AddRange(ListSearchFields.ToArray());
            tscVDA4913SearchField.SelectedIndex = 0;

            tscbEdifactSearchField.Items.Clear();
            tscbEdifactSearchField.Items.AddRange(ListSearchFields.ToArray());
            tscbEdifactSearchField.SelectedIndex = 0;

            //-- auto. Reihenvergabe 
            this.cbUseAutoRowAssignment.Visible = false;
            this.cbUseAutoRowAssignment.Checked = false;
            if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Lagerreihenverwaltung)
            {
                if (this._ctrMenu._frmMain.system.AbBereich.UseAutoRowAssignment)
                {
                    this.cbUseAutoRowAssignment.Visible = true;
                    this.cbUseAutoRowAssignment.Checked = this._ctrMenu._frmMain.system.AbBereich.UseAutoRowAssignment;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIndex"></param>
        private void LoadDgvForSelectedTab(int iIndex)
        {
            switch (iIndex)
            {
                case 0:
                    lvLogVDA4913.Items.Clear();
                    InitDgvSwitchVda();
                    break;

                case 1:
                    lvLogEDIFACT.Items.Clear();
                    InitDGVEDIFACT();
                    break;
            }
        }

        private void IniBackgroundWorker()
        {
            workerBar = new BackgroundWorker();
            workerBar.WorkerReportsProgress = true;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvSwitchVda()
        {
            dgv.MasterTemplate.Templates.Clear();
            //--- Backgroundworker
            workerBar?.Dispose(); // Alten Worker entfernen
            IniBackgroundWorker();
            workerBar.ProgressChanged += (s, args) =>
            {
                barVda.Value1 = args.ProgressPercentage;
                barVda.Text = args.ProgressPercentage.ToString() + " von " + barVda.Maximum.ToString();
            };
            if (rtsDisplayProcess.Value)
            {
                workerBar.DoWork += (s, args) =>
                {
                    InitDgvVDA();
                };
            }
            else
            {
                workerBar.DoWork += (s, args) =>
                {
                    InitDGV();
                };
            }
            if (!workerBar.IsBusy)
            {
                workerBar.RunWorkerAsync();
            }
            workerBar.RunWorkerCompleted += (s, args) =>
            {
                barVda.Text = "Anzahl: " + barVda.Maximum.ToString() + " | Dauer [s]: " + stopwatch.Elapsed.TotalSeconds.ToString("0.00");
            };
            stopwatch.Reset();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvVDA()
        {
            stopwatch.Start();
            bool IsUserComtecAdmin = Functions.CheckUserForAdminComtec(this.GL_User);
            try
            {
                if (this.InvokeRequired) // Überprüfen, ob der Aufruf im richtigen Thread erfolgt
                {
                    this.Invoke(new Action(InitDgvVDA)); // Methode im UI-Thread ausführen
                    return;
                }
                AsnViewData asnViewData = new AsnViewData(this._ctrMenu._frmMain.system);
                List<Asn> listAsn = asnViewData.GetListVda();
                //--- setzt Maximum Statusbar
                barVda.Maximum = listAsn.Count;
                List<ctrASNRead_AsnVdaView> ListHeadAsnVdaView = asnViewData.FillAsnVdaView(listAsn, this.workerBar);
                List_ctrAsnRead_AsnVdaViewDataSource = new List<ctrASNRead_AsnVdaView>(ListHeadAsnVdaView);
                dgv.DataSource = List_ctrAsnRead_AsnVdaViewDataSource;
                int x = 0;
                for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
                {
                    string ColName = dgv.Columns[i].Name.ToString();
                    //dgv.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                    switch (ColName)
                    {
                        case "Select":
                            this.dgv.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                            this.dgv.Columns[i].MinWidth = 60;
                            this.dgv.Columns[i].MaxWidth = 90;
                            this.dgv.Columns.Move(i, x);
                            x++;
                            break;
                        case "ASN":
                            this.dgv.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                            this.dgv.Columns[i].MinWidth = 80;
                            this.dgv.Columns[i].MaxWidth = 120;
                            this.dgv.Columns.Move(i, x);
                            this.dgv.Columns[i].TextAlignment = ContentAlignment.MiddleCenter;
                            x++;
                            break;
                        case "ASN-Datum":
                        case "ASN_Datum":
                            this.dgv.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                            this.dgv.Columns[i].MinWidth = 100;
                            this.dgv.Columns[i].MaxWidth = 140;
                            //this.dgv.Columns[i].Width = 120;
                            this.dgv.Columns[i].FormatString = "{0:d}";
                            this.dgv.Columns[i].TextAlignment = ContentAlignment.MiddleCenter;
                            this.dgv.Columns.Move(i, x);
                            x++;
                            break;
                        case "AuftraggeberView":
                        case "Auftraggeber":
                            this.dgv.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                            this.dgv.Columns[i].MinWidth = 130;
                            this.dgv.Columns[i].MaxWidth = 160;
                            //this.dgv.Columns[i].HeaderText = "Auftraggeber";
                            //this.dgv.Columns[i].Width = 130;
                            this.dgv.Columns.Move(i, x);
                            x++;
                            break;
                        case "EmpfaengerView":
                        case "Empfänger":
                            this.dgv.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                            this.dgv.Columns[i].MinWidth = 130;
                            this.dgv.Columns[i].MaxWidth = 160;
                            //this.dgv.Columns[i].HeaderText = "Empfänger";
                            //this.dgv.Columns[i].Width = 130;
                            this.dgv.Columns.Move(i, x);
                            x++;
                            break;
                        case "Ref.Auftraggeber":
                        case "RefAuftraggeber":
                            //this.dgv.Columns[i].Width = 100;
                            this.dgv.Columns.Move(i, x);
                            //this.dgv.Columns[i].IsVisible = IsUserComtecAdmin;
                            x++;
                            break;
                        case "Ref.Empfaenger":
                        case "RefEmpfaenger":
                            //this.dgv.Columns[i].Width = 100;
                            this.dgv.Columns[i].MinWidth = 100;
                            this.dgv.Columns[i].MaxWidth = 120;
                            this.dgv.Columns.Move(i, x);
                            //this.dgv.Columns[i].IsVisible = IsUserComtecAdmin;
                            x++;
                            break;
                        //case "TransportNr":
                        case "ExTransportRef":
                            this.dgv.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                            this.dgv.Columns[i].MinWidth = 90;
                            this.dgv.Columns[i].MaxWidth = 120;
                            //this.dgv.Columns[i].Width = 100;
                            this.dgv.Columns.Move(i, x);
                            x++;
                            break;
                        case "VS-Datum":
                            this.dgv.Columns[i].FormatString = "{0:d}";
                            this.dgv.Columns[i].Width = 100;
                            this.dgv.Columns.Move(i, x);
                            x++;
                            //this.dgv.Columns[i].HeaderText=
                            break;
                        case "LfsNr":
                        case "Lieferschein":
                            //this.dgv.Columns[i].HeaderText = "Lieferschein-Nr";
                            //this.dgv.Columns[i].Width = 100;
                            this.dgv.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                            this.dgv.Columns[i].MinWidth = 100;
                            this.dgv.Columns[i].MaxWidth = 120;
                            this.dgv.Columns[i].TextAlignment = ContentAlignment.MiddleCenter;
                            this.dgv.Columns.Move(i, x);
                            //this.dgv.Columns[i].IsVisible = IsUserComtecAdmin;
                            x++;
                            break;
                        case "Lieferant":
                            //this.dgv.Columns[i].HeaderText = "Lieferschein-Nr";
                            //this.dgv.Columns[i].Width = 100;
                            this.dgv.Columns[i].TextAlignment = ContentAlignment.MiddleCenter;
                            this.dgv.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                            this.dgv.Columns[i].MinWidth = 100;
                            this.dgv.Columns[i].MaxWidth = 120;
                            this.dgv.Columns.Move(i, x);
                            //this.dgv.Columns[i].IsVisible = IsUserComtecAdmin;
                            x++;
                            break;
                        case "ID":
                            this.dgv.Columns[i].IsVisible = false;
                            break;
                        default:
                            this.dgv.Columns[i].IsVisible = false;
                            break;
                    }
                }

                if (List_ctrAsnRead_AsnVdaViewDataSource.Count > 0)
                {
                    var listArt = asnViewData.AsnArticleVdaViewInit(List_ctrAsnRead_AsnVdaViewDataSource);
                    List_ctrAsnRead_AsnArticleVdaView = new List<ctrASNRead_AsnArticleVdaView>(listArt);
                    initVdaGridViewRelation = new ctrAsnRead_GridView_InitGridViewRelation(List_ctrAsnRead_AsnArticleVdaView, dgv.MasterTemplate);

                    dgv.MasterTemplate.Templates.Clear();
                    dgv.MasterTemplate.Templates.Add(initVdaGridViewRelation.gridViewTemplate);
                    dgv.Relations.Clear();
                    dgv.Relations.Add(initVdaGridViewRelation.gridViewRelation);

                    x = 0;
                    for (Int32 i = 0; i <= initVdaGridViewRelation.gridViewTemplate.Columns.Count - 1; i++)
                    {
                        string ColName = initVdaGridViewRelation.gridViewTemplate.Columns[i].Name.ToString();
                        initVdaGridViewRelation.gridViewTemplate.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;

                        switch (ColName)
                        {
                            case "ASN":
                                //initVdaGridViewRelation.gridViewTemplate.Columns[i].Width = 60;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MinWidth = 80;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MaxWidth = 100;
                                initVdaGridViewRelation.gridViewTemplate.Columns.Move(i, x);
                                x++;
                                break;
                            case "Position":
                            case "Pos":
                                //initVdaGridViewRelation.gridViewTemplate.Columns[i].Width = 50;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MinWidth = 50;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MaxWidth = 70;
                                initVdaGridViewRelation.gridViewTemplate.Columns.Move(i, x);
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                                x++;
                                break;
                            case "Dicke":
                            case "Breite":
                            case "Laenge":
                            case "Hoehe":
                            case "Netto":
                            case "Brutto":
                                //initVdaGridViewRelation.gridViewTemplate.Columns[i].Width = 80;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MinWidth = 70;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MaxWidth = 90;
                                initVdaGridViewRelation.gridViewTemplate.Columns.Move(i, x);
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].FormatString = "{0:N2}";
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                                x++;
                                break;
                            case "Einheit":
                                //initVdaGridViewRelation.gridViewTemplate.Columns[i].Width = 80;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MinWidth = 70;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MaxWidth = 90;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].FormatString = "{0:N2}";
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                                initVdaGridViewRelation.gridViewTemplate.Columns.Move(i, x);
                                x++;
                                break;
                            case "Anzahl":
                                //initVdaGridViewRelation.gridViewTemplate.Columns[i].Width = 80;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MinWidth = 70;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MaxWidth = 90;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].FormatString = "{0:N0}";
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                                initVdaGridViewRelation.gridViewTemplate.Columns.Move(i, x);
                                x++;
                                break;
                            case "exMaterialnummer":
                            case "Bestellnummer":
                            case "Produktionsnummer":
                            case "Werksnummer":
                            case "exBezeichnung":
                            case "exAuftragPos":
                            case "exAuftrag":
                            case "Charge":
                            case "LfsNr":
                            case "Gut":
                            case "TMS":
                            case "VehicleNo":
                                //initVdaGridViewRelation.gridViewTemplate.Columns[i].Width = 120;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MinWidth = 120;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MaxWidth = 140;
                                initVdaGridViewRelation.gridViewTemplate.Columns.Move(i, x);
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                                x++;
                                break;
                            case "GlowDate":
                            case "Glühdatum":
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].HeaderText = "Glühdatum";
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MinWidth = 80;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].MaxWidth = 100;
                                //initVdaGridViewRelation.gridViewTemplate.Columns[i].Width = 80;
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].FormatString = "{0:d}";
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                                initVdaGridViewRelation.gridViewTemplate.Columns.Move(i, x);
                                x++;
                                break;

                            default:
                                initVdaGridViewRelation.gridViewTemplate.Columns[i].IsVisible = false;
                                break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            stopwatch.Stop();
        }
        ///<summary>ctrASNRead / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV()
        {
            stopwatch.Start();
            bool IsUserComtecAdmin = Functions.CheckUserForAdminComtec(this.GL_User);
            try
            {
                ASN = new clsASN();
                ASN.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);
                ASN.Sys = this._ctrMenu._frmMain.system;


                if (this.InvokeRequired) // Überprüfen, ob der Aufruf im richtigen Thread erfolgt
                {
                    this.Invoke(new Action(InitDGV)); // Methode im UI-Thread ausführen
                    return;
                }

                clsLagerdaten tmpLgDaten = ASN.GetASNTorRead();
                if (tmpLgDaten.ListCreatedNewGArten != null)
                {
                    if (tmpLgDaten.ListCreatedNewGArten.Count > 0)
                    {
                        lvLogVDA4913.Items.Clear();

                        for (int i = 0; i <= tmpLgDaten.ListCreatedNewGArten.Count - 1; i++)
                        {
                            lvLogVDA4913.Items.Add(tmpLgDaten.ListCreatedNewGArten[i].ToString());
                        }
                    }
                }
                //MainGrid
                dtVDA4913DataSoucre = new DataTable();
                dtVDA4913DataSoucre = ASN.dtASNForEingang;
                dgv.DataSource = dtVDA4913DataSoucre;

                for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
                {
                    string ColName = dgv.Columns[i].Name.ToString();
                    switch (ColName)
                    {
                        case "Select":
                            this.dgv.Columns[i].Width = 80;
                            this.dgv.Columns.Move(i, 0);
                            break;
                        case "ASN":
                            this.dgv.Columns[i].Width = 60;
                            this.dgv.Columns.Move(i, 1);
                            break;
                        case "ASN-Datum":
                            this.dgv.Columns[i].Width = 120;
                            this.dgv.Columns[i].FormatString = "{0:d}";
                            this.dgv.Columns.Move(i, 2);
                            break;
                        case "AuftraggeberView":
                            this.dgv.Columns[i].HeaderText = "Auftraggeber";
                            this.dgv.Columns[i].Width = 130;
                            this.dgv.Columns.Move(i, 3);
                            break;
                        case "EmpfaengerView":
                            this.dgv.Columns[i].HeaderText = "Empfänger";
                            this.dgv.Columns[i].Width = 130;
                            this.dgv.Columns.Move(i, 4);
                            break;
                        case "Ref.Auftraggeber":
                            this.dgv.Columns[i].Width = 100;
                            //this.dgv.Columns.Move(i, 5);
                            this.dgv.Columns[i].IsVisible = IsUserComtecAdmin;
                            break;
                        case "Ref.Empfaenger":
                            this.dgv.Columns[i].Width = 100;
                            //this.dgv.Columns.Move(i, 6);
                            this.dgv.Columns[i].IsVisible = IsUserComtecAdmin;
                            break;
                        //case "TransportNr":
                        case "ExTransportRef":
                            this.dgv.Columns[i].Width = 100;
                            this.dgv.Columns.Move(i, 7);
                            break;
                        case "VS-Datum":
                            this.dgv.Columns[i].FormatString = "{0:d}";
                            this.dgv.Columns[i].Width = 100;
                            this.dgv.Columns.Move(i, 8);
                            //this.dgv.Columns[i].HeaderText=
                            break;
                        case "LfsNr":
                            this.dgv.Columns[i].HeaderText = "Lieferschein-Nr.";
                            this.dgv.Columns[i].Width = 100;
                            //this.dgv.Columns.Move(i, 9);
                            this.dgv.Columns[i].IsVisible = IsUserComtecAdmin;
                            break;
                        case "ID":
                            this.dgv.Columns[i].IsVisible = false;
                            break;
                        default:
                            this.dgv.Columns[i].IsVisible = false;
                            break;
                    }
                }

                //Artikel Template
                tmpArtVda = new GridViewTemplate();
                tmpArtVda.DataSource = ASN.dtASNForArt.DefaultView;
                Int32 x = 0;

                for (Int32 i = 0; i <= tmpArtVda.Columns.Count - 1; i++)
                {
                    string ColName = tmpArtVda.Columns[i].Name.ToString();
                    switch (ColName)
                    {
                        case "ASN":
                            tmpArtVda.Columns[i].Width = 60;
                            tmpArtVda.Columns.Move(i, x);
                            x++;
                            break;
                        case "Position":
                            tmpArtVda.Columns[i].Width = 50;
                            tmpArtVda.Columns.Move(i, x);
                            x++;
                            break;
                        case "Dicke":
                        case "Breite":
                        case "Laenge":
                        case "Hoehe":
                        case "Netto":
                        case "Brutto":
                            tmpArtVda.Columns[i].Width = 80;
                            tmpArtVda.Columns.Move(i, x);
                            tmpArtVda.Columns[i].FormatString = "{0:N2}";
                            tmpArtVda.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                            x++;
                            break;
                        case "Anzahl":
                        case "Einheit":
                            tmpArtVda.Columns[i].Width = 80;
                            tmpArtVda.Columns.Move(i, x);
                            x++;
                            break;
                        case "exMaterialnummer":
                        case "Bestellnummer":
                        case "Produktionsnummer":
                        case "Werksnummer":
                        case "exBezeichnung":
                        case "exAuftragPos":
                        case "exAuftrag":
                        case "Charge":
                        case "LfsNr":
                        case "Gut":
                        case "TMS":
                        case "VehicleNo":
                            tmpArtVda.Columns[i].Width = 120;
                            tmpArtVda.Columns.Move(i, x);
                            x++;
                            break;
                        case "GlowDate":
                            tmpArtVda.Columns[i].HeaderText = "Glühdatum";
                            tmpArtVda.Columns[i].Width = 80;
                            tmpArtVda.Columns[i].FormatString = "{0:d}";
                            tmpArtVda.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                            tmpArtVda.Columns.Move(i, x);
                            x++;
                            break;

                        default:
                            tmpArtVda.Columns[i].IsVisible = false;
                            break;
                    }
                }
                dgv.MasterTemplate.Templates.Add(tmpArtVda);

                GridViewRelation relation = new GridViewRelation(dgv.MasterTemplate);
                relation.ChildTemplate = tmpArtVda;
                relation.RelationName = "ASNID";
                relation.ParentColumnNames.Add("ASN");
                relation.ChildColumnNames.Add("ASN");
                dgv.Relations.Add(relation);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            stopwatch.Stop();
        }
        /// <summary>
        ///             Beim öffnen werden die Daten für die Artikel / Child ermittelt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_ChildViewExpanding(object sender, ChildViewExpandingEventArgs e)
        {
            //Fix für Thread-Safety
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new Action(() => dgv_ChildViewExpanding(sender, e)));
                return;
            }
            if (rtsDisplayProcess.Value)
            {
                var parentRow = e.ParentRow;
                //if (parentRow.DataBoundItem is ctrASNRead_AsnVdaView parentData)
                if (parentRow?.DataBoundItem is ctrASNRead_AsnVdaView parentData)
                {
                    // Proceed with parentData  
                    //var parentData = (ctrASNRead_AsnVdaView)parentRow.DataBoundItem;
                    if (parentRow.ChildRows.Count > 0)
                    {
                        //if (
                        //        (parentData.eingang is Eingaenge)
                        //        && (parentData.eingang.ASN > 0)
                        //        && (parentData.AsnMessage is Asn)
                        //        && (parentData.AsnMessage.Id > 0)
                        //        && (parentData.AsnMessage.Id == parentData.eingang.ASN)
                        //  )
                        if (
                                parentRow.ChildRows.Count > 0 &&
                                parentData.eingang is Eingaenge eingang &&
                                eingang != null &&
                                eingang.ASN > 0 &&
                                parentData.AsnMessage is Asn asn &&
                                asn != null &&
                                asn.Id > 0 &&
                                asn.Id == eingang.ASN
                            )
                        {
                            VdaMessageToClasses vmtc = new VdaMessageToClasses(this._ctrMenu._frmMain.system);
                            var listArt = vmtc.CreateArtikelVdaMessageValueList(parentData.dtAsnValues, parentData.eingang);
                            if (listArt.Count > 0)
                            {
                                List_ctrAsnRead_AsnArticleVdaView.RemoveAll(x => x.ASN == parentData.AsnMessage.Id);
                                foreach (var itm in listArt)
                                {
                                    if (!List_ctrAsnRead_AsnArticleVdaView.Contains(itm))
                                    {
                                        List_ctrAsnRead_AsnArticleVdaView.Add(itm);
                                    }
                                }
                                initVdaGridViewRelation = new ctrAsnRead_GridView_InitGridViewRelation(List_ctrAsnRead_AsnArticleVdaView, dgv.MasterTemplate);
                                dgv.MasterTemplate.Templates.Clear();
                                dgv.MasterTemplate.Templates.Add(initVdaGridViewRelation.gridViewTemplate);
                                dgv.Relations.Clear();
                                dgv.Relations.Add(initVdaGridViewRelation.gridViewRelation);
                            }
                        }
                    }
                }
                else
                {
                    // Handle the case where the type is not as expected  
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitEdifact()
        {
            dgv.MasterTemplate.Templates.Clear();
            //--- Backgroundworker
            workerBar = new BackgroundWorker();
            workerBar.WorkerReportsProgress = true;
            workerBar.ProgressChanged += (s, args) =>
            {
                barEdifact.Value1 = args.ProgressPercentage;
                barEdifact.Text = args.ProgressPercentage.ToString() + " von " + barEdifact.Maximum.ToString();
            };
            workerBar.DoWork += (s, args) =>
            {
                InitDGVEDIFACT();
            };
            if (!workerBar.IsBusy)
            {
                workerBar.RunWorkerAsync();
            }
            workerBar.RunWorkerCompleted += (s, args) =>
            {
                barEdifact.Text = "Anzahl: " + barEdifact.Maximum.ToString() + " | Dauer [s]: " + stopwatch.Elapsed.TotalSeconds.ToString("0.00");
            };
            stopwatch.Reset();
        }
        ///<summary>ctrASNRead / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGVEDIFACT()
        {
            stopwatch.Start();
            bool IsUserComtecAdmin = Functions.CheckUserForAdminComtec(this.GL_User);
            try
            {
                asnVD = new AsnReadViewData(this.GL_User, this._ctrMenu._frmMain.system);
                asnVD.GetASNListForCtrAsnRead();
                List_ctrAsnRead_AsnEdifactViewDataSource = new List<ctrASNRead_AsnEdifactView>();
                List_ctrAsnRead_AsnEdifactViewDataSource = asnVD.List_ctrAsnRead_AsnEdifactView;
                dgvEDIFACT.DataSource = List_ctrAsnRead_AsnEdifactViewDataSource;

                for (Int32 i = 0; i <= this.dgvEDIFACT.Columns.Count - 1; i++)
                {
                    string ColName = dgvEDIFACT.Columns[i].Name.ToString();
                    switch (ColName)
                    {
                        case "Select":
                            this.dgvEDIFACT.Columns[i].Width = 80;
                            this.dgvEDIFACT.Columns.Move(i, 0);
                            break;
                        case "ASN":
                            this.dgvEDIFACT.Columns[i].Width = 60;
                            this.dgvEDIFACT.Columns.Move(i, 1);
                            break;
                        case "ASN_Datum":
                            this.dgvEDIFACT.Columns[i].Width = 120;
                            this.dgvEDIFACT.Columns[i].HeaderText = "ASN-Datum";
                            this.dgvEDIFACT.Columns[i].FormatString = "{0:d}";
                            this.dgvEDIFACT.Columns.Move(i, 2);
                            break;
                        case "AuftraggeberView":
                            this.dgvEDIFACT.Columns[i].HeaderText = "Auftraggeber";
                            this.dgvEDIFACT.Columns[i].Width = 130;
                            this.dgvEDIFACT.Columns.Move(i, 3);
                            break;
                        case "EmpfaengerView":
                            this.dgvEDIFACT.Columns[i].HeaderText = "Empfänger";
                            this.dgvEDIFACT.Columns[i].Width = 130;
                            this.dgvEDIFACT.Columns.Move(i, 4);
                            break;
                        case "LfsNr":
                            this.dgvEDIFACT.Columns[i].HeaderText = "Lieferschein-Nr.";
                            this.dgvEDIFACT.Columns[i].Width = 100;
                            this.dgvEDIFACT.Columns.Move(i, 5);
                            //this.dgvEDIFACT.Columns[i].IsVisible = IsUserComtecAdmin;
                            break;
                        case "Lieferantennummer":
                            this.dgvEDIFACT.Columns[i].HeaderText = "Lieferantennummer";
                            this.dgvEDIFACT.Columns[i].Width = 100;
                            this.dgvEDIFACT.Columns.Move(i, 6);
                            //this.dgvEDIFACT.Columns[i].IsVisible = IsUserComtecAdmin;
                            break;
                        case "ExTransportRef":
                            //this.dgvEDIFACT.Columns[i].Width = 100;
                            //this.dgvEDIFACT.Columns.Move(i, 6);
                            this.dgvEDIFACT.Columns[i].IsVisible = false;
                            break;
                        case "ID":
                        case "Id":
                            this.dgvEDIFACT.Columns[i].IsVisible = IsUserComtecAdmin;
                            break;
                        default:
                            this.dgvEDIFACT.Columns[i].IsVisible = false;
                            break;
                    }

                }

                ////Artikel Template
                GridViewTemplate tmpArt = new GridViewTemplate();
                tmpArt.DataSource = asnVD.List_ctrAsnRead_AsnArticelEdifactView;
                Int32 x = 0;
                for (Int32 i = 0; i <= tmpArt.Columns.Count - 1; i++)
                {
                    string ColName = tmpArt.Columns[i].Name.ToString();
                    switch (ColName)
                    {
                        case "ASN":
                            tmpArt.Columns[i].Width = 60;
                            tmpArt.Columns.Move(i, x);
                            x++;
                            break;
                        case "Position":
                            tmpArt.Columns[i].Width = 50;
                            tmpArt.Columns.Move(i, x);
                            x++;
                            break;
                        case "Dicke":
                        case "Breite":
                        case "Laenge":
                        case "Hoehe":
                        case "Netto":
                        case "Brutto":
                            tmpArt.Columns[i].Width = 80;
                            tmpArt.Columns.Move(i, x);
                            tmpArt.Columns[i].FormatString = "{0:N2}";
                            tmpArt.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                            x++;
                            break;
                        case "Anzahl":
                        case "Einheit":
                            tmpArt.Columns[i].Width = 80;
                            tmpArt.Columns.Move(i, x);
                            x++;
                            break;
                        case "exMaterialnummer":
                        case "Bestellnummer":
                        case "Produktionsnummer":
                        case "Werksnummer":
                        case "exBezeichnung":
                        case "exAuftragPos":
                        case "exAuftrag":
                        case "Charge":
                        case "LfsNr":
                        case "Gut":
                        case "TMS":
                        case "VehicleNo":
                            tmpArt.Columns[i].Width = 120;
                            tmpArt.Columns.Move(i, x);
                            x++;
                            break;
                        case "GlowDate":
                            tmpArt.Columns[i].HeaderText = "Glühdatum";
                            tmpArt.Columns[i].Width = 80;
                            tmpArt.Columns[i].FormatString = "{0:d}";
                            tmpArt.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                            tmpArt.Columns.Move(i, x);
                            x++;
                            break;

                        default:
                            tmpArt.Columns[i].IsVisible = false;
                            break;
                    }
                }
                dgvEDIFACT.MasterTemplate.Templates.Clear();
                dgvEDIFACT.MasterTemplate.Templates.Add(tmpArt);

                GridViewRelation relation = new GridViewRelation(dgvEDIFACT.MasterTemplate);
                relation.ChildTemplate = tmpArt;
                relation.RelationName = "ASNID";
                relation.ParentColumnNames.Add("ASN");
                relation.ChildColumnNames.Add("ASN");
                dgvEDIFACT.Relations.Add(relation);

                //dgvEDIFACT.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            stopwatch.Stop();
        }
        ///<summary>ctrASNRead / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrASNRead();
        }
        ///<summary>ctrASNRead / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void dgv_CellClick(object sender, GridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case -1:
                    GridViewTemplateChildViewCollapseUncollapse.CollapseAllExceptCurrent(ref dgv);
                    break;
                case 0:
                    if (!this.dgv.Rows[e.RowIndex].Cells["Select"].ReadOnly)
                    {
                        bool CellValue = (bool)this.dgv.Rows[e.RowIndex].Cells["Select"].Value;
                        if (CellValue == true)
                        {
                            this.dgv.Rows[e.RowIndex].Cells["Select"].Value = false;
                        }
                        else
                        {
                            this.dgv.Rows[e.RowIndex].Cells["Select"].Value = true;
                        }
                    }
                    else
                    {
                        string strText = "Die DFÜ / ASN konnte nicht korrekt zugeordnet werden und kann nicht verarbeitet werden!";
                        clsMessages.Allgemein_ERRORTextShow(strText);
                    }
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvEDIFACT_CellClick(object sender, GridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case -1:
                    GridViewTemplateChildViewCollapseUncollapse.CollapseAllExceptCurrent(ref dgvEDIFACT);
                    break;
                case 0:
                    if (!this.dgvEDIFACT.Rows[e.RowIndex].Cells["Select"].ReadOnly)
                    {
                        bool CellValue = (bool)this.dgvEDIFACT.Rows[e.RowIndex].Cells["Select"].Value;
                        if (CellValue == true)
                        {
                            this.dgvEDIFACT.Rows[e.RowIndex].Cells["Select"].Value = false;
                        }
                        else
                        {
                            this.dgvEDIFACT.Rows[e.RowIndex].Cells["Select"].Value = true;
                        }
                    }
                    else
                    {
                        string strText = "Die DFÜ / ASN konnte nicht korrekt zugeordnet werden und kann nicht verarbeitet werden!";
                        clsMessages.Allgemein_ERRORTextShow(strText);
                    }
                    break;
            }
        }
        ///<summary>ctrASNRead / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void SetAllASNSelectOrUnselect(bool bSelect)
        {
            for (Int32 i = 0; i <= dgv.Rows.Count - 1; i++)
            {
                this.dgv.Rows[i].Cells["Select"].Value = bSelect;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bSelect"></param>
        private void SetDgvEdifactAllASNSelectOrUnselect(bool bSelect)
        {
            for (Int32 i = 0; i <= dgvEDIFACT.Rows.Count - 1; i++)
            {
                this.dgvEDIFACT.Rows[i].Cells["Select"].Value = bSelect;
            }
        }
        ///<summary>ctrASNRead / tsbtnAllCheck_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAllCheck_Click(object sender, EventArgs e)
        {
            SetAllASNSelectOrUnselect(true);
        }
        ///<summary>ctrASNRead / tsbtnAllUncheck_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAllUncheck_Click(object sender, EventArgs e)
        {
            SetAllASNSelectOrUnselect(false);
        }
        ///<summary>ctrASNRead / tsbtnAllUncheck_Click</summary>
        ///<remarks></remarks>
        private async void tsbtnCreateEingang_Click(object sender, EventArgs e)
        {
            lvLogVDA4913.Items.Clear();
            if (clsMessages.Allgemein_SelectionInfoTextShow("Verarbeitung gestartet...", MessageBoxIcon.Information))
            {
                //lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm")+" - Verarbeitung wird gestartet...." );

                this.Invoke((Action)(() =>
                {
                    lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " - Verarbeitung wird gestartet....");
                }));

                // Asynchroner Aufruf von DoCreateEingangVda
                await Task.Run(() => DoCreateEingangVda());
                //DoCreateEingangVda();
                InitDgvSwitchVda();
                this.Invoke((Action)(() =>
                {
                    lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " - Aufstellung wurde neu geladen....");
                }));
            }
        }
        /// <summary>
        ///             VDA - Eingang erstellen
        /// </summary>
        private void DoCreateEingangVda()
        {
            if (rtsDisplayProcess.Value)
            {
                var selectedAsnList = List_ctrAsnRead_AsnVdaViewDataSource.Where(x => x.Select == true).ToList();
                barVda.Minimum = 0;
                barVda.Value1 = 0;
                barVda.Maximum = selectedAsnList.Count;

                if (selectedAsnList.Count > 0)
                {
                    //stopwatch.Start();

                    foreach (var item in selectedAsnList)
                    {
                        barVda.Value1++;
                        barVda.Text = barVda.Value1.ToString() + " von " + barVda.Maximum.ToString();

                        //-- Artikeldaten ermitteln
                        VdaMessageToClasses vmtc = new VdaMessageToClasses(this._ctrMenu._frmMain.system);
                        var listArtItem = vmtc.CreateArtikelVdaMessageValueList(item.dtAsnValues, item.eingang);

                        var listLfs = listArtItem.Select(x => x.LfsNr).Distinct().ToList();
                        foreach (var lfs in listLfs)
                        {
                            item.ListArticleInEingang.Clear();
                            var artItem = listArtItem.Where(x => x.LfsNr == lfs).ToList();
                            item.eingang.LfsNr = lfs;
                            foreach (var x in artItem)
                            {
                                if (!item.ListArticleInEingang.Contains(x.article))
                                {
                                    item.ListArticleInEingang.Add(x.article);
                                }
                            }
                            bool bOk = asnVD.CreateStoreInByAsnEdifactViewTest(item.AsnMessage, item.eingang, item.ListArticleInEingang, (int)this._ctrMenu._frmMain.GL_User.User_ID);
                            //bool bOk = asnVD.CreateStoreInByAsnEdifactView(item.AsnMessage, item.eingang, item.ListArticleInEingang, (int)this._ctrMenu._frmMain.GL_User.User_ID);
                            if (bOk)
                            {
                                //lvLogVDA4913.Items.Add(asnVD.LogText);
                                //--alle Artikeldaten aus List
                                List_ctrAsnRead_AsnArticleVdaView.RemoveAll(x => x.ASN == item.AsnMessage.Id);
                            }
                            else
                            {
                                //lvLogVDA4913.Items.Add(asnVD.LogText);
                            }
                            this.Invoke((Action)(() =>
                            {
                                lvLogVDA4913.Items.Add(asnVD.LogText);
                            }));
                        }
                    }
                    //this.Invoke((Action)(() =>
                    //{
                    //    barVda.Text = "Anzahl: " + barVda.Maximum.ToString() + " | Dauer [s]: " + stopwatch.Elapsed.TotalSeconds.ToString("0.00");
                    //}));
                    this.Invoke((Action)(() =>
                    {
                        lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " - Verarbeitung abgeschlossen....");
                        lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " - Aufstellung wird neu geladen...");
                    }));
                    //stopwatch.Stop();
                }
                else
                {
                    //string message = string.Empty;
                    //message = "Es wurden keine ASN zur Verarbeitung ausgewählt!";
                    //lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                    //lvLogVDA4913.Items.Add(message);
                    this.Invoke((Action)(() =>
                    {
                        string message = string.Empty;
                        message = "Es wurden keine ASN zur Verarbeitung ausgewählt!";
                        lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                        lvLogVDA4913.Items.Add(message);
                    }));
                }
            }
            else
            {
                this.ASN.dtASNForEingang.DefaultView.RowFilter = "Select=True";
                DataTable dtTmpEingangToCreate = this.ASN.dtASNForEingang.DefaultView.ToTable();
                if (this.ASN.dtASNForEingang.DefaultView.Count > 0)
                {
                    clsLager Lager = new clsLager();
                    Lager.sys = this._ctrMenu._frmMain.system;
                    Lager._GL_User = this.GL_User;
                    Lager._GL_System = this._ctrMenu._frmMain.GL_System;

                    DataTable dtInserted = Lager.InsertASN(this.ASN.dtASNForEingang.DefaultView.ToTable(), ASN.dtASNForArt, this.cbUseAutoRowAssignment.Checked);

                    //Errortest
                    if (!Lager.ErrorText.Equals(string.Empty))
                    {
                        //lvLogVDA4913.Items.Add("ACHTUNG !");
                        //lvLogVDA4913.Items.Add(Lager.ErrorText);

                        //string message = string.Empty;
                        //message = "Es wurden keine ASN zur Verarbeitung ausgewählt!";
                        //lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                        //lvLogVDA4913.Items.Add(message);

                        //Lager.ErrorText = string.Empty;

                        this.Invoke((Action)(() =>
                        {
                            lvLogVDA4913.Items.Add("ACHTUNG !");
                            lvLogVDA4913.Items.Add(Lager.ErrorText);

                            string message = string.Empty;
                            message = "Es wurden keine ASN zur Verarbeitung ausgewählt!";
                            lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                            lvLogVDA4913.Items.Add(message);
                        }));
                        Lager.ErrorText = string.Empty;
                    }
                    if (dtInserted.Rows.Count > 0)
                    {
                        barVda.Minimum = 0;
                        barVda.Value1 = 0;
                        barVda.Maximum = dtInserted.Rows.Count;

                        //stopwatch.Start();

                        List<int> listInsertedArticle = new List<int>();
                        for (int i = 0; i < dtInserted.Rows.Count; i++)
                        {
                            barVda.Value1++;
                            barVda.Text = barVda.Value1.ToString() + " von " + barVda.Maximum.ToString();

                            lvLogVDA4913.Items.Add(dtInserted.Rows[i]["Date"].ToString());
                            string tmp = "-> Eingang " + dtInserted.Rows[i]["LeingangID"] + " erstellt";

                            lvLogVDA4913.Items.Add(tmp);
                            tmp = "   ASN : " + dtInserted.Rows[i]["ASN"];
                            lvLogVDA4913.Items.Add(tmp);
                            tmp = "   Artikel:";
                            lvLogVDA4913.Items.Add(tmp);

                            int iEid = 0;
                            int.TryParse(dtInserted.Rows[i]["ID"].ToString(), out iEid);
                            if (iEid > 0)
                            {
                                EingangViewData eVD = new EingangViewData(iEid, (int)GL_User.User_ID, true);
                                if (eVD.ListArticleInEingang.Count > 0)
                                {
                                    foreach (Articles art in eVD.ListArticleInEingang)
                                    {
                                        tmp = "      LVS Nummer: " + art.LVS_ID;
                                        lvLogVDA4913.Items.Add(tmp);
                                        if ((art.Id > 0) && (!listInsertedArticle.Contains(art.Id)))
                                        {
                                            listInsertedArticle.Add(art.Id);
                                            CustomProcessesViewData cpVD = new CustomProcessesViewData(eVD.Eingang.Auftraggeber, eVD.Eingang.ArbeitsbereichId, this._ctrMenu.GL_User);

                                            if (cpVD.ExistCustomProcess)
                                            {
                                                if (cpVD.CheckAndExecuteCustomProcess(art.Id, 0, 0, CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_CtrASNRead_tsbtnCreateEingang_Click))
                                                {
                                                    if (cpVD.Process_Novelis_AccessByArticleCert is CustomProcess_Novelis_AccessByArticleCert)
                                                    {
                                                        tmp = string.Empty;
                                                        switch (cpVD.Process_Novelis_AccessByArticleCert.Novelis_AccessByArticleCertStatus)
                                                        {
                                                            case enumCustumerProcessStatus_Novelis_AccessByArticleCert.ArticleBookedInSPL:
                                                                tmp = "      SPL: Artikel Zertifikat liegt nicht vor!";
                                                                break;
                                                            case enumCustumerProcessStatus_Novelis_AccessByArticleCert.ArticleCertifateExist:
                                                                tmp = "      Zertifikat liegt vor!";
                                                                break;
                                                            case enumCustumerProcessStatus_Novelis_AccessByArticleCert.NotSet:
                                                                tmp = "      Status ist nicht gesetzt!";
                                                                break;
                                                        }
                                                        //lvLogVDA4913.Items.Add(tmp);

                                                        this.Invoke((Action)(() =>
                                                        {
                                                            lvLogVDA4913.Items.Add(tmp);
                                                        }));
                                                    }
                                                }
                                            }
                                            string s = string.Empty;
                                        }
                                    }
                                }
                            }
                        }
                        //stopwatch.Stop();
                        //barVda.Text = "Anzahl: " + barVda.Maximum.ToString() + " | Dauer [s]: " + stopwatch.Elapsed.TotalSeconds.ToString("0.00");
                        this.Invoke((Action)(() =>
                        {
                            barVda.Text = "Anzahl: " + barVda.Maximum.ToString() + " | Dauer [s]: " + stopwatch.Elapsed.TotalSeconds.ToString("0.00");
                        }));
                        this.Invoke((Action)(() =>
                        {
                            lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " - Verarbeitung abgeschlossen....");
                            lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " - Aufstellung wird neu geladen...");
                        }));
                    }
                    this.ASN.dtASNForEingang.DefaultView.RowFilter = string.Empty;
                    this.tstbSelectedSearchField.Text = string.Empty;
                    //InitDGV();
                    InitDgvSwitchVda();
                }
                else
                {
                    //string message = string.Empty;
                    //message = "Es wurden keine ASN zur Verarbeitung ausgewählt!";
                    //lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                    //lvLogVDA4913.Items.Add(message);

                    this.Invoke((Action)(() =>
                    {
                        string message = string.Empty;
                        message = "Es wurden keine ASN zur Verarbeitung ausgewählt!";
                        lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                        lvLogVDA4913.Items.Add(message);
                    }));
                }
            }
            //stopwatch.Reset();

            //InitDgvSwitchVda();
        }
        /// <summary>
        /// 
        /// </summary>
        private void DoCreateEingangEdifact()
        {
            List<ctrASNRead_AsnEdifactView> ListEingangToCreate = new List<ctrASNRead_AsnEdifactView>();
            foreach (var item in asnVD.List_ctrAsnRead_AsnEdifactView)
            {
                if (item.Select)
                {
                    if (!ListEingangToCreate.Contains(item))
                    {
                        ListEingangToCreate.Add(item);
                    }
                }
            }
            if (ListEingangToCreate.Count > 0)
            {

                barVda.Minimum = 0;
                barVda.Value1 = 0;
                barVda.Maximum = ListEingangToCreate.Count;

                stopwatch.Reset();
                stopwatch.Start();
                foreach (var item in ListEingangToCreate)
                {
                    barVda.Value1++;
                    barVda.Text = barVda.Value1.ToString() + " von " + barVda.Maximum.ToString();

                    lvLogEDIFACT.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                    AsnReadViewData asnReadViewData = new AsnReadViewData();
                    //bool bReturn = asnReadViewData.CreateStoreInByAsnEdifactView(item, (int)_ctrMenu.GL_User.User_ID);
                    bool bReturn = asnReadViewData.CreateStoreInByAsnEdifactView(item.AsnMessage, item.eingang, item.ListArticleInEingang, (int)_ctrMenu.GL_User.User_ID);
                    if (!bReturn)
                    {
                        lvLogEDIFACT.Items.Add(asnReadViewData.Errortext);
                    }
                    else
                    {
                        lvLogEDIFACT.Items.Add(asnReadViewData.LogText);
                    }

                }
                barVda.Text = "Anzahl: " + barVda.Maximum.ToString() + " | Dauer [s]: " + stopwatch.Elapsed.TotalSeconds.ToString("0.00");
                stopwatch.Stop();

                this.tstbSelectedSearchField.Text = string.Empty;
                InitDGVEDIFACT();
            }
            else
            {
                string message = string.Empty;
                message = "Es wurden keine ASN zur Verarbeitung ausgewählt!";
                lvLogVDA4913.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                lvLogVDA4913.Items.Add(message);
            }
            stopwatch.Restart();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnCreateEingangEDIFACT_Click(object sender, EventArgs e)
        {
            lvLogEDIFACT.Items.Clear();
            if (clsMessages.Allgemein_SelectionInfoTextShow("Verarbeitung gestartet..."))
            {
                DoCreateEingangEdifact();
            }
        }
        ///<summary>ctrASNRead / tsbtnRefresh_Click</summary>
        ///<remarks>Liste aktualisieren</remarks>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDgvSwitchVda();
        }
        ///<summary>ctrASNRead / tsbtnExcel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnExcel_Click(object sender, EventArgs e)
        {
            bool openExportFile = false;
            LVS.clsExcel Excel = new clsExcel();
            string FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName;
            DataTable dtTmp = ASN.dtASNForEingang.DefaultView.ToTable(true, "ASN", "ASN-Datum", "AuftraggeberView", "EmpfaengerView", "VS-Datum", "LfsNr");
            string FilePath = Excel.ExportDataTableToWorksheet(dtTmp, AttachmentPath + "\\" + FileName);
            openExportFile = true;

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(FilePath);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "ASN List - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrASNRead / tsbtnExcel_Click</summary>
        ///<remarks></remarks>
        private void lvTempLog_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            //e.ToolTipText = e.ToString();
        }
        ///<summary>ctrArtSearchFilter / tstbProdNr_TextChanged</summary>
        ///<remarks></remarks>
        private void tstbProdNr_TextChanged(object sender, EventArgs e)
        {
            //if (tstbSelectedSearchField.Text.Equals(string.Empty))
            //{
            //    SetFilterforDGV(ref this.dgv, true);
            //}
        }
        private void tstbEdifactInputSearchValue_TextChanged(object sender, EventArgs e)
        {
            //if (tstbEdifactInputSearchValue.Text.Equals(string.Empty))
            //{
            //    SetFilterforDGVEdifact(ref this.dgv, true);
            //}
        }
        ///<summary>ctrArtSearchFilter / tsbtnDeleteASN_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDeleteASN_Click(object sender, EventArgs e)
        {
            var tmpDelete = List_ctrAsnRead_AsnVdaViewDataSource.Where(x => x.Select == true).ToList();
            if (tmpDelete.Count > 0)
            {
                if (clsMessages.DeleteAllgemein())
                {
                    if (rtsDisplayProcess.Value)
                    {
                        if (this.List_ctrAsnRead_AsnVdaViewDataSource.Count > 0)
                        {
                            //var tmpDelete = List_ctrAsnRead_AsnVdaViewDataSource.Where(x => x.Select == true).ToList();
                            if (tmpDelete.Count > 0)
                            {
                                foreach (var itm in tmpDelete)
                                {
                                    AsnViewData asnVD = new AsnViewData(itm.AsnMessage);
                                    asnVD.asnHead.IsRead = true;
                                    asnVD.Update();
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ASN.dtASNForEingang.DefaultView.RowFilter = "Select=True";
                        DataTable dtTmpASNToDelete = this.ASN.dtASNForEingang.DefaultView.ToTable();
                        if (dtTmpASNToDelete.Rows.Count > 0)
                        {
                            clsLager Lager = new clsLager();
                            Lager.sys = this._ctrMenu._frmMain.system;
                            Lager._GL_User = this.GL_User;
                            Lager._GL_System = this._ctrMenu._frmMain.GL_System;

                            Lager.DisableASN(dtTmpASNToDelete);
                            this.ASN.dtASNForEingang.DefaultView.RowFilter = string.Empty;
                            this.tstbSelectedSearchField.Text = string.Empty;
                            //InitDGV();
                        }
                    }
                    InitDgvSwitchVda();
                }
            }
            else
            {
                string strMes = "Es wurden keine Datensätze ausgewählt!";
                clsMessages.Allgemein_InfoTextShow(strMes);
            }
        }
        ///<summary>ctrArtSearchFilter / tsbtnDeleteASN_Click</summary>
        ///<remarks></remarks>
        private void dgv_RowFormatting(object sender, RowFormattingEventArgs e)
        {

        }
        /// <summary>  
        ///             Formatiert Zeilen im EDIFACT-Grid und setzt Select-Zelle auf ReadOnly,   
        ///             wenn Auftraggeber oder Empfänger fehlen.  
        /// </summary>
        private void dgvEDIFACT_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            // Sicherheits-Check: RowElement und RowInfo vorhanden?  
            if (e?.RowElement?.RowInfo?.Cells == null)
                return;

            var cells = e.RowElement.RowInfo.Cells;

            // --- Auftraggeber auslesen ---  
            string strSender = string.Empty;
            var senderCell = cells["AuftraggeberView"];
            if (senderCell?.Value != null)
            {
                strSender = senderCell.Value.ToString()?.Trim() ?? string.Empty;
            }

            // --- Empfänger auslesen ---  
            string strEmpf = string.Empty;
            var empfCell = cells["EmpfaengerView"];
            if (empfCell?.Value != null)
            {
                strEmpf = empfCell.Value.ToString()?.Trim() ?? string.Empty;
            }

            // --- Select-Zelle auf ReadOnly setzen, wenn Daten fehlen ---  
            var selectCell = cells["Select"];
            if (selectCell != null)
            {
                // ReadOnly = true, wenn Auftraggeber ODER Empfänger leer sind  
                bool isDataMissing = string.IsNullOrWhiteSpace(strSender) ||
                                     string.IsNullOrWhiteSpace(strEmpf);

                selectCell.ReadOnly = isDataMissing;

                // Optional: Visuelle Kennzeichnung (z.B. Hintergrundfarbe)  
                if (isDataMissing)
                {
                    e.RowElement.DrawFill = true;
                    e.RowElement.BackColor = Color.LightGray;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                }
                else
                {
                    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                }

                //string strSender = string.Empty;
                //string strEmpf = string.Empty;

                //if (e.RowElement.RowInfo.Cells["AuftraggeberView"] != null)
                //{
                //    strSender = e.RowElement.RowInfo.Cells["AuftraggeberView"].Value.ToString();
                //}
                //if (e.RowElement.RowInfo.Cells["AuftraggeberView"] != null)
                //{
                //    strEmpf = e.RowElement.RowInfo.Cells["EmpfaengerView"].Value.ToString();
                //}

                //if (e.RowElement.RowInfo.Cells["Select"] != null)
                //{
                //    if (
                //        (strSender.Equals(string.Empty)) ||
                //        (strEmpf.Equals(string.Empty))
                //    )
                //    {
                //        e.RowElement.RowInfo.Cells["Select"].ReadOnly = true;
                //    }
                //    else
                //    {
                //        e.RowElement.RowInfo.Cells["Select"].ReadOnly = false;
                //    }
                //}
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCtrAsnRead_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDgvForSelectedTab(tabCtrAsnRead.SelectedIndex);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefreshEDIFACT_Click(object sender, EventArgs e)
        {
            InitDGVEDIFACT();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAllCheckEDIFACT_Click(object sender, EventArgs e)
        {
            SetDgvEdifactAllASNSelectOrUnselect(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAllUncheckEDIFACT_Click(object sender, EventArgs e)
        {
            SetDgvEdifactAllASNSelectOrUnselect(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelEDIFACT_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeleteEDIFACT_Click(object sender, EventArgs e)
        {
            var listToDelete = asnVD.List_ctrAsnRead_AsnEdifactView.Where(x => x.Select == true).ToList();
            if (listToDelete.Count > 0)
            {
                if (clsMessages.DeleteAllgemein())
                {
                    if (listToDelete.Count > 0)
                    {
                        foreach (var item in listToDelete)
                        {
                            AsnViewData aVD = new AsnViewData(item.ASN, GL_User);
                            aVD.asnHead.IsRead = true;
                            aVD.Update();
                        }
                        InitDGVEDIFACT();
                    }
                }
            }
            else
            {
                string strMes = "Es wurden keine Datensätze ausgewählt!";
                clsMessages.Allgemein_InfoTextShow(strMes);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnCloseEDIFACT_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrASNRead();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCtrAsnRead_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabCtrAsnRead.TabPages[e.Index];
            Color tabColor = Color.Transparent; // Set the desired color for the tab

            if ((page != null) && (page.Text.Equals("VDA4913")))
            {
                if (this.dgv.RowCount > 0)
                {
                    tabColor = Color.GreenYellow;
                    page.Tag = "1";
                }
                else
                {
                    page.Tag = "0";
                }
            }
            if ((page != null) && (page.Text.Equals("EDIFACT")))
            {
                if (this.dgvEDIFACT.RowCount > 0)
                {
                    tabColor = Color.GreenYellow;
                    page.Tag = "1";
                }
                else
                {
                    page.Tag = "0";
                }
            }
            e.Graphics.FillRectangle(new SolidBrush(tabColor), e.Bounds);

            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            TextRenderer.DrawText(e.Graphics, page.Text, e.Font, paddedBounds, page.ForeColor);
        }
        ///<summary>ctrArtSearchFilter / SetFilterforDGV</summary>
        ///<remarks></remarks>
        public void SetFilterforDGV(ref RadGridView myDGV, bool bClear)
        {
            string searchText = tstbSelectedSearchField.Text.Trim();
            foreach (GridViewRowInfo row in myDGV.Rows)
            {
                row.IsVisible = string.IsNullOrEmpty(searchText); // Standardmäßig alles anzeigen

                foreach (GridViewCellInfo cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText.ToLower()))
                    {
                        row.IsVisible = true;
                        break; // Wenn ein Treffer gefunden wurde, nächste Zeile prüfen
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(searchText))
                        {
                            row.IsVisible = true;
                        }
                        else
                        {
                            row.IsVisible = false;
                        }
                    }
                }

                //---suche in child
                if (!row.IsVisible)
                {
                    if (row.DataBoundItem is ctrASNRead_AsnVdaView child)
                    {
                        var childData = (ctrASNRead_AsnVdaView)row.DataBoundItem;
                        if (childData.dtAsnValues.Rows.Count > 0)
                        {
                            DataRow[] tmpRows = childData.dtAsnValues.Select("Value like '" + searchText + "%'");
                            row.IsVisible = (tmpRows.Length > 0);
                        }
                    }
                }
            }
        }
        ///<summary>ctrArtSearchFilter / SetFilterforDGV</summary>
        ///<remarks></remarks>
        ///
        public void SetFilterforDGVEdifact(ref RadGridView myDGV, bool bClear)
        {
            string searchText = tsbtnEdifactInputSearchValue.Text.Trim();
            foreach (GridViewRowInfo row in myDGV.Rows)
            {
                row.IsVisible = string.IsNullOrEmpty(searchText); // Standardmäßig alles anzeigen

                foreach (GridViewCellInfo cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText.ToLower()))
                    {
                        row.IsVisible = true;
                        break; // Wenn ein Treffer gefunden wurde, nächste Zeile prüfen
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(searchText))
                        {
                            row.IsVisible = true;
                        }
                        else
                        {
                            row.IsVisible = false;
                        }
                    }
                }

                //---suche in child
                if (!row.IsVisible)
                {
                    if (row.DataBoundItem is ctrASNRead_AsnEdifactView child)
                    {
                        var childData = (ctrASNRead_AsnEdifactView)row.DataBoundItem;
                        if (childData.ListArticleInEingang.Count > 0)
                        {
                            string DataField = tscbEdifactSearchField.SelectedItem.ToString().Trim();
                            List<Articles> tmpList = new List<Articles>();
                            switch (DataField)
                            {
                                case "Produktionsnummer":
                                    tmpList = childData.ListArticleInEingang.Where(x => x.Produktionsnummer != null).Where(x => x.Produktionsnummer.ToLower().Contains(searchText.ToLower())).ToList();
                                    break;
                                case "Charge":
                                    tmpList = childData.ListArticleInEingang.Where(x => x.Charge != null).Where(x => x.Charge.ToLower().Contains(searchText.ToLower())).ToList();
                                    break;
                            }
                            row.IsVisible = (tmpList.Count > 0);
                        }
                    }
                }
            }
        }
        ///<summary>ctrASNRead / tsbtnSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            SetFilterforDGV(ref this.dgv, false);
        }
        private void tsbtnSearchEDIFACT_Click(object sender, EventArgs e)
        {
            SetFilterforDGVEdifact(ref dgvEDIFACT, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtsDisplayProcess_ValueChanged(object sender, EventArgs e)
        {
            string strMessage = string.Empty;
            if (rtsDisplayProcess.Value)
            {
                strMessage = const_rtsDisplayProcess_Text_UseProcessNew;
            }
            else
            {
                strMessage = const_rtsDisplayProcess_Text_UseProcessOld;
            }
            lToggleSwitschInfo.Text = strMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CellFormatting(object sender, CellFormattingEventArgs e)
        {

            // Sicherheits-Checks  
            if (e?.CellElement == null || e.Column == null)
                return;

            // Rohwert null-sicher holen  
            var raw = e.CellElement.Value;
            var rawText = raw?.ToString();

            // Wenn gar kein Wert da ist, nichts formatieren  
            if (raw == null || string.IsNullOrWhiteSpace(rawText))
                return;

            // 1) Spezielle Behandlung für ASN  
            if (string.Equals(e.Column.Name, "ASN", StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(rawText, out int asn))
                {
                    // Keine besondere Formatierung nötig, aber du könntest z.B. Leading Zeros entfernen  
                    e.CellElement.Text = asn.ToString();
                }
                return; // Rest überspringen  
            }

            // 2) Generische Behandlung nach Spaltentyp  
            var columnType = e.Column.GetType();

            if (columnType == typeof(GridViewDecimalColumn))
            {
                // Dezimalformat  
                if (decimal.TryParse(rawText, out decimal decValue))
                {
                    // Kultur-spezifisch, z.B. de-DE  
                    e.CellElement.Text = decValue.ToString("N2", CultureInfo.CreateSpecificCulture("de-DE"));
                }
            }
            else if (columnType == typeof(GridViewDateTimeColumn))
            {
                // Datumsformat  
                if (DateTime.TryParse(rawText, out DateTime dateValue))
                {
                    // Kurzes Datum  
                    e.CellElement.Text = dateValue.ToString("d", CultureInfo.CreateSpecificCulture("de-DE"));
                }
            }

            //if (e.Column.Name.Equals("ASN"))
            //{
            //    if (int.TryParse(e.CellElement.Value.ToString(), out int value))
            //    {
            //        e.CellElement.Text = value.ToString();
            //    }
            //}
            //else
            //{
            //    switch (e.Column.GetType())
            //    {
            //        case Type t when t == typeof(GridViewDecimalColumn):
            //            if (decimal.TryParse(e.CellElement.Value.ToString(), out decimal value))
            //            {
            //                e.CellElement.Text = value.ToString("N2"); // z. B. 1.234,56
            //            }
            //            break;
            //        case Type t when t == typeof(GridViewDateTimeColumn):
            //            if (DateTime.TryParse(e.CellElement.Value.ToString(), out DateTime dateValue))
            //            {
            //                e.CellElement.Text = dateValue.ToString("d"); // z. B. 31.12.2023
            //            }
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnEdifactInputSearchClear_Click_1(object sender, EventArgs e)
        {
            tsbtnEdifactInputSearchValue.Text = string.Empty;
            tscbEdifactSearchField.SelectedIndex = 0;
            SetFilterforDGVEdifact(ref dgvEDIFACT, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnClearSearchField_Click(object sender, EventArgs e)
        {
            tstbSelectedSearchField.Text = string.Empty;
            tscVDA4913SearchField.SelectedIndex = 0;
            SetFilterforDGV(ref dgv, false);
        }
    }
}
