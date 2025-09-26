<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    byte[] calVal1 = BitConverter.GetBytes((int)Model.Calibration1);
    byte[] _noise = new byte[2];
    byte[] _gain = new byte[2];
    Array.Copy(calVal1, 0, _noise, 0, 2);
    Array.Copy(calVal1, 2, _gain, 0, 2);
    //  if (BitConverter.IsLittleEndian)
    //  {
    //Array.Reverse(_gain);
    //Array.Reverse(_noise);
    //  }
    int noise = BitConverter.ToUInt16(_noise, 0);
    int gain = BitConverter.ToUInt16(_gain, 0);

    noise = ResistiveBridgeMeter.GetNoiseFilter(Model);
    gain = ResistiveBridgeMeter.GetGain(Model);

%>

<p class="useAwareState"></p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gain","Gain")%>
    </div>

    <div class="col sensorEditFormInput">
        <select class="form-select ms-0 user-dets" name="Gain" id="Gain" <%:ViewData["disabled"].ToBool() ? "disabled = disabled" : "" %>>
            <option value="0" <%:gain == 0 ? "selected='selected'" : "" %>>1x</option>
            <option value="1" <%:gain == 1 ? "selected='selected'" : "" %>>2x</option>
            <option value="2" <%:gain == 2 ? "selected='selected'" : "" %>>4x</option>
            <option value="3" <%:gain == 3 ? "selected='selected'" : "" %>>8x</option>
            <option value="4" <%:gain == 4 ? "selected='selected'" : "" %>>16x</option>
            <option value="5" <%:gain == 5 ? "selected='selected'" : "" %>>32x</option>
            <option value="6" <%:gain == 6 ? "selected='selected'" : "" %>>64x</option>
            <option value="7" <%:gain == 7 ? "selected='selected'" : "" %>>128x</option>
        </select>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Noise Rejection","Noise Rejection")%>
    </div>

    <div class="col sensorEditFormInput">
        <select class="form-select ms-0 user-dets" name="NoiseRejection" id="NoiseRejection" <%:ViewData["disabled"].ToBool() ? "disabled = disabled" : "" %>>
            <option value="0" <%:noise == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("None","None")%></option>
            <option value="1" <%:noise == 1 ? "selected='selected'" : "" %>>50 and 60 Hz</option>
            <option value="2" <%:noise == 2 ? "selected='selected'" : "" %>>50 Hz <%: Html.TranslateTag("Only","Only")%></option>
            <option value="3" <%:noise == 3 ? "selected='selected'" : "" %>>60 Hz <%: Html.TranslateTag("Only","Only")%></option>
        </select>
    </div>
</div>


<%--	Only editable in admin edit--%>
<%--<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Averaging Filter","Averaging Filter")%>
    </div>

    <div class="col sensorEditFormInput">
        <input type="number" class="form-control ms-0 user-dets"
            name="AveragingFilter" id="averagingFilter"
            <%:ViewData["disabled"].ToBool() ? "disabled = disabled" : "" %> value="<%= Model.Calibration2 %>" />
        <a id="avgFilterNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>--%>

<script>

    $(function () {
        var avgFilterMinVal = 1;
        var avgFilterMaxVal = 30;


<%--        <% if (Model.CanUpdate)
           { %>

        const arrayForSpinner = arrayBuilder(avgFilterMinVal, avgFilterMaxVal, 1);
        createSpinnerModal("avgFilterNum", 'Averaging Filter', "averagingFilter", arrayForSpinner);

         <%}%>--%>

        $("#averagingFilter").addClass('editField editFieldSmall')

        $("#averagingFilter").change(function () {
            let val = parseFloat($("#averagingFilter").val());

            if (isANumber(val)) {
                if (val < avgFilterMinVal)
                    $("#averagingFilter").val(avgFilterMinVal);
                if (val > avgFilterMaxVal)
                    $("#averagingFilter").val(avgFilterMaxVal);

            } else {

                $("#averagingFilter").val(avgFilterMinVal);
            }
        });

    });
</script>
