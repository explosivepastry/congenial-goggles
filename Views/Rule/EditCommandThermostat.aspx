<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Sensor>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CommandControlUnit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%long NotiID = ViewBag.NotificationID; %>
    <%:Html.Partial("~/Views/Rule/Header.ascx",(Notification)ViewBag.Notification) %>
    <%:Html.Partial("CommandThermostatList", Model) %>

    <script type="text/javascript">
        document.getElementById("go-back").onclick = function () {
            window.location.href = "/Rule/ChooseTaskToEdit/<%=NotiID %>";
        }
        var successString = '<%: Html.TranslateTag("Success")%>';
        var failedString = '<%: Html.TranslateTag("Failed")%>';

        $(document).ready(function () {

            $('#saveThermostatBtn').click(function () {
                var values = new Object();
                $.post('/Rule/EditThermostatSettings/<%=NotiID%>', $('#sendCommandPageForm').serialize(), function (data) {
                    if (data == "Success") {
                        toastBuilder(successString);
/*                        $('#textAreaError').html(successString)*/
                    } else {
                        toastBuilder(`${failedString}: ${data}`);
/*                        $('#textAreaError').html(failedString + ": " + data);*/
                    }
                    setTimeout(function () {
                        window.location.href = "/Rule/ChooseTaskToEdit/<%=NotiID%>";
                    }, 1000); // 1 second wait
                });
            });
        });

        function toggleThermostatDevice(deviceID, add) {
            $('#Notifier_' + deviceID).val(add);
            var url = "/Rule/ToggleThermostatDeviceEdit?id=<%=NotiID%>&";
            var params = "deviceID=" + deviceID;
            params += "&add=" + add;
            $.post(url, params, function (data) {
                $('#thermostatDevice_' + deviceID).html(data);
            });
        }

    </script>
</asp:Content>
