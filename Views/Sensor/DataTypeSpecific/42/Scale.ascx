<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<form action="/Sensor/Scale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
<%: Html.ValidationSummary(false) %>
          <input type="hidden" value="/Sensor/Scale/<%:Model.SensorID %>" name="returns" id="returns" />
      
      
<div class="formtitle">
    Measurement Scale 
</div>
<div class="formBody">
    <table style="width: 100%;">
       <tr>
            <td class="title2">Data Transformation
            </td>
        </tr>
        <tr>
            <td>0 Volt Value:
            </td>
            <td style="width: 588px;">
                <input class="aSettings__input_input" id="lowValue" type="text" name="lowValue" value="<%:Monnit.ZeroToTenVolts.GetLowValue(Model.SensorID) %>" />
            </td>
        </tr>
        <tr style="width: 588px;">
            <td>10 Volt Value:
            </td>
            <td>
                <input class="aSettings__input_input" id="highValue" type="text" name="highValue" value="<%:Monnit.ZeroToTenVolts.GetHighValue(Model.SensorID) %>" />
            </td>
        </tr>
        <tr>
            <td>Label:
            </td>
            <td style="width: 588px;">
                <input class="aSettings__input_input" id="label" type="text" name="label" value="<%:Monnit.ZeroToTenVolts.GetLabel(Model.SensorID) %>" />
            </td>
        </tr>
            <script>
                $("#lowValue").addClass('editField editFieldMedium');
                $("#highValue").addClass('editField editFieldMedium');
                $("#label").addClass('editField editFieldMedium');
            </script>
        
    </table>
</div>

<div class="buttons">
    <span style="color: red;">
        <%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
    </span>
    <span style="color: black;">
        <%: ViewBag.Message == null ? "":ViewBag.Message %>
    </span>
    <input class="bluebutton" type="button" id="save" value="Save" />
    <div style="clear: both;"></div>
</div>

<script>
    $(document).ready(function () {
        $('#save').click(function () {
            postForm($('#SensorScale_<%: Model.SensorID%>'));
        });
    });
</script>
</form>