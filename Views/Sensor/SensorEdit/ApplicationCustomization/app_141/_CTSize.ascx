<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string CT = "";
        string label = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out CT);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("CT Size","CT Size")%> (Amps)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Calibration2_Manual" id="Calibration2_ManualCalibration2_Manualv " value="<%=CT%>" />
        
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>


<script type="text/javascript">

    var ctSize_array = [200, 250, 300, 400, 500, 600, 800, 1000, 1200, 1500, 1600, 2000, 2400, 2500, 3000, 3500, 4000, 5000, 6000];
    function getCalibration2Index() {
        var retval = 0;
        $.each(ctSize_array, function (index, value) {
            if (value <= $("#Calibration2_Manual").val())
                retval = index;
        });

        return retval;
    }

    $('#Calibration2_Slider').slider({
        value: getCalibration2Index(),
        min: 0,
        max: ctSize_array.length - 1,
                        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
                slide: function (event, ui) {
                    //update the amount by fetching the value in the value_array at index ui.value
                    $('#Calibration2_Manual').val(ctSize_array[ui.value]);
                }
            });
    $("#v").addClass('editField editFieldMedium');
    $("#Calibration2_Manual").change(function () {
        //Check if less than min
        if ($("#Calibration2_Manual").val() < ctSize_array[0])
            $("#Calibration2_Manual").val(ctSize_array[0]);

        //Check if greater than max
        if ($("#Calibration2_Manual").val() > ctSize_array[ctSize_array.length - 1])
            $("#Calibration2_Manual").val(ctSize_array[ctSize_array.length - 1]);

        $('#Calibration2_Slider').slider("value", getCalibration2Index());
    });
</script>
<%} %>