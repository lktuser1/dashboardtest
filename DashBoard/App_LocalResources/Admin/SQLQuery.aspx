<%@ Page Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="SQLQuery.aspx.cs" Inherits="EDSIntranet.Admin.SQLQuery" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<form id="sqlForm" runat="server">
    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtQuery" Text="Enter SQL Query below"></asp:Label><br />
    <asp:TextBox ID="txtQuery" runat="server" Height="113px" Width="835px" AutoCompleteType="Disabled" Rows="5" TextMode="MultiLine"></asp:TextBox><br /><br />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit Query" /><br />
    <br />
    <asp:Literal ID="ltrMessage" runat="server" Mode="Encode" Visible="False"></asp:Literal><br />
    <asp:GridView ID="gvResult" runat="server">
    </asp:GridView>
</form>
</asp:Content>
