<%@ Page Language="C#" MasterPageFile="~/IntranetCore.Master" AutoEventWireup="true" CodeBehind="PageNotFound.aspx.cs" Inherits="EDS.Intranet.Error.PageNotFound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<form runat="server" id="frmPageNotFound">
    <br />
	<p>We are sorry to report that the page you requested is not available on this site.</p>
    <h3>Did you follow a link within this site?</h3>
    <p>It is possible that the page was recently moved or deleted, please <a id="A1" href="~/EDSIntranet/Utility/Comments.aspx" runat="server">contact us</a> so we can correct the problem.
    </p>
      <h3>Did you follow a link from another Web site or printed piece  &#8230; or perhaps rely upon a bookmark?</h3>
      <p> Unfortunately content does not always stay put on the Web.  The content you are after may have been moved or deleted, but please 
          return to our <a id="A2" href="~/Default.aspx" runat="server">home page</a> to quickly find what you are looking for.</p>
</form>
</asp:Content>
