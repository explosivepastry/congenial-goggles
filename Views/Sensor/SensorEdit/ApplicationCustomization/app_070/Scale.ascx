<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />


    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|0 Ohms Value:","0 Ohms Value:")%>
        </div>
        <div class="col sensorEditFormInput">
             <input class="form-control" type="text" id="lowValue" name="lowValue" value="<%:Monnit.Resistance.GetLowValue(Model.SensorID) %>" />
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%if(Model.GenerationType.ToLower().Contains("gen2"))
                { %>
               <%:Html.TranslateTag("Sensor/ApplicationCustomization/app_070|250000 Ohms Value:","250000 Ohms Value:")%>
            <%}else{ %>
            <%:Html.TranslateTag("Sensor/ApplicationCustomization/app_070|145000 Ohms Value:","145000 Ohms Value:")%>
            <%} %>
        </div>
        <div class="col sensorEditFormInput">
             <input class="form-control" type="text" id="highValue" name="highValue" value="<%:Monnit.Resistance.GetHighValue(Model.SensorID) %>" />
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|Label:","Label:")%>
        </div>
        <div class="col sensorEditFormInput">
             <input class="form-control" type="text" id="label" name="label" value="<%:Monnit.Resistance.GetLabel(Model.SensorID) %>" />
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
