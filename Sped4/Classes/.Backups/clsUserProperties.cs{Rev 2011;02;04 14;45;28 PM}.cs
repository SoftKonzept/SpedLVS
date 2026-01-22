using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sped4;
using Sped4.Classes;
using Sped4.Controls;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Sped4.Classes
{
  class clsUserProperties
  {
    /***************************************************************************************
     * Die Usereinstellungen werden pro User in einer txt.Datei gespeichert
     * Infos:
     * - Dateiname = Username.txt
     *                           - Zeile 1   Username
     *                            
     * - Bereiche
     *        - User                  Zeile  2   Beschreibung       Zeile  1
     *        - Disposition_Rowheit   Zeile 11   Name/Beschreibung  Zeile 10
     *                  
     * 

     * ***/

    clsTextDatei td = new clsTextDatei();

    private string _User;
    private Int32 _Zeile;
    private string _Bereich;
    private Int32 _RowHeight;
    private string _Filename;


    public string User
    {
      get {
            _User = "TestUser";   //momentane Testuser 
            return _User; }
      set { _User = value; }
    }
    public Int32 Zeile
    {
      get { return _Zeile; }
      set { _Zeile = value; }
    }
    public string Bereich
    {
      get { return _Bereich; }
      set { _Bereich = value; }
    }

    public string Filename
    {
      get 
      {
        _Filename = User + ".txt";
        return _Filename; }
      set { _Filename = value; }
    }

    public enum enumBereich
    {
      Disposition_RowHeight,
    }

    //
    //
    //
    private Int32 GetZeile(string SuchBereich)
    {
      switch (SuchBereich)
      {
        case "User":
          Zeile = 2;
          break;
        case "Disposition_RowHeight":
          Zeile = 11;
          break;
      }
      return Zeile;
    }
    //-liest die Inhalte aus der Datei aus ----
    //
    //
    public void ReadProperties()
    { 
      string Path = Application.StartupPath+"\\"+Filename;
      if (File.Exists(Path))
      {
        User = td.ReadLine(Path, GetZeile("User"));
        RowHeight = Convert.ToInt32(td.ReadLine(Path, GetZeile("Disposition_RowHeight")));
      }
      else
      {
        WriteProperties(true);
      }
    
    }
    //-- schreibt die Inhalte in die Datei  -------------
    //
    //
    public void WriteProperties(bool replace)
    {
      string Path = Application.StartupPath + Filename;
      td.WriteFile(Path, "");
      //User 
      td.WriteLine(Path, GetZeile("User")-1,"[Username]", replace);
      td.WriteLine(Path, GetZeile("User"), User, replace);
      //Disposition RowHeigth
      td.WriteLine(Path, GetZeile("Disposition_RowHeight")-1,"[Disposition_RowHeight]", replace);
      td.WriteLine(Path, GetZeile("Disposition_RowHeight"), RowHeight.ToString(), replace);

    }


    /***************************************************************************************
     * 
     * 
     *  Write in XML User Properties
     *  
     * *************************************************************************************/

    public Globals._GL_USER GL_User;

    private string _strXmlPath;

    public string strXmlPath
    {
      get
      {
        string strPath = Application.StartupPath;
        string strDateiname = "UserProperties.xml";
        _strXmlPath = strPath + "\\" + strDateiname;
        return _strXmlPath ;
      }
      set { _strXmlPath = value; }
    }
    //im Modul Disposition
    public Int32 RowHeight
    {
      get
      {
        if (_RowHeight < 71)
        {
          //Standardwert, sonst zu klein
          _RowHeight = 71;
        }
        return _RowHeight;
      }
      set { _RowHeight = value; }
    }




    //
    //
    //
    public void WriteXMLUserProperties()
    {
      if (GL_User.User_ID != null)
      {
        XmlWriter writer = XmlWriter.Create(strXmlPath);
        //Schreiben des Dokuments
        writer.WriteStartDocument();

        writer.WriteStartElement("Benutzereinstellungen");
        CreateNodeUserID(ref writer);
        writer.WriteEndElement();
        
        writer.WriteEndDocument();
        writer.Close();
      }
    }
    //
    //--------- Erstellt die einzelnen Elemente für die Daten im XML  -------------------
    //
    private void CreateNodeUserID(ref XmlWriter writeXML)
    {  

       writeXML.WriteStartElement("USER");
       //writeXML.WriteStartAttribute("ID",GL_User.User_ID.ToString());
       writeXML.WriteStartAttribute("ID");
       writeXML.WriteString(GL_User.User_ID.ToString());
       writeXML.WriteEndAttribute();
         
          //CreateNodeDispo(ref writeXML);
          //CreateNodeLager(ref writeXML);

       writeXML.WriteEndElement();
       writeXML.WriteStartElement("Standardeinstellungen");
         CreateNodeDispo(ref writeXML);
         CreateNodeLager(ref writeXML);
       writeXML.WriteEndElement();
    }
    //
    private void CreateNodeDispo(ref XmlWriter writeXML)
    {
      //MODUL
      writeXML.WriteStartElement("Modul");
      writeXML.WriteStartAttribute("Modulname");
      writeXML.WriteString("Disposition");
      writeXML.WriteEndAttribute();


        //RowHight - Höhe des Dispoplans jedes einzelnen Fahrzeugs
        writeXML.WriteStartElement("RowHight");
        writeXML.WriteString(RowHeight.ToString());
        writeXML.WriteEndElement();

      writeXML.WriteEndElement();
    }
    //
    private void CreateNodeLager(ref XmlWriter writeXML)
    {
      //MODUL
      writeXML.WriteStartElement("Modul");
      writeXML.WriteStartAttribute("Modulname");
      writeXML.WriteString("Lager");
      writeXML.WriteEndAttribute();

      writeXML.WriteEndElement();
    }


    //
    //--------------------------- XML auslesen ---------------------------
    //
    //
    public void ReadXMLUserProperties()
    {
      if (File.Exists(strXmlPath))
      {
        XmlReader read = XmlReader.Create(strXmlPath);

       // string test = read.ReadContentAsString();
        //string test1 = read.ReadElementContentAsString();
        while (read.Read())
        {
          string qwe = read.Name; //Gibt Elementenname aus
          string ert = read.Value;
          string dfsa = read.GetAttribute("ID"); // Gibt den jeweiligen Attributwert an
          string sfs = read.GetAttribute("Modulname");
        }
        read.Close();
      }

    }
  }
}
