<%@ Page Language="C#" 
         MasterPageFile="~/IntranetCore.Master" 
         AutoEventWireup="true" 
         CodeBehind="Map.aspx.cs" 
         Inherits="EDS.Intranet.Utility.Map" %>
         
<%@ MasterType VirtualPath="~/IntranetCore.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<p><strong>Directions: </strong>Include the main category names along with the main topics of that category. The site map should only show two levels deep of the site's information architecture.</p>
      <dl class="sitemap">
        <dt><a id="A1" href="~/Category1/Category1Main.aspx" runat="server">Category One </a></dt>
        <dd>
          <ul>
            <li><a id="A2" href="~/Category1/Category1Topic1.aspx" runat="server">Topic One</a> &nbsp;|&nbsp; </li>
            <li><a id="A3" href="~/Category1/Category1Topic2.aspx" runat="server">Topic Two</a> &nbsp;|&nbsp; </li>
            <li><a id="A4" href="~/Category1/Category1QA.aspx" runat="server">Questions &amp; Answers</a></li>
          </ul>
        </dd>
        <dt><a id="A5" href="~/Category2/Category2Main.aspx" runat="server">Category Two</a></dt>
        <dt><a id="A6" href="~/Category3/Category3Main.aspx" runat="server">Category Three</a></dt>
        <dd>
          <ul>
						<li><a id="A7" href="~/Category3/category3Topic1.aspx" runat="server">Topic One - Tables</a> &nbsp;|&nbsp; </li>
						<li><a id="A8" href="~/Category3/category3Topic2.aspx" runat="server">Topic Two- Images &amp; Icons</a> &nbsp;|&nbsp; </li>
            <li><a id="A9" href="~/Category3/Category3Glossary.aspx" runat="server">Glossary</a></li>
          </ul>
        </dd>
        <dt><a id="A10" href="~/Category4/Category4Main.aspx" runat="server">Category Four</a></dt>
					<dd>
						          <ul>
						<li><a id="A11" href="~/Category4/Category4Topic1.aspx" runat="server">Topic One - Resources &amp; Downloadables</a> &nbsp;|&nbsp; </li>
						<li><a id="A12" href="~/Category4/Category4Topic2.aspx" runat="server">Topic Two- Contacts</a> &nbsp;|&nbsp; </li>
	          <li><a id="A13" href="~/Category4/Category4Topic3.aspx" runat="server">Topic Three - A Form</a></li>
	          </ul>
					</dd>
      </dl>
<EDS:PageContact runat="server" ContactName="Firstname Lastname, role" ContactEmail="first.last@eds.com" ContactNetid="czwlcq" />
<br />
<div style="text-align:right; margin-top: -30px;">
    <asp:Localize ID="ReturnToMain" runat="server" />
</div> 
</asp:Content>


