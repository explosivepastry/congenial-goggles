<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Sensor/Scale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
<%: Html.ValidationSummary(false) %>
          <input type="hidden" value="/Sensor/Scale/<%:Model.SensorID %>" name="returns" id="returns" />
      
      
<div class="formtitle">
    Current Scale 
</div>
<div class="formBody">
    <table style="width: 100%;">
        <% Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/22/_LowHighLabel.ascx", Model);%>
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
        $('#lowValue').addClass("editField editFieldMedium");
        $('#highValue').addClass("editField editFieldMedium");
        $('#label').addClass("editField editFieldMedium");

        $('#save').click(function ()
        {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
        });
    });
</script>
</form>