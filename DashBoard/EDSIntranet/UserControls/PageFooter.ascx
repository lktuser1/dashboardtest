<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageFooter.ascx.cs" Inherits="EDS.Intranet.UserControls.PageFooter" EnableViewState="false" %>

<!-- START PageFooter control -->
<div id="page-footer">
	<ul>
		<li><a id="footeraboutlink" href="../../About.aspx" accesskey="5"
			runat="server">
			<asp:Localize ID="FooterAboutTag" runat="server" Text="<%$ Resources:FooterAboutThisSite %>" /></a>
			|</li>
		<li><a id="footercommentslink" href="../../Comments.aspx" onclick="formsLocation(this.href);return false;"
			accesskey="6" runat="server">
			<asp:Localize ID="FooterContactTag" runat="server" Text="<%$ Resources:FooterContactUs %>" /></a>
			</li>
	</ul>
	<div id="copyright">
		<p>
			<asp:Localize ID="FooterCopyrightTag" runat="server" Text="<%$ Resources:FooterCopyright %>" />
			<asp:Localize ID="FooterDisclaimerTag" runat="server" Text="<%$ Resources:FooterDisclaimer %>" />
			<br />
			<asp:Localize ID="FooterTemplateTag" runat="server" Text="<%$ Resources:FooterTemplate %>" />
		</p>
	</div>
</div>
<!-- END PageFooter control -->