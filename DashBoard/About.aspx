<%@ Page Language="C#" 
         MasterPageFile="~/IntranetCore.Master" 
         AutoEventWireup="true" 
         CodeBehind="About.aspx.cs" 
         Inherits="Dashboard.About" %>
         
<%@ MasterType VirtualPath="~/IntranetCore.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <p>Version: 3.0<br />
<EDS:PageContact id="PageContact1" runat="server" ContactName="ESM Dashboard Support Team" ContactEmail="sradashboardoperations@dxc.com" />
    <p>
<br />
<div style="text-align:right; margin-top: -30px;">
    <asp:Localize id="ReturnToMain" runat="server" />
</div> 
</asp:Content>

