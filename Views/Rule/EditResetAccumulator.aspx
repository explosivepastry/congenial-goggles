<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Sensor>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditResetAccumulator
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%long NotiID = ViewBag.NotificationID; %>
    <%:Html.Partial("~/Views/Rule/Header.ascx",(Notification)ViewBag.Notification) %>
    <%:Html.Partial("ResetAccList", Model) %>

    <script type="text/javascript">
        document.getElementById("go-back").onclick = function () {
            window.location.href = "/Rule/ChooseTaskToEdit/<%=NotiID %>";
        }
        var successString = '<%: Html.TranslateTag("Success")%>';
        var failedString = '<%: Html.TranslateTag("Failed")%>';

        $(document).ready(function () {

            $('#saveResetAccTextBtn').click(function () {
                $('#textAreaError').html("");
                $.post('/Rule/EditResetAccSettings/<%=NotiID%>', $('#sendResetAccPageForm').serialize(), function (data) {
                    if (data == "Success") {
                        /*$('#textAreaError').html(successString)*/
                        toastBuilder("Success");
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


<%--           function toggleResetAccDevice(deviceID, deviceType, add,relayNumber) {
            $('#textAreaError').html("");
            $('#Notifier_' + deviceID).val(add);
                var url = "/Rule/ToggleResetAccDeviceEdit?id=<%=NotiID%>";
            var params = "deviceID=" + deviceID;
            params += "&deviceType=" + deviceType;
            params += "&add=" + add;
            $.post(url, params, function (data) {
                if (data == "Success") {
                    if (add) {
                        $('#addRelay1_' + deviceID).hide();
                        $('#editRelay1_' + deviceID).show();
                        $('#caretOpen_' + deviceID + '_' + relayNumber).css("display", "none");
                        $('#caretClose_' + deviceID + '_' + relayNumber).css("display", "block");
                        $('#editRelay1_' + deviceID).css("height", "auto");
                        toggleResetAccRelayState('relay1Toggle_' + deviceID, 'ON');
                        $('#nrd' + deviceID).show(); // Show the div with the toggle switch
                    } else {
                        $('#addRelay1_' + deviceID).show();
                        $('#editRelay1_' + deviceID).hide();
                        toggleResetAccRelayState('relay1Toggle_' + deviceID, 'OFF');
                        $('#nrd' + deviceID).hide(); // Hide the div with the toggle switch
                    }
                } else {
                    toastBuilder(failedString);
                   $('#textAreaError').html(failedString);
                }
            });
        }--%>

        function toggleResetAccDevice(deviceID, deviceType, add, relayNumber) {
            $('#textAreaError').html("");
            $('#Notifier_' + deviceID).val(add);
            var url = "/Rule/ToggleResetAccDeviceEdit?id=<%=NotiID%>";
            var params = "deviceID=" + deviceID;
            params += "&deviceType=" + deviceType;
            params += "&add=" + add;

            $.post(url, params, function (data) {
                if (data == "Success") {
                    if (add) {
                        $('#addRelay' + relayNumber + '_' + deviceID).hide();
                        $('#editRelay' + relayNumber + '_' + deviceID).show();
                        $('#caretOpen_' + deviceID + '_' + relayNumber).css("display", "none");
                        $('#caretClose_' + deviceID + '_' + relayNumber).css("display", "block");
                        $('#editRelay' + relayNumber + '_' + deviceID).css("height", "auto");
                        toggleResetAccRelayState('relay' + relayNumber + 'Toggle_' + deviceID, 'ON');
                        $('#nrd' + relayNumber + '_' + deviceID).show(); // Show the div with the toggle switch
                    } else {
                        $('#addRelay' + relayNumber + '_' + deviceID).show();
                        $('#editRelay' + relayNumber + '_' + deviceID).hide();
                        toggleResetAccRelayState('relay' + relayNumber + 'Toggle_' + deviceID, 'OFF');
                        $('#nrd' + relayNumber + '_' + deviceID).hide(); // Hide the div with the toggle switch
                    }
                } else {
                    toastBuilder(failedString);
                    $('#textAreaError').html(failedString);
                }
            });
        }


    </script>
</asp:Content>
