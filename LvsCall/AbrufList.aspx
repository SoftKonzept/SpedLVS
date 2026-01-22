<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AbrufList.aspx.cs" Inherits="AbrufList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div id="DateFilter">
        <table style="width: 100%;">
            <tr>
<%--                <td Width="455px" style="border: thin solid #000080">
                     
                </td>--%>
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
                                <asp:Label ID="Label3" runat="server" Text="Artikelgewicht:"></asp:Label>

                            </td>                           
                            <td style="padding: inherit; margin: auto">
                                <telerik:RadNumericTextBox ID="tbGewichtArtikel" runat="server" Culture="de-DE" DataType="System.Decimal" DbValueFactor="1" LabelWidth="64px" ReadOnly="True" Skin="Office2010Blue" Value="0" Width="160px">
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
                                        <asp:ListItem>Werksnummer</asp:ListItem>
                                        <asp:ListItem>Abmessung(Dicke-Breite)</asp:ListItem>                                    
                                </asp:DropDownList>
                            </td>
                            <td style="padding: inherit; margin: auto">
                                <telerik:RadTextBox ID="txtSearch" runat="server" AutoPostBack="True" LabelWidth="64px" 
                                    OnTextChanged="txtSearch_TextChanged" 
                                    Resize="None" Skin="Office2010Blue" Width="160px"></telerik:RadTextBox>
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
                            <td style="padding: inherit; margin: auto">
                                <telerik:RadNumericTextBox ID="tbGesamtGewicht" runat="server" Culture="de-DE" DataType="System.Decimal" DbValueFactor="1" LabelWidth="64px" ReadOnly="True" Skin="Office2010Blue" Value="0" Width="120px">
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
                                <telerik:RadToolBarButton runat="server" Text="[zurück]" CommandName="BackCallBestand" ToolTip="zurück zur Bestandsübersicht..." Value="1" Visible="false">
                                </telerik:RadToolBarButton>
                                 <telerik:RadToolBarButton runat="server" Text="[zurück]" CommandName="BackReservationCall" ToolTip="zurück zu den vorgemerkten Artikeln zum Abruf..." Value="2" Visible="false">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" Text="[Abruf erstellen]" CommandName="DoCall" ToolTip="Abruf für die gewählten Artikel erstellen..." Value="1" Visible="false">
                                </telerik:RadToolBarButton>

                                <telerik:RadToolBarButton runat="server" Text="[zurück]" CommandName="BackRebookingBestand" ToolTip="zurück zur Bestandsübersicht..." Value="3" Visible="false">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" Text="[zurück]" CommandName="BackReservationRebooking" ToolTip="zurück vorgemerkten Artikeln zur Umbuchung..." Value="4" Visible="false">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" Text="[Umbuchung erstellen]" CommandName="DoRebooking" ToolTip="Umbuchung für die gewählten Artikel erstellen..." Value="3"  Visible="false">
                                </telerik:RadToolBarButton>

                                <telerik:RadToolBarButton runat="server" Text="[Liste leeren]"  CommandName="ClearList" ToolTip="selektierte Artikel entfernen..." Value="0">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" Text="[Suche starten]" CommandName="StartSearch" ToolTip="nach Artikel gemäß Sucheinstellungen suchen..." Value="0">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" Text="[Suche leeren]"  CommandName="ClearSearch" ToolTip="Sucheinstellungen zurücksetzen..." Value="0">
                                </telerik:RadToolBarButton>

                                <telerik:RadToolBarButton runat="server" Text="[weiter]" CommandName="NextStepCall" ToolTip="weiter zu den offenen Abrufen..." Value="1" Visible="false">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" Text="[weiter]" CommandName="NextStepCallFinish" ToolTip="Prozess Abrufe verlassen..." Value="2" Visible="false">
                                </telerik:RadToolBarButton>
                                
                                <telerik:RadToolBarButton runat="server" Text="[weiter]" CommandName="NextStepRebooking" ToolTip="weiter zu den offenen Umbuchungen..." Value="3" Visible="false">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" Text="[weiter]" CommandName="NextStepRebookingFinish" ToolTip="Prozess Umbuchung verlassen..." Value="4" Visible="false">
                                </telerik:RadToolBarButton>
                            </Items>
                    </telerik:RadToolBar>
                </td>
            </tr>
        </table>
    </div>

    <div id="divDGV">
        <telerik:RadGrid ID="dgv" runat="server" 
            OnNeedDataSource="dgv_NeedDataSource" 
            AutoGenerateDeleteColumn="True"
            AutoGenerateEditColumn="True" 
            Culture="de-DE" 
            OnColumnCreated="dgv_ColumnCreated"   
            Skin="Office2010Blue" 
            ShowDesignTimeSmartTagMessage="False" 
            AllowFilteringByColumn="True" 
            OnGridExporting="dgv_GridExporting"     
            style="margin-top: 0px" OnItemDataBound="dgv_ItemDataBound" OnItemCommand="dgv_ItemCommand" AllowMultiRowSelection="True" OnColumnCreating="dgv_ColumnCreating">
            <ExportSettings HideStructureColumns="True" IgnorePaging="True" ExportOnlyData="True" FileName="" OpenInNewWindow="True">
                <Pdf PageHeight="210mm" PageWidth="297mm" PaperSize="A4">
                </Pdf>
            </ExportSettings>
            <ClientSettings AllowAutoScrollOnDragDrop="False">
                <Scrolling AllowScroll="True" EnableVirtualScrollPaging="True" ScrollHeight="450px" UseStaticHeaders="True" />
            </ClientSettings>
            <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" AllowCustomPaging="True" AllowFilteringByColumn="False">
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
                <PagerStyle AlwaysVisible="True" />
                <CommandItemStyle Height="15px" />
            </MasterTableView>
            <HeaderContextMenu AppendDataBoundItems="True">
            </HeaderContextMenu>
        </telerik:RadGrid>
    </div>
 
</asp:Content>

