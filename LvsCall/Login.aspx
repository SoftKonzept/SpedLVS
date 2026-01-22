<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="conLogin" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
   <asp:Table ID="Table1" runat="server" align="center" BorderColor="#25509E" 
    BorderStyle="Solid" Height="94px" Width="242px" BorderWidth="1px" 
    BackColor="White" Font-Bold="True" Font-Italic="True" ForeColor="#25509E">
    <asp:TableRow ID="TableRow1" runat="server" BackColor="#25509E" HorizontalAlign="Center" 
      VerticalAlign="Top">
      <asp:TableCell ID="TableCell1" runat="server" ColumnSpan="2" Font-Bold="True" 
        Font-Italic="True" Font-Strikeout="False" Font-Underline="False" 
        ForeColor="White" Text="<%$ Resources:Text, Login %>">

      </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow2" runat="server">
      <asp:TableCell ID="TableCell2" runat="server" Width="90px" Text="<%$ Resources:Text, Firma %>"></asp:TableCell>
      <asp:TableCell ID="TableCell3" runat="server"><asp:TextBox ID="txtCompany" runat="server" Width="150"></asp:TextBox>
</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow3" runat="server">
      <asp:TableCell ID="TableCell4" runat="server" Width="90px" Text="<%$ Resources:Text, Benutzer %>"></asp:TableCell>
      <asp:TableCell ID="TableCell5" runat="server"><asp:TextBox ID="txtUser" runat="server" Width="150"></asp:TextBox>
</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow4" runat="server">
      <asp:TableCell ID="TableCell6" runat="server" Width="90px" Text="<%$ Resources:Text, Passwort %>"></asp:TableCell>
      <asp:TableCell ID="TableCell7" runat="server"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150"></asp:TextBox>
</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
      <asp:TableCell ID="TableCell8" runat="server" Width="100px" ></asp:TableCell>
      <asp:TableCell ID="TableCell9" runat="server" Height="25">                
          <asp:Button ID="btnLogin" runat="server" Text="<%$ Resources:Text, Login %>" Width="56px" 
                    BackColor="White" BorderColor="#25509E" BorderStyle="Solid" Font-Bold="True" 
                    Font-Italic="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#25509E" 
                    onclick="btnLogin_Click" style="height: 26px" 
                    />
       </asp:TableCell>
    </asp:TableRow>
      <asp:TableRow ID="TableRow5" runat="server" BackColor="#FF3300" Font-Bold="True" 
          Font-Italic="True" Font-Names="Arial" ForeColor="White" 
          HorizontalAlign="Center" VerticalAlign="Top" Visible="False">
          <asp:TableCell ID="TableCell10" runat="server" ColumnSpan="2"></asp:TableCell>
      </asp:TableRow>
      <asp:TableRow ID="TableRow6" runat="server" BackColor="#FF3300" Font-Bold="True" 
          Font-Italic="True" Font-Names="Arial" ForeColor="White" 
          HorizontalAlign="Center" VerticalAlign="Top" Visible="False">
          <asp:TableCell ID="TableCell11" runat="server" ColumnSpan="2">

          </asp:TableCell>
      </asp:TableRow>
  </asp:Table>

</asp:Content>

