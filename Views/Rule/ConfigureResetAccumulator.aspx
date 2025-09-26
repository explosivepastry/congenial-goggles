<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Sensor>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ConfigureResetAccumulator
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% ViewBag.inProgressRecipients = MonnitSession.NotificationRecipientsInProgress; %>
    <%=Html.Partial("_CreateNewRuleProgressBar") %>
    <%=Html.Partial("ResetAccList", Model) %>

    <script type="text/javascript">
        document.getElementById("go-back").onclick = function () {
            window.location.href = "/Rule/ChooseTask/";
        }
        var successString = '<%: Html.TranslateTag("Success")%>';
        var failedString = '<%: Html.TranslateTag("Failed")%>';

        $(document).ready(function () {

            $('#saveResetAccTextBtn').click(function () {
                $('#textAreaError').html("");
                $.post('/Rule/SaveResetAccSettings/', $('#sendResetAccPageForm').serialize(), function (data) {
                    if (data == "Success") {
                        toastBuilder(successString);
                        setTimeout(function () {
                            window.location.href = "/Rule/ChooseTask/";
                        }, 1000); // 1 second wait

                    } else {
                        toastBuilder(`${failedString}: ${data}`);
                    }
                });
            });
        });

        //function toggleResetAccDevice(deviceID, deviceType, add, relayNumber) {
        //    $('#textAreaError').html("");
        //    $('#Notifier_' + deviceID).val(add);
        //    var url = "/Rule/ToggleResetAccDevice/";
        //    var params = "deviceID=" + deviceID;
        //    params += "&deviceType=" + deviceType;
        //    params += "&add=" + add;
        //    $.post(url, params, function (data) {
        //        if (data == "Success") {
        //            if (add) {
        //                $('#addRelay' + relayNumber + '_' + deviceID).hide();
        //                $('#editRelay' + relayNumber + '_' + deviceID).show();
        //                $('#caretOpen_' + deviceID + '_' + relayNumber).css("display", "none");
        //                $('#caretClose_' + deviceID + '_' + relayNumber).css("display", "block");
        //                $('#editRelay' + relayNumber + '_' + deviceID).css("height", "auto");
        //                toggleResetAccRelayState('relay' + relayNumber + 'Toggle_' + deviceID, 'ON');
        //                $('#nrd' + deviceID).show();
        //            } else {
        //                $('#addRelay' + relayNumber + '_' + deviceID).show();
        //                $('#editRelay' + relayNumber + '_' + deviceID).hide();
        //                toggleResetAccRelayState('relay' + relayNumber + 'Toggle_' + deviceID, 'OFF');
        //                $('#nrd' + deviceID).hide();
        //            }
        //        } else {
        //            $('#textAreaError').html(failedString);
        //        }
        //    });
        //}

        function toggleResetAccDevice(deviceID, deviceType, add, relayNumber) {
            $('#textAreaError').html("");
            $('#Notifier_' + deviceID).val(add);
                    var url = "/Rule/ToggleResetAccDevice/";
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
