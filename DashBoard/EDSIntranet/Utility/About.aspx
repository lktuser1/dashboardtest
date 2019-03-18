<%@ Page Language="C#" 
         MasterPageFile="~/IntranetCore.Master" 
         AutoEventWireup="true" 
         CodeBehind="About.aspx.cs" 
         Inherits="EDS.Intranet.Utility.About" %>
         
<%@ MasterType VirtualPath="~/IntranetCore.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <p>
 This site is for &lt;--fill in the purpose of the site including your target audience--&gt;. 
 <br />See the <a href="http://web.standards.eds.com/sg/aboutus/index.html">About Us Topic</a> on Web Standards for more details on what is needed. 
 </p>
 <p>
 <strong>Note:</strong> The EDS Intranet templates have been tested for W3C accessibility checkpoints. 
 <br /><a href="http://web.standards.eds.com/templates/internal/about.html">Learn how these templates are more accessible</a> than previous versions.
 </p>
 
 <dl class="topics">
    <dt><a href="Comments.aspx"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:HeaderContact %>" /></a></dt>
    <dd><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:MsgContact %>" /></dd>
	<dt><a href="Privacy.aspx"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:HeaderPrivacy %>" /></a></dt>
	<dd><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:MsgPrivacy %>" /></dd>
	<dt><a href="Preferences.aspx"><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:HeaderPreferences %>" /></a></dt>
	<dd><asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:MsgPreferences %>" /></dd>
</dl>

<EDS:PageContact id="PageContact1" runat="server" ContactName="Firstname Lastname, role" ContactEmail="first.last@eds.com" ContactNetid="czwlcq" />
<br />
<div style="text-align:right; margin-top: -30px;">
    <asp:Localize id="ReturnToMain" runat="server" />
</div> 
</asp:Content>

