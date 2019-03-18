<%@ Page Language="C#" 
         MasterPageFile="~/IntranetCore.master" 
         AutoEventWireup="true" 
         CodeBehind="Preferences.aspx.cs" 
         Inherits="EDSIntranet.EDSIntranet.Utility.Preferences" %>

<%@ MasterType VirtualPath="~/IntranetCore.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:MsgPreferences %>" /></p>
    <p />
    <form id="frmPreferences" action=""  runat="server">
    <table>
    <tr>
    <td align="right">
        <asp:Label ID="lblLanguage" runat="server" Font-Bold="True" Text="<%$ Resources:LabelLanguage %>"></asp:Label>
    </td>
    <td style="width:90%">        
        <asp:DropDownList 
            id="ddlLanguage" 
            runat="server" 
            AutoPostBack="false"
            OnLoad="Language_OnLoad" >
        </asp:DropDownList>
    </td>
    </tr>
    </table>
    <p />&nbsp;
    <p />
    <asp:Button 
        id="btnSave" 
        runat="server" 
        CssClass="submit"
        OnClick="SaveButton_Click"
        text="<%$ Resources:BtnSave %>" />
    </form>
<br />
<div style="text-align:right; margin-top: -30px;">
    <asp:Localize ID="ReturnToMain" runat="server" />
</div> 
</asp:Content>
