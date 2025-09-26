<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<div id="container1">
    <form action="/Overview/DeviceNotify/<%:Model.SensorID %>" id="Notify_<%:Model.SensorID %>" method="post">
        <%: Html.ValidationSummary(false) %>
        <input type="hidden" value="/Overview/DeviceNotify/<%:Model.SensorID %>" name="returns" id="returns" />

        <%--        <div class="formtitle">
            Sensor Data To Transfer to Notifier  
        <input style="margin: -4px 18px 0px 0px;" class="bluebutton" type="button" id="clearPendingMsg" value="Clear Pending" />
        </div>--%>

        <div>
            <%--            <div class="formtitle">
                Pending Message History
                <br />
            <input type="button" style="margin: -4px 17px 0px 0px;" data-url="/Sensor/ClearPendingNotifierHistory?sensorID=<%: Model.SensorID%>" class="greybutton" id="clearPendingCommandHist" value="Clear Pending" />
            </div>--%>

            <%--            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_013|Pending Message History","Pending Message History")%>
            </div>--%>

            <table width="100%" class="notificationHistoryTable">
                <tr>
                    <th style="width: 20px"></th>
                    <th>Date</th>
                    <th>LED</th>
                    <th>Buzzer</th>
                    <th>Message Scroll</th>
                    <th>Backlight</th>
                    <th>Device Name</th>
                    <th style="width: 20px"></th>
                </tr>

                <%--            <div style="height: 270px; overflow: auto;">--%>
                <%--                <table width="100%">--%>
                <% foreach (var item in NotificationRecorded.LoadGetMessageForLocalNotifier(Model.SensorID))
                    {
                        if (!item.Delivered)
                        {
                %>
                <tr>
                    <td></td>
                    <td style="width: 140px"><%:  (Monnit.TimeZone.GetLocalTimeById(item.NotificationDate, MonnitSession.CurrentCustomer.Account.TimeZoneID))  %></td>
                    <% string[] ser = item.SerializedRecipientProperties.Split('|');

                        for (int i = 0; i < ser.Length; i++)
                        {%>

                    <td <%: i == 0? "style='padding-left:21px'":i == 1 || i == 2? "style='padding-left:85px'": i == 3?"style='padding-left:130px'":i == 4?"style='padding-left:65px'":"" %>><%: ser[i] %></td>
                    <%}%>
                </tr>

                <%}
                    }%>
            </table>
        </div>



    </form>
</div>

<div class="row sensorEditForm">
    <div class="col sensorEditFormInput" id="hyst3">
        <input type="button" data-url="/Sensor/ClearPendingNotifierHistory?sensorID=<%: Model.SensorID%>&newLook=true" class="btn btn-secondary btn-sm" id="clearPendingCommandHist" value="Clear Pending" />
        <span id="successfulClear"></span>
    </div>
</div>

<script type="text/javascript">

    $('#clearPendingCommandHist').click(function (e) {
        e.preventDefault();
        var Url = $(this).data('url');
        $.ajax({
            url: Url,
            context: document.body
        }).done(function (result) {
            result == 'Success' ? $('#successfulClear').text("Clear Successful") : '';
        });
    });

</script>

<style>
    #successfulClear {
        color: forestgreen;
    }
</style>
<script>
    var sensorFilterTimeout = null;
    $(document).ready(function () {



     





        $('#clearPendingCommandHist').click(function (e) {

            e.preventDefault();
            var Url = $(this).data('url');
            $('#container1').load(Url);
        });


    });







</script>
