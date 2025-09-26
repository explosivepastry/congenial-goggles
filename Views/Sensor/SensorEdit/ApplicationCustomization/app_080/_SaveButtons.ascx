<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row sensorEditForm">   
   <div class="col-12 col-md-3">
       <%if (!Model.CanUpdate) {%>
	    
        	 <span>

            <%if (MonnitSession.CustomerCan("Support_Advanced"))
              { %>
            <span class="pendingEditIconLeft pendingsvg" style="cursor: pointer; vertical-align:sub;" onclick="clearDirtyFlags(<%: Model.SensorID %>);"><%=Html.GetThemedSVG("Pending_Update") %></span>
            <%}
              else
              { %>
            <span class="pendingEditIconLeft pendingsvg" style="vertical-align:sub;"><%=Html.GetThemedSVG("Pending_Update") %></span>
            <%} %>
            &nbsp;<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fields waiting to be written to sensor are not available for edit until transaction is complete.","Fields waiting to be written to sensor are not available for edit until transaction is complete.")%>

        </span>
    <%}  %>
	</div>	
	<div class="col sensorEditFormInput">
		<input class="btn btn-primary" type="button" <%=Model.IsDirty ? "disabled" : "" %> onclick="checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="<%: Html.TranslateTag("Save","Save")%>" />
		<input class="btn btn-secondary" type="button"  value="<%: Html.TranslateTag("Reset Data Log", "Reset Data Log")%>" id="ResetDataLog" />
        <input type="button" id="DefaultsCalibrate" <%=Model.IsDirty ? "disabled" : "" %>  class="btn btn-dark" value="<%: Html.TranslateTag("Default", "Default")%>" />
		<div style="clear: both;"></div>
	</div>
</div>	

<script>
    var DefaultYouSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";
    var counterSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to reset the datalog on this sensor?")%>";
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

        // Broken
        // Overview/SensorEdit/11111180
        // /Sensor/SensorEdit/ApplicationCustomization/app_080/_SaveButtons.ascx
        // Runtime JS error
        // Uncaught ReferenceError: SensorID is not defined
        $('#ResetDataLog').on("click", function()
        {
            if(confirm(counterSure)){
                   
                $.post('/Sensor/Calibrate/',{ id: SensorID, url: returnUrl  },function(result)
                { 
                    pID.html(result); 
                });    
            }
        });

        });
</script>