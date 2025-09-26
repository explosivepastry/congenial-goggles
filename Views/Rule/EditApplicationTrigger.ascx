<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<div class="col-12">

    <%
        Html.RenderPartial("~/Views/Sensor/DataTypeSpecific\\Default\\_DatumEventTrigger.ascx", Model);
    %>

</div>

<script type="text/javascript">
    function triggerSettings() {
        return datumConfigs();
    }
    function triggerUrl() {
        return "/Rule/EditApplicationTrigger/<%:Model.NotificationID%>";
    }
</script>
