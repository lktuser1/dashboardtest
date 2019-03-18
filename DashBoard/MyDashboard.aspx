<%@ Page Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="MyDashboard.aspx.cs" Inherits="Dashboard.MyDashboard" Title="My Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= getLatestData() %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">
   
    <%= getLatestAlertData() %>
</asp:Content>

