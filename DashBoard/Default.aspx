<%@ Page Language="C#" MasterPageFile="~/IntranetCore.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Dashboard.Default" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="hp_welcome">
		<h1><asp:Localize ID="MainHeader" runat="server" Text="<%$ Resources:MainHeader %>"/></h1>
		<div id="date_stock"><!--#config timefmt="%d %b %Y"--><!--#echo var="DATE_LOCAL"--> <!-- NOTE: Pull in date via server side include or use a javascript to display the users computer date. --></div>
		  <p><asp:Localize id="Welcome" runat="server" Text="<%$ Resources:Welcome %>"/></p>
	  </div>
	  <div>
			<div id="hp_news">
				<div id="hp_sitenews">	<h2><asp:Localize ID="Header1" runat="server" Text="<%$ Resources:Header1 %>" /></h2>
				  <dl>
					<dt><a href="#"> The first headline here</a></dt><dd>Consider a short abstract  to catch users attention. </dd>
					<dt><a href="#">The second headline here</a></dt><dd>Use a short abstract to catch users attention.</dd><dt><a href="#">A third headline can be here</a></dt><dd>A short abstract  to catch users attention. </dd>	</dl>
					<p><a href="#"><strong>More Articles >></strong></a></p>
				
					<h2><asp:Localize ID="Header2" runat="server" Text="<%$ Resources:Header2 %>" /></h2>
<p>In reprehenderit in voluptate lorem ipsum dolor sit amet, quis nostrud exercitation.
Cupidatat non proident, ut labore et dolore magna aliqua. Sunt in <a href="#">culpa consectetur
adipisicing elit</a>, sed do eiusmod tempor incididunt. Ullamco laboris nisi ut enim
ad minim veniam, velit esse cillum dolore.</p>

				</div>		
				<div id="globalnews">
				<script type="text/javascript" src="http://infocentre.eds.com/news/incl_global_headlines.js"></script>
				</div>
			</div>	
				
	</div>  <!--This is to fake out the master page to place the side bar on the right-hand-side. -->
	<!--=========================== START sidebar -->
    <div id="sidebar">
      <h2>Feature</h2>
      <p class="about">Highlight something here about the workgroup site, a new
section, the main purpose of your site or something important to your target
audience.</p>
<h2 class="about">About the Templates </h2>
<p class="about">These templates are complemented by information in <a href="http://web.standards.eds.com/sg/">standards &amp; guidelines</a>.
Please see our detailed documentation about our <a href="http://web.standards.eds.com/templates/">templates</a> and
review the readme.txt file for more information. </p>
<h2>What's New</h2>
      <ul>
        <li><a href="#">What's New Item</a> </li>
				<li><a href="#">What's New Item</a> </li>
				<li><a href="#">What's New Item</a> </li>
				<li><a href="#">What's New Item</a> </li>
      </ul>
      <h2>What's Popular</h2>
      <ol>
        <li><a href="#">What's Popular Item</a> </li>
				<li><a href="#">What's Popular Item</a> </li>
				<li><a href="#">What's Popular Item</a> </li>
				<li><a href="#">What's Popular Item</a> </li>
      </ol>
    <!--=========================== END sidebar -->		
    </div>
</asp:Content>
