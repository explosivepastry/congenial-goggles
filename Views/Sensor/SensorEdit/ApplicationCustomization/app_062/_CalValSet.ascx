<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Minor Interval","Minor Interval")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class=" form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Calval1_Manual" id="Calval1_Manual" value="<%=Model.Calibration1 %>" />
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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Trip point","Trip point")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class=" form-control" type="number"<%=Model.CanUpdate ? "" : "disabled" %> name="Calval2_Manual" id="Calval2_Manual" value="<%=Model.Calibration2 %>" />
        <a id="Calval2_Num" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>


<script>
    $('#Calval1_Manual').addClass("editField editFieldMedium");
    $('#Calval3_Manual').addClass("editField editFieldMedium");
    $('#Calval2_Manual').addClass("editField editFieldMedium");

    $(function () {
        <% if (Model.CanUpdate)
                  { %>

        let arrayForSpinner = arrayBuilder(20, 30000, 20);
        createSpinnerModal("Calval1_Num", "Minor Interval", "Calval1_Manual", arrayForSpinner);

        let arrayForSpinner1 = arrayBuilder(10, 250, 10);
        createSpinnerModal("Calval3_Num", "Max Reading", "Calval3_Manual", arrayForSpinner1);

        let arrayForSpinner2 = arrayBuilder(1000, 50000, 100);
        createSpinnerModal("Calval2_Num", "Max Reading", "Calval2_Manual", arrayForSpinner2);

        <%}%>


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
                if ($("#Calval3_Manual").val() < 10)
                    $("#Calval3_Manual").val(10);
                if ($("#Calval3_Manual").val() > 250)
                    $("#Calval3_Manual").val(250);
            } else {
                $("#Calval3_Manual").val(10);
            }
        });


        $("#Calval2_Manual").change(function () {
            if (isANumber($("#Calval2_Manual").val())) {
                if ($("#Calval2_Manual").val() < 100)
                    $("#Calval2_Manual").val(100);
                if ($("#Calval2_Manual").val() > 50000)
                    $("#Calval2_Manual").val(50000);
            } else {
                $("#Calval2_Manual").val(100);
            }
        });


    });
</script>
