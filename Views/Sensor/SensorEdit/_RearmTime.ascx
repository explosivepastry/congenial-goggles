<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>





<%--Rearm Time--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Time to Re-Arm (seconds)","Time to Re-Arm (seconds)") %>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.RearmTime, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.RearmTime)%>
        <img alt="help" class="helpIcon" title="The time in seconds after a triggering event that the sensor will wait before re-arming itself." src="<%:Html.GetThemedContent("/images/help.png")%>" />
    </div>
</div>

<tr>
    <td></td>
    <td colspan="2">
        <div id="Rearm_Slider"></div>
    </td>
</tr>
<script type="text/javascript">
    var rearm_array = [1, 2, 3, 5, 7, 10, 15, 20, 25, 30, 60, 120, 180, 300, 600];
    function getAssessmentIndex() {
        var retval = 0;
        $.each(rearm_array, function (index, value) {
            if (value <= $("#RearmTime").val())
                retval = index;
        });

        return retval;
    }

    $('#Rearm_Slider').slider({
        value: getAssessmentIndex(),
        min: 0,
        max: rearm_array.length - 1,
                                    <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#RearmTime').val(rearm_array[ui.value]);
        }
    });

    $("#RearmTime").removeClass('aSettings__input_input');

    $("#RearmTime").addClass('form-control');

    $("#RearmTime").change(function () {
        if (isANumber($("#RearmTime").val())) {
            if ($("#RearmTime").val() < 1)
                $("#RearmTime").val(1);
            if ($("#RearmTime").val() > 600)
                $("#RearmTime").val(600)
            $('#Rearm_Slider').slider("value", getAssessmentIndex());
        }
        else
        {
            $("#RearmTime").val(1);
            $('#Rearm_Slider').slider("value", getAssessmentIndex());
        }
    });
</script>
