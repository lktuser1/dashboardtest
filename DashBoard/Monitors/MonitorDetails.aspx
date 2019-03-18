<%@ Page Language="C#" MasterPageFile="~/IntranetCore.master" AutoEventWireup="true"  CodeBehind="MonitorDetails.aspx.cs" Inherits="Dashboard.Monitors.MonitorDetails" Trace="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>

    <form id="form1" runat="server">
    <div>
       <!-- <u>Details</u>
        <br />
    -->
        <b>Monitor</b> <span id="name"><asp:Label ID="Labelname" runat="server" Text="Label"></asp:Label></span>
         <br />
        <b>Description:</b> <span id="description"><asp:Label ID="Labeldescription" runat="server" Text="Label"></asp:Label></span>
        <br />
        
        <b>Priority:</b> <span id="Span1"><asp:Label ID="Labelpriority" runat="server" Text="Label"></asp:Label></span>
        <b>Frequency:</b> <span id="creationtime"><asp:Label ID="Labelfreq" runat="server" Text="Label"></asp:Label></span>
         <b>Id:</b> <span id="message"><asp:Label ID="Labelid" runat="server" Text="Label"></asp:Label></span>
       

        <table border="1" style="width:100%;border:solid;color:black;font-size:1em;">
            <tr>
                <td style="width:10%;">
                    Detail Description:
                </td>
                <td style="width:90%;">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:10%;">
                    Technical Description
                </td>
                
                <td style="width:90%;">
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>

        <table border="1" style="width:100%;border:solid;color:black;font-size:1em;">
           
            <tr>
                <td style="width:10%;">
                 Error Details   
                </td>
                
                <td style="width:90%;">
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        
       </div>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            if (window.opener != null && !window.opener.closed) {
                var rowIndex = window.location.href.split("?")[1].split("=")[1];
                var parent = $(window.opener.document).contents();
                var row = parent.find("[id*=GridView1]").find("tr").eq(rowIndex);
               
                $("#name").html(row.find("td").eq(0).html());
                $("#description").html(row.find("td").eq(1).html());
            
            }
        });
    </script>
    </form>


    </asp:Content>
