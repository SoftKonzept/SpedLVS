using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LVS.ASN.vda4913
{
    public class Satz716
    {
        public const string const_Satzkennung_716 = "716";

        [Display(Name = "Satzart")]
        public string F01
        {
            get
            {
                return "716";
            }
        }

        [Display(Name = "Versionsnr")]
        public string F02
        {
            get
            {
                return "02";
            }
        }

        [Display(Name = "Text 1")]
        public string F03 { get; set; }

        [Display(Name = "Text 2")]
        public string F04 { get; set; }

        [Display(Name = "Text 3")]
        public string F05 { get; set; }

        [Display(Name = "Leer")]
        public string F06
        {
            get
            {
                return " ";
            }
        }


        public void InitClass(DataTable myDt)
        {
            DataTable dt = ExtractSatz716FromPool(myDt);
            if (dt.Rows.Count > 0)
            {
                int iCheckCount = 0;
                foreach (DataRow r in dt.Rows)
                {
                    if (iCheckCount < 2)
                    {
                        string Kennung = r["Kennung"].ToString();
                        string val = r["Value"].ToString();
                        switch (Kennung)
                        {
                            case clsASN.const_VDA4913SatzField_SATZ716F01:
                                iCheckCount++;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ716F03:
                                this.F03 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ716F04:
                                this.F04 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ716F05:
                                this.F05 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ716F06:
                                iCheckCount++;
                                break;

                        }//switch
                    }
                }//foreach

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mydt"></param>
        /// <returns></returns>
        private DataTable ExtractSatz716FromPool(DataTable mydt)
        {
            DataTable dtReturn = mydt.Copy();
            dtReturn.Clear();
            int iCheckCount = 0;

            DataRow[] results = mydt.Select("SatzKennung='" + const_Satzkennung_716 + "'");
            foreach (DataRow r in results)
            {
                if (iCheckCount < 2)
                {
                    string Kennung = r["Kennung"].ToString();
                    string val = r["Value"].ToString();
                    switch (Kennung)
                    {
                        case clsASN.const_VDA4913SatzField_SATZ716F01:
                            iCheckCount++;
                            dtReturn.ImportRow(r);
                            break;
                        case clsASN.const_VDA4913SatzField_SATZ716F03:
                        case clsASN.const_VDA4913SatzField_SATZ716F04:
                        case clsASN.const_VDA4913SatzField_SATZ716F05:
                            dtReturn.ImportRow(r);
                            break;
                        case clsASN.const_VDA4913SatzField_SATZ716F06:
                            dtReturn.ImportRow(r);
                            iCheckCount++;
                            break;

                    }//switch
                }
            }//foreeach
            return dtReturn;
        }
        /// <summary>
        ///             gibt den Property-Value eines Feldes zurück
        /// </summary>
        /// <param name="myASNField"></param>
        /// <returns></returns>
        public string GetValue(string myASNField)
        {
            string strReturn = string.Empty;
            switch (myASNField)
            {
                case clsASN.const_VDA4913SatzField_SATZ716F01:
                    strReturn = this.F01;
                    break;
                case clsASN.const_VDA4913SatzField_SATZ716F02:
                    strReturn = this.F02;
                    break;
                case clsASN.const_VDA4913SatzField_SATZ716F03:
                    strReturn = this.F03;
                    break;
                case clsASN.const_VDA4913SatzField_SATZ716F04:
                    strReturn = this.F04;
                    break;
                case clsASN.const_VDA4913SatzField_SATZ716F05:
                    strReturn = this.F05;
                    break;
                case clsASN.const_VDA4913SatzField_SATZ716F06:
                    strReturn = this.F06;
                    break;

            }
            return strReturn;
        }
        /// <summary>
        ///             Prüft anhand der Kennung, ob das Property zu dieser KLasse gehört
        /// </summary>
        /// <param name="myASNField"></param>
        /// <returns></returns>
        public bool IsProptertyMember(string myASNField)
        {
            bool bReturn = false;
            switch (myASNField)
            {
                case clsASN.const_VDA4913SatzField_SATZ716F01:
                case clsASN.const_VDA4913SatzField_SATZ716F02:
                case clsASN.const_VDA4913SatzField_SATZ716F03:
                case clsASN.const_VDA4913SatzField_SATZ716F04:
                case clsASN.const_VDA4913SatzField_SATZ716F05:
                case clsASN.const_VDA4913SatzField_SATZ716F06:
                    bReturn = true;
                    break;

            }
            return bReturn;
        }

    }
}
