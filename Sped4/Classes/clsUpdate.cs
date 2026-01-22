using LVS;
using Sped4.Classes.Update;
using System;
using System.Data.SqlClient;
using System.Threading;

namespace Sped4.Classes
{
    class clsUpdate
    {
        ///<summary>
        ///             In dieser Klasse werden die Updates für das Programm Sped4 verwaltet.Beim Programmstart überprüft Sped4 
        ///             die aktuelle Version mit der Datenbankversion. Sind die Versionen unterschiedlich werden die entsprechenden
        ///             Update durchgeführt
        ///</summary>
        ///<remarks>C:\develop\comTEC\SpedLVS\Sped4\Classes\clsUpdate.cs
        ///             Die Versionnummer von Sped4 setzt sich folgendermaßen zusammen:
        ///             Die Versionsnummer von Sped4 ist 4-stellig (Bsp: 1.234)
        ///                 1. Stelle       : besondere / grundlegenede Erweiterungen
        ///                 2. Stelle       : Datenbankänderungen 
        ///                 3. + 4. Stelle  : kleinere Änderungen im Programm
        ///               
        ///             Das Array "UpdateArray" beinhaltet alle Versionen. Diese Array wird beim Update-Vorgang durchlaufen und mit 
        ///             der Datenbankversion verglichen und entsprechend das Update durchzuführen.
        ///</remarks>
        //************  User  ***************
        private decimal _BenutzerID;

        public decimal BenutzerID
        {
            get
            {
                return _BenutzerID;
            }
            set
            {
                _BenutzerID = value;
            }
        }

        internal clsSystem system;
        internal frmUpdateMirror upMirr;
        private decimal SoftwareVersion = 1001M;
        //public int[] UpdateArray =
        //{
        //    1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1010,
        //    1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019, 1020,
        //    1021, 1022, 1023, 1024, 1025, 1027, 1028, 1029, 1030,
        //    1031, 1032, 1033, 1034, 1035, 1036, 1037, 1038, 1039, 1040,
        //    1041, 1042, 1043, 1044, 1045, 1047, 1048, 1049, 1050,
        //    1051, 1052, 1053, 1054, 1055, 1056, 1057, 1058, 1059, 1060,
        //    1061, 1062, 1063, 1064, 1065, 1066, 1067, 1068, 1069, 1070,
        //    1071, 1072, 1073, 1074, 1075, 1076, 1077, 1078, 1079, 1080,
        //    1081, 1082, 1083, 1084, 1085, 1086, 1087, 1088, 1089, 1090,
        //    1091, 1092, 1093, 1094, 1095, 1096, 1097, 1098, 1099, 1100,
        //    1101, 1102, 1103, 1104, 1105, 1106, 1107, 1108, 1109, 1110,
        //    1111, 1112, 1113, 1114, 1115, 1116, 1117, 1118, 1119, 1120,
        //    1121, 1122, 1123, 1124, 1125, 1126, 1127, 1128, 1129, 1130,
        //    1131, 1132, 1133, 1134, 1135, 1136, 1137, 1138, 1139, 1140,
        //    1141, 1142, 1143, 1144, 1145, 1146, 1147, 1148, 1149, 1150,
        //    1151, 1152, 1153, 1154, 1155, 1156, 1157, 1158, 1159, 1160,
        //    1161, 1162, 1163, 1164, 1165, 1166, 1167, 1168, 1169, 1170,
        //    1171, 1172, 1173, 1174, 1175, 1176, 1177, 1178, 1179, 1180,
        //    1181, 1182, 1183, 1184, 1185, 1186, 1187, 1188, 1189, 1190,
        //    1191, 1192, 1193, 1194, 1195, 1196, 1197, 1198, 1199, 1200,
        //    1201, 1202, 1203, 1204, 1205, 1206, 1207, 1208 ,1209, 1210,
        //    1211, 1212, 1213, 1214, 1215, 1216, 1217, 1218, 1219, 1220,
        //    1221, 1222, 1223, 1224, 1225, 1226, 1227, 1228, 1229, 1230,
        //    1231, 1232, 1233, 1234, 1235, 1236, 1237, 1238, 1239, 1240,
        //    1241, 1242, 1243, 1244, 1245, 1246, 1247, 1248, 1249, 1250,
        //    1251, 1252, 1253, 1254, 1255, 1256, 1257, 1258, 1259, 1260,
        //    1261, 1262, 1263, 1264, 1265, 1266, 1267, 1268, 1269, 1270,
        //    1271, 1272, 1273, 1274, 1275, 1276, 1277, 1278, 1279, 1280,
        //    1281, 1282, 1283, 1284, 1285, 1286, 1287, 1288, 1289, 1290,
        //    1291, 1292, 1293, 1294, 1295, 1296, 1297, 1298, 1299, 1300,
        //    1301, 1302, 1303, 1304, 1305, 1306, 1307, 1308

        //};

        /*********************************************************************************************************/
        public decimal dbVersion { get; set; }

        public bool UpdateOK { get; set; }

        ///<summary>DoUpdate(string tmpSQL) / clsUpdate</summary>
        ///<remarks>Führt die SQL - Update-Anweisung aus.</remarks>
        ///<param name="tmpSQL">SQL-Anweisung</param>
        ///<returns>Rückgabe von TRUE / FALSE nach Durchführung der SQL-Anweisung</returns>
        private bool DoUpdate(string tmpSQL)
        {
            bool updateOK = true;

            SqlCommand UpCommand = new SqlCommand();
            Globals.SQLcon.Open();

            //Start Transaction
            SqlTransaction tAction;
            tAction = Globals.SQLcon.Connection.BeginTransaction("Update");

            UpCommand.Connection = Globals.SQLcon.Connection;
            UpCommand.Transaction = tAction;
            try
            {
                UpCommand.CommandText = tmpSQL;
                //Globals.SQLcon.Open();
                UpCommand.ExecuteNonQuery();

                tAction.Commit();

                UpCommand.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                tAction.Rollback();

                //Add Logbucheintrag Exception
                string beschreibung = "Exception: " + ex.ToString();
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), beschreibung);
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
                updateOK = false;
            }
            tAction.Dispose();
            return updateOK;
        }

        //
        ///<summary>AddToLog(string strVersionsupdate) / clsUpdate</summary>
        ///<remarks>Die Update-Aktion wird im Logbuch dokumentiert.</remarks>
        ///<param name="strVersionsupdate">neue Versionsnummer nach dem Update</param>
        private void AddToLog(string strVersionsupdate)
        {
            string Beschreibung = "Software Update durchgeführt auf " + strVersionsupdate;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
        }

        //
        ///<summaryGetSQLforUpdateVersion(string strVersion) / clsUpdate</summary>
        ///<remarks>Liefert die SQL-Anweisung zum Eintrag der neuen Versionsnummer in die Datenbank.</remarks>
        ///<param name="strVersion">neue Versionsnummer nach dem Update</param>
        private string GetSQLforUpdateVersion(string strVersion)
        {
            string sql = " Update Version SET " +
                          "Versionsnummer='" + strVersion + "' ";
            //int iTmp = 0;
            int.TryParse(strVersion, out int iTmp);
            if (iTmp > 1277)
            {
                sql += ", LastUpdate = '" + DateTime.Now + "'";

            }
            sql += "WHERE ID=1 ;";
            return sql;
        }

        //
        ///<summary>InitUpdate() / clsUpdate</summary>
        ///<remarks>Start der Updatefunktion.</remarks>
        public void InitUpdate()
        {
            BenutzerID = upMirr.GL_User.User_ID;
            UpdateOK = false;
            upMirr.strFortschritt = Environment.NewLine;
            upMirr.strFortschritt += upMirr.strFortschritt.ToString().Trim() + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": START Update DB LVS" + Environment.NewLine;
            upMirr.SetInfoFortschritt();

            //DB-Version und SoftwareVersion werden verglichen
            system = new clsSystem();
            system.BenutzerID = this.BenutzerID;
            int[] UpdateArray = AppVersion.UpdateVersions();
            SoftwareVersion = Functions.GetMaxArray(UpdateArray);
            string strDBVersion = system.SystemVersionApp.ToString();
            if ((system.SystemVersionAppDecimal < SoftwareVersion) |
                (system.SystemVersionAppDecimal == 0))
            {
                //Update Array wird durchlaufen und die DB-Version mit der 
                //entsprechenden UpdateArray-Version verglichen 
                for (Int32 i = 0; i <= UpdateArray.Length - 1; i++)
                {
                    bool boUpdateOK = false;
                    decimal decTmp = 0.0M;
                    decTmp = Convert.ToDecimal(UpdateArray[i].ToString());

                    //Vergleich
                    if (system.SystemVersionAppDecimal < decTmp)
                    {
                        upMirr.strFortschritt = Environment.NewLine +
                                                upMirr.strFortschritt.ToString().Trim() +
                                                Environment.NewLine +
                                                DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") +
                                                ": Installiere Update Version -> " +
                                                Functions.FormatDecimalVersion(decTmp) +
                                                Environment.NewLine;
                        upMirr.SetInfoFortschritt();

                        switch (decTmp.ToString())
                        {
                            // 2023_03_30
                            // Update hier einfügen ab 1312
                            case up1339.const_up1339:
                                boUpdateOK = DoUpdate(up1339.SqlString());
                                boUpdateOK = DoUpdate(up1339.SqlStringUpdate_UpdateExistingColumns());
                                break;

                            case up1338.const_up1338:
                                boUpdateOK = DoUpdate(up1338.SqlString());
                                boUpdateOK = DoUpdate(up1338.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1337.const_up1337:
                                boUpdateOK = DoUpdate(up1337.SqlString());
                                boUpdateOK = DoUpdate(up1337.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1336.const_up1336:
                                boUpdateOK = DoUpdate(up1336.SqlString());
                                //boUpdateOK = DoUpdate(up1334.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1335.const_up1335:
                                boUpdateOK = DoUpdate(up1335.SqlString());
                                //boUpdateOK = DoUpdate(up1334.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1334.const_up1334:
                                boUpdateOK = DoUpdate(up1334.SqlString());
                                boUpdateOK = DoUpdate(up1334.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1333.const_up1333:
                                boUpdateOK = DoUpdate(up1333.SqlString());
                                //boUpdateOK = DoUpdate(up1332.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1332.const_up1332:
                                boUpdateOK = DoUpdate(up1332.SqlString());
                                boUpdateOK = DoUpdate(up1332.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1331.const_up1331:
                                boUpdateOK = DoUpdate(up1331.SqlString());
                                boUpdateOK = DoUpdate(up1331.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1330.const_up1330:
                                boUpdateOK = DoUpdate(up1330.SqlString());
                                boUpdateOK = DoUpdate(up1330.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1329.const_up1329:
                                boUpdateOK = DoUpdate(up1329.SqlString());
                                boUpdateOK = DoUpdate(up1329.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1328.const_up1328:
                                boUpdateOK = DoUpdate(up1328.SqlString());
                                boUpdateOK = DoUpdate(up1328.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1327.const_up1327:
                                boUpdateOK = DoUpdate(up1327.SqlString());
                                boUpdateOK = DoUpdate(up1327.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1326.const_up1326:
                                boUpdateOK = DoUpdate(up1326.SqlString());
                                boUpdateOK = DoUpdate(up1326.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1325.const_up1325:
                                boUpdateOK = DoUpdate(up1325.SqlString());
                                //boUpdateOK = DoUpdate(up1323.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1324.const_up1324:
                                boUpdateOK = DoUpdate(up1324.SqlString());
                                //boUpdateOK = DoUpdate(up1323.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1323.const_up1323:
                                boUpdateOK = DoUpdate(up1323.SqlString());
                                boUpdateOK = DoUpdate(up1323.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1322.const_up1322:
                                boUpdateOK = DoUpdate(up1322.SqlString());
                                boUpdateOK = DoUpdate(up1322.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1321.const_up1321:
                                boUpdateOK = DoUpdate(up1321.SqlString());
                                boUpdateOK = DoUpdate(up1321.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            //heraus genommen, wird nicht benötigt
                            //case up1320.const_up1320:
                            //    boUpdateOK = DoUpdate(up1320.SqlString());
                            //    //boUpdateOK = DoUpdate(up1308.SqlStringUpdate_UpdateExistingColumns());
                            //    break;

                            case up1319.const_up1319:
                                boUpdateOK = DoUpdate(up1319.SqlString());
                                //boUpdateOK = DoUpdate(up1308.SqlStringUpdate_UpdateExistingColumns());
                                break;

                            case up1315.const_up1315:
                                boUpdateOK = DoUpdate(up1315.SqlString());
                                //boUpdateOK = DoUpdate(up1308.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1314.const_up1314:
                                boUpdateOK = DoUpdate(up1314.SqlString());
                                //boUpdateOK = DoUpdate(up1308.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1312.const_up1312:
                                boUpdateOK = DoUpdate(up1312.SqlString());
                                //boUpdateOK = DoUpdate(up1308.SqlStringUpdate_UpdateExistingColumns());
                                break;



                            //Durchführung der Updates
                            case "1001":
                                boUpdateOK = DoUpdate(Update1001());
                                break;
                            case "1002":
                                boUpdateOK = DoUpdate(Update1002());
                                break;
                            case "1003":
                                boUpdateOK = DoUpdate(Update1003());
                                break;
                            case "1004":
                                boUpdateOK = DoUpdate(Update1004());
                                break;
                            case "1005":
                                boUpdateOK = DoUpdate(Update1005());
                                break;
                            case "1006":
                                boUpdateOK = DoUpdate(Update1006());
                                break;
                            case "1007":
                                boUpdateOK = DoUpdate(Update1007());
                                break;
                            case "1008":
                                boUpdateOK = DoUpdate(Update1008());
                                break;
                            case "1010":
                                boUpdateOK = DoUpdate(Update1010());
                                break;
                            case "1011":
                                boUpdateOK = DoUpdate(Update1011());
                                break;
                            case "1012":
                                boUpdateOK = DoUpdate(Update1012());
                                break;
                            case "1013":
                                boUpdateOK = DoUpdate(Update1013());
                                break;
                            case "1014":
                                boUpdateOK = DoUpdate(Update1014());
                                break;
                            case "1015":
                                boUpdateOK = DoUpdate(Update1015());
                                break;
                            case "1016":
                                boUpdateOK = DoUpdate(Update1016());
                                break;
                            case "1017":
                                boUpdateOK = DoUpdate(Update1017());
                                break;
                            case "1018":
                                boUpdateOK = DoUpdate(Update1018());
                                break;
                            case "1019":
                                boUpdateOK = DoUpdate(Update1019());
                                break;
                            case "1020":
                                boUpdateOK = DoUpdate(Update1020());
                                break;
                            case "1021":
                                boUpdateOK = DoUpdate(Update1021());
                                break;
                            case "1022":
                                boUpdateOK = DoUpdate(Update1022());
                                break;
                            case "1023":
                                boUpdateOK = DoUpdate(Update1023());
                                break;
                            case "1024":
                                boUpdateOK = DoUpdate(Update1024());
                                break;
                            case "1025":
                                boUpdateOK = DoUpdate(Update1025());
                                break;
                            /***
                            case "1026":
                            boUpdateOK = DoUpdate(Update1026());
                            break;
                            * ***/
                            case "1027":
                                boUpdateOK = DoUpdate(Update1027());
                                break;
                            case "1028":
                                boUpdateOK = DoUpdate(Update1028());
                                break;
                            case "1029":
                                boUpdateOK = DoUpdate(Update1029());
                                break;
                            case "1030":
                                boUpdateOK = DoUpdate(Update1030());
                                break;
                            case "1031":
                                boUpdateOK = DoUpdate(Update1031());
                                break;
                            case "1032":
                                boUpdateOK = DoUpdate(Update1032());
                                break;
                            case "1033":
                                boUpdateOK = DoUpdate(Update1033());
                                break;
                            case "1034":
                                boUpdateOK = DoUpdate(Update1034());
                                break;
                            case "1035":
                                boUpdateOK = DoUpdate(Update1035());
                                break;
                            case "1036":
                                boUpdateOK = DoUpdate(Update1036());
                                break;
                            case "1037":
                                boUpdateOK = DoUpdate(Update1037());
                                break;
                            case "1038":
                                boUpdateOK = DoUpdate(Update1038());
                                break;
                            case "1039":
                                boUpdateOK = DoUpdate(Update1039());
                                break;
                            case "1040":
                                boUpdateOK = DoUpdate(Update1040());
                                break;
                            case "1041":
                                boUpdateOK = DoUpdate(Update1041());
                                break;
                            case "1042":
                                boUpdateOK = DoUpdate(Update1042());
                                break;
                            case "1043":
                                boUpdateOK = DoUpdate(Update1043());
                                break;
                            case "1044":
                                boUpdateOK = DoUpdate(Update1044());
                                break;
                            case "1045":
                                boUpdateOK = DoUpdate(Update1045());
                                break;
                            case "1046":
                                //boUpdateOK = DoUpdate(Update1046());
                                break;
                            case "1047":
                                boUpdateOK = DoUpdate(Update1047());
                                break;
                            case "1048":
                                boUpdateOK = DoUpdate(Update1048());
                                break;
                            case "1049":
                                boUpdateOK = DoUpdate(Update1049());
                                break;
                            case "1050":
                                boUpdateOK = DoUpdate(Update1050());
                                break;
                            case "1051":
                                boUpdateOK = DoUpdate(Update1051());
                                break;
                            case "1052":
                                boUpdateOK = DoUpdate(Update1052());
                                break;
                            case "1053":
                                boUpdateOK = DoUpdate(Update1053());
                                break;
                            case "1054":
                                boUpdateOK = DoUpdate(Update1054());
                                break;
                            case "1055":
                                boUpdateOK = DoUpdate(Update1055());
                                break;
                            case "1056":
                                boUpdateOK = DoUpdate(Update1056());
                                break;
                            case "1057":
                                boUpdateOK = DoUpdate(Update1057());
                                break;
                            case "1058":
                                boUpdateOK = DoUpdate(Update1058());
                                break;
                            case "1059":
                                boUpdateOK = DoUpdate(Update1059());
                                break;
                            case "1060":
                                boUpdateOK = DoUpdate(Update1060());
                                break;
                            case "1061":
                                boUpdateOK = DoUpdate(Update1061());
                                break;
                            case "1062":
                                boUpdateOK = DoUpdate(Update1062());
                                break;
                            case "1063":
                                boUpdateOK = DoUpdate(Update1063());
                                break;
                            case "1064":
                                boUpdateOK = DoUpdate(Update1064());
                                break;
                            case "1065":
                                boUpdateOK = DoUpdate(Update1065());
                                break;
                            case "1066":
                                boUpdateOK = DoUpdate(Update1066());
                                break;
                            case "1067":
                                boUpdateOK = DoUpdate(Update1067());
                                break;
                            case "1068":
                                boUpdateOK = DoUpdate(Update1068());
                                break;
                            case "1069":
                                boUpdateOK = DoUpdate(Update1069());
                                break;
                            case "1070":
                                boUpdateOK = DoUpdate(Update1070());
                                break;
                            case "1071":
                                boUpdateOK = DoUpdate(Update1071());
                                break;
                            case "1072":
                                boUpdateOK = DoUpdate(Update1072());
                                break;
                            case "1073":
                                boUpdateOK = DoUpdate(Update1073());
                                break;
                            case "1074":
                                boUpdateOK = DoUpdate(Update1074());
                                break;
                            case "1075":
                                boUpdateOK = DoUpdate(Update1075());
                                break;
                            case "1076":
                                boUpdateOK = DoUpdate(Update1076());
                                break;
                            case "1077":
                                boUpdateOK = DoUpdate(Update1077());
                                break;
                            case "1078":
                                boUpdateOK = DoUpdate(Update1078());
                                break;
                            case "1079":
                                boUpdateOK = DoUpdate(Update1079());
                                break;
                            case "1080":
                                boUpdateOK = DoUpdate(Update1080());
                                break;
                            case "1081":
                                boUpdateOK = DoUpdate(Update1081());
                                break;
                            case "1082":
                                boUpdateOK = DoUpdate(Update1082());
                                break;
                            case "1083":
                                boUpdateOK = DoUpdate(Update1083());
                                break;
                            case "1084":
                                boUpdateOK = DoUpdate(Update1084());
                                break;
                            case "1085":
                                boUpdateOK = DoUpdate(Update1085());
                                break;
                            case "1086":
                                boUpdateOK = DoUpdate(Update1086());
                                break;
                            case "1087":
                                boUpdateOK = DoUpdate(Update1087());
                                break;
                            case "1088":
                                boUpdateOK = DoUpdate(Update1088());
                                break;
                            case "1089":
                                boUpdateOK = DoUpdate(Update1089());
                                break;
                            case "1090":
                                boUpdateOK = DoUpdate(Update1090());
                                break;
                            case "1091":
                                boUpdateOK = DoUpdate(Update1091());
                                break;
                            case "1092":
                                boUpdateOK = DoUpdate(Update1092());
                                break;
                            case "1093":
                                boUpdateOK = DoUpdate(Update1093());
                                break;
                            case "1094":
                                boUpdateOK = DoUpdate(Update1094());
                                break;
                            case "1095":
                                boUpdateOK = DoUpdate(Update1095());
                                break;
                            case "1096":
                                boUpdateOK = DoUpdate(Update1096());
                                break;
                            case "1097":
                                boUpdateOK = DoUpdate(Update1097());
                                break;
                            case "1098":
                                boUpdateOK = DoUpdate(Update1098());
                                break;
                            case "1099":
                                boUpdateOK = DoUpdate(Update1099());
                                break;
                            case "1100":
                                boUpdateOK = DoUpdate(Update1100());
                                break;
                            case "1101":
                                boUpdateOK = DoUpdate(Update1101());
                                break;
                            case "1102":
                                boUpdateOK = DoUpdate(Update1102());
                                break;
                            case "1103":
                                boUpdateOK = DoUpdate(Update1103());
                                break;
                            case "1104":
                                boUpdateOK = DoUpdate(Update1104());
                                break;
                            case "1105":
                                boUpdateOK = DoUpdate(Update1105());
                                break;
                            case "1106":
                                boUpdateOK = DoUpdate(Update1106());
                                break;
                            case "1107":
                                boUpdateOK = DoUpdate(Update1107());
                                break;
                            case "1108":
                                boUpdateOK = DoUpdate(Update1108());
                                break;
                            case "1109":
                                boUpdateOK = DoUpdate(Update1109());
                                break;
                            case "1110":
                                boUpdateOK = DoUpdate(Update1110());
                                break;
                            case "1111":
                                boUpdateOK = DoUpdate(Update1111());
                                break;
                            case "1112":
                                boUpdateOK = DoUpdate(Update1112());
                                break;
                            case "1113":
                                boUpdateOK = DoUpdate(Update1113());
                                break;
                            case "1114":
                                boUpdateOK = DoUpdate(Update1114());
                                break;
                            case "1115":
                                boUpdateOK = DoUpdate(Update1115());
                                break;
                            case "1116":
                                boUpdateOK = DoUpdate(Update1116());
                                break;
                            case "1117":
                                boUpdateOK = DoUpdate(Update1117());
                                break;
                            case "1118":
                                boUpdateOK = DoUpdate(Update1118());
                                break;
                            case "1119":
                                boUpdateOK = DoUpdate(Update1119());
                                break;
                            case "1120":
                                boUpdateOK = DoUpdate(Update1120());
                                break;
                            case "1121":
                                boUpdateOK = DoUpdate(Update1121());
                                break;
                            case "1122":
                                boUpdateOK = DoUpdate(Update1122());
                                break;
                            case "1123":
                                boUpdateOK = DoUpdate(Update1123());
                                break;
                            case "1124":
                                boUpdateOK = DoUpdate(Update1124());
                                break;
                            case "1125":
                                boUpdateOK = DoUpdate(Update1125());
                                break;
                            case "1126":
                                boUpdateOK = DoUpdate(Update1126());
                                break;
                            case "1127":
                                boUpdateOK = DoUpdate(Update1127());
                                break;
                            case "1128":
                                boUpdateOK = DoUpdate(Update1128());
                                break;
                            case "1129":
                                boUpdateOK = DoUpdate(Update1129());
                                break;
                            case "1130":
                                boUpdateOK = DoUpdate(Update1130());
                                break;
                            case "1131":
                                boUpdateOK = DoUpdate(Update1131());
                                break;
                            case "1132":
                                boUpdateOK = DoUpdate(Update1132());
                                break;
                            case "1133":
                                boUpdateOK = DoUpdate(Update1133());
                                break;
                            case "1134":
                                boUpdateOK = DoUpdate(Update1134());
                                break;
                            case "1135":
                                boUpdateOK = DoUpdate(Update1135());
                                break;
                            case "1136":
                                boUpdateOK = DoUpdate(Update1136());
                                break;
                            case "1137":
                                boUpdateOK = DoUpdate(Update1137());
                                break;
                            case "1138":
                                boUpdateOK = DoUpdate(Update1138());
                                break;
                            case "1139":
                                boUpdateOK = DoUpdate(Update1139());
                                break;
                            case "1140":
                                boUpdateOK = DoUpdate(Update1140());
                                break;
                            case "1141":
                                boUpdateOK = DoUpdate(Update1141());
                                break;
                            case "1142":
                                boUpdateOK = DoUpdate(Update1142());
                                break;
                            case "1143":
                                boUpdateOK = DoUpdate(Update1143());
                                break;
                            case "1144":
                                boUpdateOK = DoUpdate(Update1144());
                                break;
                            case "1145":
                                boUpdateOK = DoUpdate(Update1145());
                                break;
                            case "1146":
                                boUpdateOK = DoUpdate(Update1146());
                                break;
                            case "1147":
                                boUpdateOK = DoUpdate(Update1147());
                                break;
                            case "1148":
                                boUpdateOK = DoUpdate(Update1148());
                                break;
                            case "1149":
                                boUpdateOK = DoUpdate(Update1149());
                                break;
                            case "1150":
                                boUpdateOK = DoUpdate(Update1150());
                                break;
                            case "1151":
                                boUpdateOK = DoUpdate(Update1151());
                                break;
                            case "1152":
                                boUpdateOK = DoUpdate(Update1152());
                                break;
                            case "1153":
                                boUpdateOK = DoUpdate(Update1153());
                                break;
                            case "1154":
                                boUpdateOK = DoUpdate(Update1154());
                                break;
                            case "1155":
                                boUpdateOK = DoUpdate(Update1155());
                                break;
                            case "1156":
                                boUpdateOK = DoUpdate(Update1156());
                                break;
                            case "1157":
                                boUpdateOK = DoUpdate(Update1157());
                                break;
                            case "1158":
                                boUpdateOK = DoUpdate(Update1158());
                                break;
                            case "1159":
                                boUpdateOK = DoUpdate(Update1159());
                                break;
                            case "1160":
                                boUpdateOK = DoUpdate(Update1160());
                                break;
                            case "1161":
                                boUpdateOK = DoUpdate(Update1161());
                                break;
                            case "1162":
                                boUpdateOK = DoUpdate(Update1162());
                                break;
                            case "1163":
                                //SQL doppelt
                                //boUpdateOK = DoUpdate(Update1163());
                                boUpdateOK = true;
                                break;
                            case "1164":
                                boUpdateOK = DoUpdate(Update1164());
                                break;
                            case "1165":
                                boUpdateOK = DoUpdate(Update1165());
                                break;
                            case "1166":
                                boUpdateOK = DoUpdate(Update1166());
                                break;
                            case "1167":
                                boUpdateOK = DoUpdate(Update1167());
                                break;
                            case "1168":
                                boUpdateOK = DoUpdate(Update1168());
                                break;
                            case "1169":
                                boUpdateOK = DoUpdate(Update1169());
                                Thread.Sleep(500);
                                break;
                            case "1170":
                                boUpdateOK = DoUpdate(Update1170());
                                Thread.Sleep(500);
                                break;
                            case "1171":
                                boUpdateOK = DoUpdate(Update1171());
                                Thread.Sleep(500);
                                break;
                            case "1172":
                                boUpdateOK = DoUpdate(Update1172());
                                Thread.Sleep(500);
                                break;
                            case "1173":
                                boUpdateOK = DoUpdate(Update1173());
                                Thread.Sleep(500);
                                break;
                            case "1174":
                                boUpdateOK = DoUpdate(Update1174());
                                Thread.Sleep(500);
                                break;
                            case "1175":
                                boUpdateOK = DoUpdate(Update1175());
                                Thread.Sleep(500);
                                break;
                            case "1176":
                                boUpdateOK = DoUpdate(Update1176());
                                Thread.Sleep(500);
                                break;
                            case "1177":
                                boUpdateOK = DoUpdate(Update1177());
                                Thread.Sleep(500);
                                break;
                            case "1178":
                                boUpdateOK = DoUpdate(Update1178());
                                Thread.Sleep(500);
                                break;
                            case "1179":
                                boUpdateOK = DoUpdate(Update1179());
                                Thread.Sleep(500);
                                break;
                            case "1180":
                                boUpdateOK = DoUpdate(Update1180());
                                Thread.Sleep(500);
                                break;
                            case "1181":
                                boUpdateOK = DoUpdate(Update1181());
                                Thread.Sleep(500);
                                break;
                            case "1182":
                                boUpdateOK = DoUpdate(Update1182());
                                Thread.Sleep(500);
                                break;
                            case "1183":
                                boUpdateOK = DoUpdate(Update1183());
                                Thread.Sleep(500);
                                break;
                            case "1184":
                                boUpdateOK = DoUpdate(Update1184());
                                Thread.Sleep(500);
                                break;
                            case "1185":
                                boUpdateOK = DoUpdate(Update1185());
                                Thread.Sleep(500);
                                break;
                            case "1186":
                                boUpdateOK = DoUpdate(Update1186());
                                Thread.Sleep(500);
                                break;
                            case "1187":
                                boUpdateOK = DoUpdate(Update1187());
                                Thread.Sleep(500);
                                break;
                            case "1188":
                                boUpdateOK = DoUpdate(Update1188());
                                Thread.Sleep(500);
                                break;
                            case "1189":
                                boUpdateOK = DoUpdate(Update1189());
                                Thread.Sleep(500);
                                break;
                            case "1190":
                                boUpdateOK = DoUpdate(Update1190());
                                Thread.Sleep(500);
                                break;
                            case "1191":
                                boUpdateOK = DoUpdate(Update1191());
                                Thread.Sleep(500);
                                break;
                            case "1192":
                                boUpdateOK = DoUpdate(Update1192());
                                Thread.Sleep(500);
                                break;
                            case "1193":
                                boUpdateOK = DoUpdate(Update1193());
                                Thread.Sleep(500);
                                break;
                            case "1194":
                                boUpdateOK = DoUpdate(Update1194());
                                Thread.Sleep(500);
                                break;
                            case "1195":
                                boUpdateOK = DoUpdate(Update1195());
                                Thread.Sleep(500);
                                break;
                            case "1196":
                                boUpdateOK = DoUpdate(Update1196());
                                Thread.Sleep(500);
                                break;
                            case "1197":
                                boUpdateOK = DoUpdate(Update1197());
                                Thread.Sleep(500);
                                break;
                            case "1198":
                                boUpdateOK = DoUpdate(Update1198());
                                Thread.Sleep(500);
                                break;
                            case "1199":
                                boUpdateOK = DoUpdate(Update1199());
                                Thread.Sleep(500);
                                break;
                            case "1200":
                                boUpdateOK = DoUpdate(Update1200());
                                Thread.Sleep(500);
                                break;
                            case "1201":
                                boUpdateOK = DoUpdate(Update1201());
                                Thread.Sleep(500);
                                break;
                            case "1202":
                                boUpdateOK = DoUpdate(Update1202());
                                Thread.Sleep(500);
                                break;
                            case "1203":
                                boUpdateOK = DoUpdate(Update1203());
                                Thread.Sleep(500);
                                break;
                            case "1204":
                                boUpdateOK = DoUpdate(Update1204());
                                Thread.Sleep(500);
                                break;
                            case "1205":
                                boUpdateOK = DoUpdate(Update1205());
                                Thread.Sleep(500);
                                break;
                            case "1206":
                                boUpdateOK = DoUpdate(Update1206());
                                Thread.Sleep(500);
                                break;
                            case "1207":
                                boUpdateOK = DoUpdate(Update1207());
                                Thread.Sleep(500);
                                break;
                            case "1208":
                                boUpdateOK = DoUpdate(Update1208());
                                Thread.Sleep(500);
                                break;
                            case "1209":
                                boUpdateOK = DoUpdate(Update1209());
                                Thread.Sleep(500);
                                break;
                            case "1210":
                                boUpdateOK = DoUpdate(Update1210());
                                Thread.Sleep(500);
                                break;
                            case "1211":
                                boUpdateOK = DoUpdate(Update1211());
                                Thread.Sleep(500);
                                break;
                            case "1212":
                                boUpdateOK = DoUpdate(Update1212());
                                Thread.Sleep(500);
                                break;
                            case "1213":
                                boUpdateOK = DoUpdate(Update1213());
                                Thread.Sleep(500);
                                break;
                            case "1214":
                                boUpdateOK = DoUpdate(Update1214());
                                Thread.Sleep(500);
                                break;
                            case "1215":
                                boUpdateOK = DoUpdate(Update1215());
                                Thread.Sleep(500);
                                break;
                            case "1216":
                                boUpdateOK = DoUpdate(Update1216());
                                Thread.Sleep(500);
                                break;
                            case "1217":
                                boUpdateOK = DoUpdate(Update1217());
                                Thread.Sleep(500);
                                break;
                            case "1218":
                                boUpdateOK = DoUpdate(Update1218());
                                Thread.Sleep(500);
                                break;
                            case "1219":
                                boUpdateOK = DoUpdate(Update1219());
                                Thread.Sleep(500);
                                break;
                            case "1220":
                                boUpdateOK = DoUpdate(Update1220());
                                Thread.Sleep(500);
                                break;
                            case "1221":
                                boUpdateOK = DoUpdate(Update1221());
                                Thread.Sleep(500);
                                break;
                            case "1222":
                                boUpdateOK = DoUpdate(Update1222());
                                Thread.Sleep(500);
                                break;
                            case "1223":
                                boUpdateOK = DoUpdate(Update1223());
                                Thread.Sleep(500);
                                break;
                            case "1224":
                                boUpdateOK = DoUpdate(Update1224());
                                Thread.Sleep(500);
                                break;
                            case "1225":
                                boUpdateOK = DoUpdate(Update1225());
                                Thread.Sleep(500);
                                break;
                            case "1226":
                                boUpdateOK = DoUpdate(Update1226());
                                Thread.Sleep(500);
                                break;
                            case "1227":
                                boUpdateOK = DoUpdate(Update1227());
                                Thread.Sleep(500);
                                break;
                            case "1228":
                                boUpdateOK = DoUpdate(Update1228());
                                Thread.Sleep(500);
                                break;
                            case "1229":
                                boUpdateOK = DoUpdate(Update1229());
                                Thread.Sleep(500);
                                break;
                            case "1230":
                                boUpdateOK = DoUpdate(Update1230());
                                Thread.Sleep(500);
                                break;
                            case "1231":
                                boUpdateOK = DoUpdate(Update1231());
                                Thread.Sleep(500);
                                break;
                            case "1232":
                                boUpdateOK = DoUpdate(Update1232());
                                Thread.Sleep(500);
                                break;
                            case "1233":
                                boUpdateOK = DoUpdate(Update1233());
                                Thread.Sleep(500);
                                break;
                            case "1234":
                                boUpdateOK = DoUpdate(Update1234());
                                Thread.Sleep(500);
                                break;
                            case "1235":
                                boUpdateOK = DoUpdate(Update1235());
                                Thread.Sleep(500);
                                break;
                            case "1236":
                                boUpdateOK = DoUpdate(Update1236());
                                Thread.Sleep(500);
                                break;
                            case "1237":
                                boUpdateOK = DoUpdate(Update1237());
                                Thread.Sleep(500);
                                break;
                            case "1238":
                                boUpdateOK = DoUpdate(Update1238());
                                Thread.Sleep(500);
                                break;
                            case "1239":
                                boUpdateOK = DoUpdate(Update1239());
                                Thread.Sleep(500);
                                break;
                            case "1240":
                                boUpdateOK = DoUpdate(Update1240());
                                Thread.Sleep(500);
                                break;
                            case "1241":
                                boUpdateOK = DoUpdate(Update1241());
                                Thread.Sleep(500);
                                break;
                            case "1242":
                                boUpdateOK = DoUpdate(Update1242());
                                Thread.Sleep(500);
                                break;
                            case "1243":
                                boUpdateOK = DoUpdate(Update1243());
                                Thread.Sleep(500);
                                break;
                            case "1244":
                                boUpdateOK = DoUpdate(Update1244());
                                Thread.Sleep(500);
                                break;
                            case "1245":
                                boUpdateOK = DoUpdate(Update1245());
                                Thread.Sleep(500);
                                break;
                            case "1246":
                                boUpdateOK = DoUpdate(Update1246());
                                Thread.Sleep(500);
                                break;
                            case "1247":
                                boUpdateOK = DoUpdate(Update1247());
                                Thread.Sleep(500);
                                break;
                            case "1248":
                                boUpdateOK = DoUpdate(Update1248());
                                Thread.Sleep(500);
                                break;
                            case "1249":
                                boUpdateOK = DoUpdate(Update1249());
                                Thread.Sleep(500);
                                break;
                            case "1250":
                                boUpdateOK = DoUpdate(Update1250());
                                Thread.Sleep(500);
                                break;
                            case "1251":
                                boUpdateOK = DoUpdate(Update1251());
                                Thread.Sleep(500);
                                break;
                            case "1252":
                                boUpdateOK = DoUpdate(Update1252());
                                Thread.Sleep(200);
                                break;
                            case "1253":
                                boUpdateOK = DoUpdate(Update1253());
                                Thread.Sleep(200);
                                break;
                            case "1254":
                                boUpdateOK = DoUpdate(Update1254());
                                Thread.Sleep(200);
                                break;
                            case "1255":
                                boUpdateOK = DoUpdate(Update1255());
                                Thread.Sleep(200);
                                break;
                            case "1256":
                                boUpdateOK = DoUpdate(Update1256());
                                Thread.Sleep(200);
                                break;
                            case "1257":
                                boUpdateOK = DoUpdate(Update1257());
                                Thread.Sleep(200);
                                break;
                            case "1258":
                                boUpdateOK = DoUpdate(Update1258());
                                Thread.Sleep(200);
                                break;
                            case "1259":
                                boUpdateOK = DoUpdate(Update1259());
                                Thread.Sleep(200);
                                break;
                            case "1260":
                                boUpdateOK = DoUpdate(Update1260());
                                Thread.Sleep(200);
                                break;
                            case "1261":
                                boUpdateOK = DoUpdate(Update1261());
                                Thread.Sleep(200);
                                break;
                            case "1262":
                                boUpdateOK = DoUpdate(Update1262());
                                Thread.Sleep(200);
                                break;
                            case "1263":
                                boUpdateOK = DoUpdate(Update1263());
                                Thread.Sleep(200);
                                break;
                            case "1264":
                                boUpdateOK = DoUpdate(Update1264());
                                Thread.Sleep(200);
                                break;
                            case "1265":
                                boUpdateOK = DoUpdate(Update1265());
                                Thread.Sleep(200);
                                break;
                            case "1266":
                                boUpdateOK = DoUpdate(Update1266());
                                Thread.Sleep(200);
                                break;
                            case "1267":
                                boUpdateOK = DoUpdate(Update1267());
                                Thread.Sleep(200);
                                break;
                            case "1268":
                                boUpdateOK = DoUpdate(Update1268());
                                Thread.Sleep(200);
                                break;
                            case "1269":
                                boUpdateOK = DoUpdate(Update1269());
                                Thread.Sleep(200);
                                break;
                            case "1270":
                                boUpdateOK = DoUpdate(Update1270());
                                Thread.Sleep(200);
                                break;
                            case "1271":
                                boUpdateOK = DoUpdate(Update1271());
                                Thread.Sleep(200);
                                break;
                            case "1272":
                                boUpdateOK = DoUpdate(Update1272());
                                Thread.Sleep(200);
                                break;
                            case "1273":
                                boUpdateOK = DoUpdate(Update1273());
                                Thread.Sleep(200);
                                break;
                            case "1274":
                                boUpdateOK = DoUpdate(Update1274());
                                Thread.Sleep(200);
                                break;
                            case "1275":
                                boUpdateOK = DoUpdate(Update1275());
                                Thread.Sleep(200);
                                break;
                            case "1276":
                                boUpdateOK = DoUpdate(Update1276());
                                Thread.Sleep(200);
                                break;
                            case "1277":
                                boUpdateOK = DoUpdate(Update1277());
                                Thread.Sleep(200);
                                break;
                            case "1278":
                                boUpdateOK = DoUpdate(Update1278());
                                Thread.Sleep(200);
                                break;
                            case "1279":
                                boUpdateOK = DoUpdate(Update1279());
                                Thread.Sleep(200);
                                break;
                            case "1280":
                                boUpdateOK = DoUpdate(Update1280());
                                Thread.Sleep(200);
                                break;
                            case "1281":
                                boUpdateOK = DoUpdate(Update1281());
                                Thread.Sleep(200);
                                break;
                            case "1282":
                                boUpdateOK = DoUpdate(Update1282());
                                Thread.Sleep(200);
                                break;
                            case "1283":
                                boUpdateOK = DoUpdate(Update1283());
                                Thread.Sleep(200);
                                break;
                            case "1284":
                                boUpdateOK = DoUpdate(Update1284());
                                Thread.Sleep(200);
                                break;
                            case "1285":
                                boUpdateOK = DoUpdate(Update1285());
                                Thread.Sleep(200);
                                break;
                            case "1286":
                                boUpdateOK = DoUpdate(Update1286());
                                Thread.Sleep(200);
                                break;
                            case "1287":
                                boUpdateOK = DoUpdate(Update1287());
                                Thread.Sleep(200);
                                break;
                            case up1288.const_up1288:
                                boUpdateOK = DoUpdate(up1288.SqlString());
                                Thread.Sleep(200);
                                break;
                            case up1289.const_up1289:
                                boUpdateOK = DoUpdate(up1289.SqlString());
                                Thread.Sleep(200);
                                break;
                            case up1290.const_up1290:
                                boUpdateOK = DoUpdate(up1290.SqlString());
                                Thread.Sleep(200);
                                break;
                            case up1291.const_up1291:
                                boUpdateOK = DoUpdate(up1291.SqlString());
                                Thread.Sleep(200);
                                DoUpdate(up1291.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1292.const_up1292:
                                boUpdateOK = DoUpdate(up1292.SqlString());
                                Thread.Sleep(200);
                                DoUpdate(up1292.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1293.const_up1293:
                                boUpdateOK = DoUpdate(up1293.SqlString());
                                Thread.Sleep(200);
                                DoUpdate(up1293.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1294.const_up1294:
                                boUpdateOK = DoUpdate(up1294.SqlString());
                                boUpdateOK = DoUpdate(up1294.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1295.const_up1295:
                                boUpdateOK = DoUpdate(up1295.SqlString());
                                boUpdateOK = DoUpdate(up1295.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1296.const_up1296:
                                boUpdateOK = DoUpdate(up1296.SqlString());
                                boUpdateOK = DoUpdate(up1296.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1297.const_up1297:
                                boUpdateOK = DoUpdate(up1297.SqlString());
                                boUpdateOK = DoUpdate(up1297.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1298.const_up1298:
                                boUpdateOK = DoUpdate(up1298.SqlString());
                                //boUpdateOK = DoUpdate(up1297.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1299.const_up1299:
                                boUpdateOK = DoUpdate(up1299.SqlString());
                                //boUpdateOK = DoUpdate(up1297.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1300.const_up1300:
                                boUpdateOK = DoUpdate(up1300.SqlString());
                                //boUpdateOK = DoUpdate(up1297.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1301.const_up1301:
                                boUpdateOK = DoUpdate(up1301.SqlString());
                                boUpdateOK = DoUpdate(up1301.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1302.const_up1302:
                                boUpdateOK = DoUpdate(up1302.SqlString());
                                boUpdateOK = DoUpdate(up1302.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1303.const_up1303:
                                boUpdateOK = DoUpdate(up1303.SqlString());
                                boUpdateOK = DoUpdate(up1303.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1304.const_up1304:
                                boUpdateOK = DoUpdate(up1304.SqlString());
                                //boUpdateOK = DoUpdate(up1304.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1305.const_up1305:
                                boUpdateOK = DoUpdate(up1305.SqlString());
                                boUpdateOK = DoUpdate(up1305.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1306.const_up1306:
                                boUpdateOK = DoUpdate(up1306.SqlString());
                                boUpdateOK = DoUpdate(up1306.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1307.const_up1307:
                                boUpdateOK = DoUpdate(up1307.SqlString());
                                boUpdateOK = DoUpdate(up1307.SqlStringUpdate_UpdateExistingColumns());
                                break;
                            case up1308.const_up1308:
                                boUpdateOK = DoUpdate(up1308.SqlString());
                                boUpdateOK = DoUpdate(up1308.SqlStringUpdate_UpdateExistingColumns());
                                break;

                            default:
                                boUpdateOK = true;
                                system.SystemVersionAppDecimal = decTmp;
                                break;
                        }

                        if (boUpdateOK)   //Update OK 
                        {
                            //UpdateFUnktion wird ausgeführt und das Update im Logbuch eingetragen
                            DoUpdate(GetSQLforUpdateVersion(decTmp.ToString()));
                            AddToLog(decTmp.ToString());
                            SetMessageUpdateOK(Functions.FormatDecimalVersion(decTmp));
                        }
                        else
                        {
                            SetMessageUpdateFailed(Functions.FormatDecimalVersion(decTmp));
                            i = UpdateArray.Length;
                        }
                        //neue Versionnummer aus DB auslesen
                        strDBVersion = system.SystemVersionApp.ToString();
                        upMirr.VisibleStartUpdateButton(false);
                        UpdateOK = boUpdateOK;
                    }
                }
            }
        }

        //
        ///<summary>SetMessageUpdateOK(string strVersion) / clsUpdate </summary>
        ///<remarks>Ausgabe der Info, dass das Update erfolgreich durchgeführt wurde.</remarks>
        private void SetMessageUpdateOK(string strVersion)
        {
            upMirr.strFortschritt = Environment.NewLine +
                                    upMirr.strFortschritt.ToString().Trim() +
                                    Environment.NewLine +
                                    "Update auf  Version " + strVersion + " erfolgreich durchgeführt!" +
                                    Environment.NewLine;
            upMirr.SetInfoFortschritt();
        }

        ///<summary>SetMessageUpdateOK(string strVersion) / clsUpdate </summary>
        ///<remarks>Ausgabe der Info, dass es beim Update zu einem Fehler gekommen ist und das Update nicht erfolgreich durchgeführt werden konnte.</remarks>
        private void SetMessageUpdateFailed(string strVersion)
        {
            upMirr.strFortschritt = Environment.NewLine +
                                    upMirr.strFortschritt.ToString().Trim() +
                                    Environment.NewLine +
                                    "Update auf  Version " + strVersion + " ist fehlgeschlagen. Bitte starten Sie Sped4 erneut!" +
                                    Environment.NewLine;
            upMirr.SetInfoFortschritt();
        }

        ///<summary>Update1001() / clsUpdate</summary>
        ///<remarks>"CREATE TABLE [dbo].[test]("+
        ///                       "[id] [int] NULL, "+
        ///                       "[test] [nvarchar](10) NULL "+
        ///                       ") ON [PRIMARY]";</remarks>
        private string Update1001()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[test](" +
                  "[id] [int] NULL, " +
                  "[test] [nvarchar](10) NULL " +
                  ") ON [PRIMARY]";
            return sql;
        }

        //
        ///<summary>Update1002() / clsUpdate</summary>
        ///<remarks>"EXEC sp_rename " +
        ///                          "@objname ='Artikel.Brutto', " +
        ///                          "@newname ='Netto', " +
        ///                          "@objtype = 'COLUMN'; " +
        ///          "ALTER TABLE Artikel ADD  CONSTRAINT [DF_Artikel_Netto]  DEFAULT ((0)) FOR [Netto]; " +
        ///          "ALTER TABLE Artikel ADD [Brutto] [decimal](18, 2) NULL; " +
        ///          "ALTER TABLE Artikel ADD  CONSTRAINT [DF_Artikel_Brutto]  DEFAULT ((0)) FOR [Brutto]; "</remarks>
        private string Update1002()
        {
            string sql = string.Empty;
            sql = "EXEC sp_rename " +
                  "@objname ='Artikel.Brutto', " +
                  "@newname ='Netto', " +
                  "@objtype = 'COLUMN'; " +
                  "ALTER TABLE Artikel ADD  CONSTRAINT [DF_Artikel_Netto]  DEFAULT ((0)) FOR [Netto]; " +
                  "ALTER TABLE Artikel ADD [Brutto] [decimal](18, 2) NULL; " +
                  "ALTER TABLE Artikel ADD  CONSTRAINT [DF_Artikel_Brutto]  DEFAULT ((0)) FOR [Brutto]; ";
            return sql;
        }

        //
        ///<summary>Update1003() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Artikel ALTER COLUMN [gemGewicht] [decimal](18, 2) NOT NULL; " +
        ///          "ALTER TABLE Artikel ALTER COLUMN [Netto] [decimal](18, 2) NOT NULL; " +
        ///          "ALTER TABLE Artikel ALTER COLUMN [Brutto] [decimal](18, 2) NOT NULL; " "</remarks>
        private string Update1003()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ALTER COLUMN [gemGewicht] [decimal](18, 2) NOT NULL; " +
                  "ALTER TABLE Artikel ALTER COLUMN [Netto] [decimal](18, 2) NOT NULL; " +
                  "ALTER TABLE Artikel ALTER COLUMN [Brutto] [decimal](18, 2) NOT NULL; ";
            return sql;
        }

        //
        ///<summary>Update1004() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Frachten ALTER COLUMN [AP_ID] [decimal](18,0) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [Artikel_ID] [decimal](18, 0) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [Fracht] [decimal](18, 2) NOT NULL; " +
        ///          "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_Fracht]  DEFAULT ((0)) FOR [Fracht];"</remarks>
        private string Update1004()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Frachten ALTER COLUMN [AP_ID] [decimal](18,0) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [Artikel_ID] [decimal](18, 0) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [Fracht] [decimal](18, 2) NOT NULL; " +
                  "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_Fracht]  DEFAULT ((0)) FOR [Fracht]; ";
            return sql;
        }

        //
        ///<summary>Update1005() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Frachten ALTER COLUMN [Fracht_ADR] [decimal](28,0) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [km] [Int] NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [fpflGewicht] [decimal] (18,2) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [FV_V_ID] [decimal] (28,0) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [FV_E_ID] [decimal] (28,0) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [MargeEuro] [decimal] (18,2) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [Frachtsatz] [decimal] (18,2) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [MwStSatz] [decimal] (18,2) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [ZusatzKosten] [decimal] (18,2) NOT NULL; "</remarks>
        private string Update1005()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Frachten ALTER COLUMN [Fracht_ADR] [decimal](28,0) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [km] [Int] NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [fpflGewicht] [decimal] (18,2) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [FV_V_ID] [decimal] (28,0) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [FV_E_ID] [decimal] (28,0) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [MargeEuro] [decimal] (18,2) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [Frachtsatz] [decimal] (18,2) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [MwStSatz] [decimal] (18,2) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [ZusatzKosten] [decimal] (18,2) NOT NULL; ";
            return sql;
        }

        //
        ///<summary>Update1006() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Frachten ALTER COLUMN [RG_ID] [decimal] (28,0) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [zw_ZM] [decimal] (28,0) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [zw_Auflieger] [decimal] (28,0) NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [zw_SU] [decimal] (28,0) NOT NULL; "</remarks>
        private string Update1006()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Frachten ALTER COLUMN [RG_ID] [decimal] (28,0) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [zw_ZM] [decimal] (28,0) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [zw_Auflieger] [decimal] (28,0) NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [zw_SU] [decimal] (28,0) NOT NULL; ";
            return sql;
        }

        //
        ///<summary>Update1007() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_RG_ID]  DEFAULT ((0)) FOR [RG_ID]; "</remarks>
        private string Update1007()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_RG_ID]  DEFAULT ((0)) FOR [RG_ID]; ";
            return sql;
        }

        //
        ///<summary>Update1008() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_zw_ZM]  DEFAULT ((0)) FOR [zw_ZM]; " +
        ///          "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_zw_Auflieger]  DEFAULT ((0)) FOR [zw_Auflieger]; " +
        ///          "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_zw_SU]  DEFAULT ((0)) FOR [zw_SU]; "</remarks>
        private string Update1008()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_zw_ZM]  DEFAULT ((0)) FOR [zw_ZM]; " +
                  "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_zw_Auflieger]  DEFAULT ((0)) FOR [zw_Auflieger]; " +
                  "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_zw_SU]  DEFAULT ((0)) FOR [zw_SU]; ";
            return sql;
        }

        //
        ///<summary>Update1010() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Frachten ALTER COLUMN [GS] [bit] NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [GS_SU] [bit] NOT NULL; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [FVGS] [bit] NOT NULL; " +
        ///          "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_GS]  DEFAULT ((0)) FOR [GS]; " +
        ///          "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_GS_SU]  DEFAULT ((0)) FOR [GS_SU]; " +
        ///          "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_FVGS]  DEFAULT ((0)) FOR [FVGS]; "</remarks>
        private string Update1010()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Frachten ALTER COLUMN [GS] [bit] NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [GS_SU] [bit] NOT NULL; " +
                  "ALTER TABLE Frachten ALTER COLUMN [FVGS] [bit] NOT NULL; " +
                  "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_GS]  DEFAULT ((0)) FOR [GS]; " +
                  "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_GS_SU]  DEFAULT ((0)) FOR [GS_SU]; " +
                  "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_FVGS]  DEFAULT ((0)) FOR [FVGS]; ";
            return sql;
        }

        //
        ///<summary>Update1011() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Frachten ALTER COLUMN [Artikel_ID]  [decimal] (28,0) NOT NULL;" +
        ///          "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_Artikel_ID]  DEFAULT ((0)) FOR [Artikel_ID]; "</remarks>
        private string Update1011()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Frachten ALTER COLUMN [Artikel_ID]  [decimal] (28,0) NOT NULL;" +
                  "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_Artikel_ID]  DEFAULT ((0)) FOR [Artikel_ID]; ";
            return sql;
        }

        //
        ///<summary>Update1012() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_Pauschal]  DEFAULT ((0)) FOR [Pauschal]; " +
        ///          "ALTER TABLE Frachten ALTER COLUMN [ZusatzKosten] [decimal] (18,2) NOT NULL; " +
        ///          "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_ZusatzKosten]  DEFAULT ((0)) FOR [ZusatzKosten]; "</remarks>
        private string Update1012()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_Pauschal]  DEFAULT ((0)) FOR [Pauschal]; " +
                  "ALTER TABLE Frachten ALTER COLUMN [ZusatzKosten] [decimal] (18,2) NOT NULL; " +
                  "ALTER TABLE Frachten ADD  CONSTRAINT [DF_Frachten_ZusatzKosten]  DEFAULT ((0)) FOR [ZusatzKosten]; ";
            return sql;
        }

        //
        ///<summary>Update1013() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Artikel ADD [GutZusatz] [varchar](200) DEFAULT (('')) NOT NULL; "</remarks>
        private string Update1013()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ADD [GutZusatz] [varchar](200) DEFAULT (('')) NOT NULL; ";
            return sql;
        }

        //
        ///<summary>Update1014() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Auftrag ADD [km] [int] DEFAULT ((0)) NOT NULL; "</remarks>
        private string Update1014()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Auftrag ADD [km] [int] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        //
        ///<summary>Update1015() / clsUpdate</summary>
        ///<remarks> "ALTER TABLE Entfernungen ALTER COLUMN [km] [int] NOT NULL; "+
        ///          "ALTER TABLE Entfernungen ADD  CONSTRAINT [DF_Entfernungen_km]  DEFAULT ((0)) FOR [km]; "</remarks>
        private string Update1015()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Entfernungen ALTER COLUMN [km] [int] NOT NULL; " +
                  "ALTER TABLE Entfernungen ADD  CONSTRAINT [DF_Entfernungen_km]  DEFAULT ((0)) FOR [km]; ";
            return sql;
        }

        //
        ///<summary>Update1016() / clsUpdate</summary>
        ///<remarks>"EXEC sp_rename 'Entfernungen', 'Distance'"</remarks>
        private string Update1016()
        {
            string sql = string.Empty;
            sql = "EXEC sp_rename 'Entfernungen', 'Distance'";
            return sql;
        }

        //
        ///<summary>Update1017() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE AuftragPos ADD [vKW]  [int] NULL;" +
        ///          "ALTER TABLE AuftragPos ADD [bKW]  [int] NULL;" +
        ///          "ALTER TABLE AuftragPos ADD  CONSTRAINT [DF_AuftragPos_vKW]  DEFAULT ((0)) FOR [vKW]; "+                  
        ///          "ALTER TABLE AuftragPos ADD  CONSTRAINT [DF_AuftragPos_bKW]  DEFAULT ((0)) FOR [bKW]; "</remarks>
        private string Update1017()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE AuftragPos ADD [vKW]  [int] NULL;" +
                  "ALTER TABLE AuftragPos ADD [bKW]  [int] NULL;" +
                  "ALTER TABLE AuftragPos ADD  CONSTRAINT [DF_AuftragPos_vKW]  DEFAULT ((0)) FOR [vKW]; " +
                  "ALTER TABLE AuftragPos ADD  CONSTRAINT [DF_AuftragPos_bKW]  DEFAULT ((0)) FOR [bKW]; ";
            return sql;
        }

        //
        ///<summary>Update1018() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Auftrag DROP COLUMN [Notiz];"</remarks>
        private string Update1018()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Auftrag DROP COLUMN [Notiz];";
            return sql;
        }

        //
        ///<summary>Update1019() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE [User] ADD [dtDispoVon]  datetime NULL; " +
        ///          "ALTER TABLE [User] ADD [dtDispoBis]  datetime NULL; " +
        ///          "ALTER TABLE [User] ADD [FontSize]  [decimal] (2,1) DEFAULT ((7.5)) NOT NULL; "</remarks>
        private string Update1019()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE [User] ADD [dtDispoVon]  datetime NULL; " +
                  "ALTER TABLE [User] ADD [dtDispoBis]  datetime NULL; " +
                  "ALTER TABLE [User] ADD [FontSize]  [decimal] (2,1) DEFAULT ((7.5)) NOT NULL; ";
            return sql;
        }

        //
        ///<summary>Update1020() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE [User] ALTER COLUMN [FontSize]  [decimal] (3,2);"</remarks>
        private string Update1020()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE [User] ALTER COLUMN [FontSize]  [decimal] (3,2);";
            return sql;
        }

        //
        ///<summary>Update1021() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE [User] ALTER COLUMN [FontSize]  [decimal] (5,2);"</remarks>
        private string Update1021()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE [User] ALTER COLUMN [FontSize]  [decimal] (5,2);";
            return sql;
        }

        //
        ///<summary>Update1022() / clsUpdate</summary>
        ///<remarks>"CREATE TABLE Arbeitsbereich("+
        ///                "[ID] [decimal](22, 0) IDENTITY(1,1) NOT NULL, "+
        ///                "[Name] [nvarchar](255) NOT NULL, "+
        ///                "[Bemerkung] [nvarchar](255) NULL, "+
        ///                "CONSTRAINT [PK_Arbeitsbereich] PRIMARY KEY CLUSTERED "+ 
        ///                "( "+
        ///                    "[ID] ASC "+
        ///                ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"+
        ///                ") ON [PRIMARY]; "</remarks>
        private string Update1022()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE Arbeitsbereich(" +
                  "[ID] [decimal](22, 0) IDENTITY(1,1) NOT NULL, " +
                  "[Name] [nvarchar](50) NOT NULL, " +
                  "[Bemerkung] [nvarchar](max) NULL, " +
                  "CONSTRAINT [PK_Arbeitsbereich] PRIMARY KEY CLUSTERED " +
                  "([ID] ASC)" +
                  "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY]; ";
            return sql;
        }

        //
        ///<summary>Update1023() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Arbeitsbereich ADD [aktiv]  [bit] NOT NULL; " +
        ///          "ALTER TABLE Arbeitsbereich ADD  CONSTRAINT [DF_Arbeitsbereich_aktiv]  DEFAULT ((1)) FOR [aktiv];"</remarks>
        private string Update1023()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Arbeitsbereich ADD [aktiv]  [bit] NOT NULL; " +
                  "ALTER TABLE Arbeitsbereich ADD  CONSTRAINT [DF_Arbeitsbereich_aktiv]  DEFAULT ((1)) FOR [aktiv];";
            return sql;
        }

        //
        ///<summary>Update1024() / clsUpdate</summary>
        ///<remarks>"CREATE TABLE [dbo].[Mandanten]("+
        ///                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL,"+
        ///                  "[ADR_ID] [decimal](28, 0) NOT NULL,"+
        ///                  "[MatchCode] [nvarchar](255) NOT NULL,"+
        ///                  "[Beschreibung] [nvarchar](255) NULL,"+
        ///                  "CONSTRAINT [PK_Mandanten] PRIMARY KEY CLUSTERED ([ID] ASC) WITH "+
        ///                  "(PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF,"+
        ///                  "ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY] "+
        ///                  "ALTER TABLE Mandanten WITH CHECK ADD  CONSTRAINT [FK_Mandanten_ADR] FOREIGN KEY([ADR_ID]) "+
        ///                  "REFERENCES ADR ([ID]) "+
        ///                  "ALTER TABLE Mandanten CHECK CONSTRAINT [FK_Mandanten_ADR] "</remarks>
        private string Update1024()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE Mandanten(" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[ADR_ID] [decimal](28, 0) NOT NULL," +
                  "[Beschreibung] [nvarchar](255) NULL," +
                  "CONSTRAINT [PK_Mandanten] PRIMARY KEY CLUSTERED ([ID] ASC) WITH " +
                  "(PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF," +
                  "ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY] " +
                  "ALTER TABLE Mandanten WITH CHECK ADD  CONSTRAINT [FK_Mandanten_ADR] FOREIGN KEY([ADR_ID]) " +
                  "REFERENCES ADR ([ID]) " +
                  "ALTER TABLE Mandanten CHECK CONSTRAINT [FK_Mandanten_ADR] ";
            return sql;
        }

        ///<summary>Update1025() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Mandanten ADD [aktiv]  [bit] DEFAULT ((1)) NOT NULL; ";</remarks>
        private string Update1025()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Mandanten ADD [aktiv]  [bit] DEFAULT ((1)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1026() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Artikel DROP COLUMN [Werk]; " +
        ///          "ALTER TABLE Artikel DROP COLUMN [Halle]; " +
        ///          "ALTER TABLE Artikel DROP COLUMN [Reihe]; " +
        ///          "ALTER TABLE Artikel DROP COLUMN [Platz]; "</remarks>
        private string Update1027()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel DROP COLUMN [Werk]; " +
                  "ALTER TABLE Artikel DROP COLUMN [Halle]; " +
                  "ALTER TABLE Artikel DROP COLUMN [Reihe]; " +
                  "ALTER TABLE Artikel DROP COLUMN [Platz]; ";
            return sql;
        }

        ///<summary>Update1026() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Artikel ADD [BZK] [bit] DEFAULT ((1)) NOT NULL; " +
        ///          "ALTER TABLE Artikel ADD [ENR] [decimal] (28,0) NULL; " +
        ///          "ALTER TABLE Artikel ADD [ANR] [decimal] (28,0) NULL; " +
        ///          "ALTER TABLE Artikel ADD [interneArtID] [nvarchar] (26) NULL; " +
        ///          "ALTER TABLE Artikel ADD [exOrt] [nvarchar] (255) NULL; " +
        ///          "ALTER TABLE Artikel ADD [SPL] [bit] DEFAULT ((0)) NOT NULL; "+
        ///          "ALTER TABLE Artikel ADD [CheckArt] [bit] DEFAULT ((0)) NOT NULL; " +
        ///          "ALTER TABLE Artikel ADD [TarifID] [decimal] (28,0) NULL; " +
        ///          "ALTER TABLE Artikel ADD [Storno] [bit] DEFAULT ((0)) NOT NULL; " +
        ///          "ALTER TABLE Artikel ADD [StornoDate] [DateTime] NULL; " +
        ///          "ALTER TABLE Artikel ADD [UB] [bit] DEFAULT ((0)) NOT NULL; " +
        ///          "ALTER TABLE Artikel ADD [IDvorUB]  [decimal] (28,0) NULL; " +
        ///          "ALTER TABLE Artikel ADD [LagerOrt]  [decimal] (28,0) NULL; " +
        ///          "ALTER TABLE Artikel ADD [AbrufRef] [nvarchar] (255) NULL; " +
        ///          "ALTER TABLE Artikel ADD [TARef] [nvarchar] (255) NULL; " +
        ///          "ALTER TABLE Artikel ADD [ReBookSPL] [DateTime] NULL; " +
        ///          "ALTER TABLE Artikel ADD [RL] [bit] DEFAULT ((0)) NOT NULL; " +
        ///          "ALTER TABLE Artikel ADD [AB_ID] [decimal] (28,0) NULL; " +
        ///          "ALTER TABLE Artikel ADD [Mandanten_ID]  [decimal] (28,0) NULL;</remarks>
        private string Update1028()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ADD [BZK] [bit] DEFAULT ((1)) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [ENR] [decimal] (28,0) NULL; " +
                  "ALTER TABLE Artikel ADD [ANR] [decimal] (28,0) NULL; " +
                  "ALTER TABLE Artikel ADD [interneArtID] [nvarchar] (26) NULL; " +
                  "ALTER TABLE Artikel ADD [exOrt] [nvarchar] (255) NULL; " +
                  "ALTER TABLE Artikel ADD [SPL] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [CheckArt] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [TarifID] [decimal] (28,0) NULL; " +
                  "ALTER TABLE Artikel ADD [Storno] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [StornoDate] [DateTime] NULL; " +
                  "ALTER TABLE Artikel ADD [UB] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [IDvorUB]  [decimal] (28,0) NULL; " +
                  "ALTER TABLE Artikel ADD [LagerOrt]  [decimal] (28,0) NULL; " +
                  "ALTER TABLE Artikel ADD [AbrufRef] [nvarchar] (255) NULL; " +
                  "ALTER TABLE Artikel ADD [TARef] [nvarchar] (255) NULL; " +
                  "ALTER TABLE Artikel ADD [ReBookSPL] [DateTime] NULL; " +
                  "ALTER TABLE Artikel ADD [RL] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [AB_ID] [decimal] (28,0) NULL; " +
                  "ALTER TABLE Artikel ADD [Mandanten_ID]  [decimal] (28,0) NULL; ";
            return sql;
        }

        ///<summary>Update1029() / clsUpdate</summary>
        ///<remarks>"CREATE TABLE [dbo].[PrimeKeys](" +
        ///         "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
        ///         "[Mandanten_ID] [decimal](28, 0) NULL," +
        ///         "[AuftragsNr] [decimal](28, 0) NULL," +
        ///         "[LfsNr] [decimal](28, 0) NULL," +
        ///         "[LvsNr] [decimal](28, 0) NULL," +
        ///         "[RGNr] [decimal](28, 0) NULL," +
        ///         "[GSNr] [decimal](28, 0) NULL" +
        ///          ") ON [PRIMARY];";</remarks>
        private string Update1029()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[PrimeKeys](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[Mandanten_ID] [decimal](28, 0) DEFAULT ((0)) NOT NULL," +
                  "[AuftragsNr] [decimal](28, 0) DEFAULT ((1)) NOT NULL," +
                  "[LfsNr] [decimal](28, 0) DEFAULT ((1)) NOT NULL," +
                  "[LvsNr] [decimal](28, 0) DEFAULT ((1)) NOT NULL," +
                  "[RGNr] [decimal](28, 0) DEFAULT ((1)) NOT NULL," +
                  "[GSNr] [decimal](28, 0) DEFAULT ((1)) NOT NULL" +
                  ") ON [PRIMARY];";
            return sql;
        }

        ///<summary>Update1030() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Mandanten ADD [Matchcode] [nvarchar] (255) NULL; "</remarks>
        private string Update1030()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Mandanten ADD [Matchcode] [nvarchar] (255) NULL; ";
            return sql;
        }

        ///<summary>Update1031() / clsUpdate</summary>
        ///<remarks>"DROP Table [Auftragsnummer]; " 
        ///       "DROP Table [Lieferscheinnummer]; " 
        ///       "DROP Table [LVSNummer]; " 
        ///       "DROP Table [Rechnungsnummer];"</remarks>
        private string Update1031()
        {
            string sql = string.Empty;
            sql = "DROP Table [Auftragsnummer]; " +
                  "DROP Table [Lieferscheinnummer]; " +
                  "DROP Table [LVSNummer]; " +
                  "DROP Table [Rechnungsnummer]; ";
            return sql;
        }

        ///<summary>Update1032() / clsUpdate</summary>
        ///<remarks>"ALTER TABLE Auftrag ADD [MandantenID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; " 
        ///          "ALTER TABLE Auftrag ADD [ArbeitsbereichID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";</remarks>
        private string Update1032()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Auftrag ADD [MandantenID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Auftrag ADD [ArbeitsbereichID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";

            return sql;
        }

        ///<summary>Update1033() / clsUpdate</summary>
        ///<remarks>"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tarife]') AND type in (N'U')) "+
        ///          "DROP TABLE [dbo].[Tarife]; "+
        ///          
        ///           "CREATE TABLE [dbo].[Tarife]("+
        ///           "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL,"+
        ///           "[Tarifname] [nvarchar](100) NOT NULL,"+
        ///           "[Beschreibung] [nvarchar](max) NULL,"+
        ///           "[aktiv] [bit] NULL,"+
        ///           "[Art] [nvarchar](30) NULL,"+
        ///           "CONSTRAINT [PK_Tarife] PRIMARY KEY CLUSTERED ([ID] ASC"+
        ///           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"+
        ///          ") ON [PRIMARY]; "+
        ///
        ///         "CREATE TABLE [dbo].[TarifPositionen]("+
        ///         "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL,"+
        ///         "[TarifID] [decimal](28, 0) NOT NULL,"+
        ///         "[GrundEinheit] [nvarchar](10) NOT NULL,"+
        ///         "[Lagerdauer] [int] NULL,"+
        ///         "[Zeitraumbezogen] [bit] NOT NULL,"+
        ///         "[€/Einheit] [decimal](18, 2) NOT NULL,"+
        ///         "[EinheitVon] [int] NULL,"+
        ///         "[EinheitBis] [int] NULL,"+
        ///         "CONSTRAINT [PK_TarifPositionen] PRIMARY KEY CLUSTERED ([ID] ASC"+
        ///         ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"+
        ///         ") ON [PRIMARY]; "+
        ///
        ///         "ALTER TABLE [dbo].[TarifPositionen]  WITH CHECK ADD  CONSTRAINT [FK_TarifPositionen_Tarife] FOREIGN KEY([TarifID]) "+
        ///         "REFERENCES [dbo].[Tarife] ([ID]); "+
        ///         "ALTER TABLE [dbo].[TarifPositionen] CHECK CONSTRAINT [FK_TarifPositionen_Tarife]; "+
        ///         "ALTER TABLE [dbo].[TarifPositionen] ADD  CONSTRAINT [DF_TarifPositionen_Zeitraumbezogen]  DEFAULT ((0)) FOR [Zeitraumbezogen]; "+
        ///         "ALTER TABLE [dbo].[TarifPositionen] ADD  CONSTRAINT [DF_TarifPositionen_€/Einheit]  DEFAULT ((0)) FOR [€/Einheit]; ";</remarks>
        private string Update1033()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tarife]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[Tarife]; " +
                  "CREATE TABLE [dbo].[Tarife](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[Tarifname] [nvarchar](100) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NULL," +
                  "[aktiv] [bit] NULL," +
                  "[Art] [nvarchar](30) NULL," +
                  "CONSTRAINT [PK_Tarife] PRIMARY KEY CLUSTERED ([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY]; " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TarifPositionen]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[TarifPositionen] " +
                  "CREATE TABLE [dbo].[TarifPositionen](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[TarifID] [decimal](28, 0) NOT NULL," +
                  "[BasisEinheit] [nvarchar](10) NOT NULL," +
                  "[AbrEinheit] [nvarchar](10) NOT NULL," +
                  "[Lagerdauer] [int] NULL," +
                  "[Zeitraumbezogen] [bit] NOT NULL," +
                  "[PreisEinheit] [decimal](18, 2) NOT NULL," +
                  "[EinheitVon] [int] NOT NULL," +
                  "[EinheitBis] [int] NOT NULL," +
                  "[MargeProzentEinheit] [decimal](3, 2) NOT NULL," +
                  "[MargePreisEinheit] [decimal](18, 2) NOT NULL," +
                  "CONSTRAINT [PK_TarifPositionen] PRIMARY KEY CLUSTERED" +
                  "([ID] ASC)" +
                  "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_KundenTarife_ADR]') AND parent_object_id = OBJECT_ID(N'[dbo].[KundenTarife]')) " +
                  "ALTER TABLE [dbo].[KundenTarife] DROP CONSTRAINT [FK_KundenTarife_ADR] " +
                  "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_KundenTarife_Tarife]') AND parent_object_id = OBJECT_ID(N'[dbo].[KundenTarife]')) " +
                  "ALTER TABLE [dbo].[KundenTarife] DROP CONSTRAINT [FK_KundenTarife_Tarife] " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_KundenTarife_TarifID]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[KundenTarife] DROP CONSTRAINT [DF_KundenTarife_TarifID] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_KundenTarife_AdrID]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[KundenTarife] DROP CONSTRAINT [DF_KundenTarife_AdrID] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KundenTarife]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[KundenTarife]; " +
                  "CREATE TABLE [dbo].[KundenTarife](" +
                  "[TarifID] [decimal](28, 0) NULL," +
                  "[AdrID] [decimal](28, 0) NULL" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[KundenTarife]  WITH CHECK ADD  CONSTRAINT [FK_KundenTarife_ADR] FOREIGN KEY([AdrID]) " +
                  "REFERENCES [dbo].[ADR] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[KundenTarife] CHECK CONSTRAINT [FK_KundenTarife_ADR] " +
                  "ALTER TABLE [dbo].[KundenTarife]  WITH CHECK ADD  CONSTRAINT [FK_KundenTarife_Tarife] FOREIGN KEY([TarifID]) " +
                  "REFERENCES [dbo].[Tarife] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[KundenTarife] CHECK CONSTRAINT [FK_KundenTarife_Tarife] " +
                  "ALTER TABLE [dbo].[KundenTarife] ADD  CONSTRAINT [DF_KundenTarife_TarifID]  DEFAULT ((0)) FOR [TarifID] " +
                  "ALTER TABLE [dbo].[KundenTarife] ADD  CONSTRAINT [DF_KundenTarife_AdrID]  DEFAULT ((0)) FOR [AdrID] ";

            return sql;
        }

        ///<summary>Update1034() / clsUpdate</summary>
        ///<remarks>Table Schaeden und SchadenZuweisung werden erstellt.</remarks>
        private string Update1034()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[Schaeden](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[Bezeichnung] [nvarchar](max) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NULL," +
                  "[aktiv] [bit] NULL," +
                  "CONSTRAINT [PK_Schaeden] PRIMARY KEY CLUSTERED" +
                  "([ID] ASC)" +
                  "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY]; " +
                  "CREATE TABLE [dbo].[SchadenZuweisung](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[ArtikelID] [decimal](28, 0) NOT NULL," +
                  "[SchadenID] [decimal](28, 0) NOT NULL," +
                  "[UserID] [decimal](28, 0) NOT NULL," +
                  "[Datum] [datetime] NULL," +
                  "CONSTRAINT [PK_SchadenZuweisung] PRIMARY KEY CLUSTERED" +
                  "([ID] ASC)" +
                  "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY]; " +
                  "ALTER TABLE [dbo].[SchadenZuweisung]  WITH CHECK ADD  CONSTRAINT [FK_SchadenZuweisung_Artikel] FOREIGN KEY([ArtikelID]) " +
                  "REFERENCES [dbo].[Artikel] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[SchadenZuweisung] CHECK CONSTRAINT [FK_SchadenZuweisung_Artikel]; " +
                  "ALTER TABLE [dbo].[SchadenZuweisung]  WITH CHECK ADD  CONSTRAINT [FK_SchadenZuweisung_Schaeden] FOREIGN KEY([SchadenID]) " +
                  "REFERENCES [dbo].[Schaeden] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[SchadenZuweisung] CHECK CONSTRAINT [FK_SchadenZuweisung_Schaeden]; ";

            return sql;
        }

        ///<summary>Update1035() / clsUpdate</summary>
        ///<remarks>Änderung der Table AuftragScan:
        ///         - Tablename ändern in DocScan</remarks>
        private string Update1035()
        {
            string sql = string.Empty;
            sql = "EXEC sp_rename 'AuftragScan', 'DocScan'; ";
            return sql;
        }

        ///<summary>Update1036() / clsUpdate</summary>
        ///<remarks>Änderung der Table DocScan:
        ///         - Spalte ImageArt löschen
        ///         - Spalte AuftragPos löschen
        ///         - Spalte Vorgang ID  erstellen
        ///         - Spalte Vorgang erstellen</remarks>
        private string Update1036()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE DocScan DROP COLUMN ImageArt; " +
                  "ALTER TABLE DocScan DROP COLUMN AuftragPosID; " +
                  "ALTER TABLE DocScan ADD [LEingangID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE DocScan ADD [LAusgangID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1037() / clsUpdate</summary>
        ///<remarks>NEUE Table :
        ///         - LEingang >>> Lagereingang
        ///         - LAusgang >>> Lagerausgang</remarks>
        private string Update1037()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[LEingang](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[Date] [datetime] NULL," +
                  "[GewichtNetto] [decimal](18, 2) NULL," +
                  "[GewichtBrutto] [decimal](18, 2) NULL," +
                  "[Auftraggeber] [decimal](28, 0) NULL," +
                  "[Empfaenger] [decimal](28, 0) NULL," +
                  "[Lieferant] [decimal](28, 0) NULL," +
                  "[AbBereich] [decimal](28, 0) NULL," +
                  "[Mandant] [decimal](28, 0) NULL," +
                  " CONSTRAINT [PK_LEingang] PRIMARY KEY CLUSTERED([ID] ASC)" +
                  "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY]; " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_GewichtNetto]  DEFAULT ((0)) FOR [GewichtNetto]; " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_GewichtBrutto]  DEFAULT ((0)) FOR [GewichtBrutto]; " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_Auftraggeber]  DEFAULT ((0)) FOR [Auftraggeber]; " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_Empfaenger]  DEFAULT ((0)) FOR [Empfaenger]; " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_Lieferant]  DEFAULT ((0)) FOR [Lieferant]; " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_AbBereich]  DEFAULT ((0)) FOR [AbBereich]; " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_Mandant]  DEFAULT ((0)) FOR [Mandant]; " +
                  "CREATE TABLE [dbo].[LAusgang](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[Datum] [datetime] NULL," +
                  "[GewichtNetto] [decimal](18, 2) NULL," +
                  "[GewichtBrutto] [decimal](18, 2) NULL," +
                  "[Auftraggeber] [decimal](28, 0) NULL," +
                  "[Empfaenger] [decimal](28, 0) NULL," +
                  "[Lieferant] [decimal](28, 0) NULL," +
                  "[LfsNr] [nvarchar](50) NULL," +
                  "[LfsDate] [datetime] NULL," +
                  "[SLB] [decimal](28, 0) NULL," +
                  "[MAT] [nvarchar](50) NULL," +
                  "[Check] [bit] NULL," +
                  "[CheckDate] [datetime] NULL," +
                  "[SpedID] [decimal](28, 0) NULL," +
                  "[KFZ] [nvarchar](50) NULL," +
                  "[USER] [decimal](28, 0) NULL," +
                  "[ASN] [decimal](28, 0) NULL," +
                  "[Info] [nvarchar](max) NULL," +
                  "[AbBereich] [decimal](28, 0) NULL," +
                  "[Mandant] [decimal](28, 0) NULL," +
                  " CONSTRAINT [PK_LAusgang] PRIMARY KEY CLUSTERED ([ID] ASC)" +
                  "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_GewichtNetto]  DEFAULT ((0)) FOR [GewichtNetto]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_GewichtBrutto]  DEFAULT ((0)) FOR [GewichtBrutto]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Auftraggeber]  DEFAULT ((0)) FOR [Auftraggeber]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Empfaenger]  DEFAULT ((0)) FOR [Empfaenger]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Lieferant]  DEFAULT ((0)) FOR [Lieferant]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_SLB]  DEFAULT ((0)) FOR [SLB]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Check]  DEFAULT ((0)) FOR [Check]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_SpedID]  DEFAULT ((0)) FOR [SpedID]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_USER]  DEFAULT ((0)) FOR [USER]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_ASN]  DEFAULT ((0)) FOR [ASN]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_AbBereich]  DEFAULT ((0)) FOR [AbBereich]; " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Mandant]  DEFAULT ((0)) FOR [Mandant]; ";

            return sql;
        }

        ///<summary>Update1038() / clsUpdate</summary>
        ///<remarks>NEUE Table :
        ///         - LEingang >>> Lagereingang
        ///         - LAusgang >>> Lagerausgang</remarks>
        private string Update1038()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DocScan_Auftrag]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocScan]')) " +
                  "ALTER TABLE [dbo].[DocScan] DROP CONSTRAINT [FK_DocScan_Auftrag]; " +
                  "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DocScan_DocScan]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocScan]')) " +
                  "ALTER TABLE [dbo].[DocScan] DROP CONSTRAINT [FK_DocScan_DocScan]; " +
                  "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DocScan_LEingang]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocScan]')) " +
                  "ALTER TABLE [dbo].[DocScan] DROP CONSTRAINT [FK_DocScan_LEingang]; " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__DocScan__LEingan__178D7CA5]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[DocScan] DROP CONSTRAINT [DF__DocScan__LEingan__178D7CA5] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__DocScan__LAusgan__1881A0DE]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[DocScan] DROP CONSTRAINT [DF__DocScan__LAusgan__1881A0DE] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocScan]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[DocScan] " +
                  "CREATE TABLE [dbo].[DocScan](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[AuftragID] [decimal](28, 0) NULL," +
                  "[LEingangID] [decimal](28, 0) NULL," +
                  "[LAusgangID] [decimal](28, 0) NULL," +
                  "[PicNum] [int] NULL," +
                  "[Pfad] [varchar](max) NULL," +
                  "[ScanFilename] [varchar](50) NULL," +
                  "CONSTRAINT [PK_AuftragScan] PRIMARY KEY CLUSTERED([ID] ASC)" +
                  "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                  ") ON [PRIMARY]; " +
                  "ALTER TABLE [dbo].[DocScan] ADD  CONSTRAINT [DF_DocScan_AuftragID]  DEFAULT ((0)) FOR [AuftragID] " +
                  "ALTER TABLE [dbo].[DocScan] ADD  CONSTRAINT [DF__DocScan__LEingan__178D7CA5]  DEFAULT ((0)) FOR [LEingangID] " +
                  "ALTER TABLE [dbo].[DocScan] ADD  CONSTRAINT [DF__DocScan__LAusgan__1881A0DE]  DEFAULT ((0)) FOR [LAusgangID] ";

            return sql;
        }

        ///<summary>Update1039() / clsUpdate</summary>
        ///<remarks>NEUE Spalte in Table DocScan >>>> ImagArt (Beschreibung Dokument) </remarks>
        private string Update1039()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE DocScan ADD [ImageArt] [nvarchar](255) NULL; ";

            return sql;
        }

        ///<summary>Update1040() / clsUpdate</summary>
        ///<remarks>Table Güterarten wird erweitert.</remarks>
        private string Update1040()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Gueterart ADD [Netto] [decimal] (18,2) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Gueterart ADD [Brutto] [decimal] (18,2) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Gueterart ADD [ArtikelArt] [nvarchar](255) NULL; " +
                  "ALTER TABLE Gueterart ADD [Besonderheit] [nvarchar](255) NULL; " +
                  "ALTER TABLE Gueterart ADD [Verpackung] [nvarchar](255) NULL; " +
                  "ALTER TABLE Gueterart ADD [AbsteckBolzenNr] [nvarchar](255) NULL; " +
                  "ALTER TABLE Gueterart ADD [MEAbsteckBolzen] [nvarchar](255) NULL; " +
                  "ALTER TABLE Gueterart ADD [Arbeitsbereich] [decimal] (28,0) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Gueterart ADD [LieferantenID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Gueterart ADD [aktiv] [bit] DEFAULT ((1)) NOT NULL; ";

            return sql;
        }

        ///<summary>Update1041() / clsUpdate</summary>
        ///<remarks>Table Güterarten wird erweitert.</remarks>
        private string Update1041()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[Lieferanten](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[AdrID] [decimal](28, 0) NOT NULL, " +
                  "[LieferantenID] [decimal](28, 0) NOT NULL," +
                  "[ArbeitsbereichsID] [decimal](28, 0) NOT NULL," +
                  "[Verweis] [varchar](100) NULL," +
                  "[aktiv] [bit] NOT NULL," +
                  "CONSTRAINT [PK_Lieferanten] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Lieferanten] ADD  CONSTRAINT [DF_Lieferanten_AdrID]  DEFAULT ((0)) FOR [AdrID] " +
                  "ALTER TABLE [dbo].[Lieferanten] ADD  CONSTRAINT [DF_Lieferanten_LieferantenID]  DEFAULT ((0)) FOR [LieferantenID] " +
                  "ALTER TABLE [dbo].[Lieferanten] ADD  CONSTRAINT [DF_Lieferanten_ArbeitsbereichsID]  DEFAULT ((0)) FOR [ArbeitsbereichsID] " +
                  "ALTER TABLE [dbo].[Lieferanten] ADD  CONSTRAINT [DF_Lieferanten_aktiv]  DEFAULT ((1)) FOR [aktiv] ";

            return sql;
        }

        ///<summary>Update1042() / clsUpdate</summary>
        ///<remarks>Table UserList für die vom User erstellen Listen wird erstellt.</remarks>
        private string Update1042()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[UserList](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[Bezeichnung] [varchar](255) NULL," +
                  "[Table] [varchar](50) NULL," +
                  "[Column] [varchar](50) NULL," +
                  "[ColViewName] [varchar](100) NULL," +
                  "[Aktion] [varchar](50) NULL," +
                  "CONSTRAINT [PK_UserList] PRIMARY KEY CLUSTERED([ID] ASC)" +
                  "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY]";
            return sql;
        }

        ///<summary>Update1043() / clsUpdate</summary>
        ///<remarks>Table PrimeKeys - zusätzliche Spalten LagerEingangID und LagerAusgangID.</remarks>
        private string Update1043()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE PrimeKeys ADD [LEingangID] [decimal] (28,0) DEFAULT ((1)) NOT NULL; " +
                  "ALTER TABLE PrimeKeys ADD [LAusgangID] [decimal] (28,0) DEFAULT ((1)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1044() / clsUpdate</summary>
        ///<remarks>Table PrimeKeys - zusätzliche Spalten LEingangID.
        ///         -Löschen  Gewichte</remarks>
        private string Update1044()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_LEingangID]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_LEingangID] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_GewichtNetto]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_GewichtNetto] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_GewichtBrutto]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_GewichtBrutto] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_Auftraggeber]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_Auftraggeber] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_Empfaenger]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_Empfaenger] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_Lieferant]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_Lieferant] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_AbBereich]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_AbBereich] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_Mandant]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_Mandant] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LEingang]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[LEingang] " +
                  "CREATE TABLE [dbo].[LEingang]( " +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL, " +
                  "[LEingangID] [decimal](28, 0) NULL, " +
                  "[Date] [datetime] NULL, " +
                  "[GewichtNetto] [decimal](18, 2) NULL, " +
                  "[GewichtBrutto] [decimal](18, 2) NULL, " +
                  "[Auftraggeber] [decimal](28, 0) NULL, " +
                  "[Empfaenger] [decimal](28, 0) NULL, " +
                  "[Lieferant] [decimal](28, 0) NULL, " +
                  "[AbBereich] [decimal](28, 0) NULL, " +
                  "[Mandant] [decimal](28, 0) NULL, " +
                  "CONSTRAINT [PK_LEingang] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_LEingangID]  DEFAULT ((0)) FOR [LEingangID] " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_GewichtNetto]  DEFAULT ((0)) FOR [GewichtNetto] " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_GewichtBrutto]  DEFAULT ((0)) FOR [GewichtBrutto] " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_Auftraggeber]  DEFAULT ((0)) FOR [Auftraggeber] " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_Empfaenger]  DEFAULT ((0)) FOR [Empfaenger] " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_Lieferant]  DEFAULT ((0)) FOR [Lieferant] " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_AbBereich]  DEFAULT ((0)) FOR [AbBereich] " +
                  "ALTER TABLE [dbo].[LEingang] ADD  CONSTRAINT [DF_LEingang_Mandant]  DEFAULT ((0)) FOR [Mandant] ";
            return sql;
        }

        ///<summary>Update1045() / clsUpdate</summary>
        ///<remarks>Table PrimeKeys - zusätzliche Spalten Lieferscheinnummer.</remarks>
        private string Update1045()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE [dbo].[LEingang] ADD [LfsNr] [nvarchar] (255) NULL; ";
            return sql;
        }

        /****
        ///<summary>Update1046() / clsUpdate</summary>
        ///<remarks>Table Artikel - zusätzliche Spalten LEingangID, LAusgangID.</remarks>
        private string Update1046()
        {
           
        string sql = string.Empty;
        sql = "ALTER TABLE Artikel ADD [LEingangID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; " +
        "ALTER TABLE Artikel ADD [LAusgangID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
        return sql;
            
        }
        * ***/
        ///<summary>Update1047() / clsUpdate</summary>
        ///<remarks>Table LEingang - zusätzliche Spalten DFUE.</remarks>
        private string Update1047()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE LEingang ADD [DFUE] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1048() / clsUpdate</summary>
        ///<remarks>Table LEingang - zusätzliche Spalten DFUE.</remarks>
        private string Update1048()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ADD [LEingangTableID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [LAusgangTableID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1049() / clsUpdate</summary>
        ///<remarks>Table LEingang - zusätzliche Spalten DFUE.</remarks>
        private string Update1049()
        {
            string sql = string.Empty;

            sql = "ALTER TABLE Artikel DROP COLUMN [Schaden]; " +
                  "ALTER TABLE Artikel DROP COLUMN [Schadensbeschreibung]; " +
                  "ALTER TABLE Artikel DROP COLUMN [interneArtID]; " +
                  "ALTER TABLE Artikel ADD [ArtIDRef]  [nvarchar] (20) NULL; ";
            return sql;
        }

        ///<summary>Update1050() / clsUpdate</summary>
        ///<remarks>Table LEingang - zusätzliche Spalten DFUE.</remarks>
        private string Update1050()
        {
            string sql = string.Empty;

            sql = "EXEC sp_rename " +
                  "@objname ='Artikel.BZK', " +
                  "@newname ='BKZ', " +
                  "@objtype = 'COLUMN'; ";
            return sql;
        }

        ///<summary>Update1051() / clsUpdate</summary>
        ///<remarks>Table Artikel -löschen der spalten ENR und ANR</remarks>
        private string Update1051()
        {
            string sql = string.Empty;

            sql = "ALTER TABLE Artikel DROP COLUMN [ENR]; " +
                  "ALTER TABLE Artikel DROP COLUMN [ANR]; ";
            return sql;
        }

        ///<summary>Update1052() / clsUpdate</summary>
        ///<remarks>Table ArtikelVita neu anlegen</remarks>
        private string Update1052()
        {
            string sql = string.Empty;

            sql = "CREATE TABLE [dbo].[ArtikelVita](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[TableID] [decimal](28, 0) NOT NULL," +
                  "[TableName] [nvarchar](50) NOT NULL," +
                  "[Aktion] [nvarchar](255) NOT NULL," +
                  "[Datum] [datetime] NOT NULL," +
                  "[UserID] [decimal](28, 0) NOT NULL," +
                  "CONSTRAINT [PK_ArtikelVita] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY]";
            return sql;
        }

        ///<summary>Update1053() / clsUpdate</summary>
        ///<remarks>Table LEingang neue Spalte Check</remarks>
        private string Update1053()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE LEingang ADD [Check] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1054() / clsUpdate</summary>
        ///<remarks>Table Artikel Spalte ArtIDRef - spaltenlänge ändern auf 30</remarks>
        private string Update1054()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel DROP COLUMN [ArtIDRef]; " +
                  "ALTER TABLE Artikel ADD [ArtIDRef] [nvarchar] (26) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1055() / clsUpdate</summary>
        ///<remarks>Table Artikel  neue Spalte für LAgerort</remarks>
        private string Update1055()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ADD [Werk] [nvarchar] (100) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [Halle] [nvarchar] (100) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [Reihe] [nvarchar] (100) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [Platz] [nvarchar] (100) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1056() / clsUpdate</summary>
        ///<remarks>Table Artikel  neue Spalte für LAgerort</remarks>
        private string Update1056()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE TarifPositionen ADD [TarifPosArt] [nvarchar] (100) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1057() / clsUpdate</summary>
        ///<remarks>Table Artikel  neue Spalte für LAgerort</remarks>
        private string Update1057()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE ArtikelVita ADD [Beschreibung] [nvarchar] (255) NULL; ";
            return sql;
        }

        ///<summary>Update1058() / clsUpdate</summary>
        ///<remarks>Table Artikel  neue Spalte für AuftragPosTableID</remarks>
        private string Update1058()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ADD [AuftragPosTableID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1059() / clsUpdate</summary>
        ///<remarks>Table Artikel  neue Spalte für AuftragPosTableID</remarks>
        private string Update1059()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE AuftragPos ADD [AuftragTableID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1060() / clsUpdate</summary>
        ///<remarks>Table LAusgang löschen</remarks>
        private string Update1060()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_LAusgangID]') AND type = 'D')" +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_LAusgangID] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_GewichtNetto]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_GewichtNetto] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_GewichtBrutto]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_GewichtBrutto] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_Auftraggeber]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_Auftraggeber] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_Versender]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_Versender] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_Empfaenger]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_Empfaenger] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_Entladestelle]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_Entladestelle] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_Lieferant]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_Lieferant] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_SLB]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_SLB] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_Check]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_Check] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_SpedID]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_SpedID] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_USER]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_USER] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_ASN]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_ASN] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_AbBereich]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_AbBereich] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_Mandant]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_Mandant] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LAusgang]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[LAusgang] ";

            return sql;
        }

        ///<summary>Update1061() / clsUpdate</summary>
        ///<remarks>Table LAusgang neu erstellen</remarks>
        private string Update1061()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[LAusgang](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[LAusgangID] [decimal](28, 0) NOT NULL," +
                  "[Datum] [datetime] NOT NULL," +
                  "[Netto] [decimal](18, 2) NOT NULL," +
                  "[Brutto] [decimal](18, 2) NOT NULL," +
                  "[Auftraggeber] [decimal](28, 0) NOT NULL," +
                  "[Versender] [decimal](28, 0) NOT NULL," +
                  "[Empfaenger] [decimal](28, 0) NOT NULL," +
                  "[Entladestelle] [decimal](28, 0) NOT NULL," +
                  "[Lieferant] [decimal](28, 0) NOT NULL," +
                  "[LfsNr] [nvarchar](50) NOT NULL," +
                  "[LfsDate] [datetime] NULL," +
                  "[SLB] [decimal](28, 0) NOT NULL," +
                  "[MAT] [nvarchar](50) NOT NULL," +
                  "[Checked] [bit] NOT NULL," +
                  "[SpedID] [decimal](28, 0) NOT NULL," +
                  "[KFZ] [nvarchar](50) NULL," +
                  "[USER] [decimal](28, 0) NOT NULL," +
                  "[ASN] [decimal](28, 0) NOT NULL," +
                  "[Info] [nvarchar](max) NULL," +
                  "[AbBereich] [decimal](28, 0) NOT NULL," +
                  "[MandantenID] [decimal](28, 0) NOT NULL," +
                  "CONSTRAINT [PK_LAusgang] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_LAusgangID]  DEFAULT ((0)) FOR [LAusgangID] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_GewichtNetto]  DEFAULT ((0)) FOR [Netto] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_GewichtBrutto]  DEFAULT ((0)) FOR [Brutto] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Auftraggeber]  DEFAULT ((0)) FOR [Auftraggeber] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Versender]  DEFAULT ((0)) FOR [Versender] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Empfaenger]  DEFAULT ((0)) FOR [Empfaenger] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Entladestelle]  DEFAULT ((0)) FOR [Entladestelle] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Lieferant]  DEFAULT ((0)) FOR [Lieferant] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_SLB]  DEFAULT ((0)) FOR [SLB] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Check]  DEFAULT ((0)) FOR [Checked] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_SpedID]  DEFAULT ((0)) FOR [SpedID] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_USER]  DEFAULT ((0)) FOR [USER] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_ASN]  DEFAULT ((0)) FOR [ASN] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_AbBereich]  DEFAULT ((0)) FOR [AbBereich] " +
                  "ALTER TABLE [dbo].[LAusgang] ADD  CONSTRAINT [DF_LAusgang_Mandant]  DEFAULT ((0)) FOR [MandantenID] ";
            return sql;
        }

        ///<summary>Update1062() / clsUpdate</summary>
        ///<remarks>Table LAusgang neu erstellen</remarks>
        private string Update1062()
        {
            string sql = string.Empty;
            sql = "EXEC sp_rename " +
                  "@objname ='LEingang.DFUE', " +
                  "@newname ='ASN', " +
                  "@objtype = 'COLUMN'; ";
            return sql;
        }

        ///<summary>Update1063() / clsUpdate</summary>
        ///<remarks>Table LAusgang Spalte User löschen</remarks>
        private string Update1063()
        {
            string sql = string.Empty;
            sql = "DROP TABLE EA; ";
            return sql;
        }

        ///<summary>Update1064() / clsUpdate</summary>
        ///<remarks>Table LAusgang Spalte User löschen</remarks>
        private string Update1064()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ADD [LA_Checked] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1065() / clsUpdate</summary>
        ///<remarks>Table Rechnungen wird aktualisiert</remarks>
        private string Update1065()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rechnungen_Rechnungen]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rechnungen]')) " +
                  "ALTER TABLE [dbo].[Rechnungen] DROP CONSTRAINT [FK_Rechnungen_Rechnungen] " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rechnungen]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[Rechnungen] " +
                  "CREATE TABLE [dbo].[Rechnungen](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[RGNr] [decimal](28, 0) NOT NULL," +
                  "[Datum] [datetime] NOT NULL," +
                  "[faellig] [datetime] NOT NULL," +
                  "[Druck] [bit] NULL," +
                  "[Druckdatum] [datetime] NULL," +
                  "[Benutzer] [int] NULL," +
                  "[Benutzer_Date] [datetime] NULL," +
                  "[GS] [bit] NOT NULL," +
                  "[Bezahlt] [datetime] NULL," +
                  "[MwStSatz] [decimal](18, 2) NULL," +
                  "[exFibu] [bit] NULL," +
                  "[RGArt] [varchar](50) NULL," +
                  "CONSTRAINT [PK_Rechnungen] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Rechnungen]  WITH CHECK ADD  CONSTRAINT [FK_Rechnungen_Rechnungen] FOREIGN KEY([ID]) " +
                  "REFERENCES [dbo].[Rechnungen] ([ID]) " +
                  "ALTER TABLE [dbo].[Rechnungen] CHECK CONSTRAINT [FK_Rechnungen_Rechnungen] ";

            return sql;
        }

        ///<summary>Update1066() / clsUpdate</summary>
        ///<remarks>Table RGPositionen wird aktualisiert</remarks>
        private string Update1066()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[RGPositionen](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[RGTableID] [decimal](28, 0) NOT NULL," +
                  "[Position] [int] NOT NULL," +
                  "[RGText] [varchar](255) NULL," +
                  "[Abrechnungseinheit] [varchar](30) NULL," +
                  "[EinzelPreis] [decimal](18, 2) NULL," +
                  "[Abrechnungsart] [varchar](50) NULL," +
                  "[TarifID] [decimal](28, 0) NOT NULL," +
                  "[Tarifbeschreibung] [varchar](50) NULL," +
                  "CONSTRAINT [PK_RGPositionen] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[RGPositionen]  WITH CHECK ADD  CONSTRAINT [FK_RGPositionen_RGPositionen] FOREIGN KEY([RGTableID]) " +
                  "REFERENCES [dbo].[Rechnungen] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[RGPositionen] CHECK CONSTRAINT [FK_RGPositionen_RGPositionen] ";
            return sql;
        }

        ///<summary>Update1067() / clsUpdate</summary>
        ///<remarks>Table RGPosArtikel wird aktualisiert</remarks>
        private string Update1067()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[RGPosArtikel](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[RGPosID] [decimal](28, 0) NOT NULL," +
                  "[ArtikelID] [decimal](28, 0) NOT NULL," +
                  "[AbgerechnetVon] [datetime] NOT NULL," +
                  "[AbgerechnetBis] [datetime] NOT NULL," +
                  "CONSTRAINT [PK_RGPosArtikel] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[RGPosArtikel]  WITH CHECK ADD  CONSTRAINT [FK_RGPosArtikel_RGPositionen] FOREIGN KEY([RGPosID]) " +
                  "REFERENCES [dbo].[RGPositionen] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[RGPosArtikel] CHECK CONSTRAINT [FK_RGPosArtikel_RGPositionen] ";
            return sql;
        }

        ///<summary>Update1068() / clsUpdate</summary>
        ///<remarks>Table Rechnungen mit Spalte MandantenID und Arbeitsbereich wird aktualisiert</remarks>
        private string Update1068()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Rechnungen ADD [MandantenID] [Decimal] (28,0) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Rechnungen ADD [ArBereichID] [Decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1069() / clsUpdate</summary>
        ///<remarks>Table DocScan wird um die Spalte AuftragPosTableID erweitert.</remarks>
        private string Update1069()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE DocScan ADD [AuftragPosTableID] [Decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1070() / clsUpdate</summary>
        ///<remarks>Table DispoCheck wird um die Spalte AuftragPosTableID erweitert.</remarks>
        private string Update1070()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE DispoCheck ADD [AuftragPosTableID] [Decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1071() / clsUpdate</summary>
        ///<remarks>Table DocScan wird um die Spalte DocImage erweitert.</remarks>
        private string Update1071()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE DocScan ADD [DocImage] [varbinary] (max) NULL; ";
            return sql;
        }

        ///<summary>Update1072() / clsUpdate</summary>
        ///<remarks>Table ADR wird um die Spalte Dummy erweitert.</remarks>
        private string Update1072()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE ADR ADD [Dummy] [bit]  DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1073() / clsUpdate</summary>
        ///<remarks>Table Userberechtigungen wird erweitert.</remarks>
        private string Update1073()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Userberechtigungen ADD [D_read] [bit]  DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Userberechtigungen ADD [Sy_AB_read] [bit]  DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Userberechtigungen ADD [Sy_AB_write] [bit]  DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Userberechtigungen ADD [Sy_Mandant_read] [bit]  DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Userberechtigungen ADD [Sy_Mandant_write] [bit]  DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1074() / clsUpdate</summary>
        ///<remarks>Neue Table Tour.</remarks>
        private string Update1074()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[Tour](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[KFZ_ZM] [decimal](28, 0) NULL," +
                  "[KFZ_A] [decimal](28, 0) NULL," +
                  "[oldZM] [decimal](28, 0) NULL," +
                  "[PersonalID] [decimal](28, 0) NULL," +
                  "[StartZeit] [datetime] NULL," +
                  "[EndZeit] [datetime] NULL," +
                  "[KontaktInfo] [nvarchar](max) NULL," +
                  "[Date_Add] [datetime] NULL," +
                  "CONSTRAINT [PK_Tour] PRIMARY KEY CLUSTERED ([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] ";

            return sql;
        }

        ///<summary>Update1075() / clsUpdate</summary>
        ///<remarks>Table Kommission</remarks>
        private string Update1075()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Kommission DROP COLUMN [B_ID]; " +
                  "ALTER TABLE Kommission DROP COLUMN [E_ID]; " +
                  "ALTER TABLE Kommission DROP COLUMN [Menge]; " +
                  "ALTER TABLE Kommission DROP COLUMN [KFZ_ZM]; " +
                  "ALTER TABLE Kommission DROP COLUMN [KFZ_A]; " +
                  "ALTER TABLE Kommission DROP COLUMN [Personal]; " +
                  "ALTER TABLE Kommission DROP COLUMN [Date_Add]; " +
                  "ALTER TABLE Kommission DROP COLUMN [DispoSet]; " +
                  "ALTER TABLE Kommission DROP COLUMN [oldZM]; " +
                  "ALTER TABLE Kommission DROP COLUMN [Auftrag]; " +
                  "ALTER TABLE Kommission DROP COLUMN [AuftragPos]; ";
            return sql;
        }

        ///<summary>Update1076() / clsUpdate</summary>
        ///<remarks>Table Kommission</remarks>
        private string Update1076()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Kommission ADD [TourID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Kommission ADD [BeladePos] [INT] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Kommission ADD [EntladePos] [INT] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1077() / clsUpdate</summary>
        ///<remarks>Table Tour Spalten werden gelöscht</remarks>
        private string Update1077()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DispoCheck_OKGewicht]') AND type = 'D')" +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[DispoCheck] DROP CONSTRAINT [DF_DispoCheck_OKGewicht]" +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DispoCheck]') AND type in (N'U'))" +
                  "DROP TABLE [dbo].[DispoCheck] " +
                  "CREATE TABLE [dbo].[DispoCheck](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[TourID] [decimal](28, 0) NULL," +
                  "[oldZM] [decimal](28, 0) NULL," +
                  "[oldStartZeit] [datetime] NULL," +
                  "[oldEndZeit] [datetime] NULL," +
                  "[OKGewicht] [bit] NOT NULL," +
                  "[BackToOldZM] [bit] NULL," +
                  "[NeuDisponiert] [bit] NOT NULL," +
                  "CONSTRAINT [PK_DispoCheck] PRIMARY KEY CLUSTERED ([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY]" +
                  "ALTER TABLE [dbo].[DispoCheck] ADD  CONSTRAINT [DF_DispoCheck_OKGewicht]  DEFAULT ((0)) FOR [OKGewicht] ";
            return sql;
        }

        ///<summary>Update1078() / clsUpdate</summary>
        ///<remarks>Table Kommission ändern</remarks>
        private string Update1078()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Kommissio__TourI__7814D14C]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Kommission] DROP CONSTRAINT [DF__Kommissio__TourI__7814D14C] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Kommissio__Belad__7908F585]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Kommission] DROP CONSTRAINT [DF__Kommissio__Belad__7908F585] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Kommissio__Entla__79FD19BE]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Kommission] DROP CONSTRAINT [DF__Kommissio__Entla__79FD19BE] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Kommission__km__7AF13DF7]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Kommission] DROP CONSTRAINT [DF__Kommission__km__7AF13DF7] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kommission]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[Kommission] " +
                  "CREATE TABLE [dbo].[Kommission](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[PosID] [decimal](28, 0) NULL," +
                  "[B_Zeit] [datetime] NULL," +
                  "[E_Zeit] [datetime] NULL," +
                  "[KontaktInfo] [nvarchar](max) NULL," +
                  "[KontaktDate] [datetime] NULL," +
                  "[Docs] [bit] NOT NULL," +
                  "[FahrerKontakt] [bit] NOT NULL," +
                  "[TourID] [decimal](28, 0) NOT NULL," +
                  "[BeladePos] [int] NOT NULL," +
                  "[EntladePos] [int] NOT NULL," +
                  "[km] [int] NOT NULL," +
                  "CONSTRAINT [PK_Kommission] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Kommission] ADD  DEFAULT ((0)) FOR [TourID] " +
                  "ALTER TABLE [dbo].[Kommission] ADD  DEFAULT ((0)) FOR [BeladePos] " +
                  "ALTER TABLE [dbo].[Kommission] ADD  DEFAULT ((0)) FOR [EntladePos] " +
                  "ALTER TABLE [dbo].[Kommission] ADD  DEFAULT ((0)) FOR [km] ";
            return sql;
        }

        ///<summary>Update1079() / clsUpdate</summary>
        ///<remarks>Neue Table TourKontaktInfo ändern</remarks>
        private string Update1079()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TourKontaktInfo_Kommission]') AND parent_object_id = OBJECT_ID(N'[dbo].[TourKontaktInfo]')) " +
                  "ALTER TABLE [dbo].[TourKontaktInfo] DROP CONSTRAINT [FK_TourKontaktInfo_Kommission] " +
                  "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TourKontaktInfo_Tour]') AND parent_object_id = OBJECT_ID(N'[dbo].[TourKontaktInfo]')) " +
                  "ALTER TABLE [dbo].[TourKontaktInfo] DROP CONSTRAINT [FK_TourKontaktInfo_Tour] " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TourKontaktInfo_TourID]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[TourKontaktInfo] DROP CONSTRAINT [DF_TourKontaktInfo_TourID] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TourKontaktInfo]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[TourKontaktInfo] " +
                  "CREATE TABLE [dbo].[TourKontaktInfo](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[TourID] [decimal](28, 0) NOT NULL," +
                  "[KommissionsID] [decimal](28, 0) NOT NULL," +
                  "[InfoText] [nvarchar](max) NOT NULL," +
                  "[DateAdd] [datetime] NOT NULL," +
                  "CONSTRAINT [PK_TourKontaktInfo] PRIMARY KEY CLUSTERED([ID] ASC)" +
                  "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[TourKontaktInfo]  WITH CHECK ADD  CONSTRAINT [FK_TourKontaktInfo_Kommission] FOREIGN KEY([KommissionsID]) " +
                  "REFERENCES [dbo].[Kommission] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[TourKontaktInfo] CHECK CONSTRAINT [FK_TourKontaktInfo_Kommission] " +
                  "ALTER TABLE [dbo].[TourKontaktInfo]  WITH CHECK ADD  CONSTRAINT [FK_TourKontaktInfo_Tour] FOREIGN KEY([TourID]) " +
                  "REFERENCES [dbo].[Tour] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[TourKontaktInfo] CHECK CONSTRAINT [FK_TourKontaktInfo_Tour] " +
                  "ALTER TABLE [dbo].[TourKontaktInfo] ADD  CONSTRAINT [DF_TourKontaktInfo_TourID]  DEFAULT ((0)) FOR [TourID] ";

            return sql;
        }

        ///<summary>Update1080() / clsUpdate</summary>
        ///<remarks>Table Kommission</remarks>
        private string Update1080()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Tour DROP COLUMN [KontaktInfo]; " +
                  "ALTER TABLE Tour DROP COLUMN [oldZM]; " +
                  "ALTER TABLE Kommission DROP COLUMN [KontaktInfo]; " +
                  "ALTER TABLE Kommission DROP COLUMN [KontaktDate]; " +
                  "ALTER TABLE Kommission DROP COLUMN [FahrerKontakt]; ";
            return sql;
        }

        ///<summary>Update1081() / clsUpdate</summary>
        ///<remarks>Table Kommission</remarks>
        private string Update1081()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Tour ADD [LeerKM] [INT] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1082() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1082()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Tour ADD [KM] [INT] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1083() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1083()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ADD  CONSTRAINT [DF_Artikel_gemGewicht]  DEFAULT ((0)) FOR [gemGewicht] ;";
            return sql;
        }

        ///<summary>Update1084() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1084()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Distance ADD [vLand] [nvarchar] (100) NULL; " +
                  "ALTER TABLE Distance ADD [nLand] [nvarchar] (100) NULL; " +
                  "ALTER TABLE Distance ADD [gMaps] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }///<summary>Update1084() / clsUpdate</summary>

        ///<remarks></remarks>
        private string Update1085()
        {
            string sql = string.Empty;
            sql = "Update Distance SET vLand='Deutschland', nLand='Deutschland'; ";
            return sql;
        }

        ///<summary>Update1086() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1086()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Kommission__km__7AF13DF7]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Kommission] DROP CONSTRAINT [DF__Kommission__km__7AF13DF7] " +
                  "END " +
                  "ALTER TABLE Kommission DROP COLUMN [km]; ";
            return sql;
        }

        ///<summary>Update1087() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1087()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Fahrzeuge DROP COLUMN [Besitzer]; " +
                  "ALTER TABLE Fahrzeuge ADD [MandantenID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1088() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1088()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Fahrzeuge ADD  CONSTRAINT [DF_Fahrzeuge_ZM]  DEFAULT ('F') FOR [ZM]; " +
                  "ALTER TABLE Fahrzeuge ADD  CONSTRAINT [DF_Fahrzeuge_Anhaenger]  DEFAULT ('F') FOR [Anhaenger]; " +
                  "ALTER TABLE Fahrzeuge ADD  CONSTRAINT [DF_Fahrzeuge_Plane]  DEFAULT ('F') FOR [Plane]; " +
                  "ALTER TABLE Fahrzeuge ADD  CONSTRAINT [DF_Fahrzeuge_Sattel]  DEFAULT ('F') FOR [Sattel]; " +
                  "ALTER TABLE Fahrzeuge ADD  CONSTRAINT [DF_Fahrzeuge_Coil]  DEFAULT ('F') FOR [Coil]; " +
                  "ALTER TABLE Fahrzeuge ADD  CONSTRAINT [DF_Fahrzeuge_Besonderheit]  DEFAULT ('') FOR [Besonderheit];";
            return sql;
        }

        ///<summary>Update1089() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1089()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Mandanten ADD [Default_Sped] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Mandanten ADD [Default_Lager] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1090() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1090()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE LEingang ADD [Versender]  [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1091() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1091()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE LEingang ADD [SpedID]  [decimal] (28,0) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE LEingang ADD [KFZ]  [nvarchar](50) DEFAULT ((''))  NOT NULL; ";
            return sql;
        }

        ///<summary>Update1092() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1092()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[Werk](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[Bezeichnung] [nvarchar](50) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NOT NULL," +
                  "CONSTRAINT [PK_Werk] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Werk] ADD  CONSTRAINT [DF_Werk_Bezeichnung]  DEFAULT ('') FOR [Bezeichnung]; " +
                  "ALTER TABLE [dbo].[Werk] ADD  CONSTRAINT [DF_Werk_Beschreibung]  DEFAULT ('') FOR [Beschreibung];";
            return sql;
        }

        ///<summary>Update1093() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1093()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[Halle](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[WerkID] [decimal](28, 0) NOT NULL," +
                  "[Bezeichnung] [nvarchar](50) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NOT NULL," +
                  "CONSTRAINT [PK_Halle] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Halle]  WITH CHECK ADD  CONSTRAINT [FK_Halle_Werk] FOREIGN KEY([WerkID]) " +
                  "REFERENCES [dbo].[Werk] ([ID]) " +
                  "ON UPDATE CASCADE " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[Halle] CHECK CONSTRAINT [FK_Halle_Werk];" +
                  "ALTER TABLE [dbo].[Halle] ADD  CONSTRAINT [DF_Halle_Bezeichnung]  DEFAULT ('') FOR [Bezeichnung];" +
                  "ALTER TABLE [dbo].[Halle] ADD  CONSTRAINT [DF_Halle_Beschreibung]  DEFAULT ('') FOR [Beschreibung];";
            return sql;
        }

        ///<summary>Update1094() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1094()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[Reihe](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[HalleID] [decimal](28, 0) NOT NULL," +
                  "[Bezeichnung] [nvarchar](50) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NOT NULL," +
                  "CONSTRAINT [PK_Reihe] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Reihe]  WITH CHECK ADD  CONSTRAINT [FK_Reihe_Halle] FOREIGN KEY([HalleID]) " +
                  "REFERENCES [dbo].[Halle] ([ID]) " +
                  "ON UPDATE CASCADE " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[Reihe] CHECK CONSTRAINT [FK_Reihe_Halle];";
            return sql;
        }

        ///<summary>Update1095() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1095()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[Platz](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[ReiheID] [decimal](28, 0) NOT NULL," +
                  "[Bezeichnung] [nvarchar](50) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NOT NULL," +
                  "CONSTRAINT [PK_Platz] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Platz]  WITH CHECK ADD  CONSTRAINT [FK_Platz_Reihe] FOREIGN KEY([ReiheID]) " +
                  "REFERENCES [dbo].[Reihe] ([ID]) " +
                  "ON UPDATE CASCADE " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[Platz] CHECK CONSTRAINT [FK_Platz_Reihe];";
            return sql;
        }

        ///<summary>Update1096() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1096()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[Ebene](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[ReiheID] [decimal](28, 0) NOT NULL," +
                  "[OrderID] [int] NOT NULL," +
                  "[Bezeichnung] [nvarchar](50) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NOT NULL," +
                  "CONSTRAINT [PK_Ebene] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Ebene]  WITH CHECK ADD  CONSTRAINT [FK_Ebene_Reihe] FOREIGN KEY([ReiheID]) " +
                  "REFERENCES [dbo].[Reihe] ([ID]) " +
                  "ON UPDATE CASCADE " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[Ebene] CHECK CONSTRAINT [FK_Ebene_Reihe];" +
                  "ALTER TABLE [dbo].[Ebene] ADD  CONSTRAINT [DF_Ebene_OrderID]  DEFAULT ((0)) FOR [OrderID];" +
                  "ALTER TABLE [dbo].[Ebene] ADD  CONSTRAINT [DF_Ebene_Bezeichnung]  DEFAULT ('') FOR [Bezeichnung];" +
                  "ALTER TABLE [dbo].[Ebene] ADD  CONSTRAINT [DF_Ebene_Beschreibung]  DEFAULT ('') FOR [Beschreibung];";

            return sql;
        }

        ///<summary>Update1097() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1097()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Platz_Reihe]') AND parent_object_id = OBJECT_ID(N'[dbo].[Platz]')) " +
                  "ALTER TABLE [dbo].[Platz] DROP CONSTRAINT [FK_Platz_Reihe] " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Platz_GArt]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Platz] DROP CONSTRAINT [DF_Platz_GArt] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Platz_vGewicht]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Platz] DROP CONSTRAINT [DF_Platz_vGewicht] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Platz_bGewicht]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Platz] DROP CONSTRAINT [DF_Platz_bGewicht] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Platz]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[Platz] " +
                  "CREATE TABLE [dbo].[Platz](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[EbeneID] [decimal](28, 0) NOT NULL," +
                  "[Bezeichnung] [nvarchar](50) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NOT NULL," +
                  "[GArt] [decimal](28, 0) NOT NULL," +
                  "[vGewicht] [decimal](6, 0) NOT NULL," +
                  "[bGewicht] [decimal](6, 0) NOT NULL," +
                  "CONSTRAINT [PK_Platz] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Platz]  WITH CHECK ADD  CONSTRAINT [FK_Platz_Reihe] FOREIGN KEY([EbeneID]) " +
                  "REFERENCES [dbo].[Reihe] ([ID]) " +
                  "ON UPDATE CASCADE " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[Platz] CHECK CONSTRAINT [FK_Platz_Reihe]; " +
                  "ALTER TABLE [dbo].[Platz] ADD  CONSTRAINT [DF_Platz_GArt]  DEFAULT ((0)) FOR [GArt]; " +
                  "ALTER TABLE [dbo].[Platz] ADD  CONSTRAINT [DF_Platz_vGewicht]  DEFAULT ((0)) FOR [vGewicht]; " +
                  "ALTER TABLE [dbo].[Platz] ADD  CONSTRAINT [DF_Platz_bGewicht]  DEFAULT ((0)) FOR [bGewicht]; ";
            return sql;
        }

        ///<summary>Update1098() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1098()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Platz_Reihe]') AND parent_object_id = OBJECT_ID(N'[dbo].[Platz]')) " +
                  "ALTER TABLE [dbo].[Platz] DROP CONSTRAINT [FK_Platz_Reihe] " +
                  "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Platz_Ebene]') AND parent_object_id = OBJECT_ID(N'[dbo].[Platz]')) " +
                  "ALTER TABLE [dbo].[Platz] DROP CONSTRAINT [FK_Platz_Ebene] " +
                  "ALTER TABLE [dbo].[Platz]  WITH CHECK ADD  CONSTRAINT [FK_Platz_Ebene] FOREIGN KEY([EbeneID]) " +
                  "REFERENCES [dbo].[Ebene] ([ID]) " +
                  "ON UPDATE CASCADE " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[Platz] CHECK CONSTRAINT [FK_Platz_Ebene] ";

            return sql;
        }

        ///<summary>Update1099() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1099()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Platz ADD [OrderID]  [INT] DEFAULT ((0)) NOT NULL; ";

            return sql;
        }

        ///<summary>Update1100() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1100()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Werk ADD [OrderID]  [INT] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Halle ADD [OrderID]  [INT] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Reihe ADD [OrderID]  [INT] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1101() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1101()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Artikel__Werk__44952D46]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Artikel] DROP CONSTRAINT [DF__Artikel__Werk__44952D46] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Artikel__Halle__4589517F]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Artikel] DROP CONSTRAINT [DF__Artikel__Halle__4589517F] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Artikel__Reihe__467D75B8]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Artikel] DROP CONSTRAINT [DF__Artikel__Reihe__467D75B8] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Artikel__Platz__477199F1]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Artikel] DROP CONSTRAINT [DF__Artikel__Platz__477199F1] " +
                  "END " +
                  "ALTER TABLE Artikel DROP COLUMN [Werk]; " +
                  "ALTER TABLE Artikel DROP COLUMN [Halle]; " +
                  "ALTER TABLE Artikel DROP COLUMN [Reihe]; " +
                  "ALTER TABLE Artikel DROP COLUMN [Platz]; ";
            return sql;
        }

        ///<summary>Update1102() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1102()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Artikel__SPL__69FBBC1F]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Artikel] DROP CONSTRAINT [DF__Artikel__SPL__69FBBC1F] " +
                  "END " +
                  "ALTER TABLE Artikel DROP COLUMN [exOrt]; " +
                  "ALTER TABLE Artikel DROP COLUMN [SPL]; " +
                  "ALTER TABLE Artikel DROP COLUMN [TarifID]; " +
                  "ALTER TABLE Artikel DROP COLUMN [IDvorUB]; " +
                  "ALTER TABLE Artikel ADD [IDvorUB] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1103() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1103()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE LAusgang ADD [Termin] [datetime] NULL; ";
            return sql;
        }

        ///<summary>Update1104() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1104()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ruecklieferung]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[Ruecklieferung] " +
                  "CREATE TABLE [dbo].[Ruecklieferung](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[ArtikelID] [decimal](28, 0) NOT NULL," +
                  "[UserID] [decimal](28, 0) NOT NULL," +
                  "[Datum] [datetime] NOT NULL," +
                  "CONSTRAINT [PK_Ruecklieferung] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Ruecklieferung]  WITH CHECK ADD  CONSTRAINT [FK_Ruecklieferung_Artikel] FOREIGN KEY([ArtikelID]) " +
                  "REFERENCES [dbo].[Artikel] ([ID])" +
                  "ON UPDATE CASCADE " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[Ruecklieferung] CHECK CONSTRAINT [FK_Ruecklieferung_Artikel]; ";
            return sql;
        }

        ///<summary>Update1105() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1105()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Sperrlager_Artikel]') AND parent_object_id = OBJECT_ID(N'[dbo].[Sperrlager]')) " +
                  "ALTER TABLE [dbo].[Sperrlager] DROP CONSTRAINT [FK_Sperrlager_Artikel] " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sperrlager_BKZ]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Sperrlager] DROP CONSTRAINT [DF_Sperrlager_BKZ] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sperrlager]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[Sperrlager] " +
                  "CREATE TABLE [dbo].[Sperrlager](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[ArtikelID] [decimal](28, 0) NOT NULL," +
                  "[UserID] [decimal](28, 0) NOT NULL," +
                  "[Datum] [datetime] NOT NULL," +
                  "[BKZ] [nvarchar](5) NOT NULL ) ON [PRIMARY]" +
                  "ALTER TABLE [dbo].[Sperrlager]  WITH CHECK ADD  CONSTRAINT [FK_Sperrlager_Artikel] FOREIGN KEY([ArtikelID]) " +
                  "REFERENCES [dbo].[Artikel] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[Sperrlager] CHECK CONSTRAINT [FK_Sperrlager_Artikel] " +
                  "ALTER TABLE [dbo].[Sperrlager] ADD  CONSTRAINT [DF_Sperrlager_BKZ]  DEFAULT (N'IN') FOR [BKZ] ";
            return sql;
        }

        ///<summary>Update1106() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1106()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE LEingang ADD [DirectDelivery]  [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE LAusgang ADD [DirectDelivery]  [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1107() / clsUpdate</summary>
        ///<remarks>Table Einheiten</remarks>
        private string Update1107()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[Einheiten](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[Bezeichnung] [varchar](50) NOT NULL," +
                  "CONSTRAINT [PK_Einheiten] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] ";
            return sql;
        }

        ///<summary>Update1108() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1108()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE UserList ADD [UserID] [decimal] (28, 0) DEFAULT ((0)) NOT NULL; " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Artikel__RL__6DCC4D03]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Artikel] DROP CONSTRAINT [DF__Artikel__RL__6DCC4D03] " +
                  "END " +
                  "ALTER TABLE Artikel DROP COLUMN [ReBookSPL]; " +
                  "ALTER TABLE Artikel DROP COLUMN [RL]; ";
            return sql;
        }

        ///<summary>Update1109() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1109()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE UserList ADD [public] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1110() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1110()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ADD [Info] [nvarchar] (max) NULL; " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_GewichtNetto]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_GewichtNetto] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_GewichtBrutto]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_GewichtBrutto] " +
                  "END " +
                  "ALTER TABLE LEingang DROP COLUMN [GewichtNetto]; " +
                  "ALTER TABLE LEingang DROP COLUMN [GewichtBrutto]; ";
            return sql;
        }

        ///<summary>Update1111() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1111()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Sperrlager ADD [SPLIDIn] [decimal] (28, 0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1112() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1112()
        {
            string sql = string.Empty;
            sql = "EXEC sp_rename " +
                  "@objname ='Artikel.IDvorUB', " +
                  "@newname ='ArtIDAlt', " +
                  "@objtype = 'COLUMN'; ";
            return sql;
        }

        ///<summary>Update1113() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1113()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ADD [LOTable] [nvarchar] (20) DEFAULT (('')) NOT NULL; " +
                  "ALTER TABLE Artikel ADD [exLOTable] [nvarchar] (20) DEFAULT (('')) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1114() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1114()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Werk ADD [exLagerOrt] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1115() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1115()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TarifPositionen_Tarife]') AND parent_object_id = OBJECT_ID(N'[dbo].[TarifPositionen]')) " +
                  "ALTER TABLE TarifPositionen DROP CONSTRAINT [FK_TarifPositionen_Tarife] " +
                  "ALTER TABLE TarifPositionen  WITH CHECK ADD  CONSTRAINT [FK_TarifPositionen_Tarife] FOREIGN KEY([TarifID]) " +
                  "REFERENCES [dbo].[Tarife] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE TarifPositionen CHECK CONSTRAINT [FK_TarifPositionen_Tarife]; " +
                  "ALTER TABLE Tarife ADD [GArtID] [decimal] (28, 0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1116() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1116()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_KundenTarife_ADR]') AND parent_object_id = OBJECT_ID(N'[dbo].[KundenTarife]')) " +
                  "ALTER TABLE [dbo].[KundenTarife] DROP CONSTRAINT [FK_KundenTarife_ADR] " +
                  "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_KundenTarife_Tarife]') AND parent_object_id = OBJECT_ID(N'[dbo].[KundenTarife]')) " +
                  "ALTER TABLE [dbo].[KundenTarife] DROP CONSTRAINT [FK_KundenTarife_Tarife] " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_KundenTarife_TarifID]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[KundenTarife] DROP CONSTRAINT [DF_KundenTarife_TarifID] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_KundenTarife_AdrID]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[KundenTarife] DROP CONSTRAINT [DF_KundenTarife_AdrID] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KundenTarife]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[KundenTarife] " +
                  "CREATE TABLE [dbo].[KundenTarife](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[TarifID] [decimal](28, 0) NULL," +
                  "[AdrID] [decimal](28, 0) NULL," +
                  "CONSTRAINT [PK_KundenTarife] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[KundenTarife]  WITH CHECK ADD  CONSTRAINT [FK_KundenTarife_ADR] FOREIGN KEY([AdrID]) " +
                  "REFERENCES [dbo].[ADR] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[KundenTarife] CHECK CONSTRAINT [FK_KundenTarife_ADR] " +
                  "ALTER TABLE [dbo].[KundenTarife]  WITH CHECK ADD  CONSTRAINT [FK_KundenTarife_Tarife] FOREIGN KEY([TarifID]) " +
                  "REFERENCES [dbo].[Tarife] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[KundenTarife] CHECK CONSTRAINT [FK_KundenTarife_Tarife] " +
                  "ALTER TABLE [dbo].[KundenTarife] ADD  CONSTRAINT [DF_KundenTarife_TarifID]  DEFAULT ((0)) FOR [TarifID] " +
                  "ALTER TABLE [dbo].[KundenTarife] ADD  CONSTRAINT [DF_KundenTarife_AdrID]  DEFAULT ((0)) FOR [AdrID] ";
            return sql;
        }

        ///<summary>Update1117() / clsUpdate</summary>
        ///<remarks>Lagerausgang neutraler Auftraggeber und neutraler Empfänger hinzufügen</remarks>
        private string Update1117()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE LAusgang ADD [neutrAuftraggeber] [decimal] (28, 0) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE LAusgang ADD [neutrEmpfaenger] [decimal] (28, 0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1118() / clsUpdate</summary>
        ///<remarks>Lagerausgang neutraler Auftraggeber und neutraler Empfänger hinzufügen</remarks>
        private string Update1118()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE TarifPositionen ADD [aktiv] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1119() / clsUpdate</summary>
        ///<remarks>Teilabrechnungsarten</remarks>
        private string Update1119()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Tarife ADD [CalcEingangskosten] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Tarife ADD [CalcAusgangskosten] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Tarife ADD [CalcLagerkosten] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Tarife ADD [CalcSPLKosten] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Tarife ADD [CalcTransportkosten] [bit] DEFAULT ((0)) NOT NULL; ";

            return sql;
        }

        ///<summary>Update1120() / clsUpdate</summary>
        ///<remarks>Lagerausgang neutraler Auftraggeber und neutraler Empfänger hinzufügen</remarks>
        private string Update1120()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE TarifPositionen ADD [MasterPos] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE TarifPositionen ADD [StaffelPos] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE TarifPositionen ADD [OrderID] [INT] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1120() / clsUpdate</summary>
        ///<remarks>Lagerausgang neutraler Auftraggeber und neutraler Empfänger hinzufügen</remarks>
        private string Update1121()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Artikel__exLOTab__3CBF0154]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Artikel] DROP CONSTRAINT [DF__Artikel__exLOTab__3CBF0154] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Artikel_exLagerOrt]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Artikel] DROP CONSTRAINT [DF_Artikel_exLagerOrt] " +
                  "END " +
                  "ALTER TABLE Artikel DROP COLUMN [exLOTable] ; " +
                  "ALTER TABLE Artikel DROP COLUMN [exLagerOrt] ; " +
                  "ALTER TABLE Artikel ADD [exLagerOrt] [nvarchar] (100) DEFAULT (('')) NOT NULL;  ";
            return sql;
        }

        ///<summary>Update1122() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1122()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Tarife ADD [CalcDirectDeliveryKosten] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Tarife ADD [CalcRLKosten] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1123() / clsUpdate</summary>
        ///<remarks>Gibt an, ob der Lagerbestand incl. oder excl. Sperrbestand berechnet werden soll.</remarks>
        private string Update1123()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Tarife ADD [LagerBestandIncSPL] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1124() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1124()
        {
            string sql = string.Empty;
            sql = "EXEC sp_rename " +
                  "@objname ='RGPositionen.TarifID', " +
                  "@newname ='TarifPosID', " +
                  "@objtype = 'COLUMN'; " +
                  "EXEC sp_rename " +
                  "@objname ='RGPositionen.Tarifbeschreibung', " +
                  "@newname ='Tariftext', " +
                  "@objtype = 'COLUMN'; ";
            return sql;
        }

        ///<summary>Update1125() / clsUpdate</summary>
        ///<remarks>Neuer Spalte DatenfeldAritkel wie Brutto, Netto oder Anzahl</remarks>
        private string Update1125()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE TarifPositionen ADD [DatenfeldArtikel] [nvarchar] (50) DEFAULT (('')) NOT NULL;";
            return sql;
        }

        ///<summary>Update1126() / clsUpdate</summary>
        ///<remarks>Neugestaltung Tablle RGPositionen</remarks>
        private string Update1126()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RGPositionen_RGPositionen]') AND parent_object_id = OBJECT_ID(N'[dbo].[RGPositionen]')) " +
                  "ALTER TABLE [dbo].[RGPositionen] DROP CONSTRAINT [FK_RGPositionen_RGPositionen] " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RGPositionen_Menge]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[RGPositionen] DROP CONSTRAINT [DF_RGPositionen_Menge] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RGPositionen]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[RGPositionen] " +
                  "CREATE TABLE [dbo].[RGPositionen](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[RGTableID] [decimal](28, 0) NOT NULL," +
                  "[Position] [int] NOT NULL," +
                  "[RGText] [varchar](255) NULL," +
                  "[Abrechnungseinheit] [varchar](30) NULL," +
                  "[Menge] [decimal](18, 2) NULL," +
                  "[EinzelPreis] [money] NOT NULL," +
                  "[NettoPeis] [money] NULL," +
                  "[Abrechnungsart] [varchar](50) NULL," +
                  "[TarifPosID] [decimal](28, 0) NOT NULL," +
                  "[Tariftext] [varchar](50) NULL," +
                  "CONSTRAINT [PK_RGPositionen] PRIMARY KEY CLUSTERED ([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[RGPositionen]  WITH CHECK ADD  CONSTRAINT [FK_RGPositionen_RGPositionen] FOREIGN KEY([RGTableID]) " +
                  "REFERENCES [dbo].[Rechnungen] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[RGPositionen] CHECK CONSTRAINT [FK_RGPositionen_RGPositionen] " +
                  "ALTER TABLE [dbo].[RGPositionen] ADD  CONSTRAINT [DF_RGPositionen_Menge]  DEFAULT ((0)) FOR [Menge] ";
            return sql;
        }

        ///<summary>Update1127() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1127()
        {
            string sql = string.Empty;
            sql = "EXEC sp_rename " +
                  "@objname ='RGPositionen.NettoPeis', " +
                  "@newname ='NettoPreis', " +
                  "@objtype = 'COLUMN'; ";
            return sql;
        }

        ///<summary>Update1128() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1128()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TarifPositionen_Tarife]') AND parent_object_id = OBJECT_ID(N'[dbo].[TarifPositionen]')) " +
                  "ALTER TABLE [dbo].[TarifPositionen] DROP CONSTRAINT [FK_TarifPositionen_Tarife] " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TarifPosi__Tarif__4865BE2A]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[TarifPositionen] DROP CONSTRAINT [DF__TarifPosi__Tarif__4865BE2A] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TarifPosi__aktiv__62E4AA3C]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[TarifPositionen] DROP CONSTRAINT [DF__TarifPosi__aktiv__62E4AA3C] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TarifPosi__Maste__689D8392]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[TarifPositionen] DROP CONSTRAINT [DF__TarifPosi__Maste__689D8392] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TarifPosi__Staff__6991A7CB]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[TarifPositionen] DROP CONSTRAINT [DF__TarifPosi__Staff__6991A7CB] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TarifPosi__Order__6A85CC04]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[TarifPositionen] DROP CONSTRAINT [DF__TarifPosi__Order__6A85CC04] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__TarifPosi__Daten__6F4A8121]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[TarifPositionen] DROP CONSTRAINT [DF__TarifPosi__Daten__6F4A8121] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TarifPositionen]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[TarifPositionen] " +
                  "CREATE TABLE [dbo].[TarifPositionen](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[TarifID] [decimal](28, 0) NOT NULL," +
                  "[BasisEinheit] [nvarchar](10) NOT NULL," +
                  "[AbrEinheit] [nvarchar](10) NOT NULL," +
                  "[Lagerdauer] [int] NULL," +
                  "[Zeitraumbezogen] [bit] NOT NULL," +
                  "[PreisEinheit] [money] NOT NULL," +
                  "[EinheitVon] [int] NOT NULL," +
                  "[EinheitBis] [int] NOT NULL," +
                  "[MargeProzentEinheit] [decimal](3, 2) NOT NULL," +
                  "[MargePreisEinheit] [money] NOT NULL," +
                  "[TarifPosArt] [nvarchar](100) NOT NULL," +
                  "[aktiv] [bit] NOT NULL," +
                  "[MasterPos] [bit] NOT NULL," +
                  "[StaffelPos] [bit] NOT NULL," +
                  "[OrderID] [int] NOT NULL," +
                  "[DatenfeldArtikel] [nvarchar](50) NOT NULL," +
                  "CONSTRAINT [PK_TarifPositionen] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[TarifPositionen]  WITH CHECK ADD  CONSTRAINT [FK_TarifPositionen_Tarife] FOREIGN KEY([TarifID]) " +
                  "REFERENCES [dbo].[Tarife] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[TarifPositionen] CHECK CONSTRAINT [FK_TarifPositionen_Tarife] " +
                  "ALTER TABLE [dbo].[TarifPositionen] ADD  CONSTRAINT [DF__TarifPosi__Tarif__4865BE2A]  DEFAULT ((0)) FOR [TarifPosArt] " +
                  "ALTER TABLE [dbo].[TarifPositionen] ADD  CONSTRAINT [DF__TarifPosi__aktiv__62E4AA3C]  DEFAULT ((0)) FOR [aktiv] " +
                  "ALTER TABLE [dbo].[TarifPositionen] ADD  CONSTRAINT [DF__TarifPosi__Maste__689D8392]  DEFAULT ((0)) FOR [MasterPos] " +
                  "ALTER TABLE [dbo].[TarifPositionen] ADD  CONSTRAINT [DF__TarifPosi__Staff__6991A7CB]  DEFAULT ((0)) FOR [StaffelPos] " +
                  "ALTER TABLE [dbo].[TarifPositionen] ADD  CONSTRAINT [DF__TarifPosi__Order__6A85CC04]  DEFAULT ((0)) FOR [OrderID] " +
                  "ALTER TABLE [dbo].[TarifPositionen] ADD  CONSTRAINT [DF__TarifPosi__Daten__6F4A8121]  DEFAULT ('') FOR [DatenfeldArtikel] ";
            return sql;
        }

        ///<summary>Update1129() / clsUpdate</summary>
        ///<remarks>Tarifpositonen neue Spalte Beschreibung</remarks>
        private string Update1129()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE TarifPositionen ADD [Beschreibung] [nvarchar] (max) NULL; ";
            return sql;
        }

        ///<summary>Update1130() / clsUpdate</summary>
        ///<remarks>Tarifpositonen neue Spalte MArgen</remarks>
        private string Update1130()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE RGPositionen ADD [MargeEuro]  [money] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE RGPositionen ADD [MargeProzent]  [decimal] (3, 2) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1131() / clsUpdate</summary>
        ///<remarks>Tarifpositonen neue Spalte MArgen</remarks>
        private string Update1131()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RGPositionen_Rechnungen]') AND parent_object_id = OBJECT_ID(N'[dbo].[RGPositionen]')) " +
                  "ALTER TABLE [dbo].[RGPositionen] DROP CONSTRAINT [FK_RGPositionen_Rechnungen] " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Rechnungen_MwStBetrag]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Rechnungen] DROP CONSTRAINT [DF_Rechnungen_MwStBetrag] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Rechnungen_NettoBetrag]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Rechnungen] DROP CONSTRAINT [DF_Rechnungen_NettoBetrag] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Rechnungen_BruttoBetrag]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Rechnungen] DROP CONSTRAINT [DF_Rechnungen_BruttoBetrag] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Rechnunge__Manda__6501FCD8]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Rechnungen] DROP CONSTRAINT [DF__Rechnunge__Manda__6501FCD8] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Rechnunge__ArBer__65F62111]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Rechnungen] DROP CONSTRAINT [DF__Rechnunge__ArBer__65F62111] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rechnungen]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[Rechnungen] " +
                  "CREATE TABLE [dbo].[Rechnungen](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[RGNr] [decimal](28, 0) NOT NULL," +
                  "[Datum] [datetime] NOT NULL," +
                  "[faellig] [datetime] NOT NULL," +
                  "[MwStSatz] [decimal](6, 2) NULL," +
                  "[MwStBetrag] [money] NULL," +
                  "[NettoBetrag] [money] NULL," +
                  "[BruttoBetrag] [money] NULL," +
                  "[GS] [bit] NOT NULL," +
                  "[Bezahlt] [datetime] NULL," +
                  "[Druck] [bit] NULL," +
                  "[Druckdatum] [datetime] NULL," +
                  "[Benutzer] [int] NULL," +
                  "[RGArt] [varchar](50) NULL," +
                  "[MandantenID] [decimal](28, 0) NOT NULL," +
                  "[ArBereichID] [decimal](28, 0) NOT NULL," +
                  "[exFibu] [bit] NULL," +
                  "CONSTRAINT [PK_Rechnungen] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[Rechnungen] ADD  CONSTRAINT [DF_Rechnungen_MwStBetrag]  DEFAULT ((0)) FOR [MwStBetrag] " +
                  "ALTER TABLE [dbo].[Rechnungen] ADD  CONSTRAINT [DF_Rechnungen_NettoBetrag]  DEFAULT ((0)) FOR [NettoBetrag] " +
                  "ALTER TABLE [dbo].[Rechnungen] ADD  CONSTRAINT [DF_Rechnungen_BruttoBetrag]  DEFAULT ((0)) FOR [BruttoBetrag] " +
                  "ALTER TABLE [dbo].[Rechnungen] ADD  CONSTRAINT [DF__Rechnunge__Manda__6501FCD8]  DEFAULT ((0)) FOR [MandantenID] " +
                  "ALTER TABLE [dbo].[Rechnungen] ADD  CONSTRAINT [DF__Rechnunge__ArBer__65F62111]  DEFAULT ((0)) FOR [ArBereichID] " +
                  "ALTER TABLE [dbo].[RGPositionen]  WITH CHECK ADD  CONSTRAINT [FK_RGPositionen_Rechnungen] FOREIGN KEY([RGTableID]) " +
                  "REFERENCES [dbo].[Rechnungen] ([ID]) " +
                  "ON DELETE CASCADE ";

            return sql;
        }

        ///<summary>Update1132() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1132()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Rechnungen ADD [AbrZeitraumVon] [DateTime]  NULL; " +
                  "ALTER TABLE Rechnungen ADD [AbrZeitraumBis] [DateTime]  NULL; " +
                  "ALTER TABLE Rechnungen ADD [Empfaenger] [decimal] (28, 0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1133() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1133()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Rechnungen ADD [Storno] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1134() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1134()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Rechnungen ADD [Anfangsbestand] [decimal] (28,2) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1135() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1135()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Rechnungen ADD [Auftraggeber] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1136() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1136()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Rechnungen ADD [StornoID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1137() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1137()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE LEingang ADD [Retoure] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE LEingang ADD [Vorfracht] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE LAusgang ADD [LagerTransport] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1138() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1138()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Tarife ADD [CalcVorfracht] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE Tarife ADD [Von] [DateTime] NULL; " +
                  "ALTER TABLE Tarife ADD [Bis] [DateTime] NULL; " +
                  "ALTER TABLE [dbo].[Tarife] DROP CONSTRAINT [DF__Tarife__GArtID__5772F790];" +
                  "ALTER TABLE Tarife Drop Column [GArtID]; ";
            return sql;
        }

        ///<summary>Update1139() / clsUpdate</summary>
        ///<remarks>Datumswerte für Tarife setzte, damit es bei der späteren Verarbeitung keine Probleme gibt.</remarks>
        private string Update1139()
        {
            string sql = string.Empty;
            DateTime dtVon = Convert.ToDateTime("01.01.2013");
            DateTime dtBis = Globals.DefaultDateTimeMaxValue;
            sql = "UPDATE Tarife SET Von='" + dtVon + "', " +
                  "Bis='" + dtBis + "' ;";
            return sql;
        }

        ///<summary>Update1140() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1140()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Tarife ADD [parentID] [decimal] (28,0) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1141() / clsUpdate</summary>
        ///<remarks>Neue Kreuztabelle</remarks>
        private string Update1141()
        {
            string sql = string.Empty;
            sql = "CREATE TABLE [dbo].[TarifGArtZuweisung](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[TarifID] [decimal](28, 0) NOT NULL," +
                  "[GArtID] [decimal](28, 0) NOT NULL," +
                  "CONSTRAINT [PK_TarifGArtZuweisung] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[TarifGArtZuweisung]  WITH CHECK ADD  CONSTRAINT [FK_TarifGArtZuweisung_Gueterart] FOREIGN KEY([GArtID]) " +
                  "REFERENCES [dbo].[Gueterart] ([ID]) " +
                  "ON UPDATE CASCADE " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[TarifGArtZuweisung] CHECK CONSTRAINT [FK_TarifGArtZuweisung_Gueterart] " +
                  "ALTER TABLE [dbo].[TarifGArtZuweisung]  WITH CHECK ADD  CONSTRAINT [FK_TarifGArtZuweisung_Tarife] FOREIGN KEY([TarifID]) " +
                  "REFERENCES [dbo].[Tarife] ([ID]) " +
                  "ON UPDATE CASCADE " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[TarifGArtZuweisung] CHECK CONSTRAINT [FK_TarifGArtZuweisung_Tarife] ";
            return sql;
        }

        ///<summary>Update1142() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1142()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE TarifPositionen ADD [einheitenbezogen] [bit] DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE TarifPositionen ADD [TEinheiten] [nvarchar](20) NULL; " +
                  "ALTER TABLE TarifPositionen ADD [AnzahlTEinheiten] [INT] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1143() / clsUpdate</summary>
        ///<remarks>Flag Pauschal wird hinzugefügt</remarks>
        private string Update1143()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE TarifPositionen ADD [Pauschal] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1144() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1144()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Tarife ADD [ArtEinzelAbrechnung] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1144() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1145()
        {
            string sql = string.Empty;
            sql = "EXEC sp_rename " +
                  "@objname ='Artikel.ME', " +
                  "@newname ='Anzahl', " +
                  "@objtype = 'COLUMN'; ";
            return sql;
        }

        ///<summary>Update1144() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1146()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Artikel ADD [IsLagerArtikel] [bit] DEFAULT ((1)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1147() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1147()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE RGPositionen ADD [Anfangsbestand] [decimal] (18,2) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE RGPositionen ADD [Abgang] [decimal] (18,2) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE RGPositionen ADD [Zugang] [decimal] (18,2) DEFAULT ((0)) NOT NULL; " +
                  "ALTER TABLE RGPositionen ADD [Endbestand] [decimal] (18,2) DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1148() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1148()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE RGPosArtikel ADD [Storno] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1149() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1149()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE LEingang ADD [LagerTransport] [bit] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1150() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1150()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE RGPosArtikel ADD [TransDirection] [nvarchar] (10) NULL; ";
            return sql;
        }

        ///<summary>Update1151() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1151()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE TarifPositionen ADD [SortIndex] [int] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1152() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1152()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE TarifPositionen ADD [TPosVerweis] [int] DEFAULT ((0)) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1153() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1153()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Rechnungen ADD [AbrTarifName]  [nvarchar] (250) DEFAULT (('')) NOT NULL;";
            return sql;
        }

        ///<summary>Update1153() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1154()
        {
            string sql = string.Empty;
            sql = "EXEC sp_rename " +
                  "@objname ='TarifPositionen.AnzahlTEinheiten', " +
                  "@newname ='TEinheitVon', " +
                  "@objtype = 'COLUMN'; " +
                  "ALTER TABLE TarifPositionen ADD [TEinheitBis] [int] DEFAULT ((0)) NOT NULL;";
            return sql;
        }

        ///<summary>Update1153() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1155()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE RGPositionen ADD [RGPosText] [nvarchar] (250) DEFAULT (('')) NOT NULL;";
            return sql;
        }

        ///<summary>Update1156() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1156()
        {
            string sql = string.Empty;
            sql = "Update Kunde SET MwSt='1'; " +
                  "ALTER TABLE Kunde ALTER COLUMN [MwSt] [bit]; ";
            return sql;
        }

        ///<summary>Update1157() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1157()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE Gueterart ADD [Mindestbestand] [decimal] (18,0) DEFAULT (('0')) NOT NULL;" +
                  "ALTER TABLE Gueterart ADD [BestellNr] [varchar] (25) DEFAULT (('')) NOT NULL;" +
                  "ALTER TABLE Gueterart ALTER COLUMN [MEAbsteckBolzen] [INT];" +
                  "ALTER TABLE Gueterart ADD CONSTRAINT DF_Gueterart_MEAbsteckBolzen DEFAULT '0' FOR MEAbsteckBolzen;";
            return sql;
        }

        ///<summary>Update1158() / clsUpdate</summary>
        ///<remarks>neuer Table StyleSheetColumn</remarks>
        private string Update1158()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StyleSheetColumn]') AND type in (N'U')) " +
                  "DROP TABLE [dbo].[StyleSheetColumn] " +
                  "CREATE TABLE [dbo].[StyleSheetColumn](" +
                  "[ID] [decimal](18, 0) IDENTITY(1,1) NOT NULL," +
                  "[Bezeichnung] [varchar](100) NOT NULL," +
                  "[Typ] [varchar](50) NOT NULL," +
                  "[FTable] [varchar](50) NOT NULL," +
                  "[FTableID] [decimal](18, 0) NOT NULL," +
                  "[TableToFormat] [varchar](50) NOT NULL," +
                  "[ColToFormat] [varchar](50) NOT NULL," +
                  "[Length] [int] NOT NULL," +
                  "[CutLength] [bit] NOT NULL," +
                  "[Beschreibung] [varchar](max) NULL," +
                  "CONSTRAINT [PK_StyleSheetColumn] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[StyleSheetColumn] ADD  CONSTRAINT [DF_StyleSheetColumn_Bezeichnung]  DEFAULT ('') FOR [Bezeichnung] " +
                  "ALTER TABLE [dbo].[StyleSheetColumn] ADD  CONSTRAINT [DF_StyleSheetColumn_Typ]  DEFAULT ('') FOR [Typ] " +
                  "ALTER TABLE [dbo].[StyleSheetColumn] ADD  CONSTRAINT [DF_StyleSheetColumn_FTable]  DEFAULT ('') FOR [FTable] " +
                  "ALTER TABLE [dbo].[StyleSheetColumn] ADD  CONSTRAINT [DF_StyleSheetColumn_TableToFormat]  DEFAULT ('') FOR [TableToFormat] " +
                  "ALTER TABLE [dbo].[StyleSheetColumn] ADD  CONSTRAINT [DF_StyleSheetColumn_ColToFormat]  DEFAULT ('') FOR [ColToFormat] " +
                  "ALTER TABLE [dbo].[StyleSheetColumn] ADD  CONSTRAINT [DF_Table_1_ToCut]  DEFAULT ((0)) FOR [CutLength] ";

            return sql;
        }

        ///<summary>Update1159() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1159()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Userberec__D_rea__69C6B1F5]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP CONSTRAINT [DF__Userberec__D_rea__69C6B1F5] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Userberec__Sy_AB__6ABAD62E]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP CONSTRAINT [DF__Userberec__Sy_AB__6ABAD62E] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Userberec__Sy_AB__6BAEFA67]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP CONSTRAINT [DF__Userberec__Sy_AB__6BAEFA67] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Userberec__Sy_Ma__6CA31EA0]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP CONSTRAINT [DF__Userberec__Sy_Ma__6CA31EA0] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Userberec__Sy_Ma__6D9742D9]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP CONSTRAINT [DF__Userberec__Sy_Ma__6D9742D9] " +
                  "END " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [Sy_Mandant_write]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [Sy_Mandant_read]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [Sy_AB_write]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [Sy_AB_read]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [D_read]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [Sy_User_aen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [Sy_User_loe]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [Sy_User_an]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [L_LB_aen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [L_LB_loe]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [L_LB_an]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [FK_RGloe]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [FK_drucken]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [FK_Frachten]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [FK_StatusAen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [FK_Lanzeigen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [D_dispo]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [D_ATSUstorno]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [D_FVSU]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_AT_teil]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_AT_aen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_AT_loe]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_AT_an]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_R_aen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_R_loe]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_R_an]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_GA_aen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_GA_loe]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_GA_an]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_FZ_aen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_FZ_loe]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_FZ_an]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_P_aen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_P_loe]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_P_an]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_KD_aen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_KD_loe]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_KD_an]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_ADR_aen]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_ADR_loe]; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] DROP COLUMN [S_ADR_an]; " +
                  "EXEC sp_rename " +
                  "@objname ='Userberechtigungen.BenutzerID', " +
                  "@newname ='UserID', " +
                  "@objtype = 'COLUMN'; ";

            return sql;
        }

        ///<summary>Update1160() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1160()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_ADR] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_ADR] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Kunde] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_Kunde] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Personal] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_Personal] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_KFZ] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_KFZ] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Gut] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_Gut] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Relation] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_Relation] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Order] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_Order] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_TransportOrder] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_TransportOrder] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_Disposition] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Disposition] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_FaktLager] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_FaktLager] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_FaktSpedition] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_FaktSpedition] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Bestand] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_LagerEingang] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_LagerEingang] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_LagerAusgang] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_LagerAusgang] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_User] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_User] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Arbeitsbereich] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_Arbeitsbereich] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Mandant] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_Mandant] [bit] DEFAULT(0) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1161() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1161()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE [dbo].[Userberechtigungen] ALTER COLUMN [UserID] decimal(28,0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen]  WITH CHECK ADD  CONSTRAINT [FK_Userberechtigungen_User] FOREIGN KEY([UserID]) " +
                  "REFERENCES [dbo].[User] ([ID]) " +
                  "ON UPDATE CASCADE " +
                  "ON DELETE CASCADE ";
            return sql;
        }

        ///<summary>Update1162() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1162()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Statistik] [bit] DEFAULT(0) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1163() / clsUpdate</summary>
        ///<remarks> war doppelt</remarks>
        private string Update1163()
        {
            string sql = string.Empty;
            //sql = "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Statistik] [bit] DEFAULT(0) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1164() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1164()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Einheit] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_Einheit] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_Schaden] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_Schaden] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_LagerOrt] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_LagerOrt] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [read_ASNTransfer] [bit] DEFAULT(0) NOT NULL; " +
                  "ALTER TABLE [dbo].[Userberechtigungen] ADD [write_ASNTransfer] [bit] DEFAULT(0) NOT NULL; ";
            return sql;
        }

        ///<summary>Update1165() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1165()
        {
            string sql = string.Empty;
            sql = //Table UserList
                 "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserList]') AND type in (N'U')) " +
                 "DROP TABLE [dbo].[UserList] " +
                 "CREATE TABLE [dbo].[UserList]( " +
                 "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                 "[Bezeichnung] [nvarchar](100) NULL," +
                 "[UserID] [decimal](28, 0) NOT NULL," +
                 "[erstellt] [datetime] NULL," +
                 "[Action] [nvarchar](100) NULL," +
                 "[public] [bit] NULL, " +
                 "[IsFilter] [bit] NULL," +
                 "CONSTRAINT [PK_UserList] PRIMARY KEY CLUSTERED ([ID] ASC" +
                 ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                 ") ON [PRIMARY] " +

            //Table UserListDaten
                 "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserListDaten_UserList]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserListDaten]')) " +
                 "ALTER TABLE [dbo].[UserListDaten] DROP CONSTRAINT [FK_UserListDaten_UserList] " +
                 "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserListDaten]') AND type in (N'U')) " +
                 "DROP TABLE [dbo].[UserListDaten] " +
                 "CREATE TABLE [dbo].[UserListDaten]( " +
                 "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL, " +
                 "[UserListID] [decimal](28, 0) NULL, " +
                 "[Table] [nvarchar](100) NULL, " +
                 "[Column] [nvarchar](100) NULL, " +
                 "[ColViewName] [nvarchar](100) NULL, " +
                 "[Type] [nvarchar](100) NULL, " +
                 "[Sort] [nvarchar](50) NULL, " +
                 "CONSTRAINT [PK_UserListDaten] PRIMARY KEY CLUSTERED([ID] ASC " +
                 ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                 ") ON [PRIMARY] " +
                 "ALTER TABLE [dbo].[UserListDaten]  WITH CHECK ADD  CONSTRAINT [FK_UserListDaten_UserList] FOREIGN KEY([UserListID]) " +
                 "REFERENCES [dbo].[UserList] ([ID]) " +
                 "ON UPDATE CASCADE " +
                 "ON DELETE CASCADE " +
                 "ALTER TABLE [dbo].[UserListDaten] CHECK CONSTRAINT [FK_UserListDaten_UserList] ";

            return sql;
        }

        ///<summary>Update1166() / clsUpdate</summary>
        ///<remarks>Spalte für WaggonNr dem Lagerein- und Ausgang hinzufügen
        ///         29.01.2014</remarks>
        private string Update1166()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LEingang','WaggonNo') IS NULL " +
                  "BEGIN " +
                  //*Column does not exist or caller does not have permission to view the object*/
                  "ALTER TABLE LEingang ADD [WaggonNo] [nvarchar] (20) NULL;" +
                  "END " +
                  "IF COL_LENGTH('LAusgang','WaggonNo') IS NULL " +
                  "BEGIN " +
                  //*Column does not exist or caller does not have permission to view the object*/
                  "ALTER TABLE LAusgang ADD [WaggonNo] [nvarchar] (20) NULL;" +
                  "END ";
            return sql;
        }

        ///<summary>Update1167() / clsUpdate</summary>
        ///<remarks>Datenfelder ADR 21.02.2014</remarks>
        private string Update1167()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADR','LKZ') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [LKZ] [nvarchar] (10) NULL; " +
                  "END " +
                  "IF COL_LENGTH('ADR','UserInfoTxt') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [UserInfoTxt] [nvarchar] (max) NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','activ') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [activ] [bit] DEFAULT((1)) NOT NULL;" +
                  "END ";
            return sql;
        }

        ///<summary>Update1168() / clsUpdate</summary>
        ///<remarks>Datenfelder löschen ADR 21.02.2014</remarks>
        private string Update1168()
        {
            string sql = string.Empty;
            sql = "ALTER TABLE ADR DROP COLUMN [A]; " +
                  "ALTER TABLE ADR DROP COLUMN [V]; " +
                  "ALTER TABLE ADR DROP COLUMN [E]; ";
            return sql;
        }

        ///<summary>Update1169() / clsUpdate</summary>
        ///<remarks>Neue Table DebitorDefaultNo 21.02.2014</remarks>
        private string Update1169()
        {
            string sql = string.Empty;
            sql = "IF NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DebitorDefaultNo]') AND type in (N'U')) " +
                  "CREATE TABLE [dbo].[DebitorDefaultNo](" +
                  "[Key] [nvarchar](5) NOT NULL," +
                  "[Value] [int] NOT NULL) ON [PRIMARY] ";
            return sql;
        }

        ///<summary>Update1170() / clsUpdate</summary>
        ///<remarks>25.02.2014</remarks>
        private string Update1170()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Kunde','ZZ') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE Kunde ADD [ZZ] [INT] DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Kunde','KD_IDman') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE Kunde ADD [KD_IDman] [decimal] (28,0) DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Kunde','SalesTaxKeyDebitor') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE Kunde ADD [SalesTaxKeyDebitor] [INT] DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Kunde','SalesTaxKeyKreditor') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE Kunde ADD [SalesTaxKeyKreditor] [INT] DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('ADR','Lagernummer') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [Lagernummer] [decimal] (28,0) DEFAULT((0)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','ASNCom') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [ASNCom] [bit] DEFAULT((0)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','AdrID_Be') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [AdrID_Be]  [decimal] (28,0) DEFAULT((0)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','AdrID_Ent') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [AdrID_Ent]  [decimal] (28,0) DEFAULT((0)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','AdrID_Post') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [AdrID_Post]  [decimal] (28,0) DEFAULT((0)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','AdrID_RG') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [AdrID_RG]  [decimal] (28,0) DEFAULT((0)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','IsAuftraggeber') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [IsAuftraggeber] [bit] DEFAULT((1)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','IsVersender') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [IsVersender] [bit] DEFAULT((1)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','IsBelade') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [IsBelade] [bit] DEFAULT((1)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','IsEmpfaenger') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [IsEmpfaenger] [bit] DEFAULT((1)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','IsEntlade') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [IsEntlade] [bit] DEFAULT((1)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','IsPost') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [IsPost] [bit] DEFAULT((1)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('ADR','IsRG') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [IsRG] [bit] DEFAULT((1)) NOT NULL;" +
                  "END " +
                  "IF COL_LENGTH('Artikel','ADRLagerNr') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE Artikel ADD [ADRLagerNr] [decimal] (28,0) DEFAULT((0)) NOT NULL;" +
                  "END ";
            return sql;
        }

        ///<summary>Update1171() / clsUpdate</summary>
        ///<remarks>Mailinglist und MailingListAssignment 11.03.2014</remarks>
        private string Update1171()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MailingList]') AND type in (N'U')) " +
                  "BEGIN " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MailingList_AdrID]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[MailingList] DROP CONSTRAINT [DF_MailingList_AdrID] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MailingList_Benutzer]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[MailingList] DROP CONSTRAINT [DF_MailingList_Benutzer] " +
                  "END " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MailingList_Arbeitsbereich]') AND type = 'D') " +
                  "BEGIN " +
                  "ALTER TABLE [dbo].[MailingList] DROP CONSTRAINT [DF_MailingList_Arbeitsbereich] " +
                  "END " +
                  "CREATE TABLE [dbo].[MailingList](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[Bezeichnung] [nvarchar](50) NOT NULL," +
                  "[AdrID] [decimal](28, 0) NOT NULL," +
                  "[erstellt] [datetime] NOT NULL," +
                  "[Benutzer] [decimal](28, 0) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NULL," +
                  "[Arbeitsbereich] [decimal](28, 0) NOT NULL," +
                  "CONSTRAINT [PK_MailingList] PRIMARY KEY CLUSTERED ([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[MailingList] ADD  CONSTRAINT [DF_MailingList_AdrID]  DEFAULT ((0)) FOR [AdrID] " +
                  "ALTER TABLE [dbo].[MailingList] ADD  CONSTRAINT [DF_MailingList_Benutzer]  DEFAULT ((0)) FOR [Benutzer] " +
                  "ALTER TABLE [dbo].[MailingList] ADD  CONSTRAINT [DF_MailingList_Arbeitsbereich]  DEFAULT ((0)) FOR [Arbeitsbereich] " +
                  "END; " +
                  "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MailingListAssignment]') AND type in (N'U')) " +
                  "BEGIN " +
                  "CREATE TABLE [dbo].[MailingListAssignment](" +
                  "[MailingListID] [decimal](28, 0) NOT NULL," +
                  "[KontaktID] [decimal](28, 0) NOT NULL" +
                  ") ON [PRIMARY] " +
                  "ALTER TABLE [dbo].[MailingListAssignment]  WITH CHECK ADD  CONSTRAINT [FK_MailingListAssignment_MailingList] FOREIGN KEY([MailingListID]) " +
                  "REFERENCES [dbo].[MailingList] ([ID]) " +
                  "ON DELETE CASCADE " +
                  "ALTER TABLE [dbo].[MailingListAssignment] CHECK CONSTRAINT [FK_MailingListAssignment_MailingList] " +
                  "END; ";

            return sql;
        }

        ///<summary>Update1172() / clsUpdate</summary>
        ///<remarks>Table Kontakte anpassen 11.03.2014
        ///         - Spalte Ansprechpartner wird umbenannt in Nachname
        ///         - Neue Spalten
        ///             -> Vorname
        ///             -> Mobilnummer
        ///             -> Geburtstag</remarks>
        private string Update1172()
        {
            string sql = string.Empty;
            sql = //Kontakte
                 "IF COL_LENGTH('Kontakte','Ansprechpartner') IS NOT NULL " +
                 "BEGIN " +
                 "EXEC sp_rename " +
                 "@objname ='Kontakte.Ansprechpartner', " +
                 "@newname ='Nachname', " +
                 "@objtype = 'COLUMN'; " +
                 "END " +
                 "IF COL_LENGTH('Kontakte','Vorname') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Kontakte ADD [Vorname] [nvarchar] (30) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Kontakte','Anrede') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Kontakte ADD [Anrede] [nvarchar] (20) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Kontakte','Mobil') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Kontakte ADD [Mobil] [nvarchar] (30) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Kontakte','Birthday') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Kontakte ADD [Birthday] [datetime] NULL;" +
                 "END " +
                 //User
                 "IF COL_LENGTH('[User]','SMTPUser') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE [User] ADD [SMTPUser] [nvarchar] (50) NULL;" +
                 "END " +
                 "IF COL_LENGTH('[User]','SMTPPass') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE [User] ADD [SMTPPass] [nvarchar] (50) NULL;" +
                 "END " +
                 "IF COL_LENGTH('[User]','SMTPServer') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE [User] ADD [SMTPServer] [nvarchar] (250) NULL;" +
                 "END " +
                 "IF COL_LENGTH('[User]','SMTPPort') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE [User] ADD [SMTPPort] [INT] NULL;" +
                 "END ";
            return sql;
        }

        ///<summary>Update1173() / clsUpdate</summary>
        ///<remarks>Neuer Table ADRCategory 20.03.2014</remarks>
        private string Update1173()
        {
            string sql = string.Empty;
            sql = "IF NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADRCategory]') AND type in (N'U')) " +
                  "CREATE TABLE [dbo].[ADRCategory](" +
                  "[AdrID] [decimal](18, 0) NOT NULL," +
                  "[Bezeichnung] [varchar](100) NOT NULL) ON [PRIMARY]; ";
            return sql;
        }

        ///<summary>Update1174() / clsUpdate</summary>
        ///<remarks>Neuer Table ExtraCharge 21.03.2014</remarks>
        private string Update1174()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExtraCharge]') AND type in (N'U')) " +
                  "CREATE TABLE [dbo].[ExtraCharge](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[Bezeichnung] [nvarchar](50) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NULL," +
                  "[IsGlobal] [bit] NOT NULL," +
                  "[erstellt] [datetime] NOT NULL," +
                  "[RGText] [nvarchar](max) NULL, " +
                  "[ArbeitsbereichID] [decimal](28, 0) NULL," +
                  "	[Einheit] [nvarchar](50) NULL," +
                  "[Preis] [money] NULL," +
                  "[UserID] [decimal](28, 0) NULL," +
                  "CONSTRAINT [PK_ExtraCharge] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                  ") ON [PRIMARY] ";
            return sql;
        }

        ///<summary>Update1175() / clsUpdate</summary>
        ///<remarks>Neuer Table ExtraChargeAssignment 25.03.2014</remarks>
        private string Update1175()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExtraChargeAssignment]') AND type in (N'U')) " +
                  "CREATE TABLE [dbo].[ExtraChargeAssignment](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[ExtraChargeID] [decimal](28, 0) NOT NULL," +
                  "[ArtikelID] [decimal](28, 0) NOT NULL," +
                  "[Datum] [datetime] NOT NULL," +
                  "[Einheit] [nvarchar](50) NOT NULL," +
                  "[Preis] [money] NOT NULL," +
                  "[Menge] [int] NOT NULL," +
                  "[RGText] [nvarchar](max) NULL," +
                  "CONSTRAINT [PK_ExtraChargeAssignment] PRIMARY KEY CLUSTERED ([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                  ") ON [PRIMARY] ";
            return sql;
        }

        ///<summary>Update1176() / clsUpdate</summary>
        ///<remarks>Erweiterung der Berechtigungen für Sonderkosten 26.03.2014</remarks>
        private string Update1176()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Userberechtigungen','read_FaktExtraCharge') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE Userberechtigungen ADD [read_FaktExtraCharge] [bit]  DEFAULT ((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Userberechtigungen','write_FaktExtraCharge') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE Userberechtigungen ADD [write_FaktExtraCharge] [bit]  DEFAULT ((0)) NOT NULL; " +
                  "END ";

            return sql;
        }

        ///<summary>Update1177() / clsUpdate</summary>
        ///<remarks>Neue Table für Fibu-Kontenplan 26.03.2014</remarks>
        private string Update1177()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TableOfAccount]') AND type in (N'U')) " +
                  "CREATE TABLE [dbo].[TableOfAccount](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[KontoNr] [int] NOT NULL," +
                  "[KontoText] [nvarchar](100) NOT NULL," +
                  "[Beschreibung] [nvarchar](max) NOT NULL," +
                  "[Bis] [datetime] NOT NULL," +
                  "CONSTRAINT [PK_TableOfAccount] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                  ") ON [PRIMARY] ";

            return sql;
        }

        ///<summary>Update1178() / clsUpdate</summary>
        ///<remarks>Table Gueterarten anpassen 27.03.2014
        ///         - Neue Spalten
        ///             -> Einheit
        ///             -> MandantenID
        ///             -> Zusatz </remarks>
        private string Update1178()
        {
            string sql = string.Empty;
            sql = //Güter
                 "IF COL_LENGTH('Gueterart','Zusatz') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Gueterart ADD [Zusatz] [nvarchar] (100) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Gueterart','Einheit') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Gueterart ADD [Einheit] [nvarchar] (20) NULL;" +
                 "END " +
                 //Artikel
                 "IF COL_LENGTH('Artikel','FreigabeAbruf') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [FreigabeAbruf] [bit] DEFAULT((0)) NOT NULL;" +
                 "END " +
                 "IF COL_LENGTH('Artikel','LZZ') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [LZZ] [DateTime] NULL;" +
                 "END " +
                 //Constrains löschen falls vorhanden
                 "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ADR__IsAuftragge__2D12A970]') AND type = 'D') " +
                 "BEGIN " +
                 "ALTER TABLE [dbo].[ADR] DROP CONSTRAINT [DF__ADR__IsAuftragge__2D12A970] " +
                 "END " +
                 "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ADR__IsVersender__2E06CDA9]') AND type = 'D') " +
                 "BEGIN " +
                 "ALTER TABLE [dbo].[ADR] DROP CONSTRAINT [DF__ADR__IsVersender__2E06CDA9] " +
                 "END " +
                 "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ADR__IsBelade__2EFAF1E2]') AND type = 'D') " +
                 "BEGIN " +
                 "ALTER TABLE [dbo].[ADR] DROP CONSTRAINT [DF__ADR__IsBelade__2EFAF1E2] " +
                 "END " +
                 "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ADR__IsEmpfaenge__2FEF161B]') AND type = 'D') " +
                 "BEGIN " +
                 "ALTER TABLE [dbo].[ADR] DROP CONSTRAINT [DF__ADR__IsEmpfaenge__2FEF161B] " +
                 "END " +
                 "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ADR__IsEntlade__30E33A54]') AND type = 'D') " +
                 "BEGIN " +
                 "ALTER TABLE [dbo].[ADR] DROP CONSTRAINT [DF__ADR__IsEntlade__30E33A54] " +
                 "END " +
                 "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ADR__IsPost__31D75E8D]') AND type = 'D') " +
                 "BEGIN " +
                 "ALTER TABLE [dbo].[ADR] DROP CONSTRAINT [DF__ADR__IsPost__31D75E8D] " +
                 "END " +
                 "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ADR__IsRG__32CB82C6]') AND type = 'D') " +
                 "BEGIN " +
                 "ALTER TABLE [dbo].[ADR] DROP CONSTRAINT [DF__ADR__IsRG__32CB82C6] " +
                 "END " +
                 //Gueterarten
                 "IF COL_LENGTH('Gueterart','Verweis') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Gueterart ADD [Verweis] [nvarchar](255) NULL " +
                 "END " +
                 //ADR
                 "IF COL_LENGTH('ADR','IsAuftraggeber') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsAuftraggeber] [bit] DEFAULT((1)) NOT NULL;" +
                 "END " +
                 "IF COL_LENGTH('ADR','IsVersender') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsVersender] [bit] DEFAULT((1)) NOT NULL;" +
                 "END " +
                 "IF COL_LENGTH('ADR','IsBelade') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsBelade] [bit] DEFAULT((1)) NOT NULL;" +
                 "END " +
                 "IF COL_LENGTH('ADR','IsEmpfaenger') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsEmpfaenger] [bit] DEFAULT((1)) NOT NULL;" +
                 "END " +
                 "IF COL_LENGTH('ADR','IsEntlade') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsEntlade] [bit] DEFAULT((1)) NOT NULL;" +
                 "END " +
                 "IF COL_LENGTH('ADR','IsPost') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsPost] [bit] DEFAULT((1)) NOT NULL;" +
                 "END " +
                 "IF COL_LENGTH('ADR','IsRG') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsRG] [bit] DEFAULT((1)) NOT NULL;" +
                 "END " +
                 //Artikel
                 "IF COL_LENGTH('Artikel','ADRLagerNr') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [ADRLagerNr] [decimal] (28,0) DEFAULT((0)) NOT NULL;" +
                 "END ";

            return sql;
        }

        ///<summary>Update1179() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1179()
        {
            string sql = string.Empty;
            sql =
                 //ADR
                 "IF COL_LENGTH('ADR','IsAuftraggeber') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsAuftraggeber] [bit] DEFAULT((1)) NOT NULL; " +
                 "END " +
                 "IF COL_LENGTH('ADR','IsVersender') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsVersender] [bit] DEFAULT((1)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('ADR','IsBelade') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsBelade] [bit] DEFAULT((1)) NOT NULL  ; " +
                 "END " +
                 "IF COL_LENGTH('ADR','IsEmpfaenger') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsEmpfaenger] [bit] DEFAULT((1)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('ADR','IsEntlade') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsEntlade] [bit] DEFAULT((1)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('ADR','IsPost') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsPost] [bit] DEFAULT((1)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('ADR','IsRG') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [IsRG] [bit] DEFAULT((1)) NOT NULL  ;" +
                 "END " +
                 "IF COL_LENGTH('ADR','CalcLagerVers') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [CalcLagerVers] [bit] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('ADR','DocEinlagerAnzeige') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [DocEinlagerAnzeige]  [nvarchar] (200) NULL ; " +
                 "END " +
                 "IF COL_LENGTH('ADR','DocAuslagerAnzeige') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [DocAuslagerAnzeige]  [nvarchar] (200) NULL ; " +
                 "END ";
            return sql;
        }

        ///<summary>Update1180() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1180()
        {
            string sql = string.Empty;
            sql =
                 "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ADRText_ADR]') AND parent_object_id = OBJECT_ID(N'[dbo].[ADRText]')) " +
                 "ALTER TABLE [dbo].[ADRText] DROP CONSTRAINT [FK_ADRText_ADR] " +
                 "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ADRText_AdrID]') AND type = 'D') " +
                 "BEGIN " +
                 "ALTER TABLE [dbo].[ADRText] DROP CONSTRAINT [DF_ADRText_AdrID] " +
                 "END " +
                 "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ADRText_DocumentArtID]') AND type = 'D') " +
                 "BEGIN " +
                 "ALTER TABLE [dbo].[ADRText] DROP CONSTRAINT [DF_ADRText_DocumentArtID] " +
                 "END " +
                 "IF NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADRText]') AND type in (N'U')) " +
                 "CREATE TABLE [dbo].[ADRText](" +
                 "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                 "[AdrID] [decimal](28, 0) NOT NULL," +
                 "[DocumentArtID] [decimal](28, 0) NOT NULL," +
                 "[DocumentArtName] [nvarchar](100) NULL," +
                 "[Text] [nvarchar](max) NULL," +
                 "CONSTRAINT [PK_ADRText] PRIMARY KEY CLUSTERED([ID] ASC" +
                 ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                 ") ON [PRIMARY] " +
                 "ALTER TABLE [dbo].[ADRText]  WITH CHECK ADD  CONSTRAINT [FK_ADRText_ADR] FOREIGN KEY([AdrID]) " +
                 "REFERENCES [dbo].[ADR] ([ID]) " +
                 "ON DELETE CASCADE " +
                 "ALTER TABLE [dbo].[ADRText] CHECK CONSTRAINT [FK_ADRText_ADR] " +
                 "ALTER TABLE [dbo].[ADRText] ADD  CONSTRAINT [DF_ADRText_AdrID]  DEFAULT ((0)) FOR [AdrID] " +
                 "ALTER TABLE [dbo].[ADRText] ADD  CONSTRAINT [DF_ADRText_DocumentArtID]  DEFAULT ((0)) FOR [DocumentArtID] ";

            return sql;
        }

        ///<summary>Update1181() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1181()
        {
            string sql = string.Empty;

            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADRMan]') AND type in (N'U')) " +
                  "CREATE TABLE [dbo].[ADRMan](" +
                  "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                  "[TableName] [varchar](100) NULL," +
                  "[TableID] [decimal](28, 0) NULL," +
                  "[FBez] [nvarchar](50) NULL," +
                  "[Name1] [nvarchar](100) NULL," +
                  "[Name2] [nvarchar](100) NULL," +
                  "[Name3] [nvarchar](100) NULL," +
                  "[Strasse] [nvarchar](100) NULL," +
                  "[HausNr] [nvarchar](10) NULL," +
                  "[PLZ] [nvarchar](12) NULL," +
                  "[Ort] [nvarchar](50) NULL," +
                  "[LKZ] [nvarchar](5) NULL," +
                  "[Land] [nvarchar](100) NULL," +
                  "[AdrArtID] [int] NULL," +
                  "CONSTRAINT [PK_ADRMan] PRIMARY KEY CLUSTERED([ID] ASC" +
                  ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                  ") ON [PRIMARY] ";
            return sql;
        }

        ///<summary>Update1182() / clsUpdate</summary>
        ///<remarks>Table Artikel anpassen 10.04.2014; Erweiterung der Lagerortteile wie
        ///         - Neue Spalten
        ///             -> Werk
        ///             -> Halle
        ///             -> Reihe
        ///             -> Ebene
        ///             -> Platz
        ///            für die manuelle Eingabe des Lagerorts</remarks>
        private string Update1182()
        {
            string sql = string.Empty;
            sql =
                 //Artikel
                 "IF COL_LENGTH('Artikel','Werk') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [Werk] [nvarchar] (30) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Artikel','Halle') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [Halle] [nvarchar] (30) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Artikel','Reihe') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [Reihe] [nvarchar] (30) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Artikel','Ebene') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [Ebene] [nvarchar] (30) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Artikel','Platz') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [Platz] [nvarchar] (30) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Artikel','exAuftrag') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [exAuftrag] [nvarchar] (30) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Artikel','exAuftragPos') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [exAuftragPos] [nvarchar] (30) NULL;" +
                 "END " +
                 "IF COL_LENGTH('Artikel','ASNVerbraucher') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [ASNVerbraucher] [nvarchar] (200) NULL;" +
                 "END ";
            return sql;
        }

        ///<summary>Update1183() / clsUpdate</summary>
        ///<remarks>Table Eingang und Ausgang anpassen.
        ///         10.04.2014</remarks>
        private string Update1183()
        {
            string sql = string.Empty;
            sql =
                 //Eingang
                 "IF COL_LENGTH('LEingang','BeladeID') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE LEingang ADD [BeladeID] [decimal] (28,0) DEFAULT((0)) NOT NULL;" +
                 "END " +
                 "IF COL_LENGTH('LEingang','EntladeID') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE LEingang ADD [EntladeID] [decimal] (28,0) DEFAULT((0)) NOT NULL;" +
                 "END " +
                 //Ausgang
                 "IF COL_LENGTH('LAusgang','BeladeID') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE LAusgang ADD [BeladeID] [decimal] (28,0) DEFAULT((0)) NOT NULL;" +
                 "END ";
            return sql;
        }

        ///<summary>Update1183() / clsUpdate</summary>
        ///<remarks>Table Eingang und Ausgang anpassen.
        ///         10.04.2014</remarks>
        private string Update1184()
        {
            string sql = string.Empty;
            sql =
                 //ADR
                 "IF COL_LENGTH('ADR','Verweis') IS NULL " +
                 "BEGIN " +
                    "ALTER TABLE ADR ADD [Verweis] [nvarchar] (50) NULL;" +
                 "END " +
                 //Artikel
                 //"ALTER TABLE Artikel DROP COLUMN [ASNVerbraucher]; " +
                 "IF COL_LENGTH('Artikel','ASNVerbraucher') IS NOT NULL " +
                 "BEGIN " +
                    "ALTER TABLE Artikel DROP COLUMN [ASNVerbraucher]; " +
                 "END " +
                 "IF COL_LENGTH('Artikel','ASNVerbraucher') IS NULL " +
                 "BEGIN " +
                    "ALTER TABLE Artikel ADD [ASNVerbraucher] [nvarchar] (max) NULL;" +
                 "END ";
            return sql;
        }

        ///<summary>Update1183() / clsUpdate</summary>
        ///<remarks>Table Eingang und Ausgang anpassen.
        ///         10.04.2014</remarks>
        private string Update1185()
        {
            string sql = string.Empty;
            sql =
                 //Artikel
                 "IF COL_LENGTH('Artikel','UB_AltCalcEinlagerung') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [UB_AltCalcEinlagerung] [bit] DEFAULT((1)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('Artikel','UB_AltCalcAuslagerung') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [UB_AltCalcAuslagerung] [bit] DEFAULT((1)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('Artikel','UB_AltCalcLagergeld') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [UB_AltCalcLagergeld] [bit] DEFAULT((1)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('Artikel','UB_NeuCalcEinlagerung') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [UB_NeuCalcEinlagerung] [bit] DEFAULT((1)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('Artikel','UB_NeuCalcAuslagerung') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [UB_NeuCalcAuslagerung] [bit] DEFAULT((1)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('Artikel','UB_NeuCalcLagergeld') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [UB_NeuCalcLagergeld] [bit] DEFAULT((1)) NOT NULL ; " +
                 "END " +
                 //Tarif
                 "IF COL_LENGTH('Tarife','CalcUBExKosten') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Tarife ADD [CalcUBExKosten] [bit] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('Tarife','Modus') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Tarife ADD [Modus] [Int] DEFAULT((1)) NOT NULL ; " +
                 "END ";
            return sql;
        }

        ///<summary>Update1186() / clsUpdate</summary>
        ///<remarks>Table RGPosArtikel anpassen.
        ///         </remarks>
        private string Update1186()
        {
            string sql = string.Empty;
            sql =

            //Artikel
                 "IF COL_LENGTH('RGPosArtikel','Menge') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE RGPosArtikel ADD [Menge] [decimal] (18, 2) DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('RGPosArtikel','Preis') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE RGPosArtikel ADD [Preis] [money] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('RGPosArtikel','Dauer') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE RGPosArtikel ADD [Dauer] [int] DEFAULT((0)) NOT NULL ; " +
                 "END ";
            return sql;
        }

        ///<summary>Update1187() / clsUpdate</summary>
        ///<remarks>Table RGPosArtikel anpassen</remarks>
        private string Update1187()
        {
            string sql = string.Empty;
            sql =
                 //Artikle
                 "IF COL_LENGTH('Artikel','IsVerpackt') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [IsVerpackt] [bit] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 //Tarife
                 "IF COL_LENGTH('Tarife','VersPreis') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Tarife ADD [VersPreis] [money] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 //Rechnungen
                 "IF COL_LENGTH('Rechnungen','VersPraemie') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Rechnungen ADD [VersPraemie] [money] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 //RGPosArtikel
                 "IF COL_LENGTH('RGPosArtikel','Kosten') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE RGPosArtikel ADD [Kosten] [money] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('RGPosArtikel','TarifPosID') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE RGPosArtikel ADD [TarifPosID] [decimal] (28,0) DEFAULT((0)) NOT NULL ; " +
                 "END ";

            return sql;
        }

        ///<summary>Update1188() / clsUpdate</summary>
        ///<remarks>Table RGPosArtikel anpassen</remarks>
        private string Update1188()
        {
            string sql = string.Empty;
            sql =
                 //Artikle
                 "IF COL_LENGTH('Artikel','IsVerpackt') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [IsVerpackt] [bit] DEFAULT((0)) NOT NULL ; " +
                 "END ";

            return sql;
        }

        ///<summary>Update1188() / clsUpdate</summary>
        ///<remarks>Table RGPosArtikel anpassen</remarks>
        private string Update1189()
        {
            string sql = string.Empty;
            sql =
                 //Artikel
                 "IF COL_LENGTH('Artikel','intInfo') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [intInfo] [nvarchar] (max) NULL ; " +
                 "END " +
                 "IF COL_LENGTH('Artikel','exInfo') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE Artikel ADD [exInfo] [nvarchar] (max) NULL ; " +
                 "END ";

            return sql;
        }

        ///<summary>Update1188() / clsUpdate</summary> 
        ///<remarks>Table LEingang und LAusgang Zusatzfelder:
        ///         - PrintDoc
        ///         - PrintLfs
        ///         jeweils
        ///         </remarks>
        private string Update1190()
        {
            string sql = string.Empty;
            sql =
                 //Eingang
                 "IF COL_LENGTH('LEingang','IsPrintDoc') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE LEingang ADD [IsPrintDoc] [bit] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('LEingang','IsPrintAnzeige') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE LEingang ADD [IsPrintAnzeige] [bit] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('LEingang','IsPrintLfs') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE LEingang ADD [IsPrintLfs] [bit] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 //Ausgang
                 "IF COL_LENGTH('LAusgang','IsPrintDoc') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE LAusgang ADD [IsPrintDoc] [bit] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('LAusgang','IsPrintAnzeige') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE LAusgang ADD [IsPrintAnzeige] [bit] DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('LAusgang','IsPrintLfs') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE LAusgang ADD [IsPrintLfs] [bit] DEFAULT((0)) NOT NULL ; " +
                 "END ";
            return sql;
        }

        ///<summary>Update1188() / clsUpdate</summary> 
        ///<remarks>Table LEingang und LAusgang Zusatzfelder:
        ///         - PrintDoc
        ///         - PrintLfs
        ///         jeweils
        ///         </remarks>
        private string Update1191()
        {
            string sql = string.Empty;
            sql =
                 //ADR
                 "IF COL_LENGTH('ADR','PostRGBy') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [PostRGBy] [decimal] (28, 0) DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('ADR','PostAnlageBy') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [PostAnlageBy] [decimal] (28, 0) DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('ADR','PostLfsBy') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [PostLfsBy] [decimal] (28, 0) DEFAULT((0)) NOT NULL ; " +
                 "END " +
                 "IF COL_LENGTH('ADR','PostListBy') IS NULL " +
                 "BEGIN " +
                 "ALTER TABLE ADR ADD [PostListBy] [decimal] (28, 0) DEFAULT((0)) NOT NULL ; " +
                 "END ";
            return sql;
        }
        ///<summary>Update1192() / clsUpdate</summary>
        ///<remarks>Table ADR anpassen</remarks>
        private string Update1192()
        {
            string sql = string.Empty;
            sql =
                // ADR 
                "IF COL_LENGTH('ADR','IsDiv') IS NULL " +
                "BEGIN " +
                "ALTER TABLE [ADR] ADD [IsDiv] [bit] DEFAULT((0)) NOT NULL ; " +
                "END " +
                "IF COL_LENGTH('ADR','IsSpedition') IS NULL " +
                "BEGIN " +
                "ALTER TABLE [ADR] ADD [IsSpedition] [bit] DEFAULT((0)) NOT NULL ; " +
                "END ";
            return sql;
        }
        ///<summary>Update1193() / clsUpdate</summary>
        ///<remarks>Table ADR anpassen</remarks>
        private string Update1193()
        {
            string sql = string.Empty;
            sql =
                //LEingang
                "IF COL_LENGTH('LEingang','ExTransportRef') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [ExTransportRef] [nvarchar] (254) NULL; " +
                "END " +
                "IF COL_LENGTH('LEingang','ExAuftragRef') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [ExAuftragRef] [nvarchar] (254) NULL; " +
                "END ";

            return sql;
        }
        ///<summary>Update1194() / clsUpdate</summary>
        ///<remarks>Table ExtraCharge anpassen 23.05.2014</remarks>
        private string Update1194()
        {
            string sql = string.Empty;
            sql =
                // ExtraCharge
                "IF COL_LENGTH('ExtraCharge','AdrID') IS NULL " +
                "BEGIN " +
                "ALTER TABLE [ExtraCharge] ADD [AdrID] [decimal] DEFAULT((-1)) NOT NULL ;" +
                "END " +
                "IF COL_LENGTH('ExtraCharge','KontoID') IS NULL " +
                "BEGIN " +
                "ALTER TABLE [ExtraCharge] ADD [KontoID] [decimal] DEFAULT((-1)) NOT NULL ;" +
                "END ";
            return sql;
        }
        ///<summary>Update1194() / clsUpdate</summary>
        ///<remarks>Table ExtraCharge anpassen 23.05.2014</remarks>
        private string Update1195()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADRVerweis]') AND type in (N'U')) " +
                 "BEGIN " +
                     "CREATE TABLE [dbo].[ADRVerweis](" +
                     "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                     "[SenderAdrID] [decimal](28, 0) NOT NULL," +
                     "[VerweisAdrID] [decimal](28, 0) NOT NULL," +
                     "[LieferantenID] [decimal](28, 0) NOT NULL," +
                     "[MandantenID] [decimal](28, 0) NOT NULL," +
                     "[ArbeitsbereichID] [decimal](28, 0) NOT NULL," +
                     "[Verweis] [nvarchar](254) NULL," +
                     "[aktiv] [bit] NOT NULL," +
                     "CONSTRAINT [PK_ADRVerweis] PRIMARY KEY CLUSTERED([ID] ASC" +
                     ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                     ") ON [PRIMARY] " +

                     "ALTER TABLE [dbo].[ADRVerweis] ADD  CONSTRAINT [DF_ADRVerweis_SenderAdrID]  DEFAULT ((0)) FOR [SenderAdrID] " +
                     "ALTER TABLE [dbo].[ADRVerweis] ADD  CONSTRAINT [DF_ADRVerweis_VerweisAdrID]  DEFAULT ((0)) FOR [VerweisAdrID] " +
                     "ALTER TABLE [dbo].[ADRVerweis] ADD  CONSTRAINT [DF_ADRVerweis_LieferantenID]  DEFAULT ((0)) FOR [LieferantenID] " +
                     "ALTER TABLE [dbo].[ADRVerweis] ADD  CONSTRAINT [DF_ADRVerweis_MandantenID]  DEFAULT ((0)) FOR [MandantenID] " +
                     "ALTER TABLE [dbo].[ADRVerweis] ADD  CONSTRAINT [DF_ADRVerweis_ArbeitsbereichID]  DEFAULT ((0)) FOR [ArbeitsbereichID] " +
                     "ALTER TABLE [dbo].[ADRVerweis] ADD  CONSTRAINT [DF_ADRVerweis_aktiv]  DEFAULT ((1)) FOR [aktiv] " +
                 "END ";
            return sql;
        }
        ///<summary>Update1196() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1196()
        {
            string sql = string.Empty;
            sql =
                //LEingang
                "IF COL_LENGTH('Arbeitsbereich','ASNTransfer') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [ASNTransfer] [bit] Default(0) NOT NULL; " +
                "END ";

            return sql;
        }
        ///<summary>Update1197() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1197()
        {
            string sql = string.Empty;
            sql =
                //RGPosArtikel
                "IF COL_LENGTH('RGPosArtikel','Kosten') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [RGPosArtikel] ADD [Kosten] [money] Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('RGPosArtikel','TarifPosID') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [RGPosArtikel] ADD [TarifPosID] [decimal] (28,0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('RGPosArtikel','IsUBCalc') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [RGPosArtikel] ADD [IsUBCalc] [bit] Default(0) NOT NULL; " +
                "END ";

            return sql;
        }
        //<summary>Update1198() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1198()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExtraChargeADR]') AND type in (N'U')) " +
                    "BEGIN " +
                        "CREATE TABLE [dbo].[ExtraChargeADR]( " +
                            "[ID] [decimal](28,0) IDENTITY(1,1) NOT NULL," +
                            "[AdrID] [decimal](28,0) NOT NULL," +
                            "[ExtraChargeID] [decimal](28,0) NOT NULL," +
                            "[Preis] [money] NOT NULL," +
                            "CONSTRAINT [PK_ExtraChargeADR] PRIMARY KEY CLUSTERED ( [ID] ASC ) " +
                            "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY], " +
                            "CONSTRAINT [UQ_codes] UNIQUE NONCLUSTERED ( [AdrID], [ExtraChargeID] ) " +
                            "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                        ") ON [PRIMARY];" +
                    "END";
            return sql;
        }
        //<summary>Update1198() / clsUpdate</summary>
        ///<remarks>Tabelle Tarife Spalte CalcUBExKosten wird umbenannt in CalcNebenkosten </remarks>
        private string Update1199()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Tarife','CalcUBExKosten') IS NOT NULL " +
                "BEGIN " +
                    "EXEC sp_rename " +
                          "@objname ='Tarife.CalcUBExKosten', " +
                          "@newname ='CalcNebenkosten', " +
                          "@objtype = 'COLUMN'; " +
                "END " +
                //Tarifpositionen
                "IF COL_LENGTH('TarifPositionen','BruttoVon') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [TarifPositionen] ADD [BruttoVon] [decimal] (28,0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('TarifPositionen','BruttoBis') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [TarifPositionen] ADD [BruttoBis] [decimal] (28,0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('TarifPositionen','DickeVon') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [TarifPositionen] ADD [DickeVon] [decimal] (28,0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('TarifPositionen','DickeBis') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [TarifPositionen] ADD [DickeBis] [decimal] (28,0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('TarifPositionen','BreiteVon') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [TarifPositionen] ADD [BreiteVon] [decimal] (28,0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('TarifPositionen','BreiteBis') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [TarifPositionen] ADD [BreiteBis] [decimal] (28,0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('TarifPositionen','LaengeVon') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [TarifPositionen] ADD [LaengeVon] [decimal] (28,0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('TarifPositionen','LaengeBis') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [TarifPositionen] ADD [LaengeBis] [decimal] (28,0) Default(0) NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1198() / clsUpdate</summary>
        ///<remarks>Tabelle Tarife Spalte CalcUBExKosten wird umbenannt in CalcNebenkosten </remarks>
        private string Update1200()
        {
            string sql = string.Empty;
            sql =
                //Tarifpositionen
                "IF COL_LENGTH('Tarife','CalcGleis') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [Tarife] ADD [CalcGleis] [bit] Default(0) NOT NULL; " +
                "END " +
                //RG Positionen
                "IF COL_LENGTH('RGPositionen','FibuKto') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [RGPositionen] ADD [FibuKto] [int] Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('Rechnungen','RGBookPrintDate') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [Rechnungen] ADD [RGBookPrintDate] [Datetime] NULL; " +
                "END " +
                //LEingang ATG für 
                "IF COL_LENGTH('LEingang','ASNRef') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [ASNRef] [nvarchar] (254) NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1198() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1201()
        {
            string sql = string.Empty;
            sql =
                //Artikel 
                "IF COL_LENGTH('Artikel','Guete') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [Artikel] ADD [Guete] [nvarchar] (254) NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1202() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1202()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LagerMeldungen]') AND type in (N'U')) " +
                "BEGIN " +
                    "CREATE TABLE [dbo].[LagerMeldungen]( " +
                    " [ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                    " [ASNTypID] [decimal](28, 0) NULL," +
                    " [ASNID] [decimal](28, 0) NULL," +
                    " [Datum] [datetime] NULL," +
                    " [ArtikelID] [decimal](28, 0) NULL," +
                    " [Info] [text] NULL," +
                    " [FileName] [text] NULL," +
                    " [Typ] [nvarchar](50) NULL," +
                        " CONSTRAINT [PK_LagerMeldungen] PRIMARY KEY CLUSTERED " +
                        " ([ID] ASC" +
                        " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                    " ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]" +
                " END;";
            return sql;
        }
        //<summary>Update1203() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1203()
        {


            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('LEingang','LockedBy') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [LockedBy] [decimal] (28,0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('LAusgang','LockedBy') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [LAusgang] ADD [LockedBy] [decimal] (28,0) Default(0) NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1204() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1204()
        {


            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('LEingang','IsWaggon') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [IsWaggon] [bit] Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('LAusgang','IsWaggon') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [LAusgang] ADD [IsWaggon] [bit] Default(0) NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1205() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1205()
        {


            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('LAusgang','exTransportRef') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [LAusgang] ADD [ExTransportRef] [nvarchar](255) Default('') NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1206() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1206()
        {


            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('Schaeden','ViewID') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [Schaeden] ADD [ViewID] [decimal](28,0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('ExtraCharge','ViewID') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [ExtraCharge] ADD [ViewID] [decimal](28,0) Default(0) NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1207() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1207()
        {


            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('Artikel','exLsNoA') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [Artikel] ADD [exLsNoA] [nvarchar](50) Default('') NOT NULL; " +
                "END " +
                "IF COL_LENGTH('Artikel','exLsPosA') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [Artikel] ADD [exLsPosA] [nvarchar](50) Default('') NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1206() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1208()
        {
            string sql = string.Empty;
            sql =
                 "IF COL_LENGTH('ADR','PostAnzeigeBy') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE ADR ADD [PostAnzeigeBy] [decimal] (28, 0) DEFAULT((0)) NOT NULL;" +
                   "END ";
            return sql;
        }
        //<summary>Update1206() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1209()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Umbuchungen]') AND type in (N'U')) " +
                "BEGIN " +
                    "CREATE TABLE [dbo].[Umbuchungen]( " +
                    " [ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                    " [ArtIDAlt] [decimal](28, 0) NOT NULL," +
                    " [ArtIDNeu] [decimal](28, 0) NOT NULL," +
                    " [UB_AltCalcEinlagerung] [bit] DEFAULT((1)) NOT NULL," +
                    " [UB_AltCalcAuslagerung] [bit] DEFAULT((1)) NOT NULL," +
                    " [UB_AltCalcLagergeld] [bit] DEFAULT((1)) NOT NULL," +
                    " [UB_NeuCalcEinlagerung] [bit] DEFAULT((1)) NOT NULL," +
                    " [UB_NeuCalcAuslagerung] [bit] DEFAULT((1)) NOT NULL," +
                    " [UB_NeuCalcLagergeld] [bit] DEFAULT((1)) NOT NULL," +
                    " CONSTRAINT [PK_Umbuchungen] PRIMARY KEY CLUSTERED " +
                        " ([ID] ASC" +
                        " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                    " ) ON [PRIMARY]; " +
                    "INSERT INTO [dbo].[Umbuchungen] " +
                    "([ArtIDAlt] " +
                    ",[ArtIDNeu] " +
                    ",[UB_AltCalcEinlagerung] " +
                    ",[UB_AltCalcAuslagerung] " +
                    ",[UB_AltCalcLagergeld] " +
                    ",[UB_NeuCalcEinlagerung] " +
                    ",[UB_NeuCalcAuslagerung] " +
                    ",[UB_NeuCalcLagergeld])	 " +
                    "SELECT ArtIDAlt " +
                    ",ID " +
                    ",UB_AltCalcEinlagerung " +
                    ",UB_AltCalcAuslagerung " +
                    ",UB_AltCalcLagergeld " +
                    ",UB_NeuCalcEinlagerung " +
                    ",UB_NeuCalcAuslagerung " +
                    ",UB_NeuCalcLagergeld " +
                    " from [dbo].[Artikel] where ArtIDAlt>0;" +
                " END;";
            return sql;
        }
        //<summary>Update1210() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1210()
        {
            string sql = string.Empty;
            sql =
                 "IF COL_LENGTH('Arbeitsbereich','MandantenID') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Arbeitsbereich ADD [MandantenID] [decimal] (28, 0) DEFAULT((0)) NOT NULL;" +
                 "END " +
                  "IF COL_LENGTH('Gueterart','Werksnummer') IS NULL " +
                  "BEGIN " +
                       "ALTER TABLE Gueterart ADD [Werksnummer] [nvarchar](50) Default('') NOT NULL;" +
                  "END ";
            return sql;
        }
        //<summary>Update1210() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1211()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Arbeitsbereich','IsLager') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Arbeitsbereich ADD [IsLager]  [bit] Default(0) NOT NULL; " +
                 "END " +
                  "IF COL_LENGTH('Arbeitsbereich','IsSpedition') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Arbeitsbereich ADD [IsSpedition]  [bit] Default(0) NOT NULL; " +
                 "END ";
            return sql;
        }
        //<summary>Update1212() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1212()
        {
            string sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Emails]') AND type in (N'U')) " +
                      "BEGIN " +
                        "CREATE TABLE [dbo].[Emails]( " +
                           "[ID] [decimal](18, 0) IDENTITY(1,1) NOT NULL, " +
                           "[BenutzerID] [decimal](18, 0) NOT NULL, " +
                           "[Absender] [nvarchar](50) NOT NULL, " +
                           "[Empfaenger] [nvarchar](max) NOT NULL, " +
                           "[Text] [nvarchar](max) NOT NULL, " +
                           "[Dateien] [nvarchar](max) NOT NULL, " +
                           "[Datum] [datetime] NOT NULL, " +
                        "CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED ( " +
                        "[ID] ASC )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY];" +
                     "END";
            return sql;

        }

        //<summary>Update1213() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1213()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LEingang','Fahrer') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE LEingang ADD [Fahrer] [nvarchar](100) NULL; " +
                 "END " +
                  "IF COL_LENGTH('LAusgang','Fahrer') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE LAusgang ADD [Fahrer] [nvarchar](100) NULL; " +
                 "END ";
            return sql;
        }
        //<summary>Update1214() / clsUpdate</summary>
        ///<remarks>Table ADRVerweis neue Spalte ASNArtID</remarks>
        private string Update1214()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADRVerweis','ASNFileTyp') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE ADRVerweis ADD [ASNFileTyp] [nvarchar](50) NULL; " +
                 "END ";
            return sql;
        }
        //<summary>Update1215() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1215()
        {
            string sql = string.Empty;
            sql =
                 "IF COL_LENGTH('Tarife','IsVersPauschal') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Tarife ADD [IsVersPauschal]  [bit] Default(0) NOT NULL; " +
                 "END ";

            return sql;
        }
        //<summary>Update1215() / clsUpdate</summary>
        ///<remarks>Datentyp Spalte LieferantenID ändern</remarks>
        private string Update1216()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADRVerweis','LieferantenVerweis') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE ADRVerweis ADD [LieferantenVerweis]  [nvarchar] (25) NULL; " +
                 "END ";
            return sql;
        }
        //<summary>Update1217() / clsUpdate</summary>
        ///<remarks>Spalte LieferantenID löschen</remarks>
        private string Update1217()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADRVerweis','LieferantenVerweis') IS NOT NULL " +
                 "BEGIN " +
                      "Update ADRVerweis SET LieferantenVerweis=CAST(LieferantenID as nvarchar(25));" +
                 "END ";
            return sql;
        }
        //<summary>Update1218() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1218()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASNArtFieldAssignment]') AND type in (N'U')) " +
                  "BEGIN " +
                      "CREATE TABLE [dbo].[ASNArtFieldAssignment](" +
                      "[ID] [decimal](18, 0) IDENTITY(1,1) NOT NULL," +
                     "[Sender] [decimal](18, 0) NOT NULL," +
                      "[Receiver] [decimal](18, 0) NOT NULL," +
                     "[ASNField] [nvarchar](50) NULL," +
                     "[ArtField] [nvarchar](50) NULL," +
                      "CONSTRAINT [PK_ASNArtFieldAssignment] PRIMARY KEY CLUSTERED ([ID] ASC" +
                      ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] " +
                      ") ON [PRIMARY] " +
                 "END ";
            return sql;
        }
        //<summary>Update1216() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1219()
        {
            string sql = string.Empty;
            sql =
                 "IF COL_LENGTH('Tarife','VersMaterialWert') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Tarife ADD [VersMaterialWert]  [money] Default(0) NOT NULL; " +
                 "END ";

            return sql;
        }
        //<summary>Update1216() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1220()
        {
            string sql = string.Empty;
            sql =
                 "IF COL_LENGTH('Reihe','Anzahl') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [Anzahl]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','DickeVon') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [DickeVon]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','DickeBis') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [DickeBis]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','BreiteVon') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [BreiteVon]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','BreiteBis') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [BreiteBis]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','LaengeVon') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [LaengeVon]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','LaengeBis') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [LaengeBis]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','HoeheVon') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [HoeheVon]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','HoeheBis') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [HoeheBis]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','BruttoVon') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [BruttoVon]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','BruttoBis') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [BruttoBis]  [decimal] Default(0) NOT NULL; " +
                 "END " +
            "IF COL_LENGTH('Reihe','GArtId') IS NULL " +
                 "BEGIN " +
                      "ALTER TABLE Reihe ADD [GArtId]  [int] Default(0) NOT NULL; " +
                 "END ";


            return sql;
        }
        //<summary>Update1221() / clsUpdate</summary>
        ///<remarks>Neue Table KundeGArtDeafault beinhaltet die default Güterart für einen Arbeitsbereich</remarks>
        private string Update1221()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KundGArtDefault]') AND type in (N'U')) " +
                 "BEGIN " +

                      "CREATE TABLE [dbo].[KundGArtDefault](" +
                      "[ID] [int] IDENTITY(1,1) NOT NULL," +
                      "[AdrID] [decimal](28, 0) NULL," +
                      "[GArtID] [decimal](28, 0) NULL," +
                      "[AbBereichID] [decimal](22, 0) NULL," +
                      "CONSTRAINT [PK_KundGArtDefault] PRIMARY KEY CLUSTERED([ID] ASC" +
                      ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                      ") ON [PRIMARY] " +

                      "ALTER TABLE [dbo].[KundGArtDefault]  WITH CHECK ADD  CONSTRAINT [FK_KundGArtDefault_Gueterart] FOREIGN KEY([GArtID]) " +
                      "REFERENCES [dbo].[Gueterart] ([ID]) " +
                      "ON UPDATE CASCADE " +
                      "ON DELETE CASCADE " +

                      "ALTER TABLE [dbo].[KundGArtDefault] CHECK CONSTRAINT [FK_KundGArtDefault_Gueterart] " +
                      "ALTER TABLE [dbo].[KundGArtDefault]  WITH CHECK ADD  CONSTRAINT [FK_KundGArtDefault_KundGArtDefault1] FOREIGN KEY([AbBereichID]) " +
                      "REFERENCES [dbo].[Arbeitsbereich] ([ID]) " +
                      "ON UPDATE CASCADE " +
                      "ON DELETE CASCADE " +

                      "ALTER TABLE [dbo].[KundGArtDefault] CHECK CONSTRAINT [FK_KundGArtDefault_KundGArtDefault1] " +
                  "END ";
            return sql;
        }
        //<summary>Update1222() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1222()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('ASNArtFieldAssignment','IsDefValue') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE ASNArtFieldAssignment ADD [IsDefValue] [bit] Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('ASNArtFieldAssignment','DefValue') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE ASNArtFieldAssignment ADD [DefValue] [nvarchar] (MAX)Default('') NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1223() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1223()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArbeitsbereichTarif]') AND type in (N'U')) " +
                  "BEGIN " +
                    "CREATE TABLE [dbo].[ArbeitsbereichTarif](" +
                    "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL, " +
                    "[AbBereichID] [decimal](22, 0) NOT NULL, " +
                    "[TarifID] [decimal](28, 0) NOT NULL, " +
                    " CONSTRAINT [PK_ArbeitsbereichTarif] PRIMARY KEY CLUSTERED([ID] ASC " +
                    ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] " +
                    ") ON [PRIMARY] " +

                    "ALTER TABLE [dbo].[ArbeitsbereichTarif]  WITH CHECK ADD  CONSTRAINT [FK_ArbeitsbereichTarif_ArbeitsbereichTarif] FOREIGN KEY([AbBereichID]) " +
                    "REFERENCES [dbo].[Arbeitsbereich] ([ID]) " +
                    "ON UPDATE CASCADE " +
                    "ON DELETE CASCADE " +

                    "ALTER TABLE [dbo].[ArbeitsbereichTarif] CHECK CONSTRAINT [FK_ArbeitsbereichTarif_ArbeitsbereichTarif] " +

                    "ALTER TABLE [dbo].[ArbeitsbereichTarif]  WITH CHECK ADD  CONSTRAINT [FK_ArbeitsbereichTarif_Tarife] FOREIGN KEY([TarifID]) " +
                    "REFERENCES [dbo].[Tarife] ([ID]) " +

                    "ALTER TABLE [dbo].[ArbeitsbereichTarif] CHECK CONSTRAINT [FK_ArbeitsbereichTarif_Tarife] " +
                   "END ";
            return sql;
        }
        //<summary>Update1224() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1224()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASNTableCombiValue]') AND type in (N'U')) " +
                  "BEGIN " +
                      " CREATE TABLE [dbo].[ASNTableCombiValue](" +
                          "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                          "[Sender] [decimal](28, 0) NULL," +
                          "[Receiver] [decimal](28, 0) NULL," +
                          "[TableName] [nvarchar](50) NULL," +
                          "[ColValue] [nvarchar](100) NULL," +
                          "[ColsForCombination] [nvarchar](max) NULL," +
                       " CONSTRAINT [PK_ASNTableCombiValue] PRIMARY KEY CLUSTERED([ID] ASC " +
                       ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] " +
                       ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]  " +

                       "ALTER TABLE [dbo].[ASNTableCombiValue]  WITH CHECK ADD  CONSTRAINT [FK_ASNTableCombiValue_ADR_Sender] FOREIGN KEY([Sender]) " +
                       "REFERENCES [dbo].[ADR] ([ID]) " +
                       //"ON UPDATE CASCADE "+
                       "ON DELETE CASCADE " +
                       "ALTER TABLE [dbo].[ASNTableCombiValue] CHECK CONSTRAINT [FK_ASNTableCombiValue_ADR_Sender] " +

                       "ALTER TABLE [dbo].[ASNTableCombiValue]  WITH CHECK ADD  CONSTRAINT [FK_ASNTableCombiValue_Arbeitsbereich] FOREIGN KEY([AbBereichID]) " +
                       "REFERENCES [dbo].[Arbeitsbereich] ([ID]) " +
                       //"ON UPDATE CASCADE "+
                       "ON DELETE CASCADE " +
                       "ALTER TABLE [dbo].[ASNTableCombiValue] CHECK CONSTRAINT [FK_ASNTableCombiValue_Arbeitsbereich]  " +
                       " " + Environment.NewLine +
                   " END ";
            return sql;
        }
        //<summary>Update1225() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1225()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('Rechnungen','InfoText') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE Rechnungen ADD [InfoText] [nvarchar] (MAX) NULL;; " +
                "END ";
            return sql;
        }
        //<summary>Update1221() / clsUpdate</summary>
        ///<remarks>Neue Table KundeGArtDeafault beinhaltet die default Güterart für einen Arbeitsbereich</remarks>
        private string Update1226()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArbeitsbereichGArten]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[ArbeitsbereichGArten](" +
                        "[AbBereichID] [decimal](22, 0) NULL," +
                        "[GArtID] [decimal](28, 0) NULL" +
                        ") ON [PRIMARY] " +

                        "ALTER TABLE [dbo].[ArbeitsbereichGArten]  WITH CHECK ADD  CONSTRAINT [FK_ArbeitsbereichGArten_Arbeitsbereich] FOREIGN KEY([AbBereichID]) " +
                        "REFERENCES [dbo].[Arbeitsbereich] ([ID]) " +
                        "ON UPDATE CASCADE " +
                        "ON DELETE CASCADE " +
                        "ALTER TABLE [dbo].[ArbeitsbereichGArten] CHECK CONSTRAINT [FK_ArbeitsbereichGArten_Arbeitsbereich] " +

                        "ALTER TABLE [dbo].[ArbeitsbereichGArten]  WITH CHECK ADD  CONSTRAINT [FK_ArbeitsbereichGArten_Gueterart] FOREIGN KEY([GArtID]) " +
                        "REFERENCES [dbo].[Gueterart] ([ID]) " +
                        "ON UPDATE CASCADE " +
                        "ON DELETE CASCADE " +
                        "ALTER TABLE [dbo].[ArbeitsbereichGArten] CHECK CONSTRAINT [FK_ArbeitsbereichGArten_Gueterart] " +
                   "END ";
            return sql;
        }
        //<summary>Update1227() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1227()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('Mandanten','RGNr') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE Mandanten ADD [RGNr] [decimal] (28, 0) Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('Mandanten','GSNr') IS NULL " +
                "BEGIN " +
                   "ALTER TABLE Mandanten ADD [GSNr] [decimal] (28, 0) Default(0) NOT NULL; " +
                "END " +
                //Table Primekeys
                "IF COL_LENGTH('PrimeKeys','AbBereichID') IS NULL " +
                "BEGIN " +
                   "ALTER TABLE PrimeKeys ADD [AbBereichID] [decimal] (22, 0) Default(0) NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1228() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1228()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('Mandanten','RGNr') IS NOT NULL " +
                "BEGIN " +
                     //Update der RGNr
                     "DECLARE @iCount Integer; " +
                     "DECLARE @iRows Integer; " +
                     "DECLARE @RGNr decimal; " +
                     "DECLARE @GSNr decimal; " +
                     "DECLARE @Mandant decimal; " +
                     "DECLARE @tmpTable Table(ID decimal, Mandant nvarchar(50)); " +
                     "SET @Mandant= 0; " +
                     "INSERT INTO @tmpTable (ID, Mandant) " +
                     " Select a.ID, a.Beschreibung FROM Mandanten a   " +
                     "SET @iCount=1; " +
                     "SET @iRows = (Select COUNT(ID) FROM @tmpTable); " +
                     "SELECT * FROM @tmpTable " +
                     "WHILE(@iCount<=@iRows) " +
                     "BEGIN " +
                          "SET @Mandant =(Select ID FROM @tmpTable WHERE ID=@iCount); " +
                          "SET @RGNr=(Select RGNr FROM PrimeKeys WHERE Mandanten_ID=@Mandant); " +
                          "SET @GSNr=(Select GSNr FROM PrimeKeys WHERE Mandanten_ID=@Mandant); " +
                          "IF @Mandant>0 BEGIN " +
                             "Update Mandanten SET " +
                                          "RGNr=@RGNr " +
                                          ", GSNr=@GSNr " +
                                          "WHERE ID=@Mandant; " +
                          "END " +
                          "SET @iCount=@iCount+1; " +
                     "END " +
                "END ";
            return sql;
        }
        //<summary>Update1229() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1229()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('Rechnungen','FibuInfo') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE Rechnungen ADD [FibuInfo] [nvarchar]  (MAX) NULL; " +
                "END " +
                "IF COL_LENGTH('Artikel','IsMulde') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE Artikel ADD [IsMulde] [bit] Default(0) NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1230() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1230()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('Rechnungen','DocName') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE Rechnungen ADD [DocName] [nvarchar]  (MAX) NULL; " +
                "END " +
                "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RGVorlagenTxt]') AND type in (N'U')) " +
                "BEGIN " +
                      "CREATE TABLE [dbo].[RGVorlagenTxt](" +
                      "[Vorlage] [varchar](max) NULL," +
                      "[Vorlagentext] [varchar](max) NULL" +
                      ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] " +
                "END ";
            return sql;
        }
        //<summary>Update1230() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1231()
        {
            string sql = string.Empty;
            sql =
                "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RGPosArtikel]') AND type in (N'U')) " +
                "BEGIN " +
                      "Delete RGPosArtikel WHERE RGPosID NOT IN (Select ID FROM RGPositionen); " +

                      "ALTER TABLE [dbo].[RGPosArtikel]  WITH CHECK ADD  CONSTRAINT [FK_RGPosArtikel_RGPositionen] FOREIGN KEY([RGPosID]) " +
                      "REFERENCES [dbo].[RGPositionen] ([ID]) " +
                      "ON DELETE CASCADE " +
                      "ALTER TABLE [dbo].[RGPosArtikel] CHECK CONSTRAINT [FK_RGPosArtikel_RGPositionen] " +
                "END ";
            return sql;
        }
        //<summary>Update1230() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1232()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('RGVorlagenTxt','EinzelPreis') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE RGVorlagenTxt ADD [EinzelPreis] [money] DEFAULT ((0)) NOT NULL " +
                "END ";
            return sql;
        }
        //<summary>Update1233() / clsUpdate</summary>
        ///<remarks>Table ArbeitsbereichUser</remarks>
        private string Update1233()
        {
            string sql = string.Empty;
            sql =
                "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArbeitsbereichUser]') AND type in (N'U')) " +
                "BEGIN " +
                      "CREATE TABLE [dbo].[ArbeitsbereichUser](" +
                      "[AbBereichID] [decimal](22, 0) NULL," +
                      "[UserID] [decimal](28, 0) NULL" +
                      ") ON [PRIMARY] " +

                      "ALTER TABLE [dbo].[ArbeitsbereichUser]  WITH CHECK ADD  CONSTRAINT [FK_ArbeitsbereichUser_Arbeitsbereich] FOREIGN KEY([AbBereichID]) " +
                      "REFERENCES [dbo].[Arbeitsbereich] ([ID]) " +
                      "ON DELETE CASCADE " +
                      "ALTER TABLE [dbo].[ArbeitsbereichUser] CHECK CONSTRAINT [FK_ArbeitsbereichUser_Arbeitsbereich] " +

                      "ALTER TABLE [dbo].[ArbeitsbereichUser]  WITH CHECK ADD  CONSTRAINT [FK_ArbeitsbereichUser_User] FOREIGN KEY([UserID]) " +
                      "REFERENCES [dbo].[User] ([ID]) " +
                      "ON DELETE CASCADE " +
                      "ALTER TABLE [dbo].[ArbeitsbereichUser] CHECK CONSTRAINT [FK_ArbeitsbereichUser_User] " +

                     "DECLARE @iCount Integer; " +
                     "DECLARE @iRows Integer; " +
                     "SET @iCount=1; " +
                     "SET @iRows = (Select MAX(ID) FROM Arbeitsbereich); " +
                     "WHILE(@iCount<=@iRows) " +
                     "BEGIN " +
                          "IF ((Select ID FROM Arbeitsbereich WHERE ID=@iCount)=@iCount) " +
                          "BEGIN " +
                              "INSERT INTO ArbeitsbereichUser (AbBereichID,UserID) " +
                              "SELECT @iCount, ID FROM [User] " +
                          "END " +
                          "SET @iCount=@iCount+1; " +
                     "END " +
                 "END ";
            return sql;
        }
        //<summary>Update1233() / clsUpdate</summary>
        ///<remarks>Table User neue Spalte IsAdmin</remarks>
        private string Update1234()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('User','IsAdmin') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [User] ADD [IsAdmin] [bit] Default(0) NOT NULL; " +
                "END ";

            return sql;
        }
        //<summary>Update1235() / clsUpdate</summary>
        ///<remarks>Table  neue Spalte IsAdmin</remarks>
        private string Update1235()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('Schaeden','Art') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [Schaeden] ADD [Art] [int] Default(0) NOT NULL; " +
                "END " +
                "IF COL_LENGTH('Schaeden','Code') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [Schaeden] ADD [Code] [nvarchar] (20) NULL; " +
                "END " +
                "IF COL_LENGTH('Schaeden','AutoSPL') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [Schaeden] ADD [AutoSPL] [bit] Default(0) NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1235() / clsUpdate</summary>
        ///<remarks>Table  neue Spalte IsAdmin</remarks>
        private string Update1236()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('TarifPositionen','QuantityCalcBase') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [TarifPositionen] ADD [QuantityCalcBase] [int] Default(0) NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1237() / clsUpdate</summary>
        ///<remarks>Table Emails neue Spalte Betreff</remarks>
        private string Update1237()
        {
            string sql = string.Empty;
            sql =
                "IF COL_LENGTH('Emails','Betreff') IS NULL " +
                "BEGIN " +
                     "ALTER TABLE [Emails] ADD [Betreff]  [nvarchar] (254) NULL; " +
                "END " +
                "IF COL_LENGTH('DocScan','AuftragID') IS NOT NULL " +
                "BEGIN " +
                    "EXEC sp_rename " +
                      "@objname ='DocScan.AuftragID', " +
                      "@newname ='AuftragTableID', " +
                      "@objtype = 'COLUMN'; " +
                "END " +
                "IF COL_LENGTH('DocScan','LEingangID') IS NOT NULL " +
                "BEGIN " +
                    "EXEC sp_rename " +
                      "@objname ='DocScan.LEingangID', " +
                      "@newname ='LEingangTableID', " +
                      "@objtype = 'COLUMN'; " +
                "END " +
                "IF COL_LENGTH('DocScan','LAusgangID') IS NOT NULL " +
                "BEGIN " +
                    "EXEC sp_rename " +
                      "@objname ='DocScan.LAusgangID', " +
                      "@newname ='LAusgangTableID', " +
                      "@objtype = 'COLUMN'; " +
                "END ";
            return sql;
        }
        //<summary>Update1237() / clsUpdate</summary>
        ///<remarks>neue Tabelle GueterartenADR</remarks>
        private string Update1238()
        {
            string sql = string.Empty;
            sql =
                "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GueterartADR]') AND type in (N'U')) " +
                "BEGIN " +
                     "CREATE TABLE [dbo].[GueterartADR](" +
                     "[GArtID] [decimal](28, 0) NOT NULL," +
                     "[AdrID] [decimal](28, 0) NOT NULL," +
                     "[AbBereichID] [decimal](22, 0) NOT NULL" +
                     ") ON [PRIMARY] " +

                     "ALTER TABLE [dbo].[GueterartADR] ADD  CONSTRAINT [DF_GueterartADR_GArtID]  DEFAULT ((0)) FOR [GArtID] " +
                     "ALTER TABLE [dbo].[GueterartADR] ADD  CONSTRAINT [DF_GueterartADR_AdrID]  DEFAULT ((0)) FOR [AdrID] " +
                     "ALTER TABLE [dbo].[GueterartADR] ADD  CONSTRAINT [DF_GueterartADR_AbBereichID]  DEFAULT ((0)) FOR [AbBereichID] " +
                     "ALTER TABLE [dbo].[GueterartADR]  WITH CHECK ADD  CONSTRAINT [FK_GueterartADR_ADR] FOREIGN KEY([AdrID]) " +

                     "REFERENCES [dbo].[ADR] ([ID]) " +
                     "ON DELETE CASCADE " +
                     "ALTER TABLE [dbo].[GueterartADR] CHECK CONSTRAINT [FK_GueterartADR_ADR] " +
                     "ALTER TABLE [dbo].[GueterartADR]  WITH CHECK ADD  CONSTRAINT [FK_GueterartADR_Arbeitsbereich] FOREIGN KEY([AbBereichID]) " +
                     "REFERENCES [dbo].[Arbeitsbereich] ([ID]) " +
                     "ALTER TABLE [dbo].[GueterartADR] CHECK CONSTRAINT [FK_GueterartADR_Arbeitsbereich] " +
                "END ";
            return sql;
        }
        //<summary>Update1239() / clsUpdate</summary>
        ///<remarks>Table Lieferanten löschen</remarks>
        private string Update1239()
        {
            string sql = string.Empty;
            sql =
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lieferanten]') AND type in (N'U')) " +
                "BEGIN " +
                     "DROP TABLE [dbo].[Lieferanten] " +
                "END ";
            return sql;
        }
        //<summary>Update1240() / clsUpdate</summary>
        ///<remarks>neue Tabelle Lieferant und LieferantenGruppe</remarks>
        private string Update1240()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lieferanten]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[Lieferanten](" +
                        "[LiefGruppenID] [decimal](28, 0) NOT NULL," +
                        "[AdrIDLieferant] [decimal](28, 0) NOT NULL" +
                        ") ON [PRIMARY] " +
                   "END " +
                   "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LieferantenGruppe]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[LieferantenGruppe](" +
                        "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL, " +
                        "[Name] [nvarchar](250) NULL," +
                        "[Lieferantennummer] [nvarchar](250) NULL," +
                        "[AdrIDKomPartner] [decimal](28, 0) NULL," +
                        "[AbBereichID] [decimal](22, 0) NULL," +
                        "[activ] [bit] NOT NULL," +
                        " CONSTRAINT [PK_LieferantenGruppe] PRIMARY KEY CLUSTERED([ID] ASC" +
                        ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] " +
                        ") ON [PRIMARY] " +

                        "ALTER TABLE [dbo].[LieferantenGruppe] ADD  CONSTRAINT [DF_LieferantenGruppe_activ]  DEFAULT ((1)) FOR [activ] " +
                   "END ";
            return sql;
        }
        //<summary>Update1241() / clsUpdate</summary>
        ///<remarks>neue Tabelle Lieferant und LieferantenGruppe</remarks>
        private string Update1241()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('RGPosArtikel','ArtRGTxt') IS NULL " +
                 "BEGIN " +
                     "ALTER TABLE [RGPosArtikel] ADD [ArtRGTxt]  [nvarchar] (254) NULL; " +
                 "END " +
                 "IF COL_LENGTH('RGPosArtikel','LstDatum') IS NULL " +
                 "BEGIN " +
                     "ALTER TABLE [RGPosArtikel] ADD [LstDatum]  [Datetime] NULL; " +
                 "END " +
                 "IF COL_LENGTH('RGPosArtikel','LstEinheit') IS NULL " +
                 "BEGIN " +
                    "ALTER TABLE [RGPosArtikel] ADD [LstEinheit] [nvarchar] (50) NULL; " +
                 "END ";
            return sql;
        }
        //<summary>Update1242() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1242()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('AuftragPos','T_Date') IS NOT NULL " +
                "BEGIN " +
                    "EXEC sp_rename " +
                      "@objname ='AuftragPos.T_Date', " +
                      "@newname ='LieferTermin', " +
                      "@objtype = 'COLUMN'; " +
                "END " +
                "IF COL_LENGTH('AuftragPos','ZF') IS NOT NULL " +
                "BEGIN " +
                    "EXEC sp_rename " +
                      "@objname ='AuftragPos.ZF', " +
                      "@newname ='LieferZF', " +
                      "@objtype = 'COLUMN'; " +
                "END " +
                "IF COL_LENGTH('AuftragPos','ZFRequire') IS NOT NULL " +
                "BEGIN " +
                    "EXEC sp_rename " +
                      "@objname ='AuftragPos.ZFRequire', " +
                      "@newname ='LieferZFRequire', " +
                      "@objtype = 'COLUMN'; " +
                "END " +
                "IF COL_LENGTH('AuftragPos','LadeTermin') IS NULL " +
                 "BEGIN " +
                     "ALTER TABLE [AuftragPos] ADD [LadeTermin]  DateTime NULL; " +
                 "END " +
                 "IF COL_LENGTH('AuftragPos','LadeZF') IS NULL " +
                 "BEGIN " +
                     "ALTER TABLE [AuftragPos] ADD [LadeZF]  DateTime NULL; " +
                 "END " +
                 "IF COL_LENGTH('AuftragPos','LadeZFRequire') IS NULL " +
                 "BEGIN " +
                     "ALTER TABLE [AuftragPos] ADD [LadeZFRequire]  [bit] Default(0) NOT NULL; " +
                 "END ";
            return sql;
        }
        //<summary>Update1243() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1243()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADRVerweis','VerweisArt') IS NULL " +
                 "BEGIN " +
                     "ALTER TABLE [ADRVerweis] ADD [VerweisArt] [nvarchar] (50) NULL; " +
                 "END " +
                 "IF COL_LENGTH('ADRVerweis','Bemerkung') IS NULL " +
                 "BEGIN " +
                     "ALTER TABLE [ADRVerweis] ADD [Bemerkung] [nvarchar] (254) NULL; " +
                 "END ";
            return sql;
        }
        //<summary>Update1243() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1244()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Sperrlager','DefWindungen') IS NULL " +
                 "BEGIN " +
                     "ALTER TABLE [Sperrlager] ADD [DefWindungen] [int] Default(0) NOT NULL; " +
                 "END " +
                 "IF COL_LENGTH('Sperrlager','Sperrgrund') IS NULL " +
                 "BEGIN " +
                    "ALTER TABLE [Sperrlager] ADD [Sperrgrund] [nvarchar] (254) NULL; " +
                 "END " +
                 "IF COL_LENGTH('Sperrlager','Vermerk') IS NULL " +
                 "BEGIN " +
                    "ALTER TABLE [Sperrlager] ADD [Vermerk] [nvarchar] (254) NULL; " +
                 "END " +
                 //DocScan
                 "IF COL_LENGTH('DocScan','TableName') IS NULL " +
                 "BEGIN " +
                     "ALTER TABLE [DocScan] ADD [TableName] [nvarchar] (50) NULL; " +
                 "END " +
                 "IF COL_LENGTH('DocScan','TableID') IS NULL " +
                 "BEGIN " +
                     "ALTER TABLE [DocScan] ADD [TableID] [decimal] (28,0)  Default(0) NOT NULL; " +
                 "END " +
                "IF COL_LENGTH('DocScan','Thumbnail') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [DocScan] ADD [Thumbnail] [varbinary](max) NULL; " +
                "END " +
                "IF COL_LENGTH('DocScan','IsForSPLMessage') IS NULL " +
                "BEGIN " +
                    "ALTER TABLE [DocScan] ADD [IsForSPLMessage] [bit] DEFAULT(0) NOT NULL; " +
                "END ";
            return sql;
        }
        //<summary>Update1245() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1245()
        {
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LEingang_Lieferant]') AND type = 'D') " +
                  "BEGIN " +
                    "ALTER TABLE [dbo].[LEingang] DROP CONSTRAINT [DF_LEingang_Lieferant] " +
                  "END " +
                  "ALTER TABLE [LEingang] Alter Column [Lieferant] [nvarchar] (30) NULL; " +
                  "IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LAusgang_Lieferant]') AND type = 'D') " +
                  "BEGIN " +
                    "ALTER TABLE [dbo].[LAusgang] DROP CONSTRAINT [DF_LAusgang_Lieferant] " +
                  "END " +
                  "ALTER TABLE [LAusgang] Alter Column [Lieferant] [nvarchar] (30) NULL; ";
            return sql;
        }
        //<summary>Update1246() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1246()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Artikel','IsLabelPrint') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Artikel] ADD [IsLabelPrint] [bit] DEFAULT ((0)) NOT NULL; " +
                  "END ";
            return sql;
        }
        //<summary>Update1247() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1247()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Artikel','IsLabelPrint') IS NOT NULL " +
                  "BEGIN " +
                     "Update Artikel SET IsLabelPrint=1 WHERE LAusgangTableID>0 ;" +
                  "END " +
                  "IF COL_LENGTH('Mandanten','VDA4905Verweis') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Mandanten] ADD [VDA4905Verweis] [nvarchar] (30) NULL; " +
                  "END ";

            return sql;
        }
        //<summary>Update1248() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1248()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LAusgang','IsRL') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [LAusgang] ADD [IsRL] [bit] DEFAULT ((0)) NOT NULL; " +
                  "END " +
                   "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ruecklieferung]') AND type in (N'U')) " +
                  "BEGIN " +
                    "DROP TABLE [dbo].[Ruecklieferung]; " +
                  "END ";
            return sql;
        }
        //<summary>Update1248() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1249()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNArtFieldAssignment','CopyToField') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [ASNArtFieldAssignment] ADD [CopyToField] [nvarchar] (100) NULL; " +
                  "END " +
                  "IF COL_LENGTH('ASNArtFieldAssignment','FormatFunction') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [ASNArtFieldAssignment] ADD [FormatFunction] [nvarchar] (100) NULL; " +
                  "END ";
            return sql;
        }
        //<summary>Update1248() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1250()
        {
            string sql = string.Empty;

            sql = "IF COL_LENGTH('LagerMeldungen','Typ') IS NOT NULL " +
                  "BEGIN " +
                     "ALTER TABLE [LagerMeldungen]  DROP COLUMN [Typ];" +
                  "END " +
                  "IF COL_LENGTH('Artikel','IsProblem') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Artikel] ADD [IsProblem] [bit] DEFAULT ((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('LagerMeldungen','Sender') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [LagerMeldungen] ADD [Sender] [decimal] (28,0)  Default(0) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('LagerMeldungen','Receiver') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [LagerMeldungen] ADD [Receiver] [decimal] (28,0)  Default(0) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('LagerMeldungen','ASNAction') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [LagerMeldungen] ADD [ASNAction] [int] Default(0) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Artikel','IsKorStVerUse') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Artikel] ADD [IsKorStVerUse] [bit] Default(0) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Artikel','IgnLM') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Artikel] ADD [IgnLM] [bit] Default(0) NOT NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1251() / clsUpdate</summary>
        ///<remarks>neue Tabelle Lieferant und LieferantenGruppe</remarks>
        private string Update1251()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Abrufe]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[Abrufe](" +
                        "[ID] [int] IDENTITY(1,1) NOT NULL," +
                        "[IsRead] [bit] NOT NULL CONSTRAINT [DF_Abruf_IsRead]  DEFAULT ((0))," +
                        "[ArtikelID] [int] NOT NULL," +
                        "[LVSNr] [int] NULL," +
                        "[Werksnummer] [nvarchar](100) NULL," +
                        "[Produktionsnummer] [nvarchar](100) NULL," +
                        "[Charge] [nvarchar](100) NULL," +
                        "[Brutto] [decimal](18, 2) NULL," +
                        "[CompanyID] [int] NULL," +
                        "[CompanyName] [nvarchar](max) NULL," +
                        "[AbBereich] [int] NULL," +
                        "[Datum] [datetime] NULL," +
                        "[EintreffDatum] [datetime] NULL," +
                        "[EintreffZeit] [datetime] NULL," +
                        "[BenutzerID] [int] NULL," +
                        "[Benutzername] [nvarchar](50) NULL," +
                        "[Referenz] [nvarchar](50) NULL," +
                        "[Abladestelle] [nvarchar](50) NULL," +
                        "[Aktion] [nvarchar](50) NULL," +
                        "[Erstellt] [datetime] NULL," +
                        "[IsCreated] [bit] NULL CONSTRAINT [DF_Abruf_IsCreated]  DEFAULT ((0))," +
                        "CONSTRAINT [PK_Abruf] PRIMARY KEY CLUSTERED " +
                        "([ID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                        ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]" +

                   "END ";

            return sql;
        }
        ///<summary>Update1248() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1252()
        {
            string sql = string.Empty;

            sql = "IF COL_LENGTH('Userberechtigungen','access_StKV') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Userberechtigungen] ADD [access_StKV] [bit] DEFAULT ((0)) NOT NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1253() / clsUpdate</summary>
        ///<remarks>Tabelle DocScan Spalte ScanFilename </remarks>
        private string Update1253()
        {
            string sql = string.Empty;

            sql = "IF ((SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DocScan' AND COLUMN_NAME = 'ScanFilename')='varchar') AND " +
                     "((SELECT CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DocScan' AND COLUMN_NAME = 'ScanFilename')>-1)" +
                  "BEGIN " +
                     "ALTER TABLE [DocScan] ALTER COLUMN [ScanFilename] [varchar](MAX); " +
                  "END ";
            return sql;
        }
        ///<summary>Update1254() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1254()
        {
            string sql = string.Empty;

            sql = "IF COL_LENGTH('Abrufe','Status') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Abrufe] ADD [Status] [nvarchar] (50) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Abrufe','LiefAdrID') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Abrufe] ADD [LiefAdrID] [int]  DEFAULT ((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Abrufe','EmpAdrID') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Abrufe] ADD [EmpAdrID] [int]  DEFAULT ((0)) NOT NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1254() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1255()
        {
            string sql = string.Empty;

            sql = "IF COL_LENGTH('Gueterart','IsStackable') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Gueterart] ADD [IsStackable] [bit] DEFAULT ((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Artikel','Abladestelle') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Artikel] ADD [Abladestelle] [nvarchar] (254) NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1256() / clsUpdate</summary>
        ///<remarks>Feld VDAProduktionsnummer</remarks>
        private string Update1256()
        {
            string sql = string.Empty;

            sql = "IF COL_LENGTH('Artikel','ASNProduktionsnummer') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Artikel] ADD [ASNProduktionsnummer] [nvarchar] (254) DEFAULT (('')) NOT NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1256() / clsUpdate</summary>
        ///<remarks>Feld VDAProduktionsnummer</remarks>
        private string Update1257()
        {
            string sql = string.Empty;

            sql = "IF COL_LENGTH('LAusgang','IsPrintList') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [LAusgang] ADD [IsPrintList] [bit] DEFAULT ((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('LEingang','IsPrintList') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [IsPrintList] [bit] DEFAULT ((0)) NOT NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1258() / clsUpdate</summary>
        ///<remarks>Update der Datenbank mit den enuen Feldern</remarks>
        private string Update1258()
        {
            string sql = string.Empty;

            sql = "IF COL_LENGTH('LAusgang','IsPrintList') IS NOT NULL " +
                  "BEGIN " +
                     "Update LAusgang SET IsPrintList=1 WHERE Checked=1; " +
                  "END " +
                  "IF COL_LENGTH('LEingang','IsPrintList') IS NOT NULL " +
                  "BEGIN " +
                    "Update LEingang SET IsPrintList=1 WHERE [Check]=1; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1259() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1259()
        {
            string sql = string.Empty;

            sql = "IF COL_LENGTH('Gueterart','UseProdNrCheck') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Gueterart] ADD [UseProdNrCheck] [bit] DEFAULT ((1)) NOT NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1260() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1260()
        {
            string sql = string.Empty;

            sql = "IF COL_LENGTH('Tarife','Zahlungsziel') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Tarife] ADD [Zahlungsziel] [Int] NULL; " +
                  "END " +
                  "IF COL_LENGTH('Tarife','ZZText') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [Tarife] ADD [ZZText] [nvarchar] (254) NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1261() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1261()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportDocSetting]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[ReportDocSetting](" +
                        "[ID] [int] IDENTITY(1,1) NOT NULL," +
                        "[DocKey] [nvarchar](50) NULL," +
                        "[ViewID] [nvarchar](50) NULL," +
                        "[DocPath] [nvarchar](254) NULL," +
                        "[PrintCount] [int] NULL CONSTRAINT [DF_ReportDoc_PrintCount]  DEFAULT ((0))," +
                        "[Art] [nvarchar](50) NULL," +
                        "[activ] [bit] NULL CONSTRAINT [DF_ReportDocSetting_activ]  DEFAULT ((0))," +
                        "CONSTRAINT [PK_ReportDoc] PRIMARY KEY CLUSTERED ([ID] ASC" +
                        ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                        ") ON [PRIMARY]  " +
                   "END ";

            return sql;
        }
        ///<summary>Update1262() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1262()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportDocSettingAssignment]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE[dbo].[ReportDocSettingAssignment]( " +
                        "[ID][int] IDENTITY(1, 1) NOT NULL," +
                        "[RepDocSetID] [int] NOT NULL CONSTRAINT[DF_ReportDocSettingAssignment_RepDocSetID] DEFAULT((0))," +
                        "[DocKey] [nvarchar](50) NULL," +
                        "[Path] [nvarchar](254) NULL, " +
                        "[ReportFileName] [nvarchar](254) NULL," +
                        "[IsDefault] [bit] NULL CONSTRAINT[DF_ReportDocSettingAssignment_IsDefault]  DEFAULT((0))," +
                        "[AdrID] [decimal](18, 0) NULL CONSTRAINT[DF_ReportDocSettingAssignment_AdrID]  DEFAULT((0))," +
                        "[AbBereichID] [decimal](18, 0) NULL, CONSTRAINT[PK_ReportDocSettingAssignment] PRIMARY KEY CLUSTERED" +
                        "([ID] ASC )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                        ") ON[PRIMARY] " +
                        " ALTER TABLE[dbo].[ReportDocSettingAssignment] " +
                        "WITH CHECK ADD CONSTRAINT[FK_ReportDocSettingAssignment_ReportDocSetting] FOREIGN KEY([RepDocSetID]) " +
                        " REFERENCES[dbo].[ReportDocSetting] ([ID]) " +
                        " ON DELETE CASCADE " +
                        "ALTER TABLE[dbo].[ReportDocSettingAssignment] CHECK CONSTRAINT[FK_ReportDocSettingAssignment_ReportDocSetting] " +
                   "END ";
            return sql;
        }
        ///<summary>Update1263() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1263()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ReportDocSetting','DocPath') IS NOT NULL " +
                  "BEGIN " +
                      "ALTER TABLE [ReportDocSetting] DROP COLUMN [DocPath];" +
                  "END " +
                  "IF COL_LENGTH('ReportDocSetting','DocKeyID') IS NULL " +
                  "BEGIN " +
                      "ALTER TABLE [ReportDocSetting] ADD [DocKeyID] [int] null; " +
                  "END " +
                  "IF COL_LENGTH('ReportDocSettingAssignment','DocKeyID') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ReportDocSettingAssignment] ADD [DocKeyID] [int]  ; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1264() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1264()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('TarifPositionen','CalcModus') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [TarifPositionen] ADD [CalcModus] [int] DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('RGPositionen','CalcModus') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [RGPositionen] ADD [CalcModus] [int] DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('RGPositionen','CalcModValue') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [RGPositionen] ADD [CalcModValue] [int] DEFAULT((0)) NOT NULL; " +
                 "END ";
            return sql;
        }
        ///<summary>Update1265() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1265()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Tarife','ZZTextEdit') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Tarife] ADD [ZZTextEdit] [nvarchar] (254) NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1266() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1266()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('RGPositionen','Menge') IS NOT NULL " +
                  "BEGIN " +
                    "ALTER TABLE [RGPositionen] ALTER COLUMN [Menge] [decimal] (18,3); " +
                  "END " +
                  "IF COL_LENGTH('RGPositionen','Anfangsbestand') IS NOT NULL " +
                  "BEGIN " +
                    "ALTER TABLE [RGPositionen] ALTER COLUMN [Anfangsbestand] [decimal] (18,3); " +
                  "END " +
                  "IF COL_LENGTH('RGPositionen','Abgang') IS NOT NULL " +
                  "BEGIN " +
                    "ALTER TABLE [RGPositionen] ALTER COLUMN [Abgang] [decimal] (18,3); " +
                  "END " +
                  "IF COL_LENGTH('RGPositionen','Zugang') IS NOT NULL " +
                  "BEGIN " +
                    "ALTER TABLE [RGPositionen] ALTER COLUMN [Zugang] [decimal] (18,3); " +
                  "END " +
                  "IF COL_LENGTH('RGPositionen','Endbestand') IS NOT NULL " +
                  "BEGIN " +
                    "ALTER TABLE [RGPositionen] ALTER COLUMN [Endbestand] [decimal] (18,3); " +
                  "END ";
            return sql;
        }
        ///<summary>Update1266() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1267()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LEingang','Ship') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [Ship] [nvarchar] (100) NULL; " +
                  "END " +
                  "IF COL_LENGTH('LEingang','IsShip') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [IsShip] [bit] DEFAULT ((0)) NOT NULL; " +
                  "END ";

            return sql;
        }
        ///<summary>Update1268() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1268()
        {
            string sql = string.Empty;
            sql = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArbeitsbereichTarif]') AND type in (N'U')) " +
                  "BEGIN " +
                        "ALTER TABLE[dbo].[ArbeitsbereichTarif] DROP CONSTRAINT[FK_ArbeitsbereichTarif_Tarife]  " +
                        "ALTER TABLE[dbo].[ArbeitsbereichTarif] DROP CONSTRAINT[FK_ArbeitsbereichTarif_ArbeitsbereichTarif]  " +

                        "ALTER TABLE[dbo].[ArbeitsbereichTarif]  WITH CHECK ADD CONSTRAINT[FK_ArbeitsbereichTarif_ArbeitsbereichTarif] FOREIGN KEY([AbBereichID]) " +
                        "REFERENCES[dbo].[Arbeitsbereich]([ID]) " +
                        "ON UPDATE CASCADE " +
                        "ON DELETE CASCADE " +
                        "ALTER TABLE[dbo].[ArbeitsbereichTarif] CHECK CONSTRAINT[FK_ArbeitsbereichTarif_ArbeitsbereichTarif] " +

                        "ALTER TABLE[dbo].[ArbeitsbereichTarif]  WITH CHECK ADD CONSTRAINT[FK_ArbeitsbereichTarif_Tarife] FOREIGN KEY([TarifID]) " +
                        "REFERENCES[dbo].[Tarife]([ID]) " +
                        "ON UPDATE CASCADE " +
                        "ON DELETE CASCADE " +
                        "ALTER TABLE[dbo].[ArbeitsbereichTarif] CHECK CONSTRAINT[FK_ArbeitsbereichTarif_Tarife] " +
                   "END ";
            return sql;
        }
        ///<summary>Update1269() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1269()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Artikel','IsStackable') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Artikel] ADD [IsStackable] [bit] DEFAULT ((1)) NOT NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1270() / clsUpdate</summary>
        ///<remarks></remarks>
        private string Update1270()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADRVerweis','UseS712F04') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ADRVerweis] ADD [UseS712F04] [bit] DEFAULT ((1)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('ADRVerweis','UseS713F13') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ADRVerweis] ADD [UseS713F13] [bit] DEFAULT ((1)) NOT NULL; " +
                  "END ";
            return sql;
        }
        ///<summary>Update1271() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1271()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MailingListCombiAdr]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE[dbo].[MailingListCombiAdr]( " +
                        "[ID][int] IDENTITY(1, 1) NOT NULL, " +
                        "[MailingListID] [int] NOT NULL, " +
                        "[AdrID] [int] NOT NULL, " +
                        "CONSTRAINT[PK_MailingListCombiAdr] PRIMARY KEY CLUSTERED([ID] ASC " +
                        ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                        ") ON[PRIMARY]" +
                   "END ";
            return sql;
        }
        ///<summary>Update1272() / clsUpdate</summary>
        ///<remarks>/remarks>
        private string Update1272()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Abrufe','SpedAdrId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Abrufe] ADD [SpedAdrId] [int] DEFAULT((0)) NOT NULL; " +
                  "END ";
            return sql;
        }
        /// <summary>
        ///            Update1273() -  
        /// </summary>
        /// <returns></returns>
        private string Update1273()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ReportDocSetting','CanUseTxtModul') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ReportDocSetting] ADD [CanUseTxtModul] [bit] DEFAULT((0)) NOT NULL; " +
                  "END ";
            return sql;
        }
        /// <summary>
        ///            Update1274() -  Trailer
        /// </summary>
        /// <returns></returns>
        private string Update1274()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LAusgang','Trailer') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LAusgang] ADD [Trailer]  [nvarchar] (20) NULL; " +
                  "END ";
            return sql;
        }
        /// <summary>
        ///            Update1275() -  Schicht -> einigen Tabellen fehlt die Spalte Schicht in Table Abrufe >>> nachtrag
        /// </summary>
        /// <returns></returns>
        private string Update1275()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Abrufe','Schicht') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Abrufe] ADD [Schicht]  [nvarchar] (100) NOT NULL; " +
                  "END ";
            return sql;
        }
        /// <summary>
        ///             Spalte RGText ändern in Typ [Text]
        /// </summary>
        /// <returns></returns>
        private string Update1276()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('RGPositionen','RGText') IS NOT NULL " +
                  "BEGIN " +
                    "ALTER TABLE [RGPositionen] ALTER COLUMN [RGText] [text] NULL; " +
                  "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Update1277()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArbeitsbereichDefaultValue]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[ArbeitsbereichDefaultValue](" +
                        "[ID][decimal](22, 0) IDENTITY(1, 1) NOT NULL," +
                        "[AbBereichID] [decimal](22, 0) NULL," +
                        "[Property] [nvarchar] (100) NULL," +
                        "[Value] [nvarchar] (250) NULL," +
                        " CONSTRAINT[PK_ArbeitsbereichDefaultValue] PRIMARY KEY CLUSTERED([ID] ASC" +
                        ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                        ") ON[PRIMARY] " +

                        "ALTER TABLE[dbo].[ArbeitsbereichDefaultValue] WITH CHECK ADD CONSTRAINT[FK_ArbeitsbereichDefaultValue_Arbeitsbereich] FOREIGN KEY([AbBereichID]) " +
                        "REFERENCES[dbo].[Arbeitsbereich] ([ID]) " +
                        "ON UPDATE CASCADE " +
                        "ON DELETE CASCADE " +
                        "ALTER TABLE[dbo].[ArbeitsbereichDefaultValue] " +
                        "CHECK CONSTRAINT[FK_ArbeitsbereichDefaultValue_Arbeitsbereich] " +
                   "END ";
            return sql;
        }
        /// <summary>
        ///             Spalte RGText ändern in Typ [Text]
        /// </summary>
        /// <returns></returns>
        private string Update1278()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Version','LastUpdate') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Version] ADD [LastUpdate] [Datetime] NULL; " +
                  "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Update1279()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADRVerweis','SenderVerweis') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [ADRVerweis] ADD [SenderVerweis] [nvarchar] (30) Default('') NOT NULL;" +
                   "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Update1280()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Tarife','RGText') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [Tarife] ADD [RGText] [text] Default('') NOT NULL;" +
                   "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Update1281()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ReportDocSettingAssignment','MandantenID') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [ReportDocSettingAssignment] ADD [MandantenID] [decimal] (18, 0) DEFAULT((0)) NOT NULL;" +
                   "END " +
                   "IF COL_LENGTH('Mandanten','ReportPath') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [Mandanten] ADD [ReportPath]  [nvarchar] (254) Default('') NOT NULL;" +
                   "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Update1282()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Arbeitsbereich','UseAutoRowAssignment') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [Arbeitsbereich] ADD [UseAutoRowAssignment] [bit] DEFAULT((0)) NOT NULL; " +
                   "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Update1283()
        {
            string sql = string.Empty;
            //sql = "IF COL_LENGTH('ADRVerweis','LieferantenID') IS NOT NULL " +
            //       "BEGIN " +
            //           "ALTER TABLE [ADRVerweis] DROP COLUMN [LieferantenID]; " +
            //       "END "+
            sql = "IF COL_LENGTH('ADRVerweis','SupplierNo') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [ADRVerweis] ADD [SupplierNo] [nvarchar] (100) NULL; " +
                   "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Update1284()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNArtFieldAssignment','AbBereichID') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [ASNArtFieldAssignment] ADD [AbBereichID] [int] DEFAULT((0)) NOT NULL; " +
                   "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Update1285()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNArtFieldAssignment','AbBereichID') IS NOT NULL " +
                   "BEGIN " +
                       "Update ASNArtFieldAssignment SET AbBereichID=1 WHERE AbBereichID=0 ; " +
                   "END ";
            return sql;
        }
        /// <summary>
        ///             Spalte RGText ändern in Typ [Text]
        /// </summary>
        /// <returns></returns>
        private string Update1286()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LAusgang','LfsNr') IS NOT NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LAusgang] ALTER COLUMN [LfsNr] [nvarchar] (50); " +
                  "END " +
                  "IF COL_LENGTH('LAusgang','LfsDate') IS NOT NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LAusgang] DROP COLUMN [LfsDate]; " +
                  "END ";
            return sql;
        }
        /// <summary>
        ///             Spalte RGText ändern in Typ [Text]
        /// </summary>
        /// <returns></returns>
        private string Update1287()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Gueterart','tmpLiefVerweis') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Gueterart] ADD [tmpLiefVerweis] [nvarchar] (50) NULL; " +
                  "END ";
            return sql;
        }
    }
}
