<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        //Get Pressure label for profile
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);

        //Get the Hyst, Max, Min Values for Volts
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Above","Above")%> (Amps)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>
    $('#MaximumThreshold_Manual').addClass("editField editFieldMedium");

    $(function () {

        <% if (Model.CanUpdate)
           { %>

        createSpinnerModal("maxThreshNum", "Amps", "MaximumThreshold_Manual", [1, 2, 3, 4, 5, 6, 7], null, [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"]);

<%}%>
        $('#MaximumThreshold_Manual').change(function () {

            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($('#MaximumThreshold_Manual').val() < 0)
                    $('#MaximumThreshold_Manual').val(0)

                if ($('#MaximumThreshold_Manual').val() > 7.5)
                    $('#MaximumThreshold_Manual').val(7.5);

                if (Number($("#MaximumThreshold_Manual").val()) < Number($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val($("#MinimumThreshold_Manual").val());

            }
            else
                $('#MaximumThreshold_Manual').val(<%: Max%>);

        });

    
    });
        
</script>
<%} %>