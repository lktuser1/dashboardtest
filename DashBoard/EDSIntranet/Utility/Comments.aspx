<%@ Page Language="C#" 
         MasterPageFile="~/IntranetCore.master" 
         AutoEventWireup="true" 
         CodeBehind="Comments.aspx.cs" 
         Inherits="EDSIntranet.EDSIntranet.Utility.Comments" %>

<%@ MasterType VirtualPath="~/IntranetCore.master" %>

<asp:Content id="Content1" ContentPlaceHolderid="MainContent" runat="server">
<form id="frmComment" action=""  runat="server">
	<p><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:MsgContactUs %>" /></p>
	<edsControls:edsValidationSummary id="ValSummary" ValidationGroup="CommentForm" runat="server" />
	<br />
	<asp:Label 
	    id="NameLabel" 
	    runat="server" 
	    text="<%$ Resources:ReqLabelName %>" 
	    AssociatedControlid="NameTextBox" />
	<asp:TextBox 
	    id="NameTextBox" 
	    runat="server"   
	    columns="50" />
	<asp:RequiredFieldValidator 
	    id="RequiredFieldValidator1"
		runat="server" 
		ControlToValidate="NameTextBox" 
		ErrorMessage="<%$ Resources:MsgBlankName %>"
		text="<%$ Resources:AlertIcon %>" 
		ValidationGroup="CommentForm" 
		Display="Dynamic" />
	<br />
	
	<asp:Label 
	    id="LocationLabel" 
	    runat="server" 
	    text="<%$ Resources:LabelLocation %>" 
	    AssociatedControlid="LocationTextBox"></asp:Label>
	<asp:TextBox
		id="LocationTextBox" 
		runat="server" 
		columns="50" />
	<br />
	
	<asp:Label 
	    id="EmailLabel" 
	    runat="server" 
	    text="<%$ Resources:ReqLabelEmail %>" 
	    AssociatedControlid="EmailTextBox"></asp:Label>
	<asp:TextBox
		id="EmailTextBox" 
		runat="server" 
		columns="50" />
	<asp:RequiredFieldValidator 
	    id="RequiredFieldValidator2" 
	    runat="server"
		ControlToValidate="EmailTextBox" 
		ErrorMessage="<%$ Resources:MsgBlankEmail %>" 
		text="<%$ Resources:AlertIcon %>" 
		ValidationGroup="CommentForm"
		Display="Dynamic" />
	<asp:RegularExpressionValidator 
	    id="RegularExpressionValidator1" 
	    runat="server" 
		ErrorMessage="<%$ Resources:MsgInvalidEmail %>" 
		text="<%$ Resources:AlertIcon %>" 
		ControlToValidate="EmailTextBox" 
		ValidationGroup="CommentForm" 
		ValidationExpression="\w+([-+.']\w+)*@eds.com" />
	<br />
	
	<asp:Label 
	    id="PhoneLabel" 
	    runat="server" 
	    text="<%$ Resources:LabelPhone %>" 
	    AssociatedControlid="PhoneTextBox"></asp:Label>
	<asp:TextBox
		id="PhoneTextBox" 
		runat="server" 
		columns="50" />
	<br />
	
	<asp:Label 
	    id="CommentsLabel" 
	    runat="server" 
	    text="<%$ Resources:ReqLabelComments %>" 
	    AssociatedControlid="CommentsTextBox"></asp:Label>
	<asp:TextBox
		id="CommentsTextBox" 
		runat="server" 
		Rows="5" 
		Columns="80" 
		TextMode="MultiLine" />
	<asp:RequiredFieldValidator 
	    id="RequiredFieldValidator3"
		runat="server" 
		ControlToValidate="CommentsTextBox" 
		ErrorMessage="<%$ Resources:MsgBlankComments %>"
		text="<%$ Resources:AlertIcon %>" 
		ValidationGroup="CommentForm" 
		Display="Dynamic" />
	<p>
	<asp:Button 
	    runat="server" 
	    CssClass="submit" 
	    id="SubmitButton" 
	    text="Submit" 
	    OnClick="SubmitButton_Click"
		ValidationGroup="CommentForm" /></p>
	</form>
<br />
<div style="text-align:right; margin-top: -30px;">
    <asp:Localize ID="ReturnToMain" runat="server" />
</div> 
</asp:Content>
