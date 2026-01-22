using LVS;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace Sped4.Classes
{
    class clsPersonal
    {
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get { return _BenutzerID; }
            set { _BenutzerID = value; }
        }
        //************************************
        //private DateTime _seit = default(DateTime);
        private DateTime _bis;
        public Image _Passbild;
        public Image tmpPassbild { get; set; }
        public decimal ID { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Str { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }
        public string Abteilung { get; set; }
        public string Beruf { get; set; }
        public DateTime seit { get; set; }
        public DateTime bis
        {
            get
            {

                //_bis = Convert.ToDateTime("31.12.9999");
                return _bis;
            }
            set { _bis = value; }
        }
        public string Notiz { get; set; }
        public string Anrede { get; set; }
        public Image Passbild
        {
            get
            {
                clsImages img = new clsImages();

                try
                {
                    SqlDataAdapter ada = new SqlDataAdapter();
                    SqlCommand Command = new SqlCommand();
                    Command.Connection = Globals.SQLcon.Connection;
                    ada.SelectCommand = Command;
                    Command.CommandText = "SELECT Passbild FROM Personal WHERE ID='" + ID + "'";
                    Globals.SQLcon.Open();
                    if (Command.ExecuteScalar() is DBNull)
                    {

                    }
                    else
                    {
                        img.byteArrayIn = (byte[])Command.ExecuteScalar();
                        //img.byteArrayToImage();
                        _Passbild = img.ConvertByteArrayToImage();

                    }
                    //_Passbild.Save("C:\\Bild.jpg");

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
                return _Passbild;
            }
            set { _Passbild = value; }
        }
        //
        //

        //************************************************************************************
        //**********              Methoden
        //***********************************************************************************
        //
        ///<summary>clsPersonal/ ReadDataByID</summary>
        ///<remarks>update the DB Personal</remarks>
        public void Fill()
        {
            string strSQL = "SELECT * FROM Personal WHERE ID=" + this.ID + ";";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Personal");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;
                this.Name = dt.Rows[i]["Name"].ToString();
                this.Vorname = dt.Rows[i]["Vorname"].ToString();
                this.Str = dt.Rows[i]["Str"].ToString();
                this.PLZ = dt.Rows[i]["PLZ"].ToString();
                this.Ort = dt.Rows[i]["Ort"].ToString();
                this.Telefon = dt.Rows[i]["Telefon"].ToString();
                this.Mail = dt.Rows[i]["Mail"].ToString();
                this.Abteilung = dt.Rows[i]["Abteilung"].ToString();
                this.Beruf = dt.Rows[i]["Beruf"].ToString();
                this.seit = (DateTime)dt.Rows[i]["seit"];
                this.Notiz = dt.Rows[i]["Notiz"].ToString();
                this.Anrede = dt.Rows[i]["Anrede"].ToString();
            }
        }
        ///<summary>clsPersonal/ GetPersonalList</summary>
        ///<remarks></remarks>
        public static DataTable GetPersonalList(Globals._GL_USER myGLUser, bool _aktuelleListe)
        {
            string strSQL = string.Empty;
            if (_aktuelleListe)
            {
                strSQL = "SELECT " +
                                              "ID, " +
                                              "Anrede, " +
                                              "Name, " +
                                              "Vorname, " +
                                              "Str as 'Strasse'," +
                                              "PLZ, " +
                                              "Ort, " +
                                              "Abteilung, " +
                                              "Beruf " +
                                                          //"seit as 'Beschäftigt ab' " +
                                                          "FROM Personal WHERE bis='" + DateTime.MaxValue + "' " +
                                                          "ORDER BY Name";
            }
            else
            {
                strSQL = "SELECT " +
                                              "ID, " +
                                              "Anrede, " +
                                              "Name, " +
                                              "Vorname, " +
                                              "Str as 'Strasse'," +
                                              "PLZ, " +
                                              "Ort, " +
                                              "Abteilung, " +
                                              "Beruf " +
                                                          //"seit as 'Beschäftigt seit', " +
                                                          //"bis as 'Beschäftigt bis' "+
                                                          "FROM Personal ORDER BY Name";
            }
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Personal");
            return dt;
        }
        ///<summary>clsPersonal/ GetPersonalList</summary>
        ///<remarks>add new Personalitem to DB</remarks>
        public void AddItem()
        {
            try
            {
                string strSQL = ("INSERT INTO Personal (Name, Vorname, Str, PLZ, Ort, Telefon, Mail, Abteilung, Beruf, seit, bis, Notiz, Anrede) " +
                                                "VALUES (" +
                                                            "'" + Name + "'" +
                                                            ",'" + Vorname + "'" +
                                                            ",'" + Str + "'" +
                                                            ",'" + PLZ + "'" +
                                                            ",'" + Ort + "'" +
                                                            ",'" + Telefon + "'" +
                                                            ",'" + Mail + "'" +
                                                            ",'" + Abteilung + "'" +
                                                            ",'" + Beruf + "'" +
                                                            ",'" + seit + "'" +
                                                            ",'" + DateTime.MaxValue + "'" +
                                                            ",'" + Notiz + "'" +
                                                            ",'" + Anrede + "'" +
                                                            ")");

                strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    this.ID = decTmp;
                    clsImages img = new clsImages();
                    img.ImageIn = tmpPassbild;
                    img.WriteToPersonalImage(ID);

                    //Add Logbucheintrag Eintrag
                    string Beschreibung = "Personal: " + Name + ", " + Vorname + " hinzugefügt";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
        }
        ///<summary>clsPersonal/ GetPersonalList</summary>
        ///<remarks>update the DB Personal</remarks>
        public void updatePersonal()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand UpCommand = new SqlCommand();
                UpCommand.Connection = Globals.SQLcon.Connection;
                UpCommand.CommandText = "Update Personal SET " +
                                                      "Name='" + Name + "', " +
                                                      "Vorname='" + Vorname + "', " +
                                                      "Str='" + Str + "', " +
                                                      "PLZ='" + PLZ + "', " +
                                                      "Ort='" + Ort + "', " +
                                                      "Telefon='" + Telefon + "', " +
                                                      "Mail='" + Mail + "', " +
                                                      "Abteilung='" + Abteilung + "'," +
                                                      "Beruf='" + Beruf + "', " +
                                                      "seit='" + seit + "', " +
                                                      "bis='" + bis + "', " +
                                                      "Notiz='" + Notiz + "', " +
                                                      "Anrede='" + Anrede + "' " +
                                                                                 "WHERE ID='" + ID + "'";

                Globals.SQLcon.Open();
                UpCommand.ExecuteNonQuery();
                UpCommand.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), Beschreibung);

            }
            finally
            {
                //Add Logbucheintrag Update
                string Beschreibung = "Personal: " + Name + ", " + Vorname + " geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        ///<summary>clsPersonal/ ReadDataByID</summary>
        ///<remarks>update the DB Personal</remarks>
        public static DataSet ReadDataByID(decimal dataID)
        {
            DataSet ds = new DataSet();
            ds.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT * FROM Personal WHERE ID='" + dataID + "'";

            ada.Fill(ds);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();

            return ds;
        }

        //
        //------------ Aufliegerdaten für Fahrerliste Disposition  ------------------
        //
        public static DataTable GetFahrerListe()
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                            "ID, " +
                                            "Name, " +
                                            "Vorname " +
                                            "FROM Personal WHERE Abteilung='Fahrpersonal' AND bis='" + DateTime.MaxValue + "' ORDER BY Name ";

            ada.Fill(dataTable);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();
            return dataTable;
        }
        //
        //---------- Name zur Personal ID  -----------------------
        //
        public static string GetNameByID(decimal ID)
        {
            string Name = string.Empty;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = ("SELECT Name  FROM Personal WHERE ID = " + ID);
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();

            if (obj != null)
            {
                Name = (string)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return Name;
        }
        //
        //---------- Name zur Personal ID  -----------------------
        //
        private decimal GetIDbyDaten()
        {
            decimal ID = 0;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = ("SELECT ID FROM Personal WHERE Name='" + Name + "' AND " +
                                                                 "Vorname='" + Vorname + "' AND " +
                                                                 "Str='" + Str + "' AND " +
                                                                 "PLZ='" + PLZ + "' AND " +
                                                                 "Ort ='" + Ort + "' AND " +
                                                                 "Telefon ='" + Telefon + "' AND " +
                                                                 "Mail ='" + Mail + "' AND " +
                                                                 "Abteilung  ='" + Abteilung + "' AND " +
                                                                 "Beruf ='" + Beruf + "' AND " +
                                                                 "seit ='" + seit + "' AND " +
                                                                 "bis ='" + bis + "' AND " +
                                                                 "Notiz ='" + Notiz + "' AND " +
                                                                 "Anrede ='" + Anrede + "' ");

            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();

            if (obj != null)
            {
                ID = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return ID;
        }
        //
        //---------- Vorname zur Personal ID  -----------------------
        //
        public static string GetVornameByID(decimal ID)
        {
            string Vorname = string.Empty;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = ("SELECT Vorname  FROM Personal WHERE ID = " + ID);
            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();

            if (obj != null)
            {
                Vorname = (string)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return Vorname;
        }
        //
        //------ löschen Datensatz  -----------------------
        //
        public void DeletePersonal()
        {
            //Add Logbucheintrag Löschen
            string Name = GetNameByID(ID);
            string Vorname = GetVornameByID(ID);
            string Beschreibung = "Personal: " + Name + ", " + Vorname + " ID: " + ID + " gelöscht";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "DELETE FROM Personal WHERE ID='" + ID + "'";
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
}
