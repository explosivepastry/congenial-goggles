<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Sensor>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CommandLocalAlert
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% ViewBag.inProgressRecipients = MonnitSession.NotificationRecipientsInProgress; %>
    <%=Html.Partial("_CreateNewRuleProgressBar") %>
    <%=Html.Partial("CommandLocalAlertList", Model) %>

    <script type="text/javascript">
        document.getElementById("go-back").onclick = function () {
            window.location.href = "/Rule/ChooseTask/";
        }
        var requiredString = '<%: Html.TranslateTag("Local Alert Text Required")%>';
        var successString = '<%: Html.TranslateTag("Success")%>';
        var failedString = '<%: Html.TranslateTag("Failed")%>';

        $(document).ready(function () {

            $('#saveLocalAlertTextBtn').click(function () {
                var values = new Object();
                $("#LocalAlertText").val($("#LocalAlertText").val().replace(/[<]br[^>]*[>]/gi, ""));  // removes all <br>);
                var msg = $("#LocalAlertText").val();
                values["LocalAlertText"] = msg;

                if (msg.length == 0) {
                    toastBuilder(requiredString);
                    //$('#textAreaError').html("");
                    //$('#textAreaError').html(requiredString);
                    return;
                }
                $('#textAreaError').html("");
                $.post('/Rule/SaveLocalAlertSettings', $('#sendCommandPageForm').serialize(), function (data) {
                    if (data == "Success") {
                        toastBuilder(successString);
/*                        $('#textAreaError').html(successString)*/
                        setTimeout(function () {
                            window.location.href = "/Rule/ChooseTask/";
                        }, 1000); // 1 second wait

                    } else {
                        /*                        $('#textAreaError').html(failedString + ": " + data);*/
                        toastBuilder(`${failedString}: ${data}`);
                        //setTimeout(function () {
                        //    window.location.href = "/Rule/ChooseType/";
                        //}, 1000); // 1 second wait
                    }
                });
            });
        });

        function toggleLocalAlertDevice(deviceID, add) {
            $('#Notifier_' + deviceID).val(add);
            var url = "/Rule/ToggleLocalAlertDevice/";
            var params = "deviceID=" + deviceID;
            params += "&add=" + add;
            $.post(url, params, function (data) {
                $('#localAlertDevice_' + deviceID).html(data);
            });
        }

      
    </script>
</asp:Content>
