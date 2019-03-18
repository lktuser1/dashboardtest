<%@ Page Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="GenericError.aspx.cs" Inherits="EDS.Intranet.Error.GenericError" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
             Namespace="System.Web.UI" 
             TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<form runat="server" id="frmGenericError">
    <br />
	<p>We are sorry to report that an error has occurred during the processing of this page.</p>
    <p>Please <a id="A1" href="~/EDSIntranet/Utility/Comments.aspx" runat="server">contact us</a> so we can correct the problem.</p>

<edsControls:edsDetailsView 
    id="DetailsView1" 
    runat="server"
    AllowPaging="false"
    AutoGenerateRows="False" 
    CommandRowStyle-ForeColor="White"
    DefaultMode="Insert" 
    Height="50px"    
    Width="100%">
<HeaderTemplate>
    <asp:Literal ID="Literal1" runat="server" Text="Error Detail" />
</HeaderTemplate>
<Fields>
    <asp:TemplateField HeaderText="Error Message">
        <itemtemplate>
            <asp:Label id="ErrorMessage" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Exception Type">
        <itemtemplate>
            <asp:Label id="ExceptionType" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Application">
        <itemtemplate>
            <asp:Label id="Application" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Source File">
        <itemtemplate>
            <asp:Label id="URL" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Stack Trace">
        <itemtemplate>
            <asp:Label id="StackTrace" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Source Code">
        <itemtemplate>
            <asp:Label id="SourceCode" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Inner Exception">
        <itemtemplate>
            <asp:Label id="InnerException" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Exception">
        <itemtemplate>
            <asp:Label id="Exception" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
          
    <asp:TemplateField HeaderText="Base Exception">
        <itemtemplate>
            <asp:Label id="BaseException" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Additional Information">
        <itemtemplate>
            <asp:Label id="Data" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
</Fields>
</edsControls:edsDetailsView >

<edsControls:edsDetailsView 
    id="DetailsView2" 
    runat="server"
    AllowPaging="false"
    AutoGenerateRows="False" 
    CommandRowStyle-ForeColor="White"
    DefaultMode="Insert" 
    Height="50px"    
    Width="100%">
<HeaderTemplate>
    <asp:Literal ID="Literal1" runat="server" Text="Additional Information" />
</HeaderTemplate>
<Fields>
    <asp:TemplateField HeaderText="User ID">
        <itemtemplate>
            <asp:Label id="UserID" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="User Name">
        <itemtemplate>
            <asp:Label id="UserName" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Browser Type">
        <itemtemplate>
            <asp:Label id="BrowserType" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Computer Name">
        <itemtemplate>
            <asp:Label id="ComputerName" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
   
   <asp:TemplateField HeaderText="Operating System">
        <itemtemplate>
            <asp:Label id="OSVersion" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText=".NET Version">
        <itemtemplate>
            <asp:Label id="NetVersion" runat="server" text="" />
        </itemtemplate>
    </asp:TemplateField>
     
</Fields>
</edsControls:edsDetailsView >
</form>
</asp:Content>
