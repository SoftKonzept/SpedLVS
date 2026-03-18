using System;
using System.Data;
using System.Data.SqlClient;

namespace LVS
{
    public class clsDispoCheck
    {
        public Globals._GL_USER _GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }
        //************************************#

        private decimal _ID;
        private decimal _AuftragID;
        private decimal _AuftragPos;
        private decimal _AuftragPosTableID;
        private decimal _ZM_ID;   //Zugmaschine
        private decimal _A_ID;    //Auflieger
        private decimal _P_ID;    //Personal
        private decimal _TourID;
        private decimal _maxGewicht;//maxLadungsgewicht=maxGesamtgewicht-Leergewicht ZM - Leergewicht Auflieger
        private decimal _TourGewicht;
        private decimal _LGZM;      //Leergewicht ZM
        private decimal _LGA;       // Leergewicht Auftlieger
        private decimal _zlGG;      // max Gesamtgewicht
        private bool _disponieren;
        private bool _GewichtZuHoch;
        private bool _GewichtFreigabe;
        private decimal _TourGewichtGesamt;
        private decimal _gemGewicht;
        private decimal _Brutto;
        private DateTime _oldStartZeit;
        private DateTime _oldEndZeit;
        private decimal _oldZM;
        private Int32 _KommiAnzahlTour;
        private bool _init;
        private Int32 _iInit;
        private decimal _iBackToOldZM;
        private Int32 _iGewichtFreigabe;
        private bool _bo_BackToOldZM;
        private bool _bo_RessourcenCheckOK;

        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public bool bo_RessourcenCheckOK
        {
            get { return _bo_RessourcenCheckOK; }
            set { _bo_RessourcenCheckOK = value; }
        }
        public decimal AuftragID
        {
            get { return _AuftragID; }
            set { _AuftragID = value; }
        }
        public decimal AuftragPosTableID
        {
            get { return _AuftragPosTableID; }
            set { _AuftragPosTableID = value; }
        }
        public decimal AuftragPos
        {
            get { return _AuftragPos; }
            set { _AuftragPos = value; }
        }
        public decimal ZM_ID
        {
            get { return _ZM_ID; }
            set { _ZM_ID = value; }
        }
        public decimal A_ID
        {
            get { return _A_ID; }
            set { _A_ID = value; }
        }

        public decimal P_ID
        {
            get { return _P_ID; }
            set { _P_ID = value; }
        }
        public decimal maxGewicht
        {
            get
            {
                _maxGewicht = zlGG - LGA;
                return _maxGewicht;
            }
            set { _maxGewicht = value; }
        }
        public decimal TourID
        {
            get { return _TourID; }
            set { _TourID = value; }
        }
        public decimal TourGewicht
        {
            get
            {
                if (clsTour.ExistTourID(this._GL_User, TourID))
                {
                    string strSQL = string.Empty;
                    strSQL = "SELECT SUM(a.Brutto) as Gewicht " +
                                                  "FROM Artikel a " +
                                                  "INNER JOIN AuftragPos d ON d.ID = a.AuftragPosTableID " +
                                                  "INNER JOIN Kommission b ON b.PosID = d.ID " +
                                                  "INNER JOIN Tour c ON c.ID = b.TourID " +
                                                  "WHERE c.ID='" + TourID + "' ";
                    string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                    decimal decTmp = 0;
                    Decimal.TryParse(strTmp, out decTmp);
                    _TourGewicht = decTmp;
                }
                else
                {
                    _TourGewicht = 0;
                }
                return _TourGewicht;
            }
            set { _TourGewicht = value; }
        }
        public decimal gemGewicht
        {
            get { return _gemGewicht; }
            set { _gemGewicht = value; }
        }

        public decimal LGZM
        {
            get { return _LGZM; }
            set { _LGZM = value; }
        }
        public decimal LGA
        {
            get { return _LGA; }
            set { _LGA = value; }
        }
        public decimal zlGG
        {
            get { return _zlGG; }
            set { _zlGG = value; }
        }
        public bool disponieren
        {
            get { return _disponieren; }
            set { _disponieren = value; }
        }
        public bool GewichtZuHoch
        {
            get { return _GewichtZuHoch; }
            set { _GewichtZuHoch = value; }
        }
        public bool GewichtFreigabe
        {
            get { return _GewichtFreigabe; }
            set { _GewichtFreigabe = value; }
        }

        public DateTime oldStartZeit
        {
            get
            {
                if (_oldStartZeit < DateTime.MinValue)
                {
                    _oldStartZeit = DateTime.MinValue;
                }
                return _oldStartZeit;
            }
            set { _oldStartZeit = value; }
        }
        public DateTime oldEndZeit
        {
            get { return _oldEndZeit; }
            set { _oldEndZeit = value; }
        }
        public Int32 KommiAnzahlTour
        {
            get { return _KommiAnzahlTour; }
            set { _KommiAnzahlTour = value; }
        }
        public decimal oldZM
        {
            get { return _oldZM; }
            set { _oldZM = value; }
        }
        public bool init
        {
            get { return _init; }
            set { _init = value; }
        }

        public decimal iBackToOldZM
        {
            get
            {
                _iBackToOldZM = 0;
                if (bo_BackToOldZM == true)
                {
                    _iBackToOldZM = 1;
                }

                return _iBackToOldZM;
            }
            set { _iBackToOldZM = value; }
        }
        public Int32 iGewichtFreigabe
        {
            get
            {
                _iGewichtFreigabe = 0;
                if (GewichtFreigabe == true)
                {
                    _iGewichtFreigabe = 1;
                }

                return _iGewichtFreigabe;
            }
            set { _iGewichtFreigabe = value; }
        }
        public Int32 iInit
        {
            get
            {
                _iInit = 0;
                if (init == true)
                {
                    _iInit = 1;
                }

                return _iInit;
            }
            set { _iInit = value; }
        }
        public bool bo_BackToOldZM
        {
            get { return _bo_BackToOldZM; }
            set { _bo_BackToOldZM = value; }
        }


        public bool bFirstLoad = true;

        ///************************************************************************************************
        // *                                          Methoden
        // * *********************************************************************************************/
        /////<summary>clsDispoCheck / FillData</summary>
        /////<remarks>Initialisiert den DispoCheck.</remarks>
        //public Sped4.Controls.AFKalenderItemTour FillData(Sped4.Controls.AFKalenderItemTour ctrTour)
        //{
        //    SetDataForDispoCheck(ref ctrTour);
        //    //Check Resourcen
        //    GetAndCheckRecourcen(ref ctrTour);         
        //    if (init)
        //    {
        //        //Es wird eine neue Tour erstellt und Disponiert oder es wird ein Kommission einer bestehenden
        //        //Tour zugwiesen
        //        DispoGewichtsCheck(ref ctrTour);
        //        //Insert wenn dispo=true
        //        if (disponieren)
        //        {
        //            init = false;
        //        }
        //    }
        //    else
        //    {
        //        LoadDispoCheckDaten(ref ctrTour); 
        //        if (ctrTour.DispoCheck.ZM_ID == ctrTour.DispoCheck.oldZM)
        //        {
        //            //Die Positionen bestehender und unveränderter Touren wird verändern
        //            //entweder auf dem selben Fahrzeug oder einem neuen Fahrzeug zugewiesen
        //            ctrTour.DispoCheck.disponieren = true;
        //        }            
        //        else
        //        {
        //            //Tour wird einem anderen Fahrzeug zugewiesen
        //            ctrTour.DispoCheck.GewichtFreigabe = false;
        //            //Gesamtgewicht
        //            //disponieren ja oder nein wird in der folgenden Funktion gesetzt
        //            DispoGewichtsCheck(ref ctrTour);            
        //        }
        //    }
        //    return ctrTour;
        //}
        /////<summary>clsDispoCheck / GetAndCheckRecourcen</summary>
        /////<remarks>Ermittel die entsprechenden Resourcen für den Zeitraum.</remarks>
        //private void GetAndCheckRecourcen(ref Sped4.Controls.AFKalenderItemTour ctrTour)
        //{
        //    DataSet ds1 = clsResource.GetUsedTruckRecource(this._GL_User, ctrTour.Tour.StartZeit, ctrTour.Tour.EndZeit, ctrTour.Tour.KFZ_ZM);
        //    if (ds1.Tables[0].Rows.Count > 0)
        //    {
        //        for (Int32 i = 0; i < ds1.Tables[0].Rows.Count; i++)
        //        {
        //            string typ = ds1.Tables[0].Rows[i]["RecourceTyp"].ToString();
        //            if (typ == "A")
        //            {
        //                if (ds1.Tables[0].Rows[i]["VehicleID"] != DBNull.Value)
        //                {
        //                    ctrTour.DispoCheck.A_ID = (decimal)ds1.Tables[0].Rows[i]["VehicleID"];
        //                }
        //                if (ds1.Tables[0].Rows[i]["LG_A"] != DBNull.Value)
        //                {
        //                    ctrTour.DispoCheck.LGA = Convert.ToDecimal(ds1.Tables[0].Rows[i]["LG_A"]);
        //                }
        //                if (ds1.Tables[0].Rows[i]["zlGG"] != DBNull.Value)
        //                {
        //                    ctrTour.DispoCheck.zlGG = Convert.ToDecimal(ds1.Tables[0].Rows[i]["zlGG"]);
        //                }
        //            }
        //            if (typ == "Z")
        //            {
        //                if (ds1.Tables[0].Rows[i]["VehicleID"] != DBNull.Value)
        //                {
        //                    ctrTour.DispoCheck.ZM_ID = (decimal)ds1.Tables[0].Rows[i]["VehicleID"];
        //                }
        //                if (ds1.Tables[0].Rows[i]["LG_ZM"] != DBNull.Value)
        //                {
        //                    ctrTour.DispoCheck.LGZM = Convert.ToDecimal(ds1.Tables[0].Rows[i]["LG_ZM"]);
        //                }
        //                if (ds1.Tables[0].Rows[i]["zlGG"] != DBNull.Value)
        //                {
        //                    ctrTour.DispoCheck.zlGG = Convert.ToDecimal(ds1.Tables[0].Rows[i]["zlGG"]);
        //                }
        //            }
        //            if (typ == "F")
        //            {
        //                if (ds1.Tables[0].Rows[i]["PersonalID"] != DBNull.Value)
        //                {
        //                    ctrTour.DispoCheck.P_ID = (decimal)ds1.Tables[0].Rows[i]["PersonalID"];
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ctrTour.DispoCheck.A_ID = 0;
        //        ctrTour.DispoCheck.P_ID = 0;
        //        ctrTour.DispoCheck.zlGG = 0;
        //        ctrTour.DispoCheck.LGZM = 0;
        //        ctrTour.DispoCheck.LGA = 0;
        //    }
        //    CheckRecource(ref ctrTour);
        //}
        /////<summary>clsDispoCheck / CheckRecource</summary>
        /////<remarks>Check, ob die Resourcen (Auflieger, Fahrer) der Zugmaschine zugewiesen sind.</remarks>
        //private void CheckRecource(ref Sped4.Controls.AFKalenderItemTour ctrTour)
        //{
        //    ctrTour.DispoCheck.disponieren = true;
        //    //Check und notfalls disponieren auf false
        //    string strFehler = string.Empty;
        //    if (ctrTour.DispoCheck.A_ID == 0)
        //    {
        //        ctrTour.DispoCheck.disponieren = false;
        //        strFehler = strFehler + "\r\n" + "Ressource Auflieger\r\n";
        //    }
        //    if (ctrTour.DispoCheck.P_ID == 0)
        //    {
        //        ctrTour.DispoCheck.disponieren = false;
        //        strFehler = strFehler + "Ressource Fahrer\r\n";
        //    }
        //    if (ctrTour.DispoCheck.disponieren == false)
        //    {
        //        clsMessages.Disposition_RecourcenFehlen(strFehler);
        //        ctrTour.DispoCheck.disponieren = false;
        //        ctrTour.DispoCheck.bo_RessourcenCheckOK = false;
        //    }
        //}
        /////<summary>clsDispoCheck / DispoGewichtsCheck</summary>
        /////<remarks>Check auf Überladung.</remarks>
        //private void DispoGewichtsCheck(ref Sped4.Controls.AFKalenderItemTour ctrTour)
        //{
        //    //Gesamtes Tour Gewicht wird ermittelt
        //    ctrTour.DispoCheck.TourGewicht = TourGewicht;
        //    //das maximal zul. Ladungsgewicht wird ermittelt
        //    ctrTour.DispoCheck.maxGewicht = maxGewicht;

        //    //Der Gewichtsvergleich kann hier durchgeführt werden, da schon alle einzelnen Kommissionsgewicht,
        //    //auch der neu zugewiesenen berücksichtigt sind, da diese auch schon aus der Datenbank kommen.
        //    if ((ctrTour.DispoCheck.TourGewicht + ctrTour.Kommission.Brutto) > ctrTour.DispoCheck.maxGewicht)
        //    {
        //        ctrTour.DispoCheck.GewichtZuHoch = true;
        //        if (clsMessages.Disposition_DragDropGewichtsCheck(Functions.FormatDecimal(TourGewicht + ctrTour.Kommission.Brutto), Functions.FormatDecimal(maxGewicht)))
        //        {
        //            ctrTour.DispoCheck.GewichtFreigabe = true;
        //            SetOKGewichtTrue();
        //            ctrTour.DispoCheck.disponieren = true;
        //        }
        //        else
        //        {
        //            ctrTour.DispoCheck.GewichtFreigabe = false;
        //            SetOKGewichtFalse();
        //            ctrTour.DispoCheck.disponieren = false;
        //        }
        //    }
        //    else
        //    {
        //        ctrTour.DispoCheck.GewichtFreigabe = true;
        //        SetOKGewichtTrue();
        //        ctrTour.DispoCheck.disponieren = true;
        //    }
        //}
        /////<summary>clsDispoCheck / LoadDispoCheckDaten</summary>
        /////<remarks>Ermittelt die DispoCheckdaten für die entsprechende TourID.</remarks>
        //private void LoadDispoCheckDaten(ref Sped4.Controls.AFKalenderItemTour ctrTour)
        //{
        //    string strSQL = string.Empty;
        //    DataTable dt = new DataTable();
        //    if (clsTour.ExistTourID(this._GL_User, ctrTour.Tour.ID))
        //    {
        //        strSQL = "SELECT * FROM DispoCheck WHERE TourID='" + ctrTour.Tour.ID + "'";
        //        if (!ctrTour.Kommission.bDragDrop)
        //        {
        //            init = false;
        //        }
        //    }
        //    else
        //    {
        //        init = true;
        //    }

        //    if (strSQL != string.Empty)
        //    {
        //        dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "DispoCheck");

        //        if (dt.Rows.Count == 0)
        //        {
        //            init = true;
        //        }
        //        else
        //        {
        //            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        //            {
        //                ctrTour.DispoCheck.ID = (decimal)dt.Rows[i]["ID"];
        //                ctrTour.DispoCheck.oldZM = (decimal)dt.Rows[i]["oldZM"];
        //                ctrTour.DispoCheck.oldStartZeit = (DateTime)dt.Rows[i]["oldStartZeit"];
        //                ctrTour.DispoCheck.oldEndZeit = (DateTime)dt.Rows[i]["oldEndZeit"];
        //                ctrTour.DispoCheck.GewichtFreigabe = (bool)dt.Rows[i]["OKGewicht"];
        //                ctrTour.DispoCheck.bo_BackToOldZM = (bool)dt.Rows[i]["BackToOldZM"];
        //                ctrTour.DispoCheck.init = (bool)dt.Rows[i]["NeuDisponiert"];
        //            }
        //        }
        //    }
        //}
        ///<summary>clsDispoCheck / Add</summary>
        ///<remarks>Fügt neue DispoCheck-Daten einer Tour zur Datenbank hinzu.</remarks>
        public void Add()
        {
            try
            {
                string strSQL = string.Empty;
                strSQL = "INSERT INTO DispoCheck (TourID, oldZM, oldStartZeit, oldEndZeit, BackToOldZM, NeuDisponiert) " +
                                              "VALUES ('" + TourID.ToString().Replace(",", ".") + "', '"
                                                          + oldZM.ToString().Replace(",", ".") + "','"
                                                          + oldStartZeit + "','"
                                                          + oldEndZeit + "','"
                                                          + Convert.ToInt32(bo_BackToOldZM) + "','"
                                                          + Convert.ToInt32(init) + "')";
                strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                decimal decTmp = 0;
                decimal.TryParse(strTmp, out decTmp);
                ID = decTmp;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        ///<summary>clsDispoCheck / UpdateDispoCheckbyID</summary>
        ///<remarks>Update der DispoCheck-Daten.</remarks>
        public void UpdateDispoCheckbyID()
        {
            if (ID > 0)
            {
                string strSQL = string.Empty;
                strSQL = "Update DispoCheck SET TourID ='" + TourID.ToString().Replace(",", ".") + "', " +
                                             "oldZM='" + oldZM.ToString().Replace(",", ".") + "', " +
                                             "oldStartZeit ='" + oldStartZeit + "', " +
                                             "oldEndZeit='" + oldEndZeit + "', " +
                                             "OKGewicht='" + Convert.ToInt32(GewichtFreigabe) + "', " +
                                             "BackToOldZM='" + Convert.ToInt32(bo_BackToOldZM) + "', " +
                                             "NeuDisponiert='" + Convert.ToInt32(init) + "' " +
                                             "WHERE ID='" + ID + "'";

                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
        }
        ///<summary>clsDispoCheck / UpdateDispoCheckbyTourID</summary>
        ///<remarks>Update der DispoCheck-Daten.</remarks>
        public void UpdateDispoCheckbyTourID()
        {
            if (clsTour.ExistTourID(this._GL_User, TourID))
            {
                string strSQL = string.Empty;
                strSQL = "Update DispoCheck SET TourID ='" + TourID.ToString().Replace(",", ".") + "', " +
                                             "oldZM='" + oldZM.ToString().Replace(",", ".") + "', " +
                                             "oldStartZeit ='" + oldStartZeit + "', " +
                                             "oldEndZeit='" + oldEndZeit + "', " +
                                             "OKGewicht='" + Convert.ToInt32(GewichtFreigabe) + "', " +
                                             "BackToOldZM='" + Convert.ToInt32(bo_BackToOldZM) + "', " +
                                             "NeuDisponiert='" + Convert.ToInt32(init) + "' " +
                                             "WHERE TourID='" + TourID + "'";

                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
        }
        /////<summary>clsDispoCheck / SetDataForDispoCheck</summary>
        /////<remarks>Setzen der DispoCheck-Daten.</remarks>
        //public void SetDataForDispoCheck(ref Sped4.Controls.AFKalenderItemTour ctrTour)
        //{
        //    ctrTour.DispoCheck.TourID = ctrTour.Tour.ID;
        //    ctrTour.DispoCheck.oldZM = ctrTour.Tour.KFZ_ZM;
        //    ctrTour.DispoCheck.oldStartZeit = ctrTour.Tour.StartZeit;
        //    ctrTour.DispoCheck.oldEndZeit = ctrTour.Tour.EndZeit;
        //    ctrTour.DispoCheck.bo_BackToOldZM = false;
        //}
        ///<summary>clsDispoCheck / ExistTourInDispoCheck</summary>
        ///<remarks>Prüft, ob die Tour in der Table DispoCheck bereits existiert.</remarks>
        private bool ExistTourInDispoCheck()
        {
            string strSQL = string.Empty;
            strSQL = "SELECT ID From DispoCheck WHERE TourID ='" + TourID + "' ";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
        }
        ///<summary>clsDispoCheck / ExistTourInDispoCheck</summary>
        ///<remarks>Prüft, ob die Tour in der Table DispoCheck bereits existiert.</remarks>
        public bool ExistDispoCheckID()
        {
            string strSQL = string.Empty;
            strSQL = "Select ID FROM DispoCheck WHERE ID ='" + ID + "' ";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
        }
        //
        //
        //
        //private void SetInterneVariablen(ref Sped4.Controls.AFKalenderItemKommi KommiCtr)
        //{
        //  AuftragID = KommiCtr.Kommission.AuftragID;
        //  AuftragPos = KommiCtr.Kommission.AuftragPos;

        //  /****
        //  if (KommiCtr.Kommission.oldZM == KommiCtr.Kommission.KFZ_ZM)
        //  {
        //    oldZM = KommiCtr.Kommission.oldZM;
        //  }
        //  else
        //  {
        //    if (KommiCtr.DispoCheck.disponieren)
        //    {
        //      oldZM = KommiCtr.Kommission.KFZ_ZM;
        //    }
        //    else
        //    {
        //      if (KommiCtr.DispoCheck.bo_RessourcenCheckOK)
        //      {
        //        oldZM = KommiCtr.Kommission.KFZ_ZM;
        //      }
        //      else
        //      {
        //        // keine Resscourcen vorhanden, dann muss die Kommission zurück zum alten Fahrzeug
        //        oldZM = KommiCtr.DispoCheck.oldZM;
        //      }
        //      //oldZM = KommiCtr.Kommission.oldZM;
        //      //oldZM = KommiCtr.Kommission.KFZ_ZM;
        //    }
        //  }
        //    ***/
        //  //oldBeladezeit = KommiCtr.DispoCheck.oldBeladezeit;
        //  //oldEntladezeit = KommiCtr.DispoCheck.oldEntladezeit;

        //  bo_BackToOldZM = KommiCtr.DispoCheck.bo_BackToOldZM;
        //  disponieren = KommiCtr.DispoCheck.disponieren;
        //  GewichtFreigabe = KommiCtr.DispoCheck.GewichtFreigabe;
        //  AuftragPosTableID = KommiCtr.Kommission.AuftragPos_ID;
        //  if (KommiCtr.DispoCheck.ID == 0)
        //  {
        //    KommiCtr.DispoCheck.ID = GetID();
        //  }
        //  ID = KommiCtr.DispoCheck.ID;
        //}
        //
        //
        //

        //
        //-- DB insert --------

        //
        //
        //

        //
        //---------- Kommi ID bereits vorhanden? ---- 
        //

        //
        //
        private bool GetPermissionForWeightOVerload()
        {
            string strSQL = string.Empty;
            strSQL = "Select OKGewicht FROM DispoCheck WHERE ID ='" + ID + "' ";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
        }
        //
        public decimal GetID()
        {
            decimal Check_ID = 0;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            //Command.CommandText = "SELECT ID From DispoCheck WHERE AuftragID ='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
            Command.CommandText = "SELECT ID From DispoCheck WHERE AuftragPosTableID ='" + AuftragPosTableID + "'";
            Globals.SQLcon.Open();
            if (Command.ExecuteScalar() == null)
            {
                Check_ID = 0;
            }
            else
            {
                Check_ID = (decimal)Command.ExecuteScalar();
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return Check_ID;
        }
        //
        //----------- Delete / Löschen der Recource vom TimePanel  -------------------
        //
        public void DeleteDispoCheck()
        {
            if (ExistTourInDispoCheck())
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                //Command.CommandText = "DELETE FROM DispoCheck WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
                Command.CommandText = "DELETE FROM DispoCheck WHERE ID ='" + ID + "'";
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
        }
        //
        //----------- OK - disposition erhöhtes Gewicht  -------------------
        //
        public void SetOKGewichtTrue()
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Update DispoCheck SET OKGewicht='1' WHERE ID='" + ID + "'";

            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        public void SetOKGewichtFalse()
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Update DispoCheck SET OKGewicht='0' WHERE ID='" + ID + "'";

            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        //
        //
        public void SetDispoCheckInit()
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            if (init)
            {
                Command.CommandText = "Update DispoCheck SET NeuDisponiert='1' WHERE ID='" + ID + "'";
            }
            else
            {
                Command.CommandText = "Update DispoCheck SET NeuDisponiert='0' WHERE ID='" + ID + "'";
            }
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        //
        //
        public void SetBackToOldZM(bool Back)
        {
            Int32 iBit = 0;
            if (Back)
            {
                iBit = 1;
            }
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            //Command.CommandText = "Update DispoCheck SET BackToOldZM='"+iBit+"' WHERE KommiID='" + KommiID + "'";

            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //--------Beim Refresh des Dispoplans werden alle Daten noch einmal upgedatet-----------
        //------ dadurch wir nur noch die Abfrage bei erhöhtem Gewicht nur noch einmal ---------
        //
        //public Sped4.Controls.AFKalenderItemKommi UpdateForRefresh(Sped4.Controls.AFKalenderItemKommi KommiCtr)
        //{
        //    /****
        //    KommiCtr.DispoCheck.AuftragID = KommiCtr.Kommission.AuftragID;
        //    KommiCtr.DispoCheck.AuftragPos = KommiCtr.Kommission.AuftragPos;
        //    KommiCtr.DispoCheck.oldZM = KommiCtr.Kommission.KFZ_ZM;
        //    KommiCtr.DispoCheck.oldBeladezeit = KommiCtr.Kommission.BeladeZeit;
        //    KommiCtr.DispoCheck.oldEntladezeit = KommiCtr.Kommission.EntladeZeit;
        //    KommiCtr.DispoCheck.KommiID = KommiCtr.Kommission.ID;
        //    KommiCtr.DispoCheck.ID=GetID();
        //    KommiCtr.DispoCheck.bo_BackToOldZM = KommiCtr.DispoCheck.bo_BackToOldZM;
        //    UpdateDispoCheckbyID();

        //    if (KommiCtr.DispoCheck.GewichtFreigabe)
        //    {
        //        SetOKGewichtTrue();
        //    }
        //    else
        //    {
        //        SetOKGewichtFalse();
        //    }
        //     * ***/
        //    return KommiCtr;
        //}

        /*******************************************************************************************************
         *                        Get Daten - Check - Auswertung 
         ******************************************************************************************************/



        //
        //--------- Gewichtscheck  -------------
        //


    }
}




