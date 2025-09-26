<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 

    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string negChange = "";
        string posChange = "";
        negChange = ResistanceDelta.GetNegativeChange(Model).ToString();
        posChange = ResistanceDelta.GetPositiveChange(Model).ToString();

%>

<h5 style="font-size: 1rem; font-weight: 600; margin-top: 0.5rem; color: #515356 !important; padding-left: 6px;">Resistance Differential</h5>

<%----- Negative -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_099|Negative Change","Negative Change")%> %
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" <%=Model.CanUpdate ? "" : "disabled" %> class="form-control" name="NegativeChange_Manual" id="NegativeChange_Manual" value="<%=negChange %>" />
        <a id="negativeChangeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<%----- Positive -----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Positive Change","Positive Change")%> %
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="PositiveChange_Manual" id="PositiveChange_Manual" value="<%=posChange %>" />

        <a id="positiveChangeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>



<script>
    $(function () {
        <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(0, 5000, 100);
        createSpinnerModal("negativeChangeNum", "Negative Change", "NegativeChange_Manual", arrayForSpinner);

        <%}%>

        $("#NegativeChange_Manual").addClass('editField editFieldSmall');

        $("#NegativeChange_Manual").change(function () {
            if (isANumber($("#NegativeChange_Manual").val())) {
                if ($("#NegativeChange_Manual").val() < 0)
                    $("#NegativeChange_Manual").val(0);
                if ($("#NegativeChange_Manual").val() > 5000)
                    $("#NegativeChange_Manual").val(5000);
            }
            else {
                $("#NegativeChange_Manual").val(<%: negChange%>);
            }
        });
    });

    $(function () {
        <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner1 = arrayBuilder(0, 5000, 50);
        createSpinnerModal("positiveChangeNum", "Positive Change", "PositiveChange_Manual", arrayForSpinner1);

        <%}%>

        $("#PositiveChange_Manual").addClass('editField editFieldSmall');

        $("#PositiveChange_Manual").change(function () {
            if (isANumber($("#PositiveChange_Manual").val())) {
                if ($("#PositiveChange_Manual").val() < 0)
                    $("#PositiveChange_Manual").val(0);
                if ($("#PositiveChange_Manual").val() > 5000)
                    $("#PositiveChange_Manual").val(5000);
            }
            else {
                $("#PositiveChange_Manual").val(<%: posChange%>);
            }
        });
    });

</script>
<%} %>