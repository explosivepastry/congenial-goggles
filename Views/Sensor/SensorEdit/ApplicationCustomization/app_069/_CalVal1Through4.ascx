<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<div class="row sensorEditForm">
    <div class="col-12 col-md-3 1111">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="col sensorEditFormInput" style="justify-content: space-between; max-width: 273px">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Calibration3Chk" id="Calibration3Chk" <%=Model.Calibration3 == 1 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <button class="btn btn-secondary" style="width: 112px;" type="button" id="ResetAccumulator" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>">
            <%: Html.TranslateTag("Reset","Reset")%>
        </button>
        <div style="display: none;"><%: Html.TextBoxFor(model => model.Calibration3, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_069|LED Power Mode","LED Power Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Calibration4Chk" id="Calibration4Chk" <%=Model.Calibration4 == 1 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <div style="display: none;"><%: Html.TextBoxFor(model => model.Calibration4, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<%--Pulse Edge Detection--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_069|Detection Type","Detection Type")%>
    </div>
    <div class="col sensorEditFormInput">
        <select style="margin: 0;" id="edge" class="form-select" name="edge">
            <option value="0" <%:Model.Calibration1 == 0 ? "selected='selected'" : "" %>>Positive Edge</option>
            <option value="1" <%:Model.Calibration1 == 1 ? "selected='selected'" : "" %>>Negative Edge</option>
        </select>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Inactivity Timer","Inactivity Timer")%> Seconds
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="debounce" id="debounce" value="<%: Model.Calibration2 %>" />
        <a id="cal1" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">

    $("#edge").addClass('editField editFieldMedium');

    $(function () {
          <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner1 = arrayBuilder(1, 30, 1);
        createSpinnerModal("cal1", "Seconds", "debounce", arrayForSpinner1);

        <%}%>
        $("#debounce").change(function () {
            //Check if less than min
            if ($("#debounce").val() < 1)
                $("#debounce").val(1);
            //Check if greater than max
            if ($("#debounce").val() > 30)
                $("#debounce").val(30);
        });
    });

</script>
