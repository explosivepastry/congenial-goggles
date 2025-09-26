<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div id="button" class="buttons">
    
    <%if (!Model.CanUpdate) {%>
        <span>fields waiting to be written to sensor are not available for edit until transaction is complete.</span>
    <%} else {
          if (MonnitSession.AccountCan("sensor_multiple_edit") && MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")) && MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Configure_Multiple")) )
          { %>
            <span style="float:left; margin: 15px 0px 0px 25px;"><a style="margin: -3px;" href="/Sensor/MultiEdit?id=<%:Model.SensorID %>">Use these settings for other sensors</a></span>
    <%  }
      } %>
    <input class="bluebutton" type="button" onclick="checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="Save" />
    <input type="button" id="DefaultsCalibrate" class="greybutton" style="float: none;" value="Default" />
    <div style="clear: both;"></div>
</div>
<script>
    var DefaultYouSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";
    $(function(){
        $('#DefaultsCalibrate').on("click", function () {

            var SensorID = <%: Model.SensorID%>;
                var returnUrl = $('#returns').val();
                var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
                if(confirm(DefaultYouSure)){
                   
                    $.get('/Sensor/Default/'+  SensorID,function(result)
                    { 
                        pID.html(result); 
                    });    
                }
            });
        });
</script>