<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<tr>
    <td style="width: 250px;">
        <h3>Responsiveness</h3>
    </td>
</tr>
<tr>
    <%  bool isMPH = true;
        double MaxRange;
        double MinRange;
        double MaxSpeedMax;
        double MinSpeedMax;
   
        if (ViewData["SpeedScale"] != null)
        {
            isMPH = ViewData["SpeedScale"].ToStringSafe() == "M";
            if (isMPH)
            {

                MinRange = .01;
                MaxRange = 80.0;
                MaxSpeedMax = VehicleDetector.getMaxMPH(Model);
                MinSpeedMax = VehicleDetector.getMinMPH(Model);
            }
            else
            {
                MinRange = .01;
                MaxRange = 140.0;
                MaxSpeedMax = VehicleDetector.GetMaxKPH(Model);
                MinSpeedMax = VehicleDetector.GetMinKPH(Model);
            }
        }
        else
        {
            isMPH = VehicleDetector.IsMPH(Model.SensorID);
            if (isMPH)
            {

                MinRange = .01;
                MaxRange = 80.0;
                MaxSpeedMax = VehicleDetector.getMaxMPH(Model);
                MinSpeedMax = VehicleDetector.getMinMPH(Model);
            }
            else
            {
                MinRange = .01;
                MaxRange = 140.0;
                MaxSpeedMax = VehicleDetector.GetMaxKPH(Model);
                MinSpeedMax = VehicleDetector.GetMinKPH(Model);
            }
        }
    %>
    <tr>
        <td style="width: 250px;">Display as</td>
        <td>
            <input type="checkbox" name="SpeedScale" id="SpeedScale" <%if (isMPH) Response.Write("checked='checked'"); %> />
        </td>
        <td>
            <img alt="help" class="helpIcon" title="Sets the Speed scale that the data is displayed in." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
    </tr>
    <script type="text/javascript">

        setTimeout('$("#SpeedScale").iButton({ labelOn: "MPH" , labelOff: "KPH" });', 500);

    </script>
</tr>

   
     
<tr>
    <td>Max Speed</td>
    <td><%: Html.TextBox("MaxSpeed_Manual", MaxSpeedMax.ToString("0.00"), (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        
    </td>
    <td>
        <img alt="help" class="helpIcon" title="This value determines the sample rate of the sensor and assumes a 1 meter object(vehicle) length. If the speed is 40 MPH and the desired object length is 2 meters, changing the Max Speed to 20 MPH will guarantee a sample is taken at least once while a 2 meter object traveling at 40 MPH. If the same 2 meter object is traveling faster than 40 MPH a sample may not be taken while the object passes over the sensor." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<tr>
    <td></td>
    <td colspan="2">
        <div id="MaxSpeed_Slider"></div>
    </td>
</tr>


<script type="text/javascript">
    $('#MaxSpeed_Slider').slider({
        range: false,
        value: <%:MaxSpeedMax%>,
        min: <%:MinRange%>,
        max: <%:MaxRange%>,
                            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            //$('#MinimumThreshold_Manual').val(ui.values[0]);
            $('#MaxSpeed_Manual').val(ui.value);
        }
    });

    $("#MaxSpeed_Manual").addClass("editField editFieldMedium");

    $("#MaxSpeed_Manual").change(function () {
        if (isANumber($("#MaxSpeed_Manual").val())) {
            if ($("#MaxSpeed_Manual").val() < .01)
                $("#MaxSpeed_Manual").val(.01);
            if ($("#MaxSpeed_Manual").val() > <%:MaxRange%>)
                $("#MaxSpeed_Manual").val(<%:MaxRange%>);
            $('#MaxSpeed_Slider').slider("value", $("#MaxSpeed_Manual").val());
        }
        else
        {
            $("#MaxSpeed_Manual").val(<%: MaxSpeedMax.ToString("0.00")%>);
            $('#MaxSpeed_Slider').slider("value", $("#MaxSpeed_Manual").val());
        }
    });
</script>

<tr>
    <td style="width: 250px;">Enable Min Speed</td>
    <td>
        <input type="checkbox" name="IsZeroing" id="IsZeroing" <%if (VehicleDetector.IsActiveZeroing(Model)) Response.Write("checked='checked' "); if (!Model.CanUpdate) Response.Write("disabled='disabled'"); %> />
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Enables the use of Minimum Speed." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<script type="text/javascript">

    setTimeout('$("#IsZeroing").iButton({ labelOn: "On" , labelOff: "Off" });', 500);

</script>

</tr>

   <tr class="minSpeedContainer">
       <td>Min Speed</td>
       <td><%: Html.TextBox("MinSpeed_Manual", MinSpeedMax.ToString("0.00"), (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
          
       </td>
       <td>
           <img alt="help" class="helpIcon" title="This value is used to determin the zero sampling rate." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
   </tr>
<tr class="minSpeedContainer">
    <td></td>
    <td colspan="2">
        <div id="MinSpeed_Slider"></div>
    </td>
</tr>


<script type="text/javascript">
    $(document).ready(function(){
        if($('#IsZeroing').is(':checked'))
        {
            $('.minSpeedContainer').show();

        }
        else
        {
            $('.minSpeedContainer').hide();
        }

        $('#IsZeroing').change(function(){
    
          
            if($('#IsZeroing').is(':checked'))
            {
                $('.minSpeedContainer').show();

            }
            else
            {
                $('.minSpeedContainer').hide();
            }

            
             
        });

        $('#MinSpeed_Slider').slider({
            range: false,
            value: <%: MinSpeedMax%>,
            min: <%: MinRange%>,
            max: <%: MaxRange%>,
            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
            slide: function (event, ui) {
                //update the amount by fetching the value in the value_array at index ui.value
                //$('#MinimumThreshold_Manual').val(ui.values[0]);
                $('#MinSpeed_Manual').val(ui.value);
            }
        });

        $("#MinSpeed_Manual").addClass("editField editFieldMedium");

        $("#MinSpeed_Manual").change(function () {
            if (isANumber($("#MinSpeed_Manual").val())) {
                if ($("#MinSpeed_Manual").val() < .01)
                    $("#MinSpeed_Manual").val(.01);
                if ($("#MinSpeed_Manual").val() > <%:MaxRange%>)
                    $("#MinSpeed_Manual").val(<%:MaxRange%>);
                $('#MinSpeed_Slider').slider("value", $("#MinSpeed_Manual").val());
            }
            else
            {
                $("#MinSpeed_Manual").val(<%: MinSpeedMax.ToString("0.00")%>);
                $('#MinSpeed_Slider').slider("value", $("#MinSpeed_Manual").val());
            }
        });
    });
</script>



