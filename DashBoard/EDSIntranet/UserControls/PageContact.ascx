<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageContact.ascx.cs" Inherits="EDS.Intranet.UserControls.PageContact" EnableViewState="false" %>

<!-- START PageContact control -->
<h2 class="contact">
	<asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:Contact %>" /></h2>
<dl class="contact">
	<dt><asp:Literal ID="ContactNameText" runat="server" /></dt>
	<dd>
		<address>
			<asp:HyperLink Visible="false" runat="server" id="ContactEmailLink">
				<asp:Literal runat="server" id="ContactEmailText" /></asp:HyperLink>
				<asp:Literal runat="server" id="ViewContactSeparator"  text=" | " />
			<asp:HyperLink Visible="false" runat="server" id="ViewContactDetailsLink">
				<asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:ViewContactDetails %>" /></asp:HyperLink>
		</address>
	</dd>
</dl>
<!-- END PageContact control -->
