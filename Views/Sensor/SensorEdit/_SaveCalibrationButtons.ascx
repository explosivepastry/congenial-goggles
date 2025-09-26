<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="buttons">
        <input type="button" onclick="postForm($(this).closest('form'));" class="btn btn-primary btn-sm" value="Calibrate" />
        <input type="button" id="DefaultsCalibrate1" class="btn btn-secondary btn-sm" style="float: none;" value="Default" />
    </div>
    <script>
        var defaultSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to set your calibration back to default?")%>";
        $(function(){    
            $('#DefaultsCalibrate1').on("click", function () {
                
                var returnUrl = $('#returns').val();
                var SensorID = <%: Model.SensorID%>;
                var pID = $('#simpleCalibrate_'+SensorID).parent();               

                if (confirm(defaultSure))
                {                    
                    $.post("/Sensor/SetDefaultCalibration", { id: SensorID, url: returnUrl },function (data) 
                    {
                               pID.html(data);
                    });
                }
            }); 
        });
    </script>