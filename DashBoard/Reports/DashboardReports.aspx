<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="DashboardReports.aspx.cs" Inherits="Dashboard.DashboardReports" enableEventValidation ="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <script  type="text/javascript">
        function MakeStaticHeader(gridId, width, height, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 3 + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }



        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }


</script>

  
  
    <form id="form1" runat="server">


            <table  border ="0" style="width: 100%;border: 1px solid black;font-size:1em;font-weight: bold;padding:1px;margin:1px;vertical-align:central;">
                <tr>
                    <td>
                        Report Type
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReports" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlReports_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="1">BackLog Count in days by Month for SM Monitor Type</asp:ListItem>
                            <asp:ListItem Value="2">BackLog Count in days by Month for all Monitors</asp:ListItem>
                            <asp:ListItem Value="3">BackLog Detail per Monitor as created in DB</asp:ListItem>
                            <asp:ListItem Value="4">BackLog Detail Grouped by Hour per Monitor</asp:ListItem>
                            <asp:ListItem Value="5">BackLog Summary for SM MonitorType</asp:ListItem>
                            <asp:ListItem Value="6">BackLog Summary for all Monitors</asp:ListItem>
                            <asp:ListItem Value="21">Monitor List by Instance</asp:ListItem>
                            <asp:ListItem Value="22">Monitor Details List</asp:ListItem>
                            <asp:ListItem Value="23">Monitor Descriptions List</asp:ListItem>
                    
                        </asp:DropDownList>
                          </td>
                     <td > 
                         <asp:Button ID="txtSearch" runat="server" Text="Search" OnClick="txtSearch_Click" />
                    </td>
                    <td > 
                        <asp:Button ID="Export" runat="server" Text="Export" OnClick="Export_Click" />
                    </td>
                        <td >
                        From 
                    </td>
                    <td >
                         <asp:TextBox ID="TextBox1" value = "<%# DateTime.Now %>"  runat="server" style="width:120px;vertical-align:top;" ></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/EDSIntranet/Images/Calendar.png" onclick="ImageButton1_Click" />
                        <asp:Calendar ID="Calendar1" style="width:35px;height:35px;background-color:white;position:absolute;z-index:100;" runat="server" SelectedDate="<%# DateTime.Now %>" onselectionchanged="Calendar1_SelectionChanged"></asp:Calendar>
                          
                    </td>
                    <td >
                        To
                    </td>
                    <td >
                        <asp:TextBox ID="TextBox2" value = "<%# DateTime.Now %>"  runat="server" style="width:120px;vertical-align:top;"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/EDSIntranet/Images/Calendar.png" onclick="ImageButton2_Click" />
                        <asp:Calendar ID="Calendar2" style="width:35px;height:35px;background-color:white;position:absolute;z-index:100;" runat="server" SelectedDate="<%# DateTime.Now %>"  onselectionchanged="Calendar2_SelectionChanged"></asp:Calendar>
                     </td>
                </tr>
                 <tr>
                    <td >
                        Application 
                    </td>
                    <td ><asp:DropDownList ID="ddlApp"  DataValueField = "application" DataTextField = "application" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlApp_SelectedIndexChanged" >
                        </asp:DropDownList> 
                        <asp:Label ID="LabelReqApp" runat="server" Text="*"></asp:Label>
                        Instance 
                        <asp:DropDownList ID="ddlInstance" DataValueField = "instance" DataTextField = "instance" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInstance_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="LabelReqInstance" runat="server" Text="*"></asp:Label>
                    </td>
                   
                    <td >
                         Priority
                    </td>
                      <td >
                        <asp:DropDownList ID="ddlPriority" DataValueField = "priority" DataTextField = "priority" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlPriority_SelectedIndexChanged">
                        </asp:DropDownList>
                         <asp:Label ID="LabelReqPriority" runat="server" Text="*"></asp:Label>
                     </td>
                      <td >
                        Monitor Type
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlMonitorType" DataValueField = "id" DataTextField = "name" runat="server"></asp:DropDownList>
                        <asp:Label ID="LabelReqMonitorType" runat="server" Text="*"></asp:Label>
                    </td>
                   
                   
                    <td >
                        Monitor
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlMonitor" DataValueField = "id" DataTextField = "name" runat="server" >
                        </asp:DropDownList>
                         <asp:Label ID="LabelReqMonitor" runat="server" Text="*"></asp:Label>
                    </td>
                </tr>
                </table>
          
        <asp:Label ID="Label1" runat="server" text ="" style="color:red;"></asp:Label>
        <div id="divResult"></div>

        <!--

         <input type="button" id="btnGetAllBacklogMonitors" value="Get All Backlog Monitors" />
         <select id="application"></select>
        <select id="instance"></select>
        <select id="priority"></select>
    <br />
    <table id="tblMonitor" border="1" style="border-collapse:collapse">
        <thead>
            <tr>
                <th>Application</th>
                <th>Instance</th>
                <th>Priority</th>
                <th>Monitor</th>
                <th>ID</th>
            </tr>
        </thead>
        <tbody></tbody>
    </table>
        -->
        <div id="DivRoot" ">
            <div style="overflow: hidden;" id="DivHeaderRow">
            </div>
            <div style="width:1300px;height:450px;overflow: scroll;vertical-align:top;"  onscroll="OnScrollDiv(this)" id="DivMainContent">
                <asp:GridView ID="GridView1" EmptyDataText="There is no data to display" runat="server"
                     CssClass ="gridHeader"
                    AllowSorting="True"
                    OnSorting="GridView1_Sorting"
                    RowStyle-Wrap="false"
                    OnRowDataBound="GridView1_RowDataBound">
                    <EmptyDataRowStyle BackColor="#f9fafc" ForeColor="black" />
                    <EmptyDataTemplate>
                        No Data Found.  
                    </EmptyDataTemplate>
                </asp:GridView>

            </div>

            <div id="DivFooterRow" style="overflow:hidden;">
            </div>
        </div>
           
       
       
   

        
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">
     
    
</asp:Content>
