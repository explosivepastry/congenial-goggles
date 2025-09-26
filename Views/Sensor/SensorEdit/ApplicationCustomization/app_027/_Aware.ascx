<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Aware","Aware")%>
    </div>
    <div class="col sensorEditFormInput">

        <select <%=Model.CanUpdate ? "" : "disabled" %> name="awareState" id="awareState" class="form-select" <%: !Model.CanUpdate ? "disabled='disabled'":""%>>
            <option value="0" <%if (Model.Calibration1 == 0)
                                {%>
                selected="selected" <%}%>>When Light</option>
            <option value="1" <%if (Model.Calibration1 == 1)
                                {%>
                selected="selected" <%}%>>When Dark</option>
            <option value="2" <%if (Model.Calibration1 == 2)
                                {%>
                selected="selected" <%}%>>On Change</option>
        </select>
        <%: Html.ValidationMessageFor(model => model.EventDetectionPeriod)%><span style="font-size: 11px;"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_005|Lower numbers are more sensitive","Lower numbers are more sensitive")%></span>
    </div>
</div>

<script type="text/javascript">

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        //$('#awareState').mobiscroll().select({
        //    theme: 'ios',
        //    display: popLocation,
        //    minWidth: 120,
        //    headerText: 'Select...',
        //    defaultValue: $('#awareState').val(),
        //    onSet: function (event, inst) {
        //        var sensiVal = parseFloat(inst.getVal());
        //        $('#awareState').val(sensiVal);
        //        $('#awareState').change();
        //    }
        //});

    <%}%>

    });
</script>
