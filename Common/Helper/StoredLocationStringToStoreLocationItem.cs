using Common.ApiModels;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Helper
{
    public class StoredLocationStringToStoreLocationItem
    {
        public const string const_NullStoredLocationString = "#####";
        public string StoredLocationString { get; set; } = string.Empty;
        public string WerkInput { get; set; } = string.Empty;
        public string HalleInput { get; set; } = string.Empty;
        public string ReiheInput { get; set; } = string.Empty;
        public string EbeneInput { get; set; } = string.Empty;
        public string PlatzInput { get; set; } = string.Empty;

        public bool IsConvertationSuccessful { get; set; } = false;
        public string ErrorMessage { get; set; }
        /// <summary>
        ///             Format StoredLocation = #[Werk]#[Halle]#[Reihe]#[Ebene]#[Platz]
        ///             
        ///             1. Check: Anzahl # = 5
        /// </summary>
        /// <param name="StoredLocationString"></param>
        public StoredLocationStringToStoreLocationItem()
        {
        }

        public void Convert(string storedLocationString)
        {
            int iCount = storedLocationString.Count(c => c == '#');
            bool endsWithDigit = (!storedLocationString.EndsWith("#"));

            if ((iCount == 5) && (endsWithDigit))
            {
                StoredLocationString = storedLocationString;
                IsConvertationSuccessful = true;
                List<string> list = new List<string>();
                char lastChar = new char();
                int iSepCount = 0;

                foreach (char c in StoredLocationString)
                {
                    if (c.Equals('#'))
                    {
                        iSepCount++;
                    }
                    else
                    {
                        switch (iSepCount)
                        {
                            case 1:
                                WerkInput += c.ToString();
                                break;
                            case 2:
                                HalleInput += c.ToString();
                                break;
                            case 3:
                                ReiheInput += c.ToString();
                                break;
                            case 4:
                                EbeneInput += c.ToString();
                                break;
                            case 5:
                                PlatzInput += c.ToString();
                                break;
                        }
                    }
                    lastChar = c;
                }
            }
            else
            {
                IsConvertationSuccessful = false;
                ErrorMessage = "Der Lagerortstring ist fehlerhaft!";
            }
        }

        public void Reverse(ArticleStoreLocation storeLocation)
        {
            try
            {
                WerkInput = storeLocation.Werk;
                HalleInput = storeLocation.Halle;
                ReiheInput = storeLocation.Reihe;
                EbeneInput = storeLocation.Ebene;
                PlatzInput = storeLocation.Platz;

                CreateStoredLocationString();
            }
            catch (Exception ex)
            {
                IsConvertationSuccessful = false;
            }
        }


        public void Reverse(string werk, string halle, string reihe, string ebene, string platz)
        {
            try
            {
                WerkInput = werk;
                HalleInput = halle;
                ReiheInput = reihe;
                EbeneInput = ebene;
                PlatzInput = platz;

                CreateStoredLocationString();
            }
            catch (Exception ex)
            {
                IsConvertationSuccessful = false;
            }
        }

        private void CreateStoredLocationString()
        {
            try
            {
                string sep = "#";
                string ret = sep + WerkInput +
                             sep + HalleInput +
                             sep + ReiheInput +
                             sep + EbeneInput +
                             sep + PlatzInput;
                if (ret.Equals(StoredLocationStringToStoreLocationItem.const_NullStoredLocationString))
                {
                    ret = string.Empty;
                }
                StoredLocationString = ret;
                IsConvertationSuccessful = true;
            }
            catch (Exception ex)
            {
                IsConvertationSuccessful = false;
            }
        }


        public void ArticledataToStoreLocationString(Articles art)
        {
            try
            {
                WerkInput = art.Werk;
                HalleInput = art.Halle;
                ReiheInput = art.Reihe;
                EbeneInput = art.Ebene;
                PlatzInput = art.Platz;

                string sep = "#";
                string ret = sep + WerkInput +
                             sep + HalleInput +
                             sep + ReiheInput +
                             sep + EbeneInput +
                             sep + PlatzInput;

                StoredLocationString = ret;
                IsConvertationSuccessful = true;
            }
            catch (Exception ex)
            {
                IsConvertationSuccessful = false;
            }
        }

        public string FormatToCompareLocationString(string strToFormat)
        {
            string strReturn = string.Empty;
            char lastChar = ' ';
            if (!strToFormat.Equals(StoredLocationStringToStoreLocationItem.const_NullStoredLocationString))
            {
                foreach (char c in strToFormat)
                {
                    if (lastChar.Equals('#'))
                    {
                        if (!c.Equals('0'))
                        {
                            strReturn += c;
                        }
                    }
                    else
                    {
                        strReturn += c;
                    }
                    lastChar = c;
                }
            }
            return strReturn;
        }
    }
}
