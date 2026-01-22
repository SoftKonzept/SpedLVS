using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class helper_sql_clsLEingang
    {
        public static bool UpdateEingangImport(clsSystemImport mySysImport, clsLEingang myEingang)
        {
            string strSql = string.Empty;
            strSql = "Update LEingang SET " +
                            "EAIDaltLVS ='" + myEingang.EAIDalteLVS + "'" +
                            ", [Check] =" + Convert.ToInt32(myEingang.Checked) +
                            " WHERE ID = " + myEingang.LEingangTableID;

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
        public static clsLEingang AddEingang(clsSystemImport mySysImport
                                            , EA EAlt
                                            , clsLEingang myCls
                                            , clsADR adrAuftraggerber
                                            , clsADR adrEmpfägner
                                            , clsADR adrVersender
                                            )
        {
            myCls.LEingangDate = (DateTime)EAlt.XDAT;
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
            myCls.LEingangLfsNr = EAlt.LSNR.Trim();
            myCls.ASN = 0;
            myCls.ASNRef = string.Empty; // "DFÜ-" + EAlt.DFUENR.ToString();
            myCls.SpedID = 0;
            myCls.KFZ = EAlt.FZ.Trim();
            myCls.DirektDelivery = false;
            myCls.Retoure = false;
            myCls.Vorfracht = false;
            myCls.EAIDalteLVS = EAlt.XNR.Trim();
            myCls.LagerTransport = false;
            myCls.WaggonNr = string.Empty;
            myCls.BeladeID = adrAuftraggerber.ID;
            myCls.EntladeID = 0;
            if ((adrEmpfägner is clsADR) && (adrEmpfägner.ID > 0))
            {
                myCls.EntladeID = adrEmpfägner.ID;
            }
            myCls.IsPrintDoc = true;
            myCls.IsPrintAnzeige = true;
            myCls.IsPrintLfs = true;
            myCls.ExTransportRef = EAlt.SLB.Trim();
            myCls.LockedBy = 0;
            myCls.IsWaggon = false;
            myCls.Fahrer = EAlt.FAHRER;
            myCls.IsPrintList = false;

            if (myCls.AddLagerEingang())
            {
                myCls.Checked = false;
                if ((EAlt.XCHECK != null) && (EAlt.XCHECK.Equals("Y")))
                {
                    myCls.Checked = true;
                }
            }
            else
            {
                myCls = new clsLEingang();
            }
            return myCls;
        }
    }
}
