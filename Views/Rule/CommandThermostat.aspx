<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Sensor>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CommandControlUnit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% ViewBag.inProgressRecipients = MonnitSession.NotificationRecipientsInProgress; %>
    <%=Html.Partial("_CreateNewRuleProgressBar") %>
    <%=Html.Partial("CommandThermostatList", Model) %>

    <script type="text/javascript">
        document.getElementById("go-back").onclick = function () {
            window.location.href = "/Rule/ChooseTask/";
        }
        var successString = '<%: Html.TranslateTag("Success")%>';
        var failedString = '<%: Html.TranslateTag("Failed")%>';

        $(document).ready(function () {

            $('#saveThermostatBtn').click(function () {
                $('#textAreaError').html("");
                $.post('/Rule/SaveThermostatSettings', $('#sendCommandPageForm').serialize(), function (data) {
                    if (data == "Success") {
                        /*                        $('#textAreaError').html(successString)*/
                        toastBuilder(successString);
                        setTimeout(function () {
                            window.location.href = "/Rule/ChooseTask/";
                        }, 1000); // 1 second wait

                    } else {
                        /*                        $('#textAreaError').html(failedString + ": " + data);*/
                        toastBuilder(`${failedString}: ${data}`);
                        //setTimeout(function () {
                        //    window.location.href = "/Rule/ChooseType/";
                        //}, 3000); // 1 second wait
                    }
                });
            });
        });

        function toggleThermostatDevice(deviceID, add) {
            $('#Notifier_' + deviceID).val(add);
            var url = "/Rule/ToggleThermostatDevice/";
            var params = "deviceID=" + deviceID;
            params += "&add=" + add;
            $.post(url, params, function (data) {
                $('#thermostatDevice_' + deviceID).html(data);
            });
        }

 

    </script>
</asp:Content>
