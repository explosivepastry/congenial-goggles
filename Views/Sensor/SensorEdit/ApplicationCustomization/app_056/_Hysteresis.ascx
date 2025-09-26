<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%   
    double Hyst = Gas_H2S.GetGasHysteresis(Model);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Aware State Buffer","Aware State Buffer")%>  (ppm)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<script type="text/javascript">

    //MobiScroll
    $(function () {
          <% if (Model.CanUpdate)
    { %>

        let arrayForSpinnerWholeNumber = arrayBuilder(0, 10, 1);

        let arrayForSpinnerDecimal = [".00",".10",".20",".30",".40",".50",".60",".70",".80",".90"];
        createSpinnerModal("hystNum", "ppm", "Hysteresis_Manual", arrayForSpinnerWholeNumber, null, arrayForSpinnerDecimal);

        <%}%>

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > 10)
                    $("#Hysteresis_Manual").val(10)
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
            }
        });
    });
</script>
