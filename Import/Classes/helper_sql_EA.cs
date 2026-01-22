using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using Import.Enumerations;
using LVS;

namespace Import
{
    public class helper_sql_EA
    {
        public static EA GetEA(clsSystemImport mySysImport, string myEANr)
        {
            EA EAlt = new EA();
            string strSql = string.Empty;
            strSql = "SELECT * FROM EA   where XNR='" + myEANr + "' ;";
            DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, mySysImport.GLUser.User_ID, "EA");
            foreach (DataRow row in dt.Rows)
            {
                //---EA Daten
                EAlt = new EA();
                EAlt.XNR = row["XNR"].ToString().Trim();
                EAlt.XDAT = Convert.ToDateTime(row["XDAT"].ToString().Trim());
                EAlt.KUNR = row["KUNR"].ToString().Trim();
                EAlt.VENR = row["VENR"].ToString().Trim();
                EAlt.SPED = row["SPED"].ToString().Trim();
                EAlt.FZ = row["FZ"].ToString().Trim();
                EAlt.LSNR = row["LSNR"].ToString().Trim();
                EAlt.LIEFERNR = row["LIEFERNR"].ToString().Trim();
                EAlt.SLB = row["SLB"].ToString().Trim();
                EAlt.XCHECK = row["XCHECK"].ToString().Trim();

                EAlt.XCHECKDT = Globals.DefaultDateTimeMinValue;
                if (!row.IsNull("XCHECKDT"))
                {
                    EAlt.XCHECKDT = Convert.ToDateTime(row["XCHECKDT"].ToString().Trim());
                }
                EAlt.LTAG = Globals.DefaultDateTimeMinValue;
                if (!row.IsNull("LTAG"))
                {
                    EAlt.LTAG = Convert.ToDateTime(row["LTAG"].ToString().Trim());
                }


                //if (row["XCHECKDT"] != null)
                //{
                //    EAlt.XCHECKDT = Convert.ToDateTime(row["XCHECKDT"].ToString().Trim());
                //}
                //if (row["LTAG"] != null)
                //{
                //    EAlt.LTAG = Convert.ToDateTime(row["LTAG"].ToString().Trim());
                //}
            }
            return EAlt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool UpdateEAADRDaten(Globals._GL_USER myGLUser, Enumerations.enumADRArt myAdrArt, string myAdrViewIDNew, string myAdrViewIDOld)
        {
            bool bReturn = false;
            string strSql = string.Empty;

            switch (myAdrArt)
            {
                case enumADRArt.Auftraggeber:
                    strSql = "Update EA SET KUNR='" + myAdrViewIDNew + "' WHERE LTRIM(RTRIM(KUNR))='" + myAdrViewIDOld + "';";
                    break;

                case enumADRArt.Empfänger:
                    strSql = "Update EA SET VENR='" + myAdrViewIDNew + "' WHERE LTRIM(RTRIM(VENR))='" + myAdrViewIDOld + "';";
                    break;
            }
            bReturn = clsSQLconImport.ExecuteSQLWithTRANSACTION(strSql, "UpdateArt", myGLUser.User_ID);
            return bReturn;
        }
    }
}
