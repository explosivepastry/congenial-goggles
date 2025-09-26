<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AutomatedNotification>" %>
<div id="replace">
    <%using (Html.BeginForm("AutomatedSchedule", "Notification", FormMethod.Post, new { id = "AutomatedSchedule", name = "AutomatedSchedule" }))
      {  %>
    <%: Html.HiddenFor(vm => vm.AutomatedNotificationID) %>
    <%: Html.HiddenFor(vm => vm.AutoReset) %>
    <%: Html.HiddenFor(vm => vm.Description) %>
    <%: Html.HiddenFor(vm => vm.ExternalID) %>
    <%: Html.Hidden("id", Model.NotificationID) %>
    

    <div class="formBody" style="margin-top: 20px;">
        <table width="100%">
            <tr>
                <td width="20px"></td>
                <td>Process Frequency</td>
                <td>

                    <select id="proccessFrequency" name="proccessFrequency">
                        <option value="60" <%: Model.ProcessFrequency == 60 ? "selected" : ""%>>Every hour</option>
                        <option value="120" <%: Model.ProcessFrequency == 120 ? "selected" : ""%>>Every 2 hours</option>
                        <option value="360" <%: Model.ProcessFrequency == 360 ? "selected" : ""%>>Every 6 hours</option>
                        <option value="720" <%: Model.ProcessFrequency == 720 ? "selected" : ""%>>Every 12 hours</option>
                        <option value="1440" <%: Model.ProcessFrequency == 1440 ? "selected" : ""%>>Once every day</option>
                    </select>
                </td>
                <td width="20px"></td>
            </tr>
        </table>
    </div>

    <div class="buttons" style="height: 30px;">
        <a class="bluebutton" style="margin: 0px 0px 0px 30px;" id="save" href="Notification/AutomatedSchedule/">Save</a>
        
        <%if (!string.IsNullOrWhiteSpace(ViewBag.result)){ %>
        <div style="margin: 9px 0px 0px 0px;"><%: ViewBag.result %></div>
            <%} %>
        <%--<div style="clear: both;"></div>--%>
        
    </div>
    <%} %>
</div>

<script type="text/javascript">
    $(function () {
        $('#save').click(function (e) {
            e.preventDefault();

            var href = $(this).attr('href');
            var formSerial = $('#AutomatedSchedule').serialize();

            $.post(href, formSerial, function (data) {
                $('#replace').html(data);
            });


        });
    });
</script>
