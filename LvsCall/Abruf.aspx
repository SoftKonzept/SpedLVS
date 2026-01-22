<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Abruf.aspx.cs" Inherits="Abruf" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <telerik:RadAjaxManager runat="server">
       </telerik:RadAjaxManager>
       <div id="DateFilter">
        <table style="width: 100%;">
            <tr>
                <td Width="455px" style="border: thin solid #000080">
                     <asp:Table ID="TableEingabe" runat="server" Width="455px">
                        <asp:TableRow Height="12">
                            <asp:TableCell Width="100" Text="Eintrefftermin:"></asp:TableCell>
                            <asp:TableCell>
                                <telerik:RadDatePicker ID="dtpEintrefftermin" runat="server"                                    
                                    AutoPostBack="True" 
                                    Culture="de-DE" 
                                    FocusedDate="2005-01-01" 
                                    MinDate="2005-01-01" 
                                    Skin="Office2010Blue">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="de-DE" FastNavigationNextText="&amp;lt;&amp;lt;" EnableKeyboardNavigation="True" Skin="Office2010Blue"></Calendar>
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
                            </asp:TableCell>
                            <asp:TableCell>
                                <telerik:RadTimePicker ID="dtpEintreffZeit" runat="server" Culture="de-DE" 
                                    AutoPostBack="True" 
                                    MinDate="1900-01-01" 
                                    Skin="Office2010Blue">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="de-DE" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>
                                    <DatePopupButton Visible="False" CssClass="" ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    <TimeView CellSpacing="-1" Culture="de-DE" Columns="4" Interval="00:30:00" Caption="Eintreff-Zeit"></TimeView>
                                    <TimePopupButton CssClass="" ImageUrl="" HoverImageUrl=""></TimePopupButton>
                                    <DateInput Width="" DisplayDateFormat="dd.MM.yyyy" DateFormat="dd.MM.yyyy" LabelWidth="64px" AutoPostBack="True">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                    </DateInput>
                                </telerik:RadTimePicker>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow Height="12">
                            <asp:TableCell Width="100" Text="Referenz:"></asp:TableCell>
                            <asp:TableCell>
                                    <telerik:RadTextBox ID="tbReferenz" runat="server"                                    
                                        AutoPostBack="True"></telerik:RadTextBox>
                            </asp:TableCell>

                        </asp:TableRow>
                        <asp:TableRow Height="12">
                            <asp:TableCell Width="100" Text="Abladestelle:"></asp:TableCell>
                            <asp:TableCell>
                               <telerik:RadTextBox ID="tbAbladestelle" runat="server" 
                                   AutoPostBack="True" ></telerik:RadTextBox>
                            </asp:TableCell>
                        </asp:TableRow>                 
                     </asp:Table>
                </td>
                <td Width="380px" style="border: thin solid #000080; padding: 1px; margin: 1px; top: auto; vertical-align: top;">
                    <table Width="350px" id="TableSelected">
                        <tr>
                            <td style="padding: inherit; margin: auto" colspan="2">
                                <asp:Label ID="Label1" runat="server" Text="Übersicht ausgwählte Güterarten:" Font-Bold="true" Font-Underline="true"></asp:Label></td>
                            <td style="padding: inherit; margin: auto"></td>                        
                        </tr>
                        <tr>
                            <td style="padding: inherit; margin: auto">
                                <asp:Label ID="Label2" runat="server" Text="gewählte Anzahl:"></asp:Label></td>              
                            <td style="padding: inherit; margin: auto">
                                <telerik:RadNumericTextBox ID="tbAnzahlArtikel" runat="server" Culture="de-DE" DataType="System.Int32" DbValueFactor="1" LabelWidth="64px" ReadOnly="True" Skin="Office2010Blue" Value="0" Width="160px">
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
                                <asp:Label ID="Label3" runat="server" Text="gewähltes Gewicht:"></asp:Label></td>                           
                            <td style="padding: inherit; margin: auto"><telerik:RadNumericTextBox ID="tbGewichtArtikel" runat="server" Culture="de-DE" DataType="System.Decimal" DbValueFactor="1" LabelWidth="64px" ReadOnly="True" Skin="Office2010Blue" Value="0" Width="160px">
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
                <td style="border: thin solid #000080; padding: 1px; margin: 1px; top: auto; vertical-align: top;"  >
                    <table Width="300px" id="TableDisplay">
                        <tr>
                            <td style="padding: inherit; margin: auto" colspan="2">
                                <asp:Label ID="Label4" runat="server" Text="Übersicht angezeigte Güterarten:" Font-Bold="true" Font-Underline="true"></asp:Label></td>
                            <td style="padding: inherit; margin: auto"></td>                        
                        </tr>
                        <tr>
                            <td style="padding: inherit; margin: auto">
                                <asp:Label ID="Label5" runat="server" Text="Gesamtanzahl [Stk]:"></asp:Label></td>              
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
    <div>
        <table style=" width:100%">
            <tr>
                <td>
                    <telerik:RadToolBar ID="tbarMenu" runat="server" SingleClick="Button" OnButtonClick="tbarMenu_ButtonClick" Skin="Office2010Blue">
                            <Items>
                                <telerik:RadToolBarButton runat="server" Text="[zurück]" CommandName="BackMain" ToolTip="zurück zum Hauptmenü...">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" CommandName="NextStep" Text="[weiter]" ToolTip="weiter zur Artikelauswahl...">
                                </telerik:RadToolBarButton>
                            </Items>

                    </telerik:RadToolBar>
                </td>
            </tr>
 
        </table>
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
                        ShowFooter="True" 
                        OnColumnCreated="dgv_ColumnCreated" 
                        AllowMultiRowSelection="True" 
                        OnItemDataBound="dgv_ItemDataBound"
            
            >
                <GroupingSettings 
                    RetainGroupFootersVisibility="True" 
                    ShowUnGroupButton="True" 
                    UnGroupButtonTooltip="Gruppierung auflösen..." />
                <ExportSettings 
                    HideStructureColumns="True" 
                    IgnorePaging="True" 
                    ExportOnlyData="True" 
                    FileName="" 
                    OpenInNewWindow="True">
                    <Pdf PageHeight="210mm" PageWidth="297mm" PaperSize="A4" AllowPrinting="False">
                    </Pdf>
                </ExportSettings>
                <ClientSettings AllowAutoScrollOnDragDrop="False" AllowColumnsReorder="True" AllowDragToGroup="True">
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="true" />
                    <Scrolling AllowScroll="True" EnableVirtualScrollPaging="True" ScrollHeight="480px" UseStaticHeaders="True" />
                    <Resizing AllowResizeToFit="True" />
                </ClientSettings>
                <MasterTableView CommandItemDisplay="Top" 
                                EditMode="InPlace" 
                                EnableHeaderContextAggregatesMenu="True" 
                                EnableHeaderContextFilterMenu="True" 
                                EnableHeaderContextMenu="True" PageSize="50" 
                                ShowGroupFooter="True">
                    <Columns>
                          <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                            <ItemTemplate>
                              <asp:CheckBox ID="cbSelect" runat="server" OnCheckedChanged="ToggleRowSelection"
                                AutoPostBack="True" />
                            </ItemTemplate>
                            <HeaderTemplate>
                              <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
                                AutoPostBack="True" />
                            </HeaderTemplate>
                              <HeaderStyle Width="40px" />
                              <ItemStyle Width="40px" />
                          </telerik:GridTemplateColumn>

                    </Columns>
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
            <PagerStyle Position="Top" AlwaysVisible="True" BorderStyle="None" />
            <HeaderContextMenu AppendDataBoundItems="True">
            </HeaderContextMenu>
        </telerik:RadGrid>

    </div>
</asp:Content>

