<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Minor Interval","Minor Interval")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Calval1_Manual" id="Calval1_Manual" value="<%=Model.Calibration1 %>" />
        <a id="minNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Maximum Reading","Maximum Reading")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Calval3_Manual" id="Calval3_Manual" value="<%=Model.Calibration3 %>" />
        <a id="maxNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_060|Sensitivity","Sensitivity")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Calval2_Manual" id="Calval2_Manual" value="<%=Model.Calibration2 %>" />
        <a id="sensiNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>

<p class="useAwareState"></p>

<script type="text/javascript">

    var sensitivity_array = [1, 10, 25, 50, 100, 300, 500];
    var max_array = [10, 25, 50, 75, 100, 250];
    var min_array = [20, 50, 100, 250, 400, 600];


    $(function () {
                        <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("sensiNum", "Sensitivity", "Calval2_Manual", sensitivity_array);
        createSpinnerModal("maxNum", "Max Reading", "Calval3_Manual", max_array);
        createSpinnerModal("minNum", "Min Interval", "Calval1_Manual", min_array);

    <%}%>

        $("#Calval1_Manual").change(function () {
            if (isANumber($("#Calval1_Manual").val())) {
                if ($("#Calval1_Manual").val() < 20)
                    $("#Calval1_Manual").val(20);
                if ($("#Calval1_Manual").val() > 600)
                    $("#Calval1_Manual").val(600)
            }
            else {
                $('#Calval1_Manual').val(<%: Model.Calibration1%>);

            }

        });

        $("#Calval2_Manual").change(function () {
            if (isANumber($("#Calval2_Manual").val())) {
                if ($("#Calval2_Manual").val() < 1)
                    $("#Calval2_Manual").val(1);
                if ($("#Calval2_Manual").val() > 500)
                    $("#Calval2_Manual").val(500)
            }
            else {
                $('#Calval2_Manual').val(<%: Model.Calibration2%>);

            }

        });

        $("#Calval3_Manual").change(function () {
            if (isANumber($("#Calval3_Manual").val())) {
                if ($("#Calval3_Manual").val() < 10)
                    $("#Calval3_Manual").val(10);
                if ($("#Calval3_Manual").val() > 250)
                    $("#Calval3_Manual").val(250)
            }
            else {
                $('#Calval3_Manual').val(<%: Model.Calibration3%>);

            }

        });


    });
</script>

