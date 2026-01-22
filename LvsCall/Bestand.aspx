<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bestand.aspx.cs" Inherits="Bestand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="Div1">
        <table style="width: 100%;">
            <tr>
 <%--           <td id="colZeitraum" Width="320px" style="border: thin solid #000080">
                    <table id="tableZeitraum" style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Zeitraum von:"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dtpVon" runat="server" 
                                    Culture="de-DE" 
                                    MinDate="2005-01-01" 
                                    Skin="Office2010Blue" 
                                    OnSelectedDateChanged="dtpVon_SelectedDateChanged" 
                                    AutoPostBack="True" FocusedDate="2005-01-01" Width="170px" >
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="de-DE" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Office2007" EnableKeyboardNavigation="True"></Calendar>
                                        <DateInput DisplayDateFormat="dd.MM.yyyy" DateFormat="dd.MM.yyyy" LabelWidth="40%" AutoPostBack="True">
                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                        <FocusedStyle Resize="None"></FocusedStyle>
                                        <DisabledStyle Resize="None"></DisabledStyle>
                                        <InvalidStyle Resize="None"></InvalidStyle>
                                        <HoveredStyle Resize="None"></HoveredStyle>
                                        <EnabledStyle Resize="None"></EnabledStyle>
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Zeitraum bis:"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dtpBis" Runat="server" 
                                    Culture="de-DE" 
                                    MinDate="2005-01-01" 
                                    OnSelectedDateChanged="dtpBis_SelectedDateChanged" 
                                    Skin="Office2010Blue" 
                                    AutoPostBack="True" FocusedDate="2005-01-01" Width="170px">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="de-DE" FastNavigationNextText="&amp;lt;&amp;lt;" EnableKeyboardNavigation="True" Skin="Office2007"></Calendar>
                                        <DateInput DisplayDateFormat="dd.MM.yyyy" DateFormat="dd.MM.yyyy" LabelWidth="40%" AutoPostBack="True">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                    </table>
                </td>--%>
                <td style="border: thin solid #000080; padding: 1px; margin: 1px; top: auto; vertical-align: top;"  >
                    <table Width="300px" id="TableDisplay">
                        <tr>
                            <td style="padding: inherit; margin: auto" colspan="2">
                                <asp:Label ID="Label4" runat="server" Text="Übersicht angezeigte Güterarten:" Font-Bold="true" Font-Underline="true"></asp:Label></td>
                            <td style="padding: inherit; margin: auto"></td>                        
                        </tr>
                        <tr>
                            <td style="padding: inherit; margin: auto">
                                <asp:Label ID="Label5" runat="server" Text="Positionen [Stk]:"></asp:Label></td>              
                            <td style="padding: inherit; margin: auto">
                                <telerik:RadNumericTextBox ID="tbGeamtAnzahl" runat="server" Culture="de-DE" DataType="System.Int32" DbValueFactor="1" LabelWidth="64px" ReadOnly="True" Skin="Office2010Blue" Value="0" Width="120px">
                                    <NegativeStyle Resize="None"></NegativeStyle>
                                    <NumberFormat ZeroPattern="n"></NumberFormat>
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    <ReadOnlyStyle Resize="None" HorizontalAlign="Right"></ReadOnlyStyle>
                                    <FocusedStyle Resize="None"></FocusedStyle>
                                    <DisabledStyle Resize="None" HorizontalAlign="Right"></DisabledStyle>
                                    <InvalidStyle Resize="None"></InvalidStyle>
                                    <HoveredStyle Resize="None"></HoveredStyle>
                                    <EnabledStyle Resize="None"></EnabledStyle>
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: inherit; margin: auto">
                                <asp:Label ID="Label6" runat="server" Text="Gesamtgewicht [kg]:"></asp:Label></td>                           
                            <td style="padding: inherit; margin: auto"><telerik:RadNumericTextBox ID="tbGesamtGewicht" runat="server" Culture="de-DE" DataType="System.Decimal" DbValueFactor="1" LabelWidth="64px" ReadOnly="True" Skin="Office2010Blue" Value="0" Width="120px">
                                    <NegativeStyle Resize="None"></NegativeStyle>
                                    <NumberFormat ZeroPattern="n"></NumberFormat>
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    <ReadOnlyStyle Resize="None" HorizontalAlign="Right"></ReadOnlyStyle>
                                    <FocusedStyle Resize="None"></FocusedStyle>
                                    <DisabledStyle Resize="None" HorizontalAlign="Right"></DisabledStyle>
                                    <InvalidStyle Resize="None"></InvalidStyle>
                                    <HoveredStyle Resize="None"></HoveredStyle>
                                    <EnabledStyle Resize="None"></EnabledStyle>
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>    
                            </td>
                            <td>
                               
                            </td>                        
                        </tr>
                    </table>

                </td>
            </tr>
        </table>         
    </div>

    <div id="DateFilter">
        <asp:Table ID="Table1" runat="server" Visible="False">
            <asp:TableRow Height="12">
                <asp:TableCell Text="Zeitraum von:"></asp:TableCell>
                <asp:TableCell>

                </asp:TableCell>
                <asp:TableCell Text="Zeitraum bis:"></asp:TableCell>
                <asp:TableCell>

                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>


    <div id="Div3">
    <telerik:RadGrid ID="dgv" runat="server" 
            AllowSorting="True" 
            Culture="de-DE" 
            Skin="Office2010Blue" 
            ShowDesignTimeSmartTagMessage="False" 
            OnNeedDataSource="dgv_NeedDataSource" 
            AllowCustomPaging="True"  
            ShowStatusBar="True" 
            PageSize="30" 
            OnItemDataBound="dgv_ItemDataBound" 
            ShowFooter="True" 
            OnColumnCreated="dgv_ColumnCreated" 
        >
        <GroupingSettings RetainGroupFootersVisibility="True" ShowUnGroupButton="True" UnGroupButtonTooltip="Gruppierung auflösen..." />
        <ExportSettings HideStructureColumns="True" IgnorePaging="True" ExportOnlyData="True" FileName="" OpenInNewWindow="True">
            <Pdf PageHeight="210mm" PageWidth="297mm" PaperSize="A4" AllowPrinting="False">
            </Pdf>
        </ExportSettings>
        <ClientSettings AllowAutoScrollOnDragDrop="False" AllowColumnsReorder="True" AllowDragToGroup="True">
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="True" EnableVirtualScrollPaging="True" ScrollHeight="480px" UseStaticHeaders="True" />
            <Resizing AllowResizeToFit="True" />
        </ClientSettings>
        <MasterTableView CommandItemDisplay="Top" 
                        EditMode="InPlace" 
                        EnableHeaderContextAggregatesMenu="True" 
                        EnableHeaderContextFilterMenu="True" 
                        EnableHeaderContextMenu="True" PageSize="50" 
                        ShowGroupFooter="True">
            <CommandItemSettings ShowExportToExcelButton="True" ShowAddNewRecordButton="False" />
            <RowIndicatorColumn Visible="False">
            </RowIndicatorColumn>
            <ExpandCollapseColumn Created="True">
            </ExpandCollapseColumn>
            <EditFormSettings>
                <EditColumn ButtonType="ImageButton" HeaderButtonType="PushButton">
                </EditColumn>
            </EditFormSettings>
            <PagerStyle AlwaysVisible="True" Position="Top" />
        </MasterTableView>
        <PagerStyle Position="Top" AlwaysVisible="True" />
        <HeaderContextMenu AppendDataBoundItems="True">
        </HeaderContextMenu>
    </telerik:RadGrid>

    </div>
</asp:Content>

