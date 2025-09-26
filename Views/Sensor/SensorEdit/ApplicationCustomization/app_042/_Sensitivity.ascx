<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    int Sensitivity = 3;
    if (Model.Calibration1 >= 4000)
    {
        Sensitivity = 4;
    }
    else if (Model.Calibration1 <= 500)
    {
        Sensitivity = 1;
    }
    else if (Model.Calibration1 <= 1000)
    {
        Sensitivity = 2;
    }
                  
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Sensitivity","Sensitivity")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="Sensitivity" value="<%=Sensitivity %>" />
        <a id="sensNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <span id="AproxAssessmentTime" style="font-size: 11px;"></span>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>

<script type="text/javascript">

    //NumberPad
    $(function () {

        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner = arrayBuilder(1, 4, 1)
        createSpinnerModal("sensNum", "Sensitivity", "Sensitivity", arrayForSpinner);

        <%}%>


        $("#Sensitivity").change(function () {
            if (isANumber($("#Sensitivity").val())) {
                if ($("#Sensitivity").val() < 1)
                    $("#Sensitivity").val(1);
                if ($("#Sensitivity").val() > 4)
                    $("#Sensitivity").val(4)
            }
            else {
                $("#Sensitivity").val(<%: Sensitivity %>);
            }
        });

    });
</script>

