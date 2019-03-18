<%@ Page Title="" 
    Language="C#" 
    MasterPageFile="~/IntranetCore.master" 
     AutoEventWireup ="True" 
    CodeBehind="Maintenance.aspx.cs" 
    Inherits="EDSIntranet.Maintenance"
    %>
    
<%@ MasterType VirtualPath="~/IntranetCore.master" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<head>
    <style type="text/css" >
        .GVFixedHeader {
            position: absolute;
            font-weight: bold;
            vertical-align: text-bottom;
        }
        .GVFixedFooter
         {
            position: absolute;
            font-weight: bold;
            vertical-align: text-bottom;
        }
        </style>
</head>

  
<form id="form2" runat="server">
<table width="100%";" align="center">
<tr style="width:700px">
<td style="font-weight:normal;font-size:small;text-align:right;width:450px">
Select Table to be Updated
</td>
<td style="width:150px">
<asp:DropDownList ID="ddlTable" runat="server" OnSelectedIndexChanged="ddlTable_SelectedIndexChanged"  AutoPostBack="True" style="width:150px;height:25px;padding:5px;">
<asp:ListItem Text="--Select--" Value="Select"></asp:ListItem>
<asp:ListItem Text="DB_Monitor" Value="DB_Monitor"></asp:ListItem>
<asp:ListItem Text="DB_MonitorDetails" Value="DB_MonitorDetails"></asp:ListItem>
</asp:DropDownList>
</td>
<td style="border:none;">
<asp:Button ID="btnSubmit1" runat="server" Text="Submit" OnClick="btnSubmit1_Click" style="border:none;text-align:center;width:60px;height:25px;font-weight:bold;border:ridge"/>
</td>
</tr>
<tr>
<asp:Label ID="lblmsg" runat="server" Font-Size="Medium" Font-Italic="true" ForeColor="Green"/>
</tr>   
</table>   
    <div style="height:400px; overflow:auto">
<asp:GridView ID="GridView2" runat="server"  AutoGenerateColumns="False" ShowFooter="true" AllowPaging="false" PageSize="20"
DataKeyNames="ID"
OnPageIndexChanging="GridView2_PageIndexChanging"
OnRowDeleting="GridView2_RowDeleting"
OnRowEditing="GridView2_RowEditing"
OnRowUpdating="GridView2_RowUpdating"
OnRowCancelingEdit="GridView2_RowCancelingEdit"
OnRowCommand="GridView2_RowCommand" HeaderStyle-CssClass="GVFixedHeader" FooterStyle-CssClass="GVFixedFooter" >
<Columns>    
<asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
<asp:TemplateField HeaderText="Instance" SortExpression="Instance" HeaderStyle-Width="43px" ItemStyle-Width="43px" >
<EditItemTemplate>
<asp:TextBox ID="txtInstance" width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Text='<%# Bind("Instance") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox ID="txtInstance" width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="Label1" runat="server" Text='<%# Bind("Instance") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Application" SortExpression="Application" HeaderStyle-Width="88px" ItemStyle-Width="88px">
<EditItemTemplate>
<asp:TextBox ID="txtApplication"  width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Text='<%# Bind("Application") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox ID="txtApplication" width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server" ></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="Label2" runat="server" Text='<%# Bind("Application") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-Width="170px" ItemStyle-Width="170px">
<EditItemTemplate>
<asp:TextBox ID="txtName1"  width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox ID="txtName1"  width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server" ></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Description" SortExpression="Description" HeaderStyle-Width="495px" ItemStyle-Width="495px">
<EditItemTemplate>
<asp:TextBox ID="txtDescription"  width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox ID="txtDescription"  width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server" ></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="Label4" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Priority" SortExpression="Priority" HeaderStyle-Width="15px" ItemStyle-Width="15px">
<EditItemTemplate>
<asp:TextBox ID="txtPriority"  width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox ID="txtPriority"  width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server" ></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="Label5" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Frequency" SortExpression="Frequency" HeaderStyle-Width="22px" ItemStyle-Width="22px">
<EditItemTemplate>
<asp:TextBox ID="txtFreq"  width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Text='<%# Bind("Freq") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox ID="txtFreq"  width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server" ></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="Label6" runat="server" Text='<%# Bind("Freq") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Active" SortExpression="Active" HeaderStyle-Width="36px" ItemStyle-Width="36px">
<EditItemTemplate>
<asp:CheckBox ID="txtActive"  width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Checked='<%# Bind("Active") %>'></asp:CheckBox>
</EditItemTemplate>
<FooterTemplate>
<asp:CheckBox ID="txtActive"  width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server" ></asp:CheckBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="lblActive" runat="server" Text='<%# Bind("Active") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="MonitorDetailsID" SortExpression="MonitorDetailsID" HeaderStyle-Width="36px" ItemStyle-Width="36px">
<EditItemTemplate>
<asp:TextBox ID="txtMonitorDetailsID"  width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Text='<%# Bind("MonitorDetailsID") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox ID="txtMonitorDetailsID"  width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server" ></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="Label8" runat="server" Text='<%# Bind("MonitorDetailsID") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>     

<asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="29px" ItemStyle-Width="29px">
<EditItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update" OnClientClick="return confirm('Are you sure you want to update?');"></asp:LinkButton>
<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClientClick="return confirm('Are you sure you want to cancel?');"></asp:LinkButton>
</EditItemTemplate>
<FooterTemplate>
<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="AddNew" Text="Add New" OnClientClick="return confirm('Are you sure you want to add a new record?');"></asp:LinkButton>
</FooterTemplate>
<ItemTemplate>
<asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
</ItemTemplate>        
                 

        </asp:TemplateField>

             <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="43px" ItemStyle-Width="43px">
            <ItemTemplate>
        <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False"  CommandName="Delete" Text="Delete"  OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:LinkButton>
        </ItemTemplate>
             </asp:TemplateField>

         

            </Columns>

        <PagerSettings  Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />
    </asp:GridView> 
                     
            
         
        
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" ShowFooter="true" AllowPaging="false" PageSize="20" 
        DataKeyNames="ID"
        OnPageIndexChanging="GridView3_PageIndexChanging"
        OnRowDeleting="GridView3_RowDeleting"
        OnRowEditing="GridView3_RowEditing"
        OnRowUpdating="GridView3_RowUpdating"
        OnRowCancelingEdit="GridView3_RowCancelingEdit"
		OnRowCommand="GridView3_RowCommand">

        <Columns> 

		
  
<asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False"
ReadOnly="True" SortExpression="ID" />

<asp:TemplateField HeaderText="Name" SortExpression="Name">
<EditItemTemplate>
<asp:TextBox ID="txtName" width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox ID="txtName" width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server" ></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Detail Description" SortExpression="Detail Description">
<EditItemTemplate>
<asp:TextBox ID="txtDetaildesc"  width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Text='<%# Bind("detaildesc") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox ID="txtDetaildesc" width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server" ></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="Label2" runat="server" Text='<%# Bind("detaildesc") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Technical Description" SortExpression="Technical Description">
<EditItemTemplate>
<asp:TextBox ID="txtTechnicaldesc"  width="90%" Rows="5" TextMode="MultiLine" Wrap="true" runat="server" Text='<%# Bind("technicaldesc") %>'></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox ID="txtTechnicaldesc" width="90%" Rows="3" TextMode="MultiLine" Wrap="true" runat="server" ></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
<asp:Label ID="Label3" runat="server" Text='<%# Bind("technicaldesc") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

           

        <asp:TemplateField HeaderText="Edit" ShowHeader="False">

        <EditItemTemplate>
        <asp:LinkButton ID="LinkButton7" runat="server" CausesValidation="True" CommandName="Update" Text="Update" OnClientClick="return confirm('Are you sure you want to update?');"></asp:LinkButton>
        <asp:LinkButton ID="LinkButton8" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClientClick="return confirm('Are you sure you want to cancel?');"></asp:LinkButton>
        </EditItemTemplate>

        <FooterTemplate>
        <asp:LinkButton ID="LinkButton9" runat="server" CausesValidation="False" CommandName="AddNew" Text="Add New" OnClientClick="return confirm('Are you sure you want to add a new record?');"></asp:LinkButton>
        </FooterTemplate>

        <ItemTemplate>
        <asp:LinkButton ID="LinkButton10" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
        </ItemTemplate>        
                 

        </asp:TemplateField>

             <asp:TemplateField HeaderText="Delete">
            <ItemTemplate>
        <asp:LinkButton ID="LinkButton11" runat="server" CausesValidation="False"  CommandName="Delete" Text="Delete"  OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:LinkButton>
        </ItemTemplate>
             </asp:TemplateField>

            </Columns>

        
        
        <PagerSettings  Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />
    </asp:GridView>
        </div>

                
                 
      
  </form>
</asp:Content>

    
    
