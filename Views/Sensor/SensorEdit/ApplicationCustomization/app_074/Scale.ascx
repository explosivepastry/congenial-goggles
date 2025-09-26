<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />   
      
       <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|0 Volt Value:","0 Volt Value:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" id="lowValue" type="text" name="lowValue" value="<%:Monnit.ZeroToTenVolts.GetLowValue(Model.SensorID) %>" />
        </div>
    </div>
    
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|10 Volt Value:","10 Volt Value:")%>
        </div>
        <div class="col sensorEditFormInput">
              <input class="form-control" id="highValue" type="text" name="highValue" value="<%:Monnit.ZeroToTenVolts.GetHighValue(Model.SensorID) %>" />
        </div>
    </div>
    
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|Label:","Label:")%>
        </div>
        <div class="col sensorEditFormInput">
             <input class="form-control" id="label" type="text" name="label" value="<%:Monnit.ZeroToTenVolts.GetLabel(Model.SensorID) %>" />
        </div>
    </div>
    
    <div class="text-end">
        <input class="btn btn-primary" type="button" id="save" value="Save" />
        <div style="clear: both;"></div>
    </div>

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {



            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });


        });

    </script>
</form>