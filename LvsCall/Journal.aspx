<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Journal.aspx.cs" Inherits="Journal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div id="Div1">
        <table style="width: 100%;">
            <tr>
                <td Width="300px" style="border: thin solid #000080; padding: 1px; margin: 1px; top: auto; vertical-align: top;" >
                    <table style="width: 100%;">
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
                                    AutoPostBack="true" 
                                    FocusedDate="2005-01-01" >
                                        <Calendar 
                                            UseRowHeadersAsSelectors="False" 
                                            UseColumnHeadersAsSelectors="False" 
                                            EnableWeekends="True" 
                                            Culture="de-DE" 
                                            FastNavigationNextText="&amp;lt;&amp;lt;" 
                                            Skin="Office2010Blue" 
                                            EnableKeyboardNavigation="True"></Calendar>
                                        <DateInput 
                                            DisplayDateFormat="dd.MM.yyyy" 
                                            DateFormat="dd.MM.yyyy" 
                                            LabelWidth="40%" 
                                            AutoPostBack="true">
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
                                    AutoPostBack="true" 
                                    FocusedDate="2005-01-01">
                                    <Calendar 
                                        UseRowHeadersAsSelectors="False" 
                                        UseColumnHeadersAsSelectors="False" 
                                        EnableWeekends="True" 
                                        Culture="de-DE" 
                                        FastNavigationNextText="&amp;lt;&amp;lt;" 
                                        EnableKeyboardNavigation="True" 
                                        Skin="Office2010Blue"></Calendar>
                                    <DateInput 
                                        DisplayDateFormat="dd.MM.yyyy" 
                                        DateFormat="dd.MM.yyyy" 
                                        LabelWidth="40%" 
                                        AutoPostBack="true">
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
                </td>
                <td Width="360px" style="border: thin solid #000080; padding: 1px; margin: 1px; top: auto; vertical-align: top;" >
                    <table>
                        <tr>
                            <td style="padding: inherit; margin: auto">
                                <asp:Label ID="Label1" runat="server" Text="Übersicht ausgwählte Arikel:" Font-Bold="true" Font-Underline="true"></asp:Label></td>
                            <td style="padding: inherit; margin: auto"></td>                        
                        </tr>
                        <tr>
                            <td style="padding: inherit; margin: auto">
                                <asp:Label ID="Label2" runat="server" Text="Lieferantenauswahl:"></asp:Label></td>              
                            <td style="padding: inherit; margin: auto">
                                <asp:DropDownList ID="comboLieferant" runat="server" AutoPostBack="false" OnSelectedIndexChanged="comboLieferant_SelectedIndexChanged" Height="23px" Width="242px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: inherit; margin: auto">    
                                <asp:DropDownList ID="comboSearch" runat="server" AutoPostBack="false" OnSelectedIndexChanged="comboSearch_SelectedIndexChanged">
                                        <asp:ListItem>wählen Sie ein Suchfeld</asp:ListItem>
                                        <asp:ListItem>LVSNr</asp:ListItem>
                                        <asp:ListItem>Charge</asp:ListItem>
                                        <asp:ListItem>Produktionsnummer</asp:ListItem>
                                        <asp:ListItem>Mat.-Nr</asp:ListItem>
                                        <asp:ListItem>Dicke</asp:ListItem>
                                        <asp:ListItem>Breite</asp:ListItem>
                                        <asp:ListItem>Abmessung(Dicke-Breite)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="padding: inherit; margin: auto">
                                <telerik:RadTextBox ID="txtSearch" runat="server" AutoPostBack="false" LabelWidth="64px" OnTextChanged="txtSearch_TextChanged" Resize="None" Skin="Office2010Blue" Width="234px"></telerik:RadTextBox>
                            </td>                        
                        </tr>
                    </table>
                </td>
                <td style="border: thin solid #000080; padding: 1px; margin: 1px; top: auto; vertical-align: top;"  >
                    <table Width="300px" >
                        <tr>
                            <td style="padding: inherit; margin: auto" colspan="2">
                                <asp:Label ID="Label4" runat="server" Text="Übersicht angezeigte Arikel:" Font-Bold="true" Font-Underline="true"></asp:Label></td>
                            <td style="padding: inherit; margin: auto"></td>                        
                        </tr>
                        <tr>
                            <td style="padding: inherit; margin: auto">
                                <asp:Label ID="Label5" runat="server" Text="Gesamtartikelanzahl [Stk]:"></asp:Label></td>              
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
                                <asp:Label ID="Label6" runat="server" Text="Gesamtartikelgewicht [kg]:"></asp:Label></td>                           
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
                    </table>

                </td>
            </tr>
        </table>         
    </div>
        <div id="Menu">
        <table style=" width:100%" border="0" >
            <tr>
                <td>
                    <telerik:RadToolBar ID="RadToolBar1" runat="server" SingleClick="Button" OnButtonClick="RadToolBar1_ButtonClick" Skin="Office2010Blue">
                            <Items>
                                <telerik:RadToolBarButton runat="server" Text="[Suche starten]" CommandName="StartSearch" ToolTip="nach Artikel gemäß Sucheinstellungen suchen..." Value="0">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" Text="[Suche leeren]"  CommandName="ClearSearch" ToolTip="Sucheinstellungen zurücksetzen..." Value="0">
                                </telerik:RadToolBarButton>
                            </Items>
                    </telerik:RadToolBar>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadGrid ID="dgv" runat="server" 
            AllowSorting="True" 
            Culture="de-DE" 
            Skin="Office2010Blue" OnNeedDataSource="dgv_NeedDataSource" AllowCustomPaging="True" OnGridExporting="dgv_GridExporting" ShowStatusBar="True" PageSize="30" OnColumnCreated="dgv_ColumnCreated" OnItemDataBound="dgv_ItemDataBound" ShowFooter="True">
        <ExportSettings HideStructureColumns="True" IgnorePaging="True" ExportOnlyData="True" FileName="" OpenInNewWindow="True">
            <Pdf PageHeight="210mm" PageWidth="297mm" PaperSize="A4">
            </Pdf>
        </ExportSettings>
        <ClientSettings AllowAutoScrollOnDragDrop="False" AllowColumnsReorder="True">
            <Scrolling AllowScroll="True" EnableVirtualScrollPaging="True" ScrollHeight="480px" UseStaticHeaders="True" />
        </ClientSettings>
        <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="ArtikelID">
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
</asp:Content>

