<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
        bool FullNotiString = CurrentZeroToTwentyAmp.GetShowFullDataValue(Model.SensorID);          
%>

<div class="row sensorEditForm" style="display: none;">
    <%: Html.TextBoxFor(model => FullNotiString, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
</div>