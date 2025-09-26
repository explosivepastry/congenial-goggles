<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<iMonnit.Models.NotifySensorDataModel>>" %>

<%foreach (var item in Model)
    {
        bool isChecked = item.isSendingSensorData;
        string check = "";
        int gridCountLimit = 10;
        int maxLength = 44;

        if (item.sens.SensorName.Length > 44)
            item.sens.SensorName = item.sens.SensorName.Substring(0, 44) + "...";
%>
<%string icon = "app" + item.sens.ApplicationID; %>
<a class="d-flex" style="flex-basis:320px;" onclick="toggleSensor(<%:item.sens.SensorID%>, <%:item.sens.ApplicationID%>)">

    <!-- GridView for more than 10 list items -->


    
        <div class="turn-on-check-card" style="width:100%">
        <div class="check-card-icon">
            
                <span class="sensor icon icon-<%:icon%> icon-xs"></span>
          
        </div>
        <div class="check-card-name" >
            <label class=""><%=item.sens.SensorName%></label>
        </div>
        <div class="check-card-check  <%:isChecked?"ListBorderActive":"ListBorderNotActive"%>" id="sensor_<%:item.sens.SensorID%>" title="<%:item.sens.SensorName%>">
              <%=Html.GetThemedSVG("circle-check") %>			

       
        </div>
        <div class="hidden-lg hidden-md hidden-sm hidden-xs">
            <input type="checkbox" id="ckb_<%:item.sens.SensorID%>" data-checkbox="<%:item.sens.SensorID%>" data-appid="<%:icon%>" class="checkbox checkbox-info" <%=check %> />
        </div>
            </div>
   
</a>
<%} %>

<script>
    $(document).ready(function () {
     $('.checkbox').hide();
     });
</script>
