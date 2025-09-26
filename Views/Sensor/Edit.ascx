<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    Dictionary<string, object> dic = new Dictionary<string, object>();
    if (!Model.CanUpdate)
    {
        dic.Add("disabled", "disabled");
        ViewData["disabled"] = true;
    }

    ViewData["HtmlAttributes"] = dic;
%>
<form action="/Sensor/Edit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false)%>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".helpIcon").tipTip();
        }); 
    </script>
    <div class="formtitle">
        Basic <%:  (Model.MonnitApplication.ApplicationName)%> Sensor Configuration
    </div>
    <div class="formBody">
         <input type="hidden" value="/Sensor/Edit/<%:Model.SensorID %>" name="returns" id="returns" />
        <table style="width: 100%;">
            <% Html.RenderPartial("./SensorEdit/_SensorName", Model);  %>

            <% Html.RenderPartial("./SensorEdit/_HeartBeat", Model);  %>

            <% Html.RenderPartial("./SensorEdit/_UseWithRepeater", Model); %>

            <% Html.RenderPartial("./SensorEdit/_WifiSensor", Model); %>
        </table>
    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveButtons.ascx", Model);%>
</form>



