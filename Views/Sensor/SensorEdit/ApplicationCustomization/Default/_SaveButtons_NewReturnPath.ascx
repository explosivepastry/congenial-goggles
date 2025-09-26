<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%="" %>
<div class="form-group">
    <div class="bold col-sm-9 col-12" style="padding-bottom: 4px;" >
<%--        <%if (Model.GenerationType.ToUpper().Contains("GEN2") && new Version(Model.FirmwareVersion).Minor >= 23)
          {%>

        <a id="sensorSchedule" href="/Overview/SensorSchedule/<%:Model.SensorID%>">
            <button class="btn btn-primary btn-sm" type="button" value="<%: Html.TranslateTag("Schedule Sensor","Schedule Sensor")%>">
                <%: Html.TranslateTag("Schedule Sensor","Schedule Sensor")%>
            </button>
        </a>

        <%} %>--%>
        <%if (!Model.CanUpdate)
          {%>
        <span>

            <%if (MonnitSession.CustomerCan("Support_Advanced"))
              { %>
            <span class="pendingEditIconLeft pendingsvg" style="cursor: pointer; vertical-align:sub;" onclick="clearDirtyFlags(<%: Model.SensorID %>);"><%=Html.GetThemedSVG("Pending_Update") %></span>
            <%}
              else
              { %>
            <span class="pendingEditIconLeft pendingsvg" style="vertical-align:sub;"><%=Html.GetThemedSVG("Pending_Update") %></span>
            <%} %>
            &nbsp;<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtons_NewReturnPath|Fields waiting to be written to sensor are not available for edit until transaction is complete.","Fields waiting to be written to sensor are not available for edit until transaction is complete.")%>
        </span>
        <%}  %>
    </div>

    <div class="bold col-sm-3 col-12 d-flex justify-content-end">
<%--        <button class="btn btn-secondary" type="button" id="DefaultsCalibrate" <%=Model.CanUpdate ? "" : "disabled" %>  value="<%: Html.TranslateTag("Default","Default")%>">
            <%: Html.TranslateTag("Default","Default")%>
        </button>
        &nbsp;--%>

        <button class="btn btn-primary" type="button" id="save" <%=Model.CanUpdate ? "" : "disabled" %> onclick="checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="<%: Html.TranslateTag("Save","Save")%>">
            <%: Html.TranslateTag("Save","Save")%>
        </button>

        <button class="btn btn-primary" id="saving" style="display:none;" type="button" disabled >
          <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" ></span>
          <%: Html.TranslateTag("Saving...","Saving...")%>
        </button>
    </div>
</div>

<script type="text/javascript">
<%--    var DefaultYouSure = "<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtons_NewReturnPath|Are you sure you want to reset this sensor to defaults","Are you sure you want to reset this sensor to defaults?")%>";

    $(function () {      
        $('#simpleEdit_<%:Model.SensorID %>').attr("action","/Overview/SensorEditNewReturn/<%:Model.SensorID %>");
        $('#DefaultsCalibrate').on("click", function () {

            var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            if(confirm(DefaultYouSure)){
                $.get('/Overview/SensorDefault/'+  SensorID,function(result)
                { 
                    pID.html(result);

                    setTimeout(function () {
                        window.location.href = window.location.href;
                    }, 2000);
                });    
            }
        });
    });--%>

    function clearDirtyFlags(sensorID) {
        $.post("/Overview/SetSensorActive/" + sensorID, function (data) {
            window.location.href = window.location.href;
        });
    }

</script>
