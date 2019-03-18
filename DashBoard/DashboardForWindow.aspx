<%@ Page Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="DashboardForWindow.aspx.cs" Inherits="EDSIntranet.DashboardForWindow" Title="Dashboard For Windows" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="side">
<h2><asp:Literal ID="WhatsNewTag" runat="server" Text="Download"/></h2>
<a href="Download/download.htm" target="_blank">Dashboard Window Client</a><br />
<a href="EDSIntranet/doc/Dashboard_For_windows_User_Guide.doc" target="_blank">User Guide</a><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
</div>
<b>Dashboard for Window</b> is a Window based program to monitor
the status of Application. It has similar feature as Web based dashboard
as well as some additional functionality.
<dl class="topics">
	<dt><asp:Literal ID="TopicOne" runat="server" Text="Overview"/></dt>
	<dd>Layout of application is displayed below. It can be configured to display either in 
	horizontal or vertical mode. It can also be configured to display and track a subset of Application.
	For detailed configuration information, see the user guide.
	When minimized, it will minimized to your system tray.<br/><br/>
	<b>Horizontal Layout</b><br />
	<img src="EDSIntranet/Images/w_layout_1.gif" /><br/><b>Vertical Layout</b><br /><img src="EDSIntranet/Images/w_layout_21.gif" />
	</dd>
	<dt><asp:Literal ID="TopicTwo" runat="server" Text="Displaying Alert"/></dt>
	<dd>Whenever a severity 1 or 2 condition is encounted by monitored application, This tool will display
	the information in the popup window as shown below. It will also announce the same message as well.<br/><br/>
        <img src="EDSIntranet/Images/w_alert.gif" />
	</dd>
	<dt><asp:Literal ID="TopicThree" runat="server" Text="Getting Alert Details"/></dt>
	<dd>You can get detailed information about any alert by right clicking on the color status bar as shown below.<br/><br/>
        <img src="EDSIntranet/Images/w_alert_details.gif" />
	</dd>
</dl>
<br />
More details about the feature and functionality of this tool, Please refer to User Guide link in the download section.


</asp:Content>
