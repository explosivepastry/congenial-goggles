<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Events/EditBatteryTrigger|Notify when battery is below","Notify when battery is below")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" id="CompareType" name="CompareType" type="hidden" value="<%:eCompareType.Less_Than %>" />
        <input id="CompareValue" name="CompareValue" type="number" class="form-control" value="<%:Model.CompareValue %>"> %
    </div>
</div>
<script type="text/javascript">
    function triggerSettings() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();

        return settings;
    }
    function triggerUrl() {
        return "/Rule/EditBatteryTrigger/<%:Model.NotificationID%>";
    }
</script>
