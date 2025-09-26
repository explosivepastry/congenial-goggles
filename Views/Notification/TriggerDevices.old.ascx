<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>
<% 
    Dictionary<string, object> dic = new Dictionary<string, object>();
%>


<% using (Html.BeginForm())
   {%>
<%: Html.ValidationSummary(true) %>
<%  
       Dictionary<string, object> ClassShort = new Dictionary<string, object>();
       ClassShort.Add("class", "short"); 
%>

    <div class="title2">Assigned Devices</div>
    <div id="notifyingDevices">
        <%:Html.Partial("RetrieveDevices", new iMonnit.Models.ConfigureSensorNotificationModel(Model)) %>
    </div>
    <br />
    <div><a href="/Notification/ConfigureSensors/<%:Model.NotificationID %>" onclick="configureSensors(this); return false;">Assign notification to device(s)</a></div>
<% } %>


<script type="text/javascript">
    $(document).ready(function () {
    });

    function configureSensors(anchor) {
        newModal("Apply notification to other sensors", $(anchor).attr("href"));
    }
</script>
