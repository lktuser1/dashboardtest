<%@ Page Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="EDS.Intranet.Error.AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<form runat="server" id="frmaccessdenied">
    <br />
    <img src="~/EDSIntranet/Images/Padlock.gif"  alt="Access Denied"  runat="server" style="float: left; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; padding-right: 5px;"/>
    <span style="font-size: 10pt; font-weight: bold;">
    You are not authorized to view this page.  Either you are not currently logged in, or you do not have access to this page 
    within the application. Please contact the application administrator to obtain access.
    </span>
    <br/>
    <br/>
    <br/>
    <hr/>
    <asp:Label ID="LoginStatus" runat="server" Text="Label"></asp:Label><br />
    <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>Authenticated:</td>
            <td style="width:100%"><%=User.Identity.IsAuthenticated%></td>
        </tr>
        <tr>
            <td style="white-space:nowrap">Authentication Type:</td>
            <td style="width:100%"><%=User.Identity.AuthenticationType%></td>
        </tr>
        <tr>
            <td>IP Address:</td>
            <td><%=Request.ServerVariables["remote_addr"]%></td>
        </tr>
        <tr>
            <td>User Agent:</td>
            <td><%=Request.ServerVariables["http_user_agent"]%></td>
        </tr>
         <tr>
            <td>Authorized Roles:</td>
            <td style="padding-top:10px"><asp:BulletedList ID="RoleList" runat="server"></asp:BulletedList></td>
        </tr>
    </table>
</form>

</asp:Content>
