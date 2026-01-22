using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;


public class clsTelerikGLHelper
{

    ///<summary>clsTelerikGLHelpercs / GridFormating</summary>
    ///<remarks>formatiert die einzelen Spalten mit Schrift, Breite, Ausrichtung usw.</remarks>
    public static string GridFormatting(ref RadGrid myDGV, ref GridDataItem dataItem, ref List<clsViews> myViewList)
    {
        string strError = string.Empty;
        if (dataItem is GridDataItem)
        {
            //ForeColor setzen
            if (myDGV.MasterTableView.GetColumnSafe("CallStatus") != null)
            {
                clsAbruf abrColor = new clsAbruf();
                string strCallStatus = string.Empty;
                strCallStatus = dataItem["CallStatus"].Text;
                //Backcolor setzen
                switch (strCallStatus)
                {
                    case clsAbruf.const_Status_erstellt:
                        dataItem.ForeColor = abrColor.const_StatusColor_erstellt;
                        break;
                    case clsAbruf.const_Status_bearbeitet:
                        dataItem.ForeColor = abrColor.const_StatusColor_bearbeitet;
                        break;
                    case clsAbruf.const_Status_MAT:
                        dataItem.ForeColor = abrColor.const_StatusColor_MAT;
                        break;

                    case clsAbruf.const_Status_ENTL:
                        dataItem.ForeColor = abrColor.const_StatusColor_ENTL;
                        break;
                    default:
                        dataItem.ForeColor = Color.Black;
                        break;
                }
            }

            //SPL Farbig anzeigen
            if (myDGV.MasterTableView.GetColumnSafe("IsSPL") != null)
            {
                if (dataItem["ArtikelID"].Text.Equals("150657"))
                {
                    string strText = dataItem["ArtikelID"].Text;
                }
                bool bValue = (dataItem["IsSPL"].Controls[0] as CheckBox).Checked;
                if (bValue)
                {
                    dataItem.BackColor = Color.Tomato;
                }
                else
                {
                    dataItem.BackColor = myDGV.BackColor;
                }
            }



            //Cellenformatierung
            for (Int32 i = 0; i <= myViewList.Count - 1; i++)
            {
                clsViews tmpView = (clsViews)myViewList[i];
                if (tmpView is clsViews)
                {
                    try
                    {
                        if (dataItem[tmpView.ColumnName] != null)
                        {
                            switch (tmpView.ColumnName)
                            {
                                case "AbrufID":
                                case "LVSNr":
                                case "Benutzer":
                                case "Schicht":
                                    dataItem[tmpView.ColumnName].HorizontalAlign = HorizontalAlign.Center;
                                    break;

                                case "ArtikelID":
                                case "Werksnummer":
                                case "Produktionsnummer":
                                case "Charge":
                                case "Eingang":
                                case "Ausgang":
                                case "Lieferschein":
                                case "Art":
                                    dataItem[tmpView.ColumnName].HorizontalAlign = HorizontalAlign.Left;
                                    break;

                                case "Menge":
                                case "Anzahl":
                                    Int32 iTmp = int.Parse(dataItem[tmpView.ColumnName].Text);
                                    dataItem[tmpView.ColumnName].Text = string.Format("{0:N0}", iTmp);
                                    dataItem[tmpView.ColumnName].HorizontalAlign = HorizontalAlign.Right;
                                    break;


                                case "Netto":
                                case "Brutto":
                                case "Dicke":
                                case "Breite":
                                    decimal decTmp = 0;
                                    decimal.TryParse(dataItem[tmpView.ColumnName].Text, out decTmp);
                                    dataItem[tmpView.ColumnName].Text = string.Format("{0:N2}", decTmp);
                                    dataItem[tmpView.ColumnName].HorizontalAlign = HorizontalAlign.Right;
                                    break;

                                case "Eingangsdatum":
                                    DateTime dtTmpEingang = LVS.clsSystem.const_DefaultDateTimeValue_Min;
                                    DateTime.TryParse(dataItem[tmpView.ColumnName].Text, out dtTmpEingang);
                                    dataItem[tmpView.ColumnName].Text = string.Format("{0:d}", dtTmpEingang);
                                    dataItem[tmpView.ColumnName].HorizontalAlign = HorizontalAlign.Center;
                                    break;

                                case "Ausgangsdatum":
                                case "Eintreffdatum":
                                    DateTime dtTmpAusgang = LVS.clsSystem.const_DefaultDateTimeValue_Min;
                                    DateTime.TryParse(dataItem[tmpView.ColumnName].Text, out dtTmpAusgang);
                                    if (dtTmpAusgang > LVS.clsSystem.const_DefaultDateTimeValue_Min)
                                    {
                                        dataItem[tmpView.ColumnName].Text = string.Format("{0:d}", dtTmpAusgang);
                                    }
                                    else
                                    {
                                        dataItem[tmpView.ColumnName].Text = string.Empty;
                                    }
                                    dataItem[tmpView.ColumnName].HorizontalAlign = HorizontalAlign.Center;
                                    break;

                                case "Eintreffzeit":
                                    DateTime dtTmpEZeit = LVS.clsSystem.const_DefaultDateTimeValue_Min;
                                    DateTime.TryParse(dataItem[tmpView.ColumnName].Text, out dtTmpEZeit);
                                    dataItem[tmpView.ColumnName].Text = string.Format("{0:t}", dtTmpEZeit);
                                    dataItem[tmpView.ColumnName].HorizontalAlign = HorizontalAlign.Center;
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strError = strError + ex.ToString() + Environment.NewLine;
                    }
                }
            }
        }
        return strError;
    }
    ///<summary>clsTelerikGLHelpercs / ReorderGridSourceDataTable</summary>
    ///<remarks></remarks>
    public static void ReorderGridSourceDataTable(ref DataTable dtSource, ref clsViews myViews)
    {
        if (dtSource.Columns.Contains("ArtikelID"))
        {
            dtSource.Columns["ArtikelID"].SetOrdinal(dtSource.Columns.Count - 1);
        }

        if (myViews.ListTableView.Count > 0)
        {
            for (Int32 i = 0; i <= myViews.ListTableView.Count - 1; i++)
            {
                clsViews tmpView = (clsViews)myViews.ListTableView[i];
                if (tmpView != null)
                {
                    if (dtSource.Columns.Contains(tmpView.ColumnName))
                    {
                        //dtSource.Columns[tmpView.ColumnName].SetOrdinal(tmpView.OrderIndex);
                        dtSource.Columns[tmpView.ColumnName].SetOrdinal(tmpView.OrderIndex - 1);
                    }
                }
            }
        }
    }
    ///<summary>Abruf / SortDataTableSource</summary>
    ///<remarks></remarks>
    public static void SortDataTableSource(ref DataTable mydt, ref clsViews myView)
    {
        mydt.DefaultView.Sort = string.Empty;
        if (mydt.Rows.Count > 0)
        {
            string strSort = string.Empty;
            for (Int32 i = 0; i <= myView.ListSort.Count - 1; i++)
            {
                clsViews tmpView = (clsViews)myView.ListSort[i];
                if (tmpView != null)
                {
                    if (!strSort.Equals(string.Empty))
                    {
                        strSort = strSort + ", ";
                    }
                    strSort = strSort + tmpView.ColumnName;
                    if (tmpView.SortOrderDesc)
                    {
                        strSort = strSort + " DESC ";
                    }
                    else
                    {
                        strSort = strSort + " ASC ";
                    }
                }
            }
            mydt.DefaultView.Sort = strSort;
        }
    }
}
