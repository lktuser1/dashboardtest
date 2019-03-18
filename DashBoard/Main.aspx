<%@ Page Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Dashboard.Main" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <%= getLatestData() %>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">
    
   <script language="javascript" type="text/javascript">
       function ShowHide(strshow)
       {
           var x = document.getElementById('Alerts');
           if (x.style.display === 'none') {
               x.style.display = 'block';
               //document.getElementById('txtAlerts').innerText = '<<';
           } else {
               x.style.display = 'none';
              // document.getElementById('txtAlerts').innerText = '>>';
           }
       }
       </script>
    <form id="form1" runat="server">
      <!--
        <asp:Button ID="txtAlerts" runat="server" Text="<"  OnClientClick="ShowHide(); return false;" />
      -->  
        <div id="Alerts">
             <%= getLatestAlertData() %>

        </div>
        </form>

</asp:Content>

