<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestCascade.aspx.cs" Inherits="EDSIntranet.Reports.TestCascade" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var applicationsDDL = $('#applications');
            var instancesDDL = $('#instances');
            var prioritiesDDL = $('#priorities');
            var monitorsDDL = $('#monitors');

            var selapplication;
            var selinstance;
            var selpriority;


            $.ajax({
                url: 'TestCascadeService.asmx/GetApplications',
                method: 'post',
                dataType: 'json',
                success: function (data) {
                    applicationsDDL.append($('<option/>', { value: -1, text: 'Select Application' }));
                    instancesDDL.append($('<option/>', { value: -1, text: 'Select Instance' }));
                    prioritiesDDL.append($('<option/>', { value: -1, text: 'Select Priority' }));
                    monitorsDDL.append($('<option/>', { value: -1, text: 'Select Monitor' }));
                    instancesDDL.prop('disabled', true);
                    prioritiesDDL.prop('disabled', true);
                    monitorsDDL.prop('disabled', true);

                    $(data).each(function (index, item) {
                        applicationsDDL.append($('<option/>', { value: item.application, text: item.application }));
                    });
                },
                error: function (err) {
                    alert(err);
                }
            });

            applicationsDDL.change(function () {
                if ($(this).val() == "-1") {
                    instancesDDL.empty();
                    prioritiesDDL.empty();
                    instancesDDL.append($('<option/>', { value: -1, text: 'Select Instance' }));
                    prioritiesDDL.append($('<option/>', { value: -1, text: 'Select Priority' }));
                    instancesDDL.val('-1');
                    prioritiesDDL.val('-1');
                    instancesDDL.prop('disabled', true);
                    prioritiesDDL.prop('disabled', true);
                }
                else {
                    prioritiesDDL.val('-1');
                    prioritiesDDL.prop('disabled', true);
                    monitorsDDL.val('-1');
                    monitorsDDL.prop('disabled', true);

                    selapplication = $(this).val();
                    //alert(selapplication);

                    $.ajax({
                        url: 'TestCascadeService.asmx/GetInstances',
                        method: 'post',
                        dataType: 'json',
                        data: { application: $(this).val() },
                        success: function (data) {
                            instancesDDL.empty();
                            instancesDDL.append($('<option/>', { value: -1, text: 'Select Instance' }));
                            $(data).each(function (index, item) {
                                instancesDDL.append($('<option/>', { value: item.instance, text: item.instance }));
                            });
                            instancesDDL.val('-1');
                            instancesDDL.prop('disabled', false);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });

            instancesDDL.change(function () {
                if ($(this).val() == "-1") {
                    prioritiesDDL.empty();
                    prioritiesDDL.append($('<option/>', { value: -1, text: 'Select Priority' }));
                    prioritiesDDL.val('-1');
                    prioritiesDDL.prop('disabled', true);
                }
                else {

                    monitorsDDL.val('-1');
                    monitorsDDL.prop('disabled', true);

                    selinstance = $(this).val();
                    //alert(selinstance);
                    $.ajax({
                        url: 'TestCascadeService.asmx/GetPriorities',
                        method: 'post',
                        dataType: 'json',
                        data: { application: selapplication, instance: $(this).val() },
                        success: function (data) {
                            prioritiesDDL.empty();
                            prioritiesDDL.append($('<option/>', { value: -1, text: 'Select Priority' }));
                            $(data).each(function (index, item) {
                                prioritiesDDL.append($('<option/>', { value: item.priority, text: item.priority }));
                            });
                            prioritiesDDL.val('-1');
                            prioritiesDDL.prop('disabled', false);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });

            prioritiesDDL.change(function () {
                if ($(this).val() == "-1") {
                    monitorsDDL.empty();
                    monitorsDDL.append($('<option/>', { value: -1, text: 'Select Monitor' }));
                    monitorsDDL.val('-1');
                    monitorsDDL.prop('disabled', true);
                }
                else {
                    selpriority = $(this).val();
                    $.ajax({
                        url: 'TestCascadeService.asmx/GetBacklogMonitors',
                        method: 'post',
                        dataType: 'json',
                        data: { application: selapplication, instance: selinstance, priority: $(this).val() },
                        success: function (data) {
                            monitorsDDL.empty();
                            monitorsDDL.append($('<option/>', { value: -1, text: 'Select Monitor' }));
                            $(data).each(function (index, item) {
                                monitorsDDL.append($('<option/>', { value: item.Name, text: item.Name }));
                            });
                            monitorsDDL.val('-1');
                            monitorsDDL.prop('disabled', false);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });

        });
    </script>
    <style>
        select {
            width: 150px;
        }
    </style>
</head>
<body style="font-family: Arial">
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>Continent
                </td>
                <td>
                    <select id="applications">
                    </select>
                </td>
            </tr>
            <tr>
                <td>Country</td>
                <td>
                    <select id="instances">
                    </select>
                </td>
            </tr>
            <tr>
                <td>City</td>
                <td>
                    <select id="priorities">
                    </select>
                </td>
            </tr>
             <tr>
                <td>Monitor</td>
                <td>
                    <select id="monitors">
                    </select>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
