<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<form action="/Sensor/Scale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
<%: Html.ValidationSummary(false) %>
          <input type="hidden" value="/Sensor/Scale/<%:Model.SensorID %>" name="returns" id="returns" />
      <%
          string label = Pressure300PSI.GetLabel(Model.SensorID);
          double getSavedVal = Pressure300PSI.GetSavedValue(Model.SensorID);
         %>
      
<div class="formtitle">
    Pressure Scale 
</div>
<div class="formBody">
     
    <table style="width: 100%;">
       
       <tr>
            <td>Scale 
            </td>
           <td>
                <select id="label" name="label" class="editFieldSmall tzSettings">
                    <option value="PSI" <%: label.Contains("PSI") || string.IsNullOrWhiteSpace(label) ? "selected":"" %> >PSI</option>
                    <option value="atm" <%: label.Contains("atm") ? "selected":"" %>>atm</option>
                    <option value="bar" <%: label.Contains("bar") ? "selected":"" %>>bar</option>
                    <option value="kPA" <%: label.Contains("kPA") ? "selected":"" %>>kPA</option>
                    <option value="Torr" <%: label.Contains("Torr") ? "selected":"" %>>Torr</option>
                    <option value="Custom" <%: label.Contains("Custom") ? "selected":"" %>>Custom</option>
                </select>
           </td>     
           <td>
                   <img alt="help" class="helpIcon" title="Scale and label of the formatted output for this sensor" src="<%:Html.GetThemedContent("/images/help.png")%>" />     
           
           </td>
        </tr>
          <tr class="hideMe" hidden="hidden">
            <td>0 PSI Value:
            </td>
            <td>
                <input class="aSettings__input_input" type="text"  id="lowValue" name="lowValue" value="<%:Monnit.Pressure50PSI.GetLowValue(Model.SensorID) %>" />
            </td>
        </tr>
        <tr class="hideMe" hidden="hidden">
            <td><label id="highvalLabel"></label> PSI Value:
            </td>
            <td>
                <input class="aSettings__input_input" type="text"  id="highValue" name="highValue" value="<%:Monnit.Pressure50PSI.GetHighValue(Model.SensorID) %>" />
            </td>
        </tr>
        <tr class="hideMe" hidden="hidden">
            <td>Label:
            </td>
            <td>
                <input class="aSettings__input_input" type="text" id="customLabel" name="customLabel" value="<%:Monnit.Pressure50PSI.GetCustomLabel(Model.SensorID) %>" />
            </td>
        </tr>
        <tr class="hideMe" hidden="hidden">
            <td>Number of Decimals:
            </td>
            <td>
                <input class="aSettings__input_input" type="text" id="decimalTrunkValue" name="decimalTrunkValue" value="<%:Monnit.PressureNPSI.GetDecimalTrunkValue(Model.SensorID) %>"/>
            </td>
            <td>
                <img alt="help" class="decimalHelpIcon" title="Allowable decimal range is from 0 - 5" src="<%:Html.GetThemedContent("/images/help.png")%>" />     
           </td>
        </tr>       
        
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
        $('.helpIcon').tipTip();
        $('.decimalHelpIcon').tipTip();

        if ($('#label :selected').text() == "Custom") {

            $(".hideMe").show();
            var hilblVal = $('#MaxCapcity').val();
            $('#highvalLabel').text(hilblVal);
            $("#lowValue").addClass('editField editFieldMedium');
            $("#highValue").addClass('editField editFieldMedium');
            $("#label").addClass('editField editFieldMedium');

            $("#highValue").change(function () {
                if (!isANumber($("#highValue").val()))
                    $("#highValue").val(hilblVal);
            });

            $("#lowValue").change(function () {
                if (!isANumber($("#lowValue").val()))
                    $("#lowValue").val(0);
            });
        }

        $('#label').change(function () {

            if ($('#label :selected').text() == "Custom") {

                $(".hideMe").show();
                $('#highvalLabel').text(50);
                $("#lowValue").addClass('editField editFieldMedium');
                $("#highValue").addClass('editField editFieldMedium');
                $("#label").addClass('editField editFieldMedium');

                $("#highValue").change(function () {
                    if (!isANumber($("#highValue").val()))
                        $("#highValue").val(50);
                });

                $("#lowValue").change(function () {
                    if (!isANumber($("#lowValue").val()))
                        $("#lowValue").val(0);
                });
            }
            else {
                $(".hideMe").hide();
            }
        });

        $('#save').click(function () {
            postForm($('#SensorScale_<%: Model.SensorID%>'));
        });
    });
</script>
    
</form>