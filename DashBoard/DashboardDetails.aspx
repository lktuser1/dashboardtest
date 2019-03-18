<%@ Page Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="DashboardDetails.aspx.cs" Inherits="Dashboard.DashboardDetails" Title="DashbaordDetails" Trace="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">

        
    <script language="javascript" type="text/javascript">
function openPopup(strOpen)
{
    open("Monitors/MonitorDetailsList.aspx?inst=<%=instance%>&app=<%=application%>");
}

</script>

    <%= loadData() %>
        <div align="right">

       <!--<asp:HyperLink ID="HyperLink1"  Target="_new" Text="Monitor Descriptions List" runat="server" NavigateUrl ="Monitors/MonitorDetailsList.aspx" />
           -->
          <asp:Button Text="Monitor Details List" ID="btnMonitorList" runat="server" OnClientClick="openPopup('Monitors/MonitorDetailsList.aspx'); return false;" />
    
            </div>
       
          
        <asp:Panel ID="PanelPriority1" runat="server" Height="250px" ScrollBars="Vertical"
            Width="100%">
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966"
            BorderStyle="None" BorderWidth="1px" CellPadding="0" OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound" OnRowCreated="GridView1_RowCreated" AutoGenerateColumns="False">
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <RowStyle BackColor="White"  />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" CssClass="DataGridFixedHeader" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Priority 1 Monitor">
                    <ItemStyle Width="20%" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description">
                    <ItemStyle Width="35%" Wrap="True" />
                </asp:BoundField>
                <asp:BoundField DataField="Status">
                </asp:BoundField>
                <asp:BoundField DataField="Priority">
                </asp:BoundField>
                <asp:BoundField DataField="Message" HeaderText="Message">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="creationtime" HeaderText="Creation Time">
                    <ItemStyle Width="10%" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Isold" />
               <asp:TemplateField>
                    <ItemTemplate>
                         <asp:HyperLink ID="lnkView1"  text="..." style="color:black;background-color:lightgray;border:1px solid gray;font-weight:bolder;text-decoration:none;" NavigateUrl='<%# GetUrl(Eval("monID"), Eval("messagedetailsID"))%>'  Target="_new" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </asp:Panel>
        <br />
        <asp:Panel ID="PanelPriority2" runat="server" Height="250px" ScrollBars="Vertical"
            Width="100%">
        <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#CC9966"
            BorderStyle="None" BorderWidth="1px" CellPadding="0" OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound" OnRowCreated="GridView1_RowCreated"  OnRowCommand="GridView2_RowCommand" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" EnableModelValidation="True">
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <RowStyle BackColor="White"  />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" CssClass="DataGridFixedHeader" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Priority 2 Monitor">
                    <ItemStyle Width="20%" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description">
                    <ItemStyle Width="35%" Wrap="True" />
                </asp:BoundField>
                <asp:BoundField DataField="Status">
                </asp:BoundField>
                <asp:BoundField DataField="Priority">
                </asp:BoundField>
                <asp:BoundField DataField="Message" HeaderText="Message">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="creationtime" HeaderText="Creation Time">
                    <ItemStyle Width="10%" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Isold" />
                
              <asp:TemplateField> 
                    <ItemTemplate> 
                        <asp:HyperLink ID="lnkView2"  text="..." style="color:black;background-color:lightgray;border:1px solid gray;font-weight:bolder;text-decoration:none;" NavigateUrl='<%# GetUrl(Eval("monID"), Eval("messagedetailsID"))%>'  Target="_new" runat="server" />
                    </ItemTemplate> 
                </asp:TemplateField>
               </Columns>
        </asp:GridView>
        </asp:Panel>
                <br />
        <asp:Panel ID="PanelPriority3" runat="server" Height="250px" ScrollBars="Vertical"
            Width="100%">
        <asp:GridView ID="GridView3" runat="server" BackColor="White" BorderColor="#CC9966"
            BorderStyle="None" BorderWidth="1px" CellPadding="0" OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound" OnRowCreated="GridView1_RowCreated" AutoGenerateColumns="False" EnableModelValidation="True" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <RowStyle BackColor="White"  />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" CssClass="DataGridFixedHeader" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Priority 3 Monitor">
                    <ItemStyle Width="20%" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description">
                    <ItemStyle Width="35%" Wrap="True" />
                   </asp:BoundField>
                <asp:BoundField DataField="Status">
                </asp:BoundField>
                <asp:BoundField DataField="Priority">
                </asp:BoundField>
                <asp:BoundField DataField="Message" HeaderText="Message">
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="creationtime" HeaderText="Creation Time">
                    <ItemStyle Width="10%" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Isold" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkView3"  text="..." style="color:black;background-color:lightgray;border:1px solid gray;font-weight:bolder;text-decoration:none;" NavigateUrl='<%# GetUrl(Eval("monID"), Eval("messagedetailsID"))%>'  Target="_new" runat="server" />
                        
                    </ItemTemplate>
                </asp:TemplateField>
             
              </Columns>
           
        </asp:GridView>
        </asp:Panel>
       
   
    </form>
</asp:Content>
