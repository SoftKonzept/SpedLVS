using LVS;
using Sped4.Classes;
using System;
using System.Data;

namespace Sped4.Controls
{
    class AFMethoden
    {
        ///<summary>clsDispoCheck / FillData</summary>
        ///<remarks>Initialisiert den DispoCheck.</remarks>
        public Sped4.Controls.AFKalenderItemTour FillData(Sped4.Controls.AFKalenderItemTour ctrTour, ref clsDispoCheck dispoCheck)
        {
            SetDataForDispoCheck(ref ctrTour, ref dispoCheck);
            //Check Resourcen
            GetAndCheckRecourcen(ref ctrTour, dispoCheck._GL_User);
            if (dispoCheck.init)
            {
                //Es wird eine neue Tour erstellt und Disponiert oder es wird ein Kommission einer bestehenden
                //Tour zugwiesen
                DispoGewichtsCheck(ref ctrTour, ref dispoCheck);
                //Insert wenn dispo=true
                if (dispoCheck.disponieren)
                {
                    dispoCheck.init = false;
                }
            }
            else
            {
                LoadDispoCheckDaten(ref ctrTour, ref dispoCheck);
                if (ctrTour.DispoCheck.ZM_ID == ctrTour.DispoCheck.oldZM)
                {
                    //Die Positionen bestehender und unveränderter Touren wird verändern
                    //entweder auf dem selben Fahrzeug oder einem neuen Fahrzeug zugewiesen
                    ctrTour.DispoCheck.disponieren = true;
                }
                else
                {
                    //Tour wird einem anderen Fahrzeug zugewiesen
                    ctrTour.DispoCheck.GewichtFreigabe = false;
                    //Gesamtgewicht
                    //disponieren ja oder nein wird in der folgenden Funktion gesetzt
                    DispoGewichtsCheck(ref ctrTour, ref dispoCheck);
                }
            }
            return ctrTour;
        }

        ///<summary>clsDispoCheck / FillData</summary>
        ///<remarks>Initialisiert den DispoCheck.</remarks>
        public Sped4.Controls.ctrTourItem FillData1(Sped4.Controls.ctrTourItem ctrTour, ref clsDispoCheck dispoCheck)
        {
            SetDataForDispoCheck1(ref ctrTour, ref dispoCheck);
            //Check Resourcen
            GetAndCheckRecourcen1(ref ctrTour, dispoCheck._GL_User);
            if (dispoCheck.init)
            {
                //Es wird eine neue Tour erstellt und Disponiert oder es wird ein Kommission einer bestehenden
                //Tour zugwiesen
                DispoGewichtsCheck1(ref ctrTour, ref dispoCheck);
                //Insert wenn dispo=true
                if (dispoCheck.disponieren)
                {
                    dispoCheck.init = false;
                }
            }
            else
            {
                LoadDispoCheckDaten1(ref ctrTour, ref dispoCheck);
                if (ctrTour.DispoCheck.ZM_ID == ctrTour.DispoCheck.oldZM)
                {
                    //Die Positionen bestehender und unveränderter Touren wird verändern
                    //entweder auf dem selben Fahrzeug oder einem neuen Fahrzeug zugewiesen
                    ctrTour.DispoCheck.disponieren = true;
                }
                else
                {
                    //Tour wird einem anderen Fahrzeug zugewiesen
                    ctrTour.DispoCheck.GewichtFreigabe = false;
                    //Gesamtgewicht
                    //disponieren ja oder nein wird in der folgenden Funktion gesetzt
                    DispoGewichtsCheck1(ref ctrTour, ref dispoCheck);
                }
            }
            return ctrTour;
        }
        ///<summary>clsDispoCheck / GetAndCheckRecourcen</summary>
        ///<remarks>Ermittel die entsprechenden Resourcen für den Zeitraum.</remarks>
        private void GetAndCheckRecourcen(ref Sped4.Controls.AFKalenderItemTour ctrTour, Globals._GL_USER myGLUser)
        {
            DataSet ds1 = clsResource.GetUsedTruckRecource(myGLUser, ctrTour.Tour.StartZeit, ctrTour.Tour.EndZeit, ctrTour.Tour.KFZ_ZM);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (Int32 i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    string typ = ds1.Tables[0].Rows[i]["RecourceTyp"].ToString();
                    if (typ == "A")
                    {
                        if (ds1.Tables[0].Rows[i]["VehicleID"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.A_ID = (decimal)ds1.Tables[0].Rows[i]["VehicleID"];
                        }
                        if (ds1.Tables[0].Rows[i]["LG_A"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.LGA = Convert.ToDecimal(ds1.Tables[0].Rows[i]["LG_A"]);
                        }
                        if (ds1.Tables[0].Rows[i]["zlGG"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.zlGG = Convert.ToDecimal(ds1.Tables[0].Rows[i]["zlGG"]);
                        }
                    }
                    if (typ == "Z")
                    {
                        if (ds1.Tables[0].Rows[i]["VehicleID"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.ZM_ID = (decimal)ds1.Tables[0].Rows[i]["VehicleID"];
                        }
                        if (ds1.Tables[0].Rows[i]["LG_ZM"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.LGZM = Convert.ToDecimal(ds1.Tables[0].Rows[i]["LG_ZM"]);
                        }
                        if (ds1.Tables[0].Rows[i]["zlGG"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.zlGG = Convert.ToDecimal(ds1.Tables[0].Rows[i]["zlGG"]);
                        }
                    }
                    if (typ == "F")
                    {
                        if (ds1.Tables[0].Rows[i]["PersonalID"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.P_ID = (decimal)ds1.Tables[0].Rows[i]["PersonalID"];
                        }
                    }
                }
            }
            else
            {
                ctrTour.DispoCheck.A_ID = 0;
                ctrTour.DispoCheck.P_ID = 0;
                ctrTour.DispoCheck.zlGG = 0;
                ctrTour.DispoCheck.LGZM = 0;
                ctrTour.DispoCheck.LGA = 0;
            }
            CheckRecource(ref ctrTour);
        }
        ///<summary>clsDispoCheck / GetAndCheckRecourcen</summary>
        ///<remarks>Ermittel die entsprechenden Resourcen für den Zeitraum.</remarks>
        private void GetAndCheckRecourcen1(ref Sped4.Controls.ctrTourItem ctrTour, Globals._GL_USER myGLUser)
        {
            DataSet ds1 = clsResource.GetUsedTruckRecource(myGLUser, ctrTour.Tour.StartZeit, ctrTour.Tour.EndZeit, ctrTour.Tour.KFZ_ZM);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (Int32 i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    string typ = ds1.Tables[0].Rows[i]["RecourceTyp"].ToString();
                    if (typ == "A")
                    {
                        if (ds1.Tables[0].Rows[i]["VehicleID"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.A_ID = (decimal)ds1.Tables[0].Rows[i]["VehicleID"];
                        }
                        if (ds1.Tables[0].Rows[i]["LG_A"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.LGA = Convert.ToDecimal(ds1.Tables[0].Rows[i]["LG_A"]);
                        }
                        if (ds1.Tables[0].Rows[i]["zlGG"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.zlGG = Convert.ToDecimal(ds1.Tables[0].Rows[i]["zlGG"]);
                        }
                    }
                    if (typ == "Z")
                    {
                        if (ds1.Tables[0].Rows[i]["VehicleID"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.ZM_ID = (decimal)ds1.Tables[0].Rows[i]["VehicleID"];
                        }
                        if (ds1.Tables[0].Rows[i]["LG_ZM"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.LGZM = Convert.ToDecimal(ds1.Tables[0].Rows[i]["LG_ZM"]);
                        }
                        if (ds1.Tables[0].Rows[i]["zlGG"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.zlGG = Convert.ToDecimal(ds1.Tables[0].Rows[i]["zlGG"]);
                        }
                    }
                    if (typ == "F")
                    {
                        if (ds1.Tables[0].Rows[i]["PersonalID"] != DBNull.Value)
                        {
                            ctrTour.DispoCheck.P_ID = (decimal)ds1.Tables[0].Rows[i]["PersonalID"];
                        }
                    }
                }
            }
            else
            {
                ctrTour.DispoCheck.A_ID = 0;
                ctrTour.DispoCheck.P_ID = 0;
                ctrTour.DispoCheck.zlGG = 0;
                ctrTour.DispoCheck.LGZM = 0;
                ctrTour.DispoCheck.LGA = 0;
            }
            CheckRecource1(ref ctrTour);
        }
        ///<summary>clsDispoCheck / CheckRecource</summary>
        ///<remarks>Check, ob die Resourcen (Auflieger, Fahrer) der Zugmaschine zugewiesen sind.</remarks>
        private void CheckRecource(ref Sped4.Controls.AFKalenderItemTour ctrTour)
        {
            ctrTour.DispoCheck.disponieren = true;
            //Check und notfalls disponieren auf false
            string strFehler = string.Empty;
            if (ctrTour.DispoCheck.A_ID == 0)
            {
                ctrTour.DispoCheck.disponieren = false;
                strFehler = strFehler + "\r\n" + "Ressource Auflieger\r\n";
            }
            if (ctrTour.DispoCheck.P_ID == 0)
            {
                ctrTour.DispoCheck.disponieren = false;
                strFehler = strFehler + "Ressource Fahrer\r\n";
            }
            if (ctrTour.DispoCheck.disponieren == false)
            {
                clsMessages.Disposition_RecourcenFehlen(strFehler);
                ctrTour.DispoCheck.disponieren = false;
                ctrTour.DispoCheck.bo_RessourcenCheckOK = false;
            }
        }
        ///<summary>clsDispoCheck / CheckRecource</summary>
        ///<remarks>Check, ob die Resourcen (Auflieger, Fahrer) der Zugmaschine zugewiesen sind.</remarks>
        private void CheckRecource1(ref Sped4.Controls.ctrTourItem ctrTour)
        {
            ctrTour.DispoCheck.disponieren = true;
            //Check und notfalls disponieren auf false
            string strFehler = string.Empty;
            if (ctrTour.DispoCheck.A_ID == 0)
            {
                ctrTour.DispoCheck.disponieren = false;
                strFehler = strFehler + "\r\n" + "Ressource Auflieger\r\n";
            }
            if (ctrTour.DispoCheck.P_ID == 0)
            {
                ctrTour.DispoCheck.disponieren = false;
                strFehler = strFehler + "Ressource Fahrer\r\n";
            }
            if (ctrTour.DispoCheck.disponieren == false)
            {
                clsMessages.Disposition_RecourcenFehlen(strFehler);
                ctrTour.DispoCheck.disponieren = false;
                ctrTour.DispoCheck.bo_RessourcenCheckOK = false;
            }
        }
        ///<summary>clsDispoCheck / DispoGewichtsCheck</summary>
        ///<remarks>Check auf Überladung.</remarks>
        private void DispoGewichtsCheck(ref Sped4.Controls.AFKalenderItemTour ctrTour, ref clsDispoCheck dispoCheck)
        {
            //Gesamtes Tour Gewicht wird ermittelt
            ctrTour.DispoCheck.TourGewicht = dispoCheck.TourGewicht;
            //das maximal zul. Ladungsgewicht wird ermittelt
            ctrTour.DispoCheck.maxGewicht = dispoCheck.maxGewicht;

            //Der Gewichtsvergleich kann hier durchgeführt werden, da schon alle einzelnen Kommissionsgewicht,
            //auch der neu zugewiesenen berücksichtigt sind, da diese auch schon aus der Datenbank kommen.
            if ((ctrTour.DispoCheck.TourGewicht + ctrTour.Kommission.Brutto) > ctrTour.DispoCheck.maxGewicht)
            {
                ctrTour.DispoCheck.GewichtZuHoch = true;
                if (clsMessages.Disposition_DragDropGewichtsCheck(Functions.FormatDecimal(dispoCheck.TourGewicht + ctrTour.Kommission.Brutto), Functions.FormatDecimal(dispoCheck.maxGewicht)))
                {
                    ctrTour.DispoCheck.GewichtFreigabe = true;
                    dispoCheck.SetOKGewichtTrue();
                    ctrTour.DispoCheck.disponieren = true;
                }
                else
                {
                    ctrTour.DispoCheck.GewichtFreigabe = false;
                    dispoCheck.SetOKGewichtFalse();
                    ctrTour.DispoCheck.disponieren = false;
                }
            }
            else
            {
                ctrTour.DispoCheck.GewichtFreigabe = true;
                dispoCheck.SetOKGewichtTrue();
                ctrTour.DispoCheck.disponieren = true;
            }
        }
        ///<summary>clsDispoCheck / DispoGewichtsCheck</summary>
        ///<remarks>Check auf Überladung.</remarks>
        private void DispoGewichtsCheck1(ref Sped4.Controls.ctrTourItem ctrTour, ref clsDispoCheck dispoCheck)
        {
            //Gesamtes Tour Gewicht wird ermittelt
            ctrTour.DispoCheck.TourGewicht = dispoCheck.TourGewicht;
            //das maximal zul. Ladungsgewicht wird ermittelt
            ctrTour.DispoCheck.maxGewicht = dispoCheck.maxGewicht;

            //Der Gewichtsvergleich kann hier durchgeführt werden, da schon alle einzelnen Kommissionsgewicht,
            //auch der neu zugewiesenen berücksichtigt sind, da diese auch schon aus der Datenbank kommen.
            if ((ctrTour.DispoCheck.TourGewicht + ctrTour.Kommission.Brutto) > ctrTour.DispoCheck.maxGewicht)
            {
                ctrTour.DispoCheck.GewichtZuHoch = true;
                if (clsMessages.Disposition_DragDropGewichtsCheck(Functions.FormatDecimal(dispoCheck.TourGewicht + ctrTour.Kommission.Brutto), Functions.FormatDecimal(dispoCheck.maxGewicht)))
                {
                    ctrTour.DispoCheck.GewichtFreigabe = true;
                    dispoCheck.SetOKGewichtTrue();
                    ctrTour.DispoCheck.disponieren = true;
                }
                else
                {
                    ctrTour.DispoCheck.GewichtFreigabe = false;
                    dispoCheck.SetOKGewichtFalse();
                    ctrTour.DispoCheck.disponieren = false;
                }
            }
            else
            {
                ctrTour.DispoCheck.GewichtFreigabe = true;
                dispoCheck.SetOKGewichtTrue();
                ctrTour.DispoCheck.disponieren = true;
            }
        }
        ///<summary>clsDispoCheck / LoadDispoCheckDaten</summary>
        ///<remarks>Ermittelt die DispoCheckdaten für die entsprechende TourID.</remarks>
        private void LoadDispoCheckDaten(ref Sped4.Controls.AFKalenderItemTour ctrTour, ref clsDispoCheck dispoCheck)
        {
            string strSQL = string.Empty;
            DataTable dt = new DataTable();
            if (clsTour.ExistTourID(dispoCheck._GL_User, ctrTour.Tour.ID))
            {
                strSQL = "SELECT * FROM DispoCheck WHERE TourID='" + ctrTour.Tour.ID + "'";
                if (!ctrTour.Kommission.bDragDrop)
                {
                    dispoCheck.init = false;
                }
            }
            else
            {
                dispoCheck.init = true;
            }

            if (strSQL != string.Empty)
            {
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, dispoCheck.BenutzerID, "DispoCheck");

                if (dt.Rows.Count == 0)
                {
                    dispoCheck.init = true;
                }
                else
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        ctrTour.DispoCheck.ID = (decimal)dt.Rows[i]["ID"];
                        ctrTour.DispoCheck.oldZM = (decimal)dt.Rows[i]["oldZM"];
                        ctrTour.DispoCheck.oldStartZeit = (DateTime)dt.Rows[i]["oldStartZeit"];
                        ctrTour.DispoCheck.oldEndZeit = (DateTime)dt.Rows[i]["oldEndZeit"];
                        ctrTour.DispoCheck.GewichtFreigabe = (bool)dt.Rows[i]["OKGewicht"];
                        ctrTour.DispoCheck.bo_BackToOldZM = (bool)dt.Rows[i]["BackToOldZM"];
                        ctrTour.DispoCheck.init = (bool)dt.Rows[i]["NeuDisponiert"];
                    }
                }
            }
        }
        ///<summary>clsDispoCheck / LoadDispoCheckDaten</summary>
        ///<remarks>Ermittelt die DispoCheckdaten für die entsprechende TourID.</remarks>
        private void LoadDispoCheckDaten1(ref Sped4.Controls.ctrTourItem ctrTour, ref clsDispoCheck dispoCheck)
        {
            string strSQL = string.Empty;
            DataTable dt = new DataTable();
            if (clsTour.ExistTourID(dispoCheck._GL_User, ctrTour.Tour.ID))
            {
                strSQL = "SELECT * FROM DispoCheck WHERE TourID='" + ctrTour.Tour.ID + "'";
                if (!ctrTour.Kommission.bDragDrop)
                {
                    dispoCheck.init = false;
                }
            }
            else
            {
                dispoCheck.init = true;
            }

            if (strSQL != string.Empty)
            {
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, dispoCheck.BenutzerID, "DispoCheck");

                if (dt.Rows.Count == 0)
                {
                    dispoCheck.init = true;
                }
                else
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        ctrTour.DispoCheck.ID = (decimal)dt.Rows[i]["ID"];
                        ctrTour.DispoCheck.oldZM = (decimal)dt.Rows[i]["oldZM"];
                        ctrTour.DispoCheck.oldStartZeit = (DateTime)dt.Rows[i]["oldStartZeit"];
                        ctrTour.DispoCheck.oldEndZeit = (DateTime)dt.Rows[i]["oldEndZeit"];
                        ctrTour.DispoCheck.GewichtFreigabe = (bool)dt.Rows[i]["OKGewicht"];
                        ctrTour.DispoCheck.bo_BackToOldZM = (bool)dt.Rows[i]["BackToOldZM"];
                        ctrTour.DispoCheck.init = (bool)dt.Rows[i]["NeuDisponiert"];
                    }
                }
            }
        }
        ///<summary>clsDispoCheck / SetDataForDispoCheck</summary>
        ///<remarks>Setzen der DispoCheck-Daten.</remarks>
        public void SetDataForDispoCheck(ref Sped4.Controls.AFKalenderItemTour ctrTour, ref clsDispoCheck dispoCheck)
        {
            ctrTour.DispoCheck.TourID = ctrTour.Tour.ID;
            ctrTour.DispoCheck.oldZM = ctrTour.Tour.KFZ_ZM;
            ctrTour.DispoCheck.oldStartZeit = ctrTour.Tour.StartZeit;
            ctrTour.DispoCheck.oldEndZeit = ctrTour.Tour.EndZeit;
            ctrTour.DispoCheck.bo_BackToOldZM = false;
        }
        ///<summary>clsDispoCheck / SetDataForDispoCheck1</summary>
        ///<remarks>Setzen der DispoCheck-Daten.</remarks>
        public void SetDataForDispoCheck1(ref Sped4.Controls.ctrTourItem ctrTour, ref clsDispoCheck dispoCheck)
        {
            ctrTour.DispoCheck.TourID = ctrTour.Tour.ID;
            ctrTour.DispoCheck.oldZM = ctrTour.Tour.KFZ_ZM;
            ctrTour.DispoCheck.oldStartZeit = ctrTour.Tour.StartZeit;
            ctrTour.DispoCheck.oldEndZeit = ctrTour.Tour.EndZeit;
            ctrTour.DispoCheck.bo_BackToOldZM = false;
        }
        //
        //
        //
        private void SetInterneVariablen(ref Sped4.Controls.AFKalenderItemKommi KommiCtr, ref clsDispoCheck dispoCheck)
        {
            dispoCheck.AuftragID = KommiCtr.Kommission.AuftragID;
            dispoCheck.AuftragPos = KommiCtr.Kommission.AuftragPos;

            /****
            if (KommiCtr.Kommission.oldZM == KommiCtr.Kommission.KFZ_ZM)
            {
              oldZM = KommiCtr.Kommission.oldZM;
            }
            else
            {
              if (KommiCtr.DispoCheck.disponieren)
              {
                oldZM = KommiCtr.Kommission.KFZ_ZM;
              }
              else
              {
                if (KommiCtr.DispoCheck.bo_RessourcenCheckOK)
                {
                  oldZM = KommiCtr.Kommission.KFZ_ZM;
                }
                else
                {
                  // keine Resscourcen vorhanden, dann muss die Kommission zurück zum alten Fahrzeug
                  oldZM = KommiCtr.DispoCheck.oldZM;
                }
                //oldZM = KommiCtr.Kommission.oldZM;
                //oldZM = KommiCtr.Kommission.KFZ_ZM;
              }
            }
              ***/
            //oldBeladezeit = KommiCtr.DispoCheck.oldBeladezeit;
            //oldEntladezeit = KommiCtr.DispoCheck.oldEntladezeit;

            dispoCheck.bo_BackToOldZM = KommiCtr.DispoCheck.bo_BackToOldZM;
            dispoCheck.disponieren = KommiCtr.DispoCheck.disponieren;
            dispoCheck.GewichtFreigabe = KommiCtr.DispoCheck.GewichtFreigabe;
            dispoCheck.AuftragPosTableID = KommiCtr.Kommission.AuftragPosTableID;
            if (KommiCtr.DispoCheck.ID == 0)
            {
                KommiCtr.DispoCheck.ID = dispoCheck.GetID();
            }
            dispoCheck.ID = KommiCtr.DispoCheck.ID;
        }
        //--------Beim Refresh des Dispoplans werden alle Daten noch einmal upgedatet-----------
        //------ dadurch wir nur noch die Abfrage bei erhöhtem Gewicht nur noch einmal ---------
        //
        public Sped4.Controls.AFKalenderItemKommi UpdateForRefresh(Sped4.Controls.AFKalenderItemKommi KommiCtr, ref clsDispoCheck dispoCheck)
        {
            /****
            KommiCtr.DispoCheck.AuftragID = KommiCtr.Kommission.AuftragID;
            KommiCtr.DispoCheck.AuftragPos = KommiCtr.Kommission.AuftragPos;
            KommiCtr.DispoCheck.oldZM = KommiCtr.Kommission.KFZ_ZM;
            KommiCtr.DispoCheck.oldBeladezeit = KommiCtr.Kommission.BeladeZeit;
            KommiCtr.DispoCheck.oldEntladezeit = KommiCtr.Kommission.EntladeZeit;
            KommiCtr.DispoCheck.KommiID = KommiCtr.Kommission.ID;
            KommiCtr.DispoCheck.ID=GetID();
            KommiCtr.DispoCheck.bo_BackToOldZM = KommiCtr.DispoCheck.bo_BackToOldZM;
            UpdateDispoCheckbyID();

            if (KommiCtr.DispoCheck.GewichtFreigabe)
            {
                SetOKGewichtTrue();
            }
            else
            {
                SetOKGewichtFalse();
            }
             * ***/
            return KommiCtr;
        }

    }
}
