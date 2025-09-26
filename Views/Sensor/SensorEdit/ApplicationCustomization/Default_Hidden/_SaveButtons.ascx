<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="form-group">
    <div class="col-12 dfac text-end">
        <button class="btn btn-primary" type="button" <%=Model.CanUpdate ? "" : "disabled" %> onclick="checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="<%: Html.TranslateTag("Save","Save")%>">
            <%: Html.TranslateTag("Save","Save")%>
        </button>
        &nbsp;
<%--        <button class="btn btn-secondary" type="button" id="DefaultsCalibrate" <%=Model.CanUpdate ? "" : "disabled" %>  value="<%: Html.TranslateTag("Default", "Default")%>">
            <%: Html.TranslateTag("Default","Default")%>
        </button>--%>
        <div style="clear: both;"></div>
    </div>
</div>

<script type="text/javascript">
    var DefaultYouSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default_Hidden/_SaveButtons|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";

    $(function(){               
     
<%--        $('#DefaultsCalibrate').on("click", function () {

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
        });--%>
    });

    function clearDirtyFlags(sensorID) {
        $.post("/Overview/SetSensorActive/" + sensorID, function (data) {
            window.location.href = window.location.href;

        });
    }
</script>
