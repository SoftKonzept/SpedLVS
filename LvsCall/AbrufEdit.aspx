<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AbrufEdit.aspx.cs" Inherits="AbrufEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #DGV
        {
            width: 1447px;
        }
        .auto-style2
        {
            width: 131px;
        }
        </style>
    <script type="text/javascript" id="telerikClientEvents1">
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div id="Table">
            <table style="width: 100%;">
                <tr id="Menübutton">
                    <td>&nbsp;
                        <telerik:RadButton ID="btnSave" runat="server" Text="Speichern" Skin="Office2010Blue" OnClick="btnSave_Click"></telerik:RadButton>
                        <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" Skin="Office2010Blue" Text="Abbrechen">
                        </telerik:RadButton>
                    </td>

                </tr>
                <tr >
                    <td>&nbsp;</td>

                </tr>
                <tr id="Editarea">
                    <td>
                        <div id="Edit">
                            <table style="width: 100%;">
                                <tr>
                                    <td id="kArtikelID" class="auto-style2">LVSNr / ArtikelID:</td>
                                    <td id="vArtikelID"><telerik:RadTextBox ID="tbLVSNrArtikel" runat="server" Skin="Office2010Blue" Text="" LabelWidth="64px" Resize="None" Width="160px" ReadOnly="True"></telerik:RadTextBox></td>
                                </tr>
                                <tr>
                                    <td id="kEintreff-Datum" class="auto-style2">Eintreff-Datum:</td>
                                    <td id="vEintreff-Datum">
                                             <telerik:RadDatePicker ID="dtpEintrefftermin" runat="server" 
                                                OnSelectedDateChanged="dtpEintrefftermin_SelectedDateChanged" 
                                                AutoPostBack="True" 
                                                Culture="de-DE" 
                                                FocusedDate="2015-09-15" 
                                                MinDate="2015-09-15" 
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
                                    </td>
                                </tr>
                                <tr>
                                    <td id="kEintreff-Zeit" class="auto-style2">Eintreff-Zeit:</td>
                                    <td id="vEintreff-Zeit">
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
                                    </td>
                                </tr>
                                <tr>
                                    <td id="kPass" class="auto-style2">Abladestelle:</td>
                                    <td id="vPass"><telerik:RadTextBox ID="tbAbladestelle" runat="server" Skin="Office2010Blue" Text=""></telerik:RadTextBox></td>
                                </tr>
                                <tr>
                                    <td id="kSchicht" class="auto-style2">Referenz:</td>
                                    <td id="vSchicht"><telerik:RadTextBox ID="tbReferenz" runat="server" Skin="Office2010Blue" Text=""></telerik:RadTextBox></td>
                                </tr>
                            </table>
                        </div>
                    </td>

                </tr>
            </table>   
        </div>

</asp:Content>

