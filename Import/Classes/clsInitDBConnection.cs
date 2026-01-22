using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LVS;
using Import;

namespace Import
{
    public class clsInitDBConnection
    {

        public static bool init_conLVS(ref Globals._GL_SYSTEM _GL_SYSTEM)
        {
            clsSQLcon.Server = _GL_SYSTEM.con_Server;
            clsSQLcon.Database = _GL_SYSTEM.con_Database;
            clsSQLcon.User = _GL_SYSTEM.con_UserDB;
            clsSQLcon.Password = _GL_SYSTEM.con_PassDB;

            clsSQLcon sql = new clsSQLcon();
            if (Globals.SQLcon.init() == false)
            {
                return false;
            }
            try
            {
                sql.init();
            }
            catch (Exception ex)
            {
                //decimal decUser = -1.0M;
                //Functions.AddLogbuch(decUser, "init_con", ex.ToString());
                //sql.Close();
                return false;
            }
            return true;
        }
        public static bool init_conLVSOld(ref Globals._GL_SYSTEM _GL_SYSTEM)
        {
            clsSQLconImport.Server = _GL_SYSTEM.con_Server_Imp;
            clsSQLconImport.Database = _GL_SYSTEM.con_Database_Imp;
            clsSQLconImport.User = _GL_SYSTEM.con_UserDB_Imp;
            clsSQLconImport.Password = _GL_SYSTEM.con_PassDB_Imp;

            clsSQLconImport sql = new clsSQLconImport();
            if (Globals.SQLconImp.init() == false)
            {
                return false;
            }
            try
            {
                sql.init();
            }
            catch (Exception ex)
            {
                //decimal decUser = -1.0M;
                //Functions.AddLogbuch(decUser, "init_con", ex.ToString());
                //sql.Close();
                return false;
            }
            return true;
        }

        public static bool init_conCOM(ref Globals._GL_SYSTEM _GL_SYSTEM)
        {
            clsSQLCOM.Server = _GL_SYSTEM.con_Server_COM;
            clsSQLCOM.Database = _GL_SYSTEM.con_Database_COM;
            clsSQLCOM.User = _GL_SYSTEM.con_UserDB_COM;
            clsSQLCOM.Password = _GL_SYSTEM.con_PassDB_COM;

            clsSQLCOM sql = new clsSQLCOM();
            if (Globals.SQLconCom.init() == false)
            {
                return false;
            }
            try
            {
                sql.init();
            }
            catch (Exception ex)
            {
                //decimal decUser = -1.0M;
                //Functions.AddLogbuch(decUser, "init_con", ex.ToString());
                //sql.Close();
                return false;
            }
            return true;
        }
    }
}
