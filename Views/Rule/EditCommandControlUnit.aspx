<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Sensor>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditCommandControlUnit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%long NotiID = ViewBag.NotificationID; %>
    <%:Html.Partial("~/Views/Rule/Header.ascx",(Notification)ViewBag.Notification) %>
    <%:Html.Partial("CommandControlUnitList", Model) %>

    <script type="text/javascript">
        document.getElementById("go-back").onclick = function () {
            window.location.href = "/Rule/ChooseTaskToEdit/<%=NotiID %>";
        }
        var successString = '<%: Html.TranslateTag("Success")%>';
        var failedString = '<%: Html.TranslateTag("Failed")%>';

        $(document).ready(function () {

            $('#saveControlUnitTextBtn').click(function () {
                $('#textAreaError').html("");
                $.post('/Rule/EditControlUnitSettings/<%=NotiID%>', $('#sendCommandPageForm').serialize(), function (data) {
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

        function toggleControlUnitDevice(deviceID, deviceType, add,relayNumber) {
            $('#textAreaError').html("");
            $('#Notifier_' + deviceID).val(add);
            var url = "/Rule/ToggleControlUnitDeviceEdit?id=<%=NotiID%>&";
            var params = "deviceID=" + deviceID;
            params += "&deviceType=" + deviceType;
            params += "&add=" + add;
            $.post(url, params, function (data) {
                if (data == "Success") {
                    if (add) {
                        $('#relay' + relayNumber + 'State_' + deviceID).val("True");
                        $('#addRelay'+ relayNumber +'_' + deviceID).hide();
                        $('#editRelay' + relayNumber + '_' + deviceID).show();
                        $('#caretOpen_' + deviceID + '_' + relayNumber).css("display", "none");
                        $('#caretClose_' + deviceID + '_' + relayNumber).css("display", "block");
                        $('#editRelay' + relayNumber + '_' + deviceID).css("height", "auto");
                    } else {
                        $('#relay' + relayNumber + 'State_' + deviceID).val("False");
                        $('#addRelay' + relayNumber + '_' + deviceID).show();
                        $('#editRelay' + relayNumber + '_' + deviceID).hide();
                    }
                } else {
                    toastBuilder(failedString);
/*                    $('#textAreaError').html(failedString);*/
                }
            });
        }

    </script>
</asp:Content>
