using System;

namespace LVS
{
    public class helper_Job_ActionDate
    {
        /// <summary>
        ///             Es wird anhand der übergebenen Daten
        ///             Periode und aktuelles Aktionsdatum
        ///             das nächste neue Aktionsdatum ermittelt
        /// </summary>
        /// <param name="myPeriode"></param>
        /// <param name="myJobActionDate"></param>
        /// <returns></returns>

        public static DateTime GetNextActionDate(enumPeriode myPeriode, DateTime myJobActionDate)
        {
            DateTime dtActionDate = DateTime.Now;
            TimeSpan dtDiff = new TimeSpan();
            dtDiff = (dtActionDate - myJobActionDate);
            switch (myPeriode)
            {
                case enumPeriode.täglich:
                    if (dtDiff.Days > -1)
                    {
                        dtActionDate = myJobActionDate.AddDays(1);
                        while (dtActionDate < DateTime.Now)
                        {
                            dtActionDate = dtActionDate.AddDays(1);
                        }
                    }
                    else
                    {
                        dtActionDate = DateTime.Now.AddDays(1);
                    }
                    break;
                case enumPeriode.wöchtentlich:
                    if (dtDiff.Days > 7)
                    {
                        dtActionDate = myJobActionDate.AddDays(7);
                        while (dtActionDate < DateTime.Now)
                        {
                            dtActionDate = dtActionDate.AddDays(7);
                        }
                    }
                    else
                    {
                        dtActionDate = myJobActionDate.AddDays(7);
                    }
                    break;

                case enumPeriode.monatlich:
                    if (dtDiff.Days < 32)
                    {
                        while (dtActionDate < DateTime.Now)
                        {
                            dtActionDate = myJobActionDate.AddMonths(1);
                        }
                    }
                    else
                    {
                        dtActionDate = Convert.ToDateTime(myJobActionDate.Day.ToString() + "." + DateTime.Now.AddMonths(1).Month.ToString() + "." + DateTime.Now.AddMonths(1).Year.ToString());
                    }
                    break;

                case enumPeriode.jährlich:
                    if (dtDiff.Days < 366)
                    {
                        dtActionDate = myJobActionDate.AddYears(1);
                    }
                    else
                    {
                        dtActionDate = Convert.ToDateTime(myJobActionDate.Day.ToString() + "." + myJobActionDate.Month.ToString() + "." + DateTime.Now.AddYears(1).Year.ToString());
                    }
                    break;

                case enumPeriode.immer:
                    //this.ASN.Job.ActionDate = this.ASN.Job.ActionDate.AddDays(1);
                    break;
            }
            return dtActionDate;

        }
    }

}
