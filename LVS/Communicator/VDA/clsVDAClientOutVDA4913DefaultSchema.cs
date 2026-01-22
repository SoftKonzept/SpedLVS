using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LVS
{
    public class clsVDAClientOutVDA4913DefaultSchema
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<clsVDAClientValue> GeVDA4913Default()
        {
            List<clsVDAClientValue> tmpList = new List<clsVDAClientValue>();
            tmpList.AddRange(Get711());
            tmpList.AddRange(Get712());
            tmpList.AddRange(Get713());
            tmpList.AddRange(Get714());
            tmpList.AddRange(Get715());
            tmpList.AddRange(Get716());
            tmpList.AddRange(Get717());
            tmpList.AddRange(Get718());
            tmpList.AddRange(Get719());
            return tmpList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<clsVDAClientValue> Get711()
        {
            List<clsVDAClientValue> tmpList = new List<clsVDAClientValue>();

            clsVDAClientValue s711 = null;

            for (int i = 1; i <= 12; i++)
            {
                s711 = new clsVDAClientValue();
                s711.ASNFieldID = i;
                s711.ValueArt = clsVDACreate.const_VDA_Value_Blanks;
                s711.Value = string.Empty;
                s711.Fill0 = true;
                s711.aktiv = true;
                s711.NextSatz = 0;
                s711.IsArtSatz = false;
                s711.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                s711.FillLeft = false;

                switch (i)
                {
                    case 1:
                        s711.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s711.Value = "711";
                        s711.Fill0 = false;
                        s711.NextSatz = 712;
                        break;
                    case 2:
                        s711.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s711.Value = "02";
                        s711.Fill0 = false;
                        break;

                    case 3:
                        s711.ValueArt = clsVDACreate.const_Lieferantennummer;
                        s711.Fill0 = true;
                        break;

                    case 4:
                        s711.ValueArt = clsVDACreate.const_Sender;
                        s711.Value = "0";
                        s711.FillLeft = true;
                        break;

                    case 5:
                        s711.ValueArt = clsVDACreate.const_SIDOld;
                        s711.Value = "0";
                        s711.FillLeft = true;
                        break;

                    case 6:
                        s711.ValueArt = clsVDACreate.const_SIDNew;
                        s711.Value = "0";
                        s711.FillLeft = true;
                        break;

                    case 7:
                        s711.ValueArt = clsVDACreate.const_VDA_Value_NOW;
                        break;

                    default:
                        s711.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                        break;
                }
                tmpList.Add(s711);
            }
            return tmpList;
        }
        /// <summary>
        ///                 Satz 712
        /// </summary>
        /// <returns></returns>
        public static List<clsVDAClientValue> Get712()
        {
            List<clsVDAClientValue> tmpList = new List<clsVDAClientValue>();

            clsVDAClientValue s71_er = null;

            for (int i = 13; i <= 33; i++)
            {
                s71_er = new clsVDAClientValue();
                s71_er.ASNFieldID = i;
                s71_er.ValueArt = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.Value = string.Empty;
                s71_er.Fill0 = true;
                s71_er.aktiv = true;
                s71_er.NextSatz = 0;
                s71_er.IsArtSatz = false;
                s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.FillLeft = false;

                switch (i)
                {
                    case 13:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "712";
                        s71_er.Fill0 = false;
                        s71_er.NextSatz = 713;
                        break;
                    case 14:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "02";
                        s71_er.Fill0 = false;
                        break;
                    default:
                        s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                        break;
                }
                tmpList.Add(s71_er);
            }
            return tmpList;
        }
        /// <summary>
        ///                 Satz 713 
        /// </summary>
        /// <returns></returns>
        public static List<clsVDAClientValue> Get713()
        {
            List<clsVDAClientValue> tmpList = new List<clsVDAClientValue>();

            clsVDAClientValue s71_er = null;

            for (int i = 34; i <= 54; i++)
            {
                s71_er = new clsVDAClientValue();
                s71_er.ASNFieldID = i;
                s71_er.ValueArt = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.Value = string.Empty;
                s71_er.Fill0 = true;
                s71_er.aktiv = true;
                s71_er.NextSatz = 0;
                s71_er.IsArtSatz = false;
                s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.FillLeft = false;

                switch (i)
                {
                    case 34:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "713";
                        s71_er.Fill0 = false;
                        s71_er.NextSatz = 714;
                        s71_er.IsArtSatz = true;
                        break;
                    case 35:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "02";
                        s71_er.Fill0 = false;
                        break;
                    case 36:
                        s71_er.ValueArt = clsVDACreate.const_Eingang_LfsNr;     
                        s71_er.Fill0 = true;
                        break;
                    case 37:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_NOW;
                        break;
                    case 41:
                        s71_er.ValueArt = clsVDACreate.const_Artikel_BestellNr;
                        break;
                    case 42:
                        s71_er.ValueArt = clsVDACreate.const_Vorgang;
                        break;

                    default:
                        s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                        break;

                }
                tmpList.Add(s71_er);
            }
            return tmpList;
        }
        /// <summary>
        ///             SAtz 714
        /// </summary>
        /// <returns></returns>
        public static List<clsVDAClientValue> Get714()
        {
            List<clsVDAClientValue> tmpList = new List<clsVDAClientValue>();

            clsVDAClientValue s71_er = null;

            for (int i = 55; i <= 76; i++)
            {
                s71_er = new clsVDAClientValue();
                s71_er.ASNFieldID = i;
                s71_er.ValueArt = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.Value = string.Empty;
                s71_er.Fill0 = true;
                s71_er.aktiv = true;
                s71_er.NextSatz = 0;
                s71_er.IsArtSatz = false;
                s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.FillLeft = false;

                switch (i)
                {
                    case 55:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "714";
                        s71_er.Fill0 = false;
                        s71_er.NextSatz = 715;
                        s71_er.IsArtSatz = true;
                        break;
                    case 56:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "02";
                        s71_er.Fill0 = false;
                        break;
                    case 57:
                        s71_er.ValueArt = clsVDACreate.const_Artikel_Werksnummer;
                        break;
                    case 58:
                        s71_er.ValueArt = clsVDACreate.const_Artikel_Produktionsnummer;
                        break;
                    case 60:
                        s71_er.ValueArt = clsVDACreate.const_Artikel_Brutto;
                        break;
                    case 61:
                        s71_er.ValueArt = clsVDACreate.const_Artikel_Einheit;
                        break;
                    case 62:
                        s71_er.ValueArt = clsVDACreate.const_Artikel_Netto;
                        break;
                    case 63:
                        s71_er.ValueArt = clsVDACreate.const_Artikel_Einheit;
                        break;
                    case 68:
                        s71_er.ValueArt = clsVDACreate.const_Artikel_Charge;
                        break;

                    default:
                        s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                        break;

                }
                tmpList.Add(s71_er);
            }
            return tmpList;
        }
        /// <summary>
        ///             SAtz 715
        /// </summary>
        /// <returns></returns>
        public static List<clsVDAClientValue> Get715()
        {
            List<clsVDAClientValue> tmpList = new List<clsVDAClientValue>();

            clsVDAClientValue s71_er = null;

            for (int i = 77; i <= 92; i++)
            {
                s71_er = new clsVDAClientValue();
                s71_er.ASNFieldID = i;
                s71_er.ValueArt = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.Value = string.Empty;
                s71_er.Fill0 = true;
                s71_er.aktiv = true;
                s71_er.NextSatz = 0;
                s71_er.IsArtSatz = false;
                s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.FillLeft = false;

                switch (i)
                {
                    case 77:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "715";
                        s71_er.Fill0 = false;
                        s71_er.NextSatz = 716;
                        s71_er.IsArtSatz = true;
                        break;
                    case 78:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "02";
                        s71_er.Fill0 = false;
                        break;

                    default:
                        s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                        break;

                }
                tmpList.Add(s71_er);
            }
            return tmpList;
        }
        /// <summary>
        ///             SAtzu 716
        /// </summary>
        /// <returns></returns>
        public static List<clsVDAClientValue> Get716()
        {
            List<clsVDAClientValue> tmpList = new List<clsVDAClientValue>();

            clsVDAClientValue s71_er = null;

            for (int i = 93; i <= 98; i++)
            {
                s71_er = new clsVDAClientValue();
                s71_er.ASNFieldID = i;
                s71_er.ValueArt = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.Value = string.Empty;
                s71_er.Fill0 = true;
                s71_er.aktiv = true;
                s71_er.NextSatz = 0;
                s71_er.IsArtSatz = false;
                s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.FillLeft = false;

                switch (i)
                {
                    case 93:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "716";
                        s71_er.Fill0 = false;
                        s71_er.NextSatz = 717;
                        s71_er.IsArtSatz = true;
                        break;
                    case 94:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "02";
                        s71_er.Fill0 = false;
                        break;

                    default:
                        s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                        break;

                }
                tmpList.Add(s71_er);
            }
            return tmpList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<clsVDAClientValue> Get717()
        {
            List<clsVDAClientValue> tmpList = new List<clsVDAClientValue>();

            clsVDAClientValue s71_er = null;

            for (int i = 99; i <= 107; i++)
            {
                s71_er = new clsVDAClientValue();
                s71_er.ASNFieldID = i;
                s71_er.ValueArt = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.Value = string.Empty;
                s71_er.Fill0 = true;
                s71_er.aktiv = true;
                s71_er.NextSatz = 0;
                s71_er.IsArtSatz = false;
                s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.FillLeft = false;

                switch (i)
                {
                    case 99:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "717";
                        s71_er.Fill0 = false;
                        s71_er.NextSatz = 718;
                        s71_er.IsArtSatz = true;
                        break;
                    case 100:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "02";
                        s71_er.Fill0 = false;
                        break;

                    default:
                        s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                        break;

                }
                tmpList.Add(s71_er);
            }
            return tmpList;
        }
        /// <summary>
        ///             Satz 718
        /// </summary>
        /// <returns></returns>
        public static List<clsVDAClientValue> Get718()
        {
            List<clsVDAClientValue> tmpList = new List<clsVDAClientValue>();

            clsVDAClientValue s71_er = null;

            for (int i = 108; i <= 122; i++)
            {
                s71_er = new clsVDAClientValue();
                s71_er.ASNFieldID = i;
                s71_er.ValueArt = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.Value = string.Empty;
                s71_er.Fill0 = true;
                s71_er.aktiv = true;
                s71_er.NextSatz = 0;
                s71_er.IsArtSatz = false;
                s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.FillLeft = false;

                switch (i)
                {
                    case 108:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "718";
                        s71_er.Fill0 = false;
                        s71_er.NextSatz = 0;
                        s71_er.IsArtSatz = true;
                        break;
                    case 109:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "02";
                        s71_er.Fill0 = false;
                        break;

                    default:
                        s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                        break;

                }
                tmpList.Add(s71_er);
            }
            return tmpList;
        }
        /// <summary>
        ///             SAtz 719
        /// </summary>
        /// <returns></returns>
        public static List<clsVDAClientValue> Get719()
        {
            List<clsVDAClientValue> tmpList = new List<clsVDAClientValue>();

            clsVDAClientValue s71_er = null;

            for (int i = 123; i <= 134; i++)
            {
                s71_er = new clsVDAClientValue();
                s71_er.ASNFieldID = i;
                s71_er.ValueArt = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.Value = string.Empty;
                s71_er.Fill0 = false;
                s71_er.aktiv = true;
                s71_er.NextSatz = 0;
                s71_er.IsArtSatz = false;
                s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                s71_er.FillLeft = false;

                switch (i)
                {
                    case 123:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "719";
                        s71_er.NextSatz = 0;
                        break;
                    case 124:
                        s71_er.ValueArt = clsVDACreate.const_VDA_Value_const;
                        s71_er.Value = "02";
                        break;

                    default:
                        s71_er.FillValue = clsVDACreate.const_VDA_Value_Blanks;
                        break;

                }
                tmpList.Add(s71_er);
            }
            return tmpList;
        }
    }
}
