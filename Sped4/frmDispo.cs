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
    public partial class frmDispo : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public delegate void ThreadCtrInvokeEventHandler();
        public Int32 defRowHight = 71;
        public DataTable dtKalenderRow = new DataTable("KaldenderRow");
        public ctrMenu menue;
        internal AFKalenderItemTour _ctrKalenderItemTour;
        //Test
        //internal Sped4.Controls.Kalender.ctrCalRow _ctrCalRow;
        internal Sped4.Controls.ctrTourItem _ctrTourItem;

        internal decimal[] arZM;

        //public delegate void DispoDataChangedEventHandler(decimal Menge, Int32 RowIndex);
        public delegate void DispoDataChangedEventHandler(structAuftPosRow _IDAndRowID);
        //public event DispoDataChangedEventHandler DispoDataChanged;

        public ctrAufträge AuftragCtr;

        internal clsPointTime PointTime = new clsPointTime();
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
        //public event ctrAuftragRefreshEventHandler ctrAuftragRefresh;

        /******************************************************************************************************
         *                               Methoden / Procedures
         * ***************************************************************************************************/
        ///<summary>frmDispoKalender/ frmDispoKalender</summary>
        ///<remarks></remarks>
        public frmDispo(ctrAufträge _AuftragCtr)
        {
            InitializeComponent();
            this.ResizeRedraw = true;
            AuftragCtr = _AuftragCtr;
            GL_User = AuftragCtr._ctrMenu.GL_User;
            DispoKalender.SelectionRange.Start = GL_User.us_dtDispoVon.Date;
            DispoKalender.SelectionRange.End = GL_User.us_dtDispoBis.Date;
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
                    //LoadFahrerRecource();
                    foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find("CalRow", true))
                    {
                        ctr.LoadFahrerRecource(AllowedLoad);
                    }
                }
            }
            else
            {
                FahrerLoaded = false;
                //RemoveKalenderItemRecourceFahrer();
                foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find("CalRow", true))
                {
                    ctr.RemoveKalenderItemRecourceFahrer();
                }
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
                    //LoadAufliegerRecource();
                    foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find("CalRow", true))
                    {
                        ctr.LoadAufliegerRecource(AllowedLoad);
                    }
                }
            }
            else
            {
                TrailerLoaded = false;
                //RemoveKalenderItemRecourceAuflieger();
                foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find("CalRow", true))
                {
                    ctr.RemoveKalenderItemRecourceAuflieger();
                }
            }
            AllowedLoad = false;
            //ShowAllKalenderItemKommi();
            ShowAllKalenderItemTour();
        }
        ///<summary>frmDispoKalender/ SetRecourceCheckboxen</summary>
        ///<remarks></remarks>
        private void SetRecourceCheckboxen()
        {
            cbAuflieger.Checked = TrailerLoaded;
            cbFahrer.Checked = FahrerLoaded;
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
                //foreach (AFKalenderRow ctr in this.VehicelPanel.Controls.Find("KalenderRow", true))
                foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find("CalRow", true))
                {
                    try
                    {
                        Fahrzeug = ctr.VehicleRow;          //ZM
                        Fahrzeug.Width = this.VehicelPanel.Width;
                        Fahrzeug.XLines = PointTime.GetXArray();
                        Fahrzeug.Refresh();
                    }
                    catch
                    {
                    }
                }
                //jetzt erst die Kommissionen, damit erst die Rows gezeichnert werden 
                foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find("CalRow", true))
                {
                    try
                    {
                        //Kommisssionen werden geladen
                        if (!ctr.TourLoaded)
                        {
                            ctr.GetTourByZM();
                            ctr.GetCtrLeerKMByZM();
                        }
                        ctr.TourLoaded = true;
                    }
                    catch
                    {
                    }
                }

                //Rssourcen werden geladen
                if (cbAuflieger.Checked == true)
                {
                    //LoadAufliegerRecource();
                    foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find("CalRow", true))
                    {
                        ctr.LoadAufliegerRecource(this.AllowedLoad);
                    }
                }
                if (cbFahrer.Checked == true)
                {
                    //LoadFahrerRecource();
                    foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find("CalRow", true))
                    {
                        ctr.LoadFahrerRecource(this.AllowedLoad);
                    }
                }
                SetRecourceCheckboxen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        ///<summary>frmDispo/ getFahrzeuge_ZM</summary>
        ///<remarks>Zugmaschinen / LKE werden geladen</remarks>
        private void getFahrzeuge_ZM()
        {
            DataTable FahrzTable = new DataTable();
            FahrzTable = clsFahrzeuge.GetFahrzeuge_ZM(GL_User.User_ID);

            arZM = new decimal[FahrzTable.Rows.Count];

            for (Int32 i = 0; i <= FahrzTable.Rows.Count - 1; i++)
            {
                Sped4.Controls.Kalender.ctrCalRow ctrDispo = new Sped4.Controls.Kalender.ctrCalRow(this);
                clsFahrzeuge Fahrz = new clsFahrzeuge();
                Fahrz.BenutzerID = GL_User.User_ID;

                //---------------- Zugmaschine / Kalenderrow  ------------------------------
                Fahrz.ID = (decimal)FahrzTable.Rows[i]["ID"];
                arZM[i] = Fahrz.ID;
                Fahrz.Fill();
                ctrDispo.InitCtr(Fahrz);
                ctrDispo.Dock = System.Windows.Forms.DockStyle.Top;
                this.VehicelPanel.Controls.Add(ctrDispo);
                this.VehicelPanel.Controls.SetChildIndex(ctrDispo, 0);
                ctrDispo.Show();
            }
        }
        ///<summary>frmDispo/ VehicelPanel_DragEnter</summary>
        ///<remarks></remarks>
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
        ///<summary>frmDispo/ VehicelPanel_DragDrop</summary>
        ///<remarks></remarks>
        private void VehicelPanel_DragDrop(object sender, DragEventArgs e)
        {
            ////Check Berechtigung
            //  if (GL_User.write_Disposition)
            //{
            //  AllowedLoad = true;
            //  bool RecourceIsUsed = false;

            //  try
            //  {
            //    // für Daten Kommission
            //    if (e.Data.GetDataPresent(typeof(Globals.strAuftPosRow)))
            //    {
            //      clsDispoProperty Property = new clsDispoProperty();
            //      Globals.strAuftPosRow IDAndRowID = default(Globals.strAuftPosRow);
            //      IDAndRowID = (Globals.strAuftPosRow)e.Data.GetData(typeof(Globals.strAuftPosRow));

            //      clsAuftragsstatus ast = new clsAuftragsstatus();
            //      //ast.Auftrag_ID = IDAndRowID.AuftragID;
            //      //ast.AuftragPos = IDAndRowID.AuftragPos;
            //      //ast.AP_ID = IDAndRowID.AuftragPosID;

            //      //nur Transport, die noch Status 2 haben
            //      if (ast.GetAuftragsstatus() < 3)
            //      {
            //        IDAndRowID.disponierbar = true;
            //      }
            //      else
            //      {
            //        IDAndRowID.disponierbar = false;
            //      }

            //      if (IDAndRowID.disponierbar)
            //      {
            //          //AFKalenderItemTour ctrTour = new AFKalenderItemTour(this, AuftragCtr);
            //          AFKalenderItemTour ctrTour = new AFKalenderItemTour(this);
            //          ctrTour.LocationPositionChanged += new AFKalenderItem.LocationPositionChangedEventHandler(this.TourLocationPositionChanged);
            //          ctrTour.Kommission.AuftragPosTableID = IDAndRowID.AuftragPosTableID;
            //          ctrTour.Kommission.FillByAuftragPosTableID();
            //          ctrTour.Kommission.bDragDrop = true;

            //          pKommi.X = Cursor.Position.X;// PointTime.GetPointFromTime(KommiCtr.Kommission.BeladeZeit);
            //          //pKommi.X = PointTime.GetPointFromTime(PointTime.GetTimeFromPoint(Cursor.Position.X)); 
            //          pKommi.Y = Cursor.Position.Y;

            //          ctrTour.Tour.StartZeit = PointTime.GetTimeFromPoint(this.VehicelPanel.PointToClient(pKommi).X); 
            //          ctrTour.Tour.EndZeit = ctrTour.Kommission.EntladeZeit;
            //          ctrTour.Kommission.BeladeZeit = ctrTour.Tour.StartZeit;

            //          //Tour Startzeit darf nicht kleiner als die Beladezeit sein
            //          if (ctrTour.Tour.StartZeit < ctrTour.Kommission.BeladeZeit)
            //          {
            //              ctrTour.Tour.StartZeit = ctrTour.Kommission.BeladeZeit;
            //          }
            //          //Tour EndZeit ist kleiner Now
            //          if (ctrTour.Tour.EndZeit < DateTime.Now)
            //          {
            //              ctrTour.Tour.EndZeit = DateTime.Now.AddHours(3);
            //          }
            //          //StartZeit muss kleiner EndZeit sein
            //          if (ctrTour.Tour.StartZeit >= ctrTour.Tour.EndZeit)
            //          {
            //              ctrTour.Tour.EndZeit = ctrTour.Tour.StartZeit.AddHours(3);
            //          }
            //          ctrTour.Left = PointTime.GetPointFromTime(ctrTour.Tour.StartZeit);
            //          ctrTour.Width = PointTime.GetPointFromTime(ctrTour.Tour.EndZeit) - ctrTour.Left;
            //          ctrTour.Location = this.VehicelPanel.PointToClient(pKommi);
            //          ctrTour.Tour.KFZ_ZM = getFahrzeugCtrFromPoint(this.VehicelPanel.PointToClient(pKommi)).Fahrzeug.ID;        // ID Fahrzeug           
            //          this.VehicelPanel.Controls.Add(ctrTour);                
            //          SetTourPosition(ref ctrTour, false);
            //          ctrTour.BringToFront();

            //          //wenn eine KOmmission einer Tour zugewiesern werden soll aber den Gewichtscheck nicht besteht,
            //          //darf die Kommission nicht übernommen werden
            //          //Abfrage hier über DispoCheck.Disponieren
            //          if (ctrTour.DispoCheck.disponieren)
            //          {
            //              if (clsTour.ExistTourID(this.GL_User, ctrTour.Tour.ID))
            //              {
            //                  ctrTour.Tour.UpdateTourDaten();
            //              }
            //              else
            //              {
            //                  //Eintrag in DB Tour
            //                  ctrTour.Tour.AddToDB();
            //              }
            //              if (clsKommission.ExistKommission(this.GL_User, ctrTour.Kommission.ID))
            //              {
            //                  ctrTour.Kommission.BeladePos = ctrTour.Kommission.maxBeladePos;

            //                  ctrTour.Kommission.UpdateKommission();
            //              }
            //              else
            //              {
            //                  //Eintrag in DB Kommission
            //                  ctrTour.Kommission.TourID = ctrTour.Tour.ID;
            //                  ctrTour.Kommission.BeladePos = ctrTour.Kommission.maxBeladePos+1;
            //                  ctrTour.Kommission.EntladePos = ctrTour.Kommission.maxEntladePos+1;
            //                  ctrTour.Kommission.Add();
            //              }
            //              //Status disponiert setzen
            //              clsAuftragsstatus ap = new clsAuftragsstatus();
            //              ap.AP_ID = ctrTour.Kommission.AuftragPosTableID;
            //              ap.Status = 4; //disponiert
            //              ap.SetStatusDisposition();
            //              //ctrTour.Show();
            //              ((ctrAufträge)IDAndRowID.Receiverctr).RowUpdateFromDragDrop(IDAndRowID);

            //              //Tourberechnung der Strecke
            //              ctrTour.Tour.TourCalculation();

            //          }
            //          //Update DIspoCHeck
            //          if (ctrTour.DispoCheck.ExistDispoCheckID())
            //          {
            //              ctrTour.DispoCheck.UpdateDispoCheckbyID();
            //          }
            //          else
            //          {
            //              ctrTour.DispoCheck.TourID = ctrTour.Tour.ID;
            //              ctrTour.DispoCheck.Add();
            //          }
            //          RemoveALLKalenderItemTourByZM(ctrTour);
            //          //GetTourByZM(ctrTour.Tour.KFZ_ZM);

            //          //Leerkm löschen und neuladen
            //          RemoveALLKalenderItemLeeKMByZM(ctrTour.Tour.KFZ_ZM);
            //          //GetCtrLeerKMByZM(ctrTour.Tour.KFZ_ZM);
            //      }
            //      else
            //      {
            //        clsMessages.Disposition_AuftragNichtDisponierbar();
            //      }
            //    }
            //    //******************** Recource  ********************************************  
            //    if (e.Data.GetDataPresent(typeof(Globals._Recources)))
            //    {
            //      Globals._Recources RecourceGlobal = default(Globals._Recources);
            //      RecourceGlobal = (Globals._Recources)e.Data.GetData(typeof(Globals._Recources));

            //      AFKalenderItemRecource RecourceCtr = new AFKalenderItemRecource(this);
            //      pRecource.X = Cursor.Position.X; //PointTime.GetPointFromTime(RecourceCtr.Recource.TimeFrom);
            //      //pRecource.X = PointTime.GetPointFromTime(PointTime.GetTimeFromPoint(Cursor.Position.X));
            //      pRecource.Y = Cursor.Position.Y;
            //      RecourceCtr.Recource.TimeFrom = RecourceGlobal.TimeFrom;
            //      RecourceCtr.Recource.TimeTo = RecourceGlobal.TimeTo;

            //      if (RecourceCtr.Recource.TimeFrom != DateTime.Now)
            //      {
            //        RecourceCtr.Recource.TimeFrom = PointTime.GetTimeFromPoint(this.VehicelPanel.PointToClient(pRecource).X);
            //        //RecourceCtr.Recource.TimeTo=PointTime.GetTimeFromPoint(this.VehicelPanel.PointToClient(pRecource).X+RecourceCtr.Width);  //Endzeit unendlich          
            //      }
            //      //Unterscheidung für Auflieger / Faherer
            //      if (RecourceGlobal.RecourceTyp.ToString() == "F")    //Faherer
            //      {
            //        RecourceCtr.Name = "KalenderItemRecource_Fahrer";
            //        RecourceCtr.Recource.PersonalID = RecourceGlobal.PersonalID;
            //        RecourceCtr.Recource.Name = RecourceGlobal.Name;
            //        RecourceCtr.Recource.RecourceTyp = RecourceGlobal.RecourceTyp;

            //        /******************************************************************
            //         * 1. Check: ist die Resource (Fahrer, Auflieger) frei zur Verwendung? 
            //         * 2. Check: ob der ZM bereits eine Resource zugewiesen wurde
            //         * **************************************************************/
            //        if (clsResource.IsFahrerUsed(RecourceCtr.Recource.TimeFrom, RecourceCtr.Recource.TimeTo, RecourceGlobal.PersonalID, RecourceCtr.Recource.RecourceTyp, RecourceCtr.Recource.RecourceID))
            //        {
            //          RecourceIsUsed = true;
            //        }
            //        else
            //        {
            //          RecourceIsUsed = false;
            //        }
            //      }
            //      if (RecourceGlobal.RecourceTyp.ToString() == "A")    // Auflieger
            //      {
            //        RecourceCtr.Name = "KalenderItemRecource_Auflieger";
            //        RecourceCtr.Recource.VehicleID = RecourceGlobal.VehicleID;
            //        RecourceCtr.Recource.KFZ = RecourceGlobal.KFZ;
            //        RecourceCtr.Fahrzeug.FillData();
            //        RecourceCtr.Recource.RecourceTyp = RecourceGlobal.RecourceTyp;

            //       /******************************************************************
            //        * 1. Check: ist die Resource (Fahrer, Auflieger) frei zur Verwendung? 
            //        * 2. Check: ob der ZM bereits eine Resource zugewiesen wurde
            //        * **************************************************************/

            //        if (clsResource.IsTrailerUsed(RecourceCtr.Recource.TimeFrom, RecourceCtr.Recource.TimeTo, RecourceCtr.Recource.VehicleID, RecourceCtr.Recource.RecourceTyp, RecourceCtr.Recource.RecourceID))
            //        //if (clsResource.IsTrailerUsed(RecourceGlobal.TimeFrom, RecourceGlobal.TimeTo, RecourceGlobal.VehicleID, RecourceGlobal.RecourceTyp))
            //        {
            //          RecourceIsUsed = true;
            //        }
            //        else
            //        {
            //          RecourceIsUsed = false;
            //        }
            //      }

            //      // Wenn Recource nicht verwendet wird (RecourceIsUsed=false)
            //      if (!RecourceIsUsed)
            //      {
            //        RecourceCtr.Left = PointTime.GetPointFromTime(RecourceCtr.Recource.TimeFrom);
            //        RecourceCtr.Width = PointTime.GetPointFromTime(RecourceCtr.Recource.TimeTo) - RecourceCtr.Left;
            //        RecourceCtr.Location = this.VehicelPanel.PointToClient(Cursor.Position);


            //        //-- Zuweisung der Resource Auflieger/Faherer der ZM
            //        AFKalenderRow ZM = new AFKalenderRow();
            //        ZM = getFahrzeugCtrFromPoint(this.VehicelPanel.PointToClient(pRecource)); //original

            //        RecourceCtr.Recource.fRecEndTime = clsResource.GetFormerResscourceEndtDateTime(this.GL_User, RecourceCtr.Recource.TimeFrom, ZM.Fahrzeug.ID, true);
            //        RecourceCtr.Recource.nRecStartTime = clsResource.GetNextResscourceStartDateTime(this.GL_User, RecourceCtr.Recource.TimeFrom, ZM.Fahrzeug.ID, true);

            //        //Anfangszeitcheck 
            //        if (RecourceCtr.Recource.TimeFrom <= RecourceCtr.Recource.fRecEndTime)
            //        {
            //            RecourceCtr.Recource.TimeFrom = RecourceCtr.Recource.fRecEndTime;
            //        }
            //        //Endzeitcheck
            //        if (RecourceCtr.Recource.TimeTo >= RecourceCtr.Recource.nRecStartTime)
            //        {
            //            RecourceCtr.Recource.TimeTo = RecourceCtr.Recource.nRecStartTime;
            //        }


            //        //2. Check wie oben beschrieben
            //        if (clsResource.ZMRecourceIsUse(RecourceCtr.Recource.TimeFrom, RecourceCtr.Recource.TimeTo, ZM.Fahrzeug.ID, RecourceCtr.Recource.RecourceTyp, RecourceCtr.Recource.RecourceID))
            //        {
            //            ZM = null;
            //            clsMessages.Recource_TruckUsedTheRecource();
            //        }

            //        if (ZM != null)
            //        {
            //          clsResource RecourceCls = new clsResource();
            //          RecourceCls.m_i_VehicleID_Truck = ZM.Fahrzeug.ID;                       // ID der ZM
            //          RecourceCls.m_i_PersonalID = RecourceCtr.Recource.PersonalID;             // Faherer ID
            //          RecourceCls.m_i_VehicleID_Trailer = RecourceCtr.Recource.VehicleID;     // ID des Aufliegers
            //          RecourceCls.m_dt_TimeFrom = RecourceCtr.Recource.TimeFrom;
            //          RecourceCls.m_dt_TimeTo = RecourceCtr.Recource.TimeTo;
            //          RecourceCls.m_ch_RecourceTyp = Convert.ToChar(RecourceCtr.Recource.RecourceTyp);
            //          RecourceCls.m_str_Name = RecourceCtr.Recource.Name;

            //          //-- Eintrag in DB  ------
            //          RecourceCls.Insert_Truck();

            //          if (RecourceCls.m_ch_RecourceTyp.ToString() == "A")
            //          {
            //            RecourceCls.Insert_Trailer();
            //          }
            //          if (RecourceCls.m_ch_RecourceTyp.ToString() == "F")
            //          {
            //            RecourceCls.Insert_Fahrer();
            //          }

            //          SetRecourcePosition(ref RecourceCtr, false);  //Test
            //          this.VehicelPanel.Controls.Add(RecourceCtr);
            //          this.VehicelPanel.Controls.SetChildIndex(RecourceCtr, 0);
            //          RecourceCtr.BringToFront();
            //          RecourceCtr.Show();

            //          RessourcenRefresh(RecourceGlobal.RecourceTyp.ToString());
            //          }
            //      }
            //      else
            //      {
            //        clsMessages.Recource_IsUsed(); 
            //      }
            //    }
            //  }
            //  catch (Exception ex)
            //  {
            //    MessageBox.Show(ex.ToString());
            //  }
            //  //KalenderRefresh();

            //}
            //else
            //{
            //  clsMessages.User_NoAuthen();
            //}
        }
        ///<summary>frmDispo/ MenuDispoDataChanged</summary>
        ///<remarks></remarks>
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
        ///<summary>frmDispo/ FormatTime</summary>
        ///<remarks></remarks>
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
        ///<summary>frmDispo/ cbAufliegerListe_CheckedChanged</summary>
        ///<remarks></remarks>
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
        ///<summary>frmDispo/ UpdateGLUserInCtrMenue</summary>
        ///<remarks>läd und zeichnet das Panel neu</remarks>
        public void KalenderRefresh()
        {
            AllowedLoad = true;
            bool TrailerLoadZP = TrailerLoaded;
            bool FahrerLoadZP = FahrerLoaded;

            RemoveCtrFromDispoKalender();
            // KommisLoaded = false;  im Controll
            this.TimePanel.Refresh();
            ReloadAllKalenderRows();

            TrailerLoaded = TrailerLoadZP;
            FahrerLoaded = FahrerLoadZP;
            SetRecourceCheckboxen();
            AllowedLoad = false;
        }
        ///<summary>frmDispo/ RemoveCtrFromDispoKalender</summary>
        ///<remarks></remarks>
        private void ReloadAllKalenderRows()
        {
            foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find(Sped4.Controls.Kalender.ctrCalRow.const_ctrName, true))
            {
                ctr.GetTourByZM();
                ctr.GetCtrLeerKMByZM();
            }
        }
        ///<summary>frmDispo/ RemoveCtrFromDispoKalender</summary>
        ///<remarks></remarks>
        private void RemoveCtrFromDispoKalender()
        {
            foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find(Sped4.Controls.Kalender.ctrCalRow.const_ctrName, true))
            {
                ctr.RemoveCtrFromKalenderRow();
            }
        }
        ///<summary>frmDispo/ HideAllKalenderItemTour</summary>
        ///<remarks></remarks>
        private void HideAllKalenderItemTour()
        {
            foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find(Sped4.Controls.Kalender.ctrCalRow.const_ctrName, true))
            {
                ctr.HideAllTourItems();
            }
        }
        ///<summary>frmDispo/ ShowAllKalenderItemKommi</summary>
        ///<remarks></remarks>
        private void ShowAllKalenderItemKommi()
        {
            foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find(Sped4.Controls.Kalender.ctrCalRow.const_ctrName, true))
            {
                ctr.ShowAllTourItems();
            }
        }
        ///<summary>frmDispo/ ShowAllKalenderItemTour</summary>
        ///<remarks></remarks>
        private void ShowAllKalenderItemTour()
        {
            foreach (Sped4.Controls.Kalender.ctrCalRow ctr in this.VehicelPanel.Controls.Find(Sped4.Controls.Kalender.ctrCalRow.const_ctrName, true))
            {
                ctr.ShowAllTourItems();
            }
        }
        ///<summary>frmDispo/ cbFahrerliste_CheckedChanged</summary>
        ///<remarks></remarks>
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
        ///<summary>frmDispo/ ShowKalenderItemRecourceFahrer</summary>
        ///<remarks></remarks>
        private void ShowKalenderItemRecourceFahrer()
        {
            foreach (AFKalenderItemRecource ctr2 in this.VehicelPanel.Controls.Find("KalenderItemRecource_Fahrer", true))
            {
                ctr2.Show();
            }
        }
        ///<summary>frmDispo/ HideKalenderItemRecourceFahrer</summary>
        ///<remarks></remarks>
        private void HideKalenderItemRecourceFahrer()
        {
            foreach (AFKalenderItemRecource ctr2 in this.VehicelPanel.Controls.Find("KalenderItemRecource_Fahrer", true))
            {
                ctr2.Hide();
            }
        }
        ///<summary>frmDispo/ tsbClose_Click</summary>
        ///<remarks></remarks>
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
            this.Close();
        }
        ///<summary>frmDispo/ tsbRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            KalenderRefresh();
        }
        ///<summary>frmDispo/ SetAufliegerChecked</summary>
        ///<remarks></remarks>
        public void SetAufliegerChecked()
        {
            KalenderRefresh();
            cbAuflieger.Checked = true;
        }
        ///<summary>frmDispo/ SetFahrerChecked</summary>
        ///<remarks></remarks>
        public void SetFahrerChecked()
        {
            KalenderRefresh();
            cbFahrer.Checked = true;
        }
        ///<summary>frmDispo/ frmDispoKalender_SizeChanged</summary>
        ///<remarks></remarks>
        private void frmDispoKalender_SizeChanged(object sender, EventArgs e)
        {
            if (KommisLoaded)
            {
                this.KalenderRefresh();
            }
        }
        ///<summary>frmDispo/ pbRightVor_MouseDown</summary>
        ///<remarks>Tool Tips vor/zurück Button</remarks>
        private void pbRightVor_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            strInfo = "Endzeitpunkt +1 Tag";
            info.SetToolTip(this.pbRightVor, strInfo);
        }
        ///<summary>frmDispo/ pbRightZur_MouseDown</summary>
        ///<remarks>Tool Tips vor/zurück Button</remarks>
        private void pbRightZur_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            strInfo = "Endzeitpunkt -1 Tag";
            info.SetToolTip(this.pbRightZur, strInfo);
        }
        ///<summary>frmDispo/ pbLeftVor_MouseDown</summary>
        ///<remarks>Tool Tips vor/zurück Button</remarks>
        private void pbLeftVor_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            strInfo = "Startzeitpunkt +1 Tag";
            info.SetToolTip(this.pbLeftVor, strInfo);
        }
        ///<summary>frmDispo/ pbLeftZur_MouseDown</summary>
        ///<remarks>Tool Tips vor/zurück Button</remarks>
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
        ///<summary>frmDispo/ TimePanel_MouseDown</summary>
        ///<remarks></remarks>
        private void TimePanel_MouseDown(object sender, MouseEventArgs e)
        {
            xMove = true;
            oldPos = e.Location;
        }
        ///<summary>frmDispo/ TimePanel_MouseMove</summary>
        ///<remarks></remarks>
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
        ///<summary>frmDispo/ SetDispoZeitraum</summary>
        ///<remarks></remarks>
        private void SetDispoZeitraum(Int32 PosDiff)
        {
            DateTime newStart;
            DateTime newEnd;
            TimeSpan DiffTage = new TimeSpan();
            //DiffTage = DateTo.Subtract(DateFrom);

            DiffTage = GL_User.us_dtDispoBis.Subtract(GL_User.us_dtDispoVon);

            if (PosDiff > 0)
            {
                newStart = GL_User.us_dtDispoBis;
                newEnd = GL_User.us_dtDispoBis.Add(DiffTage);
                GL_User.us_dtDispoVon = newStart;
                GL_User.us_dtDispoBis = newEnd;
            }
            if (PosDiff < 0)
            {
                newEnd = GL_User.us_dtDispoVon;
                newStart = GL_User.us_dtDispoVon.Subtract(DiffTage);
                GL_User.us_dtDispoVon = newStart;
                GL_User.us_dtDispoBis = newEnd;
            }
            KalenderRefresh();
        }
        ///<summary>frmDispo/ pictureBox1_Click</summary>
        ///<remarks>je klick einen Tag zurück</remarks>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Int32 TageZurück = 1;
            ChangeDateInDispoPlan((-1) * TageZurück, 0);
        }
        ///<summary>frmDispo/ pbLeftZur_Click</summary>
        ///<remarks></remarks>
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
        ///<summary>frmDispo/ pictureBox2_Click</summary>
        ///<remarks></remarks>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Int32 TagVor = 1;
            ChangeDateInDispoPlan(0, TagVor);
        }
        ///<summary>frmDispo/ pbRightZur_Click</summary>
        ///<remarks></remarks>
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
        ///<summary>frmDispo/ ChangeDateInDispoPlan</summary>
        ///<remarks></remarks>
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
            this.DispoKalender.SetSelectionRange(GL_User.us_dtDispoVon, GL_User.us_dtDispoBis);
        }
        ///<summary>frmDispo/ GetDispoDateDiff</summary>
        ///<remarks></remarks>
        private TimeSpan GetDispoDateDiff()
        {
            GL_User.us_dtDispoVon = GL_User.us_dtDispoVon.Date;
            GL_User.us_dtDispoBis = GL_User.us_dtDispoBis.Date;
            TimeSpan diff = GL_User.us_dtDispoBis.Subtract(GL_User.us_dtDispoVon);
            return diff;
        }
        ///<summary>frmDispo/ DispoKalender_DateChanged</summary>
        ///<remarks></remarks>
        private void DispoKalender_DateChanged(object sender, DateRangeEventArgs e)
        {
            GL_User.us_dtDispoVon = DispoKalender.SelectionRange.Start;
            //Für die Anzeige im VehiclePanel wieder einen Tag drauf
            GL_User.us_dtDispoBis = DispoKalender.SelectionRange.End;
            KalenderRefresh();
            UpdateGLUserInCtrMenue();
        }
        ///<summary>frmDispo/ UpdateGLUserInCtrMenue</summary>
        ///<remarks></remarks>
        private void UpdateGLUserInCtrMenue()
        {
            this.AuftragCtr._ctrMenu.GL_User = GL_User;
        }
        ///<summary>frmDispo/ label2_Click</summary>
        ///<remarks>Info Schadstoffklassen ein-/ausblenden</remarks>
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
        ///<summary>frmDispo/ label2_Click</summary>
        ///<remarks>IDispokalender Ansicht integrieren</remarks>
        private void tsbtnDispoPlanIntegrieren_Click(object sender, EventArgs e)
        {
            if (this.menue != null)
            {
                this.menue.GL_User = this.GL_User;
                this.Close();
                this.menue.DispoOpenSplitter();
            }
        }
        ///<summary>frmDispo/ label2_Click</summary>
        ///<remarks> DIspokalender in eigenem WIndow</remarks>
        private void tsbtnDispoplanFenster_Click(object sender, EventArgs e)
        {
            if (this.menue != null)
            {
                this.menue.GL_User = this.GL_User;
                this.menue.CloseDispoKaledner();
                this.menue.DispoKalenderOpen();
            }
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (Sped4.Controls.Kalender.ctrCalRow ctr2 in this.VehicelPanel.Controls.Find(Sped4.Controls.Kalender.ctrCalRow.const_ctrName, true))
            {
                ctr2.ShowAllTourItems();
            }
        }







    }
}
