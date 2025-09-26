<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Sensor/Scale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
<%: Html.ValidationSummary(false) %>
         <input type="hidden" value="/Sensor/Scale/<%:Model.SensorID %>" name="returns" id="returns" />
      
<div class="formtitle">
    Measurement Scale 
</div>
<div class="formBody">
    <table style="width: 100%;">
        <% Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/67/_MeasurementScale.ascx", Model);%>
    </table>
</div>

 <div class="buttons">
     <span style="color:red;">
     <%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
    </span>
     <span style="color:black;">
     <%: ViewBag.Message == null ? "":ViewBag.Message %>
    </span>
     <input class="bluebutton" type="button" id="save" value="Save" />
   <div style="clear: both;"></div>
</div>

<script>
    $(document).ready(function ()
    {
        $('#save').click(function ()
        {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
        });
    });
</script>
</form>