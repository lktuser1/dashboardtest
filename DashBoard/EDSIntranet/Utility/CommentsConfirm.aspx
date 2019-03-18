<%@ Page Language="C#" 
         MasterPageFile="~/IntranetCore.Master" 
         AutoEventWireup="true" 
         CodeBehind="CommentsConfirm.aspx.cs" 
         Inherits="EDS.Intranet.Utility.CommentsConfirm"  %>
         
<%@ MasterType VirtualPath="~/IntranetCore.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:MsgCommentsConfirm %>" />
    </p>
    <p>&nbsp;</p>
    <div style="text-align:right; margin-top: -30px;">
        <asp:Localize ID="ReturnToMain" runat="server" />
    </div> 
</asp:Content>
