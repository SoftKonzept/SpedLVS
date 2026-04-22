using LVS;
using Sped4.Classes;
using Sped4.Controls;
using Sped4.Struct;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmDispoKalender : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public delegate void ThreadCtrInvokeEventHandler();
        public Int32 defRowHight = 71;
        public DataTable dtKalenderRow = new DataTable("KaldenderRow");
        public ctrMenu menue;
        internal AFKalenderItemTour _ctrKalenderItemTour;
        //Test
        internal Sped4.Controls.Kalender.ctrCalRow _ctrCalRow;
        internal Sped4.Controls.ctrTourItem _ctrTourItem;

        internal decimal[] arZM;

        //public delegate void DispoDataChangedEventHandler(decimal Menge, Int32 RowIndex);
        public delegate void DispoDataChangedEventHandler(structAuftPosRow _IDAndRowID);
        //public event DispoDataChangedEventHandler DispoDataChanged;

        public ctrAufträge AuftragCtr;

        private clsPointTime PointTime = new clsPointTime();
        private bool KommisLoaded = false;

        private bool TrailerLoaded = false;
        private bool FahrerLoaded = false;
        public bool AllowedLoad = true;

        private bool xMove = false;
        private Point oldPos = new Point();

        public bool FahrerAnzeigen = false;
        public bool AufliegerAnzeigen = false;

        public Point pRecource = new Point();
        public Point pKommi = new Point();


        public delegate void ctrAuftragRefreshEventHandler();
        public event ctrAuftragRefreshEventHandler ctrAuftragRefresh;

        /******************************************************************************************************
         *                               Methoden / Procedures
         * ***************************************************************************************************/
        ///<summary>frmDispoKalender/ frmDispoKalender</summary>
        ///<remarks></remarks>
        public frmDispoKalender(ctrAufträge _AuftragCtr)
        {
            InitializeComponent();
            this.ResizeRedraw = true;
            AuftragCtr = _AuftragCtr;
            GL_User = AuftragCtr._ctrMenu.GL_User;

            DispoKalender.SelectionRange.Start = GL_User.us_dtDispoVon.Date;
            DispoKalender.SelectionRange.End = GL_User.us_dtDispoBis.Date;
            InitTabelKalenderRow();
        }
        ///<summary>frmDispoKalender/ frmDispoKalender_Load</summary>
        ///<remarks></remarks>
        private void frmDispoKalender_Load(object sender, EventArgs e)
        {
            // Fahrzeug ZM - jede Zugmaschine stellt eine KalenderRow dar
            getFahrzeuge_ZM();
        }
        ///<summary>frmDispoKalender/ cbFahrer_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbFahrer_CheckedChanged(object sender, EventArgs e)
        {
            AllowedLoad = true;
            //KalenderItemKommi ausblenden
            //HideAllKalenderItemKommi();
            HideAllKalenderItemTour();
            if (cbFahrer.Checked == true)
            {
                if (!FahrerLoaded)
                {
                    LoadFahrerRecource();
                }
            }
            else
            {
                FahrerLoaded = false;
                RemoveKalenderItemRecourceFahrer();
            }
            AllowedLoad = false;
            // ShowAllKalenderItemKommi();
            ShowAllKalenderItemTour();
        }
        ///<summary>frmDispoKalender/ cbAuflieger_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbAuflieger_CheckedChanged(object sender, EventArgs e)
        {
            AllowedLoad = true;
            //KalenderItemKommi ausblenden
            //HideAllKalenderItemKommi();
            HideAllKalenderItemTour();
            if (cbAuflieger.Checked == true)
            {
                if (!TrailerLoaded)
                {
                    LoadAufliegerRecource();
                }
            }
            else
            {
                TrailerLoaded = false;
                RemoveKalenderItemRecourceAuflieger();
            }
            AllowedLoad = false;
            //ShowAllKalenderItemKommi();
            ShowAllKalenderItemTour();
        }
        ///<summary>frmDispoKalender/ LoadAufliegerRecource</summary>
        ///<remarks></remarks>
        private void LoadAufliegerRecource()
        {
            LoadUsedRecource("A", TrailerLoaded);
            TrailerLoaded = true;
        }
        ///<summary>frmDispoKalender/ LoadFahrerRecource</summary>
        ///<remarks></remarks>
        private void LoadFahrerRecource()
        {
            LoadUsedRecource("F", FahrerLoaded);
            FahrerLoaded = true;
        }
        ///<summary>frmDispoKalender/ SetRecourceCheckboxen</summary>
        ///<remarks></remarks>
        private void SetRecourceCheckboxen()
        {
            if (TrailerLoaded)
            {
                cbAuflieger.Checked = true;
            }
            else
            {
                cbAuflieger.Checked = false;
            }
            if (FahrerLoaded)
            {
                cbFahrer.Checked = true;
            }
            else
            {
                cbFahrer.Checked = false;
            }
        }
        ///<summary>frmDispoKalender/ ctrAuftragRefresh1</summary>
        ///<remarks></remarks>
        public void ctrAuftragRefresh1()
        {
            AuftragCtr.InitDGV();
        }
        ///<summary>frmDispoKalender/ TimePanel_Paint</summary>
        ///<remarks>Timepanel wird gezeichniet</remarks>
        private void TimePanel_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                DateTime DateFrom = GL_User.us_dtDispoVon;
                DateTime DateTo = GL_User.us_dtDispoBis;
                //Ein Tag hinzufügen damit dieser Endtag noch im VehiclePanel
                //angezeigt werden kann
                DateTo = DateTo.AddDays(1);

                const Int32 BeginTimeFromLeft = 104;
                TimeSpan TSTage = DateTo.Subtract(DateFrom);
                Int32 Tage = (Int32)TSTage.TotalDays;
                Int32 H = 24 * Tage;
                Int32 Stunde = 0;
                Int32 x = 0;
                Pen p = new Pen(Color.White, 1);
                Pen p0 = new Pen(Color.DarkBlue, 1);
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
                pointb.Y = this.TimePanel.Height;
                pointb.X = 0;
                pointtempa.Y = 30;
                pointtempa.X = 0;
                pointtempb.Y = this.TimePanel.Height;
                pointtempb.X = 0;

                Rectangle TempRect = new Rectangle(0, 0, TimePanel.Width, 5);
                Brush TempBrush = new LinearGradientBrush(TempRect, this.BackColor, TimePanel.BackColor, LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(TempBrush, TempRect);
                e.Graphics.DrawLine(new Pen(Color.FromArgb(255, 128, 0), 1), 0, 0, this.Width, 0);

                PointTime.ClearPointTime();
                Stunde = 0;

                DateTime dtWE = DateFrom;

                //DateTime dtWE = GL_User.us_dtDispoVon;
                //Stundenleiste wird erzeugt
                for (Int32 i = 0; i <= H; i++)
                {

                    if (Stunde == 24)
                    {
                        Stunde = 0;
                        dtWE = dtWE.AddDays(1);
                    }
                    Time = DateFrom;
                    //Time = GL_User.us_dtDispoVon;

                    x = BeginTimeFromLeft + (i * (Int32)Math.Floor((decimal)TimePanel.Width - (decimal)BeginTimeFromLeft - 20) / H);

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

                //TODO: schaun was schneller ist
                //foreach (Control ctr in this.VehicelPanel.Controls)
                foreach (AFKalenderRow ctr in this.VehicelPanel.Controls.Find("KalenderRow", true))
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
                //Kommisssionen werden geladen
                if (!KommisLoaded)
                {
                    for (Int32 i = 0; i <= arZM.Length - 1; i++)
                    {
                        decimal dec = arZM[i];
                        GetTourByZM(arZM[i]);
                        GetCtrLeerKMByZM(arZM[i]);
                    }
                    //getKommis(); //Daten aus Kommission werden geladen
                    KommisLoaded = true;
                }
                //Rssourcen werden geladen
                if (cbAuflieger.Checked == true)
                {
                    LoadAufliegerRecource();
                }
                if (cbFahrer.Checked == true)
                {
                    LoadFahrerRecource();
                }
                SetRecourceCheckboxen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /***********************************************************************************************************
         *                                     Kalender Row - Zugmaschine Dispoplan
         ***********************************************************************************************************/
        ///<summary>frmDispoKalender/ getFahrzeuge_ZM</summary>
        ///<remarks>Zugmaschinen / LKE werden geladen</remarks>
        private void getFahrzeuge_ZM()
        {
            DataTable FahrzTable = new DataTable();
            FahrzTable = clsFahrzeuge.GetFahrzeuge_ZM(GL_User.User_ID);

            arZM = new decimal[FahrzTable.Rows.Count];

            for (Int32 i = 0; i <= FahrzTable.Rows.Count - 1; i++)
            {
                AFKalenderRow ctrDispo = new AFKalenderRow();
                clsFahrzeuge Fahrz = new clsFahrzeuge();
                Fahrz.BenutzerID = GL_User.User_ID;

                //---------------- Zugmaschine / Kalenderrow  ------------------------------
                Fahrz.ID = (decimal)FahrzTable.Rows[i]["ID"];
                arZM[i] = Fahrz.ID;
                Fahrz.Fill();
                ctrDispo.Dock = System.Windows.Forms.DockStyle.Top;
                ctrDispo.Fahrzeug.ID = Fahrz.ID;
                ctrDispo.Fahrzeug.KFZ = Fahrz.KFZ;
                ctrDispo.Fahrzeug.KIntern = Fahrz.KIntern;
                ctrDispo.Fahrzeug.AbgasNorm = Fahrz.AbgasNorm;

                this.VehicelPanel.Controls.Add(ctrDispo);
                this.VehicelPanel.Controls.SetChildIndex(ctrDispo, 0);
                ctrDispo.Show();
            }
        }
        /*************************************************************************************************************
         *                                          RECOURCEN Aufliger / Fahrer
         * ***********************************************************************************************************/
        ///<summary>frmDispoKalender/ LoadUsedRecource</summary>
        ///<remarks></remarks>
        private void LoadUsedRecource(string recType, bool RecourceLoaded)
        {
            clsResource clsRec = new clsResource();
            clsRec._GL_User = this.GL_User;
            clsRec.m_dt_TimeFrom = GL_User.us_dtDispoVon;
            clsRec.m_dt_TimeTo = GL_User.us_dtDispoBis;

            clsRec.m_ch_RecourceTyp = Convert.ToChar(recType);
            DataTable RecourceTable = new DataTable();
            RecourceTable.Clear();
            RecourceTable = clsRec.LoadRecouce(recType, RecourceLoaded);
            for (Int32 i = 0; i <= RecourceTable.Rows.Count - 1; i++)
            {
                //TestDISP
                //// --- Auflieger
                //if (RecourceTable.Rows[i]["RecourceTyp"].ToString() == "A")
                //{
                //  AFKalenderItemRecource ctrRecource = new AFKalenderItemRecource(this);
                //  ctrRecource.Recource.RecourceID = (decimal)RecourceTable.Rows[i]["RecourceID"];
                //  ctrRecource.Recource.RecourceTyp = RecourceTable.Rows[i]["RecourceTyp"].ToString();
                //  ctrRecource.Recource.TimeFrom = (DateTime)RecourceTable.Rows[i]["DateFrom"];
                //  ctrRecource.Recource.TimeTo = (DateTime)RecourceTable.Rows[i]["DateTo"];
                //  ctrRecource.Recource.VehicleID = (decimal)RecourceTable.Rows[i]["VehicleID"];
                //  ctrRecource.Recource.nRecStartTime = clsResource.GetNextResscourceStartDateTime(this.GL_User, ctrRecource.Recource.TimeTo, ctrRecource.Recource.RecourceID, false);
                //  ctrRecource.Recource.fRecEndTime = clsResource.GetFormerResscourceEndtDateTime(this.GL_User, ctrRecource.Recource.TimeFrom, ctrRecource.Recource.RecourceID, false);

                //  ctrRecource.Name = "KalenderItemRecource_Auflieger";
                //  ctrRecource.AllowDrop = true;
                //  ctrRecource.LocationPositionChanged += new AFKalenderItemRecource.LocationPositionChangedEventHandler(this.RecourceLocationPositionChanged);
                //  ctrRecource.Recource.KFZ = clsFahrzeuge.GetKFZByID(ctrRecource.Recource.VehicleID);
                //  this.VehicelPanel.Controls.Add(ctrRecource);
                //  this.VehicelPanel.Controls.SetChildIndex(ctrRecource, 0);

                //  SetRecourcePosition(ref ctrRecource, true);
                //  //ctrRecource.Hide();
                //  ctrRecource.Show();
                //}
                ////--- Fahrer
                //if (RecourceTable.Rows[i]["RecourceTyp"].ToString() == "F")
                //{
                //  AFKalenderItemRecource ctrRecource = new AFKalenderItemRecource(this);
                //  ctrRecource.Recource.RecourceID = (decimal)RecourceTable.Rows[i]["RecourceID"];
                //  ctrRecource.Recource.RecourceTyp = RecourceTable.Rows[i]["RecourceTyp"].ToString();
                //  ctrRecource.Recource.TimeFrom = (DateTime)RecourceTable.Rows[i]["DateFrom"];
                //  ctrRecource.Recource.TimeTo = (DateTime)RecourceTable.Rows[i]["DateTo"];
                //  //ctrRecource.Recource.VehicleID = (Int32)RecourceTable.Rows[i]["VehicleID"];
                //  ctrRecource.Recource.PersonalID = (decimal)RecourceTable.Rows[i]["PersonalID"];
                //  ctrRecource.Recource.nRecStartTime = clsResource.GetNextResscourceStartDateTime(this.GL_User, ctrRecource.Recource.TimeTo, ctrRecource.Recource.RecourceID, false);
                //  ctrRecource.Recource.fRecEndTime = clsResource.GetFormerResscourceEndtDateTime(this.GL_User, ctrRecource.Recource.TimeFrom, ctrRecource.Recource.RecourceID, false);

                //  ctrRecource.Recource.Name = clsPersonal.GetNameByID((decimal)RecourceTable.Rows[i]["PersonalID"]);
                //  ctrRecource.Name = "KalenderItemRecource_Fahrer";
                //  ctrRecource.AllowDrop = true;
                //  ctrRecource.LocationPositionChanged += new AFKalenderItemRecource.LocationPositionChangedEventHandler(this.RecourceLocationPositionChanged);
                //  this.VehicelPanel.Controls.Add(ctrRecource);
                //  this.VehicelPanel.Controls.SetChildIndex(ctrRecource, 0);

                //  SetRecourcePosition(ref ctrRecource, true);
                //  ctrRecource.Show();
                //}
            }
        }
        ///<summary>frmDispoKalender/ SetRecourcePosition</summary>
        ///<remarks>Setzen der Rescourcen auf den Vehiclepanel</remarks>
        private void SetRecourcePosition(ref AFKalenderItemRecource RecourceCtr, bool ForInit)
        {
            if (AllowedLoad)
            {
                try
                {
                    bool okay = false;
                    //DateTime tmpTime = default(DateTime);
                    AFKalenderRow fahrzeug;
                    Point rPoint = new Point();
                    rPoint.X = PointTime.GetPointFromTime(RecourceCtr.Recource.TimeFrom);
                    rPoint.Y = Cursor.Position.Y;

                    RecourceCtr.Left = PointTime.GetPointFromTime(RecourceCtr.Recource.TimeFrom);
                    RecourceCtr.Width = PointTime.GetPointFromTime(RecourceCtr.Recource.TimeTo) - RecourceCtr.Left;

                    if (RecourceCtr.Recource.TimeFrom >= RecourceCtr.Recource.TimeTo)
                    {
                        DateTime dt = RecourceCtr.Recource.TimeFrom;
                        dt = dt.AddHours(1);
                        RecourceCtr.Width = PointTime.GetPointFromTime(dt) - RecourceCtr.Left;
                    }
                    if (RecourceCtr.Recource.RecourceID == 0)
                    {
                        if (RecourceCtr.Recource.RecourceTyp.ToString() == "A")
                        {
                            //if (clsResource.GetRecourceIDbyVehicle(DateFrom, DateTo, RecourceCtr.Recource.VehicleID) > 0)
                            if (clsResource.GetRecourceIDbyVehicle(GL_User.us_dtDispoVon, GL_User.us_dtDispoBis, RecourceCtr.Recource.VehicleID) > 0)
                            {
                                //decimal RecourceID = clsResource.GetRecourceIDbyVehicle(DateFrom, DateTo, RecourceCtr.Recource.VehicleID);
                                decimal RecourceID = clsResource.GetRecourceIDbyVehicle(GL_User.us_dtDispoVon, GL_User.us_dtDispoBis, RecourceCtr.Recource.VehicleID);
                            }
                        }
                        if (RecourceCtr.Recource.RecourceTyp.ToString() == "F")
                        {
                            //if (clsResource.GetRecourceIDbyPersonal(DateFrom, DateTo, RecourceCtr.Recource.PersonalID) > 0)
                            if (clsResource.GetRecourceIDbyPersonal(GL_User.us_dtDispoVon, GL_User.us_dtDispoBis, RecourceCtr.Recource.PersonalID) > 0)
                            {
                                //decimal RecourceID = clsResource.GetRecourceIDbyPersonal(DateFrom, DateTo, RecourceCtr.Recource.PersonalID);
                                decimal RecourceID = clsResource.GetRecourceIDbyPersonal(GL_User.us_dtDispoVon, GL_User.us_dtDispoBis, RecourceCtr.Recource.PersonalID);
                            }
                        }
                    }

                    if (ForInit)
                    {
                        decimal ZM_ID = clsResource.GetVehicleIDFromRecource(RecourceCtr.Recource.RecourceID, "Z");
                        fahrzeug = getFahrzeugCtrFromID(ZM_ID);
                    }
                    else
                    {
                        fahrzeug = getFahrzeugCtrFromPoint(this.VehicelPanel.PointToClient(rPoint));   // findet den Punkt nicht           
                    }

                    //Position wird gesetzt
                    if (fahrzeug != null)
                    {
                        if (RecourceCtr.Recource.RecourceTyp.ToString() == "A")
                        {
                            RecourceCtr.Top = fahrzeug.Bottom - RecourceCtr.Height - 1;
                        }
                        if (RecourceCtr.Recource.RecourceTyp.ToString() == "F")
                        {
                            RecourceCtr.Top = fahrzeug.Bottom - (2 * RecourceCtr.Height) - 2;
                        }
                        while (okay == false)
                        {
                            foreach (AFKalenderItemRecource ctr in this.VehicelPanel.Controls.Find("KalenderItemRecource_Auflieger", true))
                            {
                                if (!object.ReferenceEquals(ctr, RecourceCtr))
                                {
                                    while (ctr.Bounds.IntersectsWith(RecourceCtr.Bounds) == true)
                                    {
                                        //RecourceCtr.Left = PointTime.GetPointFromTime(RecourceCtr.Recource.TimeFrom);
                                        RecourceCtr.Left = ctr.Left + ctr.Width;
                                        RecourceCtr.Top = fahrzeug.Bottom - RecourceCtr.Height - 1;
                                        clsResource Recource = new clsResource();

                                        //RecourceCtr.Recource.TimeFrom = PointTime.GetTimeFromPoint(RecourceCtr.Left);
                                        Recource.m_dt_TimeFrom = PointTime.GetTimeFromPoint(RecourceCtr.Left);
                                        Recource.m_dt_TimeTo = RecourceCtr.Recource.TimeTo;
                                        Recource.m_i_VehicleID_Trailer = RecourceCtr.Recource.VehicleID;
                                        Recource.m_i_RecourceID = RecourceCtr.Recource.RecourceID;
                                        Recource.UpdateTrailerStart();
                                        Recource.m_i_VehicleID_Truck = fahrzeug.Fahrzeug.ID;
                                        Recource.UpdateTruck();
                                    }
                                    //RecourceCtr.Show();

                                }
                            }
                            foreach (AFKalenderItemRecource ctr in this.VehicelPanel.Controls.Find("KalenderItemRecource_Fahrer", true))
                            {
                                if (!object.ReferenceEquals(ctr, RecourceCtr))
                                {
                                    while (ctr.Bounds.IntersectsWith(RecourceCtr.Bounds) == true)
                                    {
                                        RecourceCtr.Left = ctr.Left + ctr.Width;
                                        RecourceCtr.Top = fahrzeug.Bottom - (2 * RecourceCtr.Height) - 2;
                                        clsResource Recource = new clsResource();

                                        RecourceCtr.Recource.TimeFrom = PointTime.GetTimeFromPoint(RecourceCtr.Left);
                                        Recource.m_dt_TimeTo = RecourceCtr.Recource.TimeTo;
                                        Recource.m_dt_TimeFrom = PointTime.GetTimeFromPoint(RecourceCtr.Left);
                                        Recource.m_i_PersonalID = RecourceCtr.Recource.PersonalID;
                                        Recource.m_i_RecourceID = RecourceCtr.Recource.RecourceID;
                                        Recource.m_i_VehicleID_Truck = fahrzeug.Fahrzeug.ID;
                                        Recource.UpdateFahrerStart();
                                        Recource.UpdateTruck();

                                    }
                                    RecourceCtr.Show();
                                }
                            }
                            okay = true;
                            foreach (AFKalenderItemRecource ctr in this.VehicelPanel.Controls.Find("KalenderItemRecource_Auflieger", true))
                            {
                                if (!object.ReferenceEquals(ctr, RecourceCtr))
                                {
                                    if (ctr.Bounds.IntersectsWith(RecourceCtr.Bounds))
                                    {
                                        okay = false;
                                    }
                                }
                            }
                            foreach (AFKalenderItemRecource ctr in this.VehicelPanel.Controls.Find("KalenderItemRecource_Fahrer", true))
                            {
                                if (!object.ReferenceEquals(ctr, RecourceCtr))
                                {
                                    if (ctr.Bounds.IntersectsWith(RecourceCtr.Bounds))
                                    {
                                        okay = false;
                                    }
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
        }
        ///<summary>frmDispoKalender/ RecourceLocationPositionChanged</summary>
        ///<remarks> Auftrag / Tour wird auf dem Timepanel verschoben / update von Zeiten und Fahrzeug  </remarks>
        public void RecourceLocationPositionChanged(Object sender, Point pointOnAFRec)
        {
            try
            {
                bool RecourceIsUsed = false;
                AFKalenderItemRecource RecCtr = (AFKalenderItemRecource)sender;
                clsResource RecourceCls = new clsResource();

                //alte Position festhalten
                RecCtr.Recource.oldPosition = RecCtr.Location;
                RecCtr.Recource.oldVehicleIDZM = clsResource.GetVehicleIDFromRecource(RecCtr.Recource.RecourceID, "Z");
                RecCtr.Recource.oldTimeFrom = RecCtr.Recource.TimeFrom;
                RecCtr.Recource.oldTimeTo = RecCtr.Recource.TimeTo;

                Point Rpoint = new Point();
                Rpoint.X = RecCtr.Location.X + pointOnAFRec.X;
                Rpoint.Y = RecCtr.Location.Y + pointOnAFRec.Y;

                //Unterscheidung Recource Auflieger / Fahrer
                clsFahrzeuge fahrz = new clsFahrzeuge();
                fahrz.BenutzerID = GL_User.User_ID;

                if (getFahrzeugCtrFromPoint(Rpoint) != null)
                {
                    fahrz.ID = getFahrzeugCtrFromPoint(Rpoint).Fahrzeug.ID;
                    AFKalenderRow ctrDispo = getFahrzeugCtrFromPoint(Rpoint);
                    if ((ctrDispo != null) && (ctrDispo.Fahrzeug.ID != null))
                    {

                        fahrz.ID = ctrDispo.Fahrzeug.ID;
                        fahrz.Fill();
                        //RecourceCls.m_i_VehicleID_Truck = fahrz.ID;
                        RecCtr.Recource.TimeFrom = PointTime.GetTimeFromPoint(RecCtr.Location.X);
                        RecCtr.Recource.TimeTo = PointTime.GetTimeFromPoint(RecCtr.Location.X + RecCtr.Width);
                        RecourceCls.m_i_VehicleID_Truck = fahrz.ID;

                        //Prüfung ob der zugwiesene Trailer / Fahrer noch frei ist
                        if (RecCtr.Recource.RecourceTyp == "A")
                        {
                            if (clsResource.IsTrailerUsed(RecCtr.Recource.TimeFrom, RecCtr.Recource.TimeTo, RecCtr.Recource.VehicleID, RecCtr.Recource.RecourceTyp, RecCtr.Recource.RecourceID))
                            {
                                RecourceIsUsed = true;
                            }
                        }
                        if (RecCtr.Recource.RecourceTyp == "F")
                        {
                            if (clsResource.IsFahrerUsed(RecCtr.Recource.TimeFrom, RecCtr.Recource.TimeTo, RecCtr.Recource.PersonalID, RecCtr.Recource.RecourceTyp, RecCtr.Recource.RecourceID))
                            {
                                RecourceIsUsed = true;
                            }
                        }
                        // Recource is used  
                        if (!RecourceIsUsed)
                        {
                            RecCtr.Recource.TimeFrom = PointTime.GetTimeFromPoint(RecCtr.Location.X);
                            RecCtr.Recource.TimeTo = PointTime.GetTimeFromPoint(RecCtr.Location.X + RecCtr.Width);
                            RecourceCls.m_i_VehicleID_Truck = fahrz.ID;
                        }
                        else
                        {
                            //RecCtr.Recource.TimeFrom = PointTime.GetTimeFromPoint(RecCtr.Recource.oldPosition.X);
                            RecCtr.Recource.TimeFrom = RecCtr.Recource.oldTimeFrom;
                            RecCtr.Recource.TimeTo = RecCtr.Recource.oldTimeTo;
                            //RecCtr.Recource.TimeTo = PointTime.GetTimeFromPoint(RecCtr.Recource.oldPosition.X + RecCtr.Width);
                            RecourceCls.m_i_VehicleID_Truck = RecCtr.Recource.oldVehicleIDZM;
                            if (RecCtr.Recource.oldVehicleIDZM != fahrz.ID)
                            {
                                KalenderRefresh();
                            }
                        }

                        //Update VehicleRecource Auflieger/Fahrer
                        RecourceCls.m_i_RecourceID = RecCtr.Recource.RecourceID;
                        RecourceCls.m_ch_RecourceTyp = Convert.ToChar(RecCtr.Recource.RecourceTyp);
                        RecourceCls.m_dt_TimeFrom = RecCtr.Recource.TimeFrom;
                        RecourceCls.m_dt_TimeTo = RecCtr.Recource.TimeTo;
                        RecourceCls.m_i_VehicleID_Trailer = RecCtr.Recource.VehicleID;
                        RecourceCls.m_i_PersonalID = RecCtr.Recource.PersonalID;

                        //Update VehicleRecource 
                        if (RecourceCls.m_ch_RecourceTyp.ToString() == "A")
                        {
                            RecourceCls.UpdateTrailer();
                            RecourceCls.UpdateTrailerStart();
                            RecourceCls.UpdateTrailerEnd();
                        }
                        //Update FahrerRecource
                        if (RecourceCls.m_ch_RecourceTyp.ToString() == "F")
                        {
                            RecourceCls.UpdateFahrer();
                            RecourceCls.UpdateFahrerStart();
                            RecourceCls.UpdateFahrerEnd();
                        }
                        RecourceCls.UpdateTruck();
                        SetRecourcePosition(ref RecCtr, false);

                        RessourcenRefresh(RecCtr.Recource.RecourceTyp);
                    }
                }
                else
                {
                    KalenderRefresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /*********************************************************************************************************************
         *                                           Kommission  Aufträge 
         *********************************************************************************************************************/
        ///<summary>frmDispoKalender/ GetTourByZM</summary>
        ///<remarks>  </remarks>
        public void GetTourByZM(decimal decZM_ID)
        {
            DataTable dt = new DataTable();
            dt.Clear();

            clsTour myTour = new clsTour();
            myTour.StartZeit = GL_User.us_dtDispoVon;
            myTour.EndZeit = GL_User.us_dtDispoBis;
            myTour.KFZ_ZM = decZM_ID;
            dt = myTour.GetTourByZM();
            if (dt.Rows.Count > 0)
            {
                //TEst Dispo
                //for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                //{
                //    //AFKalenderItemTour ctrTour = new AFKalenderItemTour(this, this.AuftragCtr);
                //    AFKalenderItemTour ctrTour = new AFKalenderItemTour(this);
                //    ctrTour.Tour._GL_User = this.GL_User;
                //    ctrTour.Tour.ID = (decimal)dt.Rows[i]["ID"];
                //    if (clsTour.ExistTourID(this.GL_User, ctrTour.Tour.ID))
                //    {
                //       ctrTour.Kommission.TourID = ctrTour.Tour.ID;  
                //       ctrTour.Tour.IniTourDaten(ctrTour.Tour.ID, ctrTour.Kommission.GetTourenKommssionen());
                //        //autoPlaced => Kommissionen die aus der DB geladen werden und auf den Vehicle Panel 
                //        //gesetzt werden. autoPlaced = true ==> die Kommission darf überall gesetzt werden
                //        //autoPlaced = false ==> Kommission wird manuell disponiert oder umgesetzt, hier gilt:
                //        //früheste Beladezeit = NOW + 30 min sonst kann die Kommission nicht gesetzt werden
                //        ctrTour.Tour.autoPlaced = true;
                //    }
                //    ctrTour.LocationPositionChanged += new AFKalenderItem.LocationPositionChangedEventHandler(this.TourLocationPositionChanged);
                //    this.VehicelPanel.Controls.Add(ctrTour);
                //    this.VehicelPanel.Controls.SetChildIndex(ctrTour, 0);
                //    SetTourPosition(ref ctrTour, true);
                //    ctrTour.Show();
                //    ctrTour.ctrAuftragRefresh += new AFKalenderItemTour.ctrAuftragRefreshEventHandler(ctrAuftragRefresh1);
                //}
            }
        }
        //
        //
        public void GetCtrLeerKMByZM(decimal decZM_ID)
        {
            //Die Tour liefert die Daten für die Anfahrt zur Tour
            //Jede Tour steht für das LKMCtr gezeichnet direkt vor der Tour
            //Deshalb ist bei dem Ctr die Startzeit = die Tourendzeit der Vorgängertour 
            //und die Endzeit = die Startzeit der Tour
            DataTable dt = new DataTable();
            dt.Clear();

            clsTour myTour = new clsTour();
            myTour.StartZeit = GL_User.us_dtDispoVon;
            myTour.EndZeit = GL_User.us_dtDispoBis;
            myTour.KFZ_ZM = decZM_ID;
            dt = myTour.GetLeerKMByZM();
            if (dt.Rows.Count > 0)
            {
                //Test Dispo
                //Int32 iPrev=0;
                //for (Int32 i = 0; i <= dt.Rows.Count-1; i++)
                //{
                //    iPrev = i - 1;
                //    AFKalenderItemLeerKM ctrLKM = new AFKalenderItemLeerKM(this);
                //    ctrLKM.LKM._GL_User = this.GL_User;
                //    ctrLKM.LKM.bToPlace = true;
                //    if(i==0)
                //    {
                //        ctrLKM.LKM.vorTourID =0;
                //        ctrLKM.LKM.nachTourID = (decimal)dt.Rows[i]["ID"];

                //        //Startzeit
                //        if ((DateTime)dt.Rows[i]["StartZeit"] <= this.GL_User.us_dtDispoVon)
                //        {
                //            ctrLKM.LKM.bToPlace = false;
                //        }
                //        else
                //        {
                //            ctrLKM.LKM.StartZeit = this.GL_User.us_dtDispoVon;
                //        }
                //        //Endzeit
                //        ctrLKM.LKM.EndZeit = (DateTime)dt.Rows[i]["StartZeit"];
                //    }
                //    else
                //    {
                //        ctrLKM.LKM.vorTourID = (decimal)dt.Rows[iPrev]["ID"];
                //        ctrLKM.LKM.nachTourID = (decimal)dt.Rows[i]["ID"];

                //        //Startzeit
                //        if (dt.Rows[iPrev] != null)
                //        {
                //            ctrLKM.LKM.StartZeit = (DateTime)dt.Rows[iPrev]["EndZeit"];
                //        }
                //        else
                //        {
                //            ctrLKM.LKM.StartZeit = this.GL_User.us_dtDispoVon;
                //        }
                //        //Endzeit
                //        ctrLKM.LKM.EndZeit = (DateTime)dt.Rows[i]["StartZeit"];         

                //    }
                //    //Alle Klassen werden gefüllt
                //    ctrLKM.LKM.Fill();
                //    this.VehicelPanel.Controls.Add(ctrLKM);
                //    this.VehicelPanel.Controls.SetChildIndex(ctrLKM, 0);
                //    SetLKMCtrPosition(ref ctrLKM);
                //    ctrLKM.Show();
                //}
            }
        }
        //
        private void KalenderRowDefaultHight(ref AFKalenderRow KalenderRow)
        {
            KalenderRow.RowHight = defRowHight;
        }
        //
        private void SetTourPosition(ref AFKalenderItemTour myTourCtr, bool ForInit)
        {
            try
            {
                AFKalenderRow fahrzeug;
                bool okay = false;

                //Cursor Position wird festgehalten zum setzten der TourCtr auf das Fahrzeug
                Point pTour = new Point();
                // pKommi.X = PointTime.GetPointFromTime(KommiCtr.Kommission.BeladeZeit);
                pTour.X = Cursor.Position.X;
                pTour.Y = Cursor.Position.Y;
                myTourCtr.Tour.StartZeitShownCtr = myTourCtr.Tour.StartZeit;
                myTourCtr.Tour.EndZeitShownCtr = myTourCtr.Tour.EndZeit;

                //Check Datum für angezeigtes CTR / Tour im Dispoplan 
                if (!myTourCtr.Tour.autoPlaced)
                {
                    //BeladezeitShownCtr 
                    if (myTourCtr.Tour.StartZeitShownCtr <= this.GL_User.us_dtDispoVon)
                    {
                        myTourCtr.Tour.StartZeitShownCtr = this.GL_User.us_dtDispoVon;
                    }
                    else
                    {
                        myTourCtr.Tour.StartZeitShownCtr = myTourCtr.Tour.StartZeit;
                    }
                    //EntladeZeitShonCtr
                    if (myTourCtr.Tour.EndZeitShownCtr <= this.GL_User.us_dtDispoBis.AddHours(24))
                    {
                        myTourCtr.Tour.EndZeitShownCtr = myTourCtr.Tour.EndZeit;
                    }
                    else
                    {
                        myTourCtr.Tour.EndZeitShownCtr = this.GL_User.us_dtDispoBis.AddHours(24);
                    }
                }

                if (myTourCtr.Tour.StartZeitShownCtr >= myTourCtr.Tour.EndZeitShownCtr)
                {
                    myTourCtr.Tour.EndZeit = myTourCtr.Tour.EndZeit.AddHours(3);
                    myTourCtr.Tour.EndZeitShownCtr = myTourCtr.Tour.EndZeit;
                }
                //TourCtr Punkte für Start und Ende wird ermittelt
                myTourCtr.Left = PointTime.GetPointFromTime(myTourCtr.Tour.StartZeitShownCtr);
                myTourCtr.Width = PointTime.GetPointFromTime(myTourCtr.Tour.EndZeitShownCtr) - myTourCtr.Left;

                //Fahrzeugdaten werden ermittel
                if (ForInit)
                {
                    fahrzeug = getFahrzeugCtrFromID(myTourCtr.Tour.KFZ_ZM);
                }
                else
                {
                    fahrzeug = getFahrzeugCtrFromPoint(pTour);
                }

                if (fahrzeug != null)
                {
                    myTourCtr.Top = fahrzeug.Top + 2;
                }

                //Dispocheck
                AFMethoden Methoden = new AFMethoden();
                Methoden.FillData(myTourCtr, ref myTourCtr.DispoCheck);

                if (myTourCtr.DispoCheck.disponieren)
                {
                    UpdateDispoCheckAfterPosition(ref myTourCtr);
                    while (okay == false)
                    {
                        foreach (AFKalenderItemTour ctr in this.VehicelPanel.Controls.Find("KalenderItemTour", true))
                        {
                            if (!object.ReferenceEquals(ctr, myTourCtr))
                            {
                                while (ctr.Bounds.IntersectsWith(myTourCtr.Bounds) == true)
                                {
                                    if (myTourCtr.Kommission.bDragDrop)
                                    {
                                        //Hier muss ein neuer DispoCheck durchgeführt werden
                                        myTourCtr.Tour.ID = ctr.Tour.ID;
                                        myTourCtr.Kommission.TourID = myTourCtr.Tour.ID;
                                        if ((myTourCtr.Kommission.TerminVorgabe >= myTourCtr.Tour.StartZeit) &&
                                            (myTourCtr.Kommission.TerminVorgabe <= myTourCtr.Tour.EndZeit))
                                        {
                                            myTourCtr.Kommission.EntladeZeit = myTourCtr.Kommission.TerminVorgabe;
                                        }
                                        else
                                        {
                                            myTourCtr.Kommission.EntladeZeit = myTourCtr.Tour.EndZeit;
                                        }
                                        myTourCtr.Tour.bPositionChange = true;
                                        //DispoCheck
                                        myTourCtr.DispoCheck = ctr.DispoCheck;
                                        myTourCtr.DispoCheck.TourID = myTourCtr.Tour.ID;
                                        myTourCtr.DispoCheck.init = true;
                                        myTourCtr.DispoCheck.GewichtFreigabe = false;
                                        //myTourCtr.DispoCheck.UpdateDispoCheckbyID();
                                        //myTourCtr.Kommission.Add();
                                        Methoden.FillData(myTourCtr, ref myTourCtr.DispoCheck);

                                        if (myTourCtr.DispoCheck.disponieren)
                                        {
                                            myTourCtr.Tour = ctr.Tour;
                                            RemoveKalenderItemTourByTour(ctr);
                                        }
                                        else
                                        {
                                            // myTourCtr.Kommission.DeleteKommission();
                                            myTourCtr.DispoCheck.TourID = myTourCtr.Tour.ID;
                                            myTourCtr.DispoCheck.init = false;
                                            myTourCtr.DispoCheck.GewichtFreigabe = true;
                                            //myTourCtr.DispoCheck.UpdateDispoCheckbyTourID();
                                            RemoveALLKalenderItemTourByZM(myTourCtr);
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        myTourCtr.Tour.StartZeit = ctr.Tour.EndZeit.AddMinutes(30);
                                        myTourCtr.Tour.EndZeit = myTourCtr.Tour.EndZeit.AddHours(3);
                                        if (myTourCtr.Tour.StartZeit > myTourCtr.Tour.EndZeit)
                                        {
                                            myTourCtr.Tour.EndZeit = myTourCtr.Tour.StartZeit.AddHours(3);
                                        }
                                        myTourCtr.Tour.UpdateTourDaten();
                                        //TourCtr Punkte für Start und Ende wird ermittelt
                                        myTourCtr.Left = PointTime.GetPointFromTime(myTourCtr.Tour.StartZeit);
                                        myTourCtr.Width = PointTime.GetPointFromTime(myTourCtr.Tour.EndZeit) - myTourCtr.Left;
                                        myTourCtr.Tour.bPositionChange = true;
                                    }
                                }
                            }
                        }
                        okay = true;
                        foreach (AFKalenderItemTour ctr in this.VehicelPanel.Controls.Find("KalenderItemTour", true))
                        {
                            if (!object.ReferenceEquals(ctr, myTourCtr))
                            {
                                if (ctr.Bounds.IntersectsWith(myTourCtr.Bounds))
                                {
                                    okay = false;
                                }
                            }
                        }
                    }
                }
                else //DispoCheck.disponieren = false
                {
                    SetOldTourPosition(ref myTourCtr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //
        private void SetLKMCtrPosition(ref AFKalenderItemLeerKM myCtrLKM)
        {
            try
            {
                AFKalenderRow fahrzeug;
                bool okay = false;

                //Check Datum für angezeigtes CTR / Tour im Dispoplan 
                //BeladezeitShownCtr 
                if (myCtrLKM.LKM.StartZeit <= this.GL_User.us_dtDispoVon)
                {
                    myCtrLKM.LKM.StartZeit = this.GL_User.us_dtDispoVon;
                }

                //EntladeZeitShonCtr
                if (myCtrLKM.LKM.EndZeit >= this.GL_User.us_dtDispoBis.AddHours(24))
                {
                    myCtrLKM.LKM.EndZeit = this.GL_User.us_dtDispoBis.AddHours(24);
                }

                if (myCtrLKM.LKM.StartZeit >= myCtrLKM.LKM.StartZeit)
                {
                    // myTourCtr.Tour.EndZeit = myTourCtr.Tour.EndZeit.AddHours(3);
                    // myTourCtr.Tour.EndZeitShownCtr = myTourCtr.Tour.EndZeit;
                }
                //TourCtr Punkte für Start und Ende wird ermittelt
                myCtrLKM.Left = PointTime.GetPointFromTime(myCtrLKM.LKM.StartZeit);
                myCtrLKM.Width = PointTime.GetPointFromTime(myCtrLKM.LKM.EndZeit) - myCtrLKM.Left;

                fahrzeug = getFahrzeugCtrFromID(myCtrLKM.LKM.nachTour.KFZ_ZM);
                if (fahrzeug != null)
                {
                    myCtrLKM.Top = fahrzeug.Top + 1;
                }

                while (okay == false)
                {
                    foreach (AFKalenderItemTour ctr in this.VehicelPanel.Controls.Find("KalenderItemTour", true))
                    {
                        if (!object.ReferenceEquals(ctr, myCtrLKM))
                        {
                            while (ctr.Bounds.IntersectsWith(myCtrLKM.Bounds) == true)
                            {
                                if (ctr.Tour.ID == myCtrLKM.LKM.nachTour.ID)
                                {
                                    myCtrLKM.LKM.EndZeit = ctr.Tour.StartZeitShownCtr;
                                    myCtrLKM.Width = ctr.Left - myCtrLKM.Left;
                                }
                            }
                        }
                    }
                    okay = true;
                    foreach (AFKalenderItemTour ctr in this.VehicelPanel.Controls.Find("KalenderItemTour", true))
                    {
                        if (!object.ReferenceEquals(ctr, myCtrLKM))
                        {
                            if (ctr.Bounds.IntersectsWith(myCtrLKM.Bounds))
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

        //
        private void SetOldTourPosition(ref AFKalenderItemTour myCtrTour)
        {
            if (myCtrTour.DispoCheck.bo_BackToOldZM == false)
            {
                //KommiCtr.DeleteKommissionFromDispoKalender();
                myCtrTour.DeleteTourFromDispoKalender();
            }
            else
            {
                UpdateTourDatenAfterPosition(ref myCtrTour);
                UpdateDispoCheckAfterPosition(ref myCtrTour);
                //RefreshCurrentKalenderRow(ref myCtrTour);
                RemoveALLKalenderItemTourByZM(myCtrTour);
                GetTourByZM(myCtrTour.Tour.KFZ_ZM);
            }
        }
        //
        //
        //--------------------- Auftrag / Tour wird auf dem Timepanel verschoben  -------------------------------
        //  ----  update von Zeiten und Fahrzeug   
        //
        public void TourLocationPositionChanged(Object sender, Point poinOnRow)
        {
            //Check for Berechtigung
            if (GL_User.write_Disposition)
            {
                try
                {
                    AFKalenderItemTour ctrTour = (AFKalenderItemTour)sender;

                    //autoPlaced => Kommissionen die aus der DB geladen werden und auf den Vehicle Panel 
                    //gesetzt werden. autoPlaced = true ==> die Kommission darf überall gesetzt werden
                    //autoPlaced = false ==> Kommission wird manuell disponiert oder umgesetzt, hier gilt:
                    //früheste Beladezeit = NOW + 30 min sonst kann die Kommission nicht gesetzt werden
                    ctrTour.Tour.autoPlaced = false;

                    Point kPoint = default(Point);
                    kPoint.X = ctrTour.Location.X + poinOnRow.X;
                    kPoint.Y = ctrTour.Location.Y + poinOnRow.Y;

                    // Fahrzeuge
                    clsFahrzeuge fahrz = new clsFahrzeuge();
                    fahrz.BenutzerID = GL_User.User_ID;
                    if (getFahrzeugCtrFromPoint(kPoint) != null)
                    {
                        decimal decTmpOldZMId = 0;
                        fahrz.ID = getFahrzeugCtrFromPoint(kPoint).Fahrzeug.ID;
                        if (fahrz.ID > 0)
                        {
                            fahrz.Fill();

                            if (fahrz.ID != ctrTour.Tour.KFZ_ZM)
                            {

                                decTmpOldZMId = ctrTour.Tour.KFZ_ZM;
                                //Remove und neuladen CTR Leerkilometer, da hier das Fahrzeug umgesetzt wird
                                RemoveALLKalenderItemLeeKMByZM(ctrTour.Tour.KFZ_ZM);
                                RefreshCurrentKalenderRow(ref ctrTour);

                                bool ZM = true;
                                // ID ZM oder Auflieger
                                if (fahrz.ZM == 'T')
                                {
                                    ctrTour.Tour.KFZ_ZM = fahrz.ID;
                                    ctrTour.Tour.UpdateKFZ(ZM);
                                }
                            }
                            //Ctr für LeerFahrt löschen
                            RemoveALLKalenderItemLeeKMByZM(fahrz.ID);
                            RefreshCurrentKalenderRow(ref ctrTour);

                            ctrTour.Tour.StartZeit = PointTime.GetTimeFromPoint(ctrTour.Location.X);
                            ctrTour.Tour.EndZeit = PointTime.GetTimeFromPoint(ctrTour.Location.X + ctrTour.Width);
                            SetTourPosition(ref ctrTour, true);

                            UpdateTourDatenAfterPosition(ref ctrTour);
                            UpdateDispoCheckAfterPosition(ref ctrTour);

                            if (ctrTour.Tour.bPositionChange)
                            {
                                ctrTour.Tour.bPositionChange = false;
                                RemoveALLKalenderItemTourByZM(ctrTour);
                                GetTourByZM(ctrTour.Tour.KFZ_ZM);
                            }

                            if (decTmpOldZMId > 0)
                            {
                                GetCtrLeerKMByZM(decTmpOldZMId);
                                decTmpOldZMId = 0;
                            }
                        }
                    }
                    //Leerkm löschen und neuladen
                    //RemoveALLKalenderItemLeeKMByZM(ctrTour.Tour.KFZ_ZM);
                    GetCtrLeerKMByZM(ctrTour.Tour.KFZ_ZM);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
                AFKalenderItemTour oldTour = (AFKalenderItemTour)sender;
                //Update der Table DispoCheck und Tour auf die alten Werte
                SetOldTourPosition(ref oldTour);

                //Leerkm löschen und neuladen
                RemoveALLKalenderItemLeeKMByZM(oldTour.Tour.KFZ_ZM);
                GetCtrLeerKMByZM(oldTour.Tour.KFZ_ZM);
            }
        }

        //
        private void UpdateDispoCheckAfterPosition(ref AFKalenderItemTour myCtrTour)
        {
            if (myCtrTour != null)
            {
                myCtrTour.DispoCheck.TourID = myCtrTour.Tour.ID;
                myCtrTour.DispoCheck.oldZM = myCtrTour.Tour.KFZ_ZM;
                myCtrTour.DispoCheck.oldStartZeit = myCtrTour.Tour.StartZeit;
                myCtrTour.DispoCheck.oldEndZeit = myCtrTour.Tour.EndZeit;
                myCtrTour.DispoCheck.GewichtFreigabe = true;
                myCtrTour.DispoCheck.bo_BackToOldZM = true;
                myCtrTour.DispoCheck.init = false;
                myCtrTour.DispoCheck.UpdateDispoCheckbyID();
            }
        }
        //
        private void UpdateTourDatenAfterPosition(ref AFKalenderItemTour myCtrTour)
        {
            if (myCtrTour != null)
            {
                //Update der Tourdaten 
                myCtrTour.Tour._GL_User = this.GL_User;
                myCtrTour.Tour.ID = myCtrTour.DispoCheck.TourID;
                myCtrTour.Tour.KFZ_ZM = myCtrTour.DispoCheck.oldZM;
                myCtrTour.Tour.KFZ_A = myCtrTour.DispoCheck.A_ID;
                myCtrTour.Tour.PersonalID = myCtrTour.DispoCheck.P_ID;
                myCtrTour.Tour.StartZeit = myCtrTour.DispoCheck.oldStartZeit;
                myCtrTour.Tour.EndZeit = myCtrTour.DispoCheck.oldEndZeit;
                myCtrTour.Tour.UpdateTourDaten();
            }
        }
        //
        //

        /**********************************************************************************************************
        *                                           DRAG & DROP  
        ***********************************************************************************************************/
        //
        //
        //
        private void VehicelPanel_DragEnter(object sender, DragEventArgs e)
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
            if (e.Data.GetDataPresent(typeof(structAuftPosRow)))
            {
                structAuftPosRow Recource = default(structAuftPosRow);
                try
                {

                    Recource = (structAuftPosRow)e.Data.GetData(typeof(structAuftPosRow));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                e.Effect = DragDropEffects.Copy;
            }
        }
        //
        //---------------- DRAG&DROP Vehicle Panel  ---------------------------------
        //
        private void VehicelPanel_DragDrop(object sender, DragEventArgs e)
        {
            //Check Berechtigung
            if (GL_User.write_Disposition)
            {
                AllowedLoad = true;
                bool RecourceIsUsed = false;

                try
                {
                    // für Daten Kommission
                    if (e.Data.GetDataPresent(typeof(structAuftPosRow)))
                    {
                        clsDispoProperty Property = new clsDispoProperty();
                        structAuftPosRow IDAndRowID = default(structAuftPosRow);
                        IDAndRowID = (structAuftPosRow)e.Data.GetData(typeof(structAuftPosRow));

                        clsAuftragsstatus ast = new clsAuftragsstatus();
                        //ast.Auftrag_ID = IDAndRowID.AuftragID;
                        //ast.AuftragPos = IDAndRowID.AuftragPos;
                        //ast.AP_ID = IDAndRowID.AuftragPosID;

                        //nur Transport, die noch Status 2 haben
                        if (ast.GetAuftragsstatus() < 3)
                        {
                            IDAndRowID.disponierbar = true;
                        }
                        else
                        {
                            IDAndRowID.disponierbar = false;
                        }

                        if (IDAndRowID.disponierbar)
                        {
                            //Test Dispo

                            ////AFKalenderItemTour ctrTour = new AFKalenderItemTour(this, AuftragCtr);
                            //AFKalenderItemTour ctrTour = new AFKalenderItemTour(this);
                            //ctrTour.LocationPositionChanged += new AFKalenderItem.LocationPositionChangedEventHandler(this.TourLocationPositionChanged);
                            //ctrTour.Kommission.AuftragPosTableID = IDAndRowID.AuftragPosTableID;
                            //ctrTour.Kommission.FillByAuftragPosTableID();
                            //ctrTour.Kommission.bDragDrop = true;

                            //pKommi.X = Cursor.Position.X;// PointTime.GetPointFromTime(KommiCtr.Kommission.BeladeZeit);
                            ////pKommi.X = PointTime.GetPointFromTime(PointTime.GetTimeFromPoint(Cursor.Position.X)); 
                            //pKommi.Y = Cursor.Position.Y;

                            //ctrTour.Tour.StartZeit = PointTime.GetTimeFromPoint(this.VehicelPanel.PointToClient(pKommi).X); 
                            //ctrTour.Tour.EndZeit = ctrTour.Kommission.EntladeZeit;
                            //ctrTour.Kommission.BeladeZeit = ctrTour.Tour.StartZeit;

                            ////Tour Startzeit darf nicht kleiner als die Beladezeit sein
                            //if (ctrTour.Tour.StartZeit < ctrTour.Kommission.BeladeZeit)
                            //{
                            //    ctrTour.Tour.StartZeit = ctrTour.Kommission.BeladeZeit;
                            //}
                            ////Tour EndZeit ist kleiner Now
                            //if (ctrTour.Tour.EndZeit < DateTime.Now)
                            //{
                            //    ctrTour.Tour.EndZeit = DateTime.Now.AddHours(3);
                            //}
                            ////StartZeit muss kleiner EndZeit sein
                            //if (ctrTour.Tour.StartZeit >= ctrTour.Tour.EndZeit)
                            //{
                            //    ctrTour.Tour.EndZeit = ctrTour.Tour.StartZeit.AddHours(3);
                            //}
                            //ctrTour.Left = PointTime.GetPointFromTime(ctrTour.Tour.StartZeit);
                            //ctrTour.Width = PointTime.GetPointFromTime(ctrTour.Tour.EndZeit) - ctrTour.Left;
                            //ctrTour.Location = this.VehicelPanel.PointToClient(pKommi);
                            //ctrTour.Tour.KFZ_ZM = getFahrzeugCtrFromPoint(this.VehicelPanel.PointToClient(pKommi)).Fahrzeug.ID;        // ID Fahrzeug           
                            //this.VehicelPanel.Controls.Add(ctrTour);                
                            //SetTourPosition(ref ctrTour, false);
                            //ctrTour.BringToFront();

                            ////wenn eine KOmmission einer Tour zugewiesern werden soll aber den Gewichtscheck nicht besteht,
                            ////darf die Kommission nicht übernommen werden
                            ////Abfrage hier über DispoCheck.Disponieren
                            //if (ctrTour.DispoCheck.disponieren)
                            //{
                            //    if (clsTour.ExistTourID(this.GL_User, ctrTour.Tour.ID))
                            //    {
                            //        ctrTour.Tour.UpdateTourDaten();
                            //    }
                            //    else
                            //    {
                            //        //Eintrag in DB Tour
                            //        ctrTour.Tour.AddToDB();
                            //    }
                            //    if (clsKommission.ExistKommission(this.GL_User, ctrTour.Kommission.ID))
                            //    {
                            //        ctrTour.Kommission.BeladePos = ctrTour.Kommission.maxBeladePos;

                            //        ctrTour.Kommission.UpdateKommission();
                            //    }
                            //    else
                            //    {
                            //        //Eintrag in DB Kommission
                            //        ctrTour.Kommission.TourID = ctrTour.Tour.ID;
                            //        ctrTour.Kommission.BeladePos = ctrTour.Kommission.maxBeladePos+1;
                            //        ctrTour.Kommission.EntladePos = ctrTour.Kommission.maxEntladePos+1;
                            //        ctrTour.Kommission.Add();
                            //    }
                            //    //Status disponiert setzen
                            //    clsAuftragsstatus ap = new clsAuftragsstatus();
                            //    ap.AP_ID = ctrTour.Kommission.AuftragPosTableID;
                            //    ap.Status = 4; //disponiert
                            //    ap.SetStatusDisposition();
                            //    //ctrTour.Show();
                            //    ((ctrAufträge)IDAndRowID.Receiverctr).RowUpdateFromDragDrop(IDAndRowID);

                            //    //Tourberechnung der Strecke
                            //    ctrTour.Tour.TourCalculation();

                            //}
                            ////Update DIspoCHeck
                            //if (ctrTour.DispoCheck.ExistDispoCheckID())
                            //{
                            //    ctrTour.DispoCheck.UpdateDispoCheckbyID();
                            //}
                            //else
                            //{
                            //    ctrTour.DispoCheck.TourID = ctrTour.Tour.ID;
                            //    ctrTour.DispoCheck.Add();
                            //}
                            //RemoveALLKalenderItemTourByZM(ctrTour);
                            //GetTourByZM(ctrTour.Tour.KFZ_ZM);

                            ////Leerkm löschen und neuladen
                            //RemoveALLKalenderItemLeeKMByZM(ctrTour.Tour.KFZ_ZM);
                            //GetCtrLeerKMByZM(ctrTour.Tour.KFZ_ZM);
                        }
                        else
                        {
                            clsMessages.Disposition_AuftragNichtDisponierbar();
                        }
                    }
                    ////******************** Recource  ********************************************  
                    //if (e.Data.GetDataPresent(typeof(Globals._Recources)))
                    //{
                    //Test Dispo
                    //Globals._Recources RecourceGlobal = default(Globals._Recources);
                    //RecourceGlobal = (Globals._Recources)e.Data.GetData(typeof(Globals._Recources));

                    //AFKalenderItemRecource RecourceCtr = new AFKalenderItemRecource(this);
                    //pRecource.X = Cursor.Position.X; //PointTime.GetPointFromTime(RecourceCtr.Recource.TimeFrom);
                    ////pRecource.X = PointTime.GetPointFromTime(PointTime.GetTimeFromPoint(Cursor.Position.X));
                    //pRecource.Y = Cursor.Position.Y;
                    //RecourceCtr.Recource.TimeFrom = RecourceGlobal.TimeFrom;
                    //RecourceCtr.Recource.TimeTo = RecourceGlobal.TimeTo;

                    //if (RecourceCtr.Recource.TimeFrom != DateTime.Now)
                    //{
                    //  RecourceCtr.Recource.TimeFrom = PointTime.GetTimeFromPoint(this.VehicelPanel.PointToClient(pRecource).X);
                    //  //RecourceCtr.Recource.TimeTo=PointTime.GetTimeFromPoint(this.VehicelPanel.PointToClient(pRecource).X+RecourceCtr.Width);  //Endzeit unendlich          
                    //}
                    ////Unterscheidung für Auflieger / Faherer
                    //if (RecourceGlobal.RecourceTyp.ToString() == "F")    //Faherer
                    //{
                    //  RecourceCtr.Name = "KalenderItemRecource_Fahrer";
                    //  RecourceCtr.Recource.PersonalID = RecourceGlobal.PersonalID;
                    //  RecourceCtr.Recource.Name = RecourceGlobal.Name;
                    //  RecourceCtr.Recource.RecourceTyp = RecourceGlobal.RecourceTyp;

                    //  /******************************************************************
                    //   * 1. Check: ist die Resource (Fahrer, Auflieger) frei zur Verwendung? 
                    //   * 2. Check: ob der ZM bereits eine Resource zugewiesen wurde
                    //   * **************************************************************/
                    //  if (clsResource.IsFahrerUsed(RecourceCtr.Recource.TimeFrom, RecourceCtr.Recource.TimeTo, RecourceGlobal.PersonalID, RecourceCtr.Recource.RecourceTyp, RecourceCtr.Recource.RecourceID))
                    //  {
                    //    RecourceIsUsed = true;
                    //  }
                    //  else
                    //  {
                    //    RecourceIsUsed = false;
                    //  }
                    //}
                    //if (RecourceGlobal.RecourceTyp.ToString() == "A")    // Auflieger
                    //{
                    //  RecourceCtr.Name = "KalenderItemRecource_Auflieger";
                    //  RecourceCtr.Recource.VehicleID = RecourceGlobal.VehicleID;
                    //  RecourceCtr.Recource.KFZ = RecourceGlobal.KFZ;
                    //  RecourceCtr.Fahrzeug.FillData();
                    //  RecourceCtr.Recource.RecourceTyp = RecourceGlobal.RecourceTyp;

                    // /******************************************************************
                    //  * 1. Check: ist die Resource (Fahrer, Auflieger) frei zur Verwendung? 
                    //  * 2. Check: ob der ZM bereits eine Resource zugewiesen wurde
                    //  * **************************************************************/

                    //  if (clsResource.IsTrailerUsed(RecourceCtr.Recource.TimeFrom, RecourceCtr.Recource.TimeTo, RecourceCtr.Recource.VehicleID, RecourceCtr.Recource.RecourceTyp, RecourceCtr.Recource.RecourceID))
                    //  //if (clsResource.IsTrailerUsed(RecourceGlobal.TimeFrom, RecourceGlobal.TimeTo, RecourceGlobal.VehicleID, RecourceGlobal.RecourceTyp))
                    //  {
                    //    RecourceIsUsed = true;
                    //  }
                    //  else
                    //  {
                    //    RecourceIsUsed = false;
                    //  }
                    //  }

                    //  // Wenn Recource nicht verwendet wird (RecourceIsUsed=false)
                    //  if (!RecourceIsUsed)
                    //  {
                    //    RecourceCtr.Left = PointTime.GetPointFromTime(RecourceCtr.Recource.TimeFrom);
                    //    RecourceCtr.Width = PointTime.GetPointFromTime(RecourceCtr.Recource.TimeTo) - RecourceCtr.Left;
                    //    RecourceCtr.Location = this.VehicelPanel.PointToClient(Cursor.Position);


                    //    //-- Zuweisung der Resource Auflieger/Faherer der ZM
                    //    AFKalenderRow ZM = new AFKalenderRow();
                    //    ZM = getFahrzeugCtrFromPoint(this.VehicelPanel.PointToClient(pRecource)); //original

                    //    RecourceCtr.Recource.fRecEndTime = clsResource.GetFormerResscourceEndtDateTime(this.GL_User, RecourceCtr.Recource.TimeFrom, ZM.Fahrzeug.ID, true);
                    //    RecourceCtr.Recource.nRecStartTime = clsResource.GetNextResscourceStartDateTime(this.GL_User, RecourceCtr.Recource.TimeFrom, ZM.Fahrzeug.ID, true);

                    //    //Anfangszeitcheck 
                    //    if (RecourceCtr.Recource.TimeFrom <= RecourceCtr.Recource.fRecEndTime)
                    //    {
                    //        RecourceCtr.Recource.TimeFrom = RecourceCtr.Recource.fRecEndTime;
                    //    }
                    //    //Endzeitcheck
                    //    if (RecourceCtr.Recource.TimeTo >= RecourceCtr.Recource.nRecStartTime)
                    //    {
                    //        RecourceCtr.Recource.TimeTo = RecourceCtr.Recource.nRecStartTime;
                    //    }


                    //    //2. Check wie oben beschrieben
                    //    if (clsResource.ZMRecourceIsUse(RecourceCtr.Recource.TimeFrom, RecourceCtr.Recource.TimeTo, ZM.Fahrzeug.ID, RecourceCtr.Recource.RecourceTyp, RecourceCtr.Recource.RecourceID))
                    //    {
                    //        ZM = null;
                    //        clsMessages.Recource_TruckUsedTheRecource();
                    //    }

                    //    if (ZM != null)
                    //    {
                    //      clsResource RecourceCls = new clsResource();
                    //      RecourceCls.m_i_VehicleID_Truck = ZM.Fahrzeug.ID;                       // ID der ZM
                    //      RecourceCls.m_i_PersonalID = RecourceCtr.Recource.PersonalID;             // Faherer ID
                    //      RecourceCls.m_i_VehicleID_Trailer = RecourceCtr.Recource.VehicleID;     // ID des Aufliegers
                    //      RecourceCls.m_dt_TimeFrom = RecourceCtr.Recource.TimeFrom;
                    //      RecourceCls.m_dt_TimeTo = RecourceCtr.Recource.TimeTo;
                    //      RecourceCls.m_ch_RecourceTyp = Convert.ToChar(RecourceCtr.Recource.RecourceTyp);
                    //      RecourceCls.m_str_Name = RecourceCtr.Recource.Name;

                    //      //-- Eintrag in DB  ------
                    //      RecourceCls.Insert_Truck();

                    //      if (RecourceCls.m_ch_RecourceTyp.ToString() == "A")
                    //      {
                    //        RecourceCls.Insert_Trailer();
                    //      }
                    //      if (RecourceCls.m_ch_RecourceTyp.ToString() == "F")
                    //      {
                    //        RecourceCls.Insert_Fahrer();
                    //      }

                    //      SetRecourcePosition(ref RecourceCtr, false);  //Test
                    //      this.VehicelPanel.Controls.Add(RecourceCtr);
                    //      this.VehicelPanel.Controls.SetChildIndex(RecourceCtr, 0);
                    //      RecourceCtr.BringToFront();
                    //      RecourceCtr.Show();

                    //      RessourcenRefresh(RecourceGlobal.RecourceTyp.ToString());
                    //      }
                    //  }
                    //  else
                    //  {
                    //    clsMessages.Recource_IsUsed(); 
                    //  }
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                //KalenderRefresh();

            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //
        //---------------- Refresh Resscourcen -----------
        //
        public void RessourcenRefresh(string strResscource)
        {
            switch (strResscource)
            {
                //Auflieger
                case "A":
                    AllowedLoad = true;
                    TrailerLoaded = false;
                    RemoveKalenderItemRecourceAuflieger();
                    LoadAufliegerRecource();
                    AllowedLoad = false;
                    break;

                case "F":
                    AllowedLoad = true;
                    FahrerLoaded = false;
                    RemoveKalenderItemRecourceFahrer();
                    LoadFahrerRecource();
                    AllowedLoad = false;
                    break;
            }
        }

        //**************************************** Funktionen für Dispoplan/Kommission/Recource **************************
        //
        //
        //
        private AFKalenderRow getFahrzeugCtrFromID(decimal ID)
        {
            AFKalenderRow Fahrzeug;
            foreach (AFKalenderRow ctr in this.VehicelPanel.Controls.Find("KalenderRow", true))
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
        //
        //
        //
        /***
            private void RefreshCurrentKalenderRow(ref AFKalenderItemKommi Kommi)
            {
                /****
              AFKalenderRow RefreshRow= getFahrzeugCtrFromID(Kommi.Kommission.KFZ_ZM);
              if(RefreshRow!=null)
              {
                RefreshRow.Refresh();
              }
                     }
            *****/
        private void RefreshCurrentKalenderRow(ref AFKalenderItemTour myTour)
        {
            AFKalenderRow RefreshRow = getFahrzeugCtrFromID(myTour.Tour.KFZ_ZM);
            if (RefreshRow != null)
            {
                RefreshRow.Refresh();
            }
        }

        private void RefreshOldKalenderRow(ref AFKalenderItemTour myTour)
        {

            AFKalenderRow RefreshRow = getFahrzeugCtrFromID(myTour.DispoCheck.oldZM);
            if (RefreshRow != null)
            {
                RefreshRow.Refresh();
            }

        }

        /***
            private void RefreshOldKalenderRow(ref AFKalenderItemKommi Kommi)
            {
                /***
              AFKalenderRow RefreshRow = getFahrzeugCtrFromID(Kommi.Kommission.oldZM);
              if (RefreshRow != null)
              {
                RefreshRow.Refresh();
              }

            }
                  * ***/
        //
        //-------------- Ein Refresh über alle KalernderRows -----------------------
        //
        private void RefreshAllKalenderRows()
        {
            /***
          foreach (AFKalenderRow ctr in this.VehicelPanel.Controls.Find("KalenderRow", true))
          {
              Int32 i = ctr.Size.Height + 10;
         
              ctr.Size = new Size(ctr.Width, i);
            ctr.Refresh();
          }
             * ***/
        }
        //
        //-------- über den Point an das Control  ------------------------
        //
        private AFKalenderRow getFahrzeugCtrFromPoint(Point point)
        {
            foreach (AFKalenderRow ctr in this.VehicelPanel.Controls.Find("KalenderRow", true))
            {
                if (ctr.Bounds.Contains(point))
                {
                    return ctr;
                }
            }
            return null;
        }
        //
        //
        //
        private enumLadestelle getBeOrEntladeFromKommiPoint(AFKalenderItem Kommi, Point point)
        {
            Rectangle BeladeArea = new Rectangle(Kommi.Bounds.X, Kommi.Bounds.Y, 30, Kommi.Height);
            Rectangle EntladeladeArea = new Rectangle(Kommi.Bounds.X + Kommi.Width - 30, Kommi.Bounds.Y, 30, Kommi.Height);
            try
            {
                if (EntladeladeArea.Contains(point))
                {
                    return enumLadestelle.Entladestelle;
                }
                else if (BeladeArea.Contains(point))
                {
                    return enumLadestelle.Beladestelle;
                }
                else
                {
                    return enumLadestelle.UnKonwn;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return enumLadestelle.UnKonwn;
        }

        //
        //------------------------ Kalender Übergabe aus Menü  ---------------------------
        //

        public void MenuDispoDataChanged(ctrMenu sender)
        {
            /******
                  if (sender.DateChange)
                  {
                    AllowedLoad = sender.DateChange;
                    if (Functions.IsFormAlreadyOpen(typeof(frmDispoKalender)) != null)
                    {
                      if (sender.DispoDatumStart == sender.DispoDatumEnd)
                      {
                        lblCaption.myText = sender.DispoDatumStart.ToLongDateString();
                      }
                      else
                      {
                        lblCaption.myText = sender.DispoDatumStart.ToLongDateString() + " - " + sender.DispoDatumEnd.ToLongDateString();
                      }

                      DateFrom = sender.DispoDatumStart;
                      DateTo = sender.DispoDatumEnd.AddDays(1);
          
                      RemoveCtrFromDispoKalender();
                      KommisLoaded = false;
                      this.TimePanel.Refresh();
                      AllowedLoad = false;

                    }
                  } 
                  //KalenderRefresh();
             * ******/
        }
        //
        //
        //
        public void ctrAuftragDispoDataChanged(ctrAufträge sender)
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispoKalender)) != null)
            {
                if (sender.DispoDatumStart == sender.DispoDatumEnd)
                {
                    lblCaption.myText = sender.DispoDatumStart.ToLongDateString();
                }
                else
                {
                    lblCaption.myText = sender.DispoDatumStart.ToLongDateString() + " - " + sender.DispoDatumEnd.ToLongDateString();
                }

                DispoKalender.SelectionRange.Start = sender.DispoDatumStart;
                DispoKalender.SelectionRange.End = sender.DispoDatumEnd;
                GL_User.us_dtDispoVon = DispoKalender.SelectionRange.Start;
                GL_User.us_dtDispoBis = DispoKalender.SelectionRange.End;

                UpdateGLUserInCtrMenue();
                this.TimePanel.Refresh();
            }
        }
        //
        //---------       Format    --------------
        //
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
        //
        //---------  Show / Hide 
        //
        private void ShowKalenderItemTrailer()
        {
            foreach (AFKalenderItemRecource ctr2 in this.VehicelPanel.Controls.Find("KalenderItemRecource_Auflieger", true))
            {
                ctr2.Show();
            }
        }
        private void HideKalenderItemTrailer()
        {
            foreach (AFKalenderItemRecource ctr2 in this.VehicelPanel.Controls.Find("KalenderItemRecource_Auflieger", true))
            {
                ctr2.Hide();
            }
        }
        //
        //
        //
        private void cbAufliegerListe_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAufliegerListe.Checked)
            {
                frmDispoAufliegerListe AufliegerListe = new frmDispoAufliegerListe();
                AufliegerListe.Show();
                AufliegerListe.BringToFront();
                cbAuflieger.Checked = true;
            }
            else
            {
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispoAufliegerListe)) != null)
                {
                    Functions.frm_FormTypeClose(typeof(frmDispoAufliegerListe));
                }
                cbAuflieger.Checked = false;
            }
        }
        //
        //---------- läd und zeichnet den TimePanel neu - -------------------------
        //
        public void KalenderRefresh()
        {

            AllowedLoad = true;
            bool TrailerLoadZP = TrailerLoaded;
            bool FahrerLoadZP = FahrerLoaded;

            RemoveCtrFromDispoKalender();
            KommisLoaded = false;
            this.TimePanel.Refresh();

            TrailerLoaded = TrailerLoadZP;
            FahrerLoaded = FahrerLoadZP;

            SetRecourceCheckboxen();
            AllowedLoad = false;

        }
        //
        //******************* Control vom Kalender entfernen **********************************
        //
        private void RemoveCtrFromDispoKalender()
        {
            //RemoveKalenderItemKommi();
            RemoveALLKalenderItemLeeKM();
            RemoveKalenderItemTour();
            RemoveKalenderItemRecourceAuflieger();
            RemoveKalenderItemRecourceFahrer();
        }
        private void RemoveKalenderItemKommi()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ThreadCtrInvokeEventHandler(RemoveKalenderItemKommi));
                return;
            }
            foreach (AFKalenderItemKommi ctr in this.VehicelPanel.Controls.Find("KalenderItemKommi", true))
            {          //ctr.DispoCheck.UpdateForRefresh(ctr);
                this.VehicelPanel.Controls.Remove(ctr);
            }
        }

        //Baustelle wird nicht verwendet raus???
        private void RemoveKalenderItemKommiByKommi(AFKalenderItemKommi Kommi)
        {
            foreach (AFKalenderItemKommi ctr in this.VehicelPanel.Controls.Find("KalenderItemKommi", true))
            {
                if (ctr.Kommission.ID == Kommi.Kommission.ID)
                {
                    this.VehicelPanel.Controls.Remove(ctr);
                }
            }
        }
        private void RemoveKalenderItemTour()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ThreadCtrInvokeEventHandler(RemoveKalenderItemTour));
                return;
            }
            foreach (AFKalenderItemTour ctr3 in this.VehicelPanel.Controls.Find("KalenderItemTour", true))
            {
                this.VehicelPanel.Controls.Remove(ctr3);
            }
        }
        private void RemoveKalenderItemTourByTour(AFKalenderItemTour myTour)
        {
            foreach (AFKalenderItemTour ctr in this.VehicelPanel.Controls.Find("KalenderItemTour", true))
            {
                if (ctr.Tour.ID == myTour.Tour.ID)
                {
                    this.VehicelPanel.Controls.Remove(ctr);
                }
            }
        }
        public void RemoveALLKalenderItemTourByZM(AFKalenderItemTour myTour)
        {
            foreach (AFKalenderItemTour ctr in this.VehicelPanel.Controls.Find("KalenderItemTour", true))
            {
                if (ctr.Tour.KFZ_ZM == myTour.Tour.KFZ_ZM)
                {
                    this.VehicelPanel.Controls.Remove(ctr);
                }
            }
        }
        //
        public void RemoveALLKalenderItemLeeKMByZM(decimal myZMId)
        {
            foreach (AFKalenderItemLeerKM ctr in this.VehicelPanel.Controls.Find("KalenderItemLeerKM", true))
            {
                if (ctr.LKM.nachTour.KFZ_ZM == myZMId)
                {
                    this.VehicelPanel.Controls.Remove(ctr);
                }
            }
        }
        //
        //
        public void RemoveALLKalenderItemLeeKM()
        {
            foreach (AFKalenderItemLeerKM ctr in this.VehicelPanel.Controls.Find("KalenderItemLeerKM", true))
            {
                this.VehicelPanel.Controls.Remove(ctr);
            }
        }
        //
        private void RemoveKalenderItemRecourceAuflieger()
        {
            foreach (AFKalenderItemRecource ctr1 in this.VehicelPanel.Controls.Find("KalenderItemRecource_Auflieger", true))
            {
                this.VehicelPanel.Controls.Remove(ctr1);
            }
            TrailerLoaded = false;
        }
        //
        private void RemoveKalenderItemRecourceFahrer()
        {
            foreach (AFKalenderItemRecource ctr2 in this.VehicelPanel.Controls.Find("KalenderItemRecource_Fahrer", true))
            {
                this.VehicelPanel.Controls.Remove(ctr2);
            }
            FahrerLoaded = false;
        }
        //
        //-------------- KalenderItemKOmmi ausblenden ---------------------
        //
        private void HideAllKalenderItemKommi()
        {
            foreach (AFKalenderItemKommi ctr in this.VehicelPanel.Controls.Find("KalenderItemKommi", true))
            {
                ctr.Hide();
            }
        }

        private void HideAllKalenderItemTour()
        {
            foreach (AFKalenderItemTour ctr in this.VehicelPanel.Controls.Find("KalenderItemTour", true))
            {
                ctr.Hide();
            }
        }
        //
        //
        //
        private void ShowAllKalenderItemKommi()
        {
            foreach (AFKalenderItemKommi ctr in this.VehicelPanel.Controls.Find("KalenderItemKommi", true))
            {
                ctr.Show();
                ctr.BringToFront();
            }
        }
        //
        private void ShowAllKalenderItemTour()
        {
            foreach (AFKalenderItemTour ctr in this.VehicelPanel.Controls.Find("KalenderItemTour", true))
            {
                ctr.Show();
                ctr.BringToFront();
            }
        }
        //
        //-------------- Check Fahrerliste  -----------------------------------
        //
        private void cbFahrerliste_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFahrerliste.Checked)
            {
                frmDispoFahrer FahrerListe = new frmDispoFahrer();
                FahrerListe.StartPosition = FormStartPosition.CenterScreen;
                FahrerListe.Show();
                FahrerListe.BringToFront();
                cbFahrer.Checked = true;
            }
            else
            {
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispoFahrer)) != null)
                {
                    Functions.frm_FormTypeClose(typeof(frmDispoFahrer));
                }
                cbFahrer.Checked = false;
            }
        }
        //
        //-------- SHow / Hide Fahrer Item
        //
        private void ShowKalenderItemRecourceFahrer()
        {
            foreach (AFKalenderItemRecource ctr2 in this.VehicelPanel.Controls.Find("KalenderItemRecource_Fahrer", true))
            {
                ctr2.Show();
            }
        }
        private void HideKalenderItemRecourceFahrer()
        {
            foreach (AFKalenderItemRecource ctr2 in this.VehicelPanel.Controls.Find("KalenderItemRecource_Fahrer", true))
            {
                ctr2.Hide();
            }
        }
        //
        //
        //
        private void tsbClose_Click(object sender, EventArgs e)
        {
            RemoveCtrFromDispoKalender();

            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispoFahrer)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmDispoFahrer));
            }
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispoAufliegerListe)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmDispoAufliegerListe));
            }
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispoKalender)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmDispoKalender));
            }
        }
        //
        //-------- Refresh  -------------------
        //
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            KalenderRefresh();
        }
        //
        //
        //
        public void SetAufliegerChecked()
        {
            KalenderRefresh();
            cbAuflieger.Checked = true;
        }
        //
        //
        //
        public void SetFahrerChecked()
        {
            KalenderRefresh();
            cbFahrer.Checked = true;
        }
        //
        //
        //
        private void frmDispoKalender_SizeChanged(object sender, EventArgs e)
        {
            if (KommisLoaded)
            {
                this.KalenderRefresh();
            }
        }
        //++++++++++++++++++++++++++++ Tool Tips vor/zurück Button ++++++++++++++++++++++++++
        //
        //
        private void pbRightVor_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            strInfo = "Endzeitpunkt +1 Tag";
            info.SetToolTip(this.pbRightVor, strInfo);
        }
        private void pbRightZur_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            strInfo = "Endzeitpunkt -1 Tag";
            info.SetToolTip(this.pbRightZur, strInfo);
        }
        private void pbLeftVor_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            strInfo = "Startzeitpunkt +1 Tag";
            info.SetToolTip(this.pbLeftVor, strInfo);
        }
        private void pbLeftZur_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            strInfo = "Startzeitpunkt -1 Tag";
            info.SetToolTip(this.pbLeftZur, strInfo);
        }
        //
        //----------- einstellungen in Datei speichern ------
        //
        private void tbnOK_Click(object sender, EventArgs e)
        {
            //rausgenommen
        }
        //------------------ MOVE Mouse für Zeitraumverschiebung -------------------
        //
        //
        private void TimePanel_MouseDown(object sender, MouseEventArgs e)
        {
            xMove = true;
            oldPos = e.Location;
        }
        //
        private void TimePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                Point startMP = new Point();
                startMP = e.Location;
                Int32 PosDiff = 0;

                if (xMove == false)
                {
                    Cursor = Cursors.Default;

                }
                else
                {
                    PosDiff = (e.X - oldPos.X) / 8;  //+ Left - right 
                    if (PosDiff % 8 == 0)
                    {
                        SetDispoZeitraum(PosDiff);
                    }
                }
            }
        }
        private void SetDispoZeitraum(Int32 PosDiff)
        {
            DateTime newStart;
            DateTime newEnd;
            TimeSpan DiffTage = new TimeSpan();
            //DiffTage = DateTo.Subtract(DateFrom);

            DiffTage = GL_User.us_dtDispoBis.Subtract(GL_User.us_dtDispoVon);

            if (PosDiff > 0)
            {
                /**
              newStart = DateTo;
              newEnd = DateTo.Add(DiffTage);
              DateFrom = newStart;
              DateTo = newEnd;
      ***/
                newStart = GL_User.us_dtDispoBis;
                newEnd = GL_User.us_dtDispoBis.Add(DiffTage);
                GL_User.us_dtDispoVon = newStart;
                GL_User.us_dtDispoBis = newEnd;


            }
            if (PosDiff < 0)
            {
                /***
              newEnd = DateFrom;
              newStart = DateFrom.Subtract(DiffTage);
              DateFrom = newStart;
              DateTo = newEnd;
                ****/

                newEnd = GL_User.us_dtDispoVon;
                newStart = GL_User.us_dtDispoVon.Subtract(DiffTage);
                GL_User.us_dtDispoVon = newStart;
                GL_User.us_dtDispoBis = newEnd;
            }
            KalenderRefresh();
        }
        //
        /*********************************************************************************************************
         *                         zeitverschiebung auf Kalender durch +/- Zeitraum
         ********************************************************************************************************/
        //
        //---- Dispoplan je klick 1 Tage zurück ----------------
        //
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Int32 TageZurück = 1;
            ChangeDateInDispoPlan((-1) * TageZurück, 0);
        }
        private void pbLeftZur_Click(object sender, EventArgs e)
        {
            TimeSpan diff;
            diff = GetDispoDateDiff();
            if (diff.Days > 1)
            {
                Int32 TageZurück = 1;
                ChangeDateInDispoPlan(TageZurück, 0);
            }
        }
        //
        //---- Dispoplan je klick 1 Tag vor ------------------
        //
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Int32 TagVor = 1;
            ChangeDateInDispoPlan(0, TagVor);
        }
        private void pbRightZur_Click(object sender, EventArgs e)
        {
            TimeSpan diff;
            diff = GetDispoDateDiff();
            if (diff.Days > 1)
            {
                Int32 TagVor = 1;
                ChangeDateInDispoPlan(0, ((-1) * TagVor));
            }
        }
        private void ChangeDateInDispoPlan(Int32 TagBack, Int32 TagForward)
        {
            DateTime oldDateFrom = GL_User.us_dtDispoVon;
            DateTime oldDateTo = GL_User.us_dtDispoBis;
            GL_User.us_dtDispoVon = GL_User.us_dtDispoVon.AddDays(TagBack);
            GL_User.us_dtDispoBis = GL_User.us_dtDispoBis.AddDays(TagForward);
            if (GL_User.us_dtDispoVon < GL_User.us_dtDispoBis)
            {
                //KalenderRefresh();
            }
            else
            {
                GL_User.us_dtDispoVon = oldDateFrom;
                GL_User.us_dtDispoBis = oldDateTo;
            }

            //Zeitraum in Kalender anpassen
            //this.DispoKalender.SelectionRange.Start = GL_User.us_dtDispoVon;
            //this.DispoKalender.SelectionRange.End = GL_User.us_dtDispoBis;
            this.DispoKalender.SetSelectionRange(GL_User.us_dtDispoVon, GL_User.us_dtDispoBis);
            //UpdateGLUserInCtrMenue();
        }
        //
        //
        private TimeSpan GetDispoDateDiff()
        {
            GL_User.us_dtDispoVon = GL_User.us_dtDispoVon.Date;
            GL_User.us_dtDispoBis = GL_User.us_dtDispoBis.Date;
            TimeSpan diff = GL_User.us_dtDispoBis.Subtract(GL_User.us_dtDispoVon);
            return diff;
        }
        //************************************** Load / Set User Properties ****************************
        //
        //-------- User Einstellungen für Dispoplan  ---------
        //
        //
        //---------- Kalender Disposition ---------------------
        //
        private void DispoKalender_DateChanged(object sender, DateRangeEventArgs e)
        {
            /*********************************************************
             * Problem autom. Update 
             * ******************************************************/
            //Wegen der Anzeige im Dispokalender -1 Tag
            /***
            if (DispoKalender.SelectionRange.Start < DispoKalender.SelectionRange.End)
            {
                DispoKalender.SelectionRange.End = DispoKalender.SelectionRange.End.AddDays(-1);
            }
            ****/

            GL_User.us_dtDispoVon = DispoKalender.SelectionRange.Start;
            //Für die Anzeige im VehiclePanel wieder einen Tag drauf
            GL_User.us_dtDispoBis = DispoKalender.SelectionRange.End;
            KalenderRefresh();
            UpdateGLUserInCtrMenue();
        }
        //
        private void UpdateGLUserInCtrMenue()
        {
            this.AuftragCtr._ctrMenu.GL_User = GL_User;
        }
        //
        //------------- Info Schadstoffklassen ein-/ausblenden -----------------
        //
        private void label2_Click(object sender, EventArgs e)
        {
            if (label2.Text == "Info Schadstoffklassen anzeigen") //anzeigen
            {
                label2.Text = "Info Schadstoffklassen ausblenden";
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmInfoSchadstoffklassen)) != null)
                {
                    Functions.frm_FormTypeClose(typeof(frmInfoSchadstoffklassen));
                }
                frmInfoSchadstoffklassen schad = new frmInfoSchadstoffklassen();
                schad.StartPosition = FormStartPosition.CenterScreen;
                schad.Show();
                schad.BringToFront();
            }
            else //ausblenden
            {
                label2.Text = "Info Schadstoffklassen anzeigen";
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmInfoSchadstoffklassen)) != null)
                {
                    Functions.frm_FormTypeClose(typeof(frmInfoSchadstoffklassen));
                }
            }
        }

        /*****************************************************************************
         *                          Table Kalender Row
         * ***************************************************************************/
        //
        //
        private void InitTabelKalenderRow()
        {
            if (dtKalenderRow.Columns["ZM_ID"] == null)
            {
                DataColumn ZM_ID = new DataColumn();
                ZM_ID.DataType = System.Type.GetType("System.Int32");
                ZM_ID.Caption = "ZM_ID";
                ZM_ID.ColumnName = "ZM_ID";
                dtKalenderRow.Columns.Add(ZM_ID);
            }
            if (dtKalenderRow.Columns["RowHight"] == null)
            {
                DataColumn RowHight = new DataColumn();
                RowHight.DataType = System.Type.GetType("System.Int32");
                RowHight.Caption = "RowHight";
                RowHight.ColumnName = "RowHight";
                dtKalenderRow.Columns.Add(RowHight);
            }
            if (dtKalenderRow.Columns["DateFrom"] == null)
            {
                DataColumn DateFrom = new DataColumn();
                DateFrom.DataType = System.Type.GetType("System.DateTime");
                DateFrom.Caption = "DateFrom";
                DateFrom.ColumnName = "DateFrom";
                dtKalenderRow.Columns.Add(DateFrom);
            }
            if (dtKalenderRow.Columns["DateTo"] == null)
            {
                DataColumn DateTo = new DataColumn();
                DateTo.DataType = System.Type.GetType("System.DateTime");
                DateTo.Caption = "DateTo";
                DateTo.ColumnName = "DateTo";
                dtKalenderRow.Columns.Add(DateTo);
            }
        }
        //
        //
        //
        private void AddToTableKalenderRow(Int32 zm, Int32 rowHight)
        {
            DataRow row = dtKalenderRow.NewRow();

            row["ZM_ID"] = zm;
            row["RowHight"] = rowHight;
            // row["DateFrom"] = DateFrom;
            // row["DateTo"] = DateTo;
            row["DateFrom"] = GL_User.us_dtDispoVon;
            row["DateTo"] = GL_User.us_dtDispoBis;
            dtKalenderRow.Rows.Add(row);
        }
        //
        //
        //
        private void UpdateRowDTKalenderRow(Int32 zm, Int32 rowHight)
        {
            if ((zm > 0) & (rowHight > 0))
                for (Int32 i = 0; i <= dtKalenderRow.Rows.Count - 1; i++)
                {
                    if ((decimal)dtKalenderRow.Rows[i]["ZM_ID"] == zm)
                    {
                        dtKalenderRow.Rows[i]["RowHight"] = rowHight;
                    }
                }
        }
        //
        //
        //
        private Int32 ReadRowHightFromDTKalenderRow(Int32 zm)
        {
            Int32 retHight = 0;
            if (zm > 0)
            {
                DataTable tmpDT = new DataTable("tmpKalenderRow");
                tmpDT = Functions.FilterDataTable(dtKalenderRow, zm.ToString(), "ZM_ID");

                if (tmpDT.Rows.Count > 0)
                {
                    if (
                        //((DateTime)tmpDT.Rows[1]["DateFrom"]== DateFrom) &
                        //((DateTime)tmpDT.Rows[1]["DateTo"]== DateTo)
                        ((DateTime)tmpDT.Rows[1]["DateFrom"] == GL_User.us_dtDispoVon) &
                        ((DateTime)tmpDT.Rows[1]["DateTo"] == GL_User.us_dtDispoBis)
                      )
                    {
                        retHight = (Int32)tmpDT.Rows[1]["RowHight"];
                    }
                }
            }
            return retHight;
        }
        //
        //
        //
        private bool ZMExistInDTKalenderRow(Int32 zm)
        {
            bool retBool = false;
            Object obj = dtKalenderRow.Compute("COUNT(ZM_ID)", "ZM_ID =" + zm.ToString());
            Int32 tmp = 0;
            if (Int32.TryParse(obj.ToString(), out tmp))
            {
                if (tmp > 0)
                {
                    retBool = true;
                }
            }
            return retBool;
        }
        //
        //------------ KalenderRows optimieren / standard setzen -----------
        //
        private void tsbtnResetRowHight_Click(object sender, EventArgs e)
        {
            AFKalenderRow Fahrzeug;
            foreach (AFKalenderRow ctr in this.VehicelPanel.Controls.Find("KalenderRow", true))
            {
                try
                {

                    Fahrzeug = (AFKalenderRow)ctr;
                    Fahrzeug.RowHight = defRowHight;
                }
                catch // Invalid Cast
                {
                }
            }
            KalenderRefresh();
        }
        //
        //----------- Dispokalender Ansicht integrieren -----------
        //
        private void tsbtnDispoPlanIntegrieren_Click(object sender, EventArgs e)
        {
            if (this.menue != null)
            {
                this.menue.GL_User = this.GL_User;
                this.menue.CloseDispoKaledner();
                this.menue.DispoKalenderOpenSplitter();
            }
        }
        //
        //------------- DIspokalender in eigenem WIndow --------------
        //
        private void tsbtnDispoplanFenster_Click(object sender, EventArgs e)
        {
            if (this.menue != null)
            {
                this.menue.GL_User = this.GL_User;
                this.menue.CloseDispoKaledner();
                this.menue.DispoKalenderOpen();
            }
        }







    }
}
