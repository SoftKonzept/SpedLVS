using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using Sped4;
using Sped4.Classes;

namespace Sped4
{
    class clsArtikel
    {
        private DataTable artTable = new DataTable();
        
        private Int32 _ID;
        private Int32 _AuftragID;
        private Int32 _AuftragPos;
        private Int32 _LVS_ID;
        private string _Gut;
        private decimal _Dicke;
        private decimal _Breite;
        private decimal _Laenge;
        private decimal _Hoehe;
        private Int32 _Menge;
        private Int32 _ME;
        private decimal _tatGewicht;
        private decimal _gemGewicht;
        private string _Werksnummer;
        private string _CPNr;
        private string _exBezeichnung;
        private string _Werk;
        private string _Halle;
        private string _Reihe;
        private string _Platz;
        private Int32 _iSchaden;
        private string _Schadensbeschreibung;
        private string _Einheit;
        private string _Charge;
        private string _Position;
        private string _Bestellnummer;
        private string _exMaterialnummer;
        //Artikelzusammenführung
        private Int32 _IDforUnion;
        private Int32 _IDforDelete;
        private Int32 _MEforUnion;
        private decimal _tatGewichtforUnion;
        private decimal _gemGewichtforUnion;



        public Int32 IDforUnion
        {
          get { return _IDforUnion; }
          set { _IDforUnion = value; }
        }
        public Int32 IDforDelete
        {
          get { return _IDforDelete; }
          set { _IDforDelete = value; }
        }
        public Int32 MEforUnion
        {
          get { return _MEforUnion; }
          set { _MEforUnion = value; }
        }
        public decimal tatGewichtforUnion
        {
          get { return _tatGewichtforUnion; }
          set { _tatGewichtforUnion = value; }
        }
        public decimal gemGewichtforUnion
        {
          get { return _gemGewichtforUnion; }
          set { _gemGewichtforUnion = value; }
        }

        public Int32 ID
        {
            get{ return _ID; }
            set { _ID = value; }
        }
       
        public int AuftragID
        {
            get { return _AuftragID; }
            set { _AuftragID = value; }
        }

        public int AuftragPos
        {
          get { return _AuftragPos; }
          set { _AuftragPos = value; }
        }
        public string Gut
        {
            get { return _Gut; }
            set { _Gut = value; }
        }
        public Int32 LVS_ID
        {
            get { return _LVS_ID; }
            set { _LVS_ID = value; }
        }
        public decimal Dicke
        {
            get { return _Dicke; }
            set { _Dicke = value; }
        }
        public decimal Breite
        {
            get { return _Breite; }
            set { _Breite = value; }
        }
        public decimal Laenge
        {
            get { return _Laenge; }
            set { _Laenge = value; }
        }
        public decimal Hoehe
        {
            get { return _Hoehe; }
            set { _Hoehe = value; }
        }

        public Int32 ME
        {
            get { return _ME; }
            set { _ME = value; }
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
        public string Werksnummer
        {
          get { return _Werksnummer; }
          set { _Werksnummer = value; }
        }
        public string CPNr
        {
          get { return _CPNr; }
          set { _CPNr = value; }
        }
        public string Einheit
        {
          get { return _Einheit; }
          set { _Einheit = value; }
        }


        public string exBezeichnung
        {
          get { return _exBezeichnung; }
          set { _exBezeichnung = value; }
        }
        public string Halle
        {
          get { return _Halle; }
          set { _Halle = value; }
        }
        public string Werk
        {
          get { return _Werk; }
          set { _Werk = value; }
        }
        public string Reihe
        {
          get { return _Reihe; }
          set { _Reihe = value; }
        }
        public string Platz
        {
          get { return _Platz; }
          set { _Platz = value; }
        }
        public Int32 iSchaden
        {
          get { return _iSchaden; }
          set { _iSchaden = value; }
        }
        public string Schadensbeschreibung
        {
          get { return _Schadensbeschreibung; }
          set { _Schadensbeschreibung = value; }
        }
        public string Charge
        {
          get { return _Charge; }
          set { _Charge = value; }
        }
        public string Bestellnummer
        {
          get { return _Bestellnummer; }
          set { _Bestellnummer = value; }
        }
        public string exMaterialnummer
        {
          get { return _exMaterialnummer; }
          set { _exMaterialnummer = value; }
        }
        public string Position
        {
          get { return _Position; }
          set { _Position = value; }
        }       

        //************************************************************************
        //-------------  Methoden
        //***********************************************************************
        //
        //
        //
        public void SetColArtTable()
        {
            //DataColumn col;

            //----  init and add the columns ----
            //Gut
            DataColumn col1 = new DataColumn();
            col1.DataType = System.Type.GetType("System.String");
            col1.ColumnName = "Gut";
            col1.ReadOnly = false;
            artTable.Columns.Add(col1);

            //Dicke
            DataColumn col2 = new DataColumn();
            col2.DataType = System.Type.GetType("System.Decimal");
            col2.ColumnName = "Dicke";
            col2.ReadOnly = false;
            artTable.Columns.Add(col2);

            //Breite
            DataColumn col3 = new DataColumn();
            col3.DataType = System.Type.GetType("System.Decimal");
            col3.ColumnName = "Breite";
            col3.ReadOnly = false;
            artTable.Columns.Add(col3);

            //Länge
            DataColumn col4 = new DataColumn();
            col4.DataType = System.Type.GetType("System.Decimal");
            col4.ColumnName = "Laenge";
            col4.ReadOnly = false;
            artTable.Columns.Add(col4);

            //Höhe
            DataColumn col5 = new DataColumn();
            col5.DataType = System.Type.GetType("System.Decimal");
            col5.ColumnName = "Hoehe";
            col5.ReadOnly = false;
            artTable.Columns.Add(col5);
            
            //ME
            DataColumn col6 = new DataColumn();
            col6.DataType = System.Type.GetType("System.Int32");
            col6.ColumnName = "ME";
            col6.ReadOnly = false;
            artTable.Columns.Add(col6);

            //gemGewicht
            DataColumn col7 = new DataColumn();
            col7.DataType = System.Type.GetType("System.Decimal");
            col7.ColumnName = "gemGewicht";
            col7.ReadOnly = false;
            artTable.Columns.Add(col7);

            //tatGewicht
            DataColumn col8 = new DataColumn();
            col8.DataType = System.Type.GetType("System.Decimal");
            col8.ColumnName = "tatGewicht";
            col8.ReadOnly = false;
            artTable.Columns.Add(col8);

            //Werksnummer
            DataColumn col9 = new DataColumn();
            col9.DataType = System.Type.GetType("System.String");
            col9.ColumnName = "Werksnummer";
            col9.ReadOnly = false;
            artTable.Columns.Add(col9);

            //ID
            DataColumn col10 = new DataColumn();
            col10.DataType = System.Type.GetType("System.String");
            col10.ColumnName = "Table_ID";
            col10.ReadOnly = false;
            artTable.Columns.Add(col10);
        }
        //
        //----  Val to DataTable  ----
        //
        public void ValueToArtTable()
       {
            DataRow dr = artTable.NewRow();
            dr["Gut"] = Gut;
            dr["Dicke"] = Dicke;
            dr["Breite"] = Breite;
            dr["Laenge"] = Laenge;
            dr["Hoehe"] = Hoehe;
            dr["ME"] = ME;
            dr["gemGewicht"] = gemGewicht;
            dr["tatGewicht"] = tatGewicht;
            dr["Werksnummer"] = Werksnummer;
            dr["Table_ID"] = ID;

            artTable.Rows.Add(dr);
        }
        //
        //--------- add Artikel to DB  -------------- 
        //
        public void Add(Int32 Auftrag, Int32 AuftragPos)
        {
            ArrayList artList = new ArrayList();
            for (int i = 0; i <= artTable.Rows.Count - 1; i++)
            {
                //-- Val der einzelen Rows ----
                AuftragID = Auftrag;
                AuftragPos = AuftragPos;

                Gut = artTable.Rows[i]["Gut"].ToString();
                Dicke = Convert.ToDecimal(artTable.Rows[i]["Dicke"].ToString());
                Breite = Convert.ToDecimal(artTable.Rows[i]["Breite"].ToString());
                Laenge = Convert.ToDecimal(artTable.Rows[i]["Laenge"].ToString());
                Hoehe = Convert.ToDecimal(artTable.Rows[i]["Hoehe"].ToString());
                ME =Convert.ToInt32(artTable.Rows[i]["ME"]);
                gemGewicht = Convert.ToDecimal(artTable.Rows[i]["gemGewicht"].ToString());
                tatGewicht = Convert.ToDecimal(artTable.Rows[i]["tatGewicht"].ToString());
                Werksnummer =artTable.Rows[i]["Werksnummer"].ToString();

                try
                {
                    //--- initialisierung des sqlcommand---
                    SqlCommand Command = new SqlCommand();
                    Command.Connection = Globals.SQLcon.Connection;

                    //----- SQL Abfrage -----------------------
                    Command.CommandText = ("INSERT INTO Artikel (AuftragID, AuftragPos, GArt, Dicke, Breite, Laenge, Hoehe ,ME ,gemGewicht, tatGewicht, Werksnummer) " +
                                                   "VALUES ('" + AuftragID + "','"
                                                               + AuftragPos + "','"
                                                               + Gut + "','" 
                                                               + Dicke.ToString().Replace(",",".") + "','" 
                                                               + Breite.ToString().Replace(",",".") + "','" 
                                                               + Laenge.ToString().Replace(",",".") + "','" 
                                                               + Hoehe.ToString().Replace(",",".") + "','"  
                                                               + ME + "','"
                                                               + gemGewicht.ToString().Replace(",", ".") + "','"  
                                                               + tatGewicht.ToString().Replace(",", ".") + "','"  
                                                               + Werksnummer + "')");

                    Globals.SQLcon.Open();
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Globals.SQLcon.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());

                }
                finally
                {
                    //MessageBox.Show("Der Artikeldaten wurde erfolgreich in die Datenbank geschrieben!");
                    //artList.Add(GetArtikelIDbyValue(ArtikelNr, Gut,Gewicht));
                }
            }
        }
        public void AddArtikel()
        {
          try
          {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = ("INSERT INTO Artikel (AuftragID, AuftragPos, GArt, Dicke, Breite, Laenge, Hoehe ,ME ,gemGewicht, tatGewicht, Werksnummer) " +
                                           "VALUES ('" + AuftragID + "','"
                                                       + AuftragPos + "','"
                                                       + Gut + "','"
                                                       + Dicke.ToString().Replace(",", ".") + "','"
                                                       + Breite.ToString().Replace(",", ".") + "','"
                                                       + Laenge.ToString().Replace(",", ".") + "','"
                                                       + Hoehe.ToString().Replace(",", ".") + "','"
                                                       + ME + "','"
                                                       + gemGewicht.ToString().Replace(",", ".") + "','"
                                                       + tatGewicht.ToString().Replace(",", ".") + "','"
                                                       + Werksnummer + "')");

            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
          }
          catch (Exception ex)
          {
            System.Windows.Forms.MessageBox.Show(ex.ToString());

          }
          finally
          {
            //MessageBox.Show("Der Artikeldaten wurde erfolgreich in die Datenbank geschrieben!");
            //artList.Add(GetArtikelIDbyValue(ArtikelNr, Gut,Gewicht));
          }
        }


        //
        //----------- Update Artikel  -------------------------------
        // durchläuft den Table, bei ID wird Update gemacht und sonst der fehldende/neue Artikel 
        //eingefügt
        public void UpdateArtikel(Int32 Auftrag, Int32 AuftragPos)
        {
            //Int32 MaxPos = GetMaxPos(Auftrag, AuftragPos);
            //ArrayList artList = new ArrayList();
            //Date_Add = DateTime.Now;
            for (int i = 0; i <= artTable.Rows.Count - 1; i++)
            {
              //-- Val der einzelen Rows ----
              AuftragID = Auftrag;
              AuftragPos = AuftragPos;
              //ArtikelNr = artTable.Rows[i]["ArtikelNr"].ToString();
              Gut = artTable.Rows[i]["Gut"].ToString();
              Dicke = Convert.ToDecimal(artTable.Rows[i]["Dicke"].ToString());
              Breite = Convert.ToDecimal(artTable.Rows[i]["Breite"].ToString());
              Laenge = Convert.ToDecimal(artTable.Rows[i]["Laenge"].ToString());
              Hoehe = Convert.ToDecimal(artTable.Rows[i]["Hoehe"].ToString());
              ME = Convert.ToInt32(artTable.Rows[i]["ME"]);
              gemGewicht = Convert.ToDecimal(artTable.Rows[i]["gemGewicht"].ToString());
              tatGewicht = Convert.ToDecimal(artTable.Rows[i]["tatGewicht"].ToString());
              Werksnummer = artTable.Rows[i]["Werksnummer"].ToString();
              ID = Convert.ToInt32(artTable.Rows[i]["Table_ID"].ToString());

              //Artikel schon vorhanden und kann über die ID upgedatet werden
              if(ID>0)
              {

                  try
                  {
                      //--- initialisierung des sqlcommand---
                      SqlCommand Command = new SqlCommand();
                      Command.Connection = Globals.SQLcon.Connection;

                      //----- SQL Abfrage -----------------------
                      Command.CommandText = ("Update Artikel SET AuftragID ='"+ AuftragID +"', "+
                                                                "AuftragPos ='"+AuftragPos + "', " +
                                                                "GArt ='"+ Gut+"', "+ 
                                                                "Dicke ='"+ Dicke.ToString().Replace(",",".") + "', " +
                                                                "Breite ='" + Breite.ToString().Replace(",",".") + "', " +
                                                                "Laenge = '"+ Laenge.ToString().Replace(",",".") + "', " +
                                                                "Hoehe ='"+ Hoehe.ToString().Replace(",",".") + "', "+  
                                                                "ME ='"+ ME + "', "+
                                                                "gemGewicht ='"+ gemGewicht.ToString().Replace(",", ".") +"', "+
                                                                "tatGewicht ='" + tatGewicht.ToString().Replace(",", ".") + "', " +
                                                                "Werksnummer ='" + Werksnummer + "' " +
                                                                    "WHERE ID='"+ID+"'");
                      Globals.SQLcon.Open();
                      Command.ExecuteNonQuery();
                      Command.Dispose();
                      Globals.SQLcon.Close();
                  }
                  catch (Exception ex)
                  {
                      System.Windows.Forms.MessageBox.Show(ex.ToString());

                  }
                  finally
                  {
                      //MessageBox.Show("Der Artikeldaten wurde erfolgreich in die Datenbank geschrieben!");
                      //artList.Add(GetArtikelIDbyValue(ArtikelNr, Gut,Gewicht));
                  }
              }
              else  // Insert, da Artikel keine ID und noch nicht vorhanden
              {
                //-- Val der einzelen Rows ----
                //Pos=Pos+1;  // pos des Datensatze wird weiter hochgezählt

                try
                {
                    //--- initialisierung des sqlcommand---
                    SqlCommand Command = new SqlCommand();
                    Command.Connection = Globals.SQLcon.Connection;

                    //----- SQL Abfrage -----------------------
                    Command.CommandText = ("INSERT INTO Artikel (AuftragID, AuftragPos, GArt, Dicke, Breite, Laenge, Hoehe ,ME ,gemGewicht, tatGewicht, Werksnummer) " +
                                                   "VALUES ('" + AuftragID + "','"
                                                               + AuftragPos + "','"
                                                               + Gut + "','" 
                                                               + Dicke.ToString().Replace(",",".") + "','" 
                                                               + Breite.ToString().Replace(",",".") + "','" 
                                                               + Laenge.ToString().Replace(",",".") + "','" 
                                                               + Hoehe.ToString().Replace(",",".") + "','"  
                                                               + ME + "','"
                                                               + gemGewicht.ToString().Replace(",", ".") + "','"
                                                               + tatGewicht.ToString().Replace(",", ".") + "','"
                                                               + Werksnummer + "')");

                    Globals.SQLcon.Open();
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Globals.SQLcon.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());

                }
                finally
                {
                    //MessageBox.Show("Der Artikeldaten wurde erfolgreich in die Datenbank geschrieben!");
                    //artList.Add(GetArtikelIDbyValue(ArtikelNr, Gut,Gewicht));
                }
                
              
              }
            }
        }
        //
        //
        //
        public void UpdateArtikelSped()
        {
          if (ID > 0)
          {

            try
            {
              //--- initialisierung des sqlcommand---
              SqlCommand Command = new SqlCommand();
              Command.Connection = Globals.SQLcon.Connection;

              //----- SQL Abfrage -----------------------
              Command.CommandText = ("Update Artikel SET AuftragID ='" + AuftragID + "', " +
                                                        "AuftragPos ='" + AuftragPos + "', " +
                                                        "GArt ='" + Gut + "', " +
                                                        "Dicke ='" + Dicke.ToString().Replace(",", ".") + "', " +
                                                        "Breite ='" + Breite.ToString().Replace(",", ".") + "', " +
                                                        "Laenge = '" + Laenge.ToString().Replace(",", ".") + "', " +
                                                        "Hoehe ='" + Hoehe.ToString().Replace(",", ".") + "', " +
                                                        "ME ='" + ME + "', " +
                                                        "gemGewicht ='" + gemGewicht.ToString().Replace(",", ".") + "', " +
                                                        "tatGewicht ='" + tatGewicht.ToString().Replace(",", ".") + "', " +
                                                        "Werksnummer ='" + Werksnummer + "' " +
                                                            "WHERE ID='" + ID + "'");
              Globals.SQLcon.Open();
              Command.ExecuteNonQuery();
              Command.Dispose();
              Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
              System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            finally
            {
              //MessageBox.Show("Der Artikeldaten wurde erfolgreich in die Datenbank geschrieben!");
              //artList.Add(GetArtikelIDbyValue(ArtikelNr, Gut,Gewicht));
            }
          }
        }
        //
        //--- Update Artikel - Auftragsplittung - > AuftragPos wird geändert
        //
        public void UpdateArtikelAuftragPosByID()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("Update Artikel SET AuftragPos ='" + AuftragPos + "' " +
                                                           //"Pos ='"+Pos+"' "+
                                                              "WHERE ID='" + ID + "'");
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        //
        //--------------- Update Artikel tatGewicht ----------------
        //
        public static void UpdateArtikelTatGewicht(Int32 artikelID, decimal Gewicht)
        {
          try
          {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = ("Update Artikel SET tatGewicht ='" + Gewicht.ToString().Replace(",", ".") + "' " +
              //"Pos ='"+Pos+"' "+
                                                          "WHERE ID='" + artikelID + "'");
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
          }
          catch (Exception ex)
          {
            System.Windows.Forms.MessageBox.Show(ex.ToString());
          }
        }
        //
        //----------------- Auflösen einer AuftragPos / Artikel ----------------------
        //
        public static void UpdateArtikelNachAuftragPosAufloesung(Int32 Auftrag, Int32 AuftragPos)
        {
           /****************************************************************************
           * - Artikel einer Auftragsposition werden der Auftragspos. 0 zugeordnet
           *   ->Vorgehen:
           *      > Check Artikel (Güterart und Abmessungen) unter der Auftragsnummer noch einmal vorhanden
           *      > ja - und Position == 0 Mengen zusammenfassen unter Pos=0
           *      > ja - und Position != 0 Artikel Positionsupdate auf Pos=0
           *      > nein - Artikel Positionsupdate auf Pos=0
           ****************************************************************************/

          //Artikel der Position
          DataTable dtArtikel = new DataTable("Artikel");
          dtArtikel = clsArtikel.GetDataTableForArtikelGrd(Auftrag, AuftragPos);

          for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
          {

            clsArtikel clsArt = new clsArtikel();
            clsArt.AuftragPos = AuftragPos;
            clsArt.AuftragID = Auftrag;
            clsArt.ID = (Int32)dtArtikel.Rows[i]["ID"];
            clsArt.SetArtikelDaten(ref clsArt, dtArtikel);

            if (clsArt.CheckArtikelByAuftragAuftragPosANDAbmessungen(ref clsArt))
            {
              //ME und Gewicht addieren
              clsArt.SetArtikeldatenForUnion(ref clsArt);
              //alten Artikel löschen
              clsArt.DeleteArtikelByID(clsArt.IDforDelete);
              //update Artikel 
              clsArt.AuftragPos = 0;
              //clsArt.UpdateArtikelAuftragPosByID();
              clsArt.UpdateArtikelSped();
            }
            else
            {
              //Update der Auftragsposition
              clsArt.AuftragPos = 0;
              clsArt.UpdateArtikelAuftragPosByID();
            }
          }   
        }
        //
        //
        //
        public bool CheckArtikelByAuftragAuftragPosANDAbmessungen(ref clsArtikel clsArt)
        {
          bool retVal = false;

          try
          {
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM Artikel WHERE AuftragID='" + AuftragID + "' AND " +
                                                                "AuftragPos='0' AND " +
                                                                "GArt='" + Gut + "' AND " +
                                                                "Dicke='" + Dicke.ToString().Replace(",", ".") + "' AND " +
                                                                "Breite='" + Breite.ToString().Replace(",", ".") + "' AND " +
                                                                "Laenge='" + Laenge.ToString().Replace(",", ".") + "' AND " +
                                                                "Hoehe='" + Hoehe.ToString().Replace(",", ".") + "'";

            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if (obj == null)
            {
              retVal = false;
            }
            else
            {
              retVal = true;
              clsArt.IDforUnion = (Int32)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
              Command.Connection.Close();
            }
          }
          catch (Exception ex)
          { }
          return retVal;
        }
        //
        //
        //
        public void SetArtikelDaten(ref clsArtikel clsArt, DataTable dt)
        {
          DataTable dtArtikel = dt;
 
            //Daten setzen
            for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
            {
              clsArt.ID = (Int32)dtArtikel.Rows[i]["ID"];
              clsArt.Gut = (string)dtArtikel.Rows[i]["Gut"];
              clsArt.gemGewicht = (decimal)dtArtikel.Rows[i]["gemGewicht"];
              clsArt.tatGewicht = (decimal)dtArtikel.Rows[i]["tatGewicht"];
              clsArt.ME = Convert.ToInt32(dtArtikel.Rows[i]["ME"].ToString());
              clsArt.Werksnummer = dtArtikel.Rows[i]["Werksnummer"].ToString();
              decimal dicke = 0.0M;
              decimal breite = 0.0M;
              decimal hoehe = 0.0M;
              decimal laenge = 0.0M;

              if (!decimal.TryParse(dtArtikel.Rows[i]["Dicke"].ToString(), out dicke))
              {
                dicke = 0.0M;
              }
              if (!decimal.TryParse(dtArtikel.Rows[i]["Länge"].ToString(), out laenge))
              {
                laenge = 0.0M;
              }
              if (!decimal.TryParse(dtArtikel.Rows[i]["Breite"].ToString(), out breite))
              {
                breite = 0.0M;
              }
              if (!decimal.TryParse(dtArtikel.Rows[i]["Höhe"].ToString(), out hoehe))
              {
                hoehe = 0.0M;
              }
              clsArt.Dicke = dicke;
              clsArt.Breite = breite;
              clsArt.Laenge = laenge;
              clsArt.Hoehe = hoehe;
            }        
        }
      //
      //
      //
        public void SetArtikeldatenForUnion(ref clsArtikel clsArt)
        {
          if (clsArt.IDforUnion > 0)
          {
            DataTable dtArtikel = new DataTable("Artikeldaten");
            try
            {
              SqlDataAdapter ada = new SqlDataAdapter();
              SqlCommand Command = new SqlCommand();
              Command.Connection = Globals.SQLcon.Connection;
              ada.SelectCommand = Command;
              Command.CommandText = "SELECT ME, " +
                                            "gemGewicht, " +
                                            "tatGewicht " +
                                            "FROM Artikel WHERE ID='" + clsArt.IDforUnion + "' ";
              Globals.SQLcon.Open();
              ada.Fill(dtArtikel);
              Command.Dispose();
              Globals.SQLcon.Close();

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
            }
            finally
            {
              //ME und Gewichte addieren
              for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
              {
                decimal altGemGewicht1 = 0.0M;
                decimal altTatGewicht1 = 0.0M;
                Int32 altME1 = 0;

                decimal altGemGewicht2 = 0.0M;
                decimal altTatGewicht2 = 0.0M;
                Int32 altME2 = 0;

                decimal neuGemGewicht = 0.0M;
                decimal neuTatGewicht = 0.0M;
                Int32 neuME = 0;
                
                altGemGewicht1 = clsArt.gemGewicht;
                altTatGewicht1 = clsArt.tatGewicht;
                altME1 = clsArt.ME;

                altGemGewicht2 = (decimal)dtArtikel.Rows[i]["gemGewicht"];
                altTatGewicht2 = (decimal)dtArtikel.Rows[i]["tatGewicht"];
                altME2 = Convert.ToInt32((string)dtArtikel.Rows[i]["ME"]);

                neuGemGewicht = altGemGewicht1 + altGemGewicht2;
                neuTatGewicht = altTatGewicht1 + altTatGewicht2;
                neuME = altME1 + altME2;

                clsArt.ME = neuME;
                clsArt.gemGewicht = neuGemGewicht;
                clsArt.tatGewicht = neuTatGewicht;
                

                //Gesamt ME und Gewicht auf den zu vereinigenden Artikel
                clsArt.IDforDelete = clsArt.ID;
                clsArt.ID = clsArt.IDforUnion;                
              }
              dtArtikel.Dispose();
            }
          }
        }
        //
        //--- Update Artikel - AuftragPos wird aufgelöst - Artikel Pos=0 zugewiesen ----
        //
        public static void UpdateArtikelChangeAuftragPosToZero(Int32 Auftrag, Int32 AuftragPos)
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("Update Artikel SET AuftragPos ='0' WHERE AuftragID='" + Auftrag + "' AND AuftragPos='"+AuftragPos+"'");
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
        }
        //
        //
        //
        public Int32 GetIDbyArtikelDaten()
        {
          Int32 retVal=0;
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();

          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT ID FROM Artikel WHERE AuftragID='" + AuftragID + "' AND " +
                                                              "AuftragPos='" + AuftragPos + "' AND " +
                                                              "GArt='" + Gut + "' AND " +
                                                              "Dicke='" + Dicke.ToString().Replace(",", ".") + "' AND " +
                                                              "Breite='" + Breite.ToString().Replace(",", ".") + "' AND " +
                                                              "Laenge='" + Laenge.ToString().Replace(",", ".") + "' AND " +
                                                              "Hoehe='" + Hoehe.ToString().Replace(",", ".") + "' AND " +
                                                              "ME='" + ME + "' AND " +
                                                              "gemGewicht='" + gemGewicht.ToString().Replace(",", ".") + "'";

          Globals.SQLcon.Open();
          if (Command.ExecuteScalar() is DBNull)
          {
            retVal = 0;
          }
          else
          { 
            retVal=(Int32)Command.ExecuteScalar();
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }
          return retVal;
        }
        //
        //--------------- Get Artikeldaten zum Auftrag ----------------
        //
        public static string GetArtikelByAuftragID(int AuftragID)
        {
            string strReturn=string.Empty;

            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT GArt FROM Artikel WHERE AuftragID='" + AuftragID + "'";
            Globals.SQLcon.Open();
        
            object obj = Command.ExecuteScalar();

            if(obj == null)
            {
              strReturn=string.Empty;
            }
            else
            {
              strReturn=(string)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return strReturn;

        }
        //
        //----------------- Gesamt Gewicht des Auftras
        //
        public static Decimal GetSumGewichtArtikelForAuftrag(Int32 AuftragID)
        {
          Decimal retSummeGewicht = 0m;
          try
          {
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT SUM(gemGewicht) FROM Artikel WHERE AuftragID='" + AuftragID + "' ";
            Globals.SQLcon.Open();

            object returnVal = Command.ExecuteScalar().ToString();
            if (Command.ExecuteScalar() is DBNull)
            {
              retSummeGewicht = 0m;
            }
            else
            {
              retSummeGewicht = Convert.ToDecimal(Command.ExecuteScalar());

            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
              Command.Connection.Close();
            }
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.ToString());
          }
          return retSummeGewicht;
        }
        //
        //----------- Gesamt Gewicht für die Auftragposition  --------------------------
        //
        public static Decimal GetSumGewichtArtikelbyAuftragForAuftragPos(Int32 AuftragID, Int32 AuftragPos)
        {
          Decimal retSummeGewicht = 0m;
          try
          {
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT SUM(gemGewicht) FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='"+AuftragPos+"'";
            Globals.SQLcon.Open();

            object returnVal = Command.ExecuteScalar().ToString();
            if (Command.ExecuteScalar() is DBNull)
            {
              retSummeGewicht = 0m;
            }
            else
            { 
              retSummeGewicht=Convert.ToDecimal(Command.ExecuteScalar());
            
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
              Command.Connection.Close();
            }
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.ToString());
          }
          return retSummeGewicht;
        }
        //
        public static DataTable GetDataTableForArtikelGrd(Int32 AuftragID, Int32 AuftragPos)
        {
            DataTable dataTable = new DataTable("Artikel");
            dataTable.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            //Command.CommandText = "SELECT GArt, Dicke, Breite, Laenge, Hoehe, ME, Gewicht FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='"+AuftragPos+"'";
            Command.CommandText = "SELECT GArt as Gut, " +
                                         "Dicke as Dicke, " +
                                         "Breite as Breite, "+
                                         "Laenge as Länge, " +
                                         "Hoehe as Höhe, " +
                                         "ME as ME, " +
                                         "gemGewicht as gemGewicht, "+
                                         "tatGewicht as tatGewicht, " +
                                         "Werksnummer as Werksnummer, " + 
                                         "ID as ID " +
                                         "FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";

            Globals.SQLcon.Open();
            ada.Fill(dataTable);
            Command.Dispose();
            Globals.SQLcon.Close();
            return dataTable;
        }
        // geänderte Abfrage für Auftragsplitting
        /// </summary>
        /// <param name="AuftragID"></param>
        /// <param name="AuftragPos"></param>
        /// <returns></returns>
        public static DataTable GetDataTableForArtikelGrdSplitting(int AuftragID, int AuftragPos)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            //Command.CommandText = "SELECT GArt, Dicke, Breite, Laenge, Hoehe, ME, Gewicht FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='"+AuftragPos+"'";
            Command.CommandText = "SELECT GArt as Gut, " +
                                         "Dicke as Dicke, " +
                                         "Breite as Breite, " +
                                         "Laenge as Länge, " +
                                         "Hoehe as Höhe, " +
                                         "ME as ME, " +
                                         "gemGewicht as ArtikelGewicht, " +
                                         "tatGewicht as tatGewicht, " +
                                         "Werksnummer as Werksnummer, " + 
                                         "ID as ID " +
                                         "FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";

            Globals.SQLcon.Open();
            ada.Fill(dataTable);
            Command.Dispose();
            Globals.SQLcon.Close();
            return dataTable;
        }
        //
        //--------------- GET ID by Auftrag und Pos ----------------
        //
        public DataSet GetIDbyAuftragAndPos()
        {
          DataSet ds = new DataSet();
          ds.Clear();
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();

          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT ID FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='"+AuftragPos+"'";
          Globals.SQLcon.Open();
          ada.Fill(ds);
          Command.Dispose();
          Globals.SQLcon.Close();
          return ds;
        }
        //
        //------------- Artikel by ID löschen -------------------
        //
        public void DeleteArtikelByID(Int32 artikelID)
        {
          //--- initialisierung des sqlcommand---
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;

          //----- SQL Abfrage -----------------------
          Command.CommandText = "DELETE FROM Artikel WHERE ID='" +artikelID + "'";
          Globals.SQLcon.Open();
          Command.ExecuteNonQuery();
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          } 
        }
        //
        //---------------  Artikel delete  -----------------------
        //
        public static void DeleteArtikel(Int32 AuftragID, Int32 AuftragPos, Int32 MaxPos)
        {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = "DELETE FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='"+AuftragPos+"'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
              Command.Connection.Close();
            }
            if ((AuftragPos == 0) & (MaxPos>0))
            {
              ChangeAuftragPosToZero(AuftragID, MaxPos);  // Update MaxPos auf Pos 0
            }
        }
        //
        //--- Max AuftragPos auf 0 setzten --------------------------
        //
        public static void ChangeAuftragPosToZero(Int32 Auftrag, Int32 altePos)
        {
          try
          {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = ("Update Artikel SET AuftragPos ='0' " +
                                                          "WHERE AuftragID='" + Auftrag + "' AND AuftragPos='" + altePos + "'");
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
            System.Windows.Forms.MessageBox.Show(ex.ToString());

          }
        }
        //
        //
        //
        public void DeleteID(DataSet ds)
        {
          for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
          {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = "DELETE FROM Artikel WHERE ID='" +Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString()) + "'";
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
        //--- löscht die Datensätze direkt auf der Datenbank nachdem Sie ausgewählt wurden ------
        //
      /****
        public static void DeleteArtikelByID(Int32 _dbID)
        {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = "DELETE FROM Artikel WHERE ID='" + _dbID + "'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
              Command.Connection.Close();
            }
        }
       * ****/
        //
        //----------- Artikelanzahl derAuftragsposition  --------------------------
        //
        public static Int32 GetArtikelAnzahl(Int32 AuftragID, Int32 AuftragPos)
        {
          Int32 Anzahl = 0;
          try
          {
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT Count(ID) FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
            Globals.SQLcon.Open();

            object returnVal = Command.ExecuteScalar();
            if (returnVal is DBNull)
            {
              Anzahl = 0;
            }
            else
            {
              Anzahl = Convert.ToInt32(returnVal);

            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
              Command.Connection.Close();
            }
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.ToString());
          }
          return Anzahl;
        }
      //
      //
      //
      /*************************************************************************************************+
       * 
       *                                    Artikel Lager
       * 
       * ***********************************************************************************************/
      //
      //
      //
        public DataSet AddArtikelLager(DataSet ds)
        {
          DataTable dtArtikel = ds.Tables["Artikel"];

          if (dtArtikel.Rows.Count > 0)
          {
            for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
            {
              //-- Val der einzelen Rows ----
              AuftragID = 0;
              AuftragPos = 0;
              LVS_ID = (Int32)dtArtikel.Rows[i]["LVS"];
              Gut = dtArtikel.Rows[i]["Gueterart"].ToString();
              Dicke = Convert.ToDecimal(dtArtikel.Rows[i]["Dicke"].ToString());
              Breite = Convert.ToDecimal(dtArtikel.Rows[i]["Breite"].ToString());
              Laenge = Convert.ToDecimal(dtArtikel.Rows[i]["Laenge"].ToString());
              Hoehe = Convert.ToDecimal(dtArtikel.Rows[i]["Hoehe"].ToString());
              ME = Convert.ToInt32(dtArtikel.Rows[i]["ME"]);
              gemGewicht = Convert.ToDecimal(dtArtikel.Rows[i]["Nettogewicht"].ToString()); //Netto
              tatGewicht = Convert.ToDecimal(dtArtikel.Rows[i]["Bruttogewicht"].ToString()); //Brutto
              Werksnummer = dtArtikel.Rows[i]["Werksnummer"].ToString();
              CPNr = dtArtikel.Rows[i]["Produktionsnummer"].ToString();
              exBezeichnung = dtArtikel.Rows[i]["exBezeichnung"].ToString();
              Charge = dtArtikel.Rows[i]["Charge"].ToString();
              Bestellnummer = dtArtikel.Rows[i]["Bestellnummer"].ToString();
              exMaterialnummer = dtArtikel.Rows[i]["exMaterialnummer"].ToString();
              Position = dtArtikel.Rows[i]["Position"].ToString();
              Werk = dtArtikel.Rows[i]["Werk"].ToString();
              Halle = dtArtikel.Rows[i]["Halle"].ToString();
              Reihe = dtArtikel.Rows[i]["Reihe"].ToString();
              Platz = dtArtikel.Rows[i]["Platz"].ToString();

              if((bool)dtArtikel.Rows[i]["Schaden"])
              {
                iSchaden = 1;
              }
              else
              {
                iSchaden = 0;
              }
              Schadensbeschreibung=dtArtikel.Rows[i]["Schadensbeschreibung"].ToString();
              Einheit = dtArtikel.Rows[i]["Einheit"].ToString();

              //Abfrage neuer Artikel oder Update
              if (ExistsLVS_ID(LVS_ID))
              {
                  UpdateSQLArtikelLager();
              }
              else
              {
                  InsertSQLArtikelLager();
              }

 
            }
          }
          return ds;
        }
        //
        //-------------- SQL Insert Lagerartikel ----------------
        //
        private void InsertSQLArtikelLager()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("INSERT INTO Artikel (AuftragID, " +
                                                            "AuftragPos, " +
                                                            "LVS_ID, " +
                                                            "GArt, " +
                                                            "Dicke, " +
                                                            "Breite, " +
                                                            "Laenge, " +
                                                            "Hoehe , " +
                                                            "ME , " +
                                                            "Einheit , " +
                                                            "gemGewicht, " +
                                                            "tatGewicht, " +
                                                            "Werksnummer, " +
                                                            "Produktionsnummer, " +
                                                            "exBezeichnung, " +
                                                            "Charge, " +
                                                            "Bestellnummer, " +
                                                            "exMaterialnummer, " +
                                                            "Position, " +
                                                            "Schaden, " +
                                                            "Schadensbeschreibung, " +
                                                            "Werk, " +
                                                            "Halle, " +
                                                            "Reihe, " +
                                                            "Platz)" +

                                               "VALUES ('" + AuftragID + "','"
                                                           + AuftragPos + "','"
                                                           + LVS_ID + "','"
                                                           + Gut + "','"
                                                           + Dicke.ToString().Replace(",", ".") + "','"
                                                           + Breite.ToString().Replace(",", ".") + "','"
                                                           + Laenge.ToString().Replace(",", ".") + "','"
                                                           + Hoehe.ToString().Replace(",", ".") + "','"
                                                           + ME + "','"
                                                           + Einheit + "','"
                                                           + gemGewicht.ToString().Replace(",", ".") + "','"
                                                           + tatGewicht.ToString().Replace(",", ".") + "','"
                                                           + Werksnummer + "','"
                                                           + CPNr + "','"
                                                           + exBezeichnung + "','"
                                                           + Charge + "','"
                                                           + Bestellnummer + "','"
                                                           + exMaterialnummer + "','"
                                                           + Position + "','"
                                                           + iSchaden + "','"
                                                           + Schadensbeschreibung + "','"
                                                           + Werk + "','"
                                                           + Halle + "','"
                                                           + Reihe + "','"
                                                           + Platz + "')");

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
        }
        //
        //------------- Update SQL Lager Artikel ---------------
        //
        private void UpdateSQLArtikelLager()
        {
              try
              {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                Command.CommandText = "Update Artikel SET "+
                                        "AuftragID='" + AuftragID + "', " +
                                        "AuftragPos='" + AuftragPos + "', " +
                                        "LVS_ID='" + LVS_ID+ "', " +
                                        "GArt='" + Gut + "', " +
                                        "Dicke='" + Dicke.ToString().Replace(",", ".") + "', " +
                                        "Breite='" + Breite.ToString().Replace(",", ".") + "', " +
                                        "Laenge='" + Laenge.ToString().Replace(",", ".") + "'," +
                                        "Hoehe='" + Hoehe.ToString().Replace(",", ".") + "', " +
                                        "ME='" + ME + "', " +
                                        "Einheit='" + Einheit + "', " +
                                        "gemGewicht='" + gemGewicht.ToString().Replace(",", ".") + "', " +
                                        "tatGewicht='" + tatGewicht.ToString().Replace(",", ".") + "', " +
                                        "Werksnummer='" + Werksnummer + "', " +
                                        "Produktionsnummer='" + CPNr + "'," +
                                        "exBezeichnung='" + exBezeichnung + "', " +
                                        "Charge='" + Charge + "', " +
                                        "Bestellnummer='" + Bestellnummer + "', " +
                                        "Position='" + Position + "', " +
                                        "Schaden='" + iSchaden + "', " +
                                        "Schadensbeschreibung='" + Schadensbeschreibung + "', " +
                                        "Werk='" + Werk + "', " +
                                        "Halle='" + Halle + "', " +
                                        "Reihe='" + Reihe + "', " +
                                        "Platz='" + Platz + "' " +
                                        "WHERE LVS_ID='" + LVS_ID + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
              }
              catch (Exception ex)
              {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

              }
        }
      //
      //---------------- ID By Artikeldaten Lager -------------------
      //
      public Int32 GetIDbyArtikelDatenLager()
      {
        Int32 retVal = 0;
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();

        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID FROM Artikel WHERE AuftragID='" + AuftragID + "' AND " +
                                                            "AuftragPos='" + AuftragPos + "' AND " +
                                                            "GArt='" + Gut + "' AND " +
                                                            "Dicke='" + Dicke.ToString().Replace(",", ".") + "' AND " +
                                                            "Breite='" + Breite.ToString().Replace(",", ".") + "' AND " +
                                                            "Laenge='" + Laenge.ToString().Replace(",", ".") + "' AND " +
                                                            "Hoehe='" + Hoehe.ToString().Replace(",", ".") + "' AND " +
                                                            "ME='" + ME + "' AND " +
                                                            "gemGewicht='" + gemGewicht.ToString().Replace(",", ".") + "' AND " +
                                                            "tatGewicht='"+ tatGewicht.ToString().Replace(",", ".") + "' AND " +
                                                            "Werksnummer ='"+ Werksnummer + "' AND " +
                                                            "Produktionsnummer='"+CPNr+"' AND "+
                                                            "exBezeichnung='"+exBezeichnung+"' AND "+
                                                            "Halle='"+Halle+"' AND "+
                                                            "Reihe ='"+Reihe + "' AND "+
                                                            "Platz ='" + Platz + "' AND " +
                                                            "Schaden='" +iSchaden + "' AND " +
                                                            "Schadensbeschreibung ='"+Schadensbeschreibung+"'";


        Globals.SQLcon.Open();
        object obj = Command.ExecuteScalar();

        if ( obj is DBNull)
        {
          retVal = 0;
        }
        else
        {
          retVal = (Int32)obj;
        }
        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
        return retVal;
      }
      //
      //----------------- Check for Row -------------
      //
      private Int32 GetRowLVSNrIsIn(DataTable dt, Int32 LVS)
      {
        Int32 iRow=0;
        for(iRow=0; iRow<=dt.Rows.Count-1; iRow++)
        {
          Int32 tmpLVS = 0;
          tmpLVS=(Int32)dt.Rows[iRow]["LVSNr"];

          if(LVS==tmpLVS)
          {
            break;
          }
        }
        return iRow;
      }
      //
      //----------- Read Auftrag Einlagerung by ID -------------
      //
      //testen ob gelöscht werden kann
      public static DataTable GetLagerArtikelByEAID(ref DataTable dt, Int32 EAID)
      {
        dt.Clear();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT " +
                                        "Artikel.ID, " +
                                        "Artikel.LVS_ID, " +
                                        "Artikel.ME, " +
                                        "(SELECT SUM(aAnzahl) FROM EA WHERE Einlagerung='0' AND LVS_ID=Artikel.LVS_ID) as 'Rest ME', " +
                                        "Artikel.Einheit, " +
                                        "Artikel.GArt, " +
                                        "Artikel.Dicke, " +
                                        "Artikel.Breite, " +
                                        "Artikel.Laenge, " +
                                        "Artikel.Hoehe, " +

                                        "Artikel.gemGewicht as 'Nettogewicht', " +
                                        "Artikel.tatGewicht as 'Bruttogewicht', " +
                                        "(SELECT SUM(aNetto) FROM EA WHERE Einlagerung='0' AND LVS_ID=Artikel.LVS_ID) as 'Rest Nettogewicht', " +
                                        "(SELECT SUM(aBrutto) FROM EA WHERE Einlagerung='0' AND LVS_ID=Artikel.LVS_ID) as 'Rest Bruttogewicht', " +

                                        "Artikel.Werksnummer, " +
                                        "Artikel.Produktionsnummer as 'C/P', " +
                                        "Artikel.exBezeichnung, " +
                                        "Artikel.Schaden, " +
                                        "Artikel.Schadensbeschreibung " +

                                        "FROM Artikel " +
                                        "INNER JOIN EA ON EA.LVS_ID=Artikel.LVS_ID " +
                                        "WHERE EA_ID='" + EAID + "'" +
                                        "AND " +
                                        "(CONVERT(DECIMAL(18,2), Artikel.ME) - (SELECT SUM(aAnzahl) FROM EA WHERE Einlagerung='0' AND LVS_ID=Artikel.LVS_ID))>'0' " +
                                        "AND " +
                                        "(SELECT SUM(aNetto) FROM EA WHERE Einlagerung='0' AND LVS_ID=Artikel.LVS_ID)>'0' " +
                                        "AND " +
                                        "(SELECT SUM(aBrutto) FROM EA WHERE Einlagerung='0' AND LVS_ID=Artikel.LVS_ID)>'0'";

        ada.Fill(dt);
        Command.Dispose();
        Globals.SQLcon.Close();
        return dt;
      }
      //
      //----------- MAX LVS in DB Artikel für Listenstart -------------------
      //
      public static Int32 GetMaxLVS()
      {
        Int32 iMaxLVS = 0;
        try
        {
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT MAX(LVS_ID) FROM Artikel";
          Globals.SQLcon.Open();

          object returnVal = Command.ExecuteScalar().ToString();
          if (returnVal is DBNull)
          {
            iMaxLVS = 0;
          }
          else
          {
            iMaxLVS = Convert.ToInt32(returnVal);

          }
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.ToString());
        }
        return iMaxLVS;
      }
    //
    //
    //
      public static bool ExistsLVS_ID(Int32 lvsID)
      {
          bool IsIn = false;
          try
          {
              SqlDataAdapter ada = new SqlDataAdapter();
              SqlCommand Command = new SqlCommand();
              Command.Connection = Globals.SQLcon.Connection;
              ada.SelectCommand = Command;
              Command.CommandText = "SELECT ID FROM Artikel WHERE Artikel.LVS_ID='" + lvsID + "'";
              Globals.SQLcon.Open();

              object obj = Command.ExecuteScalar();

              if (obj != null)
              {
                  IsIn = true;
              }

              Command.Dispose();
              Globals.SQLcon.Close();
              if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
              {
                  Command.Connection.Close();
              }

          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.ToString());
          }
          return IsIn;
      }
    }
}
