<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<AvailableNotificationBySensorModel>>" %>

<%
    Sensor sensor = ViewData["Sensor"] as Monnit.Sensor;
    int DatumIndexFilter = ViewData["DatumIndexFilter"].ToInt();
    string AssignedFilter = ViewData["AssignedFilter"].ToStringSafe();
    int Count = 0;
%>

<% 	
    foreach (AvailableNotificationBySensorModel item in Model)
    {
        foreach (AvailableNotificationBySensorDetailsModel detail in item.DetailsList)
        {
            bool ShowDatumFilter = DatumIndexFilter < 0 || DatumIndexFilter == detail.DatumIndex;
            bool ShowAssignedFilter = string.IsNullOrEmpty(AssignedFilter) || detail.SensorNotificationID > 0 == AssignedFilter.ToBool();

            if (ShowDatumFilter && ShowAssignedFilter)
            {
                Count++;
%>

<div class=" toggleRule super_small_card" data-notiid="<%:item.Notification.NotificationID%>" data-dindex="<%:detail.DatumIndex%>">
    <div class=" ">

        <div class="triggerDevice__name" style="font-size:.9rem;text-align:center;">
            <strong style=" font-size:1rem"><%:System.Web.HttpUtility.HtmlDecode(item.Notification.Name) %></strong>
           
            <%if (item.Notification.NotificationClass == eNotificationClass.Application)
                { %>

            <%:sensor.GetOnlyDatumName(detail.DatumIndex) %>
            <%} %>
        </div>

        <div class="col-1" style="text-align: center;">
            <div>
                <div class="dropleft" style="width: 50px;">
                </div>
            </div>
        </div>

    </div>
</div>

<%
            }
        }
    }
%>


<script>
    $(function () {
        $(".toggleRule").click(function (e) {
            if ($(e.target).parents('.dropleft').length > 0)
                return;

            TogggleExistingRule($(this),<%:sensor.SensorID%>, $(this).data('notiid'), $(this).data("dindex"));
        });
    });
    function TogggleExistingRule(element, sensorID, notificationID, datumindex) {
        //If Adding notification
        let checkbox = $(element).find(".sensorID" + sensorID + ".datumIndex" + datumindex).hasClass("ListBorderActive");
        if (checkbox) {
            removeNotification(sensorID, notificationID, datumindex)
        }
        else {
            AddExistingRule(sensorID, notificationID, datumindex);
        }
    }

    function AddExistingRule(sensorID, notificationID, datumindex) {
        $("#newRuleConfigurationHolder").html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
        let params = {};
        var url = "/Rule/AddDevicetoExistingNotification";
        params.sensorID = sensorID;
        params.notificationIDs = notificationID;
        params.datumindex = datumindex;

        $.post(url, params, function (data) {
            window.location.href = "/Rule/RuleComplete/" + notificationID;
        });
    }

    <%if(Count>0){%>
        $("#existingRuleDiv").show();
    <%}%>

</script>
