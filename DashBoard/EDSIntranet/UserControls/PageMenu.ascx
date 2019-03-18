<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageMenu.ascx.cs" Inherits="EDS.Intranet.UserControls.PageMenu" %>

<!-- START PageMenu control -->
<div style="background-color:violet;" id="sidebar">
    <div style="background-color:orange;" id="sidenav">
      <ul>
        <asp:Repeater ID="pageMenuRepeater" runat="server" DataSourceID="srcPageMenu">
        <ItemTemplate>
            <li class="<%#HighlightActivePageMenu((SiteMapNode)Container.DataItem)%>">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# createURL((SiteMapNode)Container.DataItem) %>'><%# Eval("Title") %></asp:HyperLink></li>
        </ItemTemplate>
        </asp:Repeater>
      </ul>
    </div>
</div>
<!-- END PageMenu control -->