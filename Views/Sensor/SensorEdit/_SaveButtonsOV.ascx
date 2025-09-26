<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="form-group">   
   <div class="bold col-md-9 col-sm-9 col-xs-12">
    <%if (!Model.CanUpdate) {%>
	    
        	<span><%: Html.TranslateTag("Overview/SensorEdit|Fields waiting to be written to sensor are not available for edit until transaction is complete.","Fields waiting to be written to sensor are not available for edit until transaction is complete.")%></span>
		
    <%} else {
        if(MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")) && MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Configure_Multiple")) && MonnitSession.AccountCan("sensor_multiple_edit")){ %>
            <a href="/Sensor/MultiEdit?id=<%:Model.SensorID %>" class="btn btn-grey"><%: Html.TranslateTag("Overview/SensorEdit|Use these settings for other sensors","Use these settings for other sensors")%></a>
    <%  }
      } %>
	</div>	
	<div class="bold col-md-3 col-sm-3 col-xs-12">
		<input class="btn btn-primary" type="button" onclick="checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="<%: Html.TranslateTag("Save","Save")%>" />
		<input type="button" id="DefaultsCalibrate" class="btn btn-dark" value="<%: Html.TranslateTag("Default", "Default")%>" />
		<div style="clear: both;"></div>
	</div>
</div>	

<script>
    var DefaultSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";
    $(function(){
        $('#DefaultsCalibrate').on("click", function () {

            var SensorID = <%: Model.SensorID%>;
                var returnUrl = $('#returns').val();
                var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            if(confirm(DefaultSure)){
                   
                    $.get('/Sensor/Default/'+  SensorID,function(result)
                    { 
                        pID.html(result); 
                    });    
                }
            });
        });
</script>