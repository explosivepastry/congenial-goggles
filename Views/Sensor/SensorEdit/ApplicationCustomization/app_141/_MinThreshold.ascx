<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        //Get Pressure label for profile
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);

        //Get the Hyst, Max, Min Values for Pascal
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Below","Below")%> (Amps)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
       
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>
    $('#MinimumThreshold_Manual').addClass("editField editFieldMedium");

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        createSpinnerModal("minThreshNum", "Amps", "MinimumThreshold_Manual", [1, 2, 3, 4, 5, 6, 7], null, [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"]);

<%}%>
         

        $('#MinimumThreshold_Manual').change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($('#MinimumThreshold_Manual').val() < 0)
                    $('#MinimumThreshold_Manual').val(0);

                if ($('#MinimumThreshold_Manual').val() > 7.5)
                    $('#MinimumThreshold_Manual').val(7.5);

                if (Number($("#MinimumThreshold_Manual").val()) > Number($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val($("#MaximumThreshold_Manual").val());
            }
            else
                $('#MinimumThreshold_Manual').val(<%: Min%>);

         });

    });
</script>
<%} %>