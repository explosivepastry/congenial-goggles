<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Sensor>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CommandControlUnit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% ViewBag.inProgressRecipients = MonnitSession.NotificationRecipientsInProgress; %>
    <%=Html.Partial("_CreateNewRuleProgressBar") %>
    <%=Html.Partial("CommandControlUnitList", Model) %>

    <script type="text/javascript">
        document.getElementById("go-back").onclick = function () {
            window.location.href = "/Rule/ChooseTask/";
        }
        var successString = '<%: Html.TranslateTag("Success")%>';
        var failedString = '<%: Html.TranslateTag("Failed")%>';

        $(document).ready(function () {

            $('#saveControlUnitTextBtn').click(function () {
                $('#textAreaError').html("");
                $.post('/Rule/SaveControlUnitSettings/', $('#sendCommandPageForm').serialize(), function (data) {
                    if (data == "Success") {
                        toastBuilder(successString);
                        setTimeout(function () {
                            window.location.href = "/Rule/ChooseTask/";
                        }, 1000); // 1 second wait

                    } else {
                        toastBuilder(`${failedString}: ${data}`);
                        /*$('#textAreaError').html(failedString + ": " + data);*/
                        //setTimeout(function () {
                        //    window.location.href = "/Rule/ChooseType/";
                        //}, 4000); // 1 second wait
                    }
                });
            });
        });

        function toggleControlUnitDevice(deviceID, deviceType, add, relayNumber) {
            $('#textAreaError').html("");
            $('#Notifier_' + deviceID).val(add);
            var url = "/Rule/ToggleControlUnitDevice/";
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
                    } else {
                        $('#addRelay' + relayNumber + '_' + deviceID).show();
                        $('#editRelay' + relayNumber + '_' + deviceID).hide();
                    }
                } else {
                    $('#textAreaError').html(failedString);
                }
            });
        }
    </script>
</asp:Content>
