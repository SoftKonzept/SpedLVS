using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using Sped4;

namespace Sped4.Classes
{
  class clsMessages
  {

      //---Dispo/Fahrerlite 
        private string _DispoFahrer_EmptyFahrerListe;
      //  private string _DispoDragDrop_GewichtsCheck;
      //--ctrAuftrag-AuftragPDF existiert
        private string _ctrAuftrag_AuftragPDFnotExists;

      //---KommiCtr 
        private string _SetKommiPosition_GesamtgewichtZuHoch;
        private string _SetKommiPosition_GesamtgewichtUeberschrittenKeineDispo;



    //***********************************++ Fahrzeuge *************************************************
    //
    //------------- Fahrzeug Daten ---------------------------
    //
    public static bool Fahrzeug_DeleteDatenSatz()
    {
      string Text = "Der Datensatz wird noch nicht verwendet und kann gelöscht werden. Soll der Datensatz wirklich gelöscht werden?";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
      if (result == DialogResult.Cancel)
      {
        return false;
      }
      else
      {
        return true;
      }
    }
    //--------- KFZ Kennzeichen ist bereits vorhanden --------
    //
    public static void Fahrzeug_KennzeichenBereitsVorhanden()
    {
      string Text = "Das Kennzeichen ist bereits in der Datenbank vorhanden!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //
    //
    //
    //
    /*****************************************************************************************************
     *                                              Personal                                              
     *                                              
     * ***************************************************************************************************/
    //
    //------------- Personal Daten ---------------------------
    //
    public static bool Personal_DeleteDatenSatz()
    {
      string Text = "Der Datensatz wird noch nicht verwendet und kann gelöscht werden. Soll der Datensatz wirklich gelöscht werden?";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
      if (result == DialogResult.Cancel)
      {
        return false;
      }
      else
      {
        return true;
      }
    }    
      //
      //-------------   Liste Dispo Fahrer -----------------------------
      //
      public string DispoFahrer_EmptyFahrerListe
      {
          get 
          {
              _DispoFahrer_EmptyFahrerListe = "Keine Fahrer in  der Personaldatenbank gefunden!";
              return _DispoFahrer_EmptyFahrerListe; 
          }
          set { _DispoFahrer_EmptyFahrerListe = value; }

      }
    //
    //
    //
    //
    //
    //
    /***********************************************************************************************************
     *                                          Abrechnung / Fakturierung
     * 
     * *********************************************************************************************************/
    //
    //
    //
    public static void Fakturierung_LesenFrachtdaten()
    {
      string Text = "Die Frachtdaten konnten nicht gelesen werden. Die Auftragspositions-ID fehlt!";
      DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
    }
    //
    public static void Fakturierung_LesenFrachtdatenByRGID()
    {
      string Text = "Die Frachtdaten konnten nicht gelesen werden. Die RG-/ GS-Nummer fehlt!";
      DialogResult result = MessageBox.Show(Text, "Fehler", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
    }
    //
    //
    //
    public void Fakturierung_EingabenFehlerhaft(string Text)
    {
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //
    public static void Fakturierung_KmAngabeFehlt()
    {
      string Text = "Aufgrund der fehlenden KM-Angabe kann die Frachtberechnung nicht durchgeführt werden!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);  
    }
    //
    //
    //
    public static void Fakturierung_MargeFalsch()
    {
      string Text = "Der Margenwert in € ist nicht korrekt!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);       
    }
    //
    //
    //
    public static bool Fakturierung_GSDatum()
    {
        string Text = "Ist das Gutschriftsdatum wirklich das heutige Datum? Bestätigen Sie mit OK, wenn das Datum korrekt ist.";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (result == DialogResult.OK)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //
    //-------------------- bereits gedruckt RG / GS soll noch einmal gedruckt werden ------------------
    //
    public static bool Fakturierung_RGGSPrintAgain()
    {
      string Text = "Soll die RG / GS noch einmal gedruckt werden?";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
      if (result == DialogResult.OK)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    //
    //----------- Check Auftragszuweisung Auftrags- und Auftragspositionsnummer -----------------
    //
    public static void Fakturierung_AuftragszuweisungCheckAuftrag()
    {
      string Text = "Ein Auftrag mit der eingegebenen Auftrags- und Auftragspositionsnummer wurde nicht gefunden!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);       
    }
    //
    //----------- Check Auftragszuweisung Auftrags- und Auftragspositionsnummer -----------------
    //
    public static void Fakturierung_FrachtFehlt()
    {
      string Text = "Fracht für Rechnung / Gutschrift fehlt!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //----------- manuelle RG / GS Erstellung /kein Auftraggeber gewählt -----------------
    //
    public static void Fakturierung_AuftraggeberFehlt()
    {
      string Text = "Rechnung / Gutschrift kann nicht erstellt werden, da kein Rechnungs-/Gutschriftsempfänger ausgewählt wurde.";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //+++++++++++++ Auftragsliste +++++++++++++
    //
      public static void Auftragsliste_ZeitraumAuswahlFalsch()
      {
        string Text = "Das Startdatum der Suche ist größer wie das Enddatum der Suche. Dies ist nicht möglich. Bitte erneut auswählen!";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (result == DialogResult.Cancel)
        {
          //return true;
        }
        else
        {
          //return false;
        }
      }
      public static void Auftragsliste_AuftragPosLoeschenNichtMoeglich()
      {
        string Text = "Die Auftragposition = 0  kann nicht aufgelöst werden!";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      //
      //
      //
      //
      //
      /******************************************************************************************************
       *                                      DIsposition
       *
       * ****************************************************************************************************/
      //
      //------------- Abfrage erhöhtes Gewicht   ----------------------
      //
      public static bool Disposition_DragDropGewichtsCheck(string Ladungsgewicht, string maxZuladung)
      {
          string text = "Gewicht Transportauftrag [kg]: " +Ladungsgewicht +"\r\n" +
                        "max. Zuladungsgewicht [kg]: " + maxZuladung  +"\r\n" +
                        "Das aktuelle Ladungsgewicht überschreitet das max. Zuladungsgewicht. Soll der Auftrag dennoch disponiert werden?";
          DialogResult result = MessageBox.Show(text, "Check Ladungsgewicht!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
          if (result == DialogResult.Cancel)
          {
              return false;
          }
          else
          {
              return true;
          }         
      }
      public static void Disposition_RecourcenFehlen(string fehlendeRecource)
      {
        string text = "Transportauftrag kann nicht disponiert werden, da folgende Recourcen noch nicht zugwiesen wurden. \r\n " +
                      fehlendeRecource;
        MessageBox.Show(text, "Check Ressourcen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      public string ctrAuftrag_AuftragPDFnotExists
      {
          get
          {
              _ctrAuftrag_AuftragPDFnotExists = "Zu diesem Auftrag kann keine Datei gefunden werden";
              return _ctrAuftrag_AuftragPDFnotExists;
          }
          set { _ctrAuftrag_AuftragPDFnotExists = value; }
      }
      //
      //
      public static void Disposition_AuftragNichtDisponierbar()
      {
          string Text = "Dieser Auftrag kann nicht disponiert werden. Es fehlen Auftragsdaten oder Auftrag ist schon disponiert bzw. durchgeführt.";
          DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
          if (result == DialogResult.Cancel)
          {
            //return true;
          }
          else
          {
            //return false;
          }
      }
      //
      //
      //
      public static void Disposition_RecourcenEndZeitZuKlein()
      {
          string Text = "Die Resourcenendzeit kann nicht vor der Resourcenstartzeit liegen. Die Resourcenendzeit wurde automatisch geändert auf Resourcenstartzeit + 3 Stunden!";
          DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
          if (result == DialogResult.Cancel)
          {
              //return true;
          }
          else
          {
              //return false;
          }
      }
      //
      //
      public static void Disposition_EntladeZeitZuKlein()
      {
          string Text = "Die Entladezeit kann nicht vor der Beladezeit liegen. Bitte die Eingabe noch einmal überprüfen!";
          DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
          if (result == DialogResult.Cancel)
          {
              //return true;
          }
          else
          {
              //return false;
          }

      }
      //
      //------------------ Änderung Recourcenentzeit - Gewähltes Datum falsch
      //
      public static void Disposition_RecourcenentzeitDatumFalsch()
      {
        string Text = "Das gewählte Datum liegt vor dem min. Recourcenentzeitpunkt. Der neue Recourcenentzeitpunkt muss geändert werden!";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
      }
      //
      //
      //
      //
      //
      /*****************************************************************************************************+
       *                                            Kommission    
       *                                                                                        
       ******************************************************************************************************/
      //-------- KommiCtr
      //
      //
      public string SetKommiPosition_GesamtgewichtZuHoch
      {
          get
          {
              _SetKommiPosition_GesamtgewichtZuHoch = "Die Gewicht der beiden Aufträge ist zu groß! Sollen die Aufträge dennoch kombiniert werden?";
              return _SetKommiPosition_GesamtgewichtZuHoch;
          }
          set { _SetKommiPosition_GesamtgewichtZuHoch = value; }
      }
      public string SetKommiPosition_GesamtgewichtUeberschrittenKeineDispo
      {
        get
        {
          _SetKommiPosition_GesamtgewichtUeberschrittenKeineDispo = "Die Gewicht der beiden Aufträge ist zu groß und kann nicht kombiniert werden!";
          return _SetKommiPosition_GesamtgewichtUeberschrittenKeineDispo;
        }
        set { _SetKommiPosition_GesamtgewichtUeberschrittenKeineDispo = value; }
      }
    //
    //------------- Gewicht überschritten - Abfrag ob Kommission wieder entfernt werden soll --------------
    //
      public static bool AuftragEingabe_Gewialue_Abbruch()
      {
        string Text = "Das eingegebenen Gewicht ist gleich 0.\n Bitte Abbrechen um den Speichervorgang zu beenden!";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (result == DialogResult.Cancel)
        {
          return true;
        }
        else
        {
          return false;
        }
      }


    //********************************************************* Auftragerfassung *******************************************************************
    //
    //
      public static bool AuftragEingabe_GewichtDefaultValue_Abbruch()
    {
      string Text = "Das eingegebenen Gewicht ist gleich 0.\n Bitte Abbrechen um den Speichervorgang zu beenden!";
      DialogResult result= MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
      if (result == DialogResult.Cancel)
      {
        return true;
      }
      else
      {
        return false;
      }    
    }
    public static void Auftragserfassung_ADRCheck(string Text)
    {
      DialogResult result = MessageBox.Show(Text, "Check Angabe Adressen", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //************************************************************ Auftragssplitting *****************************************************************
    //
    //
    public static void Auftragssplitting_SplittingGewichtZuHoch()
    {
        string text = "Es können nicht alle Artikel ausgewählt werden! Bitte Auswahl ändern.";
        MessageBox.Show(text, "Check Auftragssplitting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //-- wenn das gesplittete Gewicht zu groß für eine Ladung (ca 25 to ) ist ------
    //
    public static bool Auftragsplitting_SplittingGewichtIstKeinLadungsgewicht()
    {
        string Text = "Das ausgewählte Gewicht überschreitet das normale Ladungsgewicht von 25 to! Soll das Splitting dennoch vorgenommen werden?";
        DialogResult result = MessageBox.Show(Text, "Check Auftragssplitting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (result == DialogResult.Cancel)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    //
    //
    //
    //
    //
    /*****************************************************************************************************+
     *                                             RECOURCE   
     *                                                                                        
     ******************************************************************************************************/
    //
    //
    //
    public static bool Recource_IsUsed()
    {
      string Text = "Die Ressource wird zu diesem Zeitpunkt bereits verwendet!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
      if (result == DialogResult.Cancel)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    //ZM ist bereits die entsprechende Resource zugewiesen
    //
    public static void Recource_TruckUsedTheRecource()
    {
      string Text = "Dem Fahrzeug ist für die angegebene Zeit bereits ein entsprechender Recourcentyp zugewiesen!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }




    //************************************************************ OrderPosRectangle ******************************************************************
    //
    //
    //
    public bool OderPosRectangle_Delete()
    {
      bool delete = false;
      string Text = "Soll die Markierung des Unterauftrags gelöscht werden?";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
      if (result == DialogResult.OK)
      {
        string Text1 = "Die Markierung wird nun unwiderruflich gelöscht! \n Bitte mit OK bestätigen.";
        DialogResult result1 = MessageBox.Show(Text1, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (result1 == DialogResult.OK)
        {
          delete = true;
        }        
      }
      return delete;
    }

    //
    //---------- löschen Kontakteintrag  ------------
    //
    public static bool Kontakte_KontaktDelete()
    {
      bool delete = false;
      string Text = "Soll der Kontakt gelöscht werden?";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
      if (result == DialogResult.OK)
      {
        string Text1 = "Der Kontakt wird nun unwiderruflich gelöscht! \n Bitte mit OK bestätigen.";
        DialogResult result1 = MessageBox.Show(Text1, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (result1 == DialogResult.OK)
        {
          delete = true;
        }        
      }
      return delete;
    }
    //
    //
    //
    //
    //
    //
    /*****************************************************************************************************
     *                                                  ADR                                               
     *****************************************************************************************************/
    //
    //
    //------------- ADR löschen ---------------------------
    //
    public static bool ADR_DeleteDatenSatz()
    {
        string Text = "Der Datensatz wird noch nicht verwendet und kann gelöscht werden. Soll der Datensatz wirklich gelöscht werden? Dabei werden auch alle Kontakt-/Kundendaten dieser Adresse gelöscht!";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (result == DialogResult.Cancel)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    //
    //------------- ViewID = KD_ID Check ViewID -------------------
    //
    public static void ADR_ViewIDisNotNo()
    {
      string Text = "Der Matchcode darf nur Ziffern beinhalten, wenn er als Kundennummer verwendet werden soll!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);      
    }
    //
    //
    //
    public static void ADR_ViewIDnotZero()
    {
      string Text = "Der Matchcode darf nicht leer oder den Wert 0 aufweisen!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //------------- Check VIEWID---------------------------
    //
    public static void ADR_ViewIDIsUsed()
    {
        string Text = "Der Matchcode für diese Adresse wird bereits verwendet. Bitte den Matchcode ändern!";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //
    //
    public static void ADR_FalscheEingabeKtoOrBLZ()
    {
      string Text = "Die Eingabe der Konto- oder BLZ - Nummer enthält Buchstaben. Die Eingabe wurde auf 0 gesetzt und gespeichert.";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //--------------- Kundennummer wird schon verwendet ----------------
    //
    public static void ADR_KundenNummerIsUsed()
    {
      string Text = "Die Kundennummer ist bereits vergeben. Bitte den Kundenummer ändern!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }


    /*******************************************************************************************************
     *                                    Kunden / Kundennummern
     */
    //
    //
    //
    public static void Kunde_FalscheEingabeKDNr()
    {
      string Text = "Die Eingabe der Kundennummer enthält Buchstaben. Die Eingabe wurde zurückgesetzt.";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }




    //
    //
    //
    /***********************************************************************************************************
     * 
     *                                                  Lager
     * 
     * ********************************************************************************************************/
    //
    //--------------- Lagereingang - DGV ist leer ------------------
    //
    public static void Lager_DGVleer()
    {
      string Text = "Es liegen keien Artikel zur Ein- / Auslagerung vor!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //------------ Dateneingabe ADR - fehlen -------------------
    //
    public static void Lager_ADRfehlen()
    {
      string Text = "Adressangaben fehlen! Bitte Adressen auswählen.";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //-------------- Keine Einlagerungsdaten ---------------
    //
    public static void Lager_EinlagerungsnummerExistiertNicht()
    {
      string Text = "Die angegebene EinlagerungsID ist nicht vorhanden!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //
    //
    public static void Lager_KeineArtikelFuerAuslagerung()
    {
      string Text = "Es wurden keine Artikel der zur Auslagerung ausgewählt!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }


    //************************************** ARTIKEL / Güterart *******************************************
    //
    //------------- Artikeldatensatz löschen ---------------------------
    //
    public static bool Artikel_DeleteDatenSatz()
    {
      string Text = "Soll der Artikeldatensatz wirklich gelöscht werden?";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
      if (result == DialogResult.Cancel)
      {
        return false;
      }
      else
      {
        return true;
      }
    }    
    //
    //------------- im Datensatz nicht ausgewählt ---------------------------
    //
    public static void Artikel_ArtikelGutLeer()
    {
        string Text = "Im Artikeldatensatz müssen mindestens Gut und gemeldetes Gewicht > 0 kg angegeben werden?";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //------------- Check VIEWID---------------------------
    //
    public static void Gut_ViewIDIsUsed()
    {
        string Text = "Der Matchcode für diese Güterart wird bereits verwendet. Bitte den Matchcode ändern!";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //+++++++++++++++++++++++++++++++++++++++++ Relation  +++++++++++++++++++++++++++++++++++++++++++
    //
    //
    //
    //------------- Relation löschen ---------------------------
    //
    public static bool Relation_DeleteDatenSatz()
    {
        string Text = "Der Datensatz wird noch nicht verwendet und kann gelöscht werden. Soll der Datensatz wirklich gelöscht werden?";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (result == DialogResult.Cancel)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    //
    //------------- Check VIEWID---------------------------
    //
    public static void Relation_RelationIsUsed()
    {
        string Text = "Eine Relation mit diesem Namen existiert bereits. Bitte Namen ändern!";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //******************************************* FRachtvergabe an SU ************************
    //
    //
    public static void Frachtvergabe_SUfehlt()
    {
      string Text = "Es wurde kein Subunternehmer ausgewählt!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //
    //
    //
    //
    //
    /**************************************************************************************************
     *                                  Userverwaltung
     * 
     * ************************************************************************************************/
    //
    public static void Userverwaltung_PassNichtIdentisch()
    {
      string Text = "Die eingegebenen Passwörter sind nicht identisch. Bitte die Eingabe wiederholen!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //User noch eingeloggt
    public static void Userverwaltung_UserIsLoggedIn()
    {
      string Text = "Der zu löschende User ist noch im System angemeldet und kann nicht gelöscht werden!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }


    //
    //------------ LOGIN - Check false
    //
    public static void Userverwaltung_LOGIN()
    {
      string Text = "Die Zugangsdaten sind nicht korrekt. Bitte die Eingabe wiederholen!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //------------ LOGIN - Versuche > 3  Beenden -------------------
    //
    public static void Userverwaltung_LoginVersucheFehlgeschlagen()
    {
      string Text = "Der mehrfache Versuch sich anzumelden ist fehlgeschlagen. Sped3 wird beendet!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //-------- Nachfrage, ob User gelöscht werden soll ---------------------
    //
    public static bool User_Delete(string name)
    {
      string Text = "Soll der User ["+ name +"] unwiderruflich gelöscht werden?";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
      if (result == DialogResult.Cancel)
      {
        return false;
      }
      else
      {
        return true;
      }
    }
    //
    //---------------- Keine Berechtigung -------------------------
    //
    public static void User_NoAuthen()
    { 
      string Text = "Für diesen Vorgang haben Sie keine Berechtigung!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //
    //
    //
    //
    /****************************************************************************************************
     *                                      Allgemein
     * 
     * **************************************************************************************************/
    //
    //------------- Allgemein löschen verweigert  ----------------------------------------------
    //
    public static void DeleteDenied()
    {
        string Text = "Der Datensatz wird verwendet und kann deshalb nicht gelöscht werden!";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (result == DialogResult.Cancel)
        {
           
        }
        else
        {
        }
    }
    //
    //----------- Check auf Eingabe INT32 ist negativ -----------------
    //
    public static void Allgemein_EingabeIstKeineGanzzahl()
    {
      string Text = "Es dürfen nur ganze Zahlen eingegeben werden. Bitte Eingabe wiederholen!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    } 
    //
    //
    //
    public static void Allgemein_EingabeIstKeineDecimalzahl()
    {
      string Text = "Zahlenwert bitte in folgendem Format eingeben: 1000,00 ";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //
    //
    public static void Allgemein_EingabeIstFehlerhaft()
    {
      string Text = "Die Eingabe ist fehlerhaft oder es konnten zur Eingabe keine Daten gefunden werden!";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //
    //
    public static void Allgemein_WertZuGross()
    {
      string Text = "Der eingebene Wert ist zu groß. Eingabe bitte wiederholen.";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    } 
    //
    //------- Datum Check  -----------------------
    //
    public static void DateCheck_DateToBeInPast()
    {
        string Text = "Das Datum liegt in der Vergangenheit. Bitte Datum neu wählen.";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //
    //------- Datum Check  -----------------------
    //
    public static void DateCheck_DateToBeInFuture()
    {
      string Text = "Das Datum liegt in der Zukunft. Bitte Datum neu wählen.";
      DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
    }
    //------------- Allgemein löschen -----------------------
    //
    public static bool DeleteAllgemein()
    {
        string Text = "Soll der Datensatz / Dokument unwiederruflich gelöscht werden?";
        DialogResult result = MessageBox.Show(Text, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        if (result == DialogResult.Cancel)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
  }
}
