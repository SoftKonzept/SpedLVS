using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LVS.ASN.vda4913
{
    public class Satz715
    {
        [Display(Name = "Satzart")]
        public string F01
        {
            get
            {
                return "715";
            }
        }

        [Display(Name = "Versionsnr")]
        public string F02
        {
            get
            {
                return "03";
            }
        }

        [Display(Name = "Packmittel-Nummer Kunde")]
        public string F03 { get; set; }

        [Display(Name = "Packmittel-Nummer Lieferant")]
        public string F04 { get; set; }

        [Display(Name = "Anzahl Packmittel")]
        public string F05 { get; set; }

        [Display(Name = "Positions-Nummer Lieferschein")]
        public string F06 { get; set; }

        [Display(Name = "Füllmenge")]
        public string F07 { get; set; }

        [Display(Name = "Packstück-Nummer von")]
        public string F08 { get; set; }

        [Display(Name = "Packstück-Nummer bis")]
        public string F09 { get; set; }

        [Display(Name = "Verpackungsabmessung")]
        public string F10 { get; set; }

        [Display(Name = "Stapelfaktor")]
        public string F11 { get; set; }

        [Display(Name = "Lagerabruf-Nummer")]
        public string F12 { get; set; }

        [Display(Name = "Label-Kennung")]
        public string F13 { get; set; }

        [Display(Name = "Verpackungs-Kennung")]
        public string F14 { get; set; }

        [Display(Name = "Eigentums-Kennung")]
        public string F15 { get; set; }

        [Display(Name = "Leer")]
        public string F16
        {
            get
            {
                return " ";
            }
        }


        public void InitClass(DataTable myDt)
        {
            if (myDt.Rows.Count > 0)
            {
                int iCheckCount = 0;
                foreach (DataRow r in myDt.Rows)
                {
                    if (iCheckCount < 2)
                    {
                        string Kennung = r["Kennung"].ToString();
                        string val = r["Value"].ToString();
                        switch (Kennung)
                        {
                            case clsASN.const_VDA4913SatzField_SATZ715F01:
                                iCheckCount++;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F03:
                                this.F03 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F04:
                                this.F04 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F05:
                                this.F05 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F06:
                                this.F06 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F07:
                                this.F07 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F08:
                                this.F08 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F09:
                                this.F09 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F10:
                                this.F10 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F11:
                                this.F11 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F12:
                                this.F12 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F13:
                                this.F13 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F14:
                                this.F14 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F15:
                                this.F15 = val;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ715F16:
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
        /// <param name="myCheckVal"></param>
        /// <returns></returns>
        public bool CheckProductionNoRange(string myCheckVal)
        {
            bool bReturn = false;
            int iF08 = 0;
            int.TryParse(F08, out iF08);
            int iF09 = 0;
            int.TryParse(F09, out iF09);
            int iChk = 0;
            int.TryParse(myCheckVal, out iChk);


            if (
                (iF08 > 0) && (iF09 > 0) && (iF08 <= iF09) && (iChk >= iF08) && (iChk <= iF09)
               )
            {
                for (int x = iF08; x <= iF09; x++)
                {
                    if (x == iChk)
                    {
                        bReturn = true;
                    }
                }
            }
            return bReturn;
        }
    }
}
