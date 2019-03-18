<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="MonitorListTree.aspx.cs" Inherits="Dashboard.Monitors.MonitorListTree" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">
     <form id="form1" runat="server">
    <div style="font-family:Arial">
    <table>
        <tr>
            <td style="border:1px solid black">
                <asp:TreeView ID="TreeView1" ShowCheckBoxes="All" 
                    runat="server">
                </asp:TreeView>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text=">>" />            
            </td>
            <td>
                <asp:ListBox ID="ListBox1" runat="server" Height="145px" 
                    Width="100px">
                </asp:ListBox>
            </td>
        </tr>
    </table>
</div>
    </form>
</asp:Content>
