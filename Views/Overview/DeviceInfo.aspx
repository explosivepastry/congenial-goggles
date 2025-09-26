<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Device Info
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid mt-4">
        <div class="x_panel shadow-sm rounded">
            <div class="x_title">
                <h2><%: Html.TranslateTag("Overview/DeviceInfo|Enter Device Information","Enter Device Information")%></h2>
                <div class="clearfix"></div>
            </div>

            <div class="x_content">
                <div id="fullForm">
                    <div class="form-group">
                        <div class="bold col-sm-6 col-12">
                            <h2><%: Html.TranslateTag("Overview/DeviceInfo|Please enter your Device ID and Checkcode","Please enter your Device ID and Checkcode")%>.</h2>
                        </div>
                        <div class="col-sm-6 col-12 mdBox">
                        </div>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <%: Html.TranslateTag("Overview/DeviceInfo|Device ID","Device ID")%>
                        </div>
                        <div class="col sensorEditFormInput">
                            <input id="DeviceID" name="DeviceID" type="text" class="form-control" value="">
                        </div>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <%: Html.TranslateTag("Overview/DeviceInfo|Device Code","Device Code")%>
                        </div>
                        <div class="col sensorEditFormInput">
                            <input id="DeviceCode" name="DeviceCode" type="text" class="form-control" value="">
                        </div>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="form-group">
                        <div class="bold col-md-3 col-12"></div>
                        <div class="col-sm-9 col-12 mdBox">
                            <input type="button" value="<%: Html.TranslateTag("Overview/DeviceInfo|Look Up Device","Look Up Device")%>" id="lookupDevice" class="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="x_panel shadow-sm rounded mt-4" style="display: none;" id="deviceResults"></div>
        </div>
    </div>

    <script type="text/javascript">

        $(function () {
            $('#DeviceID').focus();

            var applicationIDs = new Object();
            <%foreach (BaseApplication.eSensorApplication item in Enum.GetValues(typeof(BaseApplication.eSensorApplication)))
        {%>
            applicationIDs["<%:((int)item)%>"] = "<%:item%>";
            <%}%>

            $('#lookupDevice').click(function (e) {
                e.preventDefault();

                var id = $('#DeviceID').val();
                var code = $('#DeviceCode').val();

                //var obj = $(this);
                //var oldHtml = $(this).html();
                $(this).html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

                $.get('/json/LookUpSensor?sensorID=' + id + '&checkDigit=' + code, function (data) {

                    if (data.Result == 'Invalid SensorID') {

                        $.get('/json/LookUpGateway?gatewayID=' + id + '&checkDigit=' + code, function (gatewayData) {
                            if (gatewayData == "Check digit did not match.") {
                                $('#deviceResults').hide();
                                //$('#deviceResults').html("<h2>" + gatewayData + "</h2>");
                            } else {
                                var jsonGateway = gatewayData.Result;
                                var deviceHtml = "<b><div class=col-md-6> <div class=x_title><%: Html.TranslateTag("Overview/DeviceInfo|Gateway Info","Gateway Info")%></div></b> <div class=x_content>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Gateway ID","GatewayID")%></div></b>";
                                deviceHtml += "<div class=col-md-6>" + jsonGateway.GatewayID + "</div>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Name","Name")%></div></b>";
                                deviceHtml += "<div class=col-md-6>" + jsonGateway.GatewayName + "</div>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Radio Band","Radio Band")%></div></b>";
                                deviceHtml += "<div class=col-md-6>" + jsonGateway.RadioBand + "</div>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|APN Firmware Version","APN Firmware Version")%></div></b>";
                                deviceHtml += "<div class=col-md-6>" + jsonGateway.APNFirmwareVersion + "</div>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Gateway Firmware Version","Gateway Firmware Version")%></div></b>";
                                deviceHtml += "<div class=col-md-6>" + jsonGateway.GatewayFirmwareVersion + "</div>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Mac Address","Mac Address")%></div></b>";
                                deviceHtml += "<div class=col-md-6>" + jsonGateway.MacAddress + "</div>";
                                deviceHtml += "</div></div>";
                                $('#deviceResults').show();
                                $('#deviceResults').html(deviceHtml);
                            }
                        });
                    } else {
                        var jsonSensor = data.Result;
                        if (jsonSensor.SensorTypeID == '6' || jsonSensor.SensorTypeID == '4') { // 6 == PoE; 4 == WiFi
                            $.get('/json/LookUpWiFiGateway?sensorID=' + id + '&checkDigit=' + code, function (poeData) {
                                if (poeData.Result == "Check digit did not match.") {
                                    $('#deviceResults').hide();
                                    //$('#deviceResults').html("<h2>" + poeData + "</h2>");
                                } else {
                                    var jsonPoe = poeData.Result;

                                    var deviceHtml = "<b><div class=col-md-6> <div class=x_title><%: Html.TranslateTag("Overview/DeviceInfo|Sensor Info","Sensor Info")%></div></b> <div class=x_content>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Application","Application")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + applicationIDs[jsonSensor.ApplicationID] + "</div>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Sensor ID","Sensor ID")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + jsonSensor.SensorID + "</div>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Name","Name")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + jsonSensor.SensorName + "</div>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Radio Band","Radio Band")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + jsonSensor.RadioBand + "</div>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Firmware Version","Firmware Version")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + jsonSensor.FirmwareVersion + "</div>";
                                    deviceHtml += "</div></div>";

                                    deviceHtml += "<b><div class=col-md-6> <div class=x_title><%: Html.TranslateTag("Overview/DeviceInfo|Gateway Info","Gateway Info")%></div></b> <div class=x_content>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Gateway ID","Gateway ID")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + jsonPoe.GatewayID + "</div>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Name","Name")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + jsonPoe.GatewayName + "</div>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Radio Band","Radio Band")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + jsonPoe.RadioBand + "</div>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|APN Firmware Version","APN Firmware Version")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + jsonPoe.APNFirmwareVersion + "</div>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Gateway Firmware Version","Gateway Firmware Version ")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + jsonPoe.GatewayFirmwareVersion + "</div>";
                                    deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Mac Address","Mac Address")%></div></b>";
                                    deviceHtml += "<div class=col-md-6>" + jsonPoe.MacAddress + "</div>";
                                    deviceHtml += "</div></div>";
                                    $('#deviceResults').show();
                                    $('#deviceResults').html(deviceHtml);
                                }
                            });
                        } else {

                            if (data.Result == "Check digit did not match.") {
                                showAlertModal(data.Result);
                            }
                            else {
                                var deviceHtml = "<b><div class=col-md-6> <div class=x_title><%: Html.TranslateTag("Overview/DeviceInfo|Sensor Info","Sensor Info")%></div></b> <div class=x_content>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Application","Application")%></div></b>";
                                deviceHtml += "<div class=col-md-6>" + applicationIDs[jsonSensor.ApplicationID] + "</div>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Sensor ID","SensorID")%></div></b>";
                                deviceHtml += "<div class=col-md-6>" + jsonSensor.SensorID + "</div>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Name","Name")%></div></b>";
                                deviceHtml += "<div class=col-md-6>" + jsonSensor.SensorName + "</div>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Radio Band","Radio Band")%></div></b>";
                                deviceHtml += "<div class=col-md-6>" + jsonSensor.RadioBand + "</div>";
                                deviceHtml += "<b><div class=col-md-6><%: Html.TranslateTag("Overview/DeviceInfo|Firmware Version","Firmware Version")%></div></b> ";
                                deviceHtml += "<div class=col-md-6>" + jsonSensor.FirmwareVersion + "</div>";
                                deviceHtml += "</div></div>";

                                $('#deviceResults').show();
                                $('#deviceResults').html(deviceHtml);
                            }
                        }
                    }
                });
            });
            obj.html(oldHtml);
        });

    </script>

</asp:Content>
