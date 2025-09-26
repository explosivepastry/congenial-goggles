<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Minor Interval","Minor Interval")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class=" form-control" type="number"<%=Model.CanUpdate ? "" : "disabled" %> name="Calval1_Manual" id="Calval1_Manual" value="<%=Model.Calibration1 %>" />
        <a id="Calval1_Num" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Maximum Reading","Maximum Reading")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class=" form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Calval3_Manual" id="Calval3_Manual" value="<%=Model.Calibration3 %>" />
        <a id="Calval3_Num" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Sensitivity","Sensitivity")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Sensitivity" id="Sensitivity" value="<%=Model.Calibration2 %>" />
        <a id="SensNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>


<script>

    //MobiScroll - Minor Interval
    $(function () {
        <% if (Model.CanUpdate)
                  { %>

        let arrayForSpinner = arrayBuilder(20, 30000, 20);
        createSpinnerModal("Calval1_Num", "Minor Interval", "Calval1_Manual", arrayForSpinner);

        let arrayForSpinner1 = arrayBuilder(100, 250, 10);
        createSpinnerModal("Calval3_Num", "Max Reading", "Calval3_Manual", arrayForSpinner1);

        let arrayForSpinner2 = arrayBuilder(1, 10, 1);
        createSpinnerModal("SensNum", "Sensitivity", "Sensitivity", arrayForSpinner2);

        <%}%>


        $('#Calval1_Manual').addClass("editField editFieldMedium");
        $('#Calval3_Manual').addClass("editField editFieldMedium");
        $('#Sensitivity').addClass("editField editFieldMedium");


        $("#Calval1_Manual").change(function () {
            if (isANumber($("#Calval1_Manual").val())) {
                if ($("#Calval1_Manual").val() < 20)
                    $("#Calval1_Manual").val(20);
                if ($("#Calval1_Manual").val() > 30000)
                    $("#Calval1_Manual").val(30000);
            } else {

                $("#Calval1_Manual").val(20);
            }
        });

        $("#Calval3_Manual").change(function () {
            if (isANumber($("#Calval3_Manual").val())) {
                if ($("#Calval3_Manual").val() < 100)
                    $("#Calval3_Manual").val(100);
                if ($("#Calval3_Manual").val() > 250)
                    $("#Calval3_Manual").val(250);
            } else {

                $("#Calval3_Manual").val(100);
            }
        });

        $("#Sensitivity").change(function () {
            if (isANumber($("#Sensitivity").val())) {
                if ($("#Sensitivity").val() < 1)
                    $("#Sensitivity").val(1);
                if ($("#Sensitivity").val() > 10)
                    $("#Sensitivity").val(10);
            } else {

                $("#Sensitivity").val(1);
            }
        });

    });


</script>
