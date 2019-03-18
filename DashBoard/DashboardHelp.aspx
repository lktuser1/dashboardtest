<%@ Page Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="DashboardHelp.aspx.cs" Inherits="EDSIntranet.DashboardHelp" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="side">
<h2><asp:Literal ID="WhatsNewTag" runat="server" Text="Download"/></h2>
<a href="EDSIntranet/doc/Dashboard_User_Guide.pptx" target="_blank">Dashboard User Guide v1.0</a><br />
<a href="EDSIntranet/doc/Dashboard Improvements - V2.0.pptx" target="_blank">Dashboard User Guide v2.0</a><br />
<a href="EDSIntranet/doc/Dashboard Improvements - V3.0.docx" target="_blank">Dashboard User Guide v3.0</a><br />
</div>
<dl class="topics">
	<dd>
	</dd>
</dl>

    <p class="MsoNormal" style="margin: 0in 0in 10pt">
        <b style="mso-bidi-font-weight: normal"><span style="font-size: 14pt; line-height: 115%">
            <span style="font-family: Calibri">Dashboard is a Web application that displays real-time
                status for various monitors. <BR />It also provides Historical Reporting of backlog for past failed processes. <br />Detail information for all Monitors is also displayed.<?xml namespace="" ns="urn:schemas-microsoft-com:office:office"
                    prefix="o" ?><o:p></o:p></span></span></b></p>
   
    <p class="MsoNormal" style="margin: 0in 0in 10pt">
        <b style="mso-bidi-font-weight: normal"><span style="font-size: 11pt"><span style="font-family: Calibri">
            Monitors<o:p></o:p></span></span></b></p>
    <p class="MsoNormal" style="margin: 0in 0in 10pt; text-indent: 0.5in">
        <span style="font-size: 11pt; font-family: Calibri">Monitors <span style="mso-spacerun: yes">
            &nbsp;</span>in the current Dashboard help to monitor backlogs with Schedulers, Events and RWS tables in ServiceManager application.<BR />It also monitors Incident and Work Order Subscription backlogs from Service Manager to Service Agent.
            RWS Request/Reply Web Services are also monitored.
        </span></p>
    <p class="MsoNormal" style="margin: 0in 0in 10pt; text-indent: 0.5in">
        <span style="font-size: 11pt; font-family: Calibri">Current Dashboard is designed <span
            style="mso-spacerun: yes">&nbsp;</span>by querying<span style="mso-spacerun: yes">&nbsp;
            </span>the tables from standby database.</span></p>
    <p class="MsoNormal" style="margin: 0in 0in 30pt; text-indent: 0.5in">
        <span style="font-size: 11pt; font-family: Calibri">The backlog indication can be differentiated
            by the colors like GREEN ,YELLOW and RED, based on current thresholds set for these monitors.<span style="mso-spacerun: yes">&nbsp;</span>Green indicates that there is no backlog and monitors are working fine.Red can be seen, if there is any backlog that needs an action.Yellow indicates a problem in its initial stage.
            <span style="mso-spacerun: yes">Purple color indicates some or all monitor’s are not 
receiving the current update. </span>
         <span style="mso-spacerun: yes">The Dashboard Detail screen also displays monitors with error message and white background color, when a potential problem may be starting to occur.</span></span>
    </p>

   <p class="MsoNormal" style="margin: 0in 0in 10pt">
        <b style="mso-bidi-font-weight: normal"><span style="font-size: 11pt"><span style="font-family: Calibri">
            Sitemap<o:p></o:p></span></b></p>
    <p  class="MsoNormal" style="margin: 0in 0in 10pt; text-indent: 0.5in">
        <span style="font-size: 11pt; font-family: Calibri">This site is developed to track the status of various applications.
  Various options available on this sites are as follows : </span>
  </p>

 <dl class="topics" style="margin: 0in 0in 10pt; text-indent: 0.5in">
    <dt><a href="Main.aspx" style="font-size: 11pt; font-family: Calibri">Dashboard</a></dt>
    <dd>Display status of all applications monitored by this site.</dd>
    <dt><a href="MyDashboard.aspx">My Dashboard</a></dt>
    <dd>Display status of selected applications as defined by a user on the Preferences tab.</dd>
    <dt><a href="Reports/DashboardReports.aspx">Historical Reporting</a></dt>
    <dd>Displays information of past failed processes or backlog based on selected criteria. Monitor detail information is also displayed.</dd>
    <dt><a href="Preferences.aspx">Preferences</a></dt>
    <dd>Allow setting up a list of instances and applications to be displayed on My Dashboard.</dd>
    <dt><a href="Comments.aspx">Contact us</a></dt>
    <dd>Allows you to leave your feedback here.</dd>
</dl>
</asp:Content>
