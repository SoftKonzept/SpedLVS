using LVS;
using LVS.Dokumente;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;



namespace Sped4.Controls
{
    public partial class AFKalenderItemTour : AFKalenderItem
    {
        public const string const_ctrName = "KalenderItemTour";
        public new Globals._GL_USER GL_User;
        public delegate void ThreadCtrInvokeEventHandler();
        public clsSystem Sys;
        public clsKommission Kommission;
        public clsTour Tour = new clsTour();

        internal clsDispoCheck DispoCheck = new clsDispoCheck();
        public bool ToRefresh = false;
        frmDispo Kalender;
        ctrAufträge AuftragCtr;
        public ctrMenu ctrMenu;

        public delegate void ctrAuftragRefreshEventHandler();
        public event ctrAuftragRefreshEventHandler ctrAuftragRefresh;

        public delegate void ctrCalRowRefreshEventHandler();
        public event ctrCalRowRefreshEventHandler ctrCalRowRefresh;

        ///<summary>AFKalenderItemTour / AFKalenderItemTour</summary>
        ///<remarks>Initialisierung.</remarks>
        //public AFKalenderItemTour(frmDispoKalender _Kalender)
        public AFKalenderItemTour(frmDispo _Kalender)
        {
            InitializeComponent();
            Kalender = _Kalender;
            GL_User = Kalender.GL_User;
            AuftragCtr = _Kalender.AuftragCtr;
            ctrMenu = _Kalender.menue;
            this.Sys = ctrMenu._frmMain.system;
            this.Name = "KalenderItemTour";
            DispoCheck.BenutzerID = GL_User.User_ID;

            Kommission = new clsKommission();
            Kommission.InitClass(this.ctrMenu._frmMain.GL_User, this.ctrMenu._frmMain.GL_System, this.ctrMenu._frmMain.system);

            this.Height = 30;
        }
        ///<summary>AFKalenderItemTour / AFKalenderItemTour_Load</summary>
        ///<remarks>CtrFarbe und Height wird gesetzt.</remarks>
        private void AFKalenderItemTour_Load(object sender, EventArgs e)
        {
            //Farbe darf sich nur ändern, wenn alle Stati der einzelnen Kommissionen 
            //entsprechend sind
            this.ColorFrom = Color.Blue;
            this.ColorTo = Color.DarkBlue;
            this.Height = 55;


            if (this.Tour.StatusTour == 7)
            {
                this.ColorFrom = Color.FromArgb(255, 128, 0);
                this.ColorTo = Color.WhiteSmoke;
            }
            else if (this.Tour.StatusTour == 8)
            {
                this.ColorFrom = Color.GhostWhite;
                this.ColorTo = Color.WhiteSmoke;
            }
            else
            {
                //this.ColorFrom = Color.FromArgb(55, 210, 47);
                //this.ColorTo = Color.FromArgb(17, 67, 14);
                this.ColorFrom = Color.Blue;
                this.ColorTo = Color.DarkBlue;
            }
            SetImageInfoTour();
        }
        ///<summary>AFKalenderItemTour / SetImageInfoTour</summary>
        ///<remarks>Setzt die Image Infos für die Tour.</remarks>
        private void SetImageInfoTour()
        {
            //Image für Dokumenteninfo wird gesetzt
            if (this.Tour.DocsOK)
            {
                this.ImagePicRight = Sped4.Properties.Resources.form_green_edit;
            }
            else
            {
                this.ImagePicRight = null;
            }
            //Image für Fahrerkontakt wird gesetzt
            if (this.Tour.FahrerKontakt)
            {
                this.ImagePicLeft = Sped4.Properties.Resources.schoolboy;
            }
            else
            {
                this.ImagePicLeft = null;
            }
        }
        ///<summary>AFKalenderItemTour / AFKalenderItemTour_Paint</summary>
        ///<remarks>Zeichnen des Ctr.</remarks>
        private void AFKalenderItemTour_Paint(object sender, PaintEventArgs e)
        {
            Rectangle oRectTextOben = new Rectangle(5, 2, this.Width - (2 * 25), 18);
            StringFormat dfOben = new StringFormat();
            dfOben.FormatFlags = StringFormatFlags.LineLimit;
            dfOben.Alignment = StringAlignment.Near;

            oRectTextOben.X++;
            oRectTextOben.Y++;
            // Daten auf das Rectangel geschrieben
            e.Graphics.DrawString("|-> " + Tour.StartOrt, myFontStyle, Brushes.Black, oRectTextOben, dfOben);
            oRectTextOben.X--;
            oRectTextOben.Y--;
            e.Graphics.DrawString("|-> " + Tour.StartOrt, myFontStyle, Brushes.White, oRectTextOben, dfOben);
            /***
            DrawFormat.Alignment = StringAlignment.Far;
            oRectText.X++;
            oRectText.Y++;
            e.Graphics.DrawString(Tour.EndOrt, myFontStyle, Brushes.Black, oRectText, DrawFormat);
            oRectText.X--;
            oRectText.Y--;
            e.Graphics.DrawString(Tour.EndOrt, myFontStyle, Brushes.White, oRectText, DrawFormat);
             ****/

            //Rectangle oRectTextUnten = new Rectangle(5, 35, this.Width - (2 * 25), 20);
            Rectangle oRectTextUnten = new Rectangle(5 + 25, 35, this.Width - (2 * 25), 18);
            StringFormat dfUnten = new StringFormat();
            dfUnten.FormatFlags = StringFormatFlags.LineLimit;
            dfUnten.Alignment = StringAlignment.Far;
            oRectTextUnten.X++;
            oRectTextUnten.Y++;
            // Daten auf das Rectangel geschrieben
            e.Graphics.DrawString(Tour.EndOrt + " ->|", myFontStyle, Brushes.Black, oRectTextUnten, dfUnten);
            oRectTextUnten.X--;
            oRectTextUnten.Y--;
            e.Graphics.DrawString(Tour.EndOrt + " ->|", myFontStyle, Brushes.White, oRectTextUnten, dfUnten);

            //Anzahl
            Rectangle oRectTextAnzahl = new Rectangle(5, 20, this.Width - (2 * 25), 18);
            StringFormat dfCenter = new StringFormat();
            dfCenter.FormatFlags = StringFormatFlags.LineLimit;
            dfCenter.Alignment = StringAlignment.Center;
            oRectTextAnzahl.X++;
            oRectTextAnzahl.Y++;
            // Daten auf das Rectangel geschrieben
            e.Graphics.DrawString(Tour.AnzahlKommissionen.ToString() + " | " + Functions.FormatDecimal(Tour.TourGewicht) + " [kg]", myFontStyle, Brushes.Black, oRectTextAnzahl, dfCenter);
            oRectTextAnzahl.X--;
            oRectTextAnzahl.Y--;
            e.Graphics.DrawString(Tour.AnzahlKommissionen.ToString() + " | " + Functions.FormatDecimal(Tour.TourGewicht) + " [kg]", myFontStyle, Brushes.White, oRectTextAnzahl, dfCenter);

        }
        ///<summary>AFKalenderItemTour / myKalenderItemTour_Refresh</summary>
        ///<remarks>Ctr auktualisieren</remarks>
        public void myKalenderItemTour_Refresh()
        {
            SetImageInfoTour();
            base.Refresh(); //Refresh AFKalenderItem wegen den Images
            this.Refresh();
        }
        ///<summary>AFKalenderItemTour / AFKalenderItemTour_MouseClick</summary>
        ///<remarks>Ctr Menü anzeigen</remarks>
        private void AFKalenderItemTour_MouseClick(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            info.SetToolTip(this, this.Tour.MouseOverInfo);

            if (e.Button == MouseButtons.Right)
            {
                base.bMouseClick = true;
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }
        ///<summary>AFKalenderItemTour / AFKalenderItemTour_MouseDoubleClick</summary>
        ///<remarks>Öffnet die Frm mit den Details der Tour.</remarks>
        private void AFKalenderItemTour_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            base.bMouseClick = true;
            if ((GL_User.write_Disposition) || (GL_User.read_Disposition))
            {
                if (e.Button == MouseButtons.Left)
                {
                    OpenFrmKommiDetailsPanel();
                }
            }
        }
        ///<summary>AFKalenderItemTour / AFKalenderItemTour_MouseDoubleClick</summary>
        ///<remarks>Öffnet die Frm mit den Details der Tour.</remarks>
        private void AFKalenderItemTour_MouseHover(object sender, EventArgs e)
        {
            //ToolTip info = new ToolTip();
            //info.SetToolTip(this, this.Tour.MouseOverInfo);

        }
        ///<summary>AFKalenderItemTour / miDelete_Click</summary>
        ///<remarks>Löschen der gesamten Tour in folgenden Tabellen.</remarks>
        private void miDelete_Click(object sender, EventArgs e)
        {
            DeleteTourFromDispoKalender();
        }
        ///<summary>AFKalenderItemTour / miDelete_Click</summary>
        ///<remarks>Löschen der gesamten Tour in folgenden Tabellen:
        ///         -Kommission
        ///         -DispoCheck
        ///         -Tour
        ///         -TourKontaktInfo
        ///         -in AuftragPos Status zurück auf 2 setzen, damit wieder disponiert werden kann.</remarks>
        public void DeleteTourFromDispoKalender()
        {
            GL_User = Kalender.GL_User;
            if (GL_User.write_Disposition)
            {
                if (clsTour.ExistTourID(this.GL_User, this.Tour.ID))
                {
                    //Tour aus Dispoplan entfernen
                    this.Tour.DeleteTour();
                    if (this.Kalender != null)
                    {
                        this.Kalender.KalenderRefresh();
                        decimal decTmp = this.Tour.KFZ_ZM;
                    }
                    if (Functions.IsCtrAlreadyOpen(ref this.AuftragCtr._ctrMenu) != null)
                    {
                        this.Kalender.AuftragCtr.InitDGV();
                    }
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        /*******************+ Start TEST ********************/
        //
        private void ReSetStatus()
        {
            clsAuftragsstatus ast = new clsAuftragsstatus();
            ast.Auftrag_ID = Kommission.AuftragID;
            ast.AuftragPos = Kommission.AuftragPos;
            ast.SetStatusBackFromDispo();
        }
        //
        private void DeleteLieferscheine()
        {
            //Baustelle Lieferscheine
            //löschen Lieferscheine
            if (clsLieferscheine.LieferscheinExist(Kommission.AuftragPosTableID))
            {
                clsLieferscheine.DeleteLieferscheinByAP_ID(Kommission.AuftragPosTableID);
            }
        }

        //
        //beinhaltet auch Info zu Fahrer und Papiere 
        private void miDetails_Click(object sender, EventArgs e)
        {
            if (this.Kalender.menue != null)
            {
                //OpenFrmKommiDetailsPanel();
                this.Kalender._ctrKalenderItemTour = this;
                this.ctrMenu = this.ctrMenu.OpenFrmTMP(this.Kalender);
            }
        }
        //
        private void OpenFrmKommiDetailsPanel()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmKommiDetailsPanel)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmKommiDetailsPanel));
            }
        }
        //
        private void miUpdateEntladeZeit_Click(object sender, EventArgs e)
        {
            this.Kalender._ctrKalenderItemTour = this;
            this.ctrMenu = this.ctrMenu.OpenFrmDispoTourChange(this.Kalender);
        }

        private void dokumenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet(); //Leer

            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmPrintCenter)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmPrintCenter));
            }
            frmPrintCenter pC = new frmPrintCenter();
            pC._AuftragID = Kommission.AuftragID;
            pC._AuftragPos = Kommission.AuftragPos;
            pC._AuftragPosTableID = Kommission.AuftragPosTableID;
            pC.GL_User = Kalender.GL_User;
            pC.Show();
            pC.BringToFront();
        }
        //
        //
        //
        private void dokumentScannenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AuftragCtr._ctrMenu.OpenScanFrm(this.Kommission.Tour.Auftrag.ID, this.Kommission.AuftragPosTableID, 0, 0, this);
        }
        //

    }
}
