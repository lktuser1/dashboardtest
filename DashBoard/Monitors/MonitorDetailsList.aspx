<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="MonitorDetailsList.aspx.cs" Inherits="Dashboard.Monitors.MonitorDetailsList" enableEventValidation ="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
         <div align="right">
              <asp:Button ID="Export" runat="server" Text="Export" OnClick="Export_Click" />
        </div>
    <div>
        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="450" Width="1300">
    <asp:GridView ID="GridView1"  runat="server" 
        cssClass ="reports"
       AllowSorting="True" 
    onsorting="GridView1_Sorting"
    CurrentSortField="id" 
    CurrentSortDirection="ASC">
</asp:GridView>
</asp:Panel>

    </div>
    </form>
</asp:Content>

