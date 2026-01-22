using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class helper_sql_clsLAusgang
    {
        public static bool UpdateAusgangImport(clsSystemImport mySysImport, clsLAusgang myAusgang)
        {
            string strSql = string.Empty;
            strSql = "Update LAusgang SET " +
                            "EAIDaltLVS ='" + myAusgang.EAIDalteLVS + "'" +
                            ", [Checked] =" + Convert.ToInt32(myAusgang.Checked) +
                            ", Netto = (SELECT SUM(Netto) FROM Artikel WHERE LAusgangTableID ="+ myAusgang.LAusgangTableID+") " +
                            ", Brutto = (SELECT SUM(Brutto) FROM Artikel WHERE LAusgangTableID =" + myAusgang.LAusgangTableID+") " +
                             " WHERE ID = " + myAusgang.LAusgangTableID;

            bool bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "UpdateEingang", mySysImport.GLUser.User_ID);
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySysImport"></param>
        /// <param name="EAlt"></param>
        /// <param name="myCls"></param>
        /// <param name="adrEmpfägner"></param>
        /// <param name="adrVersender"></param>
        /// <returns></returns>
        public static clsLAusgang AddAusgang(clsSystemImport mySysImport
                                            , EA EAlt
                                            , clsLAusgang myCls
                                            , clsADR adrAuftraggerber
                                            , clsADR adrEmpfägner
                                            , clsADR adrVersender
                                            )
        {
            myCls.LAusgangsDate = (DateTime)EAlt.XDAT;
            myCls.Auftraggeber = 0;
            if ((adrAuftraggerber is clsADR) && (adrAuftraggerber.ID > 0))
            {
                myCls.Auftraggeber = adrAuftraggerber.ID;
            }
            myCls.Empfaenger = 0;
            if ((adrEmpfägner is clsADR) && (adrEmpfägner.ID > 0))
            {
                myCls.Empfaenger = adrEmpfägner.ID;
            }
            myCls.Versender = 0;
            if ((adrVersender is clsADR) && (adrVersender.ID > 0))
            {
                myCls.Versender = adrVersender.ID;
            }
            myCls.Lieferant = EAlt.LIEFERNR.Trim();
            decimal decTmp = 0;
            //Decimal.TryParse(EAlt.LSNR.Replace("A", "").Trim(), out decTmp);
            //myCls.LfsNr = decTmp;
            //myCls.LfsDate = Convert.ToDateTime(EAlt.XCHECKDT);
            decTmp = 0;
            Decimal.TryParse(EAlt.SLB.Replace("A", "").Trim(), out decTmp);
            myCls.SLB = decTmp;
            myCls.MAT = EAlt.MAT.ToString();
            myCls.Checked = (EAlt.XCHECK == "Y");
            myCls.SpedID = 0;
            myCls.KFZ = EAlt.FZ.Trim();
            myCls.ASN = 0;
            myCls.Info = "Import aus LVS";
            myCls.Termin = Convert.ToDateTime(EAlt.LTAG);
            myCls.KFZ = EAlt.FZ;
            myCls.WaggonNr = string.Empty; 
            myCls.LockedBy = 0;
            myCls.IsWaggon = false;
            myCls.Fahrer = EAlt.FAHRER;
            myCls.IsPrintList = false;
            myCls.EAIDalteLVS = EAlt.XNR;

            if (myCls.AddLAusgang())
            {
                myCls.Checked = false;
                if ((EAlt.XCHECK != null) && (EAlt.XCHECK.Equals("Y")))
                {
                    myCls.Checked = true;
                }

                //if (helper_sql_clsLAusgang.UpdateAusgangImport(mySysImport, myCls))
                //{

                //}
            }
            else
            {
                myCls = new clsLAusgang();
            }
            return myCls;
        }
    }
}
