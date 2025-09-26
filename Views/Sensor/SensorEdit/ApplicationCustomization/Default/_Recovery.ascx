<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% if (!Model.IsWiFiSensor && !Model.IsPoESensor && !Model.IsLTESensor)
   { %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_Recovery|Failed transmissions before link mode","Failed transmissions before link mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled" %> id="Recovery" name="Recovery" value="<%=Model.Recovery %>" />
        <a id="recoNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Recovery)%>
    </div>
</div>

<script type="text/javascript">

    $(function () {

        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner = arrayBuilder(0, 10, 1);
        createSpinnerModal("recoNum", "Failed Transmissions", "Recovery", arrayForSpinner);

        <%}%>

        $("#Recovery").change(function () {
            if (isANumber($("#Recovery").val())) {
                if ($("#Recovery").val() < 0)
                    $("#Recovery").val(0);
                if ($("#Recovery").val() > 250)
                    $("#Recovery").val(250)

            }
            else {
                $("#Recovery").val(2);

            }
        });

    });
</script>

<% } %>