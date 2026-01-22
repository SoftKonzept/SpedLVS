using LVS;
using Sped4.Classes;
using Sped4.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrTourDetails : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu ctrMenu;
        public frmDispoKalender Kalender;

        internal DateTime TourStartZeit;
        internal DateTime TourEndZeit;
        private clsPointTime PointTime = new clsPointTime();
        internal bool bTourIsLoaded = false;
        internal bool bFirstLoad = true;
        internal bool bFirstKommiDatenSet = true;

        internal decimal SelectedKommiID = 0;
        internal AFKalenderItemTour ctrAFKalenderItemTour;

        ///<summary>ctrTourDetails / ctrTourDetails</summary>
        ///<remarks>Init Ctr</remarks>
        public ctrTourDetails()
        {
            InitializeComponent();

        }
        ///<summary>ctrTourDetails / ctrTourDetails_Load</summary>
        ///<remarks></remarks>
        private void ctrTourDetails_Load(object sender, EventArgs e)
        {
            ctrAFKalenderItemTour = this.Kalender._ctrKalenderItemTour;
            InitFrm();
        }
        ///<summary>ctrTourDetails / InitFrm</summary>
        ///<remarks>Initialisiert die Form und setzt alle Daten.</remarks>
        public void InitFrm()
        {
            if (ctrAFKalenderItemTour != null)
            {
                this.nudKommiBelPos.Maximum = (decimal)this.ctrAFKalenderItemTour.Kommission.maxBeladePos;
                this.nudKommiEntlPos.Maximum = (decimal)this.ctrAFKalenderItemTour.Kommission.maxEntladePos;
                SelectedKommiID = this.ctrAFKalenderItemTour.Kommission.ID;
                TourStartZeit = this.ctrAFKalenderItemTour.Tour.StartZeit;
                TourEndZeit = this.ctrAFKalenderItemTour.Tour.EndZeit;
                SetTourValueToFrm();
                LoadKommiDatenForGrd();
                SetKommiValueToFrm();
            }
        }
        ///<summary>ctrTourDetails / TourPanel_Paint</summary>
        ///<remarks>Zeichnet den Touren Zeitstrahl.</remarks>
        private void InitKalenderVehicleRow(decimal myZMId)
        {
            AFKalenderRow ctrDispo = new AFKalenderRow();
            ctrDispo.bPaintOtherColor = true;
            ctrDispo.xStartPoint = PointTime.GetPointFromTime(TourStartZeit);
            ctrDispo.xEndPoint = PointTime.GetPointFromTime(TourEndZeit);

            clsFahrzeuge Fahrz = new clsFahrzeuge();
            Fahrz.BenutzerID = this.GL_User.User_ID;

            //---------------- Zugmaschine / Kalenderrow  ------------------------------
            Fahrz.ID = myZMId;
            Fahrz.Fill();
            ctrDispo.Dock = System.Windows.Forms.DockStyle.Top;
            ctrDispo.Fahrzeug.ID = Fahrz.ID;
            ctrDispo.Fahrzeug.KFZ = Fahrz.KFZ;
            ctrDispo.Fahrzeug.KIntern = Fahrz.KIntern;
            ctrDispo.Fahrzeug.AbgasNorm = Fahrz.AbgasNorm;
            ctrDispo.Refresh();

            this.TourPanel.Controls.Add(ctrDispo);
            this.TourPanel.Controls.SetChildIndex(ctrDispo, 0);
            ctrDispo.Show();

        }
        ///<summary>ctrTourDetails / TourPanel_Paint</summary>
        ///<remarks>Zeichnet den Touren Zeitstrahl.</remarks>
        private void TourPanel_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                DateTime DateFrom = TourStartZeit.Date;
                DateTime DateTo = TourEndZeit.Date.AddHours(24);
                const Int32 BeginTimeFromLeft = 104;

                TimeSpan TSTage = DateTo.Subtract(DateFrom);
                Int32 Tage = (Int32)TSTage.TotalDays;
                if (TSTage.Hours > 0)
                {
                    Tage++;
                }
                Int32 Std = (Int32)TSTage.TotalHours;
                Int32 H = 24 * Tage;
                Int32 Stunde = 0;
                Int32 x = 0;
                Pen p = new Pen(Color.White, 1);
                Pen p0 = new Pen(Color.DarkBlue, 1);
                Pen p1 = new Pen(Color.Red, 2);
                DateTime Time = default(DateTime);
                AFKalenderRow Fahrzeug;

                Rectangle oRect;
                Rectangle oTempRect = default(Rectangle); //schaut ob sich das letzte und das aktuelle Rect der Zeit überschneiden
                Font font;

                StringFormat DrawFormat = new StringFormat();
                DrawFormat.Alignment = StringAlignment.Near;
                DrawFormat.LineAlignment = StringAlignment.Near;

                Point pointa = default(Point);
                Point pointb = default(Point);
                Point pointtempa = default(Point);
                Point pointtempb = default(Point);
                pointa.Y = 20;
                pointa.X = 0;
                pointb.Y = this.TourPanel.Height;
                pointb.X = 0;
                pointtempa.Y = 30;
                pointtempa.X = 0;
                pointtempb.Y = this.TourPanel.Height;
                pointtempb.X = 0;

                Rectangle TempRect = new Rectangle(0, 0, TimePanel.Width, 5);
                Brush TempBrush = new LinearGradientBrush(TempRect, this.BackColor, TourPanel.BackColor, LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(TempBrush, TempRect);
                e.Graphics.DrawLine(new Pen(Color.FromArgb(255, 128, 0), 1), 0, 0, this.Width, 0);

                PointTime.ClearPointTime();
                Stunde = 0;

                DateTime dtWE = DateFrom;

                //Stundenleiste wird erzeugt
                //for (Int32 i = DateFrom.Hour; i <= H; i++)
                for (Int32 i = 0; i <= H; i++)
                {

                    if (Stunde == 24)
                    {
                        Stunde = 0;
                        dtWE = dtWE.AddDays(1);
                    }
                    Time = DateFrom;
                    //Time = GL_User.us_dtDispoVon;

                    x = BeginTimeFromLeft + (i * (Int32)Math.Floor((decimal)TourPanel.Width - (decimal)BeginTimeFromLeft - 20) / H);
                    pointa.X = x;
                    pointb.X = pointa.X;

                    if (Stunde == 0)
                    {
                        e.Graphics.DrawLine(p0, pointa, pointb);
                    }
                    else
                    {
                        e.Graphics.DrawLine(p, pointa, pointb);
                    }

                    Time = Time.AddHours(i);
                    PointTime.AddPointTime(pointa.X, Time);

                    if (pointtempa.X > 0)
                    {
                        pointtempa.X = pointtempa.X + (pointa.X - pointtempa.X) / 2;
                        pointtempb.X = pointtempb.X + (pointb.X - pointtempb.X) / 2;
                        e.Graphics.DrawLine(p, pointtempa, pointtempb);
                        Time = Time.AddMinutes(-30);
                        PointTime.AddPointTime(pointtempa.X, Time);
                    }

                    pointtempa.X = pointa.X;
                    pointtempb.X = pointb.X;

                    font = new System.Drawing.Font("Microsoft Sans Serif", 7.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
                    if (Stunde == 0)
                    {
                        //------------- Tag + Datum  
                        // hier können die Anfangsbuchstaben Tage eingefügt werden
                        //oRect = new Rectangle(x - 57, 2, 65, 20);
                        oRect = new Rectangle(x, 2, 100, 20);
                        e.Graphics.DrawString(DateFrom.AddHours(i).ToString("ddd") + " " + DateFrom.AddHours(i).ToString("dd.MM.yyyy"), font, Brushes.White, oRect, DrawFormat);
                        oRect.X -= 1;
                        oRect.Y -= 1;
                        e.Graphics.DrawString(DateFrom.AddHours(i).ToString("ddd") + " " + DateFrom.AddHours(i).ToString("dd.MM.yyyy"), font, Brushes.Black, oRect, DrawFormat);
                    }
                    //----- Zeit Stunde  -------------------- 
                    oRect = new Rectangle(x - 17, 15, 25, 20);
                    if (!oTempRect.IntersectsWith(oRect))
                    {
                        e.Graphics.DrawString(FormatTime(Stunde), font, Brushes.Black, oRect, DrawFormat);
                        oRect.X -= 1;
                        oRect.Y -= 1;
                        e.Graphics.DrawString(FormatTime(Stunde), font, Brushes.White, oRect, DrawFormat);
                        oTempRect = oRect;
                    }
                    // }
                    Stunde += 1;
                }

                //Läd die Kalender Fahrzeug Row
                if (!bTourIsLoaded)
                {
                    InitKalenderVehicleRow(this.ctrAFKalenderItemTour.Tour.KFZ_ZM);
                    bTourIsLoaded = true;
                }

                //TODO: schaun was schneller ist
                //foreach (Control ctr in this.VehicelPanel.Controls)
                foreach (AFKalenderRow ctr in this.TourPanel.Controls.Find("KalenderRow", true))
                {
                    try
                    {
                        Fahrzeug = (AFKalenderRow)ctr;          //ZM
                        Fahrzeug.XLines = PointTime.GetXArray();
                        Fahrzeug.Refresh();
                    }
                    catch
                    {
                    }
                }

                SetKommiCtrForTourPanel();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        ///<summary>ctrTourDetails / FormatTime</summary>
        ///<remarks>.</remarks>
        private string FormatTime(Int32 Time)
        {
            if (Time >= 10)
            {
                return Time.ToString();
            }
            else
            {
                return "0" + Time.ToString();
            }
        }
        ///<summary>ctrTourDetails / FormatTime</summary>
        ///<remarks>.</remarks>
        private void TourPanel_SizeChanged(object sender, EventArgs e)
        {
            this.TourPanel.Refresh();
        }
        ///<summary>ctrTourDetails / RefreshTourPanel</summary>
        ///<remarks>.</remarks>
        public void RefreshTourPanel()
        {
            foreach (AFKalenderItem ctr in this.TourPanel.Controls.Find("KalenderRow", true))
            {
                this.TourPanel.Controls.Remove(ctr);
            }
            bTourIsLoaded = false;
            this.TourPanel.Refresh();
            this.TimePanel.Refresh();
        }
        ///<summary>ctrTourDetails / SetTourValueToFrm</summary>
        ///<remarks>Setzt die Tourdaten.</remarks>
        private void SetTourValueToFrm()
        {
            tslTour.Text = "[Tourdaten zur ID: " + this.ctrAFKalenderItemTour.Tour.ID.ToString() + "]";
            tbZM.Text = clsFahrzeuge.GetKFZByID(this.GL_User, this.ctrAFKalenderItemTour.Tour.KFZ_ZM);
            tbAufliefer.Text = clsFahrzeuge.GetKFZByID(this.GL_User, this.ctrAFKalenderItemTour.Tour.KFZ_A);
            tbFahrer.Text = clsPersonal.GetNameByID(this.ctrAFKalenderItemTour.Tour.PersonalID);
            tbKommiAnzahl.Text = this.ctrAFKalenderItemTour.Tour.AnzahlKommissionen.ToString();
            tbKMTour.Text = this.ctrAFKalenderItemTour.Tour.km.ToString();
            tbKMLeer.Text = this.ctrAFKalenderItemTour.Tour.kmLeer.ToString();

            dtpTourStartZeit.Value = this.ctrAFKalenderItemTour.Tour.StartZeit;
            nud_TourSZHour.Value = (decimal)this.ctrAFKalenderItemTour.Tour.StartZeit.Hour;
            nud_TourSZMin.Value = (decimal)this.ctrAFKalenderItemTour.Tour.StartZeit.Minute;

            dtpTourEndZeit.Value = this.ctrAFKalenderItemTour.Tour.EndZeit;
            nud_TourEZHour.Value = this.ctrAFKalenderItemTour.Tour.EndZeit.Hour;
            nud_TourEZMin.Value = this.ctrAFKalenderItemTour.Tour.EndZeit.Minute;
        }
        ///<summary>ctrTourDetails / SetTourValueToFrm</summary>
        ///<remarks>Setzt die Tourdaten.</remarks>
        private void SetKommiValueToFrm()
        {
            tslKommi.Text = "[Kommission zur ID: " + this.ctrAFKalenderItemTour.Kommission.ID.ToString() +
                            "- Auftragsposition: " + this.ctrAFKalenderItemTour.Kommission.AuftragID.ToString() +
                            "/" + this.ctrAFKalenderItemTour.Kommission.AuftragPos.ToString() + "]";

            tbAuftragPos.Text = this.ctrAFKalenderItemTour.Kommission.AuftragID.ToString() + "/" +
                                this.ctrAFKalenderItemTour.Kommission.AuftragPos.ToString();

            //tbBeladestelle.Text = this.ctrAFKalenderItemTour.Kommission.Beladestelle;
            tbBeladestelle.Text = clsADR.GetADRString(this.ctrAFKalenderItemTour.Kommission.B_ID);
            //tbEntladestelle.Text = this.ctrAFKalenderItemTour.Kommission.Entladestelle;
            tbEntladestelle.Text = clsADR.GetADRString(this.ctrAFKalenderItemTour.Kommission.E_ID);

            nudKommiBelPos.Value = (decimal)this.ctrAFKalenderItemTour.Kommission.BeladePos;
            nudKommiEntlPos.Value = (decimal)this.ctrAFKalenderItemTour.Kommission.EntladePos;

            dtpKommiBeladeZeit.Value = this.ctrAFKalenderItemTour.Kommission.BeladeZeit;
            nud_KommiBZHour.Value = (decimal)this.ctrAFKalenderItemTour.Kommission.BeladeZeit.Hour;
            nud_KommiBZMin.Value = (decimal)this.ctrAFKalenderItemTour.Kommission.BeladeZeit.Minute;

            dtpKommiEntladeZeit.Value = this.ctrAFKalenderItemTour.Kommission.EntladeZeit;
            nud_KommiEZHour.Value = this.ctrAFKalenderItemTour.Kommission.EntladeZeit.Hour;
            nud_KommiEZMin.Value = this.ctrAFKalenderItemTour.Kommission.EntladeZeit.Minute;

            tbKmKommi.Text = this.ctrAFKalenderItemTour.Kommission.km.ToString();

            nudKommiBelPos.Value = (decimal)this.ctrAFKalenderItemTour.Kommission.BeladePos;
            nudKommiEntlPos.Value = (decimal)this.ctrAFKalenderItemTour.Kommission.EntladePos;
        }
        ///<summary>ctrTourDetails / LoadKommiDatenForGrd</summary>
        ///<remarks>Läd die Kommissionen ins Grid.</remarks>
        private void LoadKommiDatenForGrd()
        {
            DataTable dt = new DataTable();
            dt = this.ctrAFKalenderItemTour.Kommission.GetKommissionenForTourDetails();
            this.dgv.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                this.dgv.Columns["km"].Visible = false;
                this.dgv.AutoResizeColumns();
                this.dgv.AutoResizeRows();
                //setzten auf die aktuelle Kommission
                for (Int32 i = 0; i <= dgv.Rows.Count - 1; i++)
                {
                    if (bFirstLoad)
                    {
                        //beim ersten laden soll der erste Datensatz selected werden
                        this.dgv.Rows[0].Selected = true;
                        bFirstLoad = false;
                        ShowKommiOnFrm();
                        break;
                    }
                    else
                    {
                        if (this.SelectedKommiID == (decimal)this.dgv.Rows[i].Cells["ID"].Value)
                        {
                            this.dgv.Rows[i].Selected = true;
                        }
                    }
                }
            }
        }
        ///<summary>ctrTourDetails / dgv_SelectionChange</summary>
        ///<remarks>Läd die Kommissionen ins Grid.</remarks>
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {

            if (bFirstLoad)
            {
                SelectedKommiID = (decimal)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells["ID"].Value;
            }

        }
        ///<summary>ctrTourDetails / dgv_SelectionChange</summary>
        ///<remarks>Läd die Kommissionen ins Grid.</remarks>
        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            SelectedKommiID = (decimal)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells["ID"].Value;
            ShowKommiOnFrm();
        }
        ///<summary>ctrTourDetails / ShowKommiOnFrm</summary>
        ///<remarks></remarks>
        private void ShowKommiOnFrm()
        {
            if (SelectedKommiID > 0)
            {
                if (this.ctrAFKalenderItemTour.Tour.AnzahlKommissionen > 0)
                {
                    RemoveKalenderItemKommi();
                    this.ctrAFKalenderItemTour.Tour.TourCalculation();
                    this.ctrAFKalenderItemTour.Kommission.ID = SelectedKommiID;
                    this.ctrAFKalenderItemTour.Kommission.FillByID();
                    SetKommiValueToFrm();

                    this.TourPanel.Refresh();
                    this.TimePanel.Refresh();
                }
            }
        }
        ///<summary>ctrTourDetails / RefreshCurrentKalenderRow</summary>
        ///<remarks></remarks>
        private void RefreshCurrentKalenderRow(decimal decVehicleID)
        {
            AFKalenderRow RefreshRow = getFahrzeugCtrFromID(decVehicleID);
            if (RefreshRow != null)
            {
                RefreshRow.Refresh();
            }
        }
        ///<summary>ctrTourDetails / RemoveKalenderItemKommi</summary>
        ///<remarks>Löscht das KommiCtr vom Tour Panel.</remarks>
        private void RemoveKalenderItemKommi()
        {
            foreach (AFKalenderItemKommi ctr in this.TourPanel.Controls.Find("KalenderItemKommi", true))
            {
                this.TourPanel.Controls.Remove(ctr);
            }
        }
        ///<summary>ctrTourDetails / SetKommiCtrForTourPanel</summary>
        ///<remarks>Erstell das KommissionsCtr.</remarks>
        private void SetKommiCtrForTourPanel()
        {
            bool bDrawKommi = true;
            foreach (AFKalenderItemKommi ctr in this.TourPanel.Controls.Find("KalenderItemKommi", true))
            {
                if (ctr.Kommission.ID == this.SelectedKommiID)
                {
                    bDrawKommi = false;
                }
                else
                {
                    this.TourPanel.Controls.Remove(ctr);
                }
            }

            if (bDrawKommi)
            {
                AFKalenderItemKommi ctrKommi = new AFKalenderItemKommi();
                ctrKommi.GL_User = this.GL_User;
                ctrKommi.Kalender = this.Kalender;
                ctrKommi.Kommission.ID = this.SelectedKommiID;
                ctrKommi.Kommission.FillByID();
                ctrKommi.LocationPositionChanged += new AFKalenderItem.LocationPositionChangedEventHandler(this.KommiLocationPositionChanged);
                this.TourPanel.Controls.Add(ctrKommi);
                this.TourPanel.Controls.SetChildIndex(ctrKommi, 0);
                SetKommiPosition(ref ctrKommi, true);
                ctrKommi.Show();
            }
        }
        ///<summary>ctrTourDetails / SetKommiPosition</summary>
        ///<remarks></remarks>
        private void SetKommiPosition(ref AFKalenderItemKommi KommiCtr, bool ForInit)
        {
            try
            {
                AFKalenderRow fahrzeug;
                bool okay = false;
                //CHeck ob die Beladezeiten im Tourzeitraum liegen
                //Beladezeit
                if (KommiCtr.Kommission.BeladeZeitShownCtr < this.ctrAFKalenderItemTour.Tour.StartZeit)
                {
                    KommiCtr.Kommission.BeladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.StartZeit;
                }
                if (KommiCtr.Kommission.BeladeZeitShownCtr > this.ctrAFKalenderItemTour.Tour.EndZeit)
                {
                    KommiCtr.Kommission.BeladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.EndZeit.AddHours(-1);
                    KommiCtr.Kommission.EntladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.EndZeit;
                }
                //Entladezeiten
                if (KommiCtr.Kommission.EntladeZeitShownCtr > this.ctrAFKalenderItemTour.Tour.EndZeit)
                {
                    KommiCtr.Kommission.EntladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.EndZeit;
                }
                if (KommiCtr.Kommission.EntladeZeitShownCtr < this.ctrAFKalenderItemTour.Tour.StartZeit)
                {
                    KommiCtr.Kommission.BeladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.StartZeit;
                    KommiCtr.Kommission.EntladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.StartZeit.AddHours(1);
                }

                KommiCtr.Left = PointTime.GetPointFromTime(KommiCtr.Kommission.BeladeZeitShownCtr);
                KommiCtr.Width = PointTime.GetPointFromTime(KommiCtr.Kommission.EntladeZeitShownCtr) - KommiCtr.Left;

                //Fahrzeug Row
                fahrzeug = getFahrzeugCtrFromID(this.ctrAFKalenderItemTour.Tour.KFZ_ZM);

                if (fahrzeug != null)
                {
                    KommiCtr.Top = fahrzeug.Top + 5;
                }

                while (okay == false)
                {
                    foreach (AFKalenderItemKommi ctr in this.TourPanel.Controls.Find("KalenderItemKommi", true))
                    {
                        if (!object.ReferenceEquals(ctr, KommiCtr))
                        {
                            while (ctr.Bounds.IntersectsWith(KommiCtr.Bounds) == true)
                            {
                            }
                            KommiCtr.Show();
                        }
                    }
                    okay = true;
                    foreach (AFKalenderItemKommi ctr in this.TourPanel.Controls.Find("KalenderItemKommi", true))
                    {
                        if (!object.ReferenceEquals(ctr, KommiCtr))
                        {
                            if (ctr.Bounds.IntersectsWith(KommiCtr.Bounds))
                            {
                                okay = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        ///<summary>ctrTourDetails / KommiLocationPositionChanged</summary>
        ///<remarks></remarks>
        public void KommiLocationPositionChanged(Object sender, Point pointOnAFKommi)
        {
            try
            {
                AFKalenderItemKommi KommiCtr = (AFKalenderItemKommi)sender;
                Point kPoint = default(Point);
                kPoint.X = KommiCtr.Location.X + pointOnAFKommi.X;
                kPoint.Y = KommiCtr.Location.Y + pointOnAFKommi.Y;

                // Fahrzeuge
                clsFahrzeuge fahrz = new clsFahrzeuge();
                fahrz.BenutzerID = GL_User.User_ID;
                fahrz.ID = this.ctrAFKalenderItemTour.Tour.KFZ_ZM;

                KommiCtr.Kommission.BeladeZeitShownCtr = PointTime.GetTimeFromPoint(KommiCtr.Location.X);
                KommiCtr.Kommission.EntladeZeitShownCtr = PointTime.GetTimeFromPoint(KommiCtr.Location.X + KommiCtr.Width);

                //CHeck ob die Beladezeiten im Tourzeitraum liegen
                //Beladezeit
                if (KommiCtr.Kommission.BeladeZeitShownCtr < this.ctrAFKalenderItemTour.Tour.StartZeit)
                {
                    KommiCtr.Kommission.BeladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.StartZeit;
                }
                if (KommiCtr.Kommission.BeladeZeitShownCtr > this.ctrAFKalenderItemTour.Tour.EndZeit)
                {
                    KommiCtr.Kommission.BeladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.EndZeit.AddHours(-1);
                    KommiCtr.Kommission.EntladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.EndZeit;
                }
                //Entladezeiten
                if (KommiCtr.Kommission.EntladeZeitShownCtr > this.ctrAFKalenderItemTour.Tour.EndZeit)
                {
                    KommiCtr.Kommission.EntladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.EndZeit;
                }
                if (KommiCtr.Kommission.EntladeZeitShownCtr < this.ctrAFKalenderItemTour.Tour.StartZeit)
                {
                    KommiCtr.Kommission.BeladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.StartZeit;
                    KommiCtr.Kommission.EntladeZeitShownCtr = this.ctrAFKalenderItemTour.Tour.StartZeit.AddHours(1);
                }
                SetKommiPosition(ref KommiCtr, true);
                KommiCtr.Kommission.BeladeZeit = KommiCtr.Kommission.BeladeZeitShownCtr;
                KommiCtr.Kommission.EntladeZeit = KommiCtr.Kommission.EntladeZeitShownCtr;
                KommiCtr.Kommission.UpdateKommission();

                //Daten neu setzen auf Form
                ShowKommiOnFrm();
                LoadKommiDatenForGrd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        ///<summary>ctrTourDetails / getFahrzeugCtrFromID</summary>
        ///<remarks>Ermittel das KalenderRowCtr</remarks>
        private AFKalenderRow getFahrzeugCtrFromID(decimal ID)
        {
            AFKalenderRow Fahrzeug;
            foreach (AFKalenderRow ctr in this.TourPanel.Controls.Find("KalenderRow", true))
            {
                try
                {
                    Fahrzeug = (AFKalenderRow)ctr;
                    if (Fahrzeug.Fahrzeug.ID == ID)
                    {
                        return Fahrzeug;
                    }
                }
                catch // Invalid Cast
                {
                }
            }
            return null;
        }
        ///<summary>ctrTourDetails / CheckKommiBeladeZeit</summary>
        ///<remarks>Emittelt das Startdatum</remarks>
        private void CheckKommiBeladeZeit()
        {
            string strTmp = GetBeladeDateTimeString();
            DateTime dtTmp;

            if (DateTime.TryParse(strTmp, out dtTmp))
            {
                if (dtTmp >= this.dtpKommiEntladeZeit.Value)
                {
                    dtTmp = this.dtpKommiBeladeZeit.Value;
                }
                if (dtTmp < this.ctrAFKalenderItemTour.Tour.StartZeit)
                {
                    dtTmp = this.ctrAFKalenderItemTour.Tour.StartZeit;
                }
                if (dtTmp > this.ctrAFKalenderItemTour.Tour.EndZeit)
                {
                    dtTmp = this.ctrAFKalenderItemTour.Tour.EndZeit.AddHours(-1);
                }
                dtpKommiBeladeZeit.Value = dtTmp;
            }
            else
            {
                dtpKommiBeladeZeit.Value = this.ctrAFKalenderItemTour.Kommission.BeladeZeit;
            }
            this.nud_KommiBZHour.Value = (decimal)dtpKommiBeladeZeit.Value.Hour;
            this.nud_KommiBZMin.Value = (decimal)this.dtpKommiBeladeZeit.Value.Minute;
        }
        ///<summary>ctrTourDetails / GetBeladeDateTimeString</summary>
        ///<remarks>Setz das Beladedatum als String zusammen</remarks>
        private string GetBeladeDateTimeString()
        {
            string strTmp = dtpKommiBeladeZeit.Value.ToShortDateString()
                            + " " + nud_KommiBZHour.Value.ToString()
                            + ":" + nud_KommiBZMin.Value.ToString()
                            + ":00";
            return strTmp;
        }
        ///<summary>ctrTourDetails / CheckKommiEntladeZeit</summary>
        ///<remarks>Ermittelt das Enddatum</remarks>
        private void CheckKommiEntladeZeit()
        {
            string strTmp = GetEntladeDateTimeString();
            DateTime dtTmp;

            if (DateTime.TryParse(strTmp, out dtTmp))
            {
                if (dtTmp <= this.dtpKommiBeladeZeit.Value)
                {
                    dtTmp = this.dtpKommiEntladeZeit.Value;
                }

                if (dtTmp > this.ctrAFKalenderItemTour.Tour.EndZeit)
                {
                    dtTmp = this.ctrAFKalenderItemTour.Tour.EndZeit;
                }
                if (dtTmp < this.ctrAFKalenderItemTour.Tour.StartZeit)
                {
                    dtTmp = this.ctrAFKalenderItemTour.Tour.StartZeit.AddHours(1);
                }
                dtpKommiEntladeZeit.Value = dtTmp;
            }
            else
            {
                dtpKommiEntladeZeit.Value = this.ctrAFKalenderItemTour.Tour.EndZeit;
            }
            this.nud_KommiEZHour.Value = (decimal)dtpKommiEntladeZeit.Value.Hour;
            this.nud_KommiEZMin.Value = (decimal)dtpKommiEntladeZeit.Value.Minute;
        }
        ///<summary>ctrTourDetails / GetEntladeDateTimeString</summary>
        ///<remarks>Setzt das Enddatum als String zusammen</remarks>
        private string GetEntladeDateTimeString()
        {
            string strTmp = dtpKommiEntladeZeit.Value.ToShortDateString()
                            + " " + nud_KommiEZHour.Value.ToString()
                            + ":" + nud_KommiEZMin.Value.ToString()
                            + ":00";
            return strTmp;
        }
        ///<summary>ctrTourDetails / nud_KommiBZMin_ValueChanged</summary>
        ///<remarks></remarks>
        private void nud_KommiBZMin_ValueChanged(object sender, EventArgs e)
        {
            if (nud_KommiBZMin.Value == 60)
            {
                nud_KommiBZMin.Value = 0;
            }

        }
        ///<summary>ctrTourDetails / nud_KommiBZMin_ValueChanged</summary>
        ///<remarks></remarks>
        private void nud_KommiEZMin_ValueChanged(object sender, EventArgs e)
        {
            if (nud_KommiEZMin.Value == 60)
            {
                nud_KommiEZMin.Value = 0;
            }

        }
        ///<summary>ctrTourDetails / nud_KommiBZMin_ValueChanged</summary>
        ///<remarks></remarks>
        private void nud_KommiBZHour_ValueChanged(object sender, EventArgs e)
        {
            CheckKommiBeladeZeit();

        }
        ///<summary>ctrTourDetails / nud_KommiBZMin_ValueChanged</summary>
        ///<remarks></remarks>
        private void nud_KommiEZHour_ValueChanged(object sender, EventArgs e)
        {
            CheckKommiEntladeZeit();

        }
        ///<summary>ctrTourDetails / nud_KommiBZMin_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpKommiBeladeZeit_ValueChanged(object sender, EventArgs e)
        {
            CheckKommiBeladeZeit();

        }
        ///<summary>ctrTourDetails / nud_KommiBZMin_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpKommiEntladeZeit_ValueChanged(object sender, EventArgs e)
        {
            CheckKommiEntladeZeit();

        }
        ///<summary>ctrTourDetails / toolStripButton1_Click</summary>
        ///<remarks>Speichern der Änderungen</remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CheckKommiBeladeZeit();
            CheckKommiEntladeZeit();
            this.ctrAFKalenderItemTour.Kommission.BeladeZeit = this.dtpKommiBeladeZeit.Value;
            this.ctrAFKalenderItemTour.Kommission.EntladeZeit = this.dtpKommiEntladeZeit.Value;
            //Vor dem Update der Kommissionsdaten muss ein getrenntes Update über die 
            //Be-/Entladepos gemacht werden -> Es sollen jeweils die geänderten Positionen 
            //mit Unter den betroffenen Kommissionen getauscht werden
            this.ctrAFKalenderItemTour.Kommission.ChangeBeAndEntladePosition((Int32)this.nudKommiBelPos.Value, true);
            this.ctrAFKalenderItemTour.Kommission.ChangeBeAndEntladePosition((Int32)this.nudKommiEntlPos.Value, false);
            this.ctrAFKalenderItemTour.Kommission.BeladePos = (Int32)this.nudKommiBelPos.Value;
            this.ctrAFKalenderItemTour.Kommission.EntladePos = (Int32)this.nudKommiEntlPos.Value;
            this.ctrAFKalenderItemTour.Kommission.UpdateKommission();
            ShowKommiOnFrm();
            LoadKommiDatenForGrd();
        }
        ///<summary>ctrTourDetails / tsbtnKommiDetails_Click</summary>
        ///<remarks></remarks>
        private void tsbtnKommiDetails_Click(object sender, EventArgs e)
        {
            this.ctrMenu = this.ctrMenu.OpenFrmAuftragView(this.Kalender, this.ctrAFKalenderItemTour.Kommission.Tour);
            //Nur ein Refresh reicht, da sich an den benötigten Daten nichts geändert hat
            RefreshCtrTourDetails();
        }
        ///<summary>ctrTourDetails / tsbtnKommiDel_Click</summary>
        ///<remarks>Löschen der gewählten Position aus der Tour</remarks>
        private void tsbtnKommiDel_Click(object sender, EventArgs e)
        {
            if (this.ctrAFKalenderItemTour.Tour.AnzahlKommissionen > 1)
            {
                this.ctrAFKalenderItemTour.Kommission.DeleteKommiFromTourByID();
                //update Kilometerberechnung nach Löschen einer Kommission
                this.ctrAFKalenderItemTour.Tour.TourCalculation();
                //Form muss auch neu geladen werden, da die Anzahl der Daten verändert worden sein könnten
                InitFrm();
            }
            else
            {
                clsMessages.Disposition_MinimaleKommissionsanzahlinTourErreicht();
            }
        }
        ///<summary>ctrTourDetails / ctrTourDetails_SizeChanged</summary>
        ///<remarks></remarks>
        private void ctrTourDetails_SizeChanged(object sender, EventArgs e)
        {

        }
        ///<summary>ctrTourDetails / RefreshCtrTourDetails</summary>
        ///<remarks></remarks>
        public void RefreshCtrTourDetails()
        {
            //bTourIsLoaded = false;
            bFirstLoad = true;
            RemoveKalenderItemKommi();
            this.TimePanel.Refresh();
        }
        ///<summary>ctrTourDetails / toolStripButton1_Click_1</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            foreach (AFKalenderItemKommi ctr in this.TourPanel.Controls.Find("KalenderItemKommi", true))
            {
                if (ctr.Kommission.ID == this.SelectedKommiID)
                {
                    ctr.OpenPrintCenter(ctr);
                }
            }
        }
        ///<summary>ctrTourDetails / toolStripButton2_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenScanFrm(this.ctrAFKalenderItemTour.Kommission.Tour.Auftrag.ID, this.ctrAFKalenderItemTour.Kommission.AuftragPosTableID, 0, 0, this);
        }
        ///<summary>ctrTourDetails / toolStripButton1_Click_2</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            RefreshCtrTourDetails();
        }



    }
}
