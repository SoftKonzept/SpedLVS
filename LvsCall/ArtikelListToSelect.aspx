<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ArtikelListToSelect.aspx.cs" Inherits="ArtikelListToSelect" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DateFilter">
        <table style="width: 100%;">
            <tr>
                <td Width="455px" style="border: thin solid #000080">
                     <asp:Table ID="TableEingabe" runat="server" Width="455px">
                        <asp:TableRow Height="12">
                            <asp:TableCell Width="100" Text="Eintrefftermin:"></asp:TableCell>
                            <asp:TableCell>
                                <telerik:RadDatePicker ID="dtpEintrefftermin" runat="server" 
                                    OnSelectedDateChanged="dtpEintrefftermin_SelectedDateChanged" 
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
                                    AutoPostBack="True" OnSelectedDateChanged="dtpEintreffZeit_SelectedDateChanged"
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
                                    <telerik:RadTextBox ID="tbReferenz" runat="server" OnTextChanged="tbReferenz_TextChanged"  AutoPostBack="True"></telerik:RadTextBox>
                            </asp:TableCell>

                        </asp:TableRow>
                        <asp:TableRow Height="12">
                            <asp:TableCell Width="100" Text="Abladestelle:"></asp:TableCell>
                            <asp:TableCell>
                               <telerik:RadTextBox ID="tbAbladestelle" runat="server" OnTextChanged="tbAbladestelle_TextChanged"  AutoPostBack="True" ></telerik:RadTextBox>
                            </asp:TableCell>
                        </asp:TableRow>                 
                     </asp:Table>
                </td>
                <td Width="360px" style="border: thin solid #000080; padding: 1px; margin: 1px;">
                    <table Width="350px">
                        <tr>
                            <td style="padding: inherit; margin: auto">
                                <asp:Label ID="Label1" runat="server" Text="Übersicht ausgwählte Arikel:" Font-Bold="true" Font-Underline="true"></asp:Label></td>
                            <td style="padding: inherit; margin: auto"></td>                        
                        </tr>
                        <tr>
                            <td style="padding: inherit; margin: auto">
                                <asp:Label ID="Label2" runat="server" Text="Artikelanzahl:"></asp:Label></td>              
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
                                <asp:Label ID="Label3" runat="server" Text="Artikelgewicht:"></asp:Label></td>                           
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
                            <td style="padding: inherit; margin: auto">    
                                <asp:DropDownList ID="comboSearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="comboSearch_SelectedIndexChanged">
                                        <asp:ListItem>wählen Sie ein Suchfeld</asp:ListItem>
                                        <asp:ListItem>LVSNr</asp:ListItem>
                                        <asp:ListItem>Charge</asp:ListItem>
                                        <asp:ListItem>Produktionsnummer</asp:ListItem>
                                        <asp:ListItem>Benennung</asp:ListItem>
                                        <asp:ListItem>Materialnummer</asp:ListItem>
                                        <asp:ListItem>Abmessung(Dicke-Breite)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="padding: inherit; margin: auto">
                                <telerik:RadTextBox ID="txtSearch" runat="server" AutoPostBack="True" LabelWidth="64px" OnTextChanged="txtSearch_TextChanged" Resize="None" Skin="Office2010Blue" Width="160px"></telerik:RadTextBox>
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
                                <asp:Label ID="Label7" runat="server" Text="Gesamtmenge [Stk.]:"></asp:Label></td>                           
                            <td style="padding: inherit; margin: auto"><telerik:RadNumericTextBox ID="tbGesamtMenge" runat="server" Culture="de-DE" DataType="System.Decimal" DbValueFactor="1" LabelWidth="64px" ReadOnly="True" Skin="Office2010Blue" Value="0" Width="120px">
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
    <div>
        <table style=" width:100%" border="0" >
            <tr>
                <td>
                    <telerik:RadToolBar ID="RadToolBar1" runat="server" SingleClick="Button" OnButtonClick="RadToolBar1_ButtonClick" Skin="Office2010Blue">
                            <Items>
                                <telerik:RadToolBarButton runat="server" CommandName="Back" Text="[zurück]" ToolTip="zurück zur Bestandsübersicht...">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" CommandName="AddAbruf" Text="[Artikel vormerken]"  ToolTip="Artikel vormerken...">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" CommandName="StartSearch" Text="[Suche starten]" ToolTip="nach Artikel gemäß Sucheinstellungen suchen...">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" CommandName="ClearSearch" Text="[Suche leeren]" ToolTip="Sucheinstellungen zurücksetzen...">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" CommandName="NextStep" Text="[weiter]" ToolTip="weiter zu den vorgemerkten Artikeln...">
                                </telerik:RadToolBarButton>
                            </Items>
                    </telerik:RadToolBar>
                </td>
            </tr>
        </table>
    </div>

    <div id="Div3">
            <telerik:RadGrid 
                ID="dgv" 
                runat="server" 
                AllowSorting="True" 
                Culture="de-DE" 
                Skin="Office2010Blue" 
                ShowDesignTimeSmartTagMessage="False" 
                OnNeedDataSource="dgv_NeedDataSource" 
                AllowCustomPaging="True"  
                ShowStatusBar="True" 
                AllowMultiRowSelection="True" 
                OnColumnCreated="dgv_ColumnCreated" 
                OnItemDataBound="dgv_ItemDataBound" 
                EnableLinqExpressions="False" 
                ShowFooter="True" >
                <ExportSettings HideStructureColumns="True" IgnorePaging="True" ExportOnlyData="True" FileName="" OpenInNewWindow="True">
                    <Pdf PageHeight="210mm" PageWidth="297mm" PaperSize="A4">
                    </Pdf>
                </ExportSettings>
                <ClientSettings AllowAutoScrollOnDragDrop="False" AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
                <MasterTableView CommandItemDisplay="Top" 
                                 EditMode="InPlace" 
                                 EnableHeaderContextAggregatesMenu="True" 
                                 EnableHeaderContextFilterMenu="True" 
                                 EnableHeaderContextMenu="True">
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
                <PagerStyle Position="Top" AlwaysVisible="True" />
                <HeaderContextMenu AppendDataBoundItems="True">
                </HeaderContextMenu>
            </telerik:RadGrid>

    </div>
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style2
        {
            top: auto;
            width: 366px;
        }
    </style>
</asp:Content>


