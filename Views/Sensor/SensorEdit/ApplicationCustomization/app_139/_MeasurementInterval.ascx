<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|139-MeasurementInterval-Title","Measurement Interval")%> (<%:Html.TranslateTag("Minutes") %>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number"  <%=Model.CanUpdate ? "" : "disabled" %> id="MeasurementInterval" name="MeasurementInterval" value="<%=Math.Round(LightSensor_PPFD.GetMeasurementInterval(Model)/60.0, 3) %>" />
        <a id="measNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>        
    </div>
</div>

<script type="text/javascript">

    var MeasurementInterval_array = [1, 2, 10, 20, 30, 60];
    $(function () {

               <% if (Model.CanUpdate)
                  { %>

        createSpinnerModal("measNum", "Minutes", "MeasurementInterval", MeasurementInterval_array);

        <%}%>
      
        $("#MeasurementInterval").change(function () {
            if (isANumber($("#MeasurementInterval").val())) {
                //Check if less than 1 second
                if ($("#MeasurementInterval").val() * 60 < 1)
                    $("#MeasurementInterval").val(0.017);

                //Check if greater than heartbeat
                if (Number($('#MeasurementInterval').val()) > Number($('#ReportInterval').val())) {
                    $('#MeasurementInterval').val(Number($('#ReportInterval').val()));

                }

            }
            else {
                $("#MeasurementInterval").val(<%: Math.Round(LightSensor_PPFD.GetMeasurementInterval(Model)/60.0, 3)%>);

            }
        });

        //Check values if report interval changes
        $("#ReportInterval").change(function () {
            $('#MeasurementInterval').change();
        });

    });


</script>

