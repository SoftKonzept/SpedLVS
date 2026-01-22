<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserEdit.aspx.cs" Inherits="UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #DGV
        {
            width: 1447px;
        }
        .styleCol1
        {
            width: 120px;
            height: 20px;
        }
        .styleCol2
        {
            width: 100px;
            height: 20px;
        }
        .styleCol3
        {
            height: 20px;
            width: auto;
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
                        <telerik:RadButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Skin="Office2010Blue" Text="Refresh">
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
                                    <td id="kName" class="styleCol1">Name:</td>
                                    <td id="vName" class="styleCol2"><telerik:RadTextBox ID="tbName" runat="server" Skin="Office2010Blue" Text="" LabelWidth="64px" Resize="None" Width="160px"></telerik:RadTextBox></td>
                                    <td class="syleCol3"></td>
                                </tr>
                                <tr>
                                    <td id="kVorname" class="styleCol1">Vorname:</td>
                                    <td id="vVorname" class="styleCol2"><telerik:RadTextBox ID="tbVorname" runat="server" Skin="Office2010Blue" Text=""></telerik:RadTextBox></td>
                                    <td class="syleCol3"></td>
                                </tr>
                                <tr>
                                    <td id="kLogin" class="styleCol1">Login:</td>
                                    <td id="vLogin" class="styleCol2"><telerik:RadTextBox ID="tbLogin" runat="server" Skin="Office2010Blue" Text=""></telerik:RadTextBox></td>
                                    <td class="syleCol3"></td>
                                </tr>
                                <tr>
                                    <td id="kPass" class="styleCol1">Passwort:</td>
                                    <td id="vPass" class="styleCol2">
                                        <telerik:RadTextBox ID="tbPass" runat="server" Skin="Office2010Blue" Text="" ReadOnly="True"></telerik:RadTextBox>
                                    </td>
                                    <td id="PassBtn" class="styleCol3"> 
                                        <telerik:RadButton ID="RadButton1" runat="server" Text="Passwort erzeugen" Height="22px" OnClick="btnPasswort_Click" Skin="Office2010Blue"></telerik:RadButton>
                                        <telerik:RadButton ID="RadButton2" runat="server" Text="Passwort editieren" Height="22px" OnClick="btnPassEdit_Click" Skin="Office2010Blue"></telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="kRole" class="styleCol1">Rolle:</td>
                                    <td id="vRole" class="styleCol2">
                                        <telerik:RadComboBox ID="cbRole" runat="server" Culture="de-DE" Skin="Office2010Blue" Height="200px" Width="158px" EmptyMessage="Bitte Rolle auswählen" RenderMode="Lightweight"></telerik:RadComboBox>
                                    </td>
                                    <td class="styleCol3"></td>
                                </tr>
                                <tr>
                                    <td id="kSchicht" class="styleCol1">Schicht::</td>
                                    <td id="vSchicht" class="styleCol2">
                                        <telerik:RadTextBox ID="tbSchicht" runat="server" Skin="Office2010Blue" Text=""></telerik:RadTextBox></td>
                                    <td  class="styleCol3"></td>
                                </tr>
                                <tr>
                                    <td id="kFirma" class="styleCol1">Firma:</td>
                                    <td id="vFirma" class="styleCol2"><telerik:RadTextBox ID="tbFirma" runat="server" ReadOnly="True" Skin="Office2010Blue" Text=""></telerik:RadTextBox></td>
                                    <td  class="styleCol3"></td>
                                </tr>
                                <tr>
                                    <td id="kDateAdd" class="syleCol1">Erstellt:</td>
                                    <td id="vDateAdd" class="styleCol2"><telerik:RadTextBox ID="tbDateAdd" runat="server" ReadOnly="True" Skin="Office2010Blue" Text=""></telerik:RadTextBox></td>
                                    <td  class="styleCol3"></td>
                                </tr>
                            </table>
                        </div>
                    </td>

                </tr>
            </table>   
        </div>

</asp:Content>

