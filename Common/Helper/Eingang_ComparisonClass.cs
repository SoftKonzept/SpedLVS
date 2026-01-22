using Common.Models;
using System;
using System.Reflection;

namespace Common.Helper
{
    public class Eingang_ComparisonClass
    {
        public const string EingangField_LEingangID = "LEingang.LEingangID";
        public const string EingangField_Date = "LEingang.Date";
        public const string EingangField_Auftraggeber = "LEingang.Auftraggeber";
        public const string EingangField_Empfaenger = "LEingang.Empfaenger";
        public const string EingangField_Lieferant = "LEingang.Lieferant";
        public const string EingangField_LfsNr = "LEingang.LfsNr";
        public const string EingangField_Versender = "LEingang.Versender";
        public const string EingangField_SpedID = "LEingang.SpedID";
        public const string EingangField_KFZ = "LEingang.KFZ";
        public const string EingangField_WaggonNo = "LEingang.WaggonNo";
        public const string EingangField_BeladeID = "LEingang.BeladeID";
        public const string EingangField_EntladeID = "LEingang.EntladeID";
        public const string EingangField_ExTransportRef = "LEingang.ExTransportRef";
        public const string EingangField_ExAuftragRef = "LEingang.ExAuftragRef";
        public const string EingangField_ASNRef = "LEingang.ASNRef";
        public const string EingangField_Fahrer = "LEingang.Fahrer";
        public const string EingangField_Retoure = "LEingang.Retoure";
        public const string EingangField_Verlagerung = "LEingang.Verlagerung";
        public const string EingangField_Umbuchung = "LEingang.Umbuchung";
        public const string EingangField_LagerTransport = "LEingang.LagerTransport";
        public const string EingangField_Ship = "LEingang.Ship";
        public const string EingangField_IsShip = "LEingang.IsShip";
        public const string EingangField_DirektDelivery = "LEingang.DirektDelivery";

        public string LEingangChangingText { get; set; } = string.Empty;
        public Eingang_ComparisonClass(Eingaenge artChanged, Eingaenge artOriginal)
        {
            LEingangChangingText = string.Empty;
            //Type typeSource = this.GetType();
            //PropertyInfo[] pInfoSource = typeSource.GetProperties();
            Type typeSource = artChanged.GetType();
            PropertyInfo[] pInfoSource = typeSource.GetProperties();


            Type typeCompare = artChanged.GetType();
            PropertyInfo[] pInfoCompare = typeCompare.GetProperties();

            try
            {
                foreach (PropertyInfo info in pInfoSource)
                {
                    //Test
                    if (info.Name.ToString().Equals(EingangField_DirektDelivery))
                    {
                        string str = "stop";
                    }

                    if (info.Name.ToString().Equals("DirektDelivery"))
                    {
                        string str = "stop";
                    }
                    if ((info.CanRead) & (info.CanWrite))
                    {
                        object NewValue;
                        object oldValue;
                        string PropName = info.Name.ToString();


                        switch ("LEingang." + PropName)
                        {
                            case EingangField_Date:
                                NewValue = info.GetValue(artChanged, null);
                                oldValue = typeCompare.GetProperty(PropName).GetValue(artOriginal, null);
                                DateTime dtNew = new DateTime(1900, 1, 1);
                                DateTime.TryParse(NewValue.ToString(), out dtNew);
                                DateTime dtOld = new DateTime(1900, 1, 1);
                                DateTime.TryParse(oldValue.ToString(), out dtOld);
                                if (dtNew != dtOld)
                                {
                                    LEingangChangingText += PropName + ":  [" + dtOld.Date.ToShortDateString() + "] >>> [" + dtNew.Date.ToShortDateString() + "]" + Environment.NewLine;
                                }
                                break;

                            case EingangField_Auftraggeber:
                            case EingangField_Empfaenger:
                            case EingangField_Versender:
                            case EingangField_SpedID:
                            case EingangField_BeladeID:
                            case EingangField_EntladeID:
                                Int32 iNewValue = 0;
                                Int32 ioldValue = 0;
                                NewValue = info.GetValue(artChanged, null);
                                oldValue = typeCompare.GetProperty(PropName).GetValue(artOriginal, null);
                                Int32.TryParse(NewValue.ToString(), out iNewValue);
                                Int32.TryParse(oldValue.ToString(), out ioldValue);
                                if (iNewValue != ioldValue)
                                {
                                    LEingangChangingText += PropName + ":  [" + ioldValue.ToString() + "] >>> [" + iNewValue.ToString() + "]" + Environment.NewLine;
                                }
                                break;

                            case EingangField_Lieferant:
                            case EingangField_LfsNr:
                            case EingangField_KFZ:
                            case EingangField_WaggonNo:
                            case EingangField_ExTransportRef:
                            case EingangField_ExAuftragRef:
                            case EingangField_Fahrer:
                            case EingangField_Ship:

                                NewValue = string.Empty;
                                oldValue = string.Empty;
                                NewValue = info.GetValue(artChanged, null);
                                oldValue = typeCompare.GetProperty(PropName).GetValue(artOriginal, null);
                                if (!NewValue.ToString().Equals(oldValue.ToString()))
                                {
                                    LEingangChangingText += PropName + ":  [" + oldValue.ToString() + "] >>> [" + NewValue.ToString() + "]" + Environment.NewLine;
                                }
                                break;

                            case EingangField_Retoure:
                            case EingangField_Verlagerung:
                            case EingangField_Umbuchung:
                            case EingangField_LagerTransport:
                            case EingangField_DirektDelivery:
                            case EingangField_IsShip:
                                NewValue = string.Empty;
                                oldValue = string.Empty;
                                NewValue = info.GetValue(artChanged, null);
                                oldValue = typeCompare.GetProperty(PropName).GetValue(artOriginal, null);
                                if (!NewValue.ToString().Equals(oldValue.ToString()))
                                {
                                    LEingangChangingText += PropName + ":  [" + oldValue.ToString() + "] >>> [" + NewValue.ToString() + "]" + Environment.NewLine;
                                }
                                break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            if (!LEingangChangingText.Equals(string.Empty))
            {
                LEingangChangingText = "Folgende Ängerungen wurden vorgenommen: " + Environment.NewLine + LEingangChangingText;
            }

        }
    }
}
