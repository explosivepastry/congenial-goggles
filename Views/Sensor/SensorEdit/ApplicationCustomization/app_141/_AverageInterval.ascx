<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string label = "";

    MonnitApplicationBase.ProfileLabelForScale(Model, out label);

    double intervalVal = (Model.Calibration3 / 1000d);
    if (new Version(Model.FirmwareVersion) >= new Version("14.26.17.10"))
    {
        intervalVal = Model.Calibration3;
    }

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Averaging Interval", "Averaging Interval")%> (Seconds)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="avgInterval" id="avgInterval" value="<%=intervalVal%>" />
        <a id="avgIntNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>


<script type="text/javascript">
    $('#avgInterval').addClass("editField editFieldMedium");

    $(function () {

        <% if (Model.CanUpdate) { %>

        const arrayForSpinner = arrayBuilder(1, 30, 1);
        createSpinnerModal("avgIntNum", "Seconds", "avgInterval", arrayForSpinner);

        <%}%>

        $("#avgInterval").change(function ()
        {
            if (isANumber($("#avgInterval").val())){
                if ($("#avgInterval").val() < 1)
                    $("#avgInterval").val(1);

                if ($("#avgInterval").val() > 30)
                    $("#avgInterval").val(30)
                setAproxTime();
            }
            else
            {
                $("#avgInterval").val(<%: intervalVal%>);
        }
        });

    });
</script>