<%@ Page Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="Preferences.aspx.cs" Inherits="Dashboard.Preferences" Title="Set My Dashboard Preferences" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<form id="form1" runat="server">
    Preference setting on this page allows you to customize the display on <b>MY Dashboard</b> page.
    Select the Instance and Application from the list below which you want to be displayed on the MY Dashboard page.
    <br /><br />
    <asp:Panel ID="MessagePanel" runat="server">
        <asp:Literal ID="MessageLine" runat="server"></asp:Literal>
    </asp:Panel>
    
    <asp:Panel ID="DataPanel" runat="server">
    <table border="1" class="preferences">
        <tr>
            <th colspan='5'>Instance Selection</th>
        </tr>
        <tr>
            <td style="width: 205px;" align="right">
                <asp:ListBox ID="ListBoxSourceInstance" runat="server" Width="200px" Height="200px"></asp:ListBox></td>
            <td style="width: 110px; vertical-align: middle;" align="center">
                <asp:Button ID="ButtonRightInstance" runat="server" Text=">" BorderStyle="Solid" Font-Bold="True" Width="100px" OnClick="ButtonRightInstance_Click" />
                <asp:Button ID="ButtonLeftInstance" runat="server" Text="<" BorderStyle="Solid" Font-Bold="True" Width="100px" OnClick="ButtonLeftInstance_Click" />
            </td>
            <td style="width: 205px;" align="left">
                <asp:ListBox ID="ListBoxTargetInstance" runat="server" Width="200px" Height="200px"></asp:ListBox></td>
        </tr>
    </table>
    <table border="1" class="preferences">
        <tr>
            <th colspan='5'>Application Selection</th>
        </tr>
        <tr>
            <td style="width: 205px" align="right">
                <asp:ListBox ID="ListBoxSourceApplication" runat="server" Width="200px" Height="200px"></asp:ListBox></td>
            <td style="width: 110x; vertical-align: middle;" align="center">
                <asp:Button ID="ButtonRightApplication" runat="server" Text=">" BorderStyle="Solid" Font-Bold="True" Width="100px" OnClick="ButtonRightApplication_Click" />
                <asp:Button ID="ButtonLeftApplication" runat="server" Text="<" BorderStyle="Solid" Font-Bold="True" Width="100px" OnClick="ButtonLeftApplication_Click" /></td>
            <td style="width: 205px" align="left">
                <asp:ListBox ID="ListBoxTargetApplication" runat="server" Width="200px" Height="200px"></asp:ListBox></td>
        </tr>
    </table>
    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="ApplyChanges_Click" />
        </asp:Panel>
</form>
</asp:Content>
