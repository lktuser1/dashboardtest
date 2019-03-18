<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="HistoricalReporting.aspx.cs" Inherits="EDSIntranet.HistoricalReporting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <table width="300px;" align="center">
        <tr>
            <td style="display:inline;font-weight:bold">
                Application
            </td>
            <td>
                <asp:DropDownList ID="ddlApplication" runat="server" OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged" AutoPostBack="True" style="width:120px;height:20px;">
                    <asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
                     <asp:ListItem Text="SA" Value="SA"></asp:ListItem>
                    <asp:ListItem Text="SM7 Prod" Value="SM7 Prod"></asp:ListItem>
                </asp:DropDownList>
            </td>
        
         
            <td style="display:inline;font-weight:bold">
                Instance
            </td>
            <td>
                <asp:DropDownList ID="ddlInstances" runat="server" style="display:inline;width:120px;height:20px;">
                     <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                   
                </asp:DropDownList>
            </td>
        
        
            <td style="font-weight:bold">
                Priority
            </td>
            <td>
                <asp:DropDownList ID="ddlPriority" runat="server" OnSelectedIndexChanged="ddlPriority_SelectedIndexChanged" AutoPostBack="True" style="width:120px;height:20px;">
                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                   <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>

               
        </tr>
          <tr>
            <td style="font-weight:bold">
                From
            </td>
            <td>
                 <div style="float:left;">
                <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                     </div>
                      <div style="float:left;">
                <asp:ImageButton ID="ImageButton1" ImageUrl="~/EDSIntranet/Images/calender.png" CommandName="calender" runat="server" OnClick="Unnamed1_Click"  style="width:16px;height:16px;"/>
                </div>
                 <div style="float:left;">
                          <asp:Calendar ID="Calendar1" runat="server" style="width:35px;height:35px;" Visible="false" OnSelectionChanged="DateTimeChanged"></asp:Calendar>
                     </div>
            </td>
        
          
            <td style="font-weight:bold">
                To
            </td>
            <td>
                <div style="float:left;">
                 <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                    </div>
                <div style="float:left;">
                 <asp:ImageButton ID="ImageButton2" ImageUrl="~/EDSIntranet/Images/calender.png" CommandName="calender" runat="server" OnClick="ImageButton1_Click" style="width:16px;height:16px;"/>
                </div>
                <div style="float:left;">
                    <asp:Calendar ID="Calendar2" runat="server" style="width:35px;height:35px;" Visible="false" OnSelectionChanged="DateTimeToChanged"></asp:Calendar>
                    </div>
            </td>            
        </tr>

        <tr height = 10px>

        </tr>
        <tr>
            <td colspan="5" style="border:none;text-align:center;width:400px;">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" style="border:none;text-align:center;width:75px;height:25px;font-weight:bold;border:groove"/>
            </td>
        </tr>
    </table>
        
        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanging">
            <PagerSettings  Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />
        </asp:GridView>

               <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" visible="false"/>
            <div style="float:left;">
        <asp:ImageButton ID="ImageButton3" ImageUrl="~/EDSIntranet/Images/E2E.png" runat="server" OnClick="ExportToExcel" style="width:30px;height:15px;" Visible="false" title="Export To Excel"/>
         </div>
                    
        </form>
</asp:Content>
