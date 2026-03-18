using System;
using System.Data;
using System.Data.SqlClient;


namespace LVS
{
    public class clsAuftragsstatus
    {

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get { return _BenutzerID; }
            set { _BenutzerID = value; }
        }
        //************************************

        public clsAuftrag Auftrag;

        /************************************************************************************
        * Übersicht Status und entsprechende Images
        * - 1 unvollständig  - delete_16.png   (rotes Kreuz)
        * - 2 vollständig    - add.png         (grünes Kreuz)
        * - 3 storniert      - form_green_delete.png (grünes Form mit rotem Kreuz)
        * - 4 disponiert / vergabe     - disponiert.png  (rotes Fähnchen)
        * - 5 durchgeführt   - done.png        (blaues Fähnchen)
        * - 6 Freigabe Berechnung - Freigabe_Berechnung.png  (grünes Fähnchen)
        * - 7 berechnet      - check       (gründer Haken) 
        * - 8 bezahlt      
        * ***********************************************************************************/
        private decimal _PosGewicht;
        private decimal _Gewicht;


        public decimal ID { get; set; }
        public decimal AP_ID { get; set; }
        public decimal AuftragPos { get; set; }
        public decimal Auftrag_ID { get; set; }
        public Int32 Status { get; set; }
        public decimal PosGewicht
        {
            get
            {
                if (Brutto != 0.00m)
                {
                    _PosGewicht = Brutto;
                }
                else
                {
                    _PosGewicht = gemGewicht;
                }
                return _PosGewicht;
            }
            set { _PosGewicht = value; }
        }
        public decimal Gewicht
        {
            get
            {
                if (GesamtBrutto != 0.00m)
                {
                    _Gewicht = GesamtBrutto;
                }
                else
                {
                    _Gewicht = GesamtGemGewicht;
                }
                return _Gewicht;
            }
            set { _Gewicht = value; }
        }
        public decimal gemGewicht { get; set; }
        public decimal Brutto { get; set; }
        public decimal GesamtGemGewicht { get; set; }
        public decimal GesamtBrutto { get; set; }



        /**************************************************************************************
         *                       Methoden, Procedure
         * ***********************************************************************************/
        ///<summary>clsAuftragsstatus / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER GLUser, Globals._GL_SYSTEM GLSystem, clsSystem myClsSystem)
        {
            this.BenutzerID = GLUser.User_ID;
            Auftrag = new clsAuftrag();
            Auftrag.InitClass(GLUser, GLSystem, myClsSystem);
        }
        ///<summary>clsAuftragsstatus / InitStatus</summary>
        ///<remarks></remarks>
        public void InitStatus(clsAuftragPos myAuftragPos)
        {
            Auftrag.AuftragPos = myAuftragPos;
            Auftrag.ID = Auftrag.AuftragPos.AuftragTableID;
            Auftrag.Fill();
        }
        //
        //
        //
        public Int32 GetAuftragsstatus()
        {
            Int32 returnStatus = 0;
            string strSQL = string.Empty;
            strSQL = "SELECT Status FROM AuftragPos WHERE ID='" + AP_ID + "' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            Int32.TryParse(strTmp, out returnStatus);
            return returnStatus;
        }
        //********************************************** STATUS setzen  ******************************************************
        //
        //--------------- Update des Gewichts bei Splittung von Unteraufträgen  ---------------
        //---- Auftrag vollständig zurück aus Dispo
        public void SetStatusBackFromDispo()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "Update AuftragPos SET Status='2' " +
                                                                     "WHERE Auftrag_ID='" + Auftrag_ID + "' AND AuftragPos='" + AuftragPos + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
            finally
            {
                AddToLogStatusUpdate(Auftrag_ID, AuftragPos, 2);
            }
        }
        //---- Auftrag stroniert
        public void SetStatusStorno()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "Update AuftragPos SET Status='3' " +
                                                                     "WHERE Auftrag_ID='" + Auftrag_ID + "' AND AuftragPos='" + AuftragPos + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
            finally
            {
                AddToLogStatusUpdate(Auftrag_ID, AuftragPos, 3);
            }
        }
        //--- Auftrag disponiert
        public void SetStatusDisposition()
        {
            try
            {
                string strSQL = string.Empty;
                strSQL = "Update AuftragPos SET Status='4' WHERE ID='" + AP_ID + "' ";
                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
            catch (Exception ex)
            {
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                AddToLogStatusUpdate(Auftrag_ID, AuftragPos, 4);
            }
        }
        //--- Auftrag erledigt
        public void SetStatusDone()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "Update AuftragPos SET Status='5' " +
                                                                     "WHERE Auftrag_ID='" + Auftrag_ID + "' AND AuftragPos='" + AuftragPos + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
            finally
            {
                AddToLogStatusUpdate(Auftrag_ID, AuftragPos, 5);
            }
        }
        //--- Doks liegen vor und Auftrag kann abgerechnet werden
        public void SetStatusForBerechnung()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "Update AuftragPos SET Status='6' " +
                                                                     "WHERE Auftrag_ID='" + Auftrag_ID + "' AND AuftragPos='" + AuftragPos + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
            finally
            {
                AddToLogStatusUpdate(Auftrag_ID, AuftragPos, 6);
            }
        }
        //--- Doks liegen vor und Auftrag kann abgerechnet werden
        public void SetStatusForBerechnungByAP_ID(decimal APid)
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "Update AuftragPos SET Status='6' WHERE ID='" + APid + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
            finally
            {
                Auftrag_ID = clsAuftragPos.GetAuftragIDByID(APid);
                AuftragPos = clsAuftragPos.GetAuftragPosByID(APid);
                AddToLogStatusUpdate(Auftrag_ID, AuftragPos, 6);
            }
        }
        //
        //----------------- Auftrag berechnet --------------------
        //
        public void SetStatusBerechnet()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "Update AuftragPos SET Status='7' " +
                                                                     "WHERE Auftrag_ID='" + Auftrag_ID + "' AND AuftragPos='" + AuftragPos + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
            finally
            {
                //Auftrag_ID = Classes.clsAuftragPos.GetAuftragIDByID(AP_ID);
                //AuftragPos = Classes.clsAuftragPos.GetAuftragPosByID(AP_ID);
                AddToLogStatusUpdate(Auftrag_ID, AuftragPos, 7);
            }

        }
        //
        //--------------- Status RG / GS bezahlt - Auftrag abgeschlossen -----------
        //
        public void SetStatusBezahlt()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "Update AuftragPos SET Status='8' WHERE ID='" + AP_ID + "'";


                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
            finally
            {
                Auftrag_ID = clsAuftragPos.GetAuftragIDByID(AP_ID);
                AuftragPos = clsAuftragPos.GetAuftragPosByID(AP_ID);
                AddToLogStatusUpdate(Auftrag_ID, AuftragPos, 8);
            }
        }
        //
        //---------------  
        //
        public DataTable GetStatusListeForAuftrag()
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                  "Status as 'Status', " +
                                  "Auftrag_ID as 'AuftragID', " +
                                  "AuftragPos as 'AuftragPos', " +
                                  "(Select SUM(gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'PosGemGewicht', " +
                                  "(Select SUM(Brutto) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'PosBrutto', " +
                                  "ID, " +
                                  "(Select SUM(gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'GesamtGemGewicht', " +
                                  "(Select SUM(Brutto) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'GesamtBrutto' " +
                                  "FROM AuftragPos WHERE AuftragPos.Auftrag_ID='" + Auftrag_ID + "' ORDER BY AuftragPos.AuftragPos";

            ada.Fill(dataTable);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();
            return dataTable;
        }
        //
        //---------------- Get Statusstring ------------
        //
        public static string GetStatusbeschreibung(Int32 Status)
        {
            /************************************************************************************
            * Übersicht Status und entsprechende Images
            * - 1 unvollständig  - delete_16.png   (rotes Kreuz)
            * - 2 vollständig    - add.png         (grünes Kreuz)
            * - 3 storniert      - form_green_delete.png (grünes Form mit rotem Kreuz)
            * - 4 disponiert / vergabe     - disponiert.png  (rotes Fähnchen)
            * - 5 durchgeführt   - done.png        (blaues Fähnchen)
            * - 6 Freigabe Berechnung - Freigabe_Berechnung.png  (grünes Fähnchen)
            * - 7 berechnet      - check       (gründer Haken) 
            * - 8 bezahlt
            * ***********************************************************************************/


            string stat = string.Empty;

            switch (Status)
            {
                case 1:
                    stat = "Angaben unvollständig";
                    break;
                case 2:
                    stat = "Angaben vollständig";
                    break;
                case 3:
                    stat = "Auftrag storniert";
                    break;
                case 4:
                    stat = "Auftrag disponiert";
                    break;
                case 5:
                    stat = "Angaben druchgeführt";
                    break;
                case 6:
                    stat = "Freigabe zur Berechnung";
                    break;
                case 7:
                    stat = "Auftrag berechnet";
                    break;
                case 8:
                    stat = "Auftrag bezahlt";
                    break;
            }
            return stat;
        }
        //
        //
        //
        private void AddToLogStatusUpdate(decimal auftrag, decimal auftragpos, Int32 status)
        {
            //Add Logbucheintrag update
            string Beschreibung = "Statusänderung: Auftrag:" + auftrag.ToString() + " Auftragposition: " + auftragpos.ToString() + " Status: " + GetStatusbeschreibung(status) + "  geändert";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
        }
    }
}
