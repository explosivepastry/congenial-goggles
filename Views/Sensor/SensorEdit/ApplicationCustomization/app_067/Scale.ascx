<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />
    <%
        string attVal = string.Empty;
        bool showDepth = false;
        double tankDepth = 0;
        foreach (SensorAttribute sensAttr in SensorAttribute.LoadBySensorID(Model.SensorID))
        {
            if (sensAttr.Name == "DistanceUnits")
                attVal = sensAttr.Value.ToString();

            if (sensAttr.Name == "ShowDepth")
                showDepth = sensAttr.Value.ToBool();

            if (sensAttr.Name == "TankDepth")
                tankDepth = sensAttr.Value.ToDouble();
        }

        switch (attVal)
        {
            case "Centimeter":
                break;
            case "Meter":
                tankDepth = tankDepth * 0.01;
                break;
            case "Inch":
                tankDepth = tankDepth * 0.393701;
                break;
            case "Feet":
                tankDepth = tankDepth * 0.0328084;
                break;
            case "Yard":
                tankDepth = tankDepth * 0.0109361;
                break;
        }

        tankDepth = (Math.Round(tankDepth * 1000, 1) / 1000);
        
    %>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_067|Choose Measurement Scale:","Choose Measurement Scale:")%>
        </div>
        <div class="col sensorEditFormInput">
            <select id="MeasurementScale" name="MeasurementScale" class="form-select">
                <option value="cm" <%: attVal == "Centimeter" ? "selected" : "" %>>Centimeter</option>
                <option value="in" <%: attVal == "Inch" ? "selected" : "" %>>Inch</option>
                <option value="ft" <%: attVal == "Feet" ? "selected" : "" %>>Feet</option>
                <option value="yrd" <%: attVal == "Yard" ? "selected" : "" %>>Yard</option>
                <option value="M" <%: attVal == "Meter" ? "selected" : "" %>>Meter</option>
            </select>
        </div>
    </div>
    <div class="clear"></div>
    <br />
       <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_067|Display:","Display:")%>
        </div>
        <div class="col sensorEditFormInput">
             <select id="ShowDepth" name="ShowDepth" class="form-select">
                <option value="true" <%: showDepth == true ? "selected" : "" %>>Depth</option>
                <option value="false" <%: showDepth == false ? "selected" : "" %>>Distance</option>
            </select>
        </div>
    </div>
    <div class="clear"></div>
    <br />

      <div class="row sensorEditForm" id="hideMe" style="display: none;">
        <div class="col-12 col-md-3">
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_067|Depth:","Depth:")%>
        </div>
        <div class="col sensorEditFormInput">
             <input type="text" value="<%=tankDepth %>" id="TankDepth" name="TankDepth" style="min-width: 100px; float: left;" class="form-control" />
            <div style="float: left; padding-left: 10px;" id="distlabel"><%=attVal %></div>
        </div>
    </div>


    <script>
     
        $(document).ready(function () {




            var scale = $('#MeasurementScale').val();

            if ($('#ShowDepth').val() == 'true') {
                $("#hideMe").show();
            }

            $('#MeasurementScale').change(function (e) {

                var depth = $('#TankDepth').val();
                var Toscale = $('#MeasurementScale').val();
                $('#distlabel').html(Toscale);

                var cms = MakeCentimeter(depth, scale);
                var convertedLength = ConvertCentimeterTo(cms, Toscale);
                scale = Toscale;

                $('#TankDepth').val(convertedLength);

            });



            $('#ShowDepth').change(function (e) {
                e.preventDefault();
                if ($('#ShowDepth').val() == 'true') {
                    $("#hideMe").show();
                } else {

                    $("#hideMe").hide();
                }

            });




        });

        function MakeCentimeter(length, units) {


            var Centimeters = parseFloat(length);

            switch (units) {
                case 'cm':
                    break;
                case 'M':
                    Centimeters = length * 100;
                    break;
                case 'in':
                    Centimeters = length * 2.54;
                    break;
                case 'ft':
                    Centimeters = length * 30.48;
                    break;
                case 'yrd':
                    Centimeters = length * 91.44;
                    break;
            }
            Centimeters = (Math.round(Centimeters * 1000) / 1000);
            return Centimeters

        }


        function ConvertCentimeterTo(cm, ToUnits) {

            var length = parseFloat(cm);

            switch (ToUnits) {
                case 'cm':

                    break;
                case 'M':
                    length = cm * .01;
                    break;
                case 'in':
                    length = cm * 0.393701;
                    break;
                case 'ft':
                    length = cm * 0.0328084;
                    break;
                case 'yrd':
                    length = cm * 0.0109361;
                    break;
            }

            length = (Math.round(length * 1000) / 1000).toFixed(1);
            return length

        }


    </script>


    <div class="" style="text-align: right;">
        <input class="btn btn-primary" type="button" id="save" value="Save" />
        <div style="clear: both;"></div>
    </div>

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {

            //$('#TempScale').mobiscroll().select({
            //    theme: 'ios',
            //    display: popLocation,
            //    onSet: function (event, inst) {
            //        $('#save').click();

            //    }

            //});

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });


        });

    </script>
</form>
