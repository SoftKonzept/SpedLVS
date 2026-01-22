using LVS;
using System;
namespace Sped4.Classes
{
    class clsDebitorDefaultNo
    {
        public Globals._GL_USER GL_User;
        public Globals._GL_SYSTEM GL_System;

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



        /********************************************************************************************************
         *                                  Methoden
         * *****************************************************************************************************/
        ///<summary>clsDebitorDefaultNo / GetDebitorDefaultNoByName</summary>
        ///<remarks>Die Standart Kontonummer auf Basis des Anfangsbuchstabens</remarks>
        public static Int32 GetDebitorDefaultNoByName(string strName, decimal decBenutzerID)
        {
            string strSQL = string.Empty;
            Int32 iTmp = 0;
            if (strName.Length > 0)
            {
                strName.Substring(0, 1);
                strSQL = "Select Value FROM DebitorDefaultNo WHERE [Key]='" + strName.Substring(0, 1) + "';";
                Int32.TryParse(clsSQLcon.ExecuteSQL_GetValue(strSQL, decBenutzerID), out iTmp);
            }
            return iTmp;
        }

    }
}
