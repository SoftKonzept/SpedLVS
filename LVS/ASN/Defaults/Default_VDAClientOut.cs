using Common.Models;
using LVS.ASN.EDIFACT.Defaults;
using LVS.Models;

namespace LVS.ASN.Defaults
{
    public class Default_VDAClientOut
    {
        public const string const_Const = clsEdiVDAValueAlias.const_VDA_Value_const;
        public const string const_BLANK = clsEdiVDAValueAlias.const_VDA_Value_Blanks;
        public const string const_Placeholder = "##PH##";
        public VDAClientValues VDAClientValues { get; set; }
        public EdiSegmentElementFields EdiSegmentElementFields { get; set; } = new EdiSegmentElementFields();
        public Default_VDAClientOut(EdiSegmentElementFields myEsef, Addresses myAdr, AsnArt myAsnArt)
        {
            EdiSegmentElementFields = myEsef.Copy();

            VDAClientValues = new VDAClientValues();
            VDAClientValues.AdrId = myAdr.Id;
            VDAClientValues.AsnFieldId = (int)EdiSegmentElementFields.Id; //
            VDAClientValues.ValueArt = "const";
            VDAClientValues.Value = string.Empty;
            VDAClientValues.Fill = false;
            VDAClientValues.Activ = true;
            VDAClientValues.NextSatz = 0;
            VDAClientValues.IsArtSatz = false;
            VDAClientValues.FillValue = clsEdiVDAValueAlias.const_VDA_Value_Blanks;
            VDAClientValues.FillLeft = false;
            VDAClientValues.ASNArtId = (int)myAsnArt.Id; ;
            VDAClientValues.Kennung = EdiSegmentElementFields.Kennung;
            VDAClientValues.Description = string.Empty;

            switch (myEsef.Kennung)
            {
                case "UNA | UNA | UNA":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = UNA.Name;
                    break;
                case "UNA | UNA1 | UNA1":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = UNA.const_Trennzeichen_Gruppe_Doppelpunkt.ToString();
                    break;
                case "UNA | UNA2 | UNA2":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = UNA.const_Trennzeichen_Segement_Plus;
                    break;
                case "UNA | UNA3 | UNA3":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = UNA.const_Dezimal;
                    break;
                case "UNA | UNA4 | UNA4":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = UNA.const_Fragezeichen;
                    break;
                case "UNA | UNA5 | UNA5":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = UNA.const_BLANK;
                    break;
                case "UNA | UNA6 | UNA6":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = UNA.const_BLANK;
                    break;



                case "UNB | UNB | UNB":
                    UNB_UNB_UNB_DefaultsValue vUNB = new UNB_UNB_UNB_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vUNB.ValueArt;
                    VDAClientValues.Value = vUNB.Value;
                    break;
                case "UNB | S001 | 0001":
                    UNB_S001_0001_DefaultsValue v0001 = new UNB_S001_0001_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0001.ValueArt;
                    VDAClientValues.Value = v0001.Value;
                    break;
                case "UNB | S001 | 0002":
                    UNB_S001_0002_DefaultsValue v0002 = new UNB_S001_0002_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0002.ValueArt;
                    VDAClientValues.Value = v0002.Value;
                    break;
                case "UNB | S002 | 0004":
                    UNB_S002_0004_DefaultsValue v0004 = new UNB_S002_0004_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0004.ValueArt;
                    VDAClientValues.Value = v0004.Value;
                    break;
                case "UNB | S002 | 0007":
                case "UNB | S002 | 0008":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;
                case "UNB | S003 | 0010":
                    UNB_S003_0010_DefaultsValue v0010 = new UNB_S003_0010_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0010.ValueArt;
                    VDAClientValues.Value = v0010.Value;
                    break;
                case "UNB | S003 | 0007":
                case "UNB | S003 | 0014":
                case "UNB | S003 | 0046":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;
                case "UNB | S004 | 0017":
                    UNB_S004_0017_DefaultsValue v0017 = new UNB_S004_0017_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0017.ValueArt;
                    VDAClientValues.Value = v0017.Value;
                    break;
                case "UNB | S004 | 0019":
                    UNB_S004_0019_DefaultsValue v0019 = new UNB_S004_0019_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0019.ValueArt;
                    VDAClientValues.Value = v0019.Value;
                    break;
                case "UNB | 0020 | 0020":
                    UNB_0020_0020_DefaultsValue v0020 = new UNB_0020_0020_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0020.ValueArt;
                    VDAClientValues.Value = v0020.Value;
                    break;
                case "UNB | 0026 | 0026":
                    UNB_0026_0026_DefaultsValue v0026 = new UNB_0026_0026_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0026.ValueArt;
                    VDAClientValues.Value = v0026.Value;
                    break;
                case "UNB | S005 | 0022":
                    UNB_S005_0022_DefaultsValue v0022 = new UNB_S005_0022_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0022.ValueArt;
                    VDAClientValues.Value = v0022.Value;
                    break;
                case "UNB | 0032 | 0032":
                case "UNB | S005 | 0025":
                case "UNB | 0029 | 0029":
                case "UNB | 0031 | 0031":
                case "UNB | 0035 | 0035":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;


                case "UNH | UNH | UNH":
                    UNH_UNH_UNH_DefaultsValue vUNH = new UNH_UNH_UNH_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vUNH.ValueArt;
                    VDAClientValues.Value = vUNH.Value;
                    break;
                case "UNH | 0062 | 0062":
                    UNH_0062_0062_DefaultsValue v0062 = new UNH_0062_0062_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0062.ValueArt;
                    VDAClientValues.Value = v0062.Value;
                    break;
                case "UNH | S009 | 0065":
                    UNH_S009_0065_DefaultsValue v0065 = new UNH_S009_0065_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0065.ValueArt;
                    VDAClientValues.Value = v0065.Value;
                    break;
                case "UNH | S009 | 0052":
                    UNH_S009_0052_DefaultsValue v0052 = new UNH_S009_0052_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0052.ValueArt;
                    VDAClientValues.Value = v0052.Value;
                    break;
                case "UNH | S009 | 0054":
                    UNH_S009_0054_DefaultsValue v0054 = new UNH_S009_0054_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0054.ValueArt;
                    VDAClientValues.Value = v0054.Value;
                    break;
                case "UNH | S009 | 0051":
                    UNH_S009_0051_DefaultsValue v0051 = new UNH_S009_0051_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0051.ValueArt;
                    VDAClientValues.Value = v0051.Value;
                    break;
                case "UNH | S009 | 0057":
                    UNH_S009_0057_DefaultsValue v0057 = new UNH_S009_0057_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v0057.ValueArt;
                    VDAClientValues.Value = v0057.Value;
                    break;
                case "UNH | 0068 | 0068":
                case "UNH | S010 | 0070":
                case "UNH | S010 | 0073":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;

                case "BGM | BGM | BGM":
                    BGM_BGM_BGM_DefaultsValue vBGM = new BGM_BGM_BGM_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vBGM.ValueArt;
                    VDAClientValues.Value = vBGM.Value;
                    //VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    //VDAClientValues.Value = UNH.Name;
                    break;
                case "BGM | C002 | 1001":
                    BGM_C002_1001_DefaultsValue v1001 = new BGM_C002_1001_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v1001.ValueArt;
                    VDAClientValues.Value = v1001.Value;
                    break;
                case "BGM | C002 | 1131":
                case "BGM | C002 | 3055":
                case "BGM | C002 | 1000":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;
                case "BGM | C106 | 1004":
                    BGM_C106_1004_DefaultsValue v1004 = new BGM_C106_1004_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v1004.ValueArt;
                    VDAClientValues.Value = v1004.Value;
                    break;
                case "BGM | C106 | 1056":
                case "BGM | C106 | 1060":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;
                case "BGM | 1225 | 1225":
                    BGM_1225_1225_DefaultsValue v1225 = new BGM_1225_1225_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = v1225.ValueArt;
                    VDAClientValues.Value = v1225.Value;
                    break;
                case "BGM | 4343 | 4343":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;


                case "DTM | DTM | DTM":
                    DTM_DTM_DTM_DefaultsValue vDTM = new DTM_DTM_DTM_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vDTM.ValueArt;
                    VDAClientValues.Value = vDTM.Value;
                    break;
                case "DTM | C507 | 2005":
                    DTM_C507_2005_DefaultsValue v2005 = new DTM_C507_2005_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v2005.ValueArt;
                    VDAClientValues.Value = v2005.Value;
                    break;
                case "DTM | C507 | 2380":
                    DTM_C507_2380_DefaultsValue v2380 = new DTM_C507_2380_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v2380.ValueArt;
                    VDAClientValues.Value = v2380.Value;
                    break;
                case "DTM | C507 | 2379":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = "203";
                    break;


                case "RFF | RFF | RFF":
                    RFF_RFF_RFF_DefaultsValue vRFF = new RFF_RFF_RFF_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vRFF.ValueArt;
                    VDAClientValues.Value = vRFF.Value;
                    break;
                case "RFF | C506 | 1153":
                    RFF_C506_1153_DefaultsValue v1153 = new RFF_C506_1153_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v1153.ValueArt;
                    VDAClientValues.Value = v1153.Value;
                    break;
                case "RFF | C506 | 1154":
                    RFF_C506_1154_DefaultsValue v1154 = new RFF_C506_1154_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v1154.ValueArt;
                    VDAClientValues.Value = v1154.Value;
                    break;
                case "RFF | C506 | 1156":
                    RFF_C506_1156_DefaultsValue v1156 = new RFF_C506_1156_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v1156.ValueArt;
                    VDAClientValues.Value = v1156.Value;
                    break;
                case "RFF | C506 | 4000":
                    RFF_C506_4000_DefaultsValue v4000 = new RFF_C506_4000_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v4000.ValueArt;
                    VDAClientValues.Value = v4000.Value;
                    break;


                case "NAD | NAD | NAD":
                    NAD_NAD_NAD_DefaultsValue vNAD = new NAD_NAD_NAD_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vNAD.ValueArt;
                    VDAClientValues.Value = vNAD.Value;
                    break;

                case "NAD | 3035 | 3035":
                    NAD_3035_3035_DefaultsValue v3035 = new NAD_3035_3035_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v3035.ValueArt;
                    VDAClientValues.Value = v3035.Value;
                    break;

                case "NAD | C082 | 3039":
                    NAD_C082_3039_DefaultsValue v3039 = new NAD_C082_3039_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v3039.ValueArt;
                    VDAClientValues.Value = v3039.Value;
                    break;
                case "NAD | C082 | 1131":
                    NAD_C082_1131_DefaultsValue v1131 = new NAD_C082_1131_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v1131.ValueArt;
                    VDAClientValues.Value = v1131.Value;
                    break;

                case "NAD | C082 | 3055":
                    NAD_C082_3055_DefaultsValue v3055 = new NAD_C082_3055_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v3055.ValueArt;
                    VDAClientValues.Value = v3055.Value;
                    break;

                case "NAD | C080 | 3036":
                    NAD_C080_3036_DefaultsValue v3036 = new NAD_C080_3036_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v3036.ValueArt;
                    VDAClientValues.Value = v3036.Value;
                    break;

                case "NAD | C058 | 3124":
                case "NAD | C059 | 3042":
                case "NAD | 3164 | 3164":
                case "NAD | C819 | 3229":
                case "NAD | 3251 | 3251":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;

                case "NAD | 3207 | 3207":
                    NAD_C080_3036_DefaultsValue v3207 = new NAD_C080_3036_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v3207.ValueArt;
                    VDAClientValues.Value = v3207.Value;
                    break;


                case "EQD | EQD | EQD":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = EQD.Name;
                    break;
                case "EQD | 8053 | 8053":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = "TE";
                    break;
                case "EQD | C237 | 8260":
                    VDAClientValues.ValueArt = clsEdiVDAValueAlias.const_EA_KFZ;
                    VDAClientValues.Value = string.Empty;
                    break;


                case "CPS | CPS | CPS":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = CPS.Name;
                    break;
                case "CPS | 7164 | 7164":
                    CPS_7164_7164_DefaultsValue cps7164 = new CPS_7164_7164_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = cps7164.ValueArt;
                    VDAClientValues.Value = cps7164.Value;
                    break;
                case "CPS | 7166 | 7166":
                    VDAClientValues.ValueArt = clsEdiVDAValueAlias.const_VDA_Value_Blanks;
                    VDAClientValues.Value = string.Empty;
                    break;
                case "CPS | 7075 | 7075":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = "1";
                    break;



                case "PAC | PAC | PAC":
                    PAC_PAC_PAC_DefaultsValue vPACC = new PAC_PAC_PAC_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vPACC.ValueArt;
                    VDAClientValues.Value = vPACC.Value;
                    break;
                case "PAC | 7224 | 7224":
                    PAC_7224_7224_DefaultsValue v7224 = new PAC_7224_7224_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7224.ValueArt;
                    VDAClientValues.Value = v7224.Value;
                    break;
                case "PAC | C531 | 7075":
                    PAC_C531_7075_DefaultsValue v7075 = new PAC_C531_7075_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7075.ValueArt;
                    VDAClientValues.Value = v7075.Value;
                    break;
                case "PAC | C531 | 7233":
                case "PAC | C531 | 7073":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;
                case "PAC | C202 | 7065":
                    PAC_C202_7065_DefaultsValue v7065 = new PAC_C202_7065_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7065.ValueArt;
                    VDAClientValues.Value = v7065.Value;
                    break;

                case "PAC | C202 | 3055":
                    PAC_C202_3055_DefaultsValue v3055a = new PAC_C202_3055_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v3055a.ValueArt;
                    VDAClientValues.Value = v3055a.Value;
                    break;

                case "PAC | C202 | 1131":
                    PAC_C202_1131_DefaultsValue pac1131 = new PAC_C202_1131_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = pac1131.ValueArt;
                    VDAClientValues.Value = pac1131.Value;
                    break;

                case "PAC | C202 | 7064":
                case "PAC | C402 | 7077":
                case "PAC | C402 | 7064":
                case "PAC | C402 | 7143":
                case "PAC | C532 | 8395":
                case "PAC | C532 | 8393":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;



                case "IMD | IMD | IMD":
                    IMD_IMD_IMD_DefaultsValue vIMD = new IMD_IMD_IMD_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vIMD.ValueArt;
                    VDAClientValues.Value = vIMD.Value;
                    break;

                case "IMD | C273 | 7008":
                    IMD_C273_7008_DefaultsValue v7008 = new IMD_C273_7008_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7008.ValueArt;
                    VDAClientValues.Value = v7008.Value;
                    break;

                case "IMD | 7383 | 7383":
                    IMD_7383_7383_DefaultsValue v7383 = new IMD_7383_7383_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7383.ValueArt;
                    VDAClientValues.Value = v7383.Value;
                    break;

                case "IMD | 7077 | 7077":
                case "IMD | 7081 | 7081":
                case "IMD | C273 | 7009":
                case "IMD | C273 | 1131":
                case "IMD | C273 | 3055":
                case "IMD | C273 | 3453":

                    VDAClientValues.ValueArt = Default_VDAClientOut.const_BLANK;
                    VDAClientValues.Value = string.Empty;
                    break;



                case "INV | INV | INV":
                    INV_INV_INV_DefaultsValue vINV = new INV_INV_INV_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vINV.ValueArt;
                    VDAClientValues.Value = vINV.Value;
                    break;

                case "INV | 4501 | 4501":
                    INV_4501_4501_DefaultsValue v4501 = new INV_4501_4501_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v4501.ValueArt;
                    VDAClientValues.Value = v4501.Value;
                    break;

                case "INV | 7491 | 7491":
                    INV_7491_7491_DefaultsValue v7491 = new INV_7491_7491_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7491.ValueArt;
                    VDAClientValues.Value = v7491.Value;
                    break;

                case "INV | 4499 | 4499":
                    INV_4499_4499_DefaultsValue v4499 = new INV_4499_4499_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v4499.ValueArt;
                    VDAClientValues.Value = v4499.Value;
                    break;

                case "INV | 4503 | 4503":
                    INV_4503_4503_DefaultsValue v4503 = new INV_4503_4503_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v4503.ValueArt;
                    VDAClientValues.Value = v4503.Value;
                    break;

                case "INV | C522 | 4403":
                    INV_C522_4403_DefaultsValue v4403 = new INV_C522_4403_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v4403.ValueArt;
                    VDAClientValues.Value = v4403.Value;
                    break;

                case "INV | C522 | 3055":
                    INV_C522_3055_DefaultsValue vI3055 = new INV_C522_3055_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = vI3055.ValueArt;
                    VDAClientValues.Value = vI3055.Value;
                    break;

                case "INV | C522 | 4401":
                case "INV | C522 | 1131":
                case "INV | C522 | 4400":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;



                case "PIA | PIA | PIA":
                    PIA_PIA_PIA_DefaultsValue vPIA = new PIA_PIA_PIA_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vPIA.ValueArt;
                    VDAClientValues.Value = vPIA.Value;
                    break;

                case "PIA | 4347 | 4347":
                    PIA_4347_4347_DefaultsValue v4347 = new PIA_4347_4347_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v4347.ValueArt;
                    VDAClientValues.Value = v4347.Value;
                    break;

                case "PIA | C212 | 7140":
                    PIA_C212_7140_DefaultsValue v7140 = new PIA_C212_7140_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7140.ValueArt;
                    VDAClientValues.Value = v7140.Value;
                    break;

                case "PIA | C212 | 7143":
                    PIA_C212_7143_DefaultsValue v7143 = new PIA_C212_7143_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7143.ValueArt;
                    VDAClientValues.Value = v7143.Value;
                    break;

                case "PIA | C212 | 1131":
                case "PIA | C212 | 3055":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;



                case "QTY | QTY | QTY":
                    QTY_QTY_QTY_DefaultsValue vQTY = new QTY_QTY_QTY_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vQTY.ValueArt;
                    VDAClientValues.Value = vQTY.Value;
                    break;
                case "QTY | C186 | 6063":
                    QTY_C186_6063_DefaultsValue v6063 = new QTY_C186_6063_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v6063.ValueArt;
                    VDAClientValues.Value = v6063.Value;
                    break;

                case "QTY | C186 | 6060":
                    QTY_C186_6060_DefaultsValue v6060 = new QTY_C186_6060_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v6060.ValueArt;
                    VDAClientValues.Value = v6060.Value;
                    break;
                case "QTY | C186 | 6411":
                    QTY_C186_6411_DefaultsValue v6411 = new QTY_C186_6411_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v6411.ValueArt;
                    VDAClientValues.Value = v6411.Value;
                    break;


                case "PCI | PCI | PCI":
                    IMD_IMD_IMD_DefaultsValue vPCI = new IMD_IMD_IMD_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vPCI.ValueArt;
                    VDAClientValues.Value = vPCI.Value;
                    break;
                case "PCI | 4233 | 4233":
                    PCI_4233_4233_DefaultsValue v4233 = new PCI_4233_4233_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v4233.ValueArt;
                    VDAClientValues.Value = v4233.Value;
                    break;

                case "PCI | C827 | 7511":
                    PCI_C827_7511_DefaultsValue v7511 = new PCI_C827_7511_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7511.ValueArt;
                    VDAClientValues.Value = v7511.Value;
                    break;

                case "PCI | C210 | 7102":
                case "PCI | 8275 | 8275":
                case "PCI | 8169 | 8169":
                case "PCI | C827 | 1131":
                case "PCI | C827 | 3055":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;


                case "GIN | GIN | GIN":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = GIN.Name;
                    break;
                case "GIN | 7405 | 7405":
                    GIN_7405_7405_DefaultsValue v7405 = new GIN_7405_7405_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7405.ValueArt;
                    VDAClientValues.Value = v7405.Value;
                    break;
                case "GIN | C208 | 7402":
                    GIN_C208_7402_DefaultsValue v7402 = new GIN_C208_7402_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7402.ValueArt;
                    VDAClientValues.Value = v7402.Value;
                    break;


                case "GIR | GIR | GIR":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = GIR.Name;
                    break;
                case "GIR | 7297 | 7297":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = "3";
                    break;
                case "GIR | C206 | 7402":
                    VDAClientValues.ValueArt = clsEdiVDAValueAlias.const_Artikel_Produktionsnummer;
                    VDAClientValues.Value = string.Empty;
                    break;
                case "GIR | C206 | 7405":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = "BN";
                    break;



                case "LIN | LIN | LIN":
                    LIN_LIN_LIN_DefaultsValue vLIN = new LIN_LIN_LIN_DefaultsValue(myAdr, myAsnArt);
                    VDAClientValues.ValueArt = vLIN.ValueArt;
                    VDAClientValues.Value = vLIN.Value;
                    break;
                case "LIN | 1082 | 1082":
                    LIN_1082_1082_DefaultsValue v1082 = new LIN_1082_1082_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v1082.ValueArt;
                    VDAClientValues.Value = v1082.Value;
                    break;
                case "LIN | 1229 | 1229":
                    LIN_1229_1229_DefaultsValue v1229 = new LIN_1229_1229_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v1229.ValueArt;
                    VDAClientValues.Value = v1229.Value;
                    break;
                case "LIN | C212 | 7140":
                    LIN_C212_7140_DefaultsValue v7140a = new LIN_C212_7140_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7140a.ValueArt;
                    VDAClientValues.Value = v7140a.Value;
                    break;
                case "LIN | C212 | 7143":
                    LIN_C212_7143_DefaultsValue v7143a = new LIN_C212_7143_DefaultsValue(myAdr, myAsnArt, EdiSegmentElementFields);
                    VDAClientValues.ValueArt = v7143a.ValueArt;
                    VDAClientValues.Value = v7143a.Value;
                    break;
                case "LIN | C212 | 1131":
                case "LIN | C212 | 3055":
                case "LIN | C829 | 5495":
                case "LIN | C829 | 1082":
                case "LIN | 1222 | 1222":
                case "LIN | 7083 | 7083":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;


                case "UNT | UNT | UNT":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = UNT.Name;
                    break;
                case "UNT | 0074 | 0074":
                case "UNT | 0062 | 0062":
                    VDAClientValues.ValueArt = clsEdiVDAValueAlias.const_VDA_Value_Blanks;
                    VDAClientValues.Value = string.Empty;
                    break;


                case "UNZ | UNZ | UNZ":
                    VDAClientValues.ValueArt = Defaults.Default_VDAClientOut.const_Const;
                    VDAClientValues.Value = UNZ.Name;
                    break;
                case "UNZ | 0036 | 0036":
                case "UNZ | 0020 | 0020":
                    VDAClientValues.ValueArt = clsEdiVDAValueAlias.const_VDA_Value_Blanks;
                    VDAClientValues.Value = string.Empty;
                    break;
            }

            //--- SET Article Item
            Default_VDAClientOut_SetArticleItem cvSetArtItem = new Defaults.Default_VDAClientOut_SetArticleItem(VDAClientValues, EdiSegmentElementFields, myAsnArt);
            VDAClientValues = cvSetArtItem.VDAClientValues.Copy();

        }
    }
}
