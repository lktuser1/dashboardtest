<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavMenu.ascx.cs" Inherits="EDS.Intranet.UserControls.NavMenu" %>
<!-- START NavMenu control -->
<div style="background-color:pink;" id="navbarmenu"> 
  <ul id="nav"> 
    <asp:Repeater ID="navMenuRepeater" runat="server" DataSourceID="srcNavMenu">
    <ItemTemplate>
        <li class="mainnav <%#HighlightActiveNavMenu((SiteMapNode)Container.DataItem)%>" >
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Url") %>'><%# Eval("Title") %></asp:HyperLink>
        </li>
    </ItemTemplate>
    </asp:Repeater>
  </ul>
</div>
<!-- END NavMenu control -->