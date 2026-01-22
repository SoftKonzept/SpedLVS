using Common.Models;
using LVS.ASN;
using LVS.ASN.EDIFACT;
using LVS.Communicator.EdiVDA;
using LVS.Constants;
using LVS.Models;
using LVS.Uniport;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;


namespace LVS
{
    public class clsASN
    {
        //VDA 4905
        public const string const_VDA4905SatzField_SATZ511F01 = "SATZ511F01";
        public const string const_VDA4905SatzField_SATZ511F02 = "SATZ511F02";
        public const string const_VDA4905SatzField_SATZ511F03 = "SATZ511F03";
        public const string const_VDA4905SatzField_SATZ511F04 = "SATZ511F04";
        public const string const_VDA4905SatzField_SATZ511F05 = "SATZ511F05";
        public const string const_VDA4905SatzField_SATZ511F06 = "SATZ511F06";
        public const string const_VDA4905SatzField_SATZ511F07 = "SATZ511F07";
        public const string const_VDA4905SatzField_SATZ511F08 = "SATZ511F08";
        public const string const_VDA4905SatzField_SATZ511F09 = "SATZ511F09";

        public const string const_VDA4905SatzField_SATZ512F01 = "SATZ512F01";
        public const string const_VDA4905SatzField_SATZ512F02 = "SATZ512F02";
        public const string const_VDA4905SatzField_SATZ512F03 = "SATZ512F03";
        public const string const_VDA4905SatzField_SATZ512F04 = "SATZ512F04";
        public const string const_VDA4905SatzField_SATZ512F05 = "SATZ512F05";
        public const string const_VDA4905SatzField_SATZ512F06 = "SATZ512F06";
        public const string const_VDA4905SatzField_SATZ512F07 = "SATZ512F07";
        public const string const_VDA4905SatzField_SATZ512F08 = "SATZ512F08";
        public const string const_VDA4905SatzField_SATZ512F09 = "SATZ512F09";
        public const string const_VDA4905SatzField_SATZ512F10 = "SATZ512F10";
        public const string const_VDA4905SatzField_SATZ512F11 = "SATZ512F11";
        public const string const_VDA4905SatzField_SATZ512F12 = "SATZ512F12";
        public const string const_VDA4905SatzField_SATZ512F13 = "SATZ512F13";
        public const string const_VDA4905SatzField_SATZ512F14 = "SATZ512F14";
        public const string const_VDA4905SatzField_SATZ512F15 = "SATZ512F15";
        public const string const_VDA4905SatzField_SATZ512F16 = "SATZ512F16";
        public const string const_VDA4905SatzField_SATZ512F17 = "SATZ512F17";
        public const string const_VDA4905SatzField_SATZ512F18 = "SATZ512F18";
        public const string const_VDA4905SatzField_SATZ512F19 = "SATZ512F19";
        public const string const_VDA4905SatzField_SATZ512F20 = "SATZ512F20";

        public const string const_VDA4905SatzField_SATZ513F01 = "SATZ513F01";
        public const string const_VDA4905SatzField_SATZ513F02 = "SATZ513F02";
        public const string const_VDA4905SatzField_SATZ513F03 = "SATZ513F03";
        public const string const_VDA4905SatzField_SATZ513F04 = "SATZ513F04";
        public const string const_VDA4905SatzField_SATZ513F05 = "SATZ513F05";
        public const string const_VDA4905SatzField_SATZ513F06 = "SATZ513F06";
        public const string const_VDA4905SatzField_SATZ513F07 = "SATZ513F07";
        public const string const_VDA4905SatzField_SATZ513F08 = "SATZ513F08";
        public const string const_VDA4905SatzField_SATZ513F09 = "SATZ513F09";
        public const string const_VDA4905SatzField_SATZ513F10 = "SATZ513F10";
        public const string const_VDA4905SatzField_SATZ513F11 = "SATZ513F11";
        public const string const_VDA4905SatzField_SATZ513F12 = "SATZ513F12";
        public const string const_VDA4905SatzField_SATZ513F13 = "SATZ513F13";
        public const string const_VDA4905SatzField_SATZ513F14 = "SATZ513F14";
        public const string const_VDA4905SatzField_SATZ513F15 = "SATZ513F15";
        public const string const_VDA4905SatzField_SATZ513F16 = "SATZ513F16";
        public const string const_VDA4905SatzField_SATZ513F17 = "SATZ513F17";
        public const string const_VDA4905SatzField_SATZ513F18 = "SATZ513F18";

        public const string const_VDA4905SatzField_SATZ514F01 = "SATZ514F01";
        public const string const_VDA4905SatzField_SATZ514F02 = "SATZ514F02";
        public const string const_VDA4905SatzField_SATZ514F03 = "SATZ514F03";
        public const string const_VDA4905SatzField_SATZ514F04 = "SATZ514F04";
        public const string const_VDA4905SatzField_SATZ514F05 = "SATZ514F05";
        public const string const_VDA4905SatzField_SATZ514F06 = "SATZ514F06";
        public const string const_VDA4905SatzField_SATZ514F07 = "SATZ514F07";
        public const string const_VDA4905SatzField_SATZ514F08 = "SATZ514F08";
        public const string const_VDA4905SatzField_SATZ514F09 = "SATZ514F09";
        public const string const_VDA4905SatzField_SATZ514F10 = "SATZ514F10";
        public const string const_VDA4905SatzField_SATZ514F11 = "SATZ514F11";
        public const string const_VDA4905SatzField_SATZ514F12 = "SATZ514F12";
        public const string const_VDA4905SatzField_SATZ514F13 = "SATZ514F13";
        public const string const_VDA4905SatzField_SATZ514F14 = "SATZ514F14";
        public const string const_VDA4905SatzField_SATZ514F15 = "SATZ514F15";
        public const string const_VDA4905SatzField_SATZ514F16 = "SATZ514F16";
        public const string const_VDA4905SatzField_SATZ514F17 = "SATZ514F17";
        public const string const_VDA4905SatzField_SATZ514F18 = "SATZ514F18";
        public const string const_VDA4905SatzField_SATZ514F19 = "SATZ514F19";

        public const string const_VDA4905SatzField_SATZ515F01 = "SATZ515F01";
        public const string const_VDA4905SatzField_SATZ515F02 = "SATZ515F02";
        public const string const_VDA4905SatzField_SATZ515F03 = "SATZ515F03";
        public const string const_VDA4905SatzField_SATZ515F04 = "SATZ515F04";
        public const string const_VDA4905SatzField_SATZ515F05 = "SATZ515F05";
        public const string const_VDA4905SatzField_SATZ515F06 = "SATZ515F06";
        public const string const_VDA4905SatzField_SATZ515F07 = "SATZ515F07";
        public const string const_VDA4905SatzField_SATZ515F08 = "SATZ515F08";
        public const string const_VDA4905SatzField_SATZ515F09 = "SATZ515F09";
        public const string const_VDA4905SatzField_SATZ515F10 = "SATZ515F10";
        public const string const_VDA4905SatzField_SATZ515F11 = "SATZ515F11";
        public const string const_VDA4905SatzField_SATZ515F12 = "SATZ515F12";
        public const string const_VDA4905SatzField_SATZ515F13 = "SATZ515F13";
        public const string const_VDA4905SatzField_SATZ515F14 = "SATZ515F14";
        public const string const_VDA4905SatzField_SATZ515F15 = "SATZ515F15";
        public const string const_VDA4905SatzField_SATZ515F16 = "SATZ515F16";

        public const string const_VDA4905SatzField_SATZ517F01 = "SATZ517F01";
        public const string const_VDA4905SatzField_SATZ517F02 = "SATZ517F02";
        public const string const_VDA4905SatzField_SATZ517F03 = "SATZ517F03";
        public const string const_VDA4905SatzField_SATZ517F04 = "SATZ517F04";
        public const string const_VDA4905SatzField_SATZ517F05 = "SATZ517F05";
        public const string const_VDA4905SatzField_SATZ517F06 = "SATZ517F06";

        public const string const_VDA4905SatzField_SATZ518F01 = "SATZ518F01";
        public const string const_VDA4905SatzField_SATZ518F02 = "SATZ518F02";
        public const string const_VDA4905SatzField_SATZ518F03 = "SATZ518F03";
        public const string const_VDA4905SatzField_SATZ518F04 = "SATZ518F04";
        public const string const_VDA4905SatzField_SATZ518F05 = "SATZ518F05";
        public const string const_VDA4905SatzField_SATZ518F06 = "SATZ518F06";

        public const string const_VDA4905SatzField_SATZ519F01 = "SATZ519F01";
        public const string const_VDA4905SatzField_SATZ519F02 = "SATZ519F02";
        public const string const_VDA4905SatzField_SATZ519F03 = "SATZ519F03";
        public const string const_VDA4905SatzField_SATZ519F04 = "SATZ519F04";
        public const string const_VDA4905SatzField_SATZ519F05 = "SATZ519F05";
        public const string const_VDA4905SatzField_SATZ519F06 = "SATZ519F06";
        public const string const_VDA4905SatzField_SATZ519F07 = "SATZ519F07";
        public const string const_VDA4905SatzField_SATZ519F08 = "SATZ519F08";
        public const string const_VDA4905SatzField_SATZ519F09 = "SATZ519F09";
        public const string const_VDA4905SatzField_SATZ519F10 = "SATZ519F10";
        public const string const_VDA4905SatzField_SATZ519F11 = "SATZ519F11";

        //VDA 4913
        public const string const_VDA4913SatzField_SATZ711F01 = "SATZ711F01";
        public const string const_VDA4913SatzField_SATZ711F02 = "SATZ711F02";
        public const string const_VDA4913SatzField_SATZ711F03 = "SATZ711F03";
        public const string const_VDA4913SatzField_SATZ711F04 = "SATZ711F04";
        public const string const_VDA4913SatzField_SATZ711F05 = "SATZ711F05";
        public const string const_VDA4913SatzField_SATZ711F06 = "SATZ711F06";
        public const string const_VDA4913SatzField_SATZ711F07 = "SATZ711F07";
        public const string const_VDA4913SatzField_SATZ711F08 = "SATZ711F08";
        public const string const_VDA4913SatzField_SATZ711F09 = "SATZ711F09";
        public const string const_VDA4913SatzField_SATZ711F10 = "SATZ711F10";
        public const string const_VDA4913SatzField_SATZ711F11 = "SATZ711F11";
        public const string const_VDA4913SatzField_SATZ711F12 = "SATZ711F12";

        public const string const_VDA4913SatzField_SATZ712F01 = "SATZ712F01";
        public const string const_VDA4913SatzField_SATZ712F02 = "SATZ712F02";
        public const string const_VDA4913SatzField_SATZ712F03 = "SATZ712F03";
        public const string const_VDA4913SatzField_SATZ712F04 = "SATZ712F04";
        public const string const_VDA4913SatzField_SATZ712F05 = "SATZ712F05";
        public const string const_VDA4913SatzField_SATZ712F06 = "SATZ712F06";
        public const string const_VDA4913SatzField_SATZ712F07 = "SATZ712F07";
        public const string const_VDA4913SatzField_SATZ712F08 = "SATZ712F08";
        public const string const_VDA4913SatzField_SATZ712F09 = "SATZ712F09";
        public const string const_VDA4913SatzField_SATZ712F10 = "SATZ712F10";
        public const string const_VDA4913SatzField_SATZ712F11 = "SATZ712F11";
        public const string const_VDA4913SatzField_SATZ712F12 = "SATZ712F12";
        public const string const_VDA4913SatzField_SATZ712F13 = "SATZ712F13";
        public const string const_VDA4913SatzField_SATZ712F14 = "SATZ712F14";
        public const string const_VDA4913SatzField_SATZ712F15 = "SATZ712F15";
        public const string const_VDA4913SatzField_SATZ712F16 = "SATZ712F16";
        public const string const_VDA4913SatzField_SATZ712F17 = "SATZ712F17";
        public const string const_VDA4913SatzField_SATZ712F18 = "SATZ712F18";
        public const string const_VDA4913SatzField_SATZ712F19 = "SATZ712F19";
        public const string const_VDA4913SatzField_SATZ712F20 = "SATZ712F20";
        public const string const_VDA4913SatzField_SATZ712F21 = "SATZ712F21";

        public const string const_VDA4913SatzField_SATZ713F01 = "SATZ713F01";
        public const string const_VDA4913SatzField_SATZ713F02 = "SATZ713F02";
        public const string const_VDA4913SatzField_SATZ713F03 = "SATZ713F03";
        public const string const_VDA4913SatzField_SATZ713F04 = "SATZ713F04";
        public const string const_VDA4913SatzField_SATZ713F05 = "SATZ713F05";
        public const string const_VDA4913SatzField_SATZ713F06 = "SATZ713F06";
        public const string const_VDA4913SatzField_SATZ713F07 = "SATZ713F07";
        public const string const_VDA4913SatzField_SATZ713F08 = "SATZ713F08";
        public const string const_VDA4913SatzField_SATZ713F09 = "SATZ713F09";
        public const string const_VDA4913SatzField_SATZ713F10 = "SATZ713F10";
        public const string const_VDA4913SatzField_SATZ713F11 = "SATZ713F11";
        public const string const_VDA4913SatzField_SATZ713F12 = "SATZ713F12";
        public const string const_VDA4913SatzField_SATZ713F13 = "SATZ713F13";
        public const string const_VDA4913SatzField_SATZ713F14 = "SATZ713F14";
        public const string const_VDA4913SatzField_SATZ713F15 = "SATZ713F15";
        public const string const_VDA4913SatzField_SATZ713F16 = "SATZ713F16";
        public const string const_VDA4913SatzField_SATZ713F17 = "SATZ713F17";
        public const string const_VDA4913SatzField_SATZ713F18 = "SATZ713F18";
        public const string const_VDA4913SatzField_SATZ713F19 = "SATZ713F19";
        public const string const_VDA4913SatzField_SATZ713F20 = "SATZ713F20";
        public const string const_VDA4913SatzField_SATZ713F21 = "SATZ713F21";

        public const string const_VDA4913SatzField_SATZ714F01 = "SATZ714F01";
        public const string const_VDA4913SatzField_SATZ714F02 = "SATZ714F02";
        public const string const_VDA4913SatzField_SATZ714F03 = "SATZ714F03";
        public const string const_VDA4913SatzField_SATZ714F04 = "SATZ714F04";
        public const string const_VDA4913SatzField_SATZ714F05 = "SATZ714F05";
        public const string const_VDA4913SatzField_SATZ714F06 = "SATZ714F06";
        public const string const_VDA4913SatzField_SATZ714F07 = "SATZ714F07";
        public const string const_VDA4913SatzField_SATZ714F08 = "SATZ714F08";
        public const string const_VDA4913SatzField_SATZ714F09 = "SATZ714F09";
        public const string const_VDA4913SatzField_SATZ714F10 = "SATZ714F10";
        public const string const_VDA4913SatzField_SATZ714F11 = "SATZ714F11";
        public const string const_VDA4913SatzField_SATZ714F12 = "SATZ714F12";
        public const string const_VDA4913SatzField_SATZ714F13 = "SATZ714F13";
        public const string const_VDA4913SatzField_SATZ714F14 = "SATZ714F14";
        public const string const_VDA4913SatzField_SATZ714F15 = "SATZ714F15";
        public const string const_VDA4913SatzField_SATZ714F16 = "SATZ714F16";
        public const string const_VDA4913SatzField_SATZ714F17 = "SATZ714F17";
        public const string const_VDA4913SatzField_SATZ714F18 = "SATZ714F18";
        public const string const_VDA4913SatzField_SATZ714F19 = "SATZ714F19";
        public const string const_VDA4913SatzField_SATZ714F20 = "SATZ714F20";
        public const string const_VDA4913SatzField_SATZ714F21 = "SATZ714F21";
        public const string const_VDA4913SatzField_SATZ714F22 = "SATZ714F22";

        public const string const_VDA4913SatzField_SATZ715F01 = "SATZ715F01";
        public const string const_VDA4913SatzField_SATZ715F02 = "SATZ715F02";
        public const string const_VDA4913SatzField_SATZ715F03 = "SATZ715F03";
        public const string const_VDA4913SatzField_SATZ715F04 = "SATZ715F04";
        public const string const_VDA4913SatzField_SATZ715F05 = "SATZ715F05";
        public const string const_VDA4913SatzField_SATZ715F06 = "SATZ715F06";
        public const string const_VDA4913SatzField_SATZ715F07 = "SATZ715F07";
        public const string const_VDA4913SatzField_SATZ715F08 = "SATZ715F08";
        public const string const_VDA4913SatzField_SATZ715F09 = "SATZ715F09";
        public const string const_VDA4913SatzField_SATZ715F10 = "SATZ715F10";
        public const string const_VDA4913SatzField_SATZ715F11 = "SATZ715F11";
        public const string const_VDA4913SatzField_SATZ715F12 = "SATZ715F12";
        public const string const_VDA4913SatzField_SATZ715F13 = "SATZ715F13";
        public const string const_VDA4913SatzField_SATZ715F14 = "SATZ715F14";
        public const string const_VDA4913SatzField_SATZ715F15 = "SATZ715F15";
        public const string const_VDA4913SatzField_SATZ715F16 = "SATZ715F16";

        public const string const_VDA4913SatzField_SATZ716F01 = "SATZ716F01";
        public const string const_VDA4913SatzField_SATZ716F02 = "SATZ716F02";
        public const string const_VDA4913SatzField_SATZ716F03 = "SATZ716F03";
        public const string const_VDA4913SatzField_SATZ716F04 = "SATZ716F04";
        public const string const_VDA4913SatzField_SATZ716F05 = "SATZ716F05";
        public const string const_VDA4913SatzField_SATZ716F06 = "SATZ716F06";

        public const string const_VDA4913SatzField_SATZ717F01 = "SATZ717F01";
        public const string const_VDA4913SatzField_SATZ717F02 = "SATZ717F02";
        public const string const_VDA4913SatzField_SATZ717F03 = "SATZ717F03";
        public const string const_VDA4913SatzField_SATZ717F04 = "SATZ717F04";
        public const string const_VDA4913SatzField_SATZ717F05 = "SATZ717F05";
        public const string const_VDA4913SatzField_SATZ717F06 = "SATZ717F06";
        public const string const_VDA4913SatzField_SATZ717F07 = "SATZ717F07";
        public const string const_VDA4913SatzField_SATZ717F08 = "SATZ717F08";
        public const string const_VDA4913SatzField_SATZ717F09 = "SATZ717F09";

        public const string const_VDA4913SatzField_SATZ718F01 = "SATZ718F01";
        public const string const_VDA4913SatzField_SATZ718F02 = "SATZ718F02";
        public const string const_VDA4913SatzField_SATZ718F03 = "SATZ718F03";
        public const string const_VDA4913SatzField_SATZ718F04 = "SATZ718F04";
        public const string const_VDA4913SatzField_SATZ718F05 = "SATZ718F05";
        public const string const_VDA4913SatzField_SATZ718F06 = "SATZ718F06";
        public const string const_VDA4913SatzField_SATZ718F07 = "SATZ718F07";
        public const string const_VDA4913SatzField_SATZ718F08 = "SATZ718F08";
        public const string const_VDA4913SatzField_SATZ718F09 = "SATZ718F09";
        public const string const_VDA4913SatzField_SATZ718F10 = "SATZ718F10";
        public const string const_VDA4913SatzField_SATZ718F11 = "SATZ718F11";
        public const string const_VDA4913SatzField_SATZ718F12 = "SATZ718F12";
        public const string const_VDA4913SatzField_SATZ718F13 = "SATZ718F13";
        public const string const_VDA4913SatzField_SATZ718F14 = "SATZ718F14";
        public const string const_VDA4913SatzField_SATZ718F15 = "SATZ718F15";

        public const string const_VDA4913SatzField_SATZ719F01 = "SATZ719F01";
        public const string const_VDA4913SatzField_SATZ719F02 = "SATZ719F02";
        public const string const_VDA4913SatzField_SATZ719F03 = "SATZ719F03";
        public const string const_VDA4913SatzField_SATZ719F04 = "SATZ719F04";
        public const string const_VDA4913SatzField_SATZ719F05 = "SATZ719F05";
        public const string const_VDA4913SatzField_SATZ719F06 = "SATZ719F06";
        public const string const_VDA4913SatzField_SATZ719F07 = "SATZ719F07";
        public const string const_VDA4913SatzField_SATZ719F08 = "SATZ719F08";
        public const string const_VDA4913SatzField_SATZ719F09 = "SATZ719F09";
        public const string const_VDA4913SatzField_SATZ719F10 = "SATZ719F10";
        public const string const_VDA4913SatzField_SATZ719F11 = "SATZ719F11";
        public const string const_VDA4913SatzField_SATZ719F12 = "SATZ719F12";

        //Verpackungscodierung Satz715
        public const string const_VDA715F03_VerpackungsCodierung_Bund = "0000BUN";
        public const string const_VDA715F03_VerpackungsCodierung_Pal = "0000PAL";
        public const string const_VDA715F03_VerpackungsCodierung_Bleche = "0000BLE";


        public clsSQLCOM sqlConCom = new clsSQLCOM();
        public clsUniport Uniport = new clsUniport();

        public System.Data.DataTable dtASNForEingang;
        public System.Data.DataTable dtASNForArt;

        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;
        public clsSystem Sys;
        public string tmpXMLFile { get; set; }

        public clsSQLCOM SQLConIntern = new clsSQLCOM();
        public clsASNArt ASNArt;
        public clsASNValue ASNValue;
        public clsASNTyp ASNTyp;
        //public clsASNAction ASNAction;
        public clsQueue Queue;
        public clsJobs Job = new clsJobs();
        public clsXmlStructure xmlStruct;
        public List<clsLogbuchCon> ListError;
        public clsUniPort UniPortXML;
        public clsVDA4905 VDA4905;

        public clsXmlMessages XmlMessages = new clsXmlMessages();

        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        public clsSystem SystemLVS;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }
        public Dictionary<string, clsASNArtSatz> DictVDASatz;
        public List<clsASNArtSatz> ListSatz;
        public List<clsASNValue> ListASNValueInsert;

        public decimal ID { get; set; }
        public string ASNFileTyp { get; set; }
        public decimal ASNNR { get; set; }
        public decimal ASNFieldID { get; set; }
        public decimal ASNTypID { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public DateTime Datum { get; set; }
        public bool IsRead { get; set; }
        public string Direction { get; set; }

        public string tmpWriteFile { get; set; }
        public string tmpStorFilePath { get; set; }
        public decimal MandantenID { get; set; }
        public decimal ArbeitsbereichID { get; set; }
        public string Prozess { get; set; }
        public bool IsTransferFile { get; set; }


        public event EventHandler EventWorkingReport;
        protected virtual void OnWorkingReportChange(EventArgs e)
        {
            EventHandler handler = EventWorkingReport;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public string WorkingReportText { get; set; }



        /*****************************************************************************
         *                          Methoden
         * **************************************************************************/
        ///<summary>clsASN / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_SYSTEM myGLSystem, Globals._GL_USER myGLUser)
        {
            this.IsTransferFile = false;
            this.GL_User = myGLUser;
            this.GLSystem = myGLSystem;
            //this.SQLConIntern.init_con(ref this.GLSystem, true);

            this.ASNArt = new clsASNArt();
            this.ASNArt.InitClass(ref this.GL_User, this.SQLConIntern);

            this.ASNValue = new clsASNValue();
            this.ASNValue.InitClass(this.GLSystem, this.GL_User);

            this.Queue = new clsQueue();
            this.Queue.GL_User = this.GL_User;

            //this.ASNAction = new clsASNAction();
            //this.ASNAction.InitClass(ref this.GL_User);

        }
        ///<summary>clsASN / Copy</summary>
        ///<remarks></remarks>
        public clsASN Copy()
        {
            return (clsASN)this.MemberwiseClone();
        }
        ///<summary>clsASN / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            if (ListASNValueInsert.Count > 0)
            {
                if (this.Job != null)
                {
                    this.ASNFileTyp = this.Job.ASNFileTyp;
                    this.ASNNR = this.Job.ASNArtID;
                    this.ASNFieldID = 0;
                    this.ASNTypID = this.Job.ASNTypID;
                    this.Path = this.Job.ASNFileStorePath;
                    this.Direction = this.Job.Direction;

                    if (this.ArbeitsbereichID > 0)
                    {
                        IsRead = false;
                        string strSql = string.Empty;
                        strSql = "DECLARE @ASNTableID as decimal(28,0); " +
                                 "INSERT INTO ASN (ASNFileTyp, ASNNr, ASNFieldID, ASNTypID, Path, FileName, Datum, IsRead" +
                                                   ", Direction, MandantenID, ArbeitsbereichID" +
                                                   ") " +
                                                        "VALUES ('" + ASNFileTyp + "'" +
                                                                ", " + ASNNR +
                                                                ", " + ASNFieldID +
                                                                ", " + ASNTypID +
                                                                ", '" + Path + "'" +
                                                                ", '" + FileName + "'" +
                                                                ", '" + DateTime.Now + "'" +
                                                                ", " + System.Convert.ToInt32(IsRead) +
                                                                ", '" + Direction + "'" +
                                                                ", " + MandantenID +
                                                                ", " + ArbeitsbereichID +

                                                                ");";
                        strSql = strSql + " Select  @ASNTableID  = @@IDENTITY; ";

                        for (Int32 i = 0; i <= ListASNValueInsert.Count - 1; i++)
                        {
                            clsASNValue tmp = new clsASNValue();
                            tmp = (clsASNValue)ListASNValueInsert[i];

                            this.ASNValue = new clsASNValue();
                            switch (this.ASNFileTyp)
                            {
                                //case clsASN.const_ASNFiledTyp_XML_Uniport:
                                case constValue_AsnArt.const_Art_XML_Uniport:
                                    this.ASNValue.ASNFieldID = 0;
                                    this.ASNValue.FieldName = tmp.FieldName;
                                    this.ASNValue.Value = tmp.Value;
                                    break;
                                case constValue_AsnArt.const_Art_VDA4913:
                                    //case clsASN.const_ASNFiledTyp_VDA4913:
                                    this.ASNValue = tmp;
                                    break;
                                //case clsASN.const_ASNFiledTyp_VDA4905:
                                case constValue_AsnArt.const_Art_VDA4905:
                                    this.ASNValue = tmp;
                                    break;
                                default:
                                    this.ASNValue.ASNFieldID = 0;
                                    break;
                            }
                            strSql = strSql + this.ASNValue.Add();
                        }
                        strSql = strSql + " Select @ASNTableID as ASNTableID; ";

                        string strTmp = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetValue(strSql, "ASN", BenutzerID);
                        decimal decTmp = 0;
                        Decimal.TryParse(strTmp, out decTmp);
                        if (decTmp > 0)
                        {
                            this.ID = decTmp;
                            //Filename erstellen
                            this.ASNTyp = new clsASNTyp();
                            this.ASNTyp.InitClass(ref this.GL_User);
                            //this.ASNTyp.ID = this.ASNTypID;
                            //this.ASNTyp.Fill();
                            Int32 iTmp = 0;
                            Int32.TryParse(this.ASNTypID.ToString(), out iTmp);
                            this.ASNTyp.TypID = iTmp;
                            this.ASNTyp.FillbyTypID();

                            //this.FileName = this.ASNTyp.Typ + "_[ID" + decTmp.ToString() + "]_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "_" + this.Job.FileName;
                            this.FileName = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASNTyp.Typ, decTmp.ToString()) + this.Job.FileName;
                            this.UpdateFileName();
                        }
                    }
                }
            }
        }
        /*******************************************************************************
         *                      read - ASN wird eingelesen
         * *****************************************************************************/
        ///<summary>clsASN / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            System.Data.DataTable dtASNValue = new System.Data.DataTable("ASN");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASN WHERE ID=" + ID + ";";
            dtASNValue = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASN");
            for (Int32 i = 0; i <= dtASNValue.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dtASNValue.Rows[i]["ID"];
                this.ASNFileTyp = dtASNValue.Rows[i]["ASNFileTyp"].ToString();
                this.ASNNR = (decimal)dtASNValue.Rows[i]["ASNNR"];
                this.ASNFieldID = (decimal)dtASNValue.Rows[i]["ASNFieldID"];
                this.ASNTypID = (decimal)dtASNValue.Rows[i]["ASNTypID"];
                this.Path = dtASNValue.Rows[i]["Path"].ToString();
                this.FileName = dtASNValue.Rows[i]["FileName"].ToString();
                this.Datum = (DateTime)dtASNValue.Rows[i]["Datum"];
                this.IsRead = (bool)dtASNValue.Rows[i]["IsRead"];
                this.Direction = dtASNValue.Rows[i]["Direction"].ToString();
                decimal decTmp = 0;
                Decimal.TryParse(dtASNValue.Rows[i]["MandantenID"].ToString(), out decTmp);
                this.MandantenID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dtASNValue.Rows[i]["ArbeitsbereichID"].ToString(), out decTmp);
                this.ArbeitsbereichID = decTmp;

                ASNTyp = new clsASNTyp();
                ASNTyp.InitClass(ref this.GL_User, this.ASNTypID);
                //ASNTyp.GL_User = this.GL_User;
                //ASNTyp.ID = this.ASNTypID;
                //ASNTyp.Fill();
            }
        }
        ///<summary>clsASN / UpdateIsReadToTrue</summary>
        ///<remarks></remarks>
        public void UpdateIsReadToTrue()
        {
            string strSQL = string.Empty;
            strSQL = "Update ASN " +
                            "SET IsRead=1 " +
                            "WHERE ID=" + ID + ";";
            clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
        }
        ///<summary>clsASN / UpdateFileName</summary>
        ///<remarks></remarks>
        public void UpdateFileName()
        {
            string strSQL = string.Empty;
            strSQL = " Update ASN " +
                            "SET FileName = '" + this.FileName + "' " +
                            "WHERE ID=" + ID + ";";
            clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
        }
        ///<summary>clsASN / InitClass</summary>
        ///<remarks></remarks>
        public void ReadVDAorXML(string myFilePath)
        {
            ListError = new List<clsLogbuchCon>();
            if (Job != null)
            {
                string strLine = string.Empty;


                string strSenderVerweis = string.Empty;
                string strReceiverVerweis = string.Empty;

                switch (Job.ASNFileTyp)
                {
                    //case clsASN.const_ASNFiledTyp_XML_Uniport:
                    case constValue_AsnArt.const_Art_XML_Uniport:
                        this.FileName = Job.FileName;
                        this.XmlMessages = new clsXmlMessages();
                        this.XmlMessages.InitClass(this.GLSystem, this.GL_User);
                        this.XmlMessages.Job = this.Job;

                        ListASNValueInsert = new List<clsASNValue>();
                        ListASNValueInsert = this.XmlMessages.Read(myFilePath);
                        //wenn die Liste gefüllt ist,dann Insert
                        if (ListASNValueInsert.Count > 0)
                        {
                            this.Add();
                        }
                        break;

                    case constValue_AsnArt.const_Art_BMWCall4913:
                        //FillListASN(Job.ASNArtID);
                        clsBMWCall4913 bmwCall = new clsBMWCall4913(this, myFilePath);
                        this.FileName = bmwCall.FileNameStored;
                        if (!bmwCall.Errortext.Equals(string.Empty))
                        {
                            //this.IsTransferFile = false;
                            string strError = this.Prozess + " - Datei: [" + this.Job.FileName + "] - kann nicht verarbeitet werden!" +
                                              Environment.NewLine + bmwCall.Errortext;

                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                            tmpLog.GL_User = this._GL_User;
                            tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                            //tmpLog.Typ = Globals.enumLogArtItem.ERROR.ToString();
                            tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                            tmpLog.LogText = strError + Environment.NewLine;
                            tmpLog.TableName = string.Empty;
                            decimal decTmp = 0;
                            tmpLog.TableID = decTmp;
                            this.ASNArt.ListError.Add(tmpLog);

                            clsMail EMail = new clsMail();
                            EMail.InitClass(this.GL_User, this.Sys);

                            string strSubject = string.Empty;
                            if (this.Sys.DebugModeCOM)
                            {
                                strSubject += "DEBUG MODE >>> ";
                            }
                            strSubject += this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error TASK_CALLread !!!";

                            EMail.Subject = strSubject;
                            EMail.Message = tmpLog.LogText;
                            EMail.SendError();
                        }
                        this.ListError = this.ASNArt.ListError;
                        break;

                    case constValue_AsnArt.const_Art_VDA4913:
                    case constValue_AsnArt.const_Art_VDA4905:
                        string s511F04 = string.Empty;
                        string s512F03 = string.Empty;
                        string s711F03 = string.Empty;
                        string s711F04 = string.Empty;
                        string s712F04 = string.Empty;
                        string s713F13 = string.Empty;

                        FillListASN(Job.ASNArtID);
                        strLine = string.Empty;
                        strLine = helper_IOFile.ReadFileInLine(myFilePath);

                        //ab hier muss der string in die einzelenen Sätze aufgeteilt werden
                        if (strLine != string.Empty)
                        {
                            //Hier liegt die gesamte Meldung als string vor
                            //dieser string muss nun in die einzelnen Sätze aufgeteilt werden
                            //und entsprechend in die Listen eingetragen werden
                            this.ASNArt.CreateASNStrings(strLine, this);

                            if (this.ASNArt.ListError.Count == 0)
                            {
                                //--- Check Verweis -> Arbeitsbereich und Mandant
                                clsADRVerweis adrverweis = new clsADRVerweis();
                                adrverweis = ediHelper_AdrVerweis.GetAdrVerweis4913(this);
                                //adrverweis.FillClassByVerweis(strSenderVerweis, this.Job.ASNFileTyp);
                                if (adrverweis.ID > 0)
                                {
                                    this.MandantenID = adrverweis.MandantenID;
                                    this.ArbeitsbereichID = adrverweis.ArbeitsbereichID;
                                    this.ListASNValueInsert = new List<clsASNValue>();
                                    this.ListASNValueInsert = this.ASNArt.asnSatz.ListASNValue;
                                }
                                else
                                {
                                    this.IsTransferFile = true;
                                    this.MandantenID = 0;
                                    this.ArbeitsbereichID = 0;

                                    string strError = this.Prozess + " - Datei: [" + this.Job.FileName + "] - es kann kein Arbeitsbereich zugewiesen werden, da kein gültiger Verweis [" + strSenderVerweis + "]!!!";

                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.GL_User = this._GL_User;
                                    tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                    tmpLog.LogText = strError + Environment.NewLine + "ASN: " + Environment.NewLine; //

                                    foreach (string s in this.ASNArt.asnSatz.ListSatzString)
                                    {
                                        tmpLog.LogText += s + Environment.NewLine;
                                    }//+ strLine + Environment.NewLine;

                                    tmpLog.LogText += Environment.NewLine + Environment.NewLine;
                                    tmpLog.LogText += "Verweisfelder / Value: " + Environment.NewLine;
                                    tmpLog.LogText += "711F03: " + this.ASNArt.asnSatz.Verweis_711F03 + Environment.NewLine;
                                    tmpLog.LogText += "711F04: " + this.ASNArt.asnSatz.Verweis_711F04 + Environment.NewLine;
                                    tmpLog.LogText += "712F04: " + this.ASNArt.asnSatz.Verweis_712F04 + Environment.NewLine;
                                    tmpLog.LogText += "713F13: " + this.ASNArt.asnSatz.Verweis_713F13 + Environment.NewLine;


                                    tmpLog.LogText += Environment.NewLine + Environment.NewLine;
                                    tmpLog.LogText += "Datenfelder / Value: " + Environment.NewLine;
                                    foreach (clsASNValue v in this.ASNArt.asnSatz.ListASNValue)
                                    {
                                        tmpLog.LogText += string.Format("{0}:  {1}", v.Kennung, v.Value) + Environment.NewLine;
                                    }

                                    tmpLog.TableName = string.Empty;
                                    decimal decTmp = 0;
                                    tmpLog.TableID = decTmp;
                                    this.ASNArt.ListError.Add(tmpLog);

                                    clsMail EMail = new clsMail();
                                    EMail.InitClass(this.GL_User, this.Sys);

                                    string strSubject = string.Empty;
                                    if (this.Sys.DebugModeCOM)
                                    {
                                        strSubject += "DEBUG MODE >>> ";
                                    }
                                    strSubject += this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error TASK_VDAread: kein Arbeitsbereich !!!";
                                    EMail.Subject = strSubject;
                                    EMail.Message = tmpLog.LogText;
                                    EMail.SendError();
                                }
                            }//--- Check Verweis -> Arbeitsbereich und Mandant
                            else
                            {
                                clsMail EMail = new clsMail();
                                EMail.InitClass(this.GL_User, this.Sys);

                                string strSubject = string.Empty;
                                if (this.Sys.DebugModeCOM)
                                {
                                    strSubject += "DEBUG MODE >>> ";
                                }
                                strSubject += this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error TASK_VDAread: ASN fehlerhaft !!!";
                                EMail.Subject = strSubject;
                                EMail.Message = ((clsLogbuchCon)this.ASNArt.ListError[0]).LogText;
                                EMail.SendError();
                            }
                        }
                        else
                        {
                            this.ASNArt.ListError = new List<clsLogbuchCon>();
                            string strError = this.Prozess + " - Datei: [" + this.Job.FileName + "] - Datei fehlerhaft, Datei enthält kein Daten!";

                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                            tmpLog.GL_User = this._GL_User;
                            tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                            tmpLog.LogText = strError + Environment.NewLine + "ASN: " + Environment.NewLine + strLine; ;
                            tmpLog.TableName = string.Empty;
                            decimal decTmp = 0;
                            tmpLog.TableID = decTmp;
                            this.ASNArt.ListError.Add(tmpLog);

                            clsMail EMail = new clsMail();
                            EMail.InitClass(this.GL_User, this.Sys);

                            string strSubject = string.Empty;
                            if (this.Sys.DebugModeCOM)
                            {
                                strSubject += "DEBUG MODE >>> ";
                            }
                            strSubject += this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error TASK_VDAread: Datei enthält kein Daten !!!";
                            EMail.Subject = strSubject;
                            EMail.Message = tmpLog.LogText;
                            EMail.SendError();
                        }
                        this.ListError = this.ASNArt.ListError;
                        //Check auf Error
                        if (this.ListError.Count == 0)
                        {
                            if (this.ListASNValueInsert != null)
                            {
                                if (this.ListASNValueInsert.Count > 0)
                                {
                                    this.Add();
                                }
                            }
                        }
                        break;

                    case constValue_AsnArt.const_Art_EdifactVDA4984:
                        this.IsTransferFile = false;
                        //--- füllen der Segmente in Dict. und Liste 
                        this.ASNTyp = new clsASNTyp();
                        this.ASNTyp.InitClass(ref this.GL_User);
                        this.ASNTyp.TypID = (int)this.Job.ASNTypID;
                        this.ASNTyp.FillbyTypID();

                        this.ASNArt.FillByAsnArt(constValue_AsnArt.const_Art_EdifactVDA4984);
                        this.ASNArt.EdiSegment.ASNArtId = (int)this.ASNArt.ID;
                        this.ASNArt.EdiSegment.FillbyASNArtID();
                        this.ASNArt.EdiSegment.FillListAndDictEdiSegment();
                        //FillListASN(Job.ASNArtID);
                        strLine = string.Empty;
                        try
                        {
                            using (StreamReader sr = new StreamReader(myFilePath))
                            {
                                strLine = sr.ReadToEnd().Replace(Environment.NewLine, "");
                            }
                        }
                        catch (Exception ex)
                        {
                            //Fehlermeldung
                            string strEx = ex.ToString();
                        }

                        //ab hier muss der string in die einzelenen Sätze aufgeteilt werden
                        if (strLine != string.Empty)
                        {
                            //Hier liegt die gesamte Meldung als string vor
                            //dieser string muss nun in die einzelnen Sätze aufgeteilt werden
                            //und entsprechend in die Listen eingetragen werden

                            clsEdiVDA4984Read vda4984 = new clsEdiVDA4984Read();
                            vda4984.InitClass(this.GL_User, this, this.Sys, strLine);
                            this.ListError.AddRange(vda4984.ListErrorEdiVDA);
                            if (this.ListError.Count == 0)
                            {
                                this.FileName = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASNTyp.Typ, vda4984.DocumentenNumber.ToString()) + this.Job.FileName;
                                vda4984.AddVDA4984();
                                this.ListError.AddRange(vda4984.ListErrorEdiVDA);
                            }
                        }
                        break;

                    case constValue_AsnArt.const_Art_EDIFACT_DELFOR_D97A:
                        ASNTyp = new clsASNTyp();
                        ASNTyp = this.Job.AsnTyp.Copy();

                        strLine = string.Empty;
                        try
                        {
                            using (StreamReader sr = new StreamReader(myFilePath))
                            {
                                strLine = sr.ReadToEnd().Replace(Environment.NewLine, "");
                            }
                        }
                        catch (Exception ex)
                        {
                            string strEx = ex.ToString();
                        }
                        if (strLine != string.Empty)
                        {
                            //Hier liegt die gesamte Meldung als string vor
                            //dieser string muss nun in die einzelnen Sätze aufgeteilt werden
                            //und entsprechend in die Listen eingetragen werden
                            clsEdiDelforD97A_Read delfor = new clsEdiDelforD97A_Read(this.GL_User, this, this.Sys, strLine);
                            this.FileName = delfor.Filename;
                        }
                        break;

                    case constValue_AsnArt.const_Art_EDIFACT_Qality_D96A:
                        //ASNTyp = new clsASNTyp();
                        //ASNTyp = this.Job.AsnTyp.Copy();

                        //strLine = string.Empty;
                        //try
                        //{
                        //    using (StreamReader sr = new StreamReader(myFilePath))
                        //    {
                        //        strLine = sr.ReadToEnd().Replace(Environment.NewLine, "");
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    string strEx = ex.ToString();
                        //}
                        //if (strLine != string.Empty)
                        //{
                        //    //Hier liegt die gesamte Meldung als string vor
                        //    //dieser string muss nun in die einzelnen Sätze aufgeteilt werden
                        //    //und entsprechend in die Listen eingetragen werden
                        //    //clsEdiDelforD97A_Read delfor = new clsEdiDelforD97A_Read(this.GL_User, this, this.Sys, strLine);

                        //    clsEdiQalityD96A qualityD96A = new clsEdiQalityD96A(this.GL_User, this, this.Sys, strLine); 
                        //    this.FileName = qualityD96A.Filename;
                        //}
                        break;

                    case constValue_AsnArt.const_Art_XML_ZQM_QALITY02:
                        ASNTyp = new clsASNTyp();
                        ASNTyp = this.Job.AsnTyp.Copy();

                        strLine = string.Empty;
                        try
                        {
                            EdiZQMQalityXmlViewData zqmVD = new EdiZQMQalityXmlViewData(myFilePath);

                            if (zqmVD.ediZQMQalityXml is EdiZQMQalityXml)
                            {
                                zqmVD.ediZQMQalityXml.Path = Job.ASNFileStorePath;
                                zqmVD.ediZQMQalityXml.FileName = System.IO.Path.GetFileName(myFilePath);
                                if (zqmVD.Add())
                                {
                                    this.FileName = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASNTyp.Typ, zqmVD.ediZQMQalityXml.Id.ToString()) + zqmVD.ediZQMQalityXml.FileName;
                                }
                                else
                                {
                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.GL_User = this._GL_User;
                                    tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                    //tmpLog.Typ = Globals.enumLogArtItem.ERROR.ToString();
                                    tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                    tmpLog.LogText = "Es ist ein Fehler beim Eintrag in die Datenbank aufgetreten!" + Environment.NewLine;
                                    tmpLog.TableName = string.Empty;
                                    decimal decTmp = 0;
                                    tmpLog.TableID = decTmp;
                                    this.ListError.Add(tmpLog);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string strEx = ex.ToString();
                        }
                        break;
                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                        this.IsTransferFile = false;
                        //--- füllen der Segmente in Dict. und Liste 
                        this.ASNTyp = new clsASNTyp();
                        this.ASNTyp.InitClass(ref this.GL_User);
                        this.ASNTyp.TypID = (int)this.Job.ASNTypID;
                        this.ASNTyp.FillbyTypID();

                        this.ASNArt.FillByAsnArt(constValue_AsnArt.const_Art_EDIFACT_ASN_D96A);
                        this.ASNArt.EdiSegment.ASNArtId = (int)this.ASNArt.ID;
                        this.ASNArt.EdiSegment.FillbyASNArtID();
                        this.ASNArt.EdiSegment.FillListAndDictEdiSegment();
                        //FillListASN(Job.ASNArtID);
                        strLine = string.Empty;
                        try
                        {
                            using (StreamReader sr = new StreamReader(myFilePath))
                            {
                                //strLine = sr.ReadToEnd().Replace(Environment.NewLine, "");
                                strLine = sr.ReadToEnd().Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                            }
                        }
                        catch (Exception ex)
                        {
                            //Fehlermeldung
                            string strEx = ex.ToString();
                        }

                        //ab hier muss der string in die einzelenen Sätze aufgeteilt werden
                        if (strLine != string.Empty)
                        {
                            //--- SLE Arcelor Arbeitsbereich hat keine festen Empfänger, deshalb muss dies global über den 
                            //--- AdrVerweis gelöst werden.Der Verweis wird über die AsnFileType ermittelt.
                            //--- Aus dem AdrVerweis wird dann Arbeitsbereich und Mandant ermittelt
                            clsADRVerweis Verweis_Sender = ediHelper_EdiEDIFACT_ASN_D96A_GetAdrVerweis.GetAdrVerweis_Sender(this.Job, strLine);

                            if ((Verweis_Sender is clsADRVerweis) && (Verweis_Sender.ID > 0))
                            {
                                //Hier liegt die gesamte Meldung als string vor
                                //dieser string muss nun in die einzelnen Sätze aufgeteilt werden
                                //und entsprechend in die Listen eingetragen werden
                                AsnViewData asnVd = new AsnViewData();
                                asnVd.asnHead.AsnNr = BGM.GetMessageId(strLine, (int)Job.ASNArtID);
                                asnVd.asnHead.ASNFileTyp = Job.ASNFileTyp;
                                asnVd.asnHead.AsnFieldId = 0;
                                asnVd.asnHead.AsnTypId = int.Parse(Job.ASNTypID.ToString());
                                asnVd.asnHead.Path = Job.ASNFileStorePath;
                                asnVd.asnHead.FileName = string.Empty; // System.IO.Path.GetFileName(myFilePath);
                                asnVd.asnHead.Datum = UNB_S004.GetMessageCreationDate(strLine);
                                asnVd.asnHead.IsRead = false;
                                asnVd.asnHead.Direction = "IN";
                                asnVd.asnHead.MandantenId = (int)Verweis_Sender.MandantenID;        //  int.Parse(Job.MandantenID.ToString());
                                asnVd.asnHead.WorkspaceId = (int)Verweis_Sender.ArbeitsbereichID;   // int.Parse(Job.ArbeitsbereichID.ToString());
                                asnVd.asnHead.EdiMessageValue = strLine;
                                asnVd.asnHead.AsnArtId = int.Parse(Job.ASNArtID.ToString());
                                asnVd.asnHead.Created = DateTime.Now;
                                asnVd.Add();

                                if (asnVd.asnHead.Id > 0)
                                {
                                    asnVd.asnHead.FileName = helper_FilePrefixAfterComProzess.GetPrefixVDA(this.ASNTyp.Typ, asnVd.asnHead.Id.ToString()) + System.IO.Path.GetFileName(myFilePath);
                                    asnVd.UpdateFileName();
                                    this.FileName = asnVd.asnHead.FileName;

                                    clsEdiDesadv96A_Read desadv96A = new clsEdiDesadv96A_Read(asnVd.asnHead);
                                    desadv96A.InitClass(this.GL_User, this.Sys);
                                }
                            }

                            //this.ListError.AddRange(desadv96A.ListErrorEdiVDA);
                            //if (this.ListError.Count == 0)
                            //{
                            //    this.FileName = helper_FilePrefixAfterComProzess.GetPrefixEDI(this.ASNTyp.Typ, vda4984.DocumentenNumber.ToString()) + this.Job.FileName;
                            //    vda4984.AddVDA4984();
                            //    this.ListError.AddRange(vda4984.ListErrorEdiVDA);
                            //}
                        }
                        break;
                    case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                        ASNTyp = new clsASNTyp();
                        ASNTyp = this.Job.AsnTyp.Copy();

                        strLine = string.Empty;
                        try
                        {
                            using (StreamReader sr = new StreamReader(myFilePath))
                            {
                                strLine = sr.ReadToEnd().Replace(Environment.NewLine, "");
                            }
                        }
                        catch (Exception ex)
                        {
                            string strEx = ex.ToString();
                        }
                        if (strLine != string.Empty)
                        {
                            //--- aktuell nur Mendritzki
                            AddressReferences adrRef = ediHelper_EdiEDIFACT_ASN_D07A_GetAdrVerweis.GetAdrReference(this.Job, strLine);

                            if ((adrRef is AddressReferences) && (adrRef.Id > 0))
                            {
                                //Hier liegt die gesamte Meldung als string vor
                                //dieser string muss nun in die einzelnen Sätze aufgeteilt werden
                                //und entsprechend in die Listen eingetragen werden
                                AsnViewData asnVd = new AsnViewData();
                                asnVd.asnHead.AsnNr = BGM.GetMessageId(strLine, (int)Job.ASNArtID);
                                asnVd.asnHead.ASNFileTyp = Job.ASNFileTyp;
                                asnVd.asnHead.AsnFieldId = 0;
                                asnVd.asnHead.AsnTypId = int.Parse(Job.ASNTypID.ToString());
                                asnVd.asnHead.Path = Job.ASNFileStorePath;
                                asnVd.asnHead.FileName = string.Empty; // System.IO.Path.GetFileName(myFilePath);
                                asnVd.asnHead.Datum = UNB_S004.GetMessageCreationDate(strLine);
                                asnVd.asnHead.IsRead = false;
                                asnVd.asnHead.Direction = "IN";
                                asnVd.asnHead.MandantenId = (int)adrRef.MandantenId;       //  int.Parse(Job.MandantenID.ToString());
                                asnVd.asnHead.WorkspaceId = (int)adrRef.WorkspaceId;   // int.Parse(Job.ArbeitsbereichID.ToString());
                                asnVd.asnHead.EdiMessageValue = strLine;
                                asnVd.asnHead.AsnArtId = int.Parse(Job.ASNArtID.ToString());
                                asnVd.asnHead.Created = DateTime.Now;
                                asnVd.Add();

                                if (asnVd.asnHead.Id > 0)
                                {
                                    asnVd.asnHead.FileName = helper_FilePrefixAfterComProzess.GetPrefixVDA(this.ASNTyp.Typ, asnVd.asnHead.Id.ToString()) + System.IO.Path.GetFileName(myFilePath);
                                    asnVd.UpdateFileName();
                                    this.FileName = asnVd.asnHead.FileName;

                                    clsEdiDesadvD07A_Read desadvD07A_Read = new clsEdiDesadvD07A_Read(asnVd.asnHead);
                                    desadvD07A_Read.InitClass(this.GL_User, this.Sys);
                                }
                            }
                        }
                        else
                        {
                            // Fehlermeldung
                        }
                        break;
                }
            }
        }
        ///<summary>clsASN / InitClass</summary>
        ///<remarks></remarks>
        public void WriteVDAorXML()
        {
            ListError = new List<clsLogbuchCon>();

            if (this.ASNFileTyp == constValue_AsnArt.const_Art_Textdatei)
            {
                this.Job = new clsJobs();
                Job.InitClass(this.GLSystem, this.GL_User, false);
                Job.GetJobByQueue(this.ASNFileTyp, this.Queue);

                ArticleViewData articleViewData = new ArticleViewData((int)this.Queue.TableID, 1, false);
                List<string> ListMessageText = new List<string>();
                string strMessage = string.Empty;

                int iCw0 = -22;
                int iCw1 = -30;
                //int iCw2 = 20;


                strMessage = "LAGERMELDUNG";
                Int32 iMeldung = (Int32)this.Job.ASNTypID;
                switch (iMeldung)
                {
                    case 2:
                        //EM - ENTL
                        strMessage += " - EM - Lagereingangsmeldung";
                        ListMessageText.Add(strMessage);
                        break;
                    case 4:
                        //AM - VERL
                        strMessage += " - AM - Lagerausgangsmeldung";
                        ListMessageText.Add(strMessage);
                        break;
                }
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Datum: ", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                ListMessageText.Add(strMessage);

                strMessage = string.Empty;
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Queue-Id: ", this.Queue.ID);
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Job [Id]: ", "[" + this.Job.ID + "]");
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Job.Direction: ", this.Job.Direction);
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- ASNFileTyp: ", this.Job.ASNFileTyp);
                ListMessageText.Add(strMessage);

                switch (iMeldung)
                {
                    case 2:
                        //EM - ENTL
                        strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- ASNTypId - [Id]: ", this.Job.AsnTyp.Typ + " - [" + this.Job.ASNTypID + "]");
                        ListMessageText.Add(strMessage);
                        break;
                    case 4:
                        //AM - VERL
                        strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- ASNTypId - [Id]: ", this.Job.AsnTyp.Typ + " - [" + this.Job.ASNTypID + "]");
                        ListMessageText.Add(strMessage);
                        break;
                }
                strMessage = string.Empty;
                ListMessageText.Add(strMessage);

                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "Artikel - [ID]: ", "[" + articleViewData.Artikel.Id + "]");
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- LVSNr: ", articleViewData.Artikel.LVS_ID.ToString());
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Produktionsnummer: ", articleViewData.Artikel.Produktionsnummer);
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Werksnummer: ", articleViewData.Artikel.Werksnummer);
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Brutto [kg]: ", articleViewData.Artikel.Brutto.ToString("N2", new CultureInfo("de-DE")));
                ListMessageText.Add(strMessage);

                strMessage = string.Empty;
                ListMessageText.Add(strMessage);

                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "Eingang - [ID]: ", "[" + articleViewData.Artikel.Eingang.Id + "]");
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- LEingangID: ", articleViewData.Artikel.Eingang.LEingangID.ToString());
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Datum: ", articleViewData.Artikel.Eingang.Eingangsdatum.ToString("dd.MM.yyyy"));
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- ASN: ", articleViewData.Artikel.Eingang.ASN.ToString());
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Auftraggeber: ", articleViewData.Artikel.Eingang.AuftraggeberString.ToString() + " - [" + articleViewData.Artikel.Eingang.Auftraggeber + "]");
                ListMessageText.Add(strMessage);
                strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Empfänger: ", articleViewData.Artikel.Eingang.EmpfaengerString.ToString() + " - [" + articleViewData.Artikel.Eingang.Empfaenger + "]");
                ListMessageText.Add(strMessage);

                strMessage = string.Empty;
                ListMessageText.Add(strMessage);

                switch (iMeldung)
                {
                    case 2:
                        //EM - ENTL                        
                        break;
                    case 4:
                        //AM - VERL
                        strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "Ausgang - [ID]: ", "[" + articleViewData.Artikel.Eingang.Id + "]");
                        ListMessageText.Add(strMessage);
                        strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- LAusgangID: ", articleViewData.Artikel.Ausgang.LAusgangID.ToString());
                        ListMessageText.Add(strMessage);
                        strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Datum: ", articleViewData.Artikel.Ausgang.Datum.ToString("dd.MM.yyyy"));
                        ListMessageText.Add(strMessage);
                        strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- ASN - [ID]: ", articleViewData.Artikel.Ausgang.ASN.ToString());
                        ListMessageText.Add(strMessage);
                        strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Auftraggeber: ", articleViewData.Artikel.Ausgang.AuftraggeberString.ToString() + " - [" + articleViewData.Artikel.Ausgang.Auftraggeber + "]");
                        ListMessageText.Add(strMessage);
                        strMessage = string.Format("{0," + iCw0.ToString() + "} {1," + iCw1.ToString() + "}", "|- Empfänger: ", articleViewData.Artikel.Ausgang.EmpfaengerString.ToString() + " - [" + articleViewData.Artikel.Ausgang.Empfaenger + "]");
                        ListMessageText.Add(strMessage);
                        break;
                }

                this.FileName = this.Job.FileName;
                string tmpFileName = helper_FilePrefixAfterComProzess.GetPrefixVDA(this.Job.AsnTyp.Typ, this.Queue.TableID.ToString().ToString()) + this.FileName;
                this.FileName = tmpFileName + ".txt";
                string tmpFilePath = System.IO.Path.Combine(this.Sys.VE_OdettePath + this.Job.ASNFileStorePath, FileName);

                helper_IOFile.WriteFileInLine(tmpFilePath, ListMessageText);
                this.tmpWriteFile = tmpFilePath;
                this.tmpStorFilePath = this.Job.ASNFileStorePath;
            }
            //XML Uniport Datei
            if (this.ASNFileTyp == constValue_AsnArt.const_Art_XML_Uniport)
            {
                this.Job = new clsJobs();
                Job.InitClass(this.GLSystem, this.GL_User, false);
                //Job.FillByASNFileTypAndASNTyp(clsASNArt.const_Art_XML_Uniport.ToString(), this.Queue.ASNTypID, 0, this.Queue.AdrVerweisID, false);
                Job.GetJobByQueue(constValue_AsnArt.const_Art_XML_Uniport.ToString(), this.Queue);

                this.FileName = Job.FileName;

                this.XmlMessages = new clsXmlMessages();
                this.XmlMessages.InitClass(this.GLSystem, this.GL_User);
                this.XmlMessages.Job = this.Job;
                this.XmlMessages.ID = this.ASNNR;
                this.XmlMessages.Fill();

                //Füllen der XML Struct ermittlen
                this.xmlStruct = new clsXmlStructure();
                this.xmlStruct.InitClass(this.GL_User, this.GLSystem);
                this.xmlStruct.XmlMesID = this.XmlMessages.ID;
                //Ab hier sind jetzt einmal die komplette Structur und 
                //jeweils die Structur von EM und AM in den folgenden 
                //dictEM und dictAM in der Klasse enthalten
                System.Data.DataTable dt = this.xmlStruct.dtXmlStruct;

                UniPortXML = new Uniport.clsUniPort();
                ListASNValueInsert = new List<clsASNValue>();
                ListASNValueInsert = UniPortXML.Fill(this.XmlMessages, this.xmlStruct, this);

                //In der dtLagerMeldung fehlt nun noch die ID für die gesendete Meldung,
                //deshalb muss erst die Meldung in die Datenbank eingetragen werden 
                //und dann entsprechenden die ASNID upgedated werden                
                if (this.ListASNValueInsert.Count > 0)
                {
                    this.Add();
                    //Nach dem ADD muss noch das Feld ID auf die aktuelle ASNID upgedatet werden
                    clsASNValue.UpdateXMLUniportFieldID(this.GL_User.User_ID, this.ID, this.ID);
                    //Eintrag in Table Meldungen (Auflistung der Erstellten Meldungen pro Aritkel)
                    System.Data.DataTable dtToWrite = clsASNValue.GetASNValueDataTableByASNId(this.GL_User.User_ID, this.ID);
                    tmpWriteFile = string.Empty;
                    tmpWriteFile = this.Job.PathDirectory + "\\" + this.FileName;
                    Int32 iMeldung = (Int32)this.Job.ASNTypID;
                    switch (iMeldung)
                    {
                        case 2:
                            //EM - ENTL
                            this.UniPortXML.WriteUniPortXML_ENTL(dtToWrite, this.tmpWriteFile);
                            break;
                        case 4:
                            //AM - VERL
                            this.UniPortXML.WriteUniPortXML_VERL(dtToWrite, this.tmpWriteFile);
                            break;
                    }
                }
            }

            if (this.ASNFileTyp == constValue_AsnArt.const_Art_VDA4913)
            {
                tmpWriteFile = string.Empty;
                tmpStorFilePath = string.Empty;
                string tmpFileName = string.Empty;
                //this.ASNFileTyp = clsASN.const_ASNFiledTyp_VDA4913;
                this.ASNFileTyp = constValue_AsnArt.const_Art_VDA4913;
                //--- unterscheidung BM, da BM nicht über die Queue ermittelt wird
                if (
                        (this.Job is clsJobs) &&
                        (this.Job.AsnTyp is clsASNTyp) &&
                        (
                            (this.Job.AsnTyp.Typ.Equals("BML")) ||
                            (this.Job.AsnTyp.Typ.Equals("BME"))
                        )
                    )
                {
                    this.FileName = this.Job.FileName;
                    tmpFileName = helper_FilePrefixAfterComProzess.GetPrefixVDA(this.Job.AsnTyp.Typ, this.Job.ID.ToString()) + this.FileName;
                }
                else
                {
                    this.Job = new clsJobs();
                    Job.InitClass(this.GLSystem, this.GL_User, false);
                    Job.GetJobByQueue(this.ASNFileTyp, this.Queue);

                    this.FileName = this.Job.FileName;
                    tmpFileName = helper_FilePrefixAfterComProzess.GetPrefixVDA(this.Job.AsnTyp.Typ, this.Queue.TableID.ToString().ToString()) + this.FileName;
                }
                this.FileName = Job.FileName;
                helper_IOFile.CheckPath(this.Job.PathDirectory);
                tmpWriteFile = this.Job.PathDirectory + "\\" + tmpFileName;

                if ((this.Queue.ID > 0) && (this.Queue.IsVirtFile))
                {
                    tmpWriteFile = this.Job.ASNFileStorePathDirectory + "\\" + tmpFileName;
                }
                tmpStorFilePath = this.Job.ASNFileStorePath + "\\" + tmpFileName;

                if (this.Job.ASNFileTyp == constValue_AsnArt.const_Art_VDA4913)
                {
                    clsVDACreate vda = new clsVDACreate();
                    vda.Prozess = this.Prozess;
                    vda.InitClass(this.GL_User, this);

                    //--- Info Mail Edi ignorierte Artikel
                    if (vda.ListIgnArticle.Count > 0)
                    {
                        clsMail EMail = new clsMail();
                        EMail.InitClass(this.GL_User, this.Sys);
                        string strSubject = string.Empty;
                        if (this.Sys.DebugModeCOM)
                        {
                            strSubject = "DEBUG - ";
                        }
                        strSubject += this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- INFO TASK_VDAwrite: Liste ignorierter Artikel im EDI Verkehr!";
                        EMail.Subject = strSubject;
                        string strTxt = string.Empty;
                        strTxt = LVS.Helper.helper_Queue_CreateQueueDetailString.CreateQueueDetailString(this.Queue, strTxt) + Environment.NewLine;
                        foreach (string s in vda.ListIgnArticle)
                        {
                            strTxt += s + Environment.NewLine;
                        }
                        EMail.Message = strTxt;
                        EMail.SendError();
                    }

                    if (vda.ListErrorVDA.Count > 0)
                    {
                        string strTxt = string.Empty;

                        for (Int32 l = 0; l <= vda.ListErrorVDA.Count - 1; l++)
                        {
                            clsLogbuchCon tmpLogMail = (clsLogbuchCon)vda.ListErrorVDA[l];
                            strTxt = strTxt + tmpLogMail.LogText + Environment.NewLine;
                            this.ListError.Add(tmpLogMail);
                        }
                        clsMail EMail = new clsMail();
                        EMail.InitClass(this.GL_User, this.Sys);
                        string strSubject = string.Empty;
                        if (this.Sys.DebugModeCOM)
                        {
                            strSubject = "DEBUG - ";
                        }
                        strSubject += this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error TASK_VDAwrite: Fehler bei Erstellung einer VDA4913 iDoc";
                        EMail.Subject = strSubject;
                        EMail.Message = strTxt;
                        EMail.SendError();
                    }
                    else
                    {
                        //In der ListeVDASatzString sind nun  alle stirngs enthalten und müsse nur
                        //nur noch in ein File geschrieben werden
                        if (vda.ListVDASatzString.Count > 0)
                        {
                            //Check auf Odette-StartDatei
                            if (this.Job.CreateOdetteStart)
                            {
                                //Check FIle exist
                                string strOdettePathFIle = this.Job.PathDirectory + "\\" + this.Job.OdetteStartFileName;
                                if (!File.Exists(strOdettePathFIle))
                                {
                                    List<string> listTmp = new List<string>();
                                    listTmp.Add("");
                                    System.IO.File.WriteAllLines(strOdettePathFIle, listTmp.ToArray());
                                }
                            }
                            if (!this.Job.UseCRLF)
                            {
                                System.IO.File.WriteAllText(tmpWriteFile, string.Join("", vda.ListVDASatzString.ToArray()));
                            }
                            else
                            {
                                System.IO.File.WriteAllLines(tmpWriteFile, vda.ListVDASatzString.ToArray());
                            }
                        }
                        else
                        {
                            string strError = "Datei: " + this.Job.FileName;
                            strError = strError + Environment.NewLine + "Queue ID: [" + this.Queue.ID.ToString() + "] konnte nicht erzeugt werden !  (vda.ListVDASatzString.Count=0) ";

                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                            tmpLog.GL_User = this._GL_User;
                            tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                            tmpLog.LogText = this.Prozess + ".[VDA4913] - " + strError;
                            tmpLog.TableName = "Queue";
                            //decimal decTmp = 0;
                            tmpLog.TableID = this.Queue.ID;
                            this.ListError.Add(tmpLog);

                            clsMail EMail = new clsMail();
                            EMail.InitClass(this.GL_User, this.Sys);
                            string strSubject = string.Empty;
                            if (this.Sys.DebugModeCOM)
                            {
                                strSubject = "DEBUG - ";
                            }
                            strSubject += this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error TASK_VDAwrite: Fehler bei Erstellung einer VDA4913 iDoc";
                            EMail.Subject = strSubject;
                            EMail.Message = tmpLog.LogText;
                            EMail.SendError();
                        }
                    }


                }

            }
            if (this.ASNFileTyp == constValue_AsnArt.const_Art_EdifactVDA4987)
            {
                tmpWriteFile = string.Empty;
                tmpStorFilePath = string.Empty;
                string tmpFileName = string.Empty;
                this.ASNFileTyp = constValue_AsnArt.const_Art_EdifactVDA4987;

                //--- unterscheidung BM, da BM nicht über die Queue ermittelt wird
                if (
                        (this.Job is clsJobs) &&
                        (this.Job.AsnTyp is clsASNTyp) &&
                        (
                            (this.Job.AsnTyp.Typ.Equals("BML")) ||
                            (this.Job.AsnTyp.Typ.Equals("BME"))
                        )
                    )
                {
                    this.FileName = this.Job.FileName;
                    //tmpFileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "_" + this.Job.AsnTyp.Typ + "_[" + this.Job.ID.ToString() + "]_" + this.FileName;
                    tmpFileName = helper_FilePrefixAfterComProzess.GetPrefixVDA(this.Job.AsnTyp.Typ, this.Job.ID.ToString()) + this.FileName;
                }
                else
                {
                    this.Job = new clsJobs();
                    Job.InitClass(this.GLSystem, this.GL_User, false);
                    //Job.FillByASNFileTypAndASNTyp(this.ASNFileTyp, this.Queue.ASNTypID, 0, this.Queue.AdrVerweisID, false);
                    Job.GetJobByQueue(this.ASNFileTyp, this.Queue);

                    this.FileName = this.Job.FileName;
                    //tmpFileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "_" + this.Job.AsnTyp.Typ + "_[" + this.Queue.TableID.ToString() + "]_" + this.FileName;
                    tmpFileName = helper_FilePrefixAfterComProzess.GetPrefixVDA(this.Job.AsnTyp.Typ, this.Queue.TableID.ToString()) + this.FileName;
                }
                this.FileName = Job.FileName;
                tmpWriteFile = this.Job.PathDirectory + "\\" + tmpFileName;

                if ((this.Queue.ID > 0) && (this.Queue.IsVirtFile))
                {
                    tmpWriteFile = this.Job.ASNFileStorePathDirectory + "\\" + tmpFileName;
                }
                tmpStorFilePath = this.Job.ASNFileStorePath + "\\" + tmpFileName;

                clsEdiVDACreate ediVDA = new clsEdiVDACreate();
                ediVDA.Prozess = this.Prozess;
                ediVDA.InitClass(this.GL_User, this.Job, this.Queue, this.Sys);

                //--- Info Mail Edi ignorierte Artikel
                if (ediVDA.ListIgnArticle.Count > 0)
                {
                    clsMail EMail = new clsMail();
                    EMail.InitClass(this.GL_User, this.Sys);
                    string strSubject = string.Empty;
                    if (this.Sys.DebugModeCOM)
                    {
                        strSubject = "DEBUG - ";
                    }
                    strSubject += this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- INFO TASK_VDAwrite: Liste ignorierter Artikel im EDI Verkehr!";
                    EMail.Subject = strSubject;
                    string strTxt = string.Empty;
                    strTxt = LVS.Helper.helper_Queue_CreateQueueDetailString.CreateQueueDetailString(this.Queue, strTxt) + Environment.NewLine;
                    foreach (string s in ediVDA.ListIgnArticle)
                    {
                        strTxt += s + Environment.NewLine;
                    }
                    EMail.Message = strTxt;
                    EMail.SendError();
                }

                if (ediVDA.ListErrorVDA.Count > 0)
                {
                    string strTxt = string.Empty;

                    for (Int32 l = 0; l <= ediVDA.ListErrorVDA.Count - 1; l++)
                    {
                        clsLogbuchCon tmpLogMail = (clsLogbuchCon)ediVDA.ListErrorVDA[l];
                        strTxt = strTxt + tmpLogMail.LogText + Environment.NewLine;
                        this.ListError.Add(tmpLogMail);
                    }
                    clsMail EMail = new clsMail();
                    EMail.InitClass(this.GL_User, this.Sys);
                    EMail.Subject = this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error TASK_VDAwrite: Fehler bei Erstellung einer Edi-VDA4987 iDoc";
                    EMail.Message = strTxt;
                    EMail.SendError();
                }
                else
                {
                    //In der ListeVDASatzString sind nun  alle stirngs enthalten und müsse nur
                    //nur noch in ein File geschrieben werden
                    if (ediVDA.ListEdiVDASatzString.Count > 0)
                    {
                        //Check auf Odette-StartDatei
                        if (this.Job.CreateOdetteStart)
                        {
                            //Check FIle exist
                            string strOdettePathFIle = this.Job.PathDirectory + "\\" + this.Job.OdetteStartFileName;
                            if (!File.Exists(strOdettePathFIle))
                            {
                                List<string> listTmp = new List<string>();
                                listTmp.Add("");
                                System.IO.File.WriteAllLines(strOdettePathFIle, listTmp.ToArray());
                            }
                        }
                        if (!this.Job.UseCRLF)
                        {
                            //TEst Blanks
                            //string LogText = string.Empty;
                            //for (Int32 i = 0; i <= vda.ListVDASatzString.Count - 1; i++)
                            //{
                            //    string strSatz = vda.ListVDASatzString[i];
                            //    LogText += strSatz + " -> Länge = "+ strSatz.Length.ToString()+ Environment.NewLine;
                            //}
                            //System.IO.File.WriteAllText(this.Job.PathDirectory + "\\Log_" + this.FileName, LogText);
                            System.IO.File.WriteAllText(tmpWriteFile, string.Join("", ediVDA.ListEdiVDASatzString.ToArray()));
                        }
                        else
                        {
                            System.IO.File.WriteAllLines(tmpWriteFile, ediVDA.ListEdiVDASatzString.ToArray());
                        }
                    }
                    else
                    {
                        string strError = "Datei: " + this.Job.FileName;
                        strError = strError + Environment.NewLine + "Queue ID: [" + this.Queue.ID.ToString() + "] konnte nicht erzeugt werden !  (vda.ListVDASatzString.Count=0) ";

                        clsLogbuchCon tmpLog = new clsLogbuchCon();
                        tmpLog.GL_User = this._GL_User;
                        tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                        tmpLog.LogText = this.Prozess + ".[VDA4913] - " + strError;
                        tmpLog.TableName = "Queue";
                        //decimal decTmp = 0;
                        tmpLog.TableID = this.Queue.ID;
                        this.ListError.Add(tmpLog);

                        clsMail EMail = new clsMail();
                        EMail.InitClass(this.GL_User, this.Sys);
                        EMail.Subject = this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error TASK_VDAwrite: Fehler bei Erstellung einer VDA4913 iDoc";
                        EMail.Message = tmpLog.LogText;
                        EMail.SendError();
                    }
                }

            }
            if (
                    (this.ASNFileTyp == constValue_AsnArt.const_Art_DESADV_BMW_4a) ||
                    (this.ASNFileTyp == constValue_AsnArt.const_Art_DESADV_BMW_4b) ||
                    (this.ASNFileTyp == constValue_AsnArt.const_Art_DESADV_BMW_4b_RL) ||
                    (this.ASNFileTyp == constValue_AsnArt.const_Art_DESADV_BMW_4b_ST) ||
                    (this.ASNFileTyp == constValue_AsnArt.const_Art_DESADV_BMW_6) ||
                    (this.ASNFileTyp == constValue_AsnArt.const_Art_DESADV_BMW_6_UB) ||
                    (this.ASNFileTyp == constValue_AsnArt.const_Art_EDIFACT_ASN_D97A) ||
                    (this.ASNFileTyp == constValue_AsnArt.const_Art_EDIFACT_INVRPT_D96A)
               )
            {
                tmpWriteFile = string.Empty;
                tmpStorFilePath = string.Empty;
                string tmpFileName = string.Empty;

                //--- unterscheidung BM, da BM nicht über die Queue ermittelt wird
                if (
                        (this.Job is clsJobs) &&
                        (this.Job.AsnTyp is clsASNTyp)
                   )
                {
                    this.FileName = this.Job.FileName;
                    tmpFileName = helper_FilePrefixAfterComProzess.GetPrefixVDA(this.Job.AsnTyp.Typ, this.Job.ID.ToString()) + this.FileName;
                }
                else
                {
                    this.Job = new clsJobs();
                    Job.InitClass(this.GLSystem, this.GL_User, false);
                    Job.GetJobByQueue(this.ASNFileTyp, this.Queue);

                    this.FileName = this.Job.FileName;
                    tmpFileName = helper_FilePrefixAfterComProzess.GetPrefixVDA(this.Job.AsnTyp.Typ, this.Queue.TableID.ToString()) + this.FileName;
                }
                this.FileName = Job.FileName;
                tmpWriteFile = this.Job.PathDirectory + "\\" + tmpFileName;

                if ((this.Queue.ID > 0) && (this.Queue.IsVirtFile))
                {
                    tmpWriteFile = this.Job.ASNFileStorePathDirectory + "\\" + tmpFileName;
                }
                tmpStorFilePath = this.Job.ASNFileStorePath + "\\" + tmpFileName;


                clsEdiVDACreate ediVDA = new clsEdiVDACreate();
                ediVDA.Prozess = this.Prozess;
                ediVDA.InitClass(this.GL_User, this.Job, this.Queue, this.Sys);

                if (ediVDA.ListErrorVDA.Count > 0)
                {
                    string strTxt = string.Empty;

                    for (Int32 l = 0; l <= ediVDA.ListErrorVDA.Count - 1; l++)
                    {
                        clsLogbuchCon tmpLogMail = (clsLogbuchCon)ediVDA.ListErrorVDA[l];
                        strTxt = strTxt + tmpLogMail.LogText + Environment.NewLine;
                        this.ListError.Add(tmpLogMail);
                    }
                    clsMail EMail = new clsMail();
                    EMail.InitClass(this.GL_User, this.Sys);
                    EMail.Subject = this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error TASK_VDAwrite: Fehler bei Erstellung einer Edi-VDA4987 iDoc";
                    EMail.Message = strTxt;
                    EMail.SendError();
                }
                else
                {
                    //In der ListeVDASatzString sind nun  alle stirngs enthalten und müsse nur
                    //nur noch in ein File geschrieben werden
                    if (ediVDA.ListEdiVDASatzString.Count > 0)
                    {
                        //Check auf Odette-StartDatei
                        if (this.Job.CreateOdetteStart)
                        {
                            //Check FIle exist
                            string strOdettePathFIle = this.Job.PathDirectory + "\\" + this.Job.OdetteStartFileName;
                            if (!File.Exists(strOdettePathFIle))
                            {
                                List<string> listTmp = new List<string>();
                                listTmp.Add("");
                                System.IO.File.WriteAllLines(strOdettePathFIle, listTmp.ToArray());
                            }
                        }
                        if (!this.Job.UseCRLF)
                        {
                            //TEst Blanks
                            //string LogText = string.Empty;
                            //for (Int32 i = 0; i <= vda.ListVDASatzString.Count - 1; i++)
                            //{
                            //    string strSatz = vda.ListVDASatzString[i];
                            //    LogText += strSatz + " -> Länge = "+ strSatz.Length.ToString()+ Environment.NewLine;
                            //}
                            //System.IO.File.WriteAllText(this.Job.PathDirectory + "\\Log_" + this.FileName, LogText);
                            System.IO.File.WriteAllText(tmpWriteFile, string.Join("", ediVDA.ListEdiVDASatzString.ToArray()));
                        }
                        else
                        {
                            System.IO.File.WriteAllLines(tmpWriteFile, ediVDA.ListEdiVDASatzString.ToArray());
                        }
                    }
                    else
                    {
                        string strError = "Datei: " + this.Job.FileName;
                        strError = strError + Environment.NewLine + "Queue ID: [" + this.Queue.ID.ToString() + "] konnte nicht erzeugt werden !  (vda.ListVDASatzString.Count=0) ";

                        clsLogbuchCon tmpLog = new clsLogbuchCon();
                        tmpLog.GL_User = this._GL_User;
                        tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                        tmpLog.LogText = this.Prozess + ".[" + this.ASNFileTyp.ToString() + "] - " + strError;
                        tmpLog.TableName = "Queue";
                        //decimal decTmp = 0;
                        tmpLog.TableID = this.Queue.ID;
                        this.ListError.Add(tmpLog);

                        clsMail EMail = new clsMail();
                        EMail.InitClass(this.GL_User, this.Sys);
                        EMail.Subject = this.Sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error TASK_VDAwrite: Fehler bei Erstellung einer VDA4913 iDoc";
                        EMail.Message = tmpLog.LogText;
                        EMail.SendError();
                    }
                }
            }
        }

        ///<summary>clsASN / FillListASN</summary>
        ///<remarks></remarks>
        public void FillListASN(decimal myANSArtID)
        {
            this.ASNArt.asnSatz.ASNArtID = myANSArtID;
            this.ASNArt.asnSatz.FillbyASNArtID();

            if (this.ASNArt.asnSatz.dtASNSaetze.Rows.Count > 0)
            {
                ListSatz = new List<clsASNArtSatz>();
                DictVDASatz = new Dictionary<string, clsASNArtSatz>();
                for (Int32 i = 0; i <= this.ASNArt.asnSatz.dtASNSaetze.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(this.ASNArt.asnSatz.dtASNSaetze.Rows[i]["ID"].ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        clsASNArtSatz tmpSatz = new clsASNArtSatz();
                        tmpSatz.InitClass(ref this.GL_User, this.SQLConIntern);
                        tmpSatz.ID = decTmp;
                        tmpSatz.Fill();
                        tmpSatz.FillList();
                        ListSatz.Add(tmpSatz);
                        DictVDASatz.Add(tmpSatz.Kennung.Trim(), tmpSatz);
                    }
                }
            }
        }
        /***********************************************************************************
         *                      static  procedures
         * ********************************************************************************/
        ///<summary>clsASN / GetAsnToCreateEA</summary>
        ///<remarks></remarks>
        public static System.Data.DataTable GetAsnToCreateEA(decimal myBenuzter)
        {
            System.Data.DataTable dt = new System.Data.DataTable("ASN");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASN WHERE IsRead=0  AND Direction='IN'";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myBenuzter, "ASN");
            return dt;
        }
        ///<summary>clsASN / GetAsnToCreateEA</summary>
        ///<remarks></remarks>
        public static List<string> GetListASNFileTyp()
        {
            List<string> retList = new List<string>()
                {
                      constValue_AsnArt.const_Art_VDA4913,
                      constValue_AsnArt.const_Art_VDA4905,
                      constValue_AsnArt.const_Art_XML_Uniport,
                      constValue_AsnArt.const_Art_EdifactVDA4984,
                      constValue_AsnArt.const_Art_EdifactVDA4987,
                      constValue_AsnArt.const_Art_BMWCall4913,
                      constValue_AsnArt.const_Art_EDIFACT_DELFOR_D97A,
                      constValue_AsnArt.const_Art_EDIFACT_ASN_D97A
                };
            return retList;
        }
        ///<summary>clsASN / GetAsnToCreateEA</summary>
        ///<remarks></remarks>
        //public static bool ExistNewASNToProceed(decimal myBenuzter, decimal myAbBereich)
        //{
        //    string strSQL = string.Empty;
        //    strSQL = "SELECT ID FROM ASN " +
        //                            "WHERE " +
        //                                " IsRead=0 " +
        //                                " AND Direction='IN' " +
        //                                " AND ArbeitsbereichID=" + (Int32)myAbBereich +
        //                                " AND ASNFileTyp IN ('" + constValue_AsnArt.const_Art_VDA4913 + "'" +
        //                                ",'" + constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_96A + "'" +
        //                                ",'" + constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A+ "'" +
        //                                ")"
        //                                ;
        //    return clsSQLCOM.ExecuteSQL_GetValueBool(strSQL, myBenuzter);
        //}

        //public static bool ExistNewASNToProceedByAsnFileType(decimal myBenuzter, decimal myAbBereich, string myASNFileTyp)
        //{
        //    string strSQL = string.Empty;
        //    strSQL = "SELECT ID FROM ASN " +
        //                            "WHERE " +
        //                                " IsRead=0 " +
        //                                " AND Direction='IN' " +
        //                                " AND ArbeitsbereichID=" + (Int32)myAbBereich +
        //                                " AND ASNFileTyp IN ('" + constValue_AsnArt.const_Art_VDA4913 + "'" +
        //                                ",'" + constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_96A + "'" +
        //                                ",'" + constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A + "'" +
        //                                ")"
        //                                ;
        //    return clsSQLCOM.ExecuteSQL_GetValueBool(strSQL, myBenuzter);
        //}

        //public static bool ExistNewASNToProceed(decimal myBenuzter, decimal myAbBereich)
        //{
        //    string strSQL = string.Empty;
        //    strSQL = "SELECT ID FROM ASN " +
        //                            "WHERE " +
        //                                " IsRead=0 " +
        //                                " AND Direction='IN' " +
        //                                " AND ArbeitsbereichID=" + (Int32)myAbBereich +
        //                                " AND ASNFileTyp IN ('" + constValue_AsnArt.const_Art_VDA4913 + "'" +
        //                                ",'" + constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_96A + "'" +
        //                                ",'" + constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A + "'" +
        //                                ")"
        //                                ;
        //    return clsSQLCOM.ExecuteSQL_GetValueBool(strSQL, myBenuzter);
        //}

        /*****************************************************************************
         *                          Methoden LVS
         *                          
         * **************************************************************************/
        ///<summary>clsASN / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUSer, Globals._GL_SYSTEM myGLSystem)
        {
            InitClass(myGLSystem, myGLUSer);
        }
        ///<summary>clsASN / GetASNTorRead</summary>
        ///<remarks></remarks>
        public clsLagerdaten GetASNTorRead()
        {

            //DataTable dtASN = GetASN();
            System.Data.DataTable dtTmp = new System.Data.DataTable();
            dtTmp = GetASN();
            System.Data.DataTable dtASN = EditTableForUse(dtTmp);

            //--mr jetzt EditTableForUse
            //dtASN.Columns.Add("ASNSender", typeof(decimal));
            //dtASN.Columns.Add("ASNReceiver", typeof(decimal));

            clsLagerdaten lagerdaten = new clsLagerdaten();
            lagerdaten.GLUser = this.GL_User;
            lagerdaten.GLSystem = this.GLSystem;
            lagerdaten.Sys = this.Sys;

            dtASNForEingang = new System.Data.DataTable("Eingang");
            dtASNForArt = new System.Data.DataTable("Artikel");

            dtASNForEingang = lagerdaten.GetLfsKopfdaten(ref dtASN);
            if (dtASNForEingang.Rows.Count > 0)
            {
                dtASNForArt = lagerdaten.GetArtikelDaten1(ref dtASN);
            }
            return lagerdaten;
        }
        ///<summary>clsASN / GetVDA4905</summary>
        ///<remarks></remarks> 
        public void GetVDA4905(DateTime SelDate, decimal myVDA4905ReceiverID)
        {
            VDA4905 = new clsVDA4905();
            VDA4905.InitClass(this.GL_User, this.GLSystem, this.Sys);
            VDA4905.Datum = SelDate;
            VDA4905.GetLiefereinteilungen(myVDA4905ReceiverID);
        }
        ///<summary>clsASN / GetVDA4905</summary>
        ///<remarks></remarks> 
        public void InitBSInfoVDA4905(bool bChecked, bool InclSPL, bool bActiveGT)
        {
            VDA4905 = new clsVDA4905();
            VDA4905.InitClass(this.GL_User, this.GLSystem, this.Sys);
            VDA4905.EventWorkingReport += VDA4905_EventWorkingReport;
            VDA4905.LoadBSInfo4905(bChecked, InclSPL, bActiveGT);
        }

        ///<summary>clsASN / VDA4905_EventWorkingReport</summary>
        ///<remarks></remarks> 
        private void VDA4905_EventWorkingReport(object sender, EventArgs e)
        {
            this.WorkingReportText = this.VDA4905.WorkingReportText;
            this.OnWorkingReportChange(EventArgs.Empty);
        }
        ///<summary>clsASN / GetASN</summary>
        ///<remarks>Alle Artikel im Ausgang als nicht geprüft markieren</remarks> 
        ///

        internal string sql_GetASN
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select " +
                                    "ASNValue.* " +
                                    ",ASN.ASNFileTyp " +
                                    ", ASNTyp.Typ as Typ " +
                                    ", ASNArtSatzFeld.Kennung" +
                                    ", ASNArtSatz.Kennung as SatzKennung" +
                                    " FROM ASNValue " +
                                    "INNER JOIN ASN ON ASN.ID=ASNValue.ASNID " +
                                    "INNER JOIN ASNTyp ON ASNTyp.ID=ASN.ASNTypID " +
                                    "INNER JOIN ASNArtSatzFeld ON ASNArtSatzFeld.ID = ASNValue.ASNFieldID " +
                                    "INNER JOIN ASNArtSatz ON ASNArtSatz.ID =ASNArtSatzFeld.ASNSatzID ";
                return strSql;
            }
        }



        public string sql_GetASNByAsnValue(string mySearchValue)
        {
            //DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select ";
            strSql += "Distinct ASNID ";
            strSql += ", ASNValue.Value as AsnValue ";
            strSql += ", ArbeitsbereichID as WorkspaceId";
            strSql += ", LEN(ASNValue.Value) as Length ";
            strSql += ", ASN.ASNFileTyp as AsnFileTyp ";
            strSql += " FROM ASNValue ";
            strSql += "INNER JOIN ASN ON ASN.ID=ASNValue.ASNID ";
            strSql += "INNER JOIN ASNTyp ON ASNTyp.ID=ASN.ASNTypID ";
            strSql += "INNER JOIN ASNArtSatzFeld ON ASNArtSatzFeld.ID = ASNValue.ASNFieldID ";
            strSql += "INNER JOIN ASNArtSatz ON ASNArtSatz.ID =ASNArtSatzFeld.ASNSatzID ";
            strSql += " WHERE ";
            strSql += " ASN.ASNFileTyp='" + constValue_AsnArt.const_Art_VDA4913 + "'";
            strSql += " AND ASN.IsRead=0 ";
            strSql += " AND (CHARINDEX('" + mySearchValue.ToUpper() + "', UPPER(ASNValue.Value)) > 0) ";
            //strSql += "order by LEN(ASNValue.Value) ";

            strSql += " UNION ";

            strSql += "Select ";
            strSql += "DISTINCT ASN.ID ";
            strSql += ", EdifactValue.EdiSegmentElement as AsnValue";
            strSql += ", asn.ArbeitsbereichID as WorkspaceId";
            strSql += ", 0 as Length";
            strSql += ", ASN.ASNFileTyp as AsnFileTyp ";
            strSql += " FROM EdifactValue ";
            strSql += "INNER JOIN ASNValue ON ASNValue.ID = EdifactValue.AsnId ";
            strSql += "INNER JOIN ASN ON ASN.ID = EdifactValue.ASNID ";
            strSql += "INNER JOIN ASNTyp ON ASNTyp.ID = ASN.ASNTypID ";
            strSql += "WHERE ";
            strSql += "ASN.ASNFileTyp IN ('" + constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A + "') ";
            strSql += "AND ASN.IsRead = 0 ";
            strSql += "AND(CHARINDEX('" + mySearchValue.ToUpper() + "', UPPER(EdifactValue.EdiSegmentElement)) > 0) ";

            return strSql;
        }


        public System.Data.DataTable GetASNByASNId(List<int> myListAsnId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string strSql = string.Empty;
            strSql = sql_GetASN;
            strSql += "WHERE ASN.ID in (" + string.Join(",", myListAsnId) + ") ";
            strSql += "order by ASNValue.ID ";

            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ASN");
            return dt;
        }
        ///<summary>clsASN / GetASN</summary>
        ///<remarks>Alle Artikel im Ausgang als nicht geprüft markieren</remarks> 
        public System.Data.DataTable GetASN()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string strSql = string.Empty;
            strSql += sql_GetASN;
            strSql += "WHERE ";
            strSql += "ASN.ASNFileTyp='" + constValue_AsnArt.const_Art_VDA4913 + "'" +
                                    " AND ASN.IsRead=0 " +
                                    " AND ASN.ArbeitsbereichID= " + this.GLSystem.sys_ArbeitsbereichID +
                                    " Order by ASNValue.ID; ";


            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ASN");
            return dt;
            //Table die anschließend die bearbeiteten Daten enthält
            //DataTable dtOrg = dt.DefaultView.ToTable();
            //dtOrg.Rows.Clear();
            //DataTable dtReCreatedASNValue = new DataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    //Gruppierung nach ASNID muss vorgenommen werden
            //    DataTable dtAsnID = dt.DefaultView.ToTable(true, "ASNID");
            //    foreach (DataRow rowCount in dtAsnID.Rows)
            //    {
            //        Int32 iAsnID = 0;
            //        Int32.TryParse(rowCount["ASNID"].ToString(), out iAsnID);
            //        dt.DefaultView.RowFilter = "ASNID=" + iAsnID;

            //        DataTable dtAsnValueSource = dt.DefaultView.ToTable();  //beinhaltet die Value für die gefilterte ASNID

            //        //DataTable dtTmpValue = dt.DefaultView.ToTable();  //beinhaltet die Value für die gefilterte ASNID
            //        DataTable dtSplitt = new DataTable();  //beinhaltet die Value für die gefilterte ASNID
            //        dtSplitt = dtAsnValueSource.Clone();

            //        try
            //        {
            //            //Test für 712
            //            List<DataTable> ListTmpValue = new List<DataTable>();


            //            // Datatable müssen aufgeteilt werden 
            //            //- je ein Datatable je neuem Transport SATZ712
            //            // gesammelt in der List
            //            bool bFindS71201 = false;
            //            //foreach (DataRow r in dtAsnValueSource.Rows)
            //            for (int x = 0; x <= dtAsnValueSource.Rows.Count - 1; x++)
            //            {
            //                DataRow r = dtAsnValueSource.Rows[x];
            //                int iFieldId = 0;
            //                if (int.TryParse(r["ASNFieldID"].ToString(), out iFieldId))
            //                {
            //                    if (iFieldId > 0)
            //                    {
            //                        if ((iFieldId.Equals(13)) && (bFindS71201))
            //                        {
            //                            ListTmpValue.Add(dtSplitt);
            //                            dtSplitt = new DataTable();
            //                            dtSplitt = dtAsnValueSource.Clone();
            //                            //dtSplitt = dtAsnValueSource.DefaultView.ToTable();
            //                            //dtSplitt.Rows.Clear();
            //                            dtSplitt.ImportRow(r);
            //                        }
            //                        else if ((iFieldId.Equals(13)) && (!bFindS71201))
            //                        {
            //                            bFindS71201 = true;
            //                            dtSplitt.ImportRow(r);
            //                        }
            //                        else
            //                        {
            //                            dtSplitt.ImportRow(r);
            //                        }
            //                    }

            //                    if (x == (dtAsnValueSource.Rows.Count - 1))
            //                    {
            //                        ListTmpValue.Add(dtSplitt);
            //                    }
            //                }
            //            }

            //            DataTable dtTmpASNValue = new DataTable();        //beinhaltet die neuaufgebaute Table der ASNID
            //            List<DataTable> ListTmpASNValue = new List<DataTable>();

            //            // --- die einzelnen Datatable der Liste wird durchlaufen und
            //            // --- der Neuaufbau der ASNValue Tabelle wird durchgeführt
            //            // --- die Liste ListTmpValue enthält hier die nach Satz712
            //            // --- getrennten ASNValue - Sätze

            //            foreach (DataTable dtTmpValue in ListTmpValue)
            //            {
            //                dtTmpASNValue = new DataTable(); //beinhaltet die neuaufgebaute Table der ASNID
            //                dtTmpASNValue = dtTmpValue.Clone();

            //                DataTable dtVDA4913ElemLfs = new DataTable("ElementsLfs");
            //                dtVDA4913ElemLfs.Columns.Add("ID713", typeof(Int32));
            //                dtVDA4913ElemLfs.Columns.Add("Count714", typeof(Int32));
            //                dtVDA4913ElemLfs.Columns.Add("Count715", typeof(Int32));
            //                dtVDA4913ElemLfs.Columns.Add("Count716", typeof(Int32));
            //                dtVDA4913ElemLfs.Columns.Add("Count717", typeof(Int32));
            //                dtVDA4913ElemLfs.Columns.Add("Count718", typeof(Int32));
            //                dtVDA4913ElemLfs.Columns.Add("LfsNr", typeof(string));

            //                DataRow tmpImpRow = dtVDA4913ElemLfs.NewRow();
            //                Int32 iAsnValueTableID713 = 0;
            //                Int32 iCount714 = 0;
            //                Int32 iCount715 = 0;
            //                Int32 iCount716 = 0;
            //                Int32 iCount717 = 0;
            //                Int32 iCount718 = 0;

            //                // --- 712 Datensatz hinzufügen
            //                dtTmpValue.DefaultView.RowFilter = "SatzKennung=712";
            //                DataTable dt712 = dtTmpValue.DefaultView.ToTable();
            //                dtTmpASNValue = dt712.Copy();
            //                dtTmpValue.DefaultView.RowFilter = string.Empty;

            //                dtTmpValue.DefaultView.RowFilter = "SatzKennung<>711 AND SatzKennung<>712";
            //                DataTable dtCountElements = dtTmpValue.DefaultView.ToTable();
            //                dtCountElements.Columns.Add("ID713F01", typeof(int));
            //                dtCountElements.Columns.Add("LfsNr", typeof(string));
            //                dtTmpValue.DefaultView.RowFilter = string.Empty;

            //                string strKennungLastItem = dtCountElements.Rows[dtCountElements.Rows.Count - 1]["Kennung"].ToString().Trim();
            //                string tmp713F03_Lfs = string.Empty;
            //                iAsnValueTableID713 = 0;
            //                //LfsNr füllen
            //                for (Int32 i = 0; i <= dtCountElements.Rows.Count - 1; i++)
            //                {
            //                    string strKenn = dtCountElements.Rows[i]["Kennung"].ToString().Trim();
            //                    switch (strKenn)
            //                    {
            //                        case "SATZ713F01":
            //                            //1. 713er Satz
            //                            if (Int32.TryParse(dtCountElements.Rows[i]["ID"].ToString(), out iAsnValueTableID713))
            //                            {
            //                                tmp713F03_Lfs = string.Empty;
            //                                if (dtCountElements.Rows[i + 2]["Value"] != null)
            //                                {
            //                                    tmp713F03_Lfs = dtCountElements.Rows[i + 2]["Value"].ToString();
            //                                }
            //                            }
            //                            break;
            //                        case "SATZ719F01":
            //                            tmp713F03_Lfs = string.Empty;
            //                            iAsnValueTableID713 = 0;

            //                            break;
            //                    }
            //                    dtCountElements.Rows[i]["ID713F01"] = iAsnValueTableID713;
            //                    dtCountElements.Rows[i]["LfsNr"] = tmp713F03_Lfs;
            //                }


            //                for (Int32 i = 0; i <= dtCountElements.Rows.Count - 1; i++)
            //                {
            //                    string strKenn = dtCountElements.Rows[i]["Kennung"].ToString().Trim();
            //                    switch (strKenn)
            //                    {
            //                        case "SATZ713F01":
            //                            tmpImpRow = dtVDA4913ElemLfs.NewRow();
            //                            iAsnValueTableID713 = 0;
            //                            tmp713F03_Lfs = string.Empty;

            //                            //713er Satz
            //                            if (Int32.TryParse(dtCountElements.Rows[i]["ID"].ToString(), out iAsnValueTableID713))
            //                            {

            //                            }
            //                            tmp713F03_Lfs = dtCountElements.Rows[i]["LfsNr"].ToString();
            //                            tmpImpRow["ID713"] = iAsnValueTableID713;
            //                            tmpImpRow["LfsNr"] = tmp713F03_Lfs;

            //                            if (iAsnValueTableID713 > 0)
            //                            {
            //                                int iTmp = 0;
            //                                if (int.TryParse(tmp713F03_Lfs, out iTmp))
            //                                {
            //                                    iCount714 = dtCountElements.Select("ASNFieldID=55 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
            //                                    iCount715 = dtCountElements.Select("ASNFieldID=77 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
            //                                    iCount716 = dtCountElements.Select("ASNFieldID=93 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
            //                                    iCount717 = dtCountElements.Select("ASNFieldID=99 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
            //                                    iCount718 = dtCountElements.Select("ASNFieldID=108 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
            //                                }
            //                                else
            //                                {
            //                                    iCount714 = dtCountElements.Select("ASNFieldID=55 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
            //                                    iCount715 = dtCountElements.Select("ASNFieldID=77 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
            //                                    iCount716 = dtCountElements.Select("ASNFieldID=93 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
            //                                    iCount717 = dtCountElements.Select("ASNFieldID=99 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
            //                                    iCount718 = dtCountElements.Select("ASNFieldID=108 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
            //                                }

            //                                tmpImpRow["Count714"] = iCount714;
            //                                tmpImpRow["Count715"] = iCount715;
            //                                tmpImpRow["Count716"] = iCount716;
            //                                tmpImpRow["Count717"] = iCount717;
            //                                tmpImpRow["Count718"] = iCount718;

            //                                dtVDA4913ElemLfs.Rows.Add(tmpImpRow);
            //                                iCount714 = 0;
            //                                iCount715 = 0;
            //                                iCount716 = 0;
            //                                iCount717 = 0;
            //                                iCount718 = 0;
            //                                //tmpImpRow = dtVDA4913ElemLfs.NewRow();
            //                            }
            //                            break;

            //                    }
            //                }

            //                dtTmpValue.DefaultView.RowFilter = "SatzKennung=713";
            //                DataTable dt713 = dtTmpValue.DefaultView.ToTable();
            //                dtTmpValue.DefaultView.RowFilter = string.Empty;

            //                for (Int32 x = 0; x <= dtVDA4913ElemLfs.Rows.Count - 1; x++)
            //                {
            //                    Int32 iID713 = 0;
            //                    Int32 iID713Next = 0;

            //                    string strID = dtVDA4913ElemLfs.Rows[x]["ID713"].ToString();
            //                    string strIDNext = string.Empty;
            //                    //prüfen und Anzahl ermitteln
            //                    iCount714 = 0;
            //                    string str714 = dtVDA4913ElemLfs.Rows[x]["Count714"].ToString();
            //                    Int32.TryParse(str714, out iCount714);

            //                    iCount715 = 0;
            //                    string str715 = dtVDA4913ElemLfs.Rows[x]["Count715"].ToString();
            //                    Int32.TryParse(str715, out iCount715);

            //                    iCount716 = 0;
            //                    string str716 = dtVDA4913ElemLfs.Rows[x]["Count716"].ToString();
            //                    Int32.TryParse(str716, out iCount716);

            //                    iCount717 = 0;
            //                    string str717 = dtVDA4913ElemLfs.Rows[x]["Count717"].ToString();
            //                    Int32.TryParse(str717, out iCount717);

            //                    iCount718 = 0;
            //                    string str718 = dtVDA4913ElemLfs.Rows[x]["Count718"].ToString();
            //                    Int32.TryParse(str718, out iCount718);

            //                    //den 713er Satz zur Tabelle hinzufügen
            //                    bool b713 = true;
            //                    Int32 rCount713 = 0;
            //                    string strKennung = string.Empty;
            //                    string oldKennung = string.Empty;
            //                    while (b713)
            //                    {
            //                        if (dt713.Rows.Count > 0)
            //                        {
            //                            if (rCount713 <= (dt713.Rows.Count - 1))
            //                            {
            //                                DataRow r713 = dt713.Rows[rCount713];
            //                                strKennung = r713["Kennung"].ToString();

            //                                if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ713F01) && (oldKennung != string.Empty))
            //                                {
            //                                    b713 = !(clsASN.const_VDA4913SatzField_SATZ713F01 == strKennung);
            //                                }
            //                                if (b713)
            //                                {
            //                                    dtTmpASNValue.ImportRow(r713);
            //                                    dt713.Rows.RemoveAt(rCount713);
            //                                    //rCount713++;
            //                                    //rCount714++;
            //                                }
            //                                oldKennung = strKennung;
            //                            }
            //                            else
            //                            {
            //                                b713 = false;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            b713 = false;
            //                        }
            //                    }

            //                    //prüfen existiert eine weitere Zeile
            //                    if (x + 1 <= dtVDA4913ElemLfs.Rows.Count - 1)
            //                    {
            //                        strIDNext = dtVDA4913ElemLfs.Rows[x + 1]["ID713"].ToString();
            //                    }
            //                    if (Int32.TryParse(strID, out iID713))
            //                    {
            //                        string strFilter = "ID>=" + iID713.ToString();
            //                        if (Int32.TryParse(strIDNext, out iID713Next))
            //                        {
            //                            if (iID713Next > 0)
            //                            {
            //                                strFilter = strFilter + " AND ID<" + iID713Next.ToString();
            //                            }
            //                        }
            //                        dtTmpValue.DefaultView.RowFilter = strFilter;
            //                        DataTable dtTmpLfsValue = dtTmpValue.DefaultView.ToTable();
            //                        //dtTmpLfsValue enthält nun die VDAElemente eines LIeferscheins

            //                        //Aufteilen der Sätze 714 bis 718
            //                        dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=714";
            //                        DataTable dt714 = dtTmpLfsValue.DefaultView.ToTable();
            //                        dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

            //                        dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=715";
            //                        DataTable dt715 = dtTmpLfsValue.DefaultView.ToTable();
            //                        dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

            //                        dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=716";
            //                        DataTable dt716 = dtTmpLfsValue.DefaultView.ToTable();
            //                        dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

            //                        dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=717";
            //                        DataTable dt717 = dtTmpLfsValue.DefaultView.ToTable();
            //                        dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

            //                        dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=718";
            //                        DataTable dt718 = dtTmpLfsValue.DefaultView.ToTable();
            //                        dtTmpLfsValue.DefaultView.RowFilter = string.Empty;
            //                        Int32 iCountArt = iCount714;
            //                        if (iCount714 < iCount715)
            //                        {
            //                            iCountArt = iCount715;
            //                        }
            //                        if (iCount714 < iCount716)
            //                        {
            //                            iCountArt = iCount716;
            //                        }
            //                        if (iCount714 < iCount717)
            //                        {
            //                            iCountArt = iCount717;
            //                        }
            //                        if (iCount714 < iCount718)
            //                        {
            //                            iCountArt = iCount718;
            //                        }

            //                        //714 Satz muss für die Verarbeitung je Artikel einmal vorhanden sein
            //                        Int32 iCount = 1; //da der 714 Satz schon einmal vorhanden ist
            //                        DataTable dtTmp714 = dt714.DefaultView.ToTable();
            //                        while (iCountArt > iCount)
            //                        {
            //                            //714
            //                            foreach (DataRow row1 in dtTmp714.Rows)
            //                            {
            //                                dt714.ImportRow(row1);
            //                            }
            //                            iCount++;
            //                        }

            //                        //kommt pro Artikel nur einmal vor, muss aber hier für jeden Artikel eingefügt werden
            //                        DataTable dtTmp716 = dt716.DefaultView.ToTable();
            //                        iCount = 1;
            //                        while (iCountArt > iCount)
            //                        {
            //                            //716
            //                            foreach (DataRow row2 in dtTmp716.Rows)
            //                            {
            //                                dt716.ImportRow(row2);
            //                            }
            //                            iCount++;
            //                        }
            //                        Int32 iTmp = dt714.Rows.Count;

            //                        //Tabellen zu einer zusammensetzen
            //                        for (Int32 i = 0; i <= iCountArt - 1; i++)
            //                        {
            //                            //Satz714
            //                            bool b714 = true;
            //                            Int32 rCount714 = 0;
            //                            strKennung = string.Empty;
            //                            oldKennung = string.Empty;
            //                            while (b714)
            //                            {
            //                                if (dt714.Rows.Count > 0)
            //                                {
            //                                    if (rCount714 <= (dt714.Rows.Count - 1))
            //                                    {
            //                                        DataRow r714 = dt714.Rows[rCount714];
            //                                        strKennung = r714["Kennung"].ToString();

            //                                        if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ714F01) && (oldKennung != string.Empty))
            //                                        {
            //                                            b714 = !(clsASN.const_VDA4913SatzField_SATZ714F01 == strKennung);
            //                                        }
            //                                        if (b714)
            //                                        {
            //                                            dtTmpASNValue.ImportRow(r714);
            //                                            dt714.Rows.RemoveAt(rCount714);
            //                                            //rCount714++;
            //                                        }
            //                                        oldKennung = strKennung;
            //                                    }
            //                                    else
            //                                    {
            //                                        b714 = false;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    b714 = false;
            //                                }
            //                            }
            //                            //Satz715
            //                            bool b715 = true;
            //                            Int32 rCount715 = 0;
            //                            strKennung = string.Empty;
            //                            oldKennung = string.Empty;
            //                            while (b715)
            //                            {
            //                                if (dt715.Rows.Count > 0)
            //                                {
            //                                    if (rCount715 <= (dt715.Rows.Count - 1))
            //                                    {
            //                                        DataRow r715 = dt715.Rows[rCount715];
            //                                        strKennung = r715["Kennung"].ToString();

            //                                        if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ715F01) && (oldKennung != string.Empty))
            //                                        {
            //                                            b715 = !(clsASN.const_VDA4913SatzField_SATZ715F01 == strKennung);
            //                                        }
            //                                        if (b715)
            //                                        {
            //                                            dtTmpASNValue.ImportRow(r715);
            //                                            dt715.Rows.RemoveAt(rCount715);
            //                                            //rCount715++;
            //                                        }
            //                                        oldKennung = strKennung;
            //                                    }
            //                                    else
            //                                    {
            //                                        b715 = false;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    b715 = false;
            //                                }
            //                            }
            //                            //Satz716
            //                            bool b716 = true;
            //                            Int32 rCount716 = 0;
            //                            strKennung = string.Empty;
            //                            oldKennung = string.Empty;
            //                            while (b716)
            //                            {
            //                                if (dt716.Rows.Count > 0)
            //                                {
            //                                    if (rCount716 <= (dt716.Rows.Count - 1))
            //                                    {
            //                                        DataRow r716 = dt716.Rows[rCount716];
            //                                        strKennung = r716["Kennung"].ToString();

            //                                        if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ716F01) && (oldKennung != string.Empty))
            //                                        {
            //                                            b716 = !(clsASN.const_VDA4913SatzField_SATZ716F01 == strKennung);
            //                                        }
            //                                        if (b716)
            //                                        {
            //                                            dtTmpASNValue.ImportRow(r716);
            //                                            dt716.Rows.RemoveAt(rCount716);
            //                                            //rCount716++;
            //                                        }
            //                                        oldKennung = strKennung;
            //                                    }
            //                                    else
            //                                    {
            //                                        b716 = false;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    b716 = false;
            //                                }
            //                            }
            //                            //Satz717
            //                            bool b717 = true;
            //                            Int32 rCount717 = 0;
            //                            strKennung = string.Empty;
            //                            oldKennung = string.Empty;
            //                            while (b717)
            //                            {
            //                                if (dt717.Rows.Count > 0)
            //                                {
            //                                    if (rCount717 <= (dt717.Rows.Count - 1))
            //                                    {
            //                                        DataRow r717 = dt717.Rows[rCount717];
            //                                        strKennung = r717["Kennung"].ToString();

            //                                        if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ717F01) && (oldKennung != string.Empty))
            //                                        {
            //                                            b717 = !(clsASN.const_VDA4913SatzField_SATZ717F01 == strKennung);
            //                                        }
            //                                        if (b717)
            //                                        {
            //                                            dtTmpASNValue.ImportRow(r717);
            //                                            dt717.Rows.RemoveAt(rCount717);
            //                                            //rCount717++;
            //                                        }
            //                                        oldKennung = strKennung;
            //                                    }
            //                                    else
            //                                    {
            //                                        b717 = false;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    b717 = false;
            //                                }
            //                            }


            //                            //Satz718
            //                            bool b718 = true;
            //                            Int32 rCount718 = 0;
            //                            strKennung = string.Empty;
            //                            oldKennung = string.Empty;
            //                            while (b718)
            //                            {
            //                                if (dt718.Rows.Count > 0)
            //                                {
            //                                    if (rCount718 <= (dt718.Rows.Count - 1))
            //                                    {
            //                                        DataRow r718 = dt718.Rows[rCount717];
            //                                        strKennung = r718["Kennung"].ToString();

            //                                        if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ718F01) && (oldKennung != string.Empty))
            //                                        {
            //                                            b718 = !(clsASN.const_VDA4913SatzField_SATZ718F01 == strKennung);
            //                                        }
            //                                        if (b718)
            //                                        {
            //                                            dtTmpASNValue.ImportRow(r718);
            //                                            dt718.Rows.RemoveAt(rCount718);
            //                                            //rCount717++;
            //                                        }
            //                                        oldKennung = strKennung;
            //                                    }
            //                                    else
            //                                    {
            //                                        b718 = false;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    b718 = false;
            //                                }
            //                            }
            //                            iCount++;
            //                        }
            //                    }
            //                }
            //                ListTmpASNValue.Add(dtTmpASNValue);
            //            }// Ende List

            //            dtAsnValueSource.DefaultView.RowFilter = "SatzKennung=711";
            //            DataTable dt711 = dtAsnValueSource.DefaultView.ToTable();

            //            //-- 711 hinzufügen
            //            dtReCreatedASNValue = dt711.Copy();


            //            // --- Datatable dtReCreatedASNValue füllen mit den Datensätzen 
            //            // --- aus der ListTmpASNVAlue
            //            // --- 712 bis 718 je Transport
            //            foreach (DataTable dtTmp in ListTmpASNValue)
            //            {
            //                foreach (DataRow row in dtTmp.Rows)
            //                {
            //                    dtReCreatedASNValue.ImportRow(row);
            //                }
            //            }

            //            dtAsnValueSource.DefaultView.RowFilter = "SatzKennung=719";
            //            DataTable dt719 = dtAsnValueSource.DefaultView.ToTable();

            //            //Satz 719 hinzufügen
            //            foreach (DataRow row in dt719.Rows)
            //            {
            //                dtReCreatedASNValue.ImportRow(row);
            //            }

            //            //Spalte LfdNr -> Zähler
            //            if (!dtOrg.Columns.Contains("LfdNr"))
            //            {
            //                dtOrg.Columns.Add("LfdNr", typeof(Int32));
            //            }
            //            if (!dtReCreatedASNValue.Columns.Contains("LfdNr"))
            //            {
            //                dtReCreatedASNValue.Columns.Add("LfdNr", typeof(Int32));
            //            }

            //            //Rows aus der TMPTable dtASNIDValue
            //            Int32 iLfdNr = 1;
            //            foreach (DataRow rowImp in dtReCreatedASNValue.Rows)
            //            {
            //                rowImp["LfdNr"] = iLfdNr;
            //                dtOrg.ImportRow(rowImp);
            //                iLfdNr++;
            //            }
            //            dtTmpASNValue.Rows.Clear();

            //        }
            //        catch (Exception ex)
            //        {
            //            string st = ex.ToString();
            //        }
            //    }
            //}
            //return dtOrg;
        }

        public System.Data.DataTable EditTableForUse(System.Data.DataTable dt)
        {
            System.Data.DataTable dtOrg = dt.DefaultView.ToTable();
            dtOrg.Rows.Clear();
            System.Data.DataTable dtReCreatedASNValue = new System.Data.DataTable();

            if (dt.Rows.Count > 0)
            {
                //Gruppierung nach ASNID muss vorgenommen werden
                System.Data.DataTable dtAsnID = dt.DefaultView.ToTable(true, "ASNID");
                foreach (DataRow rowCount in dtAsnID.Rows)
                {
                    Int32 iAsnID = 0;
                    Int32.TryParse(rowCount["ASNID"].ToString(), out iAsnID);
                    dt.DefaultView.RowFilter = "ASNID=" + iAsnID;

                    System.Data.DataTable dtAsnValueSource = dt.DefaultView.ToTable();  //beinhaltet die Value für die gefilterte ASNID

                    //DataTable dtTmpValue = dt.DefaultView.ToTable();  //beinhaltet die Value für die gefilterte ASNID
                    System.Data.DataTable dtSplitt = new System.Data.DataTable();  //beinhaltet die Value für die gefilterte ASNID
                    dtSplitt = dtAsnValueSource.Clone();

                    try
                    {
                        //Test für 712
                        List<System.Data.DataTable> ListTmpValue = new List<System.Data.DataTable>();
                        // Datatable müssen aufgeteilt werden 
                        //- je ein Datatable je neuem Transport SATZ712
                        // gesammelt in der List
                        bool bFindS71201 = false;
                        //foreach (DataRow r in dtAsnValueSource.Rows)
                        for (int x = 0; x <= dtAsnValueSource.Rows.Count - 1; x++)
                        {
                            DataRow r = dtAsnValueSource.Rows[x];
                            int iFieldId = 0;
                            if (int.TryParse(r["ASNFieldID"].ToString(), out iFieldId))
                            {
                                if (iFieldId > 0)
                                {
                                    if (iFieldId.Equals(107))
                                    {
                                        string str = string.Empty;
                                    }
                                    if ((iFieldId.Equals(13)) && (bFindS71201))
                                    {
                                        ListTmpValue.Add(dtSplitt);
                                        dtSplitt = new System.Data.DataTable();
                                        dtSplitt = dtAsnValueSource.Clone();
                                        //dtSplitt = dtAsnValueSource.DefaultView.ToTable();
                                        //dtSplitt.Rows.Clear();
                                        dtSplitt.ImportRow(r);
                                    }
                                    else if ((iFieldId.Equals(13)) && (!bFindS71201))
                                    {
                                        bFindS71201 = true;
                                        dtSplitt.ImportRow(r);
                                    }
                                    else
                                    {
                                        dtSplitt.ImportRow(r);
                                    }
                                }

                                if (x == (dtAsnValueSource.Rows.Count - 1))
                                {
                                    ListTmpValue.Add(dtSplitt);
                                }
                            }
                        }

                        System.Data.DataTable dtTmpASNValue = new System.Data.DataTable();        //beinhaltet die neuaufgebaute Table der ASNID
                        List<System.Data.DataTable> ListTmpASNValue = new List<System.Data.DataTable>();

                        // --- die einzelnen Datatable der Liste wird durchlaufen und
                        // --- der Neuaufbau der ASNValue Tabelle wird durchgeführt
                        // --- die Liste ListTmpValue enthält hier die nach Satz712
                        // --- getrennten ASNValue - Sätze

                        foreach (System.Data.DataTable dtTmpValue in ListTmpValue)
                        {
                            dtTmpASNValue = new System.Data.DataTable(); //beinhaltet die neuaufgebaute Table der ASNID
                            dtTmpASNValue = dtTmpValue.Clone();

                            System.Data.DataTable dtVDA4913ElemLfs = new System.Data.DataTable("ElementsLfs");
                            dtVDA4913ElemLfs.Columns.Add("ID713", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("Count714", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("Count715", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("Count716", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("Count717", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("Count718", typeof(Int32));
                            dtVDA4913ElemLfs.Columns.Add("LfsNr", typeof(string));

                            DataRow tmpImpRow = dtVDA4913ElemLfs.NewRow();
                            Int32 iAsnValueTableID713 = 0;
                            Int32 iCount714 = 0;
                            Int32 iCount715 = 0;
                            Int32 iCount716 = 0;
                            Int32 iCount717 = 0;
                            Int32 iCount718 = 0;

                            // --- 712 Datensatz hinzufügen
                            dtTmpValue.DefaultView.RowFilter = "SatzKennung=712";
                            System.Data.DataTable dt712 = dtTmpValue.DefaultView.ToTable();
                            dtTmpASNValue = dt712.Copy();
                            dtTmpValue.DefaultView.RowFilter = string.Empty;

                            dtTmpValue.DefaultView.RowFilter = "SatzKennung<>711 AND SatzKennung<>712";
                            System.Data.DataTable dtCountElements = dtTmpValue.DefaultView.ToTable();
                            dtCountElements.Columns.Add("ID713F01", typeof(int));
                            dtCountElements.Columns.Add("LfsNr", typeof(string));
                            dtTmpValue.DefaultView.RowFilter = string.Empty;

                            string strKennungLastItem = dtCountElements.Rows[dtCountElements.Rows.Count - 1]["Kennung"].ToString().Trim();
                            string tmp713F03_Lfs = string.Empty;
                            iAsnValueTableID713 = 0;
                            //LfsNr füllen
                            for (Int32 i = 0; i <= dtCountElements.Rows.Count - 1; i++)
                            {
                                string strKenn = dtCountElements.Rows[i]["Kennung"].ToString().Trim();
                                switch (strKenn)
                                {
                                    case "SATZ713F01":
                                        //1. 713er Satz
                                        if (Int32.TryParse(dtCountElements.Rows[i]["ID"].ToString(), out iAsnValueTableID713))
                                        {
                                            tmp713F03_Lfs = string.Empty;
                                            if (dtCountElements.Rows[i + 2]["Value"] != null)
                                            {
                                                tmp713F03_Lfs = dtCountElements.Rows[i + 2]["Value"].ToString();
                                            }
                                        }
                                        break;
                                    case "SATZ719F01":
                                        tmp713F03_Lfs = string.Empty;
                                        iAsnValueTableID713 = 0;

                                        break;
                                }
                                dtCountElements.Rows[i]["ID713F01"] = iAsnValueTableID713;
                                dtCountElements.Rows[i]["LfsNr"] = tmp713F03_Lfs;
                            }


                            for (Int32 i = 0; i <= dtCountElements.Rows.Count - 1; i++)
                            {
                                string strKenn = dtCountElements.Rows[i]["Kennung"].ToString().Trim();
                                switch (strKenn)
                                {
                                    case "SATZ713F01":
                                        tmpImpRow = dtVDA4913ElemLfs.NewRow();
                                        iAsnValueTableID713 = 0;
                                        tmp713F03_Lfs = string.Empty;

                                        //713er Satz
                                        if (Int32.TryParse(dtCountElements.Rows[i]["ID"].ToString(), out iAsnValueTableID713))
                                        {

                                        }
                                        tmp713F03_Lfs = dtCountElements.Rows[i]["LfsNr"].ToString();
                                        tmpImpRow["ID713"] = iAsnValueTableID713;
                                        tmpImpRow["LfsNr"] = tmp713F03_Lfs;

                                        if (iAsnValueTableID713 > 0)
                                        {
                                            int iTmp = 0;
                                            if (int.TryParse(tmp713F03_Lfs, out iTmp))
                                            {
                                                iCount714 = dtCountElements.Select("ASNFieldID=55 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
                                                iCount715 = dtCountElements.Select("ASNFieldID=77 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
                                                iCount716 = dtCountElements.Select("ASNFieldID=93 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
                                                iCount717 = dtCountElements.Select("ASNFieldID=99 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
                                                iCount718 = dtCountElements.Select("ASNFieldID=108 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr=" + tmp713F03_Lfs).Length;
                                            }
                                            else
                                            {
                                                iCount714 = dtCountElements.Select("ASNFieldID=55 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
                                                iCount715 = dtCountElements.Select("ASNFieldID=77 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
                                                iCount716 = dtCountElements.Select("ASNFieldID=93 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
                                                iCount717 = dtCountElements.Select("ASNFieldID=99 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
                                                iCount718 = dtCountElements.Select("ASNFieldID=108 AND ID713F01=" + iAsnValueTableID713 + " AND LfsNr='" + tmp713F03_Lfs + "'").Length;
                                            }

                                            tmpImpRow["Count714"] = iCount714;
                                            tmpImpRow["Count715"] = iCount715;
                                            tmpImpRow["Count716"] = iCount716;
                                            tmpImpRow["Count717"] = iCount717;
                                            tmpImpRow["Count718"] = iCount718;

                                            dtVDA4913ElemLfs.Rows.Add(tmpImpRow);
                                            iCount714 = 0;
                                            iCount715 = 0;
                                            iCount716 = 0;
                                            iCount717 = 0;
                                            iCount718 = 0;
                                            //tmpImpRow = dtVDA4913ElemLfs.NewRow();
                                        }
                                        break;

                                }
                            }

                            dtTmpValue.DefaultView.RowFilter = "SatzKennung=713";
                            System.Data.DataTable dt713 = dtTmpValue.DefaultView.ToTable();
                            dtTmpValue.DefaultView.RowFilter = string.Empty;

                            for (Int32 x = 0; x <= dtVDA4913ElemLfs.Rows.Count - 1; x++)
                            {
                                Int32 iID713 = 0;
                                Int32 iID713Next = 0;

                                string strID = dtVDA4913ElemLfs.Rows[x]["ID713"].ToString();
                                string strIDNext = string.Empty;
                                //prüfen und Anzahl ermitteln
                                iCount714 = 0;
                                string str714 = dtVDA4913ElemLfs.Rows[x]["Count714"].ToString();
                                Int32.TryParse(str714, out iCount714);

                                iCount715 = 0;
                                string str715 = dtVDA4913ElemLfs.Rows[x]["Count715"].ToString();
                                Int32.TryParse(str715, out iCount715);

                                iCount716 = 0;
                                string str716 = dtVDA4913ElemLfs.Rows[x]["Count716"].ToString();
                                Int32.TryParse(str716, out iCount716);

                                iCount717 = 0;
                                string str717 = dtVDA4913ElemLfs.Rows[x]["Count717"].ToString();
                                Int32.TryParse(str717, out iCount717);

                                iCount718 = 0;
                                string str718 = dtVDA4913ElemLfs.Rows[x]["Count718"].ToString();
                                Int32.TryParse(str718, out iCount718);

                                //den 713er Satz zur Tabelle hinzufügen
                                bool b713 = true;
                                Int32 rCount713 = 0;
                                string strKennung = string.Empty;
                                string oldKennung = string.Empty;
                                while (b713)
                                {
                                    if (dt713.Rows.Count > 0)
                                    {
                                        if (rCount713 <= (dt713.Rows.Count - 1))
                                        {
                                            DataRow r713 = dt713.Rows[rCount713];
                                            strKennung = r713["Kennung"].ToString();

                                            if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ713F01) && (oldKennung != string.Empty))
                                            {
                                                b713 = !(clsASN.const_VDA4913SatzField_SATZ713F01 == strKennung);
                                            }
                                            if (b713)
                                            {
                                                dtTmpASNValue.ImportRow(r713);
                                                dt713.Rows.RemoveAt(rCount713);
                                                //rCount713++;
                                                //rCount714++;
                                            }
                                            oldKennung = strKennung;
                                        }
                                        else
                                        {
                                            b713 = false;
                                        }
                                    }
                                    else
                                    {
                                        b713 = false;
                                    }
                                }

                                //prüfen existiert eine weitere Zeile
                                if (x + 1 <= dtVDA4913ElemLfs.Rows.Count - 1)
                                {
                                    strIDNext = dtVDA4913ElemLfs.Rows[x + 1]["ID713"].ToString();
                                }
                                if (Int32.TryParse(strID, out iID713))
                                {
                                    string strFilter = "ID>=" + iID713.ToString();
                                    if (Int32.TryParse(strIDNext, out iID713Next))
                                    {
                                        if (iID713Next > 0)
                                        {
                                            strFilter = strFilter + " AND ID<" + iID713Next.ToString();
                                        }
                                    }
                                    dtTmpValue.DefaultView.RowFilter = strFilter;
                                    System.Data.DataTable dtTmpLfsValue = dtTmpValue.DefaultView.ToTable();
                                    //dtTmpLfsValue enthält nun die VDAElemente eines LIeferscheins

                                    //Aufteilen der Sätze 714 bis 718
                                    dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=714";
                                    System.Data.DataTable dt714 = dtTmpLfsValue.DefaultView.ToTable();
                                    dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

                                    dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=715";
                                    System.Data.DataTable dt715 = dtTmpLfsValue.DefaultView.ToTable();
                                    dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

                                    dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=716";
                                    System.Data.DataTable dt716 = dtTmpLfsValue.DefaultView.ToTable();
                                    dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

                                    dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=717";
                                    System.Data.DataTable dt717 = dtTmpLfsValue.DefaultView.ToTable();
                                    dtTmpLfsValue.DefaultView.RowFilter = string.Empty;

                                    dtTmpLfsValue.DefaultView.RowFilter = "SatzKennung=718";
                                    System.Data.DataTable dt718 = dtTmpLfsValue.DefaultView.ToTable();
                                    dtTmpLfsValue.DefaultView.RowFilter = string.Empty;
                                    Int32 iCountArt = iCount714;
                                    if (iCount714 < iCount715)
                                    {
                                        iCountArt = iCount715;
                                    }
                                    if (iCount714 < iCount716)
                                    {
                                        iCountArt = iCount716;
                                    }
                                    if (iCount714 < iCount717)
                                    {
                                        //iCountArt = iCount717;   // 28.02.2024 mr test
                                    }
                                    if (iCount714 < iCount718)
                                    {
                                        iCountArt = iCount718;
                                    }

                                    //714 Satz muss für die Verarbeitung je Artikel einmal vorhanden sein
                                    Int32 iCount = 1; //da der 714 Satz schon einmal vorhanden ist
                                    System.Data.DataTable dtTmp714 = dt714.DefaultView.ToTable();
                                    while (iCountArt > iCount)
                                    {
                                        //714
                                        foreach (DataRow row1 in dtTmp714.Rows)
                                        {
                                            dt714.ImportRow(row1);
                                        }
                                        iCount++;
                                    }

                                    //kommt pro Artikel nur einmal vor, muss aber hier für jeden Artikel eingefügt werden
                                    System.Data.DataTable dtTmp716 = dt716.DefaultView.ToTable();
                                    iCount = 1;
                                    while (iCountArt > iCount)
                                    {
                                        //716
                                        foreach (DataRow row2 in dtTmp716.Rows)
                                        {
                                            dt716.ImportRow(row2);
                                        }
                                        iCount++;
                                    }
                                    Int32 iTmp = dt714.Rows.Count;

                                    //Tabellen zu einer zusammensetzen
                                    for (Int32 i = 0; i <= iCountArt - 1; i++)
                                    {
                                        //Satz714
                                        bool b714 = true;
                                        Int32 rCount714 = 0;
                                        strKennung = string.Empty;
                                        oldKennung = string.Empty;
                                        while (b714)
                                        {
                                            if (dt714.Rows.Count > 0)
                                            {
                                                if (rCount714 <= (dt714.Rows.Count - 1))
                                                {
                                                    DataRow r714 = dt714.Rows[rCount714];
                                                    strKennung = r714["Kennung"].ToString();

                                                    if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ714F01) && (oldKennung != string.Empty))
                                                    {
                                                        b714 = !(clsASN.const_VDA4913SatzField_SATZ714F01 == strKennung);
                                                    }
                                                    if (b714)
                                                    {
                                                        dtTmpASNValue.ImportRow(r714);
                                                        dt714.Rows.RemoveAt(rCount714);
                                                        //rCount714++;
                                                    }
                                                    oldKennung = strKennung;
                                                }
                                                else
                                                {
                                                    b714 = false;
                                                }
                                            }
                                            else
                                            {
                                                b714 = false;
                                            }
                                        }
                                        //Satz715
                                        bool b715 = true;
                                        Int32 rCount715 = 0;
                                        strKennung = string.Empty;
                                        oldKennung = string.Empty;
                                        while (b715)
                                        {
                                            if (dt715.Rows.Count > 0)
                                            {
                                                if (rCount715 <= (dt715.Rows.Count - 1))
                                                {
                                                    DataRow r715 = dt715.Rows[rCount715];
                                                    strKennung = r715["Kennung"].ToString();

                                                    if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ715F01) && (oldKennung != string.Empty))
                                                    {
                                                        b715 = !(clsASN.const_VDA4913SatzField_SATZ715F01 == strKennung);
                                                    }
                                                    if (b715)
                                                    {
                                                        dtTmpASNValue.ImportRow(r715);
                                                        dt715.Rows.RemoveAt(rCount715);
                                                        //rCount715++;
                                                    }
                                                    oldKennung = strKennung;
                                                }
                                                else
                                                {
                                                    b715 = false;
                                                }
                                            }
                                            else
                                            {
                                                b715 = false;
                                            }
                                        }
                                        //Satz716
                                        bool b716 = true;
                                        Int32 rCount716 = 0;
                                        strKennung = string.Empty;
                                        oldKennung = string.Empty;
                                        while (b716)
                                        {
                                            if (dt716.Rows.Count > 0)
                                            {
                                                if (rCount716 <= (dt716.Rows.Count - 1))
                                                {
                                                    DataRow r716 = dt716.Rows[rCount716];
                                                    strKennung = r716["Kennung"].ToString();

                                                    if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ716F01) && (oldKennung != string.Empty))
                                                    {
                                                        b716 = !(clsASN.const_VDA4913SatzField_SATZ716F01 == strKennung);
                                                    }
                                                    if (b716)
                                                    {
                                                        dtTmpASNValue.ImportRow(r716);
                                                        dt716.Rows.RemoveAt(rCount716);
                                                        //rCount716++;
                                                    }
                                                    oldKennung = strKennung;
                                                }
                                                else
                                                {
                                                    b716 = false;
                                                }
                                            }
                                            else
                                            {
                                                b716 = false;
                                            }
                                        }
                                        //Satz717
                                        bool b717 = true;
                                        Int32 rCount717 = 0;
                                        strKennung = string.Empty;
                                        oldKennung = string.Empty;
                                        while (b717)
                                        {
                                            if (dt717.Rows.Count > 0)
                                            {
                                                if (rCount717 <= (dt717.Rows.Count - 1))
                                                {
                                                    DataRow r717 = dt717.Rows[rCount717];
                                                    strKennung = r717["Kennung"].ToString();

                                                    if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ717F01) && (oldKennung != string.Empty))
                                                    {
                                                        b717 = !(clsASN.const_VDA4913SatzField_SATZ717F01 == strKennung);
                                                    }
                                                    if (b717)
                                                    {
                                                        dtTmpASNValue.ImportRow(r717);
                                                        dt717.Rows.RemoveAt(rCount717);
                                                        //rCount717++;
                                                    }
                                                    oldKennung = strKennung;
                                                }
                                                else
                                                {
                                                    b717 = false;
                                                }
                                            }
                                            else
                                            {
                                                b717 = false;
                                            }
                                        }


                                        //Satz718
                                        bool b718 = true;
                                        Int32 rCount718 = 0;
                                        strKennung = string.Empty;
                                        oldKennung = string.Empty;
                                        while (b718)
                                        {
                                            if (dt718.Rows.Count > 0)
                                            {
                                                if (rCount718 <= (dt718.Rows.Count - 1))
                                                {
                                                    DataRow r718 = dt718.Rows[rCount717];
                                                    strKennung = r718["Kennung"].ToString();

                                                    if ((oldKennung != clsASN.const_VDA4913SatzField_SATZ718F01) && (oldKennung != string.Empty))
                                                    {
                                                        b718 = !(clsASN.const_VDA4913SatzField_SATZ718F01 == strKennung);
                                                    }
                                                    if (b718)
                                                    {
                                                        dtTmpASNValue.ImportRow(r718);
                                                        dt718.Rows.RemoveAt(rCount718);
                                                        //rCount717++;
                                                    }
                                                    oldKennung = strKennung;
                                                }
                                                else
                                                {
                                                    b718 = false;
                                                }
                                            }
                                            else
                                            {
                                                b718 = false;
                                            }
                                        }
                                        iCount++;
                                    }
                                }
                            }
                            ListTmpASNValue.Add(dtTmpASNValue);
                        }// Ende List

                        dtAsnValueSource.DefaultView.RowFilter = "SatzKennung=711";
                        System.Data.DataTable dt711 = dtAsnValueSource.DefaultView.ToTable();

                        //-- 711 hinzufügen
                        dtReCreatedASNValue = dt711.Copy();


                        // --- Datatable dtReCreatedASNValue füllen mit den Datensätzen 
                        // --- aus der ListTmpASNVAlue
                        // --- 712 bis 718 je Transport
                        foreach (System.Data.DataTable dtTmp in ListTmpASNValue)
                        {
                            foreach (DataRow row in dtTmp.Rows)
                            {
                                dtReCreatedASNValue.ImportRow(row);
                            }
                        }

                        dtAsnValueSource.DefaultView.RowFilter = "SatzKennung=719";
                        System.Data.DataTable dt719 = dtAsnValueSource.DefaultView.ToTable();

                        //Satz 719 hinzufügen
                        foreach (DataRow row in dt719.Rows)
                        {
                            dtReCreatedASNValue.ImportRow(row);
                        }

                        //Spalte LfdNr -> Zähler
                        if (!dtOrg.Columns.Contains("LfdNr"))
                        {
                            dtOrg.Columns.Add("LfdNr", typeof(Int32));
                        }
                        if (!dtReCreatedASNValue.Columns.Contains("LfdNr"))
                        {
                            dtReCreatedASNValue.Columns.Add("LfdNr", typeof(Int32));
                        }

                        //Rows aus der TMPTable dtASNIDValue
                        Int32 iLfdNr = 1;
                        foreach (DataRow rowImp in dtReCreatedASNValue.Rows)
                        {
                            rowImp["LfdNr"] = iLfdNr;
                            dtOrg.ImportRow(rowImp);
                            iLfdNr++;
                        }
                        dtTmpASNValue.Rows.Clear();

                    }
                    catch (Exception ex)
                    {
                        string st = ex.ToString();
                    }
                }
            }

            dtOrg.Columns.Add("ASNSender", typeof(decimal));
            dtOrg.Columns.Add("ASNReceiver", typeof(decimal));
            return dtOrg;
        }
    }
}
