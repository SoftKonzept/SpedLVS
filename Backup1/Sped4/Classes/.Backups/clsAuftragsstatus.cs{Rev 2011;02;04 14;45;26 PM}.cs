using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using Sped4;


namespace Sped4
{
  class clsAuftragsstatus
  {

    //************  User  ***************
    private Int32 _BenutzerID;
    public Int32 BenutzerID
    {
      get { return _BenutzerID; }
      set { _BenutzerID = value; }
    }
    //************************************


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
    private Int32 _AuftragPos;
    private Int32 _ID;
    private Int32 _AP_ID;
    private Int32 _Auftrag_ID;
    private Int32 _Status;
    private decimal _PosGewicht;
    private decimal _gemGewicht;
    private decimal _tatGewicht; 
    private decimal _GesamtGemGewicht;
    private decimal _GesamtTatGewicht;
    private decimal _Gewicht;


    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }
    }
    public Int32 AP_ID
    {
      get { return _AP_ID; }
      set { _AP_ID = value; }
    }
    public Int32 AuftragPos
    {
        get { return _AuftragPos; }
        set { _AuftragPos = value; }
    }
    public Int32 Auftrag_ID
    {
        get { return _Auftrag_ID; }
        set { _Auftrag_ID = value; }
    }
    public Int32 Status
    {
      get { return _Status; }
      set { _Status = value; }
    }
    public decimal PosGewicht
    {
      get 
      {
          if (tatGewicht != 0.00m)
          {
              _PosGewicht = tatGewicht;
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
          if (GesamtTatGewicht != 0.00m)
          {
              _Gewicht = GesamtTatGewicht;
          }
          else
          {
              _Gewicht = GesamtGemGewicht;
          }
          return _Gewicht; 
      }
      set { _Gewicht = value; }
    }
    public decimal gemGewicht
    {
        get { return _gemGewicht; }
        set { _gemGewicht = value; }
    }
    public decimal tatGewicht
    {
        get { return _tatGewicht; }
        set { _tatGewicht = value; }
    }
    public decimal GesamtGemGewicht
    {
        get { return _GesamtGemGewicht; }
        set { _GesamtGemGewicht = value; }
    }
    public decimal GesamtTatGewicht
    {
        get { return _GesamtTatGewicht; }
        set { _GesamtTatGewicht = value; }
    }
        //
        //
        //
        public Int32 GetAuftragsstatus()
        {
            Int32 returnStatus;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "";
            Command.CommandText = "SELECT Status FROM AuftragPos " +
                                                                    "WHERE Auftrag_ID='" + Auftrag_ID + "' AND AuftragPos='"+AuftragPos+"'";

            Globals.SQLcon.Open();
            if (Command.ExecuteScalar() is DBNull)
            {
                returnStatus = 0;
            }
            else
            {
                returnStatus = (Int32)Command.ExecuteScalar();
            }

            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
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
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
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
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
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
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "Update AuftragPos SET Status='4' " +
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
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
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
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
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
            Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
          }
          finally
          {
            AddToLogStatusUpdate(Auftrag_ID, AuftragPos, 6);
          }
        }
        //--- Doks liegen vor und Auftrag kann abgerechnet werden
        public void SetStatusForBerechnungByAP_ID(Int32 APid)
        {
          try
          {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = "Update AuftragPos SET Status='6' WHERE ID='"+APid+"'";

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
            Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
          }
          finally
          {
            Auftrag_ID = Classes.clsAuftragPos.GetAuftragIDByID(APid);
            AuftragPos = Classes.clsAuftragPos.GetAuftragPosByID(APid);
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
            Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
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
            Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
          }
          finally
          { 
            Auftrag_ID=Classes.clsAuftragPos.GetAuftragIDByID(AP_ID);
            AuftragPos=Classes.clsAuftragPos.GetAuftragPosByID(AP_ID);
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
                                "Status as 'Status', "+
                                "Auftrag_ID as 'AuftragID', " +
                                "AuftragPos as 'AuftragPos', "+
                                "(Select SUM(gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'PosGemGewicht', " +
                                "(Select SUM(tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'PosTatGewicht', " +
                                "ID, " +
                                "(Select SUM(gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'GesamtGemGewicht', " +
                                "(Select SUM(tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'GesamtTatGewicht' " +
                                "FROM AuftragPos WHERE AuftragPos.Auftrag_ID='"+Auftrag_ID+"' ORDER BY AuftragPos.AuftragPos";

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
              stat= "Angaben unvollständig";
              break;
            case 2:
              stat= "Angaben vollständig";
              break;
            case 3:
              stat= "Auftrag storniert";
              break;
            case 4:
              stat= "Auftrag disponiert";
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
        private void AddToLogStatusUpdate(Int32 auftrag, Int32 auftragpos, Int32 status)
        {
          //Add Logbucheintrag update
          string Beschreibung = "Statusänderung: Auftrag:" + auftrag.ToString() + " Auftragposition: " + auftragpos.ToString() + " Status: "+ GetStatusbeschreibung(status)+"  geändert";
          Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
        }
  }
}
