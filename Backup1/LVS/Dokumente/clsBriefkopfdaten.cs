using System;
using System.Windows.Forms;


namespace LVS.Dokumente
{
    public class clsBriefkopfdaten
    {
        public Globals._GL_USER GL_User;
        ///<summary>Globals / clsINI.clsINI</summary>
        ///<remarks>Config INI</remarks>
        public static clsINI.clsINI INI_BK;
        internal clsMandanten Mandanten;

        //Absender über Adressfeld
        private string _FirmaAbs;
        private string _StrAbs;
        private string _PLZOrtAbs;
        private string _Absender;
        private decimal _MandantenID;

        public string FirmaAbs
        {
            get { return _FirmaAbs; }
            set { _FirmaAbs = value; }
        }
        public string StrAbs
        {
            get { return _StrAbs; }
            set { _StrAbs = value; }
        }
        public string PLZOrtAbs
        {
            get { return _PLZOrtAbs; }
            set { _PLZOrtAbs = value; }
        }
        public string Absender
        {
            get { return _Absender; }
            set { _Absender = value; }
        }
        public decimal MandantenID
        {
            get { return _MandantenID; }
            set { _MandantenID = value; }
        }


        //Adresse FIrma im Briefkopf 
        private string _Name1BK;
        private string _Name2BK;
        private string _StrBK;
        private string _PLZBK;
        private string _OrtBK;
        private string _SteuerBK;
        private string _USTBK;

        public string Name1BK
        {
            get { return _Name1BK; }
            set { _Name1BK = value; }
        }
        public string Name2BK
        {
            get { return _Name2BK; }
            set { _Name2BK = value; }
        }
        public string StrBK
        {
            get { return _StrBK; }
            set { _StrBK = value; }
        }
        public string PLZBK
        {
            get { return _PLZBK; }
            set { _PLZBK = value; }
        }
        public string OrtBK
        {
            get { return _OrtBK; }
            set { _OrtBK = value; }
        }
        public string SteuerBK
        {
            get { return _SteuerBK; }
            set { _SteuerBK = value; }
        }
        public string USTBK
        {
            get { return _USTBK; }
            set { _USTBK = value; }
        }

        //Logos
        private string _LogoPfad;
        private string _ZertPfad;

        public string LogoPfad
        {
            get { return _LogoPfad; }
            set { _LogoPfad = value; }
        }
        public string ZertPfad
        {
            get { return _ZertPfad; }
            set { _ZertPfad = value; }
        }

        //Bankverbindungen
        private string _Bank1;
        private string _Bank2;
        private string _Bank3;

        private string _Kto1;
        private string _Kto2;
        private string _Kto3;

        private string _BLZ1;
        private string _BLZ2;
        private string _BLZ3;

        private string _IBAN1;
        private string _IBAN2;
        private string _IBAN3;

        private string _BIC1;
        private string _BIC2;
        private string _BIC3;


        private string _HR;
        private string _Text;
        private string _TextLieferschein;
        private string _Text20120224;
        private string _Text20111024;
        public string Bank1
        {
            get { return _Bank1; }
            set { _Bank1 = value; }
        }
        public string Bank2
        {
            get { return _Bank2; }
            set { _Bank2 = value; }
        }
        public string Bank3
        {
            get { return _Bank3; }
            set { _Bank3 = value; }
        }
        public string Kto1
        {
            get { return _Kto1; }
            set { _Kto1 = value; }
        }
        public string Kto2
        {
            get { return _Kto2; }
            set { _Kto2 = value; }
        }
        public string Kto3
        {
            get { return _Kto3; }
            set { _Kto3 = value; }
        }

        public string BLZ1
        {
            get { return _BLZ1; }
            set { _BLZ1 = value; }
        }
        public string BLZ2
        {
            get { return _BLZ2; }
            set { _BLZ2 = value; }
        }
        public string BLZ3
        {
            get { return _BLZ3; }
            set { _BLZ3 = value; }
        }

        public string IBAN1
        {
            get { return _IBAN1; }
            set { _IBAN1 = value; }
        }
        public string IBAN2
        {
            get { return _IBAN2; }
            set { _IBAN2 = value; }
        }
        public string IBAN3
        {
            get { return _IBAN3; }
            set { _IBAN3 = value; }
        }

        public string BIC1
        {
            get { return _BIC1; }
            set { _BIC1 = value; }
        }
        public string BIC2
        {
            get { return _BIC2; }
            set { _BIC2 = value; }
        }
        public string BIC3
        {
            get { return _BIC3; }
            set { _BIC3 = value; }
        }

        public string HR
        {
            get { return _HR; }
            set { _HR = value; }
        }
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }


        public string TextLieferschein
        {
            get { return _TextLieferschein; }
            set { _TextLieferschein = value; }
        }
        public string Text20120224
        {
            get { return _Text20120224; }
            set { _Text20120224 = value; }
        }
        public string Text20111024
        {
            get { return _Text20111024; }
            set { _Text20111024 = value; }
        }

        /*****************************************************************************************************
         *                              Textdatei Briefkopfdaten schreiben / lesen
         * 
         * - Dateiname = Username.txt
         * 
         *      - Zeile 1   Firma Absender 
         *      - Zeile 2   Str Absender
         *      - Zeile 3   PLZ/Ort Absender
         *      
         *      - Zeile 10  Name1 BK
         *      - Zeile 11  Name2 BK
         *      - Zeile 12  Str BK
         *      - Zeile 13  PLZ BK
         *      - Zeile 14  Ort BK
         *      
         *      - Zeile 15  User / Ansprechpartner
         *      - Zeile 16  Telefon
         *      - Zeile 17  Fax
         *      - Zeile 18  Mail
         *      
         *      - Zeile 20  Logo Pfad
         *      - Zeile 21  Zert. Pfad   
         *      
         *      - Zeile 25  Bank1
         *      - Zeile 26  Kto1
         *      - Zeile 27  BLZ1
         *      
         *      - Zeile 28  Bank2
         *      - Zeile 29  Kto2
         *      - Zeile 30  BLZ2
         *      
         *      - Zeile 31  Bank3
         *      - Zeile 32  Kto3
         *      - Zeile 33  BLZ3
         *      
         *        
         *      - Zeile 35    HR
         *      - Zeile 36    Text
         *      - Zeile 37   UmsatzsteuerID
         *      - Zeile 38   Steuernummer
         *      
         * 
         * 
         * **************************************************************************************************/
        clsTextDatei td = new clsTextDatei();

        private string _User;
        private Int32 _Zeile;
        private string _Telefon;
        private string _Fax;
        private string _Mail;
        private string _Filename;


        public string User
        {
            get
            {
                _User = "TestUser";   //momentane Testuser 
                return _User;
            }
            set { _User = value; }
        }
        public string Telefon
        {
            get { return _Telefon; }
            set { _Telefon = value; }
        }
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        public string Mail
        {
            get { return _Mail; }
            set { _Mail = value; }
        }
        public Int32 Zeile
        {
            get { return _Zeile; }
            set { _Zeile = value; }
        }
        public string Filename
        {
            get
            {
                return _Filename;
            }
            set { _Filename = value; }
        }


        //

        //
        /*************************************************************************+
         * Mandant wird übernommen:
         * - Fahrzeugliste im ComboBox Besitzer
         * - frmFahrzeuge in ComboBox Besitzer
         * 
         * ************************************************************************/
        //
        public enum enumMandant
        {
            //FastSpedGmbH=1,
            //FastSpedLagerei=2,
            HeisiepSpedition = 1,
            HeisiepLagerLogistik = 2,
        }
        public void InitBriefkopfDaten()
        {
            Mandanten = new clsMandanten();
            Mandanten.GL_User = this.GL_User;
            Mandanten.ID = MandantenID;
            Mandanten.GetMandantByID();

            //Briefkopf INIFilenamee= 'Mandant'+MandantenID +'_BriefKopf.ini;
            string strIniFile = "Mandant" + Mandanten.ID.ToString() + "_Briefkopf.ini";
            INI_BK = new clsINI.clsINI(Application.StartupPath + "\\" + strIniFile);

            //Absender
            FirmaAbs = INI_BK.ReadString("ABSENDER", "FIRMA");
            StrAbs = INI_BK.ReadString("ABSENDER", "STR");
            PLZOrtAbs = INI_BK.ReadString("ABSENDER", "PLZORT");
            Absender = FirmaAbs + "-" + StrAbs + "-" + PLZOrtAbs;
            /**
            FirmaAbs    ="Bernhard Heisiep Spedition GmbH & CO. KG";
            StrAbs      ="Postfach 1 07";
            PLZOrtAbs   ="57413 Finnentrop";
            Absender    = FirmaAbs + "-" + StrAbs + "-" + PLZOrtAbs;
            ***/

            //Adresse
            Name1BK = INI_BK.ReadString("ADRESSE", "NAME1");
            Name2BK = INI_BK.ReadString("ADRESSE", "NAME2");
            StrBK = INI_BK.ReadString("ADRESSE", "STR");
            PLZBK = INI_BK.ReadString("ADRESSE", "PLZ");
            OrtBK = INI_BK.ReadString("ADRESSE", "ORT");
            /***
            Name1BK     ="Bernhard Heisiep Lastwagentransporte und";
            Name2BK     ="Spedition GmbH & Co KG";
            StrBK       ="Bamenohler Str. 93";
            PLZBK       ="57413";
            OrtBK       ="Finnentrop";
            ****/

            //

            Telefon = INI_BK.ReadString("HEADER", "TEL");
            Fax = INI_BK.ReadString("HEADER", "FAX");
            Mail = INI_BK.ReadString("HEADER", "MAIL");
            USTBK = INI_BK.ReadString("HEADER", "USTID");
            SteuerBK = INI_BK.ReadString("HEADER", "STEUERID");
            /****
            Telefon     ="Tel: +49(0) 27 21 - 51 08 0";
            Fax         ="Fax: +49(0) 27 21 - 51 08 799";
            Mail        ="";
            USTBK       ="UST-ID: DE 126 180 651";
            SteuerBK    ="Steuer-Nr.: 338/5861/0030";
             * 
            ***/
            LogoPfad = Application.StartupPath + INI_BK.ReadString("IMAGE", "LOGO1");
            ZertPfad = Application.StartupPath + INI_BK.ReadString("IMAGE", "ZERT");

            /**
            LogoPfad = Application.StartupPath + "\\Heisiep\\LogoSpedition.jpg";
            ZertPfad = Application.StartupPath + "\\Heisiep\\Zertifizierung_9001.jpg";
            ***/

            Bank1 = INI_BK.ReadString("BANKVERBINDUNG", "BANK1");
            Bank2 = INI_BK.ReadString("BANKVERBINDUNG", "BANK2");
            Bank3 = INI_BK.ReadString("BANKVERBINDUNG", "BANK3");

            Kto1 = INI_BK.ReadString("BANKVERBINDUNG", "KTO1");
            Kto2 = INI_BK.ReadString("BANKVERBINDUNG", "KTO2");
            Kto3 = INI_BK.ReadString("BANKVERBINDUNG", "KTO3");

            BLZ1 = INI_BK.ReadString("BANKVERBINDUNG", "BLZ1");
            BLZ2 = INI_BK.ReadString("BANKVERBINDUNG", "BLZ2");
            BLZ3 = INI_BK.ReadString("BANKVERBINDUNG", "BLZ3");

            /***
            Bank1       ="Sparkasse Finnentrop";
            Bank2       ="Volksbank Grevenbrück";
            Bank3       ="Postbank Dortmund";

            Kto1        ="Konto 61 55";
            Kto2        ="Konto 1000 639 300";
            Kto3        ="Konto 17 670 468";

            BLZ1        ="BLZ 462 515 90";
            BLZ2        ="BLZ 462 616 07";
            BLZ3        ="BLZ 440 100 46";
            ***/

            /***
                HR = string.Empty;
                             //"Hanndelsregister Siegen HRA 6789, "+
                             //"pers.haft.Gesellschafterin Heisiep GmbH, Finnentrop, HR B 5825 Siegen,";
           
          
              Text20111024 = "Wir arbeiten ausschließlich auf Grundlage der Allgemeinen Deutschen Spediteurbedingungen - ADSp - " +
                         "jeweils neueste Fassung. Diese Beschränken in Ziffer 23 ADSp die gesetzliche Haftung für Güterschäden anch " +
                         "§ 431 HGB für Schäden in speditionellem Gewahrsam auf 5 €/kg. " +
                         "Die Verkehrshaftungsversicherung ist über HDI-Gerling Firmen- und Privat Versicherung AG, Riethorst 2, 30659 Hannover gezeichnet. " +
                         "Gerichtsstand und Erfüllungsort ist für beide Teile Lennestadt-Grevenbrück. " +
                         "Hanndelsregister Siegen HRA 6789,pers.haft.Gesellschafterin Heisiep GmbH, Finnentrop, HR B 5825 Siegen, "+
                         "Geschäftsführer: Bernhard Heisiep, Bernd Heisiep";
               //2012_02_24_ geändert lt Herr Heisiep
               Text20120224 = "Wir arbeiten ausschließlich auf Grundlage der Allgemeinen Deutschen Spediteurbedingungen - ADSp - " +
                              "jeweils neuste Fassung. Diese beschränken in Ziffer 23 ADSp die gesetzliche Haftung für Güterschäden nach " +
                              "§ 431 HGB für Schäden in speditionellem Gewahrsam auf 5 €/kg. Die Verkehrshaftungsversicherung besteht bei der " +
                              "HDI-Gerling Firmen und Privat Versicherung AG, Riethorst 2, 30659 Hannover.";


               TextLieferschein = Text20120224;
             * ***/
            Text = INI_BK.ReadString("FOOT", "TEXT1");





            /*******************************************************************************************/

            /****
                FirmaAbs = "Spedition Karl Viegener KG";
                StrAbs = "Windhauser Str. 53";
                PLZOrtAbs = "57439 Attendorn";
                Absender = FirmaAbs + "-" + StrAbs + "-" + PLZOrtAbs;

                Name1BK = "Spedition Karl Viegener KG";
                Name2BK = "";
                StrBK = "Windhauser Str. 53";
                PLZBK = "57439";
                OrtBK = "Attendorn";


                Telefon = "Tel: +49(0) 27 22 - 59 22 oder 59 23";
                Fax = "Fax: +49(0) 27 22 - 5 30 22";
                Mail = "Info@Karl-Viegener.de";

                LogoPfad = Application.StartupPath + "\\KarlViegener\\Logo_KarlViegener.jpg";
                //ZertPfad = Application.StartupPath + "\\Heisiep\\Zertifizierung_9001.jpg";


                Bank1 = "Sparkasse Attendorn";
                Bank2 = "Volksbank Attendorn";
                Bank3 = "Commerzbank Attendorn";

                Kto1 = "Konto 11 11 111";
                Kto2 = "Konto 22 22 222";
                Kto3 = "Konto 33 33 333";

                BLZ1 = "BLZ 111 111 11";
                BLZ2 = "BLZ 222 222 22";
                BLZ3 = "BLZ 333 333 33";

                HR = "Eingetragen im Amtsgericht Olpe - HRB Nummer: 7414";
                Text = "Geschäftsführer: Gerhard Schweizer";
                USTBK = "UST-ID: DE 126 180 483";
                SteuerBK = "Steuer-Nr.: 338/5860/0922";
             ****/
        }
        //
        //
        //


    }
}
