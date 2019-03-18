<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageUtilities.ascx.cs" Inherits="EDS.Intranet.UserControls.PageUtilities" %>

<!-- START PageUtils control -->
  <div id="pageutils" runat="server" class="noprint">
    <asp:PlaceHolder runat="server" ID="toolsPlaceholder" />
	<p id="breadcrumbs"><asp:SiteMapPath runat="server" ID="smPath" /></p>
  </div>
<!-- END PageUtils control -->
