<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 

        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

        if (Model.MaximumThreshold == 4294967295)
            Model.MaximumThreshold = 15000;
 
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_120|Above","Above")%> (<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Amps","Amps")%>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>

    //MobiScroll
    $(function () {
        var MaxThresMinVal = 0;
        var MaxThresMaxVal = 600;

        <% if (Model.CanUpdate)
           { %>

        const arrayForSpinner = arrayBuilder(MaxThresMinVal, MaxThresMaxVal, 10);
        createSpinnerModal("maxThreshNum", "Amps", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())){
                if ($("#MaximumThreshold_Manual").val() < MaxThresMinVal)
                    $("#MaximumThreshold_Manual").val(MaxThresMinVal);
                if ($("#MaximumThreshold_Manual").val() > MaxThresMaxVal)
                    $("#MaximumThreshold_Manual").val(MaxThresMaxVal);

                if (Number($('#MaximumThreshold_Manual').val()) <= Number($('#MinimumThreshold_Manual').val()))
                    $('#MaximumThreshold_Manual').val((Number($('#MinimumThreshold_Manual').val()) + 1));
            }else{
                $("#MaximumThreshold_Manual").val(<%: Max%>);
            }
        });

    });
</script>
