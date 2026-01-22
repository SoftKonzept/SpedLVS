using LVS;
using Sped4.Classes;
using Sped4.Struct;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4.Controls.Kalender
{
    public partial class ctrCalRow : UserControl
    {
        public const string const_ctrName = "CalRow";
        Globals._GL_USER GL_User;
        private clsPointTime PointTime = new clsPointTime();
        public Point pRecource = new Point();
        public Point pKommi = new Point();
        public bool AllowedLoad = true;
        public bool TrailerLoaded = false;
        public bool FahrerLoaded = false;
        public bool TourLoaded = false;
        internal frmDispo _Kalender;
        public AFKalenderRow VehicleRow = new AFKalenderRow();


        /***************************************************************************************
         * 
         * ************************************************************************************/
        ///<summary>ctrCalRow/ ctrCalRow</summary>
        ///<remarks></remarks>
        public ctrCalRow(frmDispo myKalender)
        {
            InitializeComponent();
            this.Name = ctrCalRow.const_ctrName;
            this._Kalender = myKalender;
            this.GL_User = this._Kalender.AuftragCtr._ctrMenu._frmMain.GL_User;
            this.panRow.Dock = DockStyle.Fill;
            this.Dock = DockStyle.Top;
            this.PointTime = this._Kalender.PointTime;
        }
        ///<summary>ctrCalRow/ InitCtr</summary>
        ///<remarks></remarks>
        private void ctrCalRow_Paint(object sender, PaintEventArgs e)
        {

        }
        ///<summary>ctrCalRow/ InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr(clsFahrzeuge myClsFahrzeug)
        {
            VehicleRow = new AFKalenderRow();
            VehicleRow.Fahrzeug = myClsFahrzeug;
            this.panRow.Controls.Add(VehicleRow);
            this.panRow.Controls.SetChildIndex(VehicleRow, 0);
            VehicleRow.Show();
        }
        ///<summary>ctrCalRow/ LoadUsedRecource</summary>
        ///<remarks></remarks>
        public void LoadUsedRecource(string recType, bool RecourceLoaded)
        {
            clsResource clsRec = new clsResource();
            clsRec._GL_User = this.GL_User;
            clsRec.m_dt_TimeFrom = GL_User.us_dtDispoVon;
            clsRec.m_dt_TimeTo = GL_User.us_dtDispoBis;

            clsRec.m_ch_RecourceTyp = Convert.ToChar(recType);
            DataTable RecourceTable = new DataTable();
            RecourceTable.Clear();
            RecourceTable = clsRec.LoadRecouceByTruckID(this.VehicleRow.Fahrzeug.ID, recType, RecourceLoaded);
            for (Int32 i = 0; i <= RecourceTable.Rows.Count - 1; i++)
            {
                // --- Auflieger
                if (RecourceTable.Rows[i]["RecourceTyp"].ToString() == "A")
                {
                    AFKalenderItemRecource ctrRecource = new AFKalenderItemRecource(this._Kalender);
                    ctrRecource.Recource.RecourceID = (decimal)RecourceTable.Rows[i]["RecourceID"];
                    ctrRecource.Recource.RecourceTyp = RecourceTable.Rows[i]["RecourceTyp"].ToString();
                    ctrRecource.Recource.TimeFrom = (DateTime)RecourceTable.Rows[i]["DateFrom"];
                    ctrRecource.Recource.TimeTo = (DateTime)RecourceTable.Rows[i]["DateTo"];
                    ctrRecource.Recource.VehicleID = (decimal)RecourceTable.Rows[i]["VehicleID"];
                    ctrRecource.Recource.nRecStartTime = clsResource.GetNextResscourceStartDateTime(this.GL_User, ctrRecource.Recource.TimeTo, ctrRecource.Recource.RecourceID, false);
                    ctrRecource.Recource.fRecEndTime = clsResource.GetFormerResscourceEndtDateTime(this.GL_User, ctrRecource.Recource.TimeFrom, ctrRecource.Recource.RecourceID, false);

                    ctrRecource.Name = AFKalenderItemRecource.const_ctrName_Aufliefer;
                    ctrRecource.AllowDrop = true;
                    ctrRecource.LocationPositionChanged += new AFKalenderItemRecource.LocationPositionChangedEventHandler(this.RecourceLocationPositionChanged);
                    ctrRecource.Recource.KFZ = clsFahrzeuge.GetKFZByID(this.GL_User, ctrRecource.Recource.VehicleID);
                    this.panRow.Controls.Add(ctrRecource);
                    this.panRow.Controls.SetChildIndex(ctrRecource, 0);

                    SetRecourcePosition(ref ctrRecource, true);
                    //ctrRecource.Hide();
                    ctrRecource.Show();
                }
                //--- Fahrer
                if (RecourceTable.Rows[i]["RecourceTyp"].ToString() == "F")
                {
                    AFKalenderItemRecource ctrRecource = new AFKalenderItemRecource(this._Kalender);
                    ctrRecource.Recource.RecourceID = (decimal)RecourceTable.Rows[i]["RecourceID"];
                    ctrRecource.Recource.RecourceTyp = RecourceTable.Rows[i]["RecourceTyp"].ToString();
                    ctrRecource.Recource.TimeFrom = (DateTime)RecourceTable.Rows[i]["DateFrom"];
                    ctrRecource.Recource.TimeTo = (DateTime)RecourceTable.Rows[i]["DateTo"];
                    //ctrRecource.Recource.VehicleID = (Int32)RecourceTable.Rows[i]["VehicleID"];
                    ctrRecource.Recource.PersonalID = (decimal)RecourceTable.Rows[i]["PersonalID"];
                    ctrRecource.Recource.nRecStartTime = clsResource.GetNextResscourceStartDateTime(this.GL_User, ctrRecource.Recource.TimeTo, ctrRecource.Recource.RecourceID, false);
                    ctrRecource.Recource.fRecEndTime = clsResource.GetFormerResscourceEndtDateTime(this.GL_User, ctrRecource.Recource.TimeFrom, ctrRecource.Recource.RecourceID, false);

                    ctrRecource.Recource.Name = clsPersonal.GetNameByID((decimal)RecourceTable.Rows[i]["PersonalID"]);
                    ctrRecource.Name = AFKalenderItemRecource.const_ctrName_Fahrer;
                    ctrRecource.AllowDrop = true;
                    ctrRecource.LocationPositionChanged += new AFKalenderItemRecource.LocationPositionChangedEventHandler(this.RecourceLocationPositionChanged);
                    this.panRow.Controls.Add(ctrRecource);
                    this.panRow.Controls.SetChildIndex(ctrRecource, 0);

                    SetRecourcePosition(ref ctrRecource, true);
                    ctrRecource.Show();
                }
            }
        }
        ///<summary>ctrCalRow/ LoadAufliegerRecource</summary>
        ///<remarks></remarks>
        public void LoadAufliegerRecource(bool myAllowedLoad)
        {
            this.AllowedLoad = myAllowedLoad;
            LoadUsedRecource("A", TrailerLoaded);
            TrailerLoaded = true;
        }
        ///<summary>ctrCalRow/ LoadFahrerRecource</summary>
        ///<remarks></remarks>
        public void LoadFahrerRecource(bool myAllowedLoad)
        {
            this.AllowedLoad = myAllowedLoad;
            LoadUsedRecource("F", FahrerLoaded);
            FahrerLoaded = true;
        }
        ///<summary>ctrCalRow/ GetTourByZM</summary>
        ///<remarks></remarks>
        public void GetTourByZM()
        {
            DataTable dt = new DataTable();
            dt.Clear();

            clsTour myTour = new clsTour();
            myTour.StartZeit = GL_User.us_dtDispoVon;
            myTour.EndZeit = GL_User.us_dtDispoBis;
            myTour.KFZ_ZM = this.VehicleRow.Fahrzeug.ID;
            dt = myTour.GetTourByZM();

            if (myTour.KFZ_ZM == 1)
            {
                string strT = "er";
            }
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                //AFKalenderItemTour ctrTour = new AFKalenderItemTour(this, this.AuftragCtr);
                ctrTourItem ctrTour = new ctrTourItem(this._Kalender);
                ctrTour.Tour._GL_User = this.GL_User;
                ctrTour.Tour.ID = (decimal)dt.Rows[i]["ID"];
                if (clsTour.ExistTourID(this.GL_User, ctrTour.Tour.ID))
                {
                    ctrTour.Kommission.TourID = ctrTour.Tour.ID;
                    ctrTour.Tour.InitTourDaten(ctrTour.Tour.ID, ctrTour.Kommission.GetTourenKommssionen());
                    //autoPlaced => Kommissionen die aus der DB geladen werden und auf den Vehicle Panel 
                    //gesetzt werden. autoPlaced = true ==> die Kommission darf überall gesetzt werden
                    //autoPlaced = false ==> Kommission wird manuell disponiert oder umgesetzt, hier gilt:
                    //früheste Beladezeit = NOW + 30 min sonst kann die Kommission nicht gesetzt werden
                    ctrTour.Tour.autoPlaced = true;
                }
                ctrTour.LocationPositionChanged += new ctrTourItem.LocationPositionChangedEventHandler(this.TourLocationPositionChanged);
                ctrTour.ctrCalRowRefresh += new ctrTourItem.ctrCalRowRefreshEventHandler(this.RefreshCalRow);
                ctrTour.ctrAuftragRefresh += new ctrTourItem.ctrAuftragRefreshEventHandler(this._Kalender.ctrAuftragRefresh1);
                this.panRow.Controls.Add(ctrTour);
                this.panRow.Controls.SetChildIndex(ctrTour, 0);
                SetTourPosition(ref ctrTour, true);
                ctrTour.Show();
            }
        }
        ///<summary>ctrCalRow/ GetCtrLeerKMByZM</summary>
        ///<remarks></remarks>
        public void GetCtrLeerKMByZM()
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
            myTour.KFZ_ZM = this.VehicleRow.Fahrzeug.ID;
            dt = myTour.GetLeerKMByZM();
            if (dt.Rows.Count > 0)
            {
                Int32 iPrev = 0;
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    iPrev = i - 1;
                    AFKalenderItemLeerKM ctrLKM = new AFKalenderItemLeerKM(this._Kalender);
                    ctrLKM.LKM._GL_User = this.GL_User;
                    ctrLKM.LKM.bToPlace = true;
                    if (i == 0)
                    {
                        ctrLKM.LKM.vorTourID = 0;
                        ctrLKM.LKM.nachTourID = (decimal)dt.Rows[i]["ID"];

                        //Startzeit
                        if ((DateTime)dt.Rows[i]["StartZeit"] <= this.GL_User.us_dtDispoVon)
                        {
                            ctrLKM.LKM.bToPlace = false;
                        }
                        else
                        {
                            ctrLKM.LKM.StartZeit = this.GL_User.us_dtDispoVon;
                        }
                        //Endzeit
                        ctrLKM.LKM.EndZeit = (DateTime)dt.Rows[i]["StartZeit"];
                    }
                    else
                    {
                        ctrLKM.LKM.vorTourID = (decimal)dt.Rows[iPrev]["ID"];
                        ctrLKM.LKM.nachTourID = (decimal)dt.Rows[i]["ID"];

                        //Startzeit
                        if (dt.Rows[iPrev] != null)
                        {
                            ctrLKM.LKM.StartZeit = (DateTime)dt.Rows[iPrev]["EndZeit"];
                        }
                        else
                        {
                            ctrLKM.LKM.StartZeit = this.GL_User.us_dtDispoVon;
                        }
                        //Endzeit
                        ctrLKM.LKM.EndZeit = (DateTime)dt.Rows[i]["StartZeit"];

                    }
                    //Alle Klassen werden gefüllt
                    ctrLKM.LKM.Fill();
                    this.panRow.Controls.Add(ctrLKM);
                    this.panRow.Controls.SetChildIndex(ctrLKM, 0);
                    SetLKMCtrPosition(ref ctrLKM);
                    ctrLKM.Show();
                }
            }
        }
        ///<summary>ctrCalRow/ RecourceLocationPositionChanged</summary>
        ///<remarks></remarks>
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
                                //KalenderRefresh();
                                this.panRow.Refresh();
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
                    //KalenderRefresh();
                    this.panRow.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        ///<summary>ctrCalRow/ SetRecourcePosition</summary>
        ///<remarks>Setzen der Rescourcen auf den Vehiclepanel</remarks>
        private void SetRecourcePosition(ref AFKalenderItemRecource RecourceCtr, bool ForInit)
        {
            if (AllowedLoad)
            {
                try
                {
                    bool okay = false;
                    //DateTime tmpTime = default(DateTime);
                    AFKalenderRow fahrzeug = this.VehicleRow;
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

                    //fahrzeug =this;
                    //if (ForInit)
                    //{
                    //    decimal ZM_ID = clsResource.GetVehicleIDFromRecource(RecourceCtr.Recource.RecourceID, "Z");
                    //    fahrzeug = getFahrzeugCtrFromID(ZM_ID);
                    //}
                    //else
                    //{
                    //    fahrzeug = getFahrzeugCtrFromPoint(this.panRow.PointToClient(rPoint));   // findet den Punkt nicht           
                    //}

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
                            foreach (AFKalenderItemRecource ctr in this.panRow.Controls.Find(AFKalenderItemRecource.const_ctrName_Aufliefer, true))
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
                            foreach (AFKalenderItemRecource ctr in this.panRow.Controls.Find(AFKalenderItemRecource.const_ctrName_Fahrer, true))
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
                            foreach (AFKalenderItemRecource ctr in this.panRow.Controls.Find(AFKalenderItemRecource.const_ctrName_Aufliefer, true))
                            {
                                if (!object.ReferenceEquals(ctr, RecourceCtr))
                                {
                                    if (ctr.Bounds.IntersectsWith(RecourceCtr.Bounds))
                                    {
                                        okay = false;
                                    }
                                }
                            }
                            foreach (AFKalenderItemRecource ctr in this.panRow.Controls.Find(AFKalenderItemRecource.const_ctrName_Fahrer, true))
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
        ///<summary>ctrCalRow/ SetLKMCtrPosition</summary>
        ///<remarks></remarks>
        private void SetLKMCtrPosition(ref AFKalenderItemLeerKM myCtrLKM)
        {
            try
            {
                AFKalenderRow fahrzeug = this.VehicleRow;
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

                //fahrzeug = getFahrzeugCtrFromID(myCtrLKM.LKM.nachTour.KFZ_ZM);
                //fahrzeug = this;
                if (fahrzeug != null)
                {
                    myCtrLKM.Top = fahrzeug.Top + 1;
                }

                while (okay == false)
                {
                    foreach (ctrTourItem ctr in this.panRow.Controls.Find(ctrTourItem.const_ctrName, true))
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
                    foreach (ctrTourItem ctr in this.panRow.Controls.Find(ctrTourItem.const_ctrName, true))
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
        ///<summary>ctrCalRow/ panRow_DragEnter</summary>
        ///<remarks></remarks>
        private void panRow_DragEnter(object sender, DragEventArgs e)
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
        ///<summary>ctrCalRow/ panRow_DragDrop</summary>
        ///<remarks></remarks>
        private void panRow_DragDrop(object sender, DragEventArgs e)
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
                            //AFKalenderItemTour ctrTour = new AFKalenderItemTour(this, AuftragCtr);
                            ctrTourItem ctrTour = new ctrTourItem(this._Kalender);
                            ctrTour.LocationPositionChanged += new AFKalenderItem.LocationPositionChangedEventHandler(this.TourLocationPositionChanged);
                            ctrTour.ctrCalRowRefresh += new ctrTourItem.ctrCalRowRefreshEventHandler(this.RefreshCalRow);
                            ctrTour.ctrAuftragRefresh += new ctrTourItem.ctrAuftragRefreshEventHandler(this._Kalender.ctrAuftragRefresh1);

                            ctrTour.Kommission.AuftragPosTableID = IDAndRowID.AuftragPosTableID;
                            ctrTour.Kommission.FillByAuftragPosTableID();
                            ctrTour.Kommission.bDragDrop = true;

                            pKommi.X = Cursor.Position.X;// PointTime.GetPointFromTime(KommiCtr.Kommission.BeladeZeit);
                            //pKommi.X = PointTime.GetPointFromTime(PointTime.GetTimeFromPoint(Cursor.Position.X)); 
                            pKommi.Y = Cursor.Position.Y;

                            ctrTour.Tour.StartZeit = PointTime.GetTimeFromPoint(this.panRow.PointToClient(pKommi).X);
                            ctrTour.Tour.EndZeit = ctrTour.Kommission.EntladeZeit;
                            ctrTour.Kommission.BeladeZeit = ctrTour.Tour.StartZeit;

                            //Tour Startzeit darf nicht kleiner als die Beladezeit sein
                            if (ctrTour.Tour.StartZeit < ctrTour.Kommission.BeladeZeit)
                            {
                                ctrTour.Tour.StartZeit = ctrTour.Kommission.BeladeZeit;
                            }
                            //Tour EndZeit ist kleiner Now
                            if (ctrTour.Tour.EndZeit < DateTime.Now)
                            {
                                ctrTour.Tour.EndZeit = DateTime.Now.AddHours(3);
                            }
                            //StartZeit muss kleiner EndZeit sein
                            if (ctrTour.Tour.StartZeit >= ctrTour.Tour.EndZeit)
                            {
                                ctrTour.Tour.EndZeit = ctrTour.Tour.StartZeit.AddHours(3);
                            }
                            ctrTour.Left = PointTime.GetPointFromTime(ctrTour.Tour.StartZeit);
                            ctrTour.Width = PointTime.GetPointFromTime(ctrTour.Tour.EndZeit) - ctrTour.Left;
                            ctrTour.Location = this.panRow.PointToClient(pKommi);
                            //ctrTour.Tour.KFZ_ZM = getFahrzeugCtrFromPoint(this.panRow.PointToClient(pKommi)).Fahrzeug.ID;
                            ctrTour.Tour.KFZ_ZM = this.VehicleRow.Fahrzeug.ID;// ID Fahrzeug           
                            this.panRow.Controls.Add(ctrTour);
                            SetTourPosition(ref ctrTour, false);
                            ctrTour.BringToFront();

                            //wenn eine KOmmission einer Tour zugewiesern werden soll aber den Gewichtscheck nicht besteht,
                            //darf die Kommission nicht übernommen werden
                            //Abfrage hier über DispoCheck.Disponieren
                            if (ctrTour.DispoCheck.disponieren)
                            {
                                if (clsTour.ExistTourID(this.GL_User, ctrTour.Tour.ID))
                                {
                                    ctrTour.Tour.UpdateTourDaten();
                                }
                                else
                                {
                                    //Eintrag in DB Tour
                                    ctrTour.Tour.AddToDB();
                                }
                                if (clsKommission.ExistKommission(this.GL_User, ctrTour.Kommission.ID))
                                {
                                    ctrTour.Kommission.BeladePos = ctrTour.Kommission.maxBeladePos;

                                    ctrTour.Kommission.UpdateKommission();
                                }
                                else
                                {
                                    //Eintrag in DB Kommission
                                    ctrTour.Kommission.TourID = ctrTour.Tour.ID;
                                    ctrTour.Kommission.BeladePos = ctrTour.Kommission.maxBeladePos + 1;
                                    ctrTour.Kommission.EntladePos = ctrTour.Kommission.maxEntladePos + 1;
                                    ctrTour.Kommission.Add();
                                }
                                //Status disponiert setzen
                                clsAuftragsstatus ap = new clsAuftragsstatus();
                                ap.AP_ID = ctrTour.Kommission.AuftragPosTableID;
                                ap.Status = 4; //disponiert
                                ap.SetStatusDisposition();
                                //ctrTour.Show();
                                ((ctrAufträge)IDAndRowID.Receiverctr).RowUpdateFromDragDrop(IDAndRowID);

                                //Tourberechnung der Strecke
                                ctrTour.Tour.TourCalculation();

                            }
                            //Update DIspoCHeck
                            if (ctrTour.DispoCheck.ExistDispoCheckID())
                            {
                                ctrTour.DispoCheck.UpdateDispoCheckbyID();
                            }
                            else
                            {
                                ctrTour.DispoCheck.TourID = ctrTour.Tour.ID;
                                ctrTour.DispoCheck.Add();
                            }
                            //TourItem ist gesetzt jetzt komplett alle löschen und neu laden
                            RemoveALLKalenderItemTour();
                            GetTourByZM();
                            //Leerkm löschen und neuladen
                            RemoveALLKalenderItemLeeKM();
                            GetCtrLeerKMByZM();
                        }
                        else
                        {
                            clsMessages.Disposition_AuftragNichtDisponierbar();
                        }
                    }
                    //******************** Recource  ********************************************  
                    if (e.Data.GetDataPresent(typeof(structAuftPosRow)))
                    {
                        structRecources RecourceGlobal = default(structRecources);
                        RecourceGlobal = (structRecources)e.Data.GetData(typeof(structRecources));

                        AFKalenderItemRecource RecourceCtr = new AFKalenderItemRecource(this._Kalender);
                        pRecource.X = Cursor.Position.X; //PointTime.GetPointFromTime(RecourceCtr.Recource.TimeFrom);
                        //pRecource.X = PointTime.GetPointFromTime(PointTime.GetTimeFromPoint(Cursor.Position.X));
                        pRecource.Y = Cursor.Position.Y;
                        RecourceCtr.Recource.TimeFrom = RecourceGlobal.TimeFrom;
                        RecourceCtr.Recource.TimeTo = RecourceGlobal.TimeTo;

                        if (RecourceCtr.Recource.TimeFrom != DateTime.Now)
                        {
                            RecourceCtr.Recource.TimeFrom = PointTime.GetTimeFromPoint(this.panRow.PointToClient(pRecource).X);
                            //RecourceCtr.Recource.TimeTo=PointTime.GetTimeFromPoint(this.VehicelPanel.PointToClient(pRecource).X+RecourceCtr.Width);  //Endzeit unendlich          
                        }
                        //Unterscheidung für Auflieger / Faherer
                        if (RecourceGlobal.RecourceTyp.ToString() == "F")    //Faherer
                        {
                            RecourceCtr.Name = "KalenderItemRecource_Fahrer";
                            RecourceCtr.Recource.PersonalID = RecourceGlobal.PersonalID;
                            RecourceCtr.Recource.Name = RecourceGlobal.Name;
                            RecourceCtr.Recource.RecourceTyp = RecourceGlobal.RecourceTyp;

                            /******************************************************************
                             * 1. Check: ist die Resource (Fahrer, Auflieger) frei zur Verwendung? 
                             * 2. Check: ob der ZM bereits eine Resource zugewiesen wurde
                             * **************************************************************/
                            if (clsResource.IsFahrerUsed(RecourceCtr.Recource.TimeFrom, RecourceCtr.Recource.TimeTo, RecourceGlobal.PersonalID, RecourceCtr.Recource.RecourceTyp, RecourceCtr.Recource.RecourceID))
                            {
                                RecourceIsUsed = true;
                            }
                            else
                            {
                                RecourceIsUsed = false;
                            }
                        }
                        if (RecourceGlobal.RecourceTyp.ToString() == "A")    // Auflieger
                        {
                            RecourceCtr.Name = "KalenderItemRecource_Auflieger";
                            RecourceCtr.Recource.VehicleID = RecourceGlobal.VehicleID;
                            RecourceCtr.Recource.KFZ = RecourceGlobal.KFZ;
                            RecourceCtr.Fahrzeug.Fill();
                            RecourceCtr.Recource.RecourceTyp = RecourceGlobal.RecourceTyp;

                            /******************************************************************
                             * 1. Check: ist die Resource (Fahrer, Auflieger) frei zur Verwendung? 
                             * 2. Check: ob der ZM bereits eine Resource zugewiesen wurde
                             * **************************************************************/

                            if (clsResource.IsTrailerUsed(RecourceCtr.Recource.TimeFrom, RecourceCtr.Recource.TimeTo, RecourceCtr.Recource.VehicleID, RecourceCtr.Recource.RecourceTyp, RecourceCtr.Recource.RecourceID))
                            //if (clsResource.IsTrailerUsed(RecourceGlobal.TimeFrom, RecourceGlobal.TimeTo, RecourceGlobal.VehicleID, RecourceGlobal.RecourceTyp))
                            {
                                RecourceIsUsed = true;
                            }
                            else
                            {
                                RecourceIsUsed = false;
                            }
                        }

                        // Wenn Recource nicht verwendet wird (RecourceIsUsed=false)
                        if (!RecourceIsUsed)
                        {
                            RecourceCtr.Left = PointTime.GetPointFromTime(RecourceCtr.Recource.TimeFrom);
                            RecourceCtr.Width = PointTime.GetPointFromTime(RecourceCtr.Recource.TimeTo) - RecourceCtr.Left;
                            RecourceCtr.Location = this.panRow.PointToClient(Cursor.Position);


                            //-- Zuweisung der Resource Auflieger/Faherer der ZM
                            AFKalenderRow ZM = new AFKalenderRow();
                            ZM = getFahrzeugCtrFromPoint(this.panRow.PointToClient(pRecource)); //original

                            RecourceCtr.Recource.fRecEndTime = clsResource.GetFormerResscourceEndtDateTime(this.GL_User, RecourceCtr.Recource.TimeFrom, ZM.Fahrzeug.ID, true);
                            RecourceCtr.Recource.nRecStartTime = clsResource.GetNextResscourceStartDateTime(this.GL_User, RecourceCtr.Recource.TimeFrom, ZM.Fahrzeug.ID, true);

                            //Anfangszeitcheck 
                            if (RecourceCtr.Recource.TimeFrom <= RecourceCtr.Recource.fRecEndTime)
                            {
                                RecourceCtr.Recource.TimeFrom = RecourceCtr.Recource.fRecEndTime;
                            }
                            //Endzeitcheck
                            if (RecourceCtr.Recource.TimeTo >= RecourceCtr.Recource.nRecStartTime)
                            {
                                RecourceCtr.Recource.TimeTo = RecourceCtr.Recource.nRecStartTime;
                            }


                            //2. Check wie oben beschrieben
                            if (clsResource.ZMRecourceIsUse(RecourceCtr.Recource.TimeFrom, RecourceCtr.Recource.TimeTo, ZM.Fahrzeug.ID, RecourceCtr.Recource.RecourceTyp, RecourceCtr.Recource.RecourceID))
                            {
                                ZM = null;
                                clsMessages.Recource_TruckUsedTheRecource();
                            }

                            if (ZM != null)
                            {
                                clsResource RecourceCls = new clsResource();
                                RecourceCls.m_i_VehicleID_Truck = ZM.Fahrzeug.ID;                       // ID der ZM
                                RecourceCls.m_i_PersonalID = RecourceCtr.Recource.PersonalID;             // Faherer ID
                                RecourceCls.m_i_VehicleID_Trailer = RecourceCtr.Recource.VehicleID;     // ID des Aufliegers
                                RecourceCls.m_dt_TimeFrom = RecourceCtr.Recource.TimeFrom;
                                RecourceCls.m_dt_TimeTo = RecourceCtr.Recource.TimeTo;
                                RecourceCls.m_ch_RecourceTyp = Convert.ToChar(RecourceCtr.Recource.RecourceTyp);
                                RecourceCls.m_str_Name = RecourceCtr.Recource.Name;

                                //-- Eintrag in DB  ------
                                RecourceCls.Insert_Truck();

                                if (RecourceCls.m_ch_RecourceTyp.ToString() == "A")
                                {
                                    RecourceCls.Insert_Trailer();
                                }
                                if (RecourceCls.m_ch_RecourceTyp.ToString() == "F")
                                {
                                    RecourceCls.Insert_Fahrer();
                                }

                                SetRecourcePosition(ref RecourceCtr, false);  //Test
                                this.panRow.Controls.Add(RecourceCtr);
                                this.panRow.Controls.SetChildIndex(RecourceCtr, 0);
                                RecourceCtr.BringToFront();
                                RecourceCtr.Show();

                                RessourcenRefresh(RecourceGlobal.RecourceTyp.ToString());
                            }
                        }
                        else
                        {
                            clsMessages.Recource_IsUsed();
                        }
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
        ///<summary>ctrCalRow/ TourLocationPositionChanged</summary>
        ///<remarks>  </remarks>
        public void TourLocationPositionChanged(Object sender, Point poinOnRow)
        {
            //Check for Berechtigung
            if (GL_User.write_Disposition)
            {
                try
                {
                    ctrTourItem ctrTour = (ctrTourItem)sender;

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
                                RemoveALLKalenderItemLeeKM();
                                ctrTour.Refresh();
                                //RefreshCurrentKalenderRow(ref ctrTour);

                                bool ZM = true;
                                // ID ZM oder Auflieger
                                if (fahrz.ZM == 'T')
                                {
                                    ctrTour.Tour.KFZ_ZM = fahrz.ID;
                                    ctrTour.Tour.UpdateKFZ(ZM);
                                }
                            }
                            //Ctr für LeerFahrt löschen
                            RemoveALLKalenderItemLeeKM();
                            ctrTour.Refresh();

                            ctrTour.Tour.StartZeit = PointTime.GetTimeFromPoint(ctrTour.Location.X);
                            ctrTour.Tour.EndZeit = PointTime.GetTimeFromPoint(ctrTour.Location.X + ctrTour.Width);
                            SetTourPosition(ref ctrTour, true);

                            UpdateTourDatenAfterPosition(ref ctrTour);
                            UpdateDispoCheckAfterPosition(ref ctrTour);

                            if (ctrTour.Tour.bPositionChange)
                            {
                                ctrTour.Tour.bPositionChange = false;
                                RemoveALLKalenderItemTour();
                                GetTourByZM();
                            }

                            if (decTmpOldZMId > 0)
                            {
                                GetCtrLeerKMByZM();
                                decTmpOldZMId = 0;
                            }
                        }
                    }
                    //Leerkm löschen und neuladen
                    RemoveALLKalenderItemLeeKM();
                    GetCtrLeerKMByZM();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
                ctrTourItem oldTour = (ctrTourItem)sender;
                //Update der Table DispoCheck und Tour auf die alten Werte
                SetOldTourPosition(ref oldTour);

                //Leerkm löschen und neuladen
                RemoveALLKalenderItemLeeKM();
                GetCtrLeerKMByZM();
            }
        }
        ///<summary>ctrCalRow/ UpdateDispoCheckAfterPosition</summary>
        ///<remarks>  </remarks>
        private void UpdateDispoCheckAfterPosition(ref ctrTourItem myCtrTour)
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
        ///<summary>ctrCalRow/ UpdateTourDatenAfterPosition</summary>
        ///<remarks>  </remarks>
        private void UpdateTourDatenAfterPosition(ref ctrTourItem myCtrTour)
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
        ///<summary>ctrCalRow/ getFahrzeugCtrFromPoint</summary>
        ///<remarks>  </remarks>
        private AFKalenderRow getFahrzeugCtrFromPoint(Point point)
        {
            foreach (AFKalenderRow ctr in this.panRow.Controls.Find("KalenderRow", true))
            {
                if (ctr.Bounds.Contains(point))
                {
                    return ctr;
                }
            }
            return null;
        }
        ///<summary>ctrCalRow/ getFahrzeugCtrFromID</summary>
        ///<remarks>  </remarks>
        //private AFKalenderRow getFahrzeugCtrFromID(decimal ID)
        //{
        //    AFKalenderRow Fahrzeug;
        //    foreach (AFKalenderRow ctr in this.panRow.Controls.Find("KalenderRow", true))
        //    {
        //        try
        //        {
        //            Fahrzeug = (AFKalenderRow)ctr;
        //            if (Fahrzeug.Fahrzeug.ID == ID)
        //            {
        //                return Fahrzeug;
        //            }
        //        }
        //        catch // Invalid Cast
        //        {
        //        }
        //    }
        //    return null;
        //}
        ///<summary>ctrCalRow/ RefreshCalRow</summary>
        ///<remarks>  </remarks>
        private void RefreshCalRow()
        {
            this.Refresh();
            this.RemoveCtrFromKalenderRow();
            this.GetTourByZM();
            this.GetCtrLeerKMByZM();
        }
        ///<summary>ctrCalRow/ RefreshOldKalenderRow</summary>
        ///<remarks>  </remarks>
        private void RefreshOldKalenderRow(ref ctrTourItem myTour)
        {
            // AFKalenderRow RefreshRow = getFahrzeugCtrFromID(myTour.DispoCheck.oldZM);
            ctrCalRow RefreshRow = this;
            if (RefreshRow != null)
            {
                RefreshRow.Refresh();
            }
        }
        ///<summary>ctrCalRow/ SetTourPosition</summary>
        ///<remarks>  </remarks>
        private void SetTourPosition(ref ctrTourItem myTourCtr, bool ForInit)
        {
            try
            {
                AFKalenderRow fahrzeug = this.VehicleRow;
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
                //fahrzeug = this;
                //if (ForInit)
                //{
                //    fahrzeug = getFahrzeugCtrFromID(myTourCtr.Tour.KFZ_ZM);
                //}
                //else
                //{
                //    fahrzeug = getFahrzeugCtrFromPoint(pTour);
                //}

                if (fahrzeug != null)
                {
                    myTourCtr.Top = fahrzeug.Top + 2;
                }
                //Dispocheck
                AFMethoden MethDispoCheck = new AFMethoden();
                MethDispoCheck.FillData1(myTourCtr, ref myTourCtr.DispoCheck);
                //Methoden.FillData(myTourCtr, ref myTourCtr.DispoCheck);

                if (myTourCtr.DispoCheck.disponieren)
                {
                    UpdateDispoCheckAfterPosition(ref myTourCtr);
                    while (okay == false)
                    {
                        //oreach (AFKalenderItemTour ctr in this.panRow.Controls.Find("KalenderItemTour", true))
                        foreach (ctrTourItem ctr in this.panRow.Controls.Find(ctrTourItem.const_ctrName, true))
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
                                        MethDispoCheck.FillData1(myTourCtr, ref myTourCtr.DispoCheck);

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
                                            RemoveALLKalenderItemTour();
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
                        //foreach (AFKalenderItemTour ctr in this.panRow.Controls.Find("KalenderItemTour", true))
                        foreach (ctrTourItem ctr in this.panRow.Controls.Find(ctrTourItem.const_ctrName, true))
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
        ///<summary>ctrCalRow/ RemoveALLKalenderItemLeeKM</summary>
        ///<remarks>Entfernt alle KilometerItem vom Ctr</remarks>
        public void RemoveALLKalenderItemLeeKM()
        {
            foreach (AFKalenderItemLeerKM ctr in this.panRow.Controls.Find(AFKalenderItemLeerKM.const_ctrName, true))
            {
                this.panRow.Controls.Remove(ctr);
            }
        }
        ///<summary>ctrCalRow/ RemoveALLKalenderItemTourByZM</summary>
        ///<remarks>Entfernt alle KilometerItem vom Ctr</remarks>
        public void RemoveALLKalenderItemTour()
        {
            foreach (ctrTourItem ctr in this.panRow.Controls.Find(ctrTourItem.const_ctrName, true))
            {
                this.panRow.Controls.Remove(ctr);
            }
        }
        ///<summary>ctrCalRow/ SetOldTourPosition</summary>
        ///<remarks></remarks>
        private void SetOldTourPosition(ref ctrTourItem myCtrTour)
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
                RemoveALLKalenderItemTour();
                GetTourByZM();
            }
        }
        ///<summary>ctrCalRow/ RessourcenRefresh</summary>
        ///<remarks>  </remarks>
        public void RessourcenRefresh(string strResscource)
        {
            switch (strResscource)
            {
                //Auflieger
                case "A":
                    AllowedLoad = true;
                    TrailerLoaded = false;
                    RemoveKalenderItemRecourceAuflieger();
                    LoadAufliegerRecource(this.AllowedLoad);
                    AllowedLoad = false;
                    break;

                case "F":
                    AllowedLoad = true;
                    FahrerLoaded = false;
                    RemoveKalenderItemRecourceFahrer();
                    LoadFahrerRecource(this.AllowedLoad);
                    AllowedLoad = false;
                    break;
            }
        }
        ///<summary>ctrCalRow/ RemoveKalenderItemTourByTour</summary>
        ///<remarks>  </remarks>
        private void RemoveKalenderItemTourByTour(ctrTourItem myTour)
        {
            foreach (ctrTourItem ctr in this.panRow.Controls.Find(ctrTourItem.const_ctrName, true))
            {
                if (ctr.Tour.ID == myTour.Tour.ID)
                {
                    this.panRow.Controls.Remove(ctr);
                }
            }
        }
        ///<summary>ctrCalRow/ RemoveKalenderItemRecourceAuflieger</summary>
        ///<remarks>  </remarks>
        public void RemoveKalenderItemRecourceAuflieger()
        {
            foreach (AFKalenderItemRecource ctr1 in this.panRow.Controls.Find(AFKalenderItemRecource.const_ctrName_Aufliefer, true))
            {
                this.panRow.Controls.Remove(ctr1);
            }
            TrailerLoaded = false;
        }
        ///<summary>ctrCalRow/ RemoveKalenderItemRecourceFahrer</summary>
        ///<remarks>  </remarks>
        public void RemoveKalenderItemRecourceFahrer()
        {
            foreach (AFKalenderItemRecource ctr2 in this.panRow.Controls.Find(AFKalenderItemRecource.const_ctrName_Fahrer, true))
            {
                this.panRow.Controls.Remove(ctr2);
            }
            FahrerLoaded = false;
        }
        ///<summary>ctrCalRow/ RemoveCtrFromDispoKalender</summary>
        ///<remarks>  </remarks>
        public void RemoveCtrFromKalenderRow()
        {
            RemoveALLKalenderItemLeeKM();
            RemoveKalenderItemTour();
            RemoveKalenderItemRecourceAuflieger();
            RemoveKalenderItemRecourceFahrer();
        }
        ///<summary>ctrCalRow/ RemoveKalenderItemTour</summary>
        ///<remarks>  </remarks>
        public void RemoveKalenderItemTour()
        {
            foreach (ctrTourItem ctr3 in this.panRow.Controls.Find(ctrTourItem.const_ctrName, true))
            {
                this.panRow.Controls.Remove(ctr3);
            }
        }
        ///<summary>ctrCalRow/ HideAllTourItems</summary>
        ///<remarks>  </remarks>
        public void HideAllTourItems()
        {
            foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.panRow.Controls.Find(ctrTourItem.const_ctrName, true))
            {
                ctr.Hide();
            }
        }
        ///<summary>ctrCalRow/ HideAllTourItems</summary>
        ///<remarks>  </remarks>
        public void ShowAllTourItems()
        {
            foreach (Sped4.Controls.ctrTourItem ctr in this.panRow.Controls.Find(ctrTourItem.const_ctrName, true))
            {
                ctr.Show();
                ctr.BringToFront();
            }
        }

    }
}
