<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Sensor>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CommandLocalAlert
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%long NotiID = ViewBag.NotificationID; %>
    <%:Html.Partial("~/Views/Rule/Header.ascx",(Notification)ViewBag.Notification) %>
    <%:Html.Partial("CommandLocalAlertList", Model) %>
                                               
    <script type="text/javascript">
        document.getElementById("go-back").onclick = function () {
            window.location.href = "/Rule/ChooseTaskToEdit/<%=NotiID%>";
        }
        var requiredString = '<%: Html.TranslateTag("Local Alert Text Required")%>';
        var successString = '<%: Html.TranslateTag("Success")%>';
        var failedString = '<%: Html.TranslateTag("Failed")%>';

        $(document).ready(function () {

            $('#saveLocalAlertTextBtn').click(function () {
                $('#textAreaError').html("");
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
                $.post('/Rule/EditLocalAlertSettings/<%=NotiID%>', $('#sendCommandPageForm').serialize(), function (data) {
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

        function toggleLocalAlertDevice(deviceID, add) {
            $('#Notifier_' + deviceID).val(add);
            var url = "/Rule/ToggleLocalAlertDeviceEdit?id=<%=NotiID%>";
            var params = "deviceID=" + deviceID;
            params += "&add=" + add;
            $.post(url, params, function (data) {
                $('#localAlertDevice_' + deviceID).html(data);
            });
        }


    </script>
</asp:Content>
